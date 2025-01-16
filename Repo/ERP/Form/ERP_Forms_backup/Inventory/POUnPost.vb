Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPoUnpost
   Inherits System.Windows.Forms.Form
   'Dim PvtDBCN As ADODB.Connection
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   Dim FileDBCn As ADODB.Connection
   Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
   End Sub
   Private Function FieldVarification() As Boolean
      On Error GoTo ERR1
      FieldVarification = True
      If txtPONo.Text = "" Then
         MsgBox("Please Select PO No.")
         FieldVarification = False
         txtPONo.Focus()
         Exit Function
      End If

        'If chkPOStatus.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '   MsgBox("Already UnPosted.")
        '   FieldVarification = False
        '   txtPONo.Focus()
        '   Exit Function
        'End If
        Exit Function
ERR1:
      MsgBox(Err.Description)
      FieldVarification = False
   End Function
   Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click

      On Error GoTo ErrorHandler
      Dim mMkey As Double
        Dim SqlStr As String = ""
        Dim mInputKey As String = ""
        Dim mPassword As String = ""
        Dim mIsCapital As String = "N"

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '

        mIsCapital = IIf(chkAssetsPost.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", "Y")

        If chkPOStatus.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mPassword = VB6.Format(VB.Day(PubCurrDate), "00")
            mPassword = mPassword & VB.Left(txtSupplier.Text, 1)
            'mPassword = mPassword & vb.Left(txtPONo.Text, 2)
            mPassword = mPassword & VB6.Format(Month(PubCurrDate), "00")

            mInputKey = ""
            mInputKey = InputBox("Enter Password :", "Admin Password")

            If UCase(mInputKey) <> UCase(mPassword) Then
                MsgInformation("Password Not Match")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If

            If MsgQuestion("Are you Sure to UnPost PO.") = CStr(MsgBoxResult.No) Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
        End If

        If FieldVarification() = False Then GoTo ExitProc

        mMkey = CDbl(Val(txtPONo.Text) & VB6.Format(txtAmendNo.Text, "000"))

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If chkPOStatus.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            ''Closed Current PO
            SqlStr = "UPDATE PUR_PURCHASE_HDR SET PO_STATUS='N',PO_CLOSED='N', UPDATE_FROM='N'," & vbCrLf _
               & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
               & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
               & " WHERE MKEY=" & mMkey & "" & vbCrLf _
               & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            PubDBCn.Execute(SqlStr)


            ''Open Previous PO

            If Val(txtAmendNo.Text) > 0 Then
                SqlStr = "UPDATE PUR_PURCHASE_HDR SET PO_STATUS='Y',PO_CLOSED='N', UPDATE_FROM='N'," & vbCrLf _
                   & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                   & " WHERE AUTO_KEY_PO=" & Val(txtPONo.Text) & "" & vbCrLf _
                   & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND AMEND_NO=" & Val(txtAmendNo.Text) - 1 & ""

                PubDBCn.Execute(SqlStr)

            End If
        Else
            SqlStr = "UPDATE PUR_PURCHASE_HDR SET ISCAPITAL='" & mIsCapital & "'," & vbCrLf _
               & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
               & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
               & " WHERE MKEY=" & mMkey & "" & vbCrLf _
               & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            PubDBCn.Execute(SqlStr)

        End If


        PubDBCn.CommitTrans()

        CmdSave.Enabled = False
        GoTo ExitProc
ErrorHandler:
        If Err.Number = 75 Then
            MsgBox(Err.Description)
            CmdSave.Enabled = False
        Else
            MsgBox(Err.Description)
            '         Resume
        End If
        PubDBCn.RollbackTrans()
ExitProc:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
            SqlStr = SqlStr & vbCrLf & "AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        Else
            SqlStr = SqlStr & vbCrLf & "AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""
        End If

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND PUR_TYPE='" & vb.Left(lblBookType.text, 1) & "'" & vbCrLf _
        ''            & " AND ORDER_TYPE='" & Right(lblBookType.text, 1) & "'"

        If MainClass.SearchGridMaster(txtPONo.Text, "PUR_PURCHASE_HDR", "AUTO_KEY_PO", "AMEND_NO", "PUR_ORD_DATE", "SUPP_CUST_CODE", SqlStr) = True Then
            txtPONo.Text = AcName
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmPoUnpost_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Me.Top = VB6.TwipsToPixelsY(25)
        'Me.Left = VB6.TwipsToPixelsX(25)
        'Me.Height = VB6.TwipsToPixelsY(2940)
        'Me.Width = VB6.TwipsToPixelsX(5400)
        'Set PvtDBCN = New ADODB.Connection
        'PvtDBCN.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        If XRIGHT <> "" Then MODIFYMode = True
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        CmdSave.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub frmPoUnpost_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'PvtDBCN.Cancel
        'PvtDBCN.Close
        'Set PvtDBCN = Nothing
    End Sub
    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Function GetMaxAmendNo(ByRef pPONo As Double) As Integer
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf _
           & " FROM PUR_PURCHASE_HDR" & vbCrLf _
           & " WHERE " & vbCrLf _
           & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
           & " AND AUTO_KEY_PO=" & Val(CStr(pPONo)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetMaxAmendNo = Val(RsTemp.Fields("AMEND_NO").Value)
        Else
            GetMaxAmendNo = 0
        End If

        Exit Function
ErrPart:
        GetMaxAmendNo = 0
    End Function
    Private Sub txtPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPONo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mPONo As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSupplierCode As String

        If Trim(txtPONo.Text) = "" Then GoTo EventExitSub
        txtAmendNo.Text = ""
        chkPOStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAssetsPost.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtSupplier.Text = ""

        mPONo = Val(txtPONo.Text)

        SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & vbCrLf _
           & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
           & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""
        End If

        SqlStr = SqlStr & vbCrLf _
           & " AND AMEND_NO = (" & vbCrLf _
           & " SELECT MAX(AMEND_NO) AS AMEND_NO FROM PUR_PURCHASE_HDR" & vbCrLf _
           & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
           & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ") AND PO_STATUS='Y' AND PO_CLOSED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            txtAmendNo.Text = IIf(IsDbNull(RsTemp.Fields("AMEND_NO").Value), "", RsTemp.Fields("AMEND_NO").Value)
            chkPOStatus.CheckState = IIf(RsTemp.Fields("PO_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            mSupplierCode = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
            chkAssetsPost.CheckState = IIf(RsTemp.Fields("ISCAPITAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkAssetsPost.Enabled = True
            chkPOStatus.Enabled = True
            If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSupplier.Text = MasterNo
            End If
        Else
            MsgInformation("Invalid PO No.")
            CmdSave.Enabled = False
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class