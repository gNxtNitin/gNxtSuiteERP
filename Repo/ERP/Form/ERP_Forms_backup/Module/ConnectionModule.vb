Option Strict Off
Option Explicit On
Imports System.Data.SqlClient   '' System.Data.OleDb
Imports System.Data.OleDb
Imports ADODB
Imports Microsoft.VisualBasic.Compatibility

Public Module ConnectionModule

    Public Const LOCALE_SLONGDATE As Integer = &H20S 'long date format string
    Public Const LOCALE_SSHORTDATE As Integer = &H1FS ' short system date string
    Public Const LOCALE_SLANGUAGE As Integer = &H2S
    Public Const WM_SETTINGCHANGE As Short = &H1AS
    Public Const HWND_BROADCAST As Integer = &HFFFF

    Public Declare Function GetSystemDefaultLCID Lib "kernel32" () As Integer
    Public Declare Function GetUserDefaultLCID Lib "kernel32" () As Integer

    Public Declare Function GetLocaleInfo Lib "kernel32" Alias "GetLocaleInfoA" (ByVal Locale As Integer, ByVal LCType As Integer, ByVal lpLCData As String, ByVal cchData As Integer) As Integer
    Private Declare Function GetUserName Lib "advapi32.dll" Alias "GetUserNameA" (ByVal lpBuffer As String, ByRef nSize As Integer) As Integer


    Public Declare Function SetLocaleInfo Lib "kernel32" Alias "SetLocaleInfoA" (ByVal Locale As Integer, ByVal LCType As Integer, ByVal lpLCData As String) As Boolean

    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Public Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (
                    ByVal hwnd As Long,
                    ByVal lpOperation As String,
                    ByVal lpFile As String,
                    ByVal lpParameters As String,
                    ByVal lpDirectory As String,
                    ByVal nShowCmd As Long) As Long

    Public StrConn As String
    'Public OraSession As OraSession
    Public PubDBCn As ADODB.Connection '' OraDatabase     ''ADODB.Connection
    Public LocalPubDBCn As ADODB.Connection '' OraDatabase     ''ADODB.Connection
    Public PubDBCnDataGrid As New OleDbConnection '' OraDatabase     ''ADODB.Connection
    'Public PubDBCnOLE As OleDbConnection

    Public PubDBCnBlob As ADODB.Connection
    Public PubDBCnView As ADODB.Connection

    Public STRRptConn As String
    Public mLocalPath As String

    Public mPubTDSPath As String
    Public mPubBarCodePath As String

    Public mPubDigitalSignPath As String

    Public PubTerminalName As String
    Public PubTerminalIPAddress As String
    Public PubDomainUserName As String

    Public pBARCODEFORMAT1 As String           'Hero Honda Barcode
    Public pBARCODEFORMAT2 As String           'TVS Barcode
    Public pBARCODEFORMAT3 As String           'HEMA Unit Barcode
    Public pBARCODEFORMAT4 As String            ''Munjal Showa
    Public pBARCODEFORMAT5 As String            ''Omax Group
    Public pBARCODEFORMAT6 As String            ''SunBeam
    Public pBARCODEPRINTER As String

    Public pBARCODEPort As Long
    Public pPortType As String
    Public pBARCODEDarkNess As String
    Public pERPLogo As String
    Public pLOGOName As String
    Public pLOGOPath As String
    Public pFormPic As String

    Public pLicenseTo As String
    Public pERPNAME As String
    Public pJWCompanyCode As String

    Public PubMainFormHeight As Integer
    Public PubMainFormWidth As Integer

    Public pCompanyAddressLine1 As String
    Public pCompanyAddressLine2 As String
    Public pClientCompanyAddressLine1 As String
    Public pClientCompanyAddressLine2 As String


    Public DBConUID As String
    Public DBConPWD As String
    Public DBConDSN As String
    Public DBConSERVICENAME As String

    Public DBConImageDSN As String
    Public DBConDataPath As String
    Public DBConConfigFileName As String


    Public DBConTimeDSN As String
    Public DBConTimeServer As String
    Public DBConTimeDatabase As String
    Public DBConInvPrePrint As String
    Public mEDTrfPath As String
    Public PubSourceData As String
    Public Const ConAccess As String = "ACCESS"

    Public PubUSCn As Connection
    Public StrConnGrid As String
    Public StrConnDataGrid As String
    Public StrConnBlob As String
    Public PubUniversalPrinter As String
    Public PubReportFolderName As String
    Public PubReportFolderPath As String
    Public PubEmpPhotoFolderName As String

    Public PubDomainUserDesktopPath As String
    Public PubClientLogoPath As String
    Public PubButtonPath As String


    Public Sub CheckSysDateFormat()
        Dim mDateFormat As String
        Call ConvertSysDate()
        mDateFormat = UCase(GetDateFormat)
        If Not (mDateFormat = UCase("dd/MM/yyyy") Or mDateFormat = "DD-MM-YYYY") Then
            ''and Location to English[United States]
            MsgBox("Please Change Date format to  DD/MM/YYYY in Regional Settings of Control Panel", MsgBoxStyle.Information)
            End
        End If
    End Sub
    Public Function GetDateFormat() As String

        Dim LCID As Integer
        GetDateFormat = ""
        'get the locale for the user
        '   LCID = GetSystemDefaultLCID()
        LCID = GetUserDefaultLCID()


        If LCID <> 0 Then

            'return the  date format
            GetDateFormat = GetUserLocaleInfo(LCID, LOCALE_SSHORTDATE)


        End If

    End Function
    Public Sub ConvertSysDate()
        Dim dwLCID As Integer

        dwLCID = GetSystemDefaultLCID()

        If SetLocaleInfo(dwLCID, LOCALE_SSHORTDATE, "dd/MM/yyyy") = False Then
            Exit Sub
        End If

        PostMessage(HWND_BROADCAST, WM_SETTINGCHANGE, 0, 0)

    End Sub
    Public Function GetUserLocaleInfo(ByVal dwLocaleID As Integer, ByVal dwLCType As Integer) As String

        Dim sReturn As String = ""
        Dim r As Integer

        GetUserLocaleInfo = ""
        'call the function passing the Locale type
        'variable to retrieve the required size of
        'the string buffer needed
        r = GetLocaleInfo(dwLocaleID, dwLCType, sReturn, Len(sReturn))

        'if successful..
        If r Then

            'pad the buffer with r spaces
            sReturn = Space(r)

            'and call again passing the buffer
            r = GetLocaleInfo(dwLocaleID, dwLCType, sReturn, Len(sReturn))

            'if successful (r > 0)
            If r Then

                'r holds the size of the string
                'including the terminating null
                GetUserLocaleInfo = Left(sReturn, r - 1)

            End If

        End If

    End Function
    Public Sub Main()
        On Error GoTo ErrorHandler
        Dim RS As Recordset
        Dim pUserName As String
        Dim pDatabase As String
        Dim mConnectionNo As String
        Dim sUserName As String


        sUserName = New String(Chr(0), 100)

        PubTerminalName = GetComputerName()

        PubTerminalIPAddress = GetIpAddrTable()

        GetUserName(sUserName, 255)

        sUserName = Left(sUserName, InStr(sUserName, Chr(0)) - 1)

        PubDomainUserName = sUserName

        PubDomainUserDesktopPath = "C:\Users\" & PubDomainUserName & "\Desktop"

        Call CheckSysDateFormat()

        If MakeConnectionSTR() = False Then End

        mConnectionNo = "1"
        RS = New Recordset
        PubDBCn = New ADODB.Connection
        PubDBCn.Open(StrConn)
        If PubDBCn.State = ObjectStateEnum.adStateClosed Then
            End
        End If


        mConnectionNo = "2"
        PubDBCnView = New ADODB.Connection
        PubDBCnView.Open(StrConnDataGrid)
        If PubDBCn.State = ObjectStateEnum.adStateClosed Then
            End
        End If

        mConnectionNo = "3"
        PubDBCnDataGrid = New OleDbConnection
        If PubDBCnDataGrid.State <> ConnectionState.Open Then
            PubDBCnDataGrid.ConnectionString = StrConnDataGrid
            PubDBCnDataGrid.Open()
        End If

        mConnectionNo = "4"
        PubDBCnBlob = New ADODB.Connection
        PubDBCnBlob.Open(StrConnBlob)
        If PubDBCnBlob.State = ADODB.ObjectStateEnum.adStateClosed Then
            End
        End If


        ''If PubDBCnOLE.State <> ConnectionState.Open Then
        ''Dim PubDBCnOLE As New OleDbConnection(StrConn)
        'PubDBCnOLE.ConnectionString = StrConn
        'PubDBCnOLE.Open()
        ' ''End If




        ''Oracle dataBase
        '    Set OraSession = CreateObject("OracleInProcServer.XOraSession")
        '    Set PubDBCn = OraSession.OpenDatabase(DBConSERVICENAME, DBConUID & "/" & DBConPWD, 0&)

        MainClass.UOpenRecordSet("Select * from GEN_COMPANY_MST Order By COMPANY_CODE", PubDBCn, CursorTypeEnum.adOpenKeyset, RsCompany, LockTypeEnum.adLockOptimistic)
        MainClass.ReadControlsColor()

        'frmSplash.Show()
        'FrmLogin.Show()

        Exit Sub
