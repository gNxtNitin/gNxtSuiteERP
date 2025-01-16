Imports System.Windows.Forms
Imports System.Data.SqlClient   '' System.Data.OleDb
Imports System.Data.OleDb
Imports ADODB
Imports Microsoft.VisualBasic.CallType
Imports System.Reflection
Imports System.Data
'Imports System.Data.OracleClient
Imports Guifreaks.NavigationBar
Imports Microsoft.VisualBasic.Compatibility

'Imports System.IO
'Imports System.Diagnostics
'Imports System.Net
'Imports System.Security.Principal
'Imports System.Windows.Forms.UserControl
'Imports Microsoft.VisualBasic
'Imports System.Management

Public Class FormMain
   Dim MenuTree As New TreeView()
    'Dim _currentView As Form


    'Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
    '    ' Create a new instance of the child form.
    '    Dim ChildForm As New System.Windows.Forms.Form
    '    ' Make it a child of this MDI form before showing it.
    '    ChildForm.MdiParent = Me

    '    m_ChildFormNumber += 1
    '    ChildForm.Text = "Window " & m_ChildFormNumber

    '    ChildForm.Show()
    'End Sub

    'Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim OpenFileDialog As New OpenFileDialog
    '    OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    '    OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
    '    If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
    '        Dim FileName As String = OpenFileDialog.FileName
    '        ' TODO: Add code here to open the file.
    '    End If
    'End Sub

    'Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim SaveFileDialog As New SaveFileDialog
    '    SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    '    SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

    '    If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
    '        Dim FileName As String = SaveFileDialog.FileName
    '        ' TODO: Add code here to save the current contents of the form to a file.
    '    End If
    'End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
      'me.hide ''me.hide 
    End Sub

    'Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    'End Sub

    'Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    'End Sub

    'Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    'End Sub

    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub

    'Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Me.Mdi(MdiLayout.Cascade)
    'End Sub

    'Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Me.LayoutMdi(MdiLayout.TileVertical)
    'End Sub

    'Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Me.LayoutMdi(MdiLayout.TileHorizontal)
    'End Sub

    'Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Me.LayoutMdi(MdiLayout.ArrangeIcons)
    'End Sub

    'Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    ' Close all child forms of the parent.
    '    For Each ChildForm As Form In Me.MdiChildren
    '        ChildForm.Close()
    '    Next
    'End Sub

    Private m_ChildFormNumber As Integer

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseAllToolStripMenuItem.Click
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        Me.Dispose()
        Application.Exit() : End
    End Sub
    Private Sub FormMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        Application.Exit() : End
    End Sub
    Private Sub SetFormStatusBar()
        StatusPanel.BorderStyle = StatusBarPanelBorderStyle.Sunken
        StatusPanel.Text = "Application started. No action yet."
        StatusPanel.ToolTipText = "Last Activity"
        StatusPanel.AutoSize = StatusBarPanelAutoSize.Spring
        MainStatusBar.Panels.Add(StatusPanel)

        DatetimePanel.BorderStyle = StatusBarPanelBorderStyle.Raised
        DatetimePanel.ToolTipText = "DateTime: " + System.DateTime.Today.ToString()
        DatetimePanel.Text = System.DateTime.Today.ToLongDateString()
        DatetimePanel.AutoSize = StatusBarPanelAutoSize.None
        MainStatusBar.Panels.Add(DatetimePanel)

        MainStatusBar.ShowPanels = True
        Me.Controls.Add(MainStatusBar)


        lblCompanyName.Text = UCase(RsCompany.Fields("COMPANY_NAME").Value)
        lblCompanyName.BorderSides = ToolStripStatusLabelBorderSides.All
        lblCompanyName.BorderStyle = Border3DStyle.Sunken

        lblFYear.Text = RsCompany.Fields("FYEAR").Value & "-" & RsCompany.Fields("FYEAR").Value + 1
        lblFYear.BorderSides = ToolStripStatusLabelBorderSides.All
        lblFYear.BorderStyle = Border3DStyle.Sunken
        lblFYear.BackColor = Color.Lime


        Conn.Text = PubUserID   ''& "/" & DBConUID & "@" & DBConSERVICENAME

        Caps.Enabled = My.Computer.Keyboard.CapsLock
        num.Enabled = My.Computer.Keyboard.NumLock

        'sbrpnlScrollLock.Enabled = My.Computer.Keyboard.ScrollLock

        ins.Text = "INS"

        RunDate.Text = CStr(MainModule.RunDate)
        'FormMain.Time.Style = ComctlLib.PanelStyleConstants.sbrTime
        ''Master.StatusBar1.Panels(1).Width = 4500
        ''Master.StatusBar1.Panels(2).Width = 3500
    End Sub
    Private Sub FormMain_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        AddHandler MenuTree.DoubleClick, AddressOf TreeView1_DoubleClick

        Me.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height
        Me.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width

        PubMainFormHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - MenuStrip1.Height - -StatusStrip.Height
        PubMainFormWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - naviBar1.Width

        If pFormPic <> "" Then
            If UCase(Mid(pFormPic, Len(pFormPic) - 2, 3)) = UCase("ico") Then      ''If UCase(Right(pFormPic, 3)) = UCase("ico") Then
                Me.Icon = New System.Drawing.Icon(My.Application.Info.DirectoryPath & "\Picture\" & pFormPic)
            End If
        End If
        If pLOGOPath <> "" Then
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.BackgroundImage = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\" & pLOGOPath)
        End If

        'CurrModuleName = mAdminModule
        'LoadMenu(mAdminModuleID, AdminModule)
        Dim pModuleId As Long
        Dim pNewActiveBand As Guifreaks.NavigationBar.NaviBand

        Call FillModuleQuery(pModuleId)

        PubMIDFormLoad = True

        pNewActiveBand = naviBar1.Bands(0)
        naviBar1.ActiveBand = naviBar1.Bands(0)
        If PubColorTheme = 1 Then
            naviBar1.LayoutStyle = NaviLayoutStyle.Office2007Blue
        ElseIf PubColorTheme = 2 Then
            naviBar1.LayoutStyle = NaviLayoutStyle.Office2007Silver
        Else
            naviBar1.LayoutStyle = NaviLayoutStyle.Office2003Green
        End If
        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
        '    naviBar1.LayoutStyle = NaviLayoutStyle.Office2007Silver     'Office2007Blue
        'Else

        'End If
        naviBar1.Cursor = Cursors.Hand
        naviBar1.BackColor = Color.DodgerBlue    '' Color.AliceBlue
        naviBar1.ForeColor = Color.White    ''

        Me.BackColor = Color.AliceBlue
        StatusStrip.BackColor = Color.AliceBlue
        MenuStrip1.BackColor = Color.AliceBlue
        ''System.Drawing.ColorTranslator.FromOle(CInt(PubSpdShodowColor)) '' 
        ''pNewActiveBand=pNewActiveBand
        'If pNewActiveBand Is AdminModule Then
        'CurrModuleName = mAdminModule
        'pModuleId = mAdminModuleID
        'ElseIf pNewActiveBand Is AccountModule Then
        '    CurrModuleName = mAccountModule
        '    pModuleId = mAccountModuleID
        'ElseIf pNewActiveBand Is InventoryModule Then
        '    CurrModuleName = mInventoryModule
        '    pModuleId = mInventoryModuleID
        'ElseIf pNewActiveBand Is SaleModule Then
        '    CurrModuleName = mSaleModule
        '    pModuleId = mSaleModuleID
        'ElseIf pNewActiveBand Is MISModule Then
        '    CurrModuleName = mMISModule
        '    pModuleId = mMISModuleID
        'ElseIf pNewActiveBand Is PayrollModule Then
        '    CurrModuleName = mPayrollModule
        '    pModuleId = mPayrollModuleID
        'ElseIf pNewActiveBand Is ProductionModule Then
        '    CurrModuleName = mProductionModule
        '    pModuleId = mProductionModuleID
        'ElseIf pNewActiveBand Is QualityModule Then
        '    CurrModuleName = mQualityModule
        '    pModuleId = mQualityModuleID
        'ElseIf pNewActiveBand Is TDSModule Then
        '    CurrModuleName = mTDSModule
        '    pModuleId = mTDSModuleID
        'ElseIf pNewActiveBand Is CostingModule Then
        '    CurrModuleName = mCostingModule
        '    pModuleId = mCostingModuleID
        'End If

        'LoadMenu(pModuleId, pNewActiveBand)
        Call SetFormStatusBar()
        If pLOGOPath <> "" Then
            Me.Text = "gNxtSuite ERP : " & UCase(RsCompany.Fields("COMPANY_NAME").Value) & ", " & UCase(RsCompany.Fields("COMPANY_ADDR").Value) & ", " & UCase(RsCompany.Fields("COMPANY_CITY").Value) & " (" & RsCompany.Fields("FYEAR").Value & "-" & RsCompany.Fields("FYEAR").Value + 1 & ")"
        Else
            Me.Text = UCase(RsCompany.Fields("COMPANY_NAME").Value) & ", " & UCase(RsCompany.Fields("COMPANY_ADDR").Value) & ", " & UCase(RsCompany.Fields("COMPANY_CITY").Value) & " (" & RsCompany.Fields("FYEAR").Value & "-" & RsCompany.Fields("FYEAR").Value + 1 & ")"
        End If

    End Sub
    Private Sub FillModuleQuery(pModuleId As Long)
        Dim RsModule As Recordset
        Dim mSqlStr As String = ""
        Dim mCompanyCode As Long
        Dim I As Long

        Try
            RsModule = New Recordset
            mCompanyCode = -1

            If MainClass.ValidateWithMasterTable(RsCompany.Fields("COMPANY_NAME").Value, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True Then
                mCompanyCode = Val(MasterNo)
            End If

            If PubSuperUser = "S" Or PubSuperUser = "A" Then
                mSqlStr = "SELECT 'YES' AS IS_RIGHTS, MST.MODULEID, MST.MODULENAME, MST.MODULE_CAPTION, MODULE_MENU_NAME, MODULE_SHOW_SEQ,MODULE_CAPTION" & vbCrLf _
                        & " FROM GEN_Module_MST MST " & vbCrLf _
                        & " WHERE STATUS='O' AND IS_GROUP='Y' " & vbCrLf _
                        & " ORDER BY MODULE_SHOW_SEQ"
            Else
                mSqlStr = "SELECT UPPER(RIGHTS) AS IS_RIGHTS, MST.MODULEID, MST.MODULENAME, MST.MODULE_CAPTION, MODULE_MENU_NAME, MODULE_SHOW_SEQ,MODULE_CAPTION" & vbCrLf _
                        & " FROM GEN_MODULERIGHT_MST IH, GEN_Module_MST MST " & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
                        & " AND IH.USERID='" & PubUserID & "'" & vbCrLf _
                        & " AND IH.MODULEID=MST.MODULEID AND STATUS='O' AND IS_GROUP='Y' " & vbCrLf _
                        & " AND UPPER(RIGHTS)='YES'  " & vbCrLf _
                        & " ORDER BY MODULE_SHOW_SEQ"
            End If

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsModule, LockTypeEnum.adLockOptimistic)

            I = 0
            Dim NaviBand As New NaviBand()

            If RsModule.EOF = False Then
                Do While RsModule.EOF = False
                    NaviBand = New NaviBand

                    NaviBand.Text = IIf(IsDBNull(RsModule.Fields("MODULE_CAPTION").Value), "", RsModule.Fields("MODULE_CAPTION").Value)
                    NaviBand.Name = IIf(IsDBNull(RsModule.Fields("MODULE_MENU_NAME").Value), "", RsModule.Fields("MODULE_MENU_NAME").Value)
                    NaviBand.Tag = IIf(IsDBNull(RsModule.Fields("MODULEID").Value), "", RsModule.Fields("MODULEID").Value)
                    NaviBand.LargeImage = Nothing    '' SnapSoftERP.My.Resources.Resources.klipper
                    NaviBand.BackColor = Color.DodgerBlue
                    NaviBand.ForeColor = Color.White
                    'NaviBand.Font.Size = 9
                    naviBar1.Bands.Add(NaviBand)



                    If I = 0 Then
                        CurrModuleName = IIf(IsDBNull(RsModule.Fields("MODULENAME").Value), "", RsModule.Fields("MODULENAME").Value)
                        pModuleId = IIf(IsDBNull(RsModule.Fields("MODULEID").Value), "", RsModule.Fields("MODULEID").Value)
                    End If

                    RsModule.MoveNext()
                    If RsModule.EOF = False Then
                        I = I + 1
                    End If
                Loop
                naviBar1.VisibleLargeButtons = I + 1

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#Region "LoadMenu"
    Private Sub LoadMenu(ByVal mModuleID As Long, NewActiveBand As Guifreaks.NavigationBar.NaviBand)
      Dim RsModule As Recordset
      Dim mSqlStr As String = ""
      'Dim mModuleID As Long

      Try
         RsModule = New Recordset

         If PubSuperUser = "S" Or PubSuperUser = "A" Then
            Call FillTreeMenu(mModuleID, NewActiveBand)
         Else
                mSqlStr = "SELECT UPPER(RIGHTS) AS RIGHTS, MST.MODULEID, MST.MODULENAME, MST.MODULE_CAPTION" & vbCrLf _
                        & " FROM GEN_MODULERIGHT_MST IH, GEN_Module_MST MST " & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND IH.USERID='" & PubUserID & "'" & vbCrLf _
                        & " AND IH.MODULEID=" & mModuleID & "" & vbCrLf _
                        & " AND IH.MODULEID=MST.MODULEID AND STATUS='O' AND IS_GROUP='Y' " & vbCrLf _
                        & " AND UPPER(RIGHTS)='YES'  ORDER BY IH.MODULEID"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsModule, LockTypeEnum.adLockOptimistic)

            If RsModule.EOF = False Then

               'mModuleID = IIf(IsDBNull(RsModule.Fields("MODULEID").Value), "-1", RsModule.Fields("MODULEID").Value)
               Call FillTreeMenu(mModuleID, NewActiveBand)

            Else
               'MsgBox(" You have Not rights In  any Module Master." & vbCrLf _
               '& "Application Aborted.........................! ", vbExclamation)
               'Exit Sub
            End If
         End If
      Catch ex As Exception
         'Resume
         MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Try
   End Sub
