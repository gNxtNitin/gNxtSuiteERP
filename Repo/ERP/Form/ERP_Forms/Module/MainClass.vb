Option Strict Off
Option Explicit On
Imports System
Imports System.Windows.Forms
'Imports VB = Microsoft.VisualBasic

Imports System.Data.SqlClient   '' System.Data.OleDb
Imports System.Data.OleDb
Imports ADODB
Imports Microsoft.VisualBasic.Compatibility.VB6
Imports Microsoft.VisualBasic.Compatibility
Imports ADODC = Microsoft.VisualBasic.Compatibility.VB6.ADODC
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Public Class MainClass
    'Dim MainClass As New MainClass
    Public Shared Function ValidateWithMasterTable(ByVal mFieldData As Object, ByVal mFieldName As String, ByVal mGetFieldDataName As String, ByVal mTableName As String, ByVal mDBCn As Connection, ByRef StoreRetval As Object, Optional ByRef pErrMsg As String = "", Optional ByVal mSqlCond As String = "") As Boolean
        On Error GoTo CheckTheAccountNameErr
        Dim CheckTheAccountName As Object
        Dim MasterDate As Object


        Dim mSql As String = ""
        Dim RsValidate As Recordset
        Dim xStr As String
        Dim MasterNo As Object
        Dim mDate As Date
        RsValidate = New Recordset
        MasterNo = ""

        If CStr(mFieldData) <> "" And mTableName <> "" Then
            Select Case VarType(mFieldData)
                Case 2, 3, 4, 5, 14, 20
                    mSql = "Select " & mGetFieldDataName & " From " & mTableName & " Where " & mFieldName & " =" & RTrim(LTrim(mFieldData)) & ""
                Case 7
                    mDate = MainClass.ToMMDD(CObj(mFieldData))
                    mSql = "Select " & mGetFieldDataName & " From " & mTableName & " Where " & mFieldName & " ='" & RTrim(LTrim(CStr(mDate))) & "'"
                Case 8
                    xStr = mFieldData
                    xStr = MainClass.AllowSingleQuote(xStr)
                    mSql = "Select " & mGetFieldDataName & " From " & mTableName & " Where " & mFieldName & " ='" & RTrim(LTrim(xStr)) & "'"
            End Select

            If mSqlCond <> "" Then
                mSql = mSql & vbCrLf & " AND " & mSqlCond
            End If
            RsValidate = Nothing
            If MainClass.UOpenRecordSet(mSql, mDBCn, CursorTypeEnum.adOpenStatic, RsValidate, LockTypeEnum.adLockReadOnly) = False Then GoTo CheckTheAccountNameErr

            If RsValidate.EOF = False Then
                ValidateWithMasterTable = True
                MasterNo = IIf(IsDBNull(RsValidate.Fields(0).Value), "", RsValidate.Fields(0).Value)
                Select Case VarType(MasterNo)
                    Case 0 'vbNull
                        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
                    Case 2 'vbInteger
                        MasterNo = CShort(MasterNo)
                    Case 3, 14 'vbLong
                        MasterNo = CDbl(MasterNo)
                        'MasterNo = CLng(MasterNo)
                    Case 4 'vbSingle
                        MasterNo = CSng(MasterNo)
                    Case 5 'vbDouble
                        MasterNo = CDbl(MasterNo)
                    Case 7 'vbDate
                        MasterDate = MainClass.ToDDMM(CDate(CStr(MasterNo)))
                    Case 8 'vbString
                        MasterNo = CStr(MasterNo)
                    Case 10 'vbError
                        MsgBox(MasterNo)
                End Select
            ElseIf RsValidate.EOF = True Then
                ValidateWithMasterTable = False
                If pErrMsg <> "" Then
                    MsgInformation(pErrMsg)
                End If
                StoreRetval = MasterNo
                RsValidate.Close()
                RsValidate = Nothing

                Exit Function
            End If
        Else
            CheckTheAccountName = False
            Exit Function
        End If
        StoreRetval = IIf(IsDBNull(MasterNo), " ", MasterNo)

        RsValidate.Close()
        RsValidate = Nothing

        Exit Function
CheckTheAccountNameErr:
        ''Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ValidateWithMasterTable = False
        '    If RsValidate.State = adStateOpen Then
        '        RsValidate.Close
        '        Set RsValidate = Nothing
        '    End If
    End Function
    Public Shared Function GetMaxRecord(ByRef mTableName As String, ByRef mDBCn As Connection, Optional ByRef mSqlCond As String = "") As Double
        On Error GoTo ErrPart

        Dim mSql As String
        Dim RsRecordCount As Recordset ''ADODB.Recordset


        GetMaxRecord = 0
        mSql = " SELECT COUNT(1) AS MAXRECD FROM " & mTableName & "" ''& vbCrLf |            & " Where " & mSqlCond

        If mSqlCond <> "" Then
            mSql = mSql & vbCrLf & " Where " & mSqlCond
        End If

        RsRecordCount = Nothing
        MainClass.UOpenRecordSet(mSql, mDBCn, CursorTypeEnum.adOpenStatic, RsRecordCount, LockTypeEnum.adLockReadOnly)

        If RsRecordCount.EOF = False Then
            GetMaxRecord = RsRecordCount.Fields(0).Value
        ElseIf RsRecordCount.EOF = True Then
            GetMaxRecord = 0
        End If

        RsRecordCount.Close()
        RsRecordCount = Nothing

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Shared Function GetMonthInString(ByRef TextMonth As String) As String
        GetMonthInString = ""
        TextMonth = VB6.Format(TextMonth, "00")
        If TextMonth = "" Then
            GetMonthInString = ConBlankDate
        ElseIf TextMonth = "01" Then
            GetMonthInString = "January"
        ElseIf TextMonth = "02" Then
            GetMonthInString = "February"
        ElseIf TextMonth = "03" Then
            GetMonthInString = "March"
        ElseIf TextMonth = "04" Then
            GetMonthInString = "April"
        ElseIf TextMonth = "05" Then
            GetMonthInString = "May"
        ElseIf TextMonth = "06" Then
            GetMonthInString = "June"
        ElseIf TextMonth = "07" Then
            GetMonthInString = "July"
        ElseIf TextMonth = "08" Then
            GetMonthInString = "August"
        ElseIf TextMonth = "09" Then
            GetMonthInString = "September"
        ElseIf TextMonth = "10" Then
            GetMonthInString = "October"
        ElseIf TextMonth = "11" Then
            GetMonthInString = "November"
        ElseIf TextMonth = "12" Then
            GetMonthInString = "December"
        End If
    End Function
    Public Shared Function ToDDMM(ByRef FldDate As Date) As Object
        If Not IsDate(FldDate) Then
            ToDDMM = ""
        Else
            ToDDMM = VB6.Format(FldDate, "dd/MM/yyyy")
        End If
    End Function

    Public Shared Function ToMMDD(ByRef TextDate As String) As Object
        If TextDate = "" Then
            ToMMDD = ConBlankDate
        Else
            ToMMDD = VB6.Format(TextDate, "MM-DD-YYYY")
        End If
    End Function

    Public Shared Function ChkIsdateF(ByRef txtOBJ As Object) As Boolean
        ChkIsdateF = True
        If Not TypeOf txtOBJ Is System.Windows.Forms.TextBox Or txtOBJ.Text = "" Then
            If Not TypeOf txtOBJ Is System.Windows.Forms.MaskedTextBox Then
                Exit Function
            End If
        End If
        If Not IsDate(txtOBJ.Text) Then
            MsgInformation("Please Enter Valid Date")
            ChkIsdateF = False
            Exit Function
        Else
            txtOBJ.Text = VB6.Format(txtOBJ.Text, "dd/mm/yyyy")
        End If
    End Function
    Public Shared Sub ProtectCell(ByRef sprd As Object, ByRef Row As Integer, ByRef Row2 As Integer, ByRef Col As Integer, ByRef col2 As Integer)
        sprd.Row = Row
        sprd.Row2 = Row2
        sprd.Col = Col
        sprd.col2 = col2
        sprd.BlockMode = True
        sprd.Lock = True
        sprd.Protect = True
        sprd.BlockMode = False

    End Sub
    Public Shared Sub UnLockCell(ByRef sprd As Object, ByRef Row As Integer, ByRef Row2 As Integer, ByRef Col As Integer, ByRef col2 As Integer)
        sprd.Row = Row
        sprd.Row2 = Row2
        sprd.Col = Col
        sprd.col2 = col2
        sprd.BlockMode = True
        sprd.Lock = False
        sprd.BlockMode = False

    End Sub
    Public Shared Sub LockCell(ByRef sprd As Object, ByRef Row As Integer, ByRef Row2 As Integer, ByRef Col As Integer, ByRef col2 As Integer)
        sprd.Row = Row
        sprd.Row2 = Row2
        sprd.Col = Col
        sprd.col2 = col2
        sprd.BlockMode = True
        sprd.Lock = True
        sprd.BlockMode = False

    End Sub
    Public Shared Sub CellColor(ByRef sprd As Object, ByRef Row As Integer, ByRef Row2 As Integer, ByRef Col As Integer, ByRef col2 As Integer)
        sprd.Row = Row
        sprd.Row2 = Row2
        sprd.Col = Col
        sprd.col2 = col2
        sprd.BlockMode = True

        'MySpread.AppearanceStyle = FPSpreadADO.AppearanceStyleConstants.AppearanceStyleClassic     ''FPSpreadADO.AppearanceStyleConstants.AppearanceStyleEnhanced
        'MySpread.GrayAreaBackColor = Color.AliceBlue
        'MySpread.ShadowColor = Color.SkyBlue
        'MySpread.ShadowText = Color.Black  ''&HFF
        'MySpread.ScrollBarHColor = Color.AliceBlue
        'MySpread.ScrollBarVColor = Color.AliceBlue
        'MySpread.SelBackColor = Color.White
        'MySpread.SelBackColor = Color.Black

        'sprd.AppearanceStyle = FPSpreadADO.AppearanceStyleConstants.AppearanceStyleClassic  ''AppearanceStyleEnhanced   ''AppearanceStyleEnhanced MySpread.AppearanceStyle = FPSpreadADO.AppearanceStyleConstants.AppearanceStyleEnhanced
        sprd.BackColor = Color.Aqua   ''Control.DefaultBackColor ''  &HFFFF00
        sprd.GridColor = Color.Blue  '' &HC00000
        sprd.ShadowText = Color.Black  ''&HFF
        sprd.ShadowColor = Color.LightYellow ' PubSpdShodowColor ' System.Drawing.ColorTranslator.FromOle(CInt(PubSpdShodowColor)) 'Color.LightYellow ''&H80FFFF
        sprd.SelBackColor = Color.Gold  ''   LightGoldenrodYellow  ''&HC0FFFF
        sprd.SelForeColor = Color.Black  ''&H800000
        sprd.LockForeColor = Color.Maroon
        sprd.BackColorStyle = 1
        sprd.GridSolid = False
        sprd.GrayAreaBackColor = PubFormBackColor 'System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor))          ''Color.AliceBlue

        sprd.VScrollSpecial = True
        sprd.VScrollSpecialType = 0 '' FPSpreadADO.VScrollSpecialTypeConstants.VScrollSpecialNoPageUpDown

        'sprd.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        sprd.SetOddEvenRowColor(System.Drawing.ColorTranslator.ToOle(PubSpdMainColor), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), System.Drawing.ColorTranslator.ToOle(PubSpdAlterColor), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))

        sprd.BlockMode = False

    End Sub
    Public Shared Sub SearchCellColor(ByRef sprd As Object, ByRef Row As Integer, ByRef Row2 As Integer, ByRef Col As Integer, ByRef col2 As Integer)
        sprd.Row = Row
        sprd.Row2 = Row2
        sprd.Col = Col
        sprd.col2 = col2
        sprd.BlockMode = True

        'MySpread.AppearanceStyle = FPSpreadADO.AppearanceStyleConstants.AppearanceStyleClassic     ''FPSpreadADO.AppearanceStyleConstants.AppearanceStyleEnhanced
        'MySpread.GrayAreaBackColor = Color.AliceBlue
        'MySpread.ShadowColor = Color.SkyBlue
        'MySpread.ShadowText = Color.Black  ''&HFF
        'MySpread.ScrollBarHColor = Color.AliceBlue
        'MySpread.ScrollBarVColor = Color.AliceBlue
        'MySpread.SelBackColor = Color.White
        'MySpread.SelBackColor = Color.Black

        'sprd.AppearanceStyle = FPSpreadADO.AppearanceStyleConstants.AppearanceStyleClassic  ''AppearanceStyleEnhanced   ''AppearanceStyleEnhanced MySpread.AppearanceStyle = FPSpreadADO.AppearanceStyleConstants.AppearanceStyleEnhanced
        sprd.BackColor = Color.White   ''Control.DefaultBackColor ''  &HFFFF00
        sprd.GridColor = Color.Black  '' &HC00000
        sprd.ShadowText = Color.Black  ''&HFF
        sprd.ShadowColor = Color.Orange
        'sprd.ShadowDark = Color.OrangeRed
        sprd.SelBackColor = Color.DeepSkyBlue  ''   LightGoldenrodYellow  ''&HC0FFFF
        sprd.SelForeColor = Color.Black  ''&H800000
        sprd.LockForeColor = Color.Black
        sprd.BackColorStyle = 1
        sprd.GridSolid = False
        sprd.GrayAreaBackColor = Color.AliceBlue
        sprd.BorderStyle = 1
        sprd.BlockMode = False

    End Sub
    Public Shared Sub BlockCellColor(ByRef sprd As Object, ByRef Row As Integer, ByRef Row2 As Integer, ByRef Col As Integer, ByRef col2 As Integer)
        sprd.Row = Row
        sprd.Row2 = Row2
        sprd.Col = Col
        sprd.col2 = col2
        sprd.BlockMode = True
        sprd.BackColor = &HC0C0C0 ''&HFFFF00
        sprd.GridColor = &HC00000
        sprd.Lock = True
        sprd.Protect = True
        '    sprd.ShadowText = &HFF&
        '    sprd.ShadowColor = &H80FFFF
        '    sprd.SelBackColor = &HC0FFFF
        '    sprd.SelForeColor = &H800000
        sprd.BlockMode = False
    End Sub
    Public Shared Sub UnProtectCell(ByRef sprd As Object, ByRef Row As Integer, ByRef Row2 As Integer, ByRef Col As Integer, ByRef col2 As Integer)
        sprd.Row = Row
        sprd.Row2 = Row2
        sprd.Col = Col
        sprd.col2 = col2
        sprd.BlockMode = True
        sprd.Lock = False
        sprd.Protect = False
        sprd.BlockMode = False
    End Sub

    Public Shared Sub SprdAction(ByRef sprd As Object, ByRef mAction As Object)
        sprd.Col = -1
        sprd.Row = -1
        sprd.BlockMode = True
        sprd.Action = mAction
        sprd.BlockMode = False
    End Sub

    Public Shared Sub SaveStatus(ByRef MyForm As System.Windows.Forms.Button, ByRef ADDMode As Boolean, ByRef MODIFYMode As Boolean, Optional ByRef ActivateSavebutton As Boolean = False)
        Dim button1 = New System.Windows.Forms.Button

        If ADDMode = True Or MODIFYMode = True Or ActivateSavebutton = True Then
            'MyForm.CmdSave.Enabled = True
            MyForm.Enabled = True


            '        MyForm.cmdSavePrint.Enabled = True
        End If
    End Sub


    Public Shared Function UOpenRecordSet(ByRef SqlStr As String, ByRef DbCN As Connection, ByRef mOpenType As CursorTypeEnum, ByRef mRs As Recordset, Optional ByRef mLockType As LockTypeEnum = 0) As Boolean
        ''Public Shared Function UOpenRecordSet(SqlStr As String, DbCN As Connection, mOpenType As CursorTypeEnum, ByRef mRs As Recordset, Optional mLockType As LockTypeEnum) As Boolean
        On Error GoTo ERR1
        UOpenRecordSet = False
        mRs = New Recordset
        mRs.CursorLocation = CursorLocationEnum.adUseServer ''adUseClient          '

        If mLockType = 0 Then
            mRs.Open(SqlStr, DbCN, mOpenType)
        Else
            mRs.Open(SqlStr, DbCN, mOpenType, mLockType)
        End If

        ''Set mRs = DbCN.CreateDynaset(SqlStr, 0&)
        UOpenRecordSet = True
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UOpenRecordSet = False
        'Resume
    End Function
    Public Shared Function AdjNum(ByVal N As Double) As String
        AdjNum = Space(9 - Len(Trim(VB6.Format(N, "0.00")))) & VB6.Format(Trim(CStr(N)), "0.00")
    End Function
    Public Shared Function AllowSingleQuote(ByRef txt As String) As String
        txt = Trim(txt)
        AllowSingleQuote = Trim(Replace(txt, "'", "''"))
    End Function
    Public Shared Function AllowDoubleQuote(ByRef txt As String) As String
        txt = Trim(txt)
        AllowDoubleQuote = Trim(Replace(txt, Chr(34), ""))
    End Function
    Public Shared Function AllowVBNewLine(ByRef txt As String) As String
        AllowVBNewLine = Trim(Replace(txt, vbNewLine, " "))
    End Function
    '    Public Shared Function CheckDataCboCode(ByRef mAData As ADODC, ByRef mDataCBO As AxMSDataListLib.AxDataCombo, ByRef ReturnField As String, ByRef retval As Object, Optional ByRef ErrMsg As String = "") As Boolean
    '        On Error GoTo ERR1
    '        CheckDataCboCode = False
    '        mAData.Recordset.AbsolutePosition = mDataCBO.SelectedItem
    '        retval = mAData.Recordset.Fields(ReturnField).Value
    '        CheckDataCboCode = True
    '        Exit Function
    'ERR1:
    '        'ErrorMsg err.Description, err.Number, vbCritical
    '        'Assumed the string does not exist in the list
    '    End Function
    Public Shared Function AutoGenRowNo(ByRef mTable As String, ByRef mMaxField As String, ByRef DbCN As Connection, Optional ByRef mCondition As String = "") As Integer
        Dim SeqName As Object
        On Error GoTo ERR1
        Dim RS As Recordset
        Dim SqlStr As String = ""

        RS = Nothing
        SeqName = "Seq_" & mTable & "_" & mMaxField

        SqlStr = "Select " & SeqName & ".NextVal from Dual"
        ''Set Rs = DbCN.Execute(SqlStr)
        MainClass.UOpenRecordSet(SqlStr, DbCN, CursorTypeEnum.adOpenStatic, RS, LockTypeEnum.adLockReadOnly)


        If Not IsDBNull(RS.Fields(0).Value) Then
            AutoGenRowNo = RS.Fields(0).Value
        Else
            AutoGenRowNo = 1
        End If

        If UCase(mMaxField) = "CODE" Then '' FOR MASTER ADD COMPANY & BRANCH
            AutoGenRowNo = CInt(RsCompany.Fields("Company_Code").Value & VB6.Format(AutoGenRowNo, "000000"))
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Public Shared Sub SearchName(ByRef Control As System.Windows.Forms.ListBox, ByRef txt As System.Windows.Forms.TextBox)
        'Dim I, j As Short
        'Dim llct, b, prevb As Short
        'Dim idarri(50) As Short
        'b = Len(Trim(txt.Text))
        'llct = Control.Items.Count
        'If b < 1 Then
        '    Control.Text = VB6.GetItemString(Control, 0)
        '    I = 0
        '    prevb = 0
        '    For j = 0 To 50
        '        idarri(j) = 0
        '    Next
        '    j = 0
        'Else
        '    If b > prevb Then
        '        If j >= 0 Then
        '            idarri(j) = I
        '        End If
        '        j = j + 1
        '        '        i = idarri(j)-1
        '    Else
        '        j = j - 1
        '        If j >= 0 Then
        '            I = idarri(j)
        '        End If
        '    End If
        '    Do While I < llct
        '        If UCase(Left(VB6.GetItemString(Control, I), b)) = UCase(Trim(txt.Text)) Then
        '            Control.Text = VB6.GetItemString(Control, I)
        '            prevb = b
        '            Exit Sub
        '        End If
        '        I = I + 1
        '    Loop
        'End If
    End Sub

    'Public Shared Sub ReportWindow(Rept1 As CrystalReport)
    '    Rept1.WindowMaxButton = True
    '    Rept1.WindowMinButton = True
    '    Rept1.WindowShowGroupTree = True
    '    Rept1.WindowShowNavigationCtls = True
    '    Rept1.WindowAllowDrillDown = True
    '    Rept1.WindowShowPrintSetupBtn = True
    '    Rept1.WindowShowProgressCtls = True
    '    Rept1.WindowShowSearchBtn = True
    '    Rept1.WindowShowZoomCtl = True
    '    Rept1.WindowState = crptMaximized
    '    Rept1.WindowBorderStyle = crptSizable
    'End Sub

    Public Shared Function STRMenuRight(ByRef mUser As String, ByRef mModuleID As Short, ByRef mMenu As String, ByRef DbCN As Connection, Optional ByRef pCompanyCode As Long = 0) As String
        On Error GoTo ErrSTRMenuRight
        Dim RS As Recordset = Nothing 'ADODB.Recordset
        Dim SqlStr As String = ""
        Dim xCompanyCode As Long = 0

        STRMenuRight = ""

        If pCompanyCode = 0 Then
            xCompanyCode = RsCompany.Fields("COMPANY_CODE").Value
        Else
            xCompanyCode = pCompanyCode
        End If

        If mUser <> "" And mMenu <> "" Then
            If PubSuperUser = "A" Then
                STRMenuRight = "AMDVPS"
                Exit Function
            Else
                'SqlStr = " Select RIGTHS " & vbCrLf _
                '    & " From FIN_RIGHTS_MST A,  GEN_MENU_MST B" & vbCrLf _
                '    & " WHERE A.USER_ID='" & UCase(mUser) & "'" & vbCrLf _
                '    & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                '    & " --AND A.BRANCH_CODE=" & RsCompany.Fields("BRANCH_CODE").Value & "" & vbCrLf _
                '    & " AND A.MENU_CODE=B.MENU_CODE" & vbCrLf _
                '    & " AND UPPER(B.MENU_NAME)='" & UCase(mMenu) & "'"

                SqlStr = " Select Rights " & vbCrLf _
                        & " From FIN_Rights_MST " & vbCrLf _
                        & " Where UserID='" & UCase(mUser) & "'" & vbCrLf _
                        & " And COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
                        & " And MenuHead='" & UCase(mMenu) & "'"        ''AND MODULEID=" & mModuleID & " 

                MainClass.UOpenRecordSet(SqlStr, DbCN, CursorTypeEnum.adOpenStatic, RS)

                If RS.EOF = False Then
                    STRMenuRight = IIf(IsDBNull(RS.Fields("RIGHTS").Value), "", RS.Fields("RIGHTS").Value)

                    'STRMenuRight = IIf(RS.Fields("IS_ADD").Value = "Y", "A", "")
                    'STRMenuRight = STRMenuRight & IIf(RS.Fields("IS_MOD").Value = "Y", "M", "")
                    'STRMenuRight = STRMenuRight & IIf(RS.Fields("IS_DEL").Value = "Y", "D", "")
                    'STRMenuRight = STRMenuRight & IIf(RS.Fields("IS_VIEW").Value = "Y", "V", "")
                    'STRMenuRight = STRMenuRight & IIf(RS.Fields("IS_PRINT").Value = "Y", "P", "")
                    'STRMenuRight = STRMenuRight & IIf(RS.Fields("IS_ATH").Value = "Y", "S", "")
                Else
                    STRMenuRight = ""
                End If
            End If
        End If
        'If RS.State = adStateOpen Then
        '    RS.Close()
        '    RS = Nothing
        'End If

        Exit Function
