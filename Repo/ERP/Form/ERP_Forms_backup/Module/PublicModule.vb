Option Strict Off
Option Explicit On
Imports System.Net.Mail
Public Module PublicModule
    Public Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    ''Public Declare Function PEExportTo Lib "crpe32.dll" (ByVal printJob As Integer, ExportOptions As PEExportOptions) As Integer
    'Public Declare Function GetActiveWindow& Lib "user32" ()
    'Public Declare Function IsWindow& Lib "user32" (ByVal hwnd As Long)

    Public Const PD_ALLPAGES As Integer = &H0
    Public Const PD_SELECTION As Integer = &H1
    Public Const PD_PAGENUMS As Integer = &H2
    Public Const PD_NOSELECTION As Integer = &H4
    Public Const PD_NOPAGENUMS As Integer = &H8
    Public Const PD_COLLATE As Integer = &H10
    Public Const PD_PRINTTOFILE As Integer = &H20
    Public Const PD_PRINTSETUP As Integer = &H40
    Public Const PD_NOWARNING As Integer = &H80
    Public Const PD_RETURNDC As Integer = &H100
    Public Const PD_RETURNIC As Integer = &H200
    Public Const PD_RETURNDEFAULT As Integer = &H400
    Public Const PD_SHOWHELP As Integer = &H800
    Public Const PD_USEDEVMODECOPIES As Integer = &H40000
    Public Const PD_DISABLEPRINTTOFILE As Integer = &H80000
    Public Const PD_HIDEPRINTTOFILE As Integer = &H100000

    Public mFormBackColor As String
    Public mFormForeColor As String
    Public mFormFontName As String
    Public mFormFontSize As String
    Public mFormFontBold As String
    Public mTextBoxForeColor As String
    Public mTextBoxBackColor As String
    Public mTextBoxFontName As String
    Public mTextBoxFontSize As String
    Public mTextBoxFontBold As String
    Public mFrameForeColor As String
    Public mFrameBackColor As String
    Public mFrameFontName As String
    Public mFrameFontSize As String
    Public mFrameFontBold As String
    Public mCommandButtonBackColor As String
    Public mCommandButtonMaskColor As String
    Public mCommandButtonFontName As String
    Public mCommandButtonFontSize As String
    Public mCommandButtonFontBold As String
    Public mComboBoxForeColor As String
    Public mComboBoxBackColor As String
    Public mComboBoxFontName As String
    Public mComboBoxFontSize As String
    Public mComboBoxFontBold As String
    Public mOptionButtonForeColor As String
    Public mOptionButtonBackColor As String
    Public mOptionButtonMaskColor As String
    Public mOptionButtonFontName As String
    Public mOptionButtonFontSize As String
    Public mOptionButtonFontBold As String
    Public mCheckBoxForeColor As String
    Public mCheckBoxBackColor As String
    Public mCheckBoxMaskColor As String
    Public mCheckBoxFontName As String
    Public mCheckBoxFontSize As String
    Public mCheckBoxFontBold As String
    Public mLabelForeColor As String
    Public mLabelBackColor As String
    Public mLabelFontName As String
    Public mLabelFontSize As String
    Public mLabelFontBold As String
    Public mMSHFlexGridBackColor As String
    Public mMSHFlexGridForeColor As String
    Public mMSHFlexGridBackColorSel As String
    Public mMSHFlexGridBackColorFixed As String
    Public mMSHFlexGridForeColorFixed As String
    Public mMSHFlexGridFontName As String
    Public mMSHFlexGridFontSize As String
    Public mMSHFlexGridFontBold As String
    Public mvaSpreadShadowColor As String
    Public mvaSpreadShadowText As String
    Public mvaSpreadForeColor As String
    Public mvaSpreadGrayAreaBackColor As String
    Public mvaSpreadGridColor As String
    Public mvaSpreadLockForeColor As String
    Public mvaSpreadFontName As String
    Public mvaSpreadFontSize As String
    Public mvaSpreadFontBold As String

    Public Const SS_ACTION_ACTIVE_CELL = 0
    Public Const SS_ACTION_GOTO_CELL = 1
    Public Const SS_ACTION_SELECT_BLOCK = 2
    Public Const SS_ACTION_CLEAR = 3
    Public Const SS_ACTION_DELETE_COL = 4
    Public Const SS_ACTION_DELETE_ROW = 5
    Public Const SS_ACTION_INSERT_COL = 6
    Public Const SS_ACTION_INSERT_ROW = 7
    Public Const SS_ACTION_RECALC = 11
    Public Const SS_ACTION_CLEAR_TEXT = 12
    Public Const SS_ACTION_PRINT = 13
    Public Const SS_ACTION_DESELECT_BLOCK = 14
    Public Const SS_ACTION_DSAVE = 15
    Public Const SS_ACTION_SET_CELL_BORDER = 16
    Public Const SS_ACTION_ADD_MULTISELBLOCK = 17
    Public Const SS_ACTION_GET_MULTI_SELECTION = 18
    Public Const SS_ACTION_COPY_RANGE = 19
    Public Const SS_ACTION_MOVE_RANGE = 20
    Public Const SS_ACTION_SWAP_RANGE = 21
    Public Const SS_ACTION_CLIPBOARD_COPY = 22
    Public Const SS_ACTION_CLIPBOARD_CUT = 23
    Public Const SS_ACTION_CLIPBOARD_PASTE = 24
    Public Const SS_ACTION_SORT = 25
    Public Const SS_ACTION_COMBO_CLEAR = 26
    Public Const SS_ACTION_COMBO_REMOVE = 27
    Public Const SS_ACTION_RESET = 28
    Public Const SS_ACTION_SEL_MODE_CLEAR = 29
    Public Const SS_ACTION_VMODE_REFRESH = 30
    Public Const SS_ACTION_SMARTPRINT = 32

    ' SelectBlockOptions property settings
    Public Const SS_SELBLOCKOPT_COLS = 1
    Public Const SS_SELBLOCKOPT_ROWS = 2
    Public Const SS_SELBLOCKOPT_BLOCKS = 4
    Public Const SS_SELBLOCKOPT_ALL = 8

    ' DAutoSize property settings
    Public Const SS_AUTOSIZE_NO = 0
    Public Const SS_AUTOSIZE_MAX_COL_WIDTH = 1
    Public Const SS_AUTOSIZE_BEST_GUESS = 2

    ' BackColorStyle property settings
    Public Const SS_BACKCOLORSTYLE_OVERGRID = 0
    Public Const SS_BACKCOLORSTYLE_UNDERGRID = 1

    ' CellType property settings
    Public Const SS_CELL_TYPE_DATE = 0
    Public Const SS_CELL_TYPE_EDIT = 1
    Public Const SS_CELL_TYPE_FLOAT = 2
    Public Const SS_CELL_TYPE_INTEGER = 3
    Public Const SS_CELL_TYPE_PIC = 4
    Public Const SS_CELL_TYPE_STATIC_TEXT = 5
    Public Const SS_CELL_TYPE_TIME = 6
    Public Const SS_CELL_TYPE_BUTTON = 7
    Public Const SS_CELL_TYPE_COMBOBOX = 8
    Public Const SS_CELL_TYPE_PICTURE = 9
    Public Const SS_CELL_TYPE_CHECKBOX = 10
    Public Const SS_CELL_TYPE_OWNER_DRAWN = 11

    ' CellBorderType property settings
    Public Const SS_BORDER_TYPE_NONE = 0
    Public Const SS_BORDER_TYPE_OUTLINE = 16
    Public Const SS_BORDER_TYPE_LEFT = 1
    Public Const SS_BORDER_TYPE_RIGHT = 2
    Public Const SS_BORDER_TYPE_TOP = 4
    Public Const SS_BORDER_TYPE_BOTTOM = 8

    ' CellBorderStyle property settings
    Public Const SS_BORDER_STYLE_DEFAULT = 0
    Public Const SS_BORDER_STYLE_SOLID = 1
    Public Const SS_BORDER_STYLE_DASH = 2
    Public Const SS_BORDER_STYLE_DOT = 3
    Public Const SS_BORDER_STYLE_DASH_DOT = 4
    Public Const SS_BORDER_STYLE_DASH_DOT_DOT = 5
    Public Const SS_BORDER_STYLE_BLANK = 6
    Public Const SS_BORDER_STYLE_FINE_SOLID = 11
    Public Const SS_BORDER_STYLE_FINE_DASH = 12
    Public Const SS_BORDER_STYLE_FINE_DOT = 13
    Public Const SS_BORDER_STYLE_FINE_DASH_DOT = 14
    Public Const SS_BORDER_STYLE_FINE_DASH_DOT_DOT = 15

    ' ColHeaderDisplay and RowHeaderDisplay property settings
    Public Const SS_HEADER_BLANK = 0
    Public Const SS_HEADER_NUMBERS = 1
    Public Const SS_HEADER_LETTERS = 2

    ' TypeCheckTextAlign property settings
    Public Const SS_CHECKBOX_TEXT_LEFT = 0
    Public Const SS_CHECKBOX_TEXT_RIGHT = 1

    ' CursorStyle property settings
    Public Const SS_CURSOR_STYLE_USER_DEFINED = 0
    Public Const SS_CURSOR_STYLE_DEFAULT = 1
    Public Const SS_CURSOR_STYLE_ARROW = 2
    Public Const SS_CURSOR_STYLE_DEFCOLRESIZE = 3
    Public Const SS_CURSOR_STYLE_DEFROWRESIZE = 4

    ' CursorType property settings
    Public Const SS_CURSOR_TYPE_DEFAULT = 0
    Public Const SS_CURSOR_TYPE_COLRESIZE = 1
    Public Const SS_CURSOR_TYPE_ROWRESIZE = 2
    Public Const SS_CURSOR_TYPE_BUTTON = 3
    Public Const SS_CURSOR_TYPE_GRAYAREA = 4
    Public Const SS_CURSOR_TYPE_LOCKEDCELL = 5
    Public Const SS_CURSOR_TYPE_COLHEADER = 6
    Public Const SS_CURSOR_TYPE_ROWHEADER = 7

    ' OperationMode property settings
    Public Const SS_OP_MODE_NORMAL = 0
    Public Const SS_OP_MODE_READONLY = 1
    Public Const SS_OP_MODE_ROWMODE = 2
    Public Const SS_OP_MODE_SINGLE_SELECT = 3
    Public Const SS_OP_MODE_MULTI_SELECT = 4
    Public Const SS_OP_MODE_EXT_SELECT = 5

    ' SortKeyOrder property settings
    Public Const SS_SORT_ORDER_NONE = 0
    Public Const SS_SORT_ORDER_ASCENDING = 1
    Public Const SS_SORT_ORDER_DESCENDING = 2

    ' SortBy property settings
    Public Const SS_SORT_BY_ROW = 0
    Public Const SS_SORT_BY_COL = 1

    ' UserResize property settings
    Public Const SS_USER_RESIZE_COL = 1
    Public Const SS_USER_RESIZE_ROW = 2

    ' UserResizeCol and UserResizeRow property settings
    Public Const SS_USER_RESIZE_DEFAULT = 0
    Public Const SS_USER_RESIZE_ON = 1
    Public Const SS_USER_RESIZE_OFF = 2

    ' VScrollSpecialType property settings
    Public Const SS_VSCROLLSPECIAL_NO_HOME_END = 1
    Public Const SS_VSCROLLSPECIAL_NO_PAGE_UP_DOWN = 2
    Public Const SS_VSCROLLSPECIAL_NO_LINE_UP_DOWN = 4

    ' Position property settings
    Public Const SS_POSITION_UPPER_LEFT = 0
    Public Const SS_POSITION_UPPER_CENTER = 1
    Public Const SS_POSITION_UPPER_RIGHT = 2
    Public Const SS_POSITION_CENTER_LEFT = 3
    Public Const SS_POSITION_CENTER_CENTER = 4
    Public Const SS_POSITION_CENTER_RIGHT = 5
    Public Const SS_POSITION_BOTTOM_LEFT = 6
    Public Const SS_POSITION_BOTTOM_CENTER = 7
    Public Const SS_POSITION_BOTTOM_RIGHT = 8

    ' ScrollBars property settings
    Public Const SS_SCROLLBAR_NONE = 0
    Public Const SS_SCROLLBAR_H_ONLY = 1
    Public Const SS_SCROLLBAR_V_ONLY = 2
    Public Const SS_SCROLLBAR_BOTH = 3

    ' PrintOrientation property settings
    Public Const SS_PRINTORIENT_DEFAULT = 0
    Public Const SS_PRINTORIENT_PORTRAIT = 1
    Public Const SS_PRINTORIENT_LANDSCAPE = 2

    ' PrintType property settings
    Public Const SS_PRINT_ALL = 0
    Public Const SS_PRINT_CELL_RANGE = 1
    Public Const SS_PRINT_CURRENT_PAGE = 2
    Public Const SS_PRINT_PAGE_RANGE = 3

    ' TypeButtonType property settings
    Public Const SS_CELL_BUTTON_NORMAL = 0
    Public Const SS_CELL_BUTTON_TWO_STATE = 1

    ' TypeButtonAlign property settings
    Public Const SS_CELL_BUTTON_ALIGN_BOTTOM = 0
    Public Const SS_CELL_BUTTON_ALIGN_TOP = 1
    Public Const SS_CELL_BUTTON_ALIGN_LEFT = 2
    Public Const SS_CELL_BUTTON_ALIGN_RIGHT = 3

    ' ButtonDrawMode property settings
    Public Const SS_BDM_ALWAYS = 0
    Public Const SS_BDM_CURRENT_CELL = 1
    Public Const SS_BDM_CURRENT_COLUMN = 2
    Public Const SS_BDM_CURRENT_ROW = 4

    ' TypeDateFormat property settings
    Public Const SS_CELL_DATE_FORMAT_DDMONYY = 0
    Public Const SS_CELL_DATE_FORMAT_DDMMYY = 1
    Public Const SS_CELL_DATE_FORMAT_MMDDYY = 2
    Public Const SS_CELL_DATE_FORMAT_YYMMDD = 3

    ' TypeEditCharCase property settings
    Public Const SS_CELL_EDIT_CASE_LOWER_CASE = 0
    Public Const SS_CELL_EDIT_CASE_NO_CASE = 1
    Public Const SS_CELL_EDIT_CASE_UPPER_CASE = 2

    ' TypeEditCharSet property settings
    Public Const SS_CELL_EDIT_CHAR_SET_ASCII = 0
    Public Const SS_CELL_EDIT_CHAR_SET_ALPHA = 1
    Public Const SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC = 2
    Public Const SS_CELL_EDIT_CHAR_SET_NUMERIC = 3

    ' TypeTextAlignVert property settings
    Public Const SS_CELL_STATIC_V_ALIGN_BOTTOM = 0
    Public Const SS_CELL_STATIC_V_ALIGN_CENTER = 1
    Public Const SS_CELL_STATIC_V_ALIGN_TOP = 2

    ' TypeTime24Hour property settings
    Public Const SS_CELL_TIME_12_HOUR_CLOCK = 0
    Public Const SS_CELL_TIME_24_HOUR_CLOCK = 1

    'Unit type
    Public Const SS_CELL_UNIT_NORMAL = 0
    Public Const SS_CELL_UNIT_VGA = 1
    Public Const SS_CELL_UNIT_TWIPS = 2

    ' TypeHAlign property settings
    Public Const SS_CELL_H_ALIGN_LEFT = 0
    Public Const SS_CELL_H_ALIGN_RIGHT = 1
    Public Const SS_CELL_H_ALIGN_CENTER = 2

    ' EditEnterAction property settings
    Public Const SS_CELL_EDITMODE_EXIT_NONE = 0
    Public Const SS_CELL_EDITMODE_EXIT_UP = 1
    Public Const SS_CELL_EDITMODE_EXIT_DOWN = 2
    Public Const SS_CELL_EDITMODE_EXIT_LEFT = 3
    Public Const SS_CELL_EDITMODE_EXIT_RIGHT = 4
    Public Const SS_CELL_EDITMODE_EXIT_NEXT = 5
    Public Const SS_CELL_EDITMODE_EXIT_PREVIOUS = 6
    Public Const SS_CELL_EDITMODE_EXIT_SAME = 7
    Public Const SS_CELL_EDITMODE_EXIT_NEXTROW = 8

    ' Custom function parameter type used with CFGetParamInfo method
    Public Const SS_VALUE_TYPE_LONG = 0
    Public Const SS_VALUE_TYPE_DOUBLE = 1
    Public Const SS_VALUE_TYPE_STR = 2
    Public Const SS_VALUE_TYPE_CELL = 3
    Public Const SS_VALUE_TYPE_RANGE = 4

    ' Custom function parameter status used with CFGetParamInfo method
    Public Const SS_VALUE_STATUS_OK = 0
    Public Const SS_VALUE_STATUS_ERROR = 1
    Public Const SS_VALUE_STATUS_EMPTY = 2

    ' Reference style settings used with GetRefStyle/SetRefStyle methods
    Public Const SS_REFSTYLE_DEFAULT = 0
    Public Const SS_REFSTYLE_A1 = 1
    Public Const SS_REFSTYLE_R1C1 = 2

    ' Options used with Flags parameter of AddCustomFunctionExt method
    Public Const SS_CUSTFUNC_WANTCELLREF = 1
    Public Const SS_CUSTFUNC_WANTRANGEREF = 2
    Public bAuthLogin As Boolean
    Public bPopLogin As Boolean
    Public bHtml As Boolean
    'Global MyEncodeType    As ENCODE_METHOD
    'Global etPriority      As MAIL_PRIORITY
    Public bReceipt As Boolean
    Public strServerPop3 As String
    Public strServerSmtp As String
    Public strAccount As String
    Public strPassword As String

    Public Const PubPublishPath1 As String = "http://58.68.60.155/unit1/Manage.aspx?USER_ID="
    Public Const PubPublishPath As String = ""


    Public Sub WriteInI(ByRef Section As String, ByVal KeyName As String, ByVal DefaultValue As String, ByRef mFileName As String)
        On Error GoTo WriteErr
        Dim FileName As String
        FileName = mLocalPath & "\" & mFileName
        WritePrivateProfileString(Section, KeyName, DefaultValue, FileName)
        Exit Sub
