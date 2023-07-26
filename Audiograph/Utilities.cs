using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Xml;
using Audiograph.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
// Audiograph Utilities module
// Contains general methods/variables for Audiograph

namespace Audiograph;

internal static class Utilities
{
    #region Vars

    public static List<string[]> apihistory = new(); // used for api call history
    public static List<string[]> scrobblehistory = new(); // used for scrobble history
    public static ushort progress; // progress bar percentage
    public static byte progressmultiplier; // progress bar
    public static bool stoploadexecution = true; // stops things from executing on form load
    public static string userlookup; // current user in user lookup
    public static string[] tracklookup = new string[3]; // 0 = track, 1 = artist, 2 = album
    public static string artistlookup; // current artist in artist lookup
    public static string[] albumlookup = new string[2]; // 0 = album, 1 = artist
    public static List<string[]> originalqueue = new(); // original queue for media
    public static bool addingqueue; // true when something is currently being added to the queue
    public static List<string[]> queueremoved = new(); // list of removed queue items when shuffle is activated
    public static int timezoneoffset; // unix time offset for time zone
    public static List<string[]> scrobbleindexdata = new(); // 0 = filename, 1 = title, 2 = artist, 3 = album

    public static bool
        newsong = true; // true when mediaplayer is playing a new song, false when the song has already been scrobbled

    #endregion

    #region Methods

    // callapi get request
    public static string CallAPI(string method, string user = "", string param1 = "", string param2 = "",
        string param3 = "", string param4 = "")
    {
        // url formatting
        var urldata = new StringBuilder();
        urldata.Append("http://ws.audioscrobbler.com/2.0/?method=" + method + "&"); // url and method
        if (!string.IsNullOrEmpty(user))
        {
            user = user.Replace("&", "%26"); // encode ampersands
            urldata.Append("user=" + user + "&"); // user
        }

        if (!string.IsNullOrEmpty(param1))
        {
            param1 = param1.Replace("&", "%26"); // encode ampersands
            urldata.Append(param1 + "&"); // param1
        }

        if (!string.IsNullOrEmpty(param2))
        {
            param2 = param2.Replace("&", "%26"); // encode ampersands
            urldata.Append(param2 + "&"); // param2
        }

        if (!string.IsNullOrEmpty(param3))
        {
            param3 = param3.Replace("&", "%26"); // encode ampersands
            urldata.Append(param3 + "&"); // param3
        }

        if (!string.IsNullOrEmpty(param4))
        {
            param4 = param4.Replace("&", "%26"); // encode ampersands
            urldata.Append(param4 + "&"); // param3
        }

        urldata.Append("api_key=" + Secrets.APIkey); // api key

        // initialization
        var API = new WebClient(); // webclient
        API.Headers.Add("user-agent", "Audiograph/INDEV"); // add user agent
        DateTime starttime = new(), endtime = new(); // timekeeping
        var milliseconds = new TimeSpan();
        var utf8 = new UTF8Encoding(); // encode
        string response; // response holder
        var errorholder = default(string); // holds ex.message

        // make request
        try
        {
            starttime = DateTime.Now; // starting time
            response = utf8.GetString(API.DownloadData(urldata.ToString()));
            endtime = DateTime.Now; // ending time
        }
        catch (Exception ex)
        {
            endtime = DateTime.Now;
            response = "CallAPI ERROR: " + ex.Message;
            errorholder = ex.Message;
        }

        // put in apilist
        milliseconds = endtime.Subtract(starttime);
        if (string.IsNullOrEmpty(errorholder))
            // if no error
            AddAPIHistory(false, method, ParseMetadata(response, "lfm status=").ToUpper(), milliseconds.Milliseconds,
                starttime, Thread.CurrentThread.Name);
        else
            // if error
            AddAPIHistory(false, method, errorholder, milliseconds.Milliseconds, starttime, Thread.CurrentThread.Name);

        return response;
    }

