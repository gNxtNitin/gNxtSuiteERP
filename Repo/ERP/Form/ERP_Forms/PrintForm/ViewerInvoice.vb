Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewerInvoice
	Inherits System.Windows.Forms.Form
	' *************************************************************
	' Purpose: Demonstrate how the Viewer can be controlled using external
	'          controls
	'
	
	
	' Module Constants
	Const TOP_VIEW As Short = 1 ' const used to determine Parent view
	
	' These constants are used for setting the buttons in the toolbar
	Const CLOSE_BUT As Short = 1
	Const FIRSTPAGE_BUT As Short = 2
	Const PREVPAGE_BUT As Short = 3
	Const NEXTPAGE_BUT As Short = 5
	Const LASTPAGE_BUT As Short = 6
	Const PRINT_BUT As Short = 8
	Const REFRESH_BUT As Short = 9
	Const GROUPTREE_BUT As Short = 10
	Const SEARCH_BUT As Short = 12
	
	'Dim m_Report As New dsrInventory
	
	
	
	' *************************************************************
	' Enables/Disables toolbar depending upon the parameter State
	'
	Private Sub mEnableCloseButton(ByRef State As Boolean)
		Dim CloseButton As ComctlLib.Button
		
		CloseButton = Toolbar.Buttons(CLOSE_BUT)
		CloseButton.Enabled = State
		
		'UPGRADE_NOTE: Object CloseButton may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		CloseButton = Nothing
	End Sub
	
	' *************************************************************
	' Calls Smart Viewer Zoom method to set the zoom percentage according
	' to the user's choice
	'
	'UPGRADE_WARNING: Event cboZoom.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub cboZoom_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboZoom.SelectedIndexChanged
		Dim strZmPcnt As String
		
		strZmPcnt = mstrGetZoomPercentage
		CRViewer1.Zoom((CShort(strZmPcnt)))
	End Sub

    ' *************************************************************
    ' When the Group tree is toggled on the Smart Viewer toolbar,
    ' the custom toolbar needs to be notified of the change.
    '
    'Private Sub CRViewer1_GroupTreeButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxCRVIEWERLib._ICRViewerEvents_GroupTreeButtonClickedEvent) Handles CRViewer1.GroupTreeButtonClicked
    '	Call mSetGroupTree()
    'End Sub

    ' *************************************************************
    ' Disables Close button on custom toolbar if New Active View
    ' is the top most view
    '
    '   Private Sub CRViewer1_ViewChanged(ByVal eventSender As System.Object, ByVal eventArgs As AxCRVIEWERLib._ICRViewerEvents_ViewChangedEvent) Handles CRViewer1.ViewChanged
    '	If eventArgs.newViewIndex = (TOP_VIEW - 1) Then
    '		mEnableCloseButton(False)
    '	Else
    '		mEnableCloseButton(True)
    '	End If
    'End Sub

    ' *************************************************************
    ' Displays the zoom percentage in dropdown box
    '
    Private Sub mSetZoomLevelOnControl(ByRef strZoomLevel As String)
		Dim indx As Short
		
		Select Case strZoomLevel
			Case "1" 'Page Width
				cboZoom.Text = "51%"
			Case "2" 'Whole Page
				cboZoom.Text = "52%"
			Case Else
				For indx = 0 To cboZoom.Items.Count
					If strZoomLevel = VB.Left(VB6.GetItemString(cboZoom, indx), Len(strZoomLevel)) Then
						cboZoom.Text = VB6.GetItemString(cboZoom, indx)
						Exit Sub
					End If
				Next indx
		End Select
	End Sub

    ' *************************************************************
    ' The zoom level was changed by Smart Viewer toolbar, so notify
    ' the custom toolbar of the change
    '
    'Private Sub CRViewer1_ZoomLevelChanged(ByVal eventSender As System.Object, ByVal eventArgs As AxCRVIEWERLib._ICRViewerEvents_ZoomLevelChangedEvent) Handles CRViewer1.ZoomLevelChanged
    '	mSetZoomLevelOnControl(CStr(eventArgs.ZoomLevel))
    'End Sub

    ' *************************************************************
    ' Get the current page of the viewer when the viewer has finished
    ' generating the page
    '
    Private Function migetCurrentPage() As Short
        'While CRViewer1.IsBusy
        '	System.Windows.Forms.Application.DoEvents()
        'End While
        migetCurrentPage = CRViewer1.GetCurrentPageNumber
	End Function
	
	'UPGRADE_WARNING: Form event frmViewerInvoice.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Private Sub frmViewerInvoice_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		'    CRViewer1.ReportSource = Report
		'    CRViewer1.Height = frmreport3.Height - 500
		'    CRViewer1.Width = frmreport3.Width - 500
		'
		'    CRViewer1.EnableNavigationControls = True
		'    CRViewer1.EnableExportButton = False
		'    CRViewer1.EnableCloseButton = True
		'    CRViewer1.EnablePrintButton = True
		'    CRViewer1.EnableProgressControl = True
		'    CRViewer1.EnableRefreshButton = False
		'    CRViewer1.Refresh
		'    CRViewer1.DisplayTabs = True
		'    CRViewer1.DisplayBackgroundEdge = True
		'    CRViewer1.DisplayGroupTree = False
		'    CRViewer1.DisplayBorder = True
		'    CRViewer1.DisplayToolbar = True
		'    CRViewer1.EnableAnimationCtrl = True
		'    CRViewer1.ViewReport
	End Sub
	
	' *************************************************************
	' Load the Inventory Report into the Smart Viewer
	'
	Private Sub frmViewerInvoice_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		On Error GoTo Form_Load_err
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call mCreateToolbar()
		
		' Set the report source
		'UPGRADE_WARNING: Couldn't resolve default property of object CRViewer1.ReportSource. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		CRViewer1.ReportSource = objRpt
        CRViewer1.ViewReport()

        StatusBar.SimpleText = objRpt.ReportTitle
		txtCurPage.Text = CStr(migetCurrentPage)
		
		Call frmViewerInvoice_Resize(Me, New System.EventArgs())
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
		
		' Let the user know about any errors that might have occurred.
