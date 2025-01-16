Option Strict Off
Option Explicit On
'Imports Microsoft.VisualBasic.PowerPacks
Imports Microsoft.VisualBasic.Compatibility
Friend Class Resizeclass
    ' * Comments         :
    ' * This class can change size and location of controls On your form
    ' * 1. Resize form
    ' * 2. Change screen resolution
    ' * Assumes:1. Add Elastic.cls
    ' *         2. Add declaration 'Private El as New class_Elastic'
    ' *         3. Insert string like 'El.Init Me' (formload event)
    ' *         4. Insert string like 'El.FormResize Me' (Resize event)
    ' *         5. Press 'F5' and resize form ....

    '
    ' NOTE FROM FREEVBCODE.COM'S TESTING
    ' This works best if you:
    ' 1. Declare a form_level variable such as m_IsLoading as boolean
    ' 2. in Form_Load, enter code such as the following:
    '
    '        m_IsLoading = true
    '        El.Init Me
    '        m_IsLoading = False
    '
    ' 3. In Form Resize, use:
    '
    '       If Not m_IsLoading Then El.FormResize.Me
    '****************************************************************

    ' Andrew Koransky (ASK, andrewk(at)koransky.com) modified 10/22/2002:
    '  - now handles resizing of SSTab controls and ListView controls
    '  - tabs in code switched to 4 spaces
    '  - fixed some typos in the comments
    '  - comments more consistent
    '  - removed code to maximize the window when the window wasn't supposed to be maximized!
    '  - Integer replaced with Long
    '  - "Int(" replaced with "CLng("

    Private nFormHeight As Integer
    Private nFormWidth As Integer
    Private nNumOfControls As Integer
    Private nTop() As Integer
    Private nLeft() As Integer
    Private nHeight() As Integer
    Private nWidth() As Integer
    Private nFontSize() As Integer
    Private nRightMargin() As Integer
    Private nListViewColumnWidth() As Collection
    Private bFirstTime As Boolean


    Public Sub Init(ByRef frm As System.Windows.Forms.Form, Optional ByRef nWindState As Object = Nothing)

        Dim I As Integer
        Dim bWinMax As Boolean

        bWinMax = Not IsNothing(nWindState)

        nFormHeight = VB6.PixelsToTwipsY(frm.Height)
        nFormWidth = VB6.PixelsToTwipsX(frm.Width)
        nNumOfControls = frm.Controls.Count() - 1
        bFirstTime = True
        ReDim nTop(nNumOfControls)
        ReDim nLeft(nNumOfControls)
        ReDim nHeight(nNumOfControls)
        ReDim nWidth(nNumOfControls)
        ReDim nFontSize(nNumOfControls)
        ReDim nListViewColumnWidth(nNumOfControls)

        ReDim nRightMargin(nNumOfControls)
        On Error Resume Next
        Dim j As Integer
        For I = 0 To nNumOfControls
            'If TypeOf CType(frm.Controls(I), Object) Is Microsoft.VisualBasic.PowerPacks.LineShape Then
            '   nTop(I) = CType(frm.Controls(I), Object).Y1
            '   nLeft(I) = CType(frm.Controls(I), Object).X1
            '   nHeight(I) = CType(frm.Controls(I), Object).Y2
            '   nWidth(I) = CType(frm.Controls(I), Object).X2
            'Else
            ' Handle the SSTab hidden control case
            If VB6.PixelsToTwipsX(CType(frm.Controls(I), Object).Left) < 0 Then
                nLeft(I) = VB6.PixelsToTwipsX(CType(frm.Controls(I), Object).Left) + 75000
            Else
                nLeft(I) = VB6.PixelsToTwipsX(CType(frm.Controls(I), Object).Left)
            End If
            If (TypeOf CType(frm.Controls(I), Object) Is System.Windows.Forms.TabControl) Then ''Or (TypeOf CType(frm.Controls(I), Object) Is AxComctlLib.AxListView) Then
                nFontSize(I) = CType(frm.Controls(I), Object).Font.SizeInPoints
                nRightMargin(I) = CType(frm.Controls(I), Object).TabHeight ' resume next for ListView
            Else
                nFontSize(I) = CType(frm.Controls(I), Object).FontSize
                nRightMargin(I) = CType(frm.Controls(I), Object).RightMargin
            End If
            nTop(I) = VB6.PixelsToTwipsY(CType(frm.Controls(I), Object).Top)
            nHeight(I) = VB6.PixelsToTwipsY(CType(frm.Controls(I), Object).Height)
            nWidth(I) = VB6.PixelsToTwipsX(CType(frm.Controls(I), Object).Width)

            ' save off the list view column widths
            'If TypeOf CType(frm.Controls(I), Object) Is AxComctlLib.AxListView Then
            '   nListViewColumnWidth(I) = New Collection
            '   For j = 1 To CType(frm.Controls(I), Object).ColumnHeaders.Count
            '      nListViewColumnWidth(I).Add(CType(frm.Controls(I), Object).ColumnHeaders(j).Width)
            '   Next j
            'End If
            'End If
        Next

        If bWinMax Or frm.WindowState = 2 Then ' maxim
            frm.Height = VB6.TwipsToPixelsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) ''- 950
            frm.Width = VB6.TwipsToPixelsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) ''- 100
            ' -- ASK 10/22/2002 --
            ' removed these lines... not sure why they were here.
            ' This code ended up maximizing my window when I had it set for normal display.
            '   Else
            '      frm.Height = frm.Height * Screen.Height / 7290
            '      frm.Width = frm.Width * Screen.Width / 9690
        End If

        bFirstTime = True
    End Sub

    Public Sub formResize(ByRef frm As System.Windows.Forms.Form)
        Dim I As Integer
        Dim nCaptionSize As Integer
        Dim dRatioX As Double
        Dim dRatioY As Double
        Dim nSaveRedraw As Integer

        On Error Resume Next
        'nSaveRedraw = frm.AutoRedraw

        'frm.AutoRedraw = True

        If bFirstTime Then
            bFirstTime = False
            Exit Sub
        End If

        If VB6.PixelsToTwipsY(frm.Height) < nFormHeight / 2 Then frm.Height = VB6.TwipsToPixelsY(nFormHeight / 2)

        If VB6.PixelsToTwipsX(frm.Width) < nFormWidth / 2 Then frm.Width = VB6.TwipsToPixelsX(nFormWidth / 2)
        nCaptionSize = 400
        dRatioY = 1.0# * (nFormHeight - nCaptionSize) / (VB6.PixelsToTwipsY(frm.Height) - nCaptionSize)
        dRatioX = 1.0# * nFormWidth / VB6.PixelsToTwipsX(frm.Width)
        On Error Resume Next ' for comboboxes, timeres and other nonsizible controls

        ' -- ASK 10/22/2002 BEGIN --
        ' First hide the SSTab controls
        For I = 0 To nNumOfControls
            If TypeOf CType(frm.Controls(I), Object) Is System.Windows.Forms.TabControl Then
                CType(frm.Controls(I), Object).Visible = False
            End If
        Next I
        ' -- ASK 10/22/2002 END --

        Dim j As Integer
        For I = 0 To nNumOfControls

            'If TypeOf CType(frm.Controls(I), Object) Is Microsoft.VisualBasic.PowerPacks.LineShape Then
            '   CType(frm.Controls(I), Object).Y1 = CInt(nTop(I) / dRatioY)
            '   CType(frm.Controls(I), Object).X1 = CInt(nLeft(I) / dRatioX)
            '   CType(frm.Controls(I), Object).Y2 = CInt(nHeight(I) / dRatioY)
            '   CType(frm.Controls(I), Object).X2 = CInt(nWidth(I) / dRatioX)
            'Else
            ' Collect the list of hidden controls and handle them appropriately
            If VB6.PixelsToTwipsX(CType(frm.Controls(I), Object).Left) < 0 Then
                CType(frm.Controls(I), Object).Left = VB6.TwipsToPixelsX(CInt(nLeft(I) / dRatioX) - 75000)
            Else
                CType(frm.Controls(I), Object).Left = VB6.TwipsToPixelsX(CInt(nLeft(I) / dRatioX))
            End If
            CType(frm.Controls(I), Object).Top = VB6.TwipsToPixelsY(CInt(nTop(I) / dRatioY))

            ' deal with special case for font size in SSTab and ListView, and tab height in SSTab
            If (TypeOf CType(frm.Controls(I), Object) Is System.Windows.Forms.TabControl) Then '' Or (TypeOf CType(frm.Controls(I), Object) Is AxComctlLib.AxListView) Then
                CType(frm.Controls(I), Object).Font = VB6.FontChangeSize(CType(frm.Controls(I), Object).Font, CInt(nFontSize(I) / dRatioX) + CInt(nFontSize(I) / dRatioX) Mod 2)
                CType(frm.Controls(I), Object).TabHeight = CInt(nRightMargin(I) / dRatioY)
            Else
                CType(frm.Controls(I), Object).Font = VB6.FontChangeSize(CType(frm.Controls(I), Object).Font, CInt(nFontSize(I) / dRatioX) + CInt(nFontSize(I) / dRatioX) Mod 2)
                CType(frm.Controls(I), Object).RightMargin = CInt(nRightMargin(I) / dRatioY)
            End If

            CType(frm.Controls(I), Object).Height = VB6.TwipsToPixelsY(CInt(nHeight(I) / dRatioY))

            CType(frm.Controls(I), Object).Width = VB6.TwipsToPixelsX(CInt(nWidth(I) / dRatioX))

            ' Deal with the list view column proportional resizing
            'If TypeOf CType(frm.Controls(I), Object) Is AxComctlLib.AxListView Then
            '   For j = 1 To CType(frm.Controls(I), Object).ColumnHeaders.Count
            '      CType(frm.Controls(I), Object).ColumnHeaders(j).Width = CInt(nListViewColumnWidth(I).Item(j) / dRatioX)
            '   Next j
            'End If
            'End If
        Next

        ' re-display the SSTab controls
        For I = 0 To nNumOfControls
            If TypeOf CType(frm.Controls(I), Object) Is System.Windows.Forms.TabControl Then
                CType(frm.Controls(I), Object).Visible = True
            End If
        Next I

        'frm.AutoRedraw = nSaveRedraw

    End Sub
End Class