    // callapi post request
    public static string CallAPIAuth(string method, string param1 = "", string param2 = "", string param3 = "",
        string param4 = "")
    {
        // data formatting
        var postdata = new StringBuilder();
        postdata.Append("method=" + method + "&"); // method
        if (!string.IsNullOrEmpty(param1)) postdata.Append(param1 + "&"); // param1
        if (!string.IsNullOrEmpty(param2)) postdata.Append(param2 + "&"); // param2
        if (!string.IsNullOrEmpty(param3)) postdata.Append(param3 + "&"); // param3
        if (!string.IsNullOrEmpty(param4)) postdata.Append(param4 + "&"); // param3
        postdata.Append("sk=" + MySettingsProperty.Settings.SessionKey + "&"); // session key
        postdata.Append("api_key=" + Secrets.APIkey + "&"); // api key
        postdata.Append("api_sig=" + CreateSignature(method, param1, param2, param3, param4)); // api signature
        // encode as utf8
        var encoding = new UTF8Encoding();
        byte[] postbytes = encoding.GetBytes(postdata.ToString());

        // initialization
        DateTime starttime = default, endtime; // timekeeping
        var milliseconds = new TimeSpan(); // latency
        string responsestring;
        var errorholder = default(string);

        // configure request
        HttpWebRequest API = (HttpWebRequest)WebRequest.Create("http://ws.audioscrobbler.com/2.0/");
        API.Method = "POST";
        API.KeepAlive = true;
        API.UserAgent = "Audiograph/INDEV";
        API.ContentLength = postbytes.Length;
        API.ContentType = "application/x-www-form-urlencoded";

        try
        {
            // start time
            starttime = DateTime.Now;

            // send request
            var reqstream = API.GetRequestStream();
            reqstream.Write(postbytes, 0, postbytes.Length);
            reqstream.Close();

            // get response
            HttpWebResponse response = (HttpWebResponse)API.GetResponse();
            var responsereader = new StreamReader(response.GetResponseStream());
            responsestring = responsereader.ReadToEnd();

            // end time
            endtime = DateTime.Now;
        }
        catch (Exception ex)
        {
            endtime = DateTime.Now;
            responsestring = "CallAPIAuth ERROR: " + ex.Message;
            errorholder = ex.Message;
        }

        // put in apilist
        milliseconds = endtime.Subtract(starttime);
        if (string.IsNullOrEmpty(errorholder))
            // if no error
            AddAPIHistory(true, method, ParseMetadata(responsestring, "lfm status=").ToUpper(),
                milliseconds.Milliseconds, starttime, Thread.CurrentThread.Name);
        else
            // if error
            AddAPIHistory(true, method, errorholder, milliseconds.Milliseconds, starttime, Thread.CurrentThread.Name);

        return responsestring;
    }

    // converts an array of node names into their node text
    public static void ParseXML(string xml, string directory, uint num, ref string[] nodes)
    {
        // initialize xml parser
        var xmlParse = new XmlDocument();
        // check for errors which would be likely caused by invalid input
        try
        {
            xmlParse.LoadXml(xml);
        }
        catch (Exception ex)
        {
            nodes[0] = "ERROR: Likely caused by invalid input into ParseXML, check CallAPI. Message: " + ex.Message;
            return;
        }

        var nodelist = xmlParse.DocumentElement.SelectNodes(directory);

        // gets nodes from number down in list
        var node = nodelist[(int)num];

        // get node text
        for (byte nodecount = 0, loopTo = (byte)nodes.GetUpperBound(0); nodecount <= loopTo; nodecount++)
            try // error detection
            {
                nodes[nodecount] = node.SelectSingleNode(nodes[nodecount]).InnerText; // get node text
            }
            catch (Exception ex)
            {
                nodes[nodecount] = "ERROR: " + ex.Message;
            } // display error
    }