Form_Load_err: 
		MsgBox("Error: " & CStr(Err.Number) & Chr(10) & Chr(13) & ErrorToString(Err.Number),  , "Form Load")
	End Sub
	
	' *************************************************************
	' Load zoom percentages into combo box
	'
	Private Sub mInsertZoomPercentages()
		cboZoom.Items.Clear()
		With cboZoom
			.Items.Insert(0, "400%")
			.Items.Insert(1, "300%")
			.Items.Insert(2, "200%")
			.Items.Insert(3, "150%")
			.Items.Insert(4, "100%")
			.Items.Insert(5, "75%")
			.Items.Insert(6, "50%")
			.Items.Insert(7, "25%")
			.Items.Insert(8, "Page Width")
			.Items.Insert(9, "Whole Page")
		End With
		cboZoom.Text = VB6.GetItemString(cboZoom, 4) ' Set list to 100%
	End Sub
	
	' *************************************************************
	' Reposition the controls in the form to compensate for the new
	' form size.
	'
	'UPGRADE_WARNING: Event frmViewerInvoice.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub frmViewerInvoice_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
		Dim iTop As Short
		Dim iAdjustment As Short
		
		If Toolbar.Visible Then
			iTop = VB6.PixelsToTwipsY(Toolbar.Height)
			iAdjustment = VB6.PixelsToTwipsY(Toolbar.Height) + VB6.PixelsToTwipsY(StatusBar.Height)
		Else
			iTop = 0
			iAdjustment = VB6.PixelsToTwipsY(StatusBar.Height)
		End If
		
		'    Debug.Assert Me.Height > iAdjustment
		
		If VB6.PixelsToTwipsY(Me.Height) > iAdjustment Then
			CRViewer1.Top = VB6.TwipsToPixelsY(iTop)
			CRViewer1.Left = 0
			CRViewer1.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Height) - iAdjustment)
			CRViewer1.Width = Me.Width
		End If
	End Sub
	
	' *************************************************************
	' Show info about this demo
	'
	Public Sub mnuAbout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuAbout.Click
        'frmAbout.ShowDialog()
    End Sub
	
	' *************************************************************
	' Toggle the Smart Viewer toolbar
	'
	Public Sub mnuCRToolbar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuCRToolbar.Click
		CRViewer1.DisplayToolbar = Not CRViewer1.DisplayToolbar
		mnuCRToolbar.Checked = Not mnuCRToolbar.Checked
	End Sub
	
	' *************************************************************
	' Toggle the Custom toolbar
	'
	Public Sub mnuCustomToolbar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuCustomToolbar.Click
		Toolbar.Visible = Not Toolbar.Visible
		mnuCustomToolbar.Checked = Not mnuCustomToolbar.Checked
		Call frmViewerInvoice_Resize(Me, New System.EventArgs())
	End Sub
	
	' *************************************************************
	' Toggle the group tree
	'
	Public Sub mnuDisplayGroupTree_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuDisplayGroupTree.Click
		CRViewer1.DisplayGroupTree = Not CRViewer1.DisplayGroupTree
		mnuDisplayGroupTree.Checked = Not mnuDisplayGroupTree.Checked
	End Sub
	
	' *************************************************************
	' Checks active view to see if it is the top index view.  If the
	' view is the top view (1) then the close view button on toolbar
	' is disabled.
	'
	Private Sub mCheckActiveView()
		If CRViewer1.ActiveViewIndex = TOP_VIEW Then mEnableCloseButton(False)
	End Sub
	
	' *************************************************************
	' Create the toolbar with all the appropriate icons.
	'
	Private Sub mCreateToolbar()
		Dim setButton As ComctlLib.Button
		Dim ImageList As ComctlLib.ListImage
		Dim strIconPath As String
		
		On Error GoTo mCreateToolbar_err
		
		' Set path to icon directory
		strIconPath = My.Application.Info.DirectoryPath & "\icons\"
		
		' Set icon sizing and create list
		ImgLst.ImageHeight = 16
		ImgLst.ImageWidth = 16
		With ImgLst.ListImages
			ImageList = .Add( , "close", VB6.ImageToIPictureDisp(System.Drawing.Image.FromFile(strIconPath & "w95mbx01.ico")))
			ImageList = .Add( , "print", VB6.ImageToIPictureDisp(System.Drawing.Image.FromFile(strIconPath & "printfld.ico")))
			ImageList = .Add( , "refresh", VB6.ImageToIPictureDisp(System.Drawing.Image.FromFile(strIconPath & "refresh.ico")))
			ImageList = .Add( , "search", VB6.ImageToIPictureDisp(System.Drawing.Image.FromFile(strIconPath & "binoculr.ico")))
			ImageList = .Add( , "firstpage", VB6.ImageToIPictureDisp(System.Drawing.Image.FromFile(strIconPath & "arw03lt.ico")))
			ImageList = .Add( , "prevpage", VB6.ImageToIPictureDisp(System.Drawing.Image.FromFile(strIconPath & "arw04lt.ico")))
			ImageList = .Add( , "nextpage", VB6.ImageToIPictureDisp(System.Drawing.Image.FromFile(strIconPath & "arw04rt.ico")))
			ImageList = .Add( , "lastpage", VB6.ImageToIPictureDisp(System.Drawing.Image.FromFile(strIconPath & "arw03rt.ico")))
			ImageList = .Add( , "grouptree", VB6.ImageToIPictureDisp(System.Drawing.Image.FromFile(strIconPath & "graph14.ico")))
		End With
		
		' Bind toolbar to imagelist and set buttons on toolbar which require icons
		Toolbar.ImageList = ImgLst.GetOcx
		Toolbar.ButtonHeight = ImgLst.ImageHeight
		Toolbar.ButtonWidth = ImgLst.ImageWidth
		
		' Set an icon for each button on the toolbar
		setButton = Toolbar.Buttons(CLOSE_BUT)
		setButton.Image = "close"
		setButton.ToolTipText = "Close Current View"
		
		setButton = Toolbar.Buttons(FIRSTPAGE_BUT)
		setButton.Image = "firstpage"
		setButton.ToolTipText = "Go to First Page"
		
		setButton = Toolbar.Buttons(PREVPAGE_BUT)
		setButton.Image = "prevpage"
		setButton.ToolTipText = "Go to Previous Page"
		
		setButton = Toolbar.Buttons(NEXTPAGE_BUT)
		setButton.Image = "nextpage"
		setButton.ToolTipText = "Go to Next Page"
		
		setButton = Toolbar.Buttons(LASTPAGE_BUT)
		setButton.Image = "lastpage"
		setButton.ToolTipText = "Go to Last Page"
		
		setButton = Toolbar.Buttons(PRINT_BUT)
		setButton.Image = "print"
		setButton.ToolTipText = "Print Report"
		
		setButton = Toolbar.Buttons(REFRESH_BUT)
		setButton.Image = "refresh"
		setButton.ToolTipText = "Refresh"
		
		setButton = Toolbar.Buttons(SEARCH_BUT)
		setButton.Image = "search"
		setButton.ToolTipText = "Search Text"
		
		setButton = Toolbar.Buttons(GROUPTREE_BUT)
		setButton.Image = "grouptree"
		setButton.ToolTipText = "Toggle Group Tree"
		If CRViewer1.DisplayGroupTree Then
			setButton.Value = ComctlLib.ValueConstants.tbrPressed
		Else
			setButton.Value = ComctlLib.ValueConstants.tbrUnpressed
		End If
		
		mInsertZoomPercentages() 'insert zoom percentages into combo box
		Exit Sub
		
		' Handle toolbar errors
