Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Module GSTModule
    Public Function GetGSTDutyOPBal(ByRef mFieldType As String, ByRef pTillDate As String, ByRef IsReverseCharge As String) As Double
        On Error GoTo ErrPart
        Dim mOpening As Double
        Dim mSql As String
        Dim RsTemp As ADODB.Recordset

        GetGSTDutyOPBal = 0

        mSql = "SELECT CGST_OP_AMT, SGST_OP_AMT, IGST_OP_AMT, " & vbCrLf & " CGST_RCOP_AMT, SGST_RCOP_AMT, IGST_RCOP_AMT " & vbCrLf _
            & " FROM FIN_GSTOPAMT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        MainClass.UOpenRecordSet(mSql, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsReverseCharge = "N" Then
                If mFieldType = "C" Then
                    mOpening = IIf(IsDBNull(RsTemp.Fields("CGST_OP_AMT").Value), 0, RsTemp.Fields("CGST_OP_AMT").Value)
                ElseIf mFieldType = "S" Then
                    mOpening = IIf(IsDBNull(RsTemp.Fields("SGST_OP_AMT").Value), 0, RsTemp.Fields("SGST_OP_AMT").Value)
                ElseIf mFieldType = "I" Then
                    mOpening = IIf(IsDBNull(RsTemp.Fields("IGST_OP_AMT").Value), 0, RsTemp.Fields("IGST_OP_AMT").Value)
                ElseIf mFieldType = "X" Then
                    mOpening = 0
                End If
            Else
                If mFieldType = "C" Then
                    mOpening = IIf(IsDBNull(RsTemp.Fields("CGST_RCOP_AMT").Value), 0, RsTemp.Fields("CGST_OP_AMT").Value)
                ElseIf mFieldType = "S" Then
                    mOpening = IIf(IsDBNull(RsTemp.Fields("SGST_RCOP_AMT").Value), 0, RsTemp.Fields("SGST_OP_AMT").Value)
                ElseIf mFieldType = "I" Then
                    mOpening = IIf(IsDBNull(RsTemp.Fields("IGST_RCOP_AMT").Value), 0, RsTemp.Fields("IGST_OP_AMT").Value)
                ElseIf mFieldType = "X" Then
                    mOpening = 0
                End If

            End If
        Else
            mOpening = 0
        End If
        GetGSTDutyOPBal = mOpening
        Exit Function
ErrPart:
        GetGSTDutyOPBal = 0
    End Function


    Public Function GetGSTDutyAmount(ByRef mFieldType As String, ByRef pDC As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Double
        On Error GoTo ERR1
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        ' IS_REVERSE_CHARGE , ' GOOD_SERVICE				
        GetGSTDutyAmount = 0

        If mFieldType = "C" Then
            mSqlStr = "SELECT SUM(REFUNDABLE_CGST_AMOUNT) AS GSTAMOUNT "
        ElseIf mFieldType = "S" Then
            mSqlStr = "SELECT SUM(REFUNDABLE_SGST_AMOUNT) AS GSTAMOUNT "
        ElseIf mFieldType = "I" Then
            mSqlStr = "SELECT SUM(REFUNDABLE_IGST_AMOUNT) AS GSTAMOUNT "
        ElseIf mFieldType = "X" Then
            mSqlStr = "SELECT 0 AS GSTAMOUNT "
        End If

        mSqlStr = mSqlStr & vbCrLf & " FROM FIN_GST_POST_TRN B" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ISPLA='N' AND GST_DC='" & pDC & "'"

        mSqlStr = mSqlStr & vbCrLf & " AND GST_CLAIM_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND GST_CLAIM_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetGSTDutyAmount = IIf(IsDBNull(RsTemp.Fields("GSTAMOUNT").Value), 0, RsTemp.Fields("GSTAMOUNT").Value)
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
        GetGSTDutyAmount = 0
    End Function
End Module
