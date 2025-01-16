Attribute VB_Name = "ConnectionModule"
Option Explicit

Public Const LOCALE_SLONGDATE As Long = &H20  'long date format string
Public Const LOCALE_SSHORTDATE As Long = &H1F    ' short system date string
Public Const LOCALE_SLANGUAGE As Long = &H2
Public Const WM_SETTINGCHANGE = &H1A
Public Const HWND_BROADCAST = &HFFFF&

Public Declare Function GetSystemDefaultLCID Lib "kernel32" () As Long
Public Declare Function GetUserDefaultLCID Lib "kernel32" () As Long

Public Declare Function GetLocaleInfo Lib "kernel32" _
   Alias "GetLocaleInfoA" _
  (ByVal Locale As Long, _
   ByVal LCType As Long, _
   ByVal lpLCData As String, _
   ByVal cchData As Long) As Long


Public Declare Function SetLocaleInfo Lib "kernel32" _
    Alias "SetLocaleInfoA" _
    (ByVal Locale As Long, _
    ByVal LCType As Long, _
    ByVal lpLCData As String) As Boolean

Public Declare Function PostMessage Lib "user32" _
    Alias "PostMessageA" (ByVal hwnd As Long, _
    ByVal wMsg As Long, _
    ByVal wParam As Long, _
    ByVal lParam As Long) As Long


Public StrConn As String
'Public OraSession As OraSession
Public PubDBCn As ADODB.Connection      '' OraDatabase     ''ADODB.Connection
Public STRRptConn As String
Public mLocalPath  As String

Public pBARCODEFORMAT1  As String           '''Hero Honda Barcode
Public pBARCODEFORMAT2  As String           '''TVS Barcode
Public pBARCODEFORMAT3  As String           '''HEMA Unit Barcode
Public pBARCODEFORMAT4 As String            ''Munjal Showa
Public pBARCODEFORMAT5 As String            ''Omax Group
Public pBARCODEPRINTER  As String

Public pBARCODEPort  As Long
Public pBARCODEDarkNess  As String
    
Public DBConUID As String
Public DBConPWD As String
Public DBConDSN As String
Public DBConSERVICENAME As String

Public DBConTimeDSN As String
Public DBConTimeServer As String
Public DBConTimeDatabase As String

Public mEDTrfPath  As String
Public PubSourceData  As String
Public Const ConAccess = "ACCESS"

Public PubUSCn As ADODB.Connection
Public StrConnGrid As String

Public Const ConWH = "WH"
Public Const ConPH = "PH"
Public Const ConJW = "JW"
Public Const ConSH = "SH"

'''STOCK REF TYPE .......
Public Const ConStockRefType_QC = "QC"
Public Const ConStockRefType_MRR = "MRR"
Public Const ConStockRefType_ISS = "ISS"
Public Const ConStockRefType_DSP = "DSP"
Public Const ConStockRefType_NRG = "NRG"
Public Const ConStockRefType_RGP = "RGP"
Public Const ConStockRefType_SRN = "SRN"
Public Const ConStockRefType_ADJ = "ADJ"
Public Const ConStockRefType_OPN = "OPN"
Public Const ConStockRefType_MSL = "MSL"
Public Const ConStockRefType_PSL = "PSL"
Public Const ConStockRefType_CON = "CON"       ''For CO2
Public Const ConStockRefType_BDM = "BDM"
Public Const ConStockRefType_REOFFER = "REO"
Public Const ConStockRefType_PMEMO = "PMO"
Public Const ConStockRefType_PMEMODEPT = "PMD"
Public Const ConStockRefType_PISS = "PIS"
Public Const ConStockRefType_SUBISS = "SIS"
Public Const ConStockRefType_SCP = "SCP"
Public Const ConStockRefType_RWK = "PRW"
Public Const ConStockRefType_MOV = "MOV"
Public Const ConStockRefType_MANU = "SMI"
Public Const ConStockRefType_REWORK = "RWP"
Public Const ConStockRefType_PBREAKUP = "PBU"
Public Const ConStockRefType_PMS = "PMS"
Public Const ConStockRefType_BDT = "BDT"

