Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProductionPlanDaily
    Inherits System.Windows.Forms.Form
    Dim RsProdPlanMain As ADODB.Recordset
    Dim RsProdPlanMonDetail As ADODB.Recordset
    Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColMKEY As Short = 1
    Private Const ColCode As Short = 2
    Private Const ColDescription As Short = 3
    Private Const ColInHouseCode As Short = 4
    Private Const ColInHouseDesc As Short = 5
    Private Const ColCapacity As Short = 6
    Private Const ColIPlanQty As Short = 7
    Private Const ColDPlanQtyA As Short = 8
    Private Const ColDPlanQtyB As Short = 9
    Private Const ColDPlanQtyC As Short = 10
    Private Const ColDPlanQty As Short = 11

    Dim mShowDate As String
    Dim pDeptCode As String
    Dim pProductCode As String
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub Clear1()

        lblMKey.Text = ""
        '    txtSupplierCode.Text = ""	
        '    lblSupplierCode.text = ""	
        txtCode.Text = ""
        lblDescription.Text = ""
        txtPlanDate.Text = ""

        '    optPlanType(1).Value = True	
        '	
        '    lblProductDept.text = "Dept Code"	
        '    SprdMain.Row = 0	
        '    SprdMain.Col = ColCode	
        '    SprdMain.Text = "Product Code"	

        If optPlanType(0).Checked = True Then
            lblProductDept.Text = "Product Code"
            SprdMain.Row = 0
            SprdMain.Col = ColCode
            SprdMain.Text = "Dept Code"
        ElseIf optPlanType(1).Checked = True Then
            lblProductDept.Text = "Dept Code"
            SprdMain.Row = 0
            SprdMain.Col = ColCode
            SprdMain.Text = "Product Code"
        End If

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        Call MakeEnableDesableField(True)
        SprdMain.Enabled = True
        '    CmdSave.Enabled = False	
        PrintStatus((False))
        MainClass.ButtonStatus(Me, XRIGHT, RsProdPlanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            If optPlanType(0).Checked = True Then
                pProductCode = Trim(txtCode.Text)
                pDeptCode = ""
            Else
                pProductCode = ""
                pDeptCode = Trim(txtCode.Text)
            End If
            '        pCustCode = Trim(txtSupplierCode.Text)	
            Clear1()
        Else
            ADDMode = False
            MODIFYMode = False
            '        If optPlanType(0).Value = True Then	
            '            pProductCode = Trim(txtCode.Text)	
            '            pDeptCode = ""	
            '        Else	
            '            pProductCode = ""	
            '            pDeptCode = Trim(txtCode.Text)	
            '        End If	
            '        pCustCode = Trim(txtSupplierCode.Text)	
            Clear1()
            If RsProdPlanMain.EOF = False Then
                RsProdPlanMain.MoveFirst()
            Else
                Exit Sub
            End If

            '        ShowRecord          ''Show1	

            lblMKey.Text = IIf(IsDbNull(RsProdPlanMain.Fields("AUTO_KEY_PRODPLAN").Value), "", RsProdPlanMain.Fields("AUTO_KEY_PRODPLAN").Value)
            If ShowDetail1(1, lblMkey.Text, mShowDate, pProductCode, pDeptCode) = False Then Exit Sub
            FormatSprdMain(-1)

            MakeEnableDesableField((False))
            ADDMode = False
            MODIFYMode = False
            MainClass.ButtonStatus(Me, XRIGHT, RsProdPlanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            '        CmdSave.Enabled = True	

        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        Dim mDate As String
        Dim mCheckDate As String
        Dim mDays As Integer

        '    If PubSuperUser <> "S" Then	
        '        mDate = txtPlanDate.Text	
        '        If RsCompany.fields("COMPANY_CODE").value = 1 Then	
        '            mCheckDate = DateAdd("d", 1, PubCurrDate)	
        '        Else	
        '            mCheckDate = PubCurrDate	
        '        End If	
        '        If CDate(txtPlanDate.Text) <= CDate(mCheckDate) Then	
        '            MsgInformation "You have no rights to change Plan of Current / Previous Date."	
        '            Exit Sub	
        '        End If	
        '    End If	
        '	
        If RsCompany.Fields("PROD_PALN_LOCK").Value = "Y" Then
            If PubSuperUser <> "S" Then
                mDays = IIf(IsDbNull(RsCompany.Fields("PROD_PALN_LOCK_DAY").Value), 0, RsCompany.Fields("PROD_PALN_LOCK_DAY").Value)
                mDays = mDays - 1
                mCheckDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, mDays, PubCurrDate))

                If CDate(txtPlanDate.Text) <= CDate(mCheckDate) Then
                    MsgInformation("You have no rights to change Plan of Current / Previous Date.")
                    Exit Sub
                End If
            End If
        End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsProdPlanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            '        txtPlanNo.Enabled = False	
            '        cmdSearchPlanNo.Enabled = False	
            SprdMain.Enabled = True
            '        PopulateMode (True)	
        Else
            ADDMode = False
            MODIFYMode = False
            ShowRecord()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
        'Resume	
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        If FieldsVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            MakeEnableDesableField((False))
            SprdMain.Enabled = True    '' False Sandeep 15/05/2022
            ADDMode = False
            MODIFYMode = False
            ShowRecord()
            ''22-06-2009         cmdClear.Enabled = True	
            PrintStatus((True))
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mMKEY As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        For cntRow = 1 To SprdMain.MaxRows
            SqlStr = ""

            SprdMain.Row = cntRow
            SprdMain.Col = ColMKEY
            mMKEY = Trim(SprdMain.Text)

            SqlStr = " UPDATE PRD_PRODPLAN_HDR SET " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND AUTO_KEY_PRODPLAN =" & Val(mMKEY) & ""

            PubDBCn.Execute(SqlStr)
        Next

        If UpdateDetail = False Then GoTo ErrPart


        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsProdPlanMain.Requery()
        RsProdPlanMonDetail.Requery()
        MsgBox(Err.Description)
        ''Resume	
    End Function

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mCode As String
        Dim mInhouseCode As String
        Dim mDPlanQtyA As Double
        Dim mDPlanQtyB As Double
        Dim mDPlanQtyC As Double
        Dim mDPlanQty As Double
        Dim mModDate As String
        Dim mMKEY As String
        Dim mFYEAR As Integer

        mFYEAR = GetCurrentFYNo(PubDBCn, (txtPlanDate.Text))

        mModDate = VB6.Format(GetServerDate, "DD-MMM-YYYY") & " " & GetServerTime


        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColMKEY
                mMKEY = MainClass.AllowSingleQuote(.Text)

                .Col = ColCode
                mCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColInHouseCode
                mInhouseCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColDPlanQtyA
                mDPlanQtyA = Val(.Text)

                .Col = ColDPlanQtyB
                mDPlanQtyB = Val(.Text)

                .Col = ColDPlanQtyC
                mDPlanQtyC = Val(.Text)

                .Col = ColDPlanQty
                mDPlanQty = Val(.Text)


                If mCode <> "" Then


                    SqlStr = " INSERT INTO PRD_PRODPLAN_MONTH_DET_HIS ( " & vbCrLf _
                        & " USERID, MODDATE, AUTO_KEY_PRODPLAN, " & vbCrLf _
                        & " COMPANY_CODE,  PRODUCT_CODE," & vbCrLf _
                        & " SCHLD_DATE, DEPT_CODE, SERIAL_DATE," & vbCrLf _
                        & " IPLAN_QTY, DPLAN_QTY, DPLAN_QTY_A, DPLAN_QTY_B, DPLAN_QTY_C, " & vbCrLf _
                        & " PROD_LOSS, PLAN_START, INHOUSE_CODE) " & vbCrLf _
                        & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & mModDate & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                        & " AUTO_KEY_PRODPLAN, " & vbCrLf _
                        & " COMPANY_CODE, PRODUCT_CODE," & vbCrLf _
                        & " SCHLD_DATE, DEPT_CODE, SERIAL_DATE," & vbCrLf _
                        & " IPLAN_QTY, DPLAN_QTY, DPLAN_QTY_A, DPLAN_QTY_B, DPLAN_QTY_C, " & vbCrLf _
                        & " PROD_LOSS, PLAN_START, INHOUSE_CODE" & vbCrLf _
                        & " FROM PRD_PRODPLAN_MONTH_DET " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                        & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & mFYEAR & " " & vbCrLf _
                        & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(txtPlanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    If optPlanType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf _
                            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
                            & " AND DEPT_CODE='" & mCode & "'"
                    ElseIf optPlanType(1).Checked = True Then
                        SqlStr = SqlStr & vbCrLf _
                            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
                            & " AND PRODUCT_CODE='" & mCode & "'"
                    End If

                    SqlStr = SqlStr & vbCrLf _
                        & " AND INHOUSE_CODE='" & MainClass.AllowSingleQuote(mInhouseCode) & "'"

                    PubDBCn.Execute(SqlStr)

                    SqlStr = " UPDATE PRD_PRODPLAN_MONTH_DET SET " & vbCrLf _
                        & " DPLAN_QTY_A=" & mDPlanQtyA & ", " & vbCrLf _
                        & " DPLAN_QTY_B=" & mDPlanQtyB & ", " & vbCrLf _
                        & " DPLAN_QTY_C=" & mDPlanQtyC & ", " & vbCrLf _
                        & " DPLAN_QTY=" & mDPlanQty & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                        & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & mFYEAR & " " & vbCrLf _
                        & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(txtPlanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    If optPlanType(0).Checked = True Then
                        SqlStr = SqlStr & vbCrLf _
                            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
                            & " AND DEPT_CODE='" & mCode & "'"
                    ElseIf optPlanType(1).Checked = True Then
                        SqlStr = SqlStr & vbCrLf _
                            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
                            & " AND PRODUCT_CODE='" & mCode & "'"
                    End If

                    SqlStr = SqlStr & vbCrLf _
                        & " AND INHOUSE_CODE='" & MainClass.AllowSingleQuote(mInhouseCode) & "'"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function


    Private Sub cmdSearchCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCode.Click
        Dim SqlStr As String = ""
        If optPlanType(0).Checked = True Then
            SqlStr = " SELECT A.ITEM_CODE, A.ITEM_SHORT_DESC " & vbCrLf _
                & " FROM INV_ITEM_MST A " & vbCrLf _
                & " WHERE A.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " ORDER BY A.ITEM_SHORT_DESC "
            If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                txtCode.Text = AcName
                lblDescription.text = AcName1
                If txtCode.Enabled = True Then txtCode.Focus()
            End If
        ElseIf optPlanType(1).Checked = True Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "
            If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", "", "", SqlStr) = True Then
                txtCode.Text = AcName1
                lblDescription.text = AcName
                If txtCode.Enabled = True Then txtCode.Focus()
            End If
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh	
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsProdPlanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmProductionPlanDaily_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Production Plan (Daily)"

        SqlStr = "Select * From PRD_PRODPLAN_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdPlanMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PRD_PRODPLAN_MONTH_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdPlanMonDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call SetTextLengths()
        ''22-06-2009 If cmdClear.Enabled = True Then cmdClear_Click	

        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        optPlanType(1).Checked = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub
    Private Sub frmProductionPlanDaily_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmProductionPlanDaily_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7710
        CurrFormWidth = 11985

        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(7710)
        'Me.Width = VB6.TwipsToPixelsX(11985)
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmProductionPlanDaily_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 400, mReFormWidth - 400, mReFormWidth))
        fraTop1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 300, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProdPlanMonDetail.Fields("AUTO_KEY_PRODPLAN").DefinedSize
            .set_ColWidth(ColMKEY, 10)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            If optPlanType(0).Checked = True Then
                .TypeEditLen = RsProdPlanMonDetail.Fields("DEPT_CODE").DefinedSize
            ElseIf optPlanType(1).Checked = True Then
                .TypeEditLen = RsProdPlanMonDetail.Fields("PRODUCT_CODE").DefinedSize
            End If
            .set_ColWidth(ColCode, 8)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False

            .Col = ColDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColDescription, 25)
            .TypeEditMultiLine = False

            .Col = ColInHouseCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProdPlanMonDetail.Fields("INHOUSE_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColInHouseCode, 8)
            .TypeEditMultiLine = False

            .Col = ColInHouseDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColInHouseDesc, 20)
            .TypeEditMultiLine = False

            .Col = ColCapacity
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCapacity, 10)
            .TypeFloatDecimalPlaces = 3

            .Col = ColIPlanQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIPlanQty, 10)
            .TypeFloatDecimalPlaces = 3

            .Col = ColDPlanQtyA
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDPlanQtyA, 10)
            .TypeFloatDecimalPlaces = 3

            .Col = ColDPlanQtyB
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDPlanQtyB, 10)
            .TypeFloatDecimalPlaces = 3

            .Col = ColDPlanQtyC
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDPlanQtyC, 10)
            .TypeFloatDecimalPlaces = 3

            .Col = ColDPlanQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDPlanQty, 10)
            .TypeFloatDecimalPlaces = 3

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColMKEY, ColIPlanQty)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDPlanQty, ColDPlanQty)
        End With

        MainClass.SetSpreadColor(SprdMain, Arow)
        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

        '        SprdMain.SetOddEvenRowColor &HC0FFFF, vbBlack, &HFFFFC0, vbBlack	
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        '    MainClass.SetSpreadColor sprdMain, Arow	
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        '    txtSupplierCode.MaxLength = RsProdPlanMain.Fields("SUPP_CUST_CODE").DefinedSize	
        txtCode.Maxlength = RsProdPlanMain.Fields("PRODUCT_CODE").DefinedSize
        txtPlanDate.Maxlength = RsProdPlanMain.Fields("SCHLD_DATE").Precision - 6

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim ii As Integer
        Dim mDeptCode As String
        Dim mMonthPlan As Double
        Dim mProductionQty As Double
        Dim mDayPlan As Double
        Dim mProductCode As String
        Dim mInhouseCode As String
        Dim mCheckDate As String
        Dim mDays As Integer
        Dim mTodayPlanningQty As Double
        Dim mSqlStr As String
        Dim mStockQty As Double
        Dim mMinStock As Double
        Dim mCapacity As Double

        FieldsVarification = True

        '    If Trim(txtSupplierCode.Text) = "" Then	
        '        MsgInformation "Supplier Code is empty, So unable to save."	
        '        txtSupplierCode.SetFocus	
        '        FieldsVarification = False	
        '        Exit Function	
        '    End If	
        If Trim(txtCode.Text) = "" Then
            MsgInformation("Product Code is empty, So unable to save.")
            txtCode.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtPlanDate.Text) = "" Then
            MsgInformation("Plan Date is empty, So unable to save.")
            txtPlanDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        Call GridCalc(-1)

        If RsCompany.Fields("PROD_PALN_LOCK").Value = "Y" Then
            If PubSuperUser <> "S" Then
                mDays = IIf(IsDbNull(RsCompany.Fields("PROD_PALN_LOCK_DAY").Value), 0, RsCompany.Fields("PROD_PALN_LOCK_DAY").Value)
                mDays = mDays - 1
                mCheckDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, mDays, PubCurrDate))

                If CDate(txtPlanDate.Text) <= CDate(mCheckDate) Then
                    MsgInformation("You have no rights to change Plan of Current / Previous Date.")
                    FieldsVarification = False
                    Exit Function
                End If

                mCheckDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 6, PubCurrDate))

                If CDate(txtPlanDate.Text) > CDate(mCheckDate) Then
                    MsgInformation("You have no rights to add after " & mDays & " days Plan of Current Date.")
                    FieldsVarification = False
                    Exit Function
                End If

            End If
        End If

        With SprdMain
            For ii = 1 To .MaxRows
                .Row = ii

                If optPlanType(0).Checked = True Then
                    .Col = ColCode
                    mDeptCode = Trim(.Text)

                    mProductCode = Trim(txtCode.Text)
                Else
                    mDeptCode = Trim(txtCode.Text)

                    .Col = ColCode
                    mProductCode = Trim(.Text)

                End If

                .Col = ColInHouseCode
                mInhouseCode = Trim(.Text)


                .Col = ColDPlanQty
                mDayPlan = Val(.Text)

                'If mDayPlan > 0 Then
                '    mCapacity = GetCapacity(mDeptCode, mInhouseCode)
                '    mTodayPlanningQty = GetTodayPlanning(mDeptCode, mProductCode, mInhouseCode)
                '    mProductionQty = GetProductionQty(mDeptCode, mInhouseCode)
                '    mMonthPlan = GetMonthlyPlanQty(mDeptCode, mProductCode, mInhouseCode)
                '    mStockQty = GetStockQty(mDeptCode, mProductCode, mInhouseCode)
                '    mMinStock = System.Math.Round(mMonthPlan * 2 / 25, 0)

                '    If mTodayPlanningQty > mCapacity Then
                '        If mCapacity = 0 Then
                '            MsgInformation("Line Capacity (" & mCapacity & ") is not Defined, So cann't be Save.")
                '            MainClass.SetFocusToCell(SprdMain, ii, ColDPlanQtyA)
                '            FieldsVarification = False
                '            Exit Function
                '        Else
                '            MsgInformation("Line Capacity (" & mCapacity & ") is less than Plan Qty, So cann't be Save.")
                '            MainClass.SetFocusToCell(SprdMain, ii, ColDPlanQtyA)
                '            FieldsVarification = False
                '            Exit Function
                '        End If
                '    End If

                '    If mTodayPlanningQty > mMonthPlan - mProductionQty Then
                '        If PubSuperUser = "S" Then
                '            mSqlStr = "Product Code : " & mProductCode
                '            If Trim(mInhouseCode) <> Trim(mProductCode) Then
                '                mSqlStr = mSqlStr & vbCrLf & "Inhouse Code : " & mInhouseCode
                '            End If
                '            mSqlStr = mSqlStr & vbCrLf & "Month Production Plan : " & mMonthPlan
                '            mSqlStr = mSqlStr & vbCrLf & "MTD Production : " & mProductionQty
                '            mSqlStr = mSqlStr & vbCrLf & "Today Production Plan : " & mDayPlan
                '            mSqlStr = mSqlStr & vbCrLf & "Max Today Plan : " & mMonthPlan - mProductionQty
                '            mSqlStr = mSqlStr & vbCrLf & "Day Plan can't be greater than Total Month Plan. Want to process.."

                '            If MsgQuestion(mSqlStr) = CStr(MsgBoxResult.No) Then
                '                MainClass.SetFocusToCell(SprdMain, ii, ColDPlanQtyA)
                '                FieldsVarification = False
                '                Exit Function
                '            End If
                '        Else
                '            mSqlStr = "Product Code : " & mProductCode
                '            If Trim(mInhouseCode) <> Trim(mProductCode) Then
                '                mSqlStr = mSqlStr & vbCrLf & "Inhouse Code : " & mInhouseCode
                '            End If

                '            mSqlStr = mSqlStr & vbCrLf & "Customer Schedule : " & mMonthPlan
                '            mSqlStr = mSqlStr & vbCrLf & "MTD Production : " & mProductionQty
                '            mSqlStr = mSqlStr & vbCrLf & "Today Production Plan : " & mDayPlan
                '            mSqlStr = mSqlStr & vbCrLf & "Max Today Plan : " & mMonthPlan - mProductionQty


                '            mSqlStr = mSqlStr & vbCrLf & "Day Plan can't be greater than Total Month Plan. Please Correct it."

                '            MsgInformation(mSqlStr)
                '            MainClass.SetFocusToCell(SprdMain, ii, ColDPlanQtyA)
                '            FieldsVarification = False
                '            Exit Function
                '        End If
                '    End If
                '    If mTodayPlanningQty > mMonthPlan - mStockQty + mMinStock And mStockQty > 0 Then
                '        mSqlStr = "Product Code : " & mProductCode
                '        If Trim(mInhouseCode) <> Trim(mProductCode) Then
                '            mSqlStr = mSqlStr & vbCrLf & "Inhouse Code : " & mInhouseCode
                '        End If

                '        mSqlStr = mSqlStr & vbCrLf & "Customer Schedule : " & mMonthPlan
                '        mSqlStr = mSqlStr & vbCrLf & "Month Opening Stock : " & mStockQty
                '        mSqlStr = mSqlStr & vbCrLf & "Min Stock : " & mStockQty
                '        mSqlStr = mSqlStr & vbCrLf & "Max Today Plan : " & IIf(mMonthPlan - mStockQty + mStockQty > mTodayPlanningQty, mTodayPlanningQty, mMonthPlan - mStockQty + mStockQty)

                '        If PubSuperUser = "S" Then
                '            mSqlStr = mSqlStr & vbCrLf & "Day Plan can't be greater than Max Today Plan. Want to process.."

                '            If MsgQuestion(mSqlStr) = CStr(MsgBoxResult.No) Then
                '                MainClass.SetFocusToCell(SprdMain, ii, ColDPlanQtyA)
                '                FieldsVarification = False
                '                Exit Function
                '            End If
                '        Else
                '            mSqlStr = mSqlStr & vbCrLf & "Day Plan can't be greater than Max Today Plan. Please Correct it."
                '            MsgInformation(mSqlStr)
                '            MainClass.SetFocusToCell(SprdMain, ii, ColDPlanQtyA)
                '            FieldsVarification = False
                '            Exit Function
                '        End If
                '    End If
                'End If


            Next
        End With
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume	
    End Function

    Private Function GetCapacity(ByRef pDeptCode As String, ByRef mInhouseCode As String) As Double

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        GetCapacity = 0


        SqlStr = "SELECT CAPACITY_DAY" & vbCrLf _
            & " FROM INV_ITEMWISE_CAPACITY_HDR IH, INV_ITEMWISE_CAPACITY_DET ID " & vbCrLf _
            & " WHERE IH.MKEY = ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mInhouseCode) & "'" & vbCrLf _
            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf _
            & " AND IH.WEF = (" & vbCrLf _
            & " SELECT MAX(WEF) AS WEF FROM INV_ITEMWISE_CAPACITY_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mInhouseCode) & "'" & vbCrLf _
            & " AND WEF <=TO_DATE('" & VB6.Format(txtPlanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetCapacity = IIf(IsDbNull(RsTemp.Fields("CAPACITY_DAY").Value), 0, RsTemp.Fields("CAPACITY_DAY").Value)
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume	
    End Function

    Private Function GetStockQty(ByRef pDeptCode As String, ByRef mProductCode As String, ByRef mInhouseCode As String) As Double

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTempBom As ADODB.Recordset
        Dim xProductCode As String
        Dim mAlterMainItem As String
        Dim pStdQty As Double
        Dim mMonthDate As String
        Dim xItemUOM As String
        Dim pStockQty As Double
        Dim xDeptCode As String
        Dim mProductSeqNo As Integer

        GetStockQty = 0

        mMonthDate = "01/" & VB6.Format(txtPlanDate.Text, "MM/YYYY")

        mMonthDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(mMonthDate)))
        '	
        '    SqlStr = " SELECT DISTINCT " & vbCrLf _	
        ''            & " TRN.PRODUCT_CODE, TRN.RM_CODE, (TRN.STD_QTY + GROSS_WT_SCRAP) AS STD_QTY , DEPT_CODE " & vbCrLf _	
        ''            & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _	
        ''            & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _	
        ''            & " AND RM_CODE='" & MainClass.AllowSingleQuote(mInhouseCode) & "'" & vbCrLf _	
        ''            & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE || ' ')=TRIM(RM_CODE) || COMPANY_CODE || ' '"	


        SqlStr = " SELECT DISTINCT " & vbCrLf _
            & " TRN.PRODUCT_CODE, TRN.RM_CODE, (TRN.STD_QTY + GROSS_WT_SCRAP) AS STD_QTY , DEPT_CODE " & vbCrLf _
            & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
            & " AND RM_CODE='" & MainClass.AllowSingleQuote(mInhouseCode) & "'" & vbCrLf _
            & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE || ' ')=TRIM(RM_CODE) || COMPANY_CODE || ' '"

        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf _
                & " SELECT DISTINCT " & vbCrLf _
                & " TRN.PRODUCT_CODE, '' AS RM_CODE, (TRN.STD_QTY + GROSS_WT_SCRAP) AS STD_QTY , 'STR' AS DEPT_CODE " & vbCrLf _
                & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _
                & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND RM_CODE='" & MainClass.AllowSingleQuote(mInhouseCode) & "'" & vbCrLf _
                & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE || ' ')=TRIM(RM_CODE) || COMPANY_CODE || ' '"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempBom, ADODB.LockTypeEnum.adLockReadOnly)

        pStdQty = 1
        If RsTempBom.EOF = False Then
            Do While RsTempBom.EOF = False
                xProductCode = IIf(IsDbNull(RsTempBom.Fields("PRODUCT_CODE").Value), "", RsTempBom.Fields("PRODUCT_CODE").Value)
                pStdQty = pStdQty * IIf(IsDbNull(RsTempBom.Fields("STD_QTY").Value), 0, RsTempBom.Fields("STD_QTY").Value) '04/01/2016	
                xDeptCode = IIf(IsDbNull(RsTempBom.Fields("DEPT_CODE").Value), "", RsTempBom.Fields("DEPT_CODE").Value)

                xItemUOM = ""

                If MainClass.ValidateWithMasterTable(xProductCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    xItemUOM = MasterNo
                End If

                If xDeptCode = "STR" Then
                    pStockQty = GetBalanceStockQty(xProductCode, mMonthDate, xItemUOM, "", "", "", ConWH, -1)
                Else
                    pStockQty = GetBalanceStockQty(xProductCode, mMonthDate, xItemUOM, xDeptCode, "", "", ConPH, -1)
                End If

                GetStockQty = GetStockQty + (pStockQty * pStdQty)

                '            mAlterMainItem = "(SELECT REF_ITEM_CODE FROM INV_ITEM_RELATIONSHIP_DET WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(xProductCode) & "')"	

                RsTempBom.MoveNext()
            Loop
        Else
            If Trim(mInhouseCode) = Trim(mProductCode) Then
                mProductSeqNo = GetProductSeqNo(mProductCode, Trim(pDeptCode), mMonthDate)
                xItemUOM = ""

                If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    xItemUOM = MasterNo
                End If

                pStockQty = GetBalanceStockQty(mProductCode, mMonthDate, xItemUOM, "", "", "", ConWH, -1)
                GetStockQty = GetStockQty + (pStockQty)

                SqlStr = "SELECT DEPT_CODE, SERIAL_NO" & vbCrLf & " FROM PRD_PRODSEQUENCE_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND SERIAL_NO>=" & Val(CStr(mProductSeqNo)) & "" & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) AS WEF FROM PRD_PRODSEQUENCE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND WEF <=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " ORDER BY SERIAL_NO"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    Do While RsTemp.EOF = False
                        xDeptCode = IIf(IsDbNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value)

                        pStockQty = GetBalanceStockQty(mProductCode, mMonthDate, xItemUOM, xDeptCode, "", "", ConPH, -1)

                        GetStockQty = GetStockQty + (pStockQty)

                        RsTemp.MoveNext()
                    Loop
                End If
            End If
        End If

        If Trim(mInhouseCode) <> Trim(mProductCode) Then

            xItemUOM = ""

            If MainClass.ValidateWithMasterTable(mInhouseCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                xItemUOM = MasterNo
            End If

            pStockQty = GetBalanceStockQty(mInhouseCode, mMonthDate, xItemUOM, "", "", "", "", -1)

            GetStockQty = GetStockQty + (pStockQty)

        End If


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume	
    End Function


    Private Function GetTodayPlanning(ByRef pDeptCode As String, ByRef pProductCode As String, ByRef pInhouseCode As String) As Double
        On Error GoTo err_Renamed
        Dim mDeptCode As String
        Dim mProductCode As String
        Dim mInhouseCode As String
        Dim ii As Integer
        Dim mDayPlan As Double

        GetTodayPlanning = 0


        With SprdMain
            For ii = 1 To .MaxRows
                .Row = ii

                If optPlanType(0).Checked = True Then
                    .Col = ColCode
                    mDeptCode = Trim(.Text)

                    mProductCode = Trim(txtCode.Text)
                Else
                    mDeptCode = Trim(txtCode.Text)

                    .Col = ColCode
                    mProductCode = Trim(.Text)

                End If

                .Col = ColInHouseCode
                mInhouseCode = Trim(.Text)

                .Col = ColDPlanQty
                mDayPlan = Val(.Text)

                If mDeptCode = pDeptCode And mInhouseCode = pInhouseCode Then
                    GetTodayPlanning = GetTodayPlanning + mDayPlan
                End If
            Next
        End With

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume	
    End Function
    Private Function GetProductionQty(ByRef pDeptCode As String, ByRef mInhouseCode As String) As Double

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFYEAR As Integer

        mFYEAR = GetCurrentFYNo(PubDBCn, (txtPlanDate.Text))

        GetProductionQty = 0
        SqlStr = "SELECT SUM(PROD_QTY) AS PROD_QTY" & vbCrLf _
            & " FROM PRD_PMEMODEPT_HDR IH, PRD_PMEMODEPT_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND IH.FYEAR=" & mFYEAR & "" & vbCrLf _
            & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF " & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mInhouseCode) & "'" & vbCrLf _
            & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "' " & vbCrLf _
            & " AND TO_CHAR(IH.PROD_DATE,'YYYYMM')='" & vb6.Format(txtPlanDate.Text, "YYYYMM") & "'"

        SqlStr = SqlStr & vbCrLf & " AND (GETFINALOPRNEW(IH.COMPANY_CODE, IH.DEPT_CODE, ID.ITEM_CODE,ID.OPR_CODE,IH.REF_DATE)='Y' OR ID.OPR_CODE IS NULL)"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetProductionQty = IIf(IsDbNull(RsTemp.Fields("PROD_QTY").Value), 0, RsTemp.Fields("PROD_QTY").Value)
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume	
    End Function

    Private Function GetMonthlyPlanQty(ByRef pDeptCode As String, ByRef mProductCode As String, ByRef mInhouseCode As String) As Double

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTempBom As ADODB.Recordset
        Dim xProductCode As String
        Dim mAlterMainItem As String
        Dim pStdQty As Double

        GetMonthlyPlanQty = 0


        SqlStr = "SELECT SUM(IPLAN_QTY) AS IPLAN_QTY" & vbCrLf _
                    & " FROM PRD_PRODPLAN_HDR IH, PRD_PRODPLAN_MONTH_DET ID" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND " & vbCrLf _
                    & " IH.AUTO_KEY_PRODPLAN=ID.AUTO_KEY_PRODPLAN " & vbCrLf _
                    & " AND ID.INHOUSE_CODE='" & MainClass.AllowSingleQuote(mInhouseCode) & "'" & vbCrLf _
                    & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf _
                    & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')='" & VB6.Format(txtPlanDate.Text, "YYYYMM") & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetMonthlyPlanQty = IIf(IsDBNull(RsTemp.Fields("IPLAN_QTY").Value), 0, RsTemp.Fields("IPLAN_QTY").Value)
        End If

        Exit Function

        '    SqlStr = "SELECT SUM(IPLAN_QTY) AS IPLAN_QTY" & vbCrLf _	
        ''            & " FROM PRD_PRODPLAN_HDR IH, PRD_PRODPLAN_MONTH_DET ID" & vbCrLf _	
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " AND " & vbCrLf _	
        ''            & " IH.AUTO_KEY_PRODPLAN=ID.AUTO_KEY_PRODPLAN " & vbCrLf _	
        ''            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
        ''            & " AND ID.INHOUSE_CODE='" & MainClass.AllowSingleQuote(mInhouseCode) & "'" & vbCrLf _	
        ''            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf _	
        ''            & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')='" & vb6.Format(txtPlanDate.Text, "YYYYMM") & "'"	
        '	


        If Trim(mProductCode) = Trim(mInhouseCode) Then GoTo NextLevel

        SqlStr = " SELECT DISTINCT " & vbCrLf _
            & " TRN.PRODUCT_CODE, TRN.RM_CODE, (TRN.STD_QTY + GROSS_WT_SCRAP) AS STD_QTY , DEPT_CODE " & vbCrLf _
            & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " START WITH  TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND RM_CODE='" & MainClass.AllowSingleQuote(mInhouseCode) & "'" & vbCrLf _
            & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE || ' ')=TRIM(RM_CODE) || COMPANY_CODE || ' '"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempBom, ADODB.LockTypeEnum.adLockReadOnly)

        pStdQty = 1
        If RsTempBom.EOF = False Then
            Do While RsTempBom.EOF = False
                xProductCode = IIf(IsDBNull(RsTempBom.Fields("PRODUCT_CODE").Value), "", RsTempBom.Fields("PRODUCT_CODE").Value)
                pStdQty = pStdQty * IIf(IsDBNull(RsTempBom.Fields("STD_QTY").Value), 0, RsTempBom.Fields("STD_QTY").Value) '04/01/2016	
                mAlterMainItem = "(SELECT REF_ITEM_CODE FROM INV_ITEM_RELATIONSHIP_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(xProductCode) & "')"

                SqlStr = "SELECT SUM(PLANNED_QTY) AS IPLAN_QTY" & vbCrLf _
                    & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV " & vbCrLf _
                    & " AND (ID.ITEM_CODE='" & xProductCode & "' OR ID.ITEM_CODE IN " & mAlterMainItem & ")" & vbCrLf _
                    & " AND TO_CHAR(ID.SERIAL_DATE,'YYYYMM')='" & VB6.Format(txtPlanDate.Text, "YYYYMM") & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    GetMonthlyPlanQty = GetMonthlyPlanQty + (IIf(IsDBNull(RsTemp.Fields("IPLAN_QTY").Value), 0, RsTemp.Fields("IPLAN_QTY").Value) * pStdQty)
                End If

                RsTempBom.MoveNext()
            Loop
        Else
