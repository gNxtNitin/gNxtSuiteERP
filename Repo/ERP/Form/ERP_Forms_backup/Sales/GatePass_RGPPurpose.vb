Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGatePassPurpose
    Inherits System.Windows.Forms.Form
    Dim RsReqMain As ADODB.Recordset ''Recordset
    Dim RsReqDetail As ADODB.Recordset ''Recordset
    ''Private PvtDBCn As ADODB.Connection

    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim xMyMenu As String

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUOM As Short = 3
    Private Const ColQty As Short = 4
    Private Const ColJobOrderNo As Short = 5
    Dim mDeptCode As String

    Private Sub FillCboStatus()

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

        cboPurpose.Items.Clear()
        cboPurpose.Items.Add("C : Repair / Refill / Work Order")
        cboPurpose.Items.Add("D : Tool Trial")
        cboPurpose.Items.Add("E : Preparation of Tool/Die/Jigs/Fixture")
        cboPurpose.Items.Add("F : Testing / Trial")
        cboPurpose.Items.Add("G : Trolley / Bins")
        cboPurpose.Items.Add("H : FOC - Under Warranty / Re-Repair")
        cboPurpose.Items.Add("I : Fitting into any M/c coming to the company")
        cboPurpose.SelectedIndex = -1


        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub


    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1 = True Then
            txtGatepassno_Validating(txtGatepassno, New System.ComponentModel.CancelEventArgs(False))
            cmdSave.Enabled = False
        Else
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = " SELECT A.AUTO_KEY_PASSNO,A.GATEPASS_DATE,A.SUPP_CUST_CODE,B.SUPP_CUST_NAME " & vbCrLf & " From INV_GATEPASS_HDR A,FIN_SUPP_CUST_MST B WHERE " & vbCrLf & " a.SUPP_CUST_CODE = b.SUPP_CUST_CODE AND " & vbCrLf & " A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " Order by a.AUTO_KEY_PASSNO "

        If MainClass.SearchGridMasterBySQL2((txtGatepassno.Text), SqlStr) = True Then
            txtGatepassno.Text = AcName
            txtGatepassno_Validating(txtGatepassno, New System.ComponentModel.CancelEventArgs(False))
            If txtGatepassno.Enabled = True Then txtGatepassno.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmGatePassPurpose_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim mReqnum As String = ""
        Dim SqlStr As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()



        SqlStr = ""
        SqlStr = "UPDATE INV_GATEPASS_HDR SET " & vbCrLf & " PURPOSE='" & VB.Left(cboPurpose.Text, 1) & "'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_PASSNO =" & Val(txtGatepassno.Text) & ""

        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        SqlStr = " Update INV_RGP_SLIP_HDR SET " & vbCrLf & " PURPOSE='" & VB.Left(cboPurpose.Text, 1) & "'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_RGPSLIP =" & Val(txtRgpreqno.Text) & ""

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1() = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsReqMain.Requery() ''.Refresh
        RsReqDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function

        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

        '    Resume
    End Function
    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mSubRowNo As Integer
        Dim mItemCode As String
        Dim mJobOrderNo As Double

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColJobOrderNo
                mJobOrderNo = Val(.Text)

                SqlStr = ""

                SqlStr = " UPDATE INV_GATEPASS_DET SET AUTO_KEY_WO=" & mJobOrderNo & "" & vbCrLf & " WHERE AUTO_KEY_PASSNO=" & Val(txtGatepassno.Text) & "" & vbCrLf & " AND SERIAL_NO=" & I & " " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE INV_RGP_SLIP_DET SET AUTO_KEY_WO=" & mJobOrderNo & "" & vbCrLf & " WHERE AUTO_KEY_RGPSLIP=" & Val(txtRgpreqno.Text) & "" & vbCrLf & " AND SERIAL_NO=" & I & " " & vbCrLf & " AND FROM_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

                PubDBCn.Execute(SqlStr)

            Next
        End With

        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim mPurpose As String
        Dim CntRow As Long
        Dim xPoNo As Double
        Dim mDivisionCode As Double

        FieldsVarification = True

        If txtGatepassno.Text = "" Then
            MsgInformation("Gate Pass No. Cann't Blank")
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        mPurpose = Mid(cboPurpose.Text, 1, 1)

        If mPurpose = "C" Then
            With SprdMain
                For CntRow = 1 To .MaxRows - 1
                    .Row = CntRow
                    .Col = ColJobOrderNo
                    xPoNo = Val(.Text)
                    If xPoNo > 0 Then
                        If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND PUR_TYPE= 'W' AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppcode.Text) & "' AND PO_STATUS='Y' AND PO_CLOSED='N'") = False Then
                            MsgInformation("Please select Vaild WO No for Such Supplier.")
                            MainClass.SetFocusToCell(SprdMain, CntRow, ColJobOrderNo)
                            FieldsVarification = False : Exit Function
                        End If
                    Else
                        MsgInformation("WO is Blank.")
                        MainClass.SetFocusToCell(SprdMain, CntRow, ColJobOrderNo)
                        FieldsVarification = False : Exit Function
                    End If
                Next
            End With
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Public Sub frmGatePassPurpose_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = ""
        SqlStr = "Select * from INV_GATEPASS_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_GATEPASS_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call SetTextLengths()

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.5)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 10)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 35)

            .Col = ColUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_UOM", "INV_GATEPASS_DET", PubDBCn)
            .set_ColWidth(ColUOM, 4)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 9)

            .Col = ColJobOrderNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("AUTO_KEY_WO").DefinedSize ''
            .set_ColWidth(ColJobOrderNo, 12)


        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColQty)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsReqDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        With RsReqMain
            txtGatepassno.Maxlength = .Fields("GATEPASS_NO").Precision
            txtGatePassDate.Maxlength = 10
            txtSuppcode.Maxlength = .Fields("SUPP_CUST_CODE").DefinedSize
            txtRgpreqno.Maxlength = .Fields("REQ_NO").Precision
            txtRgpreqdate.Maxlength = 10

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mF4No As Double
        Dim mIsPaintF4 As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mPurpose As String

        With RsReqMain
            If Not .EOF Then
                txtGatepassno.Text = IIf(IsDbNull(.Fields("AUTO_KEY_PASSNO").Value), 0, .Fields("AUTO_KEY_PASSNO").Value)

                txtGatePassDate.Text = VB6.Format(IIf(IsDbNull(.Fields("GATEPASS_DATE").Value), "", .Fields("GATEPASS_DATE").Value), "DD/MM/YYYY")
                txtRgpreqno.Text = IIf(IsDbNull(.Fields("REQ_NO").Value), 0, .Fields("REQ_NO").Value)
                txtRgpreqdate.Text = VB6.Format(IIf(IsDbNull(.Fields("REQ_DATE").Value), "", .Fields("REQ_DATE").Value), "DD/MM/YYYY")

                txtSuppcode.Text = Trim(IIf(IsDbNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value))

                If MainClass.ValidateWithMasterTable((txtSuppcode.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSuppName.Text = MasterNo
                End If

                mPurpose = IIf(IsDBNull(.Fields("PURPOSE").Value), "", .Fields("PURPOSE").Value)

                If mPurpose = "C" Then
                    cboPurpose.SelectedIndex = 0
                ElseIf mPurpose = "D" Then
                    cboPurpose.SelectedIndex = 1
                ElseIf mPurpose = "E" Then
                    cboPurpose.SelectedIndex = 2
                ElseIf mPurpose = "F" Then
                    cboPurpose.SelectedIndex = 3
                ElseIf mPurpose = "G" Then
                    cboPurpose.SelectedIndex = 4
                ElseIf mPurpose = "H" Then
                    cboPurpose.SelectedIndex = 5
                ElseIf mPurpose = "I" Then
                    cboPurpose.SelectedIndex = 6
                Else
                    cboPurpose.SelectedIndex = 7
                End If

                If MainClass.ValidateWithMasterTable(Val(txtRgpreqno.Text), "AUTO_KEY_RGPSLIP", "DIV_CODE", "INV_RGP_SLIP_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionCode = Val(MasterNo)
                    If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDivisionDesc = Trim(MasterNo)
                        cboDivision.Text = mDivisionDesc
                    End If
                End If

                Call ShowDetail1(.Fields("AUTO_KEY_PASSNO").Value)

            End If
        End With

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowDetail1(ByVal pReqNum As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mQty As String


        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_GATEPASS_DET  " & vbCrLf & " Where AUTO_KEY_PASSNO = " & Val(CStr(pReqNum)) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsReqDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUOM
                mItemUOM = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                SprdMain.Text = mItemUOM

                SprdMain.Col = ColQty
                mQty = IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)
                SprdMain.Text = mQty

                SprdMain.Col = ColJobOrderNo
                SprdMain.Text = CStr(IIf(IsDBNull(.Fields("AUTO_KEY_WO").Value), "", .Fields("AUTO_KEY_WO").Value))


                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub Clear1()



        txtGatepassno.Text = ""
        txtGatePassDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        cboPurpose.SelectedIndex = -1

        txtRgpreqno.Text = ""
        txtSuppcode.Text = ""
        txtRgpreqdate.Text = ""
        txtSuppName.Text = ""
        MainClass.ClearGrid(SprdMain)
        cboPurpose.Enabled = True
        cmdSave.Enabled = True
        Call FormatSprdMain(-1)
    End Sub
    Private Sub frmGatePassPurpose_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmGatePassPurpose_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub
    Public Sub frmGatePassPurpose_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(6165)
        ''Me.Width = VB6.TwipsToPixelsX(9270)
        FillCboStatus()
        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        End With
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        'Dim xIName As String
        Dim SqlStr As String = ""

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String
        Dim xSuppCode As String
        Dim mDivisionCode As Double
        Dim xPoNo As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColJobOrderNo Then
            With SprdMain
                eventArgs.Row = .ActiveRow
                eventArgs.Col = ColItemCode
                mItemCode = Trim(.Text)

                eventArgs.Col = ColJobOrderNo
                xPoNo = Trim(SprdMain.Text)


                SqlStr = " SELECT DISTINCT IH.AUTO_KEY_PO  As AUTO_KEY_PO , " & vbCrLf & " IH.PUR_ORD_DATE, ID.PO_WEF_DATE, WO_DESCRIPTION " & vbCrLf & " FROM PUR_PURCHASE_HDR IH,PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PUR_TYPE='W'"

                If Trim(txtSuppName.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtSuppName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        xSuppCode = MasterNo
                        SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & xSuppCode & "'"
                    End If
                End If

                SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""


                If IsDate(txtGatePassDate.Text) Then
                    SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtGatePassDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                If PubGSTApplicable = True Then
                    SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                SqlStr = SqlStr & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N' AND ID.PO_ITEM_STATUS='N'"

                If Val(xPoNo) > 0 Then
                    SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_PO Like '" & xPoNo & "%'"
                End If

                SqlStr = SqlStr & vbCrLf & " ORDER BY TO_NUMBER(IH.AUTO_KEY_PO),IH.PUR_ORD_DATE"

                If MainClass.SearchGridMasterBySQL2(xPoNo, SqlStr) = True Then
                    eventArgs.Row = .ActiveRow
                    eventArgs.Col = ColJobOrderNo
                    .Text = AcName

                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColJobOrderNo)
                End If
            End With
        End If

    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mActiveCol As Integer

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColQty Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColQty
                If Val(SprdMain.Text) <> 0 Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        '                    FormatSprdMain SprdMain.MaxRows
                    End If
                End If
                '            SprdMain.Row = SprdMain.MaxRows
            End If
        ElseIf eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColJobOrderNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColJobOrderNo, 0))
        End If
        '    KeyCode = 9999
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColJobOrderNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColJobOrderNo, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart

        Dim mDivisionCode As Double
        Dim xPoNo As String

        If eventArgs.NewRow = -1 Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.Col
            Case ColJobOrderNo
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColJobOrderNo
                xPoNo = Trim(SprdMain.Text)

                ''AND AMEND_WEF_DATE<='" & VB6.Format(txtGatePassDate, "DD-MMM-YYYY") & "'

                If VB.Left(cboPurpose.Text, 1) = "C" Then
                    If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND PUR_TYPE= 'W' AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppcode.Text) & "' AND PO_STATUS='Y' AND PO_CLOSED='N'") = False Then
                        If xPoNo <> "" Then
                            MsgInformation("Invalid Ref No for Such Supplier")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColJobOrderNo)
                            eventArgs.cancel = True
                        End If
                    End If
                End If

        End Select
        '    FormatSprdMain -1
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtGatepassno_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGatepassno.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtGatepassno_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGatepassno.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtGatepassno.Text) = "" Then GoTo EventExitSub

        If Len(txtGatepassno.Text) < 6 Then
            txtGatepassno.Text = Val(txtGatepassno.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        SqlStr = "Select * From INV_GATEPASS_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_PASSNO=" & Val(txtGatepassno.Text) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsReqMain.EOF = False Then
            Clear1()
            If RsReqMain.Fields("PURPOSE").Value = "A" Or RsReqMain.Fields("PURPOSE").Value = "B" Then
                MsgBox("You cann't be change this Gatepass Purpose.", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            End If
            Show1()
        Else
            MsgBox("Invalid GatePass No.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
