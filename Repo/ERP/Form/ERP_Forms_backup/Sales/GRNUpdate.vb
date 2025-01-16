Option Strict Off
Option Explicit On
Imports AxFPSpreadADO
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmGRNUpdate
    Inherits System.Windows.Forms.Form
    Dim RsSaleGRMain As ADODB.Recordset ''Recordset
    Dim RsSaleGRDetail As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String = ""


    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColPartNo As Short = 3
    Private Const ColUnit As Short = 4
    Private Const ColODNo As Short = 5
    Private Const ColStoreLoc As Short = 6
    Private Const ColPackQty As Short = 7

    Private Const ColRejQty As Short = 8
    Private Const ColShortQty As Short = 9

    Private Const ConRowHeight As Short = 12
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If lblType.Text = "S" Then
            If UpdateMain1() = True Then
                Me.Close()
            Else
                MsgInformation("Record not saved")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
            'Else
            '    If UpdateGMain1() = True Then
            '        Me.Close()
            '    Else
            '        MsgInformation("Record not saved")
            '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '        Exit Sub
            '    End If

        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FrmGRNUpdate_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Close()
    End Sub


    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim CntBillNo As Integer
        Dim mBillNo As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mGRNo As String
        Dim mGRDate As String
        Dim pGRDate As String
        Dim mTransBillDate As String
        Dim I As Long
        Dim mRejQty As Double
        Dim mShortQty As Double
        Dim mItemCode As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mBillNo = txtBillNoPrefix.Text & Val(txtBillNo.Text) & txtBillNoSuffix.Text

        pGRDate = VB6.Format(TxtGRNDate.Text, "DD/MM/YYYY")

        SqlStr = ""

        If chkClear.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
                & " GRNNO= '', " & vbCrLf _
                & " GRNDATE= ''," & vbCrLf _
                & " GRN_RECD_QTY= 0, " & vbCrLf _
                & " GRN_ACCEPTED_QTY=  0," & vbCrLf _
                & " GEN_REJ_QTY= 0, " & vbCrLf _
                & " GRN_SHORTAGE_QTY=0," & vbCrLf _
                & " GRN_REMARKS= ''," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR= '" & RsCompany.Fields("FYEAR").Value & "' AND MKEY='" & lblMKey.Text & "'"

            PubDBCn.Execute(SqlStr)

            With SprdMain
                For I = 1 To SprdMain.MaxRows
                    .Row = I
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    If mItemCode <> "" Then
                        .Col = ColRejQty
                        mRejQty = Val(.Text)

                        .Col = ColRejQty
                        mShortQty = Val(.Text)

                        SqlStr = "UPDATE FIN_INVOICE_DET SET " & vbCrLf _
                            & " ITEM_SHORT_RECD_QTY=0, ITEM_REJ_RECD_QTY=0" & vbCrLf _
                            & " WHERE MKEY='" & lblMKey.Text & "' AND ITEM_CODE='" & mItemCode & "' ANd SUBROWNO=" & I & ""

                        PubDBCn.Execute(SqlStr)

                    End If
                Next
            End With
        Else
            'SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
            '    & " GRNNO= '" & MainClass.AllowSingleQuote(TxtGRNNo.Text) & "', " & vbCrLf _
            '    & " GRNDATE= TO_DATE('" & VB6.Format(pGRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
            '    & " GRN_RECD_QTY= " & Val(txtReceivedQty.Text) & ", " & vbCrLf _
            '    & " GRN_ACCEPTED_QTY=  " & Val(txtAcceptedQty.Text) & "," & vbCrLf _
            '    & " GEN_REJ_QTY= " & Val(txtRejectedQty.Text) & ", " & vbCrLf _
            '    & " GRN_SHORTAGE_QTY=" & Val(txtShortageQty.Text) & "," & vbCrLf _
            '    & " GRN_REMARKS= '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
            '    & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
            '    & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            '    & " AND FYEAR= '" & RsCompany.Fields("FYEAR").Value & "' AND MKEY='" & lblMKey.Text & "'"

            'PubDBCn.Execute(SqlStr)

            SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
               & " GRNNO= '" & MainClass.AllowSingleQuote(TxtGRNNo.Text) & "', " & vbCrLf _
               & " GRNDATE= TO_DATE('" & VB6.Format(pGRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
               & " GRN_RECD_QTY= " & Val(txtReceivedQty.Text) & ", " & vbCrLf _
               & " GRN_ACCEPTED_QTY=  " & Val(txtAcceptedQty.Text) & "," & vbCrLf _
               & " GEN_REJ_QTY= " & Val(txtRejectedQty.Text) & ", " & vbCrLf _
               & " GRN_SHORTAGE_QTY=" & Val(txtShortageQty.Text) & "," & vbCrLf _
               & " GRN_REMARKS= '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
               & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
               & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
               & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
               & " AND FYEAR= '" & RsCompany.Fields("FYEAR").Value & "' AND MKEY='" & lblMKey.Text & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = "UPDATE INV_MISC_GATE_HDR SET " & vbCrLf _
               & " IS_OUT='Y',OUT_DATE= TO_DATE('" & VB6.Format(pGRDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
               & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
               & " WHERE BILL_NO ='" & MainClass.AllowSingleQuote(Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & Trim(txtBillNoSuffix.Text)) & "' AND IS_OUT='N'"

            PubDBCn.Execute(SqlStr)

            With SprdMain
                For I = 1 To SprdMain.MaxRows
                    .Row = I
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    If mItemCode <> "" Then
                        .Col = ColRejQty
                        mRejQty = Val(.Text)

                        .Col = ColShortQty
                        mShortQty = Val(.Text)

                        SqlStr = "UPDATE FIN_INVOICE_DET SET " & vbCrLf _
                            & " ITEM_SHORT_RECD_QTY=" & mShortQty & ", ITEM_REJ_RECD_QTY=" & mRejQty & "" & vbCrLf _
                            & " WHERE MKEY='" & lblMKey.Text & "' AND ITEM_CODE='" & mItemCode & "' ANd SUBROWNO=" & I & ""

                        PubDBCn.Execute(SqlStr)

                    End If
                Next
            End With
        End If





        UpdateMain1 = True

        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        If Err.Description = "" Then Exit Function
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True

        If txtBillNo.Text = "" Then
            MsgInformation("Bill No. is Blank")
            txtBillNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If chkClear.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtGRNNo.Text) = "" Then
                MsgBox("GRN No is blank.", MsgBoxStyle.Critical)
                FieldsVarification = False
                TxtGRNNo.Focus()
                Exit Function
            End If

            If Trim(TxtGRNDate.Text) = "__/__/____" Then
                MsgBox("GRN Date is blank.", MsgBoxStyle.Critical)
                FieldsVarification = False
                TxtGRNDate.Focus()
                Exit Function
            End If

            'If TxtGRNDate.Text <> "__/__/____" Then
            '    If FYChk((TxtGRNDate.Text)) = False Then
            '        FieldsVarification = False
            '        TxtGRNDate.Focus()
            '        Exit Function
            '    End If
            'End If
        End If


        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Public Sub FrmGRNUpdate_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from FIN_INVOICE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleGRMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from FIN_INVOICE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleGRDetail, ADODB.LockTypeEnum.adLockReadOnly)




        Call SetTextLengths()
        Call Clear1()
        If lblType.Text = "S" Then
            Call Show1()
        Else
            'Call Show_G1()
        End If

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsSaleGRMain
            txtBillNoPrefix.MaxLength = .Fields("BillNoPrefix").DefinedSize ''
            txtBillNo.MaxLength = .Fields("BILLNOSEQ").Precision ''
            txtBillNoSuffix.MaxLength = .Fields("BillNoSuffix").DefinedSize ''

            TxtGRNNo.MaxLength = .Fields("GRNNo").DefinedSize ''
            TxtGRNDate.MaxLength = 10
            txtAcceptedQty.MaxLength = .Fields("GRN_ACCEPTED_QTY").Precision ''
            txtReceivedQty.MaxLength = .Fields("GRN_RECD_QTY").Precision ''
            txtRejectedQty.MaxLength = .Fields("GEN_REJ_QTY").Precision ''
            txtShortageQty.MaxLength = .Fields("GRN_SHORTAGE_QTY").Precision
            txtRemarks.MaxLength = .Fields("GRN_REMARKS").DefinedSize


        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mBillNo As String

        mBillNo = Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & Trim(txtBillNoSuffix.Text)

        SqlStr = "SELECT BILLNOPREFIX,BILLNOSUFFIX,BILLNOSEQ,INVOICE_DATE,NETVALUE,MKEY,GRNNo,GRNDATE,GRN_ACCEPTED_QTY,GRN_RECD_QTY,GEN_REJ_QTY,GRN_SHORTAGE_QTY,GRN_REMARKS,TOTQTY  " & vbCrLf _
            & " FROM FIN_INVOICE_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And FYEAR= '" & RsCompany.Fields("FYEAR").Value & "' AND MKEY='" & lblMKey.Text & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleGRMain, ADODB.LockTypeEnum.adLockReadOnly)

        With RsSaleGRMain
            If Not .EOF Then

                txtBillNoPrefix.Text = IIf(IsDBNull(.Fields("BILLNOPREFIX").Value), "", .Fields("BILLNOPREFIX").Value)
                txtBillNo.Text = VB6.Format(IIf(IsDBNull(.Fields("BILLNOSEQ").Value), "", .Fields("BILLNOSEQ").Value), ConBillFormat)
                txtBillNoSuffix.Text = IIf(IsDBNull(.Fields("BILLNOSUFFIX").Value), "", .Fields("BILLNOSUFFIX").Value)
                'txtBillNo.Text = IIf(IsDBNull(.Fields("BILLNOSEQ").Value), "", .Fields("BILLNOSEQ").Value)
                txtInvoiceDate.Text = IIf(IsDBNull(.Fields("INVOICE_DATE").Value), "__/__/____", .Fields("INVOICE_DATE").Value)
                txtBillAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("NETVALUE").Value), "0", .Fields("NETVALUE").Value), "0.00")
                txtBillQty.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTQTY").Value), "0", .Fields("TOTQTY").Value), "0.00")

                TxtGRNNo.Text = IIf(IsDBNull(.Fields("GRNNo").Value), "", .Fields("GRNNo").Value)
                TxtGRNDate.Text = IIf(IsDBNull(.Fields("GRNDATE").Value), "__/__/____", .Fields("GRNDATE").Value)
                txtAcceptedQty.Text = VB6.Format(IIf(IsDBNull(.Fields("GRN_ACCEPTED_QTY").Value), "0", .Fields("GRN_ACCEPTED_QTY").Value), "0.00")
                txtReceivedQty.Text = VB6.Format(IIf(IsDBNull(.Fields("GRN_RECD_QTY").Value), "0", .Fields("GRN_RECD_QTY").Value), "0.00")
                txtRejectedQty.Text = VB6.Format(IIf(IsDBNull(.Fields("GEN_REJ_QTY").Value), "0", .Fields("GEN_REJ_QTY").Value), "0.00")
                txtShortageQty.Text = VB6.Format(IIf(IsDBNull(.Fields("GRN_SHORTAGE_QTY").Value), "0", .Fields("GRN_SHORTAGE_QTY").Value), "0.00")
                txtRemarks.Text = IIf(IsDBNull(.Fields("GRN_REMARKS").Value), "", .Fields("GRN_REMARKS").Value)

                ShowDetail1(lblMKey.Text)
            End If
        End With

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub Clear1()

        txtRemarks.Text = ""
        txtShortageQty.Text = ""
        txtRejectedQty.Text = ""
        txtBillQty.Text = ""
        txtAcceptedQty.Text = ""
        txtReceivedQty.Text = ""
        TxtGRNNo.Text = ""
        TxtGRNDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtBillAmount.Text = ""
        'txtInvoiceDate = VB6.Format(RunDate, "DD/MM/YYYY")
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
    End Sub

    Private Sub FrmGRNUpdate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Public Sub FrmGRNUpdate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn


        MainClass.SetControlsColor(Me)
        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        'Me.Height = VB6.TwipsToPixelsY(4665)
        ''Me.Width = VB6.TwipsToPixelsX(5910)

        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBillNoTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAcceptedQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAcceptedQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtShortageQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtShortageQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtGRNDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtGRNDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If TxtGRNDate.Text = "__/__/____" Then GoTo EventExitSub
        If IsDate(TxtGRNDate.Text) = False Then
            ErrorMsg("Invalid Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtGRNNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtGRNNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtGRNNo.Text) '' MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text) '' MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtReceivedQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReceivedQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRejectedQty_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles txtRejectedQty.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .ColsFrozen = ColItemDesc
            .set_ColWidth(ColItemDesc, 20)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColPartNo, 12)
            .set_ColUserSortIndicator(ColPartNo, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColUnit, 4)
            .ColHidden = True

            .Col = ColODNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColODNo, 8)
            .ColHidden = True

            .Col = ColStoreLoc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColStoreLoc, 8)
            .ColHidden = True

            'Private Const ColPackQty As Short = 7

            'Private Const ColRejQty As Short = 8
            'Private Const ColShortQty As Short = 9

            'Private Const ConRowHeight As Short = 12

            .Col = ColPackQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPackQty, 9)

            .Col = ColRejQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRejQty, 9)

            .Col = ColShortQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColShortQty, 9)



        End With

        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColShortQty)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColPackQty)
        MainClass.SetSpreadColor(SprdMain, Arow)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub ShowDetail1(ByRef mMKey As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mStockType As String = ""
        Dim mLotNo As String
        Dim xFGBatchNoReq As String
        Dim mStoreLoc As String
        Dim mODNo As String
        Dim mSchdDate As String
        Dim mScheduleQty As Double
        Dim mDayScheduleQty As Double
        Dim mDespQty As Double
        Dim pSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSoNo As Double
        Dim mDIRequired As String
        Dim mSqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM FIN_INVOICE_DET " & vbCrLf _
            & " Where MKEY=" & Val(mMKey) & "" & vbCrLf _
            & " Order By SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleGRDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsSaleGRDetail
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = Trim(mItemDesc)

                SprdMain.Col = ColPartNo
                mPartNo = Trim(IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value))
                SprdMain.Text = Trim(mPartNo)

                '', ITEM_DESC,  , , 

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                'SprdMain.Col = ColStoreLoc
                'SprdMain.Text = IIf(IsDBNull(.Fields("LOC_CODE").Value), "", .Fields("LOC_CODE").Value)
                'mStoreLoc = IIf(IsDBNull(.Fields("LOC_CODE").Value), "", .Fields("LOC_CODE").Value)

                'SprdMain.Col = ColODNo
                'SprdMain.Text = IIf(IsDBNull(.Fields("OD_NO").Value), "", .Fields("OD_NO").Value)
                'mODNo = IIf(IsDBNull(.Fields("OD_NO").Value), "", .Fields("OD_NO").Value)

                SprdMain.Col = ColPackQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColRejQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_REJ_RECD_QTY").Value), 0, .Fields("ITEM_REJ_RECD_QTY").Value)))

                SprdMain.Col = ColShortQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_SHORT_RECD_QTY").Value), 0, .Fields("ITEM_SHORT_RECD_QTY").Value)))
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

    Private Sub SprdMain_LeaveCell(sender As Object, EventArgs As _DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        Dim xICode As String
        Dim xIUOM As String
        Dim mPackQty As Double
        Dim mCheckQty As Double

        If EventArgs.newRow = -1 Then Exit Sub


        SprdMain.Row = EventArgs.row
        SprdMain.Col = ColItemCode
        If SprdMain.Text = "" Then Exit Sub

        Select Case EventArgs.col
            Case ColShortQty
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = Trim(SprdMain.Text)
                If xICode <> "" Then
                    SprdMain.Col = ColPackQty
                    mPackQty = Val(SprdMain.Text)

                    SprdMain.Col = ColShortQty
                    mCheckQty = Val(SprdMain.Text)

                    If mCheckQty > 0 And mPackQty < mCheckQty Then
                        MsgInformation("Short Qty cann't be greater than despatch Qty.")
                        MainClass.SetFocusToCell(SprdMain, EventArgs.row, EventArgs.col)
                        Exit Sub
                    End If
                    Call CalcTots()
                End If
            Case ColRejQty
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = Trim(SprdMain.Text)
                If xICode <> "" Then
                    SprdMain.Col = ColPackQty
                    mPackQty = Val(SprdMain.Text)

                    SprdMain.Col = ColRejQty
                    mCheckQty = Val(SprdMain.Text)

                    If mCheckQty > 0 And mPackQty < mCheckQty Then
                        MsgInformation("Rejection Qty cann't be greater than despatch Qty.")
                        MainClass.SetFocusToCell(SprdMain, EventArgs.row, EventArgs.col)
                        Exit Sub
                    End If
                    Call CalcTots()
                End If
        End Select
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim mItemCode As String

        Dim I As Integer
        Dim j As Integer
        Dim mRejQty As Double
        Dim mShortQty As Double

        If IsDate(txtInvoiceDate.Text) Then
            If CDate(txtInvoiceDate.Text) < CDate("01/05/2024") Then Exit Sub
        End If

        mRejQty = 0
        mShortQty = 0
        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If mItemCode <> "" Then
                    .Col = ColRejQty
                    mRejQty = mRejQty + Val(.Text)

                    .Col = ColShortQty
                    mShortQty = mShortQty + Val(.Text)
                End If
            Next I
        End With

        txtRejectedQty.Text = VB6.Format(mRejQty, "0.00")
        txtShortageQty.Text = VB6.Format(mShortQty, "0.00")

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
End Class