#End Region
#Region "FillTreeMenu"
   Private Sub FillTreeMenu(pModuleID As Long, NewActiveBand As Guifreaks.NavigationBar.NaviBand)
        'Dim NodX As Node
        'Dim XRIGHT As String
        Dim SqlStr As String = ""
        Dim RsTemp As Recordset
        'Dim MenuTree As New TreeNode



        NewActiveBand.ClientArea.Controls.Add(MenuTree)

        NewActiveBand.ClientArea.Dock = DockStyle.Fill
        NewActiveBand.ClientArea.Controls(0).Dock = DockStyle.Fill


        'End If
        MenuTree.Nodes.Clear()
        Try
            RsTemp = New Recordset

            If PubSuperUser = "S" Or PubSuperUser = "A" Then
                SqlStr = "SELECT 'AMDVSP' AS RIGHTS, MST.MENUHEADID, MST.MENUHEADNAME, MST.ISTOPMENU, HEADCOUNT, MST.TOPMENU_HEADID" & vbCrLf _
                      & " FROM GEN_ERPMENU_MST MST " & vbCrLf _
                      & " WHERE MST.MODULEID=" & pModuleID & "" & vbCrLf _
                      & " AND IS_ACTIVE='Y' AND ISTOPMENU='Y'" & vbCrLf _
                      & " ORDER BY SUBROWNO, HEADCOUNT"
            Else
                SqlStr = "SELECT UPPER(RIGHTS) AS RIGHTS, MST.MENUHEADID, MST.MENUHEADNAME, MST.ISTOPMENU, HEADCOUNT, MST.TOPMENU_HEADID" & vbCrLf _
                      & " FROM FIN_RIGHTS_MST IH, GEN_ERPMENU_MST MST " & vbCrLf _
                      & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                      & " AND IH.USERID='" & PubUserID & "'" & vbCrLf _
                      & " AND IH.MODULEID=" & pModuleID & "" & vbCrLf _
                      & " AND IH.MODULEID = MST.MODULEID AND IH.MENUHEAD = MST.MENUHEADID AND IS_ACTIVE='Y' AND ISTOPMENU='Y'" & vbCrLf _
                      & " ORDER BY SUBROWNO, HEADCOUNT"
            End If

            Dim dt As DataTable = Me.GetData(SqlStr)
            Me.PopulateTreeView(dt, "", Nothing, pModuleID, MenuTree)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
