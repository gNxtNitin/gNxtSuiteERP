Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmInvType
   Inherits System.Windows.Forms.Form
   Dim RsInvType As ADODB.Recordset
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   ''Private PvtDBCn As ADODB.Connection

   Dim xCode As Integer
   Dim FormActive As Boolean
   Dim Shw As Boolean
   Dim MasterNo As Object
    Dim SqlStr As String = ""
    Private Sub ViewGrid()

        On Error GoTo ErrorPart
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh	
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            FraGridView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraGridView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsInvType, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtName.Text = ""
        TxtStartingNo.Text = ""
        txtAccount.Text = ""
        txtInvHeading.Text = ""
        txtAlias.Text = ""

        optType(0).Checked = True
        optType(1).Checked = False
        optType(2).Checked = False
        optType(3).Checked = False
        optType(4).Checked = False
        optType(5).Checked = False
        optType(6).Checked = False
        optType(7).Checked = False
        optType(8).Checked = False
        optType(9).Checked = False
        optType(10).Checked = False
        optType(11).Checked = False

        OptStatus(0).Checked = True
        OptStatus(1).Checked = False

        OptItemType(0).Checked = True
        OptItemType(1).Checked = False
        chkInstitutional.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkOEM.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAfterMarket.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkStockTrf.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSameGSTN.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSuppBill.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSPD.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkExportInvoice.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkScrapSale.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkSaleComp.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSale57.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkJw.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkFixAssets.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSaleReturn.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkSalesTaxReq.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSalesTaxReq.Enabled = IIf(lblCategory.Text = "P", True, False)

        chkGSTReq.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkGSTReq.Enabled = IIf(lblCategory.Text = "P", True, False)
        chkGSTReq.Visible = IIf(lblCategory.Text = "P", True, False)

        Call AutoCompleteSearch("FIN_INVTYPE_MST", "NAME", "CATEGORY='" & lblCategory.Text & "'", txtName)
        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", txtAccount)

        MainClass.ButtonStatus(Me, XRIGHT, RsInvType, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo ERR1
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtName.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "FIN_INVTYPE_MST", (txtName.Text), RsInvType, "NAME") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "FIN_INVTYPE_MST", "NAME", (txtName.Text)) = False Then GoTo DeleteErr

        SqlStr = "DELETE FROM FIN_INVTYPE_MST " & vbCrLf _
              & "WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
              & "AND Name='" & MainClass.AllowSingleQuote(UCase((txtName.Text))) & "'"
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsInvType.Requery() ''.Refresh	
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''	
        RsInvType.Requery() ''.Refresh	
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            MsgInformation("Cann't be Delete.")
            Exit Sub
        End If
        If Not RsInvType.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                If Delete1 = False Then GoTo DelErrPart
                If RsInvType.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        ErrorMsg("Record Not Deleted", "DELETE", MsgBoxStyle.Critical)
    End Sub
    Private Sub frmInvType_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmInvType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub OptItemType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptItemType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptItemType.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub


    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtName.Text = Trim(SprdView.Text)
        txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAccount.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtAccount.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(Trim(txtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Account Posting Does Not Exist In Account Master.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAlias_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAlias.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAlias_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAlias.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAlias.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtInvHeading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvHeading.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvHeading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInvHeading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInvHeading.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsInvType.EOF = False Then xCode = RsInvType.Fields("CODE").Value

        SqlStr = "SELECT * FROM FIN_INVTYPE_MST " & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
              & " AND NAME='" & MainClass.AllowSingleQuote(UCase((Trim(txtName.Text)))) & "' AND CATEGORY='" & lblCategory.text & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInvType, ADODB.LockTypeEnum.adLockReadOnly)

        If RsInvType.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Bill Exp. Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE=" & xCode & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInvType, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmInvType_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From FIN_INVTYPE_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInvType, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        Clear1()

        If UCase(lblCategory.Text) = "S" Then
            Me.Text = "Sale Invoice Type"
        Else
            Me.Text = "Purchase Invoice Type"
        End If
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmInvType_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CmdView.Text = ConCmdGridViewCaption
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        'Me.Left = 0
        'Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(6570)
        ''Me.Width = VB6.TwipsToPixelsX(8265)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmInvType_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsInvType = Nothing
        RsInvType.Close()
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        If Not RsInvType.EOF Then

            txtName.Text = IIf(IsDbNull(RsInvType.Fields("Name").Value), "", RsInvType.Fields("Name").Value)
            txtAlias.Text = IIf(IsDbNull(RsInvType.Fields("Alias").Value), "", RsInvType.Fields("Alias").Value)
            txtInvHeading.Text = IIf(IsDbNull(RsInvType.Fields("INV_HEADING").Value), "", RsInvType.Fields("INV_HEADING").Value)

            optType(0).Checked = IIf(RsInvType.Fields("Identification").Value = "E", True, False)
            optType(1).Checked = IIf(RsInvType.Fields("Identification").Value = "M", True, False)
            optType(2).Checked = IIf(RsInvType.Fields("Identification").Value = "J", True, False)
            optType(3).Checked = IIf(RsInvType.Fields("Identification").Value = "R", True, False)
            optType(4).Checked = IIf(RsInvType.Fields("Identification").Value = "C", True, False)
            optType(5).Checked = IIf(RsInvType.Fields("Identification").Value = "P", True, False)
            optType(6).Checked = IIf(RsInvType.Fields("Identification").Value = "T", True, False)
            optType(7).Checked = IIf(RsInvType.Fields("Identification").Value = "X", True, False)
            optType(8).Checked = IIf(RsInvType.Fields("Identification").Value = "Z", True, False)
            optType(9).Checked = IIf(RsInvType.Fields("Identification").Value = "W", True, False)
            optType(10).Checked = IIf(RsInvType.Fields("Identification").Value = "G", True, False)
            optType(11).Checked = IIf(RsInvType.Fields("Identification").Value = "S", True, False)

            TxtStartingNo.Text = IIf(IsDbNull(RsInvType.Fields("Invoicenostart").Value), "", RsInvType.Fields("Invoicenostart").Value)

            If MainClass.ValidateWithMasterTable(RsInvType.Fields("ACCOUNTPOSTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtAccount.Text = MasterNo
            Else
                txtAccount.Text = ""
            End If

            OptStatus(0).Checked = IIf(RsInvType.Fields("Status").Value = "O", True, False)
            OptStatus(1).Checked = IIf(RsInvType.Fields("Status").Value = "C", True, False)

            OptItemType(0).Checked = IIf(RsInvType.Fields("ITEMTYPE").Value = "R", True, False)
            OptItemType(1).Checked = IIf(RsInvType.Fields("ITEMTYPE").Value = "O", True, False)

            chkStockTrf.CheckState = IIf(RsInvType.Fields("ISSTOCKTRF").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkSameGSTN.CheckState = IIf(RsInvType.Fields("SAME_GSTN").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkSuppBill.CheckState = IIf(RsInvType.Fields("ISSUPPBILL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkSPD.CheckState = IIf(RsInvType.Fields("ISSPD").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkExportInvoice.CheckState = IIf(RsInvType.Fields("ISEXPORT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkScrapSale.CheckState = IIf(RsInvType.Fields("ISSCRAPSALE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkInstitutional.CheckState = IIf(RsInvType.Fields("IS_INSTITUTIONAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkOEM.CheckState = IIf(RsInvType.Fields("IS_OEM").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkAfterMarket.CheckState = IIf(RsInvType.Fields("IS_AFTER_MKT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkSaleComp.CheckState = IIf(RsInvType.Fields("ISSALECOMP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkSale57.CheckState = IIf(RsInvType.Fields("ISSALE57").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkJw.CheckState = IIf(RsInvType.Fields("ISSALEJW").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            '        chkJobworkExcise.Value = IIf(RsInvType.Fields("ISSALEJW_EXCISEABLE").Value = "Y", vbChecked, vbUnchecked)	

            chkSaleReturn.CheckState = IIf(RsInvType.Fields("ISSALERETURN").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkFixAssets.CheckState = IIf(RsInvType.Fields("ISFIXASSETS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkSalesTaxReq.CheckState = IIf(RsInvType.Fields("ISST_REQ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkGSTReq.CheckState = IIf(RsInvType.Fields("ISGST_REQ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            lblCategory.Text = RsInvType.Fields("CATEGORY").Value

            xCode = RsInvType.Fields("Code").Value
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsInvType, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume	
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateError
        Dim mCode As Integer
        Dim mSalesPostCode As String = ""
        Dim Identification As String = ""
        Dim mSalesTaxCode As Integer
        Dim mType As String = "" '**	
        Dim mStatus As String = "" '**	
        Dim mStartNo As Double '**	
        Dim mSTType As String = ""
        Dim mCategory As String = ""
        Dim mItemType As String = ""
        Dim mStockTrf As String = ""
        Dim mSuppBill As String = ""
        Dim mSPD As String = ""
        Dim mExportInvoice As String = ""
        Dim mSaleComp As String = ""
        Dim mSale57 As String = ""
        Dim mJW As String = ""
        Dim mFixAssets As String = ""
        Dim mSaleReturn As String = ""
        Dim mScrapSale As String = ""
        Dim mSaleTaxReq As String = ""
        Dim mGSTReq As String = ""
        Dim mIsOEM As String = ""
        Dim mIsInstitutional As String = ""
        Dim mIsAfterMarket As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Long
        Dim mSameGSTN As String = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mStatus = IIf(OptStatus(0).Checked = True, "O", "C")
        mCategory = UCase(lblCategory.Text)

        If optType(0).Checked = True Then mType = "E"
        If optType(1).Checked = True Then mType = "M"
        If optType(2).Checked = True Then mType = "J"
        If optType(3).Checked = True Then mType = "R"
        If optType(4).Checked = True Then mType = "C"
        If optType(5).Checked = True Then mType = "P"
        If optType(6).Checked = True Then mType = "T"
        If optType(7).Checked = True Then mType = "X"
        If optType(8).Checked = True Then mType = "Z"
        If optType(9).Checked = True Then mType = "W"
        If optType(10).Checked = True Then mType = "G"
        If optType(11).Checked = True Then mType = "S"

        If OptItemType(0).Checked = True Then mItemType = "R"
        If OptItemType(1).Checked = True Then mItemType = "O"
        If optType(6).Checked = True Then mItemType = "O"

        If chkStockTrf.CheckState = System.Windows.Forms.CheckState.Checked Then
            mStockTrf = "Y"
        Else
            mStockTrf = "N"
        End If

        If chkSameGSTN.CheckState = System.Windows.Forms.CheckState.Checked Then
            mSameGSTN = "Y"
        Else
            mSameGSTN = "N"
        End If

        If chkSuppBill.CheckState = System.Windows.Forms.CheckState.Checked Then
            mSuppBill = "Y"
        Else
            mSuppBill = "N"
        End If

        If chkSaleReturn.CheckState = System.Windows.Forms.CheckState.Checked Then
            mSaleReturn = "Y"
        Else
            mSaleReturn = "N"
        End If

        If chkSPD.CheckState = System.Windows.Forms.CheckState.Checked Then
            mSPD = "Y"
        Else
            mSPD = "N"
        End If

        If chkExportInvoice.CheckState = System.Windows.Forms.CheckState.Checked Then
            mExportInvoice = "Y"
        Else
            mExportInvoice = "N"
        End If

        If chkScrapSale.CheckState = System.Windows.Forms.CheckState.Checked Then
            mScrapSale = "Y"
        Else
            mScrapSale = "N"
        End If

        If chkSaleComp.CheckState = System.Windows.Forms.CheckState.Checked Then
            mSaleComp = "Y"
        Else
            mSaleComp = "N"
        End If

        If chkSalesTaxReq.CheckState = System.Windows.Forms.CheckState.Checked Then
            mSaleTaxReq = "Y"
        Else
            mSaleTaxReq = "N"
        End If

        If chkGSTReq.CheckState = System.Windows.Forms.CheckState.Checked Then
            mGSTReq = "Y"
        Else
            mGSTReq = "N"
        End If

        If chkSale57.CheckState = System.Windows.Forms.CheckState.Checked Then
            mSale57 = "Y"
        Else
            mSale57 = "N"
        End If

        If chkJw.CheckState = System.Windows.Forms.CheckState.Checked Then
            mJW = "Y"
        Else
            mJW = "N"
        End If



        If chkFixAssets.CheckState = System.Windows.Forms.CheckState.Checked Then
            mFixAssets = "Y"
        Else
            mFixAssets = "N"
        End If


        If chkOEM.CheckState = System.Windows.Forms.CheckState.Checked Then
            mIsOEM = "Y"
        Else
            mIsOEM = "N"
        End If

        If chkInstitutional.CheckState = System.Windows.Forms.CheckState.Checked Then
            mIsInstitutional = "Y"
        Else
            mIsInstitutional = "N"
        End If


        If chkAfterMarket.CheckState = System.Windows.Forms.CheckState.Checked Then
            mIsAfterMarket = "Y"
        Else
            mIsAfterMarket = "N"
        End If


        mStartNo = CDbl(TxtStartingNo.Text)
        If ADDMode = True Then
            mCode = MainClass.AutoGenRowNo("FIN_INVTYPE_MST", "Code", PubDBCn)
        End If

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                If MainClass.ValidateWithMasterTable(Trim(txtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = False Then
                    mSalesPostCode = "-1"
                Else
                    mSalesPostCode = MasterNo
                End If


                SqlStr = ""
                If ADDMode = True Then
                    'mCode = MainClass.AutoGenRowNo("FIN_INVTYPE_MST", "Code", PubDBCn)
                    SqlStr = "INSERT INTO FIN_INVTYPE_MST (" & vbCrLf _
                            & " COMPANY_CODE, CODE, NAME,ALIAS,  " & vbCrLf _
                            & " IDENTIFICATION, INVOICENOSTART, " & vbCrLf _
                            & " ACCOUNTPOSTCODE,STATUS, CATEGORY,ITEMTYPE," & vbCrLf _
                            & " ISSTOCKTRF,INV_HEADING,ISSUPPBILL,ISSPD, " & vbCrLf _
                            & " ISSALECOMP, ISSALE57, ISSALEJW, ISFIXASSETS, " & vbCrLf _
                            & " ISSALERETURN,ISSCRAPSALE, ISEXPORT,ISST_REQ,IS_OEM,IS_INSTITUTIONAL, IS_AFTER_MKT, ISGST_REQ,SAME_GSTN " & vbCrLf _
                            & " ) VALUES ( " & vbCrLf _
                            & " " & xCompanyCode & ", " & mCode & ", '" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtAlias.Text) & "'," & vbCrLf _
                            & " '" & mType & "', " & mStartNo & ", " & vbCrLf _
                            & " '" & mSalesPostCode & "', '" & mStatus & "', '" & mCategory & "'," & vbCrLf _
                            & " '" & mItemType & "','" & mStockTrf & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtInvHeading.Text) & "','" & mSuppBill & "','" & mSPD & "', " & vbCrLf _
                            & " '" & mSaleComp & "','" & mSale57 & "','" & mJW & "', '" & mFixAssets & "'," & vbCrLf _
                            & " '" & mSaleReturn & "','" & mScrapSale & "', '" & mExportInvoice & "', '" & mSaleTaxReq & "'," & vbCrLf _
                            & " '" & mIsOEM & "', '" & mIsInstitutional & "', '" & mIsAfterMarket & "','" & mGSTReq & "','" & mSameGSTN & "')"

                Else
                    SqlStr = " UPDATE FIN_INVTYPE_MST  SET " & vbCrLf _
                            & " NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf _
                            & " ALIAS='" & MainClass.AllowSingleQuote(txtAlias.Text) & "', SAME_GSTN='" & mSameGSTN & "'," & vbCrLf _
                            & " IDENTIFICATION='" & mType & "'," & vbCrLf _
                            & " INVOICENOSTART=" & mStartNo & "," & vbCrLf _
                            & " ACCOUNTPOSTCODE='" & mSalesPostCode & "'," & vbCrLf _
                            & " Status='" & mStatus & "', " & vbCrLf _
                            & " CATEGORY='" & mCategory & "', " & vbCrLf _
                            & " ITEMTYPE='" & mItemType & "', " & vbCrLf _
                            & " ISSTOCKTRF='" & mStockTrf & "'," & vbCrLf _
                            & " ISSUPPBILL='" & mSuppBill & "'," & vbCrLf _
                            & " ISSPD='" & mSPD & "', ISSCRAPSALE='" & mScrapSale & "'," & vbCrLf _
                            & " ISSALECOMP='" & mSaleComp & "'," & vbCrLf _
                            & " ISSALE57='" & mSale57 & "'," & vbCrLf _
                            & " ISSALEJW='" & mJW & "'," & vbCrLf _
                            & " ISFIXASSETS='" & mFixAssets & "'," & vbCrLf _
                            & " INV_HEADING= '" & MainClass.AllowSingleQuote(txtInvHeading.Text) & "'," & vbCrLf _
                            & " ISSALERETURN='" & mSaleReturn & "'," & vbCrLf _
                            & " ISEXPORT='" & mExportInvoice & "'," & vbCrLf _
                            & " ISST_REQ='" & mSaleTaxReq & "', ISGST_REQ='" & mGSTReq & "', IS_OEM='" & mIsOEM & "', IS_INSTITUTIONAL='" & mIsInstitutional & "', IS_AFTER_MKT='" & mIsAfterMarket & "'" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & xCompanyCode & "" & vbCrLf & " AND CODE= " & xCode & ""
                End If
UpdatePart:
                PubDBCn.Execute(SqlStr)
                RsTemp.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''	
        RsInvType.Requery() ''.Refresh	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        txtName.Maxlength = RsInvType.Fields("Name").DefinedSize
        txtAlias.Maxlength = RsInvType.Fields("Alias").DefinedSize ''	
        txtInvHeading.Maxlength = RsInvType.Fields("Inv_Heading").DefinedSize
        TxtStartingNo.Maxlength = RsInvType.Fields("INVOICENOSTART").Precision ''	
        txtAccount.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If Trim(txtName.Text) = "" Then
            MsgInformation(" Name is empty. Cannot Save")
            txtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtAlias.Text) = "" Then
            MsgInformation(" Alias is empty. Cannot Save")
            txtAlias.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtAccount.Text) = "" Then
            MsgInformation("Account Posting Cann't be Blank. Cannot Save")
            txtAccount.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(Trim(txtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Account Posting Does Not Exist In Account Master. Cannot Save")
                txtAccount.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If


        If Val(TxtStartingNo.Text) = 0 Then
            MsgInformation("Starting No cann't be Zero or Blank.")
            TxtStartingNo.Focus()
            FieldsVarification = False
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsInvType.EOF = True Then Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub txtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT FIN_INVTYPE_MST.NAME,ALIAS, " & vbCrLf _
            & " CASE WHEN FIN_INVTYPE_MST.IDENTIFICATION='E' THEN 'EXCISEABLE'  " & vbCrLf _
            & " WHEN FIN_INVTYPE_MST.IDENTIFICATION='M' THEN 'MISCELLANEOUS' " & vbCrLf _
            & " WHEN FIN_INVTYPE_MST.IDENTIFICATION='J' THEN 'JOB WORK' " & vbCrLf _
            & " WHEN FIN_INVTYPE_MST.IDENTIFICATION='R' THEN 'REJECTION' " & vbCrLf _
            & " WHEN FIN_INVTYPE_MST.IDENTIFICATION='C' THEN 'CASH MEMO' " & vbCrLf _
            & " WHEN FIN_INVTYPE_MST.IDENTIFICATION='P' THEN 'PERFORMA' WHEN FIN_INVTYPE_MST.IDENTIFICATION='T' THEN 'TRADING' END AS IDENTIFICATION, " & vbCrLf _
            & " FIN_INVTYPE_MST.INVOICENOSTART AS STARTING_NO, " & vbCrLf _
            & " A.SUPP_CUST_NAME AS ACCOUNTPOSTINGNAME," & vbCrLf _
            & " DECODE(FIN_INVTYPE_MST.ITEMTYPE,'R','RAW','OTHER') AS ITEMTYPE, DECODE(FIN_INVTYPE_MST.ISFIXASSETS,'Y','YES','NO') AS ISFIXASSETS ," & vbCrLf _
            & " DECODE(FIN_INVTYPE_MST.STATUS,'O','OPEN','CLOSE') AS STATUS," & vbCrLf _
            & " DECODE(FIN_INVTYPE_MST.ISSTOCKTRF,'Y','YES','NO') AS STOCK_TRF," & vbCrLf _
            & " DECODE(FIN_INVTYPE_MST.ISSUPPBILL,'Y','YES','NO') AS SUPP_BILL, DECODE(FIN_INVTYPE_MST.ISST_REQ,'Y','YES','NO') AS ST_REQ" & vbCrLf _
            & " FROM FIN_INVTYPE_MST,FIN_SUPP_CUST_MST A" & vbCrLf & " WHERE FIN_INVTYPE_MST.ACCOUNTPOSTCODE=A.SUPP_CUST_CODE(+)" & vbCrLf _
            & " AND FIN_INVTYPE_MST.COMPANY_CODE=A.COMPANY_CODE(+) " & vbCrLf & " AND FIN_INVTYPE_MST.CATEGORY='" & UCase(lblCategory.Text) & "' " & vbCrLf _
            & " AND FIN_INVTYPE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " ORDER BY FIN_INVTYPE_MST.NAME"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub
   Private Sub FormatSprdView()

      With SprdView
         .Row = -1
         .set_RowHeight(0, 12)
         .set_ColWidth(0, 5)
         .set_ColWidth(1, 30)
         .set_ColWidth(2, 12)
         .set_ColWidth(3, 12)
         .set_ColWidth(4, 12)
         .set_ColWidth(5, 12)
         .set_ColWidth(6, 12)
         .set_ColWidth(7, 12)
         .set_ColWidth(8, 12)
         .set_ColWidth(9, 12)
         .ColsFrozen = 1
         MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
         MainClass.SetSpreadColor(SprdView, -1)
         .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
         MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
      End With
   End Sub
   Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        Dim mTitle As String = ""
      On Error GoTo ERR1
      mTitle = ""
      Report1.Reset()
      mTitle = "Invoice Type"
      Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\InvType.rpt"
      SetCrpt(Report1, Mode, 1, mTitle)
      Report1.WindowShowGroupTree = False
      Report1.Action = 1
      Exit Sub
ERR1:
      MsgInformation(Err.Description)
   End Sub

   Private Sub TxtStartingNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtStartingNo.TextChanged

      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub TxtStartingNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtStartingNo.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

      KeyAscii = MainClass.SetNumericField(KeyAscii)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub CmdView_Click(sender As Object, e As System.EventArgs) Handles CmdView.Click
      ViewGrid()
   End Sub

   Private Sub chkAfterMarket_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkAfterMarket.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkExportInvoice_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkExportInvoice.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkFixAssets_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkFixAssets.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkGSTReq_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkGSTReq.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkInstitutional_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkInstitutional.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkJw_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkJw.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkOEM_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkOEM.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkSale57_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkSale57.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkSaleComp_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkSaleComp.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkSaleReturn_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkSaleReturn.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkSalesTaxReq_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkSalesTaxReq.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkScrapSale_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkScrapSale.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkSPD_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkSPD.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkStockTrf_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkStockTrf.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkSuppBill_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkSuppBill.CheckStateChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub CmdModify_Click(sender As Object, e As System.EventArgs) Handles CmdModify.Click
      On Error GoTo ModifyErr
      If CmdModify.Text = ConcmdmodifyCaption Then
         ADDMode = False
         MODIFYMode = True
         MainClass.ButtonStatus(Me, XRIGHT, RsInvType, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
      Else
         ADDMode = False
         MODIFYMode = False
         Show1()
      End If
      Exit Sub
ModifyErr:
      MsgBox(Err.Description)
   End Sub

   Private Sub cmdPreview_Click(sender As Object, e As System.EventArgs) Handles cmdPreview.Click
      Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
      ShowReport(Crystal.DestinationConstants.crptToWindow)
      Me.Cursor = System.Windows.Forms.Cursors.Default
   End Sub

   Private Sub cmdPrint_Click(sender As Object, e As System.EventArgs) Handles cmdPrint.Click
      Me.Cursor = System.Windows.Forms.Cursors.Arrow
      ShowReport(Crystal.DestinationConstants.crptToPrinter)
      Me.Cursor = System.Windows.Forms.Cursors.Default
   End Sub

    Private Sub cmdsearch_Click(sender As Object, e As EventArgs) Handles cmdsearch.Click
        On Error GoTo SearchError
        If MainClass.SearchMaster(txtName.Text, "FIN_INVTYPE_MST", "NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='" & UCase(lblCategory.Text) & "'") = True Then
            txtName.Text = AcName
            txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
SearchError:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub

    Private Sub CmdAcctSearch_Click(sender As Object, e As EventArgs) Handles CmdAcctSearch.Click
        If MainClass.SearchGridMaster(txtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAccount.Text = AcName
            txtAccount.Focus()
        End If
    End Sub
    Private Sub chkSameGSTN_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkSameGSTN.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
End Class