NextLevel:
            mAlterMainItem = "(SELECT REF_ITEM_CODE FROM INV_ITEM_RELATIONSHIP_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "')"
            SqlStr = "SELECT SUM(PLANNED_QTY) AS IPLAN_QTY" & vbCrLf _
                & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV " & vbCrLf _
                & " AND (ID.ITEM_CODE='" & mProductCode & "' OR ID.ITEM_CODE IN " & mAlterMainItem & ")" & vbCrLf _
                & " AND TO_CHAR(ID.SERIAL_DATE,'YYYYMM')='" & VB6.Format(txtPlanDate.Text, "YYYYMM") & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                GetMonthlyPlanQty = IIf(IsDBNull(RsTemp.Fields("IPLAN_QTY").Value), 0, RsTemp.Fields("IPLAN_QTY").Value)
            End If
        End If

        '            AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustCode) & "'	

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume	
    End Function

    Private Sub frmProductionPlanDaily_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsProdPlanMain.Close()
        RsProdPlanMain = Nothing
        RsProdPlanMonDetail.Close()
        RsProdPlanMonDetail = Nothing
        'PvtDBCn.Close	
        'Set PvtDBCn = Nothing	
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub optPlanType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPlanType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optPlanType.GetIndex(eventSender)
            If optPlanType(0).Checked = True Then
                lblProductDept.Text = "Product Code"
                SprdMain.Row = 0
                SprdMain.Col = ColCode
                SprdMain.Text = "Dept Code"
            ElseIf optPlanType(1).Checked = True Then
                lblProductDept.Text = "Dept Code"
                SprdMain.Row = 0
                SprdMain.Col = ColCode
                SprdMain.Text = "Product Code"
            End If
            Call Clear1()
        End If
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        Call GridCalc(eventArgs.Row)
    End Sub


    Private Sub GridCalc(ByRef pRow As Integer)
        On Error GoTo ErrPart
        Dim i As Integer
        Dim pRowFrom As Integer
        Dim pRowTo As Integer

        Dim mShiftAQty As Double
        Dim mShiftBQty As Double
        Dim mShiftCQty As Double
        Dim mNetQty As Double

        If pRow > 0 Then
            pRowFrom = pRow
            pRowTo = pRow
        Else
            pRowFrom = 1
            pRowTo = SprdMain.MaxRows
        End If

        With SprdMain
            For i = pRowFrom To pRowTo
                .Row = i

                .Col = ColDPlanQtyA
                mShiftAQty = Val(.Text)

                .Col = ColDPlanQtyB
                mShiftBQty = Val(.Text)

                .Col = ColDPlanQtyC
                mShiftCQty = Val(.Text)

                .Col = ColDPlanQty
                mNetQty = mShiftAQty + mShiftBQty + mShiftCQty

                .Text = VB6.Format(mNetQty, "0.000")
            Next
        End With
        Exit Sub
