using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using WMPLib;

namespace Audiograph
{
    public partial class frmAddToQueue
    {
        public frmAddToQueue()
        {
            InitializeComponent();
        }
        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }

        private void OK(object sender, EventArgs e)
        {
            // if there is nothing in box just close the form
            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                Close();
                return;
            }

            // get initial amount of queue items for later
            int queueitems = My.MyProject.Forms.frmMain.ltvMediaQueue.Items.Count;

            // get array of files
            string[] names = txtLocation.Text.Split('|');
            // loop through files to check for invalid
            foreach (string val in names)
            {
                if (val.Contains(@":\") == false && val.Contains("http://") == false && val.Contains("https://") == false)
                {
                    MessageBox.Show("The location entered does not appear to be a valid filesystem location or URL.", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // add each element to queue
            foreach (string val in names)
            {
                // only add file name if its a filesystem location, add entire url if its a url
                if (val.Contains(@":\") == true)
                {
                    // determine file type
                    if (val.ToLower().Contains(".mp3") == true || val.ToLower().Contains(".aac") == true || val.ToLower().Contains(".flac") == true || val.ToLower().Contains(".wav") == true || val.ToLower().Contains(".wma") == true || val.ToLower().Contains(".m4a") == true || val.ToLower().Contains(".mid") == true)    // audio types
                    {
                        My.MyProject.Forms.frmMain.ltvMediaQueue.Items.Add(" - " + val.Substring(Strings.InStrRev(val, @"\"))).ImageIndex = 0;
                    }
                    else if (val.ToLower().Contains(".mp4") == true || val.ToLower().Contains(".mov") == true || val.ToLower().Contains(".mpeg") == true || val.ToLower().Contains(".mpg") == true || val.ToLower().Contains(".avi") == true || val.ToLower().Contains(".wmv") == true) // video types
                    {
                        My.MyProject.Forms.frmMain.ltvMediaQueue.Items.Add(" - " + val.Substring(Strings.InStrRev(val, @"\"))).ImageIndex = 1;
                    }
                    else
                    {
                        // anything else
                        My.MyProject.Forms.frmMain.ltvMediaQueue.Items.Add(" - " + val.Substring(Strings.InStrRev(val, @"\"))).ImageIndex = 2;
                    }
                }
                else
                {
                    // link
                    My.MyProject.Forms.frmMain.ltvMediaQueue.Items.Add((My.MyProject.Forms.frmMain.ltvMediaQueue.Items.Count + 1).ToString() + " - " + val).ImageIndex = 3;
                }
                // add subitem with full location
                My.MyProject.Forms.frmMain.ltvMediaQueue.Items[My.MyProject.Forms.frmMain.ltvMediaQueue.Items.Count - 1].SubItems.Add(val);
            }

            // shuffle crap
            if (My.MyProject.Forms.frmMain.chkMediaShuffle.Checked == true)
            {
                Utilities.addingqueue = true;
                My.MyProject.Forms.frmMain.chkMediaShuffle.Checked = false;
            }

            // begin playing if nothing is in queue or in the player, only recount if not
            if (queueitems < 1 && My.MyProject.Forms.frmMain.MediaPlayer.playState != WMPPlayState.wmppsPlaying && My.MyProject.Forms.frmMain.MediaPlayer.playState != WMPPlayState.wmppsPaused)
            {
                My.MyProject.Forms.frmMain.QueuePlay(0);
            }
            else
            {
                My.MyProject.Forms.frmMain.QueueRecount();
            }

            // close form
            Close();
        }

        private void Browse(object sender, EventArgs e)
        {
            OpenFileDialog.ShowDialog();
            string[] names = OpenFileDialog.FileNames;
            // put item(s) in text box separated by quotation marks and a space
            bool first = false;

            if (names.Length > 1)    // dont run the first time to prevent | from being inserted at the start
            {
                foreach (string val in names)
                {
                    if (first == true)
                    {
                        // this will run after the first time
                        txtLocation.Text += "|" + val;
                    }
                    else
                    {
                        // this will run the first time
                        txtLocation.Text = val;
                    }
                    first = true;
                }
            }
            else if (names.Length == 1)
            {
                // just set the plain text if only one item was selected
                txtLocation.Text = names[0];
            }
        }
    }
}