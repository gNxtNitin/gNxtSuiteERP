Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSearchOuts
    Inherits System.Windows.Forms.Form

    Dim mCurrRowPos As Integer
    Dim CurrCol As Integer

    Dim IsSorted As Boolean
    Dim lastsearchrow As Long
    Dim lastsearchlen As Long

    Private Const ColBillNo As Short = 1
    Private Const ColBillDate As Short = 2
    Private Const ColLocID As Short = 3
    Private Const ColBillAmount As Short = 4
    Private Const ColBillAmountDC As Short = 5
    Private Const ColADVAmount As Short = 6
    Private Const ColDNAmount As Short = 7
    Private Const ColCNAmount As Short = 8
    Private Const ColTDSAmount As Short = 9
    Private Const ColBalance As Short = 10
    Private Const ColBalanceDC As Short = 11
    Private Const ColDueDate As Short = 12


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
        Me.Hide()
        Me.Close()
	End Sub
	
	Private Sub cmdSelect_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSelect.Click
		SprdView.Row = SprdView.ActiveRow
		SprdView.Col = SprdView.ActiveCol
		AcName = SprdView.Text
        AcName1 = lblName.Text

        SprdView.Col = ColLocID
        AcName2 = SprdView.Text

        SprdView.Col = ColBillAmount
        AcName3 = SprdView.Text

        SprdView.Col = ColBillAmountDC
        AcName4 = SprdView.Text

        SprdView.Col = ColADVAmount
        AcName5 = SprdView.Text

        SprdView.Col = ColDNAmount
        AcName6 = SprdView.Text

        SprdView.Col = ColCNAmount
        AcName7 = SprdView.Text

        SprdView.Col = ColTDSAmount
        AcName8 = SprdView.Text

        SprdView.Col = ColBalance
        AcName9 = SprdView.Text

        SprdView.Col = ColBalanceDC
        AcName10 = SprdView.Text

        Me.Hide()
        Me.Close()
	End Sub
    Private Sub frmSearchOuts_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'Dim i As Integer
        'Dim tempstr As Object = Nothing
        'IsSorted = False
        'lastsearchlen = 0

        ''Load data
        'GetBoundRecord("", "", lblQuery.Text)

        ''Init the header display
        'With SprdViewHdr
        '    .EditModePermanent = True
        '    .ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsVertical ''FPSpreadADO.ScrollBarsConstants.ScrollBarsVertical ''ScrollBarsVertical
        '    .RowHeaderDisplay = FPSpreadADO.HeaderDisplayConstants.DispBlank  ' DispBlank
        '    .ProcessTab = True
        '    .MaxRows = 1
        '    '.set_ColWidth(.Col, 24)
        '    .set_RowHeight(1, 15)
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



        '    ClearText()
        'End With

        FormatSprdView(-1)
        Text1.Focus()
    End Sub
    Private Sub frmSearchOuts_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2) '3300
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2) '1125
        MainClass.SetControlsColor(Me)
        FormatSprdView(-1)
        'SprdView.DAutoSizeCols = DAutoSizeColsMax
    End Sub

    Private Sub SprdView_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdView.ClickEvent
        If eventArgs.row = 0 Then Exit Sub
        SprdView.Row = eventArgs.row

        '    SprdView.Col = 1
        '    lblTrnType = SprdView.Text

        SprdView.Col = 1
        Text1.Text = SprdView.Text

        SprdView.Col = 2
        lblName.Text = SprdView.Text

        SprdView.Col = ColLocID
        AcName2 = SprdView.Text

        SprdView.Col = ColBillAmount
        AcName3 = SprdView.Text

        SprdView.Col = ColBillAmountDC
        AcName4 = SprdView.Text

        SprdView.Col = ColADVAmount
        AcName5 = SprdView.Text

        SprdView.Col = ColDNAmount
        AcName6 = SprdView.Text

        SprdView.Col = ColCNAmount
        AcName7 = SprdView.Text

        SprdView.Col = ColTDSAmount
        AcName8 = SprdView.Text

        SprdView.Col = ColBalance
        AcName9 = SprdView.Text

        SprdView.Col = ColBalanceDC
        AcName10 = SprdView.Text

    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        If eventArgs.Row = 0 Then Exit Sub
        AcName = Text1.Text
        AcName1 = lblName.Text

        SprdView.Row = eventArgs.row
        SprdView.Col = ColLocID
        AcName2 = SprdView.Text

        SprdView.Col = ColBillAmount
        AcName3 = SprdView.Text

        SprdView.Col = ColBillAmountDC
        AcName4 = SprdView.Text

        SprdView.Col = ColADVAmount
        AcName5 = SprdView.Text

        SprdView.Col = ColDNAmount
        AcName6 = SprdView.Text

        SprdView.Col = ColCNAmount
        AcName7 = SprdView.Text

        SprdView.Col = ColTDSAmount
        AcName8 = SprdView.Text

        SprdView.Col = ColBalance
        AcName9 = SprdView.Text

        SprdView.Col = ColBalanceDC
        AcName10 = SprdView.Text

        Me.Hide()
    End Sub

    Private Sub sprdView_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdView.KeyUpEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then
            AcName = Text1.Text
            AcName1 = lblName.Text

            SprdView.Row = SprdView.ActiveRow
            SprdView.Col = ColLocID
            AcName2 = SprdView.Text

            SprdView.Col = ColBillAmount
            AcName3 = SprdView.Text

            SprdView.Col = ColBillAmountDC
            AcName4 = SprdView.Text

            SprdView.Col = ColADVAmount
            AcName5 = SprdView.Text

            SprdView.Col = ColDNAmount
            AcName6 = SprdView.Text

            SprdView.Col = ColCNAmount
            AcName7 = SprdView.Text

            SprdView.Col = ColTDSAmount
            AcName8 = SprdView.Text

            SprdView.Col = ColBalance
            AcName9 = SprdView.Text

            SprdView.Col = ColBalanceDC
            AcName10 = SprdView.Text

            Me.Hide()
        End If
        If SprdView.ActiveRow = 0 Then Exit Sub
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        Text1.Text = SprdView.Text

        SprdView.Col = 2
        lblName.Text = SprdView.Text
    End Sub
    Private Sub FormatSprdView(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer
        Dim mCols As Integer


        ' SprdView.DAutoSizeCols = DAutoSizeColsMax
        With SprdView
            .Row = Arow
            .set_RowHeight(Arow, 12)
            mCols = .MaxCols

            .Col = 0
            .ColHidden = True

            For I = 1 To mCols
                .Col = I
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            Next

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 9)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 9)

            .Col = ColLocID
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 9)

            .Col = ColBillAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8)

            .Col = ColBillAmountDC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 3)

            .Col = ColADVAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8)

            .Col = ColDNAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8)

            .Col = ColCNAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8)

            .Col = ColTDSAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8)

            .Col = ColBalance
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8)

            .Col = ColBalanceDC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 3)

            .Col = ColDueDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 9)

            MainClass.ProtectCell(SprdView, 1, SprdView.MaxRows, 1, SprdView.MaxCols)
        End With
        MainClass.SetSpreadColor(SprdView, Arow)
        SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        '' SprdView.DAutoSizeCols = DAutoSizeColsMax
        MainClass.CellColor(SprdView, 1, SprdView.MaxRows, 1, SprdView.MaxCols)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub Text1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles Text1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, Text1.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
	
	Private Sub Text1_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles Text1.KeyUp
		Dim KeyCode As Short = eventArgs.KeyCode
		Dim Shift As Short = eventArgs.KeyData \ &H10000
        'Dim I As Integer
		Dim mStartPos As Integer
		Dim mMidPos As Integer
		Dim mEndPos As Integer
		Dim mLen As Short
		Dim mSearchItem As String
		Dim mFindItem As String
		
		mStartPos = 1
		mEndPos = SprdView.MaxRows
		mMidPos = Int((mStartPos + mEndPos) / 2)
		mLen = Len(Trim(Text1.Text))
		
		mSearchItem = UCase(Trim(Text1.Text))
		If mSearchItem = "" Then
			SprdView.Col = 1
			SprdView.Row = 1
			SprdView.Position = SS_POSITION_UPPER_LEFT
			SprdView.Action = SS_ACTION_ACTIVE_CELL
			SprdView.Action = SS_ACTION_GOTO_CELL
			Exit Sub
		End If
		mFindItem = ""
		
RepeatLoop: 
		Do While mStartPos <= mEndPos And mFindItem <> mSearchItem
			SprdView.Col = 1
			SprdView.Row = mMidPos
			mFindItem = UCase(Trim(SprdView.Text))
			If mSearchItem = VB.Left(mFindItem, mLen) Then
				SprdView.Col = 1
				SprdView.Row = mMidPos - 1
				mFindItem = UCase(Trim(SprdView.Text))
				If mSearchItem = VB.Left(mFindItem, mLen) Then
					mEndPos = mMidPos - 1
					mMidPos = Int((mStartPos + mEndPos) / 2)
					GoTo RepeatLoop
				End If
				SprdView.Row = mMidPos
				SprdView.Position = SS_POSITION_UPPER_LEFT
				SprdView.Action = SS_ACTION_ACTIVE_CELL
				SprdView.Action = SS_ACTION_GOTO_CELL
				Exit Do
			End If
			If mSearchItem < mFindItem Then
				mEndPos = mMidPos - 1
			Else
				mStartPos = mMidPos + 1
			End If
			mMidPos = Int((mStartPos + mEndPos) / 2)
		Loop 
	End Sub
End Class