    // parse xml to find image by quality, regular parsexml is not set up to do this
    public static string ParseImage(string xml, string directory, byte quality)
    {
        // initialize xml parser
        var xmlParse = new XmlDocument();
        // check for errors which would be likely caused by invalid input
        try
        {
            xmlParse.LoadXml(xml);
        }
        catch (Exception ex)
        {
            return "ERROR: Likely caused by invalid input into ParseXML, check CallAPI. Message: " + ex.Message;
        }

        var nodelist = xmlParse.DocumentElement.SelectNodes(directory);

        // gets nodes from number down in list
        var node = nodelist[quality];

        string strout;
        try
        {
            strout = node.InnerText;
        }
        catch (Exception ex)
        {
            strout = "ERROR: " + ex.Message;
        }

        return strout;
    }

    // alternate parseimage for multiple tracks with multiple nodes
    public static string ParseImage(string xml, string directory, byte count, byte quality)
    {
        // initialize xml parser
        var xmlParse = new XmlDocument();
        // check for errors which would be likely caused by invalid input
        try
        {
            xmlParse.LoadXml(xml);
        }
        catch (Exception ex)
        {
            return "ERROR: Likely caused by invalid input into ParseXML, check CallAPI. Message: " + ex.Message;
        }

        var nodelist = xmlParse.DocumentElement.SelectNodes(directory);

        // gets nodes from number down in list
        XmlNodeList imagelist;
        try
        {
            imagelist = nodelist[count].ChildNodes;
        }
        catch (Exception ex)
        {
            return "ERROR: " + ex.Message;
        }

        // gets image node from number down
        var imagenode = imagelist[quality];

        string strout;
        try
        {
            strout = imagenode.InnerText;
        }
        catch (Exception ex)
        {
            strout = "ERROR: " + ex.Message;
        }

        return strout;
    }

    // manually parse xml to return metadata
    public static string ParseMetadata(string xml, string metadata, uint count = 1U)
    {
        if (xml.Contains(metadata)) // proceed if xml input contains specified metadata
        {
            var instances = new List<string>();
            int start = 1;
            int metadatapos;
            uint firstquotpos;
            uint secondquotpos;

            try
            {
                while (instances.Count != count)
                {
                    metadatapos = Strings.InStr(start, xml, metadata); // get the pos of the beginning of the metadata
                    if (metadatapos > -1) // if instr has found new instance
                    {
                        firstquotpos =
                            (uint)Strings.InStr(metadatapos, xml,
                                Conversions.ToString('"')); // get the pos of the first quotation mark
                        secondquotpos =
                            (uint)Strings.InStr((int)(firstquotpos + 1L), xml,
                                Conversions.ToString('"')); // get the pos of the second quotation mark
                        instances.Add(xml.Substring((int)firstquotpos,
                            (int)(secondquotpos - firstquotpos - 1L))); // add metadata to list
                        start = (int)secondquotpos;
                    }
                }

                return instances[(int)(count - 1L)];
            }
            catch (Exception ex)
            {
                return "ERROR: ParseMetadata cannot locate specified metadata.";
            }
        }

        return "ERROR: ParseMetadata cannot locate specified metadata.";
    }