WriteErr:
        MsgBox(Err.Description)
    End Sub

    Public Function ReadInI(ByRef Section As String, ByVal KeyName As String, ByRef mFileName As String) As String
        On Error GoTo ReadIniErr
        Dim FileName, Default_Renamed, ReturnStr As Object
        Dim ReturnString As String
        Dim Valid As Short
        FileName = App_Path() & mFileName '' mLocalPath & "\" & mFileName
        Default_Renamed = "Not Found"
        ReturnString = Space(100)
        Valid = GetPrivateProfileString(Section, KeyName, Default_Renamed, ReturnString, Len(ReturnString) + 1, FileName)
        ReturnStr = Left(ReturnString, Valid)
        ReadInI = ReturnStr
        Exit Function
ReadIniErr:
        MsgBox(Err.Description)
    End Function
    Public Function ReadInIFromServer(ByRef Section As String, ByVal KeyName As String, ByRef BarFileName As String) As String
        On Error GoTo ReadIniErr
        Dim FileName, Default_Renamed, ReturnStr As Object
        Dim ReturnString As String
        Dim Valid As Short
        FileName = My.Application.Info.DirectoryPath & "\" & BarFileName
        'FileName = mLocalPath & "\" & BarFileName
        Default_Renamed = "Not Found"
        ReturnString = Space(100)
        Valid = GetPrivateProfileString(Section, KeyName, Default_Renamed, ReturnString, Len(ReturnString) + 1, FileName)
        ReturnStr = Left(ReturnString, Valid)
        ReadInIFromServer = ReturnStr
        Exit Function