ErrSTRMenuRight:
        'Resume
        MsgBox(Err.Description)
        '    If Rs.State = adStateOpen Then
        '        Rs.Close
        '        Set Rs = Nothing
        '    End If
    End Function
    Public Shared Sub RightsToButton(ByRef MyForm As System.Windows.Forms.Form, ByRef RightsSTR As String)
        On Error GoTo ERR1
        Dim mControl As System.Windows.Forms.Control
        mControl = Nothing

        Call SetStatusBar()

        '    Call FormOpened


        For Each mControl In MyForm.Controls
            'Making All Std. Buttons(Add, Save, Modify, Delete) Enabled=False
            If TypeOf mControl Is System.Windows.Forms.Button Then
                If mControl.Text = ConCmdAddCaption Then
                    mControl.Enabled = False
                End If
                If mControl.Text = ConCmdSaveCaption Then
                    mControl.Enabled = False
                End If
                If mControl.Text = ConcmdmodifyCaption Then
                    mControl.Enabled = False
                End If
                If mControl.Text = ConCmdDeleteCaption Then
                    mControl.Enabled = False
                End If
                MiscButtonRights(mControl, False)
            End If
            '
            '        'Making Std. Buttons(Add, Save, Modify, Delete) Enabled=True, Based on the RightsSTR
            If TypeOf mControl Is System.Windows.Forms.Button Then
                If InStr(1, RightsSTR, "A", CompareMethod.Text) <> 0 Then
                    If mControl.Text = ConCmdAddCaption Then
                        mControl.Enabled = True
                    End If
                    If mControl.Text = ConCmdSaveCaption Then
                        mControl.Enabled = True
                    End If
                    MiscButtonRights(mControl, False)
                End If

                If InStr(1, RightsSTR, "M", CompareMethod.Text) <> 0 Then
                    If mControl.Text = ConCmdSaveCaption Then
                        mControl.Enabled = True
                    End If
                    If mControl.Text = ConcmdmodifyCaption Then
                        mControl.Enabled = True
                    End If
                    MiscButtonRights(mControl, False)
                End If

                If InStr(1, RightsSTR, "D", CompareMethod.Text) <> 0 Then
                    If mControl.Text = ConCmdDeleteCaption Then
                        mControl.Enabled = True
                    End If
                    MiscButtonRights(mControl, False)
                End If
                If InStr(1, RightsSTR, "V", CompareMethod.Text) <> 0 Then
                    MiscButtonRights(mControl, True)
                End If
            End If
        Next mControl
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Public Shared Sub MiscButtonRights(ByRef mControl As System.Windows.Forms.Control, ByRef RightFlag As Boolean)
        On Error GoTo ERR1
        If mControl.Text = "&Begin" Then
            mControl.Enabled = RightFlag
        End If
        If mControl.Text = "&End" Then
            mControl.Enabled = RightFlag
        End If
        If mControl.Text = "&Open" Then
            mControl.Enabled = RightFlag
        End If
        If mControl.Text = "&Show" Then
            mControl.Enabled = RightFlag
        End If
        If mControl.Text = "Show" Then
            mControl.Enabled = RightFlag
        End If
        If UCase(mControl.Text) = "OK" Or UCase(mControl.Text) = "&OK" Then
            mControl.Enabled = RightFlag
        End If
        If mControl.Text = "&Print" Then
            mControl.Enabled = RightFlag
        End If
        If mControl.Text = "Pre&view" Then
            mControl.Enabled = RightFlag
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Public Shared Sub SetFocusToCell(ByRef sprd As Object, ByVal Row As Integer, ByVal Col As Integer, Optional ByRef pErrMsg As String = "")
        On Error GoTo SETERR

        If Trim(pErrMsg) <> "" Then
            MsgInformation(pErrMsg)
        End If
        sprd.Col = Col
        sprd.col2 = Col
        sprd.Row = Row
        sprd.Row2 = Row
        sprd.BlockMode = True
        sprd.Action = SS_ACTION_ACTIVE_CELL
        sprd.BlockMode = False

        If sprd.Enabled = True Then
            sprd.Focus()
        End If
        Exit Sub
SETERR:
        If Err.Number = 5 Then Resume Next
    End Sub

    Public Shared Sub AddBlankSprdRow(ByRef sprd As Object, ByRef CheckCol As Integer, Optional ByRef mRowHeight As Integer = 0)
        With sprd
            .Row = .MaxRows
            .Col = CheckCol
            If .Text <> "" Then
                .MaxRows = .MaxRows + 1
                .Row = .MaxRows
                .Action = SS_ACTION_INSERT_ROW
                If mRowHeight > 0 Then
                    '.RowHeight(.MaxRows) = mRowHeight
                    '.RowHeight(-1) = mRowHeight
                    .set_RowHeight(-1, mRowHeight)
                End If
            End If
        End With
    End Sub
    Public Shared Sub AddBlankfpSprdRow(ByRef sprd As Object, ByRef CheckCol As Integer, Optional ByRef mRowHeight As Integer = 0)
        With sprd
            .Row = .MaxRows
            .Col = CheckCol
            If .Text <> "" Then
                .MaxRows = .MaxRows + 1
                .Row = .MaxRows
                .Action = SS_ACTION_INSERT_ROW
                If mRowHeight > 0 Then
                    '.RowHeight(.MaxRows) = mRowHeight
                    .set_RowHeight(-1, mRowHeight)
                End If
            End If
        End With
    End Sub

    Public Shared Sub DeleteSprdRow(ByRef sprd As Object, ByRef DelRow As Integer, ByRef CheckCol As Integer, Optional ByRef DelStatus As Boolean = False)
        Dim Response As Object

        With sprd
            .Row = DelRow
            .Col = CheckCol
            'If .Text = "" Or DelRow = 0 Then Exit Sub
            If DelRow = .MaxRows Or DelRow = 0 Then Exit Sub

            Response = MsgQuestion("Click 'Yes' for Insert  And 'No' for Delete. ")
            If Response = MsgBoxResult.Yes Then
                .Row = DelRow
                .Action = SS_ACTION_INSERT_ROW
                If .MaxRows >= 1 Then .MaxRows = .MaxRows + 1
                DelStatus = False
            Else
                Response = MsgQuestion("Are you sure to Delete this Row ? ")
                If Response = MsgBoxResult.Yes Then
                    .Row = DelRow
                    .Action = SS_ACTION_DELETE_ROW
                    If .MaxRows > 1 Then .MaxRows = .MaxRows - 1
                    DelStatus = True
                Else
                    DelStatus = False
                End If
            End If
        End With
    End Sub


    Public Shared Function LastDay(ByRef GiveMonth As Short, ByRef GiveYear As Short) As Short
        On Error GoTo LastDayErr
        Dim mmm As Short
        mmm = GiveMonth
        Select Case mmm
            Case 1, 3, 5, 7, 8, 10, 12
                LastDay = 31
            Case 2
                If GiveYear Mod 4 = 0 Then
                    LastDay = 29
                Else
                    LastDay = 28
                End If
            Case 4, 6, 9, 11
                LastDay = 30
        End Select
        Exit Function
LastDayErr:
        MsgBox(Err.Description)
        Exit Function
    End Function

    Public Shared Sub ClearGrid(ByRef sprd As AxFPSpreadADO.AxfpSpread, Optional ByRef mRowHeight As Integer = 0)
        On Error GoTo ErrPart
        With sprd
            .MaxRows = 1
            .Col = -1
            .Row = 1
            .Row2 = .MaxRows
            .BlockMode = True
            .Action = SS_ACTION_CLEAR
            .Protect = False
            .Lock = False
            'If mRowHeight > 0 Then
            '    .Height = mRowHeight
            'Else
            '    .Height = 1
            'End If
            .BlockMode = False
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Public Shared Sub ClearFields(ByRef MyForm As System.Windows.Forms.Form)
        On Error GoTo ErrPart
        Dim mControl As System.Windows.Forms.Control
        mControl = Nothing
        For Each mControl In MyForm.Controls
            If TypeOf mControl Is System.Windows.Forms.TextBox Then
                mControl.Text = ""
                'ElseIf TypeOf mControl Is System.Windows.Forms.ComboBox Then
                '    If mControl.Style = 0 Then mControl.Text = ""
                '    If mControl.Style = 1 Then mControl.Text = ""
                '    If mControl.Style = 2 Then mControl.ListIndex = -1
                'ElseIf TypeOf mControl Is System.Windows.Forms.CheckBox Then
                '    mControl.Value = 0
            End If
        Next mControl
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Public Shared Function ValidDataInGrid(ByRef sprd As Object, ByRef CheckCol As Integer, ByRef SingleCharValueType As String, Optional ByRef InvalidMsg As String = "") As Boolean
        On Error GoTo ERR1
        Static I As Object
        Static j As Integer
        With sprd
            j = .MaxRows - 1
            If j = 0 Then MsgBox(InvalidMsg) : ValidDataInGrid = False : Exit Function
            For I = 1 To j
                .Row = I
                .Col = 0
                If Mid(.Text, 1, 1) <> "D" Then
                    .Col = CheckCol
                    If SingleCharValueType = "N" Then
                        If Val(.Text) <= 0 Then
                            ValidDataInGrid = False
                            GoTo DspMsg
                        Else
                            ValidDataInGrid = True
                        End If
                    ElseIf SingleCharValueType = "S" Then
                        If .Text <> "" Then
                            ValidDataInGrid = True
                        Else
                            ValidDataInGrid = False
                            GoTo DspMsg
                        End If
                    End If
                End If
            Next I
        End With
        ValidDataInGrid = True
        Exit Function
DspMsg:
        'Resume
        If InvalidMsg = "" Then
            MsgInformation("Not a valid Voucher")
            MainClass.SetFocusToCell(sprd, I, CheckCol)
        Else
            '    Resume
            MsgInformation(InvalidMsg)
            MainClass.SetFocusToCell(sprd, I, CheckCol)
        End If
        'Resume
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Public Shared Function ValidDataInFpGrid(ByRef sprd As Object, ByRef CheckCol As Integer, ByRef SingleCharValueType As String, Optional ByRef InvalidMsg As String = "") As Boolean
        '        On Error GoTo ERR1
        '        Static I As Object
        '        Static j As Integer
        '        With sprd
        '            j = .MaxRows - 1
        '            If j = 0 Then ValidDataInFpGrid = False : Exit Function
        '            For I = 1 To j
        '                .Row = I
        '                .Col = 0
        '                If Left(.Text, 1) <> "D" Then
        '                    .Col = CheckCol
        '                    If SingleCharValueType = "N" Then
        '                        If Val(.Text) <= 0 Then
        '                            ValidDataInFpGrid = False
        '                            GoTo DspMsg
        '                        Else
        '                            ValidDataInFpGrid = True
        '                        End If
        '                    ElseIf SingleCharValueType = "S" Then
        '                        If .Text <> "" Then
        '                            ValidDataInFpGrid = True
        '                        Else
        '                            ValidDataInFpGrid = False
        '                            GoTo DspMsg
        '                        End If
        '                    End If
        '                End If
        '            Next I
        '        End With
        '        ValidDataInFpGrid = True
        '        Exit Function
        'DspMsg:
        '        If InvalidMsg = "" Then
        '            MsgInformation("Not a valid Voucher")
        '            MainClass.SetFocusToCell(sprd, I, CheckCol)
        '        Else
        '            MsgInformation(InvalidMsg)
        '            MainClass.SetFocusToCell(sprd, I, CheckCol)
        '        End If
        'Resume
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Shared Function PadC(ByRef mText As String, ByRef mLength As Short, Optional ByRef FillChar As String = "") As String
        On Error GoTo ERR1
        Static I As Short
        If FillChar = "" Then
            FillChar = " "
        End If
        I = (mLength - Len(mText)) / 2
        PadC = New String(FillChar, I) & mText & New String(FillChar, I)
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Public Shared Function PadL(ByRef mText As String, ByRef mLength As Short, Optional ByRef FillChar As String = "") As String
        On Error GoTo ERR1
        Static I As Short
        If FillChar = "" Then
            FillChar = " "
        End If
        I = (mLength - Len(mText))
        PadL = New String(FillChar, I) & mText
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Public Shared Function MLCount(ByRef txtString As String, ByRef LineWidth As Short) As Short
        If Int(Len(txtString) / LineWidth) = Len(txtString) / LineWidth Then
            MLCount = Int(Len(txtString) / LineWidth)
        Else
            MLCount = Int(Len(txtString) / LineWidth) + 1
        End If
    End Function
    Public Shared Function MemoLine(ByRef txtString As String, ByRef LineNumber As Short, ByRef LineWidth As Short) As String
        MemoLine = Mid(txtString, ((LineNumber - 1) * LineWidth) + 1, LineWidth)
    End Function

    Public Shared Function SetCrpt(ByVal Report2 As CRAXDRT.Report, ByVal oPrinterValue As Boolean, ByVal mNoOfCopies As Integer, ByVal mTitle As String, ByVal mSubTitle As String) As Boolean
        On Error GoTo err1
        Dim ICodeWidth As String
        Dim CompanyName As String = ""
        Dim BranchName As String = ""
        Dim CompanyAdd As String = ""
        Dim CompanyAddress As String = ""
        Dim CompanyPhone As String = ""
        Dim UserID As String = ""
        Dim RunDate As String = ""
        Dim PageNo As String = ""
        Dim xDocNo As String = ""
        Dim xOrigDate As String = ""
        Dim xRevNo As String = ""
        Dim xRevDate As String = ""

        If oPrinterValue = True Then
            If mNoOfCopies = 0 Then
                MsgInformation("No of Copies should be more than 0")
                Exit Function
            End If
        End If

        If RsCompany.Fields("PrintTopCompanyName").Value = "Y" Then
            CompanyName = RsCompany.Fields("BRANCH_NAME").Value
        Else
            CompanyName = ""
        End If

        If RsCompany.Fields("PrintTopCompanyAddress").Value = "Y" Then
            CompanyAdd = "" & RsCompany.Fields("BRN_ADDRESS").Value & ",  " & RsCompany.Fields("BRN_CITY").Value & " , " & RsCompany.Fields("BRN_STATE").Value & " - " & RsCompany.Fields("BRN_ZIP").Value & ""
        Else
            CompanyAdd = ""
        End If

        If RsCompany.Fields("PRintTopCompanyPhone").Value = "Y" Then
            CompanyPhone = "Phone : " & RsCompany.Fields("BRN_TEL_NO").Value & " Fax : " & RsCompany.Fields("BRN_FAX_NO").Value & " e-Mail : " & RsCompany.Fields("BRN_MAILID").Value
        Else
            CompanyPhone = ""
        End If

        Report2.DiscardSavedData()
        '    MainClass.ReportWindow Report2, mTitle
        AssignCRptFormulas(Report2, "CompanyName", "'" & CompanyName & "'")
        AssignCRptFormulas(Report2, "CompanyAddress", "'" & CompanyAdd & "'")
        AssignCRptFormulas(Report2, "Title", "'" & UCase(mTitle) & "'")
        AssignCRptFormulas(Report2, "SubTitle", "'" & mSubTitle & "'")



        If RsCompany.Fields("PrintBotCompanyName").Value = "Y" Then
            CompanyName = RsCompany.Fields("Company_Name").Value
        Else
            CompanyName = ""
        End If

        CompanyAdd = IIf(RsCompany.Fields("PrintBotCompanyAddress").Value = "Y", "" & RsCompany.Fields("BRN_ADDRESS").Value & " ,    " & RsCompany.Fields("BRN_CITY").Value & ",    " & RsCompany.Fields("BRN_STATE").Value & " -   " & RsCompany.Fields("BRN_ZIP").Value & "", "")
        CompanyPhone = IIf(RsCompany.Fields("PrintBotCompanyPhone").Value = "Y", "Phone : " & RsCompany.Fields("BRN_TEL_NO").Value & " Fax : " & RsCompany.Fields("BRN_FAX_NO").Value & " e-mail : " & RsCompany.Fields("BRN_MAILID").Value, "")

        AssignCRptFormulas(Report2, "CompanyBotLine1", "'" & CompanyAdd & "'")
        AssignCRptFormulas(Report2, "CompanyBotLine2", "'" & IIf(IsDBNull(CompanyPhone), "", CompanyPhone) & "'")

        If RsCompany.Fields("Printuser").Value = "Y" Then
            UserID = PubUserID
        Else
            UserID = ""
        End If
        If RsCompany.Fields("PrintrunDate").Value = "Y" Then
            RunDate = VB6.Format(PubCurrDate, "dd/MM/yyyy") & " " & GetServerTime()
        Else
            RunDate = " "
        End If
        If RsCompany.Fields("PrintPageNo").Value = "Y" Then
            PageNo = "Y"
        Else
            PageNo = "N"
        End If

        AssignCRptFormulas(Report2, "UserID", "'" & UserID & "'")
        AssignCRptFormulas(Report2, "PrintDate", "'" & RunDate & "'")
        AssignCRptFormulas(Report2, "PrintPageNo", "'" & PageNo & "'")

        'If mDocTitle = True Then
        '    If Trim(xMenuID) <> "" Then
        '        If MainClass.SetReportDocDetail(xMenuID, PubDBCn, xDocNo, xOrigDate, xRevNo, xRevDate) = True Then
        '            AssignCRptFormulas(Report2, "DocNo", "'" & xDocNo & "'")
        '            AssignCRptFormulas(Report2, "OrigDate", "'" & xOrigDate & "'")
        '            AssignCRptFormulas(Report2, "RevNo", "'" & xRevNo & "'")
        '            AssignCRptFormulas(Report2, "RevDate", "'" & xRevDate & "'")
        '        End If
        '    End If
        'End If



        Report2.TopMargin = IIf(IsDBNull(RsCompany.Fields("REPORTMARGINTOP").Value), 0, RsCompany.Fields("REPORTMARGINTOP").Value) * 1440
        Report2.BottomMargin = IIf(IsDBNull(RsCompany.Fields("REPORTMARGINBOT").Value), 0, RsCompany.Fields("REPORTMARGINBOT").Value) * 1440
        Report2.LeftMargin = IIf(IsDBNull(RsCompany.Fields("REPORTMARGINLEFT").Value), 0, RsCompany.Fields("REPORTMARGINLEFT").Value) * 1440
        Report2.RightMargin = IIf(IsDBNull(RsCompany.Fields("REPORTMARGINRIGHT").Value), 0, RsCompany.Fields("REPORTMARGINRIGHT").Value) * 1440

        'If xCINNo = "Y" Then
        '    MainClass.AssignCRptFormulas(Report2, "COMPANYCINNo=""" & IIf(IsdbNull(RsCompany!CIN_NO), "", RsCompany!CIN_NO) & """")

        '    mRegdAddress = "Regd Office : " & IIf(IsdbNull(RsCompany!REGD_ADDR1), "", RsCompany!REGD_ADDR1)
        '    mRegdAddress = mRegdAddress & IIf(IsdbNull(RsCompany!REGD_ADDR2) Or RsCompany!REGD_ADDR2 = "", "", RsCompany!REGD_ADDR2)
        '    mRegdAddress = mRegdAddress & IIf(IsdbNull(RsCompany!REGD_CITY) Or RsCompany!REGD_CITY = "", "", RsCompany!REGD_CITY)
        '    mRegdAddress = mRegdAddress & IIf(IsdbNull(RsCompany!REGD_STATE) Or RsCompany!REGD_STATE = "", "", " - " & RsCompany!REGD_STATE)
        '    mRegdAddress = mRegdAddress & IIf(IsdbNull(RsCompany!REGD_PHONE) Or RsCompany!REGD_PHONE = "", "", " Phone : " & RsCompany!REGD_PHONE)
        '    MainClass.AssignCRptFormulas(Report2, "CompanyRegdAdd=""" & mRegdAddress & """")
        'End If


        SetCrpt = True
        Exit Function
