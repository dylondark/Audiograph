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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackupTool));
            this.txtSave = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.cmbContents = new System.Windows.Forms.ComboBox();
            this.pnlCharts = new System.Windows.Forms.Panel();
            this.radChartCountry = new System.Windows.Forms.RadioButton();
            this.radChartWorldwide = new System.Windows.Forms.RadioButton();
            this.cmbChartCountry = new System.Windows.Forms.ComboBox();
            this.chkChartTopTags = new System.Windows.Forms.CheckBox();
            this.chkChartTopArtists = new System.Windows.Forms.CheckBox();
            this.chkChartTopTracks = new System.Windows.Forms.CheckBox();
            this.nudChartResults = new System.Windows.Forms.NumericUpDown();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.pnlAlbum = new System.Windows.Forms.Panel();
            this.chkAlbumTracks = new System.Windows.Forms.CheckBox();
            this.chkAlbumInfo = new System.Windows.Forms.CheckBox();
            this.chkAlbumTags = new System.Windows.Forms.CheckBox();
            this.chkAlbumStats = new System.Windows.Forms.CheckBox();
            this.txtAlbumArtist = new System.Windows.Forms.TextBox();
            this.txtAlbumAlbum = new System.Windows.Forms.TextBox();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.dtpUserTo = new System.Windows.Forms.DateTimePicker();
            this.dtpUserFrom = new System.Windows.Forms.DateTimePicker();
            this.chkUserByDate = new System.Windows.Forms.CheckBox();
            this.nudUserNumber = new System.Windows.Forms.NumericUpDown();
            this.radUserNumber = new System.Windows.Forms.RadioButton();
            this.radUserEntire = new System.Windows.Forms.RadioButton();
            this.chkUserCharts = new System.Windows.Forms.CheckBox();
            this.btnUserUseCurrent = new System.Windows.Forms.Button();
            this.chkUserHistory = new System.Windows.Forms.CheckBox();
            this.chkUserLoved = new System.Windows.Forms.CheckBox();
            this.chkUserFriends = new System.Windows.Forms.CheckBox();
            this.chkUserInfo = new System.Windows.Forms.CheckBox();
            this.txtUserUser = new System.Windows.Forms.TextBox();
            this.pnlTag = new System.Windows.Forms.Panel();
            this.txtTagTag = new System.Windows.Forms.TextBox();
            this.chkTagTopAlbums = new System.Windows.Forms.CheckBox();
            this.chkTagTopArtists = new System.Windows.Forms.CheckBox();
            this.chkTagTopTracks = new System.Windows.Forms.CheckBox();
            this.chkTagInfo = new System.Windows.Forms.CheckBox();
            this.nudTagResults = new System.Windows.Forms.NumericUpDown();
            this.pnlTrack = new System.Windows.Forms.Panel();
            this.chkTrackSimilar = new System.Windows.Forms.CheckBox();
            this.chkTrackTags = new System.Windows.Forms.CheckBox();
            this.chkTrackStats = new System.Windows.Forms.CheckBox();
            this.chkTrackInfo = new System.Windows.Forms.CheckBox();
            this.txtTrackArtist = new System.Windows.Forms.TextBox();
            this.txtTrackTitle = new System.Windows.Forms.TextBox();
            this.pnlArtist = new System.Windows.Forms.Panel();
            this.chkArtistCharts = new System.Windows.Forms.CheckBox();
            this.chkArtistSimilar = new System.Windows.Forms.CheckBox();
            this.chkArtistTags = new System.Windows.Forms.CheckBox();
            this.chkArtistStats = new System.Windows.Forms.CheckBox();
            this.txtArtistArtist = new System.Windows.Forms.TextBox();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.bgwChart = new System.ComponentModel.BackgroundWorker();
            this.bgwTag = new System.ComponentModel.BackgroundWorker();
            this.bgwTrack = new System.ComponentModel.BackgroundWorker();
            this.bgwArtist = new System.ComponentModel.BackgroundWorker();
            this.bgwAlbum = new System.ComponentModel.BackgroundWorker();
            this.bgwUser = new System.ComponentModel.BackgroundWorker();
            this.Label14 = new Audiograph.Label();
            this.Label13 = new Audiograph.Label();
            this.Label18 = new Audiograph.Label();
            this.Label15 = new Audiograph.Label();
            this.Label16 = new Audiograph.Label();
            this.Label17 = new Audiograph.Label();
            this.Label10 = new Audiograph.Label();
            this.Label12 = new Audiograph.Label();
            this.Label11 = new Audiograph.Label();
            this.Label8 = new Audiograph.Label();
            this.Label9 = new Audiograph.Label();
            this.Label20 = new Audiograph.Label();
            this.Label5 = new Audiograph.Label();
            this.Label7 = new Audiograph.Label();
            this.Label6 = new Audiograph.Label();
            this.Label4 = new Audiograph.Label();
            this.Label2 = new Audiograph.Label();
            this.Label1 = new Audiograph.Label();
            this.Label3 = new Audiograph.Label();
            this.lblStatus = new Audiograph.Label();
            this.pnlCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChartResults)).BeginInit();
            this.pnlAlbum.SuspendLayout();
            this.pnlUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserNumber)).BeginInit();
            this.pnlTag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTagResults)).BeginInit();
            this.pnlTrack.SuspendLayout();
            this.pnlArtist.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSave
            // 
            this.txtSave.Location = new System.Drawing.Point(11, 30);
            this.txtSave.Name = "txtSave";
            this.txtSave.Size = new System.Drawing.Size(185, 20);
            this.txtSave.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(202, 28);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.Browse);
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(11, 301);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(266, 26);
            this.pbStatus.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbStatus.TabIndex = 7;
            // 
            // cmbContents
            // 
            this.cmbContents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContents.FormattingEnabled = true;
            this.cmbContents.Items.AddRange(new object[] {
            "Charts",
            "Tag",
            "Track",
            "Artist",
            "Album",
            "User"});
            this.cmbContents.Location = new System.Drawing.Point(81, 63);
            this.cmbContents.Name = "cmbContents";
            this.cmbContents.Size = new System.Drawing.Size(82, 21);
            this.cmbContents.TabIndex = 4;
            this.cmbContents.SelectedIndexChanged += new System.EventHandler(this.ChangeContents);
            // 
            // pnlCharts
            // 
            this.pnlCharts.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlCharts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCharts.Controls.Add(this.radChartCountry);
            this.pnlCharts.Controls.Add(this.radChartWorldwide);
            this.pnlCharts.Controls.Add(this.cmbChartCountry);
            this.pnlCharts.Controls.Add(this.chkChartTopTags);
            this.pnlCharts.Controls.Add(this.chkChartTopArtists);
            this.pnlCharts.Controls.Add(this.chkChartTopTracks);
            this.pnlCharts.Controls.Add(this.Label6);
            this.pnlCharts.Controls.Add(this.nudChartResults);
            this.pnlCharts.Controls.Add(this.Label4);
            this.pnlCharts.Location = new System.Drawing.Point(11, 90);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Size = new System.Drawing.Size(266, 176);
            this.pnlCharts.TabIndex = 71;
            // 
            // radChartCountry
            // 
            this.radChartCountry.AutoSize = true;
            this.radChartCountry.Location = new System.Drawing.Point(87, 35);
            this.radChartCountry.Name = "radChartCountry";
            this.radChartCountry.Size = new System.Drawing.Size(76, 17);
            this.radChartCountry.TabIndex = 9;
            this.radChartCountry.Text = "By Country";
            this.radChartCountry.UseVisualStyleBackColor = true;
            this.radChartCountry.Click += new System.EventHandler(this.ChartEnableCountries);
            // 
            // radChartWorldwide
            // 
            this.radChartWorldwide.AutoSize = true;
            this.radChartWorldwide.Checked = true;
            this.radChartWorldwide.Location = new System.Drawing.Point(6, 35);
            this.radChartWorldwide.Name = "radChartWorldwide";
            this.radChartWorldwide.Size = new System.Drawing.Size(75, 17);
            this.radChartWorldwide.TabIndex = 8;
            this.radChartWorldwide.TabStop = true;
            this.radChartWorldwide.Text = "Worldwide";
            this.radChartWorldwide.UseVisualStyleBackColor = true;
            this.radChartWorldwide.Click += new System.EventHandler(this.ChartEnableCountries);
            // 
            // cmbChartCountry
            // 
            this.cmbChartCountry.Enabled = false;
            this.cmbChartCountry.FormattingEnabled = true;
            this.cmbChartCountry.Items.AddRange(new object[] {
            "United States",
            "Afghanistan",
            "Åland Islands",
            "Albania",
            "Algeria",
            "American Samoa",
            "Andorra",
            "Angola",
            "Anguilla",
            "Antarctica",
            "Antigua and Barbuda",
            "Argentina",
            "Armenia",
            "Aruba",
            "Australia",
            "Austria",
            "Azerbaijan",
            "Bahamas",
            "Bahrain",
            "Bangladesh",
            "Barbados",
            "Belarus",
            "Belgium",
            "Belize",
            "Benin",
            "Bermuda",
            "Bhutan",
            "Bolivia, Plurinational State of",
            "Bonaire, Sint Eustatius and Saba",
            "Bosnia and Herzegovina",
            "Botswana",
            "Bouvet Island",
            "Brazil",
            "British Indian Ocean Territory",
            "Brunei Darussalam",
            "Bulgaria",
            "Burkina Faso",
            "Burundi",
            "Cambodia",
            "Cameroon",
            "Canada",
            "Cape Verde",
            "Cayman Islands",
            "Central African Republic",
            "Chad",
            "Chile",
            "China",
            "Christmas Island",
            "Cocos (Keeling) Islands",
            "Colombia",
            "Comoros",
            "Congo",
            "Congo, The Democratic Republic of the",
            "Cook Islands",
            "Costa Rica",
            "Côte D\'Ivoire",
            "Croatia",
            "Cuba",
            "Curaçao",
            "Cyprus",
            "Czech Republic",
            "Denmark",
            "Djibouti",
            "Dominica",
            "Dominican Republic",
            "Ecuador",
            "Egypt",
            "El Salvador",
            "Equatorial Guinea",
            "Eritrea",
            "Estonia",
            "Ethiopia",
            "Falkland Islands (Malvinas)",
            "Faroe Islands",
            "Fiji",
            "Finland",
            "France",
            "French Guiana",
            "French Polynesia",
            "French Southern Territories",
            "Gabon",
            "Gambia",
            "Georgia",
            "Germany",
            "Ghana",
            "Gibraltar",
            "Greece",
            "Greenland",
            "Grenada",
            "Guadeloupe",
            "Guam",
            "Guatemala",
            "Guernsey",
            "Guinea",
            "Guinea-Bissau",
            "Guyana",
            "Haiti",
            "Heard Island and McDonald Islands",
            "Holy See (Vatican City State)",
            "Honduras",
            "Hong Kong",
            "Hungary",
            "Iceland",
            "India",
            "Indonesia",
            "Iran, Islamic Republic of",
            "Iraq",
            "Ireland",
            "Isle of Man",
            "Israel",
            "Italy",
            "Jamaica",
            "Japan",
            "Jersey",
            "Jordan",
            "Kazakhstan",
            "Kenya",
            "Kiribati",
            "Korea, Democratic People\'s Republic of",
            "Korea, Republic of",
            "Kuwait",
            "Kyrgyzstan",
            "Lao People\'s Democratic Republic",
            "Latvia",
            "Lebanon",
            "Lesotho",
            "Liberia",
            "Libya",
            "Liechtenstein",
            "Lithuania",
            "Luxembourg",
            "Macao",
            "Macedonia, The former Yugoslav Republic of",
            "Madagascar",
            "Malawi",
            "Malaysia",
            "Maldives",
            "Mali",
            "Malta",
            "Marshall Islands",
            "Martinique",
            "Mauritania",
            "Mauritius",
            "Mayotte",
            "Mexico",
            "Micronesia, Federated States of",
            "Moldova, Republic of",
            "Monaco",
            "Mongolia",
            "Montenegro",
            "Montserrat",
            "Morocco",
            "Mozambique",
            "Myanmar",
            "Namibia",
            "Nauru",
            "Nepal",
            "Netherlands",
            "New Caledonia",
            "New Zealand",
            "Nicaragua",
            "Niger",
            "Nigeria",
            "Niue",
            "Norfolk Island",
            "Northern Mariana Islands",
            "Norway",
            "Oman",
            "Pakistan",
            "Palau",
            "Palestine, State of",
            "Panama",
            "Papua New Guinea",
            "Paraguay",
            "Peru",
            "Philippines",
            "Pitcairn",
            "Poland",
            "Portugal",
            "Puerto Rico",
            "Qatar",
            "Reunion",
            "Romania",
            "Russian Federation",
            "Rwanda",
            "Saint Barthélemy",
            "Saint Helena, Ascension and Tristan Da Cunha",
            "Saint Kitts and Nevis",
            "Saint Lucia",
            "Saint Martin (French Part)",
            "Saint Pierre and Miquelon",
            "Saint Vincent and the Grenadines",
            "Samoa",
            "San Marino",
            "Sao Tome and Principe",
            "Saudi Arabia",
            "Senegal",
            "Serbia",
            "Seychelles",
            "Sierra Leone",
            "Singapore",
            "Sint Maarten (Dutch Part)",
            "Slovakia",
            "Slovenia",
            "Solomon Islands",
            "Somalia",
            "South Africa",
            "South Georgia and the South Sandwich Islands",
            "South Sudan",
            "Spain",
            "Sri Lanka",
            "Sudan",
            "Suriname",
            "Svalbard and Jan Mayen",
            "Swaziland",
            "Sweden",
            "Switzerland",
            "Syrian Arab Republic",
            "Taiwan, Province of China",
            "Tajikistan",
            "Tanzania, United Republic of",
            "Thailand",
            "Timor-Leste",
            "Togo",
            "Tokelau",
            "Tonga",
            "Trinidad and Tobago",
            "Tunisia",
            "Turkey",
            "Turkmenistan",
            "Turks and Caicos Islands",
            "Tuvalu",
            "Uganda",
            "Ukraine",
            "United Arab Emirates",
            "United Kingdom",
            "United States Minor Outlying Islands",
            "Uruguay",
            "Uzbekistan",
            "Vanuatu",
            "Venezuela",
            "Viet Nam",
            "Virgin Islands, British",
            "Virgin Islands, U.S.",
            "Wallis and Futuna",
            "Western Sahara",
            "Yemen",
            "Zambia",
            "Zimbabwe"});
            this.cmbChartCountry.Location = new System.Drawing.Point(87, 58);
            this.cmbChartCountry.Name = "cmbChartCountry";
            this.cmbChartCountry.Size = new System.Drawing.Size(138, 21);
            this.cmbChartCountry.TabIndex = 7;
            this.cmbChartCountry.Text = "United States";
            // 
            // chkChartTopTags
            // 
            this.chkChartTopTags.AutoSize = true;
            this.chkChartTopTags.Checked = true;
            this.chkChartTopTags.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChartTopTags.Location = new System.Drawing.Point(52, 135);
            this.chkChartTopTags.Name = "chkChartTopTags";
            this.chkChartTopTags.Size = new System.Drawing.Size(72, 17);
            this.chkChartTopTags.TabIndex = 5;
            this.chkChartTopTags.Text = "Top Tags";
            this.chkChartTopTags.UseVisualStyleBackColor = true;
            // 
            // chkChartTopArtists
            // 
            this.chkChartTopArtists.AutoSize = true;
            this.chkChartTopArtists.Checked = true;
            this.chkChartTopArtists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChartTopArtists.Location = new System.Drawing.Point(52, 112);
            this.chkChartTopArtists.Name = "chkChartTopArtists";
            this.chkChartTopArtists.Size = new System.Drawing.Size(76, 17);
            this.chkChartTopArtists.TabIndex = 4;
            this.chkChartTopArtists.Text = "Top Artists";
            this.chkChartTopArtists.UseVisualStyleBackColor = true;
            // 
            // chkChartTopTracks
            // 
            this.chkChartTopTracks.AutoSize = true;
            this.chkChartTopTracks.Checked = true;
            this.chkChartTopTracks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChartTopTracks.Location = new System.Drawing.Point(52, 89);
            this.chkChartTopTracks.Name = "chkChartTopTracks";
            this.chkChartTopTracks.Size = new System.Drawing.Size(81, 17);
            this.chkChartTopTracks.TabIndex = 3;
            this.chkChartTopTracks.Text = "Top Tracks";
            this.chkChartTopTracks.UseVisualStyleBackColor = true;
            // 
            // nudChartResults
            // 
            this.nudChartResults.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudChartResults.Location = new System.Drawing.Point(93, 5);
            this.nudChartResults.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudChartResults.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudChartResults.Name = "nudChartResults";
            this.nudChartResults.Size = new System.Drawing.Size(52, 20);
            this.nudChartResults.TabIndex = 1;
            this.nudChartResults.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(11, 351);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.FrmClose);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(202, 351);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "&Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.StartButton);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(121, 351);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "S&top";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.StopButton);
            // 
            // pnlAlbum
            // 
            this.pnlAlbum.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlAlbum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAlbum.Controls.Add(this.chkAlbumTracks);
            this.pnlAlbum.Controls.Add(this.chkAlbumInfo);
            this.pnlAlbum.Controls.Add(this.chkAlbumTags);
            this.pnlAlbum.Controls.Add(this.chkAlbumStats);
            this.pnlAlbum.Controls.Add(this.Label15);
            this.pnlAlbum.Controls.Add(this.txtAlbumArtist);
            this.pnlAlbum.Controls.Add(this.Label16);
            this.pnlAlbum.Controls.Add(this.txtAlbumAlbum);
            this.pnlAlbum.Controls.Add(this.Label17);
            this.pnlAlbum.Location = new System.Drawing.Point(11, 90);
            this.pnlAlbum.Name = "pnlAlbum";
            this.pnlAlbum.Size = new System.Drawing.Size(266, 176);
            this.pnlAlbum.TabIndex = 80;
            // 
            // chkAlbumTracks
            // 
            this.chkAlbumTracks.AutoSize = true;
            this.chkAlbumTracks.Checked = true;
            this.chkAlbumTracks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlbumTracks.Location = new System.Drawing.Point(52, 82);
            this.chkAlbumTracks.Name = "chkAlbumTracks";
            this.chkAlbumTracks.Size = new System.Drawing.Size(59, 17);
            this.chkAlbumTracks.TabIndex = 13;
            this.chkAlbumTracks.Text = "Tracks";
            this.chkAlbumTracks.UseVisualStyleBackColor = true;
            // 
            // chkAlbumInfo
            // 
            this.chkAlbumInfo.AutoSize = true;
            this.chkAlbumInfo.Checked = true;
            this.chkAlbumInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlbumInfo.Location = new System.Drawing.Point(52, 60);
            this.chkAlbumInfo.Name = "chkAlbumInfo";
            this.chkAlbumInfo.Size = new System.Drawing.Size(44, 17);
            this.chkAlbumInfo.TabIndex = 12;
            this.chkAlbumInfo.Text = "Info";
            this.chkAlbumInfo.UseVisualStyleBackColor = true;
            // 
            // chkAlbumTags
            // 
            this.chkAlbumTags.AutoSize = true;
            this.chkAlbumTags.Checked = true;
            this.chkAlbumTags.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlbumTags.Location = new System.Drawing.Point(52, 126);
            this.chkAlbumTags.Name = "chkAlbumTags";
            this.chkAlbumTags.Size = new System.Drawing.Size(50, 17);
            this.chkAlbumTags.TabIndex = 9;
            this.chkAlbumTags.Text = "Tags";
            this.chkAlbumTags.UseVisualStyleBackColor = true;
            // 
            // chkAlbumStats
            // 
            this.chkAlbumStats.AutoSize = true;
            this.chkAlbumStats.Checked = true;
            this.chkAlbumStats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlbumStats.Location = new System.Drawing.Point(52, 104);
            this.chkAlbumStats.Name = "chkAlbumStats";
            this.chkAlbumStats.Size = new System.Drawing.Size(50, 17);
            this.chkAlbumStats.TabIndex = 8;
            this.chkAlbumStats.Text = "Stats";
            this.chkAlbumStats.UseVisualStyleBackColor = true;
            // 
            // txtAlbumArtist
            // 
            this.txtAlbumArtist.Location = new System.Drawing.Point(33, 30);
            this.txtAlbumArtist.Name = "txtAlbumArtist";
            this.txtAlbumArtist.Size = new System.Drawing.Size(227, 20);
            this.txtAlbumArtist.TabIndex = 3;
            // 
            // txtAlbumAlbum
            // 
            this.txtAlbumAlbum.Location = new System.Drawing.Point(39, 4);
            this.txtAlbumAlbum.Name = "txtAlbumAlbum";
            this.txtAlbumAlbum.Size = new System.Drawing.Size(221, 20);
            this.txtAlbumAlbum.TabIndex = 1;
            // 
            // pnlUser
            // 
            this.pnlUser.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUser.Controls.Add(this.dtpUserTo);
            this.pnlUser.Controls.Add(this.Label14);
            this.pnlUser.Controls.Add(this.dtpUserFrom);
            this.pnlUser.Controls.Add(this.chkUserByDate);
            this.pnlUser.Controls.Add(this.nudUserNumber);
            this.pnlUser.Controls.Add(this.radUserNumber);
            this.pnlUser.Controls.Add(this.radUserEntire);
            this.pnlUser.Controls.Add(this.chkUserCharts);
            this.pnlUser.Controls.Add(this.btnUserUseCurrent);
            this.pnlUser.Controls.Add(this.chkUserHistory);
            this.pnlUser.Controls.Add(this.chkUserLoved);
            this.pnlUser.Controls.Add(this.chkUserFriends);
            this.pnlUser.Controls.Add(this.chkUserInfo);
            this.pnlUser.Controls.Add(this.Label13);
            this.pnlUser.Controls.Add(this.txtUserUser);
            this.pnlUser.Controls.Add(this.Label18);
            this.pnlUser.Location = new System.Drawing.Point(11, 90);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(266, 176);
            this.pnlUser.TabIndex = 80;
            // 
            // dtpUserTo
            // 
            this.dtpUserTo.Enabled = false;
            this.dtpUserTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpUserTo.Location = new System.Drawing.Point(177, 30);
            this.dtpUserTo.Name = "dtpUserTo";
            this.dtpUserTo.Size = new System.Drawing.Size(82, 20);
            this.dtpUserTo.TabIndex = 24;
            // 
            // dtpUserFrom
            // 
            this.dtpUserFrom.Enabled = false;
            this.dtpUserFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpUserFrom.Location = new System.Drawing.Point(76, 29);
            this.dtpUserFrom.Name = "dtpUserFrom";
            this.dtpUserFrom.Size = new System.Drawing.Size(82, 20);
            this.dtpUserFrom.TabIndex = 22;
            // 
            // chkUserByDate
            // 
            this.chkUserByDate.AutoSize = true;
            this.chkUserByDate.Location = new System.Drawing.Point(6, 32);
            this.chkUserByDate.Name = "chkUserByDate";
            this.chkUserByDate.Size = new System.Drawing.Size(64, 17);
            this.chkUserByDate.TabIndex = 21;
            this.chkUserByDate.Text = "By Date";
            this.chkUserByDate.UseVisualStyleBackColor = true;
            this.chkUserByDate.CheckedChanged += new System.EventHandler(this.UserEnableDate);
            // 
            // nudUserNumber
            // 
            this.nudUserNumber.Enabled = false;
            this.nudUserNumber.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudUserNumber.Location = new System.Drawing.Point(201, 137);
            this.nudUserNumber.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudUserNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudUserNumber.Name = "nudUserNumber";
            this.nudUserNumber.Size = new System.Drawing.Size(59, 20);
            this.nudUserNumber.TabIndex = 20;
            this.nudUserNumber.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // radUserNumber
            // 
            this.radUserNumber.AutoSize = true;
            this.radUserNumber.Enabled = false;
            this.radUserNumber.Location = new System.Drawing.Point(184, 140);
            this.radUserNumber.Name = "radUserNumber";
            this.radUserNumber.Size = new System.Drawing.Size(14, 13);
            this.radUserNumber.TabIndex = 19;
            this.radUserNumber.UseVisualStyleBackColor = true;
            // 
            // radUserEntire
            // 
            this.radUserEntire.AutoSize = true;
            this.radUserEntire.Checked = true;
            this.radUserEntire.Enabled = false;
            this.radUserEntire.Location = new System.Drawing.Point(134, 137);
            this.radUserEntire.Name = "radUserEntire";
            this.radUserEntire.Size = new System.Drawing.Size(52, 17);
            this.radUserEntire.TabIndex = 18;
            this.radUserEntire.TabStop = true;
            this.radUserEntire.Text = "Entire";
            this.radUserEntire.UseVisualStyleBackColor = true;
            // 
            // chkUserCharts
            // 
            this.chkUserCharts.AutoSize = true;
            this.chkUserCharts.Checked = true;
            this.chkUserCharts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUserCharts.Location = new System.Drawing.Point(52, 146);
            this.chkUserCharts.Name = "chkUserCharts";
            this.chkUserCharts.Size = new System.Drawing.Size(56, 17);
            this.chkUserCharts.TabIndex = 17;
            this.chkUserCharts.Text = "Charts";
            this.chkUserCharts.UseVisualStyleBackColor = true;
            this.chkUserCharts.CheckedChanged += new System.EventHandler(this.UserEnableAmount);
            // 
            // btnUserUseCurrent
            // 
            this.btnUserUseCurrent.Location = new System.Drawing.Point(185, 3);
            this.btnUserUseCurrent.Name = "btnUserUseCurrent";
            this.btnUserUseCurrent.Size = new System.Drawing.Size(75, 23);
            this.btnUserUseCurrent.TabIndex = 12;
            this.btnUserUseCurrent.Text = "Use Current";
            this.btnUserUseCurrent.UseVisualStyleBackColor = true;
            this.btnUserUseCurrent.Click += new System.EventHandler(this.UserUseCurrent);
            // 
            // chkUserHistory
            // 
            this.chkUserHistory.AutoSize = true;
            this.chkUserHistory.Checked = true;
            this.chkUserHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUserHistory.Location = new System.Drawing.Point(52, 124);
            this.chkUserHistory.Name = "chkUserHistory";
            this.chkUserHistory.Size = new System.Drawing.Size(81, 17);
            this.chkUserHistory.TabIndex = 11;
            this.chkUserHistory.Text = "Play History";
            this.chkUserHistory.UseVisualStyleBackColor = true;
            this.chkUserHistory.CheckedChanged += new System.EventHandler(this.UserEnableAmount);
            // 
            // chkUserLoved
            // 
            this.chkUserLoved.AutoSize = true;
            this.chkUserLoved.Checked = true;
            this.chkUserLoved.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUserLoved.Location = new System.Drawing.Point(52, 102);
            this.chkUserLoved.Name = "chkUserLoved";
            this.chkUserLoved.Size = new System.Drawing.Size(89, 17);
            this.chkUserLoved.TabIndex = 10;
            this.chkUserLoved.Text = "Loved Songs";
            this.chkUserLoved.UseVisualStyleBackColor = true;
            // 
            // chkUserFriends
            // 
            this.chkUserFriends.AutoSize = true;
            this.chkUserFriends.Checked = true;
            this.chkUserFriends.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUserFriends.Location = new System.Drawing.Point(52, 80);
            this.chkUserFriends.Name = "chkUserFriends";
            this.chkUserFriends.Size = new System.Drawing.Size(60, 17);
            this.chkUserFriends.TabIndex = 9;
            this.chkUserFriends.Text = "Friends";
            this.chkUserFriends.UseVisualStyleBackColor = true;
            // 
            // chkUserInfo
            // 
            this.chkUserInfo.AutoSize = true;
            this.chkUserInfo.Checked = true;
            this.chkUserInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUserInfo.Location = new System.Drawing.Point(52, 58);
            this.chkUserInfo.Name = "chkUserInfo";
            this.chkUserInfo.Size = new System.Drawing.Size(44, 17);
            this.chkUserInfo.TabIndex = 8;
            this.chkUserInfo.Text = "Info";
            this.chkUserInfo.UseVisualStyleBackColor = true;
            // 
            // txtUserUser
            // 
            this.txtUserUser.Location = new System.Drawing.Point(32, 4);
            this.txtUserUser.Name = "txtUserUser";
            this.txtUserUser.Size = new System.Drawing.Size(147, 20);
            this.txtUserUser.TabIndex = 1;
            // 
            // pnlTag
            // 
            this.pnlTag.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTag.Controls.Add(this.txtTagTag);
            this.pnlTag.Controls.Add(this.Label20);
            this.pnlTag.Controls.Add(this.chkTagTopAlbums);
            this.pnlTag.Controls.Add(this.chkTagTopArtists);
            this.pnlTag.Controls.Add(this.chkTagTopTracks);
            this.pnlTag.Controls.Add(this.chkTagInfo);
            this.pnlTag.Controls.Add(this.Label5);
            this.pnlTag.Controls.Add(this.nudTagResults);
            this.pnlTag.Controls.Add(this.Label7);
            this.pnlTag.Location = new System.Drawing.Point(11, 90);
            this.pnlTag.Name = "pnlTag";
            this.pnlTag.Size = new System.Drawing.Size(266, 176);
            this.pnlTag.TabIndex = 81;
            // 
            // txtTagTag
            // 
            this.txtTagTag.Location = new System.Drawing.Point(29, 4);
            this.txtTagTag.Name = "txtTagTag";
            this.txtTagTag.Size = new System.Drawing.Size(232, 20);
            this.txtTagTag.TabIndex = 9;
            // 
            // chkTagTopAlbums
            // 
            this.chkTagTopAlbums.AutoSize = true;
            this.chkTagTopAlbums.Checked = true;
            this.chkTagTopAlbums.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTagTopAlbums.Location = new System.Drawing.Point(52, 127);
            this.chkTagTopAlbums.Name = "chkTagTopAlbums";
            this.chkTagTopAlbums.Size = new System.Drawing.Size(82, 17);
            this.chkTagTopAlbums.TabIndex = 7;
            this.chkTagTopAlbums.Text = "Top Albums";
            this.chkTagTopAlbums.UseVisualStyleBackColor = true;
            // 
            // chkTagTopArtists
            // 
            this.chkTagTopArtists.AutoSize = true;
            this.chkTagTopArtists.Checked = true;
            this.chkTagTopArtists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTagTopArtists.Location = new System.Drawing.Point(52, 104);
            this.chkTagTopArtists.Name = "chkTagTopArtists";
            this.chkTagTopArtists.Size = new System.Drawing.Size(76, 17);
            this.chkTagTopArtists.TabIndex = 6;
            this.chkTagTopArtists.Text = "Top Artists";
            this.chkTagTopArtists.UseVisualStyleBackColor = true;
            // 
            // chkTagTopTracks
            // 
            this.chkTagTopTracks.AutoSize = true;
            this.chkTagTopTracks.Checked = true;
            this.chkTagTopTracks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTagTopTracks.Location = new System.Drawing.Point(52, 81);
            this.chkTagTopTracks.Name = "chkTagTopTracks";
            this.chkTagTopTracks.Size = new System.Drawing.Size(81, 17);
            this.chkTagTopTracks.TabIndex = 5;
            this.chkTagTopTracks.Text = "Top Tracks";
            this.chkTagTopTracks.UseVisualStyleBackColor = true;
            // 
            // chkTagInfo
            // 
            this.chkTagInfo.AutoSize = true;
            this.chkTagInfo.Checked = true;
            this.chkTagInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTagInfo.Location = new System.Drawing.Point(52, 58);
            this.chkTagInfo.Name = "chkTagInfo";
            this.chkTagInfo.Size = new System.Drawing.Size(44, 17);
            this.chkTagInfo.TabIndex = 3;
            this.chkTagInfo.Text = "Info";
            this.chkTagInfo.UseVisualStyleBackColor = true;
            // 
            // nudTagResults
            // 
            this.nudTagResults.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudTagResults.Location = new System.Drawing.Point(93, 30);
            this.nudTagResults.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudTagResults.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTagResults.Name = "nudTagResults";
            this.nudTagResults.Size = new System.Drawing.Size(52, 20);
            this.nudTagResults.TabIndex = 1;
            this.nudTagResults.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // pnlTrack
            // 
            this.pnlTrack.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlTrack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTrack.Controls.Add(this.chkTrackSimilar);
            this.pnlTrack.Controls.Add(this.chkTrackTags);
            this.pnlTrack.Controls.Add(this.chkTrackStats);
            this.pnlTrack.Controls.Add(this.chkTrackInfo);
            this.pnlTrack.Controls.Add(this.Label11);
            this.pnlTrack.Controls.Add(this.txtTrackArtist);
            this.pnlTrack.Controls.Add(this.Label8);
            this.pnlTrack.Controls.Add(this.txtTrackTitle);
            this.pnlTrack.Controls.Add(this.Label9);
            this.pnlTrack.Location = new System.Drawing.Point(11, 90);
            this.pnlTrack.Name = "pnlTrack";
            this.pnlTrack.Size = new System.Drawing.Size(266, 176);
            this.pnlTrack.TabIndex = 82;
            // 
            // chkTrackSimilar
            // 
            this.chkTrackSimilar.AutoSize = true;
            this.chkTrackSimilar.Checked = true;
            this.chkTrackSimilar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrackSimilar.Location = new System.Drawing.Point(52, 124);
            this.chkTrackSimilar.Name = "chkTrackSimilar";
            this.chkTrackSimilar.Size = new System.Drawing.Size(56, 17);
            this.chkTrackSimilar.TabIndex = 11;
            this.chkTrackSimilar.Text = "Similar";
            this.chkTrackSimilar.UseVisualStyleBackColor = true;
            // 
            // chkTrackTags
            // 
            this.chkTrackTags.AutoSize = true;
            this.chkTrackTags.Checked = true;
            this.chkTrackTags.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrackTags.Location = new System.Drawing.Point(52, 102);
            this.chkTrackTags.Name = "chkTrackTags";
            this.chkTrackTags.Size = new System.Drawing.Size(50, 17);
            this.chkTrackTags.TabIndex = 9;
            this.chkTrackTags.Text = "Tags";
            this.chkTrackTags.UseVisualStyleBackColor = true;
            // 
            // chkTrackStats
            // 
            this.chkTrackStats.AutoSize = true;
            this.chkTrackStats.Checked = true;
            this.chkTrackStats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrackStats.Location = new System.Drawing.Point(52, 80);
            this.chkTrackStats.Name = "chkTrackStats";
            this.chkTrackStats.Size = new System.Drawing.Size(50, 17);
            this.chkTrackStats.TabIndex = 8;
            this.chkTrackStats.Text = "Stats";
            this.chkTrackStats.UseVisualStyleBackColor = true;
            // 
            // chkTrackInfo
            // 
            this.chkTrackInfo.AutoSize = true;
            this.chkTrackInfo.Checked = true;
            this.chkTrackInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrackInfo.Location = new System.Drawing.Point(52, 59);
            this.chkTrackInfo.Name = "chkTrackInfo";
            this.chkTrackInfo.Size = new System.Drawing.Size(44, 17);
            this.chkTrackInfo.TabIndex = 7;
            this.chkTrackInfo.Text = "Info";
            this.chkTrackInfo.UseVisualStyleBackColor = true;
            // 
            // txtTrackArtist
            // 
            this.txtTrackArtist.Location = new System.Drawing.Point(33, 30);
            this.txtTrackArtist.Name = "txtTrackArtist";
            this.txtTrackArtist.Size = new System.Drawing.Size(227, 20);
            this.txtTrackArtist.TabIndex = 3;
            // 
            // txtTrackTitle
            // 
            this.txtTrackTitle.Location = new System.Drawing.Point(30, 4);
            this.txtTrackTitle.Name = "txtTrackTitle";
            this.txtTrackTitle.Size = new System.Drawing.Size(230, 20);
            this.txtTrackTitle.TabIndex = 1;
            // 
            // pnlArtist
            // 
            this.pnlArtist.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlArtist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlArtist.Controls.Add(this.chkArtistCharts);
            this.pnlArtist.Controls.Add(this.chkArtistSimilar);
            this.pnlArtist.Controls.Add(this.chkArtistTags);
            this.pnlArtist.Controls.Add(this.chkArtistStats);
            this.pnlArtist.Controls.Add(this.Label10);
            this.pnlArtist.Controls.Add(this.txtArtistArtist);
            this.pnlArtist.Controls.Add(this.Label12);
            this.pnlArtist.Location = new System.Drawing.Point(11, 90);
            this.pnlArtist.Name = "pnlArtist";
            this.pnlArtist.Size = new System.Drawing.Size(266, 176);
            this.pnlArtist.TabIndex = 82;
            // 
            // chkArtistCharts
            // 
            this.chkArtistCharts.AutoSize = true;
            this.chkArtistCharts.Checked = true;
            this.chkArtistCharts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkArtistCharts.Location = new System.Drawing.Point(52, 98);
            this.chkArtistCharts.Name = "chkArtistCharts";
            this.chkArtistCharts.Size = new System.Drawing.Size(56, 17);
            this.chkArtistCharts.TabIndex = 12;
            this.chkArtistCharts.Text = "Charts";
            this.chkArtistCharts.UseVisualStyleBackColor = true;
            // 
            // chkArtistSimilar
            // 
            this.chkArtistSimilar.AutoSize = true;
            this.chkArtistSimilar.Checked = true;
            this.chkArtistSimilar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkArtistSimilar.Location = new System.Drawing.Point(52, 76);
            this.chkArtistSimilar.Name = "chkArtistSimilar";
            this.chkArtistSimilar.Size = new System.Drawing.Size(56, 17);
            this.chkArtistSimilar.TabIndex = 11;
            this.chkArtistSimilar.Text = "Similar";
            this.chkArtistSimilar.UseVisualStyleBackColor = true;
            // 
            // chkArtistTags
            // 
            this.chkArtistTags.AutoSize = true;
            this.chkArtistTags.Checked = true;
            this.chkArtistTags.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkArtistTags.Location = new System.Drawing.Point(52, 54);
            this.chkArtistTags.Name = "chkArtistTags";
            this.chkArtistTags.Size = new System.Drawing.Size(50, 17);
            this.chkArtistTags.TabIndex = 9;
            this.chkArtistTags.Text = "Tags";
            this.chkArtistTags.UseVisualStyleBackColor = true;
            // 
            // chkArtistStats
            // 
            this.chkArtistStats.AutoSize = true;
            this.chkArtistStats.Checked = true;
            this.chkArtistStats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkArtistStats.Location = new System.Drawing.Point(52, 32);
            this.chkArtistStats.Name = "chkArtistStats";
            this.chkArtistStats.Size = new System.Drawing.Size(50, 17);
            this.chkArtistStats.TabIndex = 8;
            this.chkArtistStats.Text = "Stats";
            this.chkArtistStats.UseVisualStyleBackColor = true;
            // 
            // txtArtistArtist
            // 
            this.txtArtistArtist.Location = new System.Drawing.Point(33, 4);
            this.txtArtistArtist.Name = "txtArtistArtist";
            this.txtArtistArtist.Size = new System.Drawing.Size(227, 20);
            this.txtArtistArtist.TabIndex = 3;
            // 
            // sfdSave
            // 
            this.sfdSave.Filter = "CSV files|*.csv|All files|*.*";
            this.sfdSave.Title = "Save file...";
            // 
            // bgwChart
            // 
            this.bgwChart.WorkerSupportsCancellation = true;
            this.bgwChart.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ChartOp);
            this.bgwChart.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundStopOp);
            // 
            // bgwTag
            // 
            this.bgwTag.WorkerSupportsCancellation = true;
            this.bgwTag.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TagOp);
            this.bgwTag.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundStopOp);
            // 
            // bgwTrack
            // 
            this.bgwTrack.WorkerSupportsCancellation = true;
            this.bgwTrack.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TrackOp);
            this.bgwTrack.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundStopOp);
            // 
            // bgwArtist
            // 
            this.bgwArtist.WorkerSupportsCancellation = true;
            this.bgwArtist.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ArtistOp);
            this.bgwArtist.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundStopOp);
            // 
            // bgwAlbum
            // 
            this.bgwAlbum.WorkerSupportsCancellation = true;
            this.bgwAlbum.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AlbumOp);
            this.bgwAlbum.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundStopOp);
            // 
            // bgwUser
            // 
            this.bgwUser.WorkerSupportsCancellation = true;
            this.bgwUser.DoWork += new System.ComponentModel.DoWorkEventHandler(this.UserOp);
            this.bgwUser.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundStopOp);
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(161, 33);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(16, 13);
            this.Label14.TabIndex = 23;
            this.Label14.Text = "to";
            this.Label14.UseMnemonic = false;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 57);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(50, 13);
            this.Label13.TabIndex = 6;
            this.Label13.Text = "Columns:";
            this.Label13.UseMnemonic = false;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(3, 7);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(32, 13);
            this.Label18.TabIndex = 0;
            this.Label18.Text = "User:";
            this.Label18.UseMnemonic = false;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(3, 59);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(50, 13);
            this.Label15.TabIndex = 6;
            this.Label15.Text = "Columns:";
            this.Label15.UseMnemonic = false;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(3, 33);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(33, 13);
            this.Label16.TabIndex = 2;
            this.Label16.Text = "Artist:";
            this.Label16.UseMnemonic = false;
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(3, 7);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(39, 13);
            this.Label17.TabIndex = 0;
            this.Label17.Text = "Album:";
            this.Label17.UseMnemonic = false;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(3, 31);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(50, 13);
            this.Label10.TabIndex = 6;
            this.Label10.Text = "Columns:";
            this.Label10.UseMnemonic = false;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(3, 7);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(33, 13);
            this.Label12.TabIndex = 2;
            this.Label12.Text = "Artist:";
            this.Label12.UseMnemonic = false;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(3, 59);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(50, 13);
            this.Label11.TabIndex = 6;
            this.Label11.Text = "Columns:";
            this.Label11.UseMnemonic = false;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(3, 33);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(33, 13);
            this.Label8.TabIndex = 2;
            this.Label8.Text = "Artist:";
            this.Label8.UseMnemonic = false;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(3, 7);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(30, 13);
            this.Label9.TabIndex = 0;
            this.Label9.Text = "Title:";
            this.Label9.UseMnemonic = false;
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(3, 7);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(29, 13);
            this.Label20.TabIndex = 8;
            this.Label20.Text = "Tag:";
            this.Label20.UseMnemonic = false;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(3, 58);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(50, 13);
            this.Label5.TabIndex = 2;
            this.Label5.Text = "Columns:";
            this.Label5.UseMnemonic = false;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(3, 32);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(93, 13);
            this.Label7.TabIndex = 0;
            this.Label7.Text = "Results (1-10000):";
            this.Label7.UseMnemonic = false;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(3, 89);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(50, 13);
            this.Label6.TabIndex = 2;
            this.Label6.Text = "Columns:";
            this.Label6.UseMnemonic = false;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(3, 7);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(93, 13);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "Results (1-10000):";
            this.Label4.UseMnemonic = false;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(7, 276);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(52, 21);
            this.Label2.TabIndex = 5;
            this.Label2.Text = "Status";
            this.Label2.UseMnemonic = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(8, 61);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(72, 21);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Contents";
            this.Label1.UseMnemonic = false;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(8, 5);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(106, 21);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Save Location";
            this.Label3.UseMnemonic = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(51, 282);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(226, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStatus.UseMnemonic = false;
            // 
            // frmBackupTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 386);
            this.Controls.Add(this.pnlUser);
            this.Controls.Add(this.pnlAlbum);
            this.Controls.Add(this.pnlArtist);
            this.Controls.Add(this.pnlTrack);
            this.Controls.Add(this.pnlTag);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlCharts);
            this.Controls.Add(this.cmbContents);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtSave);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmBackupTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup Tool";
            this.Load += new System.EventHandler(this.FrmLoad);
            this.pnlCharts.ResumeLayout(false);
            this.pnlCharts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChartResults)).EndInit();
            this.pnlAlbum.ResumeLayout(false);
            this.pnlAlbum.PerformLayout();
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserNumber)).EndInit();
            this.pnlTag.ResumeLayout(false);
            this.pnlTag.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTagResults)).EndInit();
            this.pnlTrack.ResumeLayout(false);
            this.pnlTrack.PerformLayout();
            this.pnlArtist.ResumeLayout(false);
            this.pnlArtist.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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