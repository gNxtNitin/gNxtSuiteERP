Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Public Module BatchPreFixModule

    Public Function GetBatchPrefixCode(ByRef mItemCode As String, ByRef mDate As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim BatchSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim BATCH_SEQTYPE As String = ""
        Dim BATCH_PREFIX As String = ""
        Dim BATCH_SEQ As String = ""
        Dim yearString As String = ""
        Dim monthNumber As String = ""

        GetBatchPrefixCode = ""
        SqlStr = "SELECT INVGEN.BATCH_REQUIRE,INVGEN.BATCH_SEQ,INVGEN.BATCH_PREFIX FROM INV_ITEM_MST INITEM,INV_GENERAL_MST INVGEN" & vbCrLf _
        & "WHERE INITEM.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND INITEM.CATEGORY_CODE = INVGEN.GEN_CODE AND INITEM.ITEM_CODE='" & mItemCode & "' AND INVGEN.BATCH_REQUIRE= '1'"
        'add company code check with RsCompany.Fields(
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            BATCH_SEQTYPE = IIf(IsDBNull(RsTemp.Fields("BATCH_SEQ").Value), "", RsTemp.Fields("BATCH_SEQ").Value)

            yearString = VB6.Format(RsCompany.Fields("START_DATE").Value, "YY") & "-" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")

            monthNumber = Convert.ToDateTime(mDate).ToString("MM")

            BATCH_PREFIX = IIf(IsDBNull(RsTemp.Fields("BATCH_PREFIX").Value), "", RsTemp.Fields("BATCH_PREFIX").Value)

            If (BATCH_SEQTYPE = "M") Then
                BATCH_PREFIX = BATCH_PREFIX & "/" & yearString & "/" & monthNumber & "/" ''KAC/24-25/02/
            Else
                BATCH_PREFIX = BATCH_PREFIX & "/" & yearString & "/"
            End If

            BatchSqlStr = "SELECT MAX(SUBSTR(BATCH_NO, LENGTH('" & BATCH_PREFIX & "') + 1, LENGTH(BATCH_NO) -  LENGTH('" & BATCH_PREFIX & "'))) AS BATCH_SEQNO " & vbCrLf _
                    & " FROM INV_GATE_DET WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND SUBSTR(BATCH_NO, 1, LENGTH('" & BATCH_PREFIX & "'))='" & BATCH_PREFIX & "'  ORDER BY MRR_DATE DESC"

            '' "kac" KAC/24-25/02/0001
            MainClass.UOpenRecordSet(BatchSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                BATCH_SEQ = 1
            Else
                BATCH_SEQ = Val(IIf(IsDBNull(RsTemp.Fields("BATCH_SEQNO").Value), 0, RsTemp.Fields("BATCH_SEQNO").Value)) + 1
            End If
            GetBatchPrefixCode = BATCH_PREFIX + VB6.Format(BATCH_SEQ, "0000")

        Else
            GetBatchPrefixCode = ""
        End If

        Exit Function
ErrPart:
        GetBatchPrefixCode = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Module
