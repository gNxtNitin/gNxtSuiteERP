Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Imports CrystalDecisions.CrystalReports.Engine ''CrystalDecisions.CrystalReports.Engine				
Imports CrystalDecisions.Shared
'Imports CrystalDecisions.Web.Design
Imports System.IO
Imports System.Data.OleDb
Imports System.Data.SqlClient   '' System.Data.OleDb
Imports System.Data
Imports System.Configuration



'Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6				
Module CrystalReport11
    'Option Explicit				

    ' API functions and constants used in EnumPrinterBins				
    Private Declare Function OpenPrinter Lib "winspool.drv" Alias "OpenPrinterA" (ByVal pPrinterName As String, ByRef phPrinter As Integer, ByVal pDefault As Integer) As Integer
    Private Declare Function ClosePrinter Lib "winspool.drv" (ByVal hPrinter As Integer) As Integer
    Private Declare Function DeviceCapabilities Lib "winspool.drv" Alias "DeviceCapabilitiesA" (ByVal lpDeviceName As String, ByVal lpPort As String, ByVal iIndex As Integer, ByRef lpOutput As String, ByVal dev As Integer) As Integer


    Private Const DC_BINS As Short = 6
    Private Const DC_BINNAMES As Short = 12

    Public tables As CRAXDRT.DatabaseTables

    Private Sub SetDBLogonForReport(ByVal myConnectionInfo As ConnectionInfo, ByVal myReportDocument As ReportDocument)
        Dim myTables As Tables = myReportDocument.Database.Tables
        Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
        For Each myTable In myTables
            Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
            myTableLogonInfo.ConnectionInfo = myConnectionInfo
            myTable.ApplyLogOnInfo(myTableLogonInfo)
            'myTable.Location = DBConUID + "." + myTable.Name
        Next
    End Sub

    Private Sub SetDBLogonForSubreports(ByVal myConnectionInfo As ConnectionInfo, ByVal myReportDocument As ReportDocument)
        'Dim mySections As Sections = myReportDocument.ReportDefinition.Sections
        'Dim mySection As Section
        'For Each mySection In mySections
        '    Dim myReportObjects As ReportObjects = mySection.ReportObjects
        '    Dim myReportObject As ReportObject
        '    For Each myReportObject In myReportObjects
        '        If myReportObject.Kind = ReportObjectKind.SubreportObject Then
        '            Dim mySubreportObject As SubreportObject = CType(myReportObject, SubreportObject)
        '            Dim subReportDocument As ReportDocument = mySubreportObject.OpenSubreport(mySubreportObject.SubreportName)
        '            SetDBLogonForReport(myConnectionInfo, subReportDocument)
        '        End If
        '    Next
        'Next
    End Sub
    Public Sub Connect_MainReport_To_Database_11(ByRef rep As ReportDocument)
        Dim tablecount As Object
        Dim csprop As Object
        On Error GoTo ErrPart
        Dim i As Short
        Dim j As Short
        Dim RsTemp As ADODB.Recordset = Nothing

        'Dim ds As New DataTable()
        'ds.Clear()
        'Using da As New OleDbDataAdapter(pSqlStr, PubDBCnDataGrid)
        '    da.Fill(ds)
        'End Using

        Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()

        myConnectionInfo.ServerName = DBConDSN
        myConnectionInfo.DatabaseName = DBConSERVICENAME
        myConnectionInfo.UserID = DBConUID
        myConnectionInfo.Password = DBConPWD

        SetDBLogonForReport(myConnectionInfo, rep)
        'rep.SetDatabaseLogon(DBConUID, DBConPWD, DBConDSN, DBConSERVICENAME)

        'rep.SetDataSource(ds)       ''ds


        'rep.VerifyDatabase()
        'rep.Refresh()
        'CrystalReportViewer.ReportSource = ReportDocument
        'CrystalReportViewer.Refresh()


        Exit Sub
ErrPart:
        'Resume				
        MsgInformation(Err.Description)
    End Sub
    Public Sub Connect_SubReport_To_Database_11(ByRef rep As ReportDocument, ByRef pSubReportName As String)
        Dim tablecount As Object
        Dim csprop As Object
        On Error GoTo ErrPart
        Dim i As Short
        Dim j As Short
        Dim RsTemp As ADODB.Recordset = Nothing

        'Dim ds As New DataTable()

        'Using da As New OleDbDataAdapter(pSqlStr, PubDBCnDataGrid)
        '    da.Fill(ds)
        'End Using

        Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()

        myConnectionInfo.ServerName = DBConDSN
        myConnectionInfo.DatabaseName = DBConSERVICENAME
        myConnectionInfo.UserID = DBConUID
        myConnectionInfo.Password = DBConPWD

        Dim mySections As Sections = rep.ReportDefinition.Sections
        Dim mySection As Section

        For Each mySection In mySections
            Dim myReportObjects As ReportObjects = mySection.ReportObjects
            Dim myReportObject As ReportObject
            For Each myReportObject In myReportObjects
                If myReportObject.Kind = ReportObjectKind.SubreportObject Then
                    Dim mySubreportObject As SubreportObject = CType(myReportObject, SubreportObject)
                    If pSubReportName = mySubreportObject.SubreportName Then
                        Dim subReportDocument As ReportDocument = mySubreportObject.OpenSubreport(mySubreportObject.SubreportName)
                        SetDBLogonForReport(myConnectionInfo, subReportDocument)
                        'If pValue <> "" Then
                        '    subReportDocument.RecordSelectionFormula = "{FIN_INVOICE_HDR.MKEY} = '" & MainClass.AllowSingleQuote(pValue) & "' AND {FIN_INTERFACE_MST.COMPANY_CODE} = " & RsCompany.Fields("COMPANY_CODE").Value & ""
                        'End If
                    End If
                End If
            Next
        Next



        Exit Sub