    // convert date to unix time
    public static uint DateToUnix(DateTime datein)
    {
        return (uint)Math.Round((datein - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
    }

    public static DateTime UnixToDate(uint unixtime)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(unixtime);
    }

    public static uint GetCurrentUTC()
    {
        return (uint)(DateToUnix(DateTime.Now) - timezoneoffset);
    }

    public static string Hash(string input)
    {
        // dim stuff
        var md5 = new MD5CryptoServiceProvider();
        var encoding = new UTF8Encoding();
        byte[] bytestohash = encoding.GetBytes(input);
        var result = default(string);

        // hash
        bytestohash = md5.ComputeHash(bytestohash);
        foreach (byte b in bytestohash)
            result += b.ToString("x2");

        return result;
    }

    // create api_sig
    public static string CreateSignature(string method, string param1 = "", string param2 = "", string param3 = "",
        string param4 = "", bool sk = true)
    {
        // formatting
        param1 = param1.Replace("&", string.Empty);
        param2 = param2.Replace("&", string.Empty);
        param3 = param3.Replace("&", string.Empty);
        param4 = param4.Replace("&", string.Empty);
        param1 = param1.Replace("=", string.Empty);
        param2 = param2.Replace("=", string.Empty);
        param3 = param3.Replace("=", string.Empty);
        param4 = param4.Replace("=", string.Empty);

        // put params in list for sorting
        var @params = new List<string>();
        @params.Add("method" + method); // method
        @params.Add("api_key" + Secrets.APIkey); // api key
        // sk
        if (sk) @params.Add("sk" + MySettingsProperty.Settings.SessionKey);
        // param1
        if (!string.IsNullOrEmpty(param1)) @params.Add(param1);
        // param2
        if (!string.IsNullOrEmpty(param2)) @params.Add(param2);
        // param3
        if (!string.IsNullOrEmpty(param3)) @params.Add(param3);
        // param4
        if (!string.IsNullOrEmpty(param4)) @params.Add(param4);

        // add params to single string
        @params.Sort(); // sort alphabetically
        var parambuilder = new StringBuilder();
        // append params
        foreach (string param in @params)
            parambuilder.Append(param); // append parameters
        parambuilder.Append(Secrets.APIsecret); // append secret

        return Hash(parambuilder.ToString());
    }

    // finds the amount of substrings in a string
    public static uint StrCount(string input, string substring)
    {
        bool loopend = false;
        int startpos = 1;
        var count = default(uint);
        do
        {
            startpos = Strings.InStr(startpos, input, substring);
            if (startpos <= 0)
            {
                loopend = true; // end loop if no more can be found
            }
            else
            {
                startpos += substring.Length; // increment startpos to the end of substring
                count = (uint)(count + 1L);
            } // increment usercount
        } while (loopend != true);

        return count;
    }

    // creates a larger array out of a smaller array with empty elements set to string.empty
    public static string[] FillArray(string[] commands, byte maxindex)
    {
        var newcommands = new string[maxindex + 1];
        byte upperbound;

        // set upperbound
        if (commands.GetUpperBound(0) > maxindex)
            upperbound = maxindex;
        else
            upperbound = (byte)commands.GetUpperBound(0);

        // make new array
        for (byte count = 0, loopTo = upperbound; count <= loopTo; count++)
            newcommands[count] = commands[count];

        // make empty maxindex string.empty
        for (byte count = 0, loopTo1 = maxindex; count <= loopTo1; count++)
            if (string.IsNullOrEmpty(newcommands[count]))
                newcommands[count] = string.Empty;

        return newcommands;
    }

    public static void GoToTrack(string track, string artist)
    {
        // check for errors
        if (track.Contains("ERROR: ") || artist.Contains("ERROR: ")) return;

        MyProject.Forms.frmMain.Invoke(new Action(() => MyProject.Forms.frmMain.txtTrackTitle.Text = track));
        MyProject.Forms.frmMain.Invoke(new Action(() => MyProject.Forms.frmMain.txtTrackArtist.Text = artist));
        MyProject.Forms.frmMain.Invoke(new Action(() => MyProject.Forms.frmMain.tabControl.SelectTab(2)));
        MyProject.Forms.frmMain.Invoke(new Action(() => MyProject.Forms.frmMain.btnTrackGo.Select()));
        MyProject.Forms.frmMain.btnTrackGo.PerformClick();
    }

    public static void GoToArtist(string artist)
    {
        // check for errors
        if (artist.Contains("ERROR: ")) return;

        MyProject.Forms.frmMain.Invoke(new Action(() => MyProject.Forms.frmMain.txtArtistName.Text = artist));
        MyProject.Forms.frmMain.Invoke(new Action(() => MyProject.Forms.frmMain.tabControl.SelectTab(3)));
        MyProject.Forms.frmMain.Invoke(new Action(() => MyProject.Forms.frmMain.btnArtistGo.Select()));
        MyProject.Forms.frmMain.btnArtistGo.PerformClick();
    }

    public static void GoToAlbum(string album, string artist)
    {
        // check for errors
        if (album.Contains("ERROR: ") || artist.Contains("ERROR: ")) return;

        MyProject.Forms.frmMain.Invoke(new Action(() => MyProject.Forms.frmMain.txtAlbumTitle.Text = album));
        MyProject.Forms.frmMain.Invoke(new Action(() => MyProject.Forms.frmMain.txtAlbumArtist.Text = artist));
        MyProject.Forms.frmMain.Invoke(new Action(() => MyProject.Forms.frmMain.tabControl.SelectTab(4)));
        MyProject.Forms.frmMain.Invoke(new Action(() => MyProject.Forms.frmMain.btnAlbumGo.Select()));
        MyProject.Forms.frmMain.btnAlbumGo.PerformClick();
    }

    public static void BackupTag(string tag)
    {
        // check for errors
        if (tag.Contains("ERROR: ")) return;

        MyProject.Forms.frmBackupTool.Show();
        MyProject.Forms.frmBackupTool.Activate();
        MyProject.Forms.frmBackupTool.cmbContents.SelectedIndex = 1;
        MyProject.Forms.frmBackupTool.txtTagTag.Text = tag;
    }

    public static void BackupTrack(string track, string artist)
    {
        // check for errors
        if (track.Contains("ERROR: ") || artist.Contains("ERROR: ")) return;

        MyProject.Forms.frmBackupTool.Show();
        MyProject.Forms.frmBackupTool.Activate();
        MyProject.Forms.frmBackupTool.cmbContents.SelectedIndex = 2;
        MyProject.Forms.frmBackupTool.txtTrackTitle.Text = track;
        MyProject.Forms.frmBackupTool.txtTrackArtist.Text = artist;
    }

    public static void BackupArtist(string artist)
    {
        // check for errors
        if (artist.Contains("ERROR: ")) return;

        MyProject.Forms.frmBackupTool.Show();
        MyProject.Forms.frmBackupTool.Activate();
        MyProject.Forms.frmBackupTool.cmbContents.SelectedIndex = 3;
        MyProject.Forms.frmBackupTool.txtArtistArtist.Text = artist;
    }

    public static void BackupAlbum(string album, string artist)
    {
        // check for errors
        if (album.Contains("ERROR: ") || artist.Contains("ERROR: ")) return;

        MyProject.Forms.frmBackupTool.Show();
        MyProject.Forms.frmBackupTool.Activate();
        MyProject.Forms.frmBackupTool.cmbContents.SelectedIndex = 4;
        MyProject.Forms.frmBackupTool.txtAlbumAlbum.Text = album;
        MyProject.Forms.frmBackupTool.txtAlbumArtist.Text = artist;
    }

    public static void BackupUser(string user)
    {
        // check for errors
        if (user.Contains("ERROR: ")) return;

        MyProject.Forms.frmBackupTool.Show();
        MyProject.Forms.frmBackupTool.Activate();
        MyProject.Forms.frmBackupTool.cmbContents.SelectedIndex = 5;
        MyProject.Forms.frmBackupTool.txtUserUser.Text = user;
    }

    public static void AddAPIHistory(bool post, string method, string status, int latency, DateTime time, string thread)
    {
        string poststring;
        if (post == false)
            poststring = "GET";
        else
            poststring = "POST";
        string[] listarray = { poststring, method, status, latency.ToString("N0") + "ms", time.ToString("G"), thread };

        try
        {
            apihistory.Add(listarray);
        }
        catch (IndexOutOfRangeException ex)
        {
        }
    }

    // provides the autocorrected name of an entered tag, also can be used to verify if a tag exists on the lfm servers
    public static string VerifyTag(string tag)
    {
        // strings
        string response = CallAPI("tag.getInfo", "", "tag=" + tag, "autocorrect=1");
        string[] tagnodes = { "name", "total" };

        // parse
        ParseXML(response, "/lfm/tag", 0U, ref tagnodes);

        // any tag the user types in will come up with the name but will not have any data. filter out unverifiable tags by checking if taggings = 0
        if (tagnodes[1] == "0") tagnodes[0] = "ERROR: Tag unable to be verified";

        return tagnodes[0];
    }

    // provides the autocorrected name and artist of an entered track, also can be used to verify if a track exists on the lfm servers
    public static string[] VerifyTrack(string track, string artist)
    {
        // strings
        string response = CallAPI("track.getInfo", "", "track=" + track, "artist=" + artist, "autocorrect=1");
        string[] tracknodes = { "name", "artist/name", "album/title" };

        // parse
        ParseXML(response, "/lfm/track", 0U, ref tracknodes);

        return tracknodes;
    }

    // provides the autocorrected name of an artist, also can be used to verify if an artist exists on the lfm servers
    public static string VerifyArtist(string artist)
    {
        // strings
        string response = CallAPI("artist.getInfo", "", "artist=" + artist, "autocorrect=1");
        string[] artistnodes = { "name" };

        // parse
        ParseXML(response, "/lfm/artist", 0U, ref artistnodes);

        return artistnodes[0];
    }

    // provides the autocorrected name and artist of an entered album, also can be used to verify if an album exists on the lfm servers
    public static string[] VerifyAlbum(string album, string artist)
    {
        // strings
        string response = CallAPI("album.getInfo", "", "album=" + album, "artist=" + artist, "autocorrect=1");
        string[] albumnodes = { "name", "artist" };

        // parse
        ParseXML(response, "/lfm/album", 0U, ref albumnodes);

        return albumnodes;
    }

    // scrobbles a track and adds the data to the scrobble history listviews
    // return = track, artist, album, timestamp, status
    public static string[] Scrobble(string track, string artist, uint timestamp, string source, string album = "")
    {
        // make request
        string response;
        if (string.IsNullOrEmpty(album))
            response = CallAPIAuth("track.scrobble", "track=" + track, "artist=" + artist, "timestamp=" + timestamp);
        else
            response = CallAPIAuth("track.scrobble", "track=" + track, "artist=" + artist, "timestamp=" + timestamp,
                "album=" + album);

        // check for error
        if (response.Contains("ERROR: "))
            return new[] { track, artist, album, timestamp.ToString(), "ERROR: Scrobble did not succeed." };

        // accepted true/false?
        string acceptedstring = ParseMetadata(response, "accepted=");
        bool accepted;
        if (Conversions.ToShort(acceptedstring) > 0)
            accepted = true;
        else
            accepted = false;
        // parse response
        string[] responsenodes = { "track", "artist", "album", "timestamp", "ignoredMessage" };
        ParseXML(response, "/lfm/scrobbles/scrobble", 0U, ref responsenodes);

        // status
        // check for error
        if (track.Contains("ERROR: "))
            responsenodes = new[] { track, artist, timestamp.ToString(), album, "ERROR: Scrobble did not succeed." };
        // check for accepted/ignored
        if (accepted)
            responsenodes[4] = "Success";
        else // ignored
            responsenodes[4] = responsenodes[4].Insert(0, "Ignored: ");

        // add to listviews
        MyProject.Forms.frmMain.ltvMediaHistory.Items.Insert(0, responsenodes[0]).SubItems
            .AddRange(new[] { responsenodes[1], responsenodes[4] });
        scrobblehistory.Add(new[]
        {
            responsenodes[0], responsenodes[1], responsenodes[2],
            UnixToDate((uint)Math.Round(Conversions.ToDouble(responsenodes[3]) + timezoneoffset)).ToString("G"),
            UnixToDate((uint)(GetCurrentUTC() + timezoneoffset)).ToString("G"), source, responsenodes[4]
        });

        return responsenodes;
    }

    // returns just the filename from a full path
    public static string GetFilename(string path)
    {
        int startIndex = Strings.InStrRev(Conversions.ToString(path), @"\");
        int endIndex = Strings.InStr(Conversions.ToString(path), ".") - 1;
        return Conversions.ToString(path.Substring(startIndex, endIndex - startIndex));
    }

    // searches current scrobble index for a file and returns the 0 = track, 1 = artist, 2 = album
    // returns null if not found
    public static string[] SearchIndex(string filename)
    {
        foreach (var row in scrobbleindexdata)
            if ((row[0] ?? "") == (filename ?? ""))
                return new[] { row[1], row[2], row[3] };
        return null;
    }

    #endregion
}