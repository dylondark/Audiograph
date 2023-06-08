using System;
using System.Collections.Generic;
// Audiograph by Dylan Miller (dylondark)
// Project started on 11/11/2020

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using AxWMPLib;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using WMPLib;

namespace Audiograph
{

    public partial class frmMain
    {
        public frmMain()
        {
            InitializeComponent();
        }

        #region Charts
        private void UpdateCharts()
        {
            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            #region Fatty Arrays
            Label[,] TrackLabel = new Label[,] { { lblTopTrackTitle1, lblTopTrackArtist1, lblTopTrackAlbum1, lblTopTracksListeners1 }, { lblTopTrackTitle2, lblTopTrackArtist2, lblTopTrackAlbum2, lblTopTracksListeners2 }, { lblTopTrackTitle3, lblTopTrackArtist3, lblTopTrackAlbum3, lblTopTracksListeners3 }, { lblTopTrackTitle4, lblTopTrackArtist4, lblTopTrackAlbum4, lblTopTracksListeners4 }, { lblTopTrackTitle5, lblTopTrackArtist5, lblTopTrackAlbum5, lblTopTracksListeners5 }, { lblTopTrackTitle6, lblTopTrackArtist6, lblTopTrackAlbum6, lblTopTracksListeners6 }, { lblTopTrackTitle7, lblTopTrackArtist7, lblTopTrackAlbum7, lblTopTracksListeners7 }, { lblTopTrackTitle8, lblTopTrackArtist8, lblTopTrackAlbum8, lblTopTracksListeners8 }, { lblTopTrackTitle9, lblTopTrackArtist9, lblTopTrackAlbum9, lblTopTracksListeners9 }, { lblTopTrackTitle10, lblTopTrackArtist10, lblTopTrackAlbum10, lblTopTracksListeners10 }, { lblTopTrackTitle11, lblTopTrackArtist11, lblTopTrackAlbum11, lblTopTracksListeners11 }, { lblTopTrackTitle12, lblTopTrackArtist12, lblTopTrackAlbum12, lblTopTracksListeners12 }, { lblTopTrackTitle13, lblTopTrackArtist13, lblTopTrackAlbum13, lblTopTracksListeners13 }, { lblTopTrackTitle14, lblTopTrackArtist14, lblTopTrackAlbum14, lblTopTracksListeners14 }, { lblTopTrackTitle15, lblTopTrackArtist15, lblTopTrackAlbum15, lblTopTracksListeners15 }, { lblTopTrackTitle16, lblTopTrackArtist16, lblTopTrackAlbum16, lblTopTracksListeners16 }, { lblTopTrackTitle17, lblTopTrackArtist17, lblTopTrackAlbum17, lblTopTracksListeners17 }, { lblTopTrackTitle18, lblTopTrackArtist18, lblTopTrackAlbum18, lblTopTracksListeners18 }, { lblTopTrackTitle19, lblTopTrackArtist19, lblTopTrackAlbum19, lblTopTracksListeners19 }, { lblTopTrackTitle20, lblTopTrackArtist20, lblTopTrackAlbum20, lblTopTracksListeners20 } };

            PictureBox[] TrackArt = new PictureBox[] { picTrack1, picTrack2, picTrack3, picTrack4, picTrack5, picTrack6, picTrack7, picTrack8, picTrack9, picTrack10, picTrack11, picTrack12, picTrack13, picTrack14, picTrack15, picTrack16, picTrack17, picTrack18, picTrack19, picTrack20 };

            Label[,] ArtistLabel = new Label[,] { { lblTopArtist1, lblTopArtistListeners1, lblTopArtistPlaycount1 }, { lblTopArtist2, lblTopArtistListeners2, lblTopArtistPlaycount2 }, { lblTopArtist3, lblTopArtistListeners3, lblTopArtistPlaycount3 }, { lblTopArtist4, lblTopArtistListeners4, lblTopArtistPlaycount4 }, { lblTopArtist5, lblTopArtistListeners5, lblTopArtistPlaycount5 }, { lblTopArtist6, lblTopArtistListeners6, lblTopArtistPlaycount6 }, { lblTopArtist7, lblTopArtistListeners7, lblTopArtistPlaycount7 }, { lblTopArtist8, lblTopArtistListeners8, lblTopArtistPlaycount8 }, { lblTopArtist9, lblTopArtistListeners9, lblTopArtistPlaycount9 }, { lblTopArtist10, lblTopArtistListeners10, lblTopArtistPlaycount10 }, { lblTopArtist11, lblTopArtistListeners11, lblTopArtistPlaycount11 }, { lblTopArtist12, lblTopArtistListeners12, lblTopArtistPlaycount12 }, { lblTopArtist13, lblTopArtistListeners13, lblTopArtistPlaycount13 }, { lblTopArtist14, lblTopArtistListeners14, lblTopArtistPlaycount14 }, { lblTopArtist15, lblTopArtistListeners15, lblTopArtistPlaycount15 }, { lblTopArtist16, lblTopArtistListeners16, lblTopArtistPlaycount16 }, { lblTopArtist17, lblTopArtistListeners17, lblTopArtistPlaycount17 }, { lblTopArtist18, lblTopArtistListeners18, lblTopArtistPlaycount18 }, { lblTopArtist19, lblTopArtistListeners19, lblTopArtistPlaycount19 }, { lblTopArtist20, lblTopArtistListeners20, lblTopArtistPlaycount20 } };
            #endregion

            #region Top Tracks
            // dim stuff
            string TopTrackXML;
            var country = default(string);
            Invoke(new Action(() => country = cmbChartCountry.Text.Trim()));
            if (radChartWorldwide.Checked == true)
            {
                TopTrackXML = Utilities.CallAPI("chart.getTopTracks");
            }
            else
            {
                TopTrackXML = Utilities.CallAPI("geo.getTopTracks", "", "country=" + country.Replace(" ", "+"));
            }
            string[] TopTrackNodes = new string[] { "name", "artist/name" };
            string[] TrackLookupNodes = new string[] { "name", "artist/name", "album/title", "listeners" };
            uint numberholder;

            // report 20% progress
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // populate data loop
            for (byte counter = 0; counter <= 19; counter++)
            {
                // get initial track data (title and artist) from toptracks
                Utilities.ParseXML(TopTrackXML, "/lfm/tracks/track", counter, ref TopTrackNodes);

                // replace spaces with + for proper apicall formatting
                TopTrackNodes[0] = TopTrackNodes[0].Replace(" ", "+");
                TopTrackNodes[1] = TopTrackNodes[1].Replace(" ", "+");

                // call api for track data and set as variable
                string TrackXML = Utilities.CallAPI("track.getInfo", "", "track=" + TopTrackNodes[0], "artist=" + TopTrackNodes[1], "autocorrect=1");

                // get advanced track data (title, artist, album, listeners) and parse
                Utilities.ParseXML(TrackXML, "/lfm/track", 0U, ref TrackLookupNodes);

                // detect errors and set to "(Unavailable)"
                for (byte counter2 = 0; counter2 <= 3; counter2++)
                {
                    if (TrackLookupNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TrackLookupNodes[counter2] = "(Unavailable)";
                    }
                }

                // set labels
                Invoke(new Action(() => TrackLabel[counter, 0].Text = TrackLookupNodes[0]));
                Invoke(new Action(() => TrackLabel[counter, 1].Text = TrackLookupNodes[1]));
                Invoke(new Action(() => TrackLabel[counter, 2].Text = TrackLookupNodes[2]));
                // apply formatting to playcount
                uint.TryParse(TrackLookupNodes[3], out numberholder);
                Invoke(new Action(() => TrackLabel[counter, 3].Text = numberholder.ToString("N0")));

                // set picturebox
                try
                {
                    TrackArt[counter].Load(Utilities.ParseImage(TrackXML, "/lfm/track/album/image", 1));
                }
                catch (Exception ex)
                {
                    TrackArt[counter].Image = My.Resources.Resources.imageunavailable;
                }

                // reset node arrays
                TopTrackNodes = new[] { "name", "artist/name" };
                TrackLookupNodes = new[] { "name", "artist/name", "album/title", "listeners" };

                // report progress 20-60%
                Utilities.progress = (ushort)(Utilities.progress + 2);
                UpdateProgressChange();
            }
            #endregion

            #region Top Artists
            // dim stuff
            string TopArtistXML;
            string directory;
            if (radChartWorldwide.Checked == true)
            {
                TopArtistXML = Utilities.CallAPI("chart.getTopArtists");
                directory = "/lfm/artists/artist";
            }
            else
            {
                TopArtistXML = Utilities.CallAPI("geo.getTopArtists", "", "country=" + country.Replace(" ", "+"));
                directory = "/lfm/topartists/artist";
            }
            string[] TopArtistNodes = new string[] { "name", "listeners", "playcount" };

            for (byte counter = 0; counter <= 19; counter++)
            {
                // get initial artist data (name) from topartists
                Utilities.ParseXML(TopArtistXML, directory, counter, ref TopArtistNodes);

                // detect errors and set to "(Unavailable)"
                for (byte counter2 = 0; counter2 <= 2; counter2++)
                {
                    if (TopArtistNodes[counter2].Contains("Object reference not set to an instance of an object") == true || TopArtistNodes[counter2] == "0")
                    {
                        TopArtistNodes[counter2] = "(Unavailable)";
                    }
                }

                // set labels
                Invoke(new Action(() => ArtistLabel[counter, 0].Text = TopArtistNodes[0]));
                uint.TryParse(TopArtistNodes[1], out numberholder);
                Invoke(new Action(() => ArtistLabel[counter, 1].Text = numberholder.ToString("N0")));
                uint.TryParse(TopArtistNodes[2], out numberholder);
                Invoke(new Action(() => ArtistLabel[counter, 2].Text = numberholder.ToString("N0")));

                // reset node arrays
                TopArtistNodes = new[] { "name", "listeners", "playcount" };

                // report progress 60-100
                Utilities.progress = (ushort)(Utilities.progress + 2);
                UpdateProgressChange();
            }
            #endregion
        }
        #endregion

        #region Search
        private void UpdateSearch()
        {
            // check that there is something in the search box or that the user is not typing
            if (string.IsNullOrEmpty(txtSearch.Text) || (ActiveControl.Name ?? "") == (txtSearch.Name ?? ""))
            {
                return;
            }

            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            // params
            string tag = "tag=" + txtSearch.Text.Trim().ToLower();

            // info
            string infoXML = Utilities.CallAPI("tag.getInfo", string.Empty, tag);
            string[] infonodes = new string[] { "name", "total", "reach", "wiki/content" };
            Utilities.ParseXML(infoXML, "/lfm/tag", 0U, ref infonodes);
            // check for errors
            for (int x = 0; x < infonodes.Length; x++)
            {
                if (infonodes[x].Contains("ERROR: ") == true)
                {
                    infonodes[x] = "(Unavailable)";
                }
            } 
            // set name in search box
            if (infonodes[0] != "(Unavailable)")
            {
                txtSearch.Text = infonodes[0];
            }
            // add to box
            var builder = new StringBuilder();
            string doublepar = @"\par\par ";
            BeginInvoke(new Action(() => txtSearchInfo.Clear()));
            builder.Append(@"{\rtf1\ansi ");      // append rtf declaration
            builder.Append(@"\b Name\b0\par ");
            builder.Append(infonodes[0]);
            builder.Append(doublepar);         // double new line
            builder.Append(@"\b Total Times Used\b0\par ");
            builder.Append(Conversions.ToInteger(infonodes[1]).ToString("N0"));
            builder.Append(doublepar);
            builder.Append(@"\b Reach\b0\par ");
            builder.Append(Conversions.ToInteger(infonodes[2]).ToString("N0"));
            builder.Append(doublepar);
            builder.Append(@"\b Wiki\b0\par ");
            builder.Append(infonodes[3]);
            builder.Append("}"); // rtf closing
            BeginInvoke(new Action(() => txtSearchInfo.Rtf = builder.ToString()));

            // 25%
            Utilities.progress = (ushort)(Utilities.progress + 25);
            UpdateProgressChange();

            // tracks
            Invoke(new Action(() => ltvSearchTracks.Items.Clear()));
            string trackXML = Utilities.CallAPI("tag.getTopTracks", string.Empty, tag);
            string[] tracknodes;
            // parse and add to listview
            for (byte count = 0; count <= 49; count++)
            {
                tracknodes = new[] { "name", "artist/name" };
                Utilities.ParseXML(trackXML, "/lfm/tracks/track", count, ref tracknodes);
                // set errors to unavailable
                if (tracknodes[0].Contains("ERROR: ") == false)   // dont add item if there is an error
                {
                    Invoke(new Action(() => ltvSearchTracks.Items.Add((count + 1).ToString())));
                    Invoke(new Action(() => ltvSearchTracks.Items[count].SubItems.Add(tracknodes[0])));
                    if (tracknodes[1].Contains("ERROR: ") == false)   // dont add subitem if there is an error
                    {
                        Invoke(new Action(() => ltvSearchTracks.Items[count].SubItems.Add(tracknodes[1])));
                    }
                }
            }

            // 50%
            Utilities.progress = (ushort)(Utilities.progress + 25);
            UpdateProgressChange();

            // artists
            Invoke(new Action(() => ltvSearchArtists.Items.Clear()));
            string artistXML = Utilities.CallAPI("tag.getTopArtists", string.Empty, tag);
            string[] artistnodes;
            // parse and add to listview
            for (byte count = 0; count <= 49; count++)
            {
                artistnodes = new[] { "name" };
                Utilities.ParseXML(artistXML, "/lfm/topartists/artist", count, ref artistnodes);
                // set errors to unavailable
                if (artistnodes[0].Contains("ERROR: ") == false)  // dont add item if there is an error
                {
                    Invoke(new Action(() => ltvSearchArtists.Items.Add((count + 1).ToString())));
                    Invoke(new Action(() => ltvSearchArtists.Items[count].SubItems.Add(artistnodes[0])));
                }
            }

            // 75%
            Utilities.progress = (ushort)(Utilities.progress + 25);
            UpdateProgressChange();

            // albums
            Invoke(new Action(() => ltvSearchAlbums.Items.Clear()));
            string albumXML = Utilities.CallAPI("tag.getTopAlbums", string.Empty, tag);
            string[] albumnodes;
            // parse and add to listview
            for (byte count = 0; count <= 49; count++)
            {
                albumnodes = new[] { "name", "artist/name" };
                Utilities.ParseXML(albumXML, "/lfm/albums/album", count, ref albumnodes);
                // set errors to unavailable
                if (albumnodes[0].Contains("ERROR: ") == false)   // dont add item if there is an error
                {
                    Invoke(new Action(() => ltvSearchAlbums.Items.Add((count + 1).ToString())));
                    Invoke(new Action(() => ltvSearchAlbums.Items[count].SubItems.Add(albumnodes[0])));
                    if (albumnodes[1].Contains("ERROR: ") == false)   // dont add subitem if there is an error
                    {
                        Invoke(new Action(() => ltvSearchAlbums.Items[count].SubItems.Add(albumnodes[1])));
                    }
                }
            }

            // 100%
            Utilities.progress = (ushort)(Utilities.progress + 25);
            UpdateProgressChange();
        }
        #endregion

        #region Track
        public void UpdateTrack()
        {
            // check that something is in the boxes or that the user is not typing
            if (string.IsNullOrEmpty(txtTrackTitle.Text) || string.IsNullOrEmpty(txtTrackArtist.Text) || (ActiveControl.Name ?? "") == (txtTrackTitle.Name ?? "") || (ActiveControl.Name ?? "") == (txtTrackArtist.Name ?? ""))
            {
                return;
            }

            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            // dim stuff
            string trackXML;
            string[] tracknodes;
            // check for whether response should include user or not
            if (string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                trackXML = Utilities.CallAPI("track.getInfo", "", "track=" + txtTrackTitle.Text.Trim(), "artist=" + txtTrackArtist.Text.Trim(), "autocorrect=1");
                tracknodes = new[] { "name", "artist/name", "album/title", "mbid", "url", "duration", "listeners", "playcount", "wiki/content" };
            }
            else
            {
                trackXML = Utilities.CallAPI("track.getInfo", My.MySettingsProperty.Settings.User, "track=" + txtTrackTitle.Text.Trim(), "artist=" + txtTrackArtist.Text.Trim(), "autocorrect=1");
                tracknodes = new[] { "name", "artist/name", "album/title", "mbid", "url", "duration", "listeners", "playcount", "userplaycount", "userloved", "wiki/content" };
            }

            // parse for track info
            Utilities.ParseXML(trackXML, "/lfm/track", 0U, ref tracknodes);

            // check that track actually exists
            if (tracknodes[0].Contains("ERROR: ") == true)
            {
                BeginInvoke(new Action(() => MessageBox.Show("Track data unable to be retrieved" + Constants.vbCrLf + "Check that you have spelled your search terms correctly", "Track Lookup", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier - 1);
                return;
            }

            // 20%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // set correct names into search boxes
            BeginInvoke(new Action(() => txtTrackTitle.Text = tracknodes[0]));
            BeginInvoke(new Action(() => txtTrackArtist.Text = tracknodes[1]));

            // set tracklookup info
            Utilities.tracklookup[0] = tracknodes[0];  // track
            Utilities.tracklookup[1] = tracknodes[1];  // artist
            Utilities.tracklookup[2] = tracknodes[2];  // album

            // convert duration into time
            TimeSpan ts;
            // error checking in case duration wasnt able to be retrieved
            try
            {
                ts = TimeSpan.FromMilliseconds(Conversions.ToUInteger(tracknodes[5]));
            }
            catch (Exception ex)
            {
                ts = TimeSpan.Zero;
            }

            // find amount of toptags
            byte toptagcount = (byte)Utilities.StrCount(trackXML, "<tag>");
            // parse for toptags
            string[] toptagnode;
            var toptaglist = new List<string>();
            for (byte count = 0, loopTo = toptagcount; count <= loopTo; count++)
            {
                toptagnode = new[] { "name" };
                Utilities.ParseXML(trackXML, "/lfm/track/toptags/tag", count, ref toptagnode);
                // add to list if no errors
                if (toptagnode[0].Contains("ERROR: ") == false)
                {
                    toptaglist.Add(toptagnode[0]);
                }
            }

            // 40%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // format tags into one string
            var toptags = default(string);
            foreach (string tag in toptaglist)
                toptags += tag + ", ";
            // get rid of extra comma at end
            if (!string.IsNullOrEmpty(toptags))
            {
                toptags = toptags.Remove(toptags.Length - 2, 2);
            }

            // set any errors to (Unavailable)
            for (byte count = 0, loopTo1 = (byte)(tracknodes.Count() - 1); count <= loopTo1; count++)
            {
                if (tracknodes[count].Contains("ERROR: ") == true)
                {
                    tracknodes[count] = "(Unavailable)";
                }
            }
            if (string.IsNullOrEmpty(toptags) || toptags.Contains("ERROR: ") == true)
            {
                toptags = "(Unavailable)";
            }

            // format numbers
            // global listeners
            tracknodes[6] = Conversions.ToUInteger(tracknodes[6]).ToString("N0");
            // global playcount
            tracknodes[7] = Conversions.ToUInteger(tracknodes[7]).ToString("N0");
            // user playcount
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                tracknodes[8] = Conversions.ToUInteger(tracknodes[8]).ToString("N0");
            }

            // remove link from wiki
            if (tracknodes[10].Contains("<a href") == true || tracknodes[8].Contains("<a href") == true)
            {
                if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
                {
                    tracknodes[10] = tracknodes[10].Substring(0, tracknodes[10].Count() - (tracknodes[10].Count() - Strings.InStr(tracknodes[10], "<a href") + 1));
                }
                else
                {
                    tracknodes[8] = tracknodes[8].Substring(0, tracknodes[8].Count() - (tracknodes[8].Count() - Strings.InStr(tracknodes[8], "<a href") + 1));
                }
            }

            // set text box
            BeginInvoke(new Action(() => txtTrackInfo.Clear()));
            var builder = new StringBuilder();
            builder.Append(@"{\rtf1\ansi ");   // append rtf declaration
            builder.Append(@"\b Title\b0\par ");
            builder.Append(tracknodes[0]);
            builder.Append(@"\par\par ");      // double new line
            builder.Append(@"\b Artist\b0\par ");
            builder.Append(tracknodes[1]);
            builder.Append(@"\par\par ");
            builder.Append(@"\b Album\b0\par ");
            builder.Append(tracknodes[2]);
            builder.Append(@"\par\par ");
            builder.Append(@"\b Musicbrainz ID (MBID)\b0\par ");
            builder.Append(tracknodes[3]);
            builder.Append(@"\par\par ");
            builder.Append(@"\b View on Last.fm\b0\par ");
            builder.Append(tracknodes[4]);
            builder.Append(@"\par\par ");
            builder.Append(@"\b Duration\b0\par ");
            builder.Append(ts.Minutes.ToString() + ":" + ts.Seconds.ToString("0#"));
            builder.Append(@"\par\par ");
            builder.Append(@"\b Global Listeners\b0\par ");
            builder.Append(tracknodes[6]);
            builder.Append(@"\par\par ");
            builder.Append(@"\b Global Playcount\b0\par ");
            builder.Append(tracknodes[7]);
            builder.Append(@"\par\par ");
            // add user playcount
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                builder.Append(@"\b User Playcount\b0\par ");
                builder.Append(tracknodes[8]);
                builder.Append(@"\par\par ");
            }
            builder.Append(@"\b Top Tags\b0\par ");
            builder.Append(toptags);
            builder.Append(@"\par\par ");
            builder.Append(@"\b Wiki\b0\par ");
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                builder.Append(tracknodes[10]);
            }
            else
            {
                builder.Append(tracknodes[8]);
            }
            builder.Append("}");
            BeginInvoke(new Action(() => txtTrackInfo.Rtf = builder.ToString())); // set box

            // 60%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // set track art
            try
            {
                picTrackArt.Load(Utilities.ParseImage(trackXML, "/lfm/track/album/image", 3));
            }
            catch (Exception ex)
            {
                BeginInvoke(new Action(() => picTrackArt.Image = My.Resources.Resources.imageunavailable));
            }

            // set loved button
            if (tracknodes[9] == "1")
            {
                BeginInvoke(new Action(() => btnTrackLove.Text = "Unlove"));
            }
            else
            {
                BeginInvoke(new Action(() => btnTrackLove.Text = "Love"));
            }

            // avoid trying to parse tags if there is no user set
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                // get tags
                string tagXML = Utilities.CallAPI("track.getTags", My.MySettingsProperty.Settings.User, "track=" + tracknodes[0], "artist=" + tracknodes[1]);

                // put tags into list box
                BeginInvoke(new Action(() => lstTrackUserTags.Items.Clear()));
                // find number of tags
                uint tagcount = Utilities.StrCount(tagXML, "<tag>");
                // parse for tags
                string[] tagnode;
                for (byte count = 0, loopTo2 = (byte)tagcount; count <= loopTo2; count++)
                {
                    tagnode = new[] { "name" };
                    Utilities.ParseXML(tagXML, "/lfm/tags/tag", count, ref tagnode);
                    // add to listbox if no errors
                    if (tagnode[0].Contains("ERROR: ") == false)
                    {
                        Invoke(new Action(() => lstTrackUserTags.Items.Add(tagnode[0])));
                    }
                }
            }

            // 80%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // get similar tracks
            string similarXML = Utilities.CallAPI("track.getSimilar", "", "track=" + tracknodes[0], "artist=" + tracknodes[1], "limit=50");

            // put similar tracks into listview
            BeginInvoke(new Action(() => ltvTrackSimilar.Items.Clear()));
            // parse for tracks
            string[] similarnodes;
            for (byte count = 0; count <= 49; count++)
            {
                similarnodes = new[] { "name", "artist/name", "match" };
                Utilities.ParseXML(similarXML, "/lfm/similartracks/track", count, ref similarnodes);
                // change errors to (Unavailable)
                if (similarnodes[1].Contains("ERROR: ") == true)
                {
                    similarnodes[1] = "(Unavailable)";
                }
                if (similarnodes[2].Contains("ERROR: ") == true)
                {
                    similarnodes[2] = "(Unavailable)";
                }
                else    // format match as %
                {
                    similarnodes[2] = Conversions.ToSingle(similarnodes[2]).ToString("P1");
                }
                // add to listview if no errors
                if (similarnodes[0].Contains("ERROR: ") == false)
                {
                    Invoke(new Action(() => ltvTrackSimilar.Items.Add(similarnodes[0])));
                    Invoke(new Action(() => ltvTrackSimilar.Items[count].SubItems.Add(similarnodes[1])));
                    Invoke(new Action(() => ltvTrackSimilar.Items[count].SubItems.Add(similarnodes[2])));
                }
            }