err1:
        MsgInformation(Err.Description)
    End Function

    Public Shared Function AssignCRptFormulas(ByVal Rept As CRAXDRT.Report, ByVal FormulaString As String, ByVal FormulaValue As String) As Boolean

        On Error GoTo err1
        Static I As Long
        I = 1
        Do Until Trim(Rept.FormulaFields(I).Text) = ""  ''Do Until Trim(Rept.Formulas(I)) = ""
            I = I + 1
        Loop
        Rept.FormulaFields.GetItemByName("" & FormulaString & "").Text = FormulaValue  ''Rept.Formulas(I) = FormulaString
        AssignCRptFormulas = True

        Exit Function
err1:
    End Function

    Public Shared Function ValidNameKey(ByRef KeyCode As Short) As Boolean
        ValidNameKey = False
        If KeyCode <> System.Windows.Forms.Keys.Tab And KeyCode <> System.Windows.Forms.Keys.Left And KeyCode <> System.Windows.Forms.Keys.Right And KeyCode <> System.Windows.Forms.Keys.End And KeyCode <> System.Windows.Forms.Keys.Home And KeyCode <> System.Windows.Forms.Keys.Return Then
            ValidNameKey = True
        End If
    End Function

    Public Shared Function RemoveReturnKey(ByRef tStr As String) As String
        On Error GoTo ERR1
        Static I As Integer
        Static j As Integer
        Static XX As String

        j = Len(tStr)
        RemoveReturnKey = ""
        For I = 1 To j
            XX = Mid(tStr, I, 1)
            RemoveReturnKey = CStr(CDbl(RemoveReturnKey) + IIf(XX = Chr(System.Windows.Forms.Keys.Return) Or XX = Chr(10), " ", XX))
        Next I
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Shared Function SetSpreadColor(ByRef MySpread As AxFPSpreadADO.AxfpSpread, ByRef mRow As Integer, Optional ByRef mAlternateColorLine As Boolean = True) As Object
        On Error Resume Next
        'Dim mBackColor As String
        'Dim mAlterBackColor1 As String
        'Dim mAlterBackColor2 As String

        ''MySpread.AppearanceStyle = FPSpreadADO.AppearanceStyleConstants.AppearanceStyleEnhanced
        ''MySpread.AppearanceStyle = FPSpreadADO.AppearanceStyleConstants.AppearanceStyleClassic  ''FPSpreadADO.AppearanceStyleConstants.AppearanceStyleEnhanced
        'mBackColor = "&H507D2A" '' "&HBACBDB" ''"&H94998E"   '' Color.LightSteelBlue
        'mAlterBackColor1 = "&H8D8E99"
        'mAlterBackColor2 = "&HE1DCE0"        ''"&H8D8E99"



        MySpread.GrayAreaBackColor = PubFormBackColor 'System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor))       ''Color.AliceBlue
        MySpread.GrayAreaBackColor = PubFormBackColor 'System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor))       ''Color.AliceBlue
        MySpread.ShadowColor = PubSpdShodowColor ' System.Drawing.ColorTranslator.FromOle(CInt(PubSpdShodowColor)) 'System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor))     '' Color.SkyBlue
        MySpread.ShadowText = Color.Black       ''OrangeRed   ''  Black  ''&HFF
        MySpread.ScrollBarHColor = PubSpdShodowColor ' System.Drawing.ColorTranslator.FromOle(CInt(PubSpdShodowColor)) 'System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor))     ''  Color.AliceBlue
        MySpread.ScrollBarVColor = PubSpdShodowColor 'System.Drawing.ColorTranslator.FromOle(CInt(PubSpdShodowColor)) 'System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor))     '' Color.AliceBlue
        MySpread.SelForeColor = Color.Black
        MySpread.SelBackColor = Color.LightGoldenrodYellow      ''Black

        MySpread.ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsBoth '' 3 ''ScrollBarsBoth
        MySpread.ScrollBarExtMode = True
        MySpread.VScrollSpecial = True
        MySpread.VScrollSpecialType = 0 '' FPSpreadADO.VScrollSpecialTypeConstants.VScrollSpecialNoPageUpDown    ''VScrollSpecialTypeDefault       ''SS_VSCROLLSPECIAL_NO_PAGE_UP_DOWN
        MySpread.ProcessTab = True

        MySpread.Appearance = FPSpreadADO.AppearanceConstants.Appearance3DWithBorder ' = FPSpreadADO.AppearanceConstants.AppearanceFlat
        MySpread.ActiveCellHighlightStyle = FPSpreadADO.ActiveCellHighlightStyleConstants.ActiveCellHighlightStyleNormal

        'PubFormBackColor = "&HE1DCE0"
        'PubButtonBackColor = "&H507D2A"
        'PubSpdShodowColor = "&H507D2A"
        'PubSpdMainColor = "&H507D2A"
        'PubSpdAlterColor = "&H507D2A"


        If mAlternateColorLine = True Then
            ''MySpread.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            MySpread.SetOddEvenRowColor(System.Drawing.ColorTranslator.ToOle(PubSpdMainColor), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), System.Drawing.ColorTranslator.ToOle(PubSpdAlterColor), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        End If
        With MySpread
                '.Row = mRow
                '.Row2 = mRow

                '.Col = 1
                '.col2 = .MaxCols

                '.BlockMode = True

                .Row = mRow  '' 1
                .Row2 = mRow '' .MaxRows
                .Col = 1
                .Col2 = .MaxCols
                .BlockMode = True

                .BackColorStyle = 1     ''BackColorStyleUnderGrid
                .GridSolid = False

                'MySpread.ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsBoth '' 3 ''ScrollBarsBoth
                'MySpread.ScrollBarExtMode = False   ''True
                'MySpread.VScrollSpecial = True
                'MySpread.VScrollSpecialType = 0 '' FPSpreadADO.VScrollSpecialTypeConstants.VScrollSpecialNoPageUpDown    ''VScrollSpecialTypeDefault       ''SS_VSCROLLSPECIAL_NO_PAGE_UP_DOWN
                'MySpread.ProcessTab = True

                MySpread.AutoClipboard = True
                MySpread.MoveActiveOnFocus = True
                MySpread.EditEnterAction = FPSpreadADO.EditEnterActionConstants.EditEnterActionRight
                MySpread.BackColorStyle = 1     ''BackColorStyleUnderGrid
                MySpread.GridSolid = False

                .BlockMode = False

                '.ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsBoth '' 3 ''ScrollBarsBoth
                '.ScrollBarExtMode = False   ''True
                '.VScrollSpecial = True
                '.VScrollSpecialType = FPSpreadADO.VScrollSpecialTypeConstants.VScrollSpecialTypeDefault
                '.ProcessTab = True

                '.AutoClipboard = True
                '.MoveActiveOnFocus = True

            End With

    End Function

    Public Shared Function SetInfragisticsGrid(ByRef e As Infragistics.Win.UltraWinGrid.UltraGrid, ByRef mRow As Integer, pFilterRowPrompt As String, pGroupPrompt As String) As Object
        'Infragistics.Win.UltraWinGrid
        'e.DataSource = Me.UltraDataSource1
        Try
            'Me.UltraDataSource1.Band.Columns.Add("...", GetType(String))

            e.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow

            ' FilterEvaluationTrigger specifies when UltraGrid applies the filter criteria typed 
            ' into a filter row. Default is OnCellValueChange which will cause the UltraGrid to
            ' re-filter the data as soon as the user modifies the value of a filter cell.
            e.DisplayLayout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange

            ' By default the UltraGrid selects the type of the filter operand editor based on
            ' the column's DataType. For DateTime and boolean columns it uses the column's editors.
            ' For other column types it uses the Combo. You can explicitly specify the operand
            ' editor style by setting the FilterOperandStyle on the override or the individual
            ' columns.
            'e.DisplayLayout.Override.FilterOperandStyle = FilterOperandStyle.Combo;

            ' By default UltraGrid displays user interface for selecting the filter operator. 
            ' You can set the FilterOperatorLocation to hide this user interface. This
            ' property is available on column as well so it can be controlled on a per column
            ' basis. Default is WithOperand. This property is exposed off the column as well.
            e.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand

            ' By default the UltraGrid uses StartsWith as the filter operator. You use
            ' the FilterOperatorDefaultValue property to specify a different filter operator
            ' to use. This is the default or the initial filter operator value of the cells
            ' in filter row. If filter operator user interface is enabled (FilterOperatorLocation
            ' is not set to None) then that ui will be initialized to the value of this
            ' property. The user can then change the operator as he/she chooses via the operator
            ' drop down.
            e.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains       ''

            ' FilterOperatorDropDownItems property can be used to control the options provided
            ' to the user for selecting the filter operator. By default UltraGrid bases 
            ' what operator options to provide on the column's data type. This property is
            ' avaibale on the column as well.
            'e.DisplayLayout.Override.FilterOperatorDropDownItems = FilterOperatorDropDownItems.All;

            ' By default UltraGrid displays a clear button in each cell of the filter row
            ' as well as in the row selector of the filter row. When the user clicks this
            ' button the associated filter criteria is cleared. You can use the 
            ' FilterClearButtonLocation property to control if and where the filter clear
            ' buttons are displayed.
            e.DisplayLayout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell

            ' Appearance of the filter row can be controlled using the FilterRowAppearance proeprty.
            e.DisplayLayout.Override.FilterRowAppearance.BackColor = Color.LightYellow

            ' You can use the FilterRowPrompt to display a prompt in the filter row. By default
            ' UltraGrid does not display any prompt in the filter row.
            e.DisplayLayout.Override.FilterRowPrompt = pFilterRowPrompt

            ' You can use the FilterRowPromptAppearance to change the appearance of the prompt.
            ' By default the prompt is transparent and uses the same fore color as the filter row.
            ' You can make it non-transparent by setting the appearance' BackColorAlpha property 
            ' or by setting the BackColor to a desired value.
            e.DisplayLayout.Override.FilterRowPromptAppearance.BackColorAlpha = Alpha.Opaque

            ' By default the prompt is spread across multiple cells if it's bigger than the
            ' first cell. You can confine the prompt to a particular cell by setting the
            ' SpecialRowPromptField property off the band to the key of a column.
            'e.DisplayLayout.Bands[0].SpecialRowPromptField = e.DisplayLayout.Bands[0].Columns[0].Key;

            ' Display a separator between the filter row other rows. SpecialRowSeparator property 
            ' can be used to display separators between various 'special' rows, including for the
            ' filter row. This property is a flagged enum property so it can take multiple values.
            e.DisplayLayout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow


            e.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True

            ''To Stop the resizing of row
            e.DisplayLayout.Override.RowSizing = RowSizing.Fixed

            ''For Selecting a single row
            e.DisplayLayout.Override.SelectTypeRow = SelectType.Single

            ''To stop the resizzing of Column
            e.DisplayLayout.Override.AllowColSizing = AllowColSizing.Free

            ''To display row no on the row header
            e.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            If pGroupPrompt = "" Then
                e.DisplayLayout.GroupByBox.Hidden = True
            Else
                e.DisplayLayout.GroupByBox.Prompt = pGroupPrompt
            End If

            'Freezing First primary editable column
            'e.DisplayLayout.UseFixedHeaders = True
            'Commented by Alok on 11th Feb 2009 as we will come to it in future (End)

            'ChangeGridColor(e, e.Rows.Count - 1)


            e.DisplayLayout.Override.CellAppearance.BackColor = Color.White
            e.DisplayLayout.Override.CellAppearance.ForeColor = Color.Navy

            e.DisplayLayout.Override.HeaderAppearance.BackColor = Color.LightBlue
            e.DisplayLayout.Override.HeaderAppearance.ForeColor = Color.Red

            'e.DisplayLayout.Bands(0).Columns(0).RowLayoutColumnInfo.PreferredLabelSize = New System.Drawing.Size(0, 20) '' New System.Drawing.Size(0, (Int())_columnHeaderHeight)

            'rootColumnSet.HeaderStyle = HeaderStyle.Standard
            e.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.Standard
            e.DisplayLayout.Override.RowSelectorStyle = Infragistics.Win.HeaderStyle.Standard

            e.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            e.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            '            In the Override property, scroll down to either the HeaderStyle Or RowSelectorStyle properties. Both of these properties have the same four following settings
            'Default - The default setting.
            'HeaderStyle -takes on the 'Standard' style
            '            RowSelectorStyle -takes on the 'XPThemed' style
            '            Standard -A flat 'Office 2000' look, with hot-tracking.
            '            WindowsXPCommand -The look And feel Of a command button In Windows XP, with orange hot-tracking around all four sides of the element.
            'XPThemed -The themed look drawn by the Windows XP operating system.

        Catch sqlex As SqlException
            ErrorMsg(Err.Description, Err.Number)   ''sqlex.Message, "FrmBaseMaster.vb", "UltraGrid1_InitializeLayout", "", "", "Sql Exception")
        Catch ex As Exception
            ErrorMsg(Err.Description, Err.Number)   ''ErrorTrap(ex.Message, "FrmBaseMaster.vb", "UltraGrid1_InitializeLayout", "", "", "")
        End Try


    End Function
    Public Function hexToRbgNew(ByVal Hex As String) As Color
        Hex = Replace(Hex, "#", "")
        Dim red As String = "&H" & Hex.Substring(0, 2)
        Hex = Replace(Hex, red, "", , 1)
        Dim green As String = "&H" & Hex.Substring(0, 2)
        Hex = Replace(Hex, green, "", , 1)
        Dim blue As String = "&H" & Hex.Substring(0, 2)
        Hex = Replace(Hex, blue, "", , 1)
        Return Color.FromArgb(red, green, blue)
    End Function
    Public Shared Function SetControlColors(ByRef pControl As System.Windows.Forms.Control) As Object
        On Error Resume Next
        Dim mControl As System.Windows.Forms.Control
        mControl = Nothing
        Dim mImageName As String
        Dim mImage As Boolean = True
        'Dim mBackColor As String
        'Dim mForColor As String

        'Dim mAlterBackColor As String
        'Dim mAlterForColor As String

        'mBackColor = "#8D8E99" '' "#94998E"   '' Color.LightSteelBlue
        'mAlterBackColor = "#94998E"  '"#8D8E99"

        'mBackColor = "&H94998E"   '' Color.LightSteelBlue
        'mAlterBackColor = "&H8D8E99"

        'mBackColor = "&HE4E5EA" '' "&HE1DCE0" ''"&H94998E"   '' Color.LightSteelBlue
        'mAlterBackColor = "&HCEAD6D" '' "&HCEAD6D"        ''"&H8D8E99" ''#CEAD6D



        If TypeOf pControl Is System.Windows.Forms.TextBox Then
            Dim mTextBox As System.Windows.Forms.TextBox = pControl
            pControl.BackColor = Color.White ''System.Drawing.ColorTranslator.FromOle(CInt(mTextBoxBackColor))
            pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(CInt(mTextBoxForeColor))
            pControl.Font = New Drawing.Font(mTextBoxFontName, CDec(mTextBoxFontSize), FontStyle.Regular)
        ElseIf TypeOf pControl Is System.Windows.Forms.MaskedTextBox Then
            Dim mMaskedTextBox As System.Windows.Forms.MaskedTextBox = pControl
            mMaskedTextBox.BackColor = Color.White      '' System.Drawing.ColorTranslator.FromOle(CInt(mTextBoxBackColor))
            pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(CInt(mTextBoxForeColor))
            pControl.Font = New Drawing.Font(mTextBoxFontName, CDec(mTextBoxFontSize), FontStyle.Regular)
        ElseIf TypeOf pControl Is System.Windows.Forms.GroupBox Then
            pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(CInt(mFrameForeColor))
            pControl.BackColor = PubFormBackColor ' System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor))   '' System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor))     ''Color.LightSteelBlue       ''AliceBlue '' System.Drawing.ColorTranslator.FromOle(CInt(mFrameBackColor))
            pControl.Font = New Drawing.Font(mFrameFontName, CDec(mFrameFontSize), FontStyle.Regular)

        ElseIf TypeOf pControl Is System.Windows.Forms.Button Then
            Dim mCmdButton As System.Windows.Forms.Button = pControl

            Select Case UCase(mCmdButton.Name)
                Case UCase("cmdAdd")
                    mImageName = "add.png"
                Case UCase("cmdModify")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                        mImageName = ""
                        mImage = False
                    Else
                        mImageName = "Modify.png"
                    End If
                Case UCase("cmdSave")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                        mImageName = ""
                        mImage = False
                    Else
                        mImageName = "save.png"
                    End If
                Case UCase("cmdAuthorised")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                        mImageName = ""
                        mImage = False
                    Else
                        mImageName = "Authorised.png"
                    End If
                Case UCase("cmdDelete")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                        mImageName = ""
                        mImage = False
                    Else
                        mImageName = "delete.png"
                    End If
                Case UCase("cmdBarCode")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                        mImageName = ""
                        mImage = False
                    Else
                        mImageName = "BarCode.png"
                    End If
                Case UCase("cmdPostingHead")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                        mImageName = ""
                        mImage = False
                    Else
                        mImageName = "PostingHead.png"
                    End If
                Case UCase("cmdSavePrint")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                        mImageName = ""
                        mImage = False
                    Else
                        mImageName = "SavePrint.png"
                        pControl.Font = New Drawing.Font(mCommandButtonFontName, 7, FontStyle.Regular)
                    End If
                Case UCase("cmdPrint")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                        mImageName = ""
                        mImage = False
                    Else
                        mImageName = "Print.png"
                    End If
                Case UCase("CmdPreview")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                        mImageName = ""
                        mImage = False
                    Else
                        mImageName = "Preview.png"
                    End If
                Case UCase("CmdView")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                        mImageName = ""
                        mImage = False
                    Else
                        mImageName = "View.png"
                    End If
                Case UCase("cmdClose")
                    'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                    '    mImageName = ""
                    'Else
                    mImageName = "close.png"
                    'End If
                Case UCase("cmdExport"), UCase("cmdDetail")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                        mImageName = ""
                        mImage = False
                    Else
                        mImageName = "Export.png"
                    End If
                Case Else
                    'pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    'pControl.ForeColor = Color.Black
                    'mCmdButton.FlatStyle = FlatStyle.Flat
                    If IsDBNull(mCmdButton.Image) Then
                        mImage = False
                        'mCmdButton.TextAlign = ContentAlignment.MiddleCenter
                    Else
                        'mCmdButton.ImageAlign = ContentAlignment.TopCenter
                        'mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    End If
            End Select


            mCmdButton.FlatStyle = FlatStyle.Standard
            mCmdButton.FlatAppearance.MouseOverBackColor = Color.OrangeRed
            pControl.BackColor = PubButtonBackColor 'System.Drawing.ColorTranslator.FromOle(CInt(PubButtonBackColor)) '' System.Drawing.ColorTranslator.FromOle(CInt(PubButtonBackColor))        ''Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
            pControl.ForeColor = Color.Black
            pControl.Font = New Drawing.Font(mFrameFontName, CDec(mFrameFontSize), FontStyle.Bold)

            If mImageName = "" And mImage = False Then
                mCmdButton.Image = Nothing
                mCmdButton.TextAlign = ContentAlignment.MiddleCenter
            ElseIf mImageName = "" And mImage = True Then
                mCmdButton.ImageAlign = ContentAlignment.MiddleLeft
                mCmdButton.TextAlign = ContentAlignment.MiddleRight
            Else
                mCmdButton.Image = Image.FromFile(PubButtonPath & mImageName)
                mCmdButton.ImageAlign = ContentAlignment.MiddleLeft
                mCmdButton.TextAlign = ContentAlignment.MiddleRight
            End If

            'If IsDBNull(mCmdButton.Image) Then
            '    mCmdButton.TextAlign = ContentAlignment.MiddleCenter
            'Else
            '    mCmdButton.ImageAlign = ContentAlignment.MiddleLeft
            '    mCmdButton.TextAlign = ContentAlignment.MiddleRight
            'End If

            pControl.Font = New Drawing.Font(mCommandButtonFontName, CDec(mCommandButtonFontSize), FontStyle.Bold)

        ElseIf TypeOf pControl Is System.Windows.Forms.TabControl Then
            Dim mCmdButton As System.Windows.Forms.TabControl = pControl
            pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(&H80)
            pControl.BackColor = PubFormBackColor ' System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor)) 'System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor))        ''Color.LightSteelBlue       ''AliceBlue ' System.Drawing.ColorTranslator.FromOle(&HFFFF80)

        ElseIf TypeOf pControl Is System.Windows.Forms.TabPage Then

            pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(&H80)
            pControl.BackColor = PubFormBackColor ' System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor)) 'System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor))        ''Color.LightSteelBlue       '' ' System.Drawing.ColorTranslator.FromOle(&HFFFF80)

        ElseIf TypeOf pControl Is System.Windows.Forms.ComboBox Then
            Dim mComboBox As System.Windows.Forms.ComboBox = pControl

            mComboBox.FlatStyle = FlatStyle.Popup
            pControl.BackColor = Color.LightYellow
            pControl.ForeColor = Color.Black
            'pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(mComboBoxForeColor))
            'pControl.BackColor = System.Drawing.ColorTranslator.FromOle(CInt(mComboBoxBackColor))
            pControl.Font = New Drawing.Font(mComboBoxFontName, CDec(mComboBoxFontSize), FontStyle.Regular)

        ElseIf TypeOf pControl Is System.Windows.Forms.ListBox Then
            pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue
            pControl.Font = New Drawing.Font(mFormFontName, CDec(mFormFontSize), FontStyle.Regular)
            'ElseIf TypeOf pControl Is AxMSDataListLib.AxDataCombo Then
            '    pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue
            'ElseIf TypeOf pControl Is AxMSDataListLib.AxDataList Then
            '    pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue

        ElseIf TypeOf pControl Is System.Windows.Forms.RadioButton Then
            pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(mOptionButtonForeColor))
            pControl.BackColor = Color.Transparent  '' System.Drawing.ColorTranslator.FromOle(CInt(mOptionButtonBackColor))
            pControl.Font = New Drawing.Font(mOptionButtonFontName, CDec(mOptionButtonFontSize), FontStyle.Regular)

        ElseIf TypeOf pControl Is System.Windows.Forms.CheckBox Then
            pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(mCheckBoxForeColor))
            pControl.BackColor = Color.Transparent   '' System.Drawing.ColorTranslator.FromOle(CInt(mCheckBoxBackColor))
            pControl.Font = New Drawing.Font(mCheckBoxFontName, CDec(mCheckBoxFontSize), FontStyle.Regular)

        ElseIf TypeOf pControl Is System.Windows.Forms.Label Then
            Dim mLabel As System.Windows.Forms.Label = pControl

            'mLabel.
            mLabel.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(mLabelForeColor))
            mLabel.BackColor = Color.Transparent '' System.Drawing.ColorTranslator.FromOle(CInt(mLabelBackColor))
            mLabel.Enabled = True
            mLabel.Font = New Drawing.Font(mLabelFontName, CDec(mLabelFontSize), FontStyle.Bold)
            'mLabel.BorderStyle = BorderStyle.Fixed3D
            mLabel.FlatStyle = FlatStyle.Popup

            'ElseIf TypeOf pControl Is AxMSHierarchicalFlexGridLib.AxMSHFlexGrid Then
            '    pControl.BackColor = System.Drawing.ColorTranslator.FromOle(CInt(mMSHFlexGridBackColor))
            '    pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(mMSHFlexGridForeColor))
            '    MyForm.Font = New Drawing.Font(mMSHFlexGridFontName, CDec(mMSHFlexGridFontSize), FontStyle.Regular)

        ElseIf TypeOf pControl Is AxFPSpreadADO.AxfpSpread Then
            Dim pSprdMain As AxFPSpreadADO.AxfpSpread = pControl

            'pSprdMain.hear
            pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(CInt(mvaSpreadForeColor))
            pControl.Font = New Drawing.Font(mvaSpreadFontName, CDec(mvaSpreadFontSize), FontStyle.Regular)
        End If


    End Function
    Public Shared Function SetControlColorsOld(ByRef pControl As System.Windows.Forms.Control) As Object
        On Error Resume Next
        Dim mControl As System.Windows.Forms.Control
        mControl = Nothing


        If TypeOf pControl Is System.Windows.Forms.TextBox Then
            Dim mTextBox As System.Windows.Forms.TextBox = pControl
            'If Not mTextBox.Enabled Then
            '    mTextBox.BackColor = Color.White
            '    mTextBox.ForeColor = Color.Blue '' System.Drawing.ColorTranslator.FromOle(CInt(mTextBoxForeColor))
            'Else
            '    pControl.BackColor = Color.White ''System.Drawing.ColorTranslator.FromOle(CInt(mTextBoxBackColor))
            '    pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(CInt(mTextBoxForeColor))
            'End If

            pControl.BackColor = Color.White ''System.Drawing.ColorTranslator.FromOle(CInt(mTextBoxBackColor))
            pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(CInt(mTextBoxForeColor))
            pControl.Font = New Drawing.Font(mTextBoxFontName, CDec(mTextBoxFontSize), FontStyle.Regular)
            'pControl.displaystyle = ScenicRibbon
        ElseIf TypeOf pControl Is System.Windows.Forms.MaskedTextBox Then
            Dim mMaskedTextBox As System.Windows.Forms.MaskedTextBox = pControl
            'If Not mMaskedTextBox.Enabled Then
            '    mMaskedTextBox.BackColor = Color.White
            'Else
            '    pControl.BackColor = Color.White ''System.Drawing.ColorTranslator.FromOle(CInt(mTextBoxBackColor))
            'End If
            mMaskedTextBox.BackColor = Color.White      '' System.Drawing.ColorTranslator.FromOle(CInt(mTextBoxBackColor))
            pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(CInt(mTextBoxForeColor))
            pControl.Font = New Drawing.Font(mTextBoxFontName, CDec(mTextBoxFontSize), FontStyle.Regular)
        ElseIf TypeOf pControl Is System.Windows.Forms.GroupBox Then
            pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(CInt(mFrameForeColor))
            pControl.BackColor = Color.LightSteelBlue       ''AliceBlue '' System.Drawing.ColorTranslator.FromOle(CInt(mFrameBackColor))
            pControl.Font = New Drawing.Font(mFrameFontName, CDec(mFrameFontSize), FontStyle.Regular)

        ElseIf TypeOf pControl Is System.Windows.Forms.Button Then
            Dim mCmdButton As System.Windows.Forms.Button = pControl

            Select Case UCase(mCmdButton.Name)
                Case UCase("cmdAdd")
                    mCmdButton.Image = Image.FromFile(PubButtonPath & "add.png")
                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.Standard
                    mCmdButton.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    mCmdButton.ForeColor = Color.Black


                Case UCase("cmdModify")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                        mCmdButton.Image = Nothing
                    Else
                        mCmdButton.Image = Image.FromFile(PubButtonPath & "Modify.png")
                    End If

                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.Popup  ''.Flat
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black

                Case UCase("cmdSave")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                        mCmdButton.Image = Nothing
                    Else
                        mCmdButton.Image = Image.FromFile(PubButtonPath & "save.png")
                    End If


                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.System     ''.Flat
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black

                Case UCase("cmdAuthorised")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                        mCmdButton.Image = Nothing
                    Else
                        mCmdButton.Image = Image.FromFile(PubButtonPath & "Authorised.png")
                    End If

                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.Flat
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black

                Case UCase("cmdDelete")
                    mCmdButton.Image = Image.FromFile(PubButtonPath & "delete.png")
                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.Flat
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black

                Case UCase("cmdBarCode")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                        mCmdButton.Image = Nothing
                    Else
                        mCmdButton.Image = Image.FromFile(PubButtonPath & "BarCode.png")
                    End If

                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.Flat
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black

                Case UCase("cmdPostingHead")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                        mCmdButton.Image = Nothing
                    Else
                        mCmdButton.Image = Image.FromFile(PubButtonPath & "PostingHead.png")
                    End If

                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.Flat
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black

                Case UCase("cmdSavePrint")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                        mCmdButton.Image = Nothing
                    Else
                        mCmdButton.Image = Image.FromFile(PubButtonPath & "SavePrint.png")
                    End If

                    mCmdButton.Text = "SavePrint"
                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.Flat
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black
                    pControl.Font = New Drawing.Font(mCommandButtonFontName, 7, FontStyle.Regular)
                Case UCase("cmdPrint")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                        mCmdButton.Image = Nothing
                    Else
                        mCmdButton.Image = Image.FromFile(PubButtonPath & "Print.png")
                    End If

                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.Flat
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black

                Case UCase("CmdPreview")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                        mCmdButton.Image = Nothing
                    Else
                        mCmdButton.Image = Image.FromFile(PubButtonPath & "Preview.png")
                    End If

                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.Flat
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black

                Case UCase("CmdView")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                        mCmdButton.Image = Nothing
                    Else
                        mCmdButton.Image = Image.FromFile(PubButtonPath & "View.png")
                    End If

                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.Flat
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black

                Case UCase("cmdClose")
                    mCmdButton.Image = Image.FromFile(PubButtonPath & "close.png")
                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.Flat
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black
                Case UCase("cmdExport"), UCase("cmdDetail")
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                        mCmdButton.Image = Nothing
                    Else
                        mCmdButton.Image = Image.FromFile(PubButtonPath & "Export.png")
                    End If

                    mCmdButton.ImageAlign = ContentAlignment.TopCenter
                    mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    mCmdButton.FlatStyle = FlatStyle.Flat
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black
                Case Else
                    pControl.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
                    pControl.ForeColor = Color.Black
                    mCmdButton.FlatStyle = FlatStyle.Flat
                    If IsDBNull(mCmdButton.Image) Then
                        mCmdButton.TextAlign = ContentAlignment.MiddleCenter
                    Else
                        mCmdButton.ImageAlign = ContentAlignment.TopCenter
                        mCmdButton.TextAlign = ContentAlignment.BottomCenter
                    End If
            End Select

            mCmdButton.FlatStyle = FlatStyle.Standard
            mCmdButton.BackColor = Color.AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mCommandButtonBackColor))
            mCmdButton.ForeColor = Color.Black

            If IsDBNull(mCmdButton.Image) Then
                mCmdButton.TextAlign = ContentAlignment.MiddleCenter
            Else
                mCmdButton.ImageAlign = ContentAlignment.MiddleLeft
                mCmdButton.TextAlign = ContentAlignment.MiddleRight
            End If

            pControl.Font = New Drawing.Font(mCommandButtonFontName, CDec(mCommandButtonFontSize), FontStyle.Regular)

        ElseIf TypeOf pControl Is System.Windows.Forms.TabControl Then
            Dim mCmdButton As System.Windows.Forms.TabControl = pControl
            pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(&H80)
            pControl.BackColor = Color.LightSteelBlue       ''AliceBlue ' System.Drawing.ColorTranslator.FromOle(&HFFFF80)

        ElseIf TypeOf pControl Is System.Windows.Forms.TabPage Then

            pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(&H80)
            pControl.BackColor = Color.LightSteelBlue       '' ' System.Drawing.ColorTranslator.FromOle(&HFFFF80)

        ElseIf TypeOf pControl Is System.Windows.Forms.ComboBox Then
            Dim mComboBox As System.Windows.Forms.ComboBox = pControl

            mComboBox.FlatStyle = FlatStyle.Popup
            pControl.BackColor = Color.LightYellow
            pControl.ForeColor = Color.Black
            'pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(mComboBoxForeColor))
            'pControl.BackColor = System.Drawing.ColorTranslator.FromOle(CInt(mComboBoxBackColor))
            pControl.Font = New Drawing.Font(mComboBoxFontName, CDec(mComboBoxFontSize), FontStyle.Regular)

        ElseIf TypeOf pControl Is System.Windows.Forms.ListBox Then
            pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue
            pControl.Font = New Drawing.Font(mFormFontName, CDec(mFormFontSize), FontStyle.Regular)
            'ElseIf TypeOf pControl Is AxMSDataListLib.AxDataCombo Then
            '    pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue
            'ElseIf TypeOf pControl Is AxMSDataListLib.AxDataList Then
            '    pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue

        ElseIf TypeOf pControl Is System.Windows.Forms.RadioButton Then
            pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(mOptionButtonForeColor))
            pControl.BackColor = Color.Transparent  '' System.Drawing.ColorTranslator.FromOle(CInt(mOptionButtonBackColor))
            pControl.Font = New Drawing.Font(mOptionButtonFontName, CDec(mOptionButtonFontSize), FontStyle.Regular)

        ElseIf TypeOf pControl Is System.Windows.Forms.CheckBox Then
            pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(mCheckBoxForeColor))
            pControl.BackColor = Color.Transparent   '' System.Drawing.ColorTranslator.FromOle(CInt(mCheckBoxBackColor))
            pControl.Font = New Drawing.Font(mCheckBoxFontName, CDec(mCheckBoxFontSize), FontStyle.Regular)

        ElseIf TypeOf pControl Is System.Windows.Forms.Label Then
            Dim mLabel As System.Windows.Forms.Label = pControl

            'mLabel.
            mLabel.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(mLabelForeColor))
            mLabel.BackColor = Color.Transparent '' System.Drawing.ColorTranslator.FromOle(CInt(mLabelBackColor))
            mLabel.Enabled = True
            mLabel.Font = New Drawing.Font(mLabelFontName, CDec(mLabelFontSize), FontStyle.Bold)
            'mLabel.BorderStyle = BorderStyle.Fixed3D
            mLabel.FlatStyle = FlatStyle.Popup

            'ElseIf TypeOf pControl Is AxMSHierarchicalFlexGridLib.AxMSHFlexGrid Then
            '    pControl.BackColor = System.Drawing.ColorTranslator.FromOle(CInt(mMSHFlexGridBackColor))
            '    pControl.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(mMSHFlexGridForeColor))
            '    MyForm.Font = New Drawing.Font(mMSHFlexGridFontName, CDec(mMSHFlexGridFontSize), FontStyle.Regular)

        ElseIf TypeOf pControl Is AxFPSpreadADO.AxfpSpread Then
            Dim pSprdMain As AxFPSpreadADO.AxfpSpread = pControl

            'pSprdMain.hear
            pControl.ForeColor = Color.Black '' System.Drawing.ColorTranslator.FromOle(CInt(mvaSpreadForeColor))
            pControl.Font = New Drawing.Font(mvaSpreadFontName, CDec(mvaSpreadFontSize), FontStyle.Regular)
        End If


    End Function
    Public Shared Function SetControlsColor(ByRef MyForm As System.Windows.Forms.Form) As Object
        On Error Resume Next
        Dim mControl As System.Windows.Forms.Control
        mControl = Nothing

        'Dim mBackColor As String
        'Dim mForColor As String

        'Dim mAlterBackColor As String
        'Dim mAlterForColor As String

        'Dim mBackRed As Integer
        'Dim mBackGreen As Integer
        'Dim mBackBlue As Integer

        'mBackColor = "&HE1DCE0" ''"&H94998E"   '' Color.LightSteelBlue
        'mAlterBackColor = "&HCEAD6D"        ''"&H8D8E99"

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
        '    PubFormBackColor = &HF5F6F7 ' "&HE5E5E5"
        '    PubButtonBackColor = &HD5DADD ' "&HD5DADC"
        '    PubSpdShodowColor = &HD5DADD ' &HA9B4BE ' "&HCCCCCC"

        '    PubSpdMainColor = &HFFFFFF ' &HF5F6F7 '"&HD5DADC"
        '    PubSpdAlterColor = &HD5DADD '  &HFFFFFF ' "&HF8CC8C"
        'Else
        '    PubFormBackColor = &HF0F8FF ' "&HE5E5E5"
        '    PubButtonBackColor = &HF0F8FF ' "&HD5DADC"
        '    PubSpdShodowColor = &H87CEEB ' &HA9B4BE ' "&HCCCCCC"

        '    PubSpdMainColor = &HC0FFFF ' &HF5F6F7 '"&HD5DADC"
        '    PubSpdAlterColor = &HFFFFC0 '  &HFFFFFF ' "&HF8CC8C"
        'End If

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
        '    PubFormBackColor = System.Drawing.ColorTranslator.FromHtml("#F5F6F7") ' "&HE5E5E5"
        '    PubButtonBackColor = System.Drawing.ColorTranslator.FromHtml("#D5DADD") ' "&HD5DADC"
        '    PubSpdShodowColor = System.Drawing.ColorTranslator.FromHtml("#D5DADD") ' &HA9B4BE ' "&HCCCCCC"

        '    PubSpdMainColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF") ' &HF5F6F7 '"&HD5DADC"
        '    PubSpdAlterColor = System.Drawing.ColorTranslator.FromHtml("#D5DADD") '  &HFFFFFF ' "&HF8CC8C"
        'Else
        '    PubFormBackColor = Color.AliceBlue       ' &HF0F8FF ' "&HE5E5E5"
        '    PubButtonBackColor = Color.AliceBlue       ' &HF0F8FF ' "&HD5DADC"
        '    PubSpdShodowColor = System.Drawing.ColorTranslator.FromHtml("#87CEEB") ' &HA9B4BE ' "&HCCCCCC"

        '    PubSpdMainColor = System.Drawing.ColorTranslator.FromHtml("#C0FFFF") ' &HF5F6F7 '"&HD5DADC"
        '    PubSpdAlterColor = System.Drawing.ColorTranslator.FromHtml("#FFFFC0") '  &HFFFFFF ' "&HF8CC8C"
        'End If

        If PubColorTheme = 1 Then
            PubFormBackColor = Color.AliceBlue       ' &HF0F8FF ' "&HE5E5E5"
            PubButtonBackColor = Color.AliceBlue       ' &HF0F8FF ' "&HD5DADC"
            PubSpdShodowColor = System.Drawing.ColorTranslator.FromHtml("#87CEEB") ' &HA9B4BE ' "&HCCCCCC"

            PubSpdMainColor = System.Drawing.ColorTranslator.FromHtml("#C0FFFF") ' &HF5F6F7 '"&HD5DADC"
            PubSpdAlterColor = System.Drawing.ColorTranslator.FromHtml("#FFFFC0") '  &HFFFFFF ' "&HF8CC8C"
        ElseIf PubColorTheme = 2 Then
            PubFormBackColor = System.Drawing.ColorTranslator.FromHtml("#E7ECF0") ' "&HE5E5E5"
            PubButtonBackColor = System.Drawing.ColorTranslator.FromHtml("#E5E5E5") ' "&HD5DADC"
            PubSpdShodowColor = System.Drawing.ColorTranslator.FromHtml("#A9C6DE") ' &HA9B4BE ' "&HCCCCCC"

            PubSpdMainColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF") ' &HF5F6F7 '"&HD5DADC"
            PubSpdAlterColor = System.Drawing.ColorTranslator.FromHtml("#D5DADD") '  &HFFFFFF ' "&HF8CC8C"
        ElseIf PubColorTheme = 3 Then
            PubFormBackColor = System.Drawing.ColorTranslator.FromHtml("#EAEEEF") ' "&HE5E5E5"
            PubButtonBackColor = System.Drawing.ColorTranslator.FromHtml("#EAEEEF") '("#F6E8DA") ' "&HD5DADC"
            PubSpdShodowColor = System.Drawing.ColorTranslator.FromHtml("#A7C4D4") ' &HA9B4BE ' "&HCCCCCC"

            PubSpdMainColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF") ' &HF5F6F7 '"&HD5DADC"
            PubSpdAlterColor = System.Drawing.ColorTranslator.FromHtml("#D5DADD") '  &HFFFFFF ' "&HF8CC8C"
        End If
        ''''MySpread.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))


        If pFormPic <> "" Then
            If UCase(Right(pFormPic, 3)) = UCase("ico") Then
                MyForm.Icon = New System.Drawing.Icon(My.Application.Info.DirectoryPath & "\Picture\" & pFormPic)
            End If
        End If

        MyForm.BackColor = PubFormBackColor ' System.Drawing.ColorTranslator.FromOle(CInt(PubFormBackColor)) ''Color.LightSteelBlue     ''AliceBlue      '' System.Drawing.ColorTranslator.FromOle(CInt(mFormBackColor))
        MyForm.ForeColor = Color.Black ''  LightSkyBlue    '' System.Drawing.ColorTranslator.FromOle(CInt(mFormForeColor))
        MyForm.Font = New Drawing.Font(mFormFontName, CDec(mFormFontSize), FontStyle.Regular)

        For Each mControl In MyForm.Controls
            SetControlColors(mControl)
            Call ProcessControls(mControl)
        Next
        '        For Each mControl In MyForm.Controls
        'NextChild:
        '            For Each mChildControl As Control In mControl.Controls
        '                If mChildControl.HasChildren = True Then
        '                    SetControlColors(mChildControl)
        '                    mControl = mChildControl
        '                    GoTo NextChild
        '                Else
        '                    SetControlColors(mChildControl)
        '                End If
        '            Next
        '        Next

    End Function
    Public Shared Sub ProcessControls(ByVal ctrlContainer As Control)
        For Each ctrl As Control In ctrlContainer.Controls
            SetControlColors(ctrl)
            If ctrl.HasChildren Then
                ProcessControls(ctrl)
            End If
        Next

    End Sub
    Public Shared Sub ReadControlsColor()
        On Error Resume Next
        mFormBackColor = ReadInI("Form", "BackColor", "Color.ini")
        mFormForeColor = ReadInI("Form", "ForeColor", "Color.ini")
        mFormFontName = ReadInI("Form", "Font", "Font.ini")
        mFormFontSize = ReadInI("Form", "Size", "Font.ini")
        mFormFontBold = ReadInI("Form", "Bold", "Font.ini")
        mTextBoxForeColor = ReadInI("Text Box", "ForeColor", "Color.ini")
        mTextBoxBackColor = ReadInI("Text Box", "BackColor", "Color.ini")
        mTextBoxFontName = ReadInI("Text Box", "Font", "Font.ini")
        mTextBoxFontSize = ReadInI("Text Box", "Size", "Font.ini")
        mTextBoxFontBold = ReadInI("Text Box", "Bold", "Font.ini")
        mFrameForeColor = ReadInI("Frame", "ForeColor", "Color.ini")
        mFrameBackColor = ReadInI("Frame", "BackColor", "Color.ini")
        mFrameFontName = ReadInI("Frame", "Font", "Font.ini")
        mFrameFontSize = ReadInI("Frame", "Size", "Font.ini")
        mFrameFontBold = ReadInI("Frame", "Bold", "Font.ini")
        mCommandButtonBackColor = ReadInI("Command Button", "BackColor", "Color.ini")
        mCommandButtonMaskColor = ReadInI("Command Button", "MaskColor", "Color.ini")
        mCommandButtonFontName = ReadInI("Command Button", "Font", "Font.ini")
        mCommandButtonFontSize = ReadInI("Command Button", "Size", "Font.ini")
        mCommandButtonFontBold = ReadInI("Command Button", "Bold", "Font.ini")
        mComboBoxForeColor = ReadInI("Combo Box", "ForeColor", "Color.ini")
        mComboBoxBackColor = ReadInI("Combo Box", "BackColor", "Color.ini")
        mComboBoxFontName = ReadInI("Combo Box", "Font", "Font.ini")
        mComboBoxFontSize = ReadInI("Combo Box", "Size", "Font.ini")
        mComboBoxFontBold = ReadInI("Combo Box", "Bold", "Font.ini")
        mOptionButtonForeColor = ReadInI("Option Button", "ForeColor", "Color.ini")
        mOptionButtonBackColor = ReadInI("Option Button", "BackColor", "Color.ini")
        mOptionButtonMaskColor = ReadInI("Option Button", "MaskColor", "Color.ini")
        mOptionButtonFontName = ReadInI("Option Button", "Font", "Font.ini")
        mOptionButtonFontSize = ReadInI("Option Button", "Size", "Font.ini")
        mOptionButtonFontBold = ReadInI("Option Button", "Bold", "Font.ini")
        mCheckBoxForeColor = ReadInI("Check Box", "ForeColor", "Color.ini")
        mCheckBoxBackColor = ReadInI("Check Box", "BackColor", "Color.ini")
        mCheckBoxMaskColor = ReadInI("Check Box", "MaskColor", "Color.ini")
        mCheckBoxFontName = ReadInI("Check Box", "Font", "Font.ini")
        mCheckBoxFontSize = ReadInI("Check Box", "Size", "Font.ini")
        mCheckBoxFontBold = ReadInI("Check Box", "Bold", "Font.ini")
        mLabelForeColor = ReadInI("Label", "ForeColor", "Color.ini")
        mLabelBackColor = ReadInI("Label", "BackColor", "Color.ini")
        mLabelFontName = ReadInI("Label", "Font", "Font.ini")
        mLabelFontSize = ReadInI("Label", "Size", "Font.ini")
        mLabelFontBold = ReadInI("Label", "Bold", "Font.ini")
        mMSHFlexGridBackColor = ReadInI("View Grid", "BackColor", "Color.ini")
        mMSHFlexGridForeColor = ReadInI("View Grid", "ForeColor", "Color.ini")
        mMSHFlexGridBackColorSel = ReadInI("View Grid", "BackColorSel", "Color.ini")
        mMSHFlexGridBackColorFixed = ReadInI("View Grid", "BackColorFixed", "Color.ini")
        mMSHFlexGridForeColorFixed = ReadInI("View Grid", "ForeColorFixed", "Color.ini")
        mMSHFlexGridFontName = ReadInI("View Grid", "Font", "Font.ini")
        mMSHFlexGridFontSize = ReadInI("View Grid", "Size", "Font.ini")
        mMSHFlexGridFontBold = ReadInI("View Grid", "Bold", "Font.ini")
        mvaSpreadShadowColor = ReadInI("Entry Grid", "ShadowColor", "Color.ini")
        mvaSpreadShadowText = ReadInI("Entry Grid", "ShadowText", "Color.ini")
        mvaSpreadForeColor = ReadInI("Entry Grid", "ForeColor", "Color.ini")
        mvaSpreadGrayAreaBackColor = ReadInI("Entry Grid", "GrayAreaBackColor", "Color.ini")
        mvaSpreadGridColor = ReadInI("Entry Grid", "GridColor", "Color.ini")
        mvaSpreadLockForeColor = ReadInI("Entry Grid", "LockForeColor", "Color.ini")
        mvaSpreadFontName = ReadInI("Entry Grid", "Font", "Font.ini")
        mvaSpreadFontSize = ReadInI("Entry Grid", "Size", "Font.ini")
        mvaSpreadFontBold = ReadInI("Entry Grid", "Bold", "Font.ini")
    End Sub
    Public Shared Function SequenceVal(ByRef SequenceName As String, ByRef DbCN As Connection) As Integer
        '        On Error GoTo ERR1
        '        Dim SqlStr As String=""
        '        Dim RS As New Recordset
        '        SqlStr = "Select " & SequenceName & ".Nextval from dual"
        '        MainClass.UOpenRecordSet(SqlStr, DbCN,CursorTypeEnum.adOpenStatic, RS,LockTypeEnum.adLockReadOnly)
        '        SequenceVal = RS.Fields(0).Value
        '        Exit Function
        'ERR1:
        '        ErrorMsg(Err.Desc ription, CStr(Err.Number), MsgBoxStyle.Critical)
        '        MsgInformation("Error occured in generating the rowno from sequence :     " & SequenceName)
    End Function

    'Public Shared Function AutoGenRowNo(mTable As String, mMaxField As String, dbcn As Connection, Optional mCondition As String) As Long
    'On Error GoTo Err1
    'Static Rs As Recordset
    'Static SqlStr As String
    '    Set Rs = Nothing
    '    SqlStr = "Select Max(" & mMaxField & ") from " & mTable & " " & mCondition
    '    Set Rs = dbcn.Execute(SqlStr)
    '    If Not IsNull(Rs.Fields(0)) Then
    '        AutoGenRowNo = Rs.Fields(0) + 1
    '    Else
    '        AutoGenRowNo = 1
    '    End If
    'Exit Function
    'Err1:
    'MsgInformation err.Description
    'End Function
    '
    Sub CenterForm(ByRef FrontObject As Object, ByRef BackObject As Object)
        FrontObject.Left = (BackObject.Width - FrontObject.Width) / 2
        FrontObject.Top = (BackObject.Height - FrontObject.Height) / 2
    End Sub
    Public Shared Function RupeesConversion(ByRef num As Double) As String

        'Constants are Defined
        Dim digit(100) As String
        digit(0) = ""
        digit(1) = "One "
        digit(2) = "Two "
        digit(3) = "Three "
        digit(4) = "Four "
        digit(5) = "Five "
        digit(6) = "Six "
        digit(7) = "Seven "
        digit(8) = "Eight "
        digit(9) = "Nine "
        digit(10) = "Ten "
        digit(11) = "Eleven "
        digit(12) = "Twelve "
        digit(13) = "Thirteen "
        digit(14) = "Fourteen "
        digit(15) = "Fifteen "
        digit(16) = "Sixteen "
        digit(17) = "Seventeen "
        digit(18) = "Eighteen "
        digit(19) = "Ninteen "
        digit(20) = "Twenty "
        digit(30) = "Thirty "
        digit(40) = "Fourty "
        digit(50) = "Fifty "
        digit(60) = "Sixty "
        digit(70) = "Seventy "
        digit(80) = "Eighty "
        digit(90) = "Ninty "
        digit(100) = "Hundred "
        Dim tt(5) As String
        tt(2) = "Thousand "
        tt(3) = "Lakh "
        tt(4) = "Crore "
        tt(5) = "Hundred Crore "
        'Separating the Whole Number and Digits
        Dim nn As String
        Dim dd As String = ""
        nn = Math.Round(Val(num), 2)
        If InStr(nn, ".") <> 0 Then
            dd = Mid(nn, InStr(nn, ".") + 1)
            nn = Mid(nn, 1, InStr(nn, ".") - 1)
        End If

        'Variable nn stores the whole number and dd stores the digits
        'Finding the Word for numbers

        Dim x As Integer
        Dim y As Integer = 0
        x = nn.Length - 1
        Dim z As String
        Dim str As String = ""
        Dim str1 As String = ""
        If x > 1 Then
            While (x > -1)
                'First Loop Last two digits of Number is evaluated(ones and Tens)
                If y = 0 Then
                    z = Mid(nn, x, 2)
                    If Val(z) < 21 And Val(z) > 0 Then
                        str = digit(Val(z))
                    ElseIf Val(z) > 0 Then
                        str = digit(Val(z(0)) * 10)
                        str = str & digit(Val(z(1)))
                    End If
                    x = x - 1
                End If


                'Second Loop 3rd digits of Number is evaluated(Hundred)

                If y = 1 Then
                    z = Mid(nn, x, 1)
                    If Val(z) <> 0 Then
                        str = digit(Val(z)) & "Hundred " & str
                    End If
                    x = x - 2
                End If

                'Subsequent Loop Next two digits sequence of Number is evaluated(Thousands,Lakhs,Crore,etc)


                If y > 1 Then
                    If x <> 0 Then
                        z = Mid(nn, x, 2)
                        If Val(z) < 21 And Val(z) > 0 Then
                            str = digit(Val(z)) & tt(y) & str
                        ElseIf Val(z) > 0 Then
                            str1 = digit(Val(z(0)) * 10)
                            str = str1 & digit(Val(z(1))) & tt(y) & str
                        End If
                        x = x - 2
                    Else
                        z = Mid(nn, 1, 1)
                        If Val(z) < 21 And Val(z) > 0 Then
                            str = digit(Val(z)) & tt(y) & str
                        ElseIf Val(z) > 0 Then
                            str1 = digit(Val(z(0)) * 10)
                            str = str1 & digit(Val(z(1))) & tt(y) & str
                        End If
                        x = -1
                    End If
                End If
                y = y + 1
            End While
        Else
            If Val(nn) < 21 And Val(nn) > 0 Then
                str = digit(Val(nn))
            ElseIf Val(nn) > 0 Then
                str = digit(Val(nn(0)) * 10)
                str = str & digit(Val(nn(1)))
            End If

            'str = digit(nn)

        End If
        If str = "" Then
            str = "Zero "
        End If
        str = str & "Rupees "

        'Digits are evaluated(Paise)

        If Val(dd) > 0 Then
            If dd.Length = 1 Then
                z = Val(dd) * 10
            Else
                z = dd
            End If

            If Val(z) < 21 And Val(z) > 0 Then
                str = str & "and " & digit(Val(z)) & "Paise"
            ElseIf Val(z) > 0 Then
                str1 = digit(Val(z(0)) * 10)
                str = str & "and " & str1 & digit(Val(z(1))) & "Paise"
            End If
        End If

        'Word string is returned

        RupeesConversion = str

        '        Dim paise As Object
        '        Dim newno As Object
        '        Dim newn As Object
        '        Dim secNumb As Object
        '        Dim FstNumb As Object
        '        Dim sec As Object
        '        Dim fst As Object
        '        Dim paiseand As Object
        '        Dim deci As Object
        '        Dim a(20) As String
        '        Dim b(10) As String
        '        Dim c(10) As String
        '        Dim D As Short
        '        Dim e As Short
        '        Dim DeciFlag As String
        '        Dim Numb As Double
        '        Item = CDbl(VB6.Format(Str(Item), "#,##,##,##,##0.00"))

        'RupeesConversion = ""
        '        a(0) = " "
        '        a(1) = " One"
        '        a(2) = " Two"
        '        a(3) = " Three"
        '        a(4) = " Four"
        '        a(5) = " Five"
        '        a(6) = " Six"
        '        a(7) = " Seven"
        '        a(8) = " Eight"
        '        a(9) = " Nine"
        '        a(10) = " Ten"
        '        a(11) = " Eleven"
        '        a(12) = " Twelve"
        '        a(13) = " Thirteen"
        '        a(14) = " Fourteen"
        '        a(15) = " Fifteen"
        '        a(16) = " Sixteen"
        '        a(17) = " Seventeen"
        '        a(18) = " Eighteen"
        '        a(19) = " Nineteen"

        '        b(2) = " Twenty"
        '        b(3) = " Thirty"
        '        b(4) = " Forty"
        '        b(5) = " Fifty"
        '        b(6) = " Sixty"
        '        b(7) = " Seventy"
        '        b(8) = " Eighty"
        '        b(9) = " Ninety"

        '        c(1) = " Hundred"
        '        c(2) = " Thousand"
        '        c(3) = " Lac"
        '        c(4) = " Crore"
        '        c(5) = " Hundred Crore"

        '        RupeesConversion = " " ''"Rupees  "
        '        DeciFlag = "I"
        '        Numb = Fix(Item)
        '        e = TabPrint(Item)
        '        deci = Val(Right(Str(Item * 100), 2))
        '        If deci <> 0 Then
        '            'paiseand = " and"
        '            paiseand = " paise"
        '        Else
        '            paiseand = ""
        '        End If
        'Label:
        '        If Numb < 1 Then
        '            RupeesConversion = RupeesConversion & ""
        '        Else
        '            Do While Numb > 0
        '                D = TabPrint(Numb)
        '                Select Case D
        '                    Case Is = 1
        '                        RupeesConversion = RupeesConversion & a(Numb)
        '                        Numb = 0
        '                    Case Is = 2
        '                        If Numb >= 10 And Numb <= 19 Then
        '                            RupeesConversion = RupeesConversion & a(Numb)
        '                            Numb = 0
        '                        ElseIf Numb > 19 And Numb <= 99 Then

        '                            fst = Mid(Str(Numb), 2, 1)

        '                            sec = Mid(Str(Numb), 3, 1)


        '                            FstNumb = Val(fst)


        '                            secNumb = Val(sec)

        '                            RupeesConversion = RupeesConversion & b(FstNumb)
        '                        End If
        '                        Numb = Numb - (Fix(Numb / 10) * 10)
        '                    Case Is = 3

        '                        fst = Mid(Str(Numb), 2, 1)


        '                        FstNumb = Val(fst)

        '                        RupeesConversion = RupeesConversion & a(FstNumb) & c(1) '+ paiseand
        '                        Numb = Numb - (Fix(Numb / 100) * 100)
        '                    Case Is = 4

        '                        fst = Mid(Str(Numb), 2, 1)


        '                        FstNumb = Val(fst)

        '                        RupeesConversion = RupeesConversion & a(FstNumb) & c(2)
        '                        Numb = Numb - (Fix(Numb / 1000) * 1000)
        '                    Case Is = 5

        '                        fst = Mid(Str(Numb), 2, 1)

        '                        sec = Mid(Str(Numb), 3, 1)


        '                        FstNumb = Val(fst)


        '                        secNumb = Val(sec)

        '                        If FstNumb = 1 Then


        '                            newn = fst + sec
        '                            newno = Val(newn)
        '                            RupeesConversion = RupeesConversion & a(newn) & c(2)
        '                        Else


        '                            RupeesConversion = RupeesConversion & b(FstNumb) & a(secNumb) & c(2)
        '                        End If
        '                        Numb = Numb - (Fix(Numb / 10000) * 10000)
        '                        If Len(Trim(Str(Numb))) >= 4 Then
        '                            Numb = Val(Mid(Str(Numb), 3, 3))
        '                        End If

        '                    Case Is = 6

        '                        fst = Mid(Str(Numb), 2, 1)


        '                        FstNumb = Val(fst)

        '                        RupeesConversion = RupeesConversion & a(FstNumb) & c(3)
        '                        Numb = Numb - (Fix(Numb / 100000) * 100000)
        '                    Case Is = 7

        '                        fst = Mid(Str(Numb), 2, 1)

        '                        sec = Mid(Str(Numb), 3, 1)


        '                        FstNumb = Val(fst)


        '                        secNumb = Val(sec)

        '                        If FstNumb = 1 Then


        '                            newn = fst + sec
        '                            newno = Val(newn)
        '                            RupeesConversion = RupeesConversion & a(newn) & c(3)
        '                        Else


        '                            RupeesConversion = RupeesConversion & b(FstNumb) & a(secNumb) & c(3)
        '                        End If
        '                        'RupeesConversion = RupeesConversion + b(FstNumb) + a(Secnumb) + c(3)
        '                        Numb = Numb - (Fix(Numb / 1000000) * 1000000)
        '                        If Len(Trim(Str(Numb))) >= 6 Then
        '                            Numb = Val(Mid(Str(Numb), 3, 5))
        '                        End If
        '                    Case Is = 8

        '                        fst = Mid(Str(Numb), 2, 1)


        '                        FstNumb = Val(fst)

        '                        RupeesConversion = RupeesConversion & a(FstNumb) & c(4)
        '                        Numb = Numb - (Fix(Numb / 10000000) * 10000000)
        '                    Case Is = 9

        '                        fst = Mid(Str(Numb), 2, 1)

        '                        sec = Mid(Str(Numb), 3, 1)


        '                        FstNumb = Val(fst)


        '                        secNumb = Val(sec)

        '                        If FstNumb = 1 Then


        '                            newn = fst + sec
        '                            newno = Val(newn)
        '                            RupeesConversion = RupeesConversion & a(newn) & c(4)
        '                        Else


        '                            RupeesConversion = RupeesConversion & b(FstNumb) & a(secNumb) & c(4)
        '                        End If
        '                        'RupeesConversion = RupeesConversion + b(FstNumb) + a(Secnumb) + c(4)
        '                        Numb = Numb - (Fix(Numb / 100000000) * 100000000)
        '                        If Len(Trim(Str(Numb))) >= 8 Then
        '                            Numb = Val(Mid(Str(Numb), 3, 7))
        '                        End If
        '                    Case Is = 10

        '                        fst = Mid(Str(Numb), 2, 1)


        '                        FstNumb = Val(fst)

        '                        RupeesConversion = RupeesConversion & a(FstNumb) & c(5)
        '                        Numb = Numb - (Fix(Numb / 1000000000) * 1000000000)
        '                End Select
        '            Loop
        '            If DeciFlag <> "D" Then
        '                Numb = deci
        '                If Numb = 0 Then
        '                    paise = " Only"
        '                Else
        '                    RupeesConversion = RupeesConversion & " and paise "
        '                    'paise = " Paise Only"
        '                    paise = "  Only"
        '                End If
        '                DeciFlag = "D"
        '                GoTo Label
        '            End If
        '        End If
        '        RupeesConversion = RupeesConversion + paise
    End Function

    Public Shared Function RupeesIntoForigenCurr(ByRef Item As Double, ByRef pMajorCurr As String, ByRef pMinorCurr As String) As String
        Dim paise As Object
        Dim newno As Object
        Dim newn As Object
        Dim secNumb As Object
        Dim FstNumb As Object
        Dim sec As Object
        Dim fst As Object
        Dim paiseand As Object
        Dim deci As Object
        Dim a(20) As String
        Dim b(10) As String
        Dim c(10) As String
        Dim D As Short
        Dim e As Short
        Dim DeciFlag As String
        Dim Numb As Double
        Item = CDbl(VB6.Format(Str(Item), "#,##,##,##,##0.00"))
        RupeesIntoForigenCurr = ""
        a(0) = " "
        a(1) = " One"
        a(2) = " Two"
        a(3) = " Three"
        a(4) = " Four"
        a(5) = " Five"
        a(6) = " Six"
        a(7) = " Seven"
        a(8) = " Eight"
        a(9) = " Nine"
        a(10) = " Ten"
        a(11) = " Eleven"
        a(12) = " Twelve"
        a(13) = " Thirteen"
        a(14) = " Fourteen"
        a(15) = " Fifteen"
        a(16) = " Sixteen"
        a(17) = " Seventeen"
        a(18) = " Eighteen"
        a(19) = " Nineteen"

        b(2) = " Twenty"
        b(3) = " Thirty"
        b(4) = " Forty"
        b(5) = " Fifty"
        b(6) = " Sixty"
        b(7) = " Seventy"
        b(8) = " Eighty"
        b(9) = " Ninety"

        c(1) = " Hundred"
        c(2) = " Thousand"
        c(3) = " Lac"
        c(4) = " Crore"
        c(5) = " Hundred Crore"

        RupeesIntoForigenCurr = " " ''"Rupees  "
        DeciFlag = "I"
        Numb = Fix(Item)
        e = TabPrint(Item)
        deci = Val(Right(Str(Item * 100), 2))
        If deci <> 0 Then
            'paiseand = " and"
            paiseand = " " & pMinorCurr
        Else
            paiseand = ""
        End If
Label:
        If Numb < 1 Then
            RupeesIntoForigenCurr = RupeesIntoForigenCurr & ""
        Else
            Do While Numb > 0
                D = TabPrint(Numb)
                Select Case D
                    Case Is = 1
                        RupeesIntoForigenCurr = RupeesIntoForigenCurr & a(Numb)
                        Numb = 0
                    Case Is = 2
                        If Numb >= 10 And Numb <= 19 Then
                            RupeesIntoForigenCurr = RupeesIntoForigenCurr & a(Numb)
                            Numb = 0
                        ElseIf Numb > 19 And Numb <= 99 Then

                            fst = Mid(Str(Numb), 2, 1)

                            sec = Mid(Str(Numb), 3, 1)


                            FstNumb = Val(fst)


                            secNumb = Val(sec)

                            RupeesIntoForigenCurr = RupeesIntoForigenCurr & b(FstNumb)
                        End If
                        Numb = Numb - (Fix(Numb / 10) * 10)
                    Case Is = 3

                        fst = Mid(Str(Numb), 2, 1)


                        FstNumb = Val(fst)

                        RupeesIntoForigenCurr = RupeesIntoForigenCurr & a(FstNumb) & c(1) '+ paiseand
                        Numb = Numb - (Fix(Numb / 100) * 100)
                    Case Is = 4

                        fst = Mid(Str(Numb), 2, 1)


                        FstNumb = Val(fst)

                        RupeesIntoForigenCurr = RupeesIntoForigenCurr & a(FstNumb) & c(2)
                        Numb = Numb - (Fix(Numb / 1000) * 1000)
                    Case Is = 5

                        fst = Mid(Str(Numb), 2, 1)

                        sec = Mid(Str(Numb), 3, 1)


                        FstNumb = Val(fst)


                        secNumb = Val(sec)

                        If FstNumb = 1 Then


                            newn = fst + sec
                            newno = Val(newn)
                            RupeesIntoForigenCurr = RupeesIntoForigenCurr & a(newn) & c(2)
                        Else


                            RupeesIntoForigenCurr = RupeesIntoForigenCurr & b(FstNumb) & a(secNumb) & c(2)
                        End If
                        Numb = Numb - (Fix(Numb / 10000) * 10000)
                        If Len(Trim(Str(Numb))) >= 4 Then
                            Numb = Val(Mid(Str(Numb), 3, 3))
                        End If

                    Case Is = 6

                        fst = Mid(Str(Numb), 2, 1)


                        FstNumb = Val(fst)

                        RupeesIntoForigenCurr = RupeesIntoForigenCurr & a(FstNumb) & c(3)
                        Numb = Numb - (Fix(Numb / 100000) * 100000)
                    Case Is = 7

                        fst = Mid(Str(Numb), 2, 1)

                        sec = Mid(Str(Numb), 3, 1)


                        FstNumb = Val(fst)


                        secNumb = Val(sec)

                        If FstNumb = 1 Then


                            newn = fst + sec
                            newno = Val(newn)
                            RupeesIntoForigenCurr = RupeesIntoForigenCurr & a(newn) & c(3)
                        Else


                            RupeesIntoForigenCurr = RupeesIntoForigenCurr & b(FstNumb) & a(secNumb) & c(3)
                        End If
                        'RupeesIntoForigenCurr = RupeesIntoForigenCurr + b(FstNumb) + a(Secnumb) + c(3)
                        Numb = Numb - (Fix(Numb / 1000000) * 1000000)
                        If Len(Trim(Str(Numb))) >= 6 Then
                            Numb = Val(Mid(Str(Numb), 3, 5))
                        End If
                    Case Is = 8

                        fst = Mid(Str(Numb), 2, 1)


                        FstNumb = Val(fst)

                        RupeesIntoForigenCurr = RupeesIntoForigenCurr & a(FstNumb) & c(4)
                        Numb = Numb - (Fix(Numb / 10000000) * 10000000)
                    Case Is = 9

                        fst = Mid(Str(Numb), 2, 1)

                        sec = Mid(Str(Numb), 3, 1)


                        FstNumb = Val(fst)


                        secNumb = Val(sec)

                        If FstNumb = 1 Then


                            newn = fst + sec
                            newno = Val(newn)
                            RupeesIntoForigenCurr = RupeesIntoForigenCurr & a(newn) & c(4)
                        Else


                            RupeesIntoForigenCurr = RupeesIntoForigenCurr & b(FstNumb) & a(secNumb) & c(4)
                        End If
                        'RupeesIntoForigenCurr = RupeesIntoForigenCurr + b(FstNumb) + a(Secnumb) + c(4)
                        Numb = Numb - (Fix(Numb / 100000000) * 100000000)
                        If Len(Trim(Str(Numb))) >= 8 Then
                            Numb = Val(Mid(Str(Numb), 3, 7))
                        End If
                    Case Is = 10

                        fst = Mid(Str(Numb), 2, 1)


                        FstNumb = Val(fst)

                        RupeesIntoForigenCurr = RupeesIntoForigenCurr & a(FstNumb) & c(5)
                        Numb = Numb - (Fix(Numb / 1000000000) * 1000000000)
                End Select
            Loop
            If DeciFlag <> "D" Then
                Numb = deci
                If Numb = 0 Then
                    paise = " Only"
                Else
                    RupeesIntoForigenCurr = RupeesIntoForigenCurr & " and " & pMinorCurr
                    'paise = " Paise Only"
                    paise = "  Only"
                End If
                DeciFlag = "D"
                GoTo Label
            End If
        End If
        RupeesIntoForigenCurr = pMajorCurr & RupeesIntoForigenCurr + paise
    End Function
    Public Shared Function TabPrint(ByRef Item As Double) As Short
        If Item >= 0 And Item < 10 Then
            TabPrint = 1
        ElseIf Item >= 10 And Item < 100 Then
            TabPrint = 2
        ElseIf Item >= 100 And Item < 1000 Then
            TabPrint = 3
        ElseIf Item >= 1000 And Item < 10000 Then
            TabPrint = 4
        ElseIf Item >= 10000 And Item < 100000 Then
            TabPrint = 5
        ElseIf Item >= 100000 And Item < 1000000 Then
            TabPrint = 6
        ElseIf Item >= 1000000 And Item < 10000000 Then
            TabPrint = 7
        ElseIf Item >= 10000000 And Item < 100000000 Then
            TabPrint = 8
        ElseIf Item >= 100000000 And Item < 1000000000 Then
            TabPrint = 9
        Else
            TabPrint = 10
        End If
    End Function
    Public Shared Sub ReportWindow(ByRef Rept1 As AxCrystal.AxCrystalReport, Optional ByRef mTitle As String = "") '' CrystalReport
        Rept1.WindowShowRefreshBtn = True
        Rept1.WindowShowPrintBtn = True
        Rept1.WindowTitle = mTitle
        Rept1.ProgressDialog = True
        Rept1.WindowMaxButton = True
        Rept1.WindowMinButton = True
        Rept1.WindowShowGroupTree = True
        Rept1.WindowShowNavigationCtls = True
        Rept1.WindowAllowDrillDown = True
        Rept1.WindowShowPrintSetupBtn = True
        Rept1.WindowShowProgressCtls = True
        Rept1.WindowShowSearchBtn = True
        Rept1.WindowShowZoomCtl = True
        Rept1.WindowState = Crystal.WindowStateConstants.crptMaximized
        Rept1.WindowBorderStyle = Crystal.WindowBorderStyleConstants.crptSizable
    End Sub
    'Public Shared Sub ReportWindow(ByRef Rept1 As CRAXDRT.Report, Optional ByRef mTitle As String = "")
    '    Rept1.WindowShowRefreshBtn = True
    '    Rept1.WindowShowPrintBtn = True
    '    Rept1.WindowTitle = mTitle
    '    Rept1.ProgressDialog = True
    '    Rept1.WindowMaxButton = True
    '    Rept1.WindowMinButton = True
    '    Rept1.WindowShowGroupTree = True
    '    Rept1.WindowShowNavigationCtls = True
    '    Rept1.WindowAllowDrillDown = True
    '    Rept1.WindowShowPrintSetupBtn = True
    '    Rept1.WindowShowProgressCtls = True
    '    Rept1.WindowShowSearchBtn = True
    '    Rept1.WindowShowZoomCtl = True
    '    'Rept1.WindowState = Crystal.WindowStateConstants.crptMaximized
    '    'Rept1.WindowBorderStyle = Crystal.WindowBorderStyleConstants.crptSizable
    'End Sub
    Public Shared Function ClearCRptFormulas(ByRef Rept As AxCrystal.AxCrystalReport) As Boolean 'CrystalReport
        On Error GoTo ERR1
        Static I As Integer
        I = 0
        Do Until Trim(Rept.get_Formulas(I)) = ""
            Rept.set_Formulas(I, "")
            I = I + 1
        Loop
        Exit Function
ERR1:
    End Function
    '    Public Shared Function ClearCRptFormulas(ByRef Rept As CRAXDRT.Report) As Boolean
    '        On Error GoTo ERR1
    '        Static I As Integer
    '        I = 0
    '        Do Until Trim(Rept.FormulaFields(I).Text) = ""  '' Do Until Trim(Rept.get_Formulas(I)) = ""
    '            Rept.FormulaFields(I).Text = ""  ''Rept.set_Formulas(I, "")
    '            I = I + 1
    '        Loop
    '        Exit Function
    'ERR1:
    '    End Function
    Public Shared Function ArrayScan(ByRef ArrayName As Object, ByRef SearchElmnt As Object) As Integer
        Dim ii As Integer
        For ii = 0 To UBound(ArrayName, 1) Step 1 'Len(ArrayName) Step 1
            If ArrayName(ii, 1) = SearchElmnt Then
                ArrayScan = ii
                Exit Function
            End If
        Next ii
        ArrayScan = -1 'not found
    End Function

    Public Shared Function ArrayLen(ByRef ArrayName As Object) As Integer
        'Dim II As Long
        'For II = 0 To UBound(ArrayName, 1) Step 1      'Len(ArrayName) Step 1
        '    If ArrayName(II, 1) = SearchElmnt Then
        '        ArrayLen = II
        '        Exit Function
        '    End If
        'Next II
        ArrayLen = UBound(ArrayName)
    End Function
    Public Shared Function AssignDataInDataGrid(ByRef mSqlStr As String) As Boolean
        On Error GoTo ERR1

        AssignDataInDataGrid = False
        frmSearchGrid.SprdView.DataSource = Nothing

        Dim ds As New DataTable()
        Using da As New OleDbDataAdapter(mSqlStr, PubDBCnDataGrid)
            da.Fill(ds)
            frmSearchGrid.SprdView.DataSource = ds
        End Using

        AssignDataInDataGrid = True

        Exit Function
ERR1:
        AssignDataInDataGrid = False
        ErrorMsg(Err.Description, Err.Number, vbCritical)

    End Function
    Public Shared Function AssignDataInSprd(ByRef mSql As String, ByRef mADOData As ADODC, ByRef mConnectString As String, Optional ByRef mRefresh As String = "") As Boolean
        On Error GoTo ERR1
        Dim x As String
        Dim RsTemp As ADODB.Recordset = Nothing

        AssignDataInSprd = False


        mADOData.CommandType = CommandTypeEnum.adCmdText     ''= adCmdUnknown
        mADOData.LockType = LockTypeEnum.adLockReadOnly      ''adLockBatchOptimistic '' adLockReadOnly
        mADOData.CursorLocation = CursorLocationEnum.adUseClient       ''adUseServer     ''adUseClient
        mADOData.RecordSource = mSql



        If mRefresh = "N" Then
            mADOData.ConnectionString = ""
        Else
            mADOData.ConnectionString = StrConnDataGrid '' StrConn '' StrConnGrid
            mADOData.Refresh()
            mADOData.ConnectionString = ""
        End If

        AssignDataInSprd = True
        '    AssignDataInDataSprd = True
        Exit Function
ERR1:
        AssignDataInSprd = False
        ErrorMsg(Err.Description, Err.Number, vbCritical)

    End Function

    Public Shared Function AssignDataInSprd8(ByRef mSql As String, ByRef MySpread As Object, ByRef mConnectString As String, Optional ByRef mRefresh As String = "") As Boolean
        On Error GoTo ERR1
        Dim x As String
        Dim RsTemp As ADODB.Recordset = Nothing

        AssignDataInSprd8 = False

        If mRefresh = "N" Then
            'MySpread.DataSource = Nothing
        Else
            MainClass.UOpenRecordSet(mSql, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            MySpread.DataSource = Nothing
            MySpread.DataSource = RsTemp.DataSource
        End If

        AssignDataInSprd8 = True

        Exit Function
ERR1:
        AssignDataInSprd8 = False
        ErrorMsg(Err.Description, Err.Number, vbCritical)

    End Function


    '    Public Shared Function ClearCrptStoredProcParams(ByRef Rept As AxCrystal.AxCrystalReport) As Boolean
    '        On Error GoTo ERR1
    '        Dim I As Integer
    '        For I = 0 To 100
    '            Rept.set_StoredProcParam(I, "")
    '        Next I
    '        Exit Function
    'ERR1:
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    '    End Function

    '    Public Shared Sub AssignGrid(ByRef Sql As String, ByRef Conn As String, ByRef MSHFGrid1 As AxMSHierarchicalFlexGridLib.AxMSHFlexGrid, ByRef mADOData As VB6.ADODC, Optional ByRef IsMasterGrid As Boolean = False)
    '        On Error GoTo LErr
    '        If IsMasterGrid Then
    '            mADOData.ConnectionString = StrConn
    '            mADOData.RecordSource = Sql
    '            With MSHFGrid1
    '                .set_ColWidth(0, , 0)
    '                .set_ColWidth(1, , 576 * 4)
    '            End With
    '        Else
    '            mADOData.ConnectionString = StrConn
    '            mADOData.RecordSource = Sql
    '            mADOData.Refresh()
    '            MSHFGrid1.Refresh()
    '        End If
    '        Exit Sub
    'LErr:
    '        MsgBox(Err.Description)
    '        'Resume
    '    End Sub
    Public Shared Sub ButtonStatus(ByRef MyForm As System.Windows.Forms.Form,
                                        ByRef XRIGHT As String, ByRef MyRS As Recordset,
                                        ByRef ADDMode As Boolean,
                                        ByRef MODIFYMode As Boolean,
                                        ByRef pCmdAdd As System.Windows.Forms.Button,
                                        ByRef pCmdModify As System.Windows.Forms.Button,
                                        ByRef pCmdClose As System.Windows.Forms.Button,
                                        ByRef pCmdSave As System.Windows.Forms.Button,
                                        ByRef pCmdDelete As System.Windows.Forms.Button,
                                        ByRef pCmdMovement As System.Windows.Forms.Button,
                                        ByRef pcmdSavePrint As System.Windows.Forms.Button,
                                        ByRef pcmdPrint As System.Windows.Forms.Button,
                                        ByRef pCmdPreview As System.Windows.Forms.Button,
                                        ByRef pcmdAuthorised As System.Windows.Forms.Button,
                                        ByRef pCmdView As System.Windows.Forms.Button,
                                        Optional ByRef NoNavigation As Boolean = False,
                                        Optional ByRef KeepEnabled As Boolean = False,
                                        Optional ByRef Authorised As Boolean = False)
        On Error GoTo ErrPart
        NoNavigation = Not NoNavigation
        'With MyForm

        pCmdSave.Enabled = False
        If ADDMode = True Then
            pCmdAdd.Text = ConCmdCancelCaption
            'pCmdAdd. = "Cancel Add Operation"
            pCmdClose.Enabled = False

            pCmdModify.Text = ConcmdmodifyCaption

            pCmdModify.Enabled = False

            pCmdDelete.Enabled = False
            If NoNavigation = True Then
                'pCmdMovement(0).Enabled = False
                '.CmdMovement(1).Enabled = False
                '.CmdMovement(2).Enabled = False
                '.CmdMovement(3).Enabled = False
            Else
                pcmdSavePrint.Enabled = False
                pcmdPrint.Enabled = False
                pCmdPreview.Enabled = False
            End If

            If Authorised = True Then
                pcmdAuthorised.Enabled = False
            End If
            pCmdView.Enabled = False
        ElseIf MODIFYMode = True Then

            pCmdModify.Text = ConCmdCancelCaption

            'pCmdModify.ToolTipText = "Cancel Modify Operation"
            pCmdClose.Enabled = False
            pCmdAdd.Text = ConCmdAddCaption
            pCmdAdd.Enabled = False

            pCmdDelete.Enabled = False
            If NoNavigation = True Then
                'pcmdMovement(0).Enabled = False
                'pcmdMovement(1).Enabled = False
                'pcmdMovement(2).Enabled = False
                'pcmdMovement(3).Enabled = False
            Else
                pcmdSavePrint.Enabled = False
                pcmdPrint.Enabled = False
                pCmdPreview.Enabled = False
            End If
            pCmdView.Enabled = False
            If Authorised = True Then
                pcmdAuthorised.Enabled = False
            End If

        ElseIf MyRS.EOF = True Then
            If NoNavigation = True Then
                'pCmdMovement(0).Enabled = False
                'pCmdMovement(1).Enabled = False
                'pCmdMovement(2).Enabled = False
                'pCmdMovement(3).Enabled = False
            Else
                pcmdSavePrint.Enabled = False
                pcmdPrint.Enabled = False
                pCmdPreview.Enabled = False
            End If
            If pCmdView.Text = ConCmdViewCaption Then
                pCmdAdd.Enabled = False
            Else
                pCmdAdd.Enabled = IIf(InStr(1, XRIGHT, "A") > 0, True, False)
                pCmdModify.Enabled = False
                pCmdDelete.Enabled = False
                pCmdClose.Enabled = True
                pCmdAdd.Text = ConCmdAddCaption
                pCmdModify.Text = ConcmdmodifyCaption
                'pCmdAdd.ToolTipText = "Add New Record"
                'pCmdModify.ToolTipText = "Modify Record"
                pCmdView.Enabled = True
            End If
            If Authorised = True Then
                pcmdAuthorised.Enabled = False
            End If
        ElseIf MyRS.EOF = False And pCmdView.Text = ConCmdViewCaption Then
            If NoNavigation = True Then
                'pCmdMovement(0).Enabled = IIf(KeepEnabled = True, True, False)
                'pCmdMovement(1).Enabled = IIf(KeepEnabled = True, True, False)
                'pCmdMovement(2).Enabled = IIf(KeepEnabled = True, True, False)
                'pCmdMovement(3).Enabled = IIf(KeepEnabled = True, True, False)
            End If
            pCmdAdd.Enabled = IIf(InStr(1, XRIGHT, "A") = 0, False, IIf(KeepEnabled = True, True, IIf(pCmdView.Text = ConCmdViewCaption, False, True)))
            pCmdModify.Enabled = IIf(InStr(1, XRIGHT, "A") = 0, False, IIf(KeepEnabled = True, True, IIf(pCmdView.Text = ConCmdViewCaption, False, True)))
            pCmdDelete.Enabled = IIf(InStr(1, XRIGHT, "A") = 0, False, IIf(KeepEnabled = True, True, IIf(pCmdView.Text = ConCmdViewCaption, False, True)))
            pCmdClose.Enabled = True
            pCmdAdd.Text = ConCmdAddCaption
            pCmdModify.Text = ConcmdmodifyCaption
            'pCmdAdd.ToolTipText = "Add New Record"
            'pCmdModify.ToolTipText = "Modify Record"
            pCmdView.Enabled = True
            If Authorised = True Then
                pcmdAuthorised.Enabled = IIf(InStr(1, XRIGHT, "S") = 0, False, IIf(KeepEnabled = True, True, IIf(pCmdView.Text = ConCmdViewCaption, False, True)))
            End If
        ElseIf MyRS.EOF = False Then
            If NoNavigation = True Then
                'pCmdMovement(0).Enabled = True
                'pCmdMovement(1).Enabled = True
                'pCmdMovement(2).Enabled = True
                'pCmdMovement(3).Enabled = True
            Else
                pcmdPrint.Enabled = True
                pCmdPreview.Enabled = True
            End If
            pCmdView.Enabled = True
            pCmdDelete.Enabled = IIf(InStr(1, XRIGHT, "D") > 0, True, False)
            pCmdModify.Enabled = IIf(InStr(1, XRIGHT, "M") > 0, True, False)
            pCmdAdd.Enabled = IIf(InStr(1, XRIGHT, "A") > 0, True, False)
            pCmdClose.Enabled = True
            pCmdAdd.Text = ConCmdAddCaption
            pCmdModify.Text = ConcmdmodifyCaption
            'pCmdAdd.ToolTipText = "Add New Record"
            'pCmdModify.ToolTipText = "Modify Record"
            If Authorised = True Then
                pcmdAuthorised.Enabled = IIf(InStr(1, XRIGHT, "S") > 0, True, False)
            End If
        End If
        'End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub


    Public Shared Sub GridButtonStatus(ByRef MyForm As System.Windows.Forms.Form, ByRef XRIGHT As String, ByRef mRows As Integer, ByRef ADDMode As Boolean, ByRef MODIFYMode As Boolean, Optional ByRef NoNavigation As Boolean = False, Optional ByRef KeepEnabled As Boolean = False)
        'NoNavigation = Not NoNavigation
        'With MyForm
        '    pcmdSave.Enabled = False
        '    If ADDMode = True Then
        '        .cmdAdd.text = ConCmdCancelCaption
        '        .cmdAdd.ToolTipText = "Cancel Add Operation"
        '        .cmdClose.Enabled = False

        '        .cmdModify.text = ConcmdmodifyCaption

        '        .cmdModify.Enabled = False

        '        .cmdDelete.Enabled = False
        '        If NoNavigation = True Then
        '            .CmdMovement(0).Enabled = False
        '            .CmdMovement(1).Enabled = False
        '            .CmdMovement(2).Enabled = False
        '            .CmdMovement(3).Enabled = False
        '        Else
        '            .cmdSavePrint.Enabled = False
        '            .cmdPrint.Enabled = False
        '            .CmdPreview.Enabled = False
        '        End If
        '        .CmdView.Enabled = False
        '    ElseIf MODIFYMode = True Then

        '        .cmdModify.text = ConCmdCancelCaption

        '        .cmdModify.ToolTipText = "Cancel Modify Operation"
        '        .cmdClose.Enabled = False
        '        .cmdAdd.text = ConCmdAddCaption
        '        .cmdAdd.Enabled = False

        '        .cmdDelete.Enabled = False
        '        If NoNavigation = True Then
        '            .CmdMovement(0).Enabled = False
        '            .CmdMovement(1).Enabled = False
        '            .CmdMovement(2).Enabled = False
        '            .CmdMovement(3).Enabled = False
        '        Else
        '            .cmdSavePrint.Enabled = False
        '            .cmdPrint.Enabled = False
        '            .CmdPreview.Enabled = False
        '        End If
        '        .CmdView.Enabled = False
        '    ElseIf mRows <= 1 Then
        '        If NoNavigation = True Then
        '            .CmdMovement(0).Enabled = False
        '            .CmdMovement(1).Enabled = False
        '            .CmdMovement(2).Enabled = False
        '            .CmdMovement(3).Enabled = False
        '        Else
        '            .cmdSavePrint.Enabled = False
        '            .cmdPrint.Enabled = False
        '            .CmdPreview.Enabled = False
        '        End If
        '        If .CmdView.text = ConCmdViewCaption Then
        '            .cmdAdd.Enabled = False
        '        Else
        '            .cmdAdd.Enabled = IIf(InStr(1, XRIGHT, "A") > 0, True, False)

        '            .cmdModify.Enabled = IIf(InStr(1, XRIGHT, "M") > 0, True, False)

        '            .cmdDelete.Enabled = False
        '            .cmdClose.Enabled = True
        '            .cmdAdd.text = ConCmdAddCaption

        '            .cmdModify.text = ConcmdmodifyCaption
        '            .cmdAdd.ToolTipText = "Add New Record"

        '            .cmdModify.ToolTipText = "Modify Record"
        '            .CmdView.Enabled = True
        '        End If
        '    ElseIf mRows > 1 And .CmdView.text = ConCmdViewCaption Then
        '        If NoNavigation = True Then
        '            .CmdMovement(0).Enabled = IIf(KeepEnabled = True, True, False)
        '            .CmdMovement(1).Enabled = IIf(KeepEnabled = True, True, False)
        '            .CmdMovement(2).Enabled = IIf(KeepEnabled = True, True, False)
        '            .CmdMovement(3).Enabled = IIf(KeepEnabled = True, True, False)
        '        End If
        '        .cmdAdd.Enabled = IIf(InStr(1, XRIGHT, "A") = 0, False, IIf(KeepEnabled = True, True, IIf(.CmdView.text = ConCmdViewCaption, False, True)))

        '        .cmdModify.Enabled = IIf(InStr(1, XRIGHT, "A") = 0, False, IIf(KeepEnabled = True, True, IIf(.CmdView.text = ConCmdViewCaption, False, True)))

        '        .cmdDelete.Enabled = IIf(InStr(1, XRIGHT, "A") = 0, False, IIf(KeepEnabled = True, True, IIf(.CmdView.text = ConCmdViewCaption, False, True)))
        '        .cmdClose.Enabled = True
        '        .cmdAdd.text = ConCmdAddCaption

        '        .cmdModify.text = ConcmdmodifyCaption
        '        .cmdAdd.ToolTipText = "Add New Record"

        '        .cmdModify.ToolTipText = "Modify Record"
        '        .CmdView.Enabled = True
        '    ElseIf mRows > 1 Then
        '        If NoNavigation = True Then
        '            .CmdMovement(0).Enabled = True
        '            .CmdMovement(1).Enabled = True
        '            .CmdMovement(2).Enabled = True
        '            .CmdMovement(3).Enabled = True
        '        Else
        '            .cmdPrint.Enabled = True
        '            .CmdPreview.Enabled = True
        '        End If
        '        .CmdView.Enabled = True

        '        .cmdDelete.Enabled = IIf(InStr(1, XRIGHT, "D") > 0, True, False)

        '        .cmdModify.Enabled = IIf(InStr(1, XRIGHT, "M") > 0, True, False)
        '        .cmdAdd.Enabled = IIf(InStr(1, XRIGHT, "A") > 0, True, False)
        '        .cmdClose.Enabled = True
        '        .cmdAdd.text = ConCmdAddCaption

        '        .cmdModify.text = ConcmdmodifyCaption
        '        .cmdAdd.ToolTipText = "Add New Record"

        '        .cmdModify.ToolTipText = "Modify Record"
        '    End If
        'End With
    End Sub
    Public Shared Sub DoFunctionKey(ByRef mFORM As System.Windows.Forms.Form, ByRef mkeyCode As Short)
        'If mkeyCode = System.Windows.Forms.Keys.F2 And mFORM.cmdAdd.Enabled = True Then mFORM.cmdAdd = True
        ''If mkeyCode = vbKeyF3 And mFORM.cmdModify.Enabled = True Then mFORM.cmdModify = True
        'If mkeyCode = System.Windows.Forms.Keys.F4 And mFORM.CmdSave.Enabled = True Then mFORM.CmdSave = True
        'If mkeyCode = System.Windows.Forms.Keys.F5 And mFORM.cmdSavePrint.Enabled = True Then mFORM.cmdSavePrint = True

        'If mkeyCode = System.Windows.Forms.Keys.F6 And mFORM.cmdDelete.Enabled = True Then mFORM.cmdDelete = True
        'If mkeyCode = System.Windows.Forms.Keys.F7 And mFORM.cmdPrint.Enabled = True Then mFORM.cmdPrint = True
        'If mkeyCode = System.Windows.Forms.Keys.F8 And mFORM.CmdPreview.Enabled = True Then mFORM.CmdPreview = True
        'If mkeyCode = System.Windows.Forms.Keys.F9 And mFORM.CmdView.Enabled = True Then mFORM.CmdView = True
        'If mkeyCode = System.Windows.Forms.Keys.F10 And mFORM.cmdClose.Enabled = True Then mFORM.cmdClose = True
    End Sub

    Public Shared Function SetNumericField(ByRef mKeyAscii As Short) As Short
        'mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
        If (mKeyAscii >= 48 And mKeyAscii <= 57) Or mKeyAscii = 8 Or mKeyAscii = 46 Or mKeyAscii = 45 Then
            SetNumericField = mKeyAscii
        Else
            SetNumericField = 0
        End If
    End Function
    Public Shared Function TitleCase(ByRef mKeyAscii As Short, ByRef TxtStr As String) As Short
        'Dim KeyAscii As Object
        'Static mI As Short
        'If mI = 1 Then
        '    mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
        '    mI = 0
        'ElseIf mI = 0 And mKeyAscii = vbBack Then
        '    mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
        '    mI = 0
        'End If
        'If Len(TxtStr) < 1 Then
        '    mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
        'End If
        'If mKeyAscii = System.Windows.Forms.Keys.Space Or mKeyAscii = System.Windows.Forms.Keys.Tab Then
        '    mI = 1
        'End If
        'TitleCase = mKeyAscii
    End Function
    Public Shared Function UpperCase(ByRef mKeyAscii As String, ByRef TxtStr As String, Optional ByRef SpeacialCharAllow As String = "", Optional ByRef SpaceAllow As String = "") As Short
        'Dim mI As Short


        If SpeacialCharAllow = "N" Then
            If (mKeyAscii >= 48 And mKeyAscii <= 57) Or (mKeyAscii >= 97 And mKeyAscii <= 122) Or (mKeyAscii >= 65 And mKeyAscii <= 90) Or mKeyAscii = 8 Or mKeyAscii = 45 Then
                mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
            Else
                If SpaceAllow = "Y" And mKeyAscii = 32 Then
                    mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
                Else
                    mKeyAscii = 0
                End If
            End If
        Else
            If SpaceAllow = "N" And mKeyAscii = 32 Then
                mKeyAscii = 0 'Asc(UCase(Chr(mKeyAscii)))
            End If
        End If
        mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
        UpperCase = mKeyAscii
    End Function

    Public Shared Function CheckDateKey(ByRef mKeyAscii As Short) As Short
        Dim strvalid As Object
        strvalid = "0123456789/-"
        If mKeyAscii > 26 Then
            If InStr(strvalid, Chr(mKeyAscii)) = 0 Then
                mKeyAscii = 0
            End If
        End If
        CheckDateKey = mKeyAscii
    End Function
    Public Shared Function SetMaxLength(ByRef mFieldName As String, ByRef mTable As String, ByRef mConn As Connection) As Integer
        Dim RS As Recordset
        Dim SqlStr As String = ""
        'Dim mDataType As Integer

        RS = Nothing

        SqlStr = "Select " & mFieldName & " From " & mTable & " WHERE 1=2"
        MainClass.UOpenRecordSet(SqlStr, mConn, CursorTypeEnum.adOpenStatic, RS, LockTypeEnum.adLockReadOnly)

        '    mDataType = Rs.Fields(0).OraIDataType
        Select Case RS.Fields(0).Type ''mDataType           ''
            Case 131 ''ORATYPE_NUMBER         ''
                SetMaxLength = RS.Fields(0).Precision ''.Precision     '' - 2
            Case 135 ''ORATYPE_DATE           ''
                SetMaxLength = 10 ''Rs.Fields(0).DefinedSize - 6
            Case Else
                SetMaxLength = RS.Fields(0).DefinedSize ''.DefinedSize           ''
        End Select
        RS.Close()
        RS = Nothing
    End Function

    Public Shared Sub FillSearchList(ByRef SqlStr As String, ByRef FLDName As String, ByRef PvtDBCn As Connection)
        '        On Error GoTo ERR1
        '        Dim RS As Recordset
        '        MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenStatic, RS, LockTypeEnum.adLockReadOnly)
        '        frmSearch.DefInstance.Lstempnames.Items.Clear()

        '        If RS.EOF = True Then Exit Sub
        '        Do While RS.EOF = False
        '            frmSearch.DefInstance.Lstempnames.Items.Add(RS.Fields(FLDName).Value)
        '            RS.MoveNext()
        '        Loop
        '        Exit Sub
        'ERR1:
        '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Public Shared Function SearchBySQL(ByRef SqlStr As String, ByRef FLDName As String) As Boolean
        '        Dim ReturnField As Object
        On Error GoTo ERR1

        On Error GoTo ERR1
        'Dim SqlStr As String = ""
        Dim mPOS As Long
        Dim mGroupPOS As Long
        Dim mGroupBy As Boolean = False
        Dim mStartWithBy As Long
        Dim pNewSqlStr As String
        Dim mASOn As Long
        Dim mCharField As Long
        Dim mUnion As Long


        'frmSearchGrid.lblFieldType.Text = mFieldType

        mGroupPOS = InStr(UCase(SqlStr), "GROUP BY")
        mStartWithBy = InStr(UCase(SqlStr), "START WITH") 'UNION
        mASOn = InStr(UCase(SqlStr), " AS ")
        mCharField = InStr(UCase(SqlStr), "TO_CHAR") + InStr(UCase(SqlStr), "TO_DATE")
        mUnion = InStr(UCase(SqlStr), "UNION") '

        If mGroupPOS > 0 Or mStartWithBy > 0 Or mASOn > 0 Or mCharField > 0 Or mUnion > 0 Then
            mGroupBy = True
            pNewSqlStr = ""
            MainClass.ClearGrid(frmSearchGrid.SprdView)
            MainClass.AssignDataInSprd8(SqlStr, frmSearchGrid.SprdView, StrConn, "Y")
            frmSearchGrid.lblGroupBy.Text = "True"
        Else
            mPOS = InStr(UCase(SqlStr), "ORDER BY")
            mPOS = IIf(mPOS = 0, Len(SqlStr), mPOS - 1)

            pNewSqlStr = Mid(SqlStr, 1, mPOS)
            frmSearchGrid.lblGroupBy.Text = "False"
        End If

        frmSearchGrid.lblQuery.Text = pNewSqlStr
        'frmSearchGrid.Text1.Text = LikeSearchString
        frmSearchGrid.ShowDialog()
        If AcName <> "" Then
            SearchBySQL = True
        Else
            SearchBySQL = False
        End If
        Exit Function

        '        MainClass.AssignDataInDataCbo(SqlStr, (frmSearch.DefInstance.AdataSearch), (frmSearch.DefInstance.ADATACboSearch), FLDName, StrConn)

        '        VB6.ZOrder(frmSearch.DefInstance.fraData, (0))
        '        'frmSearch.Text1.Text = LikeSearchString
        '        frmSearch.DefInstance.ShowDialog()
        '        If ReturnField <> "" Then
        '            'If MainClass.ValidateWithMasterTable(AcName, FldName, ReturnField, TableName, PubDBCn, MasterNo) = True Then
        '            ' AcName = MasterNo
        '            'End If
        '        End If
        '        If AcName <> "" Then
        '            SearchBySQL = True
        '        Else
        '            SearchBySQL = False
        '        End If
        '        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    'Public Shared Function AssignDataInDataCbo(mSql As String, mADOData As ADODC, mDataCBO As DataCombo, mListField As String, mConnectString As String) As Boolean
    '        On Error GoTo ERR1
    '        AssignDataInDataCbo = False
    '        mDataCBO.ConnectionString = mConnectString
    '        mDataCBO.CommandType = adCmdUnknown
    '        mDataCBO.LockType = adLockReadOnly
    '        mDataCBO.RecordSource = mSql
    '        mDataCBO.Refresh()
    '        mDataCBO.Refresh()
    '        mDataCBO.ListField = mListField
    '        AssignDataInDataCbo = True
    '        frmSearch.fraData.ZOrder(0)
    '        mADOData = Nothing

    '        Exit Function
    'ERR1:
    '        ErrorMsg(Err.Description, Err.Number, vbCritical)
    '        '    Resume
    'End Function
    Public Shared Function SearchMaster(ByRef LikeSearchString As String, ByRef TableName As String, ByRef FLDName As String, Optional ByRef AdditionalCondition As String = "", Optional ByRef ReturnField As String = "") As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        Dim mColNo As Integer

NextStep:


        SqlStr = "Select DISTINCT " & FLDName & " "

        SqlStr = SqlStr & vbCrLf & " FROM " & TableName & " " & vbCrLf _
            & " Where " & vbCrLf _
            & " " & FLDName & " Like '" & AllowSingleQuote(UCase(LikeSearchString)) & "%'"

        If AdditionalCondition <> "" Then SqlStr = SqlStr & vbCrLf & " AND " & AdditionalCondition

        'If TableName = "INV_ITEM_MST" Then
        '    SqlStr = SqlStr & vbCrLf & " ORDER BY 1 FETCH FIRST 100 ROWS ONLY"
        'End If

        '' 

        'SqlStr = SqlStr & vbCrLf & " Order By 1" ' & FLDName

        'ClearGrid((frmSearchGrid.SprdView1))
        MainClass.ClearGrid(frmSearchGrid.SprdView)

        'If AssignDataInSprd8(SqlStr, frmSearchGrid.SprdView, StrConn, "Y") = False Then GoTo ERR1

        frmSearchGrid.lblGroupBy.Text = "False"
        frmSearchGrid.lblQuery.Text = SqlStr
        frmSearchGrid.Text1.Text = LikeSearchString
        frmSearchGrid.lblStockShow.Text = "" ''IIf(UCase(FLDName) = "ITEM_CODE", "Y", "N")
        frmSearchGrid.lblItemCol.Text = CStr(mColNo)

        frmSearchGrid.ShowDialog()


        If ReturnField <> "" Then
            If MainClass.ValidateWithMasterTable(AcName, FLDName, ReturnField, TableName, PubDBCn, MasterNo) = True Then
                AcName = MasterNo
            End If
        End If
        If AcName <> "" Then
            SearchMaster = True
        Else
            SearchMaster = False
        End If

        Exit Function

        '        On Error GoTo ERR1
        '        Dim SqlStr As String = ""
        '        SqlStr = "Select DISTINCT " & FLDName & " from " & TableName & " Where " & FLDName & " Like '" & MainClass.AllowSingleQuote(UCase(LikeSearchString)) & "%'"
        '        If AdditionalCondition <> "" Then SqlStr = SqlStr & " AND " & AdditionalCondition
        '        SqlStr = SqlStr & " Order By " & FLDName
        '        '-------------- THESE TWO LINES FOR DATA SOURCE BASED SEARCH -----------------
        '        MainClass.AssignDataInDataCbo(SqlStr, (frmSearch.DefInstance.AdataSearch), (frmSearch.DefInstance.ADATACboSearch), FLDName, StrConn)
        '        VB6.ZOrder(frmSearch.DefInstance.fraData, (0))
        '        '-------------- THESE TWO LINES FOR FILL COMBO BASED METHOD ------------------
        '        '    MainClass.FillSearchList SqlStr, FldName, PubDBCn
        '        '    frmSearch.fraSimple.ZOrder (0)
        '        '----------------------------------------------------
        '        frmSearch.DefInstance.Text1.Text = LikeSearchString
        '        frmSearch.DefInstance.ShowDialog()
        '        If ReturnField <> "" Then
        '            If MainClass.ValidateWithMasterTable(AcName, FLDName, ReturnField, TableName, PubDBCn, MasterNo) = True Then
        '                AcName = MasterNo
        '            End If
        '        End If
        '        If AcName <> "" Then
        '            SearchMaster = True
        '        Else
        '            SearchMaster = False
        '        End If


        '        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function

    Public Shared Sub UserUnlock()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        SqlStr = "UPDATE USERS Set DUMKEY='' WHERE USERID='" & PubUserID & "'"
        PubDBCn.Execute(SqlStr)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Public Shared Function CheckUserLock(ByRef KeyFldStr As String) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As Recordset
        RS = Nothing
        SqlStr = "Select * FROM USERS WHERE DumKey='" & MainClass.AllowSingleQuote(KeyFldStr) & "' and UserId<> '" & PubUserID & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenStatic, RS)
        If RS.EOF = False Then
            MsgInformation("USER " & RS.Fields("UserId").Value & "ALREADY USING THE SAME")
            CheckUserLock = False
        Else
            SqlStr = "UPDATE USER SET DUMKEY='" & MainClass.AllowSingleQuote(KeyFldStr) & "'"
            PubDBCn.Execute(SqlStr)
            CheckUserLock = True
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Public Shared Function FormatRupees(ByVal prmAmount As Object) As String
        Dim CommasInserted As Object
        Dim LI_LeftDigits As Object
        Dim L_Sign As String = ""
        Dim L_prmAmount As String = ""
        '' prmAmount in Format$(prmAmount, "0.00") format

        '' 12/5/98
        'prmAmount = VB6.Format(prmAmount, "0.00")
        L_prmAmount = VB6.Format(prmAmount, "0.00")

        If Len(L_prmAmount) < 9 Then
            L_prmAmount = VB6.Format(L_prmAmount, "#,##0.00")
        ElseIf Len(L_prmAmount) >= 9 Then
            '' 27/1/99: negative numbers have -ve sign as an extra character...
            If prmAmount < 0 Then
                L_Sign = "-"
                L_prmAmount = Right(L_prmAmount, Len(L_prmAmount) - 1)
            Else
                L_Sign = ""
            End If
            L_prmAmount = Left(L_prmAmount, Len(L_prmAmount) - 6) & "," & Right(L_prmAmount, 6)
            LI_LeftDigits = Len(L_prmAmount) - 6
            CommasInserted = 1
            Do While LI_LeftDigits > 3
                L_prmAmount = Left(L_prmAmount, Len(L_prmAmount) - (CommasInserted * 2 + 6 + CommasInserted)) & "," & Right(L_prmAmount, CommasInserted * 2 + 6 + CommasInserted)
                LI_LeftDigits = Len(L_prmAmount) - (CommasInserted * 2 + 6 + CommasInserted)
                CommasInserted = CommasInserted + 1
            Loop
        End If
        FormatRupees = L_Sign & L_prmAmount
    End Function
    Public Shared Sub FillCombo(ByRef mCbo As System.Windows.Forms.ComboBox, ByRef mTableName As String, ByRef mFieldName As String, Optional ByRef InitialValue As String = "", Optional ByRef AdditionalCondition As String = "")
        On Error GoTo ERR1
        Dim RS As Recordset
        Dim SqlStr As String = ""

        RS = Nothing

        SqlStr = "Select distinct " & mFieldName & " from " & mTableName
        If AdditionalCondition <> "" Then SqlStr = SqlStr & " Where " & AdditionalCondition
        SqlStr = SqlStr & " ORDER BY " & mFieldName
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenStatic, RS, LockTypeEnum.adLockReadOnly)
        mCbo.Items.Clear()
        If InitialValue <> "" Then mCbo.Items.Add(InitialValue)
        If RS.EOF = False Then
            Do While RS.EOF = False
                mCbo.Items.Add(IIf(IsDBNull(RS.Fields(0).Value), "", RS.Fields(0).Value))
                RS.MoveNext()
            Loop
        Else
            mCbo.Items.Add("")
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Public Shared Function CheckDigit(ByRef pBarCode As String) As String
        On Error GoTo CheckDigitERR
        Dim mSum1 As Double
        Dim mSum2 As Double
        Dim mSum3 As Double
        Dim mNextMultiOf10 As Integer
        Dim ii As Integer
        mSum1 = 0
        mSum2 = 0
        mSum3 = 0

        For ii = 1 To Len(pBarCode) Step 2
            mSum1 = mSum1 + Val(Mid(pBarCode, ii, 1))
        Next
        mSum1 = mSum1 * 3

        For ii = Len(pBarCode) - 1 To 1 Step -2
            mSum2 = mSum2 + Val(Mid(pBarCode, ii, 1))
        Next
        mSum3 = mSum1 + mSum2

        '******* STEP 3
        mNextMultiOf10 = (Int(mSum3 / 10) + 1) * 10
        CheckDigit = Right(CStr(mNextMultiOf10 - mSum3), 1)
        Exit Function
CheckDigitERR:
        MsgBox(Err.Description)
        '    Resume
    End Function

    Public Shared Sub ScanBarCode(ByRef pBarCode As String, ByRef pRetItemCode As String, ByRef pRetBatchNo As String)
        On Error GoTo ScanERR
        pRetItemCode = Left(pBarCode, 14)
        pRetBatchNo = Mid(pBarCode, 15, 5)
        Exit Sub
ScanERR:
        MsgBox(Err.Description)
    End Sub
    Public Shared Function MakeFirstLot(ByRef pDBCn As Connection, ByRef pItemCode As Object) As String
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As Recordset

        RS = Nothing

        '    SqlStr = "Select BRANCH.BranchShortCode " _
        ''        & " FROM BRANCH,ITEM " _
        ''        & " WHERE BRANCH.BRANCHCODE=ITEM.DIVISIONCODE " _
        ''        & " And ITEM.ITEMCODE='" & pItemCode & "'"
        ' MARKED TO GET 1ST LOT NO. FROM BARCODE TABLE
        SqlStr = "SELECT MIN(LOTNO) AS BATCHNO" & " FROM BARCODE " & " WHERE ITEMCODE='" & pItemCode & "'"
        MainClass.UOpenRecordSet(SqlStr, pDBCn, CursorTypeEnum.adOpenStatic, RS, LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            'MakeFirstLot = IIf(IsdbNull(rs.FIELDS("BranchShortCode").Value), "", rs.FIELDS("BranchShortCode").Value) & "001"
            MakeFirstLot = IIf(IsDBNull(RS.Fields("BATCHNO").Value), "", RS.Fields("BATCHNO").Value)
        Else
            MakeFirstLot = ""
        End If
        Exit Function
ERR1:
        MakeFirstLot = ""
    End Function
    Public Shared Function BarCodeValidation(ByRef pDBCn As Connection, ByRef pBarCode As String, Optional ByRef pRetItemShortName As String = "", Optional ByRef pRetCost As Double = 0, Optional ByRef pRetMRP As Double = 0) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As Recordset
        RS = Nothing
        SqlStr = "Select * FROM BARCODE " & " WHERE ITEMCODE='" & Left(pBarCode, 14) & "' " & " AND LOTNO='" & Mid(pBarCode, 15, 5) & "'"
        MainClass.UOpenRecordSet(SqlStr, pDBCn, CursorTypeEnum.adOpenStatic, RS, LockTypeEnum.adLockReadOnly)
        If RS.EOF = True Then
            BarCodeValidation = False
            pRetItemShortName = ""
            pRetCost = 0
            pRetMRP = 0
        Else
            BarCodeValidation = True
            pRetItemShortName = IIf(IsDBNull(RS.Fields("ITEMSHORTNAME").Value), "", RS.Fields("ITEMSHORTNAME").Value)
            pRetCost = Val(IIf(IsDBNull(RS.Fields("COSTPRICE").Value), "", RS.Fields("COSTPRICE").Value))
            pRetMRP = Val(IIf(IsDBNull(RS.Fields("MRP").Value), "", RS.Fields("MRP").Value))
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        pRetItemShortName = ""
        pRetCost = 0
        pRetMRP = 0
        BarCodeValidation = False
    End Function
    Public Shared Sub SortGrid(ByRef SprdMain As Object, ByRef xCol1 As Integer, ByRef xCol2 As Integer, Optional ByRef mCol1Desc As Boolean = False, Optional ByRef mCol2Desc As Boolean = False)
        SprdMain.SortBy = SS_SORT_BY_ROW
        SprdMain.SortKey(1) = xCol1
        SprdMain.SortKey(2) = xCol2
        SprdMain.SortKeyOrder(1) = IIf(mCol1Desc = False, SS_SORT_ORDER_ASCENDING, SS_SORT_ORDER_DESCENDING)
        SprdMain.SortKeyOrder(2) = IIf(mCol2Desc = False, SS_SORT_ORDER_ASCENDING, SS_SORT_ORDER_DESCENDING)
        SprdMain.Col = 1
        SprdMain.col2 = SprdMain.MaxCols
        SprdMain.Row = 0
        SprdMain.Row2 = SprdMain.MaxRows
        SprdMain.Action = SS_ACTION_SORT
    End Sub

    Public Shared Function SumQty(ByRef SprdMain As Object, ByRef ColItemCode As Integer, ByRef mItemCode As String, ByRef ColLotNo As Integer, ByRef mBatchNo As String, ByRef ColQty As Integer, ByRef mQty As Double, ByRef I As Integer) As Double
        On Error GoTo ERR1
        Dim mItemCode2 As String
        Dim mBatchNo2 As String
        Dim mFOC2 As String

        With SprdMain
            I = I + 1
            .Row = I


            .Col = ColItemCode
            mItemCode2 = .Text

            .Col = ColLotNo
            mBatchNo2 = .Text


            Do While mItemCode = mItemCode2 And mBatchNo = mBatchNo2
                .Col = ColQty
                mQty = mQty + Val(.Text)

                I = I + 1
                .Row = I
                .Col = ColItemCode
                mItemCode2 = .Text
                .Col = ColLotNo
                mBatchNo2 = .Text
            Loop
        End With
        SumQty = mQty
        Exit Function
ERR1:
        SumQty = mQty
    End Function

    Public Shared Function SearchGridMaster(ByRef LikeSearchString As String, ByRef TableName As String, ByRef FLDName As String, Optional ByRef FLDName1 As String = "", Optional ByRef FLDName2 As String = "", Optional ByRef FLDName3 As String = "", Optional ByRef AdditionalCondition As String = "", Optional ByRef ReturnField As String = "", Optional ByRef ReturnValue As Object = Nothing, Optional ByRef FLDName4 As String = "") As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mIsItemCode As String
        Dim mColNo As Integer

        mIsItemCode = "N"

        If UCase(FLDName) = "ITEM_CODE" Then
            mIsItemCode = "Y"
            mColNo = 1
            GoTo NextStep
        End If

        If UCase(FLDName1) = "ITEM_CODE" Then
            mIsItemCode = "Y"
            mColNo = 2
            GoTo NextStep
        End If

        If UCase(FLDName2) = "ITEM_CODE" Then
            mIsItemCode = "Y"
            mColNo = 3
            GoTo NextStep
        End If

        If UCase(FLDName3) = "ITEM_CODE" Then
            mIsItemCode = "Y"
            mColNo = 4
            GoTo NextStep
        End If

        If UCase(FLDName4) = "ITEM_CODE" Then
            mIsItemCode = "Y"
            mColNo = 4
            GoTo NextStep
        End If

NextStep:


        SqlStr = "Select DISTINCT " & FLDName & " "

        If Trim(FLDName1) <> "" Then
            SqlStr = SqlStr & vbCrLf & ", " & FLDName1 & ""
        End If

        If Trim(FLDName2) <> "" Then
            SqlStr = SqlStr & vbCrLf & ", " & FLDName2 & ""
        End If

        If Trim(FLDName3) <> "" Then
            SqlStr = SqlStr & vbCrLf & ", " & FLDName3 & ""
        End If

        If Trim(FLDName4) <> "" Then
            SqlStr = SqlStr & vbCrLf & ", " & FLDName4 & ""
        End If

        SqlStr = SqlStr & vbCrLf & " FROM " & TableName & " " & vbCrLf _
            & " Where " & vbCrLf _
            & " " & FLDName & " Like '" & AllowSingleQuote(UCase(LikeSearchString)) & "%'"

        If AdditionalCondition <> "" Then SqlStr = SqlStr & vbCrLf & " AND " & AdditionalCondition

        'If TableName = "INV_ITEM_MST" Then
        '    SqlStr = SqlStr & vbCrLf & " ORDER BY 1 FETCH FIRST 100 ROWS ONLY"
        'End If

        '' 

        'SqlStr = SqlStr & vbCrLf & " Order By 1" ' & FLDName

        'ClearGrid((frmSearchGrid.SprdView1))
        MainClass.ClearGrid(frmSearchGrid.SprdView)

        'If AssignDataInSprd8(SqlStr, frmSearchGrid.SprdView, StrConn, "Y") = False Then GoTo ERR1

        frmSearchGrid.lblGroupBy.Text = "False"
        frmSearchGrid.lblQuery.Text = SqlStr
        frmSearchGrid.Text1.Text = LikeSearchString
        frmSearchGrid.lblStockShow.Text = mIsItemCode ''IIf(UCase(FLDName) = "ITEM_CODE", "Y", "N")
        frmSearchGrid.lblItemCol.Text = CStr(mColNo)

        frmSearchGrid.ShowDialog()


        If ReturnField <> "" Then
            If MainClass.ValidateWithMasterTable(AcName, FLDName, ReturnField, TableName, PubDBCn, MasterNo) = True Then
                ReturnValue = MasterNo
            End If
        End If
        If AcName <> "" Then
            SearchGridMaster = True
        Else
            SearchGridMaster = False
        End If

        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function



    Public Shared Function SearchGridMasterBySQL(ByRef LikeSearchString As String, ByRef mSqlStr As String) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        MainClass.ClearGrid(frmSearchOuts.SprdView)
        MainClass.AssignDataInSprd8(mSqlStr, frmSearchOuts.SprdView, StrConn, "Y")

        frmSearchOuts.lblQuery.Text = "" '' mSqlStr
        frmSearchOuts.Text1.Text = LikeSearchString
        frmSearchOuts.ShowDialog()
        If AcName <> "" Then
            SearchGridMasterBySQL = True
        Else
            SearchGridMasterBySQL = False
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Public Shared Sub FormOpened()
        '        On Error GoTo ErrPart
        '        Dim nForms As Integer
        '        nForms = Forms.Count

        '        If nForms >= 7 Then
        '            MsgBox("Too many Forms Opened In your System", MsgBoxStyle.Information)
        '        End If

        '        Exit Sub
        'ErrPart:
        '        MsgBox(Err.Description)
    End Sub

    Public Shared Sub DeleteSprdRowCTRLD(ByRef sprd As Object, ByRef DelRow As Integer, ByRef CheckCol As Integer, Optional ByRef DelStatus As Boolean = False)
        Dim Response As Object
        DelStatus = False
        With sprd
            .Row = DelRow
            .Col = CheckCol
            If DelRow = .MaxRows Or DelRow = 0 Then Exit Sub
            DelStatus = True
            Response = MsgQuestion("Are you sure To Delete this Row ? ")
            If Response = MsgBoxResult.Yes Then
                .Row = DelRow
                .Action = SS_ACTION_DELETE_ROW
                If .MaxRows > 1 Then .MaxRows = .MaxRows - 1
            End If
        End With
    End Sub

    Public Shared Function SearchIntoFullGrid(ByRef mSprd As Object, ByRef col2 As Integer, ByRef pSearchKey As String, ByRef pSearchRow As Integer, ByRef pSearchCol As Integer) As Boolean
        Dim mSearchStringLen As Integer
        Dim cntRow As Integer
        mSearchStringLen = Len(pSearchKey)
        Dim counter As Short
        Dim Colcounter As Short
        If mSearchStringLen > 0 Then
            With mSprd
                counter = pSearchRow
                Colcounter = pSearchCol
                For cntRow = counter To .MaxRows
                    .Row = cntRow
                    For cntCol = Colcounter To .Maxcols
                        .Col = cntCol
                        If InStr(1, UCase(.Text), UCase(pSearchKey), CompareMethod.Text) > 0 Then
                            'If UCase(Mid(.Text, 1, mSearchStringLen)) = UCase(pSearchKey) Then
                            .Position = SS_POSITION_UPPER_LEFT
                            .Action = SS_ACTION_ACTIVE_CELL
                            .Action = SS_ACTION_GOTO_CELL
                            pSearchRow = IIf(cntCol + 1 > mSprd.Maxcols, cntRow + 1, cntRow)      '' cntRow
                            pSearchCol = IIf(cntCol + 1 > mSprd.Maxcols, 1, cntCol + 1)
                            SearchIntoFullGrid = True
                            Exit Function
                        End If
                    Next
                    pSearchCol = 1
                    Colcounter = 1
                Next
                pSearchCol = 1
                Colcounter = 1
                pSearchRow = 1
                counter = 1
            End With
        End If
        SearchIntoFullGrid = False
    End Function

    Public Shared Sub SearchIntoGrid(ByRef mSprd As Object, ByRef col2 As Integer, ByRef pSearchKey As String, ByRef pSearchRow As Integer)
        Dim mSearchStringLen As Integer
        Dim cntRow As Integer
        mSearchStringLen = Len(pSearchKey)
        Dim counter As Short
        If mSearchStringLen > 0 Then
            With mSprd
                counter = pSearchRow
                For cntRow = counter To .MaxRows
                    .Row = cntRow
                    .Col = col2
                    If InStr(1, UCase(.Text), UCase(pSearchKey), CompareMethod.Text) > 0 Then
                        'If UCase(Mid(.Text, 1, mSearchStringLen)) = UCase(pSearchKey) Then
                        .Position = SS_POSITION_UPPER_LEFT
                        .Action = SS_ACTION_ACTIVE_CELL
                        .Action = SS_ACTION_GOTO_CELL
                        pSearchRow = cntRow
                        Exit Sub
                    End If
                Next
            End With
        End If
    End Sub
    Public Shared Function SearchGridMasterBySQL2(ByRef LikeSearchString As String, ByRef mSqlStr As String, Optional ByRef mIsItemCode As String = "", Optional ByRef mColNo As String = "", Optional ByRef mFieldType As String = "") As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mPOS As Long
        Dim mGroupPOS As Long
        Dim mGroupBy As Boolean = False
        Dim mStartWithBy As Long
        Dim pNewSqlStr As String
        Dim mASOn As Long
        Dim mCharField As Long
        Dim mUnion As Long

        mIsItemCode = IIf(mIsItemCode = "", "N", mIsItemCode)
        mColNo = IIf(mColNo = "", "0", mColNo)


        'If Trim(mColNo) <> "" Then
        frmSearchGrid.lblStockShow.Text = mIsItemCode
        frmSearchGrid.lblItemCol.Text = mColNo
        'End If

        frmSearchGrid.lblFieldType.Text = mFieldType

        mGroupPOS = InStr(UCase(mSqlStr), "GROUP BY")
        mStartWithBy = InStr(UCase(mSqlStr), "START WITH") 'UNION
        mASOn = InStr(UCase(mSqlStr), " AS ")
        mCharField = InStr(UCase(mSqlStr), "TO_CHAR") + InStr(UCase(mSqlStr), "TO_DATE")
        mUnion = InStr(UCase(mSqlStr), "UNION") '

        If mGroupPOS > 0 Or mStartWithBy > 0 Or mASOn > 0 Or mCharField > 0 Or mUnion > 0 Then
            mGroupBy = True
            pNewSqlStr = ""
            MainClass.ClearGrid(frmSearchGrid.SprdView)
            MainClass.AssignDataInSprd8(mSqlStr, frmSearchGrid.SprdView, StrConn, "Y") ''Sandeeep 23/01/2024
            frmSearchGrid.lblGroupBy.Text = "True"
        Else
            mPOS = InStr(UCase(mSqlStr), "ORDER BY")
            mPOS = IIf(mPOS = 0, Len(mSqlStr), mPOS - 1)

            pNewSqlStr = Mid(mSqlStr, 1, mPOS)
            ''Sandeep add 07-03-2023
            MainClass.ClearGrid(frmSearchGrid.SprdView)
            MainClass.AssignDataInSprd8(pNewSqlStr, frmSearchGrid.SprdView, StrConn, "Y")
            frmSearchGrid.lblGroupBy.Text = "False"
        End If


        frmSearchGrid.lblQuery.Text = pNewSqlStr
        frmSearchGrid.Text1.Text = LikeSearchString
        frmSearchGrid.lblStockShow.Text = mIsItemCode ''IIf(UCase(FLDName) = "ITEM_CODE", "Y", "N")
        frmSearchGrid.lblItemCol.Text = CStr(mColNo)

        frmSearchGrid.ShowDialog()
        If AcName <> "" Then
            SearchGridMasterBySQL2 = True
        Else
            SearchGridMasterBySQL2 = False
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Public Shared Function FillPrintDummyDataFromSprd(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer, ByRef mPvtDBCn As Connection) As Boolean
        '' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String = ""


        mPvtDBCn.Errors.Clear()

        mPvtDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        mPvtDBCn.Execute(SqlStr)

        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 1
            SetData = ""
            GetData = ""
            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                GridName.Col = FieldNum
                If FieldNum = prmStartGridCol Then
                    SetData = "FIELD" & FieldCnt
                    GetData = "'" & MainClass.AllowSingleQuote(Left(GridName.Text, 255)) & "'"
                Else
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(Left(GridName.Text, 255)) & "'"
                End If
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " " & GetData & ") "
            mPvtDBCn.Execute(SqlStr)
NextRec:
        Next

        mPvtDBCn.CommitTrans()
        FillPrintDummyDataFromSprd = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillPrintDummyDataFromSprd = False
        mPvtDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Shared Function FetchFromTempData(ByRef mSqlStr As String, ByRef mOrderBy As String) As String

        mSqlStr = " SELECT * " & vbCrLf _
            & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf _
            & " WHERE  " & vbCrLf _
            & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        If mOrderBy = "" Then
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY SUBROW"
        Else
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY " & mOrderBy
        End If

        FetchFromTempData = mSqlStr

    End Function


    Public Shared Function GetUserCanModify(ByRef pVNoDate As String) As Boolean
        On Error GoTo ErrPart
        Dim mEntryDate As String
        Dim SqlStr As String = ""
        Dim RsCFYNo As Recordset
        Dim mCurrFYYear As Integer

        RsCFYNo = Nothing
        SqlStr = "Select FYEAR FROM GEN_CMPYRDTL_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And START_DATE<=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND END_DATE>=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenStatic, RsCFYNo)
        If Not RsCFYNo.EOF Then
            mCurrFYYear = CInt(VB6.Format(CStr(RsCFYNo.Fields("FYEAR").Value), "0000"))
        End If

        GetUserCanModify = False

        If CDbl(PubUserLevel) = 1 Then
            GetUserCanModify = True
        ElseIf CDbl(PubUserLevel) = 2 Then
            '        If mCurrFYYear = RsCompany.Fields("FYEAR").Value Then
            '            GetUserCanModify = True
            '        End If
            mEntryDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 60, CDate(pVNoDate)))
            If DateDiff(Microsoft.VisualBasic.DateInterval.Day, PubCurrDate, CDate(mEntryDate)) >= 0 Then
                GetUserCanModify = True
            End If
        ElseIf CDbl(PubUserLevel) = 3 Then
            mEntryDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 45, CDate(pVNoDate)))
            If DateDiff(Microsoft.VisualBasic.DateInterval.Day, PubCurrDate, CDate(mEntryDate)) >= 0 Then
                GetUserCanModify = True
            End If
        ElseIf CDbl(PubUserLevel) = 4 Then
            mEntryDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(pVNoDate)))
            If DateDiff(Microsoft.VisualBasic.DateInterval.Day, PubCurrDate, CDate(mEntryDate)) >= 0 Then
                GetUserCanModify = True
            End If
            '    ElseIf PubUserLevel = 5 Then
            '        mEntryDate = DateAdd("d", 1, pVNoDate)
            '        If DateDiff("d", PubCurrDate, mEntryDate) >= 0 Then
            '            GetUserCanModify = True
            '        End If
        End If

        Exit Function
ErrPart:
        GetUserCanModify = False
    End Function
    Public Shared Function SetReportDocDetail(ByRef mMenu As String,
    DbCN As ADODB.Connection, ByRef pDOC_NO As String, ByRef pDATE_ORIG As String,
    ByRef pREV_NO As String, ByRef pDATE_REV As String) As Boolean

        On Error GoTo ErrSTRMenuRight
        Dim RS As ADODB.Recordset = Nothing      'ADODB.Recordset
        Dim SqlStr As String = ""


        SqlStr = " Select * From ATH_REPORT_NO_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " And MENUHEAD='" & UCase(mMenu) & "'"

        'MainClass.UOpenRecordSet(Sqlstr, DbCN, adOpenStatic, RS)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            pDOC_NO = IIf(IsDBNull(RS.Fields("DOC_NO").Value), "", RS.Fields("DOC_NO").Value)
            pDATE_ORIG = IIf(IsDBNull(RS.Fields("DATE_ORIG").Value), "", RS.Fields("DATE_ORIG").Value)
            pREV_NO = IIf(IsDBNull(RS.Fields("REV_NO").Value), "", RS.Fields("REV_NO").Value)
            pDATE_REV = IIf(IsDBNull(RS.Fields("DATE_REV").Value), "", RS.Fields("DATE_REV").Value)
        Else
            pDOC_NO = ""
            pDATE_ORIG = ""
            pREV_NO = ""
            pDATE_REV = ""
        End If

        SetReportDocDetail = True
        Exit Function
ErrSTRMenuRight:
        'Resume
        MsgBox(Err.Description)
        SetReportDocDetail = False
    End Function
    Public Shared Function AssignCRptFormulas(ByRef Rept As AxCrystal.AxCrystalReport, ByRef FormulaString As String) As Boolean '' CrystalReport
        On Error GoTo ERR1
        Dim I As Integer
        I = 0
        Do Until Trim(Rept.get_Formulas(I)) = ""
            I = I + 1
        Loop
        Rept.set_Formulas(I, FormulaString)
        AssignCRptFormulas = True
        Exit Function
ERR1:
    End Function
    Public Shared Function GetUserCanModifyMaster(ByRef pDate As String, ByRef pXRIGHT As String) As Boolean
        On Error GoTo ErrPart
        Dim mEntryDate As String

        GetUserCanModifyMaster = False

        If InStr(pXRIGHT, "S") > 0 Then
            GetUserCanModifyMaster = True
        Else
            '        mEntryDate = DateAdd("d", 7, pDate)
            If DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(pDate), PubCurrDate) <= 7 Then
                GetUserCanModifyMaster = True
            End If
        End If

        Exit Function
ErrPart:
        GetUserCanModifyMaster = False
    End Function
    Public Shared Function AutoGenVNo(ByRef SqlStr As String, ByRef DbCN As ADODB.Connection) As Integer

        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        RS = Nothing

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If Not IsDBNull(RS.Fields(0).Value) Then
            AutoGenVNo = Val(RS.Fields(0).Value) + 1
        Else
            AutoGenVNo = 1
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Sub GetPartyF4detailFromRGP(ByRef mRGPNo As Double, ByRef mOutwardF4No As String, ByRef mOutwardF4Date As String, ByRef mExpDate As String)
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        '    mCheckF4 = False
        mOutwardF4No = "0"
        mOutwardF4Date = ""
        mExpDate = ""

        mSqlStr = " SELECT OUTWARD_57F4NO,GATEPASS_DATE,EXP_RTN_DATE " & vbCrLf & " FROM INV_GATEPASS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO=" & mRGPNo & ""

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mOutwardF4No = IIf(IsDBNull(RsTemp.Fields("OUTWARD_57F4NO").Value), "0", RsTemp.Fields("OUTWARD_57F4NO").Value)
            mOutwardF4Date = VB6.Format(IIf(IsDBNull(RsTemp.Fields("GATEPASS_DATE").Value), "", RsTemp.Fields("GATEPASS_DATE").Value), "DD/MM/YYYY")
            mExpDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("EXP_RTN_DATE").Value), "", RsTemp.Fields("EXP_RTN_DATE").Value), "DD/MM/YYYY")
            '        If Val(mOutwardF4No) = 0 Then
            '            mCheckF4 = False
            '        Else
            '            mCheckF4 = True
            '        End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

End Class
