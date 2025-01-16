Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Imports CrystalDecisions.CrystalReports.Engine ''CrystalDecisions.CrystalReports.Engine				
Imports CrystalDecisions.Shared
'Imports CrystalDecisions.Web.Design
Imports System.IO
'Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6				
Module eMailCrystal9
    'Option Explicit				

    ' API functions and constants used in EnumPrinterBins				
    Private Declare Function OpenPrinter Lib "winspool.drv" Alias "OpenPrinterA" (ByVal pPrinterName As String, ByRef phPrinter As Integer, ByVal pDefault As Integer) As Integer
    Private Declare Function ClosePrinter Lib "winspool.drv" (ByVal hPrinter As Integer) As Integer
    Private Declare Function DeviceCapabilities Lib "winspool.drv" Alias "DeviceCapabilitiesA" (ByVal lpDeviceName As String, ByVal lpPort As String, ByVal iIndex As Integer, ByRef lpOutput As String, ByVal dev As Integer) As Integer


    Private Const DC_BINS As Short = 6
    Private Const DC_BINNAMES As Short = 12

    Public tables As CRAXDRT.DatabaseTables

    '*********************************************************************				
    ' Add a list of the available paper sources for <PrinterName> to				
    ' the combobox <cbo>				
    '				

    Public Sub EnumPrinterBins(ByRef PrinterName As String)
        '      Dim prn As Printer				
        'Dim hPrinter As Integer ' Handle of the selected printer				
        'Dim dwbins As Integer ' The number of paperbins in the printer				
        'Dim i As Integer ' counter				
        'Dim nameslist As String ' The string returned with all the bin names				
        'Dim NameBin As String ' The parsed bin name				
        'Dim numBin() As Short ' Used as part of the DeviceCapabilities API call				

        'For	Each prn In Printers			
        '	' Look through all the currently installed printers			
        '	If prn.DeviceName = PrinterName Then			
        '		' We've found our printer -- open a handle to it		
        '		If OpenPrinter(prn.DeviceName, hPrinter, 0) <> 0 Then		
        '			' Get the available bin numbers	
        '			dwbins = DeviceCapabilities(prn.DeviceName, prn.Port, DC_BINS, vbNullString, 0)	

        '			dwbins = 1	
        '			ReDim numBin(dwbins)	
        '			nameslist = New String(Chr(0), 24 * dwbins)	
        '			dwbins = DeviceCapabilities(prn.DeviceName, prn.Port, DC_BINS, numBin(1), 0)	
        '			dwbins = DeviceCapabilities(prn.DeviceName, prn.Port, DC_BINNAMES, nameslist, 0)	
        '			For i = 1 To dwbins	
        '				' For each bin number, add its corresponding name
        '				' to our combo box
        '				NameBin = Mid(nameslist, 24 * (i - 1) + 1, 24)
        '				NameBin = Left(NameBin, InStr(1, NameBin, Chr(0)) - 1)
        '				'                    cbo.AddItem NameBin
        '				'                    cbo.ItemData(cbo.NewIndex) = numBin(i)
        '			Next i	
        '			' Close the printer	
        '			Call ClosePrinter(hPrinter)	
        '		Else		
        '			' OpenPrinter failed, so we can't retrieve information about it	
        '			'                cbo.AddItem prn.DeviceName & "  <Unavailable>"	
        '		End If		
        '	End If			
        'Next prn				
    End Sub

    ''13/12/2016 Public csprop As CRAXDRT.ConnectionProperties				
    ''13/12/2016 Public cs As CRAXDRT.ConnectionProperty				
    Public Function ClearCRpt8Formulas(ByRef Rept As CRAXDRT.Report) As Boolean 'CrystalReport				
        On Error GoTo ERR1
        Static i As Integer
        i = 1
        Do Until Trim(Rept.FormulaFields(i).Text) = ""
            Rept.FormulaFields(i).Text = ""
            i = i + 1
        Loop
        Exit Function