Sub CheckSysDateFormat()
  Dim mDateFormat As String
  Call ConvertSysDate
  mDateFormat = UCase(GetDateFormat)
  If Not (mDateFormat = "DD/MM/YYYY" Or mDateFormat = "DD-MM-YYYY") Then
        ''and Location to English[United States]
      MsgInformation "Please Change Date format to  DD/MM/YYYY in Regional Settings of Control Panel"
      End
  End If
End Sub
Public Function GetDateFormat() As String

   Dim LCID As Long
   Dim I As Long
  'get the locale for the user
'   LCID = GetSystemDefaultLCID()
   LCID = GetUserDefaultLCID()


   If LCID <> 0 Then
      
     'return the  date format
    GetDateFormat = GetUserLocaleInfo(LCID, LOCALE_SSHORTDATE)
    
    
   End If
   
End Function
Public Sub ConvertSysDate()
  Dim dwLCID As Long
  
  dwLCID = GetSystemDefaultLCID()
  
  If SetLocaleInfo(dwLCID, LOCALE_SSHORTDATE, "dd/MM/yyyy") = False Then
        Exit Sub
  End If
  
  PostMessage HWND_BROADCAST, WM_SETTINGCHANGE, 0, 0

End Sub
Public Function GetUserLocaleInfo(ByVal dwLocaleID As Long, _
                                   ByVal dwLCType As Long) As String

   Dim sReturn As String
   Dim r As Long

  'call the function passing the Locale type
  'variable to retrieve the required size of
  'the string buffer needed
   r = GetLocaleInfo(dwLocaleID, dwLCType, sReturn, Len(sReturn))

  'if successful..
   If r Then

     'pad the buffer with r spaces
      sReturn = Space$(r)

     'and call again passing the buffer
      r = GetLocaleInfo(dwLocaleID, dwLCType, sReturn, Len(sReturn))

     'if successful (r > 0)
      If r Then

        'r holds the size of the string
        'including the terminating null
         GetUserLocaleInfo = Left$(sReturn, r - 1)

      End If

   End If

End Function
Public Sub Main()
On Error GoTo ErrorHandler
Dim RS As ADODB.Recordset
Dim pUserName As String
Dim pDatabase As String
    
    
    Call CheckSysDateFormat
    
    If MakeConnectionSTR = False Then End

    Set RS = New ADODB.Recordset
    Set PubDBCn = New ADODB.Connection
    PubDBCn.Open StrConn
    If PubDBCn.State = adStateClosed Then
        End
    End If
 
    UOpenRecordSet "Select * from GEN_COMPANY_MST WHERE COMPANY_CODE=" & PubCompanyCode & " Order By COMPANY_NAME", PubDBCn, adOpenKeyset, RsCompany, adLockOptimistic
 
    PubCurrDate = GetServerDate(PubDBCn)
    Call mAutoEmail(PubDBCn)
    
Exit Sub
ErrorHandler:
'    Resume
    ''MsgInformation err.Description + " - " + Str(err.Number) + " (Main Module)"
    ErrorMsg Err.Description, Err.Number & " (Main Module)", vbCritical
'    Resume
End Sub