ErrPart:
        '    Resume	
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        On Error GoTo ErrPart
        Dim xMkey As String = ""
        Dim SqlStr As String = ""
        Dim cntRow As Integer

        Clear1()
        FormatSprdMain(-1)

        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        xMkey = SprdView.Text

        SprdView.Col = 2
        SprdView.Row = SprdView.ActiveRow
        pProductCode = Trim(SprdView.Text)

        SprdView.Col = 5
        SprdView.Row = SprdView.ActiveRow
        pDeptCode = Trim(SprdView.Text)

        SprdView.Col = 4
        SprdView.Row = SprdView.ActiveRow
        txtPlanDate.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")

        SqlStr = "SELECT * FROM PRD_PRODPLAN_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND AUTO_KEY_PRODPLAN=" & xMkey & ""

        '    If optPlanType(1).Value = True Then	
        '        SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"	
        '    End If	

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdPlanMain, ADODB.LockTypeEnum.adLockReadOnly)
        cntRow = 1
        If RsProdPlanMain.EOF = False Then
            lblMKey.Text = IIf(IsDbNull(RsProdPlanMain.Fields("AUTO_KEY_PRODPLAN").Value), "", RsProdPlanMain.Fields("AUTO_KEY_PRODPLAN").Value)
            If ShowDetail1(cntRow, lblMkey.Text, (txtPlanDate.Text), pProductCode, pDeptCode) = False Then GoTo ErrPart
            FormatSprdMain(-1)
            MakeEnableDesableField((False))
        Else
            MsgBox("Monthly Schedule not made for these parameters.", MsgBoxStyle.Information)
            '        ShowRecord = False	
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsProdPlanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Call CmdView_Click(CmdView, New System.EventArgs())
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtPlanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPlanDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtPlanDate.Text = "" Then GoTo EventExitSub
        If Len(txtPlanDate.Text) = 8 Then
            txtPlanDate.Text = VB.Left(txtPlanDate.Text, 2) & "/" & Mid(txtPlanDate.Text, 3, 2) & "/" & Mid(txtPlanDate.Text, 5)
        End If
        If IsDate(txtPlanDate.Text) = False Then
            MsgBox("Not a valid Date")
            Cancel = True
        Else
            '        MakeEnableDesableField (False)	
            '        If FYChk(txtPlanDate.Text) = False Then	
            '            Cancel = True	
            '        Else	
            If ShowRecord() = False Then Exit Sub
            '        End If	
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.DoubleClick
        Call cmdSearchCode_Click(cmdSearchCode, New System.EventArgs())
    End Sub

    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCode_Click(cmdSearchCode, New System.EventArgs())
    End Sub

    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        If Trim(txtCode.Text) = "" Then GoTo EventExitSub

        If optPlanType(0).Checked = True Then
            SqlStr = " SELECT ITEM_SHORT_DESC  " & vbCrLf _
                & " FROM INV_ITEM_MST B " & vbCrLf _
                & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(txtCode.Text) & "' "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If Not RsTemp.EOF Then
                lblDescription.Text = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
                If ShowRecord() = False Then
                    Cancel = True
                End If
            Else
                MsgBox("Not a valid Supplier's Product Code")
                Cancel = True
            End If
        ElseIf optPlanType(1).Checked = True Then
            SqlStr = " SELECT DEPT_DESC  " & vbCrLf _
                & " FROM PAY_DEPT_MST " & vbCrLf _
                & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND DEPT_CODE = '" & MainClass.AllowSingleQuote(txtCode.Text) & "' "
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If Not RsTemp.EOF Then
                lblDescription.Text = IIf(IsDbNull(RsTemp.Fields("DEPT_DESC").Value), "", RsTemp.Fields("DEPT_DESC").Value)
                If ShowRecord() = False Then
                    Cancel = True
                End If
            Else
                MsgBox("Not a valid Department Code")
                Cancel = True
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function ShowDetail1(ByRef cntRow As Integer, ByRef xMkey As String, ByRef mDate As String, ByRef pProductCode As String, ByRef pDeptCode As String) As Boolean

        On Error GoTo ERR1
        'Dim I As Long	
        Dim mCode As String
        Dim SqlStr As String = ""
        Dim mInhouseCode As String
        Dim mCustCode As String
        Dim mFYEAR As Integer
        Dim mDeptCode As String

        mFYEAR = GetCurrentFYNo(PubDBCn, mDate)

        SqlStr = "SELECT ID.*, " & vbCrLf _
            & " INVMST.ITEM_SHORT_DESC AS INHOUSE_NAME, PMST.ITEM_SHORT_DESC AS PRODUCT_NAME, DEPT.DEPT_DESC " & vbCrLf _
            & " FROM PRD_PRODPLAN_MONTH_DET ID, INV_ITEM_MST INVMST, INV_ITEM_MST PMST, PAY_DEPT_MST DEPT " & vbCrLf _
            & " WHERE ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & mFYEAR & " "

        SqlStr = SqlStr & vbCrLf _
            & " AND ID.COMPANY_CODE=PMST.COMPANY_CODE AND ID.PRODUCT_CODE=PMST.ITEM_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.INHOUSE_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE=DEPT.COMPANY_CODE AND ID.DEPT_CODE=DEPT.DEPT_CODE"

        If optPlanType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_PRODPLAN=" & xMkey & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        If optPlanType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.DEPT_CODE "
        ElseIf optPlanType(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.INHOUSE_CODE,ID.PRODUCT_CODE "
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdPlanMonDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsProdPlanMonDetail
            If .EOF = True Then
                ShowDetail1 = True
                mShowDate = ""
                Exit Function
            End If

            mDeptCode = Trim(IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value))
            txtPlanDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SERIAL_DATE").Value), "", .Fields("SERIAL_DATE").Value), "DD/MM/YYYY")
            '        txtSupplierCode.Text = Trim(IIf(IsNull(!SUPP_CUST_CODE), "", !SUPP_CUST_CODE))	

            If optPlanType(0).Checked = True Then
                txtCode.Text = Trim(IIf(IsDBNull(.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value))
                If MainClass.ValidateWithMasterTable(txtCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    lblDescription.Text = MasterNo
                End If
            Else
                txtCode.Text = Trim(IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value))
                If MainClass.ValidateWithMasterTable(txtCode.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    lblDescription.Text = MasterNo
                End If
            End If
            '        If MainClass.ValidateWithMasterTable(txtSupplierCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value) = True Then	
            '            lblSupplierCode.text = MasterNo	
            '        End If	



            mShowDate = VB6.Format(txtPlanDate.Text, "DD/MM/YYYY")

            '        I = 1	
            Do While Not .EOF
                SprdMain.MaxRows = cntRow
                SprdMain.Row = cntRow

                SprdMain.Col = ColMKEY
                SprdMain.Text = CStr(IIf(IsDBNull(.Fields("AUTO_KEY_PRODPLAN").Value), "", .Fields("AUTO_KEY_PRODPLAN").Value))

                SprdMain.Col = ColCode
                If optPlanType(0).Checked = True Then
                    SprdMain.Text = IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                ElseIf optPlanType(1).Checked = True Then
                    SprdMain.Text = IIf(IsDBNull(.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value)
                End If
                mCode = Trim(SprdMain.Text)

                SprdMain.Col = ColDescription
                If optPlanType(0).Checked = True Then
                    SprdMain.Text = Trim(IIf(IsDBNull(.Fields("DEPT_DESC").Value), "", .Fields("DEPT_DESC").Value))
                    '                If MainClass.ValidateWithMasterTable(mCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value) = True Then	
                    '                    SprdMain.Text = MasterNo	
                    '                End If	
                ElseIf optPlanType(1).Checked = True Then
                    SprdMain.Text = Trim(IIf(IsDBNull(.Fields("PRODUCT_NAME").Value), "", .Fields("PRODUCT_NAME").Value))
                    '                If MainClass.ValidateWithMasterTable(mCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value) = True Then	
                    '                    SprdMain.Text = MasterNo	
                    '                End If	
                End If


                SprdMain.Col = ColInHouseCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("INHOUSE_CODE").Value), "", .Fields("INHOUSE_CODE").Value))
                mInhouseCode = Trim(IIf(IsDBNull(.Fields("INHOUSE_CODE").Value), "", .Fields("INHOUSE_CODE").Value))

                SprdMain.Col = ColInHouseDesc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("INHOUSE_NAME").Value), "", .Fields("INHOUSE_NAME").Value))
                '            If MainClass.ValidateWithMasterTable(mInhouseCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value) = True Then	
                '                SprdMain.Text = MasterNo	
                '            Else	
                '                SprdMain.Text = ""	
                '            End If	


                SprdMain.Col = ColCapacity
                SprdMain.Text = GetCapacity(mDeptCode, mInhouseCode)

                SprdMain.Col = ColIPlanQty
                SprdMain.Text = IIf(IsDBNull(.Fields("IPLAN_QTY").Value), "", CStr(.Fields("IPLAN_QTY").Value))

                SprdMain.Col = ColDPlanQtyA
                SprdMain.Text = IIf(IsDBNull(.Fields("DPLAN_QTY_A").Value), "", CStr(.Fields("DPLAN_QTY_A").Value))

                SprdMain.Col = ColDPlanQtyB
                SprdMain.Text = IIf(IsDBNull(.Fields("DPLAN_QTY_B").Value), "", CStr(.Fields("DPLAN_QTY_B").Value))

                SprdMain.Col = ColDPlanQtyC
                SprdMain.Text = IIf(IsDBNull(.Fields("DPLAN_QTY_C").Value), "", CStr(.Fields("DPLAN_QTY_C").Value))

                SprdMain.Col = ColDPlanQty
                SprdMain.Text = IIf(IsDBNull(.Fields("DPLAN_QTY").Value), "", CStr(.Fields("DPLAN_QTY").Value))

                .MoveNext()
                cntRow = cntRow + 1
            Loop
        End With

        ShowDetail1 = True
        '    CmdSave.Enabled = True	

        Exit Function