ERR1:
    End Function
    Public Function SetCrpteMail(ByRef Report2 As CRAXDRT.Report, ByRef mNoOfCopies As Short, ByRef mTitle As String, Optional ByRef mSubTitle As String = "", Optional ByRef mDocTitle As Boolean = False, Optional ByRef xMenuID As String = "") As Boolean
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
            CompanyName = pCompanyName '' RsCompany.Fields("Company_Name").Value
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


        Report2.DiscardSavedData()
        '    MainClass.ReportWindow Report2, mTitle				
        '    Report2.FormulaFields.GetItemByName("CompanyName").Text = "" & CompanyName & ""				
        AssignCRpt8Formulas(Report2, "CompanyName", "'" & CompanyName & "'")
        AssignCRpt8Formulas(Report2, "CompanyAddress", "'" & CompanyAdd & "'")
        AssignCRpt8Formulas(Report2, "Title", "'" & UCase(mTitle) & "'")
        AssignCRpt8Formulas(Report2, "SubTitle", "'" & mSubTitle & "'")



        If RsCompany.Fields("PrintBotCompanyName").Value = "Y" Then
            CompanyName = pCompanyName ''RsCompany.Fields("Company_Name").Value
        Else
            CompanyName = ""
        End If
        CompanyAdd = IIf(RsCompany.Fields("PrintBotCompanyAddress").Value = "Y", "" & RsCompany.Fields("COMPANY_ADDR").Value & " ,    " & RsCompany.Fields("COMPANY_CITY").Value & ",    " & RsCompany.Fields("COMPANY_STATE").Value & " -   " & RsCompany.Fields("COMPANY_PIN").Value & "", "")
        CompanyPhone = IIf(RsCompany.Fields("PrintBotCompanyPhone").Value = "Y", "Phone : " & RsCompany.Fields("COMPANY_PHONE").Value & " Fax : " & RsCompany.Fields("COMPANY_FAXNO").Value & " e-mail : " & RsCompany.Fields("COMPANY_MAILID").Value, "")

        AssignCRpt8Formulas(Report2, "CompanyBotLine1", "'" & CompanyAdd & "'")
        AssignCRpt8Formulas(Report2, "CompanyBotLine2", "'" & IIf(IsDBNull(CompanyPhone), "", CompanyPhone) & "'")

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

        AssignCRpt8Formulas(Report2, "UserID", "'" & UserID & "'")
        AssignCRpt8Formulas(Report2, "PrintDate", "'" & RunDate & "'")
        AssignCRpt8Formulas(Report2, "PrintPageNo", "'" & PageNo & "'")

        Report2.TopMargin = IIf(IsDBNull(RsCompany.Fields("REPORTMARGINTOP").Value), 0, RsCompany.Fields("REPORTMARGINTOP").Value) * 1440
        Report2.BottomMargin = IIf(IsDBNull(RsCompany.Fields("REPORTMARGINBOT").Value), 0, RsCompany.Fields("REPORTMARGINBOT").Value) * 1440
        Report2.LeftMargin = IIf(IsDBNull(RsCompany.Fields("REPORTMARGINLEFT").Value), 0, RsCompany.Fields("REPORTMARGINLEFT").Value) * 1440
        Report2.RightMargin = IIf(IsDBNull(RsCompany.Fields("REPORTMARGINRIGHT").Value), 0, RsCompany.Fields("REPORTMARGINRIGHT").Value) * 1440

        '    Report2.Connect = STRRptConn				
        SetCrpteMail = True
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume				
    End Function

    Public Function AssignCRpt8Formulas(ByRef Rept As CRAXDRT.Report, ByRef FormulaString As String, ByRef FormulaValue As String) As Boolean '' CrystalReport				
        On Error GoTo ERR1
        Dim i As Integer
        i = 1
        Do Until Trim(Rept.FormulaFields(i).Text) = ""
            i = i + 1
        Loop
        Rept.FormulaFields.GetItemByName("" & FormulaString & "").Text = FormulaValue
        AssignCRpt8Formulas = True
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Sub Connect_Report_To_Database(ByRef rep As CRAXDRT.Report, ByRef RS As ADODB.Recordset, ByRef pSqlStr As String, Optional ByRef pSubSqlStr As String = "")
        Dim tablecount As Object
        Dim csprop As Object
        On Error GoTo ErrPart
        Dim i As Short
        Dim j As Short
        Dim crxTables As CRAXDRT.DatabaseTables
        Dim crxTable As CRAXDRT.DatabaseTable
        Dim crxSubreportObject As CRAXDRT.SubreportObject
        Dim crxSubReport As CRAXDRT.Report = Nothing
        Dim crxSections As CRAXDRT.Sections
        Dim crxSection As CRAXDRT.Section
        Dim mChk As Integer
        Dim RsSubReport As ADODB.Recordset = Nothing
        Dim Cnt As Integer

        Dim subReport As CRAXDRT.Report
        'Dim CRXDATABASETABLE As CRAXDRT.DatabaseTable				



        If pSubSqlStr <> "" Then
            MainClass.UOpenRecordSet(pSubSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenKeyset, RsSubReport, ADODB.LockTypeEnum.adLockReadOnly)
        End If

        i = 1
        crxTables = rep.Database.Tables
        For Each crxTable In crxTables
            With crxTable
                rep.Database.Tables(i).SetLogOnInfo(DBConDSN, , DBConUID, DBConPWD)

                'rep.Database.SetDataSource(RS, , i)				
                rep.SQLQueryString = pSqlStr

                ''            .Location = .Name				

                'mChk = InStr(.Location, ".")				
                'If mChk = 0 Then				
                '    .Location = DBConUID & "." & .Location				
                'Else				
                '    .Location = DBConUID & "." & Mid(.Location, mChk + 1)				
                'End If				

                i = i + 1
            End With
        Next crxTable

        crxSections = rep.Sections

        For i = 1 To crxSections.Count
            crxSection = crxSections(i)

            For j = 1 To crxSection.ReportObjects.Count
                If crxSection.ReportObjects(j).Kind = ReportObjectKind.SubreportObject Then ''CRAXDDRT.SubreportObject Then ''CRAXDDRT  ''ReportObjectKind.crSubreportObject Then				
                    crxSubreportObject = crxSection.ReportObjects(j)

                    'Open the subreport, and treat like any other report				
                    crxSubReport = crxSubreportObject.OpenSubreport
                    '*****************************************				
                    crxTables = crxSubReport.Database.Tables

                    Cnt = 1
                    For Each crxTable In crxTables
                        With crxTable
                            .SetLogOnInfo(DBConDSN, , DBConUID, DBConPWD)
                            ''                        crxSubReport.Database.tables(j).SetLogOnInfo DBConDSN, , DBConUID, DBConPWD				
                            'crxSubReport.Database.SetDataSource(RsSubReport, , Cnt)				
                            ''                        crxSubReport.ParameterFields(1).AddCurrentValue RsCompany.fields("COMPANY_CODE").value				
                            ''                        crxSubReport.ParameterFields(2).AddCurrentValue mKey				

                            ''                        crxSubReport.SQLQueryString = pSubSqlStr				

                            'mChk = InStr(.Location, ".")				
                            'If mChk = 0 Then				
                            '    .Location = DBConUID & "." & .Location				
                            'Else				
                            '    .Location = DBConUID & "." & Mid(.Location, mChk + 1)				
                            'End If				
                        End With
                        Cnt = Cnt + 1
                    Next crxTable

                    crxSubReport.SQLQueryString = pSubSqlStr

                    '****************************************				
                End If

            Next j
        Next i
        If pSubSqlStr <> "" Then
            crxSubReport.SQLQueryString = pSubSqlStr
        End If

        Exit Sub

        '    For Each CRXDATABASETABLE In rep.Database.tables				
        '        CRXDATABASETABLE.ConnectionProperties("DSN") = DBConDSN				
        '        CRXDATABASETABLE.ConnectionProperties("USER ID") = DBConUID				
        '        CRXDATABASETABLE.ConnectionProperties("password") = DBConPWD				
        '    Next CRXDATABASETABLE				

        '    Exit Sub				
        csprop = Nothing
        tables = rep.Database.Tables
        tablecount = tables.Count
        ''Set csprop = tables.Item(tablecount).ConnectionProperties				
        '    rep.Database.tables(tablecount).SetDataSource RS				
        For i = 1 To tablecount

            '        While (I < rep.Database.tables(1).ConnectionProperties.Count)				
            '            strItem = rep.Database.tables(1).ConnectionProperties.NameIDs(I)				
            '            If strItem = "Password" Then				
            '            MsgBox "****Can't Display Password****"				
            '            Form2.Text1.Text = Form2.Text1.Text & "****Can't Display Password****" & vbCrLf				
            '            Else				
            '            Debug.Print strItem & ":  [" & rep.Database.tables(1).ConnectionProperties.Item(strItem) & "]"				
            ''            Form2.Text1.Text = Form2.Text1.Text & strItem & ":  [" & crxrpt.Database.tables(1).ConnectionProperties.Item(strItem) & "]" + vbCrLf				
            '            'MsgBox crxRpt.Database.Tables(1).ConnectionProperties.Item(strItem), vbOKOnly, strItem				
            '            End If				
            '            I = I + 1				
            '        Wend				

            rep.Database.LogOnServer(rep.Database.Tables(i).DllName, DBConDSN, , DBConUID, DBConPWD)
            '        rep.Database.tables(I).SetDataSource RS				
            '        rep.Database.SetDataSource RS, , I				
            '        Set csprop = tables.Item(I).ConnectionProperties				
            '        csprop.Item("Data Source") = DBConSERVICENAME       '' "MYERP"				
            '    '    csprop.Item("SERVICE NAME") = DBConSERVICENAME				
            ''        csprop.Item("DSN") = DBConDSN				
            '        csprop.Item("User ID") = DBConUID           ''"TAXATION"				
            '        csprop.Item("Password") = DBConPWD          ''"TAX"				
        Next




        Exit Sub
