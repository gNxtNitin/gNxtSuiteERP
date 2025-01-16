Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO
Imports System.ComponentModel

Friend Class frmSearchGrid
    Inherits System.Windows.Forms.Form
    Dim mCurrRowPos As Integer
    Dim CurrCol As Integer

    Dim IsSorted As Boolean
    Dim lastsearchrow As Long
    Dim lastsearchlen As Long
    Dim lastcol As Long
    Private FormLoaded As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        AcName = ""
        AcName1 = ""
        AcName2 = ""
        AcName3 = ""
        AcName4 = ""
        AcName5 = ""
        AcName6 = ""
        AcName7 = ""
        AcName8 = ""
        AcName9 = ""
        AcName10 = ""
        AcName11 = ""
        '    frmSearchGrid.Hide  '03/01/2004
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdSelect_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSelect.Click
        Dim mMaxCol As Long
        mMaxCol = SprdView.MaxCols
        SprdView.Row = SprdView.ActiveRow
        SprdView.Col = 1 'SprdView.ActiveCol
        AcName = Trim(SprdView.Text)
        AcName1 = Trim(lblName.Text)

        SprdView.Row = SprdView.ActiveRow
        If mMaxCol >= 3 Then
            SprdView.Col = 3
            AcName2 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 4 Then
            SprdView.Col = 4
            AcName3 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 5 Then
            SprdView.Col = 5
            AcName4 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 6 Then
            SprdView.Col = 6
            AcName5 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 7 Then
            SprdView.Col = 7
            AcName6 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 8 Then
            SprdView.Col = 8
            AcName7 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 9 Then
            SprdView.Col = 9
            AcName8 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 10 Then
            SprdView.Col = 10
            AcName9 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 11 Then
            SprdView.Col = 11
            AcName10 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 12 Then
            SprdView.Col = 12
            AcName11 = Trim(SprdView.Text)
        End If

        ''frmSearchGrid.Hide  '03/01/2004
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub frmSearchGrid_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Dim i As Integer
        Dim tempstr As Object = Nothing

        If FormLoaded = True Then Exit Sub

        FormLoaded = True
        IsSorted = False
        lastsearchlen = 0

        'Load data
        GetBoundRecord("", "", lblQuery.Text, 0)

        'Init the header display
        'With SprdView
        '    .EditModePermanent = True
        '    .ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsVertical ''FPSpreadADO.ScrollBarsConstants.ScrollBarsVertical ''ScrollBarsVertical
        '    .RowHeaderDisplay = FPSpreadADO.HeaderDisplayConstants.DispBlank  ' DispBlank
        '    .ProcessTab = True
        '    .MaxRows = 1
        '    '.set_ColWidth(.Col, 24)
        '    .set_RowHeight(1, VB6.PixelsToTwipsX(32))
        '    .Row = 1
        '    .Col = -1
        '    .BackColor = Color.White        ''RGB(172, 172, 172)

        '    .Col = 0
        '    .ColHidden = True

        '    .MaxCols = SprdView.DataColCnt

        '    For i = 1 To SprdView.DataColCnt
        '        'Add Header text to search row
        '        SprdView.GetText(i, 0, tempstr)
        '        SprdViewHdr.SetText(i, 0, tempstr)
        '        tempstr = Nothing
        '    Next i



        'ClearText()
        'End With

        FormatSprdView(-1)
        Text1.Focus()
    End Sub
    Private Sub ClearText()

        'Clear any search text
        'SprdViewHdr.ClearRange(1, 1, SprdViewHdr.MaxCols, 1, True)

    End Sub
    Private Sub frmSearchGrid_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim x As Boolean

        FormLoaded = False

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2) '3300
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2) '1125
        MainClass.SetControlsColor(Me)
        'SprdView.DAutoSizeCols = FPSpreadADO.DAutoSizeColsConstants.DAutoSizeColsMax

        SprdView.DAutoCellTypes = True
        SprdView.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH

        CurrFormHeight = VB6.PixelsToTwipsY(Me.Height)
        CurrFormWidth = VB6.PixelsToTwipsX(Me.Width)
        Text1.Width = Me.Width - 30 ''CurrFormWidth - 100

        ' Control displays text tips aligned to pointer with focus
        SprdView.TextTip = FPSpreadADO.TextTipConstants.TextTipFloatingFocusOnly
        ' Control displays text tips after 250 milliseconds
        SprdView.TextTipDelay = 0.001
        ' Text tip displays custom font and colors
        ' Background is yellow, RGB(255, 255, 0)
        ' Foreground is dark blue, RGB(0, 0, 128)
        x = SprdView.SetTextTipAppearance("Segoe UI Semibold", CShort("11"), False, False, &HFFFF, &H800000)

        SprdView.RowHeaderDisplay = FPSpreadADO.HeaderDisplayConstants.DispBlank ''DispBlank
        SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle 'OperationModeSingle
        SprdView.ColHeadersShow = True

        Text1.BackColor = Color.LightYellow        ''Color.Aqua  ''LightBlue
        MainClass.SetSpreadColor(SprdView, -1, False)

        'MainClass.SetSpreadColor(SprdViewHdr, -1, False)
        MainClass.SearchCellColor(SprdView, 1, SprdView.MaxRows, 1, SprdView.MaxCols)

        lastcol = -1

    End Sub
    Private Sub GetBoundRecord(fpfname As String, pSearchText As String, pQuery As String, pColNo As Integer)
        'Dim query As String
        Dim i As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mString As String = ""
        Dim mPOS As Long
        Dim mActualFieldName As String = ""

        Dim mGroupPOS As Long

        Text1.Text = ""
        If lblGroupBy.Text = "False" Then
            mGroupPOS = InStr(UCase(pQuery), "GROUP BY")
            If mGroupPOS > 0 Then
                mString = pQuery
            Else
                mPOS = InStr(UCase(pQuery), "ORDER BY")
                mPOS = IIf(mPOS = 0, Len(pQuery), mPOS - 1)

                mString = Mid(pQuery, 1, mPOS)

                mActualFieldName = GetQueryFieldName(mString, pColNo)

                If mActualFieldName <> "" Then
                    If pSearchText <> "" Then
                        mString = mString & " AND " & mActualFieldName & " Like '%" & pSearchText & "%'"
                    End If
                End If
                'If pSearchText <> "" Then
                '    mString = mString & " AND " & fpfname & " Like '%" & pSearchText & "%'"
                'End If

                If pColNo = 0 Then
                    mString = mString & vbCrLf & " Order By 1"
                Else
                    mString = mString & vbCrLf & " Order By " & pColNo
                End If


                mString = mString & vbCrLf & " FETCH FIRST 500 ROWS ONLY"

            End If


            MainClass.ClearGrid(SprdView)
            MainClass.UOpenRecordSet(mString, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            SprdView.DataSource = Nothing
            SprdView.DataSource = RsTemp.DataSource
            SprdView.DataSource = Nothing

            'Else
            '    For i = 1 To SprdView.DataColCnt
            '        'Set col widths
            '        SprdViewHdr.colwidth(i) = SprdView.colwidth(i)
            '    Next i

            '    Set rs = New ADODB.Recordset
            '    rs.Open query, adoconn1, adOpenStatic, adLockOptimistic
            '    If Not rs.EOF Then
            '        'Attach spread to recordset
            '        fpSpread1.DAutoSizeCols = DAutoSizeColsBest
            '        Set fpSpread1.DataSource = rs
            '        Set fpSpread1.DataSource = Nothing

            '         For i = 1 To fpSpread1.DataColCnt
            '            'Set col widths
            '            fpSpread1head.colwidth(i) = fpSpread1.colwidth(i)

            '        Next i
            '    End If
        End If
    End Sub
    Public Function GetNthIndex(searchString As String, charToFind As Char, n As Integer) As Integer
        Dim charIndexPair = searchString.Select(Function(c, i) New With {.Character = c, .Index = i}) _
                                    .Where(Function(x) x.Character = charToFind) _
                                    .ElementAtOrDefault(n - 1)
        Return If(charIndexPair IsNot Nothing, charIndexPair.Index, -1)
    End Function
    Private Function GetQueryFieldName(ByRef pQuery As String, pColNo As Integer) As String
        'Dim mPOs As Long
        Dim pSearchQuery As String
        Dim pNewSearchQuery As String
        Dim Pos1 As Integer
        Dim Pos2 As Integer
        Dim mFieldName As String

        GetQueryFieldName = ""


        If pColNo = 0 Then Exit Function

        pSearchQuery = UCase(pQuery)

        pSearchQuery = Replace(pSearchQuery, vbCrLf, "")
        pSearchQuery = Replace(pSearchQuery, vbNewLine, "")
        'pSearchQuery = Replace(pSearchQuery, "& VBCRLF _", "")
        'pSearchQuery = Replace(pSearchQuery, "VBCRLF", "")
        pSearchQuery = Replace(pSearchQuery, "&", "")
        pSearchQuery = Trim(Replace(pSearchQuery, "DISTINCT", ""))

        Dim mSelectPos As Integer = InStr(pSearchQuery, "SELECT ")   ''GetNthIndex(pSearchQuery, "SELECT ", 1)
        Dim mFromPos As Integer = InStr(pSearchQuery, " FROM ")   '' GetNthIndex(pSearchQuery, " FROM ", 1)

        pNewSearchQuery = Mid(pSearchQuery, mSelectPos + Len("SELECT "), mFromPos - (Len("SELECT ") + 1))

        'pNewSearchQuery = Trim(Replace(pNewSearchQuery, " ", ""))
        pNewSearchQuery = Trim(Replace(pNewSearchQuery, "TO_CHAR(", ""))
        pNewSearchQuery = Trim(Replace(pNewSearchQuery, ",'DD-MON-YYYY')", ""))


        Dim SearchArray() As String = Split(pNewSearchQuery, ",")
        ' testArray holds {"apple", "", "", "", "pear", "banana", "", ""}
        Dim lastNonEmpty As Integer = -1
        For i As Integer = 0 To SearchArray.Length - 1
            If SearchArray(i) <> "" Then
                lastNonEmpty += 1
                SearchArray(lastNonEmpty) = SearchArray(i)
            End If
        Next
        ReDim Preserve SearchArray(lastNonEmpty)

        GetQueryFieldName = SearchArray(pColNo - 1)

        Dim mCasePos As Integer = InStr(pQuery, "CASE WHEN ")

        If mCasePos > 0 Then GetQueryFieldName = "" : Exit Function

        'If pColNo = 1 Then
        '    Pos1 = 1
        'Else
        '    Pos1 = GetNthIndex(pNewSearchQuery, ",", pColNo - 1)
        '    If Pos1 < 0 Then
        '        Pos1 = 1
        '    Else
        '        Pos1 = Pos1 + 2
        '    End If
        'End If

        'Pos2 = GetNthIndex(pNewSearchQuery, ",", pColNo)
        'Pos2 = IIf(Pos2 < 0, Len(pNewSearchQuery) - Pos1, Pos2 - Pos1)
        'mFieldName = Mid(pNewSearchQuery, Pos1, Pos2)

        'Dim mFieldAs As Integer = InStr(mFieldName, " AS ")

        'If mFieldAs > 0 Then
        '    mFieldName = Mid(mFieldName, 1, mFieldAs - 1)
        'End If
        'GetQueryFieldName = mFieldName

    End Function
    Private Sub GetBoundRecordAnyWhere(fpfname As String, pSearchText As String, pQuery As String, pColNo As Integer)
        'Dim query As String
        Dim i As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mString As String = ""
        Dim mPOS As Long
        Dim mActualFieldName As String = ""

        Dim mActualFieldName1 As String = ""
        Dim mActualFieldName2 As String = ""
        Dim mActualFieldName3 As String = ""
        Dim mActualFieldName4 As String = ""
        Dim mActualFieldName5 As String = ""
        Dim mActualFieldName6 As String = ""
        Dim mActualFieldName7 As String = ""
        Dim mActualFieldName8 As String = ""
        Dim mActualFieldName9 As String = ""
        Dim mActualFieldName10 As String = ""

        Dim mActualFieldName11 As String = ""
        Dim mActualFieldName12 As String = ""
        Dim mActualFieldName13 As String = ""
        Dim mActualFieldName14 As String = ""
        Dim mActualFieldName15 As String = ""

        Dim mGroupPOS As Long
        ClearText()

        If lblGroupBy.Text = "False" Then
            mGroupPOS = InStr(UCase(pQuery), "GROUP BY")
            If mGroupPOS > 0 Then
                mString = pQuery
            Else
                mPOS = InStr(UCase(pQuery), "ORDER BY")
                mPOS = IIf(mPOS = 0, Len(pQuery), mPOS - 1)

                mString = Mid(pQuery, 1, mPOS)

                mActualFieldName = GetQueryFieldName(mString, 1)

                If SprdView.MaxCols >= 2 Then
                    mActualFieldName1 = GetQueryFieldName(mString, 2)
                End If

                If SprdView.MaxCols >= 3 Then
                    mActualFieldName2 = GetQueryFieldName(mString, 3)
                End If

                If SprdView.MaxCols >= 4 Then
                    mActualFieldName3 = GetQueryFieldName(mString, 4)
                End If

                If SprdView.MaxCols >= 5 Then
                    mActualFieldName4 = GetQueryFieldName(mString, 5)
                End If

                If SprdView.MaxCols >= 6 Then
                    mActualFieldName5 = GetQueryFieldName(mString, 6)
                End If

                If SprdView.MaxCols >= 7 Then
                    mActualFieldName6 = GetQueryFieldName(mString, 7)
                End If

                If SprdView.MaxCols >= 8 Then
                    mActualFieldName7 = GetQueryFieldName(mString, 8)
                End If

                If SprdView.MaxCols >= 9 Then
                    mActualFieldName8 = GetQueryFieldName(mString, 9)
                End If

                If SprdView.MaxCols >= 10 Then
                    mActualFieldName9 = GetQueryFieldName(mString, 10)
                End If

                If SprdView.MaxCols >= 11 Then
                    mActualFieldName9 = GetQueryFieldName(mString, 11)
                End If

                If SprdView.MaxCols >= 12 Then
                    mActualFieldName9 = GetQueryFieldName(mString, 12)
                End If

                If SprdView.MaxCols >= 13 Then
                    mActualFieldName9 = GetQueryFieldName(mString, 13)
                End If

                If SprdView.MaxCols >= 14 Then
                    mActualFieldName9 = GetQueryFieldName(mString, 14)
                End If

                If SprdView.MaxCols >= 15 Then
                    mActualFieldName9 = GetQueryFieldName(mString, 15)
                End If

                If pSearchText <> "" Then
                    If mActualFieldName <> "" Then
                        mString = mString & " AND (" & mActualFieldName & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName1 <> "" Then
                        mString = mString & " OR " & mActualFieldName1 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName2 <> "" Then
                        mString = mString & " OR " & mActualFieldName2 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName3 <> "" Then
                        mString = mString & " OR " & mActualFieldName3 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName4 <> "" Then
                        mString = mString & " OR " & mActualFieldName4 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName5 <> "" Then
                        mString = mString & " OR " & mActualFieldName5 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName6 <> "" Then
                        mString = mString & " OR " & mActualFieldName6 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName7 <> "" Then
                        mString = mString & " OR " & mActualFieldName7 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName8 <> "" Then
                        mString = mString & " OR " & mActualFieldName8 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName9 <> "" Then
                        mString = mString & " OR " & mActualFieldName9 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName10 <> "" Then
                        mString = mString & " OR " & mActualFieldName10 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName11 <> "" Then
                        mString = mString & " OR " & mActualFieldName11 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName12 <> "" Then
                        mString = mString & " OR " & mActualFieldName12 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName13 <> "" Then
                        mString = mString & " OR " & mActualFieldName13 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName14 <> "" Then
                        mString = mString & " OR " & mActualFieldName14 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName15 <> "" Then
                        mString = mString & " OR " & mActualFieldName15 & " Like '%" & pSearchText & "%'"
                    End If

                    If mActualFieldName <> "" Then
                        mString = mString & ")"
                    End If
                End If

                'If pSearchText <> "" Then
                '    mString = mString & " AND " & fpfname & " Like '%" & pSearchText & "%'"
                'End If

                If pColNo = 0 Then
                    mString = mString & vbCrLf & " Order By 1"
                Else
                    mString = mString & vbCrLf & " Order By " & pColNo
                End If


                mString = mString & vbCrLf & " FETCH FIRST 500 ROWS ONLY"

            End If


            MainClass.ClearGrid(SprdView)
            MainClass.UOpenRecordSet(mString, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            SprdView.DataSource = Nothing
            SprdView.DataSource = RsTemp.DataSource
            SprdView.DataSource = Nothing
            MainClass.SearchCellColor(SprdView, 1, SprdView.MaxRows, 1, SprdView.MaxCols)
            'Else
            '    For i = 1 To SprdView.DataColCnt
            '        'Set col widths
            '        SprdViewHdr.colwidth(i) = SprdView.colwidth(i)
            '    Next i

            '    Set rs = New ADODB.Recordset
            '    rs.Open query, adoconn1, adOpenStatic, adLockOptimistic
            '    If Not rs.EOF Then
            '        'Attach spread to recordset
            '        fpSpread1.DAutoSizeCols = DAutoSizeColsBest
            '        Set fpSpread1.DataSource = rs
            '        Set fpSpread1.DataSource = Nothing

            '         For i = 1 To fpSpread1.DataColCnt
            '            'Set col widths
            '            fpSpread1head.colwidth(i) = fpSpread1.colwidth(i)

            '        Next i
            '    End If
        End If

    End Sub
    Private Sub frmSearchGrid_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MainClass.AssignDataInSprd8("", "", "", "N")
        'AcName = ""
        'AcName1 = ""
        'AcName2 = ""
        'AcName3 = ""
        'AcName4 = ""
        'AcName5 = ""
        'AcName6 = ""
        'AcName7 = ""
        'AcName8 = ""
        'AcName9 = ""
        'AcName10 = ""
        '''    frmSearchGrid.Hide  '03/01/2004
        'Me.Hide()
        'Me.Dispose()
        'Me.Close()
    End Sub

    Private Sub optOrderType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optOrderType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optOrderType.GetIndex(eventSender)
            Dim I As Integer
            Dim mStartPos As Integer
            Dim mEndPos As Integer

            If Index = 0 Then
                For I = mStartPos To mEndPos
                    SprdView.Col = 1
                    SprdView.Row = I
                    SprdView.RowHidden = False
                Next
            End If
        End If
    End Sub
    Private Sub SprdView_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdView.ClickEvent
        'Dim mCol1 As Long
        'Dim mCol2 As Long
        Dim mMaxCol As Double

        mMaxCol = SprdView.MaxCols

        If eventArgs.row = 0 Then
            '        mCol1 = Col
            '        mCol2 = IIf(mCol1 = 1, 2, 1)
            '
            '        Call MainClass.SortGrid(SprdView, mCol1, mCol2)
            Exit Sub
        End If
        SprdView.Row = eventArgs.row
        SprdView.Col = 1
        Text1.Text = SprdView.Text

        SprdView.Col = 2
        lblName.Text = Trim(SprdView.Text)

        SprdView.Row = eventArgs.row
        If mMaxCol >= 3 Then
            SprdView.Col = 3
            AcName2 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 4 Then
            SprdView.Col = 4
            AcName3 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 5 Then
            SprdView.Col = 5
            AcName4 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 6 Then
            SprdView.Col = 6
            AcName5 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 7 Then
            SprdView.Col = 7
            AcName6 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 8 Then
            SprdView.Col = 8
            AcName7 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 9 Then
            SprdView.Col = 9
            AcName8 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 10 Then
            SprdView.Col = 10
            AcName9 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 11 Then
            SprdView.Col = 11
            AcName10 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 12 Then
            SprdView.Col = 12
            AcName11 = Trim(SprdView.Text)
        End If

    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        Dim mMaxCol As Double
        If eventArgs.row = 0 Then Exit Sub

        mMaxCol = SprdView.MaxCols

        SprdView.Row = eventArgs.row
        SprdView.Col = 1
        Text1.Text = SprdView.Text
        AcName = Trim(SprdView.Text)


        SprdView.Col = 2
        lblName.Text = Trim(SprdView.Text)
        AcName1 = Trim(SprdView.Text)

        'AcName = Text1.Text
        'AcName1 = lblName.Text

        SprdView.Row = SprdView.ActiveRow
        If mMaxCol >= 3 Then
            SprdView.Col = 3
            AcName2 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 4 Then
            SprdView.Col = 4
            AcName3 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 5 Then
            SprdView.Col = 5
            AcName4 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 6 Then
            SprdView.Col = 6
            AcName5 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 7 Then
            SprdView.Col = 7
            AcName6 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 8 Then
            SprdView.Col = 8
            AcName7 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 9 Then
            SprdView.Col = 9
            AcName8 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 10 Then
            SprdView.Col = 10
            AcName9 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 11 Then
            SprdView.Col = 11
            AcName10 = Trim(SprdView.Text)
        End If

        If mMaxCol >= 12 Then
            SprdView.Col = 12
            AcName11 = Trim(SprdView.Text)
        End If

        Me.Hide() '03/01/2004
        Me.Dispose()
        Me.Close()
    End Sub


    Private Sub FormatSprdView(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer
        Dim mCols As Integer
        Dim tempstr As Object = Nothing
        Dim mMaxColLen As Long
        Dim mFormWidth As Long
        Dim mCellWidth As Long
        Dim mActCellWidth As Long
        Dim mCellWidthPer As Double

        'SprdView.DAutoSizeCols = FPSpreadADO.DAutoSizeColsConstants.DAutoSizeColsMax

        'With SprdViewHdr
        '    .set_RowHeight(0, VB6.PixelsToTwipsX(20))
        '    .set_RowHeight(-1, VB6.PixelsToTwipsX(20))
        '    '.Row = Arow
        '    '.Col = 0
        '    '.Text = " "
        '    '.ColHidden = False
        'End With

        With SprdView
            .Row = Arow
            .set_RowHeight(0, VB6.PixelsToTwipsX(30))
            .set_RowHeight(Arow, VB6.PixelsToTwipsX(20))
            mCols = .MaxCols

            .Col = 0
            .Text = " "
            .ColHidden = True

            For I = 1 To mCols
                .Col = I
                If lblFieldType.Text = "D" And .Col = 1 Then
                    .CellType = SS_CELL_TYPE_DATE
                    .TypeDateCentury = True
                    '                .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
                    .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
                    .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
                    .set_ColUserSortIndicator(I, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending)
                Else
                    .CellType = SS_CELL_TYPE_EDIT
                    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
                    .set_ColUserSortIndicator(I, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending)
                End If
            Next
            MainClass.ProtectCell(SprdView, 1, SprdView.MaxRows, 1, SprdView.MaxCols)
            .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSort
        End With

        MainClass.SetSpreadColor(SprdView, Arow, False)
        SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
        SprdView.DAutoCellTypes = True
        SprdView.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH

        'mMaxColLen = FPSpreadADO.DAutoSizeColsConstants.DAutoSizeColsMax
        'SprdView.DAutoSizeCols = FPSpreadADO.DAutoSizeColsConstants.DAutoSizeColsMax
        'MainClass.CellColor(SprdView, 1, SprdView.MaxRows, 1, SprdView.MaxCols)



        mFormWidth = VB6.PixelsToTwipsX(SprdView.Width - 100)  ''CurrFormWidth

        For I = 1 To SprdView.DataColCnt
            mCellWidth = mCellWidth + SprdView.get_ColWidth(I)
        Next

        mActCellWidth = mCellWidth

        mCellWidth = IIf(mCellWidth >= VB6.PixelsToTwipsX(Screen.PrimaryScreen.WorkingArea.Width * 0.9), VB6.PixelsToTwipsX(Screen.PrimaryScreen.WorkingArea.Width * 0.9), mCellWidth)

        If mCellWidth > mFormWidth Then
            Me.Width = VB6.TwipsToPixelsX(mCellWidth)
            SprdView.Width = VB6.TwipsToPixelsX(mCellWidth)
            'SprdViewHdr.Width = VB6.TwipsToPixelsX(mCellWidth)
            Me.StartPosition = FormStartPosition.Manual
            Frame2.Width = SprdView.Width - 15
            cmdCancel.Left = Frame2.Width - 80

            Dim x As Long = Screen.PrimaryScreen.WorkingArea.Width - Me.Width
            Me.Left = CInt(x / 2)

            '    x = r.Width - frm.Width
            '    y = r.Height - frm.Height
            'End If

            'x = CInt(x / 2)
            'y = CInt(y / 2)

            'frm.StartPosition = FormStartPosition.Manual
            'frm.Location = New Point(x, y)
        End If

        mFormWidth = VB6.PixelsToTwipsX(SprdView.Width)

        mCellWidthPer = IIf(mActCellWidth > 0, (mFormWidth - 400) / mActCellWidth, 1)

        For I = 1 To SprdView.DataColCnt
            'Set col widths
            'SprdViewHdr.colwidth(I) = SprdView.colwidth(I)
            'SprdView.set_ColWidth(I, FPSpreadADO.DAutoSizeColsConstants.DAutoSizeColsBest)

            'mFormWidth = mFormWidth - SprdView.get_ColWidth(I)
            'If mFormWidth > 0 And I = SprdView.DataColCnt Then
            '    SprdView.set_ColWidth(I, mFormWidth + SprdView.get_ColWidth(I))
            '    SprdViewHdr.set_ColWidth(I, mFormWidth + SprdView.get_ColWidth(I))
            'Else
            SprdView.set_ColWidth(I, SprdView.get_ColWidth(I) * mCellWidthPer)
            'SprdViewHdr.set_ColWidth(I, SprdView.get_ColWidth(I))
            'End If



            'SprdViewHdr.CellType = SprdView.CellType
            'SprdViewHdr.TypeEditCharSet = SprdView.TypeEditCharSet
            'SprdViewHdr.Col = I
            'SprdViewHdr.Row = -1
            'SprdViewHdr.TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

        Next I
        SprdView.DAutoSizeCols = SS_AUTOSIZE_NO       '' FPSpreadADO.DAutoSizeColsConstants.DAutoSizeColsNone

        For I = 1 To SprdView.DataColCnt
            'Add Header text to search row
            SprdView.GetText(I, 0, tempstr)
            'SprdViewHdr.SetText(I, 0, tempstr)
            'SprdView.GetText(I, 0, Text1.Text)
            'SprdViewHdr.SetText(I, 0, Text1.Text)
            tempstr = Nothing
        Next I



        MainClass.SetSpreadColor(SprdView, Arow, False)
        MainClass.SearchCellColor(SprdView, 1, SprdView.MaxRows, 1, SprdView.MaxCols)
        SprdView.ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsVertical

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            SprdView.ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsBoth '' 3 ''ScrollBarsBoth
            SprdView.ScrollBarExtMode = True
            SprdView.VScrollSpecial = False
            SprdView.VScrollSpecialType = 0 '' FPSpreadADO.VScrollSpecialTypeConstants.VScrollSpecialNoPageUpDown    ''VScrollSpecialTypeDefault       ''SS_VSCROLLSPECIAL_NO_PAGE_UP_DOWN
            SprdView.ProcessTab = True
        Else

        End If


        'MySpread.GrayAreaBackColor = Color.AliceBlue
        'MySpread.ShadowColor = Color.SkyBlue
        'MySpread.ShadowText = Color.OrangeRed   ''  Black  ''&HFF
        'MySpread.ScrollBarHColor = Color.AliceBlue
        'MySpread.ScrollBarVColor = Color.AliceBlue
        'MySpread.SelForeColor = Color.Black
        'MySpread.SelBackColor = Color.LightGoldenrodYellow      ''Black

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub SprdView_TextTipFetch(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles SprdView.TextTipFetch
        Dim mStock As Double
        Dim mItemCode As String
        Dim mItemUOM As String = ""

        If lblStockShow.Text = "Y" Then
            eventArgs.showTip = True
            SprdView.Row = eventArgs.row
            SprdView.Col = Val(lblItemCol.Text)
            mItemCode = Trim(SprdView.Text)
            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                mItemUOM = Trim(MasterNo)
            End If

            mStock = GetBalanceStockQty(mItemCode, VB6.Format(PubCurrDate, "dd/MM/yyyy"), mItemUOM, "STR", "ST", "", ConWH, -1)
            eventArgs.tipText = mItemCode & ":     " & VB6.Format(mStock, "0.0000")
        End If

    End Sub

    Private Sub Text1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles Text1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, Text1.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If

        Dim stext As Object = Nothing
        Dim fpfname As Object = Nothing
        Dim I As Long


        'Find the search text

        ''New column search ''Sandeep
        'If lastcol <> SprdViewHdr.ActiveCol Then
        '    'Clear existing text
        '    ClearText()
        '    SortCol(SprdViewHdr.ActiveCol)
        'End If

        'lastcol = SprdViewHdr.ActiveCol ''Sandeep

        ''Get search text
        'SprdViewHdr.GetText(SprdViewHdr.ActiveCol, 1, stext)  ''Sandeep
        stext = Text1.Text

        If KeyAscii = 8 Then        ''If KeyAscii = 8 Then
            'backspace
            If Len(stext) = 1 Then stext = ""
        Else
            stext = stext & Chr(KeyAscii)       ''stext = stext & Chr(KeyAscii)
        End If

        'Get field name, from col header text
        SprdView.GetText(1, 0, fpfname)

        If lblGroupBy.Text = "False" Then 'If Option1.Value = True Then
            '    'Bound
            Call GetBoundRecordAnyWhere(CStr(fpfname), CStr(stext), lblQuery.Text, 1)

        Else
            'Unbound

            If IsSorted = False Then
                'Sort data
                SortCol(SprdView.ActiveCol)
                lastsearchrow = 1
            End If
            For I = 1 To SprdView.MaxCols
                If FindMatch(I, CStr(stext)) = True Then
                    Exit For
                End If
            Next
        End If

        'Dim I As Long

        'For I = 1 To SprdView.DataColCnt
        '    'Set col widths
        '    'SprdViewHdr.colwidth(I) = SprdView.colwidth(I)
        '    SprdViewHdr.set_ColWidth(I, SprdView.get_ColWidth(I))
        '    'SprdViewHdr.CellType = SprdView.CellType
        '    'SprdViewHdr.TypeEditCharSet = SprdView.TypeEditCharSet
        '    SprdViewHdr.Col = I
        '    SprdViewHdr.Row = -1
        '    SprdViewHdr.TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

        'Next I

        'FormatSprdView(-1)
        'MainClass.CellColor(SprdView, 1, SprdView.MaxRows, 1, SprdView.MaxCols)
    End Sub

    Private Sub Text1_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles Text1.KeyUp
        '        If lblGroupBy.Text = "False" Then Exit Sub
        '        Dim KeyCode As Short = eventArgs.KeyCode
        '        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '        Dim I As Integer
        '        Dim mStartPos As Integer
        '        Dim mMidPos As Integer
        '        Dim mEndPos As Integer
        '        Dim mLen As Short
        '        Dim mSearchItem As String
        '        Dim mFindItem As String

        '        mStartPos = 1
        '        mEndPos = SprdView.MaxRows
        '        mMidPos = Int((mStartPos + mEndPos) / 2)
        '        mLen = Len(Trim(Text1.Text))

        '        mSearchItem = UCase(Trim(Text1.Text))
        '        If mSearchItem = "" Then
        '            SprdView.Col = 1
        '            SprdView.Row = 1
        '            SprdView.Position = SS_POSITION_UPPER_LEFT
        '            SprdView.Action = SS_ACTION_ACTIVE_CELL
        '            SprdView.Action = SS_ACTION_GOTO_CELL
        '            Exit Sub
        '        End If
        '        mFindItem = ""

        '        If optOrderType(0).Checked Then

        'RepeatLoop:
        '            Do While mStartPos <= mEndPos And mFindItem <> mSearchItem
        '                SprdView.Col = 1
        '                SprdView.Row = mMidPos
        '                mFindItem = UCase(Trim(SprdView.Text))
        '                If mSearchItem = VB.Left(mFindItem, mLen) Then
        '                    SprdView.Col = 1
        '                    SprdView.Row = mMidPos - 1
        '                    mFindItem = UCase(Trim(SprdView.Text))
        '                    If mSearchItem = VB.Left(mFindItem, mLen) Then
        '                        mEndPos = mMidPos - 1
        '                        mMidPos = Int((mStartPos + mEndPos) / 2)
        '                        GoTo RepeatLoop
        '                    End If
        '                    SprdView.Row = mMidPos
        '                    SprdView.Position = SS_POSITION_UPPER_LEFT
        '                    SprdView.Action = SS_ACTION_ACTIVE_CELL
        '                    SprdView.Action = SS_ACTION_GOTO_CELL
        '                    Exit Do
        '                End If
        '                If mSearchItem < mFindItem Then
        '                    mEndPos = mMidPos - 1
        '                Else
        '                    mStartPos = mMidPos + 1
        '                End If
        '                mMidPos = Int((mStartPos + mEndPos) / 2)
        '            Loop
        '        Else
        'RepeatLoop1:
        '            ''        I = SprdView.SearchCol(1, 0, -1, mSearchItem, SearchFlagsGreaterOrEqual)
        '            ''        If I <> -1 Then
        '            '            SprdView.ShowCell 1, I, PositionUpperLeft
        '            ''            SprdView.SetSelection 1, I, SprdView.MaxCols, I
        '            ''        End If
        '            If mFindItem <> mSearchItem Then
        '                For I = mStartPos To mEndPos
        '                    SprdView.Col = 1
        '                    SprdView.Row = I
        '                    mFindItem = UCase(Trim(SprdView.Text))
        '                    '            If mSearchItem = Left(mFindItem, mLen) Then
        '                    If InStr(1, mFindItem, mSearchItem) = 0 Then
        '                        SprdView.RowHidden = True
        '                    Else
        '                        SprdView.RowHidden = False
        '                    End If
        '                Next
        '            End If
        '        End If
    End Sub
    Private Sub SortCol(Col As Long)
        'Sort the specified column
        IsSorted = True
        SprdView.Sort(1, 1, SprdView.MaxCols, SprdView.DataRowCnt, FPSpreadADO.SortByConstants.SortByRow, Col, SS_SORT_ORDER_ASCENDING) ''SS_SORT_ORDER_ASCENDING ''SortByRow

    End Sub

    Private Function FindMatch(Col As Long, stext As String) As Boolean
        'Highlight the matching item
        Dim i As Long

        'No search criteria
        If stext = "" Then
            SprdView.TopRow = 1
            FindMatch = False
            Exit Function
        End If

        stext = UCase(stext)

        With SprdView

            'If backspacing, start search at row 1
            If lastsearchlen >= Len(stext) Then
                lastsearchrow = 1
            End If

            i = .SearchCol(Col, lastsearchrow, .DataRowCnt, stext, FPSpreadADO.SearchFlagsConstants.SearchFlagsPartialMatch)   ''.SearchFlagsGreaterOrEqual'' SearchFlagsGreaterOrEqual

            If i > 1 Then
                .TopRow = i
                .Col = 1        ''Col
                .Row = i
                .Position = SS_POSITION_UPPER_LEFT
                .Action = SS_ACTION_ACTIVE_CELL
                .Action = SS_ACTION_GOTO_CELL
                FindMatch = True
            Else
                FindMatch = False
            End If
        End With

    End Function

    Private Function IsMatch(searchstring As String, Row As Long) As Boolean
        'See if the text matches
        Dim tempstr As String
        Dim CntCol As Long

        IsMatch = False
        With SprdView
            For CntCol = 1 To .MaxCols
                .Row = Row
                .Col = CntCol
                tempstr = Trim(.Text)
                If InStr(tempstr, searchstring) > 0 Then
                    IsMatch = True
                    Exit Function
                End If
            Next
        End With

        'tempstr = VB.Left(searchstring, Len(searchfor))

        'If tempstr = searchfor Then
        '    IsMatch = True
        'Else
        '    IsMatch = False
        'End If

    End Function

    Private Sub SprdViewHdr_TopLeftChange(sender As Object, e As _DSpreadEvents_TopLeftChangeEvent)
        'Scroll the data spread
        If SprdView.LeftCol <> e.newLeft Then
            SprdView.LeftCol = e.newLeft
        End If
    End Sub

    'Private Sub SprdViewHdr_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent)
    '    Dim stext As Object = Nothing
    '    Dim fpfname As Object = Nothing
    '    'Find the search text

    '    'New column search
    '    If lastcol <> SprdViewHdr.ActiveCol Then
    '        'Clear existing text
    '        ClearText()
    '        SortCol(SprdViewHdr.ActiveCol)
    '    End If

    '    lastcol = SprdViewHdr.ActiveCol

    '    'Get search text
    '    SprdViewHdr.GetText(SprdViewHdr.ActiveCol, 1, stext)

    '    If e.keyAscii = 8 Then        ''If KeyAscii = 8 Then
    '        'backspace
    '        If Len(stext) = 1 Then stext = ""
    '    Else
    '        stext = stext & Chr(e.keyAscii)       ''stext = stext & Chr(KeyAscii)
    '    End If

    '    'Get field name, from col header text
    '    SprdViewHdr.GetText(SprdViewHdr.ActiveCol, 0, fpfname)

    '    If lblGroupBy.Text = "False" Then  ''If Option1.Value = True Then
    '        '    'Bound
    '        Call GetBoundRecord(CStr(fpfname), CStr(stext), lblQuery.Text, SprdViewHdr.ActiveCol)

    '    Else
    '        'Unbound
    '        If IsSorted = False Then
    '            'Sort data
    '            SortCol(SprdViewHdr.ActiveCol)
    '            lastsearchrow = 1
    '        End If

    '        FindMatch(SprdViewHdr.ActiveCol, CStr(stext))


    '    End If
    '    'Dim I As Long

    '    'For I = 1 To SprdView.DataColCnt
    '    '    'Set col widths
    '    '    'SprdViewHdr.colwidth(I) = SprdView.colwidth(I)
    '    '    SprdViewHdr.set_ColWidth(I, SprdView.get_ColWidth(I))
    '    '    'SprdViewHdr.CellType = SprdView.CellType
    '    '    'SprdViewHdr.TypeEditCharSet = SprdView.TypeEditCharSet
    '    '    SprdViewHdr.Col = I
    '    '    SprdViewHdr.Row = -1
    '    '    SprdViewHdr.TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

    '    'Next I
    '    'FormatSprdView(-1)
    '    'MainClass.CellColor(SprdView, 1, SprdView.MaxRows, 1, SprdView.MaxCols)
    'End Sub


    Private Sub SprdView_TopLeftChange(sender As Object, e As _DSpreadEvents_TopLeftChangeEvent) Handles SprdView.TopLeftChange
        'If SprdViewHdr.LeftCol <> e.newLeft Then
        '    SprdViewHdr.LeftCol = e.newLeft
        'End If
    End Sub

    Private Sub cmdSearchAnyWhere_Click(sender As Object, e As EventArgs) Handles cmdSearchAnyWhere.Click


        'Call GetBoundRecordAnyWhere(CStr(Text1.Text), lblQuery.Text)

        'If IsSorted = False Then
        '    'Sort data
        '    SortCol(1)
        'End If

        'FindMatch(1, CStr(Text1.Text))

        Dim CntRow As Long

        If Trim(Text1.Text) = "" Then
            For CntRow = 1 To SprdView.MaxRows
                SprdView.Row = CntRow
                SprdView.RowHidden = False
            Next
            Exit Sub
        End If
        With SprdView
            For CntRow = 1 To .MaxRows
                SprdView.Row = CntRow
                If IsMatch(Trim(Text1.Text), CntRow) = False Then
                    SprdView.RowHidden = True
                Else
                    SprdView.RowHidden = False
                End If
            Next
        End With
    End Sub

    Private Sub Text1_Validating(sender As Object, e As CancelEventArgs) Handles Text1.Validating
        Dim CntRow As Long

        If Trim(Text1.Text) = "" Then
            For CntRow = 1 To SprdView.MaxRows
                SprdView.Row = CntRow
                SprdView.RowHidden = False
            Next
            Exit Sub
        End If
    End Sub

    'Private Sub SprdViewHdr_ColWidthChange(sender As Object, e As _DSpreadEvents_ColWidthChangeEvent)
    '    Dim I As Long

    '    For I = 1 To SprdViewHdr.MaxCols
    '        'Set col widths
    '        SprdView.set_ColWidth(I, SprdViewHdr.get_ColWidth(I))
    '    Next I
    'End Sub

    Private Sub SprdView_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent

        'Dim mCol As Short
        'mCol = SprdMain.ActiveCol
        'If EventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))

        If e.keyAscii = System.Windows.Forms.Keys.Enter Then
            Call SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, SprdView.ActiveRow))
        End If
    End Sub

    Private Sub frmSearchGrid_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 280, mReFormWidth - 260, mReFormWidth))
        'SprdViewHdr.Width = SprdView.Width
        CurrFormWidth = mReFormWidth
        Text1.Width = Me.Width - 30         'mReFormWidth - 100
        Frame2.Width = SprdView.Width - 15
        cmdCancel.Left = Frame2.Width - 80
        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
