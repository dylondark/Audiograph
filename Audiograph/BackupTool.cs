using System;
using System.Collections;
using System.Collections.Generic;
using global::System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using global::System.IO;
using System.Linq;
using global::System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using WMPLib;

namespace Audiograph
{

    public partial class frmBackupTool
    {
        // 0 = chart, 1 = tag, 2 = track, etc
        private global::System.Byte section;
        // contains text box contents for the selected content page
        private global::System.Collections.Generic.List<global::System.String> textContents = new global::System.Collections.Generic.List<global::System.String>();
        // contains values of checkboxes on the selected content page from top to bottom
        private global::System.Collections.Generic.List<global::System.Boolean> columnContents = new global::System.Collections.Generic.List<global::System.Boolean>();
        // contains value of the currently selected country on the chart page (empty for worldwide)
        private global::System.String chartCountry;
        // contains values of the datetimepickers on the user content page
        private global::System.Collections.Generic.List<global::System.DateTime> dateContents = new global::System.Collections.Generic.List<global::System.DateTime>();
        // contains value of the number of results (0 for entire)
        private global::System.Int32 chartResults;
        private global::System.Int32 tagResults;
        private global::System.Int32 trackResults;
        private global::System.Int32 artistResults;
        private global::System.Int32 albumResults;
        private global::System.Int32 userResults;
        private global::System.Byte progressMultiplier;

        public frmBackupTool()
        {
            InitializeComponent();
        }

        #region UI
        private void FrmLoad(global::System.Object sender, global::System.EventArgs e)
        {
            // automatically switch to and fill in data from current tab
            switch (global::Audiograph.My.MyProject.Forms.frmMain.tabControl.SelectedIndex)
            {
                case 0:
                    {
                        // charts tab
                        this.cmbContents.SelectedIndex = 0;
                        // select worldwide or country
                        if (((global::Audiograph.My.MyProject.Forms.frmMain.radChartWorldwide.Checked) == (true)))
                        {
                            this.radChartWorldwide.Checked = true;
                        }
                        else
                        {
                            this.radChartCountry.Checked = true;
                        }
                        // fill in country
                        this.cmbChartCountry.SelectedIndex = global::Audiograph.My.MyProject.Forms.frmMain.cmbChartCountry.SelectedIndex;
                        ChartEnableCountries(null, null);
                        break;
                    }
                case 1:
                    {
                        // tag tab
                        this.cmbContents.SelectedIndex = 1;
                        this.txtTagTag.Text = global::Audiograph.My.MyProject.Forms.frmMain.txtSearch.Text;
                        break;
                    }
                case 2:
                    {
                        // track tab
                        this.cmbContents.SelectedIndex = 2;
                        this.txtTrackTitle.Text = global::Audiograph.My.MyProject.Forms.frmMain.txtTrackTitle.Text;
                        this.txtTrackArtist.Text = global::Audiograph.My.MyProject.Forms.frmMain.txtTrackArtist.Text;
                        break;
                    }
                case 3:
                    {
                        // artist tab
                        this.cmbContents.SelectedIndex = 3;
                        this.txtArtistArtist.Text = global::Audiograph.My.MyProject.Forms.frmMain.txtArtistName.Text;
                        break;
                    }
                case 4:
                    {
                        // album tab
                        this.cmbContents.SelectedIndex = 4;
                        this.txtAlbumAlbum.Text = global::Audiograph.My.MyProject.Forms.frmMain.txtAlbumTitle.Text;
                        this.txtAlbumArtist.Text = global::Audiograph.My.MyProject.Forms.frmMain.txtAlbumArtist.Text;
                        break;
                    }
                case 5:
                    {
                        // user tab
                        this.cmbContents.SelectedIndex = 5;
                        this.txtUserUser.Text = global::Audiograph.My.MyProject.Forms.frmMain.txtUser.Text;
                        break;
                    }
                case 6:
                    {
                        // user lookup tab
                        this.cmbContents.SelectedIndex = 5;
                        this.txtUserUser.Text = global::Audiograph.My.MyProject.Forms.frmMain.txtUserL.Text;
                        break;
                    }

                default:
                    {
                        this.cmbContents.SelectedIndex = 0;
                        break;
                    }
            }

            UserEnableAmount(null, null);
            UserEnableDate(null, null);
        }

        private void ChangeContents(global::System.Object sender, global::System.EventArgs e)
        {
            this.InvisibleAllPanels();
            switch (this.cmbContents.SelectedIndex)
            {
                case 0:
                    {
                        this.pnlCharts.Visible = true;
                        break;
                    }
                case 1:
                    {
                        this.pnlTag.Visible = true;
                        break;
                    }
                case 2:
                    {
                        this.pnlTrack.Visible = true;
                        break;
                    }
                case 3:
                    {
                        this.pnlArtist.Visible = true;
                        break;
                    }
                case 4:
                    {
                        this.pnlAlbum.Visible = true;
                        break;
                    }
                case 5:
                    {
                        this.pnlUser.Visible = true;
                        break;
                    }
            }
        }

        private void InvisibleAllPanels()
        {
            this.pnlCharts.Visible = false;
            this.pnlTag.Visible = false;
            this.pnlTrack.Visible = false;
            this.pnlArtist.Visible = false;
            this.pnlAlbum.Visible = false;
            this.pnlUser.Visible = false;
        }

        private void Browse(global::System.Object sender, global::System.EventArgs e)
        {
            var result = this.sfdSave.ShowDialog();

            if ((result == global::System.Windows.Forms.DialogResult.OK))
            {
                this.txtSave.Text = this.sfdSave.FileName;
            }
        }

        private void StartButton(global::System.Object sender, global::System.EventArgs e)
        {
            this.StartOp();
        }

        private void StopButton(global::System.Object sender, global::System.EventArgs e)
        {
            this.StopOp();
        }