ERR1:
        ShowDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function

    Private Function ShowRecord() As Boolean

        On Error GoTo ERR1
        Dim mProdCode As String
        Dim mPlanDate As String
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim xMkey As String = ""
        Dim cntRow As Integer
        Dim mFYEAR As Integer

        ShowRecord = True
        If SprdMain.Enabled = True Then SprdMain.Focus()
        '    If Trim(txtSupplierCode.Text) = "" Then Exit Function	

        mProdCode = Trim(txtCode.Text)
        If Trim(txtPlanDate.Text) = "" Then Exit Function
        mPlanDate = Trim(txtPlanDate.Text)
        mFYEAR = GetCurrentFYNo(PubDBCn, mPlanDate)


        xMkey = ""

        If MODIFYMode = True And RsProdPlanMain.BOF = False Then xMkey = RsProdPlanMain.Fields("AUTO_KEY_PRODPLAN").Value

        If xMkey <> "" Then

            SqlStr = "SELECT * FROM PRD_PRODPLAN_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & mFYEAR & " " & vbCrLf _
                & " AND AUTO_KEY_PRODPLAN='" & xMkey & "'"

            '        If optPlanType(0).Value = True Then	
            '            SqlStr = SqlStr & vbCrLf & " ORDER BY DEPT_CODE "	
            '        ElseIf optPlanType(1).Value = True Then	
            '            SqlStr = SqlStr & vbCrLf & " ORDER BY PRODUCT_CODE "	
            '        End If	

        Else

            SqlStr = "SELECT * FROM PRD_PRODPLAN_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & mFYEAR & " " & vbCrLf _
                & " AND TO_CHAR(SCHLD_DATE,'YYYYMM')='" & VB6.Format(mPlanDate, "YYYYMM") & "'"

            If optPlanType(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProdCode) & "'" ''& vbCrLf |                    & " ORDER BY DEPT_CODE "	
                '        ElseIf optPlanType(1).Value = True Then	
                '            SqlStr = SqlStr & vbCrLf _	
                ''                    & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mProdCode) & "'" ''& vbCrLf _	
                '                    & " ORDER BY PRODUCT_CODE "	
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdPlanMain, ADODB.LockTypeEnum.adLockReadOnly)
        cntRow = 1
        If RsProdPlanMain.EOF = False Then
            Do While RsProdPlanMain.EOF = False
                lblMKey.Text = IIf(IsDbNull(RsProdPlanMain.Fields("AUTO_KEY_PRODPLAN").Value), "", RsProdPlanMain.Fields("AUTO_KEY_PRODPLAN").Value)
                If optPlanType(0).Checked = True Then
                    pProductCode = Trim(txtCode.Text)
                    pDeptCode = ""
                Else
                    pProductCode = ""
                    pDeptCode = Trim(txtCode.Text)
                End If
                If ShowDetail1(cntRow, lblMkey.Text, mPlanDate, pProductCode, pDeptCode) = False Then GoTo ERR1

                If optPlanType(0).Checked = True Then
                    RsProdPlanMain.MoveNext()
                Else
                    Exit Do
                End If
            Loop
            RsProdPlanMain.MoveFirst()
        Else
            MsgBox("Monthly Schedule not made for these parameters.", MsgBoxStyle.Information)
            ShowRecord = False
        End If

        Call GridCalc(-1)
        ADDMode = False
        MODIFYMode = False
        FormatSprdMain(-1)
        MakeEnableDesableField((False))
        MainClass.ButtonStatus(Me, XRIGHT, RsProdPlanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Function
ERR1:
        '    Resume	
        MsgInformation(Err.Description)
    End Function
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = ""
        'SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_PRODPLAN AS PLAN_NUMBER, SUPP_CUST_CODE, PRODUCT_CODE, INHOUSE_CODE, TO_CHAR(SCHLD_DATE,'DD-MM-YYYY') AS SCHLD_DATE, " & vbCrLf & " TO_CHAR(SERIAL_DATE,'DD-MM-YYYY') AS SERIAL_DATE, DEPT_CODE, IPLAN_QTY AS MONTHLY_PLAN, DPLAN_QTY  AS DAILY_PLAN" & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_PRODPLAN"

        SqlStr = " SELECT DISTINCT " & vbCrLf _
            & " AUTO_KEY_PRODPLAN AS PLAN_NUMBER, PRODUCT_CODE, TO_CHAR(SCHLD_DATE,'DD-MM-YYYY') AS SCHLD_DATE, " & vbCrLf _
            & " TO_CHAR(SERIAL_DATE,'DD-MM-YYYY') AS SERIAL_DATE, DEPT_CODE, IPLAN_QTY AS MONTHLY_PLAN, DPLAN_QTY  AS DAILY_PLAN" & vbCrLf _
            & " FROM PRD_PRODPLAN_MONTH_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " ORDER BY AUTO_KEY_PRODPLAN"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))

        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 500 * 4)
            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 5)
            .set_ColWidth(5, 500 * 3)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        '    txtSupplierCode.Enabled = mMode	
        '    cmdsearchSupp.Enabled = mMode	
        txtCode.Enabled = mMode
        cmdSearchCode.Enabled = mMode
        txtPlanDate.Enabled = mMode
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub ReportOnProdPlan(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mRPTName As String
        Dim mTitle As String
        Dim mSubTitle As String

        SqlStr = ""

        If InsertIntoTemp_Table() = False Then GoTo ERR1
        SqlStr = ""

        SqlStr = "SELECT * FROM TEMP_PRD_REQ_PRODPLAN_DET " & vbCrLf _
            & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' ORDER BY USERID, INHOUSE_CODE" 'RM_CODE	

        mRPTName = "DailyProdPlanReq.rpt"
        mTitle = "Requisition Slip as per Production Plan"

        mTitle = mTitle & " - as on " & VB6.Format(txtPlanDate.Text, "DD/MM/YYYY")

        If optPlanType(0).Checked = True Then
            mSubTitle = "(Product : " & txtCode.Text & ")"
        Else
            mSubTitle = "(Dept : " & txtCode.Text & ")"
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRPTName)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        '    Resume	
    End Sub

    Private Function InsertIntoTemp_Table() As Boolean

        On Error GoTo InsertErr
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mCustCode As String
        Dim mCustName As String
        Dim mCode As String
        Dim mDescription As String
        Dim mInhouseCode As String
        Dim mInHouseDesc As String
        Dim mDPlanQtyA As Double
        Dim mDPlanQtyB As Double
        Dim mDPlanQtyC As Double
        Dim mDPlanQty As Double
        Dim mDeptCode As String
        Dim mFYEAR As Integer

        mFYEAR = GetCurrentFYNo(PubDBCn, (txtPlanDate.Text))

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PRD_REQ_PRODPLAN_DET " & vbCrLf _
            & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' "

        PubDBCn.Execute(SqlStr)

        SqlStr = "SELECT INHOUSE_CODE, DEPT_CODE, " & vbCrLf _
            & " SUM(DPLAN_QTY_A) AS DPLAN_QTY_A," & vbCrLf _
            & " SUM(DPLAN_QTY_B) AS DPLAN_QTY_B," & vbCrLf _
            & " SUM(DPLAN_QTY_C) AS DPLAN_QTY_C," & vbCrLf _
            & " SUM(DPLAN_QTY) AS DPLAN_QTY" & vbCrLf _
            & " FROM PRD_PRODPLAN_MONTH_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & mFYEAR & " "


        If optPlanType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_PRODPLAN='" & lblMKey.Text & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(txtPlanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY INHOUSE_CODE, DEPT_CODE "
        SqlStr = SqlStr & vbCrLf & " ORDER BY INHOUSE_CODE, DEPT_CODE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        Do While RsTemp.EOF = False
            mInhouseCode = IIf(IsDbNull(RsTemp.Fields("INHOUSE_CODE").Value), "", RsTemp.Fields("INHOUSE_CODE").Value)
            mDeptCode = IIf(IsDbNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value)
            mDPlanQtyA = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("DPLAN_QTY_A").Value), 0, RsTemp.Fields("DPLAN_QTY_A").Value), "0.00"))
            mDPlanQtyB = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("DPLAN_QTY_B").Value), 0, RsTemp.Fields("DPLAN_QTY_B").Value), "0.00"))
            mDPlanQtyC = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("DPLAN_QTY_C").Value), 0, RsTemp.Fields("DPLAN_QTY_C").Value), "0.00"))
            mDPlanQty = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("DPLAN_QTY").Value), 0, RsTemp.Fields("DPLAN_QTY").Value), "0.00"))

            If mInhouseCode <> "" And mDPlanQty > 0 Then
                If UpdateTempDetail(mInhouseCode, mDeptCode, mDPlanQtyA, mDPlanQtyB, mDPlanQtyC, mDPlanQty, (txtPlanDate.Text)) = False Then GoTo InsertErr
            End If

            RsTemp.MoveNext()
        Loop
        '    With SprdMain	
        '        For I = 1 To .MaxRows	
        '            .Row = I	
        '	
        '            .Col = ColCustCode	
        '            mCustCode = MainClass.AllowSingleQuote(.Text)	
        '	
        '            .Col = ColCustName	
        '            mCustName = MainClass.AllowSingleQuote(.Text)	
        '	
        '            .Col = ColCode	
        '            mCode = MainClass.AllowSingleQuote(.Text)	
        '	
        '            .Col = ColDescription	
        '            mDescription = MainClass.AllowSingleQuote(.Text)	
        '	
        '            .Col = ColInHouseCode	
        '            mInhouseCode = MainClass.AllowSingleQuote(.Text)	
        '	
        '            .Col = ColInHouseDesc	
        '            mInHouseDesc = MainClass.AllowSingleQuote(.Text)	
        '	
        '            .Col = ColDPlanQty	
        '            mDPlanQty = Val(.Text)	
        '	
        '            If optPlanType(0).Value = True Then	
        '                mDeptCode = mCode	
        '            ElseIf optPlanType(1).Value = True Then	
        '                mDeptCode = MainClass.AllowSingleQuote(txtCode.Text)	
        '            End If	
        '	
        '            SqlStr = ""	
        '            If mInhouseCode <> "" And mDPlanQty > 0 Then	
        '                If UpdateTempDetail(mCustCode, mInhouseCode, mDeptCode, mDPlanQty, txtPlanDate.Text) = False Then GoTo InsertErr	
        '            End If	
        '        Next	
        '    End With	
        PubDBCn.CommitTrans()
        InsertIntoTemp_Table = True
        Exit Function