ErrPart:
        'Resume				
        MsgInformation(Err.Description)
    End Sub
    Public Function GetPictureSectionName_11(rep As ReportDocument, pPicName As String, mBMPFileName As String) As String
        On Error GoTo ErrPart
        Dim tablecount As Object
        Dim csprop As Object

        Dim mySections As Sections = rep.ReportDefinition.Sections
        Dim mySection As Section

        For Each mySection In mySections
            Dim myReportObjects As ReportObjects = mySection.ReportObjects
            Dim myReportObject As ReportObject
            For Each myReportObject In myReportObjects
                If myReportObject.Kind = ReportObjectKind.PictureObject Then
                    If myReportObject.Name = pPicName Then
                        'myReportObject.ObjectFormat(Image)  Image.FromFile(mBMPFileName)
                        'Dim pic = CType(rep.ReportDefinition.ReportObjects(pPicName), PictureObject)
                        'pic.ObjectFormat.EnableSuppress = True
                        'rep.ReportDefinition.ReportObjects("QRCode").ObjectFormat.l = Image.FromFile(mBMPFileName)       '(pPicName).PictureObject = 1
                        GetPictureSectionName_11 = mySection.Name
                        'myReportObject.ObjectFormat = Image.FromFile(mBMPFileName)
                        'objRpt.Sections(mPicSectionName).ReportObjects.Item("QRCode").FormattedPicture = Image.FromFile(mBMPFileName)
                        Exit Function
                    End If
                End If
            Next
        Next


        Exit Function
ErrPart:
        'Resume				
        GetPictureSectionName_11 = ""
        MsgInformation(Err.Description)
    End Function
    Public Function ClearCRpt11Formulas(ByRef Rept As ReportDocument) As Boolean 'CrystalReport				
        On Error GoTo ERR1
        Static i As Integer
        i = 1
        Do Until Trim(Rept.DataDefinition.FormulaFields(i).Text) = ""
            Rept.DataDefinition.FormulaFields(i).Text = ""
            i = i + 1
        Loop
        Exit Function
