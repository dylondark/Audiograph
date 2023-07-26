using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Audiograph.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Audiograph;

public partial class frmBackupTool
{
    private int albumResults;

    private int artistResults;

    // contains value of the currently selected country on the chart page (empty for worldwide)
    private string chartCountry;

    // contains value of the number of results (0 for entire)
    private int chartResults;

    // contains values of checkboxes on the selected content page from top to bottom
    private readonly List<bool> columnContents = new();

    // contains values of the datetimepickers on the user content page
    private readonly List<DateTime> dateContents = new();

    private byte progressMultiplier;

    // 0 = chart, 1 = tag, 2 = track, etc
    private byte section;

    private int tagResults;

    // contains text box contents for the selected content page
    private readonly List<string> textContents = new();
    private int trackResults;
    private int userResults;

    public frmBackupTool()
    {
        InitializeComponent();
    }

    #region Tag

    private void TagOp(object sender, DoWorkEventArgs e)
    {
        try
        {
            // init
            Thread.CurrentThread.Name = "BackupTag";
            var tagInfo = new List<string[]>();
            var topTrackInfo = new List<string[]>();
            var topArtistInfo = new List<string[]>();
            var topAlbumInfo = new List<string[]>();
            var lists = new List<List<string[]>>();
            int progress = 0;

            // verify
            if (Utilities.VerifyTag(textContents[0]).Contains("ERROR: "))
            {
                MessageBox.Show(
                    "Tag data unable to be retrived" + Constants.vbCrLf +
                    "Check that you have spelled your search terms correctly", "Backup Tag", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Invoke(new Action(() => StopOp()));
                return;
            }

            // progress multiplier
            progressMultiplier = 0;
            // info
            if (columnContents[0])
            {
                tagInfo.Add(new[] { "Name", "Taggings", "Reach" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // tracks
            if (columnContents[1])
            {
                topTrackInfo.Add(new[] { "Track", "Artist" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // artists
            if (columnContents[2])
            {
                topArtistInfo.Add(new[] { "Artist" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // albums
            if (columnContents[3])
            {
                topAlbumInfo.Add(new[] { "Album", "Artist" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // info
            if (columnContents[0])
            {
                lists.Add(tagInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(tagResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting tag info...");

                xmlPage = Utilities.CallAPI("tag.getInfo", string.Empty, "tag=" + textContents[0]);

                // cancel check
                if (bgwTag.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;

                // progress
                UpdateProgress(false, progress, "Parsing tag info...");

                // parse
                xmlNodes = new[] { "name", "total", "reach" };
                Utilities.ParseXML(xmlPage, "/lfm/tag", 0U, ref xmlNodes);
                tagInfo.Add(xmlNodes);

                // cancel check
                if (bgwTag.CancellationPending) return;

                progress += 20;
            }

            // top tracks
            if (columnContents[1])
            {
                lists.Add(topTrackInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(tagResults / 50d));
                var xmlPages = new List<string>();
                for (int currentPage = 0, loopTo = pageAmount - 1; currentPage <= loopTo; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 80d + progress,
                        "Getting top tracks... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    if (currentPage == pageAmount - 1)
                    {
                        // last page, only request leftover
                        if (tagResults <= 50)
                            // if results below 50
                            xmlPages.Add(Utilities.CallAPI("tag.getTopTracks", string.Empty,
                                "page=" + (currentPage + 1), "limit=" + tagResults, "tag=" + textContents[0]));
                        else if (tagResults % 50 == 0)
                            // if no remainder
                            xmlPages.Add(Utilities.CallAPI("tag.getTopTracks", string.Empty,
                                "page=" + (currentPage + 1), "limit=50", "tag=" + textContents[0]));
                        else
                            // if not below 50 get remainder
                            xmlPages.Add(Utilities.CallAPI("tag.getTopTracks", string.Empty,
                                "page=" + (currentPage + 1), "limit=" + tagResults % 50, "tag=" + textContents[0]));
                    }
                    else
                    {
                        xmlPages.Add(Utilities.CallAPI("tag.getTopTracks", string.Empty, "page=" + (currentPage + 1),
                            "limit=50", "tag=" + textContents[0]));
                    }

                    // cancel check
                    if (bgwTag.CancellationPending) return;
                }

                progress += 80;

                // add to list
                int currentPageAmount;
                string[] xmlNodes;
                for (int currentPage = 0, loopTo1 = pageAmount - 1; currentPage <= loopTo1; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 20d + progress,
                        "Parsing top tracks... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    currentPageAmount =
                        Conversions.ToInteger(Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                    // parse
                    for (int artist = 0, loopTo2 = currentPageAmount - 1; artist <= loopTo2; artist++)
                    {
                        xmlNodes = new[] { "name", "artist/name" };
                        Utilities.ParseXML(xmlPages[currentPage], "/lfm/tracks/track", (uint)artist, ref xmlNodes);
                        topTrackInfo.Add(xmlNodes);
                    }

                    // cancel check
                    if (bgwTag.CancellationPending) return;
                }

                progress += 20;
            }

            // top artists
            if (columnContents[1])
            {
                lists.Add(topArtistInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(tagResults / 50d));
                var xmlPages = new List<string>();
                for (int currentPage = 0, loopTo3 = pageAmount - 1; currentPage <= loopTo3; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 80d + progress,
                        "Getting top artists... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    if (currentPage == pageAmount - 1)
                    {
                        // last page, only request leftover
                        if (tagResults <= 50)
                            // if results below 50
                            xmlPages.Add(Utilities.CallAPI("tag.getTopArtists", string.Empty,
                                "page=" + (currentPage + 1), "limit=" + tagResults, "tag=" + textContents[0]));
                        else if (tagResults % 50 == 0)
                            // if no remainder
                            xmlPages.Add(Utilities.CallAPI("tag.getTopArtists", string.Empty,
                                "page=" + (currentPage + 1), "limit=50", "tag=" + textContents[0]));
                        else
                            // if not below 50 get remainder
                            xmlPages.Add(Utilities.CallAPI("tag.getTopArtists", string.Empty,
                                "page=" + (currentPage + 1), "limit=" + tagResults % 50, "tag=" + textContents[0]));
                    }
                    else
                    {
                        xmlPages.Add(Utilities.CallAPI("tag.getTopArtists", string.Empty, "page=" + (currentPage + 1),
                            "limit=50", "tag=" + textContents[0]));
                    }

                    // cancel check
                    if (bgwTag.CancellationPending) return;
                }

                progress += 80;

                // add to list
                int currentPageAmount;
                string[] xmlNodes;
                for (int currentPage = 0, loopTo4 = pageAmount - 1; currentPage <= loopTo4; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 20d + progress,
                        "Parsing top artists... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    currentPageAmount =
                        Conversions.ToInteger(Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                    // parse
                    for (int artist = 0, loopTo5 = currentPageAmount - 1; artist <= loopTo5; artist++)
                    {
                        xmlNodes = new[] { "name" };
                        Utilities.ParseXML(xmlPages[currentPage], "/lfm/topartists/artist", (uint)artist, ref xmlNodes);
                        topArtistInfo.Add(xmlNodes);
                    }

                    // cancel check
                    if (bgwTag.CancellationPending) return;
                }

                progress += 20;
            }

            // top albums
            if (columnContents[3])
            {
                lists.Add(topAlbumInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(tagResults / 50d));
                var xmlPages = new List<string>();
                for (int currentPage = 0, loopTo6 = pageAmount - 1; currentPage <= loopTo6; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 80d + progress,
                        "Getting top albums... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    if (currentPage == pageAmount - 1)
                    {
                        // last page, only request leftover
                        if (tagResults <= 50)
                            // if results below 50
                            xmlPages.Add(Utilities.CallAPI("tag.getTopAlbums", string.Empty,
                                "page=" + (currentPage + 1), "limit=" + tagResults, "tag=" + textContents[0]));
                        else if (tagResults % 50 == 0)
                            // if no remainder
                            xmlPages.Add(Utilities.CallAPI("tag.getTopAlbums", string.Empty,
                                "page=" + (currentPage + 1), "limit=50", "tag=" + textContents[0]));
                        else
                            // if not below 50 get remainder
                            xmlPages.Add(Utilities.CallAPI("tag.getTopAlbums", string.Empty,
                                "page=" + (currentPage + 1), "limit=" + tagResults % 50, "tag=" + textContents[0]));
                    }
                    else
                    {
                        xmlPages.Add(Utilities.CallAPI("tag.getTopAlbums", string.Empty, "page=" + (currentPage + 1),
                            "limit=50", "tag=" + textContents[0]));
                    }

                    // cancel check
                    if (bgwTag.CancellationPending) return;
                }

                progress += 80;

                // add to list
                int currentPageAmount;
                string[] xmlNodes;
                for (int currentPage = 0, loopTo7 = pageAmount - 1; currentPage <= loopTo7; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 20d + progress,
                        "Parsing top albums... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    currentPageAmount =
                        Conversions.ToInteger(Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                    // parse
                    for (int album = 0, loopTo8 = currentPageAmount - 1; album <= loopTo8; album++)
                    {
                        xmlNodes = new[] { "name", "artist/name" };
                        Utilities.ParseXML(xmlPages[currentPage], "/lfm/albums/album", (uint)album, ref xmlNodes);
                        topAlbumInfo.Add(xmlNodes);
                    }

                    // cancel check
                    if (bgwTag.CancellationPending) return;
                }

                progress += 20;
            }

            Save(lists.ToArray());
            Invoke(new Action(() => StopOp()));
        }
        catch (Exception ex)
        {
            Invoke(new Action(() =>
                MessageBox.Show("ERROR: " + ex.Message, "Tag Backup", MessageBoxButtons.OK, MessageBoxIcon.Error)));
        }
    }

    #endregion

    #region Track

    private void TrackOp(object sender, DoWorkEventArgs e)
    {
        try
        {
            // init
            Thread.CurrentThread.Name = "BackupTrack";
            var trackInfo = new List<string[]>();
            var statsInfo = new List<string[]>();
            var tagsInfo = new List<string[]>();
            var similarInfo = new List<string[]>();
            var lists = new List<List<string[]>>();
            int progress = 0;

            // verify
            if (Utilities.VerifyTrack(textContents[0], textContents[1])[0].Contains("ERROR: "))
            {
                MessageBox.Show(
                    "Track data unable to be retrived" + Constants.vbCrLf +
                    "Check that you have spelled your search terms correctly", "Backup Track", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Invoke(new Action(() => StopOp()));
                return;
            }

            // progress multiplier
            progressMultiplier = 0;
            // info
            if (columnContents[0])
            {
                trackInfo.Add(new[] { "Name", "Artist", "Album", "Duration (s)" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // stats
            if (columnContents[1])
            {
                statsInfo.Add(new[] { "Listeners", "Playcount" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // tags
            if (columnContents[2])
            {
                tagsInfo.Add(new[] { "Tag", "Taggings" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // similar
            if (columnContents[3])
            {
                similarInfo.Add(new[] { "Track", "Artist", "Match %" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // info
            if (columnContents[0])
            {
                lists.Add(trackInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(trackResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting track info...");

                xmlPage = Utilities.CallAPI("track.getInfo", string.Empty, "track=" + textContents[0],
                    "artist=" + textContents[1], "autocorrect=1");

                // cancel check
                if (bgwTrack.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;

                // progress
                UpdateProgress(false, progress, "Parsing track info...");

                // parse
                xmlNodes = new[] { "name", "artist/name", "album/title", "duration" };
                Utilities.ParseXML(xmlPage, "/lfm/track", 0U, ref xmlNodes);

                // fix album if not present
                if (xmlNodes[2].Contains("ERROR: ")) xmlNodes[2] = string.Empty;

                // fix duration if not present
                if (xmlNodes[3] == "0")
                    xmlNodes[3] = string.Empty;
                else
                    xmlNodes[3] = (Conversions.ToInteger(xmlNodes[3]) / 1000d).ToString("N0");

                trackInfo.Add(xmlNodes);

                // cancel check
                if (bgwTrack.CancellationPending) return;

                progress += 20;
            }

            // stats
            if (columnContents[1])
            {
                lists.Add(statsInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(trackResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting stats info...");

                xmlPage = Utilities.CallAPI("track.getInfo", string.Empty, "track=" + textContents[0],
                    "artist=" + textContents[1], "autocorrect=1");

                // cancel check
                if (bgwTrack.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;

                // progress
                UpdateProgress(false, progress, "Parsing stats info...");

                // parse
                xmlNodes = new[] { "listeners", "playcount" };
                Utilities.ParseXML(xmlPage, "/lfm/track", 0U, ref xmlNodes);

                statsInfo.Add(xmlNodes);

                // cancel check
                if (bgwTrack.CancellationPending) return;

                progress += 20;
            }

            // tags
            if (columnContents[2])
            {
                lists.Add(tagsInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(trackResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting tag info...");

                xmlPage = Utilities.CallAPI("track.getTopTags", string.Empty, "track=" + textContents[0],
                    "artist=" + textContents[1], "autocorrect=1");

                // cancel check
                if (bgwTrack.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;

                // progress
                UpdateProgress(false, progress, "Parsing tag info...");

                // get amount of tags and handle if there are no tags
                int tagCount = (int)Utilities.StrCount(xmlPage, "<name>");
                if (tagCount > 0)
                    // parse
                    for (uint tag = 0U, loopTo = (uint)(tagCount - 1); tag <= loopTo; tag++)
                    {
                        xmlNodes = new[] { "name", "count" };
                        Utilities.ParseXML(xmlPage, "/lfm/toptags/tag", tag, ref xmlNodes);

                        tagsInfo.Add(xmlNodes);
                    }
                else
                    tagsInfo.Add(new[] { string.Empty });

                // cancel check
                if (bgwTrack.CancellationPending) return;

                progress += 20;
            }

            // similar
            if (columnContents[3])
            {
                lists.Add(similarInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(trackResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting similar tracks...");

                xmlPage = Utilities.CallAPI("track.getSimilar", string.Empty, "track=" + textContents[0],
                    "artist=" + textContents[1], "autocorrect=1");

                // cancel check
                if (bgwTrack.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;

                // progress
                UpdateProgress(false, progress, "Parsing similar tracks...");

                // get amount of tracks and handle if there are no tracks
                int trackCount = (int)Utilities.StrCount(xmlPage, "<track>");
                if (trackCount > 0)
                    // parse
                    for (uint track = 0U, loopTo1 = (uint)(trackCount - 1); track <= loopTo1; track++)
                    {
                        xmlNodes = new[] { "name", "artist/name", "match" };
                        Utilities.ParseXML(xmlPage, "/lfm/similartracks/track", track, ref xmlNodes);

                        // convert match from decimal to percent
                        xmlNodes[2] = Conversions.ToDouble(xmlNodes[2]).ToString("P");

                        similarInfo.Add(xmlNodes);
                    }
                else
                    similarInfo.Add(new[] { string.Empty });

                // cancel check
                if (bgwTrack.CancellationPending) return;

                progress += 20;
            }

            Save(lists.ToArray());
            Invoke(new Action(() => StopOp()));
        }
        catch (Exception ex)
        {
            Invoke(new Action(() =>
                MessageBox.Show("ERROR: " + ex.Message, "Track Backup", MessageBoxButtons.OK, MessageBoxIcon.Error)));
        }
    }

    #endregion

    #region Artist

    private void ArtistOp(object sender, DoWorkEventArgs e)
    {
        try
        {
            // init
            Thread.CurrentThread.Name = "BackupArtist";
            var statsInfo = new List<string[]>();
            var tagsInfo = new List<string[]>();
            var similarInfo = new List<string[]>();
            var chartsInfo = new List<string[]>();
            var lists = new List<List<string[]>>();
            int progress = 0;

            // verify
            if (Utilities.VerifyArtist(textContents[0]).Contains("ERROR: "))
            {
                MessageBox.Show(
                    "Artist data unable to be retrived" + Constants.vbCrLf +
                    "Check that you have spelled your search terms correctly", "Backup Artist", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Invoke(new Action(() => StopOp()));
                return;
            }

            // progress multiplier
            progressMultiplier = 0;
            // stats
            if (columnContents[0])
            {
                statsInfo.Add(new[] { "Artist", "Listeners", "Playcount" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // tags
            if (columnContents[1])
            {
                tagsInfo.Add(new[] { "Tag", "Count" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // similar
            if (columnContents[2])
            {
                similarInfo.Add(new[] { "Artist", "Match %" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // charts
            if (columnContents[3])
            {
                chartsInfo.Add(new[] { "Top Track", "Top Album" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // stats
            if (columnContents[0])
            {
                lists.Add(statsInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(artistResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting stats info...");

                xmlPage = Utilities.CallAPI("artist.getInfo", string.Empty, "artist=" + textContents[0],
                    "autocorrect=1");

                // cancel check
                if (bgwArtist.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;

                // progress
                UpdateProgress(false, progress, "Parsing stats info...");

                // parse
                xmlNodes = new[] { "name", "stats/listeners", "stats/playcount" };
                Utilities.ParseXML(xmlPage, "/lfm/artist", 0U, ref xmlNodes);

                statsInfo.Add(xmlNodes);

                // cancel check
                if (bgwArtist.CancellationPending) return;

                progress += 20;
            }

            // tags
            if (columnContents[1])
            {
                lists.Add(tagsInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(artistResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting tag info...");

                xmlPage = Utilities.CallAPI("artist.getTopTags", string.Empty, "artist=" + textContents[0],
                    "autocorrect=1");

                // cancel check
                if (bgwArtist.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;

                // progress
                UpdateProgress(false, progress, "Parsing tag info...");

                // get amount of tags and handle if there are no tags
                int tagCount = (int)Utilities.StrCount(xmlPage, "<name>");
                if (tagCount > 0)
                    // parse
                    for (uint tag = 0U, loopTo = (uint)(tagCount - 1); tag <= loopTo; tag++)
                    {
                        xmlNodes = new[] { "name", "count" };
                        Utilities.ParseXML(xmlPage, "/lfm/toptags/tag", tag, ref xmlNodes);

                        tagsInfo.Add(xmlNodes);
                    }

                // cancel check
                if (bgwArtist.CancellationPending) return;

                progress += 20;
            }

            // similar
            if (columnContents[2])
            {
                lists.Add(similarInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(artistResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting similar artists...");

                xmlPage = Utilities.CallAPI("artist.getSimilar", string.Empty, "artist=" + textContents[0],
                    "autocorrect=1");

                // cancel check
                if (bgwArtist.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;

                // progress
                UpdateProgress(false, progress, "Parsing similar artists...");

                // get amount of tags and handle if there are no tags
                int artistCount = (int)Utilities.StrCount(xmlPage, "<artist>");
                if (artistCount > 0)
                    // parse
                    for (uint artist = 0U, loopTo1 = (uint)(artistCount - 1); artist <= loopTo1; artist++)
                    {
                        xmlNodes = new[] { "name", "match" };
                        Utilities.ParseXML(xmlPage, "/lfm/similarartists/artist", artist, ref xmlNodes);

                        // convert match from decimal to percent
                        xmlNodes[1] = Conversions.ToDouble(xmlNodes[1]).ToString("P");

                        similarInfo.Add(xmlNodes);
                    }

                // cancel check
                if (bgwArtist.CancellationPending) return;

                progress += 20;
            }

            // charts
            if (columnContents[3])
            {
                lists.Add(chartsInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(artistResults / 50d));
                var xmlPage = new string[2];

                // progress
                UpdateProgress(false, progress, "Getting artist charts...");

                xmlPage[0] = Utilities.CallAPI("artist.getTopTracks", string.Empty, "artist=" + textContents[0],
                    "autocorrect=1"); // tracks
                xmlPage[1] = Utilities.CallAPI("artist.getTopAlbums", string.Empty, "artist=" + textContents[0],
                    "autocorrect=1"); // albums

                // cancel check
                if (bgwArtist.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;
                var xmlNodesFinal = new List<string>();

                // progress
                UpdateProgress(false, progress, "Parsing similar artists...");

                // get amount of tracks and handle if there are no tracks
                int count = (int)Utilities.StrCount(xmlPage[0], "</track>");
                if (count > 0)
                    // parse
                    for (uint artist = 0U, loopTo2 = (uint)(count - 1); artist <= loopTo2; artist++)
                    {
                        xmlNodes = new[] { "name" };
                        Utilities.ParseXML(xmlPage[0], "/lfm/toptracks/track", artist, ref xmlNodes);

                        // add to final list
                        xmlNodesFinal.Add(xmlNodes[0]);

                        xmlNodes = new[] { "name" };
                        Utilities.ParseXML(xmlPage[1], "/lfm/topalbums/album", artist, ref xmlNodes);

                        xmlNodesFinal.Add(xmlNodes[0]);

                        chartsInfo.Add(xmlNodesFinal.ToArray());
                        xmlNodesFinal.Clear();
                    }

                // cancel check
                if (bgwArtist.CancellationPending) return;

                progress += 20;
            }

            Save(lists.ToArray());
            Invoke(new Action(() => StopOp()));
        }
        catch (Exception ex)
        {
            Invoke(new Action(() =>
                MessageBox.Show("ERROR: " + ex.Message, "Artist Backup", MessageBoxButtons.OK, MessageBoxIcon.Error)));
        }
    }

    #endregion

    #region Album

    private void AlbumOp(object sender, DoWorkEventArgs e)
    {
        try
        {
            // init
            Thread.CurrentThread.Name = "BackupAlbum";
            var albumInfo = new List<string[]>();
            var tracksInfo = new List<string[]>();
            var statsInfo = new List<string[]>();
            var tagsInfo = new List<string[]>();
            var lists = new List<List<string[]>>();
            int progress = 0;

            // verify
            if (Utilities.VerifyAlbum(textContents[0], textContents[1])[0].Contains("ERROR: "))
            {
                MessageBox.Show(
                    "Album data unable to be retrived" + Constants.vbCrLf +
                    "Check that you have spelled your search terms correctly", "Backup Album", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Invoke(new Action(() => StopOp()));
                return;
            }

            // progress multiplier
            progressMultiplier = 0;
            // info
            if (columnContents[0])
            {
                albumInfo.Add(new[] { "Album", "Artist" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // tracks
            if (columnContents[1])
            {
                tracksInfo.Add(new[] { "Track", "Duration (s)" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // stats
            if (columnContents[2])
            {
                statsInfo.Add(new[] { "Listeners", "Playcount" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // tags
            if (columnContents[3])
            {
                tagsInfo.Add(new[] { "Tag", "Taggings" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // info
            if (columnContents[0])
            {
                lists.Add(albumInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(albumResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting album info...");

                xmlPage = Utilities.CallAPI("album.getInfo", string.Empty, "album=" + textContents[0],
                    "artist=" + textContents[1], "autocorrect=1");

                // cancel check
                if (bgwAlbum.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;

                // progress
                UpdateProgress(false, progress, "Parsing album info...");

                // parse
                xmlNodes = new[] { "name", "artist" };
                Utilities.ParseXML(xmlPage, "/lfm/album", 0U, ref xmlNodes);

                albumInfo.Add(xmlNodes);

                // cancel check
                if (bgwAlbum.CancellationPending) return;

                progress += 20;
            }

            // tracks
            if (columnContents[1])
            {
                lists.Add(tracksInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(albumResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting tracks info...");

                xmlPage = Utilities.CallAPI("album.getInfo", string.Empty, "album=" + textContents[0],
                    "artist=" + textContents[1], "autocorrect=1");

                // cancel check
                if (bgwAlbum.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;

                // progress
                UpdateProgress(false, progress, "Parsing tracks info...");

                // get amount of tags and handle if there are no tags
                int trackCount = (int)Utilities.StrCount(xmlPage, "</track>");
                if (trackCount > 0)
                    // parse
                    for (uint track = 0U, loopTo = (uint)(trackCount - 1); track <= loopTo; track++)
                    {
                        xmlNodes = new[] { "name", "duration" };
                        Utilities.ParseXML(xmlPage, "/lfm/album/tracks/track", track, ref xmlNodes);

                        tracksInfo.Add(xmlNodes);
                    }

                // cancel check
                if (bgwAlbum.CancellationPending) return;

                progress += 20;
            }

            // stats
            if (columnContents[2])
            {
                lists.Add(statsInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(albumResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting stats info...");

                xmlPage = Utilities.CallAPI("album.getInfo", string.Empty, "album=" + textContents[0],
                    "artist=" + textContents[1], "autocorrect=1");

                // cancel check
                if (bgwAlbum.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;

                // progress
                UpdateProgress(false, progress, "Parsing stats info...");

                // parse
                xmlNodes = new[] { "listeners", "playcount" };
                Utilities.ParseXML(xmlPage, "/lfm/album", 0U, ref xmlNodes);

                statsInfo.Add(xmlNodes);

                // cancel check
                if (bgwAlbum.CancellationPending) return;

                progress += 20;
            }

            // tags
            if (columnContents[3])
            {
                lists.Add(tagsInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(albumResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting tag info...");

                xmlPage = Utilities.CallAPI("album.getTopTags", string.Empty, "album=" + textContents[0],
                    "artist=" + textContents[1], "autocorrect=1");

                // cancel check
                if (bgwAlbum.CancellationPending) return;

                progress += 80;

                // add to list
                string[] xmlNodes;

                // progress
                UpdateProgress(false, progress, "Parsing tag info...");

                // get amount of tags and handle if there are no tags
                int tagCount = (int)Utilities.StrCount(xmlPage, "<name>");
                if (tagCount > 0)
                    // parse
                    for (uint tag = 0U, loopTo1 = (uint)(tagCount - 1); tag <= loopTo1; tag++)
                    {
                        xmlNodes = new[] { "name", "count" };
                        Utilities.ParseXML(xmlPage, "/lfm/toptags/tag", tag, ref xmlNodes);

                        tagsInfo.Add(xmlNodes);
                    }
                else
                    tagsInfo.Add(new[] { string.Empty });

                // cancel check
                if (bgwAlbum.CancellationPending) return;

                progress += 20;
            }

            Save(lists.ToArray());
            Invoke(new Action(() => StopOp()));
        }
        catch (Exception ex)
        {
            Invoke(new Action(() =>
                MessageBox.Show("ERROR: " + ex.Message, "Album Backup", MessageBoxButtons.OK, MessageBoxIcon.Error)));
        }
    }

    #endregion

    #region UI

    private void FrmLoad(object sender, EventArgs e)
    {
        // automatically switch to and fill in data from current tab
        switch (MyProject.Forms.frmMain.tabControl.SelectedIndex)
        {
            case 0:
            {
                // charts tab
                cmbContents.SelectedIndex = 0;
                // select worldwide or country
                if (MyProject.Forms.frmMain.radChartWorldwide.Checked)
                    radChartWorldwide.Checked = true;
                else
                    radChartCountry.Checked = true;
                // fill in country
                cmbChartCountry.SelectedIndex = MyProject.Forms.frmMain.cmbChartCountry.SelectedIndex;
                ChartEnableCountries(null, null);
                break;
            }
            case 1:
            {
                // tag tab
                cmbContents.SelectedIndex = 1;
                txtTagTag.Text = MyProject.Forms.frmMain.txtSearch.Text;
                break;
            }
            case 2:
            {
                // track tab
                cmbContents.SelectedIndex = 2;
                txtTrackTitle.Text = MyProject.Forms.frmMain.txtTrackTitle.Text;
                txtTrackArtist.Text = MyProject.Forms.frmMain.txtTrackArtist.Text;
                break;
            }
            case 3:
            {
                // artist tab
                cmbContents.SelectedIndex = 3;
                txtArtistArtist.Text = MyProject.Forms.frmMain.txtArtistName.Text;
                break;
            }
            case 4:
            {
                // album tab
                cmbContents.SelectedIndex = 4;
                txtAlbumAlbum.Text = MyProject.Forms.frmMain.txtAlbumTitle.Text;
                txtAlbumArtist.Text = MyProject.Forms.frmMain.txtAlbumArtist.Text;
                break;
            }
            case 5:
            {
                // user tab
                cmbContents.SelectedIndex = 5;
                txtUserUser.Text = MyProject.Forms.frmMain.txtUser.Text;
                break;
            }
            case 6:
            {
                // user lookup tab
                cmbContents.SelectedIndex = 5;
                txtUserUser.Text = MyProject.Forms.frmMain.txtUserL.Text;
                break;
            }

            default:
            {
                cmbContents.SelectedIndex = 0;
                break;
            }
        }

        UserEnableAmount(null, null);
        UserEnableDate(null, null);
    }

    private void ChangeContents(object sender, EventArgs e)
    {
        InvisibleAllPanels();
        switch (cmbContents.SelectedIndex)
        {
            case 0:
            {
                pnlCharts.Visible = true;
                break;
            }
            case 1:
            {
                pnlTag.Visible = true;
                break;
            }
            case 2:
            {
                pnlTrack.Visible = true;
                break;
            }
            case 3:
            {
                pnlArtist.Visible = true;
                break;
            }
            case 4:
            {
                pnlAlbum.Visible = true;
                break;
            }
            case 5:
            {
                pnlUser.Visible = true;
                break;
            }
        }
    }

    private void InvisibleAllPanels()
    {
        pnlCharts.Visible = false;
        pnlTag.Visible = false;
        pnlTrack.Visible = false;
        pnlArtist.Visible = false;
        pnlAlbum.Visible = false;
        pnlUser.Visible = false;
    }

    private void Browse(object sender, EventArgs e)
    {
        var result = sfdSave.ShowDialog();

        if (result == DialogResult.OK) txtSave.Text = sfdSave.FileName;
    }

    private void StartButton(object sender, EventArgs e)
    {
        StartOp();
    }

    private void StopButton(object sender, EventArgs e)
    {
        StopOp();
    }

    private void FrmClose(object sender, EventArgs e)
    {
        Close();
    }

    #endregion

    #region Ops

    private void UpdateProgress(bool finalStage, double percentage, string status)
    {
        // only add if the progressbar will not go over 95%
        if (finalStage == false)
        {
            if (percentage / progressMultiplier * 0.95d <= 95d)
                Invoke(new Action(() => pbStatus.Value = (int)Math.Round(percentage / progressMultiplier * 0.95d)));
        }
        else if (percentage != 100d)
        {
            Invoke(new Action(() => pbStatus.Value = (int)Math.Round(95d + percentage * 0.05d)));
        }
        else
        {
            Invoke(new Action(() => pbStatus.Value = 100));
        }

        Invoke(new Action(() => lblStatus.Text = status));
    }

    private void StartOp()
    {
        // ui
        btnStart.Enabled = false;
        btnStop.Enabled = true;
        progressMultiplier = 1;
        UpdateProgress(false, 0d, "Starting...");

        // clear
        textContents.Clear();
        columnContents.Clear();
        dateContents.Clear();

        // make sure something is entered in the browse field
        if (string.IsNullOrEmpty(txtSave.Text.Trim()))
        {
            MessageBox.Show("Valid data must be entered in the Save Location field", "Backup Tool",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            StopOp();
            return;
        }

        // evaluate what needs to be backed up
        section = (byte)cmbContents.SelectedIndex;
        switch (section)
        {
            case 0:
            {
                // results
                chartResults = (int)Math.Round(nudChartResults.Value);

                // country
                if (radChartWorldwide.Checked)
                    chartCountry = string.Empty;
                else
                    chartCountry = cmbChartCountry.Text;

                // columns
                columnContents.Add(chkChartTopTracks.Checked);
                columnContents.Add(chkChartTopArtists.Checked);
                columnContents.Add(chkChartTopTags.Checked);

                bgwChart.RunWorkerAsync();
                break;
            }
            case 1:
            {
                // text
                if (string.IsNullOrEmpty(txtTagTag.Text.Trim()))
                {
                    MessageBox.Show("Valid data must be entered in the Tag field", "Tag Backup", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    StopOp();
                    return;
                }

                textContents.Add(txtTagTag.Text);

                // results
                tagResults = (int)Math.Round(nudTagResults.Value);

                // columns
                columnContents.Add(chkTagInfo.Checked);
                columnContents.Add(chkTagTopTracks.Checked);
                columnContents.Add(chkTagTopArtists.Checked);
                columnContents.Add(chkTagTopAlbums.Checked);

                bgwTag.RunWorkerAsync();
                break;
            }
            case 2:
            {
                // text
                if (string.IsNullOrEmpty(txtTrackTitle.Text.Trim()) || string.IsNullOrEmpty(txtTrackArtist.Text.Trim()))
                {
                    MessageBox.Show("Valid data must be entered in the Track and Artist fields", "Track Backup",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StopOp();
                    return;
                }

                textContents.Add(txtTrackTitle.Text);
                textContents.Add(txtTrackArtist.Text);

                // columns
                columnContents.Add(chkTrackInfo.Checked);
                columnContents.Add(chkTrackStats.Checked);
                columnContents.Add(chkTrackTags.Checked);
                columnContents.Add(chkTrackSimilar.Checked);

                bgwTrack.RunWorkerAsync();
                break;
            }
            case 3:
            {
                // text
                if (string.IsNullOrEmpty(txtArtistArtist.Text.Trim()))
                {
                    MessageBox.Show("Valid data must be entered in the Artist field", "Artist Backup",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StopOp();
                    return;
                }

                textContents.Add(txtArtistArtist.Text);

                // columns
                columnContents.Add(chkArtistStats.Checked);
                columnContents.Add(chkArtistTags.Checked);
                columnContents.Add(chkArtistSimilar.Checked);
                columnContents.Add(chkArtistCharts.Checked);

                bgwArtist.RunWorkerAsync();
                break;
            }
            case 4:
            {
                // text
                if (string.IsNullOrEmpty(txtAlbumAlbum.Text.Trim()) || string.IsNullOrEmpty(txtAlbumArtist.Text.Trim()))
                {
                    MessageBox.Show("Valid data must be entered in the Album and Artist fields", "Album Backup",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StopOp();
                    return;
                }

                textContents.Add(txtAlbumAlbum.Text);
                textContents.Add(txtAlbumArtist.Text);

                // columns
                columnContents.Add(chkAlbumInfo.Checked);
                columnContents.Add(chkAlbumTracks.Checked);
                columnContents.Add(chkAlbumStats.Checked);
                columnContents.Add(chkAlbumTags.Checked);

                bgwAlbum.RunWorkerAsync();
                break;
            }
            case 5:
            {
                // text
                if (string.IsNullOrEmpty(txtUserUser.Text.Trim()))
                {
                    MessageBox.Show("Valid data must be entered in the User field", "User Backup", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    StopOp();
                    return;
                }

                textContents.Add(txtUserUser.Text);

                // date
                if (chkUserByDate.Checked)
                {
                    // check that from is before to
                    if (dtpUserFrom.Value > dtpUserTo.Value)
                    {
                        MessageBox.Show("From date must be before to date", "User Backup", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        StopOp();
                        return;
                    }

                    dateContents.Add(dtpUserFrom.Value.Date);
                    dateContents.Add(dtpUserTo.Value.Date);
                }

                // results
                if (radUserEntire.Checked)
                    userResults = 0;
                else
                    userResults = (int)Math.Round(nudUserNumber.Value);

                // columns
                columnContents.Add(chkUserInfo.Checked);
                columnContents.Add(chkUserFriends.Checked);
                columnContents.Add(chkUserLoved.Checked);
                columnContents.Add(chkUserHistory.Checked);
                columnContents.Add(chkUserCharts.Checked);

                bgwUser.RunWorkerAsync();
                break;
            }
        }
    }

    private void StopOp()
    {
        // stop threads
        if (bgwChart.IsBusy) bgwChart.CancelAsync();
        if (bgwTag.IsBusy) bgwTag.CancelAsync();
        if (bgwTrack.IsBusy) bgwTrack.CancelAsync();
        if (bgwArtist.IsBusy) bgwArtist.CancelAsync();
        if (bgwAlbum.IsBusy) bgwAlbum.CancelAsync();
        if (bgwUser.IsBusy) bgwUser.CancelAsync();

        // ui
        btnStart.Enabled = true;
        btnStop.Enabled = false;
        pbStatus.Value = 0;
        lblStatus.Text = "Ready";
    }

    private void BackgroundStopOp(object sender, RunWorkerCompletedEventArgs e)
    {
        // ui
        btnStart.Enabled = true;
        btnStop.Enabled = false;
        pbStatus.Value = 0;
        lblStatus.Text = "Ready";
    }

    // the most retardedly confusing method ive ever written
    private void Save(List<string[]>[] lists)
    {
        // List of string()()    -1 contains all the categories
        // List of string()	-2 contains all the lines in the category
        // String()		-3 contains all the cells of a line
        // String     -4 contains each individual cell of a line

        var outputList = new List<string[]>();
        var currentLine = new List<string>();
        int largestValue = 0;

        UpdateProgress(true, 0d, "Assembling...");

        // get the largest list
        foreach (var list in lists) // 1 - finding the biggest category
            if (list.Count - 1 > largestValue)
                largestValue = list.Count - 1;

        // 3 - cycling through each line
        for (int line = 0, loopTo = largestValue; line <= loopTo; line++)
        {
            // 2 - cycling through the categories to add one line from each
            for (int list = 0, loopTo1 = lists.Count() - 1; list <= loopTo1; list++)
            {
                // attempt to add if there is more from the category, if not then add empty
                if (lists[list].Count - 1 >= line)
                    // 4 - cycle through each cell of the line and add to current line
                    foreach (var item in lists[list][line])
                        // check for errors
                        if (item.Contains("ERROR: ") == false)
                            currentLine.Add(item.Replace(Conversions.ToString('"'), string.Empty));
                        else
                            currentLine.Add(string.Empty);
                else
                    // add empty cells
                    foreach (var item in lists[list][0])
                        currentLine.Add(string.Empty);

                // add separator only if not on the last list
                if (list != lists.Count() - 1) currentLine.Add("-");
            }

            // add and clear
            outputList.Add(currentLine.ToArray());
            currentLine.Clear();
        }

        UpdateProgress(true, 50d, "Writing...");

        // save
        var fi = new FileInfo(txtSave.Text.Trim());

        // delete file if it already exists
        if (fi.Exists)
            try
            {
                fi.Delete();
            }
            catch (IOException ex)
            {
                MessageBox.Show(
                    "Could not write to file" + Constants.vbCrLf + "Check that another program is not using the file",
                    "Backup Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Invoke(new Action(() => StopOp()));
                return;
            }

        // create new file and filestream
        var fs = default(FileStream);
        using (fs)
        {
            fs = fi.Create();

            // compile data to string
            var str = new StringBuilder();

            foreach (var line in outputList)
                for (int cell = 0, loopTo2 = line.Count() - 1; cell <= loopTo2; cell++)
                    // check if on the last cell
                    if (cell < line.Count() - 1)
                    {
                        str.Append('"' + line[cell] + '"' + ",");
                    }

                    else
                    {
                        // if on last line dont add comma and add line
                        str.Append('"' + line[cell] + '"');
                        str.AppendLine();
                    }

            // convert to bytearray and write
            byte[] bytes = new UTF32Encoding().GetBytes(str.ToString());
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }

        UpdateProgress(true, 100d, "Success!");
    }

    #endregion

    #region Charts

    private void ChartOp(object sender, DoWorkEventArgs e)
    {
        try
        {
            // init
            Thread.CurrentThread.Name = "BackupChart";
            var topTrackInfo = new List<string[]>();
            var topArtistInfo = new List<string[]>();
            var topTagInfo = new List<string[]>();
            var lists = new List<List<string[]>>();
            int progress = 0;
            bool geo = false;
            Invoke(new Action(() => geo = radChartCountry.Checked));
            string country = null;
            Invoke(new Action(() => country = cmbChartCountry.Text));    

            // progress multiplier
            progressMultiplier = 0;
            if (columnContents[0])
            {
                if (geo)
                    topTrackInfo.Add(new[] { "Track", "Artist", "Listeners" });
                else
                    topTrackInfo.Add(new[] { "Track", "Artist", "Listeners", "Playcount" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            if (columnContents[1])
            {
                if (geo)
                    topArtistInfo.Add(new[] { "Artist", "Listeners" });
                else
                    topArtistInfo.Add(new[] { "Artist", "Listeners", "Playcount" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            if (!geo && columnContents[2])
            {
                topTagInfo.Add(new[] { "Tag", "Reach", "Taggings" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // top tracks
            if (columnContents[0])
            {
                lists.Add(topTrackInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(chartResults / 50d));
                var xmlPages = new List<string>();

                for (int currentPage = 0, loopTo = pageAmount - 1; currentPage <= loopTo; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 80d + progress,
                        "Getting top tracks... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    if (currentPage == pageAmount - 1)
                    {
                        // last page, only request leftover
                        if (chartResults <= 50)
                            // if results below 50
                            if (geo)
                                xmlPages.Add(Utilities.CallAPI("geo.getTopTracks", string.Empty,
                                    "country=" + country, "page=" + (currentPage + 1), "limit=" + chartResults));
                            else
                                xmlPages.Add(Utilities.CallAPI("chart.getTopTracks", string.Empty,
                                    "page=" + (currentPage + 1), "limit=" + chartResults));
                        else if (chartResults % 50 == 0)
                            // if no remainder
                            if (geo)
                                xmlPages.Add(Utilities.CallAPI("geo.getTopTracks", string.Empty,
                                    "country=" + country, "page=" + (currentPage + 1), "limit=50"));
                            else
                                xmlPages.Add(Utilities.CallAPI("chart.getTopTracks", string.Empty,
                                    "page=" + (currentPage + 1), "limit=50"));
                        else
                            // if not below 50 get remainder
                            if (geo)
                                xmlPages.Add(Utilities.CallAPI("geo.getTopTracks", string.Empty,
                                    "country=" + country, "page=" + (currentPage + 1), "limit=" + chartResults % 50));
                        else
                                xmlPages.Add(Utilities.CallAPI("chart.getTopTracks", string.Empty,
                                    "page=" + (currentPage + 1), "limit=" + chartResults % 50));
                    }
                    else
                    {
                        if (geo)
                            xmlPages.Add(Utilities.CallAPI("geo.getTopTracks", string.Empty, "country=" + country, "page=" + (currentPage + 1),
                                "limit=50"));
                        else
                            xmlPages.Add(Utilities.CallAPI("chart.getTopTracks", string.Empty, "page=" + (currentPage + 1),
                                "limit=50"));
                    }

                    // cancel check
                    if (bgwChart.CancellationPending) return;
                }

                progress += 80;

                // add to list
                int currentPageAmount;
                string[] xmlNodes;
                for (int currentPage = 0, loopTo1 = pageAmount - 1; currentPage <= loopTo1; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 20d + progress,
                        "Parsing top tracks... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    currentPageAmount =
                        Conversions.ToInteger(Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                    // parse
                    for (int track = 0, loopTo2 = currentPageAmount - 1; track <= loopTo2; track++)
                    {
                        if (geo)
                            xmlNodes = new[] { "name", "artist/name", "listeners" };
                        else
                            xmlNodes = new[] { "name", "artist/name", "listeners", "playcount" };
                        Utilities.ParseXML(xmlPages[currentPage], "/lfm/tracks/track", (uint)track, ref xmlNodes);
                        topTrackInfo.Add(xmlNodes);
                    }

                    // cancel check
                    if (bgwChart.CancellationPending) return;
                }

                progress += 20;
            }

            // top artists
            if (columnContents[1])
            {
                lists.Add(topArtistInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(chartResults / 50d));
                var xmlPages = new List<string>();
                for (int currentPage = 0, loopTo3 = pageAmount - 1; currentPage <= loopTo3; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 80d + progress,
                        "Getting top artists... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    if (currentPage == pageAmount - 1)
                    {
                        // last page, only request leftover
                        if (chartResults <= 50)
                            // if results below 50
                            if (geo)
                                xmlPages.Add(Utilities.CallAPI("geo.getTopArtists", string.Empty,
                                    "country=" + country, "page=" + (currentPage + 1), "limit=" + chartResults));
                            else
                                xmlPages.Add(Utilities.CallAPI("chart.getTopArtists", string.Empty,
                                    "page=" + (currentPage + 1), "limit=" + chartResults));
                        else if (chartResults % 50 == 0)
                            // if no remainder
                            if (geo)
                                 xmlPages.Add(Utilities.CallAPI("geo.getTopArtists", string.Empty,
                                     "country=" + country, "page=" + (currentPage + 1), "limit=50"));
                            else
                                xmlPages.Add(Utilities.CallAPI("chart.getTopArtists", string.Empty,
                                    "page=" + (currentPage + 1), "limit=50"));
                        else
                            // if not below 50 get remainder
                            if (geo)
                                xmlPages.Add(Utilities.CallAPI("geo.getTopArtists", string.Empty,
                                    "country=" + country, "page=" + (currentPage + 1), "limit=" + chartResults % 50));
                            else
                                xmlPages.Add(Utilities.CallAPI("chart.getTopArtists", string.Empty,
                                    "page=" + (currentPage + 1), "limit=" + chartResults % 50));
                    }
                    else
                    {
                        if (geo)
                            xmlPages.Add(Utilities.CallAPI("geo.getTopArtists", string.Empty, "page=" + (currentPage + 1),
                                "country=" + country, "limit=50"));
                        else
                            xmlPages.Add(Utilities.CallAPI("chart.getTopArtists", string.Empty, "page=" + (currentPage + 1),
                            "limit=50"));
                    }

                    // cancel check
                    if (bgwChart.CancellationPending) return;
                }

                progress += 80;

                // add to list
                int currentPageAmount;
                string[] xmlNodes;

                string directory;
                if (geo)    // correct annoying inconsistent api behavior
                    directory = "topartists";
                else
                    directory = "artists";

                for (int currentPage = 0, loopTo4 = pageAmount - 1; currentPage <= loopTo4; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 20d + progress,
                        "Parsing top artists... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    currentPageAmount =
                        Conversions.ToInteger(Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                    // parse
                    for (int artist = 0, loopTo5 = currentPageAmount - 1; artist <= loopTo5; artist++)
                    {
                        if (geo)
                            xmlNodes = new[] { "name", "listeners" };
                        else
                            xmlNodes = new[] { "name", "listeners", "playcount" };
                        Utilities.ParseXML(xmlPages[currentPage], "/lfm/" + directory + "/artist", (uint)artist, ref xmlNodes);
                        topArtistInfo.Add(xmlNodes);
                    }

                    // cancel check
                    if (bgwChart.CancellationPending) return;
                }

                progress += 20;
            }

            // top tags
            if (!geo && columnContents[2])
            {
                lists.Add(topTagInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(chartResults / 50d));
                var xmlPages = new List<string>();
                for (int currentPage = 0, loopTo6 = pageAmount - 1; currentPage <= loopTo6; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 80d + progress,
                        "Getting top tags... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    if (currentPage == pageAmount - 1)
                    {
                        // last page, only request leftover
                        if (chartResults <= 50)
                            // if results below 50
                            xmlPages.Add(Utilities.CallAPI("chart.getTopTags", string.Empty,
                                "page=" + (currentPage + 1), "limit=" + chartResults));
                        else if (chartResults % 50 == 0)
                            // if no remainder
                            xmlPages.Add(Utilities.CallAPI("chart.getTopTags", string.Empty,
                                "page=" + (currentPage + 1), "limit=50"));
                        else
                            // if not below 50 get remainder
                            xmlPages.Add(Utilities.CallAPI("chart.getTopTags", string.Empty,
                                "page=" + (currentPage + 1), "limit=" + chartResults % 50));
                    }
                    else
                    {
                        xmlPages.Add(Utilities.CallAPI("chart.getTopTags", string.Empty, "page=" + (currentPage + 1),
                            "limit=50"));
                    }

                    // cancel check
                    if (bgwChart.CancellationPending) return;
                }

                progress += 80;

                // add to list
                int currentPageAmount;
                string[] xmlNodes;
                for (int currentPage = 0, loopTo7 = pageAmount - 1; currentPage <= loopTo7; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 20d + progress,
                        "Parsing top tags... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    currentPageAmount =
                        Conversions.ToInteger(Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                    // parse
                    for (int tag = 0, loopTo8 = currentPageAmount - 1; tag <= loopTo8; tag++)
                    {
                        xmlNodes = new[] { "name", "reach", "taggings" };
                        Utilities.ParseXML(xmlPages[currentPage], "/lfm/tags/tag", (uint)tag, ref xmlNodes);
                        topTagInfo.Add(xmlNodes);
                    }

                    // cancel check
                    if (bgwChart.CancellationPending) return;
                }
            }

            Save(lists.ToArray());
            Invoke(new Action(() => StopOp()));
        }
        catch (Exception ex)
        {
            Invoke(new Action(() => MessageBox.Show("An error occurred. This may be due to rate limiting or otherwise invalid data from the API. \nPlease try again later.", "Charts Backup", MessageBoxButtons.OK,
                MessageBoxIcon.Error)));
        }
    }

    private void ChartEnableCountries(object sender, EventArgs e)
    {
        if (radChartWorldwide.Checked)
        {
            cmbChartCountry.Enabled = false;
            chkChartTopTags.Enabled = true;
        }
        else
        {
            cmbChartCountry.Enabled = true;
            chkChartTopTags.Enabled = false;
        }
    }

    #endregion

    #region User

    private void UserOp(object sender, DoWorkEventArgs e)
    {
        try
        {
            // init
            Thread.CurrentThread.Name = "UserBackup";
            var userInfo = new List<string[]>();
            var friendsInfo = new List<string[]>();
            var lovedInfo = new List<string[]>();
            var historyInfo = new List<string[]>();
            var topTrackInfo = new List<string[]>();
            var topArtistInfo = new List<string[]>();
            var topAlbumInfo = new List<string[]>();
            var lists = new List<List<string[]>>();
            int progress = 0;

            // progress multiplier
            progressMultiplier = 0;
            // info
            if (columnContents[0])
            {
                userInfo.Add(new[]
                {
                    "Username", "Real Name", "Url", "Country", "Age", "Gender", "Playcount", "Playlists",
                    "Date Registered"
                });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // friends
            if (columnContents[1])
            {
                friendsInfo.Add(new[] { "Name", "Real Name", "Url", "Date Registered" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // loved songs
            if (columnContents[2])
            {
                lovedInfo.Add(new[] { "Loved Track", "Artist", "Date Loved (Unix)" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // history
            if (columnContents[3])
            {
                historyInfo.Add(new[] { "Historical Track", "Artist", "Album", "Date Scrobbled" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // charts
            if (columnContents[4])
            {
                topTrackInfo.Add(new[] { "Top Track", "Artist", "User Playcount" });
                topArtistInfo.Add(new[] { "Top Artist", "User Playcount" });
                topAlbumInfo.Add(new[] { "Top Album", "Artist", "User Playcount" });
                progressMultiplier = (byte)(progressMultiplier + 1);
            }

            // info
            if (columnContents[0])
            {
                lists.Add(userInfo);
                // get all xml pages
                int pageAmount = (int)Math.Round(Math.Ceiling(userResults / 50d));
                string xmlPage;

                // progress
                UpdateProgress(false, progress, "Getting user info...");

                xmlPage = Utilities.CallAPI("user.getInfo", textContents[0]);

                // cancel check
                if (bgwUser.CancellationPending) return;

                progress += 80;

                // progress
                UpdateProgress(false, progress, "Parsing user info...");

                // parse
                string[] xmlNodes =
                    { "name", "realname", "url", "country", "age", "gender", "playcount", "playlists", "registered" };
                Utilities.ParseXML(xmlPage, "/lfm/user", 0U, ref xmlNodes);

                // gender formatting
                switch (xmlNodes[5] ?? "")
                {
                    case "m":
                    {
                        xmlNodes[5] = "Male";
                        break;
                    }
                    case "f":
                    {
                        xmlNodes[5] = "Female";
                        break;
                    }

                    default:
                    {
                        xmlNodes[5] = "Not Specified";
                        break;
                    }
                }

                // age formatting
                if (xmlNodes[4] == "0") xmlNodes[4] = "Not Specified";

                userInfo.Add(xmlNodes);

                // cancel check
                if (bgwUser.CancellationPending) return;

                progress += 20;
            }

            // friends
            if (columnContents[1])
            {
                lists.Add(friendsInfo);
                // get total
                int userResults2 =
                    Conversions.ToInteger(Utilities.ParseMetadata(Utilities.CallAPI("user.getFriends", textContents[0]),
                        "total="));
                int pageAmount = (int)Math.Round(Math.Ceiling(userResults2 / 50d));
                var xmlPages = new List<string>();

                for (int currentPage = 0, loopTo = pageAmount - 1; currentPage <= loopTo; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 80d + progress,
                        "Getting user friends... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    if (currentPage == pageAmount - 1)
                    {
                        // last page, only request leftover
                        if (userResults2 <= 50)
                            // if results below 50
                            xmlPages.Add(Utilities.CallAPI("user.getFriends", textContents[0],
                                "page=" + (currentPage + 1), "limit=" + userResults2));
                        else
                            // if not below 50 get remainder
                            xmlPages.Add(Utilities.CallAPI("user.getFriends", textContents[0],
                                "page=" + (currentPage + 1), "limit=50"));
                    }
                    else
                    {
                        xmlPages.Add(Utilities.CallAPI("user.getFriends", textContents[0], "page=" + (currentPage + 1),
                            "limit=50"));
                    }

                    // cancel check
                    if (bgwUser.CancellationPending) return;
                }

                progress += 80;

                // add to list
                int currentPageAmount;
                string[] xmlNodes;
                for (int currentPage = 0, loopTo1 = pageAmount - 1; currentPage <= loopTo1; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 20d + progress,
                        "Parsing user friends... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    currentPageAmount =
                        Conversions.ToInteger(Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                    // parse
                    for (int user = 0, loopTo2 = currentPageAmount - 1; user <= loopTo2; user++)
                    {
                        xmlNodes = new[] { "name", "realname", "url", "registered" };
                        Utilities.ParseXML(xmlPages[currentPage], "/lfm/friends/user", (uint)user, ref xmlNodes);
                        friendsInfo.Add(xmlNodes);

                        // cancel check
                        if (bgwUser.CancellationPending) return;
                    }
                }

                progress += 20;
            }

            // loved songs
            if (columnContents[2])
            {
                lists.Add(lovedInfo);
                // get total
                int userResults2 =
                    Conversions.ToInteger(
                        Utilities.ParseMetadata(Utilities.CallAPI("user.getLovedTracks", textContents[0]), "total="));
                int pageAmount = (int)Math.Round(Math.Ceiling(userResults2 / 50d));
                var xmlPages = new List<string>();

                for (int currentPage = 0, loopTo3 = pageAmount - 1; currentPage <= loopTo3; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 80d + progress,
                        "Getting loved tracks... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    if (currentPage == pageAmount - 1)
                    {
                        // last page, only request leftover
                        if (userResults2 <= 50)
                            // if results below 50
                            xmlPages.Add(Utilities.CallAPI("user.getLovedTracks", textContents[0],
                                "page=" + (currentPage + 1), "limit=" + userResults2));
                        else
                            // if not below 50 get remainder
                            xmlPages.Add(Utilities.CallAPI("user.getLovedTracks", textContents[0],
                                "page=" + (currentPage + 1), "limit=50"));
                    }
                    else
                    {
                        xmlPages.Add(Utilities.CallAPI("user.getLovedTracks", textContents[0],
                            "page=" + (currentPage + 1), "limit=50"));
                    }

                    // cancel check
                    if (bgwUser.CancellationPending) return;
                }

                progress += 80;

                // add to list
                int currentPageAmount;
                string[] xmlNodes;
                for (int currentPage = 0, loopTo4 = pageAmount - 1; currentPage <= loopTo4; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 20d + progress,
                        "Parsing loved tracks... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                    currentPageAmount =
                        Conversions.ToInteger(Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                    // parse
                    for (int track = 0, loopTo5 = currentPageAmount - 1; track <= loopTo5; track++)
                    {
                        xmlNodes = new[] { "name", "artist/name", "date" };
                        Utilities.ParseXML(xmlPages[currentPage], "/lfm/lovedtracks/track", (uint)track, ref xmlNodes);
                        lovedInfo.Add(xmlNodes);

                        // cancel check
                        if (bgwUser.CancellationPending) return;
                    }
                }

                progress += 20;
            }

            // play history
            if (columnContents[3])
            {
                lists.Add(historyInfo);
                // get total
                int userResults2;
                if (userResults == 0)
                {
                    if (dateContents.Count == 0)
                        userResults2 = Conversions.ToInteger(
                            Utilities.ParseMetadata(Utilities.CallAPI("user.getRecentTracks", textContents[0]),
                                "total="));
                    else
                        userResults2 = Conversions.ToInteger(Utilities.ParseMetadata(
                            Utilities.CallAPI("user.getRecentTracks", textContents[0],
                                "from=" + Utilities.DateToUnix(dateContents[0]),
                                "to=" + Utilities.DateToUnix(dateContents[1])), "total="));
                }
                else
                {
                    userResults2 = userResults;
                }

                int pageAmount = (int)Math.Round(Math.Ceiling(userResults2 / 200d));
                var xmlPages = new List<string>();

                for (int currentPage = 0, loopTo6 = pageAmount - 1; currentPage <= loopTo6; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 80d + progress,
                        "Getting play history... (" + (currentPage + 1) * 200 + " of " + (pageAmount * 200) + ")");

                    if (dateContents.Count == 0)
                    {
                        if (currentPage == pageAmount - 1)
                        {
                            // last page, only request leftover
                            if (userResults2 <= 200)
                                // if results below 200
                                xmlPages.Add(Utilities.CallAPI("user.getRecentTracks", textContents[0],
                                    "page=" + (currentPage + 1), "limit=" + userResults2));
                            else
                                // if not below 200 get remainder
                                xmlPages.Add(Utilities.CallAPI("user.getRecentTracks", textContents[0],
                                    "page=" + (currentPage + 1), "limit=200"));
                        }
                        else
                        {
                            xmlPages.Add(Utilities.CallAPI("user.getRecentTracks", textContents[0],
                                "page=" + (currentPage + 1), "limit=200"));
                        }
                    }
                    else if (currentPage == pageAmount - 1)
                    {
                        // last page, only request leftover
                        if (userResults2 <= 200)
                            // if results below 200
                            xmlPages.Add(Utilities.CallAPI("user.getRecentTracks", textContents[0],
                                "page=" + (currentPage + 1), "limit=" + userResults2,
                                "from=" + Utilities.DateToUnix(dateContents[0]),
                                "to=" + Utilities.DateToUnix(dateContents[1])));
                        else
                            // if not below 200 get remainder
                            xmlPages.Add(Utilities.CallAPI("user.getRecentTracks", textContents[0],
                                "page=" + (currentPage + 1), "limit=200",
                                "from=" + Utilities.DateToUnix(dateContents[0]),
                                "to=" + Utilities.DateToUnix(dateContents[1])));
                    }
                    else
                    {
                        xmlPages.Add(Utilities.CallAPI("user.getRecentTracks", textContents[0],
                            "page=" + (currentPage + 1), "limit=200", "from=" + Utilities.DateToUnix(dateContents[0]),
                            "to=" + Utilities.DateToUnix(dateContents[1])));
                    }

                    // cancel check
                    if (bgwUser.CancellationPending) return;
                }

                progress += 80;

                // add to list
                int currentPageAmount;
                string[] xmlNodes;
                for (int currentPage = 0, loopTo7 = pageAmount - 1; currentPage <= loopTo7; currentPage++)
                {
                    // progress
                    UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 20d + progress,
                        "Parsing play history... (" + (currentPage + 1) * 200 + " of " + (pageAmount * 200) + ")");

                    if (currentPage == pageAmount - 1)
                        currentPageAmount = userResults2 % 200;
                    else
                        try
                        {
                            currentPageAmount =
                                Conversions.ToInteger(Utilities.ParseMetadata(xmlPages[currentPage], "perPage=")
                                    .Trim());
                        }
                        catch (Exception ex)
                        {
                            currentPageAmount = 200;
                        }


                    // parse
                    for (int track = 0, loopTo8 = currentPageAmount - 1; track <= loopTo8; track++)
                    {
                        xmlNodes = new[] { "name", "artist", "album", "date" };
                        Utilities.ParseXML(xmlPages[currentPage], "/lfm/recenttracks/track", (uint)track, ref xmlNodes);

                        // check for now playing
                        if (track == 0 && currentPage == 0 &&
                            xmlNodes[3].Contains("Object reference not set to an instance of an object"))
                            xmlNodes[3] = "Now Playing";

                        historyInfo.Add(xmlNodes);

                        // cancel check
                        if (bgwUser.CancellationPending) return;
                    }

                    // cancel check
                    if (bgwUser.CancellationPending) return;
                }

                progress += 20;
            }

            // charts
            if (columnContents[4])
            {
                lists.Add(topTrackInfo);
                lists.Add(topArtistInfo);
                lists.Add(topAlbumInfo);
                // results
                int topTrackResults = default, topArtistResults = default, topAlbumResults = default;
                if (userResults == 0)
                {
                    if (dateContents.Count == 0)
                    {
                        // if entire but no date
                        topTrackResults = Conversions.ToInteger(
                            Utilities.ParseMetadata(Utilities.CallAPI("user.getTopTracks", textContents[0]), "total="));
                        topArtistResults = Conversions.ToInteger(
                            Utilities.ParseMetadata(Utilities.CallAPI("user.getTopArtists", textContents[0]),
                                "total="));
                        topAlbumResults = Conversions.ToInteger(
                            Utilities.ParseMetadata(Utilities.CallAPI("user.getTopAlbums", textContents[0]), "total="));
                    }
                }
                else
                {
                    // if not entire 
                    topTrackResults = userResults;
                    topArtistResults = userResults;
                    topAlbumResults = userResults;
                }

                var pageAmount = default(int);
                var xmlPages = new List<string>();
                var xmlPage = default(string);

                #region Top Tracks

                if (dateContents.Count == 0)
                {
                    pageAmount = (int)Math.Round(Math.Ceiling(topTrackResults / 50d));

                    // no date, needs mutliple pages
                    for (int currentPage = 0, loopTo9 = pageAmount - 1; currentPage <= loopTo9; currentPage++)
                    {
                        // progress
                        UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 26.66d + progress,
                            "Getting user top tracks... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                        if (currentPage == pageAmount - 1)
                        {
                            // last page, only request leftover
                            if (topTrackResults <= 50)
                                // if results below 50
                                xmlPages.Add(Utilities.CallAPI("user.getTopTracks", textContents[0],
                                    "page=" + (currentPage + 1), "limit=" + topTrackResults));
                            else if (topTrackResults % 50 == 0)
                                // if no remainder
                                xmlPages.Add(Utilities.CallAPI("user.getTopTracks", textContents[0],
                                    "page=" + (currentPage + 1), "limit=50"));
                            else
                                // if not below 50 get remainder
                                xmlPages.Add(Utilities.CallAPI("user.getTopTracks", textContents[0],
                                    "page=" + (currentPage + 1), "limit=" + topTrackResults % 50));
                        }
                        else
                        {
                            xmlPages.Add(Utilities.CallAPI("user.getTopTracks", textContents[0],
                                "page=" + (currentPage + 1), "limit=50"));
                        }

                        // cancel check
                        if (bgwUser.CancellationPending) return;
                    }
                }
                else
                {
                    // date
                    // progress
                    UpdateProgress(false, progress, "Getting user top tracks...");

                    xmlPage = Utilities.CallAPI("user.getWeeklyTrackChart", textContents[0],
                        "from=" + Utilities.DateToUnix(dateContents[0]), "to=" + Utilities.DateToUnix(dateContents[1]));
                    // results
                    if (userResults == 0)
                        // if entire
                        topTrackResults = (int)Utilities.StrCount(xmlPage, "</track>");
                    else
                        // if not entire
                        topTrackResults = userResults;
                    xmlPages.Add(xmlPage);
                }

                // cancel check
                if (bgwUser.CancellationPending) return;

                progress = (int)Math.Round(progress + 26.66d);

                // add to list
                int currentPageAmount;
                string[] xmlNodes;
                if (dateContents.Count == 0)
                {
                    for (int currentPage = 0, loopTo10 = pageAmount - 1; currentPage <= loopTo10; currentPage++)
                    {
                        // progress
                        UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 6.66d + progress,
                            "Parsing user top tracks... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                        currentPageAmount =
                            Conversions.ToInteger(Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                        // parse
                        for (int track = 0, loopTo11 = currentPageAmount - 1; track <= loopTo11; track++)
                        {
                            xmlNodes = new[] { "name", "artist/name", "playcount" };
                            Utilities.ParseXML(xmlPages[currentPage], "/lfm/toptracks/track", (uint)track,
                                ref xmlNodes);

                            topTrackInfo.Add(xmlNodes);
                        }

                        // cancel check
                        if (bgwUser.CancellationPending) return;
                    }
                }
                else
                {
                    // progress
                    UpdateProgress(false, progress, "Parsing user top tracks...");

                    currentPageAmount = topTrackResults;

                    for (int track = 0, loopTo12 = currentPageAmount - 1; track <= loopTo12; track++)
                    {
                        xmlNodes = new[] { "name", "artist", "playcount" };
                        Utilities.ParseXML(xmlPage, "/lfm/weeklytrackchart/track", (uint)track, ref xmlNodes);

                        topTrackInfo.Add(xmlNodes);

                        // cancel check
                        if (bgwUser.CancellationPending) return;
                    }
                }

                progress = (int)Math.Round(progress + 6.67d);

                #endregion

                #region Top Artists

                xmlPages.Clear();
                if (dateContents.Count == 0)
                {
                    pageAmount = (int)Math.Round(Math.Ceiling(topArtistResults / 50d));

                    // no date, needs mutliple pages
                    for (int currentPage = 0, loopTo13 = pageAmount - 1; currentPage <= loopTo13; currentPage++)
                    {
                        // progress
                        UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 26.66d + progress,
                            "Getting user top artists... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) +
                            ")");

                        if (currentPage == pageAmount - 1)
                        {
                            // last page, only request leftover
                            if (topArtistResults <= 50)
                                // if results below 50
                                xmlPages.Add(Utilities.CallAPI("user.getTopArtists", textContents[0],
                                    "page=" + (currentPage + 1), "limit=" + topArtistResults));
                            else if (topArtistResults % 50 == 0)
                                // if no remainder
                                xmlPages.Add(Utilities.CallAPI("user.getTopArtists", textContents[0],
                                    "page=" + (currentPage + 1), "limit=50"));
                            else
                                // if not below 50 get remainder
                                xmlPages.Add(Utilities.CallAPI("user.getTopArtists", textContents[0],
                                    "page=" + (currentPage + 1), "limit=" + topArtistResults % 50));
                        }
                        else
                        {
                            xmlPages.Add(Utilities.CallAPI("user.getTopArtists", textContents[0],
                                "page=" + (currentPage + 1), "limit=50"));
                        }

                        // cancel check
                        if (bgwUser.CancellationPending) return;
                    }
                }
                else
                {
                    // date
                    // progress
                    UpdateProgress(false, progress, "Getting user top artists...");

                    xmlPage = Utilities.CallAPI("user.getWeeklyArtistChart", textContents[0],
                        "from=" + Utilities.DateToUnix(dateContents[0]), "to=" + Utilities.DateToUnix(dateContents[1]));
                    // results
                    if (userResults == 0)
                        // if entire
                        topArtistResults = (int)Utilities.StrCount(xmlPage, "</artist>");
                    else
                        // if not entire
                        topArtistResults = userResults;
                    xmlPages.Add(xmlPage);
                }

                // cancel check
                if (bgwUser.CancellationPending) return;

                progress = (int)Math.Round(progress + 26.66d);

                // add to list
                if (dateContents.Count == 0)
                {
                    for (int currentPage = 0, loopTo14 = pageAmount - 1; currentPage <= loopTo14; currentPage++)
                    {
                        // progress
                        UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 6.66d + progress,
                            "Parsing user top artists... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) +
                            ")");

                        currentPageAmount =
                            Conversions.ToInteger(Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                        // parse
                        for (int artist = 0, loopTo15 = currentPageAmount - 1; artist <= loopTo15; artist++)
                        {
                            xmlNodes = new[] { "name", "playcount" };
                            Utilities.ParseXML(xmlPages[currentPage], "/lfm/topartists/artist", (uint)artist,
                                ref xmlNodes);

                            topArtistInfo.Add(xmlNodes);
                        }

                        // cancel check
                        if (bgwUser.CancellationPending) return;

                        // cancel check
                        if (bgwUser.CancellationPending) return;
                    }
                }
                else
                {
                    // progress
                    UpdateProgress(false, progress, "Parsing user top artists...");

                    currentPageAmount = topArtistResults;

                    for (int artist = 0, loopTo16 = currentPageAmount - 1; artist <= loopTo16; artist++)
                    {
                        xmlNodes = new[] { "name", "playcount" };
                        Utilities.ParseXML(xmlPage, "/lfm/weeklyartistchart/artist", (uint)artist, ref xmlNodes);

                        topArtistInfo.Add(xmlNodes);

                        // cancel check
                        if (bgwUser.CancellationPending) return;
                    }
                }

                progress = (int)Math.Round(progress + 6.67d);

                #endregion

                #region Top Albums

                xmlPages.Clear();
                if (dateContents.Count == 0)
                {
                    pageAmount = (int)Math.Round(Math.Ceiling(topAlbumResults / 50d));

                    // no date, needs mutliple pages
                    for (int currentPage = 0, loopTo17 = pageAmount - 1; currentPage <= loopTo17; currentPage++)
                    {
                        // progress
                        UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 26.67d + progress,
                            "Getting user top albums... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                        if (currentPage == pageAmount - 1)
                        {
                            // last page, only request leftover
                            if (topAlbumResults <= 50)
                                // if results below 50
                                xmlPages.Add(Utilities.CallAPI("user.getTopAlbums", textContents[0],
                                    "page=" + (currentPage + 1), "limit=" + topAlbumResults));
                            else if (topAlbumResults % 50 == 0)
                                // if no remainder
                                xmlPages.Add(Utilities.CallAPI("user.getTopAlbums", textContents[0],
                                    "page=" + (currentPage + 1), "limit=50"));
                            else
                                // if not below 50 get remainder
                                xmlPages.Add(Utilities.CallAPI("user.getTopAlbums", textContents[0],
                                    "page=" + (currentPage + 1), "limit=" + topAlbumResults % 50));
                        }
                        else
                        {
                            xmlPages.Add(Utilities.CallAPI("user.getTopAlbums", textContents[0],
                                "page=" + (currentPage + 1), "limit=50"));
                        }

                        // cancel check
                        if (bgwUser.CancellationPending) return;
                    }
                }
                else
                {
                    // date
                    // progress
                    UpdateProgress(false, progress, "Getting user top albums...");

                    xmlPage = Utilities.CallAPI("user.getWeeklyAlbumChart", textContents[0],
                        "from=" + Utilities.DateToUnix(dateContents[0]), "to=" + Utilities.DateToUnix(dateContents[1]));
                    // results
                    if (userResults == 0)
                        // if entire
                        topAlbumResults = (int)Utilities.StrCount(xmlPage, "</album>");
                    else
                        // if not entire
                        topAlbumResults = userResults;
                    xmlPages.Add(xmlPage);
                }

                // cancel check
                if (bgwUser.CancellationPending) return;

                progress = (int)Math.Round(progress + 26.67d);

                // add to list
                if (dateContents.Count == 0)
                {
                    for (int currentPage = 0, loopTo18 = pageAmount - 1; currentPage <= loopTo18; currentPage++)
                    {
                        // progress
                        UpdateProgress(false, (currentPage + 1) / (double)pageAmount * 6.67d + progress,
                            "Parsing user top albums... (" + (currentPage + 1) * 50 + " of " + (pageAmount * 50) + ")");

                        currentPageAmount =
                            Conversions.ToInteger(Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                        // parse
                        for (int album = 0, loopTo19 = currentPageAmount - 1; album <= loopTo19; album++)
                        {
                            xmlNodes = new[] { "name", "artist/name", "playcount" };
                            Utilities.ParseXML(xmlPages[currentPage], "/lfm/topalbums/album", (uint)album,
                                ref xmlNodes);

                            topAlbumInfo.Add(xmlNodes);

                            // cancel check
                            if (bgwUser.CancellationPending) return;
                        }

                        // cancel check
                        if (bgwUser.CancellationPending) return;
                    }
                }
                else
                {
                    // progress
                    UpdateProgress(false, progress, "Parsing user top albums...");

                    currentPageAmount = topAlbumResults;

                    for (int album = 0, loopTo20 = currentPageAmount - 1; album <= loopTo20; album++)
                    {
                        xmlNodes = new[] { "name", "artist", "playcount" };
                        Utilities.ParseXML(xmlPage, "/lfm/weeklyalbumchart/album", (uint)album, ref xmlNodes);

                        topAlbumInfo.Add(xmlNodes);

                        // cancel check
                        if (bgwUser.CancellationPending) return;
                    }
                }

                progress = (int)Math.Round(progress + 6.67d);

                #endregion
            }

            Save(lists.ToArray());
            Invoke(new Action(() => StopOp()));
        }
        catch (Exception ex)
        {
            Invoke(new Action(() =>
                MessageBox.Show("ERROR: " + ex.Message, "User Backup", MessageBoxButtons.OK, MessageBoxIcon.Error)));
        }
    }

    private void UserUseCurrent(object sender, EventArgs e)
    {
        txtUserUser.Text = MySettingsProperty.Settings.User;
    }

    private void UserEnableDate(object sender, EventArgs e)
    {
        if (chkUserByDate.Checked)
        {
            dtpUserFrom.Enabled = true;
            dtpUserTo.Enabled = true;
        }
        else
        {
            dtpUserFrom.Enabled = false;
            dtpUserTo.Enabled = false;
        }
    }

    private void UserEnableAmount(object sender, EventArgs e)
    {
        if (chkUserHistory.Checked || chkUserCharts.Checked)
        {
            radUserEntire.Enabled = true;
            radUserNumber.Enabled = true;
            nudUserNumber.Enabled = true;
        }
        else
        {
            radUserEntire.Enabled = false;
            radUserNumber.Enabled = false;
            nudUserNumber.Enabled = false;
            radUserEntire.Checked = true;
        }
    }

    #endregion
}