InsertErr:
        'Resume	
        PubDBCn.RollbackTrans()
        InsertIntoTemp_Table = False
        MsgBox(Err.Description)
    End Function
    Private Function UpdateTempDetail(ByRef mInhouseCode As String, ByRef mDeptCode As String, ByRef pPlanningQtyA As Double, ByRef pPlanningQtyB As Double, ByRef pPlanningQtyC As Double, ByRef pPlanningQty As Double, ByRef pPlanDate As String) As Boolean


        On Error GoTo UpdateDetailERR
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mSqlStr As String
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mMainItemCode As String

        mMainItemCode = GetMainItemCode(Trim(mInhouseCode))

        SqlStr = ""
        SqlStr = " SELECT " & vbCrLf _
            & " IH.PRODUCT_CODE, " & vbCrLf _
            & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf _
            & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf _
            & " ID.STD_QTY, ID.GROSS_WT_SCRAP, " & vbCrLf _
            & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MINIMUM_QTY, MAXIMUM_QTY "

        SqlStr = SqlStr & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "' AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.PRODUCT_CODE, ID.SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                Call UpdateItemInTemp(RsShow, mInhouseCode, mInhouseCode, pPlanningQtyA, pPlanningQtyB, pPlanningQtyC, pPlanningQty, mDeptCode, pPlanDate)
                RsShow.MoveNext()
            Loop
        End If

        UpdateTempDetail = True
        Exit Function