ErrPart:
        'Resume				
        MsgInformation(Err.Description)
    End Sub
    Public Function GetPictureSectionName(rep As CRAXDRT.Report, pPicName As String) As String
        On Error GoTo ErrPart
        Dim I As Integer
        Dim j As Integer

        Dim crxSections As CRAXDRT.Sections
        Dim crxSection As CRAXDRT.Section

        Dim crxOLEObject As CRAXDRT.OleObject

        Dim mQRCode As CRAXDRT.OleObject ' OLEObject				

        GetPictureSectionName = ""
        crxSections = rep.Sections

        For I = 1 To crxSections.Count
            crxSection = crxSections(I)

            '        MsgBox crxSection.Name				
            For j = 1 To crxSection.ReportObjects.Count
                'MsgBox(crxSection.ReportObjects(j).Kind)				

                If crxSection.ReportObjects(j).Kind = ReportObjectKind.PictureObject Then     ''crOLEObject  'CRObjectKind.crOleObject Then				

                    If crxSection.ReportObjects(j).Name = pPicName Then     ''"QRCode"				
                        GetPictureSectionName = crxSection.Name
                        Exit Function
                    End If
                End If
            Next j
        Next I

        Exit Function
ErrPart:
        'Resume				
        GetPictureSectionName = ""
        MsgInformation(Err.Description)
    End Function

End Module
