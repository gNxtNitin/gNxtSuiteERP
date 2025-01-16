Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmPackingNote
    Inherits System.Windows.Forms.Form
    Dim RsPackMain As ADODB.Recordset
    Dim RsPackDetail As ADODB.Recordset
    Dim RsPackExp As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection	

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer

    Dim mCustomerCode As String
    Dim mWithOutOrder As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColSONo As Short = 1
    Private Const ColSODate As Short = 2
    Private Const ColBuyerPO As Short = 3
    Private Const ColBuyerDate As Short = 4
    Private Const ColItemCode As Short = 5
    Private Const ColItemDesc As Short = 6
    Private Const ColPartNo As Short = 7
    Private Const ColUnit As Short = 8

    Private Const ColGlassDescription As Short = 9
    Private Const ColActualWidth As Short = 10
    Private Const ColActualHeight As Short = 11
    Private Const ColSize As Short = 12

    Private Const ColChargeableWidth As Short = 13
    Private Const ColChargeableHeight As Short = 14
    Private Const ColGlassArea As Short = 15

    Private Const ColModelNo As Short = 16
    Private Const ColDrawingNo As Short = 17

    Private Const ColMarks As Short = 18
    Private Const ColBuyerPOQty As Short = 19
    Private Const ColPalletNo As Short = 20
    Private Const ColPackQty As Short = 21
    Private Const ColQty As Short = 22
    Private Const ColNetWt As Short = 23
    Private Const ColGrossWt As Short = 24

    Private Const ColOthersName As Short = 1
    Private Const ColOthersDesc As Short = 2
    Private Const ColOthersValue As Short = 3


    Private Sub chkDC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDC.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkExciseInvoice_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkExciseInvoice.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkExportInv_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkExportInv.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonPacking(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportonPacking(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRptFileName As String

        Report1.Reset()
        mTitle = "Requisation Slip/Dispatch Advice"  '' "Packing List"
        mSubTitle = ""

        SqlStr = MakeSQL()

        mRptFileName = "PackingList_New.RPT"        ''"PackingList.RPT"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonPacking(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearchBuyer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchBuyer.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & ""  '' AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtBuyerName.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR || SUPP_CUST_CITY || SUPP_CUST_STATE", SqlStr) = True Then
            txtBuyerName.Text = AcName
            txtBuyerCode.Text = AcName1
            txtBillTo.Text = AcName2
            txtBuyerName_Validating(txtBuyerName, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_LeaveRow(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveRowEvent) Handles SprdMain.LeaveRow
        '    SprdMain.Row = Row	
        '    SprdMain.Row2 = Row	
        '    SprdMain.Col = 1	
        '    SprdMain.col2 = SprdMain.ActiveCol	
        '    SprdMain.BlockMode = True	
        '    SprdMain.BackColor = &HFFFF80	
        '    SprdMain.BlockMode = False	
    End Sub
    Private Sub txtBuyerDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBuyerDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBuyerDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBuyerDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtBuyerDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtBuyerDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtBuyerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBuyerName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBuyerName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBuyerName.DoubleClick
        cmdsearchBuyer_Click(cmdsearchBuyer, New System.EventArgs())
    End Sub

    Private Sub txtBuyerName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBuyerName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBuyerName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBuyerName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearchBuyer_Click(cmdsearchBuyer, New System.EventArgs())
    End Sub

    Private Sub txtBuyerName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBuyerName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtBuyerName.Text) = "" Then
            txtBuyerAddress.Text = ""
            txtBuyerCode.Text = ""
            txtBillTo.Text = ""
            GoTo EventExitSub
        End If


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((txtBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Buyer Name")
            Cancel = True
        Else
            txtBuyerCode.Text = MasterNo
        End If

        txtBuyerAddress.Text = FillAddressDetail(txtBuyerCode.Text, txtBillTo.Text)
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBuyerNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBuyerNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBuyerNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBuyerNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCarriage_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCarriage.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCarriage_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCarriage.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCarriage.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtContainerNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtContainerNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtContainerNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtContainerNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtContainerNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCustomerCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomerCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomerCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomerCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCustomerCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomerCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtCustomerCode.Text) = "" Then
            txtConsigneeAddress.Text = ""
            TxtCustomerName.Text = ""
            txtShipTo.Text = ""
            GoTo EventExitSub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((txtCustomerCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCustomerName.Text = MasterNo
            mCustomerCode = txtCustomerCode.Text
        Else
            mCustomerCode = "-1"
            txtCustomerName.Text = ""
            Cancel = True
        End If

        If ADDMode = True Then
            Call FillCustomerDetail()
        End If

        txtConsigneeAddress.Text = FillAddressDetail(txtCustomerCode.Text, txtShipTo.Text)

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FillCustomerDetail()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xBuyerCode As String


        SqlStr = " SELECT BUYERCODE, CARRIAGE, LOADINGPORT, " & vbCrLf & " DISCHARGEPORT, FINALDEST, PAYMENTTERMS, SUPP_CUST_CITY, SUPP_CUST_STATE,COUNTRY " & vbCrLf & " FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCustomerCode.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            With RsTemp
                xBuyerCode = IIf(IsDbNull(.Fields("BUYERCODE").Value), "", .Fields("BUYERCODE").Value)

                If MainClass.ValidateWithMasterTable(xBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtBuyerName.Text = MasterNo
                End If

                txtCarriage.Text = IIf(IsDbNull(.Fields("CARRIAGE").Value), "", .Fields("CARRIAGE").Value)
                txtLoading.Text = IIf(IsDbNull(.Fields("LOADINGPORT").Value), "", .Fields("LOADINGPORT").Value)
                txtDischarge.Text = IIf(IsDbNull(.Fields("DISCHARGEPORT").Value), "", .Fields("DISCHARGEPORT").Value)
                txtFinalDestination.Text = IIf(IsDbNull(.Fields("FINALDEST").Value), "", .Fields("FINALDEST").Value)
                txtPayments.Text = IIf(IsDbNull(.Fields("PAYMENTTERMS").Value), "", .Fields("PAYMENTTERMS").Value)
                txtDestination.Text = IIf(IsDbNull(.Fields("COUNTRY").Value), "", .Fields("COUNTRY").Value) ''& IIf(IsNull(!SUPP_CUST_STATE), "", ", " & !SUPP_CUST_STATE)	
            End With
        End If
        Exit Sub

ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtCustomerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCustomerName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomerName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCustomerName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtCustomerName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtCustomerName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtCustomerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDestination_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDestination.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDestination_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDestination.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDestination.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDischarge_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDischarge.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDischarge_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDischarge.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDischarge.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtExciseBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExciseBillDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExciseBillDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExciseBillDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtExciseBillDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtExciseBillDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtExciseBillDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtExciseBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExciseBillNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExciseBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExciseBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtExciseBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtFinalDestination_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFinalDestination.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFinalDestination_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFinalDestination.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFinalDestination.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtFlight_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFlight.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFlight_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFlight.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFlight.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtIECNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIECNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIECNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIECNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIECNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInvDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInvDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtInvDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtInvDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtInvDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtInvNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInvNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInvNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtLoading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoading.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLoading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLoading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLoading.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtNotifyParty1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNotifyParty1.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNotifyParty1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNotifyParty1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNotifyParty1.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNotifyParty2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNotifyParty2.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNotifyParty2_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNotifyParty2.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNotifyParty2.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNotifyParty3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNotifyParty3.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNotifyParty3_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNotifyParty3.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNotifyParty3.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOrigin_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOrigin.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOrigin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOrigin.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOrigin.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPackNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPackNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtPackNo.Enabled = False
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Call FillSprdOther()
            Call FormatSprdOther(-1)

            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart

        Dim xDCNo As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBookCode As Integer

        If chkDC.CheckState = System.Windows.Forms.CheckState.Checked Or chkExciseInvoice.CheckState = System.Windows.Forms.CheckState.Checked Or chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Transaction Made Against This Despatch Note So Cann't be Deleted")
            Exit Sub
        End If

        If ValidateBranchLocking((txtPackDate.Text)) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, txtPackDate.Text, (txtCustomerName.Text), mCustomerCode) = True Then
            Exit Sub
        End If

        If Trim(txtPackNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        '    If CheckBillPayment(mCustomerCode, txtBillNo.Text, "B") = True Then Exit Sub	

        If Not RsPackMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "DSP_PACKING_HDR", (txtPackNo.Text), RsPackMain, "REFNO") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "DSP_PACKING_HDR", "AUTO_KEY_PACK", (lblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from FIN_PACKING_PARA_EXP Where AUTO_KEY_PACK=" & Val(LblMkey.Text) & "")
                PubDBCn.Execute("Delete from DSP_PACKING_DET Where AUTO_KEY_PACK=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("Delete from DSP_PACKING_HDR Where AUTO_KEY_PACK=" & Val(lblMkey.Text) & "")

                PubDBCn.CommitTrans()
                RsPackMain.Requery() ''.Refresh	
                RsPackDetail.Requery() ''.Refresh	
                RsPackExp.Requery()
                Clear1()
                'Call FillSprdOther()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''	
        RsPackMain.Requery() ''.Refresh	
        RsPackDetail.Requery() ''.Refresh	
        RsPackExp.Requery()
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume	
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If PubSuperUser <> "S" Then
            If chkDC.CheckState = System.Windows.Forms.CheckState.Checked Or chkExciseInvoice.CheckState = System.Windows.Forms.CheckState.Checked Or chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Transaction Made Against This Despatch Note So Cann't be Deleted")
                Exit Sub
            End If
        End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPackMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtPackNo.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            'Call FillSprdOther()
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtPackNo_Validating(txtPackNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True And CmdAdd.Visible = True Then CmdAdd.Focus()
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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & ""  '' AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((TxtCustomerName.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR || SUPP_CUST_CITY || SUPP_CUST_STATE", SqlStr) = True Then
            TxtCustomerName.Text = AcName
            txtCustomerCode.Text = AcName1
            txtShipTo.Text = AcName2
            txtCustomerName_Validating(TxtCustomerName, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        Dim xSoNo As Double

        If eventArgs.row = 0 And eventArgs.col = ColSONo And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColSONo
                SqlStr = GetSearchSO()
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColSONo
                    .Text = Trim(AcName)
                    .Col = ColSODate
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColSONo)
            End With
        End If


        If eventArgs.row = 0 And eventArgs.col = ColItemCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColSONo
                xSoNo = Val(.Text)

                .Col = ColItemCode
                SqlStr = GetSearchItem("Y", IIf(xSoNo = 0, Val(txtSONo.Text), xSoNo))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)
                    .Col = ColItemDesc
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColSONo
                xSoNo = Val(.Text)

                .Col = ColItemDesc
                SqlStr = GetSearchItem("N", Val(txtSONo.Text))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = Trim(AcName)
                    .Col = ColItemCode
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        Dim DelStatus As Boolean
        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode, DelStatus)
                FormatSprdMain(-1)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
            End If
        End If

    End Sub

    Private Sub SprdOther_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdOther.ClickEvent

        Dim SqlStr As String = ""
        Dim mFieldName As String = ""

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If eventArgs.row = 0 And eventArgs.col = ColOthersValue Then
            With SprdOther
                .Row = .ActiveRow
                .Col = ColOthersName
                mFieldName = Trim(.Text)

                If Trim(mFieldName) <> "" Then
                    SqlStr = "SELECT FIELD_VALUE " & vbCrLf _
                            & " FROM FIN_EXPORT_FIELD_MST" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND FIELD_NAME='" & mFieldName & "'"

                    If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColOthersValue
                        .Text = Trim(AcName)
                    End If
                End If
                MainClass.SetFocusToCell(SprdOther, SprdOther.ActiveRow, ColOthersValue)
            End With
        End If


    End Sub

    Private Function GetSearchItem(ByRef mByCode As String, ByRef pSONo As Double) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String

        '    xSuppCode = IIf(Trim(txtCustomerCode.Text) = "", "-1", Trim(txtCustomerCode.Text))	

        If MainClass.ValidateWithMasterTable((txtBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSuppCode = MasterNo
        Else
            xSuppCode = "-1"
        End If

        If mByCode = "Y" Then
            mSqlStr = "SELECT A.ITEM_CODE,A.ITEM_SHORT_DESC "
        Else
            mSqlStr = "SELECT A.ITEM_SHORT_DESC,A.ITEM_CODE "
        End If

        If pSONo = 0 Then
            mSqlStr = mSqlStr & vbCrLf & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf _
                & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
                & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf _
                & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'"
        Else
            mSqlStr = mSqlStr & vbCrLf _
                & " FROM DSP_SALEORDER_HDR B, DSP_SALEORDER_DET C, INV_ITEM_MST A " & vbCrLf _
                & " WHERE B.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND B.COMPANY_CODE=C.COMPANY_CODE " & vbCrLf _
                & " AND B.MKEY=C.MKEY " & vbCrLf & " AND C.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
                & " AND C.ITEM_CODE=A.ITEM_CODE " & vbCrLf _
                & " AND B.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xSuppCode) & "'" & vbCrLf _
                & " AND B.AUTO_KEY_SO=" & Val(CStr(pSONo)) & " AND B.SO_STATUS='O' AND SO_APPROVED='Y'" '& vbCrLf |                & " AND A.ITEM_CODE LIKE '" & pItemCode & "%'"	
        End If
        GetSearchItem = mSqlStr
        Exit Function
ErrPart:
        GetSearchItem = ""

    End Function

    Private Function GetSearchSO() As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String

        '    xSuppCode = IIf(Trim(txtCustomerCode.Text) = "", "-1", Trim(txtCustomerCode.Text))	

        If MainClass.ValidateWithMasterTable((txtBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSuppCode = MasterNo
        Else
            xSuppCode = "-1"
        End If

        mSqlStr = "SELECT AUTO_KEY_SO, SO_DATE, CUST_PO_NO, CUST_PO_DATE " & vbCrLf _
            & " FROM DSP_SALEORDER_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & xSuppCode & "' AND SO_STATUS='O' AND SO_APPROVED='Y'"
        GetSearchSO = mSqlStr
        Exit Function
ErrPart:
        GetSearchSO = ""

    End Function
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xICode As String
        Dim xSoNo As Double
        Dim xSoDate As String
        Dim xBuyerPONo As String
        Dim xBuyerPODate As String
        If eventArgs.NewRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColSONo
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColSONo
                xSoNo = Val(SprdMain.Text)
                If xSoNo = 0 Then Exit Sub

                If GetValidSO(xSoNo, xSoDate, xBuyerPONo, xBuyerPODate) = True Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColSODate
                    SprdMain.Text = VB6.Format(xSoDate, "DD/MM/YYYY")

                    SprdMain.Col = ColBuyerPO
                    SprdMain.Text = Trim(xBuyerPONo)

                    SprdMain.Col = ColBuyerDate
                    SprdMain.Text = VB6.Format(xBuyerPODate, "DD/MM/YYYY")

                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColSONo)
                End If

            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColSONo
                xSoNo = Val(SprdMain.Text)

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                If GetValidItem(xICode, xSoNo) = True Then
                    '                If CheckDuplicateItem(xICode) = False Then	
                    If FillGridRow(xICode, (SprdMain.ActiveRow)) = False Then Exit Sub
                    '                End If	
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If
            Case ColPalletNo
                If CheckDSBalQty() = False Then
                    MsgInformation("Schedule Qty is less then Packing Qty.")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPalletNo)
                End If
                Call CalcQty()
            Case ColPackQty
                If CheckDSBalQty() = False Then
                    MsgInformation("Schedule Qty is less then Packing Qty.")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPalletNo)
                End If
                Call CalcQty()
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdOther_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdOther.LeaveCell

        On Error GoTo ErrPart
        Dim mName As String = ""
        Dim mValue As String = ""
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColOthersValue
                SprdOther.Row = SprdOther.ActiveRow

                SprdOther.Col = ColOthersName
                mName = SprdOther.Text

                If mName = "" Then Exit Sub

                SprdOther.Col = ColOthersValue
                mValue = SprdOther.Text

                If mValue = "" Then Exit Sub

                SqlStr = "SELECT FIELD_VALUE " & vbCrLf _
                        & " FROM FIN_EXPORT_FIELD_MST" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND FIELD_NAME='" & mName & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    If MainClass.ValidateWithMasterTable(mValue, "FIELD_VALUE", "FIELD_VALUE", "FIN_EXPORT_FIELD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FIELD_NAME='" & mName & "'") = False Then
                        MsgInformation("Please Select Vaild Value.")
                        MainClass.SetFocusToCell(SprdOther, eventArgs.row, ColOthersValue)
                    End If
                End If


        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetValidItem(ByRef pItemCode As String, ByRef pSONo As Double) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        '    xSuppCode = IIf(Trim(txtCustomerCode.Text) = "", "-1", Trim(txtCustomerCode.Text))	
        If MainClass.ValidateWithMasterTable((txtBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSuppCode = MasterNo
        Else
            xSuppCode = "-1"
        End If


        '     mSqlStr = "SELECT AUTO_KEY_SO, SO_DATE, CUST_PO_NO, CUST_PO_DATE " & vbCrLf _	
        ''            & " FROM DSP_SALEORDER_HDR" & vbCrLf _	
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xSuppCode) & "'" & vbCrLf _	
        ''            & " AND AUTO_KEY_SO=" & pSONo & ""	



        If pSONo = 0 Then
            mSqlStr = "SELECT B.ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'" & vbCrLf & " AND B.ITEM_CODE LIKE '" & pItemCode & "%'"
        Else
            mSqlStr = "SELECT A.ITEM_CODE " & vbCrLf _
                & " FROM DSP_SALEORDER_HDR B, DSP_SALEORDER_DET C, INV_ITEM_MST A " & vbCrLf _
                & " WHERE B.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND B.COMPANY_CODE=C.COMPANY_CODE " & vbCrLf _
                & " AND B.MKEY=C.MKEY " & vbCrLf _
                & " AND C.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
                & " AND C.ITEM_CODE=A.ITEM_CODE " & vbCrLf _
                & " AND B.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xSuppCode) & "'" & vbCrLf _
                & " AND B.AUTO_KEY_SO=" & Val(CStr(pSONo)) & " AND SO_APPROVED='Y'" & vbCrLf _
                & " AND A.ITEM_CODE LIKE '" & pItemCode & "%'"
        End If
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetValidItem = True
        Else
            MsgInformation("Please Check Item.")
            GetValidItem = False
        End If

        Exit Function
ErrPart:
        GetValidItem = False
    End Function


    Private Function GetValidSO(ByRef pSONo As Double, ByRef pSoDate As String, ByRef pBuyerPONo As String, ByRef pBuyerPODate As String) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If MainClass.ValidateWithMasterTable((txtBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSuppCode = MasterNo
        Else
            xSuppCode = "-1"
        End If

        '    xSuppCode = IIf(Trim(txtCustomerCode.Text) = "", "-1", Trim(txtCustomerCode.Text))	
        '	

        mSqlStr = "SELECT AUTO_KEY_SO, SO_DATE, CUST_PO_NO, CUST_PO_DATE " & vbCrLf & " FROM DSP_SALEORDER_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xSuppCode) & "'" & vbCrLf & " AND AUTO_KEY_SO=" & pSONo & " AND SO_APPROVED='Y'"


        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pSoDate = IIf(IsDbNull(RsTemp.Fields("SO_DATE").Value), "", RsTemp.Fields("SO_DATE").Value)
            pBuyerPONo = IIf(IsDbNull(RsTemp.Fields("CUST_PO_NO").Value), "", RsTemp.Fields("CUST_PO_NO").Value)
            pBuyerPODate = IIf(IsDbNull(RsTemp.Fields("CUST_PO_DATE").Value), "", RsTemp.Fields("CUST_PO_DATE").Value)
            GetValidSO = True
        Else
            MsgInformation("Please Check Sale Order.")
            GetValidSO = False
        End If

        Exit Function
ErrPart:
        GetValidSO = False
    End Function

    Private Function GetValidSOItem(ByRef pSONo As Double, ByRef pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        '    xSuppCode = IIf(Trim(txtCustomerCode.Text) = "", "-1", Trim(txtCustomerCode.Text))	
        If MainClass.ValidateWithMasterTable((txtBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSuppCode = MasterNo
        Else
            xSuppCode = "-1"
        End If

        mSqlStr = "SELECT IH.AUTO_KEY_SO" & vbCrLf & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xSuppCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.AUTO_KEY_SO=" & pSONo & " AND SO_APPROVED='Y'"


        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetValidSOItem = True
        Else
            GetValidSOItem = False
        End If

        Exit Function
ErrPart:
        GetValidSOItem = False
    End Function
    Private Function FillGridRow(ByRef mItemCode As String, ByRef pCurrentRow As Integer) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mSoNo As Double
        Dim mDSQty As Double
        Dim mCheckRowData As String
        Dim mDrawingNo As String
        Dim mModelNo As String
        Dim mActualHeight As Double
        Dim mActualWidth As String


        If mItemCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " Select INVMST.* " & vbCrLf _
            & " FROM INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE " & vbCrLf & " INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND INVMST.ITEM_CODE='" & Trim(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc
                SprdMain.Row = pCurrentRow

                SprdMain.Col = ColSONo
                mSoNo = Val(SprdMain.Text)

                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDbNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)

                SprdMain.Col = ColPartNo
                SprdMain.Text = IIf(IsDbNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)

                mCheckRowData = mItemCode

                SprdMain.Col = ColGlassDescription
                mCheckRowData = mCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                'SprdMain.Col = ColActualHeight
                'mCheckRowData = mCheckRowData & Val(SprdMain.Text)

                'SprdMain.Col = ColActualWidth
                'mCheckRowData = mCheckRowData & Val(SprdMain.Text)


                SprdMain.Col = ColSize
                mCheckRowData = mCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                SprdMain.Col = ColModelNo
                mCheckRowData = mCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                SprdMain.Col = ColDrawingNo
                mCheckRowData = mCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)


                If CheckDuplicateRow(mSoNo, mCheckRowData) = True Then
                    FillGridRow = False
                    Exit Function
                End If



                SprdMain.Col = ColDrawingNo
                mDrawingNo = MainClass.AllowSingleQuote(SprdMain.Text)

                SprdMain.Row = pCurrentRow

                SprdMain.Col = ColModelNo
                mModelNo = MainClass.AllowSingleQuote(SprdMain.Text)


                SprdMain.Col = ColActualHeight
                mActualHeight = Val(SprdMain.Text)

                SprdMain.Col = ColActualWidth
                mActualWidth = Val(SprdMain.Text)

                mDSQty = GetSalesDSQty(mItemCode, mSoNo, mModelNo, mDrawingNo, mActualHeight, mActualWidth)
                SprdMain.Row = pCurrentRow
                SprdMain.Col = ColBuyerPOQty
                SprdMain.Text = VB6.Format(mDSQty, "0.00")


                MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                FormatSprdMain(-1)

            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Function GetSalesDSQty(ByRef pItemCode As String, ByRef pSONo As Double, ByRef pModelNo As String, ByRef pDrawingNo As String, ByRef pActualHeight As Double, ByRef pActualWidth As Double) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOrderType As String
        Dim mBuyerCode As String

        GetSalesDSQty = 0
        If MainClass.ValidateWithMasterTable((txtBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBuyerCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(Val(CStr(pSONo)), "AUTO_KEY_SO", "ORDER_TYPE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & Trim(mBuyerCode) & "' AND SO_APPROVED='Y'") = True Then
            mOrderType = MasterNo
        Else
            mOrderType = "O"
        End If

        If mOrderType = "C" And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then

            mSqlStr = " SELECT POD.SO_QTY" & vbCrLf _
              & " FROM DSP_SALEORDER_HDR POM,DSP_SALEORDER_DET POD " & vbCrLf _
              & " WHERE POM.MKEY = POD.MKEY " & vbCrLf _
              & " AND POM.AUTO_KEY_SO=" & Val(pSONo) & " AND SO_APPROVED='Y' AND GOODS_SERVICE='G'"

            mSqlStr = mSqlStr & vbCrLf & " AND POM.SUPP_CUST_CODE='" & mBuyerCode & "' "

            mSqlStr = mSqlStr & vbCrLf & " AND POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND POM.SO_STATUS='O' "

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                If Trim(pModelNo) <> "" Then
                    mSqlStr = mSqlStr & vbCrLf & " AND ITEM_MODEL='" & pModelNo & "' "
                End If

                If Trim(pDrawingNo) <> "" Then
                    mSqlStr = mSqlStr & vbCrLf & " AND ITEM_DRAWINGNO='" & pDrawingNo & "' "
                End If

                If Val(pActualHeight) > 0 Then
                    mSqlStr = mSqlStr & vbCrLf & " AND ACTUAL_HEIGHT=" & pActualHeight & " "
                End If

                If Val(pActualWidth) > 0 Then
                    mSqlStr = mSqlStr & vbCrLf & " AND ACTUAL_WIDTH=" & pActualWidth & " "
                End If

            End If
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsTemp.EOF Then
                GetSalesDSQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SO_QTY").Value), 0, RsTemp.Fields("SO_QTY").Value), "0.00"))
            End If
        Else
                mSqlStr = " SELECT SUM(ITEM_QTY) AS ITEM_QTY " & vbCrLf _
                    & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DELV_SCHLD_DET ID " & vbCrLf _
                    & " WHERE IH.AUTO_KEY_DELV = ID.AUTO_KEY_DELV" & vbCrLf _
                    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mBuyerCode) & "'" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                    & " AND IH.AUTO_KEY_SO=" & Val(CStr(pSONo)) & ""

            If mOrderType = "C" Then
                '        mSqlStr = mSqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')<='" & vb6.Format(txtDNDate, "YYYYMM") & "'"	
            Else
                mSqlStr = mSqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')='" & VB6.Format(txtPackDate.Text, "YYYYMM") & "'"
            End If

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                GetSalesDSQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))
            End If
        End If



        mSqlStr = " SELECT SUM(PACKED_QTY) AS ITEM_QTY " & vbCrLf _
            & " FROM DSP_PACKING_HDR IH, DSP_PACKING_DET ID " & vbCrLf _
            & " WHERE IH.AUTO_KEY_PACK = ID.AUTO_KEY_PACK" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.BUYER_CODE='" & MainClass.AllowSingleQuote(mBuyerCode) & "'" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND ID.AUTO_KEY_SO=" & Val(CStr(pSONo)) & ""

        If Val(txtPackNo.Text) <> 0 Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_PACK<>" & Val(txtPackNo.Text) & ""
        End If


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            If Trim(pModelNo) <> "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND ITEM_MODEL='" & pModelNo & "' "
            End If

            If Trim(pDrawingNo) <> "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND ITEM_DRAWINGNO='" & pDrawingNo & "' "
            End If

            If Val(pActualHeight) > 0 Then
                mSqlStr = mSqlStr & vbCrLf & " AND ACTUAL_HEIGHT=" & pActualHeight & " "
            End If

            If Val(pActualWidth) > 0 Then
                mSqlStr = mSqlStr & vbCrLf & " AND ACTUAL_WIDTH=" & pActualWidth & " "
            End If

        End If

        If mOrderType = "C" Then
            '        mSqlStr = mSqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')<='" & vb6.Format(txtDNDate, "YYYYMM") & "'"	
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND TO_CHAR(IH.PACK_DATE,'YYYYMM')='" & VB6.Format(txtPackDate.Text, "YYYYMM") & "'"
        End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSalesDSQty = GetSalesDSQty - CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.Row

            .Col = 1
            txtPackNo.Text = CStr(Val(.Text))

            txtPackNo_Validating(txtPackNo, New System.ComponentModel.CancelEventArgs(False))
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Sub txtPackDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPackDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPackDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPackDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Not IsDate(txtPackDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtPackDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPackNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPackNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtPackNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPackNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mMRRNo As String
        Dim SqlStr As String = ""

        If Trim(txtPackNo.Text) = "" Then GoTo EventExitSub

        If Len(txtPackNo.Text) < 6 Then
            txtPackNo.Text = VB6.Format(Val(txtPackNo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsPackMain.EOF = False Then xMkey = RsPackMain.Fields("AUTO_KEY_PACK").Value
        mMRRNo = Trim(txtPackNo.Text)

        SqlStr = " SELECT * FROM DSP_PACKING_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PACK,LENGTH(AUTO_KEY_PACK)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PACK=" & Val(mMRRNo) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPackMain.EOF = False Then
            Clear1()
            'Call FillSprdOther()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such MRR, Use Generate MRR Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM DSP_PACKING_HDR " & " WHERE AUTO_KEY_PACK=" & Val(xMkey) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""
        Dim mVNoSeq As Double
        Dim mSuppCustCode As String
        Dim mDCMade As String
        Dim mEXP_INV_MADE As String
        Dim mEXCISE_INV_MADE As String
        Dim mBuyerCode As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        PubDBCn.Execute("Delete From DSP_PACKING_DET Where AUTO_KEY_PACK='" & lblMkey.Text & "'")
        PubDBCn.Execute("Delete From FIN_PACKING_PARA_EXP Where AUTO_KEY_PACK='" & LblMkey.Text & "'")


        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((txtCustomerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        If MainClass.ValidateWithMasterTable((txtBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBuyerCode = MasterNo
        End If

        mDCMade = IIf(chkDC.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mEXCISE_INV_MADE = IIf(chkExciseInvoice.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mEXP_INV_MADE = IIf(chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If Val(txtPackNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtPackNo.Text)
        End If

        txtPackNo.Text = CStr(Val(CStr(mVNoSeq)))

        ''Temp. Commit.....	
        If CheckValidVDate(mVNoSeq) = False Then GoTo ErrPart

        SqlStr = ""

        If ADDMode = True Then
            lblMkey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO DSP_PACKING_HDR( " & vbCrLf _
                & " COMPANY_CODE, FYEAR, AUTO_KEY_PACK," & vbCrLf _
                & " PACK_DATE, SUPP_CUST_CODE, REF_NO," & vbCrLf _
                & " BUYER_PO, BUYER_PO_DATE, " & vbCrLf _
                & " EXCISE_INV_NO, EXCISE_INV_DATE," & vbCrLf _
                & " INVOICE_NO, INVOICE_DATE," & vbCrLf _
                & " ORIGIN_COUNTRY, DEST_COUNTRY, CARRIAGE," & vbCrLf _
                & " LOADINGPORT, DISCHARGEPORT, FINALDEST," & vbCrLf _
                & " PAYMENTTERMS, FLIGHT_NO, CONTAINERNO," & vbCrLf _
                & " RECIPT_PLACE, REMARKS, " & vbCrLf _
                & " DC_MADE, EXCISE_INV_MADE, EXP_INV_MADE, BUYER_CODE, " & vbCrLf _
                & " NOTIFY_PARTY_1, NOTIFY_PARTY_2, NOTIFY_PARTY_3," & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE,AUTO_KEY_SO, SO_DATE, BILL_TO_LOC_ID, SHIP_TO_LOC_ID )"

            SqlStr = SqlStr & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & Val(CStr(mVNoSeq)) & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtPackDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', '" & MainClass.AllowSingleQuote((txtIECNo.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtBuyerNo.Text)) & "', TO_DATE('" & VB6.Format(txtBuyerDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtExciseBillNo.Text)) & "', TO_DATE('" & VB6.Format(txtExciseBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtInvNo.Text)) & "', TO_DATE('" & VB6.Format(txtInvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtOrigin.Text)) & "', '" & MainClass.AllowSingleQuote((txtDestination.Text)) & "', '" & MainClass.AllowSingleQuote((txtCarriage.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtLoading.Text)) & "', '" & MainClass.AllowSingleQuote((txtDischarge.Text)) & "', '" & MainClass.AllowSingleQuote((txtFinalDestination.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtPayments.Text)) & "', '" & MainClass.AllowSingleQuote((txtFlight.Text)) & "', '" & MainClass.AllowSingleQuote((txtContainerNo.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtPlace.Text)) & "', '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "'," & vbCrLf _
                & " '" & mDCMade & "', '" & mEXCISE_INV_MADE & "', '" & mEXP_INV_MADE & "', '" & MainClass.AllowSingleQuote(mBuyerCode) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtNotifyParty1.Text)) & "', '" & MainClass.AllowSingleQuote((txtNotifyParty2.Text)) & "', '" & MainClass.AllowSingleQuote((txtNotifyParty3.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & Val(txtSONo.Text) & ",TO_DATE('" & VB6.Format(txtSODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtBillTo.Text) & "','" & MainClass.AllowSingleQuote(txtShipTo.Text) & "')"

        ElseIf MODIFYMode = True Then

            SqlStr = ""
            SqlStr = "UPDATE DSP_PACKING_HDR SET " & vbCrLf _
                & " AUTO_KEY_PACK =" & Val(CStr(mVNoSeq)) & " ," & vbCrLf _
                & " PACK_DATE=TO_DATE('" & VB6.Format(txtPackDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'," & vbCrLf _
                & " REF_NO='" & MainClass.AllowSingleQuote((txtIECNo.Text)) & "'," & vbCrLf _
                & " BUYER_PO='" & MainClass.AllowSingleQuote((txtBuyerNo.Text)) & "', " & vbCrLf _
                & " BUYER_PO_DATE=TO_DATE('" & VB6.Format(txtBuyerDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EXCISE_INV_NO='" & MainClass.AllowSingleQuote((txtExciseBillNo.Text)) & "', " & vbCrLf _
                & " EXCISE_INV_DATE=TO_DATE('" & VB6.Format(txtExciseBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " INVOICE_NO='" & MainClass.AllowSingleQuote((txtInvNo.Text)) & "', " & vbCrLf _
                & " INVOICE_DATE=TO_DATE('" & VB6.Format(txtInvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " ORIGIN_COUNTRY='" & MainClass.AllowSingleQuote((txtOrigin.Text)) & "', " & vbCrLf _
                & " DEST_COUNTRY='" & MainClass.AllowSingleQuote((txtDestination.Text)) & "', " & vbCrLf _
                & " CARRIAGE='" & MainClass.AllowSingleQuote((txtCarriage.Text)) & "'," & vbCrLf _
                & " LOADINGPORT='" & MainClass.AllowSingleQuote((txtLoading.Text)) & "', " & vbCrLf _
                & " DISCHARGEPORT='" & MainClass.AllowSingleQuote((txtDischarge.Text)) & "', " & vbCrLf _
                & " FINALDEST='" & MainClass.AllowSingleQuote((txtFinalDestination.Text)) & "'," & vbCrLf _
                & " PAYMENTTERMS='" & MainClass.AllowSingleQuote((txtPayments.Text)) & "', " & vbCrLf _
                & " FLIGHT_NO='" & MainClass.AllowSingleQuote((txtFlight.Text)) & "', " & vbCrLf _
                & " CONTAINERNO='" & MainClass.AllowSingleQuote((txtContainerNo.Text)) & "'," & vbCrLf _
                & " RECIPT_PLACE='" & MainClass.AllowSingleQuote((txtPlace.Text)) & "', " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "' , SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtShipTo.Text) & "', "

            SqlStr = SqlStr & vbCrLf & " DC_MADE='" & mDCMade & "', " & vbCrLf _
                & " EXCISE_INV_MADE='" & mEXCISE_INV_MADE & "', " & vbCrLf _
                & " EXP_INV_MADE='" & mEXP_INV_MADE & "'," & vbCrLf _
                & " BUYER_CODE='" & MainClass.AllowSingleQuote(mBuyerCode) & "'," & vbCrLf _
                & " NOTIFY_PARTY_1='" & MainClass.AllowSingleQuote(txtNotifyParty1.Text) & "'," & vbCrLf _
                & " NOTIFY_PARTY_2='" & MainClass.AllowSingleQuote(txtNotifyParty2.Text) & "'," & vbCrLf _
                & " NOTIFY_PARTY_3='" & MainClass.AllowSingleQuote(txtNotifyParty3.Text) & "',"

            SqlStr = SqlStr & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), AUTO_KEY_SO=" & Val(txtSONo.Text) & ",SO_DATE=TO_DATE('" & VB6.Format(txtSODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

            SqlStr = SqlStr & vbCrLf & " WHERE AUTO_KEY_PACK ='" & MainClass.AllowSingleQuote(LblMkey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(mVNoSeq) = False Then GoTo ErrPart
        UpdateMain1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''	
        RsPackMain.Requery() ''.Refresh	
        RsPackDetail.Requery() ''.Refresh	
        RsPackExp.Requery()
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume	
    End Function

    Private Function CheckValidVDate(ByRef pDNNoSeq As Double) As Object

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mRsCheck1 As ADODB.Recordset
        Dim mRsCheck2 As ADODB.Recordset
        Dim mBackBillDate As String
        Dim mMaxInvStrfNo As Integer
        CheckValidVDate = True


        If CDate(txtPackDate.Text) <= CDate("30-jun-2022") Then
            CheckValidVDate = True
            Exit Function
        End If
        If txtPackNo.Text = 1 & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") Then Exit Function

        SqlStr = "SELECT MAX(PACK_DATE)" & vbCrLf & " FROM DSP_PACKING_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PACK,LENGTH(AUTO_KEY_PACK)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PACK<" & Val(CStr(pDNNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDbNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(PACK_DATE)" & " FROM DSP_PACKING_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PACK,LENGTH(AUTO_KEY_PACK)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PACK>" & Val(CStr(pDNNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck1.EOF = False And Not IsDbNull(mRsCheck1.Fields(0).Value) And mRsCheck2.EOF = False And Not IsDbNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtPackDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Despatch Note Date Is Greater Than The Despatch Note Date Of Next Despatch Note No.")
                CheckValidVDate = False
            ElseIf CDate(txtPackDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Despatch Note Date Is Less Than The Despatch Note Date Of Previous Despatch Note No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck1.EOF = False And Not IsDbNull(mRsCheck1.Fields(0).Value) Then
            If CDate(txtPackDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Despatch Note Date Is Greater Than The Despatch Note Date Of Next Despatch Note No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck2.EOF = False And Not IsDbNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtPackDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Despatch Note Date Is Less Than The Despatch Note Date Of Previous Despatch Note No.")
                CheckValidVDate = False
            End If
        End If

        Exit Function
CheckERR:
        CheckValidVDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsPackMainGen As ADODB.Recordset
        Dim mNewSeqNo As Double
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_PACK)  " & vbCrLf & " FROM DSP_PACKING_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPackMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CDbl(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoGenSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateDetail1(ByRef pVnoseq As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mSubRowNo As Integer
        Dim mItemCode As String
        Dim mUnit As String
        Dim mMarks As String
        Dim mPalletNo As Double
        Dim mPktQty As Double
        Dim mQty As Double
        Dim mNetWt As Double
        Dim mGrossWt As Double
        Dim mSoNo As Double
        Dim mSODate As String
        Dim mBuyerPO As String
        Dim mBuyerDATE As String

        Dim mSize As String
        Dim mModelNo As String
        Dim mDrawingNo As String
        Dim mGlassDescription As String

        Dim mActualHeight As Double
        Dim mActualWidth As Double

        Dim mChargeableHeight As Double
        Dim mChargeableWidth As Double
        Dim mGlassArea As Double

        mSubRowNo = 0

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColMarks
                mMarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColPalletNo
                mPalletNo = Val(.Text)

                .Col = ColPackQty
                mPktQty = Val(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColNetWt
                mNetWt = Val(.Text)

                .Col = ColGrossWt
                mGrossWt = Val(.Text)

                .Col = ColSONo
                mSoNo = Val(.Text)

                .Col = ColSODate
                mSODate = Trim(.Text)

                .Col = ColBuyerPO
                mBuyerPO = Trim(.Text)

                .Col = ColBuyerDate
                mBuyerDATE = Trim(.Text)

                .Col = ColGlassDescription
                mGlassDescription = MainClass.AllowSingleQuote(.Text)

                .Col = ColActualHeight
                mActualHeight = Val(.Text)

                .Col = ColActualWidth
                mActualWidth = Val(.Text)

                .Col = ColSize
                mSize = MainClass.AllowSingleQuote(.Text)

                .Col = ColModelNo
                mModelNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColDrawingNo
                mDrawingNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColChargeableHeight
                mChargeableHeight = Val(.Text)

                .Col = ColChargeableWidth
                mChargeableWidth = Val(.Text)

                .Col = ColGlassArea
                mGlassArea = Val(.Text)


                SqlStr = ""

                If mItemCode <> "" And mQty > 0 Then
                    mSubRowNo = mSubRowNo + 1
                    SqlStr = " INSERT INTO DSP_PACKING_DET ( " & vbCrLf _
                        & " COMPANY_CODE, AUTO_KEY_PACK, " & vbCrLf _
                        & " SERIAL_NO, ITEM_CODE," & vbCrLf _
                        & " ITEM_UOM, MARKS, " & vbCrLf _
                        & " PALLETNO," & vbCrLf _
                        & " PACKED_QTY, NO_OF_PACKETS, " & vbCrLf _
                        & " NET_WT, GROSS_WT," & vbCrLf _
                        & " AUTO_KEY_SO,SO_DATE,CUST_PO_NO,CUST_PO_DATE," & vbCrLf _
                        & " GLASS_DESC, ACTUAL_HEIGHT, ACTUAL_WIDTH," & vbCrLf _
                        & " ITEM_SIZE, ITEM_MODEL, ITEM_DRAWINGNO, CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH, GLASS_AREA)" & vbCrLf


                    SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(pVnoseq)) & ", " & vbCrLf _
                        & " " & mSubRowNo & ", '" & mItemCode & "', " & vbCrLf _
                        & " '" & mUnit & "', '" & MainClass.AllowSingleQuote(mMarks) & "', " & vbCrLf _
                        & " " & mPalletNo & ",  " & vbCrLf _
                        & " " & mQty & ", " & mPktQty & ", " & vbCrLf _
                        & " " & mNetWt & ", " & mGrossWt & "," & vbCrLf _
                        & " " & Val(CStr(mSoNo)) & ", TO_DATE('" & VB6.Format(mSODate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mBuyerPO) & "', TO_DATE('" & VB6.Format(mBuyerDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " '" & mGlassDescription & "', " & mActualHeight & ", " & mActualWidth & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mSize) & "', '" & MainClass.AllowSingleQuote(mModelNo) & "', '" & MainClass.AllowSingleQuote(mDrawingNo) & "'," & vbCrLf _
                        & " " & mChargeableHeight & ", " & mChargeableWidth & ", " & mGlassArea & ")"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        Dim mOthersValue As String = ""
        Dim mOthersName As String = ""
        With SprdOther
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColOthersName
                mOthersName = MainClass.AllowSingleQuote(.Text)

                .Col = ColOthersValue
                mOthersValue = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mOthersName) <> "" Then
                    SqlStr = " INSERT INTO FIN_PACKING_PARA_EXP ( " & vbCrLf _
                    & " AUTO_KEY_PACK, SERAIL_NO, FIELD_NAME, FIELD_VALUE ) " & vbCrLf _
                    & " VALUES ( " & Val(pVnoseq) & ", " & I & ", '" & mOthersName & "', '" & mOthersValue & "' )"

                PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim cntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim xPORate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xRateDiffCN As Double
        Dim mExciseDutyAmt As Double
        Dim mSalesTaxAmount As Double
        Dim mWithInState As String
        Dim pSONo As Double
        Dim mItemCode As String

        FieldsVarification = True
        If ValidateBranchLocking((txtPackDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, txtPackDate.Text, (txtCustomerName.Text), mCustomerCode) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPackMain.EOF = True Then Exit Function

        If MODIFYMode = True And txtPackNo.Text = "" Then
            MsgInformation("Voucher No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtPackDate.Text = "" Then
            MsgBox("VDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPackDate.Focus()
            Exit Function
        ElseIf FYChk((txtPackDate.Text)) = False Then
            FieldsVarification = False
            If txtPackDate.Enabled = True Then txtPackDate.Focus()
            Exit Function
        End If

        If Trim(txtCustomerName.Text) = "" Then
            MsgBox("Supplier Cannot Be Blank", MsgBoxStyle.Information)
            ' TxtCustomerName.SetFocus	
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtOrigin.Text) = "" Then
            MsgBox("Origin Cannot Be Blank", MsgBoxStyle.Information)
            txtOrigin.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDestination.Text) = "" Then
            MsgBox("Destination Cannot Be Blank", MsgBoxStyle.Information)
            txtDestination.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgBox("Buyer Location Cannot Be Blank", MsgBoxStyle.Information)
            txtBillTo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtShipTo.Text) = "" Then
            MsgBox("Consignee Cannot Be Blank", MsgBoxStyle.Information)
            txtShipTo.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If MODIFYMode = True And PubSuperUser <> "S" Then
            If chkDC.CheckState = System.Windows.Forms.CheckState.Checked Or chkExciseInvoice.CheckState = System.Windows.Forms.CheckState.Checked Or chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Transaction Made Against This Despatch Note So Cann't be Changed.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110 Then

        Else
            If txtSONo.Text = "" Then
                MsgBox("Sales Order No. is Blank", MsgBoxStyle.Information)
                FieldsVarification = False
                txtSONo.Focus()
                Exit Function
            End If

            If Trim(txtBuyerNo.Text) = "" Then
                MsgBox("Customer Sale Order No. is Blank", MsgBoxStyle.Information)
                FieldsVarification = False
                txtSONo.Focus()
                Exit Function
            End If
        End If


        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If mItemCode <> "" Then
                    .Col = ColSONo
                    pSONo = Val(.Text)

                    If Val(CStr(pSONo)) = 0 Then ''As per Naveen Mail as on 05-02-2013 ''Sales Order is must.	
                        MsgInformation("Invalid Order No for Such Buyer.")
                        FieldsVarification = False
                        Exit Function
                    Else
                        If GetValidSOItem(pSONo, mItemCode) = False Then
                            MsgInformation("Invalid Order No Or Item Code for Such Buyer.")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If
            Next
        End With

        If CheckDSBalQty = False Then
            MsgInformation("Schedule Qty is less then Packing Qty.")
            FieldsVarification = False
            Exit Function
        End If
        Call CalcQty()

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        'If MainClass.ValidDataInGrid(SprdMain, ColPackQty, "N", "Please Check pack Quantity.") = False Then FieldsVarification = False : Exit Function
        'If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmPackingNote_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Packing List"

        SqlStr = ""
        SqlStr = "Select * from DSP_PACKING_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from DSP_PACKING_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from FIN_PACKING_PARA_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackExp, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mCustomerCode = CStr(-1)


        CmdAdd.Visible = True
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())


        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume	
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""
        SqlStr = ""

        MainClass.ClearGrid(SprdView)

        SqlStr = "Select IH.AUTO_KEY_PACK REF_NO, IH.PACK_DATE AS REF_DATE, IH.SUPP_CUST_CODE, AC.SUPP_CUST_NAME AS CustomerName, " & vbCrLf _
            & " IH.REF_NO, IH.BUYER_PO, IH.BUYER_PO_DATE, IH.EXCISE_INV_NO, IH.EXCISE_INV_DATE," & vbCrLf _
            & " IH.INVOICE_NO, IH.INVOICE_DATE, IH.ORIGIN_COUNTRY, IH.DEST_COUNTRY, IH.CARRIAGE, IH.LOADINGPORT, IH.DISCHARGEPORT, " & vbCrLf _
            & " IH.FINALDEST, IH.PAYMENTTERMS, IH.FLIGHT_NO, IH.CONTAINERNO, IH.RECIPT_PLACE, IH.REMARKS, IH.DC_MADE, IH.EXCISE_INV_MADE, IH.EXP_INV_MADE, " & vbCrLf _
            & " IH.BUYER_CODE, IH.NOTIFY_PARTY_1, IH.NOTIFY_PARTY_2, IH.NOTIFY_PARTY_3, " & vbCrLf _
            & " ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, ID.ITEM_UOM, ID.MARKS,ID.PALLETNO,ID.PACKED_QTY,ID.NO_OF_PACKETS,ID.NET_WT,ID.GROSS_WT " & vbCrLf _
            & " FROM DSP_PACKING_HDR IH, DSP_PACKING_DET ID, INV_ITEM_MST IMST, FIN_SUPP_CUST_MST AC " & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " And IH.AUTO_KEY_PACK=ID.AUTO_KEY_PACK " & vbCrLf _
            & " And IH.COMPANY_CODE=AC.COMPANY_CODE " & vbCrLf _
            & " And IH.SUPP_CUST_CODE=AC.SUPP_CUST_CODE " & vbCrLf _
            & " And IH.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf _
            & " And ID.ITEM_CODE=IMST.ITEM_CODE " & vbCrLf _
            & " Order by IH.AUTO_KEY_PACK DESC, ID.SERIAL_NO"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume	
    End Sub
    Private Function MakeSQL() As Object
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = ""

        MakeSQL = "SELECT IH.*, ID.*, CMST.*, INVMST.* "

        MakeSQL = MakeSQL & vbCrLf _
            & " FROM DSP_PACKING_HDR IH, DSP_PACKING_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST,  FIN_SUPP_CUST_BUSINESS_MST BMST,INV_ITEM_MST INVMST "

        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " And IH.AUTO_KEY_PACK=ID.AUTO_KEY_PACK " & vbCrLf _
            & " And IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " And IH.COMPANY_CODE=BMST.COMPANY_CODE " & vbCrLf _
            & " And IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE " & vbCrLf _
            & " And IH.SHIP_TO_LOC_ID=BMST.LOCATION_ID " & vbCrLf _
            & " And ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " And ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " And IH.AUTO_KEY_PACK=" & Val(txtPackNo.Text) & ""

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY ID.SERIAL_NO"

        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume	
    End Function
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 600)

            .set_ColWidth(1, 1200)
            .set_ColWidth(2, 1000)
            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 3500)
            .set_ColWidth(5, 1200)
            .set_ColWidth(6, 1500)
            .set_ColWidth(7, 1500)
            .set_ColWidth(8, 1200)
            .set_ColWidth(9, 1200)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColSONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeEditLen = RsPackDetail.Fields("AUTO_KEY_SO").Precision
            .set_ColWidth(ColSONo, 7)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110, False, True)


            .Col = ColSODate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10 ''	
            .set_ColWidth(ColSODate, 6)
            .ColHidden = True

            .Col = ColBuyerPO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPackDetail.Fields("CUST_PO_NO").DefinedSize ''	
            .set_ColWidth(ColBuyerPO, 6)
            .ColHidden = True

            .Col = ColBuyerDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10 ''	
            .set_ColWidth(ColBuyerDate, 6)
            .ColHidden = True

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPackDetail.Fields("ITEM_CODE").DefinedSize ''	
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .ColsFrozen = ColItemDesc
            .set_ColWidth(ColItemDesc, 30)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColPartNo, 12)
            .ColHidden = True

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsPackDetail.Fields("ITEM_UOM").DefinedSize ''	
            .set_ColWidth(ColUnit, 3)

            .Col = ColMarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsPackDetail.Fields("MARKS").DefinedSize
            .set_ColWidth(ColMarks, 5)

            .Col = ColPalletNo
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPalletNo, 5)

            '        .CellType = SS_CELL_TYPE_EDIT	
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII	
            '        .TypeEditMultiLine = True	
            '        .TypeEditLen = RsPackDetail.Fields("PALLETNO").DefinedSize	
            '        .ColWidth(ColPalletNo) = 9	

            .Col = ColBuyerPOQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBuyerPOQty, 7)

            .Col = ColPackQty
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeIntegerMax = CInt("99999999")
            .TypeIntegerMin = CInt("-99999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPackQty, 7)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 8)



            For I = ColNetWt To ColGrossWt
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 9)
            Next
            .Col = ColSize
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPackDetail.Fields("ITEM_SIZE").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColModelNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPackDetail.Fields("ITEM_MODEL").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 10)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColDrawingNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPackDetail.Fields("ITEM_DRAWINGNO").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 12)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)



            .Col = ColGlassDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsPackDetail.Fields("GLASS_DESC").DefinedSize ''				
            .set_ColWidth(.Col, 20)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            For cntCol = ColActualWidth To ColActualHeight
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 7)
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Next

            For cntCol = ColChargeableWidth To ColGlassArea
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 7)
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Next

        End With

        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColPackQty)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColDrawingNo)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQty, ColQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColSODate, ColBuyerDate)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColBuyerPOQty, ColBuyerPOQty)

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsPackDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillSprdOther()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim RS As ADODB.Recordset = Nothing

        SSTInfo.SelectedIndex = 1
        MainClass.ClearGrid(SprdOther)

        SqlStr = "Select * From FIN_EXPORT_PARA_MST ORDER BY SERAIL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            I = 1
            Do While Not RS.EOF

                SprdOther.Row = I

                SprdOther.Col = ColOthersName
                SprdOther.Text = If(IsDBNull(RS.Fields("FIELD_NAME").Value), "", RS.Fields("FIELD_NAME").Value)

                SprdOther.Col = ColOthersDesc
                SprdOther.Text = If(IsDBNull(RS.Fields("FIELD_CAPTION").Value), "", RS.Fields("FIELD_CAPTION").Value)

                SprdOther.Col = ColOthersValue
                SprdOther.Text = If(IsDBNull(RS.Fields("DEFAULT_VALUE").Value), "", RS.Fields("DEFAULT_VALUE").Value)

                RS.MoveNext()
                If RS.EOF = False Then
                    SprdOther.MaxRows = SprdOther.MaxRows + 1
                    I = I + 1
                End If
            Loop
        End If
        SSTInfo.SelectedIndex = 0
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsPackExp.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdOther(ByRef Arow As Integer)

        On Error GoTo ERR1
        SSTInfo.SelectedIndex = 1

        With SprdOther
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 2)


            .Col = ColOthersName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '.TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("FIELD_NAME", "FIN_EXPORT_PARA_MST", PubDBCn)
            .set_ColWidth(ColOthersName, 6)
            .ColHidden = True

            .Col = ColOthersDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("FIELD_CAPTION", "FIN_EXPORT_PARA_MST", PubDBCn)
            .ColsFrozen = ColOthersDesc
            .set_ColWidth(ColOthersDesc, 30)
            .ColHidden = False

            .Col = ColOthersValue
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPackExp.Fields("FIELD_VALUE").DefinedSize
            .set_ColWidth(ColOthersValue, 40)
            .ColHidden = False

        End With

        MainClass.ProtectCell(SprdOther, 1, SprdOther.MaxRows, ColOthersName, ColOthersDesc)

        MainClass.SetSpreadColor(SprdOther, Arow)
        SSTInfo.SelectedIndex = 0
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsPackExp.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckDuplicateRow(ByRef mSoNo As Double, ByRef mCheckRowData As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        Dim xCheckRowData

        If mCheckRowData = "" Or mSoNo = 0 Then CheckDuplicateRow = False : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColSONo
                If Val(.Text) = Val(CStr(mSoNo)) Then
                    .Col = ColItemCode
                    xCheckRowData = MainClass.AllowSingleQuote(SprdMain.Text)

                    SprdMain.Col = ColGlassDescription
                    xCheckRowData = xCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                    'SprdMain.Col = ColActualHeight
                    'mCheckRowData = mCheckRowData & Val(SprdMain.Text)

                    'SprdMain.Col = ColActualWidth
                    'mCheckRowData = mCheckRowData & Val(SprdMain.Text)


                    SprdMain.Col = ColSize
                    xCheckRowData = xCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                    SprdMain.Col = ColModelNo
                    xCheckRowData = xCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                    SprdMain.Col = ColDrawingNo
                    xCheckRowData = xCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                    If UCase(Trim(xCheckRowData)) = UCase(Trim(mCheckRowData)) Then
                        mItemRept = mItemRept + 1
                        If mItemRept > 1 Then
                            CheckDuplicateRow = True
                            MsgInformation("Duplicate Item Code")
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                            Exit Function
                        End If
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsPackMain
            txtPackNo.MaxLength = .Fields("AUTO_KEY_PACK").Precision
            txtPackDate.MaxLength = 10
            TxtCustomerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

            txtCustomerCode.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize
            txtBuyerCode.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize
            txtBuyerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtInvNo.MaxLength = .Fields("INVOICE_NO").DefinedSize
            txtInvDate.MaxLength = 10
            txtExciseBillNo.MaxLength = .Fields("EXCISE_INV_NO").DefinedSize
            txtExciseBillDate.MaxLength = 10
            txtBuyerNo.MaxLength = .Fields("BUYER_PO").DefinedSize
            txtBuyerDate.MaxLength = 10
            txtRemarks.MaxLength = .Fields("REMARKS").DefinedSize
            txtIECNo.MaxLength = .Fields("REF_NO").DefinedSize
            txtOrigin.MaxLength = .Fields("ORIGIN_COUNTRY").DefinedSize
            txtDestination.MaxLength = .Fields("DEST_COUNTRY").DefinedSize
            txtCarriage.MaxLength = .Fields("CARRIAGE").DefinedSize
            txtPlace.MaxLength = .Fields("RECIPT_PLACE").DefinedSize
            txtFlight.MaxLength = .Fields("FLIGHT_NO").DefinedSize
            txtLoading.MaxLength = .Fields("LOADINGPORT").DefinedSize
            txtDischarge.MaxLength = .Fields("DISCHARGEPORT").DefinedSize
            txtFinalDestination.MaxLength = .Fields("FINALDEST").DefinedSize
            txtPayments.MaxLength = .Fields("PAYMENTTERMS").DefinedSize
            txtContainerNo.MaxLength = .Fields("CONTAINERNO").DefinedSize
            txtSONo.MaxLength = .Fields("AUTO_KEY_SO").Precision
            txtSODate.MaxLength = 10

            '        txtNotifyParty1.MaxLength = .Fields("NOTIFY_PARTY_1").DefinedSize	
            '        txtNotifyParty2.MaxLength = .Fields("NOTIFY_PARTY_2").DefinedSize	
            '        txtNotifyParty3.MaxLength = .Fields("NOTIFY_PARTY_3").DefinedSize	

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mBuyerCode As String

        With RsPackMain
            If Not .EOF Then
                LblMkey.Text = .Fields("AUTO_KEY_PACK").Value
                txtPackNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_PACK").Value), "", .Fields("AUTO_KEY_PACK").Value)
                txtPackDate.Text = VB6.Format(IIf(IsDBNull(.Fields("PACK_DATE").Value), "", .Fields("PACK_DATE").Value), "DD/MM/YYYY")

                mCustomerCode = .Fields("SUPP_CUST_CODE").Value
                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtCustomerName.Text = MasterNo
                End If


                txtCustomerCode.Text = Trim(mCustomerCode)

                mBuyerCode = IIf(IsDBNull(.Fields("BUYER_CODE").Value), "", .Fields("BUYER_CODE").Value)
                txtBuyerCode.Text = Trim(mBuyerCode)

                If MainClass.ValidateWithMasterTable(mBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtBuyerName.Text = MasterNo
                Else
                    If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "BUYERCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mBuyerCode = MasterNo
                        If MainClass.ValidateWithMasterTable(mBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            txtBuyerName.Text = MasterNo
                        End If
                    End If
                End If


                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                txtShipTo.Text = IIf(IsDBNull(.Fields("SHIP_TO_LOC_ID").Value), "", .Fields("SHIP_TO_LOC_ID").Value)

                txtInvNo.Text = IIf(IsDBNull(.Fields("INVOICE_NO").Value), "", .Fields("INVOICE_NO").Value)
                txtInvDate.Text = VB6.Format(IIf(IsDBNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                txtExciseBillNo.Text = IIf(IsDBNull(.Fields("EXCISE_INV_NO").Value), "", .Fields("EXCISE_INV_NO").Value)
                txtExciseBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("EXCISE_INV_DATE").Value), "", .Fields("EXCISE_INV_DATE").Value), "DD/MM/YYYY")

                If IsDBNull(.Fields("AUTO_KEY_SO").Value) Then
                    txtSONo.Text = ""
                Else
                    txtSONo.Text = IIf(.Fields("AUTO_KEY_SO").Value = 0, "", .Fields("AUTO_KEY_SO").Value)
                End If

                txtSODate.Text = VB6.Format(IIf(IsDBNull(.Fields("SO_DATE").Value), "", .Fields("SO_DATE").Value), "DD/MM/YYYY")


                txtBuyerNo.Text = IIf(IsDBNull(.Fields("BUYER_PO").Value), "", .Fields("BUYER_PO").Value)
                txtBuyerDate.Text = VB6.Format(IIf(IsDBNull(.Fields("BUYER_PO_DATE").Value), "", .Fields("BUYER_PO_DATE").Value), "DD/MM/YYYY")
                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtIECNo.Text = IIf(IsDBNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                txtOrigin.Text = IIf(IsDBNull(.Fields("ORIGIN_COUNTRY").Value), "", .Fields("ORIGIN_COUNTRY").Value)
                txtDestination.Text = IIf(IsDBNull(.Fields("DEST_COUNTRY").Value), "", .Fields("DEST_COUNTRY").Value)
                txtCarriage.Text = IIf(IsDBNull(.Fields("CARRIAGE").Value), "", .Fields("CARRIAGE").Value)
                txtPlace.Text = IIf(IsDBNull(.Fields("RECIPT_PLACE").Value), "", .Fields("RECIPT_PLACE").Value)
                txtFlight.Text = IIf(IsDBNull(.Fields("FLIGHT_NO").Value), "", .Fields("FLIGHT_NO").Value)
                txtLoading.Text = IIf(IsDBNull(.Fields("LOADINGPORT").Value), "", .Fields("LOADINGPORT").Value)
                txtDischarge.Text = IIf(IsDBNull(.Fields("DISCHARGEPORT").Value), "", .Fields("DISCHARGEPORT").Value)
                txtFinalDestination.Text = IIf(IsDBNull(.Fields("FINALDEST").Value), "", .Fields("FINALDEST").Value)
                txtPayments.Text = IIf(IsDBNull(.Fields("PAYMENTTERMS").Value), "", .Fields("PAYMENTTERMS").Value)
                txtContainerNo.Text = IIf(IsDBNull(.Fields("CONTAINERNO").Value), "", .Fields("CONTAINERNO").Value)

                txtNotifyParty1.Text = IIf(IsDBNull(.Fields("NOTIFY_PARTY_1").Value), "", .Fields("NOTIFY_PARTY_1").Value)
                txtNotifyParty2.Text = IIf(IsDBNull(.Fields("NOTIFY_PARTY_2").Value), "", .Fields("NOTIFY_PARTY_2").Value)
                txtNotifyParty3.Text = IIf(IsDBNull(.Fields("NOTIFY_PARTY_3").Value), "", .Fields("NOTIFY_PARTY_3").Value)

                chkDC.CheckState = IIf(.Fields("DC_MADE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkExciseInvoice.CheckState = IIf(.Fields("Excise_INV_MADE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkExportInv.CheckState = IIf(.Fields("EXp_INV_MADE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                Call ShowDetail1((LblMkey.Text))

                TxtCustomerName.Enabled = IIf(chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                txtCustomerCode.Enabled = IIf(chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                txtBuyerCode.Enabled = IIf(chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                txtBuyerName.Enabled = IIf(chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                cmdsearchBuyer.Enabled = IIf(chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

                cmdsearch.Enabled = IIf(chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

                txtSONo.Enabled = IIf(chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                txtSODate.Enabled = IIf(chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                cmdSearchSo.Enabled = IIf(chkExportInv.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

                txtConsigneeAddress.Text = FillAddressDetail(txtCustomerCode.Text, txtShipTo.Text)
                txtBuyerAddress.Text = FillAddressDetail(txtBuyerCode.Text, txtBillTo.Text)

            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPackMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtPackNo.Enabled = True

        SSTInfo.SelectedIndex = 0
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub
    Private Sub ShowDetail1(ByRef mMKey As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mStockType As String
        Dim mSoNo As Double
        Dim mDate As String
        Dim pModelNo As String
        Dim pDrawingNo As String
        Dim pActualHeight As Double
        Dim pActualWidth As Double

        SSTInfo.SelectedIndex = 0
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM DSP_PACKING_DET " & vbCrLf & " Where AUTO_KEY_PACK=" & Val(mMKey) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsPackDetail
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColSONo
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("AUTO_KEY_SO").Value), 0, .Fields("AUTO_KEY_SO").Value)))
                mSoNo = Val(IIf(IsDBNull(.Fields("AUTO_KEY_SO").Value), 0, .Fields("AUTO_KEY_SO").Value))

                SprdMain.Col = ColSODate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("SO_DATE").Value), "", .Fields("SO_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColBuyerPO
                SprdMain.Text = IIf(IsDBNull(.Fields("CUST_PO_NO").Value), "", .Fields("CUST_PO_NO").Value)

                SprdMain.Col = ColBuyerDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("CUST_PO_DATE").Value), "", .Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = Trim(mItemDesc)

                SprdMain.Col = ColPartNo
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mPartNo = MasterNo
                SprdMain.Text = Trim(mPartNo)

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColMarks
                SprdMain.Text = IIf(IsDBNull(.Fields("MARKS").Value), "", .Fields("MARKS").Value)

                SprdMain.Col = ColPalletNo
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("PalletNo").Value), 0, .Fields("PalletNo").Value), "0")

                SprdMain.Col = ColPackQty
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("NO_OF_PACKETS").Value), 0, .Fields("NO_OF_PACKETS").Value), "0")

                SprdMain.Col = ColQty
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("PACKED_QTY").Value), 0, .Fields("PACKED_QTY").Value), "0.00")

                SprdMain.Col = ColNetWt
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("NET_WT").Value), 0, .Fields("NET_WT").Value), "0.000")

                SprdMain.Col = ColGrossWt
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("GROSS_WT").Value), 0, .Fields("GROSS_WT").Value), "0.000")




                SprdMain.Col = ColSize
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_SIZE").Value), "", .Fields("ITEM_SIZE").Value))

                SprdMain.Col = ColModelNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value))

                SprdMain.Col = ColDrawingNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_DRAWINGNO").Value), "", .Fields("ITEM_DRAWINGNO").Value))

                SprdMain.Col = ColGlassDescription
                SprdMain.Text = IIf(IsDBNull(.Fields("GLASS_DESC").Value), "", .Fields("GLASS_DESC").Value)

                SprdMain.Col = ColActualHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_HEIGHT").Value), 0, .Fields("ACTUAL_HEIGHT").Value)))

                SprdMain.Col = ColActualWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_WIDTH").Value), 0, .Fields("ACTUAL_WIDTH").Value)))

                SprdMain.Col = ColChargeableHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_HEIGHT").Value), 0, .Fields("CHARGEABLE_HEIGHT").Value)))

                SprdMain.Col = ColChargeableWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_WIDTH").Value), 0, .Fields("CHARGEABLE_WIDTH").Value)))

                SprdMain.Col = ColGlassArea
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GLASS_AREA").Value), 0, .Fields("GLASS_AREA").Value)))



                pModelNo = Trim(IIf(IsDBNull(.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value))
                pDrawingNo = Trim(IIf(IsDBNull(.Fields("ITEM_DRAWINGNO").Value), "", .Fields("ITEM_DRAWINGNO").Value))
                pActualHeight = IIf(IsDBNull(.Fields("ACTUAL_HEIGHT").Value), 0, .Fields("ACTUAL_HEIGHT").Value)
                pActualWidth = IIf(IsDBNull(.Fields("ACTUAL_WIDTH").Value), 0, .Fields("ACTUAL_WIDTH").Value)

                SprdMain.Col = ColBuyerPOQty
                SprdMain.Text = CStr(GetSalesDSQty(mItemCode, mSoNo, pModelNo, pDrawingNo, pActualHeight, pActualWidth))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)

        SSTInfo.SelectedIndex = 2
        SqlStr = " SELECT *  FROM FIN_PACKING_PARA_EXP Where AUTO_KEY_PACK=" & Val(mMKey) & " Order By SERAIL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackExp, ADODB.LockTypeEnum.adLockReadOnly)

        Dim mOthersName As String
        Dim mOthersValue As String
        Dim RsTemp As ADODB.Recordset = Nothing

        With SprdOther
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColOthersName
                mOthersName = Trim(.Text)

                SqlStr = " SELECT *  FROM FIN_PACKING_PARA_EXP Where AUTO_KEY_PACK=" & Val(mMKey) & " And FIELD_NAME='" & MainClass.AllowSingleQuote(mOthersName) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                mOthersValue = ""
                If RsTemp.EOF = False Then
                    mOthersValue = If(IsDBNull(RsTemp.Fields("FIELD_VALUE").Value), "", RsTemp.Fields("FIELD_VALUE").Value)
                End If

                .Col = ColOthersValue
                .Text = mOthersValue

            Next
        End With


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume	
    End Sub
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdataItem.Refresh	
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsPackMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        lblMkey.Text = ""

        mCustomerCode = CStr(-1)
        txtPackNo.Text = ""
        txtPackDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtCustomerName.Text = ""
        txtCustomerCode.Text = ""
        txtBuyerCode.Text = ""
        txtBuyerName.Text = ""

        txtBillTo.Text = ""
        txtShipTo.Text = ""
        txtInvNo.Text = ""
        txtInvDate.Text = ""
        txtExciseBillNo.Text = ""
        txtExciseBillDate.Text = ""
        txtSONo.Text = ""
        txtSODate.Text = ""
        txtBuyerNo.Text = ""
        txtBuyerDate.Text = ""

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110 Then
            txtBuyerNo.Enabled = True
            txtBuyerDate.Enabled = True
        Else
            txtBuyerNo.Enabled = False
            txtBuyerDate.Enabled = False
        End If

        txtRemarks.Text = ""
        txtIECNo.Text = IIf(IsDbNull(RsCompany.Fields("IEC_NO").Value), "", RsCompany.Fields("IEC_NO").Value)
        txtIECNo.Enabled = False

        txtOrigin.Text = "INDIA"
        txtDestination.Text = ""
        txtCarriage.Text = ""
        txtPlace.Text = ""
        txtFlight.Text = ""
        txtLoading.Text = ""
        txtDischarge.Text = ""
        txtFinalDestination.Text = ""
        txtPayments.Text = ""
        txtContainerNo.Text = ""
        txtBuyerAddress.Text = ""
        txtConsigneeAddress.Text = ""
        txtNotifyParty1.Text = ""
        txtNotifyParty2.Text = ""
        txtNotifyParty3.Text = ""

        chkDC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkExciseInvoice.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkExportInv.CheckState = System.Windows.Forms.CheckState.Unchecked
        SSTInfo.SelectedIndex = 0

        txtCustomerName.Enabled = True
        txtCustomerCode.Enabled = True
        txtBuyerCode.Enabled = True
        txtInvNo.Enabled = False
        txtInvDate.Enabled = False
        txtExciseBillNo.Enabled = False
        txtExciseBillDate.Enabled = False

        txtSONo.Enabled = True
        txtSODate.Enabled = False
        cmdSearchSo.Enabled = True


        txtBuyerName.Enabled = True
        cmdsearchBuyer.Enabled = True
        cmdsearch.Enabled = True
        MainClass.ClearGrid(SprdMain)

        Call FillSprdOther()
        Call FormatSprdMain(-1)
        Call FormatSprdOther(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsPackMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FrmPackingNote_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub FrmPackingNote_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmPackingNote_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode	
    End Sub

    Private Sub FrmPackingNote_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245) '8000	
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900	

        'AdataItem.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))

        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        'With SprdMain
        '    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub

    Private Sub SprdOther_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdOther.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdOther_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdOther.KeyUpEvent
        Dim mCol As Short
        mCol = SprdOther.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColOthersValue Then SprdOther_ClickEvent(SprdOther, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOthersValue, SprdOther.ActiveRow))

        SprdOther.Refresh()
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mCompanyCity As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBuyerCode As String
        Dim mFormulaStr As String
        Dim pSqlStr As String
        Dim mCOMPANYTYPE As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        mCompanyCity = IIf(IsDbNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        mCompanyCity = mCompanyCity & "-" & IIf(IsDbNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)
        mCompanyCity = mCompanyCity & "(" & IIf(IsDbNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & ") INDIA"


        MainClass.AssignCRptFormulas(Report1, "CompanyAddress=""" & IIf(IsDbNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyCity=""" & mCompanyCity & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyPhone=""" & IIf(IsDbNull(RsCompany.Fields("COMPANY_PHONE").Value), "", RsCompany.Fields("COMPANY_PHONE").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyEmail=""" & IIf(IsDbNull(RsCompany.Fields("COMPANY_MAILID").Value), "", RsCompany.Fields("COMPANY_MAILID").Value) & """")

        mCOMPANYTYPE = IIf(RsCompany.Fields("ISEOU").Value = "Y", "100% E.O.U.", "")
        MainClass.AssignCRptFormulas(Report1, "COMPANYTYPE=""" & mCOMPANYTYPE & """")

        If MainClass.ValidateWithMasterTable(txtBuyerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBuyerCode = MasterNo
            If mBuyerCode = "" Then
                mBuyerCode = txtCustomerCode.Text
            End If
            pSqlStr = " SELECT CMST.SUPP_CUST_NAME, BMST.SUPP_CUST_ADDR, " & vbCrLf _
                & " BMST.SUPP_CUST_CITY, BMST.COUNTRY, BMST.SUPP_CUST_PIN, " & vbCrLf _
                & "  CMST.SUPP_CUST_PHONE,  CMST.SUPP_CUST_FAXNO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BMST" & vbCrLf _
                & " WHERE  CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND  CMST.COMPANY_CODE=BMST.COMPANY_CODE" & vbCrLf _
                & " AND  CMST.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND  BMST.LOCATION_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'" & vbCrLf _
                & " AND  CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mBuyerCode) & "'"

            MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


            If RsTemp.EOF = False Then
                mFormulaStr = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                MainClass.AssignCRptFormulas(Report1, "BuyerName=""" & mFormulaStr & """")

                mFormulaStr = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                mFormulaStr = Replace(mFormulaStr, vbCrLf, " ")
                MainClass.AssignCRptFormulas(Report1, "BuyerAddress=""" & mFormulaStr & """")

                mFormulaStr = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                MainClass.AssignCRptFormulas(Report1, "BuyerCity=""" & mFormulaStr & """")

                mFormulaStr = IIf(IsDbNull(RsTemp.Fields("COUNTRY").Value), "", RsTemp.Fields("COUNTRY").Value)
                MainClass.AssignCRptFormulas(Report1, "BuyerCountry=""" & mFormulaStr & """")

                mFormulaStr = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_PHONE").Value), "", "Phone No.:" & RsTemp.Fields("SUPP_CUST_PHONE").Value)
                mFormulaStr = mFormulaStr & IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_FAXNO").Value), "", "Fax No.:" & RsTemp.Fields("SUPP_CUST_FAXNO").Value)
                MainClass.AssignCRptFormulas(Report1, "BuyerPhone=""" & mFormulaStr & """")
            End If
        End If


        Report1.ReportFileName = PubReportFolderPath & mRptFileName


        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub txtCustomerName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtCustomerName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtCustomerName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtCustomerName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(TxtCustomerName.Text) = "" Then
            txtConsigneeAddress.Text = ""
            txtCustomerCode.Text = ""
            txtShipTo.Text = ""
            GoTo EventExitSub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((txtCustomerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCustomerCode = MasterNo
            txtCustomerCode.Text = mCustomerCode
        Else
            mCustomerCode = "-1"
            Cancel = True
        End If

        If ADDMode = True Then
            Call FillCustomerDetail()
        End If

        txtConsigneeAddress.Text = FillAddressDetail(txtCustomerCode.Text, txtShipTo.Text)

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function CheckDuplicateItem(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If mItemCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                If UCase(.Text) = UCase(mItemCode) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)

    End Function

    Private Sub txtPayments_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPayments.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPayments_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPayments.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPayments.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPlace_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPlace.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPlace_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPlace.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPlace.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub



    Private Function CalcQty() As Object
        On Error GoTo ErrPart
        Dim mPalletNo As Double
        Dim mPackQty As Double
        Dim mQty As Double
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mItemWt As Double
        Dim mTotItemWt As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If mItemCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_WEIGHT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemWt = Val(MasterNo)
                        mItemWt = mItemWt * 0.001
                    Else
                        mItemWt = 0
                    End If

                    .Col = ColPalletNo
                    mPalletNo = Val(.Text)

                    .Col = ColPackQty
                    mPackQty = Val(.Text)

                    .Col = ColQty
                    mQty = mPalletNo * mPackQty
                    .Text = VB6.Format(mQty, "0.00")

                    .Col = ColNetWt
                    'If ADDMode = True Or Val(.Text) = 0 Then
                    mTotItemWt = mItemWt * mQty
                    .Text = VB6.Format(IIf(Val(.Text) = 0, mTotItemWt, Val(.Text)), "0.00")
                    'End If

                End If
            Next
        End With
        Exit Function
ErrPart:
        'Resume	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function CheckDSBalQty() As Boolean
        On Error GoTo ErrPart
        Dim mPalletNo As Double
        Dim mPackQty As Double
        Dim mQty As Double
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mItemWt As Double
        Dim mTotItemWt As Double
        Dim mSchdBalQty As Double
        Dim mDS As Double


        If PubUserID = "G0416" Then
            CheckDSBalQty = True
            Exit Function
        End If
        CheckDSBalQty = False
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If mItemCode <> "" Then
                    .Col = ColSONo
                    mDS = Val(.Text)

                    .Col = ColBuyerPOQty
                    mSchdBalQty = Val(.Text)

                    .Col = ColPalletNo
                    mPalletNo = Val(.Text)

                    .Col = ColPackQty
                    mPackQty = Val(.Text)

                    .Col = ColQty
                    mQty = mPalletNo * mPackQty
                    .Text = VB6.Format(mQty, "0.00")

                    If PubUserID = "G0416" Then
                    Else
                        If Val(CStr(mDS)) <> 0 Then
                            If mSchdBalQty < mQty Then
                                CheckDSBalQty = False
                                Exit Function
                            End If
                        End If
                    End If

                End If
            Next
        End With
        CheckDSBalQty = True
        Exit Function
ErrPart:
        CheckDSBalQty = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub txtSODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSODate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSONo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSONo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSONo.DoubleClick
        cmdSearchSo_Click(cmdSearchSo, New System.EventArgs())
    End Sub

    Private Sub txtSONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSONo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSONo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchSo_Click(cmdSearchSo, New System.EventArgs())
    End Sub
    Private Sub txtSONo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        Dim RsPO As ADODB.Recordset = Nothing
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String
        Dim mDSQty As Double
        Dim cntRow As Long
        Dim pSOQty As Double
        Dim pModelNo As String
        Dim pDrawingNo As String
        Dim pActualHeight As Double
        Dim pActualWidth As Double

        If ADDMode = False Then Exit Sub

        If Trim(txtSONo.Text) = "" Then
            txtSODate.Text = ""
            txtBuyerNo.Text = ""
            txtBuyerDate.Text = ""
        End If

        SqlStr = " SELECT POM.*, POD.SO_QTY," & vbCrLf _
                & " POD.SERIAL_NO, POD.SUPP_CUST_CODE, POD.ITEM_CODE, POD.UOM_CODE, POD.PART_NO,  POD.ITEM_MODEL, POD.ITEM_DRAWINGNO, POD.GLASS_DESC, POD.ACTUAL_HEIGHT, POD.ACTUAL_WIDTH," & vbCrLf _
                & " POD.ITEM_PRICE, POD.PACK_TYPE, POD.COLOUR_DTL, AC.SUPP_CUST_NAME as SuppName, POD.ITEM_SIZE, POD.CHARGEABLE_HEIGHT , POD.CHARGEABLE_WIDTH, POD.GLASS_AREA  AS GLASS_AREA" & vbCrLf _
                & " FROM DSP_SALEORDER_HDR POM,DSP_SALEORDER_DET POD,FIN_SUPP_CUST_MST AC " & vbCrLf _
                & " WHERE POM.MKEY = POD.MKEY " & vbCrLf _
                & " AND POM.Company_Code = AC.Company_Code " & vbCrLf _
                & " AND POM.SUPP_CUST_CODE = AC.SUPP_CUST_CODE " & vbCrLf _
                & " AND POM.AUTO_KEY_SO=" & Val(txtSONo.Text) & " AND SO_APPROVED='Y' AND GOODS_SERVICE='G'"

        SqlStr = SqlStr & vbCrLf & " AND POM.SUPP_CUST_CODE='" & txtBuyerCode.Text & "' "

        SqlStr = SqlStr & vbCrLf & " AND POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND POM.SO_STATUS='O' " & vbCrLf _
                & " ORDER BY POD.SERIAL_NO"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPO, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsPO.EOF Then
            MainClass.ClearGrid(SprdMain)
            txtSONo.Text = IIf(IsDBNull(RsPO.Fields("AUTO_KEY_SO").Value), "", RsPO.Fields("AUTO_KEY_SO").Value)
            txtSODate.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("SO_DATE").Value), "", RsPO.Fields("SO_DATE").Value), "DD/MM/YYYY")
            txtBuyerNo.Text = IIf(IsDBNull(RsPO.Fields("CUST_PO_NO").Value), "", RsPO.Fields("CUST_PO_NO").Value)
            txtBuyerDate.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("CUST_PO_DATE").Value), "", RsPO.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

            If Trim(txtCustomerCode.Text) = "" Then
                txtCustomerCode.Text = IIf(IsDBNull(RsPO.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsPO.Fields("SHIPPED_TO_PARTY_CODE").Value)

                txtCustomerCode_Validating(txtCustomerCode, New System.ComponentModel.CancelEventArgs(False))

                txtShipTo.Text = IIf(IsDBNull(RsPO.Fields("SHIP_TO_LOC_ID").Value), "", RsPO.Fields("SHIP_TO_LOC_ID").Value)
            End If

            txtBillTo.Text = IIf(IsDBNull(RsPO.Fields("BILL_TO_LOC_ID").Value), "", RsPO.Fields("BILL_TO_LOC_ID").Value)


            txtConsigneeAddress.Text = FillAddressDetail(txtCustomerCode.Text, txtShipTo.Text)
            txtBuyerAddress.Text = FillAddressDetail(txtBuyerCode.Text, txtBillTo.Text)

            cntRow = 1
            Do While RsPO.EOF = False

                SprdMain.Row = cntRow
                SprdMain.Col = ColSONo
                SprdMain.Text = Trim(Str(IIf(IsDBNull(RsPO.Fields("AUTO_KEY_SO").Value), "", RsPO.Fields("AUTO_KEY_SO").Value)))

                SprdMain.Col = ColSODate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("SO_DATE").Value), "", RsPO.Fields("SO_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColBuyerPO
                SprdMain.Text = Trim(IIf(IsDBNull(RsPO.Fields("CUST_PO_NO").Value), "", RsPO.Fields("CUST_PO_NO").Value))

                SprdMain.Col = ColBuyerDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("CUST_PO_DATE").Value), "", RsPO.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                mItemCode = Trim(IIf(IsDBNull(RsPO.Fields("ITEM_CODE").Value), "", RsPO.Fields("ITEM_CODE").Value))

                SprdMain.Col = ColItemCode
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                mItemDesc = ""
                SprdMain.Col = ColPartNo

                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo

                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUnit
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "ISSUE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mUOM = MasterNo
                SprdMain.Text = mUOM




                SprdMain.Col = ColSize
                SprdMain.Text = Trim(IIf(IsDBNull(RsPO.Fields("ITEM_SIZE").Value), "", RsPO.Fields("ITEM_SIZE").Value))

                SprdMain.Col = ColModelNo
                SprdMain.Text = Trim(IIf(IsDBNull(RsPO.Fields("ITEM_MODEL").Value), "", RsPO.Fields("ITEM_MODEL").Value))
                pModelNo = Trim(IIf(IsDBNull(RsPO.Fields("ITEM_MODEL").Value), "", RsPO.Fields("ITEM_MODEL").Value))

                SprdMain.Col = ColDrawingNo
                SprdMain.Text = Trim(IIf(IsDBNull(RsPO.Fields("ITEM_DRAWINGNO").Value), "", RsPO.Fields("ITEM_DRAWINGNO").Value))
                pDrawingNo = Trim(IIf(IsDBNull(RsPO.Fields("ITEM_DRAWINGNO").Value), "", RsPO.Fields("ITEM_DRAWINGNO").Value))

                SprdMain.Col = ColGlassDescription
                SprdMain.Text = IIf(IsDBNull(RsPO.Fields("GLASS_DESC").Value), "", RsPO.Fields("GLASS_DESC").Value)

                SprdMain.Col = ColActualHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ACTUAL_HEIGHT").Value), 0, RsPO.Fields("ACTUAL_HEIGHT").Value)))
                pActualHeight = Trim(IIf(IsDBNull(RsPO.Fields("ACTUAL_HEIGHT").Value), 0, RsPO.Fields("ACTUAL_HEIGHT").Value))

                SprdMain.Col = ColActualWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ACTUAL_WIDTH").Value), 0, RsPO.Fields("ACTUAL_WIDTH").Value)))
                pActualWidth = Trim(IIf(IsDBNull(RsPO.Fields("ACTUAL_WIDTH").Value), 0, RsPO.Fields("ACTUAL_WIDTH").Value))

                SprdMain.Col = ColChargeableHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("CHARGEABLE_HEIGHT").Value), 0, RsPO.Fields("CHARGEABLE_HEIGHT").Value)))

                SprdMain.Col = ColChargeableWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("CHARGEABLE_WIDTH").Value), 0, RsPO.Fields("CHARGEABLE_WIDTH").Value)))

                SprdMain.Col = ColGlassArea
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("GLASS_AREA").Value), 0, RsPO.Fields("GLASS_AREA").Value)))


                'Dim pModelNo As String
                'Dim pDrawingNo As String
                'Dim pActualHeight As Double
                'Dim pActualWidth As Double

                SprdMain.Col = ColBuyerPOQty
                pSOQty = Val(IIf(IsDBNull(RsPO.Fields("SO_QTY").Value), 0, RsPO.Fields("SO_QTY").Value))
                mDSQty = GetSalesDSQty(mItemCode, Val(txtSONo.Text), pModelNo, pDrawingNo, pActualHeight, pActualWidth)
                SprdMain.Row = cntRow
                SprdMain.Col = ColBuyerPOQty
                SprdMain.Text = VB6.Format(mDSQty, "0.00")


                cntRow = cntRow + 1
                SprdMain.MaxRows = SprdMain.MaxRows + 1
                RsPO.MoveNext()
            Loop
            'If mBillToSameShipToCode = "Y" Then
            '    mShippedToCode = IIf(IsDBNull(RsPO.Fields("SUPP_CUST_CODE").Value), "", RsPO.Fields("SUPP_CUST_CODE").Value)
            '    txtShipTo.Text = txtBillTo.Text
            '    chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
            'Else
            '    mShippedToCode = IIf(IsDBNull(RsPO.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsPO.Fields("SHIPPED_TO_PARTY_CODE").Value)
            '    txtShipTo.Text = IIf(IsDBNull(RsPO.Fields("SHIP_TO_LOC_ID").Value), "", RsPO.Fields("SHIP_TO_LOC_ID").Value)
            '    chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked
            'End If

            'If MainClass.ValidateWithMasterTable(mShippedToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    txtShipCustomer.Text = MasterNo
            'Else
            '    txtShipCustomer.Text = ""
            'End If
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSearchSO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSo.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & " AND SO_STATUS='O' AND SO_APPROVED='Y' AND GOODS_SERVICE='G' AND SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtBuyerCode.Text) & "' AND BILL_TO_LOC_ID='" & txtBillTo.Text & "'"


        If MainClass.SearchGridMaster(txtSONo.Text, "DSP_SALEORDER_HDR", "AUTO_KEY_SO", "SO_DATE", "CUST_PO_NO", "CUST_PO_DATE", SqlStr) = True Then
            txtSONo.Text = AcName
            txtSONo_Validating(txtSONo, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtBuyerCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBuyerCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBuyerCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBuyerCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBuyerCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBuyerCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtBuyerCode.Text) = "" Then
            txtBuyerAddress.Text = ""
            txtBuyerName.Text = ""
            txtBillTo.Text = ""
            GoTo EventExitSub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((txtBuyerCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtBuyerName.Text = MasterNo
        Else
            txtBuyerName.Text = ""
            Cancel = True
        End If

        txtBuyerAddress.Text = FillAddressDetail(txtBuyerCode.Text, txtBillTo.Text)

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function FillAddressDetail(ByVal pCode As String, ByVal mLocation As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xBuyerCode As String
        Dim xBuyerAddress As String

        If pCode = "" Then Exit Function
        If mLocation = "" Then Exit Function
        FillAddressDetail = ""

        SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE" & vbCrLf _
                    & " FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCode) & "'" & vbCrLf _
                    & " AND LOCATION_ID='" & MainClass.AllowSingleQuote(mLocation) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            With RsTemp

                xBuyerAddress = IIf(IsDBNull(.Fields("SUPP_CUST_ADDR").Value), "", .Fields("SUPP_CUST_ADDR").Value)
                xBuyerAddress = xBuyerAddress & ", " & IIf(IsDBNull(.Fields("SUPP_CUST_CITY").Value), "", .Fields("SUPP_CUST_CITY").Value)
                xBuyerAddress = xBuyerAddress & ", " & IIf(IsDBNull(.Fields("SUPP_CUST_STATE").Value), "", .Fields("SUPP_CUST_STATE").Value)

                FillAddressDetail = xBuyerAddress
            End With
        End If
        Exit Function

ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

End Class