#Region "Populatedata"
    Private Sub PopulateTreeView(dtParent As DataTable, parentId As String, treeNode As TreeNode, pModuleID As Long, ByRef MenuTree As TreeView)
        Dim SqlStr As String = ""
        Dim RsTemp As Recordset
        Try
            For Each row As DataRow In dtParent.Rows
                Dim child As New TreeNode() With { _
                .Text = row("MENUHEADNAME").ToString(), _
                .Tag = row("MENUHEADID") _
                }

                MenuTree.ItemHeight = 25
                MenuTree.BorderStyle = BorderStyle.Fixed3D
                'MenuTree.ExpandAll()
                RsTemp = New Recordset

                If PubSuperUser = "S" Or PubSuperUser = "A" Then
                    SqlStr = "SELECT 'AMV' AS RIGHTS, MST.MENUHEADID, MST.MENUHEADNAME, MST.ISTOPMENU, HEADCOUNT, MST.TOPMENU_HEADID" & vbCrLf _
                            & " FROM GEN_ERPMENU_MST MST " & vbCrLf _
                            & " WHERE " & vbCrLf _
                            & " MST.MODULEID=" & pModuleID & " AND MST.TOPMENU_HEADID='" & child.Tag & "'" & vbCrLf _
                            & " AND IS_ACTIVE='Y' AND ISTOPMENU='N'" & vbCrLf _
                            & " ORDER BY SUBROWNO, HEADCOUNT"
                Else
                    SqlStr = "SELECT UPPER(RIGHTS) AS RIGHTS, MST.MENUHEADID, MST.MENUHEADNAME, MST.ISTOPMENU, HEADCOUNT, MST.TOPMENU_HEADID" & vbCrLf _
                            & " FROM FIN_RIGHTS_MST IH, GEN_ERPMENU_MST MST " & vbCrLf _
                            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND IH.USERID='" & PubUserID & "' AND IH.MODULEID=" & pModuleID & " AND MST.TOPMENU_HEADID='" & child.Tag & "'" & vbCrLf _
                            & " AND IH.MODULEID = MST.MODULEID AND IH.MENUHEAD = MST.MENUHEADID AND IS_ACTIVE='Y' AND ISTOPMENU='N'" & vbCrLf _
                            & " ORDER BY SUBROWNO, HEADCOUNT"
                End If


                If parentId = "" Then
                    MenuTree.Nodes.Add(child)

                    Dim dtChild As DataTable = Me.GetData(SqlStr)
                    PopulateTreeView(dtChild, child.Tag, child, pModuleID, MenuTree)        ''PopulateTreeView(dtChild, Convert.ToInt32(child.Tag), child)
                Else

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsTemp, LockTypeEnum.adLockOptimistic)

                    If RsTemp.EOF = False Then
                        treeNode.Nodes.Add(child)
                        Dim dtChild As DataTable = Me.GetData(SqlStr)
                        PopulateTreeView(dtChild, child.Tag, child, pModuleID, MenuTree)        ''PopulateTreeView(dtChild, Convert.ToInt32(child.Tag), child)
                    Else
                        treeNode.Nodes.Add(child)
                    End If

                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