ERR1:
    End Function
    Public Function AssignCRpt11Formulas(ByRef Rept As ReportDocument, ByRef FormulaString As String, ByRef FormulaValue As String) As Boolean '' CrystalReport				
        On Error GoTo ERR1
        Dim i As Integer
        i = 0
        FormulaString = "{@" & FormulaString & "}"
        For i = 0 To Rept.DataDefinition.FormulaFields.Count - 1
            If UCase(Trim(Rept.DataDefinition.FormulaFields(i).FormulaName)) = UCase(Trim(FormulaString)) Then
                Rept.DataDefinition.FormulaFields(i).Text = FormulaValue     '' FormulaValue
                Exit For
            End If
        Next
        AssignCRpt11Formulas = True
        Exit Function

ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Function SetCompanyReport11(ByRef Report2 As ReportDocument, ByRef mNoOfCopies As Short, ByRef mTitle As String, Optional ByRef mSubTitle As String = "", Optional ByRef mDocTitle As Boolean = False, Optional ByRef xMenuID As String = "") As Boolean
        On Error GoTo ERR1
        Dim ICodeWidth As String
        Dim CompanyName As String
        Dim BranchName As String
        Dim CompanyAdd As Object
        Dim mCompanyAddress As String
        Dim UserID, CompanyPhone, RunDate As Object
        Dim PageNo As String
        Dim xDocNo As String
        Dim xOrigDate As String
        Dim xRevNo As String
        Dim xRevDate As String

        Dim pCompanyName As String

        pCompanyName = IIf(IsDBNull(RsCompany.Fields("PRINT_COMPANY_NAME").Value), "", RsCompany.Fields("PRINT_COMPANY_NAME").Value)
        pCompanyName = IIf(pCompanyName = "", RsCompany.Fields("Company_Name").Value, pCompanyName)

        If RsCompany.Fields("PrintTopCompanyName").Value = "Y" Then
            ''CompanyName = IIf(RsCompany.Fields("PrintCompanyFull_ShortName").Value = "F", RsCompany.Fields("Company_Name").Value, IIf(IsNull(RsCompany.Fields("CompanyShortName").Value), "", RsCompany.Fields("CompanyShortName").Value))				
            CompanyName = pCompanyName   '' RsCompany.Fields("Company_Name").Value
        Else
            CompanyName = ""
        End If

        ''BranchName = RsCompany.Fields("BranchName").Value				

        If RsCompany.Fields("PrintTopCompanyAddress").Value = "Y" Then
            CompanyAdd = "" & RsCompany.Fields("COMPANY_ADDR").Value & ",  " & RsCompany.Fields("COMPANY_CITY").Value & " , " & RsCompany.Fields("COMPANY_STATE").Value & " - " & RsCompany.Fields("COMPANY_PIN").Value & ""
        Else
            CompanyAdd = ""
        End If
        If RsCompany.Fields("PRintTopCompanyPhone").Value = "Y" Then
            CompanyPhone = "Phone : " & RsCompany.Fields("COMPANY_PHONE").Value & " Fax : " & RsCompany.Fields("COMPANY_FAXNO").Value & " e-Mail : " & RsCompany.Fields("COMPANY_MAILID").Value
        Else
            CompanyPhone = ""
        End If
        If RsCompany.Fields("PrintTopCompanyAddress").Value = "N" Then
            mCompanyAddress = ""
        End If


        'Report2.DiscardSavedData()
        '    MainClass.ReportWindow Report2, mTitle				
        '    Report2.FormulaFields.GetItemByName("CompanyName").Text = "" & CompanyName & ""				
        AssignCRpt11Formulas(Report2, "CompanyName", "'" & CompanyName & "'")
        AssignCRpt11Formulas(Report2, "CompanyAddress", "'" & CompanyAdd & "'")
        AssignCRpt11Formulas(Report2, "Title", "'" & UCase(mTitle) & "'")
        AssignCRpt11Formulas(Report2, "SubTitle", "'" & mSubTitle & "'")



        If RsCompany.Fields("PrintBotCompanyName").Value = "Y" Then
            CompanyName = pCompanyName   '' RsCompany.Fields("Company_Name").Value
        Else
            CompanyName = ""
        End If
        CompanyAdd = IIf(RsCompany.Fields("PrintBotCompanyAddress").Value = "Y", "" & RsCompany.Fields("COMPANY_ADDR").Value & " ,    " & RsCompany.Fields("COMPANY_CITY").Value & ",    " & RsCompany.Fields("COMPANY_STATE").Value & " -   " & RsCompany.Fields("COMPANY_PIN").Value & "", "")
        CompanyPhone = IIf(RsCompany.Fields("PrintBotCompanyPhone").Value = "Y", "Phone : " & RsCompany.Fields("COMPANY_PHONE").Value & " Fax : " & RsCompany.Fields("COMPANY_FAXNO").Value & " e-mail : " & RsCompany.Fields("COMPANY_MAILID").Value, "")

        AssignCRpt11Formulas(Report2, "CompanyBotLine1", "'" & CompanyAdd & "'")
        AssignCRpt11Formulas(Report2, "CompanyBotLine2", "'" & IIf(IsDBNull(CompanyPhone), "", CompanyPhone) & "'")

        If RsCompany.Fields("Printuser").Value = "Y" Then
            UserID = PubUserID
        Else
            UserID = ""
        End If
        If RsCompany.Fields("PrintrunDate").Value = "Y" Then
            RunDate = Str(Today.ToOADate)
        Else
            RunDate = " "
        End If
        If RsCompany.Fields("PrintPageNo").Value = "Y" Then
            PageNo = "Y"
        Else
            PageNo = "N"
        End If

        If mDocTitle = True Then
            If Trim(xMenuID) <> "" Then
            End If
        End If

        AssignCRpt11Formulas(Report2, "UserID", "'" & UserID & "'")
        AssignCRpt11Formulas(Report2, "PrintDate", "'" & RunDate & "'")
        AssignCRpt11Formulas(Report2, "PrintPageNo", "'" & PageNo & "'")

        'Report2.TopMargin = IIf(IsDBNull(RsCompany.Fields("REPORTMARGINTOP").Value), 0, RsCompany.Fields("REPORTMARGINTOP").Value) * 1440
        'Report2.BottomMargin = IIf(IsDBNull(RsCompany.Fields("REPORTMARGINBOT").Value), 0, RsCompany.Fields("REPORTMARGINBOT").Value) * 1440
        'Report2.LeftMargin = IIf(IsDBNull(RsCompany.Fields("REPORTMARGINLEFT").Value), 0, RsCompany.Fields("REPORTMARGINLEFT").Value) * 1440
        'Report2.RightMargin = IIf(IsDBNull(RsCompany.Fields("REPORTMARGINRIGHT").Value), 0, RsCompany.Fields("REPORTMARGINRIGHT").Value) * 1440

        '    Report2.Connect = STRRptConn				
        SetCompanyReport11 = True
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume				
    End Function
End Module
