using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Audiograph
{

    public partial class frmScrobbleIndexAddRow
    {
        public frmScrobbleIndexAddRow()
        {
            InitializeComponent();
        }
        private void Browse(object sender, EventArgs e)
        {
            var response = ofdBrowse.ShowDialog();
            string file = ofdBrowse.FileName;

            if (response == DialogResult.OK)
            {
                if (file.Contains(@":\") == true)
                {
                    txtFilename.Text = Utilities.GetFilename(file);
                }
                else
                {
                    MessageBox.Show("Valid file not detected", "Browse for File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Search(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmScrobbleIndexTrackAdvanced.Show();
        }

        private void Verify(object sender, EventArgs e)
        {
            // check that there is something in the boxes
            if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtArtist.Text))
            {
                MessageBox.Show("Valid data must be entered in both the Track and Artist fields", "Add Row", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // verify
            string[] info = Utilities.VerifyTrack(txtTitle.Text.Trim(), txtArtist.Text.Trim());
            // if cannot be found
            if (info[0].Contains("ERROR: ") == true)
            {
                MessageBox.Show("Track was unable to be verified", "Add Row", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else // if was found
            {
                MessageBox.Show("Track verified as " + info[0] + " by " + info[1], "Add Row", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTitle.Text = info[0];
                txtArtist.Text = info[1];
                txtAlbum.Text = info[2];
            }
        }

        private void OK(object sender, EventArgs e)
        {
            // check for missing data
            if (string.IsNullOrEmpty(txtFilename.Text) || string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtArtist.Text))
            {
                MessageBox.Show("Valid data must be entered into the File, Title, and Artist fields", "Add Row", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            My.MyProject.Forms.frmScrobbleIndexEditor.dgvData.Rows.Add(new[] { txtFilename.Text.Trim(), txtTitle.Text.Trim(), txtArtist.Text.Trim(), txtAlbum.Text.Trim() });
            My.MyProject.Forms.frmScrobbleIndexEditor.Saved(false);
            Close();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }

        private void FormClose(object sender, CancelEventArgs e)
        {
            My.MyProject.Forms.frmScrobbleIndexTrackAdvanced.Close();
        }
    }
}