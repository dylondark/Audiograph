using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Audiograph
{

    public partial class frmTrackAdvanced
    {
        public frmTrackAdvanced()
        {
            InitializeComponent();
        }
        private void Search(object sender, EventArgs e)
        {
            // end if no data has been entered in track box
            if (string.IsNullOrEmpty(txtSearchTrack.Text))
            {
                MessageBox.Show("Please make sure you have entered data in the track search field.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // get search results
            string results;
            if (string.IsNullOrEmpty(txtSearchArtist.Text))
            {
                results = Utilities.CallAPI("track.search", "", "track=" + txtSearchTrack.Text.Trim().Replace(" ", "+"));
            }
            else
            {
                results = Utilities.CallAPI("track.search", "", "track=" + txtSearchTrack.Text.Trim().Replace(" ", "+"), "artist=" + txtSearchArtist.Text.Trim().Replace(" ", "+"));
            }

            // remove opensearch garbage
            if (results.Contains("opensearch") == true)
            {
                uint startindex = (uint)(Strings.InStr(results, "<opensearch:Query") - 1);            // find starting index
                uint endindex = (uint)(Strings.InStr(results, "</opensearch:itemsPerPage>") + 25);    // find ending index
                results = results.Remove((int)startindex, (int)(endindex - startindex));                     // remove (get number of chars between start and end index)
            }

            // check for errors/populate listview
            ltvResults.Items.Clear();
            // get status
            string status = Utilities.ParseMetadata(results, "lfm status=");
            if (status.Contains("ok") == true)
            {
                for (byte count = 0; count <= 29; count++)
                {
                    // parse for data
                    string[] searchnodes = new string[] { "name", "artist", "listeners" };
                    Utilities.ParseXML(results, "/lfm/results/trackmatches/track", count, ref searchnodes);

                    // add to listview
                    if (searchnodes[0].Contains("ERROR: ") == false)
                    {
                        ltvResults.Items.Add(searchnodes[0]);                    // name
                        ltvResults.Items[count].SubItems.Add(searchnodes[1]);    // artist
                        ltvResults.Items[count].SubItems.Add(Conversions.ToUInteger(searchnodes[2]).ToString("N0"));  // listeners
                    }
                }
            }
            else    // if errors
            {
                MessageBox.Show("API ERROR: Cannot retrieve search results.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // display message if no results found
            if (ltvResults.Items.Count == 0)
            {
                MessageBox.Show("No results found, please alter your search and try again.", "Search: " + txtSearchArtist.Text + " - " + txtSearchTrack.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                txtListeners.Text = ltvResults.SelectedItems[0].SubItems[2].Text;
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
                        responseXML = Utilities.CallAPI("track.getInfo", My.MySettingsProperty.Settings.User, "track=" + track, "artist=" + artist);

                        // parse data
                        string[] tracknodes = new[] { "name", "artist/name", "album/title", "listeners" };
                        Utilities.ParseXML(responseXML, "/lfm/track", 0U, ref tracknodes);
                        if (tracknodes[2].Contains("ERROR: ") == true)
                        {
                            tracknodes[2] = "N/A";
                        }
                        try
                        {
                            tracknodes[3] = Conversions.ToInteger(tracknodes[3]).ToString("N0");
                        }
                        catch (Exception ex)
                        {
                            tracknodes[3] = "Number Error";
                        }

                        // prevent thread window mismatch garbage from people hitting ok too fast
                        if (Visible == true)
                        {
                            // set data
                            Invoke(new Action(() =>
       {
                                if (tracknodes[0].Contains("ERROR: ") == false)
                                {
                                    txtTrack.Text = tracknodes[0];
                                }
                                if (tracknodes[1].Contains("ERROR: ") == false)
                                {
                                    txtArtist.Text = tracknodes[1];
                                }
                                txtAlbum.Text = tracknodes[2];
                            }));

                            // set image
                            string imageURL = Utilities.ParseImage(responseXML, "/lfm/track/album/image", 2);
                            if (imageURL.Contains("ERROR: ") == false)
                            {
                                Invoke(new Action(() => { try { picArt.LoadAsync(imageURL); } catch (Exception ex) { picArt.Image = picArt.InitialImage; } }));
                            }
                            else
                            {
                                picArt.Image = picArt.ErrorImage;
                            }
                        }
                    });
                th.Name = "TrackSearch";
                th.Start();
            }
        }

        private void MBID(object sender, EventArgs e)
        {
            // call api
            string info = Utilities.CallAPI("track.getInfo", My.MySettingsProperty.Settings.User, "mbid=" + txtMBID.Text.Trim().ToLower());

            // get info or error
            string[] infonodes = new string[] { "name", "artist/name", "album/title", "listeners" };
            Utilities.ParseXML(info, "/lfm/track", 0U, ref infonodes);
            // check for error
            if (infonodes[0].Contains("ERROR: ") == false)
            {
                picArt.Image = picArt.InitialImage;
                picArt.LoadAsync(Utilities.ParseImage(info, "/lfm/track/album/image", 2));
                txtTrack.Text = infonodes[0];
                txtArtist.Text = infonodes[1];
                if (infonodes[2].Contains("ERROR: ") == false)
                {
                    txtAlbum.Text = infonodes[2];
                }
                else
                {
                    txtAlbum.Text = "N/A";
                }
                if (infonodes[3].Contains("ERROR: ") == false)
                {
                    try
                    {
                        txtListeners.Text = Conversions.ToInteger(infonodes[3]).ToString("N0");
                    }
                    catch (Exception ex)
                    {
                        txtListeners.Text = "Number Error";
                    }
                }
            }

            else
            {
                MessageBox.Show("No track was able to be found from this MBID, please try again.", "MBID: " + txtMBID.Text.Trim().ToLower(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMBID.Select();
            }
        }

        // put final box data into main form
        private void OK(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTrack.Text) && !string.IsNullOrEmpty(txtArtist.Text) && txtTrack.Text != "N/A" && txtArtist.Text != "N/A")    // if there is data in both boxes
            {
                Utilities.GoToTrack(txtTrack.Text.Trim(), txtArtist.Text.Trim());
                Close();                                      // close window
            }
            else if (string.IsNullOrEmpty(txtTrack.Text) && !string.IsNullOrEmpty(txtArtist.Text)) // if there is only data in track
            {
                MessageBox.Show("Please make sure you have entered data into the Track field.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(txtArtist.Text) && !string.IsNullOrEmpty(txtTrack.Text)) // if there is only data in artist
            {
                MessageBox.Show("Please make sure you have entered data into the Artist field.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else                                                                            // if there is no data entered
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
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(sender.ImageLocation.Contains("http"), true, false)))
            {
                Process.Start(sender.ImageLocation);
            }
        }

        private void UserType(TextBox sender, KeyEventArgs e)
        {
            if (ltvResults.SelectedItems.Count == 0)
            {
                return;
            }

            // clear all info
            ltvResults.SelectedItems.Clear();
            sender.Text = string.Empty;
            if (sender.Name == "txtTrack")
            {
                txtArtist.Clear();
            }
            else
            {
                txtTrack.Clear();
            }
            txtAlbum.Text = "N/A";
            txtListeners.Text = "N/A";
            picArt.Image = picArt.InitialImage;
            picArt.ImageLocation = string.Empty;
        }

        private void DoubleClickItem(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }
    }
}