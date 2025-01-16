Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmDespNoteSeq
    Inherits System.Windows.Forms.Form

    'Private PvtDBCN As ADODB.Connection			

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        lblStatus.Visible = True

        If UpdateMain1() = False Then
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            lblStatus.Visible = False
            cmdSave.Enabled = False
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume			
    End Sub
    Private Function FieldVarification() As Boolean
        On Error GoTo err_Renamed
        FieldVarification = True


        If Val(txtFromDNo.Text) = 0 Then
            MsgInformation("from Despatch Note is empty. Cannot Update")
            txtFromDNo.Focus()
            FieldVarification = False
            Exit Function
        End If

        'UPGRADE_WARNING: Untranslated statement in FieldVarification. Please check source code.			


        If Val(txtToDNo.Text) = 0 Then
            MsgInformation("Despatch Note is empty. Cannot Update")
            txtToDNo.Focus()
            FieldVarification = False
            Exit Function
        End If

        If optUpdate(0).Checked = True Then
            If MainClass.ValidateWithMasterTable(Val(txtToDNo.Text), "AUTO_KEY_DESP", "AUTO_KEY_DESP", "DSP_DESPATCH_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Despatch Note. Cannot Update")
                txtToDNo.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub FrmDespNoteSeq_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Function UpdateMain1() As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mSqlStr As String
        Dim mNewDNNo As Double
        Dim mOldDNNo As Double
        Dim mPreFix As Double
        Dim mNewDNNoStr As Double
        Dim mOldDNNoStr As Double
        Dim mNewDNNoStr1 As Double
        Dim mTableName As String

        mNewDNNo = Val(Mid(txtFromDNo.Text, 1, Len(txtFromDNo.Text) - 6))
        mOldDNNo = Val(Mid(txtToDNo.Text, 1, Len(txtToDNo.Text) - 6))

        mNewDNNoStr = Val(txtFromDNo.Text)
        mOldDNNoStr = Val(txtToDNo.Text)

        mTableName = ConInventoryTable

        '    If RsCompany!COMPANY_CODE = 1 Then			
        '        mTableName = "INV_STOCK_REC_TRN" & RsCompany!FYEAR			
        '    ElseIf RsCompany!COMPANY_CODE = 3 Or RsCompany!COMPANY_CODE = 10 Or RsCompany!COMPANY_CODE = 12 Then			
        '        mTableName = "INV_STOCK_REC_TRN" & vb6.Format(RsCompany!COMPANY_CODE, "00") & RsCompany!FYEAR			
        '    Else			
        '        mTableName = "INV_STOCK_REC_TRN"			
        '    End If			


        mPreFix = CDbl(VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        If optUpdate(0).Checked = True Then
            mNewDNNoStr1 = CDbl(Val(Mid(txtFromDNo.Text, 1, Len(txtFromDNo.Text) - 6)) + 1 & mPreFix)
        Else
            mNewDNNoStr1 = Val(txtFromDNo.Text)
        End If


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        'mSqlStr = "ALTER TABLE DSP_DESPATCH_DET MODIFY CONSTRAINT DSP_DESP_DET_FK DISABLE"
        'PubDBCn.Execute(mSqlStr)

        'mSqlStr = "ALTER TABLE DSP_DESPATCH_DET MODIFY CONSTRAINT DSP_DESP_DET_PK DISABLE"
        'PubDBCn.Execute(mSqlStr)

        If optUpdate(0).Checked = True Then
            SqlStr = " UPDATE DSP_DESPATCH_DET SET " & vbCrLf _
                & " AUTO_KEY_DESP=SUBSTR(AUTO_KEY_DESP,1, LENGTH(AUTO_KEY_DESP)-6) + 1 || " & mPreFix & "" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND SUBSTR(AUTO_KEY_DESP,1, LENGTH(AUTO_KEY_DESP)-6) >= " & mOldDNNo & "" ''& vbCrLf |                & " AND SUBSTR(AUTO_KEY_DESP,1, LENGTH(AUTO_KEY_DESP)-6) <> " & mNewDNNo & ""			

            PubDBCn.Execute(SqlStr)

            SqlStr = " UPDATE DSP_PAINT57F4_TRN SET " & vbCrLf & " MKEY=SUBSTR(MKEY,1, LENGTH(MKEY)-6) + 1 || " & mPreFix & "," & vbCrLf & " BILL_NO=SUBSTR(MKEY,1, LENGTH(MKEY)-6) + 1 || " & mPreFix & "" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND SUBSTR(MKEY,1, LENGTH(MKEY)-6) >= " & mOldDNNo & "" & vbCrLf & " AND " & vbCrLf & " BookType='D' AND BookSubType='O' AND TRNTYPE='D'"

            PubDBCn.Execute(SqlStr)

            ''SUBSTR(MKEY,1, LENGTH(MKEY)-6) <> " & mNewDNNo & "			

            SqlStr = " UPDATE " & mTableName & " SET " & vbCrLf & " REF_NO=SUBSTR(REF_NO,1, LENGTH(REF_NO)-6) + 1 || " & mPreFix & "" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND SUBSTR(REF_NO,1, LENGTH(REF_NO)-6) >= " & mOldDNNo & "" & vbCrLf & " AND " & vbCrLf _
                & " REF_TYPE='" & ConStockRefType_DSP & "'" & vbCrLf & " AND STOCK_ID='" & ConWH & "'"

            PubDBCn.Execute(SqlStr)
            ''AND SUBSTR(REF_NO,1, LENGTH(REF_NO)-6) <> " & mNewDNNo & "			

            SqlStr = " UPDATE DSP_DESPATCH_HDR SET " & vbCrLf _
                & " AUTO_KEY_DESP=SUBSTR(AUTO_KEY_DESP,1, LENGTH(AUTO_KEY_DESP)-6) + 1 || " & mPreFix & "" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND SUBSTR(AUTO_KEY_DESP,1, LENGTH(AUTO_KEY_DESP)-6) >= " & mOldDNNo & "" ''' & vbCrLf |                & " AND SUBSTR(AUTO_KEY_DESP,1, LENGTH(AUTO_KEY_DESP)-6) <> " & mNewDNNo & ""			

            PubDBCn.Execute(SqlStr)

            SqlStr = " UPDATE FIN_INVOICE_HDR SET " & vbCrLf & " AUTO_KEY_DESP=SUBSTR(AUTO_KEY_DESP,1, LENGTH(AUTO_KEY_DESP)-6) + 1 || " & mPreFix & "," & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND SUBSTR(AUTO_KEY_DESP,1, LENGTH(AUTO_KEY_DESP)-6) >= " & mOldDNNo & " AND TRNTYPE IN (SELECT CODE FROM FIN_INVTYPE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IDENTIFICATION <>'P')" '''& vbCrLf |                & " AND SUBSTR(AUTO_KEY_DESP,1, LENGTH(AUTO_KEY_DESP)-6) <> " & mNewDNNo & ""			

            PubDBCn.Execute(SqlStr)
        End If

        ''Update .....			
        SqlStr = " UPDATE DSP_DESPATCH_DET SET " & vbCrLf & " AUTO_KEY_DESP=" & mOldDNNoStr & ", " & vbCrLf _
            & " DESP_DATE=TO_DATE('" & VB6.Format(txtToDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AUTO_KEY_DESP = " & mNewDNNoStr1 & ""

        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE DSP_PAINT57F4_TRN SET " & vbCrLf & " MKEY=" & mOldDNNoStr & "," & vbCrLf & " BILL_NO=" & mOldDNNoStr & "," & vbCrLf _
            & " BILL_DATE=TO_DATE('" & VB6.Format(txtToDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND MKEY = " & mNewDNNoStr1 & "" & vbCrLf & " AND BookType='D' AND BookSubType='O' AND TRNTYPE='D'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE " & mTableName & " SET " & vbCrLf & " REF_NO=" & mOldDNNoStr & "," & vbCrLf _
            & " REF_DATE=TO_DATE('" & VB6.Format(txtToDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND REF_NO= " & mNewDNNoStr1 & "" & vbCrLf _
            & " AND REF_TYPE='" & ConStockRefType_DSP & "'" & vbCrLf & " AND STOCK_ID='" & ConWH & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE FIN_INVOICE_HDR SET " & vbCrLf & " AUTO_KEY_DESP=" & mOldDNNoStr & "," & vbCrLf _
            & " DCDATE=TO_DATE('" & VB6.Format(txtToDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " UPDATE_FROM='H'," & vbCrLf _
            & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AUTO_KEY_DESP = " & mNewDNNoStr1 & ""

        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE DSP_DESPATCH_HDR SET " & vbCrLf & " AUTO_KEY_DESP=" & mOldDNNoStr & "," & vbCrLf _
            & " DESP_DATE=TO_DATE('" & VB6.Format(txtToDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AUTO_KEY_DESP = " & mNewDNNoStr1 & ""

        PubDBCn.Execute(SqlStr)

        'mSqlStr = "ALTER TABLE DSP_DESPATCH_DET MODIFY CONSTRAINT DSP_DESP_DET_FK ENABLE"
        'PubDBCn.Execute(mSqlStr)

        'mSqlStr = "ALTER TABLE DSP_DESPATCH_DET MODIFY CONSTRAINT DSP_DESP_DET_PK ENABLE"
        'PubDBCn.Execute(mSqlStr)

        PubDBCn.CommitTrans()
        UpdateMain1 = True

        Exit Function
ErrPart:
        'Resume			
        'mSqlStr = "ALTER TABLE DSP_DESPATCH_DET MODIFY CONSTRAINT DSP_DESP_DET_FK ENABLE"
        'PubDBCn.Execute(mSqlStr)

        'mSqlStr = "ALTER TABLE DSP_DESPATCH_DET MODIFY CONSTRAINT DSP_DESP_DET_PK ENABLE"
        'PubDBCn.Execute(mSqlStr)

        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateMain1 = False
        PubDBCn.RollbackTrans()
    End Function
    Public Sub FrmDespNoteSeq_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume			
    End Sub
    Public Sub FrmDespNoteSeq_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCN = New ADODB.Connection			
        'PvtDBCN.Open StrConn			

        '    XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)			
        '    MainClass.RightsToButton Me, XRIGHT			


        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(3405) '''8000			
        'Me.Width = VB6.TwipsToPixelsX(8040) '''11900			

        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtFromDNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFromDNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFromDNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFromDNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If Val(txtFromDNo.Text) = 0 Then GoTo EventExitSub

        SqlStr = "Select AUTO_KEY_DESP, DESP_DATE From DSP_DESPATCH_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AUTO_KEY_DESP=" & Val(txtFromDNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtFromDNDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DESP_DATE").Value), "", RsTemp.Fields("DESP_DATE").Value), "DD/MM/YYYY")
        Else
            MsgBox("Invalid Despatch Note No.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtToDNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToDNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtToDNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToDNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If Val(txtToDNo.Text) = 0 Then GoTo EventExitSub

        SqlStr = "Select AUTO_KEY_DESP, DESP_DATE From DSP_DESPATCH_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AUTO_KEY_DESP=" & Val(txtToDNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtToDNDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DESP_DATE").Value), "", RsTemp.Fields("DESP_DATE").Value), "DD/MM/YYYY")
        Else
            If optUpdate(0).Checked = True Then
                MsgBox("Invalid Despatch Note No. want", MsgBoxStyle.Information)
                Cancel = True
            End If

        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
