<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBackupTool
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBackupTool))
        Me.txtSave = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pbStatus = New System.Windows.Forms.ProgressBar()
        Me.cmbContents = New System.Windows.Forms.ComboBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pnlCharts = New System.Windows.Forms.Panel()
        Me.radChartCountry = New System.Windows.Forms.RadioButton()
        Me.radChartWorldwide = New System.Windows.Forms.RadioButton()
        Me.cmbChartCountry = New System.Windows.Forms.ComboBox()
        Me.chkChartTopTags = New System.Windows.Forms.CheckBox()
        Me.chkChartTopArtists = New System.Windows.Forms.CheckBox()
        Me.chkChartTopTracks = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.nudChartResults = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.pnlAlbum = New System.Windows.Forms.Panel()
        Me.chkAlbumTracks = New System.Windows.Forms.CheckBox()
        Me.chkAlbumInfo = New System.Windows.Forms.CheckBox()
        Me.chkAlbumTags = New System.Windows.Forms.CheckBox()
        Me.chkAlbumStats = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtAlbumArtist = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtAlbumAlbum = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pnlUser = New System.Windows.Forms.Panel()
        Me.dtpUserTo = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpUserFrom = New System.Windows.Forms.DateTimePicker()
        Me.chkUserByDate = New System.Windows.Forms.CheckBox()
        Me.nudUserNumber = New System.Windows.Forms.NumericUpDown()
        Me.radUserNumber = New System.Windows.Forms.RadioButton()
        Me.radUserEntire = New System.Windows.Forms.RadioButton()
        Me.chkUserCharts = New System.Windows.Forms.CheckBox()
        Me.btnUserUseCurrent = New System.Windows.Forms.Button()
        Me.chkUserHistory = New System.Windows.Forms.CheckBox()
        Me.chkUserLoved = New System.Windows.Forms.CheckBox()
        Me.chkUserFriends = New System.Windows.Forms.CheckBox()
        Me.chkUserInfo = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtUserUser = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.pnlTag = New System.Windows.Forms.Panel()
        Me.txtTagTag = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.chkTagTopAlbums = New System.Windows.Forms.CheckBox()
        Me.chkTagTopArtists = New System.Windows.Forms.CheckBox()
        Me.chkTagTopTracks = New System.Windows.Forms.CheckBox()
        Me.chkTagInfo = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.nudTagResults = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlTrack = New System.Windows.Forms.Panel()
        Me.chkTrackSimilar = New System.Windows.Forms.CheckBox()
        Me.chkTrackTags = New System.Windows.Forms.CheckBox()
        Me.chkTrackStats = New System.Windows.Forms.CheckBox()
        Me.chkTrackInfo = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTrackArtist = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTrackTitle = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlArtist = New System.Windows.Forms.Panel()
        Me.chkArtistCharts = New System.Windows.Forms.CheckBox()
        Me.chkArtistSimilar = New System.Windows.Forms.CheckBox()
        Me.chkArtistTags = New System.Windows.Forms.CheckBox()
        Me.chkArtistStats = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtArtistArtist = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.sfdSave = New System.Windows.Forms.SaveFileDialog()
        Me.bgwChart = New System.ComponentModel.BackgroundWorker()
        Me.bgwTag = New System.ComponentModel.BackgroundWorker()
        Me.bgwTrack = New System.ComponentModel.BackgroundWorker()
        Me.bgwArtist = New System.ComponentModel.BackgroundWorker()
        Me.bgwAlbum = New System.ComponentModel.BackgroundWorker()
        Me.bgwUser = New System.ComponentModel.BackgroundWorker()
        Me.pnlCharts.SuspendLayout()
        CType(Me.nudChartResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAlbum.SuspendLayout()
        Me.pnlUser.SuspendLayout()
        CType(Me.nudUserNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTag.SuspendLayout()
        CType(Me.nudTagResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTrack.SuspendLayout()
        Me.pnlArtist.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSave
        '
        Me.txtSave.Location = New System.Drawing.Point(11, 30)
        Me.txtSave.Name = "txtSave"
        Me.txtSave.Size = New System.Drawing.Size(185, 20)
        Me.txtSave.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 21)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Save Location"
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(202, 28)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.Text = "&Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 21)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Contents"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 276)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 21)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Status"
        '
        'pbStatus
        '
        Me.pbStatus.Location = New System.Drawing.Point(11, 301)
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(266, 26)
        Me.pbStatus.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbStatus.TabIndex = 7
        '
        'cmbContents
        '
        Me.cmbContents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbContents.FormattingEnabled = True
        Me.cmbContents.Items.AddRange(New Object() {"Charts", "Tag", "Track", "Artist", "Album", "User"})
        Me.cmbContents.Location = New System.Drawing.Point(81, 63)
        Me.cmbContents.Name = "cmbContents"
        Me.cmbContents.Size = New System.Drawing.Size(82, 21)
        Me.cmbContents.TabIndex = 4
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(51, 282)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(226, 13)
        Me.lblStatus.TabIndex = 6
        Me.lblStatus.Text = "Ready"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlCharts
        '
        Me.pnlCharts.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pnlCharts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCharts.Controls.Add(Me.radChartCountry)
        Me.pnlCharts.Controls.Add(Me.radChartWorldwide)
        Me.pnlCharts.Controls.Add(Me.cmbChartCountry)
        Me.pnlCharts.Controls.Add(Me.chkChartTopTags)
        Me.pnlCharts.Controls.Add(Me.chkChartTopArtists)
        Me.pnlCharts.Controls.Add(Me.chkChartTopTracks)
        Me.pnlCharts.Controls.Add(Me.Label6)
        Me.pnlCharts.Controls.Add(Me.nudChartResults)
        Me.pnlCharts.Controls.Add(Me.Label4)
        Me.pnlCharts.Location = New System.Drawing.Point(11, 90)
        Me.pnlCharts.Name = "pnlCharts"
        Me.pnlCharts.Size = New System.Drawing.Size(266, 176)
        Me.pnlCharts.TabIndex = 71
        '
        'radChartCountry
        '
        Me.radChartCountry.AutoSize = True
        Me.radChartCountry.Location = New System.Drawing.Point(87, 35)
        Me.radChartCountry.Name = "radChartCountry"
        Me.radChartCountry.Size = New System.Drawing.Size(76, 17)
        Me.radChartCountry.TabIndex = 9
        Me.radChartCountry.Text = "By Country"
        Me.radChartCountry.UseVisualStyleBackColor = True
        '
        'radChartWorldwide
        '
        Me.radChartWorldwide.AutoSize = True
        Me.radChartWorldwide.Checked = True
        Me.radChartWorldwide.Location = New System.Drawing.Point(6, 35)
        Me.radChartWorldwide.Name = "radChartWorldwide"
        Me.radChartWorldwide.Size = New System.Drawing.Size(75, 17)
        Me.radChartWorldwide.TabIndex = 8
        Me.radChartWorldwide.TabStop = True
        Me.radChartWorldwide.Text = "Worldwide"
        Me.radChartWorldwide.UseVisualStyleBackColor = True
        '
        'cmbChartCountry
        '
        Me.cmbChartCountry.Enabled = False
        Me.cmbChartCountry.FormattingEnabled = True
        Me.cmbChartCountry.Items.AddRange(New Object() {"United States", "Afghanistan", "Åland Islands", "Albania", "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia, Plurinational State of", "Bonaire, Sint Eustatius and Saba", "Bosnia and Herzegovina", "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean Territory", "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands", "Central African Republic", "Chad", "Chile", "China", "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo", "Congo, The Democratic Republic of the", "Cook Islands", "Costa Rica", "Côte D'Ivoire", "Croatia", "Cuba", "Curaçao", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Falkland Islands (Malvinas)", "Faroe Islands", "Fiji", "Finland", "France", "French Guiana", "French Polynesia", "French Southern Territories", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard Island and McDonald Islands", "Holy See (Vatican City State)", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran, Islamic Republic of", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Jamaica", "Japan", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea, Democratic People's Republic of", "Korea, Republic of", "Kuwait", "Kyrgyzstan", "Lao People's Democratic Republic", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macao", "Macedonia, The former Yugoslav Republic of", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia, Federated States of", "Moldova, Republic of", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "Northern Mariana Islands", "Norway", "Oman", "Pakistan", "Palau", "Palestine, State of", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Pitcairn", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russian Federation", "Rwanda", "Saint Barthélemy", "Saint Helena, Ascension and Tristan Da Cunha", "Saint Kitts and Nevis", "Saint Lucia", "Saint Martin (French Part)", "Saint Pierre and Miquelon", "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Sint Maarten (Dutch Part)", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Georgia and the South Sandwich Islands", "South Sudan", "Spain", "Sri Lanka", "Sudan", "Suriname", "Svalbard and Jan Mayen", "Swaziland", "Sweden", "Switzerland", "Syrian Arab Republic", "Taiwan, Province of China", "Tajikistan", "Tanzania, United Republic of", "Thailand", "Timor-Leste", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States Minor Outlying Islands", "Uruguay", "Uzbekistan", "Vanuatu", "Venezuela", "Viet Nam", "Virgin Islands, British", "Virgin Islands, U.S.", "Wallis and Futuna", "Western Sahara", "Yemen", "Zambia", "Zimbabwe"})
        Me.cmbChartCountry.Location = New System.Drawing.Point(87, 58)
        Me.cmbChartCountry.Name = "cmbChartCountry"
        Me.cmbChartCountry.Size = New System.Drawing.Size(138, 21)
        Me.cmbChartCountry.TabIndex = 7
        Me.cmbChartCountry.Text = "United States"
        '
        'chkChartTopTags
        '
        Me.chkChartTopTags.AutoSize = True
        Me.chkChartTopTags.Checked = True
        Me.chkChartTopTags.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkChartTopTags.Location = New System.Drawing.Point(52, 135)
        Me.chkChartTopTags.Name = "chkChartTopTags"
        Me.chkChartTopTags.Size = New System.Drawing.Size(72, 17)
        Me.chkChartTopTags.TabIndex = 5
        Me.chkChartTopTags.Text = "Top Tags"
        Me.chkChartTopTags.UseVisualStyleBackColor = True
        '
        'chkChartTopArtists
        '
        Me.chkChartTopArtists.AutoSize = True
        Me.chkChartTopArtists.Checked = True
        Me.chkChartTopArtists.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkChartTopArtists.Location = New System.Drawing.Point(52, 112)
        Me.chkChartTopArtists.Name = "chkChartTopArtists"
        Me.chkChartTopArtists.Size = New System.Drawing.Size(76, 17)
        Me.chkChartTopArtists.TabIndex = 4
        Me.chkChartTopArtists.Text = "Top Artists"
        Me.chkChartTopArtists.UseVisualStyleBackColor = True
        '
        'chkChartTopTracks
        '
        Me.chkChartTopTracks.AutoSize = True
        Me.chkChartTopTracks.Checked = True
        Me.chkChartTopTracks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkChartTopTracks.Location = New System.Drawing.Point(52, 89)
        Me.chkChartTopTracks.Name = "chkChartTopTracks"
        Me.chkChartTopTracks.Size = New System.Drawing.Size(81, 17)
        Me.chkChartTopTracks.TabIndex = 3
        Me.chkChartTopTracks.Text = "Top Tracks"
        Me.chkChartTopTracks.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 89)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Columns:"
        '
        'nudChartResults
        '
        Me.nudChartResults.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        Me.nudChartResults.Location = New System.Drawing.Point(93, 5)
        Me.nudChartResults.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudChartResults.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudChartResults.Name = "nudChartResults"
        Me.nudChartResults.Size = New System.Drawing.Size(52, 20)
        Me.nudChartResults.TabIndex = 1
        Me.nudChartResults.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Results (1-10000):"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(11, 351)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(202, 351)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 10
        Me.btnStart.Text = "&Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(121, 351)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(75, 23)
        Me.btnStop.TabIndex = 9
        Me.btnStop.Text = "S&top"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'pnlAlbum
        '
        Me.pnlAlbum.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pnlAlbum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAlbum.Controls.Add(Me.chkAlbumTracks)
        Me.pnlAlbum.Controls.Add(Me.chkAlbumInfo)
        Me.pnlAlbum.Controls.Add(Me.chkAlbumTags)
        Me.pnlAlbum.Controls.Add(Me.chkAlbumStats)
        Me.pnlAlbum.Controls.Add(Me.Label15)
        Me.pnlAlbum.Controls.Add(Me.txtAlbumArtist)
        Me.pnlAlbum.Controls.Add(Me.Label16)
        Me.pnlAlbum.Controls.Add(Me.txtAlbumAlbum)
        Me.pnlAlbum.Controls.Add(Me.Label17)
        Me.pnlAlbum.Location = New System.Drawing.Point(11, 90)
        Me.pnlAlbum.Name = "pnlAlbum"
        Me.pnlAlbum.Size = New System.Drawing.Size(266, 176)
        Me.pnlAlbum.TabIndex = 80
        '
        'chkAlbumTracks
        '
        Me.chkAlbumTracks.AutoSize = True
        Me.chkAlbumTracks.Checked = True
        Me.chkAlbumTracks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAlbumTracks.Location = New System.Drawing.Point(52, 82)
        Me.chkAlbumTracks.Name = "chkAlbumTracks"
        Me.chkAlbumTracks.Size = New System.Drawing.Size(59, 17)
        Me.chkAlbumTracks.TabIndex = 13
        Me.chkAlbumTracks.Text = "Tracks"
        Me.chkAlbumTracks.UseVisualStyleBackColor = True
        '
        'chkAlbumInfo
        '
        Me.chkAlbumInfo.AutoSize = True
        Me.chkAlbumInfo.Checked = True
        Me.chkAlbumInfo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAlbumInfo.Location = New System.Drawing.Point(52, 60)
        Me.chkAlbumInfo.Name = "chkAlbumInfo"
        Me.chkAlbumInfo.Size = New System.Drawing.Size(44, 17)
        Me.chkAlbumInfo.TabIndex = 12
        Me.chkAlbumInfo.Text = "Info"
        Me.chkAlbumInfo.UseVisualStyleBackColor = True
        '
        'chkAlbumTags
        '
        Me.chkAlbumTags.AutoSize = True
        Me.chkAlbumTags.Checked = True
        Me.chkAlbumTags.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAlbumTags.Location = New System.Drawing.Point(52, 126)
        Me.chkAlbumTags.Name = "chkAlbumTags"
        Me.chkAlbumTags.Size = New System.Drawing.Size(50, 17)
        Me.chkAlbumTags.TabIndex = 9
        Me.chkAlbumTags.Text = "Tags"
        Me.chkAlbumTags.UseVisualStyleBackColor = True
        '
        'chkAlbumStats
        '
        Me.chkAlbumStats.AutoSize = True
        Me.chkAlbumStats.Checked = True
        Me.chkAlbumStats.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAlbumStats.Location = New System.Drawing.Point(52, 104)
        Me.chkAlbumStats.Name = "chkAlbumStats"
        Me.chkAlbumStats.Size = New System.Drawing.Size(50, 17)
        Me.chkAlbumStats.TabIndex = 8
        Me.chkAlbumStats.Text = "Stats"
        Me.chkAlbumStats.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(3, 59)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(50, 13)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "Columns:"
        '
        'txtAlbumArtist
        '
        Me.txtAlbumArtist.Location = New System.Drawing.Point(33, 30)
        Me.txtAlbumArtist.Name = "txtAlbumArtist"
        Me.txtAlbumArtist.Size = New System.Drawing.Size(227, 20)
        Me.txtAlbumArtist.TabIndex = 3
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(3, 33)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(33, 13)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "Artist:"
        '
        'txtAlbumAlbum
        '
        Me.txtAlbumAlbum.Location = New System.Drawing.Point(39, 4)
        Me.txtAlbumAlbum.Name = "txtAlbumAlbum"
        Me.txtAlbumAlbum.Size = New System.Drawing.Size(221, 20)
        Me.txtAlbumAlbum.TabIndex = 1
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(3, 7)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(39, 13)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Album:"
        '
        'pnlUser
        '
        Me.pnlUser.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pnlUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlUser.Controls.Add(Me.dtpUserTo)
        Me.pnlUser.Controls.Add(Me.Label14)
        Me.pnlUser.Controls.Add(Me.dtpUserFrom)
        Me.pnlUser.Controls.Add(Me.chkUserByDate)
        Me.pnlUser.Controls.Add(Me.nudUserNumber)
        Me.pnlUser.Controls.Add(Me.radUserNumber)
        Me.pnlUser.Controls.Add(Me.radUserEntire)
        Me.pnlUser.Controls.Add(Me.chkUserCharts)
        Me.pnlUser.Controls.Add(Me.btnUserUseCurrent)
        Me.pnlUser.Controls.Add(Me.chkUserHistory)
        Me.pnlUser.Controls.Add(Me.chkUserLoved)
        Me.pnlUser.Controls.Add(Me.chkUserFriends)
        Me.pnlUser.Controls.Add(Me.chkUserInfo)
        Me.pnlUser.Controls.Add(Me.Label13)
        Me.pnlUser.Controls.Add(Me.txtUserUser)
        Me.pnlUser.Controls.Add(Me.Label18)
        Me.pnlUser.Location = New System.Drawing.Point(11, 90)
        Me.pnlUser.Name = "pnlUser"
        Me.pnlUser.Size = New System.Drawing.Size(266, 176)
        Me.pnlUser.TabIndex = 80
        '
        'dtpUserTo
        '
        Me.dtpUserTo.Enabled = False
        Me.dtpUserTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpUserTo.Location = New System.Drawing.Point(177, 30)
        Me.dtpUserTo.Name = "dtpUserTo"
        Me.dtpUserTo.Size = New System.Drawing.Size(82, 20)
        Me.dtpUserTo.TabIndex = 24
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(161, 33)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(16, 13)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "to"
        '
        'dtpUserFrom
        '
        Me.dtpUserFrom.Enabled = False
        Me.dtpUserFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpUserFrom.Location = New System.Drawing.Point(76, 29)
        Me.dtpUserFrom.Name = "dtpUserFrom"
        Me.dtpUserFrom.Size = New System.Drawing.Size(82, 20)
        Me.dtpUserFrom.TabIndex = 22
        '
        'chkUserByDate
        '
        Me.chkUserByDate.AutoSize = True
        Me.chkUserByDate.Location = New System.Drawing.Point(6, 32)
        Me.chkUserByDate.Name = "chkUserByDate"
        Me.chkUserByDate.Size = New System.Drawing.Size(64, 17)
        Me.chkUserByDate.TabIndex = 21
        Me.chkUserByDate.Text = "By Date"
        Me.chkUserByDate.UseVisualStyleBackColor = True
        '
        'nudUserNumber
        '
        Me.nudUserNumber.Enabled = False
        Me.nudUserNumber.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        Me.nudUserNumber.Location = New System.Drawing.Point(201, 137)
        Me.nudUserNumber.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudUserNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudUserNumber.Name = "nudUserNumber"
        Me.nudUserNumber.Size = New System.Drawing.Size(59, 20)
        Me.nudUserNumber.TabIndex = 20
        Me.nudUserNumber.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'radUserNumber
        '
        Me.radUserNumber.AutoSize = True
        Me.radUserNumber.Enabled = False
        Me.radUserNumber.Location = New System.Drawing.Point(184, 140)
        Me.radUserNumber.Name = "radUserNumber"
        Me.radUserNumber.Size = New System.Drawing.Size(14, 13)
        Me.radUserNumber.TabIndex = 19
        Me.radUserNumber.UseVisualStyleBackColor = True
        '
        'radUserEntire
        '
        Me.radUserEntire.AutoSize = True
        Me.radUserEntire.Checked = True
        Me.radUserEntire.Enabled = False
        Me.radUserEntire.Location = New System.Drawing.Point(134, 137)
        Me.radUserEntire.Name = "radUserEntire"
        Me.radUserEntire.Size = New System.Drawing.Size(52, 17)
        Me.radUserEntire.TabIndex = 18
        Me.radUserEntire.TabStop = True
        Me.radUserEntire.Text = "Entire"
        Me.radUserEntire.UseVisualStyleBackColor = True
        '
        'chkUserCharts
        '
        Me.chkUserCharts.AutoSize = True
        Me.chkUserCharts.Checked = True
        Me.chkUserCharts.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUserCharts.Location = New System.Drawing.Point(52, 146)
        Me.chkUserCharts.Name = "chkUserCharts"
        Me.chkUserCharts.Size = New System.Drawing.Size(56, 17)
        Me.chkUserCharts.TabIndex = 17
        Me.chkUserCharts.Text = "Charts"
        Me.chkUserCharts.UseVisualStyleBackColor = True
        '
        'btnUserUseCurrent
        '
        Me.btnUserUseCurrent.Location = New System.Drawing.Point(185, 3)
        Me.btnUserUseCurrent.Name = "btnUserUseCurrent"
        Me.btnUserUseCurrent.Size = New System.Drawing.Size(75, 23)
        Me.btnUserUseCurrent.TabIndex = 12
        Me.btnUserUseCurrent.Text = "Use Current"
        Me.btnUserUseCurrent.UseVisualStyleBackColor = True
        '
        'chkUserHistory
        '
        Me.chkUserHistory.AutoSize = True
        Me.chkUserHistory.Checked = True
        Me.chkUserHistory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUserHistory.Location = New System.Drawing.Point(52, 124)
        Me.chkUserHistory.Name = "chkUserHistory"
        Me.chkUserHistory.Size = New System.Drawing.Size(81, 17)
        Me.chkUserHistory.TabIndex = 11
        Me.chkUserHistory.Text = "Play History"
        Me.chkUserHistory.UseVisualStyleBackColor = True
        '
        'chkUserLoved
        '
        Me.chkUserLoved.AutoSize = True
        Me.chkUserLoved.Checked = True
        Me.chkUserLoved.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUserLoved.Location = New System.Drawing.Point(52, 102)
        Me.chkUserLoved.Name = "chkUserLoved"
        Me.chkUserLoved.Size = New System.Drawing.Size(89, 17)
        Me.chkUserLoved.TabIndex = 10
        Me.chkUserLoved.Text = "Loved Songs"
        Me.chkUserLoved.UseVisualStyleBackColor = True
        '
        'chkUserFriends
        '
        Me.chkUserFriends.AutoSize = True
        Me.chkUserFriends.Checked = True
        Me.chkUserFriends.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUserFriends.Location = New System.Drawing.Point(52, 80)
        Me.chkUserFriends.Name = "chkUserFriends"
        Me.chkUserFriends.Size = New System.Drawing.Size(60, 17)
        Me.chkUserFriends.TabIndex = 9
        Me.chkUserFriends.Text = "Friends"
        Me.chkUserFriends.UseVisualStyleBackColor = True
        '
        'chkUserInfo
        '
        Me.chkUserInfo.AutoSize = True
        Me.chkUserInfo.Checked = True
        Me.chkUserInfo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUserInfo.Location = New System.Drawing.Point(52, 58)
        Me.chkUserInfo.Name = "chkUserInfo"
        Me.chkUserInfo.Size = New System.Drawing.Size(44, 17)
        Me.chkUserInfo.TabIndex = 8
        Me.chkUserInfo.Text = "Info"
        Me.chkUserInfo.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(3, 57)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(50, 13)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "Columns:"
        '
        'txtUserUser
        '
        Me.txtUserUser.Location = New System.Drawing.Point(32, 4)
        Me.txtUserUser.Name = "txtUserUser"
        Me.txtUserUser.Size = New System.Drawing.Size(147, 20)
        Me.txtUserUser.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(3, 7)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(32, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "User:"
        '
        'pnlTag
        '
        Me.pnlTag.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pnlTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTag.Controls.Add(Me.txtTagTag)
        Me.pnlTag.Controls.Add(Me.Label20)
        Me.pnlTag.Controls.Add(Me.chkTagTopAlbums)
        Me.pnlTag.Controls.Add(Me.chkTagTopArtists)
        Me.pnlTag.Controls.Add(Me.chkTagTopTracks)
        Me.pnlTag.Controls.Add(Me.chkTagInfo)
        Me.pnlTag.Controls.Add(Me.Label5)
        Me.pnlTag.Controls.Add(Me.nudTagResults)
        Me.pnlTag.Controls.Add(Me.Label7)
        Me.pnlTag.Location = New System.Drawing.Point(11, 90)
        Me.pnlTag.Name = "pnlTag"
        Me.pnlTag.Size = New System.Drawing.Size(266, 176)
        Me.pnlTag.TabIndex = 81
        '
        'txtTagTag
        '
        Me.txtTagTag.Location = New System.Drawing.Point(29, 4)
        Me.txtTagTag.Name = "txtTagTag"
        Me.txtTagTag.Size = New System.Drawing.Size(232, 20)
        Me.txtTagTag.TabIndex = 9
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(3, 7)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(29, 13)
        Me.Label20.TabIndex = 8
        Me.Label20.Text = "Tag:"
        '
        'chkTagTopAlbums
        '
        Me.chkTagTopAlbums.AutoSize = True
        Me.chkTagTopAlbums.Checked = True
        Me.chkTagTopAlbums.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTagTopAlbums.Location = New System.Drawing.Point(52, 127)
        Me.chkTagTopAlbums.Name = "chkTagTopAlbums"
        Me.chkTagTopAlbums.Size = New System.Drawing.Size(82, 17)
        Me.chkTagTopAlbums.TabIndex = 7
        Me.chkTagTopAlbums.Text = "Top Albums"
        Me.chkTagTopAlbums.UseVisualStyleBackColor = True
        '
        'chkTagTopArtists
        '
        Me.chkTagTopArtists.AutoSize = True
        Me.chkTagTopArtists.Checked = True
        Me.chkTagTopArtists.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTagTopArtists.Location = New System.Drawing.Point(52, 104)
        Me.chkTagTopArtists.Name = "chkTagTopArtists"
        Me.chkTagTopArtists.Size = New System.Drawing.Size(76, 17)
        Me.chkTagTopArtists.TabIndex = 6
        Me.chkTagTopArtists.Text = "Top Artists"
        Me.chkTagTopArtists.UseVisualStyleBackColor = True
        '
        'chkTagTopTracks
        '
        Me.chkTagTopTracks.AutoSize = True
        Me.chkTagTopTracks.Checked = True
        Me.chkTagTopTracks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTagTopTracks.Location = New System.Drawing.Point(52, 81)
        Me.chkTagTopTracks.Name = "chkTagTopTracks"
        Me.chkTagTopTracks.Size = New System.Drawing.Size(81, 17)
        Me.chkTagTopTracks.TabIndex = 5
        Me.chkTagTopTracks.Text = "Top Tracks"
        Me.chkTagTopTracks.UseVisualStyleBackColor = True
        '
        'chkTagInfo
        '
        Me.chkTagInfo.AutoSize = True
        Me.chkTagInfo.Checked = True
        Me.chkTagInfo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTagInfo.Location = New System.Drawing.Point(52, 58)
        Me.chkTagInfo.Name = "chkTagInfo"
        Me.chkTagInfo.Size = New System.Drawing.Size(44, 17)
        Me.chkTagInfo.TabIndex = 3
        Me.chkTagInfo.Text = "Info"
        Me.chkTagInfo.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Columns:"
        '
        'nudTagResults
        '
        Me.nudTagResults.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        Me.nudTagResults.Location = New System.Drawing.Point(93, 30)
        Me.nudTagResults.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudTagResults.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudTagResults.Name = "nudTagResults"
        Me.nudTagResults.Size = New System.Drawing.Size(52, 20)
        Me.nudTagResults.TabIndex = 1
        Me.nudTagResults.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Results (1-10000):"
        '
        'pnlTrack
        '
        Me.pnlTrack.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pnlTrack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTrack.Controls.Add(Me.chkTrackSimilar)
        Me.pnlTrack.Controls.Add(Me.chkTrackTags)
        Me.pnlTrack.Controls.Add(Me.chkTrackStats)
        Me.pnlTrack.Controls.Add(Me.chkTrackInfo)
        Me.pnlTrack.Controls.Add(Me.Label11)
        Me.pnlTrack.Controls.Add(Me.txtTrackArtist)
        Me.pnlTrack.Controls.Add(Me.Label8)
        Me.pnlTrack.Controls.Add(Me.txtTrackTitle)
        Me.pnlTrack.Controls.Add(Me.Label9)
        Me.pnlTrack.Location = New System.Drawing.Point(11, 90)
        Me.pnlTrack.Name = "pnlTrack"
        Me.pnlTrack.Size = New System.Drawing.Size(266, 176)
        Me.pnlTrack.TabIndex = 82
        '
        'chkTrackSimilar
        '
        Me.chkTrackSimilar.AutoSize = True
        Me.chkTrackSimilar.Checked = True
        Me.chkTrackSimilar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTrackSimilar.Location = New System.Drawing.Point(52, 124)
        Me.chkTrackSimilar.Name = "chkTrackSimilar"
        Me.chkTrackSimilar.Size = New System.Drawing.Size(56, 17)
        Me.chkTrackSimilar.TabIndex = 11
        Me.chkTrackSimilar.Text = "Similar"
        Me.chkTrackSimilar.UseVisualStyleBackColor = True
        '
        'chkTrackTags
        '
        Me.chkTrackTags.AutoSize = True
        Me.chkTrackTags.Checked = True
        Me.chkTrackTags.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTrackTags.Location = New System.Drawing.Point(52, 102)
        Me.chkTrackTags.Name = "chkTrackTags"
        Me.chkTrackTags.Size = New System.Drawing.Size(50, 17)
        Me.chkTrackTags.TabIndex = 9
        Me.chkTrackTags.Text = "Tags"
        Me.chkTrackTags.UseVisualStyleBackColor = True
        '
        'chkTrackStats
        '
        Me.chkTrackStats.AutoSize = True
        Me.chkTrackStats.Checked = True
        Me.chkTrackStats.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTrackStats.Location = New System.Drawing.Point(52, 80)
        Me.chkTrackStats.Name = "chkTrackStats"
        Me.chkTrackStats.Size = New System.Drawing.Size(50, 17)
        Me.chkTrackStats.TabIndex = 8
        Me.chkTrackStats.Text = "Stats"
        Me.chkTrackStats.UseVisualStyleBackColor = True
        '
        'chkTrackInfo
        '
        Me.chkTrackInfo.AutoSize = True
        Me.chkTrackInfo.Checked = True
        Me.chkTrackInfo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTrackInfo.Location = New System.Drawing.Point(52, 59)
        Me.chkTrackInfo.Name = "chkTrackInfo"
        Me.chkTrackInfo.Size = New System.Drawing.Size(44, 17)
        Me.chkTrackInfo.TabIndex = 7
        Me.chkTrackInfo.Text = "Info"
        Me.chkTrackInfo.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 59)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(50, 13)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "Columns:"
        '
        'txtTrackArtist
        '
        Me.txtTrackArtist.Location = New System.Drawing.Point(33, 30)
        Me.txtTrackArtist.Name = "txtTrackArtist"
        Me.txtTrackArtist.Size = New System.Drawing.Size(227, 20)
        Me.txtTrackArtist.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 33)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Artist:"
        '
        'txtTrackTitle
        '
        Me.txtTrackTitle.Location = New System.Drawing.Point(30, 4)
        Me.txtTrackTitle.Name = "txtTrackTitle"
        Me.txtTrackTitle.Size = New System.Drawing.Size(230, 20)
        Me.txtTrackTitle.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 7)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(30, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Title:"
        '
        'pnlArtist
        '
        Me.pnlArtist.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pnlArtist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlArtist.Controls.Add(Me.chkArtistCharts)
        Me.pnlArtist.Controls.Add(Me.chkArtistSimilar)
        Me.pnlArtist.Controls.Add(Me.chkArtistTags)
        Me.pnlArtist.Controls.Add(Me.chkArtistStats)
        Me.pnlArtist.Controls.Add(Me.Label10)
        Me.pnlArtist.Controls.Add(Me.txtArtistArtist)
        Me.pnlArtist.Controls.Add(Me.Label12)
        Me.pnlArtist.Location = New System.Drawing.Point(11, 90)
        Me.pnlArtist.Name = "pnlArtist"
        Me.pnlArtist.Size = New System.Drawing.Size(266, 176)
        Me.pnlArtist.TabIndex = 82
        '
        'chkArtistCharts
        '
        Me.chkArtistCharts.AutoSize = True
        Me.chkArtistCharts.Checked = True
        Me.chkArtistCharts.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkArtistCharts.Location = New System.Drawing.Point(52, 98)
        Me.chkArtistCharts.Name = "chkArtistCharts"
        Me.chkArtistCharts.Size = New System.Drawing.Size(56, 17)
        Me.chkArtistCharts.TabIndex = 12
        Me.chkArtistCharts.Text = "Charts"
        Me.chkArtistCharts.UseVisualStyleBackColor = True
        '
        'chkArtistSimilar
        '
        Me.chkArtistSimilar.AutoSize = True
        Me.chkArtistSimilar.Checked = True
        Me.chkArtistSimilar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkArtistSimilar.Location = New System.Drawing.Point(52, 76)
        Me.chkArtistSimilar.Name = "chkArtistSimilar"
        Me.chkArtistSimilar.Size = New System.Drawing.Size(56, 17)
        Me.chkArtistSimilar.TabIndex = 11
        Me.chkArtistSimilar.Text = "Similar"
        Me.chkArtistSimilar.UseVisualStyleBackColor = True
        '
        'chkArtistTags
        '
        Me.chkArtistTags.AutoSize = True
        Me.chkArtistTags.Checked = True
        Me.chkArtistTags.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkArtistTags.Location = New System.Drawing.Point(52, 54)
        Me.chkArtistTags.Name = "chkArtistTags"
        Me.chkArtistTags.Size = New System.Drawing.Size(50, 17)
        Me.chkArtistTags.TabIndex = 9
        Me.chkArtistTags.Text = "Tags"
        Me.chkArtistTags.UseVisualStyleBackColor = True
        '
        'chkArtistStats
        '
        Me.chkArtistStats.AutoSize = True
        Me.chkArtistStats.Checked = True
        Me.chkArtistStats.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkArtistStats.Location = New System.Drawing.Point(52, 32)
        Me.chkArtistStats.Name = "chkArtistStats"
        Me.chkArtistStats.Size = New System.Drawing.Size(50, 17)
        Me.chkArtistStats.TabIndex = 8
        Me.chkArtistStats.Text = "Stats"
        Me.chkArtistStats.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 31)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(50, 13)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Columns:"
        '
        'txtArtistArtist
        '
        Me.txtArtistArtist.Location = New System.Drawing.Point(33, 4)
        Me.txtArtistArtist.Name = "txtArtistArtist"
        Me.txtArtistArtist.Size = New System.Drawing.Size(227, 20)
        Me.txtArtistArtist.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(3, 7)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(33, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Artist:"
        '
        'sfdSave
        '
        Me.sfdSave.Filter = "CSV files|*.csv|All files|*.*"
        Me.sfdSave.Title = "Save file..."
        '
        'bgwChart
        '
        Me.bgwChart.WorkerSupportsCancellation = True
        '
        'bgwTag
        '
        Me.bgwTag.WorkerSupportsCancellation = True
        '
        'bgwTrack
        '
        Me.bgwTrack.WorkerSupportsCancellation = True
        '
        'bgwArtist
        '
        Me.bgwArtist.WorkerSupportsCancellation = True
        '
        'bgwAlbum
        '
        Me.bgwAlbum.WorkerSupportsCancellation = True
        '
        'bgwUser
        '
        Me.bgwUser.WorkerSupportsCancellation = True
        '
        'frmBackupTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(288, 386)
        Me.Controls.Add(Me.pnlUser)
        Me.Controls.Add(Me.pnlAlbum)
        Me.Controls.Add(Me.pnlArtist)
        Me.Controls.Add(Me.pnlTrack)
        Me.Controls.Add(Me.pnlTag)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.pnlCharts)
        Me.Controls.Add(Me.cmbContents)
        Me.Controls.Add(Me.pbStatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.txtSave)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblStatus)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmBackupTool"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Backup Tool"
        Me.pnlCharts.ResumeLayout(False)
        Me.pnlCharts.PerformLayout()
        CType(Me.nudChartResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAlbum.ResumeLayout(False)
        Me.pnlAlbum.PerformLayout()
        Me.pnlUser.ResumeLayout(False)
        Me.pnlUser.PerformLayout()
        CType(Me.nudUserNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTag.ResumeLayout(False)
        Me.pnlTag.PerformLayout()
        CType(Me.nudTagResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTrack.ResumeLayout(False)
        Me.pnlTrack.PerformLayout()
        Me.pnlArtist.ResumeLayout(False)
        Me.pnlArtist.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtSave As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnBrowse As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents pbStatus As ProgressBar
    Friend WithEvents cmbContents As ComboBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents pnlCharts As Panel
    Friend WithEvents nudChartResults As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents cmbChartCountry As ComboBox
    Friend WithEvents chkChartTopTags As CheckBox
    Friend WithEvents chkChartTopArtists As CheckBox
    Friend WithEvents chkChartTopTracks As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents radChartCountry As RadioButton
    Friend WithEvents radChartWorldwide As RadioButton
    Friend WithEvents btnClose As Button
    Friend WithEvents btnStart As Button
    Friend WithEvents btnStop As Button
    Friend WithEvents pnlAlbum As Panel
    Friend WithEvents chkAlbumTags As CheckBox
    Friend WithEvents chkAlbumStats As CheckBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txtAlbumArtist As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtAlbumAlbum As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents pnlUser As Panel
    Friend WithEvents nudUserNumber As NumericUpDown
    Friend WithEvents radUserNumber As RadioButton
    Friend WithEvents radUserEntire As RadioButton
    Friend WithEvents chkUserCharts As CheckBox
    Friend WithEvents btnUserUseCurrent As Button
    Friend WithEvents chkUserHistory As CheckBox
    Friend WithEvents chkUserLoved As CheckBox
    Friend WithEvents chkUserFriends As CheckBox
    Friend WithEvents chkUserInfo As CheckBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtUserUser As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents pnlTag As Panel
    Friend WithEvents pnlTrack As Panel
    Friend WithEvents chkTrackSimilar As CheckBox
    Friend WithEvents chkTrackTags As CheckBox
    Friend WithEvents chkTrackStats As CheckBox
    Friend WithEvents chkTrackInfo As CheckBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtTrackArtist As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtTrackTitle As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents chkTagTopAlbums As CheckBox
    Friend WithEvents chkTagTopArtists As CheckBox
    Friend WithEvents chkTagTopTracks As CheckBox
    Friend WithEvents chkTagInfo As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents nudTagResults As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents pnlArtist As Panel
    Friend WithEvents chkArtistCharts As CheckBox
    Friend WithEvents chkArtistSimilar As CheckBox
    Friend WithEvents chkArtistTags As CheckBox
    Friend WithEvents chkArtistStats As CheckBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtArtistArtist As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtTagTag As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents dtpUserTo As DateTimePicker
    Friend WithEvents Label14 As Label
    Friend WithEvents dtpUserFrom As DateTimePicker
    Friend WithEvents chkUserByDate As CheckBox
    Friend WithEvents sfdSave As SaveFileDialog
    Friend WithEvents bgwChart As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwTag As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwTrack As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwArtist As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwAlbum As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwUser As System.ComponentModel.BackgroundWorker
    Friend WithEvents chkAlbumInfo As CheckBox
    Friend WithEvents chkAlbumTracks As CheckBox
End Class