mCreateToolbar_err: 
		MsgBox("Error: " & CStr(Err.Number) & Chr(10) & Chr(13) & ErrorToString(Err.Number),  , "Creating Toolbar")
	End Sub
	
	' *************************************************************
	' Determine which toolbar button was clicked and then execute
	' that action
	'
	Private Sub Toolbar_ButtonClick(ByVal eventSender As System.Object, ByVal eventArgs As AxComctlLib.IToolbarEvents_ButtonClickEvent) Handles Toolbar.ButtonClick
		Select Case eventArgs.Button.Index
			Case CLOSE_BUT
				' Closes active view
				If CRViewer1.ActiveViewIndex > 1 Then
					CRViewer1.CloseView((CRViewer1.ActiveViewIndex))
				End If
				mCheckActiveView()
			Case FIRSTPAGE_BUT
				CRViewer1.ShowFirstPage()
				txtCurPage.Text = CStr(migetCurrentPage)
			Case PREVPAGE_BUT
				CRViewer1.ShowPreviousPage()
				txtCurPage.Text = CStr(migetCurrentPage)
			Case NEXTPAGE_BUT
				CRViewer1.ShowNextPage()
				txtCurPage.Text = CStr(migetCurrentPage)
			Case LASTPAGE_BUT
				CRViewer1.ShowLastPage()
				txtCurPage.Text = CStr(migetCurrentPage)
			Case PRINT_BUT
				CRViewer1.PrintReport()
			Case REFRESH_BUT
                'UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
                CRViewer1.Refresh()
            Case SEARCH_BUT
				Call mSearchForText()
			Case GROUPTREE_BUT
				CRViewer1.DisplayGroupTree = Not CRViewer1.DisplayGroupTree
				Call mSetGroupTree()
		End Select
	End Sub
	
	' *************************************************************
	' Search for text in the report
	'
	Private Sub mSearchForText()
		If cboSearch.Text = "" Then
			MsgBox("Search Text not specified", MsgBoxStyle.OKOnly, "Search Text")
		Else
			CRViewer1.SearchForText((cboSearch.Text))
		End If
	End Sub
	
	' *************************************************************
	' Set the GroupTree button to be the same as the "Display Group Tree"
	' option in the viewer
	'
	Private Sub mSetGroupTree()
		Dim GroupTreeButton As ComctlLib.Button
		
		GroupTreeButton = Toolbar.Buttons(GROUPTREE_BUT)
		If CRViewer1.DisplayGroupTree Then
			GroupTreeButton.Value = ComctlLib.ValueConstants.tbrPressed
		Else
			GroupTreeButton.Value = ComctlLib.ValueConstants.tbrUnpressed
		End If
		
		'UPGRADE_NOTE: Object GroupTreeButton may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		GroupTreeButton = Nothing
	End Sub
	
	' *************************************************************
	' Return a string that the viewer can use to set the zoom-in value
	'
	Private Function mstrGetZoomPercentage() As String
		Dim ipercentpos As String
		
		ipercentpos = CStr(InStr(1, cboZoom.Text, "%"))
		If CDbl(ipercentpos) <> 0 Then
			mstrGetZoomPercentage = VB.Left(cboZoom.Text, CDbl(ipercentpos) - 1) ' Returns a numeric string
		ElseIf cboZoom.Text = "Page Width" Then 
			mstrGetZoomPercentage = "1"
		ElseIf cboZoom.Text = "Whole Page" Then 
			mstrGetZoomPercentage = "2"
		End If
	End Function
End Class