#Region "GetData"
    Private Function GetData(query As String) As DataTable
        Dim dt As New DataTable()

        Using con As New OleDbConnection(StrConn)
            Using cmd As New OleDbCommand(query, con)
                Using sda As New OleDbDataAdapter()
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    sda.Fill(dt)
                End Using
            End Using
            Return dt
        End Using

    End Function
#End Region

#Region "TreeView1_DoubleClick"
   Private Sub TreeView1_DoubleClick(sender As Object, e As System.EventArgs)
      Try
            Dim pFormName As String = ""
            Dim pCondName1 As String = ""
            Dim pCondValue1 As String = ""
            Dim pCondName2 As String = ""
            Dim pCondValue2 As String = ""
            Dim pCondName3 As String = ""
            Dim pCondValue3 As String = ""
            Dim pCondName4 As String = ""
            Dim pCondValue4 As String = ""
            Dim pCondName5 As String = ""
            Dim pCondValue5 As String = ""
            Dim mm As New Form
            'Dim lblName1 As Label
            'Dim lblName2 As Label
            'Dim pSeparateGST As String
            'Dim MyControl As Object
            Dim pFormCaption As String = ""
            Dim n As Windows.Forms.TreeNode
            Dim FormCollection As New Collection()
            Dim pSeparateGST As String = "N"

         n = MenuTree.SelectedNode

         myMenu = n.Tag

         Call GetFormName(myMenu, pFormName, pCondName1, pCondValue1, pCondName2, pCondValue2, pCondName3, pCondValue3, pCondName4, pCondValue4, pCondName5, pCondValue5, pFormCaption)

         If pFormName = "" Then
            Exit Sub
         End If


            'Dim nextFormReference As String = ""
            'nextFormReference = String.Concat("[Assembly].", pFormName)
            'Dim nextFormType As Type = Type.GetType(nextFormReference)
            'mm = System.Activator.CreateInstance(nextFormType)

            'Dim app As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            ''Dim dlg As New Form
            'mm = app.CreateInstance(pFormName)

            'Dim strCreatedFromButton As String = "Form3"

            mm = CType(CreateObjectInstance(pFormName), Form)       '' DirectCast(CreateObjectInstance(pFormName), Form)
            'mm = CType(Activator.CreateInstance(pFormName), Form)

            'mm = CallByName(FormCollection, "Add", CallType.Method, pFormName)        ''CallByName(Forms, "Add", vbMethod, pFormName)      ''CallByName(Forms, "Add", vbMethod, pFormName)

            If myMenu = UCase("mnuPurchaseChallan") Then

                pSeparateGST = GetSeparateGSTRefund()

                pCondName1 = "lblSeprateGST"
                mm.Controls.Find(pCondName1, True)(0).Text = pSeparateGST    ''mm.Controls.Find(pCondName1, True)(0).Text = pCondValue1

                pCondName1 = "lblBookCode"
                mm.Controls.Find(pCondName1, True)(0).Text = ConPurchaseBookCode

                pCondName1 = "txtVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "txtVDate"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "lblPurchaseVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = False

                pCondName1 = "lblPurchaseType"
                mm.Controls.Find(pCondName1, True)(0).Text = "G"

                pCondName1 = "lblPurchaseSeqType"
                mm.Controls.Find(pCondName1, True)(0).Text = "9"

                pCondName1 = "lblVNo"
                mm.Controls.Find(pCondName1, True)(0).Text = "VNo :"

                'pCondName1 = "chkRejection"
                'mm.Controls.Find(pCondName1, True)(0).value = vbchecked

                pCondName1 = "chkRejection"
                mm.Controls.Find(pCondName1, True)(0).Enabled = False

                mm.Text = "Purchase Entry (GST Goods Order Under Challan)"

                '        myMenu = "mnuPurchaseAll"
                'pSeparateGST = GetSeparateGSTRefund()
                'mm.lblSeprateGST.text = pSeparateGST
                'mm.lblBookCode.text = ConPurchaseBookCode
                'mm.txtVNo.Visible = True
                'mm.txtVDate.Visible = True
                'mm.lblPurchaseVNo.Visible = False
                'mm.lblPurchaseType.text = "G"
                'mm.lblPurchaseSeqType.text = "9"
                'mm.lblVNo.text = "VNo :"
                'mm.chkRejection.Value = vbUnchecked
                'mm.chkRejection.Enabled = False
                'mm.text = "Purchase Entry (GST Goods Order Under Challan)"
            ElseIf myMenu = UCase("mnuPurchaseSRInv") Then
                pSeparateGST = GetSeparateGSTRefund()

                pCondName1 = "lblSeprateGST"
                mm.Controls.Find(pCondName1, True)(0).Text = pSeparateGST    ''mm.Controls.Find(pCondName1, True)(0).Text = pCondValue1

                pCondName1 = "lblBookCode"
                mm.Controls.Find(pCondName1, True)(0).Text = ConPurchaseBookCode

                pCondName1 = "txtVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "txtVDate"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "lblPurchaseVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = False

                pCondName1 = "lblPurchaseType"
                mm.Controls.Find(pCondName1, True)(0).Text = "G"

                pCondName1 = "lblPurchaseSeqType"
                mm.Controls.Find(pCondName1, True)(0).Text = "8"

                pCondName1 = "lblVNo"
                mm.Controls.Find(pCondName1, True)(0).Text = "VNo :"

                'pCondName1 = "chkRejection"
                'mm.Controls.Find(pCondName1, True)(0).Text = System.Windows.Forms.CheckState.Checked      ''.value = vbchecked

                pCondName1 = "chkRejection"
                mm.Controls.Find(pCondName1, True)(0).Enabled = False

                mm.Text = "Purchase Entry (Sale Return) Agt Invoice"

                'pSeparateGST = GetSeparateGSTRefund()
                'mm.lblSeprateGST.text = pSeparateGST
                'mm.lblBookCode.text = ConPurchaseBookCode
                'mm.txtVNo.Visible = True
                'mm.txtVDate.Visible = True
                'mm.lblPurchaseVNo.Visible = False
                'mm.lblPurchaseType.text = "G"
                'mm.lblPurchaseSeqType.text = "8"
                'mm.lblVNo.text = "VNo :"
                'mm.chkRejection.Value = vbChecked
                'mm.chkRejection.Enabled = False
                'mm.text = "Purchase Entry (Sale Return) Agt Invoice"
            ElseIf myMenu = UCase("mnuPurchase") Then
                'mm.lblBookCode.text = ConPurchaseBookCode

                'mm.txtVNo.Visible = True
                'mm.txtVDate.Visible = True
                'mm.lblVNo.text = "VNo :"
                'mm.text = "Purchase Entry"
            ElseIf myMenu = UCase("mnuPurchaseAll") Then
                pSeparateGST = GetSeparateGSTRefund()

                pCondName1 = "lblSeprateGST"
                mm.Controls.Find(pCondName1, True)(0).Text = pSeparateGST    ''mm.Controls.Find(pCondName1, True)(0).Text = pCondValue1

                pCondName1 = "lblBookCode"
                mm.Controls.Find(pCondName1, True)(0).Text = ConPurchaseBookCode

                pCondName1 = "txtVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "txtVDate"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "lblPurchaseVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "lblPurchaseType"
                mm.Controls.Find(pCondName1, True)(0).Text = "G"

                pCondName1 = "lblPurchaseSeqType"
                mm.Controls.Find(pCondName1, True)(0).Text = "1"

                pCondName1 = "lblVNo"
                mm.Controls.Find(pCondName1, True)(0).Text = "VNo :"

                'pCondName1 = "chkRejection"
                'mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "chkRejection"
                mm.Controls.Find(pCondName1, True)(0).Enabled = False

                mm.Text = "Purchase Entry (GST Goods Order)"

                'mm.lblSeprateGST.text = pSeparateGST
                'mm.lblBookCode.text = ConPurchaseBookCode
                'mm.txtVNo.Visible = True
                'mm.txtVDate.Visible = True
                'mm.lblPurchaseVNo.Visible = False
                'mm.lblPurchaseType.text = "G"
                'mm.lblPurchaseSeqType.text = "1"
                'mm.lblVNo.text = "VNo :"
                'mm.chkRejection.Value = vbUnchecked
                'mm.chkRejection.Enabled = False
                'mm.text = "Purchase Entry (GST Goods Order)"
            ElseIf myMenu = UCase("mnuPurchaseSR") Then

                pSeparateGST = GetSeparateGSTRefund()

                pCondName1 = "lblSeprateGST"
                mm.Controls.Find(pCondName1, True)(0).Text = pSeparateGST    ''mm.Controls.Find(pCondName1, True)(0).Text = pCondValue1

                pCondName1 = "lblBookCode"
                mm.Controls.Find(pCondName1, True)(0).Text = ConPurchaseBookCode

                pCondName1 = "txtVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "txtVDate"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "lblPurchaseVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = False

                pCondName1 = "lblPurchaseType"
                mm.Controls.Find(pCondName1, True)(0).Text = "G"

                pCondName1 = "lblPurchaseSeqType"
                mm.Controls.Find(pCondName1, True)(0).Text = "2"

                pCondName1 = "lblVNo"
                mm.Controls.Find(pCondName1, True)(0).Text = "VNo :"

                'pCondName1 = "chkRejection"
                'mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "chkRejection"
                mm.Controls.Find(pCondName1, True)(0).Enabled = False

                mm.Text = "Purchase Entry (Sale Return) Agt Debit Note"

                'pSeparateGST = GetSeparateGSTRefund()
                'mm.lblSeprateGST.text = pSeparateGST
                'mm.lblBookCode.text = ConPurchaseBookCode
                'mm.txtVNo.Visible = True
                'mm.txtVDate.Visible = True
                'mm.lblPurchaseVNo.Visible = False
                'mm.lblPurchaseType.text = "G"
                'mm.lblPurchaseSeqType.text = "2"
                'mm.lblVNo.text = "VNo :"
                'mm.chkRejection.Value = vbChecked
                'mm.chkRejection.Enabled = False
                'mm.text = "Purchase Entry (Sale Return) Agt Debit Note"
            ElseIf myMenu = UCase("mnuPurchaseShip") Then
                pSeparateGST = GetSeparateGSTRefund()

                pCondName1 = "lblSeprateGST"
                mm.Controls.Find(pCondName1, True)(0).Text = pSeparateGST    ''mm.Controls.Find(pCondName1, True)(0).Text = pCondValue1

                pCondName1 = "lblBookCode"
                mm.Controls.Find(pCondName1, True)(0).Text = ConPurchaseBookCode

                pCondName1 = "txtVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "txtVDate"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "lblPurchaseVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = False

                pCondName1 = "lblPurchaseType"
                mm.Controls.Find(pCondName1, True)(0).Text = "G"

                pCondName1 = "lblPurchaseSeqType"
                mm.Controls.Find(pCondName1, True)(0).Text = "3"

                pCondName1 = "lblVNo"
                mm.Controls.Find(pCondName1, True)(0).Text = "VNo :"

                'pCondName1 = "chkRejection"
                'mm.Controls.Find(pCondName1, True)(0).Visible = True

                'pCondName1 = "chkRejection"
                'mm.Controls.Find(pCondName1, True)(0).Enabled = False

                mm.Text = "Purchase Entry (GST Ship)"

                'pSeparateGST = GetSeparateGSTRefund()
                'mm.lblSeprateGST.text = pSeparateGST
                'mm.lblBookCode.text = ConPurchaseBookCode
                'mm.txtVNo.Visible = True
                'mm.txtVDate.Visible = True
                'mm.lblPurchaseVNo.Visible = False
                'mm.lblPurchaseType.text = "G"
                'mm.lblPurchaseSeqType.text = "3"
                'mm.lblVNo.text = "VNo :"
                'mm.text = "Purchase Entry (GST Ship)"
            ElseIf myMenu = UCase("mnuPurchaseAllJW") Then

                pSeparateGST = GetSeparateGSTRefund()

                pCondName1 = "lblSeprateGST"
                mm.Controls.Find(pCondName1, True)(0).Text = pSeparateGST    ''mm.Controls.Find(pCondName1, True)(0).Text = pCondValue1

                pCondName1 = "lblBookCode"
                mm.Controls.Find(pCondName1, True)(0).Text = ConPurchaseBookCode

                pCondName1 = "txtVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "txtVDate"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "lblPurchaseVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = False

                pCondName1 = "lblPurchaseType"
                mm.Controls.Find(pCondName1, True)(0).Text = "J"

                pCondName1 = "lblPurchaseSeqType"
                mm.Controls.Find(pCondName1, True)(0).Text = "4"

                pCondName1 = "lblVNo"
                mm.Controls.Find(pCondName1, True)(0).Text = "VNo :"

                'pCondName1 = "chkRejection"
                'mm.Controls.Find(pCondName1, True)(0).Visible = True

                'pCondName1 = "chkRejection"
                'mm.Controls.Find(pCondName1, True)(0).Enabled = False

                mm.Text = "Purchase Entry (GST-Jobwork Order)"

                'pSeparateGST = GetSeparateGSTRefund()
                'mm.lblSeprateGST.text = pSeparateGST
                'mm.lblBookCode.text = ConPurchaseBookCode
                'mm.txtVNo.Visible = True
                'mm.txtVDate.Visible = True
                'mm.lblPurchaseVNo.Visible = False
                'mm.lblPurchaseType.text = "J"
                'mm.lblPurchaseSeqType.text = "4"
                'mm.lblVNo.text = "VNo :"
                'mm.text = "Purchase Entry (GST-Jobwork Order)"
            ElseIf myMenu = UCase("mnuPurchaseRepair") Then
                pSeparateGST = GetSeparateGSTRefund()

                pCondName1 = "lblSeprateGST"
                mm.Controls.Find(pCondName1, True)(0).Text = pSeparateGST    ''mm.Controls.Find(pCondName1, True)(0).Text = pCondValue1

                pCondName1 = "lblBookCode"
                mm.Controls.Find(pCondName1, True)(0).Text = ConPurchaseBookCode

                pCondName1 = "txtVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "txtVDate"
                mm.Controls.Find(pCondName1, True)(0).Visible = True

                pCondName1 = "lblPurchaseVNo"
                mm.Controls.Find(pCondName1, True)(0).Visible = False

                pCondName1 = "lblPurchaseType"
                mm.Controls.Find(pCondName1, True)(0).Text = "R"

                pCondName1 = "lblPurchaseSeqType"
                mm.Controls.Find(pCondName1, True)(0).Text = "5"

                pCondName1 = "lblVNo"
                mm.Controls.Find(pCondName1, True)(0).Text = "VNo :"

                'pCondName1 = "chkRejection"
                'mm.Controls.Find(pCondName1, True)(0).Visible = True

                'pCondName1 = "chkRejection"
                'mm.Controls.Find(pCondName1, True)(0).Enabled = False

                mm.Text = "Purchase Entry (GST-Repair)"

                'pSeparateGST = GetSeparateGSTRefund()
                'mm.lblSeprateGST.text = pSeparateGST
                'mm.lblBookCode.text = ConPurchaseBookCode
                'mm.txtVNo.Visible = True
                'mm.txtVDate.Visible = True
                'mm.lblPurchaseVNo.Visible = False
                'mm.lblPurchaseType.text = "R"
                'mm.lblPurchaseSeqType.text = "5"
                'mm.lblVNo.text = "VNo :"
                'mm.text = "Purchase Entry (GST-Repair)"
            ElseIf myMenu = UCase("mnuPurchaseWO") Then
                pCondName1 = "lblBookCode"
                mm.Controls.Find(pCondName1, True)(0).Text = ConPurchaseBookCode

                pCondName1 = "lblPurchaseType"
                mm.Controls.Find(pCondName1, True)(0).Text = "W"

                pCondName1 = "lblPurchaseSeqType"
                mm.Controls.Find(pCondName1, True)(0).Text = "6"

                mm.Text = "Purchase Entry (GST Work Order)"


                'mm.lblBookCode.text = ConPurchaseBookCode
                'mm.lblPurchaseType.text = "W"
                'mm.lblPurchaseSeqType.text = "6"
                'mm.text = "Purchase Entry (GST Work Order)"
            ElseIf myMenu = UCase("mnuPurchaseWOServ") Then
                pCondName1 = "lblBookCode"
                mm.Controls.Find(pCondName1, True)(0).Text = ConPurchaseBookCode

                pCondName1 = "lblPurchaseType"
                mm.Controls.Find(pCondName1, True)(0).Text = "S"

                pCondName1 = "lblPurchaseSeqType"
                mm.Controls.Find(pCondName1, True)(0).Text = "7"

                mm.Text = "Contract / Service / Other Bill Entry - GST"

                'mm.lblBookCode.text = ConPurchaseBookCode
                'mm.lblPurchaseType.text = "S"
                'mm.lblPurchaseSeqType.text = "7"
                'mm.text = "Contract / Service / Other Bill Entry - GST"
            ElseIf myMenu = UCase("mnuDevelopmentOrder") Then
                'mm.lblBookType.text = "I"
                'mm.txtOrderGivenBy.Enabled = True
                'mm.txtOrderGivenBy.Visible = True
                'mm.lblGiven.Visible = True

                'mm.lblCustCaption.Visible = False
                'mm.txtSuppCust.Visible = False
                'mm.cmdSearchSuppCust.Visible = False
                'mm.lblSuppCustName.Visible = False
            ElseIf myMenu = UCase("mnuDevelopmentOrderE") Then
                'mm.lblBookType.text = "E"

                'mm.txtOrderGivenBy.Enabled = False
                'mm.txtOrderGivenBy.Visible = False
                'mm.lblGiven.Visible = False

                'mm.lblCustCaption.Visible = True
                'mm.txtSuppCust.Visible = True
                'mm.cmdSearchSuppCust.Visible = True
                'mm.lblSuppCustName.Visible = True
            ElseIf myMenu = UCase("mnuLoadingReg") Then
                pCondName1 = "lblBookType"
                mm.Controls.Find(pCondName1, True)(0).Text = "L"

                'mm.lblBookType.text = "L"
                'mm.chkWOCollection.Enabled = False
                'mm.chkWOCollection.Visible = False
            ElseIf myMenu = UCase("mnuLoadingSlip") Then

                pCondName1 = "lblBookType"
                mm.Controls.Find(pCondName1, True)(0).Text = "L"

                'pCondName1 = "optShow"
                'mm.Controls.Find(pCondName1, True)(0).Enabled = True
                'mm.Controls.Find(pCondName1, True)(0).Visible = True

                'mm.Controls.Find(pCondName1, True)(1).Enabled = True
                'mm.Controls.Find(pCondName1, True)(1).Visible = True

                'mm.Controls.Find(pCondName1, True)(2).Enabled = False
                'mm.Controls.Find(pCondName1, True)(2).Visible = False

                'mm.Controls.Find(pCondName1, True)(3).Enabled = False
                'mm.Controls.Find(pCondName1, True)(3).Visible = False

                'mm.optShow(0).Enabled = True
                'mm.optShow(1).Enabled = True
                'mm.optShow(2).Enabled = False
                'mm.optShow(3).Enabled = False

                'mm.optShow(0).Visible = True
                'mm.optShow(1).Visible = True
                'mm.optShow(2).Visible = False
                'mm.optShow(3).Visible = False

                'mm.optShow(0).Value = True
            ElseIf myMenu = UCase("mnuLoadingSlipAck") Then

                pCondName1 = "lblBookType"
                mm.Controls.Find(pCondName1, True)(0).Text = "L"

                pCondName1 = "lblAck"
                mm.Controls.Find(pCondName1, True)(0).Text = "Y"

                'pCondName1 = "optShow"
                'mm.Controls.Find(pCondName1, True)(0).Enabled = True
                'mm.Controls.Find(pCondName1, True)(0).Visible = True

                'mm.Controls.Find(pCondName1, True)(1).Enabled = True
                'mm.Controls.Find(pCondName1, True)(1).Visible = True

                'mm.Controls.Find(pCondName1, True)(2).Enabled = False
                'mm.Controls.Find(pCondName1, True)(2).Visible = False

                'mm.Controls.Find(pCondName1, True)(3).Enabled = False
                'mm.Controls.Find(pCondName1, True)(3).Visible = False

                'mm.optShow(0).Enabled = True
                'mm.optShow(1).Enabled = True
                'mm.optShow(2).Enabled = False
                'mm.optShow(3).Enabled = False

                'mm.optShow(0).Visible = True
                'mm.optShow(1).Visible = True
                'mm.optShow(2).Visible = False
                'mm.optShow(3).Visible = False

                'mm.optShow(0).Value = True
            ElseIf myMenu = UCase("mnuModvatDCEntry") Then
                'mm.lblBookCode.text = ConModvatBookCode
                'mm.txtModvatNo.Visible = True
                'mm.txtModvatDate.Visible = True
                'mm.lblVNo.text = "Modvat No :"
                'mm.ChkCapital.Enabled = True
                'mm.text = "Modvat Dr/Cr Entry"
            ElseIf myMenu = UCase("mnuModvatEntry") Then
                'mm.lblBookCode.text = ConModvatBookCode
                'mm.txtModvatNo.Visible = True
                'mm.txtModvatDate.Visible = True
                'mm.lblVNo.text = "Modvat No :"
                'mm.ChkCapital.Enabled = True
                'mm.text = "Modvat Entry"
            ElseIf myMenu = UCase("mnuServicetaxClaimDCEntry") Then
                'mm.lblBookCode.text = ConServiceClaimBookCode
                'mm.txtModvatNo.Visible = True
                'mm.txtModvatDate.Visible = True
                'mm.lblVNo.text = "Ref No :"
                'mm.ChkCapital.Enabled = False
                'mm.text = "Service tax Dr/Cr Entry"

                'ElseIf myMenu = UCase("mnuPerksRegister") Then

                '    pCondName1 = "lblIsArrear"
                '    mm.Controls.Find(pCondName1, True)(0).Text = "P"

                '    pCondName1 = "lblMonthTerms"
                '    mm.Controls.Find(pCondName1, True)(0).Visible = True

                '    pCondName1 = "cboMonthTerm"
                '    mm.Controls.Find(pCondName1, True)(0).Enable = True

                '    pCondName1 = "cboMonthTerm"
                '    mm.Controls.Find(pCondName1, True)(0).Visible = False


                '    pCondName1 = "chkPerksHead"
                '    mm.Controls.Find(pCondName1, True)(0).CheckState = System.Windows.Forms.CheckState.Checked

                '    mm.Text = "Perks Register"
            Else


                If pCondName1 <> "" Then
                    mm.Controls.Find(pCondName1, True)(0).Text = pCondValue1
                    'mm.Controls(pCondName1).Text = pCondValue1
                End If


                'System.Windows.Forms.Label()

                If pCondName2 <> "" Then
                    mm.Controls.Find(pCondName2, True)(0).Text = pCondValue2
                    'mm.Controls(pCondName2).Text = pCondValue2
                End If

                If pCondName3 <> "" Then
                    mm.Controls.Find(pCondName3, True)(0).Text = pCondValue3
                    'mm.Controls(pCondName3).Text = pCondValue3
                End If

                If pCondName4 <> "" Then
                    mm.Controls.Find(pCondName4, True)(0).Text = pCondValue4
                    'mm.Controls(pCondName4).Text = pCondValue4
                End If

                If pCondName5 <> "" Then
                    mm.Controls.Find(pCondName5, True)(0).Text = pCondValue5
                    'mm.Controls(pCondName5).Text = pCondValue5
                End If

                If pFormCaption <> "" Then
                    mm.Text = pFormCaption
                End If
            End If

            mm.MdiParent = Me
            'mm.Dock = DockStyle.Fill
            'mm.Parent = Me.FormPanel
            'Me.FormPanel.Controls.Add(mm)
            mm.Show()

            'mm.Text = pFormCaption
            'TabControl1.TabPages.Add(mm)

            'ShowView(mm)



            'Dim newTab As New TabPage
            'Dim TabCount As Integer = TabControl1.TabCount

            'If TabCount > 10 Then
            '    MsgInformation("You already open too much form, please closed some forms")
            '    Exit Sub
            'End If


            'TabControl1.Controls.Add(newTab)
            ''newTab = TabControl1.TabPages(TabCount)
            'TabControl1.TabPages(TabCount).Name = pFormCaption
            'mm.TopLevel = False
            'mm.WindowState = FormWindowState.Maximized
            'mm.FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
            ''mm.Dock = DockStyle.Fill
            'mm.Visible = True
            'Me.TabControl1.TabPages(TabCount).Controls.Add(mm)
            'Me.TabControl1.TabPages(TabCount).Select()


            '            // Removes the selected tab  
            'TabControl1.TabPages.Remove(TabControl1.SelectedTab);  
            '// Removes all the tabs  
            'TabControl1.TabPages.Clear();  

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
    'Private Sub ShowView(ByVal childView As Control)
    '    Cursor = Cursors.WaitCursor

    '    If _currentView IsNot Nothing Then


    '        If TypeOf _currentView Is Form Then
    '            Dim myform As Form = _currentView

    '            myform.Close()

    '        Else

    '            _currentView.Visible = False

    '            _currentView.Dispose()

    '        End If

    '        _currentView = Nothing

    '    End If

    '    childView.Hide()

    '    If TypeOf childView Is Form Then

    '        Dim childForm As Form = childView

    '        childForm.TopLevel = False

    '        childForm.FormBorderStyle = Windows.Forms.FormBorderStyle.None

    '    End If

    '    Me.SplitContainer1.Panel2.Controls.Add(childView)

    '    childView.BringToFront()

    '    childView.BackColor = Me.BackColor

    '    childView.Dock = DockStyle.Fill

    '    childView.Show()

    '    _currentView = childView

    '    Cursor = Cursors.Default

    'End Sub
