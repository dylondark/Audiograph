Module CMSLists
    Public ChartTrackLabel(,) As Label = {{frmMain.lblTopTrackTitle1, frmMain.lblTopTrackArtist1, frmMain.lblTopTrackAlbum1, frmMain.lblTopTracksListeners1},
                                      {frmMain.lblTopTrackTitle2, frmMain.lblTopTrackArtist2, frmMain.lblTopTrackAlbum2, frmMain.lblTopTracksListeners2},
                                      {frmMain.lblTopTrackTitle3, frmMain.lblTopTrackArtist3, frmMain.lblTopTrackAlbum3, frmMain.lblTopTracksListeners3},
                                      {frmMain.lblTopTrackTitle4, frmMain.lblTopTrackArtist4, frmMain.lblTopTrackAlbum4, frmMain.lblTopTracksListeners4},
                                      {frmMain.lblTopTrackTitle5, frmMain.lblTopTrackArtist5, frmMain.lblTopTrackAlbum5, frmMain.lblTopTracksListeners5},
                                      {frmMain.lblTopTrackTitle6, frmMain.lblTopTrackArtist6, frmMain.lblTopTrackAlbum6, frmMain.lblTopTracksListeners6},
                                      {frmMain.lblTopTrackTitle7, frmMain.lblTopTrackArtist7, frmMain.lblTopTrackAlbum7, frmMain.lblTopTracksListeners7},
                                      {frmMain.lblTopTrackTitle8, frmMain.lblTopTrackArtist8, frmMain.lblTopTrackAlbum8, frmMain.lblTopTracksListeners8},
                                      {frmMain.lblTopTrackTitle9, frmMain.lblTopTrackArtist9, frmMain.lblTopTrackAlbum9, frmMain.lblTopTracksListeners9},
                                      {frmMain.lblTopTrackTitle10, frmMain.lblTopTrackArtist10, frmMain.lblTopTrackAlbum10, frmMain.lblTopTracksListeners10},
                                      {frmMain.lblTopTrackTitle11, frmMain.lblTopTrackArtist11, frmMain.lblTopTrackAlbum11, frmMain.lblTopTracksListeners11},
                                      {frmMain.lblTopTrackTitle12, frmMain.lblTopTrackArtist12, frmMain.lblTopTrackAlbum12, frmMain.lblTopTracksListeners12},
                                      {frmMain.lblTopTrackTitle13, frmMain.lblTopTrackArtist13, frmMain.lblTopTrackAlbum13, frmMain.lblTopTracksListeners13},
                                      {frmMain.lblTopTrackTitle14, frmMain.lblTopTrackArtist14, frmMain.lblTopTrackAlbum14, frmMain.lblTopTracksListeners14},
                                      {frmMain.lblTopTrackTitle15, frmMain.lblTopTrackArtist15, frmMain.lblTopTrackAlbum15, frmMain.lblTopTracksListeners15},
                                      {frmMain.lblTopTrackTitle16, frmMain.lblTopTrackArtist16, frmMain.lblTopTrackAlbum16, frmMain.lblTopTracksListeners16},
                                      {frmMain.lblTopTrackTitle17, frmMain.lblTopTrackArtist17, frmMain.lblTopTrackAlbum17, frmMain.lblTopTracksListeners17},
                                      {frmMain.lblTopTrackTitle18, frmMain.lblTopTrackArtist18, frmMain.lblTopTrackAlbum18, frmMain.lblTopTracksListeners18},
                                      {frmMain.lblTopTrackTitle19, frmMain.lblTopTrackArtist19, frmMain.lblTopTrackAlbum19, frmMain.lblTopTracksListeners19},
                                      {frmMain.lblTopTrackTitle20, frmMain.lblTopTrackArtist20, frmMain.lblTopTrackAlbum20, frmMain.lblTopTracksListeners20}}

    Public ChartArtistLabel(,) As Label = {{frmMain.lblTopArtist1, frmMain.lblTopArtistListeners1, frmMain.lblTopArtistPlaycount1},
                                          {frmMain.lblTopArtist2, frmMain.lblTopArtistListeners2, frmMain.lblTopArtistPlaycount2},
                                          {frmMain.lblTopArtist3, frmMain.lblTopArtistListeners3, frmMain.lblTopArtistPlaycount3},
                                          {frmMain.lblTopArtist4, frmMain.lblTopArtistListeners4, frmMain.lblTopArtistPlaycount4},
                                          {frmMain.lblTopArtist5, frmMain.lblTopArtistListeners5, frmMain.lblTopArtistPlaycount5},
                                          {frmMain.lblTopArtist6, frmMain.lblTopArtistListeners6, frmMain.lblTopArtistPlaycount6},
                                          {frmMain.lblTopArtist7, frmMain.lblTopArtistListeners7, frmMain.lblTopArtistPlaycount7},
                                          {frmMain.lblTopArtist8, frmMain.lblTopArtistListeners8, frmMain.lblTopArtistPlaycount8},
                                          {frmMain.lblTopArtist9, frmMain.lblTopArtistListeners9, frmMain.lblTopArtistPlaycount9},
                                          {frmMain.lblTopArtist10, frmMain.lblTopArtistListeners10, frmMain.lblTopArtistPlaycount10},
                                          {frmMain.lblTopArtist11, frmMain.lblTopArtistListeners11, frmMain.lblTopArtistPlaycount11},
                                          {frmMain.lblTopArtist12, frmMain.lblTopArtistListeners12, frmMain.lblTopArtistPlaycount12},
                                          {frmMain.lblTopArtist13, frmMain.lblTopArtistListeners13, frmMain.lblTopArtistPlaycount13},
                                          {frmMain.lblTopArtist14, frmMain.lblTopArtistListeners14, frmMain.lblTopArtistPlaycount14},
                                          {frmMain.lblTopArtist15, frmMain.lblTopArtistListeners15, frmMain.lblTopArtistPlaycount15},
                                          {frmMain.lblTopArtist16, frmMain.lblTopArtistListeners16, frmMain.lblTopArtistPlaycount16},
                                          {frmMain.lblTopArtist17, frmMain.lblTopArtistListeners17, frmMain.lblTopArtistPlaycount17},
                                          {frmMain.lblTopArtist18, frmMain.lblTopArtistListeners18, frmMain.lblTopArtistPlaycount18},
                                          {frmMain.lblTopArtist19, frmMain.lblTopArtistListeners19, frmMain.lblTopArtistPlaycount19},
                                          {frmMain.lblTopArtist20, frmMain.lblTopArtistListeners20, frmMain.lblTopArtistPlaycount20}}

    Public Function GetChartRowIndex(rowObject As Label) As Byte
        ' determine track vs artist
        If rowObject.Name.Contains("lblTopTrack") = True Then
            ' track
            For row As Byte = 0 To 19
                For column As Byte = 0 To 3
                    If rowObject.Name = ChartTrackLabel(row, column).Name Then
                        Return row
                    End If
                Next
            Next
        Else
            ' artist
            For row As Byte = 0 To 19
                For column As Byte = 0 To 2
                    If rowObject.Name = ChartArtistLabel(row, column).Name Then
                        Return row
                    End If
                Next
            Next
        End If
        Return 255
    End Function

    Public UserRecentLabel(,) As Label = {{frmMain.lblUserRecentTitle1, frmMain.lblUserRecentArtist1, frmMain.lblUserRecentAlbum1},
                                    {frmMain.lblUserRecentTitle2, frmMain.lblUserRecentArtist2, frmMain.lblUserRecentAlbum2},
                                    {frmMain.lblUserRecentTitle3, frmMain.lblUserRecentArtist3, frmMain.lblUserRecentAlbum3},
                                    {frmMain.lblUserRecentTitle4, frmMain.lblUserRecentArtist4, frmMain.lblUserRecentAlbum4},
                                    {frmMain.lblUserRecentTitle5, frmMain.lblUserRecentArtist5, frmMain.lblUserRecentAlbum5},
                                    {frmMain.lblUserRecentTitle6, frmMain.lblUserRecentArtist6, frmMain.lblUserRecentAlbum6},
                                    {frmMain.lblUserRecentTitle7, frmMain.lblUserRecentArtist7, frmMain.lblUserRecentAlbum7},
                                    {frmMain.lblUserRecentTitle8, frmMain.lblUserRecentArtist8, frmMain.lblUserRecentAlbum8},
                                    {frmMain.lblUserRecentTitle9, frmMain.lblUserRecentArtist9, frmMain.lblUserRecentAlbum9},
                                    {frmMain.lblUserRecentTitle10, frmMain.lblUserRecentArtist10, frmMain.lblUserRecentAlbum10},
                                    {frmMain.lblUserRecentTitle11, frmMain.lblUserRecentArtist11, frmMain.lblUserRecentAlbum11},
                                    {frmMain.lblUserRecentTitle12, frmMain.lblUserRecentArtist12, frmMain.lblUserRecentAlbum12},
                                    {frmMain.lblUserRecentTitle13, frmMain.lblUserRecentArtist13, frmMain.lblUserRecentAlbum13},
                                    {frmMain.lblUserRecentTitle14, frmMain.lblUserRecentArtist14, frmMain.lblUserRecentAlbum14},
                                    {frmMain.lblUserRecentTitle15, frmMain.lblUserRecentArtist15, frmMain.lblUserRecentAlbum15},
                                    {frmMain.lblUserRecentTitle16, frmMain.lblUserRecentArtist16, frmMain.lblUserRecentAlbum16},
                                    {frmMain.lblUserRecentTitle17, frmMain.lblUserRecentArtist17, frmMain.lblUserRecentAlbum17},
                                    {frmMain.lblUserRecentTitle18, frmMain.lblUserRecentArtist18, frmMain.lblUserRecentAlbum18},
                                    {frmMain.lblUserRecentTitle19, frmMain.lblUserRecentArtist19, frmMain.lblUserRecentAlbum19},
                                    {frmMain.lblUserRecentTitle20, frmMain.lblUserRecentArtist20, frmMain.lblUserRecentAlbum20}}

    Public UserLRecentLabel(,) As Label = {{frmMain.lblUserLRecentTitle1, frmMain.lblUserLRecentArtist1, frmMain.lblUserLRecentAlbum1},
                                    {frmMain.lblUserLRecentTitle2, frmMain.lblUserLRecentArtist2, frmMain.lblUserLRecentAlbum2},
                                    {frmMain.lblUserLRecentTitle3, frmMain.lblUserLRecentArtist3, frmMain.lblUserLRecentAlbum3},
                                    {frmMain.lblUserLRecentTitle4, frmMain.lblUserLRecentArtist4, frmMain.lblUserLRecentAlbum4},
                                    {frmMain.lblUserLRecentTitle5, frmMain.lblUserLRecentArtist5, frmMain.lblUserLRecentAlbum5},
                                    {frmMain.lblUserLRecentTitle6, frmMain.lblUserLRecentArtist6, frmMain.lblUserLRecentAlbum6},
                                    {frmMain.lblUserLRecentTitle7, frmMain.lblUserLRecentArtist7, frmMain.lblUserLRecentAlbum7},
                                    {frmMain.lblUserLRecentTitle8, frmMain.lblUserLRecentArtist8, frmMain.lblUserLRecentAlbum8},
                                    {frmMain.lblUserLRecentTitle9, frmMain.lblUserLRecentArtist9, frmMain.lblUserLRecentAlbum9},
                                    {frmMain.lblUserLRecentTitle10, frmMain.lblUserLRecentArtist10, frmMain.lblUserLRecentAlbum10},
                                    {frmMain.lblUserLRecentTitle11, frmMain.lblUserLRecentArtist11, frmMain.lblUserLRecentAlbum11},
                                    {frmMain.lblUserLRecentTitle12, frmMain.lblUserLRecentArtist12, frmMain.lblUserLRecentAlbum12},
                                    {frmMain.lblUserLRecentTitle13, frmMain.lblUserLRecentArtist13, frmMain.lblUserLRecentAlbum13},
                                    {frmMain.lblUserLRecentTitle14, frmMain.lblUserLRecentArtist14, frmMain.lblUserLRecentAlbum14},
                                    {frmMain.lblUserLRecentTitle15, frmMain.lblUserLRecentArtist15, frmMain.lblUserLRecentAlbum15},
                                    {frmMain.lblUserLRecentTitle16, frmMain.lblUserLRecentArtist16, frmMain.lblUserLRecentAlbum16},
                                    {frmMain.lblUserLRecentTitle17, frmMain.lblUserLRecentArtist17, frmMain.lblUserLRecentAlbum17},
                                    {frmMain.lblUserLRecentTitle18, frmMain.lblUserLRecentArtist18, frmMain.lblUserLRecentAlbum18},
                                    {frmMain.lblUserLRecentTitle19, frmMain.lblUserLRecentArtist19, frmMain.lblUserLRecentAlbum19},
                                    {frmMain.lblUserLRecentTitle20, frmMain.lblUserLRecentArtist20, frmMain.lblUserLRecentAlbum20}}

    Public Function GetUserRecentRowIndex(rowObject As Label) As Byte
        ' determine user vs userl
        If rowObject.Name.Contains("UserL") = False Then
            ' user
            For row As Byte = 0 To 19
                For column As Byte = 0 To 2
                    If rowObject.Name = UserRecentLabel(row, column).Name Then
                        Return row
                    End If
                Next
            Next
        Else
            ' userlookup
            For row As Byte = 0 To 19
                For column As Byte = 0 To 2
                    If rowObject.Name = UserLRecentLabel(row, column).Name Then
                        Return row
                    End If
                Next
            Next
        End If
        Return 255
    End Function

    Public UserTopTracksLabel(,) As Label = {{frmMain.lblUserTopTrackTitle1, frmMain.lblUserTopTrackArtist1, frmMain.lblUserTopTrackAlbum1, frmMain.lblUserTopTrackPlaycount1},
                                      {frmMain.lblUserTopTrackTitle2, frmMain.lblUserTopTrackArtist2, frmMain.lblUserTopTrackAlbum2, frmMain.lblUserTopTrackPlaycount2},
                                      {frmMain.lblUserTopTrackTitle3, frmMain.lblUserTopTrackArtist3, frmMain.lblUserTopTrackAlbum3, frmMain.lblUserTopTrackPlaycount3},
                                      {frmMain.lblUserTopTrackTitle4, frmMain.lblUserTopTrackArtist4, frmMain.lblUserTopTrackAlbum4, frmMain.lblUserTopTrackPlaycount4},
                                      {frmMain.lblUserTopTrackTitle5, frmMain.lblUserTopTrackArtist5, frmMain.lblUserTopTrackAlbum5, frmMain.lblUserTopTrackPlaycount5},
                                      {frmMain.lblUserTopTrackTitle6, frmMain.lblUserTopTrackArtist6, frmMain.lblUserTopTrackAlbum6, frmMain.lblUserTopTrackPlaycount6},
                                      {frmMain.lblUserTopTrackTitle7, frmMain.lblUserTopTrackArtist7, frmMain.lblUserTopTrackAlbum7, frmMain.lblUserTopTrackPlaycount7},
                                      {frmMain.lblUserTopTrackTitle8, frmMain.lblUserTopTrackArtist8, frmMain.lblUserTopTrackAlbum8, frmMain.lblUserTopTrackPlaycount8},
                                      {frmMain.lblUserTopTrackTitle9, frmMain.lblUserTopTrackArtist9, frmMain.lblUserTopTrackAlbum9, frmMain.lblUserTopTrackPlaycount9},
                                      {frmMain.lblUserTopTrackTitle10, frmMain.lblUserTopTrackArtist10, frmMain.lblUserTopTrackAlbum10, frmMain.lblUserTopTrackPlaycount10},
                                      {frmMain.lblUserTopTrackTitle11, frmMain.lblUserTopTrackArtist11, frmMain.lblUserTopTrackAlbum11, frmMain.lblUserTopTrackPlaycount11},
                                      {frmMain.lblUserTopTrackTitle12, frmMain.lblUserTopTrackArtist12, frmMain.lblUserTopTrackAlbum12, frmMain.lblUserTopTrackPlaycount12},
                                      {frmMain.lblUserTopTrackTitle13, frmMain.lblUserTopTrackArtist13, frmMain.lblUserTopTrackAlbum13, frmMain.lblUserTopTrackPlaycount13},
                                      {frmMain.lblUserTopTrackTitle14, frmMain.lblUserTopTrackArtist14, frmMain.lblUserTopTrackAlbum14, frmMain.lblUserTopTrackPlaycount14},
                                      {frmMain.lblUserTopTrackTitle15, frmMain.lblUserTopTrackArtist15, frmMain.lblUserTopTrackAlbum15, frmMain.lblUserTopTrackPlaycount15},
                                      {frmMain.lblUserTopTrackTitle16, frmMain.lblUserTopTrackArtist16, frmMain.lblUserTopTrackAlbum16, frmMain.lblUserTopTrackPlaycount16},
                                      {frmMain.lblUserTopTrackTitle17, frmMain.lblUserTopTrackArtist17, frmMain.lblUserTopTrackAlbum17, frmMain.lblUserTopTrackPlaycount17},
                                      {frmMain.lblUserTopTrackTitle18, frmMain.lblUserTopTrackArtist18, frmMain.lblUserTopTrackAlbum18, frmMain.lblUserTopTrackPlaycount18},
                                      {frmMain.lblUserTopTrackTitle19, frmMain.lblUserTopTrackArtist19, frmMain.lblUserTopTrackAlbum19, frmMain.lblUserTopTrackPlaycount19},
                                      {frmMain.lblUserTopTrackTitle20, frmMain.lblUserTopTrackArtist20, frmMain.lblUserTopTrackAlbum20, frmMain.lblUserTopTrackPlaycount20}}

    Public UserLTopTracksLabel(,) As Label = {{frmMain.lblUserLTopTrackTitle1, frmMain.lblUserLTopTrackArtist1, frmMain.lblUserLTopTrackAlbum1, frmMain.lblUserLTopTrackPlaycount1},
                                      {frmMain.lblUserLTopTrackTitle2, frmMain.lblUserLTopTrackArtist2, frmMain.lblUserLTopTrackAlbum2, frmMain.lblUserLTopTrackPlaycount2},
                                      {frmMain.lblUserLTopTrackTitle3, frmMain.lblUserLTopTrackArtist3, frmMain.lblUserLTopTrackAlbum3, frmMain.lblUserLTopTrackPlaycount3},
                                      {frmMain.lblUserLTopTrackTitle4, frmMain.lblUserLTopTrackArtist4, frmMain.lblUserLTopTrackAlbum4, frmMain.lblUserLTopTrackPlaycount4},
                                      {frmMain.lblUserLTopTrackTitle5, frmMain.lblUserLTopTrackArtist5, frmMain.lblUserLTopTrackAlbum5, frmMain.lblUserLTopTrackPlaycount5},
                                      {frmMain.lblUserLTopTrackTitle6, frmMain.lblUserLTopTrackArtist6, frmMain.lblUserLTopTrackAlbum6, frmMain.lblUserLTopTrackPlaycount6},
                                      {frmMain.lblUserLTopTrackTitle7, frmMain.lblUserLTopTrackArtist7, frmMain.lblUserLTopTrackAlbum7, frmMain.lblUserLTopTrackPlaycount7},
                                      {frmMain.lblUserLTopTrackTitle8, frmMain.lblUserLTopTrackArtist8, frmMain.lblUserLTopTrackAlbum8, frmMain.lblUserLTopTrackPlaycount8},
                                      {frmMain.lblUserLTopTrackTitle9, frmMain.lblUserLTopTrackArtist9, frmMain.lblUserLTopTrackAlbum9, frmMain.lblUserLTopTrackPlaycount9},
                                      {frmMain.lblUserLTopTrackTitle10, frmMain.lblUserLTopTrackArtist10, frmMain.lblUserLTopTrackAlbum10, frmMain.lblUserLTopTrackPlaycount10},
                                      {frmMain.lblUserLTopTrackTitle11, frmMain.lblUserLTopTrackArtist11, frmMain.lblUserLTopTrackAlbum11, frmMain.lblUserLTopTrackPlaycount11},
                                      {frmMain.lblUserLTopTrackTitle12, frmMain.lblUserLTopTrackArtist12, frmMain.lblUserLTopTrackAlbum12, frmMain.lblUserLTopTrackPlaycount12},
                                      {frmMain.lblUserLTopTrackTitle13, frmMain.lblUserLTopTrackArtist13, frmMain.lblUserLTopTrackAlbum13, frmMain.lblUserLTopTrackPlaycount13},
                                      {frmMain.lblUserLTopTrackTitle14, frmMain.lblUserLTopTrackArtist14, frmMain.lblUserLTopTrackAlbum14, frmMain.lblUserLTopTrackPlaycount14},
                                      {frmMain.lblUserLTopTrackTitle15, frmMain.lblUserLTopTrackArtist15, frmMain.lblUserLTopTrackAlbum15, frmMain.lblUserLTopTrackPlaycount15},
                                      {frmMain.lblUserLTopTrackTitle16, frmMain.lblUserLTopTrackArtist16, frmMain.lblUserLTopTrackAlbum16, frmMain.lblUserLTopTrackPlaycount16},
                                      {frmMain.lblUserLTopTrackTitle17, frmMain.lblUserLTopTrackArtist17, frmMain.lblUserLTopTrackAlbum17, frmMain.lblUserLTopTrackPlaycount17},
                                      {frmMain.lblUserLTopTrackTitle18, frmMain.lblUserLTopTrackArtist18, frmMain.lblUserLTopTrackAlbum18, frmMain.lblUserLTopTrackPlaycount18},
                                      {frmMain.lblUserLTopTrackTitle19, frmMain.lblUserLTopTrackArtist19, frmMain.lblUserLTopTrackAlbum19, frmMain.lblUserLTopTrackPlaycount19},
                                      {frmMain.lblUserLTopTrackTitle20, frmMain.lblUserLTopTrackArtist20, frmMain.lblUserLTopTrackAlbum20, frmMain.lblUserLTopTrackPlaycount20}}

    Public Function GetUserTopTrackRowIndex(rowObject As Label) As Byte
        ' determine user vs userl
        If rowObject.Name.Contains("UserL") = False Then
            ' user
            For row As Byte = 0 To 19
                For column As Byte = 0 To 3
                    If rowObject.Name = UserTopTracksLabel(row, column).Name Then
                        Return row
                    End If
                Next
            Next
        Else
            ' userlookup
            For row As Byte = 0 To 19
                For column As Byte = 0 To 3
                    If rowObject.Name = UserLTopTracksLabel(row, column).Name Then
                        Return row
                    End If
                Next
            Next
        End If
        Return 255
    End Function

    Public UserTopArtistsLabel(,) As Label = {{frmMain.lblUserTopArtist1, frmMain.lblUserTopAlbumPlaycount1},
                                      {frmMain.lblUserTopArtist2, frmMain.lblUserTopAlbumPlaycount2},
                                      {frmMain.lblUserTopArtist3, frmMain.lblUserTopAlbumPlaycount3},
                                      {frmMain.lblUserTopArtist4, frmMain.lblUserTopAlbumPlaycount4},
                                      {frmMain.lblUserTopArtist5, frmMain.lblUserTopAlbumPlaycount5},
                                      {frmMain.lblUserTopArtist6, frmMain.lblUserTopAlbumPlaycount6},
                                      {frmMain.lblUserTopArtist7, frmMain.lblUserTopAlbumPlaycount7},
                                      {frmMain.lblUserTopArtist8, frmMain.lblUserTopAlbumPlaycount8},
                                      {frmMain.lblUserTopArtist9, frmMain.lblUserTopAlbumPlaycount9},
                                      {frmMain.lblUserTopArtist10, frmMain.lblUserTopAlbumPlaycount10},
                                      {frmMain.lblUserTopArtist11, frmMain.lblUserTopAlbumPlaycount11},
                                      {frmMain.lblUserTopArtist12, frmMain.lblUserTopAlbumPlaycount12},
                                      {frmMain.lblUserTopArtist13, frmMain.lblUserTopAlbumPlaycount13},
                                      {frmMain.lblUserTopArtist14, frmMain.lblUserTopAlbumPlaycount14},
                                      {frmMain.lblUserTopArtist15, frmMain.lblUserTopAlbumPlaycount15},
                                      {frmMain.lblUserTopArtist16, frmMain.lblUserTopAlbumPlaycount16},
                                      {frmMain.lblUserTopArtist17, frmMain.lblUserTopAlbumPlaycount17},
                                      {frmMain.lblUserTopArtist18, frmMain.lblUserTopAlbumPlaycount18},
                                      {frmMain.lblUserTopArtist19, frmMain.lblUserTopAlbumPlaycount19},
                                      {frmMain.lblUserTopArtist20, frmMain.lblUserTopAlbumPlaycount20}}

    Public UserLTopArtistsLabel(,) As Label = {{frmMain.lblUserLTopArtist1, frmMain.lblUserLTopAlbumPlaycount1},
                                      {frmMain.lblUserLTopArtist2, frmMain.lblUserLTopAlbumPlaycount2},
                                      {frmMain.lblUserLTopArtist3, frmMain.lblUserLTopAlbumPlaycount3},
                                      {frmMain.lblUserLTopArtist4, frmMain.lblUserLTopAlbumPlaycount4},
                                      {frmMain.lblUserLTopArtist5, frmMain.lblUserLTopAlbumPlaycount5},
                                      {frmMain.lblUserLTopArtist6, frmMain.lblUserLTopAlbumPlaycount6},
                                      {frmMain.lblUserLTopArtist7, frmMain.lblUserLTopAlbumPlaycount7},
                                      {frmMain.lblUserLTopArtist8, frmMain.lblUserLTopAlbumPlaycount8},
                                      {frmMain.lblUserLTopArtist9, frmMain.lblUserLTopAlbumPlaycount9},
                                      {frmMain.lblUserLTopArtist10, frmMain.lblUserLTopAlbumPlaycount10},
                                      {frmMain.lblUserLTopArtist11, frmMain.lblUserLTopAlbumPlaycount11},
                                      {frmMain.lblUserLTopArtist12, frmMain.lblUserLTopAlbumPlaycount12},
                                      {frmMain.lblUserLTopArtist13, frmMain.lblUserLTopAlbumPlaycount13},
                                      {frmMain.lblUserLTopArtist14, frmMain.lblUserLTopAlbumPlaycount14},
                                      {frmMain.lblUserLTopArtist15, frmMain.lblUserLTopAlbumPlaycount15},
                                      {frmMain.lblUserLTopArtist16, frmMain.lblUserLTopAlbumPlaycount16},
                                      {frmMain.lblUserLTopArtist17, frmMain.lblUserLTopAlbumPlaycount17},
                                      {frmMain.lblUserLTopArtist18, frmMain.lblUserLTopAlbumPlaycount18},
                                      {frmMain.lblUserLTopArtist19, frmMain.lblUserLTopAlbumPlaycount19},
                                      {frmMain.lblUserLTopArtist20, frmMain.lblUserLTopAlbumPlaycount20}}

    Public Function GetUserTopArtistRowIndex(rowObject As Label) As Byte
        ' determine user vs userl
        If rowObject.Name.Contains("UserL") = False Then
            ' user
            For row As Byte = 0 To 19
                For column As Byte = 0 To 1
                    If rowObject.Name = UserTopArtistsLabel(row, column).Name Then
                        Return row
                    End If
                Next
            Next
        Else
            ' userlookup
            For row As Byte = 0 To 19
                For column As Byte = 0 To 1
                    If rowObject.Name = UserLTopArtistsLabel(row, column).Name Then
                        Return row
                    End If
                Next
            Next
        End If
        Return 255
    End Function

    Public UserTopAlbumsLabel(,) As Label = {{frmMain.lblUserTopAlbum1, frmMain.lblUserTopAlbumArtist1, frmMain.lblUserTopAlbumPlaycount1},
                                      {frmMain.lblUserTopAlbum2, frmMain.lblUserTopAlbumArtist2, frmMain.lblUserTopAlbumPlaycount2},
                                      {frmMain.lblUserTopAlbum3, frmMain.lblUserTopAlbumArtist3, frmMain.lblUserTopAlbumPlaycount3},
                                      {frmMain.lblUserTopAlbum4, frmMain.lblUserTopAlbumArtist4, frmMain.lblUserTopAlbumPlaycount4},
                                      {frmMain.lblUserTopAlbum5, frmMain.lblUserTopAlbumArtist5, frmMain.lblUserTopAlbumPlaycount5},
                                      {frmMain.lblUserTopAlbum6, frmMain.lblUserTopAlbumArtist6, frmMain.lblUserTopAlbumPlaycount6},
                                      {frmMain.lblUserTopAlbum7, frmMain.lblUserTopAlbumArtist7, frmMain.lblUserTopAlbumPlaycount7},
                                      {frmMain.lblUserTopAlbum8, frmMain.lblUserTopAlbumArtist8, frmMain.lblUserTopAlbumPlaycount8},
                                      {frmMain.lblUserTopAlbum9, frmMain.lblUserTopAlbumArtist9, frmMain.lblUserTopAlbumPlaycount9},
                                      {frmMain.lblUserTopAlbum10, frmMain.lblUserTopAlbumArtist10, frmMain.lblUserTopAlbumPlaycount10},
                                      {frmMain.lblUserTopAlbum11, frmMain.lblUserTopAlbumArtist11, frmMain.lblUserTopAlbumPlaycount11},
                                      {frmMain.lblUserTopAlbum12, frmMain.lblUserTopAlbumArtist12, frmMain.lblUserTopAlbumPlaycount12},
                                      {frmMain.lblUserTopAlbum13, frmMain.lblUserTopAlbumArtist13, frmMain.lblUserTopAlbumPlaycount13},
                                      {frmMain.lblUserTopAlbum14, frmMain.lblUserTopAlbumArtist14, frmMain.lblUserTopAlbumPlaycount14},
                                      {frmMain.lblUserTopAlbum15, frmMain.lblUserTopAlbumArtist15, frmMain.lblUserTopAlbumPlaycount15},
                                      {frmMain.lblUserTopAlbum16, frmMain.lblUserTopAlbumArtist16, frmMain.lblUserTopAlbumPlaycount16},
                                      {frmMain.lblUserTopAlbum17, frmMain.lblUserTopAlbumArtist17, frmMain.lblUserTopAlbumPlaycount17},
                                      {frmMain.lblUserTopAlbum18, frmMain.lblUserTopAlbumArtist18, frmMain.lblUserTopAlbumPlaycount18},
                                      {frmMain.lblUserTopAlbum19, frmMain.lblUserTopAlbumArtist19, frmMain.lblUserTopAlbumPlaycount19},
                                      {frmMain.lblUserTopAlbum20, frmMain.lblUserTopAlbumArtist20, frmMain.lblUserTopAlbumPlaycount20}}

    Public UserLTopAlbumsLabel(,) As Label = {{frmMain.lblUserLTopAlbum1, frmMain.lblUserLTopAlbumArtist1, frmMain.lblUserLTopAlbumPlaycount1},
                                      {frmMain.lblUserLTopAlbum2, frmMain.lblUserLTopAlbumArtist2, frmMain.lblUserLTopAlbumPlaycount2},
                                      {frmMain.lblUserLTopAlbum3, frmMain.lblUserLTopAlbumArtist3, frmMain.lblUserLTopAlbumPlaycount3},
                                      {frmMain.lblUserLTopAlbum4, frmMain.lblUserLTopAlbumArtist4, frmMain.lblUserLTopAlbumPlaycount4},
                                      {frmMain.lblUserLTopAlbum5, frmMain.lblUserLTopAlbumArtist5, frmMain.lblUserLTopAlbumPlaycount5},
                                      {frmMain.lblUserLTopAlbum6, frmMain.lblUserLTopAlbumArtist6, frmMain.lblUserLTopAlbumPlaycount6},
                                      {frmMain.lblUserLTopAlbum7, frmMain.lblUserLTopAlbumArtist7, frmMain.lblUserLTopAlbumPlaycount7},
                                      {frmMain.lblUserLTopAlbum8, frmMain.lblUserLTopAlbumArtist8, frmMain.lblUserLTopAlbumPlaycount8},
                                      {frmMain.lblUserLTopAlbum9, frmMain.lblUserLTopAlbumArtist9, frmMain.lblUserLTopAlbumPlaycount9},
                                      {frmMain.lblUserLTopAlbum10, frmMain.lblUserLTopAlbumArtist10, frmMain.lblUserLTopAlbumPlaycount10},
                                      {frmMain.lblUserLTopAlbum11, frmMain.lblUserLTopAlbumArtist11, frmMain.lblUserLTopAlbumPlaycount11},
                                      {frmMain.lblUserLTopAlbum12, frmMain.lblUserLTopAlbumArtist12, frmMain.lblUserLTopAlbumPlaycount12},
                                      {frmMain.lblUserLTopAlbum13, frmMain.lblUserLTopAlbumArtist13, frmMain.lblUserLTopAlbumPlaycount13},
                                      {frmMain.lblUserLTopAlbum14, frmMain.lblUserLTopAlbumArtist14, frmMain.lblUserLTopAlbumPlaycount14},
                                      {frmMain.lblUserLTopAlbum15, frmMain.lblUserLTopAlbumArtist15, frmMain.lblUserLTopAlbumPlaycount15},
                                      {frmMain.lblUserLTopAlbum16, frmMain.lblUserLTopAlbumArtist16, frmMain.lblUserLTopAlbumPlaycount16},
                                      {frmMain.lblUserLTopAlbum17, frmMain.lblUserLTopAlbumArtist17, frmMain.lblUserLTopAlbumPlaycount17},
                                      {frmMain.lblUserLTopAlbum18, frmMain.lblUserLTopAlbumArtist18, frmMain.lblUserLTopAlbumPlaycount18},
                                      {frmMain.lblUserLTopAlbum19, frmMain.lblUserLTopAlbumArtist19, frmMain.lblUserLTopAlbumPlaycount19},
                                      {frmMain.lblUserLTopAlbum20, frmMain.lblUserLTopAlbumArtist20, frmMain.lblUserLTopAlbumPlaycount20}}

    Public Function GetUserTopAlbumRowIndex(rowObject As Label) As Byte
        ' determine user vs userl
        If rowObject.Name.Contains("UserL") = False Then
            ' user
            For row As Byte = 0 To 19
                For column As Byte = 0 To 2
                    If rowObject.Name = UserTopAlbumsLabel(row, column).Name Then
                        Return row
                    End If
                Next
            Next
        Else
            ' userlookup
            For row As Byte = 0 To 19
                For column As Byte = 0 To 2
                    If rowObject.Name = UserLTopAlbumsLabel(row, column).Name Then
                        Return row
                    End If
                Next
            Next
        End If
        Return 255
    End Function
End Module