ErrorHandler:

        ''MsgInformation err.Description + " - " + Str(err.Number) + " (Main Module)"
        ErrorMsg(Err.Description, " (Main Module)" & " - " & mConnectionNo, MsgBoxStyle.Critical)
        End
        '    Resume
    End Sub
    Public Function App_Path() As String
        Return System.AppDomain.CurrentDomain.BaseDirectory()
    End Function
    Public Function MakeConnectionSTR() As Boolean
        On Error GoTo DSMCFGErr
        Dim mConfigFile As String
        Dim MyString As String = ""

        MakeConnectionSTR = True

        pLOGOName = "Snap Soft"
        pLOGOPath = "CLogo.jpg"
        pLicenseTo = "Company Name"
        pFormPic = ""
        pERPNAME = "SnapSoft"
        PubReportFolderName = "Reports"
        PubClientLogoPath = App_Path() & "ClientLogo\ClientLogo.bmp"
        PubButtonPath = App_Path() & "ButtonImage\"

        mConfigFile = App_Path() & "ERPConfig.CFG"

        If System.IO.File.Exists(mConfigFile) = False Then 'Config FILE DOES NOT EXIST
            MsgInformation("Configuration file not found at " & App_Path())
            GoTo ConnCondition
        End If

        FileOpen(1, mConfigFile, OpenMode.Input)
        Do While Not EOF(1) ' Loop until end of file.
            Input(1, MyString) ', MYNUMBER   ' Read data into two variables.
            If Left(MyString, 5) = "[UID]" Then
                DBConUID = Trim(Mid(MyString, 6))
            End If
            If Left(MyString, 5) = "[PWD]" Then
                DBConPWD = Trim(Mid(MyString, 6))
            End If
            If Left(MyString, 6) = "[PATH]" Then
                DBConSERVICENAME = Trim(Mid(MyString, 7))
            ElseIf Left(MyString, 13) = "[SERVICENAME]" Then
                DBConSERVICENAME = Trim(Mid(MyString, 14))
            ElseIf Left(MyString, 12) = "[SERVERNAME]" Then
                DBConSERVICENAME = Trim(Mid(MyString, 13))
            End If

            If Left(MyString, 5) = "[DSN]" Then
                DBConDSN = Trim(Mid(MyString, 6))
            End If

            If Left(MyString, 11) = "[DSN_IMAGE]" Then
                DBConImageDSN = Trim(Mid(MyString, 12))
            End If

            If Left(MyString, 13) = "[DATA_FOLDER]" Then
                DBConDataPath = Trim(Mid(MyString, 14))
            End If

            If Left(MyString, 11) = "[LOCALPATH]" Then
                mLocalPath = Trim(Mid(MyString, 13))
            End If

            If Left(MyString, 13) = "[TDSFILEPATH]" Then
                mPubTDSPath = Trim(Mid(MyString, 15))
            End If

            If Left(MyString, 13) = "[BARCODEPATH]" Then
                mPubBarCodePath = Trim(Mid(MyString, 15))
            End If

            If Left(MyString, 10) = "[BARCODE1]" Then
                pBARCODEFORMAT1 = Trim(Mid(MyString, 12))
            End If
            If Left(MyString, 10) = "[BARCODE2]" Then
                pBARCODEFORMAT2 = Trim(Mid(MyString, 12))
            End If
            If Left(MyString, 10) = "[BARCODE3]" Then
                pBARCODEFORMAT3 = Trim(Mid(MyString, 12))
            End If
            If Left(MyString, 10) = "[BARCODE4]" Then
                pBARCODEFORMAT4 = Trim(Mid(MyString, 12))
            End If
            If Left(MyString, 10) = "[BARCODE5]" Then
                pBARCODEFORMAT5 = Trim(Mid(MyString, 12))
            End If
            If Left(MyString, 10) = "[BARCODE6]" Then
                pBARCODEFORMAT6 = Trim(Mid(MyString, 12))
            End If


            If Left(MyString, 12) = "[BARCODEPRN]" Then
                pBARCODEPRINTER = Trim(Mid(MyString, 14))
            End If

            If Left(MyString, 6) = "[PORT]" Then
                pBARCODEPort = Val(Trim(Mid(MyString, 8)))
            End If

            If Left(MyString, 10) = "[PORTTYPE]" Then
                pPortType = Trim(Trim(Mid(MyString, 12)))
            End If

            If Left(MyString, 10) = "[DARKNESS]" Then
                pBARCODEDarkNess = Trim(Mid(MyString, 12))
            End If

            If Left(MyString, 9) = "[TIMEDSN]" Then
                DBConTimeDSN = Trim(Mid(MyString, 11))
            End If

            If Left(MyString, 12) = "[TIMESERVER]" Then
                DBConTimeServer = Trim(Mid(MyString, 14))
            End If

            If Left(MyString, 14) = "[TIMEDATABASE]" Then
                DBConTimeDatabase = Trim(Mid(MyString, 16))
            End If

            If Left(MyString, 17) = "[INVOICEPREPRINT]" Then
                DBConInvPrePrint = Trim(Mid(MyString, 19))
            End If

            If Left(MyString, 10) = "[LOGONAME]" Then
                pLOGOName = Trim(Mid(MyString, 12))
            End If

            If Left(MyString, 10) = "[ERPLOGO]" Then
                pERPLogo = Trim(Mid(MyString, 11))
            End If
            If Left(MyString, 9) = "[LOGOPIC]" Then
                pLOGOPath = Trim(Mid(MyString, 11))
            End If

            If Left(MyString, 9) = "[LICENSE]" Then
                pLicenseTo = Trim(Mid(MyString, 11))
            End If

            If Left(MyString, 10) = "[FORM_PIC]" Then
                pFormPic = Trim(Mid(MyString, 12))
            End If

            If Left(MyString, 10) = "[ERP_NAME]" Then
                pERPNAME = Trim(Mid(MyString, 12))
            End If


            If Left(MyString, 17) = "[JW_COMPANY_CODE]" Then
                pJWCompanyCode = Trim(Mid(MyString, 19))
            End If

            '    PubReportFolderName As String

            If Left(MyString, 18) = "[REPORTFOLDERNAME]" Then
                PubReportFolderName = Trim(Mid(MyString, 20))
            End If

            If Left(MyString, 20) = "[EMPPHOTOFOLDERNAME]" Then
                PubEmpPhotoFolderName = Trim(Mid(MyString, 22))
            End If

            If Left(MyString, 18) = "[UNIVERSALPRINTER]" Then
                PubUniversalPrinter = Trim(Mid(MyString, 20))
            End If

        Loop
        FileClose(1)

        Dim mLocalTempPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)

        mLocalPath = mLocalTempPath & "\ERP"

        If Not System.IO.Directory.Exists(mLocalPath) Then
            System.IO.Directory.CreateDirectory(mLocalPath)
        End If

        mPubTDSPath = mLocalTempPath & "\TDS"

        If Not System.IO.Directory.Exists(mPubTDSPath) Then
            System.IO.Directory.CreateDirectory(mPubTDSPath)
        End If

        mPubBarCodePath = mLocalTempPath & "\BARCODE"

        If Not System.IO.Directory.Exists(mPubBarCodePath) Then
            System.IO.Directory.CreateDirectory(mPubBarCodePath)
        End If


        mPubDigitalSignPath = mLocalTempPath & "\BARCODE\DSC"
        If Not System.IO.Directory.Exists(mPubDigitalSignPath) Then
            System.IO.Directory.CreateDirectory(mPubDigitalSignPath)
        End If

        PubReportFolderPath = App_Path() & PubReportFolderName & "\"        ''PubReportFolderPath  ''"G:\VBDotNetERP_Working\Form\bin\Debug\"

        If DBConDSN = "" Then
            MsgInformation("Database DSN not defined in Configuration file ")
            GoTo ConnCondition
        ElseIf DBConUID = "" Then
            MsgInformation("Database UID not defined in Local Configuration file ")
            GoTo ConnCondition
        ElseIf DBConPWD = "" Then
            MsgInformation("Database PWD not defined in Local Configuration file ")
            GoTo ConnCondition
        ElseIf DBConSERVICENAME = "" Then
            MsgInformation("Database Path/Service Name not defined in Local Configuration file ")
            GoTo ConnCondition
        End If

        If DBConSERVICENAME = "" Then
            StrConn = "DRIVER={Microsoft ODBC for ORACLE};" & "UID=" & DBConUID & ";PWD=" & DBConPWD

            StrConnGrid = "Provider=OraOLEDB.Oracle.1;" & "Persist Security Info=False;" & "User ID=" & DBConUID & ";Password=" & DBConPWD

        Else

            ''StrConn = " Provider=MSDAORA.1; " & " Password=" & DBConPWD & "; " & " User ID=" & DBConUID & "; " & " Data Source=" & DBConSERVICENAME & "; " & " Persist Security Info=FALSE"

            StrConn = "Provider=OraOLEDB.Oracle;" & "Persist Security Info=False;" & "Data Source=" & DBConSERVICENAME & ";" & "User ID=" & DBConUID & ";Password=" & DBConPWD & ";" & "OLEDB.NET=False;"
            StrConnGrid = "DRIVER={Microsoft ODBC for ORACLE};" & "UID=" & DBConUID & ";PWD=" & DBConPWD & "@" & DBConSERVICENAME
            StrConnDataGrid = "Provider=OraOLEDB.Oracle;" & "Persist Security Info=False;" & "Data Source=" & DBConSERVICENAME & ";" & "User ID=" & DBConUID & ";Password=" & DBConPWD & ";" & "OLEDB.NET=True;"
            ''Provider=OraOLEDB.Oracle;Data Source=MyOracleDB;User Id=myUsername;Password=myPassword;OLEDB.NET=True;

            ''OK ''05-09-2005
            ''StrConnGrid = "DRIVER={Microsoft ODBC for ORACLE};" & "UID=" & DBConUID & ";PWD=" & DBConPWD & "@" & DBConSERVICENAME
            'StrConnGrid = "Provider=Microsoft.Ace.Oledb.12.0; Data Source=" & My.Application.Info.DirectoryPath.ToString() & "\Students\Students.Accdb;"

            'Provider=msdaora;Data Source=MyOracleDB;User Id=myUsername;Password=myPassword;   (Microsoft OLE DB Provider for Oracle must be installed)

            'Provider=OraOLEDB.Oracle;Data Source=MyOracleDB;User Id=myUsername;Password=myPassword;   (Oracle Provider for OLE DB must be installed)



            'StrConn = " Provider=MSDAORA.1; " & _
            '        " Password=" & DBConPWD & "; " & _
            '        " User ID=" & DBConUID & "; " & _
            '        " Data Source=" & DBConSERVICENAME & "; " & _
            '        " Persist Security Info=FALSE"


            ''        StrConn = "DRIVER={ORACLE ODBC DRIVER};SERVER=" & DBConSERVICENAME & "; " & _
            ''                    "UID=" & DBConUID & ";PWD=" & DBConPWD & ";" & _
            ''                    "DBQ=" & DBConSERVICENAME & ";" & _
            ''                    "DBA=W;APA=T;FEN=T;QTO=T;FRC=10;FDL=10;LOB=T;RST=T;FRL=F;MTS=F;CSR=F;PFC=10;TLO=O;"



        End If

        STRRptConn = "DSN=" & DBConDSN & ";UID=" & DBConUID & ";PWD=" & DBConPWD & ";DSQ=;"
        StrConnBlob = "Provider=OraOLEDB.Oracle;Data Source=" & DBConSERVICENAME & ";User ID=" & DBConUID & ";Password=" & DBConPWD & ";"

        '''testing
        'StrConn = " Provider=MSDAORA.1; " & " Password=" & DBConPWD & "; " & " User ID=" & DBConUID & "; " & " Data Source=" & DBConSERVICENAME & "; " & " Persist Security Info=FALSE"
        'StrConnGrid = "DRIVER={Microsoft ODBC for ORACLE};" & "UID=" & DBConUID & ";PWD=" & DBConPWD & "@" & DBConSERVICENAME
        'StrConnDataGrid = StrConn
        'StrConnBlob = StrConn



        '    RC = PEOpenEngine()
        '    printWindowOpts.StructSize = PE_SIZEOF_WINDOW_OPTIONS

        Exit Function
ConnCondition:
        MakeConnectionSTR = False
        Exit Function
DSMCFGErr:
        FileClose(1)
        MakeConnectionSTR = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Sub OpenLocalConnection()
        On Error GoTo ErrorHandler


        LocalPubDBCn = New ADODB.Connection
        LocalPubDBCn.Open(StrConn)
        'If LocalPubDBCn.State = ObjectStateEnum.adStateClosed Then
        '    End
        'End If


        Exit Sub
ErrorHandler:

        ''MsgInformation err.Description + " - " + Str(err.Number) + " (Main Module)"
        ErrorMsg(Err.Description, " (Main Module)" & " - ", MsgBoxStyle.Critical)
        End
        '    Resume
    End Sub
    Public Sub CloseLocalConnection()
        On Error GoTo ErrorHandler

        LocalPubDBCn.Close()
        'LocalPubDBCn.Dispose()

        Exit Sub
ErrorHandler:

        ''MsgInformation err.Description + " - " + Str(err.Number) + " (Main Module)"
        ErrorMsg(Err.Description, " (Main Module)" & " - ", MsgBoxStyle.Critical)
        End
        '    Resume
    End Sub
End Module