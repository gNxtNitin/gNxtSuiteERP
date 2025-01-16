Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Module OperationModule
    Public Function OperationQuery(ByRef pProductCode As String, ByRef pDeptCode As String, ByRef pOPRCode As String, ByRef pOPRDesc As String, ByRef pDate As String,
                                   ByRef mField1 As String, Optional ByRef mField2 As String = "", Optional ByRef mField3 As String = "", Optional ByRef mField4 As String = "",
                                   Optional ByRef mField5 As String = "", Optional ByRef pCompanyCode As Long = 0) As String

        Dim SqlStr As Object
        On Error GoTo ErrPart

        SqlStr = " SELECT " & mField1 & ""

        If mField2 <> "" Then
            SqlStr = SqlStr & vbCrLf & ", " & mField2 & ""
        End If

        If mField3 <> "" Then
            SqlStr = SqlStr & vbCrLf & ", " & mField3 & ""
        End If

        If mField4 <> "" Then
            SqlStr = SqlStr & vbCrLf & ", " & mField4 & ""
        End If

        If mField5 <> "" Then
            SqlStr = SqlStr & vbCrLf & ", " & mField5 & ""
        End If

        SqlStr = SqlStr & vbCrLf & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _
            & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & IIf(pCompanyCode = 0, RsCompany.Fields("COMPANY_CODE").Value, pCompanyCode) & "" & vbCrLf _
            & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf & " AND TRN.OPR_CODE=MST.OPR_CODE "

        If Trim(pDeptCode) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.DEPT_CODE ='" & MainClass.AllowSingleQuote(pDeptCode) & "'"
        End If

        If Trim(pOPRCode) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND MST.OPR_CODE ='" & MainClass.AllowSingleQuote(pOPRCode) & "'"
        End If

        If Trim(pOPRDesc) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND MST.OPR_DESC ='" & MainClass.AllowSingleQuote(pOPRDesc) & "'"
        End If

        If Trim(pProductCode) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND WEF = ( SELECT MAX(WEF) FROM PRD_OPR_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & IIf(pCompanyCode = 0, RsCompany.Fields("COMPANY_CODE").Value, pCompanyCode) & ""

        If Trim(pDeptCode) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE ='" & MainClass.AllowSingleQuote(pDeptCode) & "'"
        End If

        If Trim(pProductCode) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'"
        End If

        If pDate = "" Or pDate = "__/__/____" Then

        Else
            SqlStr = SqlStr & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(pDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf & ")"

        If UCase(mField1) = "ISOPTIONAL" Then

        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY " & mField1 & ""
        End If

        OperationQuery = SqlStr
        Exit Function
ErrPart:
        OperationQuery = ""
    End Function
End Module