Public Function MakeConnectionSTR() As Boolean
On Error GoTo DSMCFGErr
Dim MyString As String
    
    MakeConnectionSTR = True
    PubHO = "N"
    pBARCODEPRINTER = "N"
    pBARCODEPort = 1
    pBARCODEDarkNess = "D7"
    
    If Dir(App.Path + "\REVIVE.CFG") = "" Then 'REVIVE FILE DOES NOT EXIST
        MsgInformation "REVIVE Configuration file not found at " + App.Path
        GoTo ConnCondition:
    End If
    
    Open App.Path + "\REVIVE.CFG" For Input As #1
    Do While Not EOF(1)   ' Loop until end of file.
        Input #1, MyString ', MYNUMBER   ' Read data into two variables.
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
        
        If Left(MyString, 12) = "[HEADOFFICE]" Then
           PubHO = Trim(Mid(MyString, 13))
        End If
        
        If Left(MyString, 13) = "[COMPANYCODE]" Then
           PubCompanyCode = Trim(Mid(MyString, 14))
        End If
        
        If Left(MyString, 11) = "[LOCALPATH]" Then
           mLocalPath = Trim(Mid(MyString, 13))
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
        If Left(MyString, 12) = "[BARCODEPRN]" Then
           pBARCODEPRINTER = Trim(Mid(MyString, 14))
        End If
        
        If Left(MyString, 6) = "[PORT]" Then
           pBARCODEPort = Val(Trim(Mid(MyString, 8)))
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
    Loop
    Close #1
    
    PubRun_IN = IIf(PubHO = "Y", "H", "U")
    If DBConDSN = "" Then
        MsgInformation "Database DSN not defined in Configuration file "
        GoTo ConnCondition:
    ElseIf mLocalPath = "" Then
        MsgInformation "LOCAL PATH not defined in Configuration file "
        GoTo ConnCondition:
    ElseIf DBConUID = "" Then
        MsgInformation "Database UID not defined in Local Configuration file "
        GoTo ConnCondition:
    ElseIf DBConPWD = "" Then
        MsgInformation "Database PWD not defined in Local Configuration file "
        GoTo ConnCondition:
    ElseIf DBConSERVICENAME = "" Then
        MsgInformation "Database Path/Service Name not defined in Local Configuration file "
        GoTo ConnCondition:
    End If
    
    If DBConSERVICENAME = "" Then
        StrConn = "DRIVER={Microsoft ODBC for ORACLE};" & _
                     "UID=" & DBConUID & ";PWD=" & DBConPWD
                     
        StrConnGrid = "Provider=OraOLEDB.Oracle.1;" & _
                   "Persist Security Info=False;" & _
                   "User ID=" & DBConUID & ";Password=" & DBConPWD
                   
    Else
        
        StrConn = " Provider=MSDAORA.1; " & _
                " Password=" & DBConPWD & "; " & _
                " User ID=" & DBConUID & "; " & _
                " Data Source=" & DBConSERVICENAME & "; " & _
                " Persist Security Info=FALSE"
        
  
  
  
  ''OK ''05-09-2005
        StrConnGrid = "DRIVER={Microsoft ODBC for ORACLE};" & _
                     "UID=" & DBConUID & ";PWD=" & DBConPWD & "@" & DBConSERVICENAME
        

        
''        StrConn = "DRIVER={ORACLE ODBC DRIVER};SERVER=" & DBConSERVICENAME & "; " & _
''                    "UID=" & DBConUID & ";PWD=" & DBConPWD & ";" & _
''                    "DBQ=" & DBConSERVICENAME & ";" & _
''                    "DBA=W;APA=T;FEN=T;QTO=T;FRC=10;FDL=10;LOB=T;RST=T;FRL=F;MTS=F;CSR=F;PFC=10;TLO=O;"
       
       
       
    End If
    
    STRRptConn = "DSN=" & DBConDSN & ";UID=" & DBConUID & ";PWD=" & DBConPWD & ";DSQ=;"
    
'    RC = PEOpenEngine()
'    printWindowOpts.StructSize = PE_SIZEOF_WINDOW_OPTIONS
    
    Exit Function
ConnCondition:
    MakeConnectionSTR = False
    Exit Function
DSMCFGErr:
    Close #1
    MakeConnectionSTR = False
    ErrorMsg Err.Description, Err.Number, vbCritical
End Function




