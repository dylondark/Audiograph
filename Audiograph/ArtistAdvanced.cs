using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Audiograph;

public partial class frmArtistAdvanced
{
    public frmArtistAdvanced()
    {
        InitializeComponent();
    }

    private void Search(object sender, EventArgs e)
    {
        // end if no data has been entered in track box
        if (string.IsNullOrEmpty(txtSearchArtist.Text))
        {
            MessageBox.Show("Please make sure you have entered data in the artist search field.", "Advanced Search",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // get search results
        string results =
            Utilities.CallAPI("artist.search", "", "artist=" + txtSearchArtist.Text.Trim().Replace(" ", "+"));

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
                string[] searchnodes = { "name", "listeners" };
                Utilities.ParseXML(results, "/lfm/results/artistmatches/artist", count, ref searchnodes);

                // add to listview
                if (searchnodes[0].Contains("ERROR: ") == false)
                {
                    ltvResults.Items.Add(searchnodes[0]); // name
                    ltvResults.Items[count].SubItems
                        .Add(Conversions.ToUInteger(searchnodes[1]).ToString("N0")); // listeners
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
                "Search: " + txtSearchArtist.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtSearchArtist.Select();
        }
    }

    // put selected search item into final boxes
    private void SelectSearchItem(object sender, ListViewItemSelectionChangedEventArgs e)
    {
        if (ltvResults.SelectedItems.Count > 0)
        {
            txtArtist.Text = ltvResults.SelectedItems[0].Text;
            txtListeners.Text = ltvResults.SelectedItems[0].SubItems[1].Text;
        }
    }

    private void MBID(object sender, EventArgs e)
    {
        // call api
        string info = Utilities.CallAPI("artist.getInfo", "", "mbid=" + txtMBID.Text.Trim().ToLower());

        // get info or error
        string[] infonodes = { "name", "stats/listeners" };
        Utilities.ParseXML(info, "/lfm/artist", 0U, ref infonodes);
        // check for error
        if (infonodes[0].Contains("ERROR: ") == false && infonodes[0].Contains("ERROR: ") == false)
        {
            txtArtist.Text = infonodes[0];
            try
            {
                txtListeners.Text = Conversions.ToInteger(infonodes[1]).ToString("N0");
            }
            catch (Exception ex)
            {
                txtListeners.Text = "Number Error";
            }
        }
        else
        {
            MessageBox.Show("No artist was able to be found from this MBID, please try again.",
                "MBID: " + txtMBID.Text.Trim().ToLower(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtMBID.Select();
        }
    }

    // put final box data into main form
    private void OK(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtArtist.Text) && txtArtist.Text != "N/A") // if there is data in artist box
        {
            Utilities.GoToArtist(txtArtist.Text.Trim());
            Close(); // close window
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
        txtListeners.Text = "N/A";
        ((TextBox)sender).Clear();
    }

    private void DoubleClickItem(object sender, EventArgs e)
    {
        btnOK.PerformClick();
    }
}