ReadIniErr:
        MsgBox(Err.Description)
    End Function
    Public Function GetComputerName() As String
        GetComputerName = ""
        'Dim sResult As New VB6.FixedLengthString(255)
        'GetComputerNameA(sResult.Value, 255)
        'GetComputerName = Left(sResult.Value, InStr(sResult.Value, Chr(0)) - 1)
    End Function
    Public Function GetIpAddrTable() As String
        GetIpAddrTable = ""
        'Dim Buf(511) As Byte
        'Dim BufSize As Integer : BufSize = UBound(Buf) + 1
        'Dim rc As Integer
        'rc = GetIpAddrTable_API(Buf(0), BufSize, 1)
        'If rc <> 0 Then err.Raise(vbObjectError, , "GetIpAddrTable failed with return value " & rc)
        'Dim NrOfEntries As Short : NrOfEntries = Buf(1) * 256 + Buf(0)
        'If NrOfEntries = 0 Then
        '    GetIpAddrTable = CStr(New Object() {}) : Exit Function
        'End If
        'Dim IpAddrs(NrOfEntries - 1) As String
        'Dim I As Short
        'Dim j As Short
        'Dim s As String
        'For I = 0 To NrOfEntries - 1 : s = ""
        '    For j = 0 To 3 : s = s & IIf(j > 0, ".", "") & Buf(4 + I * 24 + j) : Next
        '    IpAddrs(I) = s
        '    GetIpAddrTable = IIf(GetIpAddrTable = "", "", GetIpAddrTable & "|") & s
        'Next
    End Function


    Public Sub CreateLogFile(ByRef RsEmp As ADODB.Recordset, ByRef mTableName As String, ByRef pMKey As String)
        On Error GoTo ErrPart
        Dim mString As String = ""
        Dim FileName As String

        Dim I As Integer
        Dim mFieldName As String
        Dim mFieldValue As String
        Dim mSection As String

        'Dim mylog As New FileSystemObject
        'Dim createlog As TextStream
        Dim nUnit As Short

        nUnit = FreeFile


        For I = 0 To RsEmp.Fields.Count - 1
            mFieldName = RsEmp.Fields(I).Name
            mFieldValue = IIf(IsDbNull(RsEmp.Fields(mFieldName).Value), "", RsEmp.Fields(mFieldName).Value)

            mString = mString & "<" & mFieldName & "><" & mFieldValue & ">"
        Next

        mSection = "<" & PubTerminalName & "><" & PubUserID & ">" ''& "-" & VB6.Format(PubCurrDate, "DD/MM/YYYY")
        FileName = My.Application.Info.DirectoryPath & "\ERPLOGFile.txt"
        '    WritePrivateProfileString mSection, mTableName & "-" & pMkey, mString, FileName


        '    If mylog.FILEExists(FileName) = False Then
        '        Set createlog = mylog.CreateTextFile(FileName, False)
        '    Else
        '        Set createlog = mylog.OpenTextFile(FileName, ForAppending, False, TristateUseDefault)
        '    End If
        '    createlog.WriteLine ("Command Button1 click " & Date & " " & Time)

        FileOpen(nUnit, FileName, OpenMode.Append)
        PrintLine(nUnit, PubCurrDate & "<" & GetServerTime() & ">" & mSection & "><" & mTableName & ">" & mString)
        '    Print #nUnit, "  " & ErrNum & ", " & ErrorMsg
        '    Print #nUnit, "  " & vb6.Format$(Now)
        '    Print #nUnit,
        FileClose(nUnit)


        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Public Function SendMailProcess(ByRef pFrom As String, ByRef pRecipient As String, ByRef pCcRecipient As String,
                                    ByRef pBccRecipient As String, ByRef mAttachmentFile As String,
                                    ByRef mSubject As String, ByRef mBodyText As String, Optional ByRef mAttachmentFile1 As String = "") As Boolean
        If pRecipient = "" Then
            '    MsgBox("Required Gmail...")
            'ElseIf Not txtto.Text.EndsWith("@gmail.com") Then
            '    MsgBox("Invalid Format Gmail Account.", MsgBoxStyle.Exclamation)
            '    txtto.Text = ""
        Else
            Try

                Dim pEnableSSLValue As String
                Dim pPort As String


                strServerPop3 = GetEMailID("POP_ID")
                strServerSmtp = GetEMailID("SMTP_ID")
                strAccount = GetEMailID("MAIL_ACCOUNT")
                strPassword = GetEMailID("PASSWORD")
                pEnableSSLValue = GetEMailID("SSL_ENABLE")
                pPort = GetEMailID("MAIL_PORT")


                Dim smtp_server As New SmtpClient       ''(strServerSmtp)
                Dim e_mail As New MailMessage()
                Dim attachment As System.Net.Mail.Attachment

                Dim strToArray() As String
                Dim strCCArray() As String
                Dim strBCCArray() As String

                strAccount = If(Len(strAccount) < 5, "", Trim(strAccount))
                pRecipient = If(Len(pRecipient) < 5, "", Trim(pRecipient))


                If strServerSmtp = "" Or strAccount = "" Or strPassword = "" Or pRecipient = "" Then
                    MsgBox("Please Check Email Configuration", vbInformation)
                    SendMailProcess = True
                    Exit Function
                End If

                smtp_server.UseDefaultCredentials = False
                smtp_server.Credentials = New System.Net.NetworkCredential(strAccount, strPassword)
                smtp_server.Port = Val(pPort)
                smtp_server.EnableSsl = IIf(pEnableSSLValue = "0", False, True)  ''True
                smtp_server.Host = strServerSmtp        ''"smtp.gmail.com"
                e_mail = New MailMessage()
                e_mail.From = New MailAddress(pFrom)
                e_mail.IsBodyHtml = True

                strToArray = Split(pRecipient, ";")
                strCCArray = Split(pCcRecipient, ";")
                strBCCArray = Split(pBccRecipient, ";")


                'e_mail.To.Add(pRecipient)

                For y = 0 To UBound(strToArray)
                    If Trim(strToArray(y)) <> "" Then
                        e_mail.To.Add(strToArray(y))           ''SMTP.AddRecipient(strToArray(y), strToArray(y), 1)
                    End If
                Next y

                If Trim(pCcRecipient) <> "" Then
                    For y = 0 To UBound(strCCArray)
                        If Trim(strCCArray(y)) <> "" Then
                            e_mail.CC.Add(strCCArray(y))           ''SMTP.AddRecipient(strToArray(y), strToArray(y), 1)
                        End If
                    Next y
                    'e_mail.CC.Add(pCcRecipient)
                End If

                If Trim(pBccRecipient) <> "" Then
                    For y = 0 To UBound(strBCCArray)
                        If Trim(strBCCArray(y)) <> "" Then
                            e_mail.Bcc.Add(strBCCArray(y))           ''SMTP.AddRecipient(strToArray(y), strToArray(y), 1)
                        End If
                    Next y
                    'e_mail.Bcc.Add(pBccRecipient)
                End If

                e_mail.Subject = mSubject
                e_mail.Body = mBodyText
                If Trim(mAttachmentFile) <> "" Then
                    attachment = New System.Net.Mail.Attachment(mAttachmentFile)
                    e_mail.Attachments.Add(attachment)
                End If

                smtp_server.Send(e_mail)

                MsgBox("Email has been sent!", MsgBoxStyle.Information)
                SendMailProcess = True
                'Me.Dispose()
            Catch ex As Exception
                MsgBox(Err.Description, MsgBoxStyle.Information)
            End Try
        End If
    End Function
    '    Public Function SendMailProcess(ByRef pFrom As String, ByRef pRecipient As String, ByRef pCcRecipient As String,
    '                                    ByRef pBccRecipient As String, ByRef mAttachmentFile As String,
    '                                    ByRef mSubject As String, ByRef mBodyText As String, Optional ByRef mAttachmentFile1 As String = "") As Boolean
    '        On Error GoTo SendMailErr
    '        Dim x As Short
    '        Dim y As Object
    '        Dim SMTP As Object
    '        Dim Msg As String
    '        Dim strToArray() As String
    '        Dim strCCArray() As String
    '        Dim strBCCArray() As String
    '        Dim cid As String
    '        Dim strAttachArray() As String
    '        Dim I As Object
    '        Dim pSMTPAuthMode As String
    '        Dim pEnableSSLValue As String
    '        Dim pPort As String

    '        SMTP = CreateObject("EasyMail.SMTP.5")
    '        SMTP.LicenseKey = "brain/S1cI500R1AX50C0R0200"

    '        strServerPop3 = GetEMailID("POP_ID")
    '        strServerSmtp = GetEMailID("SMTP_ID")
    '        strAccount = GetEMailID("MAIL_ACCOUNT")
    '        strPassword = GetEMailID("PASSWORD")
    '        pEnableSSLValue = GetEMailID("SSL_ENABLE")
    '        pPort = GetEMailID("MAIL_PORT")

    '        SMTP.MailServer = strServerSmtp
    '        SMTP.FromAddr = pFrom

    '        strToArray = Split(pRecipient, ";")
    '        strCCArray = Split(pCcRecipient, ";")
    '        strBCCArray = Split(pBccRecipient, ";")

    '        pSMTPAuthMode = GetEMailID("SMTP_AUTHMODE")


    '        SMTP.Subject = mSubject
    '        SMTP.BodyText = mBodyText

    '        SMTP.ESMTP_AuthMode = Val(pSMTPAuthMode) '' IIf(RsCompany.fields("COMPANY_CODE").value = 16 Or RsCompany.fields("COMPANY_CODE").value = 31, 1, 0)
    '        SMTP.ESMTP_Account = strAccount
    '        SMTP.ESMTP_Password = strPassword

    '        'SMTP.ESMTP_Port = pPort     ''587

    '        '    SMTP.Html = True
    '        SMTP.BodyFormat = 1

    '        'Always set AutoWrap to zero for HTML messages
    '        SMTP.AutoWrap = 0

    '        SMTP.BodyEncoding = 1 ''2
    '        SMTP.TimeOut = 3600


    '        For y = 0 To UBound(strToArray)
    '            If Trim(pRecipient) <> "" Then
    '                SMTP.AddRecipient(strToArray(y), strToArray(y), 1)
    '            End If
    '        Next y
    '        For y = 0 To UBound(strCCArray)
    '            If Trim(pCcRecipient) <> "" Then
    '                SMTP.AddRecipient(strCCArray(y), strCCArray(y), 2)
    '            End If
    '        Next y
    '        For y = 0 To UBound(strBCCArray)
    '            If Trim(pBccRecipient) <> "" Then
    '                SMTP.AddRecipient(strBCCArray(y), strBCCArray(y), 3)
    '            End If
    '        Next y


    '        strAttachArray = Split(mAttachmentFile, ";")

    '        For I = 0 To UBound(strAttachArray)
    '            outSourec = strAttachArray(I)
    '            If Trim(outSourec) <> "" Then
    '                y = SMTP.AddAttachment(outSourec, 0)
    '            End If
    '        Next I

    '        ''    outSourec = mAttachmentFile
    '        ''    If outSourec <> "" Then
    '        ''        y = SMTP.AddAttachment(outSourec, 0)
    '        ''    End If

    '        'outSourec = mAttachmentFile1
    '        'If outSourec <> "" Then
    '        '    y = SMTP.AddAttachment(outSourec, 0)
    '        'End If


    '        x = SMTP.Send
    '        If x = 0 Then
    '            Msg = "Message sent successfully."
    '        Else
    '            MsgBox(CStr(x) & " " & "" & GetErrorMSG(Int(x)), MsgBoxStyle.Critical)
    '            Msg = "There was an error sending your message.  Error: "
    '            GoTo SendMailErr
    '        End If

    '        'If y = 0 Then
    '        'Else
    '        '    Msg = "Error with attachment. Error: "
    '        '    GoTo SendMailErr
    '        'End If

    '        SMTP = Nothing
    '        outSourec = ""
    '        SendMailProcess = True
    '        Exit Function
    'SendMailErr:
    '        'Resume
    '        MsgBox(Msg & CStr(x) & " " & "" & GetErrorMSG(Int(x)), vbCritical)
    '        ErrorMsg(Err.Description, CStr(Err.Number))
    '        SendMailProcess = False
    '    End Function
    Public Sub SendMailProcessThroughCDO(ByRef sFrom As String, ByRef sTo As String, ByRef pCcRecipient As String,
                                         ByRef pBccRecipient As String, ByRef sSmtpUser As String, ByRef sSmtpPword As String,
                                         ByRef sFilePath As String, ByRef sBody As String, ByRef sSubject As String)

        '        On Error GoTo SendMail_Error
        '        Dim lobj_cdomsg As CDO.Message

        '        Dim bSmtpSSL As Boolean
        '        Dim iSmtpPort As Short
        '        'Dim sBody As String
        '        'Global strServerSmtp As String
        '        'Global strAccount As String
        '        'Global strPassword As String
        '        '
        '        iSmtpPort = 587 '  587 ''25 ' 465  25 ''
        '        bSmtpSSL = False ''False ''

        '        lobj_cdomsg = New CDO.Message
        '        lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSMTPServer) = strServerSmtp
        '        lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSMTPServerPort).Value = iSmtpPort

        '        lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSMTPUseSSL).Value = bSmtpSSL




        '        lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSMTPAuthenticate).Value = CDO.CdoProtocolsAuthentication.cdoBasic
        '        lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSendUserName).Value = sSmtpUser
        '        lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSendPassword).Value = sSmtpPword
        '        lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSMTPConnectionTimeout).Value = 30
        '        lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSendUsingMethod).Value = CDO.CdoSendUsing.cdoSendUsingPort
        '        lobj_cdomsg.Configuration.Fields.Update()

        '        '    lobj_cdomsg.Fields("urn:schemas:mailheader:X-MC-Tags") = "CKSR001"
        '        '    lobj_cdomsg.Fields.Update

        '        lobj_cdomsg.To = sTo
        '        lobj_cdomsg.CC = pCcRecipient
        '        lobj_cdomsg.BCC = pBccRecipient
        '        lobj_cdomsg.From = sSmtpUser '' sFrom
        '        lobj_cdomsg.Subject = sSubject
        '        '    sBody = sSubject & " (" & DBConLic & "). This is Auto Generated Mail."
        '        lobj_cdomsg.HTMLBody = sBody

        '        If Trim(sFilePath) <> vbNullString Then
        '            lobj_cdomsg.AddAttachment(sFilePath)
        '        End If
        '        lobj_cdomsg.Send()
        '        lobj_cdomsg = Nothing
        '        '    MsgBox "ok"
        '        Exit Sub

        'SendMail_Error:
        '        MsgBox(Err.Description)

    End Sub
    Public Function SendMailProcessNew(ByRef sFrom As String, ByRef sTo As String, ByRef pCcRecipient As String, ByRef pBccRecipient As String, ByRef sSmtpUser As String, ByRef sSmtpPword As String, ByRef sFilePath As String, ByRef sPicPath As String, ByRef sPicName As String, ByRef sSubject As String, ByRef mBodyHeader As String, ByRef mBodyText As String, ByRef mBodyFooter As String) As Boolean
        'On Error GoTo SendMailErr
        'Dim lobj_cdomsg As CDO.Message
        'Dim sBody As String

        'lobj_cdomsg = New CDO.Message

        ''    If sPicName = "" Then
        ''        sBody = mBodyHeader _
        ' ''                & mBodyText _
        ' ''                & mBodyFooter
        ''    Else
        ''        sBody = mBodyHeader _
        ' ''                & "<IMG src=" & sPicName & "></P>" _
        ' ''                & mBodyText _
        ' ''                & mBodyFooter
        ''    End If

        'sBody = mBodyText

        'lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSMTPServer).Value = strServerSmtp
        'lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSMTPServerPort).Value = 587 '25
        'lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSMTPUseSSL).Value = False ' bSmtpSSL
        'lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSMTPAuthenticate).Value = CDO.CdoProtocolsAuthentication.cdoBasic ''cdoAnonymous ''cdoBasic
        'lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSendUserName).Value = sSmtpUser
        'lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSendPassword).Value = sSmtpPword
        'lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSMTPConnectionTimeout).Value = 30
        'lobj_cdomsg.Configuration.Fields(CDO.CdoConfiguration.cdoSendUsingMethod).Value = CDO.CdoSendUsing.cdoSendUsingPort
        'lobj_cdomsg.Configuration.Fields.Update()
        'lobj_cdomsg.To = LCase(sTo)
        'If Trim(pCcRecipient) <> "" Then
        '    lobj_cdomsg.CC = pCcRecipient
        'End If
        'If Trim(pBccRecipient) <> "" Then
        '    lobj_cdomsg.BCC = pBccRecipient
        'End If
        'lobj_cdomsg.From = sFrom
        'lobj_cdomsg.Subject = sSubject
        'lobj_cdomsg.HTMLBody = sBody

        'If sPicName <> "" Then
        '    lobj_cdomsg.AddRelatedBodyPart(sPicPath, sPicName, CDO.CdoReferenceType.cdoRefTypeId)
        'End If
        '' iMsg.AddRelatedBodyPart "D:\wwwroot\XYZ.gif", "XYZ.gif", cdoRefTypeId

        ''    lobj_cdomsg.TextBody = sBody
        'If Trim(sFilePath) <> vbNullString Then
        '    lobj_cdomsg.AddAttachment(sFilePath)
        'End If
        'lobj_cdomsg.Send()
        'lobj_cdomsg = Nothing
        SendMailProcessNew = True
        Exit Function

SendMailErr:
        ErrorMsg(CStr(Err.Number), Err.Description)
        SendMailProcessNew = False
    End Function
    Public Function GetEMailID(ByRef mFieldName As Object) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim RsTemp As ADODB.Recordset = Nothing


        SqlStr = "SELECT " & mFieldName & " AS DEPT_EMAIL FROM GEN_EMAIL_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetEMailID = IIf(IsDBNull(RsTemp.Fields("DEPT_EMAIL").Value), "", RsTemp.Fields("DEPT_EMAIL").Value)
        Else
            GetEMailID = ""
        End If

        Exit Function
ErrPart:
        GetEMailID = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Public Function GetAuthoritiesEMailID(ByRef mAthName As Object) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        SqlStr = "SELECT AUTH_MAILID FROM ATH_AUTHORITIES_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTH_NAME='" & mAthName & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetAuthoritiesEMailID = IIf(IsDbNull(RsTemp.Fields("AUTH_MAILID").Value), "", RsTemp.Fields("AUTH_MAILID").Value)
        Else
            GetAuthoritiesEMailID = ""
        End If

        Exit Function
ErrPart:
        GetAuthoritiesEMailID = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Module
