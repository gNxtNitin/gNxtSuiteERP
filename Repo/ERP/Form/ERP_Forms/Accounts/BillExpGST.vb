Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmBillExpGST
   Inherits System.Windows.Forms.Form
   Dim RsBillExp As ADODB.Recordset
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   'Private PvtDBCn As ADODB.Connection	

   Private Const IdCGST As String = "CGST"
   Private Const IdIGST As String = "IGST"
   Private Const IdSGST As String = "SGST"
   Private Const IdFRO As String = "Freight & Cartage"
   Private Const IdOTR As String = "Others"
   Private Const IdDOB As String = "Discount"
   Private Const IdVOD As String = "Volume Discount"
   Private Const IdRO As String = "RoundOff"
   Private Const IdMSC As String = "Material Supplied By Client"
   Private Const IdTCS As String = "Tax Collection At Source"
   Private Const IdADE As String = "Additional Duty Excise"
   Private Const IdEE As String = "Export Expenses"
   Private Const IdBCD As String = "Basic Custom Duty"
   Private Const IdMSR As String = "Material Supplied By Client (Reverse)"
   'Private Const IdMSN = "Material Supplied By Client (Non Taxable)"	

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
        MainClass.ButtonStatus(Me, XRIGHT, RsBillExp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)


        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtName.Text = ""
        OptAdd_Ded(0).Checked = True
        OptType(2).Checked = True
        chkRoundOff.CheckState = System.Windows.Forms.CheckState.Unchecked
        TxtDefaultPer.Text = ""
        txtPrintSequence.Text = ""
        txtSales.Text = ""
        txtPurchase.Text = ""
        txtScrap.Text = ""
        TxtDefaultPer.Text = ""
        chkTaxable.CheckState = System.Windows.Forms.CheckState.Unchecked
        CboIdentification.Text = IdOTR
        OptStatus(0).Checked = True


        chkIncludingSales.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtSalesExpHead.Text = ""

        chkGSTRecoverable.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtGSTRecoverable.Text = ""

        Call AutoCompleteSearch("FIN_INTERFACE_MST", "NAME", "GST_ENABLED='Y'", txtName)

        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", txtSales)
        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", txtPurchase)
        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", txtScrap)
        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", txtSalesExpHead)
        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", txtGSTRecoverable)

        MainClass.ButtonStatus(Me, XRIGHT, RsBillExp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub CboIdentification_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CboIdentification.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CboIdentification_TextChanged(sender As Object, e As System.EventArgs) Handles CboIdentification.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkGSTRecoverable_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkGSTRecoverable.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkIncludingSales_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkIncludingSales.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdGSTRecoverable_Click(sender As Object, e As System.EventArgs)
        If MainClass.SearchGridMaster(txtGSTRecoverable.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtGSTRecoverable.Text = AcName
            txtGSTRecoverable.Focus()
        End If
    End Sub

    Private Sub txtGSTRecoverable_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGSTRecoverable.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGSTRecoverable.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGSTRecoverable_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtGSTRecoverable.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtGSTRecoverable_TextChanged(sender As Object, e As System.EventArgs) Handles txtGSTRecoverable.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSalesExpHead_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSalesExpHead.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSalesExpHead.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSalesExpHead_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSalesExpHead.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtSalesExpHead_TextChanged(sender As Object, e As System.EventArgs) Handles txtSalesExpHead.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdSalesExpSearch_Click(sender As Object, e As System.EventArgs)
        If MainClass.SearchGridMaster(txtSalesExpHead.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSalesExpHead.Text = AcName
            txtSalesExpHead.Focus()
        End If
    End Sub

    Private Sub chkRoundOff_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkRoundOff.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkTaxable_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkTaxable.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(sender As Object, e As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsBillExp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
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

    Private Sub CmdPurchSearch_Click(sender As Object, e As System.EventArgs)
        If MainClass.SearchGridMaster(txtPurchase.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtPurchase.Text = AcName
        End If
    End Sub

    Private Sub CmdSalesSearch_Click(sender As Object, e As System.EventArgs)
        If MainClass.SearchGridMaster(txtSales.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSales.Text = AcName
            txtSales.Focus()
        End If
    End Sub

    Private Sub CmdScrapSearch_Click(sender As Object, e As System.EventArgs)
        If MainClass.SearchGridMaster(txtScrap.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtScrap.Text = AcName
            txtScrap.Focus()
        End If
    End Sub

    Private Sub CmdView_Click(sender As Object, e As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub CmdAdd_Click(sender As Object, e As System.EventArgs) Handles CmdAdd.Click
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

    Private Sub CmdClose_Click(sender As Object, e As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        Sqlstr = ""

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        If InsertIntoDelAudit(PubDBCn, "FIN_INTERFACE_MST", (txtName.Text), RsBillExp, "NAME", "D") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "FIN_INTERFACE_MST", "NAME", (txtName.Text)) = False Then GoTo DeleteErr

        Sqlstr = "DELETE FROM FIN_INTERFACE_MST " & vbCrLf _
              & "WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
              & "AND Name='" & MainClass.AllowSingleQuote(UCase((txtName.Text))) & "' AND GST_ENABLED='Y'"

        PubDBCn.Execute(Sqlstr)
        PubDBCn.CommitTrans()
        RsBillExp.Requery() '' .Refresh		
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''		
        RsBillExp.Requery() ''.Refresh		
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub CmdDelete_Click(sender As Object, e As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsBillExp.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.		
                If Delete1() = False Then GoTo DelErrPart
                If RsBillExp.EOF = True Then
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

    Private Sub cmdsearch_Click(sender As Object, e As System.EventArgs)
        On Error GoTo SearchError
        Dim SqlStr As String = ""

        'If MainClass.SearchMaster(txtName.Text, "FIN_INTERFACE_MST", "NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y'") = True Then
        If MainClass.SearchGridMaster(txtName.Text, "FIN_INTERFACE_MST", "NAME", "NAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y'") = True Then
            txtName.Text = AcName
            txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
            CboIdentification.Focus()
        End If

        Exit Sub
SearchError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmBillExpGST_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From FIN_INTERFACE_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBillExp, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        FillIdentification()
        Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmBillExpGST_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsBillExp = Nothing
        RsBillExp.Close()
    End Sub

    Private Sub frmBillExpGST_KeyDown(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmBillExpGST_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub


    Private Sub OptType_CheckedChanged(eventSender As Object, e As System.EventArgs) Handles OptType.CheckedChanged
        If eventSender.Checked Then
            'Dim Index As Short = OptType.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub OptStatus_CheckedChanged(eventSender As Object, e As System.EventArgs) Handles OptStatus.CheckedChanged
        If eventSender.Checked Then
            'Dim Index As Short = OptStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdView_DblClick(sender As Object, e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 2
        SprdView.Row = SprdView.ActiveRow
        txtName.Text = Trim(SprdView.Text)
        TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(sender As Object, EventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If EventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))

    End Sub

    Private Sub TxtDefaultPer_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtDefaultPer.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub
    Private Sub txtName_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtName_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        Sqlstr = ""
        Sqlstr = " SELECT FIN_INTERFACE_MST.PRINTSEQUENCE, FIN_INTERFACE_MST.NAME, " & vbCrLf & " CASE " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='CGST' THEN 'CGST' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='IGST' THEN 'IGST' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='SGST' THEN 'SGST' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='FRO' THEN 'FREIGHT' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='OTR' THEN 'OTHERS' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='DOB' THEN 'DISCOUNT' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='VOD' THEN 'VOLUME DISCOUNT' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='RO' THEN 'ROUND OFF' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='MSC' THEN 'Material Supplied By Client (Taxable)' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='TCS' THEN 'TCS' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='ADE' THEN 'ADD. EXCISE DUTY' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='EE' THEN 'EXPORT EXPENSES' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='BCD' THEN 'BASIC CUSTOM DUTY' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='MSR' THEN 'Material Supplied By Client (Reverse)' " & vbCrLf & " WHEN FIN_INTERFACE_MST.IDENTIFICATION='MSN' THEN 'Material Supplied By Client (Non Taxable)' " & vbCrLf & " END AS IDENTIFICATION, "


        Sqlstr = Sqlstr & vbCrLf & " DECODE(FIN_INTERFACE_MST.ADD_DED,'A','ADD','DEDUCT') AS ADD_DED," & vbCrLf & " DECODE(FIN_INTERFACE_MST.ROUNDOFF,'Y','YES','NO') AS ROUNDOFF," & vbCrLf & " TO_CHAR(FIN_INTERFACE_MST.DefaultPercent,'00.00') AS Percentage," & vbCrLf & " A.SUPP_CUST_NAME AS SALEPOSTING," & vbCrLf & " b.SUPP_CUST_NAME PURCHASEPOSTING"

        Sqlstr = Sqlstr & vbCrLf & " FROM FIN_INTERFACE_MST,FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_MST B" & vbCrLf & " WHERE FIN_INTERFACE_MST.SALEPOSTCODE=A.SUPP_CUST_CODE(+)" & vbCrLf & " AND FIN_INTERFACE_MST.PURCHASEPOSTCODE=B.SUPP_CUST_CODE(+) " & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=A.COMPANY_CODE(+) " & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=B.COMPANY_CODE(+) " & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y'" & vbCrLf & " ORDER BY FIN_INTERFACE_MST.PRINTSEQUENCE, FIN_INTERFACE_MST.NAME"

        MainClass.AssignDataInSprd8(Sqlstr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub

   Private Sub txtName_TextChanged(sender As Object, e As System.EventArgs) Handles txtName.TextChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtName_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
      Dim Cancel As Boolean = EventArgs.Cancel

      On Error GoTo ERR1
      Sqlstr = ""
      If Trim(txtName.Text) = "" Then GoTo EventExitSub
      If MODIFYMode = True And RsBillExp.EOF = False Then xCode = RsBillExp.Fields("CODE").Value

      Sqlstr = "SELECT * FROM FIN_INTERFACE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND NAME='" & MainClass.AllowSingleQuote(UCase((Trim(txtName.Text)))) & "' AND GST_ENABLED='Y'"

      MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBillExp, ADODB.LockTypeEnum.adLockReadOnly)

      If RsBillExp.EOF = False Then
         ADDMode = False
         MODIFYMode = False
         Show1()
      Else
         If ADDMode = False And MODIFYMode = False Then
            MsgBox("Bill Exp. Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
            Cancel = True
         ElseIf MODIFYMode = True Then
            Sqlstr = ""
            Sqlstr = "SELECT * FROM FIN_INTERFACE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE=" & xCode & " AND GST_ENABLED='Y'"

            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBillExp, ADODB.LockTypeEnum.adLockReadOnly)
         End If
      End If
      GoTo EventExitSub
ERR1:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      EventArgs.Cancel = Cancel
   End Sub

   Private Sub txtPrintSequence_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPrintSequence.KeyPress
      Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

      KeyAscii = MainClass.SetNumericField(KeyAscii)
      EventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         EventArgs.Handled = True
      End If
   End Sub
   Private Sub txtPurchase_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurchase.KeyPress
      Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

      KeyAscii = MainClass.UpperCase(KeyAscii, txtPurchase.Text)
      EventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         EventArgs.Handled = True
      End If
   End Sub

   Private Sub txtPurchase_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPurchase.KeyUp
      Dim KeyCode As Short = EventArgs.KeyCode
      Dim Shift As Short = EventArgs.KeyData \ &H10000
   End Sub

   Private Sub txtPurchase_TextChanged(sender As Object, e As System.EventArgs) Handles txtPurchase.TextChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtSales_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSales.KeyPress
      Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

      KeyAscii = MainClass.UpperCase(KeyAscii, txtSales.Text)
      EventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         EventArgs.Handled = True
      End If
   End Sub

   Private Sub txtSales_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSales.KeyUp
      Dim KeyCode As Short = EventArgs.KeyCode
      Dim Shift As Short = EventArgs.KeyData \ &H10000
   End Sub

   Private Sub txtSales_TextChanged(sender As Object, e As System.EventArgs) Handles txtSales.TextChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub frmBillExpGST_Load(sender As Object, e As System.EventArgs) Handles Me.Load
      On Error GoTo ErrPart
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
      ''Set PvtDBCn = New ADODB.Connection		
      ''PvtDBCn.Open StrConn		

      XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
      MainClass.RightsToButton(Me, XRIGHT)
      MainClass.SetControlsColor(Me)
      CmdView.Text = ConCmdGridViewCaption
      ADDMode = False
      MODIFYMode = False
      Call SetMainFormCordinate(Me)
        'Me.Left = 0
        'Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(6360)
        ''Me.Width = VB6.TwipsToPixelsX(8220)
        Call frmBillExpGST_Activated(sender, e)
        Exit Sub
ErrPart:
      MsgBox(Err.Description)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub Show1()

      On Error GoTo ShowErrPart
      If Not RsBillExp.EOF Then
         txtName.Text = IIf(IsDbNull(RsBillExp.Fields("Name").Value), "", RsBillExp.Fields("Name").Value)
         If RsBillExp.Fields("Add_Ded").Value = "A" Then
            OptAdd_Ded(0).Checked = True
         Else
            OptAdd_Ded(1).Checked = True
         End If

         chkTaxable.CheckState = IIf(RsBillExp.Fields("Taxable").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

         chkRoundOff.CheckState = IIf(RsBillExp.Fields("RoundOff").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

         TxtDefaultPer.Text = IIf(IsDbNull(RsBillExp.Fields("DefaultPercent").Value), "", RsBillExp.Fields("DefaultPercent").Value)
         txtPrintSequence.Text = IIf(IsDbNull(RsBillExp.Fields("PrintSequence").Value), "", RsBillExp.Fields("PrintSequence").Value)

         If IsDbNull(RsBillExp.Fields("SALEPOSTCODE").Value) Then
            txtSales.Text = ""
         Else
            If MainClass.ValidateWithMasterTable(RsBillExp.Fields("SALEPOSTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
               txtSales.Text = MasterNo
            Else
               txtSales.Text = ""
            End If
         End If

         If IsDbNull(RsBillExp.Fields("PURCHASEPOSTCODE").Value) Then
            txtPurchase.Text = ""
         Else
            If MainClass.ValidateWithMasterTable(RsBillExp.Fields("PURCHASEPOSTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
               txtPurchase.Text = MasterNo
            Else
               txtPurchase.Text = ""
            End If
         End If

         If IsDbNull(RsBillExp.Fields("SCRAPPOSTCODE").Value) Then
            txtScrap.Text = ""
         Else
            If MainClass.ValidateWithMasterTable(RsBillExp.Fields("SCRAPPOSTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
               txtScrap.Text = MasterNo
            Else
               txtScrap.Text = ""
            End If
         End If

         chkIncludingSales.CheckState = IIf(RsBillExp.Fields("ISINCLUDING_SALES").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
         If IsDbNull(RsBillExp.Fields("SALEEXPPOSTCODE").Value) Then
            txtSalesExpHead.Text = ""
         Else
            If MainClass.ValidateWithMasterTable(RsBillExp.Fields("SALEEXPPOSTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
               txtSalesExpHead.Text = MasterNo
            Else
               txtSalesExpHead.Text = ""
            End If
         End If

         chkGSTRecoverable.CheckState = IIf(RsBillExp.Fields("IS_GSTRECOVERABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
         If IsDbNull(RsBillExp.Fields("GSTRECOVERABLECODE").Value) Then
            txtGSTRecoverable.Text = ""
         Else
            If MainClass.ValidateWithMasterTable(RsBillExp.Fields("GSTRECOVERABLECODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
               txtGSTRecoverable.Text = MasterNo
            Else
               txtGSTRecoverable.Text = ""
            End If
         End If

         Select Case RsBillExp.Fields("Identification").Value
            Case "CGS"
               CboIdentification.Text = IdCGST
            Case "IGS"
               CboIdentification.Text = IdIGST
            Case "SGS"
               CboIdentification.Text = IdSGST
            Case "FRO"
               CboIdentification.Text = IdFRO
            Case "OTR"
               CboIdentification.Text = IdOTR
            Case "DOB"
               CboIdentification.Text = IdDOB
            Case "VOD"
               CboIdentification.Text = IdVOD
            Case "RO"
               CboIdentification.Text = IdRO
            Case "MSC"
               CboIdentification.Text = IdMSC
            Case "TCS"
               CboIdentification.Text = IdTCS
            Case "ADE"
               CboIdentification.Text = IdADE
            Case "EE"
               CboIdentification.Text = IdEE
            Case "BCD"
               CboIdentification.Text = IdBCD
            Case "MSR"
               CboIdentification.Text = IdMSR
               '            Case "MSN"		
               '                CboIdentification.Text = IdMSN		
         End Select


         If IsDbNull(RsBillExp.Fields("Type").Value) Then
            OptType(2).Checked = True
         Else
            If RsBillExp.Fields("Type").Value = "P" Then OptType(0).Checked = True
            If RsBillExp.Fields("Type").Value = "S" Then OptType(1).Checked = True
            If RsBillExp.Fields("Type").Value = "B" Then OptType(2).Checked = True
         End If

         OptStatus(0).Checked = IIf(RsBillExp.Fields("Status").Value = "O", True, False)
         OptStatus(1).Checked = IIf(RsBillExp.Fields("Status").Value = "C", True, False)

         '        OptSTType(0).Value = IIf(RsBillExp.Fields("STTYPE").Value = "L", True, False)		
         '        OptSTType(1).Value = IIf(RsBillExp.Fields("STTYPE").Value = "C", True, False)		

         xCode = RsBillExp.Fields("Code").Value
      End If
      ADDMode = False
      MODIFYMode = False
      MainClass.ButtonStatus(Me, XRIGHT, RsBillExp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
      Exit Sub
ShowErrPart:
      'Resume		
      MsgBox(Err.Description)
   End Sub

   Private Sub CmdSave_Click(sender As Object, e As System.EventArgs) Handles CmdSave.Click
      On Error GoTo ErrorHandler
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
      If FieldsVarification() = False Then
         System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
         Exit Sub
      End If
      If Update1() = True Then
         ADDMode = False
         MODIFYMode = False
         txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
         Frame1.Enabled = True
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
      Dim mPurchasePostCode As String = ""
      Dim mSalesEXPPostCode As String = ""
      Dim mGSTRecoverableCode As String = ""
      Dim Identification As String = ""
      Dim mSalesTaxCode As Integer
      Dim mType As String = ""
      Dim mStatus As String = ""
      Dim mSTType As String = ""
      Dim mFTYPECode As Integer
      Dim mScrapPostCode As String = ""

      PubDBCn.Errors.Clear()
      PubDBCn.BeginTrans()

      mStatus = IIf(OptStatus(0).Checked = True, "O", "C")

      If OptType(0).Checked = True Then mType = "P"
      If OptType(1).Checked = True Then mType = "S"
      If OptType(2).Checked = True Then mType = "B"

      If MainClass.ValidateWithMasterTable(Trim(txtSales.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
         mSalesPostCode = ""
      Else
         mSalesPostCode = MasterNo
      End If

      If MainClass.ValidateWithMasterTable(Trim(txtPurchase.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
         mPurchasePostCode = ""
      Else
         mPurchasePostCode = MasterNo
      End If

      If MainClass.ValidateWithMasterTable(Trim(txtScrap.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
         mScrapPostCode = ""
      Else
         mScrapPostCode = MasterNo
      End If

      mFTYPECode = -1
      mSalesEXPPostCode = ""


      If MainClass.ValidateWithMasterTable(Trim(txtSalesExpHead.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
         mSalesEXPPostCode = ""
      Else
         mSalesEXPPostCode = MasterNo
      End If


      mGSTRecoverableCode = ""
      If MainClass.ValidateWithMasterTable(Trim(txtGSTRecoverable.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
         mGSTRecoverableCode = ""
      Else
         mGSTRecoverableCode = MasterNo
      End If

      Select Case CboIdentification.Text
         Case IdCGST
            Identification = "CGS"
         Case IdIGST
            Identification = "IGS"
         Case IdSGST
            Identification = "SGS"
         Case IdFRO
            Identification = "FRO"
         Case IdOTR
            Identification = "OTR"
         Case IdDOB
            Identification = "DOB"
         Case IdVOD
            Identification = "VOD"
         Case IdRO
            Identification = "RO"
         Case IdMSC
            Identification = "MSC"
         Case IdTCS
            Identification = "TCS"
         Case IdADE
            Identification = "ADE"
         Case IdEE
            Identification = "EE"
         Case IdBCD
            Identification = "BCD"
         Case IdMSR
            Identification = "MSR"
            '        Case IdMSN		
            '            Identification = "MSN"		
      End Select


      '    mSTType = IIf(OptSTType(0).Value = True, "L", "C")		

      Sqlstr = ""
      If ADDMode = True Then
         mCode = MainClass.AutoGenRowNo("FIN_INTERFACE_MST", "Code", PubDBCn)
         Sqlstr = "INSERT INTO FIN_INTERFACE_MST (" & vbCrLf _
            & " COMPANY_CODE, CODE, NAME,  " & vbCrLf _
            & " SALEPOSTCODE, PURCHASEPOSTCODE, ADD_DED, " & vbCrLf _
            & " TAXABLE, DEFAULTPERCENT, PRINTSEQUENCE,  " & vbCrLf _
            & " ROUNDOFF, SALESTAXCODE, IDENTIFICATION, " & vbCrLf _
            & " TYPE, STATUS, EDITRF, " & vbCrLf _
            & " EXCISEABLE, FORMTYPE, FORMTYPECODE, STTYPE,CESSABLE,SERVTAXABLE," & vbCrLf _
            & " CEDCESSABLE, ADDCESSABLE,SALEEXPPOSTCODE,ISINCLUDING_SALES,GST_ENABLED,SCRAPPOSTCODE,IS_GSTRECOVERABLE, GSTRECOVERABLECODE) VALUES ( " & vbCrLf _
            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCode & ", '" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf _
            & " '" & mSalesPostCode & "', '" & mPurchasePostCode & "', '" & IIf(OptAdd_Ded(0).Checked = True, "A", "D") & "', " & vbCrLf _
            & " '" & IIf(chkTaxable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & Val(TxtDefaultPer.Text) & ", " & Val(txtPrintSequence.Text) & "," & vbCrLf _
            & " '" & IIf(chkRoundOff.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & mSalesTaxCode & "," & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(Identification) & "', " & vbCrLf & " '" & mType & "', '" & mStatus & "', 'N', " & vbCrLf _
            & " 'N'," & vbCrLf & " 'N'," & mFTYPECode & ", " & vbCrLf _
            & " '" & mSTType & "','N','N'," & vbCrLf _
            & " 'N'," & vbCrLf & " 'N','" & mSalesEXPPostCode & "','" & IIf(chkIncludingSales.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "','Y', " & vbCrLf _
            & "  '" & mScrapPostCode & "', '" & IIf(chkGSTRecoverable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', '" & mGSTRecoverableCode & "') "
      Else
         Sqlstr = " UPDATE FIN_INTERFACE_MST  SET " & vbCrLf _
            & " NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf _
            & " ADD_DED='" & IIf(OptAdd_Ded(0).Checked = True, "A", "D") & "'," & vbCrLf _
            & " TAXABLE='" & IIf(chkTaxable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
            & " ROUNDOFF='" & IIf(chkRoundOff.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
            & " DEFAULTPERCENT=" & Val(TxtDefaultPer.Text) & "," & vbCrLf _
            & " PRINTSEQUENCE=" & Val(txtPrintSequence.Text) & "," & vbCrLf _
            & " SALEPOSTCODE='" & mSalesPostCode & "', SCRAPPOSTCODE='" & mScrapPostCode & "'," & vbCrLf _
            & " PURCHASEPOSTCODE='" & mPurchasePostCode & "'," & vbCrLf _
            & " SALEEXPPOSTCODE='" & mSalesEXPPostCode & "'," & vbCrLf _
            & " ISINCLUDING_SALES='" & IIf(chkIncludingSales.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
            & " IDENTIFICATION='" & MainClass.AllowSingleQuote(Identification) & "',SalesTaxcode=" & mSalesTaxCode & ", " & vbCrLf _
            & " TYPE='" & mType & "', " & vbCrLf _
            & " FORMTYPECODE=" & mFTYPECode & ", " & vbCrLf _
            & " Status='" & mStatus & "', EDITrf='N', " & vbCrLf _
            & " STTYPE='" & mSTType & "',GST_ENABLED='Y',IS_GSTRECOVERABLE= '" & IIf(chkGSTRecoverable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "',GSTRECOVERABLECODE= '" & mGSTRecoverableCode & "'" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND CODE= " & xCode & ""
      End If
UpdatePart:
      PubDBCn.Execute(Sqlstr)
      PubDBCn.CommitTrans()
      Update1 = True
      Exit Function
UpdateError:
      Update1 = False
      PubDBCn.RollbackTrans() ''		
      RsBillExp.Requery() ''.Refresh		
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Function
   Private Sub SetTextLengths()
      On Error GoTo ERR1
      txtName.Maxlength = RsBillExp.Fields("Name").DefinedSize ''		
      ''		
      TxtDefaultPer.Maxlength = RsBillExp.Fields("DefaultPercent").Precision '' + 1		
      txtPrintSequence.Maxlength = RsBillExp.Fields("PrintSequence").Precision ''		
      Exit Sub
ERR1:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
   End Sub
   Private Sub FillIdentification()
      On Error GoTo ERR1

      CboIdentification.Items.Add(IdCGST)
      CboIdentification.Items.Add(IdIGST)
      CboIdentification.Items.Add(IdSGST)
      CboIdentification.Items.Add(IdFRO)
      CboIdentification.Items.Add(IdOTR)
      CboIdentification.Items.Add(IdDOB)
      CboIdentification.Items.Add(IdVOD)
      CboIdentification.Items.Add(IdRO)
      CboIdentification.Items.Add(IdMSC)
      CboIdentification.Items.Add(IdTCS)
      CboIdentification.Items.Add(IdADE)
      CboIdentification.Items.Add(IdEE)
      CboIdentification.Items.Add(IdBCD)
      CboIdentification.Items.Add(IdMSR)
      '    CboIdentification.AddItem IdMSN		

      Exit Sub
ERR1:
      MsgBox(Err.Description)
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
      If Trim(CboIdentification.Text) = "" Then
         MsgInformation("Identification is empty. Cannot Save")
         CboIdentification.Focus()
         FieldsVarification = False
         Exit Function
      End If



      If Trim(txtSales.Text) <> "" Then
         '        MsgInformation "Sale Posting Account Cann't be Blank. Cannot Save"		
         '        txtSales.SetFocus		
         '        FieldsVarification = False		
         '        Exit Function		
         '    Else		
         If MainClass.ValidateWithMasterTable(Trim(txtSales.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Sale Posting Account Does Not Exist In Account Master. Cannot Save")
            txtSales.Focus()
            FieldsVarification = False
            Exit Function
         End If
      End If

      If Trim(txtPurchase.Text) <> "" Then
         '        MsgInformation "Purchase Posting Account Cann't be Blank. Cannot Save"		
         '        txtPurchase.SetFocus		
         '        FieldsVarification = False		
         '        Exit Function		
         '    Else		
         If MainClass.ValidateWithMasterTable(Trim(txtPurchase.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Purchase Posting Account Does Not Exist In Account Master. Cannot Save")
            txtPurchase.Focus()
            FieldsVarification = False
            Exit Function
         End If
      End If

      If Trim(txtScrap.Text) <> "" Then
         '        MsgInformation "Sale Posting Account Cann't be Blank. Cannot Save"		
         '        txtSales.SetFocus		
         '        FieldsVarification = False		
         '        Exit Function		
         '    Else		
         If CboIdentification.Text = IdTCS Then
            If MainClass.ValidateWithMasterTable(Trim(txtScrap.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
               MsgInformation("Scrap Posting Account Does Not Exist In Account Master. Cannot Save")
               txtScrap.Focus()
               FieldsVarification = False
               Exit Function
            End If
         Else
            MsgInformation("Scrap Posting Account must be blank. Cannot Save")
            FieldsVarification = False
            Exit Function
         End If
      End If

      If chkIncludingSales.CheckState = System.Windows.Forms.CheckState.Checked Then
         If Trim(txtSalesExpHead.Text) = "" Then
            MsgInformation("Sales Expense Head Posting Account Cann't be Blank. Cannot Save")
            txtSalesExpHead.Focus()
            FieldsVarification = False
            Exit Function
         End If
      End If

      If Trim(txtSalesExpHead.Text) <> "" Then
         If MainClass.ValidateWithMasterTable(Trim(txtSalesExpHead.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Sale Expense Head Posting Account Does Not Exist In Account Master. Cannot Save")
            txtSalesExpHead.Focus()
            FieldsVarification = False
            Exit Function
         End If
      End If

      If chkGSTRecoverable.CheckState = System.Windows.Forms.CheckState.Checked Then
         If Trim(txtGSTRecoverable.Text) = "" Then
            MsgInformation("GST Recoverable Posting Account Cann't be Blank. Cannot Save")
            txtGSTRecoverable.Focus()
            FieldsVarification = False
            Exit Function
         End If
      End If

      If Trim(txtGSTRecoverable.Text) <> "" Then
         If InStr(1, Trim(TxtDefaultPer.Text), ".", vbTextCompare) <> 3 Then
            If Len(Trim(TxtDefaultPer.Text)) > 2 And InStr(1, Trim(TxtDefaultPer.Text), ".", vbTextCompare) = 0 Or Len(Trim(TxtDefaultPer.Text)) > 2 And InStr(1, Trim(TxtDefaultPer.Text), ".", vbTextCompare) > 2 Then
               MsgInformation("Discount can not be more than 99.99%. ")
               TxtDefaultPer.Focus()
               FieldsVarification = False
               Exit Function
            End If
         End If
      End If


      If Trim(TxtDefaultPer.Text) <> "" Then
         If InStr(1, Trim(TxtDefaultPer.Text), ".", CompareMethod.Text) <> 3 Then
            If Len(Trim(TxtDefaultPer.Text)) > 2 And InStr(1, Trim(TxtDefaultPer.Text), ".", CompareMethod.Text) = 0 Or Len(Trim(TxtDefaultPer.Text)) > 2 And InStr(1, Trim(TxtDefaultPer.Text), ".", CompareMethod.Text) > 2 Then
               MsgInformation("Discount can not be more than 99.99%. ")
               TxtDefaultPer.Focus()
               FieldsVarification = False
               Exit Function
            End If
         End If
      End If

      If ADDMode = False And MODIFYMode = False Then
         MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
         FieldsVarification = False
      End If
      If MODIFYMode = True And RsBillExp.EOF = True Then Exit Function
      Exit Function
err_Renamed:
      MsgBox(Err.Description)
   End Function
   Private Sub FormatSprdView()

      With SprdView
         .Row = -1
         .set_RowHeight(0, 12)
         .set_ColWidth(0, 5)
         .set_ColWidth(1, 8)
         .set_ColWidth(2, 16)
         .set_ColWidth(3, 12)
         .set_ColWidth(4, 12)
         .set_ColWidth(5, 12)
         .set_ColWidth(6, 12)
         .set_ColWidth(7, 12)
         .set_ColWidth(8, 12)
         .set_ColWidth(9, 12)
         .set_ColWidth(10, 12)
         .set_ColWidth(11, 12)
         .ColsFrozen = 1
         MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
         MainClass.SetSpreadColor(SprdView, -1)
         .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle		
         MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
      End With
   End Sub
   Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        Dim mTitle As String = ""
      On Error GoTo ERR1
      mTitle = ""
      Report1.Reset()
      mTitle = "Bill Exp"
      Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\Billexp.rpt"
      SetCrpt(Report1, Mode, 1, mTitle)
      Report1.WindowShowGroupTree = False
      Report1.Action = 1
      Exit Sub
ERR1:
      MsgInformation(Err.Description)
   End Sub

   Private Sub TxtDefaultPer_TextChanged(sender As Object, e As System.EventArgs) Handles TxtDefaultPer.TextChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub OptAdd_Ded_CheckedChanged(eventSender As Object, e As System.EventArgs) Handles OptAdd_Ded.CheckedChanged
      If eventSender.Checked Then
            'Dim Index As Short = OptAdd_Ded.GetIndex(eventSender)

         MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
      End If
   End Sub

   Private Sub txtPrintSequence_TextChanged(sender As Object, e As System.EventArgs) Handles txtPrintSequence.TextChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtScrap_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtScrap.KeyPress
      Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

      KeyAscii = MainClass.UpperCase(KeyAscii, txtScrap.Text)
      EventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         EventArgs.Handled = True
      End If
   End Sub

   Private Sub txtScrap_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtScrap.KeyUp
      Dim KeyCode As Short = EventArgs.KeyCode
      Dim Shift As Short = EventArgs.KeyData \ &H10000
   End Sub

   Private Sub txtScrap_TextChanged(sender As Object, e As System.EventArgs) Handles txtScrap.TextChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub
End Class