        private void FrmClose(global::System.Object sender, global::System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Ops
        private void UpdateProgress(global::System.Boolean finalStage, global::System.Double percentage, global::System.String status)
        {
            // only add if the progressbar will not go over 95%
            if (((finalStage) == (false)))
            {
                if (((((percentage) / (global::System.Double)(this.progressMultiplier)) * (0.95d)) <= (95d)))
                {
                    this.Invoke(new Action(() => this.pbStatus.Value = (global::System.Int32)Math.Round((((percentage) / (global::System.Double)(this.progressMultiplier)) * (0.95d)))));
                }
            }
            else if (((percentage) != (100d)))
            {
                this.Invoke(new Action(() => this.pbStatus.Value = (global::System.Int32)Math.Round(((95d) + ((percentage) * (0.05d))))));
            }
            else
            {
                this.Invoke(new Action(() => this.pbStatus.Value = 100));
            }

            this.Invoke(new Action(() => this.lblStatus.Text = status));
        }

        private void StartOp()
        {
            // ui
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            this.progressMultiplier = (global::System.Byte)1;
            this.UpdateProgress(false, 0d, "Starting...");

            // clear
            this.textContents.Clear();
            this.columnContents.Clear();
            this.dateContents.Clear();

            // make sure something is entered in the browse field
            if (string.IsNullOrEmpty(this.txtSave.Text.Trim()))
            {
                global::System.Windows.Forms.MessageBox.Show("Valid data must be entered in the Save Location field", "Backup Tool", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                this.StopOp();
                return;
            }

            // evaluate what needs to be backed up
            this.section = (global::System.Byte)this.cmbContents.SelectedIndex;
            switch (this.section)
            {
                case 0:
                    {
                        // results
                        this.chartResults = (global::System.Int32)Math.Round(this.nudChartResults.Value);

                        // country
                        if (((this.radChartWorldwide.Checked) == (true)))
                        {
                            this.chartCountry = global::System.String.Empty;
                        }
                        else
                        {
                            this.chartCountry = this.cmbChartCountry.Text;
                        }

                        // columns
                        this.columnContents.Add(this.chkChartTopTracks.Checked);
                        this.columnContents.Add(this.chkChartTopArtists.Checked);
                        this.columnContents.Add(this.chkChartTopTags.Checked);

                        this.bgwChart.RunWorkerAsync();
                        break;
                    }
                case 1:
                    {
                        // text
                        if (string.IsNullOrEmpty(this.txtTagTag.Text.Trim()))
                        {
                            global::System.Windows.Forms.MessageBox.Show("Valid data must be entered in the Tag field", "Tag Backup", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                            this.StopOp();
                            return;
                        }
                        this.textContents.Add(this.txtTagTag.Text);

                        // results
                        this.tagResults = (global::System.Int32)Math.Round(this.nudTagResults.Value);

                        // columns
                        this.columnContents.Add(this.chkTagInfo.Checked);
                        this.columnContents.Add(this.chkTagTopTracks.Checked);
                        this.columnContents.Add(this.chkTagTopArtists.Checked);
                        this.columnContents.Add(this.chkTagTopAlbums.Checked);

                        this.bgwTag.RunWorkerAsync();
                        break;
                    }
                case 2:
                    {
                        // text
                        if ((string.IsNullOrEmpty(this.txtTrackTitle.Text.Trim()) || string.IsNullOrEmpty(this.txtTrackArtist.Text.Trim())))
                        {
                            global::System.Windows.Forms.MessageBox.Show("Valid data must be entered in the Track and Artist fields", "Track Backup", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                            this.StopOp();
                            return;
                        }
                        this.textContents.Add(this.txtTrackTitle.Text);
                        this.textContents.Add(this.txtTrackArtist.Text);

                        // columns
                        this.columnContents.Add(this.chkTrackInfo.Checked);
                        this.columnContents.Add(this.chkTrackStats.Checked);
                        this.columnContents.Add(this.chkTrackTags.Checked);
                        this.columnContents.Add(this.chkTrackSimilar.Checked);

                        this.bgwTrack.RunWorkerAsync();
                        break;
                    }
                case 3:
                    {
                        // text
                        if (string.IsNullOrEmpty(this.txtArtistArtist.Text.Trim()))
                        {
                            global::System.Windows.Forms.MessageBox.Show("Valid data must be entered in the Artist field", "Artist Backup", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                            this.StopOp();
                            return;
                        }
                        this.textContents.Add(this.txtArtistArtist.Text);

                        // columns
                        this.columnContents.Add(this.chkArtistStats.Checked);
                        this.columnContents.Add(this.chkArtistTags.Checked);
                        this.columnContents.Add(this.chkArtistSimilar.Checked);
                        this.columnContents.Add(this.chkArtistCharts.Checked);

                        this.bgwArtist.RunWorkerAsync();
                        break;
                    }
                case 4:
                    {
                        // text
                        if ((string.IsNullOrEmpty(this.txtAlbumAlbum.Text.Trim()) || string.IsNullOrEmpty(this.txtAlbumArtist.Text.Trim())))
                        {
                            global::System.Windows.Forms.MessageBox.Show("Valid data must be entered in the Album and Artist fields", "Album Backup", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                            this.StopOp();
                            return;
                        }
                        this.textContents.Add(this.txtAlbumAlbum.Text);
                        this.textContents.Add(this.txtAlbumArtist.Text);

                        // columns
                        this.columnContents.Add(this.chkAlbumInfo.Checked);
                        this.columnContents.Add(this.chkAlbumTracks.Checked);
                        this.columnContents.Add(this.chkAlbumStats.Checked);
                        this.columnContents.Add(this.chkAlbumTags.Checked);

                        this.bgwAlbum.RunWorkerAsync();
                        break;
                    }
                case 5:
                    {
                        // text
                        if (string.IsNullOrEmpty(this.txtUserUser.Text.Trim()))
                        {
                            global::System.Windows.Forms.MessageBox.Show("Valid data must be entered in the User field", "User Backup", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                            this.StopOp();
                            return;
                        }
                        this.textContents.Add(this.txtUserUser.Text);

                        // date
                        if (((this.chkUserByDate.Checked) == (true)))
                        {
                            // check that from is before to
                            if (((this.dtpUserFrom.Value) > (this.dtpUserTo.Value)))
                            {
                                global::System.Windows.Forms.MessageBox.Show("From date must be before to date", "User Backup", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                                this.StopOp();
                                return;
                            }

                            this.dateContents.Add(this.dtpUserFrom.Value.Date);
                            this.dateContents.Add(this.dtpUserTo.Value.Date);
                        }

                        // results
                        if (((this.radUserEntire.Checked) == (true)))
                        {
                            this.userResults = 0;
                        }
                        else
                        {
                            this.userResults = (global::System.Int32)Math.Round(this.nudUserNumber.Value);
                        }

                        // columns
                        this.columnContents.Add(this.chkUserInfo.Checked);
                        this.columnContents.Add(this.chkUserFriends.Checked);
                        this.columnContents.Add(this.chkUserLoved.Checked);
                        this.columnContents.Add(this.chkUserHistory.Checked);
                        this.columnContents.Add(this.chkUserCharts.Checked);

                        this.bgwUser.RunWorkerAsync();
                        break;
                    }
            }
        }

        private void StopOp()
        {
            // stop threads
            if (((this.bgwChart.IsBusy) == (true)))
            {
                this.bgwChart.CancelAsync();
            }
            if (((this.bgwTag.IsBusy) == (true)))
            {
                this.bgwTag.CancelAsync();
            }
            if (((this.bgwTrack.IsBusy) == (true)))
            {
                this.bgwTrack.CancelAsync();
            }
            if (((this.bgwArtist.IsBusy) == (true)))
            {
                this.bgwArtist.CancelAsync();
            }
            if (((this.bgwAlbum.IsBusy) == (true)))
            {
                this.bgwAlbum.CancelAsync();
            }
            if (((this.bgwUser.IsBusy) == (true)))
            {
                this.bgwUser.CancelAsync();
            }

            // ui
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
            this.pbStatus.Value = 0;
            this.lblStatus.Text = "Ready";
        }

        private void BackgroundStopOp(object sender, global::System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // ui
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
            this.pbStatus.Value = 0;
            this.lblStatus.Text = "Ready";
        }

        // the most retardedly confusing method ive ever written
        private void Save(global::System.Collections.Generic.List<global::System.String[]>[] lists)
        {
            // List of string()()    -1 contains all the categories
            // List of string()	-2 contains all the lines in the category
            // String()		-3 contains all the cells of a line
            // String     -4 contains each individual cell of a line

            var outputList = new global::System.Collections.Generic.List<global::System.String[]>();
            var currentLine = new global::System.Collections.Generic.List<global::System.String>();
            global::System.Int32 largestValue = 0;

            this.UpdateProgress(true, 0d, "Assembling...");

            // get the largest list
            foreach (var list in lists) // 1 - finding the biggest category
            {
                if ((((list.Count) - (1)) > (largestValue)))
                {
                    largestValue = ((list.Count) - (1));
                }
            }

            // 3 - cycling through each line
            for (global::System.Int32 line = 0, loopTo = largestValue; line <= loopTo; line++)
            {
                // 2 - cycling through the categories to add one line from each
                for (global::System.Int32 list = 0, loopTo1 = (lists.Count()) - (1); list <= loopTo1; list++)
                {
                    // attempt to add if there is more from the category, if not then add empty
                    if ((((lists[list].Count) - (1)) >= (line)))
                    {
                        // 4 - cycle through each cell of the line and add to current line
                        foreach (var item in lists[list][line])
                        {
                            // check for errors
                            if (((item.Contains("ERROR: ")) == (false)))
                            {
                                currentLine.Add(item.Replace(Conversions.ToString('"'), global::System.String.Empty));
                            }
                            else
                            {
                                currentLine.Add(global::System.String.Empty);
                            }
                        }
                    }
                    else
                    {
                        // add empty cells
                        foreach (var item in lists[list][0])
                            currentLine.Add(global::System.String.Empty);
                    }

                    // add separator only if not on the last list
                    if (((list) != ((lists.Count()) - (1))))
                    {
                        currentLine.Add("-");
                    }
                }
                // add and clear
                outputList.Add(currentLine.ToArray());
                currentLine.Clear();
            }

            this.UpdateProgress(true, 50d, "Writing...");

            // save
            var fi = new global::System.IO.FileInfo(this.txtSave.Text.Trim());

            // delete file if it already exists
            if (((fi.Exists) == (true)))
            {
                try
                {
                    fi.Delete();
                }
                catch (global::System.IO.IOException ex)
                {
                    global::System.Windows.Forms.MessageBox.Show(("Could not write to file" + global::Microsoft.VisualBasic.Constants.vbCrLf) + "Check that another program is not using the file", "Backup Tool", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                    this.Invoke(new Action(() => this.StopOp()));
                    return;
                }
            }

            // create new file and filestream
            var fs = default(global::System.IO.FileStream);
            using (fs)
            {
                fs = fi.Create();

                // compile data to string
                var str = new global::System.Text.StringBuilder();

                foreach (var line in outputList)
                {
                    for (global::System.Int32 cell = 0, loopTo2 = (line.Count()) - (1); cell <= loopTo2; cell++)
                    {
                        // check if on the last cell
                        if (((cell) < ((line.Count()) - (1))))
                        {
                            str.Append((('"' + line[cell]) + '"') + ",");
                        }

                        else
                        {
                            // if on last line dont add comma and add line
                            str.Append(('"' + line[cell]) + '"');
                            str.AppendLine();
                        }
                    }
                }

                // convert to bytearray and write
                global::System.Byte[] bytes = new global::System.Text.UTF32Encoding().GetBytes(str.ToString());
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }

            this.UpdateProgress(true, 100d, "Success!");
        }
        #endregion

        #region Charts
        private void ChartOp(global::System.Object sender, global::System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // init
                global::System.Threading.Thread.CurrentThread.Name = "BackupChart";
                var topTrackInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var topArtistInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var topTagInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var lists = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.String[]>>();
                global::System.Int32 progress = 0;

                // progress multiplier
                this.progressMultiplier = (global::System.Byte)0;
                if (((this.columnContents[0]) == (true)))
                {
                    topTrackInfo.Add(new[] { "Track", "Artist", "Listeners", "Playcount" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                if (((this.columnContents[1]) == (true)))
                {
                    topArtistInfo.Add(new[] { "Artist", "Listeners", "Playcount" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                if (((this.columnContents[2]) == (true)))
                {
                    topTagInfo.Add(new[] { "Tag", "Reach", "Taggings" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }

                // top tracks
                if (((this.columnContents[0]) == (true)))
                {
                    lists.Add(topTrackInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.chartResults) / (50d)));
                    var xmlPages = new global::System.Collections.Generic.List<global::System.String>();

                    for (global::System.Int32 currentPage = 0, loopTo = (pageAmount) - (1); currentPage <= loopTo; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (80d)))) + (global::System.Double)(progress), ((("Getting top tracks... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        if (((currentPage) == ((pageAmount) - (1))))
                        {
                            // last page, only request leftover
                            if (((this.chartResults) <= (50)))
                            {
                                // if results below 50
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("chart.getTopTracks", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=" + this.chartResults.ToString()));
                            }
                            else if ((((this.chartResults) % (50)) == (0)))
                            {
                                // if no remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("chart.getTopTracks", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                            }
                            else
                            {
                                // if not below 50 get remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("chart.getTopTracks", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=" + (((this.chartResults) % (50))).ToString()));
                            }
                        }
                        else
                        {
                            xmlPages.Add(global::Audiograph.Utilities.CallAPI("chart.getTopTracks", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                        }

                        // cancel check
                        if (((this.bgwChart.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 80;

                    // add to list
                    global::System.Int32 currentPageAmount;
                    global::System.String[] xmlNodes;
                    for (global::System.Int32 currentPage = 0, loopTo1 = (pageAmount) - (1); currentPage <= loopTo1; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (20d)))) + (global::System.Double)(progress), ((("Parsing top tracks... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        currentPageAmount = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                        // parse
                        for (global::System.Int32 track = 0, loopTo2 = (currentPageAmount) - (1); track <= loopTo2; track++)
                        {
                            xmlNodes = new[] { "name", "artist/name", "listeners", "playcount" };
                            global::Audiograph.Utilities.ParseXML(xmlPages[currentPage], "/lfm/tracks/track", (global::System.UInt32)track, ref xmlNodes);
                            topTrackInfo.Add(xmlNodes);
                        }

                        // cancel check
                        if (((this.bgwChart.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 20;
                }

                // top artists
                if (((this.columnContents[1]) == (true)))
                {
                    lists.Add(topArtistInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.chartResults) / (50d)));
                    var xmlPages = new global::System.Collections.Generic.List<global::System.String>();
                    for (global::System.Int32 currentPage = 0, loopTo3 = (pageAmount) - (1); currentPage <= loopTo3; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (80d)))) + (global::System.Double)(progress), ((("Getting top artists... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        if (((currentPage) == ((pageAmount) - (1))))
                        {
                            // last page, only request leftover
                            if (((this.chartResults) <= (50)))
                            {
                                // if results below 50
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("chart.getTopArtists", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=" + this.chartResults.ToString()));
                            }
                            else if ((((this.chartResults) % (50)) == (0)))
                            {
                                // if no remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("chart.getTopArtists", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                            }
                            else
                            {
                                // if not below 50 get remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("chart.getTopArtists", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=" + (((this.chartResults) % (50))).ToString()));
                            }
                        }
                        else
                        {
                            xmlPages.Add(global::Audiograph.Utilities.CallAPI("chart.getTopArtists", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                        }

                        // cancel check
                        if (((this.bgwChart.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 80;

                    // add to list
                    global::System.Int32 currentPageAmount;
                    global::System.String[] xmlNodes;
                    for (global::System.Int32 currentPage = 0, loopTo4 = (pageAmount) - (1); currentPage <= loopTo4; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (20d)))) + (global::System.Double)(progress), ((("Parsing top artists... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        currentPageAmount = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                        // parse
                        for (global::System.Int32 artist = 0, loopTo5 = (currentPageAmount) - (1); artist <= loopTo5; artist++)
                        {
                            xmlNodes = new[] { "name", "listeners", "playcount" };
                            global::Audiograph.Utilities.ParseXML(xmlPages[currentPage], "/lfm/artists/artist", (global::System.UInt32)artist, ref xmlNodes);
                            topArtistInfo.Add(xmlNodes);
                        }

                        // cancel check
                        if (((this.bgwChart.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 20;
                }

                // top tags
                if (((this.columnContents[2]) == (true)))
                {
                    lists.Add(topTagInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.chartResults) / (50d)));
                    var xmlPages = new global::System.Collections.Generic.List<global::System.String>();
                    for (global::System.Int32 currentPage = 0, loopTo6 = (pageAmount) - (1); currentPage <= loopTo6; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (80d)))) + (global::System.Double)(progress), ((("Getting top tags... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        if (((currentPage) == ((pageAmount) - (1))))
                        {
                            // last page, only request leftover
                            if (((this.chartResults) <= (50)))
                            {
                                // if results below 50
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("chart.getTopTags", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=" + this.chartResults.ToString()));
                            }
                            else if ((((this.chartResults) % (50)) == (0)))
                            {
                                // if no remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("chart.getTopTags", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                            }
                            else
                            {
                                // if not below 50 get remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("chart.getTopTags", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=" + (((this.chartResults) % (50))).ToString()));
                            }
                        }
                        else
                        {
                            xmlPages.Add(global::Audiograph.Utilities.CallAPI("chart.getTopTags", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                        }

                        // cancel check
                        if (((this.bgwChart.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 80;

                    // add to list
                    global::System.Int32 currentPageAmount;
                    global::System.String[] xmlNodes;
                    for (global::System.Int32 currentPage = 0, loopTo7 = (pageAmount) - (1); currentPage <= loopTo7; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (20d)))) + (global::System.Double)(progress), ((("Parsing top tags... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        currentPageAmount = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                        // parse
                        for (global::System.Int32 tag = 0, loopTo8 = (currentPageAmount) - (1); tag <= loopTo8; tag++)
                        {
                            xmlNodes = new[] { "name", "reach", "taggings" };
                            global::Audiograph.Utilities.ParseXML(xmlPages[currentPage], "/lfm/tags/tag", (global::System.UInt32)tag, ref xmlNodes);
                            topTagInfo.Add(xmlNodes);
                        }

                        // cancel check
                        if (((this.bgwChart.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }
                }

                this.Save(lists.ToArray());
                this.Invoke(new Action(() => this.StopOp()));
            }
            catch (global::System.Exception ex)
            {
                this.Invoke(new Action(() => global::System.Windows.Forms.MessageBox.Show("ERROR: " + ex.Message)), "Charts Backup", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
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

        #region Tag
        private void TagOp(global::System.Object sender, global::System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // init
                global::System.Threading.Thread.CurrentThread.Name = "BackupTag";
                var tagInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var topTrackInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var topArtistInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var topAlbumInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var lists = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.String[]>>();
                global::System.Int32 progress = 0;

                // verify
                if (((global::Audiograph.Utilities.VerifyTag(this.textContents[0]).Contains("ERROR: ")) == (true)))
                {
                    global::System.Windows.Forms.MessageBox.Show(("Tag data unable to be retrived" + global::Microsoft.VisualBasic.Constants.vbCrLf) + "Check that you have spelled your search terms correctly", "Backup Tag", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                    this.Invoke(new Action(() => this.StopOp()));
                    return;
                }

                // progress multiplier
                this.progressMultiplier = (global::System.Byte)0;
                // info
                if (((this.columnContents[0]) == (true)))
                {
                    tagInfo.Add(new[] { "Name", "Taggings", "Reach" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // tracks
                if (((this.columnContents[1]) == (true)))
                {
                    topTrackInfo.Add(new[] { "Track", "Artist" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // artists
                if (((this.columnContents[2]) == (true)))
                {
                    topArtistInfo.Add(new[] { "Artist" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // albums
                if (((this.columnContents[3]) == (true)))
                {
                    topAlbumInfo.Add(new[] { "Album", "Artist" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }

                // info
                if (((this.columnContents[0]) == (true)))
                {
                    lists.Add(tagInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.tagResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting tag info...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("tag.getInfo", global::System.String.Empty, "tag=" + this.textContents[0]);

                    // cancel check
                    if (((this.bgwTag.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing tag info...");

                    // parse
                    xmlNodes = new[] { "name", "total", "reach" };
                    global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/tag", 0U, ref xmlNodes);
                    tagInfo.Add(xmlNodes);

                    // cancel check
                    if (((this.bgwTag.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                // top tracks
                if (((this.columnContents[1]) == (true)))
                {
                    lists.Add(topTrackInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.tagResults) / (50d)));
                    var xmlPages = new global::System.Collections.Generic.List<global::System.String>();
                    for (global::System.Int32 currentPage = 0, loopTo = (pageAmount) - (1); currentPage <= loopTo; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (80d)))) + (global::System.Double)(progress), ((("Getting top tracks... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        if (((currentPage) == ((pageAmount) - (1))))
                        {
                            // last page, only request leftover
                            if (((this.tagResults) <= (50)))
                            {
                                // if results below 50
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("tag.getTopTracks", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=" + this.tagResults.ToString(), "tag=" + this.textContents[0]));
                            }
                            else if ((((this.tagResults) % (50)) == (0)))
                            {
                                // if no remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("tag.getTopTracks", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=50", "tag=" + this.textContents[0]));
                            }
                            else
                            {
                                // if not below 50 get remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("tag.getTopTracks", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=" + (((this.tagResults) % (50))).ToString(), "tag=" + this.textContents[0]));
                            }
                        }
                        else
                        {
                            xmlPages.Add(global::Audiograph.Utilities.CallAPI("tag.getTopTracks", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=50", "tag=" + this.textContents[0]));
                        }

                        // cancel check
                        if (((this.bgwTag.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 80;

                    // add to list
                    global::System.Int32 currentPageAmount;
                    global::System.String[] xmlNodes;
                    for (global::System.Int32 currentPage = 0, loopTo1 = (pageAmount) - (1); currentPage <= loopTo1; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (20d)))) + (global::System.Double)(progress), ((("Parsing top tracks... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        currentPageAmount = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                        // parse
                        for (global::System.Int32 artist = 0, loopTo2 = (currentPageAmount) - (1); artist <= loopTo2; artist++)
                        {
                            xmlNodes = new[] { "name", "artist/name" };
                            global::Audiograph.Utilities.ParseXML(xmlPages[currentPage], "/lfm/tracks/track", (global::System.UInt32)artist, ref xmlNodes);
                            topTrackInfo.Add(xmlNodes);
                        }

                        // cancel check
                        if (((this.bgwTag.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 20;
                }

                // top artists
                if (((this.columnContents[1]) == (true)))
                {
                    lists.Add(topArtistInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.tagResults) / (50d)));
                    var xmlPages = new global::System.Collections.Generic.List<global::System.String>();
                    for (global::System.Int32 currentPage = 0, loopTo3 = (pageAmount) - (1); currentPage <= loopTo3; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (80d)))) + (global::System.Double)(progress), ((("Getting top artists... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        if (((currentPage) == ((pageAmount) - (1))))
                        {
                            // last page, only request leftover
                            if (((this.tagResults) <= (50)))
                            {
                                // if results below 50
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("tag.getTopArtists", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=" + this.tagResults.ToString(), "tag=" + this.textContents[0]));
                            }
                            else if ((((this.tagResults) % (50)) == (0)))
                            {
                                // if no remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("tag.getTopArtists", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=50", "tag=" + this.textContents[0]));
                            }
                            else
                            {
                                // if not below 50 get remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("tag.getTopArtists", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=" + (((this.tagResults) % (50))).ToString(), "tag=" + this.textContents[0]));
                            }
                        }
                        else
                        {
                            xmlPages.Add(global::Audiograph.Utilities.CallAPI("tag.getTopArtists", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=50", "tag=" + this.textContents[0]));
                        }

                        // cancel check
                        if (((this.bgwTag.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 80;

                    // add to list
                    global::System.Int32 currentPageAmount;
                    global::System.String[] xmlNodes;
                    for (global::System.Int32 currentPage = 0, loopTo4 = (pageAmount) - (1); currentPage <= loopTo4; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (20d)))) + (global::System.Double)(progress), ((("Parsing top artists... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        currentPageAmount = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                        // parse
                        for (global::System.Int32 artist = 0, loopTo5 = (currentPageAmount) - (1); artist <= loopTo5; artist++)
                        {
                            xmlNodes = new[] { "name" };
                            global::Audiograph.Utilities.ParseXML(xmlPages[currentPage], "/lfm/topartists/artist", (global::System.UInt32)artist, ref xmlNodes);
                            topArtistInfo.Add(xmlNodes);
                        }

                        // cancel check
                        if (((this.bgwTag.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 20;
                }

                // top albums
                if (((this.columnContents[3]) == (true)))
                {
                    lists.Add(topAlbumInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.tagResults) / (50d)));
                    var xmlPages = new global::System.Collections.Generic.List<global::System.String>();
                    for (global::System.Int32 currentPage = 0, loopTo6 = (pageAmount) - (1); currentPage <= loopTo6; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (80d)))) + (global::System.Double)(progress), ((("Getting top albums... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        if (((currentPage) == ((pageAmount) - (1))))
                        {
                            // last page, only request leftover
                            if (((this.tagResults) <= (50)))
                            {
                                // if results below 50
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("tag.getTopAlbums", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=" + this.tagResults.ToString(), "tag=" + this.textContents[0]));
                            }
                            else if ((((this.tagResults) % (50)) == (0)))
                            {
                                // if no remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("tag.getTopAlbums", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=50", "tag=" + this.textContents[0]));
                            }
                            else
                            {
                                // if not below 50 get remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("tag.getTopAlbums", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=" + (((this.tagResults) % (50))).ToString(), "tag=" + this.textContents[0]));
                            }
                        }
                        else
                        {
                            xmlPages.Add(global::Audiograph.Utilities.CallAPI("tag.getTopAlbums", global::System.String.Empty, "page=" + (((currentPage) + (1))).ToString(), "limit=50", "tag=" + this.textContents[0]));
                        }

                        // cancel check
                        if (((this.bgwTag.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 80;

                    // add to list
                    global::System.Int32 currentPageAmount;
                    global::System.String[] xmlNodes;
                    for (global::System.Int32 currentPage = 0, loopTo7 = (pageAmount) - (1); currentPage <= loopTo7; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (20d)))) + (global::System.Double)(progress), ((("Parsing top albums... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        currentPageAmount = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                        // parse
                        for (global::System.Int32 album = 0, loopTo8 = (currentPageAmount) - (1); album <= loopTo8; album++)
                        {
                            xmlNodes = new[] { "name", "artist/name" };
                            global::Audiograph.Utilities.ParseXML(xmlPages[currentPage], "/lfm/albums/album", (global::System.UInt32)album, ref xmlNodes);
                            topAlbumInfo.Add(xmlNodes);
                        }

                        // cancel check
                        if (((this.bgwTag.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 20;
                }

                this.Save(lists.ToArray());
                this.Invoke(new Action(() => this.StopOp()));
            }
            catch (global::System.Exception ex)
            {
                this.Invoke(new Action(() => global::System.Windows.Forms.MessageBox.Show("ERROR: " + ex.Message, "Tag Backup", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error)));
            }
        }
        #endregion

        #region Track
        private void TrackOp(global::System.Object sender, global::System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // init
                global::System.Threading.Thread.CurrentThread.Name = "BackupTrack";
                var trackInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var statsInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var tagsInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var similarInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var lists = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.String[]>>();
                global::System.Int32 progress = 0;

                // verify
                if (((global::Audiograph.Utilities.VerifyTrack(this.textContents[0], this.textContents[1])[0].Contains("ERROR: ")) == (true)))
                {
                    global::System.Windows.Forms.MessageBox.Show(("Track data unable to be retrived" + global::Microsoft.VisualBasic.Constants.vbCrLf) + "Check that you have spelled your search terms correctly", "Backup Track", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                    this.Invoke(new Action(() => this.StopOp()));
                    return;
                }

                // progress multiplier
                this.progressMultiplier = (global::System.Byte)0;
                // info
                if (((this.columnContents[0]) == (true)))
                {
                    trackInfo.Add(new[] { "Name", "Artist", "Album", "Duration (s)" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // stats
                if (((this.columnContents[1]) == (true)))
                {
                    statsInfo.Add(new[] { "Listeners", "Playcount" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // tags
                if (((this.columnContents[2]) == (true)))
                {
                    tagsInfo.Add(new[] { "Tag", "Taggings" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // similar
                if (((this.columnContents[3]) == (true)))
                {
                    similarInfo.Add(new[] { "Track", "Artist", "Match %" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }

                // info
                if (((this.columnContents[0]) == (true)))
                {
                    lists.Add(trackInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.trackResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting track info...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("track.getInfo", global::System.String.Empty, "track=" + this.textContents[0], "artist=" + this.textContents[1], "autocorrect=1");

                    // cancel check
                    if (((this.bgwTrack.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing track info...");

                    // parse
                    xmlNodes = new[] { "name", "artist/name", "album/title", "duration" };
                    global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/track", 0U, ref xmlNodes);

                    // fix album if not present
                    if (((xmlNodes[2].Contains("ERROR: ")) == (true)))
                    {
                        xmlNodes[2] = global::System.String.Empty;
                    }

                    // fix duration if not present
                    if ((xmlNodes[3] == "0"))
                    {
                        xmlNodes[3] = global::System.String.Empty;
                    }
                    else
                    {
                        xmlNodes[3] = (((global::System.Double)((Conversions.ToInteger(xmlNodes[3]))) / (1000d))).ToString("N0");
                    }

                    trackInfo.Add(xmlNodes);

                    // cancel check
                    if (((this.bgwTrack.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                // stats
                if (((this.columnContents[1]) == (true)))
                {
                    lists.Add(statsInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.trackResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting stats info...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("track.getInfo", global::System.String.Empty, "track=" + this.textContents[0], "artist=" + this.textContents[1], "autocorrect=1");

                    // cancel check
                    if (((this.bgwTrack.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing stats info...");

                    // parse
                    xmlNodes = new[] { "listeners", "playcount" };
                    global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/track", 0U, ref xmlNodes);

                    statsInfo.Add(xmlNodes);

                    // cancel check
                    if (((this.bgwTrack.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                // tags
                if (((this.columnContents[2]) == (true)))
                {
                    lists.Add(tagsInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.trackResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting tag info...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("track.getTopTags", global::System.String.Empty, "track=" + this.textContents[0], "artist=" + this.textContents[1], "autocorrect=1");

                    // cancel check
                    if (((this.bgwTrack.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing tag info...");

                    // get amount of tags and handle if there are no tags
                    global::System.Int32 tagCount = (global::System.Int32)global::Audiograph.Utilities.StrCount(xmlPage, "<name>");
                    if (((tagCount) > (0)))
                    {
                        // parse
                        for (global::System.UInt32 tag = 0U, loopTo = (global::System.UInt32)((tagCount) - (1)); tag <= loopTo; tag++)
                        {
                            xmlNodes = new[] { "name", "count" };
                            global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/toptags/tag", tag, ref xmlNodes);

                            tagsInfo.Add(xmlNodes);
                        }
                    }
                    else
                    {
                        tagsInfo.Add(new[] { global::System.String.Empty });
                    }

                    // cancel check
                    if (((this.bgwTrack.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                // similar
                if (((this.columnContents[3]) == (true)))
                {
                    lists.Add(similarInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.trackResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting similar tracks...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("track.getSimilar", global::System.String.Empty, "track=" + this.textContents[0], "artist=" + this.textContents[1], "autocorrect=1");

                    // cancel check
                    if (((this.bgwTrack.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing similar tracks...");

                    // get amount of tracks and handle if there are no tracks
                    global::System.Int32 trackCount = (global::System.Int32)global::Audiograph.Utilities.StrCount(xmlPage, "<track>");
                    if (((trackCount) > (0)))
                    {
                        // parse
                        for (global::System.UInt32 track = 0U, loopTo1 = (global::System.UInt32)((trackCount) - (1)); track <= loopTo1; track++)
                        {
                            xmlNodes = new[] { "name", "artist/name", "match" };
                            global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/similartracks/track", track, ref xmlNodes);

                            // convert match from decimal to percent
                            xmlNodes[2] = (Conversions.ToDouble(xmlNodes[2])).ToString("P");

                            similarInfo.Add(xmlNodes);
                        }
                    }
                    else
                    {
                        similarInfo.Add(new[] { global::System.String.Empty });
                    }

                    // cancel check
                    if (((this.bgwTrack.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                this.Save(lists.ToArray());
                this.Invoke(new Action(() => this.StopOp()));
            }
            catch (global::System.Exception ex)
            {
                this.Invoke(new Action(() => global::System.Windows.Forms.MessageBox.Show("ERROR: " + ex.Message, "Track Backup", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error)));
            }
        }
        #endregion

        #region Artist
        private void ArtistOp(global::System.Object sender, global::System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // init
                global::System.Threading.Thread.CurrentThread.Name = "BackupArtist";
                var statsInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var tagsInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var similarInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var chartsInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var lists = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.String[]>>();
                global::System.Int32 progress = 0;

                // verify
                if (((global::Audiograph.Utilities.VerifyArtist(this.textContents[0]).Contains("ERROR: ")) == (true)))
                {
                    global::System.Windows.Forms.MessageBox.Show(("Artist data unable to be retrived" + global::Microsoft.VisualBasic.Constants.vbCrLf) + "Check that you have spelled your search terms correctly", "Backup Artist", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                    this.Invoke(new Action(() => this.StopOp()));
                    return;
                }

                // progress multiplier
                this.progressMultiplier = (global::System.Byte)0;
                // stats
                if (((this.columnContents[0]) == (true)))
                {
                    statsInfo.Add(new[] { "Artist", "Listeners", "Playcount" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // tags
                if (((this.columnContents[1]) == (true)))
                {
                    tagsInfo.Add(new[] { "Tag", "Count" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // similar
                if (((this.columnContents[2]) == (true)))
                {
                    similarInfo.Add(new[] { "Artist", "Match %" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // charts
                if (((this.columnContents[3]) == (true)))
                {
                    chartsInfo.Add(new[] { "Top Track", "Top Album" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }

                // stats
                if (((this.columnContents[0]) == (true)))
                {
                    lists.Add(statsInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.artistResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting stats info...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("artist.getInfo", global::System.String.Empty, "artist=" + this.textContents[0], "autocorrect=1");

                    // cancel check
                    if (((this.bgwArtist.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing stats info...");

                    // parse
                    xmlNodes = new[] { "name", "stats/listeners", "stats/playcount" };
                    global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/artist", 0U, ref xmlNodes);

                    statsInfo.Add(xmlNodes);

                    // cancel check
                    if (((this.bgwArtist.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                // tags
                if (((this.columnContents[1]) == (true)))
                {
                    lists.Add(tagsInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.artistResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting tag info...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("artist.getTopTags", global::System.String.Empty, "artist=" + this.textContents[0], "autocorrect=1");

                    // cancel check
                    if (((this.bgwArtist.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing tag info...");

                    // get amount of tags and handle if there are no tags
                    global::System.Int32 tagCount = (global::System.Int32)global::Audiograph.Utilities.StrCount(xmlPage, "<name>");
                    if (((tagCount) > (0)))
                    {
                        // parse
                        for (global::System.UInt32 tag = 0U, loopTo = (global::System.UInt32)((tagCount) - (1)); tag <= loopTo; tag++)
                        {
                            xmlNodes = new[] { "name", "count" };
                            global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/toptags/tag", tag, ref xmlNodes);

                            tagsInfo.Add(xmlNodes);
                        }
                    }

                    // cancel check
                    if (((this.bgwArtist.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                // similar
                if (((this.columnContents[2]) == (true)))
                {
                    lists.Add(similarInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.artistResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting similar artists...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("artist.getSimilar", global::System.String.Empty, "artist=" + this.textContents[0], "autocorrect=1");

                    // cancel check
                    if (((this.bgwArtist.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing similar artists...");

                    // get amount of tags and handle if there are no tags
                    global::System.Int32 artistCount = (global::System.Int32)global::Audiograph.Utilities.StrCount(xmlPage, "<artist>");
                    if (((artistCount) > (0)))
                    {
                        // parse
                        for (global::System.UInt32 artist = 0U, loopTo1 = (global::System.UInt32)((artistCount) - (1)); artist <= loopTo1; artist++)
                        {
                            xmlNodes = new[] { "name", "match" };
                            global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/similarartists/artist", artist, ref xmlNodes);

                            // convert match from decimal to percent
                            xmlNodes[1] = (Conversions.ToDouble(xmlNodes[1])).ToString("P");

                            similarInfo.Add(xmlNodes);
                        }
                    }

                    // cancel check
                    if (((this.bgwArtist.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                // charts
                if (((this.columnContents[3]) == (true)))
                {
                    lists.Add(chartsInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.artistResults) / (50d)));
                    var xmlPage = new global::System.String[2];

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting artist charts...");

                    xmlPage[0] = global::Audiograph.Utilities.CallAPI("artist.getTopTracks", global::System.String.Empty, "artist=" + this.textContents[0], "autocorrect=1"); // tracks
                    xmlPage[1] = global::Audiograph.Utilities.CallAPI("artist.getTopAlbums", global::System.String.Empty, "artist=" + this.textContents[0], "autocorrect=1"); // albums

                    // cancel check
                    if (((this.bgwArtist.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;
                    var xmlNodesFinal = new global::System.Collections.Generic.List<global::System.String>();

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing similar artists...");

                    // get amount of tracks and handle if there are no tracks
                    global::System.Int32 count = (global::System.Int32)global::Audiograph.Utilities.StrCount(xmlPage[0], "</track>");
                    if (((count) > (0)))
                    {
                        // parse
                        for (global::System.UInt32 artist = 0U, loopTo2 = (global::System.UInt32)((count) - (1)); artist <= loopTo2; artist++)
                        {
                            xmlNodes = new[] { "name" };
                            global::Audiograph.Utilities.ParseXML(xmlPage[0], "/lfm/toptracks/track", artist, ref xmlNodes);

                            // add to final list
                            xmlNodesFinal.Add(xmlNodes[0]);

                            xmlNodes = new[] { "name" };
                            global::Audiograph.Utilities.ParseXML(xmlPage[1], "/lfm/topalbums/album", artist, ref xmlNodes);

                            xmlNodesFinal.Add(xmlNodes[0]);

                            chartsInfo.Add(xmlNodesFinal.ToArray());
                            xmlNodesFinal.Clear();
                        }
                    }

                    // cancel check
                    if (((this.bgwArtist.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                this.Save(lists.ToArray());
                this.Invoke(new Action(() => this.StopOp()));
            }
            catch (global::System.Exception ex)
            {
                this.Invoke(new Action(() => global::System.Windows.Forms.MessageBox.Show("ERROR: " + ex.Message, "Artist Backup", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error)));
            }
        }
        #endregion

        #region Album
        private void AlbumOp(global::System.Object sender, global::System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // init
                global::System.Threading.Thread.CurrentThread.Name = "BackupAlbum";
                var albumInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var tracksInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var statsInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var tagsInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var lists = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.String[]>>();
                global::System.Int32 progress = 0;

                // verify
                if (((global::Audiograph.Utilities.VerifyAlbum(this.textContents[0], this.textContents[1])[0].Contains("ERROR: ")) == (true)))
                {
                    global::System.Windows.Forms.MessageBox.Show(("Album data unable to be retrived" + global::Microsoft.VisualBasic.Constants.vbCrLf) + "Check that you have spelled your search terms correctly", "Backup Album", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                    this.Invoke(new Action(() => this.StopOp()));
                    return;
                }

                // progress multiplier
                this.progressMultiplier = (global::System.Byte)0;
                // info
                if (((this.columnContents[0]) == (true)))
                {
                    albumInfo.Add(new[] { "Album", "Artist" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // tracks
                if (((this.columnContents[1]) == (true)))
                {
                    tracksInfo.Add(new[] { "Track", "Duration (s)" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // stats
                if (((this.columnContents[2]) == (true)))
                {
                    statsInfo.Add(new[] { "Listeners", "Playcount" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // tags
                if (((this.columnContents[3]) == (true)))
                {
                    tagsInfo.Add(new[] { "Tag", "Taggings" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }

                // info
                if (((this.columnContents[0]) == (true)))
                {
                    lists.Add(albumInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.albumResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting album info...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("album.getInfo", global::System.String.Empty, "album=" + this.textContents[0], "artist=" + this.textContents[1], "autocorrect=1");

                    // cancel check
                    if (((this.bgwAlbum.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing album info...");

                    // parse
                    xmlNodes = new[] { "name", "artist" };
                    global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/album", 0U, ref xmlNodes);

                    albumInfo.Add(xmlNodes);

                    // cancel check
                    if (((this.bgwAlbum.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                // tracks
                if (((this.columnContents[1]) == (true)))
                {
                    lists.Add(tracksInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.albumResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting tracks info...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("album.getInfo", global::System.String.Empty, "album=" + this.textContents[0], "artist=" + this.textContents[1], "autocorrect=1");

                    // cancel check
                    if (((this.bgwAlbum.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing tracks info...");

                    // get amount of tags and handle if there are no tags
                    global::System.Int32 trackCount = (global::System.Int32)global::Audiograph.Utilities.StrCount(xmlPage, "</track>");
                    if (((trackCount) > (0)))
                    {
                        // parse
                        for (global::System.UInt32 track = 0U, loopTo = (global::System.UInt32)((trackCount) - (1)); track <= loopTo; track++)
                        {
                            xmlNodes = new[] { "name", "duration" };
                            global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/album/tracks/track", track, ref xmlNodes);

                            tracksInfo.Add(xmlNodes);
                        }
                    }

                    // cancel check
                    if (((this.bgwAlbum.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                // stats
                if (((this.columnContents[2]) == (true)))
                {
                    lists.Add(statsInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.albumResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting stats info...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("album.getInfo", global::System.String.Empty, "album=" + this.textContents[0], "artist=" + this.textContents[1], "autocorrect=1");

                    // cancel check
                    if (((this.bgwAlbum.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing stats info...");

                    // parse
                    xmlNodes = new[] { "listeners", "playcount" };
                    global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/album", 0U, ref xmlNodes);

                    statsInfo.Add(xmlNodes);

                    // cancel check
                    if (((this.bgwAlbum.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                // tags
                if (((this.columnContents[3]) == (true)))
                {
                    lists.Add(tagsInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.albumResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting tag info...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("album.getTopTags", global::System.String.Empty, "album=" + this.textContents[0], "artist=" + this.textContents[1], "autocorrect=1");

                    // cancel check
                    if (((this.bgwAlbum.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // add to list
                    global::System.String[] xmlNodes;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing tag info...");

                    // get amount of tags and handle if there are no tags
                    global::System.Int32 tagCount = (global::System.Int32)global::Audiograph.Utilities.StrCount(xmlPage, "<name>");
                    if (((tagCount) > (0)))
                    {
                        // parse
                        for (global::System.UInt32 tag = 0U, loopTo1 = (global::System.UInt32)((tagCount) - (1)); tag <= loopTo1; tag++)
                        {
                            xmlNodes = new[] { "name", "count" };
                            global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/toptags/tag", tag, ref xmlNodes);

                            tagsInfo.Add(xmlNodes);
                        }
                    }
                    else
                    {
                        tagsInfo.Add(new[] { global::System.String.Empty });
                    }

                    // cancel check
                    if (((this.bgwAlbum.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                this.Save(lists.ToArray());
                this.Invoke(new Action(() => this.StopOp()));
            }
            catch (global::System.Exception ex)
            {
                this.Invoke(new Action(() => global::System.Windows.Forms.MessageBox.Show("ERROR: " + ex.Message, "Album Backup", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error)));
            }
        }
        #endregion

        #region User
        private void UserOp(global::System.Object sender, global::System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // init
                global::System.Threading.Thread.CurrentThread.Name = "UserBackup";
                var userInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var friendsInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var lovedInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var historyInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var topTrackInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var topArtistInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var topAlbumInfo = new global::System.Collections.Generic.List<global::System.String[]>();
                var lists = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.String[]>>();
                global::System.Int32 progress = 0;

                // progress multiplier
                this.progressMultiplier = (global::System.Byte)0;
                // info
                if (((this.columnContents[0]) == (true)))
                {
                    userInfo.Add(new[] { "Username", "Real Name", "Url", "Country", "Age", "Gender", "Playcount", "Playlists", "Date Registered" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // friends
                if (((this.columnContents[1]) == (true)))
                {
                    friendsInfo.Add(new[] { "Name", "Real Name", "Url", "Date Registered" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // loved songs
                if (((this.columnContents[2]) == (true)))
                {
                    lovedInfo.Add(new[] { "Loved Track", "Artist", "Date Loved (Unix)" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // history
                if (((this.columnContents[3]) == (true)))
                {
                    historyInfo.Add(new[] { "Historical Track", "Artist", "Album", "Date Scrobbled" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }
                // charts
                if (((this.columnContents[4]) == (true)))
                {
                    topTrackInfo.Add(new[] { "Top Track", "Artist", "User Playcount" });
                    topArtistInfo.Add(new[] { "Top Artist", "User Playcount" });
                    topAlbumInfo.Add(new[] { "Top Album", "Artist", "User Playcount" });
                    this.progressMultiplier = (global::System.Byte)(this.progressMultiplier + 1);
                }

                // info
                if (((this.columnContents[0]) == (true)))
                {
                    lists.Add(userInfo);
                    // get all xml pages
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(this.userResults) / (50d)));
                    global::System.String xmlPage;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Getting user info...");

                    xmlPage = global::Audiograph.Utilities.CallAPI("user.getInfo", this.textContents[0]);

                    // cancel check
                    if (((this.bgwUser.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 80;

                    // progress
                    this.UpdateProgress(false, (global::System.Double)progress, "Parsing user info...");

                    // parse
                    global::System.String[] xmlNodes = new global::System.String[] { "name", "realname", "url", "country", "age", "gender", "playcount", "playlists", "registered" };
                    global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/user", 0U, ref xmlNodes);

                    // gender formatting
                    switch ((xmlNodes[5] ?? ""))
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
                    if ((xmlNodes[4] == "0"))
                    {
                        xmlNodes[4] = "Not Specified";
                    }

                    userInfo.Add(xmlNodes);

                    // cancel check
                    if (((this.bgwUser.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress += 20;
                }

                // friends
                if (((this.columnContents[1]) == (true)))
                {
                    lists.Add(friendsInfo);
                    // get total
                    global::System.Int32 userResults2 = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(global::Audiograph.Utilities.CallAPI("user.getFriends", this.textContents[0]), "total="));
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(userResults2) / (50d)));
                    var xmlPages = new global::System.Collections.Generic.List<global::System.String>();

                    for (global::System.Int32 currentPage = 0, loopTo = (pageAmount) - (1); currentPage <= loopTo; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (80d)))) + (global::System.Double)(progress), ((("Getting user friends... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        if (((currentPage) == ((pageAmount) - (1))))
                        {
                            // last page, only request leftover
                            if (((userResults2) <= (50)))
                            {
                                // if results below 50
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getFriends", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=" + userResults2.ToString()));
                            }
                            else
                            {
                                // if not below 50 get remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getFriends", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                            }
                        }
                        else
                        {
                            xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getFriends", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                        }

                        // cancel check
                        if (((this.bgwUser.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 80;

                    // add to list
                    global::System.Int32 currentPageAmount;
                    global::System.String[] xmlNodes;
                    for (global::System.Int32 currentPage = 0, loopTo1 = (pageAmount) - (1); currentPage <= loopTo1; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (20d)))) + (global::System.Double)(progress), ((("Parsing user friends... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        currentPageAmount = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                        // parse
                        for (global::System.Int32 user = 0, loopTo2 = (currentPageAmount) - (1); user <= loopTo2; user++)
                        {
                            xmlNodes = new[] { "name", "realname", "url", "registered" };
                            global::Audiograph.Utilities.ParseXML(xmlPages[currentPage], "/lfm/friends/user", (global::System.UInt32)user, ref xmlNodes);
                            friendsInfo.Add(xmlNodes);

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }
                        }
                    }

                    progress += 20;
                }

                // loved songs
                if (((this.columnContents[2]) == (true)))
                {
                    lists.Add(lovedInfo);
                    // get total
                    global::System.Int32 userResults2 = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(global::Audiograph.Utilities.CallAPI("user.getLovedTracks", this.textContents[0]), "total="));
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(userResults2) / (50d)));
                    var xmlPages = new global::System.Collections.Generic.List<global::System.String>();

                    for (global::System.Int32 currentPage = 0, loopTo3 = (pageAmount) - (1); currentPage <= loopTo3; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (80d)))) + (global::System.Double)(progress), ((("Getting loved tracks... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        if (((currentPage) == ((pageAmount) - (1))))
                        {
                            // last page, only request leftover
                            if (((userResults2) <= (50)))
                            {
                                // if results below 50
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getLovedTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=" + userResults2.ToString()));
                            }
                            else
                            {
                                // if not below 50 get remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getLovedTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                            }
                        }
                        else
                        {
                            xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getLovedTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                        }

                        // cancel check
                        if (((this.bgwUser.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 80;

                    // add to list
                    global::System.Int32 currentPageAmount;
                    global::System.String[] xmlNodes;
                    for (global::System.Int32 currentPage = 0, loopTo4 = (pageAmount) - (1); currentPage <= loopTo4; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (20d)))) + (global::System.Double)(progress), ((("Parsing loved tracks... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                        currentPageAmount = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                        // parse
                        for (global::System.Int32 track = 0, loopTo5 = (currentPageAmount) - (1); track <= loopTo5; track++)
                        {
                            xmlNodes = new[] { "name", "artist/name", "date" };
                            global::Audiograph.Utilities.ParseXML(xmlPages[currentPage], "/lfm/lovedtracks/track", (global::System.UInt32)track, ref xmlNodes);
                            lovedInfo.Add(xmlNodes);

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }
                        }
                    }

                    progress += 20;
                }

                // play history
                if (((this.columnContents[3]) == (true)))
                {
                    lists.Add(historyInfo);
                    // get total
                    global::System.Int32 userResults2;
                    if (((this.userResults) == (0)))
                    {
                        if (((this.dateContents.Count) == (0)))
                        {
                            userResults2 = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(global::Audiograph.Utilities.CallAPI("user.getRecentTracks", this.textContents[0]), "total="));
                        }
                        else
                        {
                            userResults2 = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(global::Audiograph.Utilities.CallAPI("user.getRecentTracks", this.textContents[0], "from=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[0]).ToString(), "to=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[1]).ToString()), "total="));
                        }
                    }
                    else
                    {
                        userResults2 = this.userResults;
                    }
                    global::System.Int32 pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(userResults2) / (200d)));
                    var xmlPages = new global::System.Collections.Generic.List<global::System.String>();

                    for (global::System.Int32 currentPage = 0, loopTo6 = (pageAmount) - (1); currentPage <= loopTo6; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (80d)))) + (global::System.Double)(progress), ((("Getting play history... (" + ((((((currentPage) + (1)))) * (200))).ToString()) + " of ") + (((pageAmount) * (200))).ToString()) + ")");

                        if (((this.dateContents.Count) == (0)))
                        {
                            if (((currentPage) == ((pageAmount) - (1))))
                            {
                                // last page, only request leftover
                                if (((userResults2) <= (200)))
                                {
                                    // if results below 200
                                    xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getRecentTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=" + userResults2.ToString()));
                                }
                                else
                                {
                                    // if not below 200 get remainder
                                    xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getRecentTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=200"));
                                }
                            }
                            else
                            {
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getRecentTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=200"));
                            }
                        }
                        else if (((currentPage) == ((pageAmount) - (1))))
                        {
                            // last page, only request leftover
                            if (((userResults2) <= (200)))
                            {
                                // if results below 200
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getRecentTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=" + userResults2.ToString(), "from=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[0]).ToString(), "to=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[1]).ToString()));
                            }
                            else
                            {
                                // if not below 200 get remainder
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getRecentTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=200", "from=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[0]).ToString(), "to=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[1]).ToString()));
                            }
                        }
                        else
                        {
                            xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getRecentTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=200", "from=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[0]).ToString(), "to=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[1]).ToString()));
                        }

                        // cancel check
                        if (((this.bgwUser.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 80;

                    // add to list
                    global::System.Int32 currentPageAmount;
                    global::System.String[] xmlNodes;
                    for (global::System.Int32 currentPage = 0, loopTo7 = (pageAmount) - (1); currentPage <= loopTo7; currentPage++)
                    {
                        // progress
                        this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (20d)))) + (global::System.Double)(progress), ((("Parsing play history... (" + ((((((currentPage) + (1)))) * (200))).ToString()) + " of ") + (((pageAmount) * (200))).ToString()) + ")");

                        if (((currentPage) == ((pageAmount) - (1))))
                        {
                            currentPageAmount = ((userResults2) % (200));
                        }
                        else
                        {
                            try
                            {
                                currentPageAmount = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());
                            }
                            catch (global::System.Exception ex)
                            {
                                currentPageAmount = 200;
                            }
                        }


                        // parse
                        for (global::System.Int32 track = 0, loopTo8 = (currentPageAmount) - (1); track <= loopTo8; track++)
                        {
                            xmlNodes = new[] { "name", "artist", "album", "date" };
                            global::Audiograph.Utilities.ParseXML(xmlPages[currentPage], "/lfm/recenttracks/track", (global::System.UInt32)track, ref xmlNodes);

                            // check for now playing
                            if (((((track) == (0)) && ((currentPage) == (0))) && ((xmlNodes[3].Contains("Object reference not set to an instance of an object")) == (true))))
                            {
                                xmlNodes[3] = "Now Playing";
                            }

                            historyInfo.Add(xmlNodes);

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }
                        }

                        // cancel check
                        if (((this.bgwUser.CancellationPending) == (true)))
                        {
                            return;
                        }
                    }

                    progress += 20;
                }

                // charts
                if (((this.columnContents[4]) == (true)))
                {
                    lists.Add(topTrackInfo);
                    lists.Add(topArtistInfo);
                    lists.Add(topAlbumInfo);
                    // results
                    global::System.Int32 topTrackResults = default(global::System.Int32), topArtistResults = default(global::System.Int32), topAlbumResults = default(global::System.Int32);
                    if (((this.userResults) == (0)))
                    {
                        if (((this.dateContents.Count) == (0)))
                        {
                            // if entire but no date
                            topTrackResults = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(global::Audiograph.Utilities.CallAPI("user.getTopTracks", this.textContents[0]), "total="));
                            topArtistResults = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(global::Audiograph.Utilities.CallAPI("user.getTopArtists", this.textContents[0]), "total="));
                            topAlbumResults = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(global::Audiograph.Utilities.CallAPI("user.getTopAlbums", this.textContents[0]), "total="));
                        }
                    }
                    else
                    {
                        // if not entire 
                        topTrackResults = this.userResults;
                        topArtistResults = this.userResults;
                        topAlbumResults = this.userResults;
                    }
                    var pageAmount = default(global::System.Int32);
                    var xmlPages = new global::System.Collections.Generic.List<global::System.String>();
                    var xmlPage = default(global::System.String);

                    #region Top Tracks
                    if (((this.dateContents.Count) == (0)))
                    {
                        pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(topTrackResults) / (50d)));

                        // no date, needs mutliple pages
                        for (global::System.Int32 currentPage = 0, loopTo9 = (pageAmount) - (1); currentPage <= loopTo9; currentPage++)
                        {
                            // progress
                            this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (26.66d)))) + (global::System.Double)(progress), ((("Getting user top tracks... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                            if (((currentPage) == ((pageAmount) - (1))))
                            {
                                // last page, only request leftover
                                if (((topTrackResults) <= (50)))
                                {
                                    // if results below 50
                                    xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getTopTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=" + topTrackResults.ToString()));
                                }
                                else if ((((topTrackResults) % (50)) == (0)))
                                {
                                    // if no remainder
                                    xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getTopTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                                }
                                else
                                {
                                    // if not below 50 get remainder
                                    xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getTopTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=" + (((topTrackResults) % (50))).ToString()));
                                }
                            }
                            else
                            {
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getTopTracks", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                            }

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        // date
                        // progress
                        this.UpdateProgress(false, (global::System.Double)progress, "Getting user top tracks...");

                        xmlPage = global::Audiograph.Utilities.CallAPI("user.getWeeklyTrackChart", this.textContents[0], "from=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[0]).ToString(), "to=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[1]).ToString());
                        // results
                        if (((this.userResults) == (0)))
                        {
                            // if entire
                            topTrackResults = (global::System.Int32)global::Audiograph.Utilities.StrCount(xmlPage, "</track>");
                        }
                        else
                        {
                            // if not entire
                            topTrackResults = this.userResults;
                        }
                        xmlPages.Add(xmlPage);
                    }

                    // cancel check
                    if (((this.bgwUser.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress = (global::System.Int32)Math.Round(progress + 26.66d);

                    // add to list
                    global::System.Int32 currentPageAmount;
                    global::System.String[] xmlNodes;
                    if (((this.dateContents.Count) == (0)))
                    {
                        for (global::System.Int32 currentPage = 0, loopTo10 = (pageAmount) - (1); currentPage <= loopTo10; currentPage++)
                        {
                            // progress
                            this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (6.66d)))) + (global::System.Double)(progress), ((("Parsing user top tracks... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                            currentPageAmount = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                            // parse
                            for (global::System.Int32 track = 0, loopTo11 = (currentPageAmount) - (1); track <= loopTo11; track++)
                            {
                                xmlNodes = new[] { "name", "artist/name", "playcount" };
                                global::Audiograph.Utilities.ParseXML(xmlPages[currentPage], "/lfm/toptracks/track", (global::System.UInt32)track, ref xmlNodes);

                                topTrackInfo.Add(xmlNodes);
                            }

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        // progress
                        this.UpdateProgress(false, (global::System.Double)progress, "Parsing user top tracks...");

                        currentPageAmount = topTrackResults;

                        for (global::System.Int32 track = 0, loopTo12 = (currentPageAmount) - (1); track <= loopTo12; track++)
                        {
                            xmlNodes = new[] { "name", "artist", "playcount" };
                            global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/weeklytrackchart/track", (global::System.UInt32)track, ref xmlNodes);

                            topTrackInfo.Add(xmlNodes);

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }
                        }
                    }

                    progress = (global::System.Int32)Math.Round(progress + 6.67d);
                    #endregion

                    #region Top Artists
                    xmlPages.Clear();
                    if (((this.dateContents.Count) == (0)))
                    {
                        pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(topArtistResults) / (50d)));

                        // no date, needs mutliple pages
                        for (global::System.Int32 currentPage = 0, loopTo13 = (pageAmount) - (1); currentPage <= loopTo13; currentPage++)
                        {
                            // progress
                            this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (26.66d)))) + (global::System.Double)(progress), ((("Getting user top artists... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                            if (((currentPage) == ((pageAmount) - (1))))
                            {
                                // last page, only request leftover
                                if (((topArtistResults) <= (50)))
                                {
                                    // if results below 50
                                    xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getTopArtists", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=" + topArtistResults.ToString()));
                                }
                                else if ((((topArtistResults) % (50)) == (0)))
                                {
                                    // if no remainder
                                    xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getTopArtists", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                                }
                                else
                                {
                                    // if not below 50 get remainder
                                    xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getTopArtists", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=" + (((topArtistResults) % (50))).ToString()));
                                }
                            }
                            else
                            {
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getTopArtists", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                            }

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        // date
                        // progress
                        this.UpdateProgress(false, (global::System.Double)progress, "Getting user top artists...");

                        xmlPage = global::Audiograph.Utilities.CallAPI("user.getWeeklyArtistChart", this.textContents[0], "from=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[0]).ToString(), "to=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[1]).ToString());
                        // results
                        if (((this.userResults) == (0)))
                        {
                            // if entire
                            topArtistResults = (global::System.Int32)global::Audiograph.Utilities.StrCount(xmlPage, "</artist>");
                        }
                        else
                        {
                            // if not entire
                            topArtistResults = this.userResults;
                        }
                        xmlPages.Add(xmlPage);
                    }

                    // cancel check
                    if (((this.bgwUser.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress = (global::System.Int32)Math.Round(progress + 26.66d);

                    // add to list
                    if (((this.dateContents.Count) == (0)))
                    {
                        for (global::System.Int32 currentPage = 0, loopTo14 = (pageAmount) - (1); currentPage <= loopTo14; currentPage++)
                        {
                            // progress
                            this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (6.66d)))) + (global::System.Double)(progress), ((("Parsing user top artists... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                            currentPageAmount = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                            // parse
                            for (global::System.Int32 artist = 0, loopTo15 = (currentPageAmount) - (1); artist <= loopTo15; artist++)
                            {
                                xmlNodes = new[] { "name", "playcount" };
                                global::Audiograph.Utilities.ParseXML(xmlPages[currentPage], "/lfm/topartists/artist", (global::System.UInt32)artist, ref xmlNodes);

                                topArtistInfo.Add(xmlNodes);
                            }

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        // progress
                        this.UpdateProgress(false, (global::System.Double)progress, "Parsing user top artists...");

                        currentPageAmount = topArtistResults;

                        for (global::System.Int32 artist = 0, loopTo16 = (currentPageAmount) - (1); artist <= loopTo16; artist++)
                        {
                            xmlNodes = new[] { "name", "playcount" };
                            global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/weeklyartistchart/artist", (global::System.UInt32)artist, ref xmlNodes);

                            topArtistInfo.Add(xmlNodes);

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }
                        }
                    }

                    progress = (global::System.Int32)Math.Round(progress + 6.67d);
                    #endregion

                    #region Top Albums
                    xmlPages.Clear();
                    if (((this.dateContents.Count) == (0)))
                    {
                        pageAmount = (global::System.Int32)Math.Round(global::System.Math.Ceiling((global::System.Double)(topAlbumResults) / (50d)));

                        // no date, needs mutliple pages
                        for (global::System.Int32 currentPage = 0, loopTo17 = (pageAmount) - (1); currentPage <= loopTo17; currentPage++)
                        {
                            // progress
                            this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (26.67d)))) + (global::System.Double)(progress), ((("Getting user top albums... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                            if (((currentPage) == ((pageAmount) - (1))))
                            {
                                // last page, only request leftover
                                if (((topAlbumResults) <= (50)))
                                {
                                    // if results below 50
                                    xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getTopAlbums", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=" + topAlbumResults.ToString()));
                                }
                                else if ((((topAlbumResults) % (50)) == (0)))
                                {
                                    // if no remainder
                                    xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getTopAlbums", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                                }
                                else
                                {
                                    // if not below 50 get remainder
                                    xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getTopAlbums", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=" + (((topAlbumResults) % (50))).ToString()));
                                }
                            }
                            else
                            {
                                xmlPages.Add(global::Audiograph.Utilities.CallAPI("user.getTopAlbums", this.textContents[0], "page=" + (((currentPage) + (1))).ToString(), "limit=50"));
                            }

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        // date
                        // progress
                        this.UpdateProgress(false, (global::System.Double)progress, "Getting user top albums...");

                        xmlPage = global::Audiograph.Utilities.CallAPI("user.getWeeklyAlbumChart", this.textContents[0], "from=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[0]).ToString(), "to=" + global::Audiograph.Utilities.DateToUnix(this.dateContents[1]).ToString());
                        // results
                        if (((this.userResults) == (0)))
                        {
                            // if entire
                            topAlbumResults = (global::System.Int32)global::Audiograph.Utilities.StrCount(xmlPage, "</album>");
                        }
                        else
                        {
                            // if not entire
                            topAlbumResults = this.userResults;
                        }
                        xmlPages.Add(xmlPage);
                    }

                    // cancel check
                    if (((this.bgwUser.CancellationPending) == (true)))
                    {
                        return;
                    }

                    progress = (global::System.Int32)Math.Round(progress + 26.67d);

                    // add to list
                    if (((this.dateContents.Count) == (0)))
                    {
                        for (global::System.Int32 currentPage = 0, loopTo18 = (pageAmount) - (1); currentPage <= loopTo18; currentPage++)
                        {
                            // progress
                            this.UpdateProgress(false, (((((((global::System.Double)((((currentPage) + (1)))) / (global::System.Double)(pageAmount)))) * (6.67d)))) + (global::System.Double)(progress), ((("Parsing user top albums... (" + ((((((currentPage) + (1)))) * (50))).ToString()) + " of ") + (((pageAmount) * (50))).ToString()) + ")");

                            currentPageAmount = Conversions.ToInteger(global::Audiograph.Utilities.ParseMetadata(xmlPages[currentPage], "perPage=").Trim());

                            // parse
                            for (global::System.Int32 album = 0, loopTo19 = (currentPageAmount) - (1); album <= loopTo19; album++)
                            {
                                xmlNodes = new[] { "name", "artist/name", "playcount" };
                                global::Audiograph.Utilities.ParseXML(xmlPages[currentPage], "/lfm/topalbums/album", (global::System.UInt32)album, ref xmlNodes);

                                topAlbumInfo.Add(xmlNodes);

                                // cancel check
                                if (((this.bgwUser.CancellationPending) == (true)))
                                {
                                    return;
                                }
                            }

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        // progress
                        this.UpdateProgress(false, (global::System.Double)progress, "Parsing user top albums...");

                        currentPageAmount = topAlbumResults;

                        for (global::System.Int32 album = 0, loopTo20 = (currentPageAmount) - (1); album <= loopTo20; album++)
                        {
                            xmlNodes = new[] { "name", "artist", "playcount" };
                            global::Audiograph.Utilities.ParseXML(xmlPage, "/lfm/weeklyalbumchart/album", (global::System.UInt32)album, ref xmlNodes);

                            topAlbumInfo.Add(xmlNodes);

                            // cancel check
                            if (((this.bgwUser.CancellationPending) == (true)))
                            {
                                return;
                            }
                        }
                    }

                    progress = (global::System.Int32)Math.Round(progress + 6.67d);
                    #endregion
                }

                this.Save(lists.ToArray());
                this.Invoke(new Action(() => this.StopOp()));
            }
            catch (global::System.Exception ex)
            {
                this.Invoke(new Action(() => global::System.Windows.Forms.MessageBox.Show("ERROR: " + ex.Message, "User Backup", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error)));
            }
        }

        private void UserUseCurrent(global::System.Object sender, global::System.EventArgs e)
        {
            this.txtUserUser.Text = global::Audiograph.My.MySettingsProperty.Settings.User;
        }

        private void UserEnableDate(global::System.Object sender, global::System.EventArgs e)
        {
            if (((this.chkUserByDate.Checked) == (true)))
            {
                this.dtpUserFrom.Enabled = true;
                this.dtpUserTo.Enabled = true;
            }
            else
            {
                this.dtpUserFrom.Enabled = false;
                this.dtpUserTo.Enabled = false;
            }
        }

        private void UserEnableAmount(global::System.Object sender, global::System.EventArgs e)
        {
            if ((((this.chkUserHistory.Checked) == (true)) || ((this.chkUserCharts.Checked) == (true))))
            {
                this.radUserEntire.Enabled = true;
                this.radUserNumber.Enabled = true;
                this.nudUserNumber.Enabled = true;
            }
            else
            {
                this.radUserEntire.Enabled = false;
                this.radUserNumber.Enabled = false;
                this.nudUserNumber.Enabled = false;
                this.radUserEntire.Checked = true;
            }
        }
        #endregion

    }
}