            // 100%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();
        }
        #endregion

        #region Artist
        public void UpdateArtist()
        {
            // check that something is in the box or that the user is not typing
            if (string.IsNullOrEmpty(txtArtistName.Text) || (ActiveControl.Name ?? "") == (txtArtistName.Name ?? ""))
            {
                return;
            }

            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            // dim stuff
            string artistXML;
            string[] artistnodes;
            // check for whether response should include user or not
            if (string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                artistXML = Utilities.CallAPI("artist.getInfo", "", "artist=" + txtArtistName.Text.Trim(), "autocorrect=1");
                artistnodes = new[] { "name", "mbid", "url", "stats/listeners", "stats/playcount", "bio/content" };
            }
            else
            {
                artistXML = Utilities.CallAPI("artist.getInfo", My.MySettingsProperty.Settings.User, "artist=" + txtArtistName.Text.Trim(), "autocorrect=1");
                artistnodes = new[] { "name", "mbid", "url", "stats/listeners", "stats/playcount", "stats/userplaycount", "bio/content" };
            }

            // parse for artist info
            Utilities.ParseXML(artistXML, "/lfm/artist", 0U, ref artistnodes);

            // check that the artist actually exists
            if (artistnodes[0].Contains("ERROR: ") == true)
            {
                Invoke(new Action(() => MessageBox.Show("Artist data unable to be retrieved" + Constants.vbCrLf + "Check that you have spelled your search terms correctly", "Artist Lookup", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier - 1);
                return;
            }

            // 20%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // set correct name into search box
            Invoke(new Action(() => txtArtistName.Text = artistnodes[0]));

            // set artistlookup
            Utilities.artistlookup = artistnodes[0];

            // set image
            string topalbumXML = Utilities.CallAPI("artist.getTopAlbums", "", "artist=" + artistnodes[0], "limit=20");
            try
            {
                picArtistArt.Load(Utilities.ParseImage(topalbumXML, "/lfm/topalbums/album/image", 3));
            }
            catch (Exception ex)
            {
                picArtistArt.Image = My.Resources.Resources.imageunavailable;
            }

            // find amount of toptags
            byte toptagcount = (byte)Utilities.StrCount(artistXML, "<tag>");
            // parse for toptags
            string[] toptagnode;
            var toptaglist = new List<string>();
            for (byte count = 0, loopTo = toptagcount; count <= loopTo; count++)
            {
                toptagnode = new[] { "name" };
                Utilities.ParseXML(artistXML, "/lfm/artist/tags/tag", count, ref toptagnode);
                // add to list if no errors
                if (toptagnode[0].Contains("ERROR: ") == false)
                {
                    toptaglist.Add(toptagnode[0]);
                }
            }

            // format tags into one string
            var toptags = default(string);
            foreach (string tag in toptaglist)
                toptags += tag + ", ";
            // get rid of extra comma at end
            if (!string.IsNullOrEmpty(toptags))
            {
                toptags = toptags.Remove(toptags.Length - 2, 2);
            }

            // set any errors to (Unavailable)
            for (byte count = 0, loopTo1 = (byte)(artistnodes.Count() - 1); count <= loopTo1; count++)
            {
                if (artistnodes[count].Contains("ERROR: ") == true)
                {
                    artistnodes[count] = "(Unavailable)";
                }
            }
            if (string.IsNullOrEmpty(toptags) || toptags.Contains("ERROR: ") == true)
            {
                toptags = "(Unavailable)";
            }

            // 40%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // format numbers
            uint numberholder;
            // global listeners
            uint.TryParse(artistnodes[3], out numberholder);
            artistnodes[3] = numberholder.ToString("N0");
            // global playcount
            uint.TryParse(artistnodes[4], out numberholder);
            artistnodes[4] = numberholder.ToString("N0");
            // user playcount
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                uint.TryParse(artistnodes[5], out numberholder);
                artistnodes[5] = numberholder.ToString("N0");
            }

            // remove link from wiki
            if (artistnodes[6].Contains("<a href") == true || artistnodes[5].Contains("<a href") == true)
            {
                if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
                {
                    artistnodes[6] = artistnodes[6].Substring(0, artistnodes[6].Count() - (artistnodes[6].Count() - Strings.InStr(artistnodes[6], "<a href") + 1));
                }
                else
                {
                    artistnodes[5] = artistnodes[5].Substring(0, artistnodes[5].Count() - (artistnodes[5].Count() - Strings.InStr(artistnodes[5], "<a href") + 1));
                }
            }

            // set text box
            Invoke(new Action(() => txtArtistInfo.Clear()));
            var builder = new StringBuilder();
            builder.Append(@"{\rtf1\ansi ");   // append rtf declaration
            builder.Append(@"\b Artist\b0\par ");
            builder.Append(artistnodes[0]);
            builder.Append(@"\par\par ");      // double new line
            builder.Append(@"\b Musicbrainz ID (MBID)\b0\par ");
            builder.Append(artistnodes[1]);
            builder.Append(@"\par\par ");
            builder.Append(@"\b View on Last.fm\b0\par ");
            builder.Append(artistnodes[2]);
            builder.Append(@"\par\par ");
            builder.Append(@"\b Global Listeners\b0\par ");
            builder.Append(artistnodes[3]);
            builder.Append(@"\par\par ");
            builder.Append(@"\b Global Playcount\b0\par ");
            builder.Append(artistnodes[4]);
            builder.Append(@"\par\par ");
            // add user playcount
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                builder.Append(@"\b User Playcount\b0\par ");
                builder.Append(artistnodes[5]);
                builder.Append(@"\par\par ");
            }
            builder.Append(@"\b Top Tags\b0\par ");
            builder.Append(toptags);
            builder.Append(@"\par\par ");
            builder.Append(@"\b Wiki\b0\par ");
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                builder.Append(artistnodes[6]);
            }
            else
            {
                builder.Append(artistnodes[5]);
            }
            builder.Append("}");
            Invoke(new Action(() => txtArtistInfo.Rtf = builder.ToString())); // set box

            // 60%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // avoid trying to parse tags if there is no user set
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                // get tags
                string tagXML = Utilities.CallAPI("artist.getTags", My.MySettingsProperty.Settings.User, "artist=" + artistnodes[0]);

                // put tags into list box
                Invoke(new Action(() => lstArtistUserTags.Items.Clear()));
                // find number of tags
                byte tagcount = (byte)Utilities.StrCount(tagXML, "<tag>");
                if (tagcount > 0)
                {
                    tagcount = (byte)(tagcount - 1);
                }
                // parse for tags
                string[] tagnode;
                for (byte count = 0, loopTo2 = tagcount; count <= loopTo2; count++)
                {
                    tagnode = new[] { "name" };
                    Utilities.ParseXML(tagXML, "/lfm/tags/tag", count, ref tagnode);
                    // add to listbox if no errors
                    if (tagnode[0].Contains("ERROR: ") == false)
                    {
                        Invoke(new Action(() => lstArtistUserTags.Items.Add(tagnode[0])));
                    }
                }
            }

            // get similar artists
            string similarXML = Utilities.CallAPI("artist.getSimilar", "", "artist=" + artistnodes[0], "limit=50");

            // put similar tracks into listview
            Invoke(new Action(() => ltvArtistSimilar.Items.Clear()));
            // parse for tracks
            string[] similarnodes;
            float matchholder;
            for (byte count = 0; count <= 49; count++)
            {
                similarnodes = new[] { "name", "match" };
                Utilities.ParseXML(similarXML, "/lfm/similarartists/artist", count, ref similarnodes);
                // change errors to (Unavailable)
                if (similarnodes[1].Contains("ERROR: ") == true)
                {
                    similarnodes[1] = "(Unavailable)";
                }
                else    // format match as %
                {
                    float.TryParse(similarnodes[1], out matchholder);
                    similarnodes[1] = matchholder.ToString("P1");
                }
                // add to listview if no errors
                if (similarnodes[0].Contains("ERROR: ") == false)
                {
                    Invoke(new Action(() => ltvArtistSimilar.Items.Add(similarnodes[0])));
                    Invoke(new Action(() => ltvArtistSimilar.Items[count].SubItems.Add(similarnodes[1])));
                }
            }

            // 80%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            #region Fatty Arrays
            Label[,] TopTrackLabel = new Label[,] { { lblArtistTopTrackTitle1, lblArtistTopTrackListeners1 }, { lblArtistTopTrackTitle2, lblArtistTopTrackListeners2 }, { lblArtistTopTrackTitle3, lblArtistTopTrackListeners3 }, { lblArtistTopTrackTitle4, lblArtistTopTrackListeners4 }, { lblArtistTopTrackTitle5, lblArtistTopTrackListeners5 }, { lblArtistTopTrackTitle6, lblArtistTopTrackListeners6 }, { lblArtistTopTrackTitle7, lblArtistTopTrackListeners7 }, { lblArtistTopTrackTitle8, lblArtistTopTrackListeners8 }, { lblArtistTopTrackTitle9, lblArtistTopTrackListeners9 }, { lblArtistTopTrackTitle10, lblArtistTopTrackListeners10 }, { lblArtistTopTrackTitle11, lblArtistTopTrackListeners11 }, { lblArtistTopTrackTitle12, lblArtistTopTrackListeners12 }, { lblArtistTopTrackTitle13, lblArtistTopTrackListeners13 }, { lblArtistTopTrackTitle14, lblArtistTopTrackListeners14 }, { lblArtistTopTrackTitle15, lblArtistTopTrackListeners15 }, { lblArtistTopTrackTitle16, lblArtistTopTrackListeners16 }, { lblArtistTopTrackTitle17, lblArtistTopTrackListeners17 }, { lblArtistTopTrackTitle18, lblArtistTopTrackListeners18 }, { lblArtistTopTrackTitle19, lblArtistTopTrackListeners19 }, { lblArtistTopTrackTitle20, lblArtistTopTrackListeners20 } };

            Label[] TopAlbumLabel = new Label[] { lblArtistTopAlbum1, lblArtistTopAlbum2, lblArtistTopAlbum3, lblArtistTopAlbum4, lblArtistTopAlbum5, lblArtistTopAlbum6, lblArtistTopAlbum7, lblArtistTopAlbum8, lblArtistTopAlbum9, lblArtistTopAlbum10 };
            PictureBox[] TopAlbumPic = new PictureBox[] { picArtistTopAlbum1, picArtistTopAlbum2, picArtistTopAlbum3, picArtistTopAlbum4, picArtistTopAlbum5, picArtistTopAlbum6, picArtistTopAlbum7, picArtistTopAlbum8, picArtistTopAlbum9, picArtistTopAlbum10 };
            #endregion

            // top tracks
            string toptrackXML = Utilities.CallAPI("artist.getTopTracks", "", "artist=" + artistnodes[0], "limit=20");
            string[] toptracknodes;
            // put tracks into tlp
            for (byte count = 0; count <= 19; count++)
            {
                // parse
                toptracknodes = new[] { "name", "listeners" };
                Utilities.ParseXML(toptrackXML, "/lfm/toptracks/track", count, ref toptracknodes);

                // set
                Invoke(new Action(() => TopTrackLabel[count, 0].Text = toptracknodes[0]));
                try
                {
                    Invoke(new Action(() => TopTrackLabel[count, 1].Text = Conversions.ToUInteger(toptracknodes[1]).ToString("N0")));
                }
                catch (Exception ex)
                {
                    Invoke(new Action(() => TopTrackLabel[count, 1].Text = toptracknodes[1]));
                }
            }

            // top albums
            string[] topalbumnodes;
            // put albums into tlp
            for (byte count = 0; count <= 9; count++)
            {
                // parse
                topalbumnodes = new[] { "name" };
                Utilities.ParseXML(topalbumXML, "/lfm/topalbums/album", count, ref topalbumnodes);

                // set
                Invoke(new Action(() => TopAlbumLabel[count].Text = topalbumnodes[0]));
                try
                {
                    TopAlbumPic[count].Load(Utilities.ParseImage(topalbumXML, "/lfm/topalbums/album", count, 6));
                }
                catch (Exception ex)
                {
                    TopAlbumPic[count].Image = My.Resources.Resources.imageunavailable;
                }
            }

            // 100%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();
        }
        #endregion

        #region Album
        public void UpdateAlbum()
        {
            // check that something is in the boxes or that the user is not typing
            if (string.IsNullOrEmpty(txtAlbumTitle.Text) || string.IsNullOrEmpty(txtAlbumArtist.Text) || (ActiveControl.Name ?? "") == (txtAlbumTitle.Name ?? "") || (ActiveControl.Name ?? "") == (txtAlbumArtist.Name ?? ""))
            {
                return;
            }

            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            // dim stuff
            string albumXML;
            string[] albumnodes;
            // check for whether response should include user or not
            if (string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                albumXML = Utilities.CallAPI("album.getInfo", "", "album=" + txtAlbumTitle.Text.Trim(), "artist=" + txtAlbumArtist.Text.Trim(), "autocorrect=1");
                albumnodes = new[] { "name", "artist", "mbid", "url", "listeners", "playcount", "wiki/content" };
            }
            else
            {
                albumXML = Utilities.CallAPI("album.getInfo", My.MySettingsProperty.Settings.User, "album=" + txtAlbumTitle.Text.Trim(), "artist=" + txtAlbumArtist.Text.Trim(), "autocorrect=1");
                albumnodes = new[] { "name", "artist", "mbid", "url", "listeners", "playcount", "userplaycount", "wiki/content" };
            }

            // parse for album info
            Utilities.ParseXML(albumXML, "/lfm/album", 0U, ref albumnodes);

            // check that album actually exists
            if (albumnodes[0].Contains("ERROR: ") == true)
            {
                BeginInvoke(new Action(() => MessageBox.Show("Album data unable to be retrieved" + Constants.vbCrLf + "Check that you have spelled your search terms correctly", "Album Lookup", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier - 1);
                return;
            }

            // 20%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // set correct names into search boxes
            BeginInvoke(new Action(() => txtAlbumTitle.Text = albumnodes[0]));
            BeginInvoke(new Action(() => txtAlbumArtist.Text = albumnodes[1]));

            // set albumlookup info
            Utilities.albumlookup[0] = albumnodes[0];  // album
            Utilities.albumlookup[1] = albumnodes[1];  // artist

            // find amount of toptags
            byte toptagcount = (byte)Utilities.StrCount(albumXML, "<tag>");
            // parse for toptags
            string[] toptagnode;
            var toptaglist = new List<string>();
            for (byte count = 0, loopTo = toptagcount; count <= loopTo; count++)
            {
                toptagnode = new[] { "name" };
                Utilities.ParseXML(albumXML, "/lfm/album/tags/tag", count, ref toptagnode);
                // add to list if no errors
                if (toptagnode[0].Contains("ERROR: ") == false)
                {
                    toptaglist.Add(toptagnode[0]);
                }
            }

            // 40%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // format tags into one string
            var toptags = default(string);
            foreach (string tag in toptaglist)
                toptags += tag + ", ";
            // get rid of extra comma at end
            if (!string.IsNullOrEmpty(toptags))
            {
                toptags = toptags.Remove(toptags.Length - 2, 2);
            }

            // set any errors to (Unavailable)
            for (byte count = 0, loopTo1 = (byte)(albumnodes.Count() - 1); count <= loopTo1; count++)
            {
                if (albumnodes[count].Contains("ERROR: ") == true)
                {
                    albumnodes[count] = "(Unavailable)";
                }
            }
            if (string.IsNullOrEmpty(toptags) || toptags.Contains("ERROR: ") == true)
            {
                toptags = "(Unavailable)";
            }

            // format numbers
            // global listeners
            albumnodes[4] = Conversions.ToUInteger(albumnodes[4]).ToString("N0");
            // global playcount
            albumnodes[5] = Conversions.ToUInteger(albumnodes[5]).ToString("N0");
            // user playcount
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                albumnodes[6] = Conversions.ToUInteger(albumnodes[6]).ToString("N0");
            }

            // remove link from wiki
            if (albumnodes[7].Contains("<a href") == true || albumnodes[6].Contains("<a href") == true)
            {
                if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
                {
                    albumnodes[7] = albumnodes[7].Substring(0, albumnodes[7].Count() - (albumnodes[7].Count() - Strings.InStr(albumnodes[7], "<a href") + 1));
                }
                else
                {
                    albumnodes[6] = albumnodes[6].Substring(0, albumnodes[6].Count() - (albumnodes[6].Count() - Strings.InStr(albumnodes[6], "<a href") + 1));
                }
            }

            // set text box
            BeginInvoke(new Action(() => txtAlbumInfo.Clear()));
            var builder = new StringBuilder();
            builder.Append(@"{\rtf1\ansi ");   // append rtf declaration
            builder.Append(@"\b Title\b0\par ");
            builder.Append(albumnodes[0]);
            builder.Append(@"\par\par ");      // double new line
            builder.Append(@"\b Artist\b0\par ");
            builder.Append(albumnodes[1]);
            builder.Append(@"\par\par ");
            builder.Append(@"\b Musicbrainz ID (MBID)\b0\par ");
            builder.Append(albumnodes[2]);
            builder.Append(@"\par\par ");
            builder.Append(@"\b View on Last.fm\b0\par ");
            builder.Append(albumnodes[3]);
            builder.Append(@"\par\par ");
            builder.Append(@"\b Global Listeners\b0\par ");
            builder.Append(albumnodes[4]);
            builder.Append(@"\par\par ");
            builder.Append(@"\b Global Playcount\b0\par ");
            builder.Append(albumnodes[5]);
            builder.Append(@"\par\par ");
            // add user playcount
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                builder.Append(@"\b User Playcount\b0\par ");
                builder.Append(albumnodes[6]);
                builder.Append(@"\par\par ");
            }
            builder.Append(@"\b Top Tags\b0\par ");
            builder.Append(toptags);
            builder.Append(@"\par\par ");
            builder.Append(@"\b Wiki\b0\par ");
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                builder.Append(albumnodes[7]);
            }
            else
            {
                builder.Append(albumnodes[6]);
            }
            builder.Append("}");
            BeginInvoke(new Action(() => txtAlbumInfo.Rtf = builder.ToString())); // set box

            // 60%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // set album art
            try
            {
                picAlbumArt.Load(Utilities.ParseImage(albumXML, "/lfm/album/image", 3));
            }
            catch (Exception ex)
            {
                BeginInvoke(new Action(() => picAlbumArt.Image = My.Resources.Resources.imageunavailable));
            }

            // avoid trying to parse tags if there is no user set
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                // get tags
                string tagXML = Utilities.CallAPI("album.getTags", My.MySettingsProperty.Settings.User, "album=" + albumnodes[0], "artist=" + albumnodes[1]);

                // put tags into list box
                Invoke(new Action(() => lstAlbumUserTags.Items.Clear()));
                // find number of tags
                byte tagcount = (byte)Utilities.StrCount(tagXML, "<tag>");
                // parse for tags
                string[] tagnode;
                for (byte count = 0, loopTo2 = tagcount; count <= loopTo2; count++)
                {
                    tagnode = new[] { "name" };
                    Utilities.ParseXML(tagXML, "/lfm/tags/tag", count, ref tagnode);
                    // add to listbox if no errors
                    if (tagnode[0].Contains("ERROR: ") == false)
                    {
                        Invoke(new Action(() => lstAlbumUserTags.Items.Add(tagnode[0])));
                    }
                }
            }

            // 80%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // track list
            BeginInvoke(new Action(() => ltvAlbumTrackList.Items.Clear()));
            // get amount of tracks
            ushort tracknum = (ushort)Utilities.StrCount(albumXML, "<track rank");
            if (tracknum == 0)
            {
                tracknum = 1;
            }
            // init
            var ts = new TimeSpan();
            ushort currentnum = 0;
            // parse for albums
            string[] tracknodes;
            for (byte count = 0, loopTo3 = (byte)(tracknum - 1); count <= loopTo3; count++)
            {
                tracknodes = new[] { "name", "duration" };
                Utilities.ParseXML(albumXML, "/lfm/album/tracks/track", count, ref tracknodes);
                // change errors to (Unavailable)
                if (tracknodes[0].Contains("ERROR: ") == true)
                {
                    tracknodes[0] = "(Unavailable)";
                }
                // set to 0 if duration cannot be parsed
                try
                {
                    ts = TimeSpan.FromSeconds(Conversions.ToUInteger(tracknodes[1]));
                }
                catch (Exception ex)
                {
                    ts = TimeSpan.Zero;
                }
                // add to listview if no errors
                if (tracknodes[0] != "(Unavailable)")
                {
                    currentnum = (ushort)(currentnum + 1);
                    Invoke(new Action(() => ltvAlbumTrackList.Items.Add(currentnum.ToString())));   // #
                    Invoke(new Action(() => ltvAlbumTrackList.Items[count].SubItems.Add(tracknodes[0])));    // title
                    Invoke(new Action(() => ltvAlbumTrackList.Items[count].SubItems.Add(ts.Minutes.ToString() + ":" + ts.Seconds.ToString("0#"))));  // duration    ' duration
                }
            }

            // 100%
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();
        }
        #endregion

        #region User
        public void UpdateUser()
        {
            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            #region Info
            // xml stuff
            string UserInfoXML = Utilities.CallAPI("user.getInfo", My.MySettingsProperty.Settings.User);
            string[] UserInfoNodes = new string[] { "name", "realname", "url", "country", "age", "gender", "playcount", "playlists", "registered" };

            // parse xml
            Utilities.ParseXML(UserInfoXML, "/lfm/user", 0U, ref UserInfoNodes);
            try
            {
                picUser.Load(Utilities.ParseImage(UserInfoXML, "/lfm/user/image", 3));
            }
            catch (Exception ex)
            {
                picUser.Image = My.Resources.Resources.imageunavailable;
            }

            // convert registered date to datetime
            uint unixtime;
            uint.TryParse(UserInfoNodes[8], out unixtime);
            var RegisteredDateTime = Utilities.UnixToDate((uint)(unixtime + Utilities.timezoneoffset));

            // gender formatting
            switch (UserInfoNodes[5] ?? "")
            {
                case "m":
                    {
                        UserInfoNodes[5] = "Male";
                        break;
                    }
                case "f":
                    {
                        UserInfoNodes[5] = "Female";
                        break;
                    }

                default:
                    {
                        UserInfoNodes[5] = "Not Specified";
                        break;
                    }
            }

            // age formatting
            if (UserInfoNodes[4] == "0")
            {
                UserInfoNodes[4] = "Not Specified";
            }

            // playcount formatting
            UserInfoNodes[6] = Conversions.ToInteger(UserInfoNodes[6]).ToString("N0");

            // create textbox data
            Invoke(new Action(() => txtUserInfo.Clear()));
            Invoke(new Action(() => txtUserInfo.Text = "Name" + Constants.vbCrLf + UserInfoNodes[0] + Constants.vbCrLf + Constants.vbCrLf + "Real Name" + Constants.vbCrLf + UserInfoNodes[1] + Constants.vbCrLf + Constants.vbCrLf + "URL" + Constants.vbCrLf + UserInfoNodes[2] + Constants.vbCrLf + Constants.vbCrLf + "Country" + Constants.vbCrLf + UserInfoNodes[3] + Constants.vbCrLf + Constants.vbCrLf + "Age" + Constants.vbCrLf + UserInfoNodes[4] + Constants.vbCrLf + Constants.vbCrLf + "Gender" + Constants.vbCrLf + UserInfoNodes[5] + Constants.vbCrLf + Constants.vbCrLf + "Playcount" + Constants.vbCrLf + UserInfoNodes[6] + Constants.vbCrLf + Constants.vbCrLf + "Playlists" + Constants.vbCrLf + UserInfoNodes[7] + Constants.vbCrLf + Constants.vbCrLf + "Registered" + Constants.vbCrLf + RegisteredDateTime.ToString()));

            // format textbox
            var BoldFont = new Font("Segoe UI", 10f, FontStyle.Bold);
            // name
            Invoke(new Action(() => txtUserInfo.SelectionStart = 0));
            Invoke(new Action(() => txtUserInfo.SelectionLength = "Text".Length));
            Invoke(new Action(() => txtUserInfo.SelectionFont = BoldFont));
            // real name
            Invoke(new Action(() => txtUserInfo.SelectionStart = Strings.InStr(txtUserInfo.Text, "Real Name") - 1));
            Invoke(new Action(() => txtUserInfo.SelectionLength = "Real Name".Length));
            Invoke(new Action(() => txtUserInfo.SelectionFont = BoldFont));
            // URL
            Invoke(new Action(() => txtUserInfo.SelectionStart = Strings.InStr(txtUserInfo.Text, "URL") - 1));
            Invoke(new Action(() => txtUserInfo.SelectionLength = "URL".Length));
            Invoke(new Action(() => txtUserInfo.SelectionFont = BoldFont));
            // country
            Invoke(new Action(() => txtUserInfo.SelectionStart = Strings.InStr(txtUserInfo.Text, "Country") - 1));
            Invoke(new Action(() => txtUserInfo.SelectionLength = "Country".Length));
            Invoke(new Action(() => txtUserInfo.SelectionFont = BoldFont));
            // age
            Invoke(new Action(() => txtUserInfo.SelectionStart = Strings.InStr(txtUserInfo.Text, "Age") - 1));
            Invoke(new Action(() => txtUserInfo.SelectionLength = "Age".Length));
            Invoke(new Action(() => txtUserInfo.SelectionFont = BoldFont));
            // gender
            Invoke(new Action(() => txtUserInfo.SelectionStart = Strings.InStr(txtUserInfo.Text, "Gender") - 1));
            Invoke(new Action(() => txtUserInfo.SelectionLength = "Gender".Length));
            Invoke(new Action(() => txtUserInfo.SelectionFont = BoldFont));
            // playcount
            Invoke(new Action(() => txtUserInfo.SelectionStart = Strings.InStr(txtUserInfo.Text, "Playcount") - 1));
            Invoke(new Action(() => txtUserInfo.SelectionLength = "Playcount".Length));
            Invoke(new Action(() => txtUserInfo.SelectionFont = BoldFont));
            // playlists
            Invoke(new Action(() => txtUserInfo.SelectionStart = Strings.InStr(txtUserInfo.Text, "Playlists") - 1));
            Invoke(new Action(() => txtUserInfo.SelectionLength = "Playlists".Length));
            Invoke(new Action(() => txtUserInfo.SelectionFont = BoldFont));
            // registered
            Invoke(new Action(() => txtUserInfo.SelectionStart = Strings.InStr(txtUserInfo.Text, "Registered") - 1));
            Invoke(new Action(() => txtUserInfo.SelectionLength = "Registered".Length));
            Invoke(new Action(() => txtUserInfo.SelectionFont = BoldFont));

            // report 34% progress
            Utilities.progress = (ushort)(Utilities.progress + 34);
            UpdateProgressChange();
            #endregion

            #region Friends
            // parse for total friends
            string FriendsTotalXML = Utilities.CallAPI("user.getFriends", My.MySettingsProperty.Settings.User, "limit=1");
            string totalfriends = Utilities.ParseMetadata(FriendsTotalXML, "total=");
            if (totalfriends.Contains("ERROR:") == true)
            {
                totalfriends = "max";
            }

            // xml stuff
            string FriendsXML = Utilities.CallAPI("user.getFriends", My.MySettingsProperty.Settings.User, "limit=" + totalfriends);
            Invoke(new Action(() => lblUserFriendTotal.Text = "Friends: " + Utilities.ParseMetadata(FriendsXML, "total=")));
            if (lblUserFriendTotal.Text.Contains("ParseMetadata") == true)
            {
                Invoke(new Action(() => lblUserFriendTotal.Text = "Friends: 0"));
            }
            string[] FriendsNodes = new string[] { "name", "realname", "url", "registered" };

            // find number of users in xml
            uint usercount = Utilities.StrCount(FriendsXML, "<user>");

            // add each friend to list view
            Invoke(new Action(() => ltvUserFriends.Items.Clear()));
            if (usercount > 0L)
            {
                for (ushort count = 0, loopTo = (ushort)(usercount - 1L); count <= loopTo; count++)
                {
                    Utilities.ParseXML(FriendsXML, "/lfm/friends/user", count, ref FriendsNodes);              // get data from xml
                    Invoke(new Action(() => ltvUserFriends.Items.Add(FriendsNodes[0])));                     // add listview item
                    Invoke(new Action(() => ltvUserFriends.Items[count].SubItems.Add(FriendsNodes[1])));     // add subitem 1
                    Invoke(new Action(() => ltvUserFriends.Items[count].SubItems.Add(FriendsNodes[2])));     // add subitem 2
                    Invoke(new Action(() => ltvUserFriends.Items[count].SubItems.Add(FriendsNodes[3])));     // add subitem 3
                    FriendsNodes = new[] { "name", "realname", "url", "registered" };                    // reset nodes
                }
            }

            // report 67% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

            #region Recent Tracks
            #region Fatty Arrays
            Label[,] TrackLabel = new Label[,] { { lblUserRecentTitle1, lblUserRecentArtist1, lblUserRecentAlbum1 }, { lblUserRecentTitle2, lblUserRecentArtist2, lblUserRecentAlbum2 }, { lblUserRecentTitle3, lblUserRecentArtist3, lblUserRecentAlbum3 }, { lblUserRecentTitle4, lblUserRecentArtist4, lblUserRecentAlbum4 }, { lblUserRecentTitle5, lblUserRecentArtist5, lblUserRecentAlbum5 }, { lblUserRecentTitle6, lblUserRecentArtist6, lblUserRecentAlbum6 }, { lblUserRecentTitle7, lblUserRecentArtist7, lblUserRecentAlbum7 }, { lblUserRecentTitle8, lblUserRecentArtist8, lblUserRecentAlbum8 }, { lblUserRecentTitle9, lblUserRecentArtist9, lblUserRecentAlbum9 }, { lblUserRecentTitle10, lblUserRecentArtist10, lblUserRecentAlbum10 }, { lblUserRecentTitle11, lblUserRecentArtist11, lblUserRecentAlbum11 }, { lblUserRecentTitle12, lblUserRecentArtist12, lblUserRecentAlbum12 }, { lblUserRecentTitle13, lblUserRecentArtist13, lblUserRecentAlbum13 }, { lblUserRecentTitle14, lblUserRecentArtist14, lblUserRecentAlbum14 }, { lblUserRecentTitle15, lblUserRecentArtist15, lblUserRecentAlbum15 }, { lblUserRecentTitle16, lblUserRecentArtist16, lblUserRecentAlbum16 }, { lblUserRecentTitle17, lblUserRecentArtist17, lblUserRecentAlbum17 }, { lblUserRecentTitle18, lblUserRecentArtist18, lblUserRecentAlbum18 }, { lblUserRecentTitle19, lblUserRecentArtist19, lblUserRecentAlbum19 }, { lblUserRecentTitle20, lblUserRecentArtist20, lblUserRecentAlbum20 } };

            PictureBox[] TrackArt = new PictureBox[] { picUserRecentArt1, picUserRecentArt2, picUserRecentArt3, picUserRecentArt4, picUserRecentArt5, picUserRecentArt6, picUserRecentArt7, picUserRecentArt8, picUserRecentArt9, picUserRecentArt10, picUserRecentArt11, picUserRecentArt12, picUserRecentArt13, picUserRecentArt14, picUserRecentArt15, picUserRecentArt16, picUserRecentArt17, picUserRecentArt18, picUserRecentArt19, picUserRecentArt20 };
            #endregion

            // xml stuff
            string recenttrackxml = Utilities.CallAPI("user.getRecentTracks", My.MySettingsProperty.Settings.User, "extended=1");
            string[] recenttracknodes = new string[] { "name", "artist/name", "album" };

            for (byte count = 0; count <= 19; count++)
            {
                // parse xml
                Utilities.ParseXML(recenttrackxml, "/lfm/recenttracks/track", count, ref recenttracknodes);

                // set labels
                Invoke(new Action(() => TrackLabel[count, 0].Text = recenttracknodes[0]));
                Invoke(new Action(() => TrackLabel[count, 1].Text = recenttracknodes[1]));
                Invoke(new Action(() => TrackLabel[count, 2].Text = recenttracknodes[2]));

                // set art
                string loadurl = Utilities.ParseImage(recenttrackxml, "/lfm/recenttracks/track", count, 8);
                try
                {
                    TrackArt[count].Load(loadurl);
                }
                catch (Exception ex)
                {
                    TrackArt[count].Image = My.Resources.Resources.imageunavailable;
                }

                // reset nodes
                recenttracknodes = new[] { "name", "artist/name", "album" };
            }

            // report 100% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

        }

        // user loved tracks (separate because of page control)
        public void UserLovedTracksUpdate(string page)
        {
            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            // get xml
            string xml = Utilities.CallAPI("user.getLovedTracks", My.MySettingsProperty.Settings.User, "page=" + page);

            // report 20% progress
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // set labels
            Invoke(new Action(() => lblUserLovedTotalPages.Text = "Total Pages: " + Utilities.ParseMetadata(xml, "totalPages=")));  // set total pages label
            Invoke(new Action(() => lblUserLovedTotalTracks.Text = "Total Tracks: " + Utilities.ParseMetadata(xml, "total=")));     // set total tracks label
            Invoke(new Action(() => ltvUserLovedTracks.Items.Clear()));

            // report 30% progress
            Utilities.progress = (ushort)(Utilities.progress + 10);
            UpdateProgressChange();

            // set max numeric box value
            ushort totalpages;
            ushort.TryParse(Utilities.ParseMetadata(xml, "totalPages="), out totalpages);
            Invoke(new Action(() => nudUserLovedPage.Maximum = totalpages));

            // report 40% progress
            Utilities.progress = (ushort)(Utilities.progress + 10);
            UpdateProgressChange();

            // parse information to put in listview
            string[] xmlnodes = new string[] { "name", "artist/name", "date" };
            for (byte count = 0; count <= 49; count++)
            {
                Utilities.ParseXML(xml, "/lfm/lovedtracks/track", count, ref xmlnodes);

                // add items if no error
                if (xmlnodes[0].Contains("ERROR:") == false)
                {
                    Invoke(new Action(() => ltvUserLovedTracks.Items.Add(xmlnodes[0])));
                    Invoke(new Action(() => ltvUserLovedTracks.Items[count].SubItems.Add(xmlnodes[1])));
                    Invoke(new Action(() => ltvUserLovedTracks.Items[count].SubItems.Add(xmlnodes[2])));
                }
                // reset nodes
                xmlnodes = new[] { "name", "artist/name", "date" };

                // report 40-90% progress
                Utilities.progress = (ushort)(Utilities.progress + 1);
                UpdateProgressChange();
            }

            // set numeric box value back to current page
            ushort pagenum;
            string metadata = Utilities.ParseMetadata(xml, "page=");
            ushort.TryParse(metadata, out pagenum);
            if (nudUserLovedPage.Maximum > 0m)
            {
                Invoke(new Action(() => nudUserLovedPage.Value = pagenum));
            }
            else
            {
                Invoke(new Action(() => nudUserLovedPage.Value = 0m));
            }

            // report 100% progress
            Utilities.progress = (ushort)(Utilities.progress + 10);
            UpdateProgressChange();
        }

        // user charts (separate because of date control)
        public void UserChartsUpdate()
        {
            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            #region Fatty Arrays
            Label[,] TopTrackLabels = new Label[,] { { lblUserTopTrackTitle1, lblUserTopTrackArtist1, lblUserTopTrackAlbum1, lblUserTopTrackPlaycount1 }, { lblUserTopTrackTitle2, lblUserTopTrackArtist2, lblUserTopTrackAlbum2, lblUserTopTrackPlaycount2 }, { lblUserTopTrackTitle3, lblUserTopTrackArtist3, lblUserTopTrackAlbum3, lblUserTopTrackPlaycount3 }, { lblUserTopTrackTitle4, lblUserTopTrackArtist4, lblUserTopTrackAlbum4, lblUserTopTrackPlaycount4 }, { lblUserTopTrackTitle5, lblUserTopTrackArtist5, lblUserTopTrackAlbum5, lblUserTopTrackPlaycount5 }, { lblUserTopTrackTitle6, lblUserTopTrackArtist6, lblUserTopTrackAlbum6, lblUserTopTrackPlaycount6 }, { lblUserTopTrackTitle7, lblUserTopTrackArtist7, lblUserTopTrackAlbum7, lblUserTopTrackPlaycount7 }, { lblUserTopTrackTitle8, lblUserTopTrackArtist8, lblUserTopTrackAlbum8, lblUserTopTrackPlaycount8 }, { lblUserTopTrackTitle9, lblUserTopTrackArtist9, lblUserTopTrackAlbum9, lblUserTopTrackPlaycount9 }, { lblUserTopTrackTitle10, lblUserTopTrackArtist10, lblUserTopTrackAlbum10, lblUserTopTrackPlaycount10 }, { lblUserTopTrackTitle11, lblUserTopTrackArtist11, lblUserTopTrackAlbum11, lblUserTopTrackPlaycount11 }, { lblUserTopTrackTitle12, lblUserTopTrackArtist12, lblUserTopTrackAlbum12, lblUserTopTrackPlaycount12 }, { lblUserTopTrackTitle13, lblUserTopTrackArtist13, lblUserTopTrackAlbum13, lblUserTopTrackPlaycount13 }, { lblUserTopTrackTitle14, lblUserTopTrackArtist14, lblUserTopTrackAlbum14, lblUserTopTrackPlaycount14 }, { lblUserTopTrackTitle15, lblUserTopTrackArtist15, lblUserTopTrackAlbum15, lblUserTopTrackPlaycount15 }, { lblUserTopTrackTitle16, lblUserTopTrackArtist16, lblUserTopTrackAlbum16, lblUserTopTrackPlaycount16 }, { lblUserTopTrackTitle17, lblUserTopTrackArtist17, lblUserTopTrackAlbum17, lblUserTopTrackPlaycount17 }, { lblUserTopTrackTitle18, lblUserTopTrackArtist18, lblUserTopTrackAlbum18, lblUserTopTrackPlaycount18 }, { lblUserTopTrackTitle19, lblUserTopTrackArtist19, lblUserTopTrackAlbum19, lblUserTopTrackPlaycount19 }, { lblUserTopTrackTitle20, lblUserTopTrackArtist20, lblUserTopTrackAlbum20, lblUserTopTrackPlaycount20 } };

            PictureBox[] TopTrackArt = new PictureBox[] { picUserTopTrackArt1, picUserTopTrackArt2, picUserTopTrackArt3, picUserTopTrackArt4, picUserTopTrackArt5, picUserTopTrackArt6, picUserTopTrackArt7, picUserTopTrackArt8, picUserTopTrackArt9, picUserTopTrackArt10, picUserTopTrackArt11, picUserTopTrackArt12, picUserTopTrackArt13, picUserTopTrackArt14, picUserTopTrackArt15, picUserTopTrackArt16, picUserTopTrackArt17, picUserTopTrackArt18, picUserTopTrackArt19, picUserTopTrackArt20 };

            Label[,] TopArtistLabels = new Label[,] { { lblUserTopArtist1, lblUserTopArtistPlaycount1 }, { lblUserTopArtist2, lblUserTopArtistPlaycount2 }, { lblUserTopArtist3, lblUserTopArtistPlaycount3 }, { lblUserTopArtist4, lblUserTopArtistPlaycount4 }, { lblUserTopArtist5, lblUserTopArtistPlaycount5 }, { lblUserTopArtist6, lblUserTopArtistPlaycount6 }, { lblUserTopArtist7, lblUserTopArtistPlaycount7 }, { lblUserTopArtist8, lblUserTopArtistPlaycount8 }, { lblUserTopArtist9, lblUserTopArtistPlaycount9 }, { lblUserTopArtist10, lblUserTopArtistPlaycount10 }, { lblUserTopArtist11, lblUserTopArtistPlaycount11 }, { lblUserTopArtist12, lblUserTopArtistPlaycount12 }, { lblUserTopArtist13, lblUserTopArtistPlaycount13 }, { lblUserTopArtist14, lblUserTopArtistPlaycount14 }, { lblUserTopArtist15, lblUserTopArtistPlaycount15 }, { lblUserTopArtist16, lblUserTopArtistPlaycount16 }, { lblUserTopArtist17, lblUserTopArtistPlaycount17 }, { lblUserTopArtist18, lblUserTopArtistPlaycount18 }, { lblUserTopArtist19, lblUserTopArtistPlaycount19 }, { lblUserTopArtist20, lblUserTopArtistPlaycount20 } };

            Label[,] TopAlbumLabels = new Label[,] { { lblUserTopAlbum1, lblUserTopAlbumArtist1, lblUserTopAlbumPlaycount1 }, { lblUserTopAlbum2, lblUserTopAlbumArtist2, lblUserTopAlbumPlaycount2 }, { lblUserTopAlbum3, lblUserTopAlbumArtist3, lblUserTopAlbumPlaycount3 }, { lblUserTopAlbum4, lblUserTopAlbumArtist4, lblUserTopAlbumPlaycount4 }, { lblUserTopAlbum5, lblUserTopAlbumArtist5, lblUserTopAlbumPlaycount5 }, { lblUserTopAlbum6, lblUserTopAlbumArtist6, lblUserTopAlbumPlaycount6 }, { lblUserTopAlbum7, lblUserTopAlbumArtist7, lblUserTopAlbumPlaycount7 }, { lblUserTopAlbum8, lblUserTopAlbumArtist8, lblUserTopAlbumPlaycount8 }, { lblUserTopAlbum9, lblUserTopAlbumArtist9, lblUserTopAlbumPlaycount9 }, { lblUserTopAlbum10, lblUserTopAlbumArtist10, lblUserTopAlbumPlaycount10 }, { lblUserTopAlbum11, lblUserTopAlbumArtist11, lblUserTopAlbumPlaycount11 }, { lblUserTopAlbum12, lblUserTopAlbumArtist12, lblUserTopAlbumPlaycount12 }, { lblUserTopAlbum13, lblUserTopAlbumArtist13, lblUserTopAlbumPlaycount13 }, { lblUserTopAlbum14, lblUserTopAlbumArtist14, lblUserTopAlbumPlaycount14 }, { lblUserTopAlbum15, lblUserTopAlbumArtist15, lblUserTopAlbumPlaycount15 }, { lblUserTopAlbum16, lblUserTopAlbumArtist16, lblUserTopAlbumPlaycount16 }, { lblUserTopAlbum17, lblUserTopAlbumArtist17, lblUserTopAlbumPlaycount17 }, { lblUserTopAlbum18, lblUserTopAlbumArtist18, lblUserTopAlbumPlaycount18 }, { lblUserTopAlbum19, lblUserTopAlbumArtist19, lblUserTopAlbumPlaycount19 }, { lblUserTopAlbum20, lblUserTopAlbumArtist20, lblUserTopAlbumPlaycount20 } };

            PictureBox[] TopAlbumArt = new PictureBox[] { picUserTopAlbumArt1, picUserTopAlbumArt2, picUserTopAlbumArt3, picUserTopAlbumArt4, picUserTopAlbumArt5, picUserTopAlbumArt6, picUserTopAlbumArt7, picUserTopAlbumArt8, picUserTopAlbumArt9, picUserTopAlbumArt10, picUserTopAlbumArt11, picUserTopAlbumArt12, picUserTopAlbumArt13, picUserTopAlbumArt14, picUserTopAlbumArt15, picUserTopAlbumArt16, picUserTopAlbumArt17, picUserTopAlbumArt18, picUserTopAlbumArt19, picUserTopAlbumArt20 };

            // report 1% progress
            Utilities.progress = (ushort)(Utilities.progress + 1);
            UpdateProgressChange();
            #endregion

            #region Tracks
            // dim stuff
            string TopTrackXML = Utilities.CallAPI("user.getTopTracks", My.MySettingsProperty.Settings.User, "limit=20");
            string[] TopTrackNodes = new string[] { "name", "artist/name", "playcount" };
            string TrackInfoXML;
            string[] TrackInfoNodes = new string[] { "title" };
            uint numberholder;

            // get track info for 20 tracks
            for (byte count = 0; count <= 19; count++)
            {
                // get initial track data (title and artist) from toptracks
                Utilities.ParseXML(TopTrackXML, "/lfm/toptracks/track", count, ref TopTrackNodes);

                // get track info xml
                TrackInfoXML = Utilities.CallAPI("track.getInfo", "", "track=" + TopTrackNodes[0].Replace(" ", "+"), "artist=" + TopTrackNodes[1].Replace(" ", "+"));

                // get track album from trackinfoxml
                Utilities.ParseXML(TrackInfoXML, "/lfm/track/album", 0U, ref TrackInfoNodes);

                // detect errors and set to "(Unavailable)"
                for (byte counter2 = 0; counter2 <= 2; counter2++)
                {
                    if (TopTrackNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TopTrackNodes[counter2] = "(Unavailable)";
                    }
                }
                if (TrackInfoNodes[0].Contains("Object reference not set to an instance of an object") == true)
                {
                    TrackInfoNodes[0] = "(Unavailable)";
                }

                // set labels
                Invoke(new Action(() => TopTrackLabels[count, 0].Text = TopTrackNodes[0]));     // title
                Invoke(new Action(() => TopTrackLabels[count, 1].Text = TopTrackNodes[1]));     // artist
                Invoke(new Action(() => TopTrackLabels[count, 2].Text = TrackInfoNodes[0]));    // album
                                                                                                // apply formatting to playcount
                uint.TryParse(TopTrackNodes[2], out numberholder);
                Invoke(new Action(() => TopTrackLabels[count, 3].Text = numberholder.ToString("N0")));

                // set picturebox
                try
                {
                    TopTrackArt[count].Load(Utilities.ParseImage(TrackInfoXML, "/lfm/track/album/image", 1));
                }
                catch (Exception ex)
                {
                    TopTrackArt[count].Image = My.Resources.Resources.imageunavailable;
                }

                // reset node arrays
                TopTrackNodes = new[] { "name", "artist/name", "playcount" };
                TrackInfoNodes = new[] { "title" };
            }

            // report 34% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

            #region Artists
            // dim stuff
            string TopArtistXML = Utilities.CallAPI("user.getTopArtists", My.MySettingsProperty.Settings.User, "limit=20");
            string[] TopArtistNodes = new string[] { "name", "playcount" };

            for (byte count = 0; count <= 19; count++)
            {
                // get artist data (name and playcount)
                Utilities.ParseXML(TopArtistXML, "/lfm/topartists/artist", count, ref TopArtistNodes);

                // detect errors and set to unavailable
                for (byte counter2 = 0; counter2 <= 1; counter2++)
                {
                    if (TopArtistNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TopArtistNodes[counter2] = "(Unavailable)";
                    }
                }

                // set labels
                Invoke(new Action(() => TopArtistLabels[count, 0].Text = TopArtistNodes[0]));     // title
                                                                                                  // apply formatting to playcount
                uint.TryParse(TopArtistNodes[1], out numberholder);
                Invoke(new Action(() => TopArtistLabels[count, 1].Text = numberholder.ToString("N0")));

                // reset node arrays
                TopArtistNodes = new[] { "name", "playcount" };
            }

            // report 67% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

            #region Albums
            // dim stuff
            string TopAlbumXML = Utilities.CallAPI("user.getTopAlbums", My.MySettingsProperty.Settings.User, "limit=20");
            string[] TopAlbumNodes = new string[] { "name", "artist/name", "playcount" };

            for (byte count = 0; count <= 19; count++)
            {
                // get album data (name, artist, playcount)
                Utilities.ParseXML(TopAlbumXML, "/lfm/topalbums/album", count, ref TopAlbumNodes);

                // detect errors and set to unavailable
                for (byte counter2 = 0; counter2 <= 2; counter2++)
                {
                    if (TopAlbumNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TopAlbumNodes[counter2] = "(Unavailable)";
                    }
                }

                // set labels
                Invoke(new Action(() => TopAlbumLabels[count, 0].Text = TopAlbumNodes[0]));     // album
                Invoke(new Action(() => TopAlbumLabels[count, 1].Text = TopAlbumNodes[1]));     // artist
                                                                                                // apply formatting to playcount
                uint.TryParse(TopAlbumNodes[2], out numberholder);
                Invoke(new Action(() => TopAlbumLabels[count, 2].Text = numberholder.ToString("N0")));

                // set picturebox
                try
                {
                    TopAlbumArt[count].Load(Utilities.ParseImage(TopAlbumXML, "/lfm/topalbums/album", count, 6));
                }
                catch (Exception ex)
                {
                    TopAlbumArt[count].Image = My.Resources.Resources.imageunavailable;
                }

                // reset node arrays
                TopAlbumNodes = new[] { "name", "artist/name", "playcount" };
            }

            // report 100% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

        }

        public void UserChartsUpdate(uint unixfrom, uint unixto)
        {
            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            #region To>From Check
            if (unixfrom > unixto)
            {
                Invoke(new Action(() => MessageBox.Show("From date must be before to date", "User Charts", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                Utilities.progress = (ushort)(Utilities.progress + 100);
                UpdateProgressChange();
                return;
            }
            #endregion

            #region Fatty Arrays
            Label[,] TopTrackLabels = new Label[,] { { lblUserTopTrackTitle1, lblUserTopTrackArtist1, lblUserTopTrackAlbum1, lblUserTopTrackPlaycount1 }, { lblUserTopTrackTitle2, lblUserTopTrackArtist2, lblUserTopTrackAlbum2, lblUserTopTrackPlaycount2 }, { lblUserTopTrackTitle3, lblUserTopTrackArtist3, lblUserTopTrackAlbum3, lblUserTopTrackPlaycount3 }, { lblUserTopTrackTitle4, lblUserTopTrackArtist4, lblUserTopTrackAlbum4, lblUserTopTrackPlaycount4 }, { lblUserTopTrackTitle5, lblUserTopTrackArtist5, lblUserTopTrackAlbum5, lblUserTopTrackPlaycount5 }, { lblUserTopTrackTitle6, lblUserTopTrackArtist6, lblUserTopTrackAlbum6, lblUserTopTrackPlaycount6 }, { lblUserTopTrackTitle7, lblUserTopTrackArtist7, lblUserTopTrackAlbum7, lblUserTopTrackPlaycount7 }, { lblUserTopTrackTitle8, lblUserTopTrackArtist8, lblUserTopTrackAlbum8, lblUserTopTrackPlaycount8 }, { lblUserTopTrackTitle9, lblUserTopTrackArtist9, lblUserTopTrackAlbum9, lblUserTopTrackPlaycount9 }, { lblUserTopTrackTitle10, lblUserTopTrackArtist10, lblUserTopTrackAlbum10, lblUserTopTrackPlaycount10 }, { lblUserTopTrackTitle11, lblUserTopTrackArtist11, lblUserTopTrackAlbum11, lblUserTopTrackPlaycount11 }, { lblUserTopTrackTitle12, lblUserTopTrackArtist12, lblUserTopTrackAlbum12, lblUserTopTrackPlaycount12 }, { lblUserTopTrackTitle13, lblUserTopTrackArtist13, lblUserTopTrackAlbum13, lblUserTopTrackPlaycount13 }, { lblUserTopTrackTitle14, lblUserTopTrackArtist14, lblUserTopTrackAlbum14, lblUserTopTrackPlaycount14 }, { lblUserTopTrackTitle15, lblUserTopTrackArtist15, lblUserTopTrackAlbum15, lblUserTopTrackPlaycount15 }, { lblUserTopTrackTitle16, lblUserTopTrackArtist16, lblUserTopTrackAlbum16, lblUserTopTrackPlaycount16 }, { lblUserTopTrackTitle17, lblUserTopTrackArtist17, lblUserTopTrackAlbum17, lblUserTopTrackPlaycount17 }, { lblUserTopTrackTitle18, lblUserTopTrackArtist18, lblUserTopTrackAlbum18, lblUserTopTrackPlaycount18 }, { lblUserTopTrackTitle19, lblUserTopTrackArtist19, lblUserTopTrackAlbum19, lblUserTopTrackPlaycount19 }, { lblUserTopTrackTitle20, lblUserTopTrackArtist20, lblUserTopTrackAlbum20, lblUserTopTrackPlaycount20 } };

            PictureBox[] TopTrackArt = new PictureBox[] { picUserTopTrackArt1, picUserTopTrackArt2, picUserTopTrackArt3, picUserTopTrackArt4, picUserTopTrackArt5, picUserTopTrackArt6, picUserTopTrackArt7, picUserTopTrackArt8, picUserTopTrackArt9, picUserTopTrackArt10, picUserTopTrackArt11, picUserTopTrackArt12, picUserTopTrackArt13, picUserTopTrackArt14, picUserTopTrackArt15, picUserTopTrackArt16, picUserTopTrackArt17, picUserTopTrackArt18, picUserTopTrackArt19, picUserTopTrackArt20 };

            Label[,] TopArtistLabels = new Label[,] { { lblUserTopArtist1, lblUserTopArtistPlaycount1 }, { lblUserTopArtist2, lblUserTopArtistPlaycount2 }, { lblUserTopArtist3, lblUserTopArtistPlaycount3 }, { lblUserTopArtist4, lblUserTopArtistPlaycount4 }, { lblUserTopArtist5, lblUserTopArtistPlaycount5 }, { lblUserTopArtist6, lblUserTopArtistPlaycount6 }, { lblUserTopArtist7, lblUserTopArtistPlaycount7 }, { lblUserTopArtist8, lblUserTopArtistPlaycount8 }, { lblUserTopArtist9, lblUserTopArtistPlaycount9 }, { lblUserTopArtist10, lblUserTopArtistPlaycount10 }, { lblUserTopArtist11, lblUserTopArtistPlaycount11 }, { lblUserTopArtist12, lblUserTopArtistPlaycount12 }, { lblUserTopArtist13, lblUserTopArtistPlaycount13 }, { lblUserTopArtist14, lblUserTopArtistPlaycount14 }, { lblUserTopArtist15, lblUserTopArtistPlaycount15 }, { lblUserTopArtist16, lblUserTopArtistPlaycount16 }, { lblUserTopArtist17, lblUserTopArtistPlaycount17 }, { lblUserTopArtist18, lblUserTopArtistPlaycount18 }, { lblUserTopArtist19, lblUserTopArtistPlaycount19 }, { lblUserTopArtist20, lblUserTopArtistPlaycount20 } };

            Label[,] TopAlbumLabels = new Label[,] { { lblUserTopAlbum1, lblUserTopAlbumArtist1, lblUserTopAlbumPlaycount1 }, { lblUserTopAlbum2, lblUserTopAlbumArtist2, lblUserTopAlbumPlaycount2 }, { lblUserTopAlbum3, lblUserTopAlbumArtist3, lblUserTopAlbumPlaycount3 }, { lblUserTopAlbum4, lblUserTopAlbumArtist4, lblUserTopAlbumPlaycount4 }, { lblUserTopAlbum5, lblUserTopAlbumArtist5, lblUserTopAlbumPlaycount5 }, { lblUserTopAlbum6, lblUserTopAlbumArtist6, lblUserTopAlbumPlaycount6 }, { lblUserTopAlbum7, lblUserTopAlbumArtist7, lblUserTopAlbumPlaycount7 }, { lblUserTopAlbum8, lblUserTopAlbumArtist8, lblUserTopAlbumPlaycount8 }, { lblUserTopAlbum9, lblUserTopAlbumArtist9, lblUserTopAlbumPlaycount9 }, { lblUserTopAlbum10, lblUserTopAlbumArtist10, lblUserTopAlbumPlaycount10 }, { lblUserTopAlbum11, lblUserTopAlbumArtist11, lblUserTopAlbumPlaycount11 }, { lblUserTopAlbum12, lblUserTopAlbumArtist12, lblUserTopAlbumPlaycount12 }, { lblUserTopAlbum13, lblUserTopAlbumArtist13, lblUserTopAlbumPlaycount13 }, { lblUserTopAlbum14, lblUserTopAlbumArtist14, lblUserTopAlbumPlaycount14 }, { lblUserTopAlbum15, lblUserTopAlbumArtist15, lblUserTopAlbumPlaycount15 }, { lblUserTopAlbum16, lblUserTopAlbumArtist16, lblUserTopAlbumPlaycount16 }, { lblUserTopAlbum17, lblUserTopAlbumArtist17, lblUserTopAlbumPlaycount17 }, { lblUserTopAlbum18, lblUserTopAlbumArtist18, lblUserTopAlbumPlaycount18 }, { lblUserTopAlbum19, lblUserTopAlbumArtist19, lblUserTopAlbumPlaycount19 }, { lblUserTopAlbum20, lblUserTopAlbumArtist20, lblUserTopAlbumPlaycount20 } };

            PictureBox[] TopAlbumArt = new PictureBox[] { picUserTopAlbumArt1, picUserTopAlbumArt2, picUserTopAlbumArt3, picUserTopAlbumArt4, picUserTopAlbumArt5, picUserTopAlbumArt6, picUserTopAlbumArt7, picUserTopAlbumArt8, picUserTopAlbumArt9, picUserTopAlbumArt10, picUserTopAlbumArt11, picUserTopAlbumArt12, picUserTopAlbumArt13, picUserTopAlbumArt14, picUserTopAlbumArt15, picUserTopAlbumArt16, picUserTopAlbumArt17, picUserTopAlbumArt18, picUserTopAlbumArt19, picUserTopAlbumArt20 };

            // report 1% progress
            Utilities.progress = (ushort)(Utilities.progress + 1);
            UpdateProgressChange();
            #endregion

            #region Tracks
            // dim stuff
            string TopTrackXML = Utilities.CallAPI("user.getWeeklyTrackChart", My.MySettingsProperty.Settings.User, "from=" + unixfrom.ToString(), "to=" + unixto.ToString());
            string[] TopTrackNodes = new string[] { "name", "artist", "playcount" };
            string TrackInfoXML;
            string[] TrackInfoNodes = new string[] { "title" };
            uint numberholder;

            // get track info for 20 tracks
            for (byte count = 0; count <= 19; count++)
            {
                // get initial track data (title and artist) from toptracks
                Utilities.ParseXML(TopTrackXML, "/lfm/weeklytrackchart/track", count, ref TopTrackNodes);

                // get track info xml
                TrackInfoXML = Utilities.CallAPI("track.getInfo", "", "track=" + TopTrackNodes[0].Replace(" ", "+"), "artist=" + TopTrackNodes[1].Replace(" ", "+"));

                // get track album from trackinfoxml
                Utilities.ParseXML(TrackInfoXML, "/lfm/track/album", 0U, ref TrackInfoNodes);

                // detect errors and set to "(Unavailable)"
                for (byte counter2 = 0; counter2 <= 2; counter2++)
                {
                    if (TopTrackNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TopTrackNodes[counter2] = "(Unavailable)";
                    }
                }
                if (TrackInfoNodes[0].Contains("Object reference not set to an instance of an object") == true)
                {
                    TrackInfoNodes[0] = "(Unavailable)";
                }

                // set labels
                Invoke(new Action(() => TopTrackLabels[count, 0].Text = TopTrackNodes[0]));     // title
                Invoke(new Action(() => TopTrackLabels[count, 1].Text = TopTrackNodes[1]));     // artist
                Invoke(new Action(() => TopTrackLabels[count, 2].Text = TrackInfoNodes[0]));    // album
                                                                                                // apply formatting to playcount
                uint.TryParse(TopTrackNodes[2], out numberholder);
                Invoke(new Action(() => TopTrackLabels[count, 3].Text = numberholder.ToString("N0")));

                // set picturebox
                try
                {
                    TopTrackArt[count].Load(Utilities.ParseImage(TrackInfoXML, "/lfm/track/album/image", 1));
                }
                catch (Exception ex)
                {
                    TopTrackArt[count].Image = My.Resources.Resources.imageunavailable;
                }

                // reset node arrays
                TopTrackNodes = new[] { "name", "artist", "playcount" };
                TrackInfoNodes = new[] { "title" };
            }

            // report 34% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

            #region Artists
            // dim stuff
            string TopArtistXML = Utilities.CallAPI("user.getWeeklyArtistChart", My.MySettingsProperty.Settings.User, "from=" + unixfrom.ToString(), "to=" + unixto.ToString());
            string[] TopArtistNodes = new string[] { "name", "playcount" };

            for (byte count = 0; count <= 19; count++)
            {
                // get artist data (name and playcount)
                Utilities.ParseXML(TopArtistXML, "/lfm/weeklyartistchart/artist", count, ref TopArtistNodes);

                // detect errors and set to unavailable
                for (byte counter2 = 0; counter2 <= 1; counter2++)
                {
                    if (TopArtistNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TopArtistNodes[counter2] = "(Unavailable)";
                    }
                }

                // set labels
                Invoke(new Action(() => TopArtistLabels[count, 0].Text = TopArtistNodes[0]));     // title
                                                                                                  // apply formatting to playcount
                uint.TryParse(TopArtistNodes[1], out numberholder);
                Invoke(new Action(() => TopArtistLabels[count, 1].Text = numberholder.ToString("N0")));

                // reset node arrays
                TopArtistNodes = new[] { "name", "playcount" };
            }

            // report 67% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

            #region Albums
            // dim stuff
            string TopAlbumXML = Utilities.CallAPI("user.getWeeklyAlbumChart", My.MySettingsProperty.Settings.User, "from=" + unixfrom.ToString(), "to=" + unixto.ToString());
            string[] TopAlbumNodes = new string[] { "name", "artist", "playcount" };
            string AlbumInfoXML;

            for (byte count = 0; count <= 19; count++)
            {
                // get album data (name, artist, playcount)
                Utilities.ParseXML(TopAlbumXML, "/lfm/weeklyalbumchart/album", count, ref TopAlbumNodes);

                // get album info for image
                AlbumInfoXML = Utilities.CallAPI("album.getInfo", My.MySettingsProperty.Settings.User, "album=" + TopAlbumNodes[0].Replace(" ", "+"), "artist=" + TopAlbumNodes[1].Replace(" ", "+"));

                // detect errors and set to unavailable
                for (byte counter2 = 0; counter2 <= 2; counter2++)
                {
                    if (TopAlbumNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TopAlbumNodes[counter2] = "(Unavailable)";
                    }
                }

                // set labels
                Invoke(new Action(() => TopAlbumLabels[count, 0].Text = TopAlbumNodes[0]));     // album
                Invoke(new Action(() => TopAlbumLabels[count, 1].Text = TopAlbumNodes[1]));     // artist
                                                                                                // apply formatting to playcount
                uint.TryParse(TopAlbumNodes[2], out numberholder);
                Invoke(new Action(() => TopAlbumLabels[count, 2].Text = numberholder.ToString("N0")));

                // set picturebox
                try
                {
                    TopAlbumArt[count].Load(Utilities.ParseImage(AlbumInfoXML, "/lfm/album/image", 1));
                }
                catch (Exception ex)
                {
                    TopAlbumArt[count].Image = My.Resources.Resources.imageunavailable;
                }

                // reset node arrays
                TopAlbumNodes = new[] { "name", "artist", "playcount" };
            }

            // report 100% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

        }

        public void UserHistoryUpdate()
        {
            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            // determine whether date is used or not
            string historyXML;
            if (radUserAllTime.Checked == true)
            {
                historyXML = Utilities.CallAPI("user.getRecentTracks", My.MySettingsProperty.Settings.User, "page=" + nudUserHistoryPage.Value.ToString());
            }
            else
            {
                historyXML = Utilities.CallAPI("user.getRecentTracks", My.MySettingsProperty.Settings.User, "page=" + nudUserHistoryPage.Value.ToString(), "from=" + (Utilities.DateToUnix(dtpUserFrom.Value.Date) - Utilities.timezoneoffset), "to=" + (Utilities.DateToUnix(dtpUserTo.Value.Date) - Utilities.timezoneoffset));
            }

            // determine whether something is now playing 
            byte metadataOffset = 1;
            if (Utilities.StrCount(historyXML, "</track>") > Utilities.StrCount(historyXML, "date uts="))
            {
                metadataOffset = 0;
            }

            // report 20% progress
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // set labels
            Invoke(new Action(() => lblUserHistoryTotalPages.Text = "Total Pages: " + Utilities.ParseMetadata(historyXML, "totalPages=")));  // set total pages label
            Invoke(new Action(() => lblUserHistoryTotalTracks.Text = "Total Tracks: " + Utilities.ParseMetadata(historyXML, "total=")));    // set total tracks label
            Invoke(new Action(() => ltvUserHistory.Items.Clear()));

            // report 30% progress
            Utilities.progress = (ushort)(Utilities.progress + 10);
            UpdateProgressChange();

            // set max numeric box value
            ushort totalpages;
            ushort.TryParse(Utilities.ParseMetadata(historyXML, "totalPages="), out totalpages);
            Invoke(new Action(() => nudUserHistoryPage.Maximum = totalpages));

            // report 40% progress
            Utilities.progress = (ushort)(Utilities.progress + 10);
            UpdateProgressChange();

            // parse xml and add to list
            string[] historynodes = new string[] { "name", "artist", "album", "date" };
            for (byte count = 0; count <= 50; count++)
            {
                // parse xml
                Utilities.ParseXML(historyXML, "/lfm/recenttracks/track", count, ref historynodes);

                // add items if no error
                int counterrors = 0;
                if (historynodes[0].Contains("ERROR:") == false)
                {
                    Invoke(new Action(() => ltvUserHistory.Items.Add(historynodes[0])));
                    Invoke(new Action(() => ltvUserHistory.Items[count - counterrors].SubItems.Add(historynodes[1])));
                    Invoke(new Action(() => ltvUserHistory.Items[count - counterrors].SubItems.Add(historynodes[2])));
                    // check for date error due to now playing
                    if (historynodes[3].Contains("ERROR: ") == false)
                    {
                        Invoke(new Action(() => ltvUserHistory.Items[count].SubItems.Add(Utilities.UnixToDate((uint)Math.Round(Conversions.ToDouble(Utilities.ParseMetadata(historyXML, "date uts=", (uint)(count - counterrors + metadataOffset))) + Utilities.timezoneoffset)).ToString("G"))));
                    }
                    else
                    {
                        Invoke(new Action(() => ltvUserHistory.Items[count].SubItems.Add("Now Playing")));
                    }
                }
                else
                {
                    counterrors += 1;
                }
                // reset nodes
                historynodes = new[] { "name", "artist", "album", "date" };

                // report 40-91% progress
                Utilities.progress = (ushort)(Utilities.progress + 1);
                UpdateProgressChange();
            }

            // set numeric box value back to current page
            ushort pagenum;
            string metadata = Utilities.ParseMetadata(historyXML, "page=");
            ushort.TryParse(metadata, out pagenum);
            if (nudUserHistoryPage.Maximum > 0m && nudUserHistoryPage.Minimum > 0m)
            {
                Invoke(new Action(() => nudUserHistoryPage.Value = pagenum));
            }
            else
            {
                Invoke(new Action(() => nudUserHistoryPage.Minimum = 0m));
                Invoke(new Action(() => nudUserHistoryPage.Value = 0m));
            }

            // report 100% progress
            Utilities.progress = (ushort)(Utilities.progress + 9);
            UpdateProgressChange();
        }
        #endregion

        #region User Lookup
        public void UpdateUserL()
        {
            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            #region Info
            // xml stuff
            string UserInfoXML = Utilities.CallAPI("user.getInfo", Utilities.userlookup);
            string[] UserInfoNodes = new string[] { "name", "realname", "url", "country", "age", "gender", "playcount", "playlists", "registered" };

            // parse xml
            Utilities.ParseXML(UserInfoXML, "/lfm/user", 0U, ref UserInfoNodes);
            try
            {
                picUserL.Load(Utilities.ParseImage(UserInfoXML, "/lfm/user/image", 3));
            }
            catch (Exception ex)
            {
                picUserL.Image = My.Resources.Resources.imageunavailable;
            }

            // convert registered date to datetime
            uint unixtime;
            uint.TryParse(UserInfoNodes[8], out unixtime);
            var RegisteredDateTime = Utilities.UnixToDate((uint)(unixtime + Utilities.timezoneoffset));

            // gender formatting
            switch (UserInfoNodes[5] ?? "")
            {
                case "m":
                    {
                        UserInfoNodes[5] = "Male";
                        break;
                    }
                case "f":
                    {
                        UserInfoNodes[5] = "Female";
                        break;
                    }

                default:
                    {
                        UserInfoNodes[5] = "Not Specified";
                        break;
                    }
            }

            // age formatting
            if (UserInfoNodes[4] == "0")
            {
                UserInfoNodes[4] = "Not Specified";
            }

            // playcount formatting
            UserInfoNodes[6] = Conversions.ToInteger(UserInfoNodes[6]).ToString("N0");

            // create textbox data
            Invoke(new Action(() => txtUserLInfo.Clear()));
            Invoke(new Action(() => txtUserLInfo.Text = "Name" + Constants.vbCrLf + UserInfoNodes[0] + Constants.vbCrLf + Constants.vbCrLf + "Real Name" + Constants.vbCrLf + UserInfoNodes[1] + Constants.vbCrLf + Constants.vbCrLf + "URL" + Constants.vbCrLf + UserInfoNodes[2] + Constants.vbCrLf + Constants.vbCrLf + "Country" + Constants.vbCrLf + UserInfoNodes[3] + Constants.vbCrLf + Constants.vbCrLf + "Age" + Constants.vbCrLf + UserInfoNodes[4] + Constants.vbCrLf + Constants.vbCrLf + "Gender" + Constants.vbCrLf + UserInfoNodes[5] + Constants.vbCrLf + Constants.vbCrLf + "Playcount" + Constants.vbCrLf + UserInfoNodes[6] + Constants.vbCrLf + Constants.vbCrLf + "Playlists" + Constants.vbCrLf + UserInfoNodes[7] + Constants.vbCrLf + Constants.vbCrLf + "Registered" + Constants.vbCrLf + RegisteredDateTime.ToString()));

            // format textbox
            var BoldFont = new Font("Segoe UI", 10f, FontStyle.Bold);
            // name
            Invoke(new Action(() => txtUserLInfo.SelectionStart = 0));
            Invoke(new Action(() => txtUserLInfo.SelectionLength = "Text".Length));
            Invoke(new Action(() => txtUserLInfo.SelectionFont = BoldFont));
            // real name
            Invoke(new Action(() => txtUserLInfo.SelectionStart = Strings.InStr(txtUserLInfo.Text, "Real Name") - 1));
            Invoke(new Action(() => txtUserLInfo.SelectionLength = "Real Name".Length));
            Invoke(new Action(() => txtUserLInfo.SelectionFont = BoldFont));
            // URL
            Invoke(new Action(() => txtUserLInfo.SelectionStart = Strings.InStr(txtUserLInfo.Text, "URL") - 1));
            Invoke(new Action(() => txtUserLInfo.SelectionLength = "URL".Length));
            Invoke(new Action(() => txtUserLInfo.SelectionFont = BoldFont));
            // country
            Invoke(new Action(() => txtUserLInfo.SelectionStart = Strings.InStr(txtUserLInfo.Text, "Country") - 1));
            Invoke(new Action(() => txtUserLInfo.SelectionLength = "Country".Length));
            Invoke(new Action(() => txtUserLInfo.SelectionFont = BoldFont));
            // age
            Invoke(new Action(() => txtUserLInfo.SelectionStart = Strings.InStr(txtUserLInfo.Text, "Age") - 1));
            Invoke(new Action(() => txtUserLInfo.SelectionLength = "Age".Length));
            Invoke(new Action(() => txtUserLInfo.SelectionFont = BoldFont));
            // gender
            Invoke(new Action(() => txtUserLInfo.SelectionStart = Strings.InStr(txtUserLInfo.Text, "Gender") - 1));
            Invoke(new Action(() => txtUserLInfo.SelectionLength = "Gender".Length));
            Invoke(new Action(() => txtUserLInfo.SelectionFont = BoldFont));
            // playcount
            Invoke(new Action(() => txtUserLInfo.SelectionStart = Strings.InStr(txtUserLInfo.Text, "Playcount") - 1));
            Invoke(new Action(() => txtUserLInfo.SelectionLength = "Playcount".Length));
            Invoke(new Action(() => txtUserLInfo.SelectionFont = BoldFont));
            // playlists
            Invoke(new Action(() => txtUserLInfo.SelectionStart = Strings.InStr(txtUserLInfo.Text, "Playlists") - 1));
            Invoke(new Action(() => txtUserLInfo.SelectionLength = "Playlists".Length));
            Invoke(new Action(() => txtUserLInfo.SelectionFont = BoldFont));
            // registered
            Invoke(new Action(() => txtUserLInfo.SelectionStart = Strings.InStr(txtUserLInfo.Text, "Registered") - 1));
            Invoke(new Action(() => txtUserLInfo.SelectionLength = "Registered".Length));
            Invoke(new Action(() => txtUserLInfo.SelectionFont = BoldFont));

            // report 34% progress
            Utilities.progress = (ushort)(Utilities.progress + 34);
            UpdateProgressChange();
            #endregion

            #region Friends
            // parse for total friends
            string FriendsTotalXML = Utilities.CallAPI("user.getFriends", Utilities.userlookup, "limit=1");
            string totalfriends = Utilities.ParseMetadata(FriendsTotalXML, "total=");
            if (totalfriends.Contains("ERROR:") == true)
            {
                totalfriends = "0";
            }

            // xml stuff
            string FriendsXML = Utilities.CallAPI("user.getFriends", Utilities.userlookup, "limit=" + totalfriends);
            Invoke(new Action(() => lblUserLFriendTotal.Text = "Friends: " + totalfriends));
            if (lblUserLFriendTotal.Text.Contains("ParseMetadata") == true)
            {
                Invoke(new Action(() => lblUserLFriendTotal.Text = "Friends: 0"));
            }
            string[] FriendsNodes = new string[] { "name", "realname", "url", "registered" };

            // find number of users in xml
            bool loopend = false;
            int startpos = 1;
            var usercount = default(ushort);
            do
            {
                startpos = Strings.InStr(startpos, FriendsXML, "<user>");
                if (startpos <= 0)
                {
                    loopend = true;  // end loop if no more can be found
                }
                else
                {
                    startpos += 6;   // increment startpos to the end of <user>
                    usercount = (ushort)(usercount + 1);
                }  // increment usercount
            }
            while (loopend != true);

            // add each friend to list view
            Invoke(new Action(() => ltvUserLFriends.Items.Clear()));
            if (usercount > 0)
            {
                for (ushort count = 0, loopTo = (ushort)(usercount - 1); count <= loopTo; count++)
                {
                    Utilities.ParseXML(FriendsXML, "/lfm/friends/user", count, ref FriendsNodes);              // get data from xml
                    Invoke(new Action(() => ltvUserLFriends.Items.Add(FriendsNodes[0])));                     // add listview item
                    Invoke(new Action(() => ltvUserLFriends.Items[count].SubItems.Add(FriendsNodes[1])));     // add subitem 1
                    Invoke(new Action(() => ltvUserLFriends.Items[count].SubItems.Add(FriendsNodes[2])));     // add subitem 2
                    Invoke(new Action(() => ltvUserLFriends.Items[count].SubItems.Add(FriendsNodes[3])));     // add subitem 3
                    FriendsNodes = new[] { "name", "realname", "url", "registered" };                    // reset nodes
                }
            }

            // report 67% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

            #region Recent Tracks
            #region Fatty Arrays
            Label[,] TrackLabel = new Label[,] { { lblUserLRecentTitle1, lblUserLRecentArtist1, lblUserLRecentAlbum1 }, { lblUserLRecentTitle2, lblUserLRecentArtist2, lblUserLRecentAlbum2 }, { lblUserLRecentTitle3, lblUserLRecentArtist3, lblUserLRecentAlbum3 }, { lblUserLRecentTitle4, lblUserLRecentArtist4, lblUserLRecentAlbum4 }, { lblUserLRecentTitle5, lblUserLRecentArtist5, lblUserLRecentAlbum5 }, { lblUserLRecentTitle6, lblUserLRecentArtist6, lblUserLRecentAlbum6 }, { lblUserLRecentTitle7, lblUserLRecentArtist7, lblUserLRecentAlbum7 }, { lblUserLRecentTitle8, lblUserLRecentArtist8, lblUserLRecentAlbum8 }, { lblUserLRecentTitle9, lblUserLRecentArtist9, lblUserLRecentAlbum9 }, { lblUserLRecentTitle10, lblUserLRecentArtist10, lblUserLRecentAlbum10 }, { lblUserLRecentTitle11, lblUserLRecentArtist11, lblUserLRecentAlbum11 }, { lblUserLRecentTitle12, lblUserLRecentArtist12, lblUserLRecentAlbum12 }, { lblUserLRecentTitle13, lblUserLRecentArtist13, lblUserLRecentAlbum13 }, { lblUserLRecentTitle14, lblUserLRecentArtist14, lblUserLRecentAlbum14 }, { lblUserLRecentTitle15, lblUserLRecentArtist15, lblUserLRecentAlbum15 }, { lblUserLRecentTitle16, lblUserLRecentArtist16, lblUserLRecentAlbum16 }, { lblUserLRecentTitle17, lblUserLRecentArtist17, lblUserLRecentAlbum17 }, { lblUserLRecentTitle18, lblUserLRecentArtist18, lblUserLRecentAlbum18 }, { lblUserLRecentTitle19, lblUserLRecentArtist19, lblUserLRecentAlbum19 }, { lblUserLRecentTitle20, lblUserLRecentArtist20, lblUserLRecentAlbum20 } };

            PictureBox[] TrackArt = new PictureBox[] { picUserLRecentArt1, picUserLRecentArt2, picUserLRecentArt3, picUserLRecentArt4, picUserLRecentArt5, picUserLRecentArt6, picUserLRecentArt7, picUserLRecentArt8, picUserLRecentArt9, picUserLRecentArt10, picUserLRecentArt11, picUserLRecentArt12, picUserLRecentArt13, picUserLRecentArt14, picUserLRecentArt15, picUserLRecentArt16, picUserLRecentArt17, picUserLRecentArt18, picUserLRecentArt19, picUserLRecentArt20 };
            #endregion

            // xml stuff
            string recenttrackxml = Utilities.CallAPI("user.getRecentTracks", Utilities.userlookup, "extended=1");
            string[] recenttracknodes = new string[] { "name", "artist/name", "album" };

            for (byte count = 0; count <= 19; count++)
            {
                // parse xml
                Utilities.ParseXML(recenttrackxml, "/lfm/recenttracks/track", count, ref recenttracknodes);

                // set labels
                Invoke(new Action(() => TrackLabel[count, 0].Text = recenttracknodes[0]));
                Invoke(new Action(() => TrackLabel[count, 1].Text = recenttracknodes[1]));
                Invoke(new Action(() => TrackLabel[count, 2].Text = recenttracknodes[2]));

                // set art
                string loadurl = Utilities.ParseImage(recenttrackxml, "/lfm/recenttracks/track", count, 8);
                try
                {
                    TrackArt[count].Load(loadurl);
                }
                catch (Exception ex)
                {
                    TrackArt[count].Image = My.Resources.Resources.imageunavailable;
                }

                // reset nodes
                recenttracknodes = new[] { "name", "artist/name", "album" };
            }

            // report 100% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

        }

        public void UserLLovedTracksUpdate(string page)
        {
            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            // get xml
            string xml = Utilities.CallAPI("user.getLovedTracks", Utilities.userlookup, "page=" + page);

            // report 20% progress
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // set labels
            Invoke(new Action(() => lblUserLLovedTotalPages.Text = "Total Pages: " + Utilities.ParseMetadata(xml, "totalPages=")));  // set total pages label
            Invoke(new Action(() => lblUserLLovedTotalTracks.Text = "Total Tracks: " + Utilities.ParseMetadata(xml, "total=")));     // set total tracks label
            Invoke(new Action(() => ltvUserLLovedTracks.Items.Clear()));

            // report 30% progress
            Utilities.progress = (ushort)(Utilities.progress + 10);
            UpdateProgressChange();

            // set max numeric box value
            ushort totalpages;
            ushort.TryParse(Utilities.ParseMetadata(xml, "totalPages="), out totalpages);
            Invoke(new Action(() => nudUserLLovedPage.Maximum = totalpages));

            // report 40% progress
            Utilities.progress = (ushort)(Utilities.progress + 10);
            UpdateProgressChange();

            // parse information to put in listview
            string[] xmlnodes = new string[] { "name", "artist/name", "date" };
            for (byte count = 0; count <= 49; count++)
            {
                Utilities.ParseXML(xml, "/lfm/lovedtracks/track", count, ref xmlnodes);

                // add items if no error
                if (xmlnodes[0].Contains("ERROR:") == false)
                {
                    Invoke(new Action(() => ltvUserLLovedTracks.Items.Add(xmlnodes[0])));
                    Invoke(new Action(() => ltvUserLLovedTracks.Items[count].SubItems.Add(xmlnodes[1])));
                    Invoke(new Action(() => ltvUserLLovedTracks.Items[count].SubItems.Add(xmlnodes[2])));
                }
                // reset nodes
                xmlnodes = new[] { "name", "artist/name", "date" };

                // report 40-90% progress
                Utilities.progress = (ushort)(Utilities.progress + 1);
                UpdateProgressChange();
            }

            // set numeric box value back to current page
            ushort pagenum;
            string metadata = Utilities.ParseMetadata(xml, "page=");
            ushort.TryParse(metadata, out pagenum);
            if (nudUserLLovedPage.Maximum > 0m && nudUserLLovedPage.Minimum > 0m)
            {
                Invoke(new Action(() => nudUserLLovedPage.Value = pagenum));
            }
            else
            {
                Invoke(new Action(() => nudUserLLovedPage.Minimum = 0m));
                Invoke(new Action(() => nudUserLLovedPage.Value = 0m));
            }

            // report 100% progress
            Utilities.progress = (ushort)(Utilities.progress + 10);
            UpdateProgressChange();
        }

        public void UserLChartsUpdate()
        {
            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            #region Fatty Arrays
            Label[,] TopTrackLabels = new Label[,] { { lblUserLTopTrackTitle1, lblUserLTopTrackArtist1, lblUserLTopTrackAlbum1, lblUserLTopTrackPlaycount1 }, { lblUserLTopTrackTitle2, lblUserLTopTrackArtist2, lblUserLTopTrackAlbum2, lblUserLTopTrackPlaycount2 }, { lblUserLTopTrackTitle3, lblUserLTopTrackArtist3, lblUserLTopTrackAlbum3, lblUserLTopTrackPlaycount3 }, { lblUserLTopTrackTitle4, lblUserLTopTrackArtist4, lblUserLTopTrackAlbum4, lblUserLTopTrackPlaycount4 }, { lblUserLTopTrackTitle5, lblUserLTopTrackArtist5, lblUserLTopTrackAlbum5, lblUserLTopTrackPlaycount5 }, { lblUserLTopTrackTitle6, lblUserLTopTrackArtist6, lblUserLTopTrackAlbum6, lblUserLTopTrackPlaycount6 }, { lblUserLTopTrackTitle7, lblUserLTopTrackArtist7, lblUserLTopTrackAlbum7, lblUserLTopTrackPlaycount7 }, { lblUserLTopTrackTitle8, lblUserLTopTrackArtist8, lblUserLTopTrackAlbum8, lblUserLTopTrackPlaycount8 }, { lblUserLTopTrackTitle9, lblUserLTopTrackArtist9, lblUserLTopTrackAlbum9, lblUserLTopTrackPlaycount9 }, { lblUserLTopTrackTitle10, lblUserLTopTrackArtist10, lblUserLTopTrackAlbum10, lblUserLTopTrackPlaycount10 }, { lblUserLTopTrackTitle11, lblUserLTopTrackArtist11, lblUserLTopTrackAlbum11, lblUserLTopTrackPlaycount11 }, { lblUserLTopTrackTitle12, lblUserLTopTrackArtist12, lblUserLTopTrackAlbum12, lblUserLTopTrackPlaycount12 }, { lblUserLTopTrackTitle13, lblUserLTopTrackArtist13, lblUserLTopTrackAlbum13, lblUserLTopTrackPlaycount13 }, { lblUserLTopTrackTitle14, lblUserLTopTrackArtist14, lblUserLTopTrackAlbum14, lblUserLTopTrackPlaycount14 }, { lblUserLTopTrackTitle15, lblUserLTopTrackArtist15, lblUserLTopTrackAlbum15, lblUserLTopTrackPlaycount15 }, { lblUserLTopTrackTitle16, lblUserLTopTrackArtist16, lblUserLTopTrackAlbum16, lblUserLTopTrackPlaycount16 }, { lblUserLTopTrackTitle17, lblUserLTopTrackArtist17, lblUserLTopTrackAlbum17, lblUserLTopTrackPlaycount17 }, { lblUserLTopTrackTitle18, lblUserLTopTrackArtist18, lblUserLTopTrackAlbum18, lblUserLTopTrackPlaycount18 }, { lblUserLTopTrackTitle19, lblUserLTopTrackArtist19, lblUserLTopTrackAlbum19, lblUserLTopTrackPlaycount19 }, { lblUserLTopTrackTitle20, lblUserLTopTrackArtist20, lblUserLTopTrackAlbum20, lblUserLTopTrackPlaycount20 } };

            PictureBox[] TopTrackArt = new PictureBox[] { picUserLTopTrackArt1, picUserLTopTrackArt2, picUserLTopTrackArt3, picUserLTopTrackArt4, picUserLTopTrackArt5, picUserLTopTrackArt6, picUserLTopTrackArt7, picUserLTopTrackArt8, picUserLTopTrackArt9, picUserLTopTrackArt10, picUserLTopTrackArt11, picUserLTopTrackArt12, picUserLTopTrackArt13, picUserLTopTrackArt14, picUserLTopTrackArt15, picUserLTopTrackArt16, picUserLTopTrackArt17, picUserLTopTrackArt18, picUserLTopTrackArt19, picUserLTopTrackArt20 };

            Label[,] TopArtistLabels = new Label[,] { { lblUserLTopArtist1, lblUserLTopArtistPlaycount1 }, { lblUserLTopArtist2, lblUserLTopArtistPlaycount2 }, { lblUserLTopArtist3, lblUserLTopArtistPlaycount3 }, { lblUserLTopArtist4, lblUserLTopArtistPlaycount4 }, { lblUserLTopArtist5, lblUserLTopArtistPlaycount5 }, { lblUserLTopArtist6, lblUserLTopArtistPlaycount6 }, { lblUserLTopArtist7, lblUserLTopArtistPlaycount7 }, { lblUserLTopArtist8, lblUserLTopArtistPlaycount8 }, { lblUserLTopArtist9, lblUserLTopArtistPlaycount9 }, { lblUserLTopArtist10, lblUserLTopArtistPlaycount10 }, { lblUserLTopArtist11, lblUserLTopArtistPlaycount11 }, { lblUserLTopArtist12, lblUserLTopArtistPlaycount12 }, { lblUserLTopArtist13, lblUserLTopArtistPlaycount13 }, { lblUserLTopArtist14, lblUserLTopArtistPlaycount14 }, { lblUserLTopArtist15, lblUserLTopArtistPlaycount15 }, { lblUserLTopArtist16, lblUserLTopArtistPlaycount16 }, { lblUserLTopArtist17, lblUserLTopArtistPlaycount17 }, { lblUserLTopArtist18, lblUserLTopArtistPlaycount18 }, { lblUserLTopArtist19, lblUserLTopArtistPlaycount19 }, { lblUserLTopArtist20, lblUserLTopArtistPlaycount20 } };

            Label[,] TopAlbumLabels = new Label[,] { { lblUserLTopAlbum1, lblUserLTopAlbumArtist1, lblUserLTopAlbumPlaycount1 }, { lblUserLTopAlbum2, lblUserLTopAlbumArtist2, lblUserLTopAlbumPlaycount2 }, { lblUserLTopAlbum3, lblUserLTopAlbumArtist3, lblUserLTopAlbumPlaycount3 }, { lblUserLTopAlbum4, lblUserLTopAlbumArtist4, lblUserLTopAlbumPlaycount4 }, { lblUserLTopAlbum5, lblUserLTopAlbumArtist5, lblUserLTopAlbumPlaycount5 }, { lblUserLTopAlbum6, lblUserLTopAlbumArtist6, lblUserLTopAlbumPlaycount6 }, { lblUserLTopAlbum7, lblUserLTopAlbumArtist7, lblUserLTopAlbumPlaycount7 }, { lblUserLTopAlbum8, lblUserLTopAlbumArtist8, lblUserLTopAlbumPlaycount8 }, { lblUserLTopAlbum9, lblUserLTopAlbumArtist9, lblUserLTopAlbumPlaycount9 }, { lblUserLTopAlbum10, lblUserLTopAlbumArtist10, lblUserLTopAlbumPlaycount10 }, { lblUserLTopAlbum11, lblUserLTopAlbumArtist11, lblUserLTopAlbumPlaycount11 }, { lblUserLTopAlbum12, lblUserLTopAlbumArtist12, lblUserLTopAlbumPlaycount12 }, { lblUserLTopAlbum13, lblUserLTopAlbumArtist13, lblUserLTopAlbumPlaycount13 }, { lblUserLTopAlbum14, lblUserLTopAlbumArtist14, lblUserLTopAlbumPlaycount14 }, { lblUserLTopAlbum15, lblUserLTopAlbumArtist15, lblUserLTopAlbumPlaycount15 }, { lblUserLTopAlbum16, lblUserLTopAlbumArtist16, lblUserLTopAlbumPlaycount16 }, { lblUserLTopAlbum17, lblUserLTopAlbumArtist17, lblUserLTopAlbumPlaycount17 }, { lblUserLTopAlbum18, lblUserLTopAlbumArtist18, lblUserLTopAlbumPlaycount18 }, { lblUserLTopAlbum19, lblUserLTopAlbumArtist19, lblUserLTopAlbumPlaycount19 }, { lblUserLTopAlbum20, lblUserLTopAlbumArtist20, lblUserLTopAlbumPlaycount20 } };

            PictureBox[] TopAlbumArt = new PictureBox[] { picUserLTopAlbumArt1, picUserLTopAlbumArt2, picUserLTopAlbumArt3, picUserLTopAlbumArt4, picUserLTopAlbumArt5, picUserLTopAlbumArt6, picUserLTopAlbumArt7, picUserLTopAlbumArt8, picUserLTopAlbumArt9, picUserLTopAlbumArt10, picUserLTopAlbumArt11, picUserLTopAlbumArt12, picUserLTopAlbumArt13, picUserLTopAlbumArt14, picUserLTopAlbumArt15, picUserLTopAlbumArt16, picUserLTopAlbumArt17, picUserLTopAlbumArt18, picUserLTopAlbumArt19, picUserLTopAlbumArt20 };

            // report 1% progress
            Utilities.progress = (ushort)(Utilities.progress + 1);
            UpdateProgressChange();
            #endregion

            #region Tracks
            // dim stuff
            string TopTrackXML = Utilities.CallAPI("user.getTopTracks", Utilities.userlookup, "limit=20");
            string[] TopTrackNodes = new string[] { "name", "artist/name", "playcount" };
            string TrackInfoXML;
            string[] TrackInfoNodes = new string[] { "title" };
            uint numberholder;

            // get track info for 20 tracks
            for (byte count = 0; count <= 19; count++)
            {
                // get initial track data (title and artist) from toptracks
                Utilities.ParseXML(TopTrackXML, "/lfm/toptracks/track", count, ref TopTrackNodes);

                // get track info xml
                TrackInfoXML = Utilities.CallAPI("track.getInfo", "", "track=" + TopTrackNodes[0].Replace(" ", "+"), "artist=" + TopTrackNodes[1].Replace(" ", "+"));

                // get track album from trackinfoxml
                Utilities.ParseXML(TrackInfoXML, "/lfm/track/album", 0U, ref TrackInfoNodes);

                // detect errors and set to "(Unavailable)"
                for (byte counter2 = 0; counter2 <= 2; counter2++)
                {
                    if (TopTrackNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TopTrackNodes[counter2] = "(Unavailable)";
                    }
                }
                if (TrackInfoNodes[0].Contains("Object reference not set to an instance of an object") == true)
                {
                    TrackInfoNodes[0] = "(Unavailable)";
                }

                // set labels
                Invoke(new Action(() => TopTrackLabels[count, 0].Text = TopTrackNodes[0]));     // title
                Invoke(new Action(() => TopTrackLabels[count, 1].Text = TopTrackNodes[1]));     // artist
                Invoke(new Action(() => TopTrackLabels[count, 2].Text = TrackInfoNodes[0]));    // album
                                                                                                // apply formatting to playcount
                uint.TryParse(TopTrackNodes[2], out numberholder);
                Invoke(new Action(() => TopTrackLabels[count, 3].Text = numberholder.ToString("N0")));

                // set picturebox
                try
                {
                    TopTrackArt[count].Load(Utilities.ParseImage(TrackInfoXML, "/lfm/track/album/image", 1));
                }
                catch (Exception ex)
                {
                    TopTrackArt[count].Image = My.Resources.Resources.imageunavailable;
                }

                // reset node arrays
                TopTrackNodes = new[] { "name", "artist/name", "playcount" };
                TrackInfoNodes = new[] { "title" };
            }

            // report 34% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

            #region Artists
            // dim stuff
            string TopArtistXML = Utilities.CallAPI("user.getTopArtists", Utilities.userlookup, "limit=20");
            string[] TopArtistNodes = new string[] { "name", "playcount" };

            for (byte count = 0; count <= 19; count++)
            {
                // get artist data (name and playcount)
                Utilities.ParseXML(TopArtistXML, "/lfm/topartists/artist", count, ref TopArtistNodes);

                // detect errors and set to unavailable
                for (byte counter2 = 0; counter2 <= 1; counter2++)
                {
                    if (TopArtistNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TopArtistNodes[counter2] = "(Unavailable)";
                    }
                }

                // set labels
                Invoke(new Action(() => TopArtistLabels[count, 0].Text = TopArtistNodes[0]));     // title
                                                                                                  // apply formatting to playcount
                uint.TryParse(TopArtistNodes[1], out numberholder);
                Invoke(new Action(() => TopArtistLabels[count, 1].Text = numberholder.ToString("N0")));

                // reset node arrays
                TopArtistNodes = new[] { "name", "playcount" };
            }

            // report 67% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

            #region Albums
            // dim stuff
            string TopAlbumXML = Utilities.CallAPI("user.getTopAlbums", Utilities.userlookup, "limit=20");
            string[] TopAlbumNodes = new string[] { "name", "artist/name", "playcount" };

            for (byte count = 0; count <= 19; count++)
            {
                // get album data (name, artist, playcount)
                Utilities.ParseXML(TopAlbumXML, "/lfm/topalbums/album", count, ref TopAlbumNodes);

                // detect errors and set to unavailable
                for (byte counter2 = 0; counter2 <= 2; counter2++)
                {
                    if (TopAlbumNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TopAlbumNodes[counter2] = "(Unavailable)";
                    }
                }

                // set labels
                Invoke(new Action(() => TopAlbumLabels[count, 0].Text = TopAlbumNodes[0]));     // album
                Invoke(new Action(() => TopAlbumLabels[count, 1].Text = TopAlbumNodes[1]));     // artist
                                                                                                // apply formatting to playcount
                uint.TryParse(TopAlbumNodes[2], out numberholder);
                Invoke(new Action(() => TopAlbumLabels[count, 2].Text = numberholder.ToString("N0")));

                // set picturebox
                try
                {
                    TopAlbumArt[count].Load(Utilities.ParseImage(TopAlbumXML, "/lfm/topalbums/album", count, 6));
                }
                catch (Exception ex)
                {
                    TopAlbumArt[count].Image = My.Resources.Resources.imageunavailable;
                }

                // reset node arrays
                TopAlbumNodes = new[] { "name", "artist/name", "playcount" };
            }

            // report 100% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

            #region History
            // Invoke(Sub() nudUserLHistoryPage.Value = nudUserLHistoryPage.Minimum)
            // UserLHistoryUpdate()
            #endregion

        }

        public void UserLChartsUpdate(uint unixfrom, uint unixto)
        {
            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            #region To>From Check
            if (unixfrom > unixto)
            {
                Invoke(new Action(() => MessageBox.Show("From date must be before to date", "User Lookup Charts", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                Utilities.progress = (ushort)(Utilities.progress + 100);
                UpdateProgressChange();
                return;
            }
            #endregion

            #region Fatty Arrays
            Label[,] TopTrackLabels = new Label[,] { { lblUserLTopTrackTitle1, lblUserLTopTrackArtist1, lblUserLTopTrackAlbum1, lblUserLTopTrackPlaycount1 }, { lblUserLTopTrackTitle2, lblUserLTopTrackArtist2, lblUserLTopTrackAlbum2, lblUserLTopTrackPlaycount2 }, { lblUserLTopTrackTitle3, lblUserLTopTrackArtist3, lblUserLTopTrackAlbum3, lblUserLTopTrackPlaycount3 }, { lblUserLTopTrackTitle4, lblUserLTopTrackArtist4, lblUserLTopTrackAlbum4, lblUserLTopTrackPlaycount4 }, { lblUserLTopTrackTitle5, lblUserLTopTrackArtist5, lblUserLTopTrackAlbum5, lblUserLTopTrackPlaycount5 }, { lblUserLTopTrackTitle6, lblUserLTopTrackArtist6, lblUserLTopTrackAlbum6, lblUserLTopTrackPlaycount6 }, { lblUserLTopTrackTitle7, lblUserLTopTrackArtist7, lblUserLTopTrackAlbum7, lblUserLTopTrackPlaycount7 }, { lblUserLTopTrackTitle8, lblUserLTopTrackArtist8, lblUserLTopTrackAlbum8, lblUserLTopTrackPlaycount8 }, { lblUserLTopTrackTitle9, lblUserLTopTrackArtist9, lblUserLTopTrackAlbum9, lblUserLTopTrackPlaycount9 }, { lblUserLTopTrackTitle10, lblUserLTopTrackArtist10, lblUserLTopTrackAlbum10, lblUserLTopTrackPlaycount10 }, { lblUserLTopTrackTitle11, lblUserLTopTrackArtist11, lblUserLTopTrackAlbum11, lblUserLTopTrackPlaycount11 }, { lblUserLTopTrackTitle12, lblUserLTopTrackArtist12, lblUserLTopTrackAlbum12, lblUserLTopTrackPlaycount12 }, { lblUserLTopTrackTitle13, lblUserLTopTrackArtist13, lblUserLTopTrackAlbum13, lblUserLTopTrackPlaycount13 }, { lblUserLTopTrackTitle14, lblUserLTopTrackArtist14, lblUserLTopTrackAlbum14, lblUserLTopTrackPlaycount14 }, { lblUserLTopTrackTitle15, lblUserLTopTrackArtist15, lblUserLTopTrackAlbum15, lblUserLTopTrackPlaycount15 }, { lblUserLTopTrackTitle16, lblUserLTopTrackArtist16, lblUserLTopTrackAlbum16, lblUserLTopTrackPlaycount16 }, { lblUserLTopTrackTitle17, lblUserLTopTrackArtist17, lblUserLTopTrackAlbum17, lblUserLTopTrackPlaycount17 }, { lblUserLTopTrackTitle18, lblUserLTopTrackArtist18, lblUserLTopTrackAlbum18, lblUserLTopTrackPlaycount18 }, { lblUserLTopTrackTitle19, lblUserLTopTrackArtist19, lblUserLTopTrackAlbum19, lblUserLTopTrackPlaycount19 }, { lblUserLTopTrackTitle20, lblUserLTopTrackArtist20, lblUserLTopTrackAlbum20, lblUserLTopTrackPlaycount20 } };

            PictureBox[] TopTrackArt = new PictureBox[] { picUserLTopTrackArt1, picUserLTopTrackArt2, picUserLTopTrackArt3, picUserLTopTrackArt4, picUserLTopTrackArt5, picUserLTopTrackArt6, picUserLTopTrackArt7, picUserLTopTrackArt8, picUserLTopTrackArt9, picUserLTopTrackArt10, picUserLTopTrackArt11, picUserLTopTrackArt12, picUserLTopTrackArt13, picUserLTopTrackArt14, picUserLTopTrackArt15, picUserLTopTrackArt16, picUserLTopTrackArt17, picUserLTopTrackArt18, picUserLTopTrackArt19, picUserLTopTrackArt20 };

            Label[,] TopArtistLabels = new Label[,] { { lblUserLTopArtist1, lblUserLTopArtistPlaycount1 }, { lblUserLTopArtist2, lblUserLTopArtistPlaycount2 }, { lblUserLTopArtist3, lblUserLTopArtistPlaycount3 }, { lblUserLTopArtist4, lblUserLTopArtistPlaycount4 }, { lblUserLTopArtist5, lblUserLTopArtistPlaycount5 }, { lblUserLTopArtist6, lblUserLTopArtistPlaycount6 }, { lblUserLTopArtist7, lblUserLTopArtistPlaycount7 }, { lblUserLTopArtist8, lblUserLTopArtistPlaycount8 }, { lblUserLTopArtist9, lblUserLTopArtistPlaycount9 }, { lblUserLTopArtist10, lblUserLTopArtistPlaycount10 }, { lblUserLTopArtist11, lblUserLTopArtistPlaycount11 }, { lblUserLTopArtist12, lblUserLTopArtistPlaycount12 }, { lblUserLTopArtist13, lblUserLTopArtistPlaycount13 }, { lblUserLTopArtist14, lblUserLTopArtistPlaycount14 }, { lblUserLTopArtist15, lblUserLTopArtistPlaycount15 }, { lblUserLTopArtist16, lblUserLTopArtistPlaycount16 }, { lblUserLTopArtist17, lblUserLTopArtistPlaycount17 }, { lblUserLTopArtist18, lblUserLTopArtistPlaycount18 }, { lblUserLTopArtist19, lblUserLTopArtistPlaycount19 }, { lblUserLTopArtist20, lblUserLTopArtistPlaycount20 } };

            Label[,] TopAlbumLabels = new Label[,] { { lblUserLTopAlbum1, lblUserLTopAlbumArtist1, lblUserLTopAlbumPlaycount1 }, { lblUserLTopAlbum2, lblUserLTopAlbumArtist2, lblUserLTopAlbumPlaycount2 }, { lblUserLTopAlbum3, lblUserLTopAlbumArtist3, lblUserLTopAlbumPlaycount3 }, { lblUserLTopAlbum4, lblUserLTopAlbumArtist4, lblUserLTopAlbumPlaycount4 }, { lblUserLTopAlbum5, lblUserLTopAlbumArtist5, lblUserLTopAlbumPlaycount5 }, { lblUserLTopAlbum6, lblUserLTopAlbumArtist6, lblUserLTopAlbumPlaycount6 }, { lblUserLTopAlbum7, lblUserLTopAlbumArtist7, lblUserLTopAlbumPlaycount7 }, { lblUserLTopAlbum8, lblUserLTopAlbumArtist8, lblUserLTopAlbumPlaycount8 }, { lblUserLTopAlbum9, lblUserLTopAlbumArtist9, lblUserLTopAlbumPlaycount9 }, { lblUserLTopAlbum10, lblUserLTopAlbumArtist10, lblUserLTopAlbumPlaycount10 }, { lblUserLTopAlbum11, lblUserLTopAlbumArtist11, lblUserLTopAlbumPlaycount11 }, { lblUserLTopAlbum12, lblUserLTopAlbumArtist12, lblUserLTopAlbumPlaycount12 }, { lblUserLTopAlbum13, lblUserLTopAlbumArtist13, lblUserLTopAlbumPlaycount13 }, { lblUserLTopAlbum14, lblUserLTopAlbumArtist14, lblUserLTopAlbumPlaycount14 }, { lblUserLTopAlbum15, lblUserLTopAlbumArtist15, lblUserLTopAlbumPlaycount15 }, { lblUserLTopAlbum16, lblUserLTopAlbumArtist16, lblUserLTopAlbumPlaycount16 }, { lblUserLTopAlbum17, lblUserLTopAlbumArtist17, lblUserLTopAlbumPlaycount17 }, { lblUserLTopAlbum18, lblUserLTopAlbumArtist18, lblUserLTopAlbumPlaycount18 }, { lblUserLTopAlbum19, lblUserLTopAlbumArtist19, lblUserLTopAlbumPlaycount19 }, { lblUserLTopAlbum20, lblUserLTopAlbumArtist20, lblUserLTopAlbumPlaycount20 } };

            PictureBox[] TopAlbumArt = new PictureBox[] { picUserLTopAlbumArt1, picUserLTopAlbumArt2, picUserLTopAlbumArt3, picUserLTopAlbumArt4, picUserLTopAlbumArt5, picUserLTopAlbumArt6, picUserLTopAlbumArt7, picUserLTopAlbumArt8, picUserLTopAlbumArt9, picUserLTopAlbumArt10, picUserLTopAlbumArt11, picUserLTopAlbumArt12, picUserLTopAlbumArt13, picUserLTopAlbumArt14, picUserLTopAlbumArt15, picUserLTopAlbumArt16, picUserLTopAlbumArt17, picUserLTopAlbumArt18, picUserLTopAlbumArt19, picUserLTopAlbumArt20 };

            // report 1% progress
            Utilities.progress = (ushort)(Utilities.progress + 1);
            UpdateProgressChange();
            #endregion

            #region Tracks
            // dim stuff
            string TopTrackXML = Utilities.CallAPI("user.getWeeklyTrackChart", Utilities.userlookup, "from=" + unixfrom.ToString(), "to=" + unixto.ToString());
            string[] TopTrackNodes = new string[] { "name", "artist", "playcount" };
            string TrackInfoXML;
            string[] TrackInfoNodes = new string[] { "title" };
            uint numberholder;

            // get track info for 20 tracks
            for (byte count = 0; count <= 19; count++)
            {
                // get initial track data (title and artist) from toptracks
                Utilities.ParseXML(TopTrackXML, "/lfm/weeklytrackchart/track", count, ref TopTrackNodes);

                // get track info xml
                TrackInfoXML = Utilities.CallAPI("track.getInfo", "", "track=" + TopTrackNodes[0].Replace(" ", "+"), "artist=" + TopTrackNodes[1].Replace(" ", "+"));

                // get track album from trackinfoxml
                Utilities.ParseXML(TrackInfoXML, "/lfm/track/album", 0U, ref TrackInfoNodes);

                // detect errors and set to "(Unavailable)"
                for (byte counter2 = 0; counter2 <= 2; counter2++)
                {
                    if (TopTrackNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TopTrackNodes[counter2] = "(Unavailable)";
                    }
                }
                if (TrackInfoNodes[0].Contains("Object reference not set to an instance of an object") == true)
                {
                    TrackInfoNodes[0] = "(Unavailable)";
                }

                // set labels
                Invoke(new Action(() => TopTrackLabels[count, 0].Text = TopTrackNodes[0]));     // title
                Invoke(new Action(() => TopTrackLabels[count, 1].Text = TopTrackNodes[1]));     // artist
                Invoke(new Action(() => TopTrackLabels[count, 2].Text = TrackInfoNodes[0]));    // album
                                                                                                // apply formatting to playcount
                uint.TryParse(TopTrackNodes[2], out numberholder);
                Invoke(new Action(() => TopTrackLabels[count, 3].Text = numberholder.ToString("N0")));

                // set picturebox
                try
                {
                    TopTrackArt[count].Load(Utilities.ParseImage(TrackInfoXML, "/lfm/track/album/image", 1));
                }
                catch (Exception ex)
                {
                    TopTrackArt[count].Image = My.Resources.Resources.imageunavailable;
                }

                // reset node arrays
                TopTrackNodes = new[] { "name", "artist", "playcount" };
                TrackInfoNodes = new[] { "title" };
            }

            // report 34% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

            #region Artists
            // dim stuff
            string TopArtistXML = Utilities.CallAPI("user.getWeeklyArtistChart", Utilities.userlookup, "from=" + unixfrom.ToString(), "to=" + unixto.ToString());
            string[] TopArtistNodes = new string[] { "name", "playcount" };

            for (byte count = 0; count <= 19; count++)
            {
                // get artist data (name and playcount)
                Utilities.ParseXML(TopArtistXML, "/lfm/weeklyartistchart/artist", count, ref TopArtistNodes);

                // detect errors and set to unavailable
                for (byte counter2 = 0; counter2 <= 1; counter2++)
                {
                    if (TopArtistNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TopArtistNodes[counter2] = "(Unavailable)";
                    }
                }

                // set labels
                Invoke(new Action(() => TopArtistLabels[count, 0].Text = TopArtistNodes[0]));     // title
                                                                                                  // apply formatting to playcount
                uint.TryParse(TopArtistNodes[1], out numberholder);
                Invoke(new Action(() => TopArtistLabels[count, 1].Text = numberholder.ToString("N0")));

                // reset node arrays
                TopArtistNodes = new[] { "name", "playcount" };
            }

            // report 67% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

            #region Albums
            // dim stuff
            string TopAlbumXML = Utilities.CallAPI("user.getWeeklyAlbumChart", Utilities.userlookup, "from=" + unixfrom.ToString(), "to=" + unixto.ToString());
            string[] TopAlbumNodes = new string[] { "name", "artist", "playcount" };
            string AlbumInfoXML;

            for (byte count = 0; count <= 19; count++)
            {
                // get album data (name, artist, playcount)
                Utilities.ParseXML(TopAlbumXML, "/lfm/weeklyalbumchart/album", count, ref TopAlbumNodes);

                // get album info for image
                AlbumInfoXML = Utilities.CallAPI("album.getInfo", Utilities.userlookup, "album=" + TopAlbumNodes[0].Replace(" ", "+"), "artist=" + TopAlbumNodes[1].Replace(" ", "+"));

                // detect errors and set to unavailable
                for (byte counter2 = 0; counter2 <= 2; counter2++)
                {
                    if (TopAlbumNodes[counter2].Contains("Object reference not set to an instance of an object") == true)
                    {
                        TopAlbumNodes[counter2] = "(Unavailable)";
                    }
                }

                // set labels
                Invoke(new Action(() => TopAlbumLabels[count, 0].Text = TopAlbumNodes[0]));     // album
                Invoke(new Action(() => TopAlbumLabels[count, 1].Text = TopAlbumNodes[1]));     // artist
                                                                                                // apply formatting to playcount
                uint.TryParse(TopAlbumNodes[2], out numberholder);
                Invoke(new Action(() => TopAlbumLabels[count, 2].Text = numberholder.ToString("N0")));

                // set picturebox
                try
                {
                    TopAlbumArt[count].Load(Utilities.ParseImage(AlbumInfoXML, "/lfm/album/image", 1));
                }
                catch (Exception ex)
                {
                    TopAlbumArt[count].Image = My.Resources.Resources.imageunavailable;
                }

                // reset node arrays
                TopAlbumNodes = new[] { "name", "artist", "playcount" };
            }

            // report 100% progress
            Utilities.progress = (ushort)(Utilities.progress + 33);
            UpdateProgressChange();
            #endregion

            #region History
            Invoke(new Action(() => nudUserLHistoryPage.Value = nudUserLHistoryPage.Minimum));
            UserLHistoryUpdate();
            #endregion

        }

        public void UserLHistoryUpdate()
        {
            Utilities.progressmultiplier = (byte)(Utilities.progressmultiplier + 1);

            // determine whether date is used or not
            string historyXML;
            if (radUserLAllTime.Checked == true)
            {
                historyXML = Utilities.CallAPI("user.getRecentTracks", Utilities.userlookup, "page=" + nudUserLHistoryPage.Value.ToString());
            }
            else
            {
                historyXML = Utilities.CallAPI("user.getRecentTracks", Utilities.userlookup, "page=" + nudUserLHistoryPage.Value.ToString(), "from=" + (Utilities.DateToUnix(dtpUserLFrom.Value.Date) - Utilities.timezoneoffset), "to=" + (Utilities.DateToUnix(dtpUserLTo.Value.Date) - Utilities.timezoneoffset));
            }

            // determine whether something is now playing 
            byte metadataOffset = 1;
            if (Utilities.StrCount(historyXML, "</track>") > Utilities.StrCount(historyXML, "date uts="))
            {
                metadataOffset = 0;
            }

            // report 20% progress
            Utilities.progress = (ushort)(Utilities.progress + 20);
            UpdateProgressChange();

            // set labels
            Invoke(new Action(() => lblUserLHistoryTotalPages.Text = "Total Pages: " + Utilities.ParseMetadata(historyXML, "totalPages=")));  // set total pages label
            Invoke(new Action(() => lblUserLHistoryTotalTracks.Text = "Total Tracks: " + Utilities.ParseMetadata(historyXML, "total=")));     // set total tracks label
            Invoke(new Action(() => ltvUserLHistory.Items.Clear()));

            // report 30% progress
            Utilities.progress = (ushort)(Utilities.progress + 10);
            UpdateProgressChange();

            // set max numeric box value
            ushort totalpages;
            ushort.TryParse(Utilities.ParseMetadata(historyXML, "totalPages="), out totalpages);
            Invoke(new Action(() => nudUserLHistoryPage.Maximum = totalpages));

            // report 40% progress
            Utilities.progress = (ushort)(Utilities.progress + 10);
            UpdateProgressChange();

            // parse xml and add to list
            string[] historynodes = new string[] { "name", "artist", "album", "date" };
            for (byte count = 0; count <= 50; count++)
            {
                // parse xml
                Utilities.ParseXML(historyXML, "/lfm/recenttracks/track", count, ref historynodes);

                // add items if no error
                int counterrors = 0;
                if (historynodes[0].Contains("ERROR:") == false)
                {
                    Invoke(new Action(() => ltvUserLHistory.Items.Add(historynodes[0])));
                    Invoke(new Action(() => ltvUserLHistory.Items[count - counterrors].SubItems.Add(historynodes[1])));
                    Invoke(new Action(() => ltvUserLHistory.Items[count - counterrors].SubItems.Add(historynodes[2])));
                    // check for date error due to now playing
                    if (historynodes[3].Contains("ERROR: ") == false)
                    {
                        Invoke(new Action(() => ltvUserLHistory.Items[count].SubItems.Add(Utilities.UnixToDate((uint)Math.Round(Conversions.ToDouble(Utilities.ParseMetadata(historyXML, "date uts=", (uint)(count - counterrors + metadataOffset))) + Utilities.timezoneoffset)).ToString("G"))));
                    }
                    else
                    {
                        Invoke(new Action(() => ltvUserLHistory.Items[count].SubItems.Add("Now Playing")));
                    }
                }
                else
                {
                    counterrors += 1;
                }
                // reset nodes
                historynodes = new[] { "name", "artist", "album", "date" };

                // report 40-91% progress
                Utilities.progress = (ushort)(Utilities.progress + 1);
                UpdateProgressChange();
            }

            // set numeric box value back to current page
            ushort pagenum;
            string metadata = Utilities.ParseMetadata(historyXML, "page=");
            ushort.TryParse(metadata, out pagenum);
            if (nudUserLHistoryPage.Maximum > 0m && nudUserLHistoryPage.Minimum > 0m)
            {
                // check if pagenum is higher than maximum
                if (pagenum <= nudUserLHistoryPage.Maximum)
                {
                    Invoke(new Action(() => nudUserLHistoryPage.Value = pagenum));
                }
                else
                {
                    Invoke(new Action(() => nudUserLHistoryPage.Value = nudUserLHistoryPage.Maximum));
                }
            }
            else
            {
                Invoke(new Action(() => nudUserLHistoryPage.Minimum = 0m));
                Invoke(new Action(() => nudUserLHistoryPage.Value = 0m));
            }

            // report 100% progress
            Utilities.progress = (ushort)(Utilities.progress + 9);
            UpdateProgressChange();
        }
        #endregion

        #region Other Update
        public void LastPlayedUpdate()
        {
            // get recent track
            string[] recenttracknodes = new string[] { "artist", "name" };
            string lastplayedxml = Utilities.CallAPI("user.getRecentTracks", My.MySettingsProperty.Settings.User, "limit=1");
            Utilities.ParseXML(lastplayedxml, "/lfm/recenttracks/track", 0U, ref recenttracknodes);

            // determine whether track is now playing or last played
            string playingstatus;
            if (lastplayedxml.Contains("nowplaying=" + '"' + "true" + '"') == true)
            {
                playingstatus = "Now Playing: ";
            }
            else
            {
                playingstatus = "Last Played: ";
            }

            // if artist and title are both errors then dont display anything
            if (recenttracknodes[0].Contains("ERROR:") == true && recenttracknodes[1].Contains("ERROR:") == true)
            {
                lblLastPlayed.Text = "Last Played: N/A";
            }
            else
            {
                // check if artist has an error, set to (Unavailable)
                if (recenttracknodes[0].Contains("ERROR:") == true)
                {
                    recenttracknodes[0] = "(Unavailable)";
                }
                else if (recenttracknodes[1].Contains("ERROR:") == true)
                {
                    recenttracknodes[1] = "(Unavailable)";
                }
                Invoke(new Action(() => lblLastPlayed.Text = playingstatus + recenttracknodes[0].Replace("&", "&&") + " - " + recenttracknodes[1].Replace("&", "&&")));
            }

            // hide label if error
            if (lblLastPlayed.Text == "Last Played: (Unavailable) - name")
            {
                lblLastPlayed.Visible = false;
                separator1.Visible = false;
            }
            else
            {
                lblLastPlayed.Visible = true;
                separator1.Visible = true;
            }

            // set tags for label
            Invoke(new Action(() => lblLastPlayed.Tag = recenttracknodes[0] + Constants.vbCrLf + recenttracknodes[1]));
        }

        public void UpdateProgressChange()
        {
            // update progress bar
            float progressvalue;
            try     // error catching for possible overflow
            {
                progressvalue = (float)(Utilities.progress / (double)Utilities.progressmultiplier);
            }
            catch (Exception ex)
            {
                Invoke(new Action(() => MessageBox.Show("UpdateProgressChange has encountered an error. Message: " + ex.Message, "Audiograph Error", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                return;
            }
            if (progressvalue >= 100f)
            {
                Utilities.progressmultiplier = 0;
                Utilities.progress = 0;
                Invoke(new Action(() => Cursor = Cursors.Default));
                Invoke(new Action(() => UpdateProgress.Visible = false));
                Invoke(new Action(() => UpdateProgress.Value = 100));
            }

            else
            {
                Invoke(new Action(() => Cursor = Cursors.AppStarting));
                Invoke(new Action(() => UpdateProgress.Visible = true));
                Invoke(new Action(() => UpdateProgress.Value = (int)Math.Round(Math.Floor((double)progressvalue))));
            }
        }

        // update all tabs
        public void UpdateAll()
        {
            // resetprog
            Utilities.progress = 0;
            Utilities.progressmultiplier = 0;
            UpdateProgress.Value = 100;
            UpdateProgress.Visible = false;

            if (bgwChartUpdater.IsBusy == false)
            {
                bgwChartUpdater.RunWorkerAsync();
            }
            if (bgwTrackUpdater.IsBusy == false)
            {
                bgwTrackUpdater.RunWorkerAsync();
            }
            if (bgwArtistUpdater.IsBusy == false)
            {
                bgwArtistUpdater.RunWorkerAsync();
            }
            if (bgwAlbumUpdater.IsBusy == false)
            {
                bgwAlbumUpdater.RunWorkerAsync();
            }
            if (bgwSearchUpdater.IsBusy == false)
            {
                bgwSearchUpdater.RunWorkerAsync();
            }
            if (bgwUserUpdater.IsBusy == false)
            {
                bgwUserUpdater.RunWorkerAsync();
            }
            if (bgwUserLookupUpdater.IsBusy == false)
            {
                bgwUserLookupUpdater.RunWorkerAsync();
            }
            LastPlayedUpdate();
        }

        // update current tab
        private void UpdateCurrentTab(object sender, EventArgs e)
        {
            // resetprog
            Utilities.progress = 0;
            Utilities.progressmultiplier = 0;
            UpdateProgress.Value = 100;
            UpdateProgress.Visible = false;

            switch (tabControl.SelectedIndex)
            {
                case 0:
                    {
                        if (bgwChartUpdater.IsBusy == false)
                        {
                            bgwChartUpdater.RunWorkerAsync();
                        }

                        break;
                    }
                case 1:
                    {
                        if (bgwTrackUpdater.IsBusy == false)
                        {
                            bgwTrackUpdater.RunWorkerAsync();
                        }

                        break;
                    }
                case 2:
                    {
                        if (bgwArtistUpdater.IsBusy == false)
                        {
                            bgwArtistUpdater.RunWorkerAsync();
                        }

                        break;
                    }
                case 3:
                    {
                        if (bgwAlbumUpdater.IsBusy == false)
                        {
                            bgwAlbumUpdater.RunWorkerAsync();
                        }

                        break;
                    }
                case 4:
                    {
                        if (bgwSearchUpdater.IsBusy == false)
                        {
                            bgwSearchUpdater.RunWorkerAsync();
                        }

                        break;
                    }
                case 5:
                    {
                        if (bgwUserUpdater.IsBusy == false)
                        {
                            bgwUserUpdater.RunWorkerAsync();
                        }

                        break;
                    }
                case 6:
                    {
                        if (bgwUserLookupUpdater.IsBusy == false)
                        {
                            bgwUserLookupUpdater.RunWorkerAsync();
                        }

                        break;
                    }
            }
            LastPlayedUpdate();
        }

        private void AutoRefresh(object sender, EventArgs e)
        {
            UpdateAll();
        }
        #endregion

        #region Update Threads
        private void ChartThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "Chart";
            UpdateCharts();
        }

        private void SearchThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "Search";
            UpdateSearch();
        }

        private void TrackThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "Track";
            UpdateTrack();
        }

        private void ArtistThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "Artist";
            UpdateArtist();
        }

        private void AlbumThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "Album";
            UpdateAlbum();
        }

        private void UserThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "User";
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                // start other threads
                if (bgwUserChartUpdater.IsBusy == false)
                {
                    bgwUserChartUpdater.RunWorkerAsync();
                }
                if (bgwUserHistoryUpdater.IsBusy == false)
                {
                    bgwUserHistoryUpdater.RunWorkerAsync();
                }
                if (bgwUserLovedUpdater.IsBusy == false)
                {
                    bgwUserLovedUpdater.RunWorkerAsync();
                }
                UpdateUser();
            }
        }

        private void UserLovedThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "UserLoved";
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                UserLovedTracksUpdate(nudUserLovedPage.Value.ToString());
            }
        }

        private void UserChartThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "UserChart";
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                if (radUserAllTime.Checked == true)
                {
                    UserChartsUpdate();
                }
                else
                {
                    UserChartsUpdate(Utilities.DateToUnix(dtpUserFrom.Value), Utilities.DateToUnix(dtpUserTo.Value));
                }
            }
        }

        private void UserHistoryThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "UserHistory";
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                UserHistoryUpdate();
            }
        }

        private void UserLThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "UserLookup";
            if (!string.IsNullOrEmpty(Utilities.userlookup))
            {
                LastPlayedUpdate();
                if (bgwUserLChartUpdater.IsBusy == false)
                {
                    bgwUserLChartUpdater.RunWorkerAsync();
                }
                if (bgwUserLHistoryUpdater.IsBusy == false)
                {
                    bgwUserLHistoryUpdater.RunWorkerAsync();
                }
                if (bgwUserLLovedUpdater.IsBusy == false)
                {
                    bgwUserLLovedUpdater.RunWorkerAsync();
                }
                UpdateUserL();
            }
        }

        private void UserLLovedThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "UserLLoved";
            if (!string.IsNullOrEmpty(Utilities.userlookup))
            {
                UserLLovedTracksUpdate(nudUserLLovedPage.Value.ToString());
            }
        }

        private void UserLChartThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "UserLChart";
            if (!string.IsNullOrEmpty(Utilities.userlookup))
            {
                if (radUserLAllTime.Checked == true)
                {
                    UserLChartsUpdate();
                }
                else
                {
                    UserLChartsUpdate(Utilities.DateToUnix(dtpUserLFrom.Value), Utilities.DateToUnix(dtpUserLTo.Value));
                }
            }
        }

        private void UserLHistoryThread(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "UserLHistory";
            if (!string.IsNullOrEmpty(Utilities.userlookup))
            {
                UserLHistoryUpdate();
            }
        }

        private byte[] _CustomScaling_statecontrol = new byte[7];
        #endregion

        #region General UI
        private void CustomScaling(object sender, EventArgs e)   // ensures operations arent needlessly run multiple times to reduce lag
        {

            // tabcontrol
            tabControl.Height = Height - 72;
            tabControl.Width = Width - 24;

            #region Chart
            // chart tlp scaling
            tlpCharts.Width = pgCharts.Width - 6;
            tlpCharts.Height = pgCharts.Height - 35;

            // user tlp scaling
            tlpUser.Width = pgUser.Width - 6;
            tlpUser.Height = pgUser.Height - 35;
            #endregion

            #region Search
            tlpSearch.Width = pgSearch.Width - 6;
            tlpSearch.Height = pgSearch.Height - 35;
            #endregion

            #region Track
            // track tlp
            tlpTrack.Width = pgTrack.Width - 6;
            tlpTrack.Height = pgTrack.Height - 35;

            // left tlp
            if (picTrackArt.Width >= 25)
            {
                spcTrack.SplitterDistance = picTrackArt.Width;
            }

            // add/remove buttons
            if (Width < 1110)
            {
                if (_CustomScaling_statecontrol[0] != 1)   // perf
                {
                    btnTrackTagAdd.Width = 22;
                    btnTrackTagRemove.Width = 22;
                    btnTrackTagAdd.Text = "+";
                    btnTrackTagRemove.Text = "-";
                    btnTrackTagRemove.Left = 62;
                    _CustomScaling_statecontrol[0] = 1;
                }
            }
            else if (_CustomScaling_statecontrol[0] != 2)   // perf
            {
                btnTrackTagAdd.Width = 62;
                btnTrackTagRemove.Width = 62;
                btnTrackTagAdd.Text = "Add";
                btnTrackTagRemove.Text = "Remove";
                btnTrackTagRemove.Left = 102;
                _CustomScaling_statecontrol[0] = 2;
            }

            // top bar
            if (Width <= 737 && Width > 694)
            {
                if (_CustomScaling_statecontrol[1] != 1)
                {
                    btnTrackGo.Width = 41;
                    btnTrackAdvanced.Width = 68;
                    btnTrackAdvanced.Left = 424;
                    _CustomScaling_statecontrol[1] = 1;
                }
            }
            else if (Width <= 694)
            {
                if (_CustomScaling_statecontrol[1] != 2)
                {
                    txtTrackTitle.Width = 125;
                    txtTrackArtist.Width = 125;
                    txtTrackArtist.Left = 186;
                    lblTrackArtist.Left = 156;
                    btnTrackGo.Width = 41;
                    btnTrackGo.Left = 317;
                    btnTrackAdvanced.Width = 68;
                    btnTrackAdvanced.Left = 364;
                    _CustomScaling_statecontrol[1] = 2;
                }
            }
            else if (_CustomScaling_statecontrol[1] != 3)
            {
                txtTrackArtist.Left = 216;
                txtTrackTitle.Width = 155;
                txtTrackArtist.Width = 155;
                btnTrackGo.Width = 75;
                btnTrackGo.Left = 377;
                btnTrackAdvanced.Width = 75;
                btnTrackAdvanced.Left = 458;
                lblTrackArtist.Left = 186;
                _CustomScaling_statecontrol[1] = 3;
            }

            // track spc2
            if (Width <= 1917 && Width > 1153)
            {
                if (spcTrack2.SplitterDistance != 26)    // perf
                {
                    spcTrack2.SplitterDistance = 26;
                }
            }
            else if (Width <= 1153 && Width > 769)
            {
                if (spcTrack2.SplitterDistance != 40)    // perf
                {
                    spcTrack2.SplitterDistance = 40;
                }
            }
            else if (Width <= 769 && Width > 682)
            {
                if (spcTrack2.SplitterDistance != 54)    // perf
                {
                    spcTrack2.SplitterDistance = 54;
                }
            }
            else if (Width <= 682)
            {
                if (spcTrack2.SplitterDistance != 66)    // perf
                {
                    spcTrack2.SplitterDistance = 66;
                }
            }
            else if (spcTrack2.SplitterDistance != 14)    // perf
            {
                spcTrack2.SplitterDistance = 14;
            }
            #endregion

            #region Artist
            // artist tlp
            tlpArtist.Width = pgArtist.Width - 6;
            tlpArtist.Height = pgArtist.Height - 35;

            // left tlp
            if (picArtistArt.Width >= 25)
            {
                spcArtist.SplitterDistance = picArtistArt.Width;
            }

            // user tag listview
            lstArtistUserTags.Height = gpbArtistUser.Height - 80;
            lstArtistUserTags.Width = gpbArtistUser.Width - 12;

            // add/remove buttons
            if (Width < 1110)
            {
                if (_CustomScaling_statecontrol[5] != 1)   // perf
                {
                    btnArtistTagAdd.Width = 22;
                    btnArtistTagRemove.Width = 22;
                    btnArtistTagAdd.Text = "+";
                    btnArtistTagRemove.Text = "-";
                    btnArtistTagRemove.Left = 62;
                    _CustomScaling_statecontrol[5] = 1;
                }
            }
            else if (_CustomScaling_statecontrol[5] != 2)   // perf
            {
                btnArtistTagAdd.Width = 62;
                btnArtistTagRemove.Width = 62;
                btnArtistTagAdd.Text = "Add";
                btnArtistTagRemove.Text = "Remove";
                btnArtistTagRemove.Left = 102;
                _CustomScaling_statecontrol[5] = 2;
            }

            // artist spc2
            if (Width <= 1917 && Width > 1153)
            {
                if (spcArtist2.SplitterDistance != 26)    // perf
                {
                    spcArtist2.SplitterDistance = 26;
                }
            }
            else if (Width <= 1153 && Width > 769)
            {
                if (spcArtist2.SplitterDistance != 40)    // perf
                {
                    spcArtist2.SplitterDistance = 40;
                }
            }
            else if (Width <= 769 && Width > 682)
            {
                if (spcArtist2.SplitterDistance != 54)    // perf
                {
                    spcArtist2.SplitterDistance = 54;
                }
            }
            else if (Width <= 682)
            {
                if (spcArtist2.SplitterDistance != 66)    // perf
                {
                    spcArtist2.SplitterDistance = 66;
                }
            }
            else if (spcArtist2.SplitterDistance != 14)    // perf
            {
                spcArtist2.SplitterDistance = 14;
            }
            #endregion

            #region Album
            // album tlp
            tlpAlbum.Width = pgAlbum.Width - 6;
            tlpAlbum.Height = pgAlbum.Height - 35;

            // left tlp
            if (picAlbumArt.Width >= 25)
            {
                spcAlbum.SplitterDistance = picAlbumArt.Width;
            }

            // user tag listview
            lstAlbumUserTags.Height = gpbAlbumUser.Height - 80;
            lstAlbumUserTags.Width = gpbAlbumUser.Width - 12;

            // add/remove buttons
            if (Width < 1110)
            {
                if (_CustomScaling_statecontrol[4] != 1)   // perf
                {
                    btnAlbumTagAdd.Width = 22;
                    btnAlbumTagRemove.Width = 22;
                    btnAlbumTagAdd.Text = "+";
                    btnAlbumTagRemove.Text = "-";
                    btnAlbumTagRemove.Left = 62;
                    _CustomScaling_statecontrol[4] = 1;
                }
            }
            else if (_CustomScaling_statecontrol[4] != 2)   // perf
            {
                btnAlbumTagAdd.Width = 62;
                btnAlbumTagRemove.Width = 62;
                btnAlbumTagAdd.Text = "Add";
                btnAlbumTagRemove.Text = "Remove";
                btnAlbumTagRemove.Left = 102;
                _CustomScaling_statecontrol[4] = 2;
            }

            // Album spc2
            if (Width <= 1917 && Width > 1153)
            {
                if (spcAlbum2.SplitterDistance != 26)    // perf
                {
                    spcAlbum2.SplitterDistance = 26;
                }
            }
            else if (Width <= 1153 && Width > 769)
            {
                if (spcAlbum2.SplitterDistance != 40)    // perf
                {
                    spcAlbum2.SplitterDistance = 40;
                }
            }
            else if (Width <= 769 && Width > 682)
            {
                if (spcAlbum2.SplitterDistance != 54)    // perf
                {
                    spcAlbum2.SplitterDistance = 54;
                }
            }
            else if (Width <= 682)
            {
                if (spcAlbum2.SplitterDistance != 66)    // perf
                {
                    spcAlbum2.SplitterDistance = 66;
                }
            }
            else if (spcAlbum2.SplitterDistance != 14)    // perf
            {
                spcAlbum2.SplitterDistance = 14;
            }
            #endregion

            #region User
            // user tlp
            tlpUser.Width = pgUser.Width - 6;
            tlpUser.Height = pgUser.Height - 35;

            // user chart
            tbcUserCharts.Width = gpbUserCharts.Width - 6;
            if (Width > 940)
            {
                tbcUserCharts.Height = gpbUserCharts.Height - 50;
                if (_CustomScaling_statecontrol[2] != 1)
                {
                    tbcUserCharts.Location = new Point(3, 45);
                    btnUserChartGo.Location = new Point(386, 18);
                    dtpUserTo.Location = new Point(283, 19);
                    lblUserTo.Location = new Point(263, 22);
                    _CustomScaling_statecontrol[2] = 1;
                }
            }
            else if (Width <= 940 && Width > 815)
            {
                tbcUserCharts.Height = gpbUserCharts.Height - 75;
                if (_CustomScaling_statecontrol[2] != 2)
                {
                    tbcUserCharts.Location = new Point(3, 70);
                    btnUserChartGo.Location = new Point(7, 43);
                    dtpUserTo.Location = new Point(283, 19);
                    lblUserTo.Location = new Point(263, 22);
                    _CustomScaling_statecontrol[2] = 2;
                }
            }
            else if (Width <= 815)
            {
                tbcUserCharts.Height = gpbUserCharts.Height - 75;
                if (_CustomScaling_statecontrol[2] != 3)
                {
                    tbcUserCharts.Location = new Point(3, 70);
                    btnUserChartGo.Location = new Point(129, 43);
                    dtpUserTo.Location = new Point(26, 44);
                    lblUserTo.Location = new Point(6, 47);
                    _CustomScaling_statecontrol[2] = 3;
                }
            }
            ltvUserHistory.Width = pgUserHistory.Width - 7;
            ltvUserHistory.Height = pgUserHistory.Height - 35;

            // user friends
            ltvUserFriends.Width = pgUserFriends.Width - 6;
            ltvUserFriends.Height = pgUserFriends.Height - 35;
            #endregion

            #region User Lookup
            // user loved tracks
            ltvUserLovedTracks.Width = pgUserLovedTracks.Width - 6;
            ltvUserLovedTracks.Height = pgUserLovedTracks.Height - 35;

            // user lookup tlp
            tlpUserL.Width = pgUserLookup.Width - 6;
            tlpUserL.Height = pgUserLookup.Height - 35;

            // user lookup chart
            tbcUserLCharts.Width = gpbUserLCharts.Width - 6;
            if (Width > 940)
            {
                tbcUserLCharts.Height = gpbUserLCharts.Height - 50;
                if (_CustomScaling_statecontrol[3] != 1)
                {
                    tbcUserLCharts.Location = new Point(3, 45);
                    btnUserLChartGo.Location = new Point(386, 18);
                    dtpUserLTo.Location = new Point(283, 19);
                    lblUserLTo.Location = new Point(263, 22);
                    _CustomScaling_statecontrol[3] = 1;
                }
            }
            else if (Width <= 940 && Width > 815)
            {
                tbcUserLCharts.Height = gpbUserLCharts.Height - 75;
                if (_CustomScaling_statecontrol[3] != 2)
                {
                    tbcUserLCharts.Location = new Point(3, 70);
                    btnUserLChartGo.Location = new Point(7, 43);
                    dtpUserLTo.Location = new Point(283, 19);
                    lblUserLTo.Location = new Point(263, 22);
                    _CustomScaling_statecontrol[3] = 2;
                }
            }
            else if (Width <= 815)
            {
                tbcUserLCharts.Height = gpbUserLCharts.Height - 75;
                if (_CustomScaling_statecontrol[3] != 3)
                {
                    tbcUserLCharts.Location = new Point(3, 70);
                    btnUserLChartGo.Location = new Point(129, 43);
                    dtpUserLTo.Location = new Point(26, 44);
                    lblUserLTo.Location = new Point(6, 47);
                    _CustomScaling_statecontrol[3] = 3;
                }
            }
            ltvUserLHistory.Width = pgUserLHistory.Width - 7;
            ltvUserLHistory.Height = pgUserLHistory.Height - 35;

            // user lookup friends
            ltvUserLFriends.Width = pgUserLFriends.Width - 6;
            ltvUserLFriends.Height = pgUserLFriends.Height - 35;

            // user lookup loved tracks
            ltvUserLLovedTracks.Width = pgUserLLovedTracks.Width - 6;
            ltvUserLLovedTracks.Height = pgUserLLovedTracks.Height - 35;
            #endregion

            #region Media
            // scrobble textboxes
            // title
            if (Width > 940)
            {
                txtMediaTitle.Width = pnlMediaScrobble.Width - 37;
            }
            // artist
            if (Width > 940)
            {
                txtMediaArtist.Width = pnlMediaScrobble.Width - 40;
            }
            // album
            if (Width > 940)
            {
                txtMediaAlbum.Width = pnlMediaScrobble.Width - 42;
            }
            // time
            if (Width > 940)
            {
                cmbMediaTime.Left = pnlMediaScrobble.Width - 59;
                nudMediaMinute.Left = cmbMediaTime.Left - 42;
                lblMediaDivider.Left = nudMediaMinute.Left - 8;
                nudMediaHour.Left = lblMediaDivider.Left - 31;
                dtpMediaScrobble.Width = pnlMediaScrobble.Width - (pnlMediaScrobble.Width - nudMediaHour.Left) - 42;
            }

            // history listview
            // height
            if (pnlMediaScrobble.Height > 450)
            {
                ltvMediaHistory.Height = pnlMediaScrobble.Height - 338;
            }
            // width
            if (Width > 940)
            {
                ltvMediaHistory.Width = pnlMediaScrobble.Width;
            }
            #endregion

            #region Easter Egg
            if (Size == MinimumSize)
            {
                var rng = new Random();
                byte random = (byte)rng.Next(0, 99);
                if (random == 69)
                {
                    Text = Text.Remove(0, 10);
                    Text = Text.Insert(0, "I HATE YOU");
                }
            }
            #endregion

        }

        private void FormLoad(object sender, EventArgs e)
        {
            Thread.CurrentThread.Name = "Main";

            // set form title
            if (string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                lblUser.Text = "Current User: N/A (Please Set User)";
                Text = "Audiograph";
            }
            else
            {
                lblUser.Text = "Current User: " + My.MySettingsProperty.Settings.User;
                Text = "Audiograph - " + My.MySettingsProperty.Settings.User;
            }

            // remove session key if no user
            if (string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                My.MySettingsProperty.Settings.SessionKey = string.Empty;
                My.MySettingsProperty.Settings.Save();
            }

            // enable or disable authentication ui
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.SessionKey) && !string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                AuthenticatedUI(true);
            }
            else
            {
                AuthenticatedUI(false);
            }

            // set timezone offset
            var offsetspan = new TimeSpan();
            var timezone = TimeZone.CurrentTimeZone;
            offsetspan = timezone.GetUtcOffset(DateTime.Now);
            Utilities.timezoneoffset = (int)Math.Round(offsetspan.TotalSeconds);

            // scrobble index
            if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.CurrentScrobbleIndex))
            {
                LoadScrobbleIndex(My.MySettingsProperty.Settings.CurrentScrobbleIndex);
            }
            else
            {
                radMediaEnable.Enabled = false;
                radMediaDisable.Enabled = false;
                radMediaDisable.Checked = true;
                btnMediaEditIndex.Enabled = false;
            }

            // set scrobble time
            cmbMediaTime.SelectedIndex = 0;

            // get version of wmp
            lblMediaVersion.Text = "v." + MediaPlayer.versionInfo;

            // set auto refresh time
            switch (My.MySettingsProperty.Settings.AutoRefresh)
            {
                case 1:
                    {
                        mnuAuto1Min.PerformClick();
                        break;
                    }
                case 2:
                    {
                        mnuAuto2Min.PerformClick();
                        break;
                    }
                case 3:
                    {
                        mnuAuto3Min.PerformClick();
                        break;
                    }
                case 4:
                    {
                        mnuAuto4Min.PerformClick();
                        break;
                    }
                case 5:
                    {
                        mnuAuto5Min.PerformClick();
                        break;
                    }
                case 10:
                    {
                        mnuAuto10Min.PerformClick();
                        break;
                    }

                default:
                    {
                        mnuAutoOff.PerformClick();
                        break;
                    }
            }

            // update all
            UpdateAll();

            // turn off load execution prevention
            Utilities.stoploadexecution = false;
        }

        private void LayoutSuspend(object sender, EventArgs e)
        {
            tlpCharts.SuspendLayout();
        }

        private void LayoutResume(object sender, EventArgs e)
        {
            tlpCharts.ResumeLayout();
        }

        // reset user text box and user status label when opening user tab
        private void TabChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 5)
            {
                txtUser.Text = My.MySettingsProperty.Settings.User;
                My.MySettingsProperty.Settings.Save();
                if (!string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
                {
                    if (string.IsNullOrEmpty(My.MySettingsProperty.Settings.SessionKey))
                    {
                        lblUserStatus.Text = "Welcome, " + My.MySettingsProperty.Settings.User;
                    }
                    else
                    {
                        lblUserStatus.Text = "Welcome, " + My.MySettingsProperty.Settings.User + " (Authenticated)";
                    }
                    Text = "Audiograph - " + My.MySettingsProperty.Settings.User;
                }
                else
                {
                    lblUserStatus.Text = "User Not Set";
                }
            }

            CustomScaling(null, null);
        }

        // opens clicked image in browser
        private void ArtClicked(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && ((PictureBox)sender).ImageLocation.Contains("http") == true)
            {
                Process.Start(((PictureBox)sender).ImageLocation);
            }
        }

        // makes ui changes when user is authenticated
        public void AuthenticatedUI(bool yesno)
        {
            if (yesno == true)
            {
                btnUserAuthenticate.Enabled = false;
                // user labels
                if (lblUser.Text.Contains("(Authenticated)") == false)
                {
                    lblUser.Text += " (Authenticated)";
                    lblUserStatus.Text += " (Authenticated)";
                }
                // track love
                btnTrackLove.Enabled = true;
                // add/remove tags
                btnTrackTagAdd.Enabled = true;
                btnTrackTagRemove.Enabled = true;
                btnArtistTagAdd.Enabled = true;
                btnArtistTagRemove.Enabled = true;
                btnAlbumTagAdd.Enabled = true;
                btnAlbumTagRemove.Enabled = true;
                // auth warning
                lblTrackAuthWarning.Enabled = false;
                lblArtistAuthWarning.Enabled = false;
                lblAlbumAuthWarning.Enabled = false;
                // ui labels
                lblTrackLove.Enabled = true;
                lblTrackTags.Enabled = true;
                lblArtistTags.Enabled = true;
                lblAlbumTags.Enabled = true;
                // media
                txtMediaTitle.Enabled = true;
                txtMediaArtist.Enabled = true;
                txtMediaAlbum.Enabled = true;
                btnMediaScrobble.Enabled = true;
                btnMediaVerify.Enabled = true;
                btnMediaSearch.Enabled = true;
                dtpMediaScrobble.Enabled = true;
                nudMediaHour.Enabled = true;
                nudMediaMinute.Enabled = true;
                lblMediaTitle.Enabled = true;
                lblMediaArtist.Enabled = true;
                lblMediaAlbum.Enabled = true;
                lblMediaTime.Enabled = true;
                lblMediaDivider.Enabled = true;
                radMediaDisable.Enabled = true;
                cmbMediaTime.Enabled = true;
                radMediaEnable.Enabled = true;
                radMediaEnable.Checked = true;
                lblMediaScrobble.Text = "Nothing scrobbled yet.";
            }
            else
            {
                btnUserAuthenticate.Enabled = true;
                // user labels
                lblUser.Text = lblUser.Text.Replace(" (Authenticated)", string.Empty);
                lblUserStatus.Text = lblUser.Text.Replace(" (Authenticated)", string.Empty);
                // track love
                btnTrackLove.Enabled = false;
                // add/remove tags
                btnTrackTagAdd.Enabled = false;
                btnTrackTagRemove.Enabled = false;
                btnArtistTagAdd.Enabled = false;
                btnArtistTagRemove.Enabled = false;
                btnAlbumTagAdd.Enabled = false;
                btnAlbumTagRemove.Enabled = false;
                // auth warning
                lblTrackAuthWarning.Enabled = true;
                lblArtistAuthWarning.Enabled = true;
                lblAlbumAuthWarning.Enabled = true;
                // ui labels
                lblTrackLove.Enabled = false;
                lblTrackTags.Enabled = false;
                lblArtistTags.Enabled = false;
                lblAlbumTags.Enabled = false;
                // media
                txtMediaTitle.Enabled = false;
                txtMediaArtist.Enabled = false;
                txtMediaAlbum.Enabled = false;
                btnMediaScrobble.Enabled = false;
                btnMediaVerify.Enabled = false;
                btnMediaSearch.Enabled = false;
                dtpMediaScrobble.Enabled = false;
                nudMediaHour.Enabled = false;
                nudMediaMinute.Enabled = false;
                lblMediaTitle.Enabled = false;
                lblMediaArtist.Enabled = false;
                lblMediaAlbum.Enabled = false;
                lblMediaTime.Enabled = false;
                lblMediaDivider.Enabled = false;
                radMediaDisable.Enabled = false;
                cmbMediaTime.Enabled = false;
                radMediaEnable.Enabled = false;
                radMediaDisable.Checked = true;
                lblMediaScrobble.Text = "Authenticate account to begin scrobbling.";
            }
        }

        private void AutoUncheckAll()
        {
            mnuAuto1Min.Checked = false;
            mnuAuto2Min.Checked = false;
            mnuAuto3Min.Checked = false;
            mnuAuto4Min.Checked = false;
            mnuAuto5Min.Checked = false;
            mnuAuto10Min.Checked = false;
            mnuAutoOff.Checked = false;
        }
        #endregion

        #region Menu Items
        private void UpdateAllButton(object sender, EventArgs e)
        {
            UpdateAll();
        }

        private void OpenAPIHistory(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmAPIHistory.Show();
            My.MyProject.Forms.frmAPIHistory.Activate();
        }

        private void LFMWebsite(object sender, EventArgs e)
        {
            Process.Start("https://www.last.fm");
        }

        private void About(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmAbout.Show();
            My.MyProject.Forms.frmAbout.Activate();
        }

        private void AltF4(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Reload(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void OpenConsole(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmConsole.Show();
            My.MyProject.Forms.frmConsole.Activate();
        }

        private void OpenScrobbleHistory(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmScrobbleHistory.Show();
            My.MyProject.Forms.frmScrobbleHistory.Activate();
        }

        private void OpenScrobbleIndexEditor(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmScrobbleIndexEditor.Show();
            My.MyProject.Forms.frmScrobbleIndexEditor.Activate();
            My.MyProject.Forms.frmScrobbleIndexEditor.NewFile();
        }

        private void OpenBackupTool(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmBackupTool.Show();
            My.MyProject.Forms.frmBackupTool.Activate();
        }

        // takes user to user tab and selects the user box
        private void SetUserButton(object sender, EventArgs e)
        {
            tabControl.SelectTab(5);
            txtUser.Select();
            txtUser.SelectAll();
        }

        // switch to user tab
        private void UserClicked(object sender, EventArgs e)
        {
            tabControl.SelectTab(5);
        }

        // go to track page with last played track
        private void LastPlayedClicked(object sender, EventArgs e)
        {
            string[] trackinfo = lblLastPlayed.Tag.ToString().Split(Conversions.ToChar(Constants.vbCrLf));    // split lines into artist and track
            trackinfo[1].Replace(Constants.vbCrLf, string.Empty);  // remove vbcrlf
                                                                   // do not proceed if unavailble
            if (trackinfo[0] != "(Unavailable)" && trackinfo[1] != "(Unavailable)")
            {
                Utilities.GoToTrack(trackinfo[1], trackinfo[0]);
            }
        }

        private void ProgressBarClicked(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmAPIHistory.Show();
            My.MyProject.Forms.frmAPIHistory.Activate();
        }

        private void Auto1Minute(object sender, EventArgs e)
        {
            // check
            AutoUncheckAll();
            mnuAuto1Min.Checked = true;

            tmrAutoRefresh.Stop();
            tmrAutoRefresh.Interval = 60000;
            tmrAutoRefresh.Start();

            My.MySettingsProperty.Settings.AutoRefresh = 1;
        }

        private void Auto2Minute(object sender, EventArgs e)
        {
            // check
            AutoUncheckAll();
            mnuAuto2Min.Checked = true;

            tmrAutoRefresh.Stop();
            tmrAutoRefresh.Interval = 120000;
            tmrAutoRefresh.Start();

            My.MySettingsProperty.Settings.AutoRefresh = 2;
        }

        private void Auto3Minute(object sender, EventArgs e)
        {
            // check
            AutoUncheckAll();
            mnuAuto3Min.Checked = true;

            tmrAutoRefresh.Stop();
            tmrAutoRefresh.Interval = 180000;
            tmrAutoRefresh.Start();

            My.MySettingsProperty.Settings.AutoRefresh = 3;
        }

        private void Auto4Minute(object sender, EventArgs e)
        {
            // check
            AutoUncheckAll();
            mnuAuto4Min.Checked = true;

            tmrAutoRefresh.Stop();
            tmrAutoRefresh.Interval = 240000;
            tmrAutoRefresh.Start();

            My.MySettingsProperty.Settings.AutoRefresh = 4;
        }

        private void Auto5Minute(object sender, EventArgs e)
        {
            // check
            AutoUncheckAll();
            mnuAuto5Min.Checked = true;

            tmrAutoRefresh.Stop();
            tmrAutoRefresh.Interval = 300000;
            tmrAutoRefresh.Start();

            My.MySettingsProperty.Settings.AutoRefresh = 5;
        }

        private void Auto10Minute(object sender, EventArgs e)
        {
            // check
            AutoUncheckAll();
            mnuAuto10Min.Checked = true;

            tmrAutoRefresh.Stop();
            tmrAutoRefresh.Interval = 600000;
            tmrAutoRefresh.Start();

            My.MySettingsProperty.Settings.AutoRefresh = 10;
        }

        private void AutoOff(object sender, EventArgs e)
        {
            // check
            AutoUncheckAll();
            mnuAutoOff.Checked = true;

            tmrAutoRefresh.Stop();

            My.MySettingsProperty.Settings.AutoRefresh = 0;
        }

        private void ViewCharts(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }

        private void ViewSearch(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void ViewTrack(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 2;
        }

        private void ViewArtist(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 3;
        }

        private void ViewAlbum(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 4;
        }

        private void ViewUser(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 5;
        }

        private void ViewUserL(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 6;
        }

        private void ViewMedia(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 7;
        }
        #endregion

        #region Context Menu
        private void CmsArtOpen(object sender, EventArgs e)
        {
            // get parent
            PictureBox parent = (PictureBox)((ContextMenuStrip)sender).SourceControl;

            if (parent.ImageLocation.Contains("http") == true)
            {
                mnuArtOpenImage.Enabled = true;
                mnuArtCopyImage.Enabled = true;
                mnuArtCopyImageLink.Enabled = true;
                mnuArtSaveImage.Enabled = true;
            }
            else
            {
                mnuArtOpenImage.Enabled = false;
                mnuArtCopyImage.Enabled = false;
                mnuArtCopyImageLink.Enabled = false;
                mnuArtSaveImage.Enabled = false;
            }
        }

        private void ArtOpenImage(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            PictureBox finalParent = (PictureBox)parent2.SourceControl;

            if (finalParent.ImageLocation.Contains("http") == true)
            {
                Process.Start(finalParent.ImageLocation);
            }
        }

        private void ArtCopyImage(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            PictureBox finalParent = (PictureBox)parent2.SourceControl;

            try
            {
                My.MyProject.Computer.Clipboard.SetImage(finalParent.Image);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program was unable to write to the clipboard" + Constants.vbCrLf + "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ArtCopyImageLink(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            PictureBox finalParent = (PictureBox)parent2.SourceControl;

            try
            {
                My.MyProject.Computer.Clipboard.SetText(finalParent.ImageLocation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program was unable to write to the clipboard" + Constants.vbCrLf + "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ArtSaveImage(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            PictureBox finalParent = (PictureBox)parent2.SourceControl;

            var result = sfdSaveImage.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    switch (sfdSaveImage.FilterIndex)
                    {
                        case 0:
                            {
                                // jpeg
                                finalParent.Image.Save(sfdSaveImage.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            }
                        case 1:
                            {
                                // png
                                finalParent.Image.Save(sfdSaveImage.FileName, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            }
                        case 2:
                            {
                                // bmp
                                finalParent.Image.Save(sfdSaveImage.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                            }

                        default:
                            {
                                // all files
                                finalParent.Image.Save(sfdSaveImage.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Program was unable to save image" + Constants.vbCrLf + "Message: " + ex.Message, "Image Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CmsChartTrackOpen(object sender, EventArgs e)
        {
            byte row = CMSLists.GetChartRowIndex((Label)((ContextMenuStrip)sender).SourceControl);

            // disable track if needed
            if (CMSLists.ChartTrackLabel[row, 0].Text.Contains("(Unavailable)") == true || CMSLists.ChartTrackLabel[row, 0].Text.Contains("ERROR: ") == true || CMSLists.ChartTrackLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.ChartTrackLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuChartTrackGoToTrack.Enabled = false;
                mnuChartTrackBackupTrack.Enabled = false;
            }
            else
            {
                mnuChartTrackGoToTrack.Enabled = true;
                mnuChartTrackBackupTrack.Enabled = true;
            }

            // disable artist if needed
            if (CMSLists.ChartTrackLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.ChartTrackLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuChartTrackGoToArtist.Enabled = false;
                mnuChartTrackBackupArtist.Enabled = false;
            }
            else
            {
                mnuChartTrackGoToArtist.Enabled = true;
                mnuChartTrackBackupArtist.Enabled = true;
            }

            // disable album if needed
            if (CMSLists.ChartTrackLabel[row, 2].Text.Contains("(Unavailable)") == true || CMSLists.ChartTrackLabel[row, 2].Text.Contains("ERROR: ") == true || CMSLists.ChartTrackLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.ChartTrackLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuChartTrackGoToAlbum.Enabled = false;
                mnuChartTrackBackupAlbum.Enabled = false;
            }
            else
            {
                mnuChartTrackGoToAlbum.Enabled = true;
                mnuChartTrackBackupAlbum.Enabled = true;
            }
        }

        private void ChartTrackGoToTrack(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetChartRowIndex(finalParent);

            Utilities.GoToTrack(CMSLists.ChartTrackLabel[row, 0].Text, CMSLists.ChartTrackLabel[row, 1].Text);
        }

        private void ChartTrackGoToArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetChartRowIndex(finalParent);

            Utilities.GoToArtist(CMSLists.ChartTrackLabel[row, 1].Text);
        }

        private void ChartTrackGoToAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetChartRowIndex(finalParent);

            Utilities.GoToAlbum(CMSLists.ChartTrackLabel[row, 2].Text, CMSLists.ChartTrackLabel[row, 1].Text);
        }

        private void ChartTrackBackupTrack(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetChartRowIndex(finalParent);

            Utilities.BackupTrack(CMSLists.ChartTrackLabel[row, 0].Text, CMSLists.ChartTrackLabel[row, 1].Text);
        }

        private void ChartTrackBackupArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetChartRowIndex(finalParent);

            Utilities.BackupArtist(CMSLists.ChartTrackLabel[row, 1].Text);
        }

        private void ChartTrackBackupAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetChartRowIndex(finalParent);

            Utilities.BackupAlbum(CMSLists.ChartTrackLabel[row, 2].Text, CMSLists.ChartTrackLabel[row, 1].Text);
        }

        private void CmsChartArtistOpen(object sender, CancelEventArgs e)
        {
            byte row = CMSLists.GetChartRowIndex((Label)((ContextMenuStrip)sender).SourceControl);

            // disable artist if needed
            if (CMSLists.ChartArtistLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.ChartArtistLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuChartArtistGoToArtist.Enabled = false;
                mnuChartArtistBackupArtist.Enabled = false;
            }
            else
            {
                mnuChartArtistGoToArtist.Enabled = true;
                mnuChartArtistBackupArtist.Enabled = true;
            }
        }

        private void ChartArtistGoToArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetChartRowIndex(finalParent);

            Utilities.GoToArtist(CMSLists.ChartArtistLabel[row, 0].Text);
        }

        private void ChartArtistBackupArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetChartRowIndex(finalParent);

            Utilities.BackupArtist(CMSLists.ChartArtistLabel[row, 0].Text);
        }

        private void CmsSearchOpen(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchInfo.Text))
            {
                e.Cancel = true;
                return;
            }

            if (string.IsNullOrEmpty(txtSearchInfo.SelectedText))
            {
                mnuSearchCopy.Enabled = false;
            }
            else
            {
                mnuSearchCopy.Enabled = true;
            }
        }

        private void SearchBackupTag(object sender, EventArgs e)
        {
            Utilities.BackupTag(txtSearchInfo.Text.Split(Conversions.ToChar(Constants.vbLf))[1]);
        }

        private void SearchSelectAll(object sender, EventArgs e)
        {
            txtSearchInfo.SelectAll();
        }

        private void SearchCopy(object sender, EventArgs e)
        {
            try
            {
                My.MyProject.Computer.Clipboard.SetText(txtSearchInfo.SelectedText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program was unable to write to the clipboard" + Constants.vbCrLf + "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmsSearchListsOpen(object sender, CancelEventArgs e)
        {
            if (((ListView)((ContextMenuStrip)sender).SourceControl).SelectedItems.Count <= 0)
            {
                e.Cancel = true;
            }
        }

        private void SearchGoToTrack(object sender, EventArgs e)
        {
            Utilities.GoToTrack(ltvSearchTracks.SelectedItems[0].SubItems[1].Text, ltvSearchTracks.SelectedItems[0].SubItems[2].Text);
        }

        private void SearchGoToArtist(object sender, EventArgs e)
        {
            Utilities.GoToArtist(ltvSearchArtists.SelectedItems[0].SubItems[1].Text);
        }

        private void SearchGoToAlbum(object sender, EventArgs e)
        {
            Utilities.GoToAlbum(ltvSearchAlbums.SelectedItems[0].SubItems[1].Text, ltvSearchAlbums.SelectedItems[0].SubItems[2].Text);
        }

        private void SearchBackupTrack(object sender, EventArgs e)
        {
            Utilities.BackupTrack(ltvSearchTracks.SelectedItems[0].SubItems[1].Text, ltvSearchTracks.SelectedItems[0].SubItems[2].Text);
        }

        private void SearchBackupArtist(object sender, EventArgs e)
        {
            Utilities.BackupArtist(ltvSearchArtists.SelectedItems[0].SubItems[1].Text);
        }

        private void SearchBackupAlbum(object sender, EventArgs e)
        {
            Utilities.BackupAlbum(ltvSearchAlbums.SelectedItems[0].SubItems[1].Text, ltvSearchAlbums.SelectedItems[0].SubItems[2].Text);
        }

        private void CmsTrackOpen(object sender, CancelEventArgs e)
        {
            // do not open if nothing is in the text box
            if (string.IsNullOrEmpty(txtTrackInfo.Text.Trim()))
            {
                e.Cancel = true;
                return;
            }

            if (string.IsNullOrEmpty(txtTrackInfo.SelectedText))
            {
                mnuTrackCopy.Enabled = false;
            }
            else
            {
                mnuTrackCopy.Enabled = true;
            }
        }

        private void TrackBackupTrack(object sender, EventArgs e)
        {
            Utilities.BackupTrack(txtTrackInfo.Text.Split(Conversions.ToChar(Constants.vbLf))[1], txtTrackInfo.Text.Split(Conversions.ToChar(Constants.vbLf))[4]);
        }

        private void TrackSelectAll(object sender, EventArgs e)
        {
            txtTrackInfo.SelectAll();
        }

        private void TrackCopy(object sender, EventArgs e)
        {
            try
            {
                My.MyProject.Computer.Clipboard.SetText(txtTrackInfo.SelectedText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program was unable to write to the clipboard" + Constants.vbCrLf + "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmsArtistOpen(object sender, CancelEventArgs e)
        {
            // do not open if nothing is in the text box
            if (string.IsNullOrEmpty(txtArtistInfo.Text.Trim()))
            {
                e.Cancel = true;
                return;
            }

            if (string.IsNullOrEmpty(txtArtistInfo.SelectedText))
            {
                mnuArtistCopy.Enabled = false;
            }
            else
            {
                mnuArtistCopy.Enabled = true;
            }
        }

        private void ArtistBackupArtist(object sender, EventArgs e)
        {
            Utilities.BackupArtist(txtArtistInfo.Text.Split(Conversions.ToChar(Constants.vbLf))[1]);
        }

        private void ArtistSelectAll(object sender, EventArgs e)
        {
            txtArtistInfo.SelectAll();
        }

        private void ArtistCopy(object sender, EventArgs e)
        {
            try
            {
                My.MyProject.Computer.Clipboard.SetText(txtArtistInfo.SelectedText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program was unable to write to the clipboard" + Constants.vbCrLf + "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmsAlbumOpen(object sender, CancelEventArgs e)
        {
            // do not open if nothing is in the text box
            if (string.IsNullOrEmpty(txtAlbumInfo.Text.Trim()))
            {
                e.Cancel = true;
                return;
            }

            if (string.IsNullOrEmpty(txtAlbumInfo.SelectedText))
            {
                mnuAlbumCopy.Enabled = false;
            }
            else
            {
                mnuAlbumCopy.Enabled = true;
            }
        }

        private void AlbumBackupAlbum(object sender, EventArgs e)
        {
            Utilities.BackupAlbum(txtAlbumInfo.Text.Split(Conversions.ToChar(Constants.vbLf))[1], txtAlbumInfo.Text.Split(Conversions.ToChar(Constants.vbLf))[4]);
        }

        private void AlbumSelectAll(object sender, EventArgs e)
        {
            txtAlbumInfo.SelectAll();
        }

        private void AlbumCopy(object sender, EventArgs e)
        {
            try
            {
                My.MyProject.Computer.Clipboard.SetText(txtAlbumInfo.SelectedText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program was unable to write to the clipboard" + Constants.vbCrLf + "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmsUserOpen(object sender, CancelEventArgs e)
        {
            // do not open if nothing is in the text box
            if (string.IsNullOrEmpty(txtUserInfo.Text.Trim()))
            {
                e.Cancel = true;
                return;
            }

            if (string.IsNullOrEmpty(txtUserInfo.SelectedText))
            {
                mnuUserCopy.Enabled = false;
            }
            else
            {
                mnuUserCopy.Enabled = true;
            }
        }

        private void UserBackupUser(object sender, EventArgs e)
        {
            Utilities.BackupUser(txtUserInfo.Text.Split(Conversions.ToChar(Constants.vbLf))[1]);
        }

        private void UserSelectAll(object sender, EventArgs e)
        {
            txtUserInfo.SelectAll();
        }

        private void UserCopy(object sender, EventArgs e)
        {
            try
            {
                My.MyProject.Computer.Clipboard.SetText(txtUserInfo.SelectedText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program was unable to write to the clipboard" + Constants.vbCrLf + "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmsUserFriendsOpen(object sender, CancelEventArgs e)
        {
            // do not open if nothing is selected
            if (ltvUserFriends.SelectedItems.Count <= 0)
            {
                e.Cancel = true;
                return;
            }
        }

        private void UserFriendGoToUser(object sender, EventArgs e)
        {
            txtUserL.Text = ltvUserFriends.SelectedItems[0].SubItems[0].Text;
            tabControl.SelectedIndex = 6;
            btnUserLSet.PerformClick();
        }

        private void UserFriendOpenLink(object sender, EventArgs e)
        {
            Process.Start(ltvUserFriends.SelectedItems[0].SubItems[2].Text);
        }

        private void UserFriendBackupUser(object sender, EventArgs e)
        {
            Utilities.BackupUser(ltvUserFriends.SelectedItems[0].SubItems[0].Text);
        }

        private void UserFriendCopyUsername(object sender, EventArgs e)
        {
            try
            {
                My.MyProject.Computer.Clipboard.SetText(ltvUserFriends.SelectedItems[0].SubItems[0].Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program was unable to write to the clipboard" + Constants.vbCrLf + "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmsUserLovedTracksOpen(object sender, CancelEventArgs e)
        {
            // do not open if nothing is selected
            if (ltvUserLovedTracks.SelectedItems.Count <= 0)
            {
                e.Cancel = true;
                return;
            }
        }

        private void UserLovedTrackGoToTrack(object sender, EventArgs e)
        {
            Utilities.GoToTrack(ltvUserLovedTracks.SelectedItems[0].SubItems[0].Text, ltvUserLovedTracks.SelectedItems[0].SubItems[1].Text);
        }

        private void UserLovedTrackGoToArtist(object sender, EventArgs e)
        {
            Utilities.GoToArtist(ltvUserLovedTracks.SelectedItems[0].SubItems[1].Text);
        }

        private void UserLovedTrackBackupTrack(object sender, EventArgs e)
        {
            Utilities.BackupTrack(ltvUserLovedTracks.SelectedItems[0].SubItems[0].Text, ltvUserLovedTracks.SelectedItems[0].SubItems[1].Text);
        }

        private void UserLovedTrackBackupArtist(object sender, EventArgs e)
        {
            Utilities.BackupArtist(ltvUserLovedTracks.SelectedItems[0].SubItems[1].Text);
        }

        private void CmsUserRecentOpen(object sender, CancelEventArgs e)
        {
            byte row;
            try
            {
                row = CMSLists.GetUserRecentRowIndex((Label)((ContextMenuStrip)sender).SourceControl);

            }
            catch (InvalidCastException)
            {
                return;
            }

            // disable track if needed
            if (CMSLists.UserRecentLabel[row, 0].Text.Contains("(Unavailable)") == true || CMSLists.UserRecentLabel[row, 0].Text.Contains("ERROR: ") == true || CMSLists.UserRecentLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserRecentLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserGoToRecentTrack.Enabled = false;
                mnuUserBackupRecentTrack.Enabled = false;
            }
            else
            {
                mnuUserGoToRecentTrack.Enabled = true;
                mnuUserBackupRecentTrack.Enabled = true;
            }

            // disable artist if needed
            if (CMSLists.UserRecentLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserRecentLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserGoToRecentArtist.Enabled = false;
                mnuUserBackupRecentArtist.Enabled = false;
            }
            else
            {
                mnuUserGoToRecentArtist.Enabled = true;
                mnuUserBackupRecentArtist.Enabled = true;
            }

            // disable album if needed
            if (CMSLists.UserRecentLabel[row, 2].Text.Contains("(Unavailable)") == true || CMSLists.UserRecentLabel[row, 2].Text.Contains("ERROR: ") == true || CMSLists.UserRecentLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserRecentLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserGoToRecentAlbum.Enabled = false;
                mnuUserBackupRecentAlbum.Enabled = false;
            }
            else
            {
                mnuUserGoToRecentAlbum.Enabled = true;
                mnuUserBackupRecentAlbum.Enabled = true;
            }
        }

        private void UserRecentGoToTrack(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserRecentRowIndex(finalParent);

            Utilities.GoToTrack(CMSLists.UserRecentLabel[row, 0].Text, CMSLists.UserRecentLabel[row, 1].Text);
        }

        private void UserRecentGoToArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserRecentRowIndex(finalParent);

            Utilities.GoToArtist(CMSLists.UserRecentLabel[row, 1].Text);
        }

        private void UserRecentGoToAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserRecentRowIndex(finalParent);

            Utilities.GoToAlbum(CMSLists.UserRecentLabel[row, 2].Text, CMSLists.UserRecentLabel[row, 1].Text);
        }

        private void UserRecentBackupTrack(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserRecentRowIndex(finalParent);

            Utilities.BackupTrack(CMSLists.UserRecentLabel[row, 0].Text, CMSLists.UserRecentLabel[row, 1].Text);
        }

        private void UserRecentBackupArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserRecentRowIndex(finalParent);

            Utilities.BackupArtist(CMSLists.UserRecentLabel[row, 1].Text);
        }

        private void UserRecentBackupAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserRecentRowIndex(finalParent);

            Utilities.BackupAlbum(CMSLists.UserRecentLabel[row, 2].Text, CMSLists.UserRecentLabel[row, 1].Text);
        }

        private void CmsUserTopTrackOpen(object sender, CancelEventArgs e)
        {
            byte row = CMSLists.GetUserTopTrackRowIndex((Label)((ContextMenuStrip)sender).SourceControl);

            // disable track if needed
            if (CMSLists.UserTopTracksLabel[row, 0].Text.Contains("(Unavailable)") == true || CMSLists.UserTopTracksLabel[row, 0].Text.Contains("ERROR: ") == true || CMSLists.UserTopTracksLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserTopTracksLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserTopTrackGoToTrack.Enabled = false;
                mnuUserTopTrackBackupTrack.Enabled = false;
            }
            else
            {
                mnuUserTopTrackGoToTrack.Enabled = true;
                mnuUserTopTrackBackupTrack.Enabled = true;
            }

            // disable artist if needed
            if (CMSLists.UserTopTracksLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserTopTracksLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserTopTrackGoToTrack.Enabled = false;
                mnuUserTopTrackBackupTrack.Enabled = false;
            }
            else
            {
                mnuUserTopTrackGoToTrack.Enabled = true;
                mnuUserTopTrackBackupTrack.Enabled = true;
            }

            // disable album if needed
            if (CMSLists.UserTopTracksLabel[row, 2].Text.Contains("(Unavailable)") == true || CMSLists.UserTopTracksLabel[row, 2].Text.Contains("ERROR: ") == true || CMSLists.UserTopTracksLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserTopTracksLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserTopTrackGoToAlbum.Enabled = false;
                mnuUserTopTrackBackupAlbum.Enabled = false;
            }
            else
            {
                mnuUserTopTrackGoToAlbum.Enabled = true;
                mnuUserTopTrackBackupAlbum.Enabled = true;
            }
        }

        private void UserTopTrackGoToTrack(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopTrackRowIndex(finalParent);

            Utilities.GoToTrack(CMSLists.UserTopTracksLabel[row, 0].Text, CMSLists.UserTopTracksLabel[row, 1].Text);
        }

        private void UserTopTrackGoToArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopTrackRowIndex(finalParent);

            Utilities.GoToArtist(CMSLists.UserTopTracksLabel[row, 1].Text);
        }

        private void UserTopTrackGoToAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopTrackRowIndex(finalParent);

            Utilities.GoToAlbum(CMSLists.UserTopTracksLabel[row, 2].Text, CMSLists.UserTopTracksLabel[row, 1].Text);
        }

        private void UserTopTrackBackupTrack(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopTrackRowIndex(finalParent);

            Utilities.BackupTrack(CMSLists.UserTopTracksLabel[row, 0].Text, CMSLists.UserTopTracksLabel[row, 1].Text);
        }

        private void UserTopTrackBackupArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopTrackRowIndex(finalParent);

            Utilities.BackupArtist(CMSLists.UserTopTracksLabel[row, 1].Text);
        }

        private void UserTopTrackBackupAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopTrackRowIndex(finalParent);

            Utilities.BackupAlbum(CMSLists.UserTopTracksLabel[row, 2].Text, CMSLists.UserTopTracksLabel[row, 1].Text);
        }

        private void UserTopArtistGoToArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopArtistRowIndex(finalParent);

            Utilities.GoToArtist(CMSLists.UserTopArtistsLabel[row, 0].Text);
        }

        private void UserTopArtistBackupArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopArtistRowIndex(finalParent);

            Utilities.BackupArtist(CMSLists.UserTopArtistsLabel[row, 0].Text);
        }

        private void UserTopAlbumGoToAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopAlbumRowIndex(finalParent);

            Utilities.GoToAlbum(CMSLists.UserTopAlbumsLabel[row, 0].Text, CMSLists.UserTopAlbumsLabel[row, 1].Text);
        }

        private void UserTopAlbumGoToArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopAlbumRowIndex(finalParent);

            Utilities.GoToArtist(CMSLists.UserTopAlbumsLabel[row, 1].Text);
        }

        private void UserTopAlbumBackupAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopAlbumRowIndex(finalParent);

            Utilities.BackupAlbum(CMSLists.UserTopAlbumsLabel[row, 0].Text, CMSLists.UserTopAlbumsLabel[row, 1].Text);
        }

        private void UserTopAlbumBackupArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopAlbumRowIndex(finalParent);

            Utilities.BackupArtist(CMSLists.UserTopAlbumsLabel[row, 1].Text);
        }

        private void CmsUserHistoryOpen(object sender, CancelEventArgs e)
        {
            // do not open if nothing is selected
            if (ltvUserHistory.SelectedItems.Count <= 0)
            {
                e.Cancel = true;
                return;
            }
        }

        private void UserHistoryGoToTrack(object sender, EventArgs e)
        {
            Utilities.GoToTrack(ltvUserHistory.SelectedItems[0].SubItems[0].Text, ltvUserHistory.SelectedItems[0].SubItems[1].Text);
        }

        private void UserHistoryGoToArtist(object sender, EventArgs e)
        {
            Utilities.GoToArtist(ltvUserHistory.SelectedItems[0].SubItems[1].Text);
        }

        private void UserHistoryGoToAlbum(object sender, EventArgs e)
        {
            Utilities.GoToAlbum(ltvUserHistory.SelectedItems[0].SubItems[2].Text, ltvUserHistory.SelectedItems[0].SubItems[1].Text);
        }

        private void UserHistoryBackupTrack(object sender, EventArgs e)
        {
            Utilities.BackupTrack(ltvUserHistory.SelectedItems[0].SubItems[0].Text, ltvUserHistory.SelectedItems[0].SubItems[1].Text);
        }

        private void UserHistoryBackupArtist(object sender, EventArgs e)
        {
            Utilities.BackupArtist(ltvUserHistory.SelectedItems[0].SubItems[1].Text);
        }

        private void UserHistoryBackupAlbum(object sender, EventArgs e)
        {
            Utilities.BackupAlbum(ltvUserHistory.SelectedItems[0].SubItems[2].Text, ltvUserHistory.SelectedItems[0].SubItems[1].Text);
        }

        private void CmsUserLOpen(object sender, CancelEventArgs e)
        {
            // do not open if nothing is in the text box
            if (string.IsNullOrEmpty(txtUserLInfo.Text.Trim()))
            {
                e.Cancel = true;
                return;
            }

            if (string.IsNullOrEmpty(txtUserLInfo.SelectedText))
            {
                mnuUserLCopy.Enabled = false;
            }
            else
            {
                mnuUserLCopy.Enabled = true;
            }
        }

        private void UserLBackupUserL(object sender, EventArgs e)
        {
            Utilities.BackupUser(txtUserLInfo.Text.Split(Conversions.ToChar(Constants.vbLf))[1]);
        }

        private void UserLSelectAll(object sender, EventArgs e)
        {
            txtUserLInfo.SelectAll();
        }

        private void UserLCopy(object sender, EventArgs e)
        {
            try
            {
                My.MyProject.Computer.Clipboard.SetText(txtUserLInfo.SelectedText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program was unable to write to the clipboard" + Constants.vbCrLf + "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmsUserLFriendsOpen(object sender, CancelEventArgs e)
        {
            // do not open if nothing is selected
            if (ltvUserLFriends.SelectedItems.Count <= 0)
            {
                e.Cancel = true;
                return;
            }
        }

        private void UserLFriendGoToUserL(object sender, EventArgs e)
        {
            txtUserL.Text = ltvUserLFriends.SelectedItems[0].SubItems[0].Text;
            tabControl.SelectedIndex = 6;
            btnUserLSet.PerformClick();
        }

        private void UserLFriendOpenLink(object sender, EventArgs e)
        {
            Process.Start(ltvUserLFriends.SelectedItems[0].SubItems[2].Text);
        }

        private void UserLFriendBackupUserL(object sender, EventArgs e)
        {
            Utilities.BackupUser(ltvUserLFriends.SelectedItems[0].SubItems[0].Text);
        }

        private void UserLFriendCopyUserLname(object sender, EventArgs e)
        {
            try
            {
                My.MyProject.Computer.Clipboard.SetText(ltvUserLFriends.SelectedItems[0].SubItems[0].Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program was unable to write to the clipboard" + Constants.vbCrLf + "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmsUserLLovedTracksOpen(object sender, CancelEventArgs e)
        {
            // do not open if nothing is selected
            if (ltvUserLLovedTracks.SelectedItems.Count <= 0)
            {
                e.Cancel = true;
                return;
            }
        }

        private void UserLLovedTrackGoToTrack(object sender, EventArgs e)
        {
            Utilities.GoToTrack(ltvUserLLovedTracks.SelectedItems[0].SubItems[0].Text, ltvUserLLovedTracks.SelectedItems[0].SubItems[1].Text);
        }

        private void UserLLovedTrackGoToArtist(object sender, EventArgs e)
        {
            Utilities.GoToArtist(ltvUserLLovedTracks.SelectedItems[0].SubItems[1].Text);
        }

        private void UserLLovedTrackBackupTrack(object sender, EventArgs e)
        {
            Utilities.BackupTrack(ltvUserLLovedTracks.SelectedItems[0].SubItems[0].Text, ltvUserLLovedTracks.SelectedItems[0].SubItems[1].Text);
        }

        private void UserLLovedTrackBackupArtist(object sender, EventArgs e)
        {
            Utilities.BackupArtist(ltvUserLLovedTracks.SelectedItems[0].SubItems[1].Text);
        }

        private void CmsUserLRecentOpen(object sender, CancelEventArgs e)
        {
            byte row;
            try
            {
                row = CMSLists.GetUserRecentRowIndex((Label)((ContextMenuStrip)sender).SourceControl);

            }
            catch (InvalidCastException)
            {
                return;
            }

            // disable track if needed
            if (CMSLists.UserLRecentLabel[row, 0].Text.Contains("(Unavailable)") == true || CMSLists.UserLRecentLabel[row, 0].Text.Contains("ERROR: ") == true || CMSLists.UserLRecentLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserLRecentLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserLGoToRecentTrack.Enabled = false;
                mnuUserLBackupRecentTrack.Enabled = false;
            }
            else
            {
                mnuUserLGoToRecentTrack.Enabled = true;
                mnuUserLBackupRecentTrack.Enabled = true;
            }

            // disable artist if needed
            if (CMSLists.UserLRecentLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserLRecentLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserLGoToRecentArtist.Enabled = false;
                mnuUserLBackupRecentArtist.Enabled = false;
            }
            else
            {
                mnuUserLGoToRecentArtist.Enabled = true;
                mnuUserLBackupRecentArtist.Enabled = true;
            }

            // disable album if needed
            if (CMSLists.UserLRecentLabel[row, 2].Text.Contains("(Unavailable)") == true || CMSLists.UserLRecentLabel[row, 2].Text.Contains("ERROR: ") == true || CMSLists.UserLRecentLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserLRecentLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserLGoToRecentAlbum.Enabled = false;
                mnuUserLBackupRecentAlbum.Enabled = false;
            }
            else
            {
                mnuUserLGoToRecentAlbum.Enabled = true;
                mnuUserLBackupRecentAlbum.Enabled = true;
            }
        }

        private void UserLRecentGoToTrack(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserRecentRowIndex(finalParent);

            Utilities.GoToTrack(CMSLists.UserLRecentLabel[row, 0].Text, CMSLists.UserLRecentLabel[row, 1].Text);
        }

        private void UserLRecentGoToArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserRecentRowIndex(finalParent);

            Utilities.GoToArtist(CMSLists.UserLRecentLabel[row, 1].Text);
        }

        private void UserLRecentGoToAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserRecentRowIndex(finalParent);

            Utilities.GoToAlbum(CMSLists.UserLRecentLabel[row, 2].Text, CMSLists.UserLRecentLabel[row, 1].Text);
        }

        private void UserLRecentBackupTrack(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserRecentRowIndex(finalParent);

            Utilities.BackupTrack(CMSLists.UserLRecentLabel[row, 0].Text, CMSLists.UserLRecentLabel[row, 1].Text);
        }

        private void UserLRecentBackupArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserRecentRowIndex(finalParent);

            Utilities.BackupArtist(CMSLists.UserLRecentLabel[row, 1].Text);
        }

        private void UserLRecentBackupAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserRecentRowIndex(finalParent);

            Utilities.BackupAlbum(CMSLists.UserLRecentLabel[row, 2].Text, CMSLists.UserLRecentLabel[row, 1].Text);
        }

        private void CmsUserLTopTrackOpen(object sender, CancelEventArgs e)
        {
            byte row = CMSLists.GetUserTopTrackRowIndex((Label)((ContextMenuStrip)sender).SourceControl);

            // disable track if needed
            if (CMSLists.UserLTopTracksLabel[row, 0].Text.Contains("(Unavailable)") == true || CMSLists.UserLTopTracksLabel[row, 0].Text.Contains("ERROR: ") == true || CMSLists.UserLTopTracksLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserLTopTracksLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserLTopTrackGoToTrack.Enabled = false;
                mnuUserLTopTrackBackupTrack.Enabled = false;
            }
            else
            {
                mnuUserLTopTrackGoToTrack.Enabled = true;
                mnuUserLTopTrackBackupTrack.Enabled = true;
            }

            // disable artist if needed
            if (CMSLists.UserLTopTracksLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserLTopTracksLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserLTopTrackGoToTrack.Enabled = false;
                mnuUserLTopTrackBackupTrack.Enabled = false;
            }
            else
            {
                mnuUserLTopTrackGoToTrack.Enabled = true;
                mnuUserLTopTrackBackupTrack.Enabled = true;
            }

            // disable album if needed
            if (CMSLists.UserLTopTracksLabel[row, 2].Text.Contains("(Unavailable)") == true || CMSLists.UserLTopTracksLabel[row, 2].Text.Contains("ERROR: ") == true || CMSLists.UserLTopTracksLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserLTopTracksLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserLTopTrackGoToAlbum.Enabled = false;
                mnuUserLTopTrackBackupAlbum.Enabled = false;
            }
            else
            {
                mnuUserLTopTrackGoToAlbum.Enabled = true;
                mnuUserLTopTrackBackupAlbum.Enabled = true;
            }
        }

        private void UserLTopTrackGoToTrack(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopTrackRowIndex(finalParent);

            Utilities.GoToTrack(CMSLists.UserLTopTracksLabel[row, 0].Text, CMSLists.UserLTopTracksLabel[row, 1].Text);
        }

        private void UserLTopTrackGoToArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopTrackRowIndex(finalParent);

            Utilities.GoToArtist(CMSLists.UserLTopTracksLabel[row, 1].Text);
        }

        private void UserLTopTrackGoToAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopTrackRowIndex(finalParent);

            Utilities.GoToAlbum(CMSLists.UserLTopTracksLabel[row, 2].Text, CMSLists.UserLTopTracksLabel[row, 1].Text);
        }

        private void UserLTopTrackBackupTrack(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopTrackRowIndex(finalParent);

            Utilities.BackupTrack(CMSLists.UserLTopTracksLabel[row, 0].Text, CMSLists.UserLTopTracksLabel[row, 1].Text);
        }

        private void UserLTopTrackBackupArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopTrackRowIndex(finalParent);

            Utilities.BackupArtist(CMSLists.UserLTopTracksLabel[row, 1].Text);
        }

        private void UserLTopTrackBackupAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopTrackRowIndex(finalParent);

            Utilities.BackupAlbum(CMSLists.UserLTopTracksLabel[row, 2].Text, CMSLists.UserLTopTracksLabel[row, 1].Text);
        }

        private void UserLTopArtistGoToArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopArtistRowIndex(finalParent);

            Utilities.GoToArtist(CMSLists.UserLTopArtistsLabel[row, 0].Text);
        }

        private void UserLTopArtistBackupArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopArtistRowIndex(finalParent);

            Utilities.BackupArtist(CMSLists.UserLTopArtistsLabel[row, 0].Text);
        }

        private void UserLTopAlbumGoToAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopAlbumRowIndex(finalParent);

            Utilities.GoToAlbum(CMSLists.UserLTopAlbumsLabel[row, 0].Text, CMSLists.UserLTopAlbumsLabel[row, 1].Text);
        }

        private void UserLTopAlbumGoToArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopAlbumRowIndex(finalParent);

            Utilities.GoToArtist(CMSLists.UserLTopAlbumsLabel[row, 1].Text);
        }

        private void UserLTopAlbumBackupAlbum(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopAlbumRowIndex(finalParent);

            Utilities.BackupAlbum(CMSLists.UserLTopAlbumsLabel[row, 0].Text, CMSLists.UserLTopAlbumsLabel[row, 1].Text);
        }

        private void UserLTopAlbumBackupArtist(object sender, EventArgs e)
        {
            var parent1 = ((ToolStripMenuItem)sender).GetCurrentParent();
            ContextMenuStrip parent2 = (ContextMenuStrip)parent1;
            Label finalParent = (Label)parent2.SourceControl;
            byte row = CMSLists.GetUserTopAlbumRowIndex(finalParent);

            Utilities.BackupArtist(CMSLists.UserLTopAlbumsLabel[row, 1].Text);
        }

        private void CmsUserLHistoryOpen(object sender, CancelEventArgs e)
        {
            // do not open if nothing is selected
            if (ltvUserLHistory.SelectedItems.Count <= 0)
            {
                e.Cancel = true;
                return;
            }
        }

        private void UserLHistoryGoToTrack(object sender, EventArgs e)
        {
            Utilities.GoToTrack(ltvUserLHistory.SelectedItems[0].SubItems[0].Text, ltvUserLHistory.SelectedItems[0].SubItems[1].Text);
        }

        private void UserLHistoryGoToArtist(object sender, EventArgs e)
        {
            Utilities.GoToArtist(ltvUserLHistory.SelectedItems[0].SubItems[1].Text);
        }

        private void UserLHistoryGoToAlbum(object sender, EventArgs e)
        {
            Utilities.GoToAlbum(ltvUserLHistory.SelectedItems[0].SubItems[2].Text, ltvUserLHistory.SelectedItems[0].SubItems[1].Text);
        }

        private void UserLHistoryBackupTrack(object sender, EventArgs e)
        {
            Utilities.BackupTrack(ltvUserLHistory.SelectedItems[0].SubItems[0].Text, ltvUserLHistory.SelectedItems[0].SubItems[1].Text);
        }

        private void UserLHistoryBackupArtist(object sender, EventArgs e)
        {
            Utilities.BackupArtist(ltvUserLHistory.SelectedItems[0].SubItems[1].Text);
        }

        private void UserLHistoryBackupAlbum(object sender, EventArgs e)
        {
            Utilities.BackupAlbum(ltvUserLHistory.SelectedItems[0].SubItems[2].Text, ltvUserLHistory.SelectedItems[0].SubItems[1].Text);
        }
        #endregion

        #region Chart UI
        private void ChartRad(object sender, EventArgs e)
        {
            if (radChartWorldwide.Checked == true)
            {
                cmbChartCountry.Enabled = false;
            }
            else
            {
                cmbChartCountry.Enabled = true;
            }
        }

        private void ChartGo(object sender, EventArgs e)
        {
            if (bgwChartUpdater.IsBusy == false)
            {
                bgwChartUpdater.RunWorkerAsync();
            }
        }

        private void ChartTrackTrackClick(Object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetChartRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && ((Label)sender).Text.Contains("(Unavailable)") == false && ((Label)sender).Text.Contains("ERROR: ") == false && CMSLists.UserRecentLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserRecentLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToTrack(CMSLists.ChartTrackLabel[row, 0].Text, CMSLists.ChartTrackLabel[row, 1].Text);
            }
        }

        private void ChartTrackArtistClick(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetChartRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && ((Label)sender).Text.Contains("(Unavailable)") == false && ((Label)sender).Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToArtist(CMSLists.ChartTrackLabel[row, 1].Text);
            }
        }

        private void ChartTrackAlbumClick(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetChartRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && ((Label)sender).Text.Contains("(Unavailable)") == false && ((Label)sender).Text.Contains("ERROR: ") == false && CMSLists.UserRecentLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserRecentLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToAlbum(CMSLists.ChartTrackLabel[row, 2].Text, CMSLists.ChartTrackLabel[row, 1].Text);
            }
        }

        private void ChartArtistClick(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetChartRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && ((Label)sender).Text.Contains("(Unavailable)") == false && ((Label)sender).Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToArtist(CMSLists.ChartArtistLabel[row, 0].Text);
            }
        }

        private void CmsUserTopArtistOpen(object sender, EventArgs e)
        {
            byte row = CMSLists.GetUserTopArtistRowIndex((Label)((ContextMenuStrip)sender).SourceControl);

            // disable artist if needed
            if (CMSLists.UserTopArtistsLabel[row, 1].Text.Contains("(Unavailable)") == true || CMSLists.UserTopArtistsLabel[row, 1].Text.Contains("ERROR: ") == true)
            {
                mnuUserTopArtistGoToArtist.Enabled = false;
                mnuUserTopArtistBackupArtist.Enabled = false;
            }
            else
            {
                mnuUserTopArtistGoToArtist.Enabled = true;
                mnuUserTopArtistBackupArtist.Enabled = true;
            }
        }
        #endregion

        #region Search UI
        private void GoSearch(object sender, EventArgs e)
        {
            if (bgwSearchUpdater.IsBusy == false)
            {
                bgwSearchUpdater.RunWorkerAsync();
            }
        }

        private void GoSearchTrack(object sender, EventArgs e)
        {
            Utilities.GoToTrack(ltvSearchTracks.SelectedItems[0].SubItems[1].Text, ltvSearchTracks.SelectedItems[0].SubItems[2].Text);
        }

        private void GoSearchArtist(object sender, EventArgs e)
        {
            Utilities.GoToArtist(ltvSearchArtists.SelectedItems[0].SubItems[1].Text);
        }

        private void GoSearchAlbum(object sender, EventArgs e)
        {
            Utilities.GoToAlbum(ltvSearchAlbums.SelectedItems[0].SubItems[1].Text, ltvSearchAlbums.SelectedItems[0].SubItems[2].Text);
        }
        #endregion

        #region Track UI
        private void TrackGo(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTrackTitle.Text) || string.IsNullOrEmpty(txtTrackArtist.Text))
            {
                MessageBox.Show("You must enter data into both the Title and Artist fields in order to use this", "Track Lookup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (bgwTrackUpdater.IsBusy == false)
            {
                bgwTrackUpdater.RunWorkerAsync();
            }
        }

        // stop user from breaking crap by spamming the button
        private bool _TrackLove_working = false;

        private void TrackLove(object sender, EventArgs e)
        {
            // stop if no track is loaded
            if (string.IsNullOrEmpty(Utilities.tracklookup[0]))
            {
                return;
            }
            if (_TrackLove_working == false)
            {
                // if track not loved
                if (btnTrackLove.Text == "Love")
                {
                    // set working flag to true
                    _TrackLove_working = true;

                    // call api
                    string lovedresponse = Utilities.CallAPIAuth("track.love", "track=" + Utilities.tracklookup[0], "artist=" + Utilities.tracklookup[1]);

                    // determine if love was successful or not
                    if (lovedresponse.Contains("ok") == true)
                    {
                        btnTrackLove.Text = "Unlove";
                    }
                    else
                    {
                        btnTrackLove.Text = "Love";
                    }

                    // set working flag back to false
                    _TrackLove_working = false;
                }
                else    // if track is loved already
                {
                    // set working flag to true
                    _TrackLove_working = true;

                    // call api
                    string lovedresponse = Utilities.CallAPIAuth("track.unlove", "track=" + Utilities.tracklookup[0], "artist=" + Utilities.tracklookup[1]);

                    // determine if love was successful or not
                    if (lovedresponse.Contains("ok") == true)
                    {
                        btnTrackLove.Text = "Love";
                    }
                    else
                    {
                        btnTrackLove.Text = "Unlove";
                    }

                    // set working flag back to false
                    _TrackLove_working = false;
                }
            }
        }

        private void TrackAddTag(object sender, EventArgs e)
        {
            // stop if no track is loaded
            if (string.IsNullOrEmpty(Utilities.tracklookup[0]))
            {
                return;
            }

            // get input
            string tags = Interaction.InputBox("Please enter tag(s), multiple can be added by separating them with commas (10 max).", "Enter Tags");

            // halt if no input
            if (string.IsNullOrEmpty(tags))
            {
                return;
            }

            // remove spaces after commas
            if (tags.Contains(",") == true)
            {
                ushort commaindex = 1;
                for (byte count = 1, loopTo = (byte)Utilities.StrCount(tags, ","); count <= loopTo; count++)
                {
                    // find comma index
                    commaindex = (ushort)Strings.InStr(commaindex + 1, tags, ",");

                    // get rid of spaces
                    if (tags[commaindex] == ' ')
                    {
                        tags = tags.Remove(commaindex, 1);
                    }
                }
            }

            // call api
            string tagresponse = Utilities.CallAPIAuth("track.addTags", "track=" + Utilities.tracklookup[0], "artist=" + Utilities.tracklookup[1], "tags=" + tags);

            // check for success
            if (tagresponse.Contains("ok") == true)
            {
                btnTrackGo.PerformClick();
            }
            else
            {
                MessageBox.Show("Error while attempting to add tags. Please make sure you are using commas to separate tags and you are not using any special characters.", "Add Tags", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TrackRemoveTag(object sender, EventArgs e)
        {
            // stop if no track is loaded or no tag selected
            if (lstTrackUserTags.SelectedIndex == -1 || string.IsNullOrEmpty(Utilities.tracklookup[0]))
            {
                return;
            }

            // call api
            string tagresponse = Utilities.CallAPIAuth("track.removeTag", "track=" + Utilities.tracklookup[0], "artist=" + Utilities.tracklookup[1], "tag=" + lstTrackUserTags.SelectedItem.ToString());

            // check for success
            if (tagresponse.Contains("ok") == true)
            {
                lstTrackUserTags.Items.RemoveAt(lstTrackUserTags.SelectedIndex); // remove from list box
            }
            else
            {
                MessageBox.Show("Error while attempting to remove tag. Please try again later.", "Remove Tag", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TrackGoArtist(object sender, EventArgs e)
        {
            Utilities.GoToArtist(Utilities.tracklookup[1]);
        }

        private void TrackGoAlbum(object sender, EventArgs e)
        {
            Utilities.GoToAlbum(Utilities.tracklookup[2], Utilities.tracklookup[1]);
        }

        private void TrackAdvancedSearch(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmTrackAdvanced.Show();
            My.MyProject.Forms.frmTrackAdvanced.Activate();
        }

        private void TrackSimilarClicked(object sender, EventArgs e)
        {
            txtTrackTitle.Text = ltvTrackSimilar.SelectedItems[0].Text;              // set title
            txtTrackArtist.Text = ltvTrackSimilar.SelectedItems[0].SubItems[1].Text; // set artist
            btnTrackGo.PerformClick();
        }

        private void TrackLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
        #endregion

        #region Artist UI
        private void ArtistAddTag(object sender, EventArgs e)
        {
            // stop if no track is loaded
            if (string.IsNullOrEmpty(Utilities.artistlookup))
            {
                return;
            }

            // get input
            string tags = Interaction.InputBox("Please enter tag(s), multiple can be added by separating them with commas (10 max).", "Enter Tags");

            // halt if no input
            if (string.IsNullOrEmpty(tags))
            {
                return;
            }

            // remove spaces after commas
            if (tags.Contains(",") == true)
            {
                ushort commaindex = 1;
                for (byte count = 1, loopTo = (byte)Utilities.StrCount(tags, ","); count <= loopTo; count++)
                {
                    // find comma index
                    commaindex = (ushort)Strings.InStr(commaindex + 1, tags, ",");

                    // get rid of spaces
                    if (tags[commaindex] == ' ')
                    {
                        tags = tags.Remove(commaindex, 1);
                    }
                }
            }

            // call api
            string tagresponse = Utilities.CallAPIAuth("artist.addTags", "artist=" + Utilities.artistlookup, "tags=" + tags);

            // check for success
            if (tagresponse.Contains("ok") == true)
            {
                btnArtistGo.PerformClick();
            }
            else
            {
                MessageBox.Show("Error while attempting to add tags. Please make sure you are using commas to separate tags and you are not using any special characters.", "Add Tags", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ArtistRemoveTag(object sender, EventArgs e)
        {
            // stop if no track is loaded or no tag is selected
            if (lstArtistUserTags.SelectedIndex == -1 || string.IsNullOrEmpty(Utilities.artistlookup))
            {
                return;
            }

            // call api
            string tagresponse = Utilities.CallAPIAuth("artist.removeTag", "artist=" + Utilities.artistlookup, "tag=" + lstArtistUserTags.SelectedItem.ToString());

            // check for success
            if (tagresponse.Contains("ok") == true)
            {
                lstArtistUserTags.Items.RemoveAt(lstArtistUserTags.SelectedIndex); // remove from list box
            }
            else
            {
                MessageBox.Show("Error while attempting to remove tag. Please try again later.", "Remove Tag", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ArtistGo(object sender, EventArgs e)
        {
            if (bgwArtistUpdater.IsBusy == false)
            {
                bgwArtistUpdater.RunWorkerAsync();
            }
        }

        private void ArtistSimilarClicked(object sender, EventArgs e)
        {
            txtArtistName.Text = ltvArtistSimilar.SelectedItems[0].Text;              // set title
            btnArtistGo.PerformClick();
        }

        private void ArtistAdvancedSearch(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmArtistAdvanced.Show();
            My.MyProject.Forms.frmArtistAdvanced.Activate();
        }

        private void ArtistLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void ArtistTopTrackClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ((Label)sender).Text.Contains("(Unavailable)") == false && ((Label)sender).Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToTrack(((Label)sender).Text, txtArtistInfo.Text.Split(Conversions.ToChar(Constants.vbLf))[1]);
            }
        }

        private void ArtistTopAlbumClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ((Label)sender).Text.Contains("(Unavailable)") == false && ((Label)sender).Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToAlbum(((Label)sender).Text, txtArtistInfo.Text.Split(Conversions.ToChar(Constants.vbLf))[1]);
            }
        }
        #endregion

        #region Album UI
        private void AlbumGo(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAlbumTitle.Text) || string.IsNullOrEmpty(txtAlbumArtist.Text))
            {
                MessageBox.Show("You must enter data into both the Title and Artist fields in order to use this", "album Lookup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (bgwAlbumUpdater.IsBusy == false)
            {
                bgwAlbumUpdater.RunWorkerAsync();
            }
        }

        private void AlbumAddTag(object sender, EventArgs e)
        {
            // stop if no album is loaded
            if (string.IsNullOrEmpty(Utilities.albumlookup[0]))
            {
                return;
            }

            // get input
            string tags = Interaction.InputBox("Please enter tag(s), multiple can be added by separating them with commas (10 max).", "Enter Tags");

            // halt if no input
            if (string.IsNullOrEmpty(tags))
            {
                return;
            }

            // remove spaces after commas
            if (tags.Contains(",") == true)
            {
                ushort commaindex = 1;
                for (byte count = 1, loopTo = (byte)Utilities.StrCount(tags, ","); count <= loopTo; count++)
                {
                    // find comma index
                    commaindex = (ushort)Strings.InStr(commaindex + 1, tags, ",");

                    // get rid of spaces
                    if (tags[commaindex] == ' ')
                    {
                        tags = tags.Remove(commaindex, 1);
                    }
                }
            }

            // call api
            string tagresponse = Utilities.CallAPIAuth("album.addTags", "album=" + Utilities.albumlookup[0], "artist=" + Utilities.albumlookup[1], "tags=" + tags);

            // check for success
            if (tagresponse.Contains("ok") == true)
            {
                btnAlbumGo.PerformClick();
            }
            else
            {
                MessageBox.Show("Error while attempting to add tags. Please make sure you are using commas to separate tags and you are not using any special characters.", "Add Tags", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AlbumRemoveTag(object sender, EventArgs e)
        {
            // stop if no album is loaded or no tag selected
            if (lstAlbumUserTags.SelectedIndex == -1 || string.IsNullOrEmpty(Utilities.albumlookup[0]))
            {
                return;
            }

            // call api
            string tagresponse = Utilities.CallAPIAuth("album.removeTag", "album=" + Utilities.albumlookup[0], "artist=" + Utilities.albumlookup[1], "tag=" + lstAlbumUserTags.SelectedItem.ToString());

            // check for success
            if (tagresponse.Contains("ok") == true)
            {
                lstAlbumUserTags.Items.RemoveAt(lstAlbumUserTags.SelectedIndex); // remove from list box
            }
            else
            {
                MessageBox.Show("Error while attempting to remove tag. Please try again later.", "Remove Tag", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AlbumAdvancedClicked(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmAlbumAdvanced.Show();
            My.MyProject.Forms.frmAlbumAdvanced.Activate();
        }

        private void AlbumTrackClicked(object sender, EventArgs e)
        {
            Utilities.GoToTrack(ltvAlbumTrackList.SelectedItems[0].SubItems[1].Text, Utilities.albumlookup[1]);
        }

        private void AlbumGoArtist(object sender, EventArgs e)
        {
            Utilities.GoToArtist(Utilities.albumlookup[1]);
        }

        private void AlbumLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
        #endregion

        #region User UI
        // set user
        private void SetUser(object sender, EventArgs e)
        {
            // set user var
            string userinput = txtUser.Text.Trim();

            // validation process
            lblUserStatus.Text = "Validating...";
            // attempt to get user info to check if user exists
            if (Utilities.CallAPI("user.getInfo", userinput).Contains("ERROR:") == false)
            {
                // if validation succeeded
                My.MySettingsProperty.Settings.User = userinput;                          // set user variable
                lblUserStatus.Text = "Welcome, " + My.MySettingsProperty.Settings.User;   // update user status label
                lblUser.Text = "Current User: " + My.MySettingsProperty.Settings.User;    // update current user label
                Text = "Audiograph - " + My.MySettingsProperty.Settings.User;          // update form name

                // get rid of previous session key and authenticated state if applicable
                My.MySettingsProperty.Settings.SessionKey = string.Empty;
                My.MySettingsProperty.Settings.Save();
                AuthenticatedUI(false);

                // update all
                UpdateAll();

                // enable authentication button
                btnUserAuthenticate.Enabled = true;
            }
            else
            {
                // if validation failed
                lblUserStatus.Text = "User Cannot Be Found";
                txtUser.SelectAll();
            }
        }

        // keep user pic square when splitter is moved
        private void PicUserScaling(object sender, EventArgs e)
        {
            picUser.Height = picUser.Width;
        }
        // stop this from running on startup
        private bool _UserLovedTracksPageChanged_first = true;

        // change user loved tracks page
        private void UserLovedTracksPageChanged(object sender, EventArgs e)
        {
            if (Utilities.stoploadexecution == false && _UserLovedTracksPageChanged_first == false && bgwUserLovedUpdater.IsBusy == false)
            {
                bgwUserLovedUpdater.RunWorkerAsync();
            }
            _UserLovedTracksPageChanged_first = false;
        }
        // stop this from running on startup
        private bool _UserHistoryPageChanged_first = true;

        private void UserHistoryPageChanged(object sender, EventArgs e)
        {
            if (Utilities.stoploadexecution == false && _UserHistoryPageChanged_first == false && bgwUserHistoryUpdater.IsBusy == false)
            {
                bgwUserHistoryUpdater.RunWorkerAsync();
            }
            _UserHistoryPageChanged_first = false;
        }

        private void UserInfoTabScaling(object sender, EventArgs e)
        {
            // user friends
            ltvUserFriends.Width = pgUserFriends.Width - 6;
            ltvUserFriends.Height = pgUserFriends.Height - 35;

            // user loved tracks
            ltvUserLovedTracks.Width = pgUserLovedTracks.Width - 6;
            ltvUserLovedTracks.Height = pgUserLovedTracks.Height - 35;
        }

        private void UserLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void UserPictureClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && picUser.ImageLocation.Contains("http") == true)
            {
                Process.Start(picUser.ImageLocation);
            }
        }

        private void UserLovedSongClick(object sender, EventArgs e)
        {
            Utilities.GoToTrack(ltvUserLovedTracks.SelectedItems[0].SubItems[0].Text, ltvUserLovedTracks.SelectedItems[0].SubItems[1].Text);
        }

        private void UserFriendClick(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 6;
            txtUserL.Text = ltvUserFriends.SelectedItems[0].Text;
            btnUserLSet.PerformClick();
        }

        private void UserChartRad(object sender, EventArgs e)
        {
            if (radUserAllTime.Checked == true)
            {
                dtpUserFrom.Enabled = false;
                dtpUserTo.Enabled = false;
                lblUserFrom.Enabled = false;
                lblUserTo.Enabled = false;
            }
            else
            {
                dtpUserFrom.Enabled = true;
                dtpUserTo.Enabled = true;
                lblUserFrom.Enabled = true;
                lblUserTo.Enabled = true;
            }
        }

        private void UserChartGo(object sender, EventArgs e)
        {
            // notify user and do nothing if there is no user set
            if (string.IsNullOrEmpty(My.MySettingsProperty.Settings.User))
            {
                MessageBox.Show("You must set user before attempting to use this", "User Charts", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                nudUserHistoryPage.Value = nudUserHistoryPage.Minimum;
                if (Utilities.stoploadexecution == false && bgwUserChartUpdater.IsBusy == false)
                {
                    bgwUserChartUpdater.RunWorkerAsync();
                    if (bgwUserHistoryUpdater.IsBusy == false)
                    {
                        bgwUserHistoryUpdater.RunWorkerAsync();
                    }
                }
            }
        }

        private void UserAuthenticate(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmAuthentication.Show();
        }

        private void UserRecentTrackClick(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserRecentRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserRecentLabel[row, 0].Text.Contains("(Unavailable)") == false && CMSLists.UserRecentLabel[row, 0].Text.Contains("ERROR: ") == false && CMSLists.UserRecentLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserRecentLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToTrack(CMSLists.UserRecentLabel[row, 0].Text, CMSLists.UserRecentLabel[row, 1].Text);
            }
        }

        private void UserRecentArtistClick(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserRecentRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && ((Label)sender).Text.Contains("(Unavailable)") == false && ((Label)sender).Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToArtist(CMSLists.UserRecentLabel[row, 1].Text);
            }
        }

        private void UserRecentAlbumClick(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserRecentRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserRecentLabel[row, 2].Text.Contains("(Unavailable)") == false && CMSLists.UserRecentLabel[row, 2].Text.Contains("ERROR: ") == false && CMSLists.UserRecentLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserRecentLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToAlbum(CMSLists.UserRecentLabel[row, 2].Text, CMSLists.UserRecentLabel[row, 1].Text);
            }
        }

        private void UserTopTrackTrackClicked(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserTopTrackRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserTopTracksLabel[row, 0].Text.Contains("(Unavailable)") == false && CMSLists.UserTopTracksLabel[row, 0].Text.Contains("ERROR: ") == false && CMSLists.UserTopTracksLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserTopTracksLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToTrack(CMSLists.UserTopTracksLabel[row, 0].Text, CMSLists.UserTopTracksLabel[row, 1].Text);
            }
        }

        private void UserTopTrackArtistClicked(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserTopTrackRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserTopTracksLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserTopTracksLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToArtist(CMSLists.UserTopTracksLabel[row, 1].Text);
            }
        }

        private void UserTopTrackAlbumClicked(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserTopTrackRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserTopTracksLabel[row, 2].Text.Contains("(Unavailable)") == false && CMSLists.UserTopTracksLabel[row, 2].Text.Contains("ERROR: ") == false && CMSLists.UserTopTracksLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserTopTracksLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToAlbum(CMSLists.UserTopTracksLabel[row, 2].Text, CMSLists.UserTopTracksLabel[row, 1].Text);
            }
        }

        private void UserTopArtistClicked(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserTopArtistRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserTopArtistsLabel[row, 0].Text.Contains("(Unavailable)") == false && CMSLists.UserTopArtistsLabel[row, 0].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToArtist(CMSLists.UserTopArtistsLabel[row, 0].Text);
            }
        }

        private void UserTopAlbumAlbumClicked(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserTopAlbumRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserTopAlbumsLabel[row, 0].Text.Contains("(Unavailable)") == false && CMSLists.UserTopAlbumsLabel[row, 0].Text.Contains("ERROR: ") == false && CMSLists.UserTopAlbumsLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserTopAlbumsLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToAlbum(CMSLists.UserTopAlbumsLabel[row, 0].Text, CMSLists.UserTopAlbumsLabel[row, 1].Text);
            }
        }

        private void UserTopAlbumArtistClicked(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserTopAlbumRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserTopAlbumsLabel[row, 0].Text.Contains("(Unavailable)") == false && CMSLists.UserTopAlbumsLabel[row, 0].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToArtist(CMSLists.UserTopAlbumsLabel[row, 1].Text);
            }
        }

        private void UserHistoryClicked(object sender, EventArgs e)
        {
            Utilities.GoToTrack(ltvUserHistory.SelectedItems[0].SubItems[0].Text, ltvUserHistory.SelectedItems[0].SubItems[1].Text);
        }
        #endregion

        #region UserL UI
        // set user
        private void SetUserL(object sender, EventArgs e)
        {
            // set user var
            string userinput = txtUserL.Text.Trim();

            // validation process
            lblUserLStatus.Text = "Validating...";
            // attempt to get user info to check if user exists
            if (Utilities.CallAPI("user.getInfo", userinput).Contains("ERROR:") == false)
            {
                // if validation succeeded
                Utilities.userlookup = userinput;                          // set user variable
                lblUserLStatus.Text = "Lookup: " + userinput;    // update user status label

                // update userlookup tab
                if (bgwUserLookupUpdater.IsBusy == false)
                {
                    bgwUserLookupUpdater.RunWorkerAsync();
                }
            }
            else
            {
                // if validation failed
                lblUserLStatus.Text = "User Cannot Be Found";
                txtUser.SelectAll();
            }
        }

        private void PicUserLScaling(object sender, EventArgs e)
        {
            picUserL.Height = picUserL.Width;
        }

        // change user loved tracks page
        private void UserLLovedTracksPageChanged(object sender, EventArgs e)
        {
            if (Utilities.stoploadexecution == false && bgwUserLLovedUpdater.IsBusy == false)
            {
                bgwUserLLovedUpdater.RunWorkerAsync();
            }
        }
        // stop this from running on startup
        private bool _UserLHistoryPageChanged_first = true;

        private void UserLHistoryPageChanged(object sender, EventArgs e)
        {
            if (Utilities.stoploadexecution == false && _UserLHistoryPageChanged_first == false && bgwUserLHistoryUpdater.IsBusy == false)
            {
                bgwUserLHistoryUpdater.RunWorkerAsync();
            }
            _UserLHistoryPageChanged_first = false;
        }

        private void UserLInfoTabScaling(object sender, EventArgs e)
        {
            // user loved tracks
            ltvUserLLovedTracks.Width = pgUserLLovedTracks.Width - 6;
            ltvUserLLovedTracks.Height = pgUserLLovedTracks.Height - 35;

            // user friends
            ltvUserLFriends.Width = pgUserLFriends.Width - 6;
            ltvUserLFriends.Height = pgUserLFriends.Height - 35;
        }

        // userl profile link clicked
        private void UserLLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void UserLPictureClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && picUserL.ImageLocation.Contains("http") == true)
            {
                Process.Start(picUserL.ImageLocation);
            }
        }

        private void UserLLovedSongClick(object sender, EventArgs e)
        {
            Utilities.GoToTrack(ltvUserLLovedTracks.SelectedItems[0].SubItems[0].Text, ltvUserLLovedTracks.SelectedItems[0].SubItems[1].Text);
        }

        private void UserLFriendClick(object sender, EventArgs e)
        {
            txtUserL.Text = ltvUserLFriends.SelectedItems[0].Text;
            btnUserLSet.PerformClick();
        }

        private void UserLChartRad(object sender, EventArgs e)
        {
            if (radUserLAllTime.Checked == true)
            {
                dtpUserLFrom.Enabled = false;
                dtpUserLTo.Enabled = false;
                lblUserLFrom.Enabled = false;
                lblUserLTo.Enabled = false;
            }
            else
            {
                dtpUserLFrom.Enabled = true;
                dtpUserLTo.Enabled = true;
                lblUserLFrom.Enabled = true;
                lblUserLTo.Enabled = true;
            }
        }

        private void UserLChartGo(object sender, EventArgs e)
        {
            // notify user and do nothing if there is no user set
            if (string.IsNullOrEmpty(Utilities.userlookup))
            {
                MessageBox.Show("You must set user before attempting to use this", "User Lookup Charts", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (bgwUserLChartUpdater.IsBusy == false)
            {
                bgwUserLChartUpdater.RunWorkerAsync();
            }
        }

        private void UserLRecentTrackClick(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserRecentRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserLRecentLabel[row, 0].Text.Contains("(Unavailable)") == false && CMSLists.UserLRecentLabel[row, 0].Text.Contains("ERROR: ") == false && CMSLists.UserLRecentLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserLRecentLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToTrack(CMSLists.UserLRecentLabel[row, 0].Text, CMSLists.UserLRecentLabel[row, 1].Text);
            }
        }

        private void UserLRecentArtistClick(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserRecentRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && ((Label)sender).Text.Contains("(Unavailable)") == false && ((Label)sender).Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToArtist(CMSLists.UserLRecentLabel[row, 1].Text);
            }
        }

        private void UserLRecentAlbumClick(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserRecentRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserLRecentLabel[row, 2].Text.Contains("(Unavailable)") == false && CMSLists.UserLRecentLabel[row, 2].Text.Contains("ERROR: ") == false && CMSLists.UserLRecentLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserLRecentLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToAlbum(CMSLists.UserLRecentLabel[row, 2].Text, CMSLists.UserLRecentLabel[row, 1].Text);
            }
        }

        private void UserLTopTrackTrackClicked(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserTopTrackRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserLTopTracksLabel[row, 0].Text.Contains("(Unavailable)") == false && CMSLists.UserLTopTracksLabel[row, 0].Text.Contains("ERROR: ") == false && CMSLists.UserLTopTracksLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserLTopTracksLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToTrack(CMSLists.UserLTopTracksLabel[row, 0].Text, CMSLists.UserLTopTracksLabel[row, 1].Text);
            }
        }

        private void UserLTopTrackArtistClicked(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserTopTrackRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserLTopTracksLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserLTopTracksLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToArtist(CMSLists.UserLTopTracksLabel[row, 1].Text);
            }
        }

        private void UserLTopTrackAlbumClicked(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserTopTrackRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserLTopTracksLabel[row, 2].Text.Contains("(Unavailable)") == false && CMSLists.UserLTopTracksLabel[row, 2].Text.Contains("ERROR: ") == false && CMSLists.UserLTopTracksLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserLTopTracksLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToAlbum(CMSLists.UserLTopTracksLabel[row, 2].Text, CMSLists.UserLTopTracksLabel[row, 1].Text);
            }
        }

        private void UserLTopArtistClicked(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserTopArtistRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserLTopArtistsLabel[row, 0].Text.Contains("(Unavailable)") == false && CMSLists.UserLTopArtistsLabel[row, 0].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToArtist(CMSLists.UserLTopArtistsLabel[row, 0].Text);
            }
        }

        private void UserLTopAlbumAlbumClicked(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserTopAlbumRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserLTopAlbumsLabel[row, 0].Text.Contains("(Unavailable)") == false && CMSLists.UserLTopAlbumsLabel[row, 0].Text.Contains("ERROR: ") == false && CMSLists.UserLTopAlbumsLabel[row, 1].Text.Contains("(Unavailable)") == false && CMSLists.UserLTopAlbumsLabel[row, 1].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToAlbum(CMSLists.UserLTopAlbumsLabel[row, 0].Text, CMSLists.UserLTopAlbumsLabel[row, 1].Text);
            }
        }

        private void UserLTopAlbumArtistClicked(object sender, MouseEventArgs e)
        {
            byte row = CMSLists.GetUserTopAlbumRowIndex((Label)sender);

            if (e.Button == MouseButtons.Left && CMSLists.UserLTopAlbumsLabel[row, 0].Text.Contains("(Unavailable)") == false && CMSLists.UserLTopAlbumsLabel[row, 0].Text.Contains("ERROR: ") == false)
            {
                Utilities.GoToArtist(CMSLists.UserLTopAlbumsLabel[row, 1].Text);
            }
        }

        private void UserLHistoryClicked(object sender, EventArgs e)
        {
            Utilities.GoToTrack(ltvUserLHistory.SelectedItems[0].SubItems[0].Text, ltvUserLHistory.SelectedItems[0].SubItems[1].Text);
        }
        #endregion

        #region Media UI

        private void AddQueue(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmAddToQueue.Show();
            My.MyProject.Forms.frmAddToQueue.Activate();
        }

        private void NextQueue(object sender, EventArgs e)
        {
            QueuePlay(0);
        }

        public void QueuePlay(int item)
        {
            // check that there are things to play
            if (ltvMediaQueue.Items.Count < 1)
            {
                return;
            }

            Utilities.newsong = true;
            MediaPlayer.URL = ltvMediaQueue.Items[item].SubItems[1].Text;
            QueueRemove(new[] { item });
            // check if player is playing, if its not then turn on the timer
            if (MediaPlayer.playState != WMPPlayState.wmppsPlaying)
            {
                tmrMediaPlayer.Enabled = true;
            }
        }

        public void QueueRemove(int[] index)
        {
            Array.Sort(index);           // sort ascending

            // if shuffle is on add item to list
            for (int count = 0, loopTo = index.Length - 1; count <= loopTo; count++)
            {
                byte originalindex;
                if (chkMediaShuffle.Checked == true)
                {
                    // filter out the number before adding to queue
                    originalindex = (byte)(Strings.InStr(ltvMediaQueue.Items[count].Text, " ") - 1);
                    Utilities.queueremoved.Add(new[] { ltvMediaQueue.Items[count].Text.Substring(originalindex), ltvMediaQueue.Items[count].SubItems[1].Text });
                }
            }

            for (int count = 0, loopTo1 = index.Length - 1; count <= loopTo1; count++)
                ltvMediaQueue.Items[index[count] - count].Remove();  // remove at index minus count to adjust for deletion
            QueueRecount();
        }

        // re-orders the numbers of the queue items
        public void QueueRecount()
        {
            // check that there is items in the queue, exit if none
            if (ltvMediaQueue.Items.Count == 0)
            {
                return;
            }

            for (int count = 0, loopTo = ltvMediaQueue.Items.Count - 1; count <= loopTo; count++)
            {
                byte firstpos = (byte)Strings.InStr(ltvMediaQueue.Items[count].Text, " ");                        // get pos of first space
                string newstring = ltvMediaQueue.Items[count].Text.Remove(0, firstpos);             // remove old number
                ltvMediaQueue.Items[count].Text = newstring.Insert(0, (count + 1).ToString("N0") + " ");   // add new number and space
            }
        }

        public void QueueRecountImg()
        {
            // check that there is items in the queue, exit if none
            if (ltvMediaQueue.Items.Count == 0)
            {
                return;
            }

            for (int count = 0, loopTo = ltvMediaQueue.Items.Count - 1; count <= loopTo; count++)
            {
                byte firstpos = (byte)Strings.InStr(ltvMediaQueue.Items[count].Text, " ");                        // get pos of first space
                string newstring = ltvMediaQueue.Items[count].Text.Remove(0, firstpos);             // remove old number
                ltvMediaQueue.Items[count].Text = newstring.Insert(0, (count + 1).ToString("N0") + " ");   // add new number and space
                                                                                                           // set image
                if (ltvMediaQueue.Items[count].Text.ToLower().Contains(".mp3") == true || ltvMediaQueue.Items[count].Text.ToLower().Contains(".aac") == true || ltvMediaQueue.Items[count].Text.ToLower().Contains(".flac") == true || ltvMediaQueue.Items[count].Text.ToLower().Contains(".wav") == true || ltvMediaQueue.Items[count].Text.ToLower().Contains(".wma") == true || ltvMediaQueue.Items[count].Text.ToLower().Contains(".m4a") == true || ltvMediaQueue.Items[count].Text.ToLower().Contains(".mid") == true)    // audio types
                {
                    ltvMediaQueue.Items[count].ImageIndex = 0;
                }
                else if (ltvMediaQueue.Items[count].Text.ToLower().Contains(".mp4") == true || ltvMediaQueue.Items[count].Text.ToLower().Contains(".mov") == true || ltvMediaQueue.Items[count].Text.ToLower().Contains(".mpeg") == true || ltvMediaQueue.Items[count].Text.ToLower().Contains(".mpg") == true || ltvMediaQueue.Items[count].Text.ToLower().Contains(".avi") == true || ltvMediaQueue.Items[count].Text.ToLower().Contains(".wmv") == true) // video types
                {
                    ltvMediaQueue.Items[count].ImageIndex = 1;
                }
                else
                {
                    // anything else
                    ltvMediaQueue.Items[count].ImageIndex = 2;
                }
            }
        }

        private void DoubleClickQueue(object sender, EventArgs e)
        {
            QueuePlay(ltvMediaQueue.SelectedItems[0].Index);
        }

        private void RemoveQueueBtn(object sender, EventArgs e)
        {
            // check that something is selected to remove, if not then exit sub
            if (ltvMediaQueue.SelectedItems.Count == 0)
            {
                return;
            }

            var indexes = new List<int>();
            // get indexes of selected items
            foreach (ListViewItem item in ltvMediaQueue.SelectedItems)
                indexes.Add(item.Index);
            QueueRemove(indexes.ToArray());
        }
        private bool _PlayStateChange_ended = false;

        private void PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {

            // media ended
            if (e.newState == (int)WMPPlayState.wmppsMediaEnded)
            {
                _PlayStateChange_ended = true;
                // hide play button
                separator2.Visible = false;
                btnMediaPlay.Visible = false;
                tmrMediaScrobble.Enabled = false;
                Utilities.newsong = true;
                btnMediaPlay.Visible = false;
                btnMediaPlay.Image = My.Resources.Resources.play;
            }

            // repeat current
            if (_PlayStateChange_ended == true && e.newState == (int)WMPPlayState.wmppsStopped && chkMediaRepeat.Checked == true)
            {
                MediaPlayer.Ctlcontrols.play();
                _PlayStateChange_ended = false;
                Utilities.newsong = true;
            }

            // song end
            if (_PlayStateChange_ended == true && e.newState == (int)WMPPlayState.wmppsStopped && chkMediaRepeat.Checked == false)
            {
                QueuePlay(0);
                _PlayStateChange_ended = false;
                tmrMediaScrobble.Enabled = false;
                Utilities.newsong = true;
                btnMediaPlay.Visible = false;
                btnMediaPlay.Image = My.Resources.Resources.play;
            }

            // user hits stop
            if (_PlayStateChange_ended == false && e.newState == (int)WMPPlayState.wmppsStopped && chkMediaRepeat.Checked == false)
            {
                _PlayStateChange_ended = true;
                tmrMediaScrobble.Enabled = false;
                Utilities.newsong = true;
                btnMediaPlay.Visible = false;
                btnMediaPlay.Image = My.Resources.Resources.play;
            }

            // paused
            if (e.newState == (int)WMPPlayState.wmppsPaused)
            {
                tmrMediaScrobble.Enabled = false;
                btnMediaPlay.Image = My.Resources.Resources.play;
            }

            // playing
            if (e.newState == (int)WMPPlayState.wmppsPlaying)
            {
                tmrMediaPlayer.Enabled = false;
                // show play button
                separator2.Visible = true;
                btnMediaPlay.Visible = true;
                btnMediaPlay.Image = My.Resources.Resources.pause;
                // enable scrobble check timer
                tmrMediaScrobble.Enabled = true;
                // update now playing
                if (radMediaEnable.Checked == true && Utilities.SearchIndex(Utilities.GetFilename(MediaPlayer.URL)) is not null && Utilities.scrobbleindexdata.Count > 0)
                {
                    string[] data = Utilities.SearchIndex(Utilities.GetFilename(MediaPlayer.URL));

                    var th = new Thread(() =>
                        {
                            string[] results = Utilities.SearchIndex(Utilities.GetFilename(MediaPlayer.URL));

                            if (results is not null && Utilities.scrobbleindexdata.Count > 0)
                            {
                                Utilities.CallAPIAuth("track.updateNowPlaying", "track=" + results[0], "artist=" + results[1], "album=" + results[2], "duration=" + Math.Round(MediaPlayer.currentMedia.duration - MediaPlayer.Ctlcontrols.currentPosition).ToString());
                            }
                        });
                    th.Name = "Media";
                    th.Start();
                }
            }
        }

        // timer that repeatedly tells the media player to play after its stopped
        private void TimerPlay(object sender, EventArgs e)
        {
            MediaPlayer.Ctlcontrols.play();
        }

        private void MediaShuffle(object sender, EventArgs e)
        {
            var workingqueue = new List<string[]>();       // list to use for shuffle
            var shuffledqueue = new List<string[]>();      // final shuffled list
            var RNG = new Random();
            uint RNGnum;

            if (Utilities.addingqueue == true)
            {
                Utilities.addingqueue = false;
                return;
            }

            if (chkMediaShuffle.Checked == true)  // when shuffle is turned on
            {
                Utilities.originalqueue.Clear();
                for (int count = 0, loopTo = ltvMediaQueue.Items.Count - 1; count <= loopTo; count++)
                {
                    Utilities.originalqueue.Add(new[] { ltvMediaQueue.Items[count].Text, ltvMediaQueue.Items[count].SubItems[1].Text }); // save original queue
                    workingqueue.Add(new[] { ltvMediaQueue.Items[count].Text, ltvMediaQueue.Items[count].SubItems[1].Text });  // also add to working queue
                }

                // shuffle
                foreach (var item in ltvMediaQueue.Items)
                {
                    RNGnum = (uint)RNG.Next(0, workingqueue.Count);
                    shuffledqueue.Add(workingqueue[(int)RNGnum]);
                    workingqueue.RemoveAt((int)RNGnum);
                }

                // add to listview
                ltvMediaQueue.Items.Clear();
                foreach (string[] item in shuffledqueue)
                    ltvMediaQueue.Items.Add(item[0]).SubItems.Add(item[1]);

                QueueRecountImg();
            }
            else    // when shuffle is turned off
            {
                // replace listview items with original items
                ltvMediaQueue.Items.Clear();

                // remove originalqueue items that correspond with queueremoved items
                if (Utilities.queueremoved.Count > 0)
                {
                    ushort originalindex;    // stores the index of the text after the number of the removed element
                    string[] moditem;        // array storing the modified item
                    foreach (string[] item in Utilities.originalqueue.ToList())
                    {
                        originalindex = (ushort)(Strings.InStr(item[0], " ") - 1);     // get original index for substring
                        moditem = new[] { item[0].Substring(originalindex), item[1] };

                        // check if item is in queueremoved, remove it if it is
                        foreach (string[] removeditem in Utilities.queueremoved.ToList())   // has to be tolist because of another dumb vb error
                        {
                            if ((removeditem[0] ?? "") == (moditem[0] ?? ""))
                            {
                                Utilities.originalqueue.Remove(item);
                            }
                        }
                    }

                    Utilities.queueremoved.Clear();
                }

                // add to listview
                foreach (string[] item in Utilities.originalqueue)
                    ltvMediaQueue.Items.Add(item[0]).SubItems.Add(item[1]);

                QueueRecountImg();
            }
        }

        private void MediaPlayButton(object sender, EventArgs e)
        {
            if (MediaPlayer.playState == WMPPlayState.wmppsPlaying)
            {
                btnMediaPlay.Image = My.Resources.Resources.play;
                MediaPlayer.Ctlcontrols.pause();
            }
            else
            {
                btnMediaPlay.Image = My.Resources.Resources.pause;
                MediaPlayer.Ctlcontrols.play();
            }
        }

        private void ButtonVerifyTrack(object sender, EventArgs e)
        {
            // check that there is something in the boxes
            if (string.IsNullOrEmpty(txtMediaTitle.Text) || string.IsNullOrEmpty(txtMediaArtist.Text))
            {
                lblMediaScrobble.Text = "Valid data must be entered in both the Track and Artist fields!";
                return;
            }

            // verify
            string[] info = Utilities.VerifyTrack(txtMediaTitle.Text.Trim(), txtMediaArtist.Text.Trim());
            // if cannot be found
            if (info[0].Contains("ERROR: ") == true)
            {
                lblMediaScrobble.Text = "Track was unable to be verified.";
            }
            else // if was found
            {
                lblMediaScrobble.Text = "Track verified as " + info[0] + " by " + info[1] + ".";
                txtMediaTitle.Text = info[0];
                txtMediaArtist.Text = info[1];
                if (info[2].Contains("ERROR:") == false)
                {
                    txtMediaAlbum.Text = info[2];
                }
                else
                {
                    txtMediaAlbum.Text = string.Empty;
                }
            }
        }

        private void ClickScrobble(object sender, EventArgs e)
        {
            // check that there is something in the boxes
            if (string.IsNullOrEmpty(txtMediaTitle.Text.Trim()) || string.IsNullOrEmpty(txtMediaArtist.Text.Trim()))
            {
                MessageBox.Show("Valid data must be entered into the Title and Artist fields!", "Scrobble Track", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // set label
            lblMediaScrobble.Text = "Scrobbling...";

            // get time
            var workingdate = dtpMediaScrobble.Value.Date;
            if (cmbMediaTime.SelectedIndex == 0)
            {
                // am
                if (nudMediaHour.Value < 12m)
                {
                    workingdate = workingdate.AddHours((double)nudMediaHour.Value);
                }
            }
            // pm
            else if (nudMediaHour.Value < 12m)
            {
                workingdate = workingdate.AddHours((double)(nudMediaHour.Value + 12m));
            }
            else
            {
                workingdate = workingdate.AddHours(12d);
            }
            workingdate = workingdate.AddMinutes((double)nudMediaMinute.Value);

            // scrobble
            string[] response = Utilities.Scrobble(txtMediaTitle.Text.Trim(), txtMediaArtist.Text.Trim(), (uint)(Utilities.DateToUnix(workingdate) - Utilities.timezoneoffset), "User", txtMediaAlbum.Text.Trim());
            lblMediaScrobble.Text = response[4];
        }

        private void ScrobbleTimerTick(object sender, EventArgs e)
        {
            // analyze the current position of the playing media and decide if it must be scrobbled according to lfm protocol
            int totalDuration = (int)Math.Round(MediaPlayer.currentMedia.duration);
            int currentDuration = (int)Math.Round(MediaPlayer.Ctlcontrols.currentPosition);

            if (radMediaEnable.Checked == true && Utilities.newsong == true && (currentDuration > totalDuration / 2d || currentDuration > 240) && totalDuration > 30)
            {
                Utilities.newsong = false;
                string[] results = Utilities.SearchIndex(Utilities.GetFilename(MediaPlayer.URL));

                if (results is not null)
                {
                    // update now playing
                    if (Utilities.scrobbleindexdata.Count > 0)
                    {
                        Utilities.CallAPIAuth("track.updateNowPlaying", "track=" + results[0], "artist=" + results[1], "album=" + results[2], "duration=" + Math.Round(MediaPlayer.currentMedia.duration - MediaPlayer.Ctlcontrols.currentPosition).ToString());
                    }

                    // scrobble
                    Utilities.Scrobble(results[0], results[1], Utilities.GetCurrentUTC(), "Auto", results[2]);
                }
            }
        }

        private void ExpandScrobbleHistory(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmScrobbleHistory.Show();
            My.MyProject.Forms.frmScrobbleHistory.Activate();
        }

        private void ScrobbleSearchButton(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmScrobbleSearch.Show();
            My.MyProject.Forms.frmScrobbleSearch.Activate();
        }

        private void ScrobbleIndexEditorMediaButton(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmScrobbleIndexEditor.Show();
            My.MyProject.Forms.frmScrobbleIndexEditor.Activate();
            My.MyProject.Forms.frmScrobbleIndexEditor.Open(My.MySettingsProperty.Settings.CurrentScrobbleIndex);
        }

        public void LoadScrobbleIndex(string location)
        {
            // init reader
            try
            {
                using (var reader = new TextFieldParser(location))
                {
                    reader.TextFieldType = FieldType.Delimited;
                    reader.SetDelimiters(",");

                    // read file and gather errors if any

                    Utilities.scrobbleindexdata.Clear();
                    string[] currentRow;
                    var badLines = new List<uint>();
                    var currentLine = default(uint);
                    bool blank = true; // if this remains true after the loop that means the file is blank
                    while (!reader.EndOfData)
                    {
                        blank = false;
                        currentLine = (uint)(currentLine + 1L);
                        try
                        {
                            currentRow = reader.ReadFields();
                            // row must have 4 fields
                            if (currentRow.Length != 4)
                            {
                                badLines.Add(currentLine);
                            }
                            else
                            {
                                // add line to contents
                                Utilities.scrobbleindexdata.Add(currentRow);
                            }
                        }
                        catch (MalformedLineException ex)
                        {
                            badLines.Add(currentLine);
                        }
                    }

                    // handle errors
                    if (blank == false)
                    {
                        // if entire file is unusable
                        if (badLines.Count >= currentLine)
                        {
                            MessageBox.Show("Scrobble index was unable to be parsed", "Scrobble Index", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        // if only some lines are usuable
                        if (badLines.Count >= 1)
                        {
                            MessageBox.Show("Scrobble index contains errors on some lines, these lines will be ignored by the program", "Scrobble Index", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    // save file location
                    My.MySettingsProperty.Settings.CurrentScrobbleIndex = location;

                    // change label
                    lblMediaIndex.Text = "Using index " + '"' + Utilities.GetFilename(My.MySettingsProperty.Settings.CurrentScrobbleIndex) + ".agsi" + '"';

                    // enable button
                    btnMediaEditIndex.Enabled = true;

                    // change editor button if open
                    if (My.MyProject.Forms.frmScrobbleIndexEditor.Visible == true)
                    {
                        if ((location ?? "") == (My.MyProject.Forms.frmScrobbleIndexEditor.currentIndexLocation ?? ""))
                        {
                            My.MyProject.Forms.frmScrobbleIndexEditor.btnSetIndex.Enabled = false;
                        }
                        else
                        {
                            My.MyProject.Forms.frmScrobbleIndexEditor.btnSetIndex.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // if there was an error loading
                MessageBox.Show("Scrobble index " + '"' + Utilities.GetFilename(My.MySettingsProperty.Settings.CurrentScrobbleIndex) + '"' + " was unable to be loaded." + Constants.vbCrLf + "Please load a new scrobble index.", "Scrobble Index", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // clear file location
                My.MySettingsProperty.Settings.CurrentScrobbleIndex = string.Empty;
                // change label
                lblMediaIndex.Text = "No scrobble index loaded.";
                // disable button
                btnMediaEditIndex.Enabled = false;
            }
        }

        private void IndexLoadClick(object sender, EventArgs e)
        {
            if (ofdIndexLoad.ShowDialog() == DialogResult.OK)
            {
                LoadScrobbleIndex(ofdIndexLoad.FileName);
            }
        }

        private void IndexCreateClick(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmScrobbleIndexEditor.Show();
            My.MyProject.Forms.frmScrobbleIndexEditor.NewFile();
        }

        #endregion

    }
}