UpdateDetailERR:
        UpdateTempDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function
    Private Sub UpdateItemInTemp(ByRef pRs As ADODB.Recordset, ByRef pProductCode As String, ByRef pParentCode As String, ByRef mProductPlanQtyA As Double, ByRef mProductPlanQtyB As Double, ByRef mProductPlanQtyC As Double, ByRef mProductPlanQty As Double, ByRef xDeptCode As String, ByRef pPlanDate As String)


        On Error GoTo FillGERR
        Dim mRMCode As String
        Dim mItemUOM As String
        Dim mQtyA As Double
        Dim mQtyB As Double
        Dim mQtyC As Double
        Dim mQty As Double
        Dim mDeptCode As String
        Dim mWIPStock As Double
        Dim mProd_Type As Boolean
        Dim xAutoIssue As Boolean

        Dim mCommonDivision As Double
        Dim mStockQty As Double
        Dim xAlterItemCode As String
        Dim pDemandQty As Double
        Dim mProductName As String
        Dim mInHouseName As String
        Dim mRMName As String
        Dim SqlStr As String = ""

        mDeptCode = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)
        mRMCode = Trim(IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
        mItemUOM = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
        xAutoIssue = CheckAutoIssue(pPlanDate, mRMCode)

        mProductName = ""
        mInHouseName = ""
        mRMName = ""

        If MainClass.ValidateWithMasterTable(pParentCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            mProductName = Trim(MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(pProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            mInHouseName = Trim(MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            mRMName = Trim(MasterNo)
        End If



        If mDeptCode = Trim(xDeptCode) Then
            mProd_Type = IsProductionItem(mRMCode)

            mQtyA = CDbl(VB6.Format(mProductPlanQtyA * Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value)), "0.00"))
            mQtyB = CDbl(VB6.Format(mProductPlanQtyB * Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value)), "0.00"))
            mQtyC = CDbl(VB6.Format(mProductPlanQtyC * Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value)), "0.00"))
            mQty = CDbl(VB6.Format(mProductPlanQty * Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value)), "0.00"))
            '            mStockQty = GetBalanceStockQty(mRMCode, pPlanDate, mItemUOM, "", "ST", "", ConWH, -1) - pDemandQty	

            '            If mQty > mStockQty Then	
            '                If UpdateAlterItemReq(pReqNo, pRow, mRMCode, mItemUOM, mQty, mStockQty, pPlanDate) = False Then GoTo FillGERR	
            '            Else	

            SqlStr = " INSERT INTO TEMP_PRD_REQ_PRODPLAN_DET ( " & vbCrLf _
                & " UserID, COMPANY_CODE, " & vbCrLf _
                & " PRODUCT_CODE, PRODUCT_NAME, DEPT_CODE," & vbCrLf _
                & " SERIAL_DATE, INHOUSE_CODE, INHOUSE_NAME, PROD_PLAN_QTY," & vbCrLf _
                & " RM_CODE, RM_NAME, RM_UOM, RM_DEMAND_QTY, " & vbCrLf _
                & " PROD_PLAN_QTY_A, RM_DEMAND_QTY_A, PROD_PLAN_QTY_B, RM_DEMAND_QTY_B, PROD_PLAN_QTY_C, RM_DEMAND_QTY_C) "

            SqlStr = SqlStr & vbCrLf _
                & " VALUES ('" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(pParentCode) & "', '" & MainClass.AllowSingleQuote(mProductName) & "', '" & MainClass.AllowSingleQuote(mDeptCode) & "'," & vbCrLf _
                & "  TO_DATE('" & VB6.Format(pPlanDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(pProductCode) & "', '" & MainClass.AllowSingleQuote(mInHouseName) & "', " & mProductPlanQty & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mRMCode) & "', '" & MainClass.AllowSingleQuote(mRMName) & "', '" & MainClass.AllowSingleQuote(mItemUOM) & "'," & mQty & ", " & vbCrLf _
                & " " & mProductPlanQtyA & ", " & mQtyA & ", " & mProductPlanQtyB & ", " & mQtyB & ", " & mProductPlanQtyC & ", " & mQtyC & "" & vbCrLf _
                & " ) "


            PubDBCn.Execute(SqlStr)
        End If

NextRecd:
        Exit Sub
FillGERR:
        '    Resume	
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProdPlan(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProdPlan(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtPlanDate_TextChanged(sender As Object, e As EventArgs) Handles txtPlanDate.TextChanged

    End Sub
End Class
