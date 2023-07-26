using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Audiograph.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Audiograph;

public partial class frmAlbumAdvanced
{
    public frmAlbumAdvanced()
    {
        InitializeComponent();
    }

    private void Search(object sender, EventArgs e)
    {
        // end if no data has been entered in album box
        if (string.IsNullOrEmpty(txtSearchAlbum.Text))
        {
            MessageBox.Show("Please make sure you have entered data in the album search field.", "Advanced Search",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // get search results
        string results;
        if (string.IsNullOrEmpty(txtSearchArtist.Text))
            results = Utilities.CallAPI("album.search", "", "album=" + txtSearchAlbum.Text.Trim().Replace(" ", "+"));
        else
            results = Utilities.CallAPI("album.search", "", "album=" + txtSearchAlbum.Text.Trim().Replace(" ", "+"),
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
                string[] searchnodes = { "name", "artist" };
                Utilities.ParseXML(results, "/lfm/results/albummatches/album", count, ref searchnodes);

                // add to listview
                if (searchnodes[0].Contains("ERROR: ") == false)
                {
                    ltvResults.Items.Add(searchnodes[0]); // name
                    ltvResults.Items[count].SubItems.Add(searchnodes[1]); // artist
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
                "Search: " + txtSearchArtist.Text + " - " + txtSearchAlbum.Text, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            txtSearchAlbum.Select();
        }
    }

    // put selected search item into final boxes
    private void SelectSearchItem(object sender, ListViewItemSelectionChangedEventArgs e)
    {
        if (ltvResults.SelectedItems.Count > 0)
        {
            txtAlbum.Text = ltvResults.SelectedItems[0].Text;
            txtArtist.Text = ltvResults.SelectedItems[0].SubItems[1].Text;
            txtTracks.Text = "Loading...";
            txtListeners.Text = "Loading...";
            picArt.Image = picArt.InitialImage;

            // start new thread and get album playcount and art
            string responseXML;
            var th = new Thread(() =>
            {
                // get xml
                string album = default, artist = default;
                Invoke(new Action(() =>
                {
                    album = ltvResults.SelectedItems[0].SubItems[0].Text;
                    artist = ltvResults.SelectedItems[0].SubItems[1].Text;
                }));
                responseXML = Utilities.CallAPI("album.getInfo", MySettingsProperty.Settings.User, "album=" + album,
                    "artist=" + artist);

                // parse data
                string[] albumnodes = { "name", "artist", "listeners" };
                Utilities.ParseXML(responseXML, "/lfm/album", 0U, ref albumnodes);
                try
                {
                    albumnodes[2] = Conversions.ToInteger(albumnodes[2]).ToString("N0");
                }
                catch (Exception ex)
                {
                    albumnodes[2] = "Number Error";
                }

                // get tracks
                ushort tracks = (ushort)Utilities.StrCount(responseXML, "track rank=");

                // prevent thread window mismatch garbage from people hitting ok too fast
                if (Visible)
                {
                    // set data
                    Invoke(new Action(() =>
                    {
                        // album name
                        if (albumnodes[0].Contains("ERROR: ") == false) txtAlbum.Text = albumnodes[0];
                        // artist name
                        if (albumnodes[1].Contains("ERROR: ") == false) txtArtist.Text = albumnodes[1];
                        // listeners
                        txtListeners.Text = albumnodes[2];
                        // tracks
                        if (tracks > 0)
                            txtTracks.Text = tracks.ToString("N0");
                        else
                            txtTracks.Text = "N/A";
                    }));

                    // set image
                    string imageURL = Utilities.ParseImage(responseXML, "/lfm/album/image", 2);
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
            th.Name = "AlbumSearch";
            th.Start();
        }
    }

    private void MBID(object sender, EventArgs e)
    {
        // call api
        string info = Utilities.CallAPI("album.getInfo", MySettingsProperty.Settings.User,
            "mbid=" + txtMBID.Text.Trim().ToLower());

        // get info or error
        string[] infonodes = { "name", "artist", "listeners" };
        Utilities.ParseXML(info, "/lfm/album", 0U, ref infonodes);
        // get tracks
        ushort tracks = (ushort)Utilities.StrCount(info, "track rank=");
        // check for error
        if (infonodes[0].Contains("ERROR: ") == false)
        {
            // image
            picArt.Image = picArt.InitialImage;
            picArt.LoadAsync(Utilities.ParseImage(info, "/lfm/album/image", 2));
            // album name
            txtAlbum.Text = infonodes[0];
            // artist name
            txtArtist.Text = infonodes[1];
            // listeners
            if (infonodes[2].Contains("ERROR: ") == false)
                try
                {
                    txtListeners.Text = Conversions.ToInteger(infonodes[2]).ToString("N0");
                }
                catch (Exception ex)
                {
                    txtListeners.Text = "Number Error";
                }

            // tracks
            if (tracks > 0)
                txtTracks.Text = tracks.ToString("N0");
            else
                txtTracks.Text = "N/A";
        }
        else
        {
            MessageBox.Show("No album was able to be found from this MBID, please try again.",
                "MBID: " + txtMBID.Text.Trim().ToLower(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtMBID.Select();
        }
    }

    // put final box data into main form
    private void OK(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtAlbum.Text) && !string.IsNullOrEmpty(txtArtist.Text) && txtArtist.Text != "N/A" &&
            txtArtist.Text != "N/A") // if there is data in both boxes
        {
            Utilities.GoToAlbum(txtAlbum.Text.Trim(), txtArtist.Text.Trim());
            Close(); // close window
        }
        else if (string.IsNullOrEmpty(txtTracks.Text) &&
                 !string.IsNullOrEmpty(txtArtist.Text)) // if there is only data in album
        {
            MessageBox.Show("Please make sure you have entered data into the Album field.", "Advanced Search",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else if (string.IsNullOrEmpty(txtArtist.Text) &&
                 !string.IsNullOrEmpty(txtTracks.Text)) // if there is only data in artist
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

    private void UserType(object sender, KeyEventArgs e)
    {
        if (ltvResults.SelectedItems.Count == 0) return;

        // clear all info
        ltvResults.SelectedItems.Clear();
        ((TextBox)sender).Text = string.Empty;
        if (((TextBox)sender).Name == "txtAlbum")
            txtArtist.Clear();
        else
            txtAlbum.Clear();
        txtListeners.Text = "N/A";
        txtTracks.Text = "N/A";
        picArt.Image = picArt.InitialImage;
        picArt.ImageLocation = string.Empty;
    }
}