#Region "CreateObjectInstance"
    Public Function CreateObjectInstance(ByVal objectName As String) As Object
        Dim obj As Object
        Try
            If objectName.LastIndexOf(".") = -1 Then
                objectName = [Assembly].GetEntryAssembly.GetName.Name & "." & objectName
            End If

            obj = [Assembly].GetEntryAssembly.CreateInstance(objectName)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            obj = Nothing
        End Try
        Return obj

    End Function
#End Region

    Private Sub naviBar1_ActiveBandChanging(sender As Object, e As Guifreaks.NavigationBar.NaviBandEventArgs) Handles naviBar1.ActiveBandChanging
        Dim pModuleId As Long

        If PubMIDFormLoad = False Then Exit Sub

        pModuleId = e.NewActiveBand.Tag
        CurrModuleName = e.NewActiveBand.Text

        'If e.NewActiveBand Is AdminModule Then
        '    CurrModuleName = mAdminModule
        'pModuleId = mAdminModuleID
        'ElseIf e.NewActiveBand Is AccountModule Then
        '    CurrModuleName = mAccountModule
        '    pModuleId = mAccountModuleID
        'ElseIf e.NewActiveBand Is InventoryModule Then
        '    CurrModuleName = mInventoryModule
        '    pModuleId = mInventoryModuleID
        'ElseIf e.NewActiveBand Is SaleModule Then
        '    CurrModuleName = mSaleModule
        '    pModuleId = mSaleModuleID
        'ElseIf e.NewActiveBand Is MISModule Then
        '    CurrModuleName = mMISModule
        '    pModuleId = mMISModuleID
        'ElseIf e.NewActiveBand Is PayrollModule Then
        '    CurrModuleName = mPayrollModule
        '    pModuleId = mPayrollModuleID
        'ElseIf e.NewActiveBand Is ProductionModule Then
        '    CurrModuleName = mProductionModule
        '    pModuleId = mProductionModuleID
        'ElseIf e.NewActiveBand Is QualityModule Then
        '    CurrModuleName = mQualityModule
        '    pModuleId = mQualityModuleID
        'ElseIf e.NewActiveBand Is TDSModule Then
        '    CurrModuleName = mTDSModule
        '    pModuleId = mTDSModuleID
        'End If

        LoadMenu(pModuleId, e.NewActiveBand)
    End Sub

    Private Sub FormMain_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        PubMainFormHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - MenuStrip1.Height - -StatusStrip.Height - 20
        PubMainFormWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - (naviBar1.Width) - 20
    End Sub

    Private Sub FormMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode

            Case Keys.CapsLock

                Caps.Enabled = Not Caps.Enabled

            Case Keys.NumLock

                num.Enabled = Not num.Enabled

            Case Keys.Scroll

                'sbrpnlScrollLock.Enabled = Not sbrpnlScrollLock.Enabled

            Case Keys.Insert

                ins.Text = IIf(ins.Text = "OVR", "INS", "OVR")

        End Select
    End Sub

    Private Sub PasswordChangeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasswordChangeToolStripMenuItem.Click
        Try
            Dim pFormName As String = ""
            Dim pFormCaption As String = ""
            Dim mm As New Form


            'Call GetFormName(myMenu, pFormName, pCondName1, pCondValue1, pCondName2, pCondValue2, pCondName3, pCondValue3, pCondName4, pCondValue4, pCondName5, pCondValue5, pFormCaption)

            pFormName = "frmChangePwd"
            mm = CType(CreateObjectInstance(pFormName), Form)       '' DirectCast(CreateObjectInstance(pFormName), Form)
            mm.MdiParent = Me
            mm.Show()


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
