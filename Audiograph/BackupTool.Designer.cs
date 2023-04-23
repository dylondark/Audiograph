using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Audiograph
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmBackupTool : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackupTool));
            txtSave = new TextBox();
            Label3 = new Label();
            btnBrowse = new Button();
            btnBrowse.Click += new EventHandler(Browse);
            Label1 = new Label();
            Label2 = new Label();
            pbStatus = new ProgressBar();
            cmbContents = new ComboBox();
            cmbContents.SelectedIndexChanged += new EventHandler(ChangeContents);
            lblStatus = new Label();
            pnlCharts = new Panel();
            radChartCountry = new RadioButton();
            radChartWorldwide = new RadioButton();
            cmbChartCountry = new ComboBox();
            chkChartTopTags = new CheckBox();
            chkChartTopArtists = new CheckBox();
            chkChartTopTracks = new CheckBox();
            Label6 = new Label();
            nudChartResults = new NumericUpDown();
            Label4 = new Label();
            btnClose = new Button();
            btnClose.Click += new EventHandler(FrmClose);
            btnStart = new Button();
            btnStart.Click += new EventHandler(StartButton);
            btnStop = new Button();
            btnStop.Click += new EventHandler(StopButton);
            pnlAlbum = new Panel();
            chkAlbumTracks = new CheckBox();
            chkAlbumInfo = new CheckBox();
            chkAlbumTags = new CheckBox();
            chkAlbumStats = new CheckBox();
            Label15 = new Label();
            txtAlbumArtist = new TextBox();
            Label16 = new Label();
            txtAlbumAlbum = new TextBox();
            Label17 = new Label();
            pnlUser = new Panel();
            dtpUserTo = new DateTimePicker();
            Label14 = new Label();
            dtpUserFrom = new DateTimePicker();
            chkUserByDate = new CheckBox();
            chkUserByDate.CheckedChanged += new EventHandler(UserEnableDate);
            nudUserNumber = new NumericUpDown();
            radUserNumber = new RadioButton();
            radUserEntire = new RadioButton();
            chkUserCharts = new CheckBox();
            chkUserCharts.CheckedChanged += new EventHandler(UserEnableAmount);
            btnUserUseCurrent = new Button();
            btnUserUseCurrent.Click += new EventHandler(UserUseCurrent);
            chkUserHistory = new CheckBox();
            chkUserHistory.CheckedChanged += new EventHandler(UserEnableAmount);
            chkUserLoved = new CheckBox();
            chkUserFriends = new CheckBox();
            chkUserInfo = new CheckBox();
            Label13 = new Label();
            txtUserUser = new TextBox();
            Label18 = new Label();
            pnlTag = new Panel();
            txtTagTag = new TextBox();
            Label20 = new Label();
            chkTagTopAlbums = new CheckBox();
            chkTagTopArtists = new CheckBox();
            chkTagTopTracks = new CheckBox();
            chkTagInfo = new CheckBox();
            Label5 = new Label();
            nudTagResults = new NumericUpDown();
            Label7 = new Label();
            pnlTrack = new Panel();
            chkTrackSimilar = new CheckBox();
            chkTrackTags = new CheckBox();
            chkTrackStats = new CheckBox();
            chkTrackInfo = new CheckBox();
            Label11 = new Label();
            txtTrackArtist = new TextBox();
            Label8 = new Label();
            txtTrackTitle = new TextBox();
            Label9 = new Label();
            pnlArtist = new Panel();
            chkArtistCharts = new CheckBox();
            chkArtistSimilar = new CheckBox();
            chkArtistTags = new CheckBox();
            chkArtistStats = new CheckBox();
            Label10 = new Label();
            txtArtistArtist = new TextBox();
            Label12 = new Label();
            sfdSave = new SaveFileDialog();
            bgwChart = new System.ComponentModel.BackgroundWorker();
            bgwChart.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundStopOp);
            bgwChart.DoWork += new System.ComponentModel.DoWorkEventHandler(ChartOp);
            bgwTag = new System.ComponentModel.BackgroundWorker();
            bgwTag.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundStopOp);
            bgwTag.DoWork += new System.ComponentModel.DoWorkEventHandler(TagOp);
            bgwTrack = new System.ComponentModel.BackgroundWorker();
            bgwTrack.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundStopOp);
            bgwTrack.DoWork += new System.ComponentModel.DoWorkEventHandler(TrackOp);
            bgwArtist = new System.ComponentModel.BackgroundWorker();
            bgwArtist.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundStopOp);
            bgwArtist.DoWork += new System.ComponentModel.DoWorkEventHandler(ArtistOp);
            bgwAlbum = new System.ComponentModel.BackgroundWorker();
            bgwAlbum.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundStopOp);
            bgwAlbum.DoWork += new System.ComponentModel.DoWorkEventHandler(AlbumOp);
            bgwUser = new System.ComponentModel.BackgroundWorker();
            bgwUser.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundStopOp);
            bgwUser.DoWork += new System.ComponentModel.DoWorkEventHandler(UserOp);
            pnlCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudChartResults).BeginInit();
            pnlAlbum.SuspendLayout();
            pnlUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudUserNumber).BeginInit();
            pnlTag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTagResults).BeginInit();
            pnlTrack.SuspendLayout();
            pnlArtist.SuspendLayout();
            SuspendLayout();
            // 
            // txtSave
            // 
            txtSave.Location = new Point(11, 30);
            txtSave.Name = "txtSave";
            txtSave.Size = new Size(185, 20);
            txtSave.TabIndex = 1;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3.Location = new Point(8, 5);
            Label3.Name = "Label3";
            Label3.Size = new Size(106, 21);
            Label3.TabIndex = 0;
            Label3.Text = "Save Location";
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(202, 28);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "&Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label1.Location = new Point(8, 61);
            Label1.Name = "Label1";
            Label1.Size = new Size(72, 21);
            Label1.TabIndex = 3;
            Label1.Text = "Contents";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.Location = new Point(7, 276);
            Label2.Name = "Label2";
            Label2.Size = new Size(52, 21);
            Label2.TabIndex = 5;
            Label2.Text = "Status";
            // 
            // pbStatus
            // 
            pbStatus.Location = new Point(11, 301);
            pbStatus.Name = "pbStatus";
            pbStatus.Size = new Size(266, 26);
            pbStatus.Style = ProgressBarStyle.Continuous;
            pbStatus.TabIndex = 7;
            // 
            // cmbContents
            // 
            cmbContents.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbContents.FormattingEnabled = true;
            cmbContents.Items.AddRange(new object[] { "Charts", "Tag", "Track", "Artist", "Album", "User" });
            cmbContents.Location = new Point(81, 63);
            cmbContents.Name = "cmbContents";
            cmbContents.Size = new Size(82, 21);
            cmbContents.TabIndex = 4;
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(51, 282);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(226, 13);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlCharts
            // 
            pnlCharts.BackColor = SystemColors.ControlLight;
            pnlCharts.BorderStyle = BorderStyle.FixedSingle;
            pnlCharts.Controls.Add(radChartCountry);
            pnlCharts.Controls.Add(radChartWorldwide);
            pnlCharts.Controls.Add(cmbChartCountry);
            pnlCharts.Controls.Add(chkChartTopTags);
            pnlCharts.Controls.Add(chkChartTopArtists);
            pnlCharts.Controls.Add(chkChartTopTracks);
            pnlCharts.Controls.Add(Label6);
            pnlCharts.Controls.Add(nudChartResults);
            pnlCharts.Controls.Add(Label4);
            pnlCharts.Location = new Point(11, 90);
            pnlCharts.Name = "pnlCharts";
            pnlCharts.Size = new Size(266, 176);
            pnlCharts.TabIndex = 71;
            // 
            // radChartCountry
            // 
            radChartCountry.AutoSize = true;
            radChartCountry.Location = new Point(87, 35);
            radChartCountry.Name = "radChartCountry";
            radChartCountry.Size = new Size(76, 17);
            radChartCountry.TabIndex = 9;
            radChartCountry.Text = "By Country";
            radChartCountry.UseVisualStyleBackColor = true;
            // 
            // radChartWorldwide
            // 
            radChartWorldwide.AutoSize = true;
            radChartWorldwide.Checked = true;
            radChartWorldwide.Location = new Point(6, 35);
            radChartWorldwide.Name = "radChartWorldwide";
            radChartWorldwide.Size = new Size(75, 17);
            radChartWorldwide.TabIndex = 8;
            radChartWorldwide.TabStop = true;
            radChartWorldwide.Text = "Worldwide";
            radChartWorldwide.UseVisualStyleBackColor = true;
            // 
            // cmbChartCountry
            // 
            cmbChartCountry.Enabled = false;
            cmbChartCountry.FormattingEnabled = true;
            cmbChartCountry.Items.AddRange(new object[] { "United States", "Afghanistan", "Åland Islands", "Albania", "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia, Plurinational State of", "Bonaire, Sint Eustatius and Saba", "Bosnia and Herzegovina", "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean Territory", "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands", "Central African Republic", "Chad", "Chile", "China", "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo", "Congo, The Democratic Republic of the", "Cook Islands", "Costa Rica", "Côte D'Ivoire", "Croatia", "Cuba", "Curaçao", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Falkland Islands (Malvinas)", "Faroe Islands", "Fiji", "Finland", "France", "French Guiana", "French Polynesia", "French Southern Territories", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard Island and McDonald Islands", "Holy See (Vatican City State)", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran, Islamic Republic of", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Jamaica", "Japan", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea, Democratic People's Republic of", "Korea, Republic of", "Kuwait", "Kyrgyzstan", "Lao People's Democratic Republic", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macao", "Macedonia, The former Yugoslav Republic of", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia, Federated States of", "Moldova, Republic of", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "Northern Mariana Islands", "Norway", "Oman", "Pakistan", "Palau", "Palestine, State of", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Pitcairn", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russian Federation", "Rwanda", "Saint Barthélemy", "Saint Helena, Ascension and Tristan Da Cunha", "Saint Kitts and Nevis", "Saint Lucia", "Saint Martin (French Part)", "Saint Pierre and Miquelon", "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Sint Maarten (Dutch Part)", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Georgia and the South Sandwich Islands", "South Sudan", "Spain", "Sri Lanka", "Sudan", "Suriname", "Svalbard and Jan Mayen", "Swaziland", "Sweden", "Switzerland", "Syrian Arab Republic", "Taiwan, Province of China", "Tajikistan", "Tanzania, United Republic of", "Thailand", "Timor-Leste", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States Minor Outlying Islands", "Uruguay", "Uzbekistan", "Vanuatu", "Venezuela", "Viet Nam", "Virgin Islands, British", "Virgin Islands, U.S.", "Wallis and Futuna", "Western Sahara", "Yemen", "Zambia", "Zimbabwe" });
            cmbChartCountry.Location = new Point(87, 58);
            cmbChartCountry.Name = "cmbChartCountry";
            cmbChartCountry.Size = new Size(138, 21);
            cmbChartCountry.TabIndex = 7;
            cmbChartCountry.Text = "United States";
            // 
            // chkChartTopTags
            // 
            chkChartTopTags.AutoSize = true;
            chkChartTopTags.Checked = true;
            chkChartTopTags.CheckState = CheckState.Checked;
            chkChartTopTags.Location = new Point(52, 135);
            chkChartTopTags.Name = "chkChartTopTags";
            chkChartTopTags.Size = new Size(72, 17);
            chkChartTopTags.TabIndex = 5;
            chkChartTopTags.Text = "Top Tags";
            chkChartTopTags.UseVisualStyleBackColor = true;
            // 
            // chkChartTopArtists
            // 
            chkChartTopArtists.AutoSize = true;
            chkChartTopArtists.Checked = true;
            chkChartTopArtists.CheckState = CheckState.Checked;
            chkChartTopArtists.Location = new Point(52, 112);
            chkChartTopArtists.Name = "chkChartTopArtists";
            chkChartTopArtists.Size = new Size(76, 17);
            chkChartTopArtists.TabIndex = 4;
            chkChartTopArtists.Text = "Top Artists";
            chkChartTopArtists.UseVisualStyleBackColor = true;
            // 
            // chkChartTopTracks
            // 
            chkChartTopTracks.AutoSize = true;
            chkChartTopTracks.Checked = true;
            chkChartTopTracks.CheckState = CheckState.Checked;
            chkChartTopTracks.Location = new Point(52, 89);
            chkChartTopTracks.Name = "chkChartTopTracks";
            chkChartTopTracks.Size = new Size(81, 17);
            chkChartTopTracks.TabIndex = 3;
            chkChartTopTracks.Text = "Top Tracks";
            chkChartTopTracks.UseVisualStyleBackColor = true;
            // 
            // Label6
            // 
            Label6.AutoSize = true;
            Label6.Location = new Point(3, 89);
            Label6.Name = "Label6";
            Label6.Size = new Size(50, 13);
            Label6.TabIndex = 2;
            Label6.Text = "Columns:";
            // 
            // nudChartResults
            // 
            nudChartResults.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            nudChartResults.Location = new Point(93, 5);
            nudChartResults.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudChartResults.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudChartResults.Name = "nudChartResults";
            nudChartResults.Size = new Size(52, 20);
            nudChartResults.TabIndex = 1;
            nudChartResults.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(3, 7);
            Label4.Name = "Label4";
            Label4.Size = new Size(93, 13);
            Label4.TabIndex = 0;
            Label4.Text = "Results (1-10000):";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(11, 351);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 8;
            btnClose.Text = "&Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(202, 351);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 10;
            btnStart.Text = "&Start";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(121, 351);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 23);
            btnStop.TabIndex = 9;
            btnStop.Text = "S&top";
            btnStop.UseVisualStyleBackColor = true;
            // 
            // pnlAlbum
            // 
            pnlAlbum.BackColor = SystemColors.ControlLight;
            pnlAlbum.BorderStyle = BorderStyle.FixedSingle;
            pnlAlbum.Controls.Add(chkAlbumTracks);
            pnlAlbum.Controls.Add(chkAlbumInfo);
            pnlAlbum.Controls.Add(chkAlbumTags);
            pnlAlbum.Controls.Add(chkAlbumStats);
            pnlAlbum.Controls.Add(Label15);
            pnlAlbum.Controls.Add(txtAlbumArtist);
            pnlAlbum.Controls.Add(Label16);
            pnlAlbum.Controls.Add(txtAlbumAlbum);
            pnlAlbum.Controls.Add(Label17);
            pnlAlbum.Location = new Point(11, 90);
            pnlAlbum.Name = "pnlAlbum";
            pnlAlbum.Size = new Size(266, 176);
            pnlAlbum.TabIndex = 80;
            // 
            // chkAlbumTracks
            // 
            chkAlbumTracks.AutoSize = true;
            chkAlbumTracks.Checked = true;
            chkAlbumTracks.CheckState = CheckState.Checked;
            chkAlbumTracks.Location = new Point(52, 82);
            chkAlbumTracks.Name = "chkAlbumTracks";
            chkAlbumTracks.Size = new Size(59, 17);
            chkAlbumTracks.TabIndex = 13;
            chkAlbumTracks.Text = "Tracks";
            chkAlbumTracks.UseVisualStyleBackColor = true;
            // 
            // chkAlbumInfo
            // 
            chkAlbumInfo.AutoSize = true;
            chkAlbumInfo.Checked = true;
            chkAlbumInfo.CheckState = CheckState.Checked;
            chkAlbumInfo.Location = new Point(52, 60);
            chkAlbumInfo.Name = "chkAlbumInfo";
            chkAlbumInfo.Size = new Size(44, 17);
            chkAlbumInfo.TabIndex = 12;
            chkAlbumInfo.Text = "Info";
            chkAlbumInfo.UseVisualStyleBackColor = true;
            // 
            // chkAlbumTags
            // 
            chkAlbumTags.AutoSize = true;
            chkAlbumTags.Checked = true;
            chkAlbumTags.CheckState = CheckState.Checked;
            chkAlbumTags.Location = new Point(52, 126);
            chkAlbumTags.Name = "chkAlbumTags";
            chkAlbumTags.Size = new Size(50, 17);
            chkAlbumTags.TabIndex = 9;
            chkAlbumTags.Text = "Tags";
            chkAlbumTags.UseVisualStyleBackColor = true;
            // 
            // chkAlbumStats
            // 
            chkAlbumStats.AutoSize = true;
            chkAlbumStats.Checked = true;
            chkAlbumStats.CheckState = CheckState.Checked;
            chkAlbumStats.Location = new Point(52, 104);
            chkAlbumStats.Name = "chkAlbumStats";
            chkAlbumStats.Size = new Size(50, 17);
            chkAlbumStats.TabIndex = 8;
            chkAlbumStats.Text = "Stats";
            chkAlbumStats.UseVisualStyleBackColor = true;
            // 
            // Label15
            // 
            Label15.AutoSize = true;
            Label15.Location = new Point(3, 59);
            Label15.Name = "Label15";
            Label15.Size = new Size(50, 13);
            Label15.TabIndex = 6;
            Label15.Text = "Columns:";
            // 
            // txtAlbumArtist
            // 
            txtAlbumArtist.Location = new Point(33, 30);
            txtAlbumArtist.Name = "txtAlbumArtist";
            txtAlbumArtist.Size = new Size(227, 20);
            txtAlbumArtist.TabIndex = 3;
            // 
            // Label16
            // 
            Label16.AutoSize = true;
            Label16.Location = new Point(3, 33);
            Label16.Name = "Label16";
            Label16.Size = new Size(33, 13);
            Label16.TabIndex = 2;
            Label16.Text = "Artist:";
            // 
            // txtAlbumAlbum
            // 
            txtAlbumAlbum.Location = new Point(39, 4);
            txtAlbumAlbum.Name = "txtAlbumAlbum";
            txtAlbumAlbum.Size = new Size(221, 20);
            txtAlbumAlbum.TabIndex = 1;
            // 
            // Label17
            // 
            Label17.AutoSize = true;
            Label17.Location = new Point(3, 7);
            Label17.Name = "Label17";
            Label17.Size = new Size(39, 13);
            Label17.TabIndex = 0;
            Label17.Text = "Album:";
            // 
            // pnlUser
            // 
            pnlUser.BackColor = SystemColors.ControlLight;
            pnlUser.BorderStyle = BorderStyle.FixedSingle;
            pnlUser.Controls.Add(dtpUserTo);
            pnlUser.Controls.Add(Label14);
            pnlUser.Controls.Add(dtpUserFrom);
            pnlUser.Controls.Add(chkUserByDate);
            pnlUser.Controls.Add(nudUserNumber);
            pnlUser.Controls.Add(radUserNumber);
            pnlUser.Controls.Add(radUserEntire);
            pnlUser.Controls.Add(chkUserCharts);
            pnlUser.Controls.Add(btnUserUseCurrent);
            pnlUser.Controls.Add(chkUserHistory);
            pnlUser.Controls.Add(chkUserLoved);
            pnlUser.Controls.Add(chkUserFriends);
            pnlUser.Controls.Add(chkUserInfo);
            pnlUser.Controls.Add(Label13);
            pnlUser.Controls.Add(txtUserUser);
            pnlUser.Controls.Add(Label18);
            pnlUser.Location = new Point(11, 90);
            pnlUser.Name = "pnlUser";
            pnlUser.Size = new Size(266, 176);
            pnlUser.TabIndex = 80;
            // 
            // dtpUserTo
            // 
            dtpUserTo.Enabled = false;
            dtpUserTo.Format = DateTimePickerFormat.Short;
            dtpUserTo.Location = new Point(177, 30);
            dtpUserTo.Name = "dtpUserTo";
            dtpUserTo.Size = new Size(82, 20);
            dtpUserTo.TabIndex = 24;
            // 
            // Label14
            // 
            Label14.AutoSize = true;
            Label14.Location = new Point(161, 33);
            Label14.Name = "Label14";
            Label14.Size = new Size(16, 13);
            Label14.TabIndex = 23;
            Label14.Text = "to";
            // 
            // dtpUserFrom
            // 
            dtpUserFrom.Enabled = false;
            dtpUserFrom.Format = DateTimePickerFormat.Short;
            dtpUserFrom.Location = new Point(76, 29);
            dtpUserFrom.Name = "dtpUserFrom";
            dtpUserFrom.Size = new Size(82, 20);
            dtpUserFrom.TabIndex = 22;
            // 
            // chkUserByDate
            // 
            chkUserByDate.AutoSize = true;
            chkUserByDate.Location = new Point(6, 32);
            chkUserByDate.Name = "chkUserByDate";
            chkUserByDate.Size = new Size(64, 17);
            chkUserByDate.TabIndex = 21;
            chkUserByDate.Text = "By Date";
            chkUserByDate.UseVisualStyleBackColor = true;
            // 
            // nudUserNumber
            // 
            nudUserNumber.Enabled = false;
            nudUserNumber.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            nudUserNumber.Location = new Point(201, 137);
            nudUserNumber.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudUserNumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudUserNumber.Name = "nudUserNumber";
            nudUserNumber.Size = new Size(59, 20);
            nudUserNumber.TabIndex = 20;
            nudUserNumber.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // radUserNumber
            // 
            radUserNumber.AutoSize = true;
            radUserNumber.Enabled = false;
            radUserNumber.Location = new Point(184, 140);
            radUserNumber.Name = "radUserNumber";
            radUserNumber.Size = new Size(14, 13);
            radUserNumber.TabIndex = 19;
            radUserNumber.UseVisualStyleBackColor = true;
            // 
            // radUserEntire
            // 
            radUserEntire.AutoSize = true;
            radUserEntire.Checked = true;
            radUserEntire.Enabled = false;
            radUserEntire.Location = new Point(134, 137);
            radUserEntire.Name = "radUserEntire";
            radUserEntire.Size = new Size(52, 17);
            radUserEntire.TabIndex = 18;
            radUserEntire.TabStop = true;
            radUserEntire.Text = "Entire";
            radUserEntire.UseVisualStyleBackColor = true;
            // 
            // chkUserCharts
            // 
            chkUserCharts.AutoSize = true;
            chkUserCharts.Checked = true;
            chkUserCharts.CheckState = CheckState.Checked;
            chkUserCharts.Location = new Point(52, 146);
            chkUserCharts.Name = "chkUserCharts";
            chkUserCharts.Size = new Size(56, 17);
            chkUserCharts.TabIndex = 17;
            chkUserCharts.Text = "Charts";
            chkUserCharts.UseVisualStyleBackColor = true;
            // 
            // btnUserUseCurrent
            // 
            btnUserUseCurrent.Location = new Point(185, 3);
            btnUserUseCurrent.Name = "btnUserUseCurrent";
            btnUserUseCurrent.Size = new Size(75, 23);
            btnUserUseCurrent.TabIndex = 12;
            btnUserUseCurrent.Text = "Use Current";
            btnUserUseCurrent.UseVisualStyleBackColor = true;
            // 
            // chkUserHistory
            // 
            chkUserHistory.AutoSize = true;
            chkUserHistory.Checked = true;
            chkUserHistory.CheckState = CheckState.Checked;
            chkUserHistory.Location = new Point(52, 124);
            chkUserHistory.Name = "chkUserHistory";
            chkUserHistory.Size = new Size(81, 17);
            chkUserHistory.TabIndex = 11;
            chkUserHistory.Text = "Play History";
            chkUserHistory.UseVisualStyleBackColor = true;
            // 
            // chkUserLoved
            // 
            chkUserLoved.AutoSize = true;
            chkUserLoved.Checked = true;
            chkUserLoved.CheckState = CheckState.Checked;
            chkUserLoved.Location = new Point(52, 102);
            chkUserLoved.Name = "chkUserLoved";
            chkUserLoved.Size = new Size(89, 17);
            chkUserLoved.TabIndex = 10;
            chkUserLoved.Text = "Loved Songs";
            chkUserLoved.UseVisualStyleBackColor = true;
            // 
            // chkUserFriends
            // 
            chkUserFriends.AutoSize = true;
            chkUserFriends.Checked = true;
            chkUserFriends.CheckState = CheckState.Checked;
            chkUserFriends.Location = new Point(52, 80);
            chkUserFriends.Name = "chkUserFriends";
            chkUserFriends.Size = new Size(60, 17);
            chkUserFriends.TabIndex = 9;
            chkUserFriends.Text = "Friends";
            chkUserFriends.UseVisualStyleBackColor = true;
            // 
            // chkUserInfo
            // 
            chkUserInfo.AutoSize = true;
            chkUserInfo.Checked = true;
            chkUserInfo.CheckState = CheckState.Checked;
            chkUserInfo.Location = new Point(52, 58);
            chkUserInfo.Name = "chkUserInfo";
            chkUserInfo.Size = new Size(44, 17);
            chkUserInfo.TabIndex = 8;
            chkUserInfo.Text = "Info";
            chkUserInfo.UseVisualStyleBackColor = true;
            // 
            // Label13
            // 
            Label13.AutoSize = true;
            Label13.Location = new Point(3, 57);
            Label13.Name = "Label13";
            Label13.Size = new Size(50, 13);
            Label13.TabIndex = 6;
            Label13.Text = "Columns:";
            // 
            // txtUserUser
            // 
            txtUserUser.Location = new Point(32, 4);
            txtUserUser.Name = "txtUserUser";
            txtUserUser.Size = new Size(147, 20);
            txtUserUser.TabIndex = 1;
            // 
            // Label18
            // 
            Label18.AutoSize = true;
            Label18.Location = new Point(3, 7);
            Label18.Name = "Label18";
            Label18.Size = new Size(32, 13);
            Label18.TabIndex = 0;
            Label18.Text = "User:";
            // 
            // pnlTag
            // 
            pnlTag.BackColor = SystemColors.ControlLight;
            pnlTag.BorderStyle = BorderStyle.FixedSingle;
            pnlTag.Controls.Add(txtTagTag);
            pnlTag.Controls.Add(Label20);
            pnlTag.Controls.Add(chkTagTopAlbums);
            pnlTag.Controls.Add(chkTagTopArtists);
            pnlTag.Controls.Add(chkTagTopTracks);
            pnlTag.Controls.Add(chkTagInfo);
            pnlTag.Controls.Add(Label5);
            pnlTag.Controls.Add(nudTagResults);
            pnlTag.Controls.Add(Label7);
            pnlTag.Location = new Point(11, 90);
            pnlTag.Name = "pnlTag";
            pnlTag.Size = new Size(266, 176);
            pnlTag.TabIndex = 81;
            // 
            // txtTagTag
            // 
            txtTagTag.Location = new Point(29, 4);
            txtTagTag.Name = "txtTagTag";
            txtTagTag.Size = new Size(232, 20);
            txtTagTag.TabIndex = 9;
            // 
            // Label20
            // 
            Label20.AutoSize = true;
            Label20.Location = new Point(3, 7);
            Label20.Name = "Label20";
            Label20.Size = new Size(29, 13);
            Label20.TabIndex = 8;
            Label20.Text = "Tag:";
            // 
            // chkTagTopAlbums
            // 
            chkTagTopAlbums.AutoSize = true;
            chkTagTopAlbums.Checked = true;
            chkTagTopAlbums.CheckState = CheckState.Checked;
            chkTagTopAlbums.Location = new Point(52, 127);
            chkTagTopAlbums.Name = "chkTagTopAlbums";
            chkTagTopAlbums.Size = new Size(82, 17);
            chkTagTopAlbums.TabIndex = 7;
            chkTagTopAlbums.Text = "Top Albums";
            chkTagTopAlbums.UseVisualStyleBackColor = true;
            // 
            // chkTagTopArtists
            // 
            chkTagTopArtists.AutoSize = true;
            chkTagTopArtists.Checked = true;
            chkTagTopArtists.CheckState = CheckState.Checked;
            chkTagTopArtists.Location = new Point(52, 104);
            chkTagTopArtists.Name = "chkTagTopArtists";
            chkTagTopArtists.Size = new Size(76, 17);
            chkTagTopArtists.TabIndex = 6;
            chkTagTopArtists.Text = "Top Artists";
            chkTagTopArtists.UseVisualStyleBackColor = true;
            // 
            // chkTagTopTracks
            // 
            chkTagTopTracks.AutoSize = true;
            chkTagTopTracks.Checked = true;
            chkTagTopTracks.CheckState = CheckState.Checked;
            chkTagTopTracks.Location = new Point(52, 81);
            chkTagTopTracks.Name = "chkTagTopTracks";
            chkTagTopTracks.Size = new Size(81, 17);
            chkTagTopTracks.TabIndex = 5;
            chkTagTopTracks.Text = "Top Tracks";
            chkTagTopTracks.UseVisualStyleBackColor = true;
            // 
            // chkTagInfo
            // 
            chkTagInfo.AutoSize = true;
            chkTagInfo.Checked = true;
            chkTagInfo.CheckState = CheckState.Checked;
            chkTagInfo.Location = new Point(52, 58);
            chkTagInfo.Name = "chkTagInfo";
            chkTagInfo.Size = new Size(44, 17);
            chkTagInfo.TabIndex = 3;
            chkTagInfo.Text = "Info";
            chkTagInfo.UseVisualStyleBackColor = true;
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.Location = new Point(3, 58);
            Label5.Name = "Label5";
            Label5.Size = new Size(50, 13);
            Label5.TabIndex = 2;
            Label5.Text = "Columns:";
            // 
            // nudTagResults
            // 
            nudTagResults.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            nudTagResults.Location = new Point(93, 30);
            nudTagResults.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudTagResults.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTagResults.Name = "nudTagResults";
            nudTagResults.Size = new Size(52, 20);
            nudTagResults.TabIndex = 1;
            nudTagResults.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // Label7
            // 
            Label7.AutoSize = true;
            Label7.Location = new Point(3, 32);
            Label7.Name = "Label7";
            Label7.Size = new Size(93, 13);
            Label7.TabIndex = 0;
            Label7.Text = "Results (1-10000):";
            // 
            // pnlTrack
            // 
            pnlTrack.BackColor = SystemColors.ControlLight;
            pnlTrack.BorderStyle = BorderStyle.FixedSingle;
            pnlTrack.Controls.Add(chkTrackSimilar);
            pnlTrack.Controls.Add(chkTrackTags);
            pnlTrack.Controls.Add(chkTrackStats);
            pnlTrack.Controls.Add(chkTrackInfo);
            pnlTrack.Controls.Add(Label11);
            pnlTrack.Controls.Add(txtTrackArtist);
            pnlTrack.Controls.Add(Label8);
            pnlTrack.Controls.Add(txtTrackTitle);
            pnlTrack.Controls.Add(Label9);
            pnlTrack.Location = new Point(11, 90);
            pnlTrack.Name = "pnlTrack";
            pnlTrack.Size = new Size(266, 176);
            pnlTrack.TabIndex = 82;
            // 
            // chkTrackSimilar
            // 
            chkTrackSimilar.AutoSize = true;
            chkTrackSimilar.Checked = true;
            chkTrackSimilar.CheckState = CheckState.Checked;
            chkTrackSimilar.Location = new Point(52, 124);
            chkTrackSimilar.Name = "chkTrackSimilar";
            chkTrackSimilar.Size = new Size(56, 17);
            chkTrackSimilar.TabIndex = 11;
            chkTrackSimilar.Text = "Similar";
            chkTrackSimilar.UseVisualStyleBackColor = true;
            // 
            // chkTrackTags
            // 
            chkTrackTags.AutoSize = true;
            chkTrackTags.Checked = true;
            chkTrackTags.CheckState = CheckState.Checked;
            chkTrackTags.Location = new Point(52, 102);
            chkTrackTags.Name = "chkTrackTags";
            chkTrackTags.Size = new Size(50, 17);
            chkTrackTags.TabIndex = 9;
            chkTrackTags.Text = "Tags";
            chkTrackTags.UseVisualStyleBackColor = true;
            // 
            // chkTrackStats
            // 
            chkTrackStats.AutoSize = true;
            chkTrackStats.Checked = true;
            chkTrackStats.CheckState = CheckState.Checked;
            chkTrackStats.Location = new Point(52, 80);
            chkTrackStats.Name = "chkTrackStats";
            chkTrackStats.Size = new Size(50, 17);
            chkTrackStats.TabIndex = 8;
            chkTrackStats.Text = "Stats";
            chkTrackStats.UseVisualStyleBackColor = true;
            // 
            // chkTrackInfo
            // 
            chkTrackInfo.AutoSize = true;
            chkTrackInfo.Checked = true;
            chkTrackInfo.CheckState = CheckState.Checked;
            chkTrackInfo.Location = new Point(52, 59);
            chkTrackInfo.Name = "chkTrackInfo";
            chkTrackInfo.Size = new Size(44, 17);
            chkTrackInfo.TabIndex = 7;
            chkTrackInfo.Text = "Info";
            chkTrackInfo.UseVisualStyleBackColor = true;
            // 
            // Label11
            // 
            Label11.AutoSize = true;
            Label11.Location = new Point(3, 59);
            Label11.Name = "Label11";
            Label11.Size = new Size(50, 13);
            Label11.TabIndex = 6;
            Label11.Text = "Columns:";
            // 
            // txtTrackArtist
            // 
            txtTrackArtist.Location = new Point(33, 30);
            txtTrackArtist.Name = "txtTrackArtist";
            txtTrackArtist.Size = new Size(227, 20);
            txtTrackArtist.TabIndex = 3;
            // 
            // Label8
            // 
            Label8.AutoSize = true;
            Label8.Location = new Point(3, 33);
            Label8.Name = "Label8";
            Label8.Size = new Size(33, 13);
            Label8.TabIndex = 2;
            Label8.Text = "Artist:";
            // 
            // txtTrackTitle
            // 
            txtTrackTitle.Location = new Point(30, 4);
            txtTrackTitle.Name = "txtTrackTitle";
            txtTrackTitle.Size = new Size(230, 20);
            txtTrackTitle.TabIndex = 1;
            // 
            // Label9
            // 
            Label9.AutoSize = true;
            Label9.Location = new Point(3, 7);
            Label9.Name = "Label9";
            Label9.Size = new Size(30, 13);
            Label9.TabIndex = 0;
            Label9.Text = "Title:";
            // 
            // pnlArtist
            // 
            pnlArtist.BackColor = SystemColors.ControlLight;
            pnlArtist.BorderStyle = BorderStyle.FixedSingle;
            pnlArtist.Controls.Add(chkArtistCharts);
            pnlArtist.Controls.Add(chkArtistSimilar);
            pnlArtist.Controls.Add(chkArtistTags);
            pnlArtist.Controls.Add(chkArtistStats);
            pnlArtist.Controls.Add(Label10);
            pnlArtist.Controls.Add(txtArtistArtist);
            pnlArtist.Controls.Add(Label12);
            pnlArtist.Location = new Point(11, 90);
            pnlArtist.Name = "pnlArtist";
            pnlArtist.Size = new Size(266, 176);
            pnlArtist.TabIndex = 82;
            // 
            // chkArtistCharts
            // 
            chkArtistCharts.AutoSize = true;
            chkArtistCharts.Checked = true;
            chkArtistCharts.CheckState = CheckState.Checked;
            chkArtistCharts.Location = new Point(52, 98);
            chkArtistCharts.Name = "chkArtistCharts";
            chkArtistCharts.Size = new Size(56, 17);
            chkArtistCharts.TabIndex = 12;
            chkArtistCharts.Text = "Charts";
            chkArtistCharts.UseVisualStyleBackColor = true;
            // 
            // chkArtistSimilar
            // 
            chkArtistSimilar.AutoSize = true;
            chkArtistSimilar.Checked = true;
            chkArtistSimilar.CheckState = CheckState.Checked;
            chkArtistSimilar.Location = new Point(52, 76);
            chkArtistSimilar.Name = "chkArtistSimilar";
            chkArtistSimilar.Size = new Size(56, 17);
            chkArtistSimilar.TabIndex = 11;
            chkArtistSimilar.Text = "Similar";
            chkArtistSimilar.UseVisualStyleBackColor = true;
            // 
            // chkArtistTags
            // 
            chkArtistTags.AutoSize = true;
            chkArtistTags.Checked = true;
            chkArtistTags.CheckState = CheckState.Checked;
            chkArtistTags.Location = new Point(52, 54);
            chkArtistTags.Name = "chkArtistTags";
            chkArtistTags.Size = new Size(50, 17);
            chkArtistTags.TabIndex = 9;
            chkArtistTags.Text = "Tags";
            chkArtistTags.UseVisualStyleBackColor = true;
            // 
            // chkArtistStats
            // 
            chkArtistStats.AutoSize = true;
            chkArtistStats.Checked = true;
            chkArtistStats.CheckState = CheckState.Checked;
            chkArtistStats.Location = new Point(52, 32);
            chkArtistStats.Name = "chkArtistStats";
            chkArtistStats.Size = new Size(50, 17);
            chkArtistStats.TabIndex = 8;
            chkArtistStats.Text = "Stats";
            chkArtistStats.UseVisualStyleBackColor = true;
            // 
            // Label10
            // 
            Label10.AutoSize = true;
            Label10.Location = new Point(3, 31);
            Label10.Name = "Label10";
            Label10.Size = new Size(50, 13);
            Label10.TabIndex = 6;
            Label10.Text = "Columns:";
            // 
            // txtArtistArtist
            // 
            txtArtistArtist.Location = new Point(33, 4);
            txtArtistArtist.Name = "txtArtistArtist";
            txtArtistArtist.Size = new Size(227, 20);
            txtArtistArtist.TabIndex = 3;
            // 
            // Label12
            // 
            Label12.AutoSize = true;
            Label12.Location = new Point(3, 7);
            Label12.Name = "Label12";
            Label12.Size = new Size(33, 13);
            Label12.TabIndex = 2;
            Label12.Text = "Artist:";
            // 
            // sfdSave
            // 
            sfdSave.Filter = "CSV files|*.csv|All files|*.*";
            sfdSave.Title = "Save file...";
            // 
            // bgwChart
            // 
            bgwChart.WorkerSupportsCancellation = true;
            // 
            // bgwTag
            // 
            bgwTag.WorkerSupportsCancellation = true;
            // 
            // bgwTrack
            // 
            bgwTrack.WorkerSupportsCancellation = true;
            // 
            // bgwArtist
            // 
            bgwArtist.WorkerSupportsCancellation = true;
            // 
            // bgwAlbum
            // 
            bgwAlbum.WorkerSupportsCancellation = true;
            // 
            // bgwUser
            // 
            bgwUser.WorkerSupportsCancellation = true;
            // 
            // frmBackupTool
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(288, 386);
            Controls.Add(pnlUser);
            Controls.Add(pnlAlbum);
            Controls.Add(pnlArtist);
            Controls.Add(pnlTrack);
            Controls.Add(pnlTag);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(btnClose);
            Controls.Add(pnlCharts);
            Controls.Add(cmbContents);
            Controls.Add(pbStatus);
            Controls.Add(Label2);
            Controls.Add(Label1);
            Controls.Add(btnBrowse);
            Controls.Add(txtSave);
            Controls.Add(Label3);
            Controls.Add(lblStatus);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmBackupTool";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Backup Tool";
            pnlCharts.ResumeLayout(false);
            pnlCharts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudChartResults).EndInit();
            pnlAlbum.ResumeLayout(false);
            pnlAlbum.PerformLayout();
            pnlUser.ResumeLayout(false);
            pnlUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudUserNumber).EndInit();
            pnlTag.ResumeLayout(false);
            pnlTag.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudTagResults).EndInit();
            pnlTrack.ResumeLayout(false);
            pnlTrack.PerformLayout();
            pnlArtist.ResumeLayout(false);
            pnlArtist.PerformLayout();
            Load += new EventHandler(FrmLoad);
            ResumeLayout(false);
            PerformLayout();

        }

        internal TextBox txtSave;
        internal Label Label3;
        internal Button btnBrowse;
        internal Label Label1;
        internal Label Label2;
        internal ProgressBar pbStatus;
        internal ComboBox cmbContents;
        internal Label lblStatus;
        internal Panel pnlCharts;
        internal NumericUpDown nudChartResults;
        internal Label Label4;
        internal ComboBox cmbChartCountry;
        internal CheckBox chkChartTopTags;
        internal CheckBox chkChartTopArtists;
        internal CheckBox chkChartTopTracks;
        internal Label Label6;
        internal RadioButton radChartCountry;
        internal RadioButton radChartWorldwide;
        internal Button btnClose;
        internal Button btnStart;
        internal Button btnStop;
        internal Panel pnlAlbum;
        internal CheckBox chkAlbumTags;
        internal CheckBox chkAlbumStats;
        internal Label Label15;
        internal TextBox txtAlbumArtist;
        internal Label Label16;
        internal TextBox txtAlbumAlbum;
        internal Label Label17;
        internal Panel pnlUser;
        internal NumericUpDown nudUserNumber;
        internal RadioButton radUserNumber;
        internal RadioButton radUserEntire;
        internal CheckBox chkUserCharts;
        internal Button btnUserUseCurrent;
        internal CheckBox chkUserHistory;
        internal CheckBox chkUserLoved;
        internal CheckBox chkUserFriends;
        internal CheckBox chkUserInfo;
        internal Label Label13;
        internal TextBox txtUserUser;
        internal Label Label18;
        internal Panel pnlTag;
        internal Panel pnlTrack;
        internal CheckBox chkTrackSimilar;
        internal CheckBox chkTrackTags;
        internal CheckBox chkTrackStats;
        internal CheckBox chkTrackInfo;
        internal Label Label11;
        internal TextBox txtTrackArtist;
        internal Label Label8;
        internal TextBox txtTrackTitle;
        internal Label Label9;
        internal CheckBox chkTagTopAlbums;
        internal CheckBox chkTagTopArtists;
        internal CheckBox chkTagTopTracks;
        internal CheckBox chkTagInfo;
        internal Label Label5;
        internal NumericUpDown nudTagResults;
        internal Label Label7;
        internal Panel pnlArtist;
        internal CheckBox chkArtistCharts;
        internal CheckBox chkArtistSimilar;
        internal CheckBox chkArtistTags;
        internal CheckBox chkArtistStats;
        internal Label Label10;
        internal TextBox txtArtistArtist;
        internal Label Label12;
        internal TextBox txtTagTag;
        internal Label Label20;
        internal DateTimePicker dtpUserTo;
        internal Label Label14;
        internal DateTimePicker dtpUserFrom;
        internal CheckBox chkUserByDate;
        internal SaveFileDialog sfdSave;
        internal System.ComponentModel.BackgroundWorker bgwChart;
        internal System.ComponentModel.BackgroundWorker bgwTag;
        internal System.ComponentModel.BackgroundWorker bgwTrack;
        internal System.ComponentModel.BackgroundWorker bgwArtist;
        internal System.ComponentModel.BackgroundWorker bgwAlbum;
        internal System.ComponentModel.BackgroundWorker bgwUser;
        internal CheckBox chkAlbumInfo;
        internal CheckBox chkAlbumTracks;
    }
}