﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Audiograph.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Audiograph;

public partial class frmScrobbleSearch
{
    public frmScrobbleSearch()
    {
        InitializeComponent();
    }

    private void Search(object sender, EventArgs e)
    {
        // end if no data has been entered in track box
        if (string.IsNullOrEmpty(txtSearchTrack.Text))
        {
            MessageBox.Show("Please make sure you have entered data in the track search field.", "Advanced Search",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // get search results
        string results;
        if (string.IsNullOrEmpty(txtSearchArtist.Text))
            results = Utilities.CallAPI("track.search", "", "track=" + txtSearchTrack.Text.Trim().Replace(" ", "+"));
        else
            results = Utilities.CallAPI("track.search", "", "track=" + txtSearchTrack.Text.Trim().Replace(" ", "+"),
                "artist=" + txtSearchArtist.Text.Trim().Replace(" ", "+"));

        // remove opensearch garbage
        if (results.Contains("opensearch"))
        {
            uint startindex = (uint)(Strings.InStr(results, "<opensearch:Query") - 1); // find starting index
            uint endindex = (uint)(Strings.InStr(results, "</opensearch:itemsPerPage>") + 25); // find ending index
            results = results.Remove((int)startindex,
                (int)(endindex - startindex)); // remove (get number of chars between start and end index)
        }

        // check for errors/populate listview
        ltvResults.Items.Clear();
        // get status
        string status = Utilities.ParseMetadata(results, "lfm status=");
        if (status.Contains("ok"))
        {
            for (byte count = 0; count <= 29; count++)
            {
                // parse for data
                string[] searchnodes = { "name", "artist", "listeners" };
                Utilities.ParseXML(results, "/lfm/results/trackmatches/track", count, ref searchnodes);

                // add to listview
                if (searchnodes[0].Contains("ERROR: ") == false)
                {
                    ltvResults.Items.Add(searchnodes[0]); // name
                    ltvResults.Items[count].SubItems.Add(searchnodes[1]); // artist
                    ltvResults.Items[count].SubItems
                        .Add(Conversions.ToUInteger(searchnodes[2]).ToString("N0")); // listeners
                }
            }
        }
        else // if errors
        {
            MessageBox.Show("API ERROR: Cannot retrieve search results.", "Advanced Search", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        // display message if no results found
        if (ltvResults.Items.Count == 0)
        {
            MessageBox.Show("No results found, please alter your search and try again.",
                "Search: " + txtSearchArtist.Text + " - " + txtSearchTrack.Text, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            txtSearchTrack.Select();
        }
    }

    // put selected search item into final boxes
    private void SelectSearchItem(object sender, ListViewItemSelectionChangedEventArgs e)
    {
        if (ltvResults.SelectedItems.Count > 0)
        {
            txtTrack.Text = ltvResults.SelectedItems[0].Text;
            txtArtist.Text = ltvResults.SelectedItems[0].SubItems[1].Text;
            txtAlbum.Text = "Loading...";
            txtPlaycount.Text = "Loading...";
            picArt.Image = picArt.InitialImage;

            // start new thread and get album playcount and art
            string responseXML;
            var th = new Thread(() =>
            {
                // get xml
                string track = default, artist = default;
                Invoke(new Action(() =>
                {
                    track = ltvResults.SelectedItems[0].SubItems[0].Text;
                    artist = ltvResults.SelectedItems[0].SubItems[1].Text;
                }));
                responseXML = Utilities.CallAPI("track.getInfo", MySettingsProperty.Settings.User, "track=" + track,
                    "artist=" + artist);

                // parse data
                string[] tracknodes = { "name", "artist/name", "album/title", "userplaycount" };
                Utilities.ParseXML(responseXML, "/lfm/track", 0U, ref tracknodes);
                if (tracknodes[2].Contains("ERROR: ")) tracknodes[2] = "N/A";
                try
                {
                    tracknodes[3] = Conversions.ToInteger(tracknodes[3]).ToString("N0");
                }
                catch (Exception ex)
                {
                    tracknodes[3] = "Number Error";
                }

                // set data
                if (Visible)
                {
                    Invoke(new Action(() =>
                    {
                        if (tracknodes[0].Contains("ERROR: ") == false) txtTrack.Text = tracknodes[0];
                        if (tracknodes[1].Contains("ERROR: ") == false) txtArtist.Text = tracknodes[1];
                        txtAlbum.Text = tracknodes[2];
                        txtPlaycount.Text = tracknodes[3];
                    }));

                    // set image
                    string imageURL = Utilities.ParseImage(responseXML, "/lfm/track/album/image", 2);
                    if (imageURL.Contains("ERROR: ") == false)
                        Invoke(new Action(() =>
                        {
                            try
                            {
                                picArt.LoadAsync(imageURL);
                            }
                            catch (Exception ex)
                            {
                                picArt.Image = picArt.InitialImage;
                            }
                        }));
                    else
                        picArt.Image = picArt.ErrorImage;
                }
            });
            th.Name = "ScrobbleSearch";
            th.Start();
        }
    }

    private void MBID(object sender, EventArgs e)
    {
        // call api
        string info = Utilities.CallAPI("track.getInfo", MySettingsProperty.Settings.User,
            "mbid=" + txtMBID.Text.Trim().ToLower());

        // get info or error
        string[] infonodes = { "name", "artist/name", "album/title", "userplaycount" };
        Utilities.ParseXML(info, "/lfm/track", 0U, ref infonodes);
        // check for error
        if (infonodes[0].Contains("ERROR: ") == false)
        {
            picArt.Image = picArt.InitialImage;
            picArt.LoadAsync(Utilities.ParseImage(info, "/lfm/track/album/image", 2));
            txtTrack.Text = infonodes[0];
            txtArtist.Text = infonodes[1];
            if (infonodes[2].Contains("ERROR: ") == false)
                txtAlbum.Text = infonodes[2];
            else
                txtAlbum.Text = "N/A";
            if (infonodes[3].Contains("ERROR: ") == false)
                try
                {
                    txtPlaycount.Text = Conversions.ToInteger(infonodes[3]).ToString("N0");
                }
                catch (Exception ex)
                {
                    txtPlaycount.Text = "Number Error";
                }
        }

        else
        {
            MessageBox.Show("No track was able to be found from this MBID, please try again.",
                "MBID: " + txtMBID.Text.Trim().ToLower(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtMBID.Select();
        }
    }

    // put final box data into main form
    private void OK(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtTrack.Text) &&
            !string.IsNullOrEmpty(txtArtist.Text)) // if there is data in both boxes
        {
            if (txtTrack.Text != "N/A" && txtTrack.Text.Contains("ERROR: ") == false)
                MyProject.Forms.frmMain.txtMediaTitle.Text = txtTrack.Text.Trim();
            else
                MyProject.Forms.frmMain.txtMediaTitle.Text = string.Empty;
            if (txtArtist.Text != "N/A" && txtArtist.Text.Contains("ERROR: ") == false)
                MyProject.Forms.frmMain.txtMediaArtist.Text = txtArtist.Text.Trim();
            else
                MyProject.Forms.frmMain.txtMediaArtist.Text = string.Empty;
            if (txtAlbum.Text != "N/A" && txtAlbum.Text.Contains("ERROR: ") == false &&
                txtAlbum.Text.Contains("Loading") == false)
                MyProject.Forms.frmMain.txtMediaAlbum.Text = txtAlbum.Text.Trim();
            else
                MyProject.Forms.frmMain.txtMediaAlbum.Text = string.Empty;
            Close(); // close window
        }
        else if (string.IsNullOrEmpty(txtTrack.Text) &&
                 !string.IsNullOrEmpty(txtArtist.Text)) // if there is only data in track
        {
            MessageBox.Show("Please make sure you have entered data into the Track field.", "Advanced Search",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else if (string.IsNullOrEmpty(txtArtist.Text) &&
                 !string.IsNullOrEmpty(txtTrack.Text)) // if there is only data in artist
        {
            MessageBox.Show("Please make sure you have entered data into the Artist field.", "Advanced Search",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else // if there is no data entered
        {
            Close();
        }
    }

    private void Cancel(object sender, EventArgs e)
    {
        Close();
    }

    private void UserType(object sender, KeyEventArgs e)
    {
        if (ltvResults.SelectedItems.Count == 0) return;

        // clear all info
        ltvResults.SelectedItems.Clear();
        ((TextBox)sender).Text = string.Empty;
        if (((TextBox)sender).Name == "txtTrack")
            txtArtist.Clear();
        else
            txtTrack.Clear();
        txtAlbum.Text = "N/A";
        txtPlaycount.Text = "N/A";
        picArt.Image = picArt.InitialImage;
        picArt.ImageLocation = string.Empty;
    }

    private void ArtClicked(object sender, EventArgs e)
    {
        if (Conversions.ToBoolean(
                Operators.ConditionalCompareObjectEqual(((PictureBox)sender).ImageLocation.Contains("http"), true,
                    false))) Process.Start(((PictureBox)sender).ImageLocation);
    }

    private void DoubleClickItem(object sender, EventArgs e)
    {
        btnOK.PerformClick();
    }
}