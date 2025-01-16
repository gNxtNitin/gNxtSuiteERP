Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility

Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration


Module SprdPreView
    Public zoomindex As Short
    Public PrintSpread As Boolean

    Sub GetZoom(ByRef zoomlabel As Short)
        'Set up the print previews zoom

        '    With frmViewCrossBook.SprdPreview
        '    Select Case zoomlabel
        '            Case 0
        '                .PageViewType = 2
        '                .PageViewPercentage = 200
        '
        '            Case 1
        '                .PageViewType = 2
        '                .PageViewPercentage = 150
        '
        '            Case 2
        '                .PageViewType = 2
        '                .PageViewPercentage = 100
        '
        '            Case 3
        '                .PageViewType = 2
        '                .PageViewPercentage = 75
        '
        '            Case 4
        '                .PageViewType = 2
        '                .PageViewPercentage = 50
        '
        '            Case 5
        '                .PageViewType = 2
        '                .PageViewPercentage = 25
        '
        '            Case 6
        '                .PageViewType = 2
        '                .PageViewPercentage = 10
        '
        '            Case 7
        '                .PageViewType = 3
        '
        '            Case 8
        '                .PageViewType = 4
        '
        '            Case 9
        '                .PageViewType = 0
        '
        '            Case 10
        '                .PageViewType = 5
        '                .PageMultiCntH = 2
        '                .PageMultiCntV = 1
        '
        '            Case 11
        '                .PageViewType = 5
        '                .PageMultiCntH = 3
        '                .PageMultiCntV = 1
        '
        '            Case 12
        '                .PageViewType = 5
        '                .PageMultiCntH = 2
        '                .PageMultiCntV = 2
        '
        '            Case 13
        '                .PageViewType = 5
        '                .PageMultiCntH = 3
        '                .PageMultiCntV = 2
        '
        '        End Select
        '    End With
    End Sub
    Public Sub SpreadPrint(ByRef SpreadView As AxFPSpreadADO.AxfpSpread)

        With SpreadView
            .PrintColHeaders = True
            .PrintRowHeaders = False
            .PrintBorder = True
            .PrintColor = False
            .PrintGrid = False
            .PrintShadows = False
            .PrintUseDataMax = True

            .PrintType = SS_PRINT_ALL

            'Print control

            .PrintMarginTop = 100 ''1440
            .PrintMarginBottom = 100 ''1440
            .PrintMarginLeft = 50 ''720
            .PrintMarginRight = 50 ''720

            .Action = SS_ACTION_PRINT
        End With
    End Sub

    Public Sub SpreadSheetPreview(ByRef SpreadView As AxFPSpreadADO.AxfpSpread, ByRef SpreadPreview As AxFPSpreadADO.AxfpSpreadPreview, ByRef SpreadCommand As AxFPSpreadADO.AxfpSpread, ByRef mScaleWidth As Object, ByRef mScaleHeight As Object)
        SetupToolbar(SpreadCommand)

        'Disable Previous button
        DisableButton(4, "LEFT", SpreadCommand)

        'Get the zoom display
        '' Call GetZoom(SprdPreview, zoomindex)

        'Set up page numbering
        With SpreadView
            If .PrintPageCount = 1 Then
                'Disable Next button if only one page
                DisableButton(2, "LEFT", SpreadCommand)
            End If

            .PrintBorder = True
            .PrintOrientation = FPSpreadADO.PrintOrientationConstants.PrintOrientationLandscape
            .PrintColHeaders = True
            .PrintRowHeaders = False
            .PrintBorder = True
            .PrintColor = True
            .PrintShadows = True
            .PrintGrid = True
            .PrintUseDataMax = True
        End With

        SpreadPreview.hWndSpread = SpreadView.hWnd

        'Update page count listing
        UpdatePageCount(SpreadView, SpreadPreview, SpreadCommand)

        'SpreadCommand.Move(60, 180, mScaleWidth, SpreadCommand.Height)
        'SpreadPreview.Move(60, SpreadCommand.Height + 180, mScaleWidth, mScaleHeight - SpreadCommand.Height - 1000)

    End Sub



    Public Sub ShowNextPage(ByRef SpreadView As AxFPSpreadADO.AxfpSpread, ByRef SpreadPreview As AxFPSpreadADO.AxfpSpreadPreview, ByRef SpreadCommand As AxFPSpreadADO.AxfpSpread, ByRef col2 As Integer)

        With SpreadPreview
            If .PageCurrent < SpreadView.PrintPageCount Then
                .PageCurrent = .PageCurrent + .PagesPerScreen
                EnableButton(col2, "RIGHT", SpreadCommand)
                'Enable Previous button
                EnableButton(4, "LEFT", SpreadCommand)
                'Update page count listing
                UpdatePageCount(SpreadView, SpreadPreview, SpreadCommand)
            End If

            'If at last page, disable button
            If .PageCurrent >= SpreadView.PrintPageCount Then
                ''If .PageCurrent >= mPrintPageCont - .PagesPerScreen Then
                DisableButton(col2, "RIGHT", SpreadCommand)
            End If
        End With
    End Sub

    Public Sub ShowPreviousPage(ByRef SpreadView As AxFPSpreadADO.AxfpSpread, ByRef SpreadPreview As AxFPSpreadADO.AxfpSpreadPreview, ByRef SpreadCommand As AxFPSpreadADO.AxfpSpread, ByRef col2 As Integer)

        With SpreadPreview
            If .PageCurrent > 1 Then
                .PageCurrent = .PageCurrent - .PagesPerScreen
                EnableButton(col2, "LEFT", SpreadCommand)
                EnableButton(2, "RIGHT", SpreadCommand)
                'Update page count listing
                UpdatePageCount(SpreadView, SpreadPreview, SpreadCommand)
            End If

            'If at first page, disable button
            If .PageCurrent = 1 Then
                DisableButton(col2, "LEFT", SpreadCommand)
            End If
        End With
    End Sub
    Public Function ExportSprdToExcel(ByRef pCommonDialog1 As System.Windows.Forms.OpenFileDialog) As String



        With pCommonDialog1
            .FileName = ""
            .Filter = "Report Files (*.xls)|*.xls|(*.html)|*.html|All Files|*.*"
            .FilterIndex = 1 ' Use *.rpt as the default
            .InitialDirectory = "C:\MY Documents\"
            .Title = "Export to Excel"
            '.action = 1

            ''.InitDir = "C:\MY Documents\"
            ''.DialogTitle = "Export to Excel"
            ''.CancelError = True
            ''.Action = 2

            ExportSprdToExcel = .FileName
        End With
    End Function
    Public Sub SetupToolbar(ByRef pSpreadCommand As AxFPSpreadADO.AxfpSpread)
        Dim I As Short

        'Specify whether Edit Mode is to remain on when switching between cells

        Dim bRet As Boolean
        With pSpreadCommand
            .EditModePermanent = True

            .Col = -1
            .Row = -1
            .Lock = True

            'Set the number of rows in the spreadsheet
            .MaxRows = 1

            'Set the height of a selected row
            .set_RowHeight(0, 15)

            'Set the number of columns in the spreadsheet
            .MaxCols = 19

            'Set the column widths
            For I = 1 To .MaxCols Step 2
                .set_ColWidth(I, 0.3)
            Next I

            'Resize wide column
            .set_ColWidth(14, 10)      ''.ColWidth(14) = 10

            'Show or hide the column headers
            .DisplayColHeaders = False
            .DisplayRowHeaders = False

            'Turn off scroll bars
            .ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsNone

            'Turn off border
            .BorderStyle = FPSpreadADO.BorderStyleConstants.BorderStyleNone

            'Select row(s)
            .Row = 1
            .Col = -1

            'Determine the color of background, foreground and border color
            .ForeColor = Color.Black  ''RGB(0, 0, 0)
            .BackColor = Color.Silver  'RGB(192, 192, 192)
            .FontName = "MS Sans Serif"
            .FontSize = 8
            .FontBold = False

            'Select a single cell
            .Col = 2
            .Row = 1

            'Define cells as type BUTTON
            .CellType = SS_CELL_TYPE_BUTTON
            .Lock = False
            .TypeButtonText = "Next"
            .TypeButtonPicture = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\PICTURE\RIGHT.BMP")
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT

            'Select a single cell
            .Col = 4
            .Row = 1

            'Define cells as type BUTTON
            .CellType = SS_CELL_TYPE_BUTTON
            .Lock = False
            .TypeButtonText = "Previous"
            .TypeButtonPicture = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\PICTURE\LEFT.BMP")
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_RIGHT

            'Select a single cell
            .Col = 6
            .Row = 1

            'Define cells as type BUTTON
            .CellType = SS_CELL_TYPE_BUTTON
            .Lock = False
            .TypeButtonText = "Zoom"
            .TypeButtonPicture = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\PICTURE\ZOOM.BMP")
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_RIGHT

            'Select a single cell
            .Col = 8
            .Row = 1

            'Define cells as type BUTTON
            .CellType = SS_CELL_TYPE_BUTTON
            .Lock = False
            .TypeButtonText = "Print"
            .TypeButtonPicture = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\PICTURE\PRINT.BMP")
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_RIGHT

            'Select a single cell
            .Col = 10
            .Row = 1

            'Define cells as type BUTTON
            .CellType = SS_CELL_TYPE_BUTTON
            .Lock = False
            .TypeButtonText = "Export"
            .TypeButtonPicture = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\PICTURE\PrintSETUP.BMP")
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_RIGHT

            'Select a single cell
            .Col = 12
            .Row = 1

            'Define cells as type BUTTON
            .CellType = SS_CELL_TYPE_BUTTON
            .Lock = False
            .TypeButtonText = "eMail"
            .TypeButtonPicture = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\PICTURE\eMail.BMP")
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_RIGHT

            'Select a single cell
            .Col = 18
            .Row = 1

            'Define cells as type BUTTON
            .CellType = SS_CELL_TYPE_BUTTON
            .Lock = False
            .TypeButtonText = "Close"
            .TypeButtonPicture = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\PICTURE\CLOSED.BMP")
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_RIGHT
            .TextTip = FPSpreadADO.TextTipConstants.TextTipFloating
            bRet = .SetTextTipAppearance("MS Sans Serif", 8, 0, 0, &HC0FFFF, &H0)
            .CursorType = FPSpreadADO.CursorTypeConstants.CursorTypeLockedCell
            .CursorStyle = FPSpreadADO.CursorStyleConstants.CursorStyleArrow
            .NoBeep = True
        End With
    End Sub
    Public Sub DisableButton(ByRef Col As Integer, ByRef bitmapdirection As String, ByRef pSpreadCommand As AxFPSpreadADO.AxfpSpread)
        'Disable specified button
        With pSpreadCommand
            .ReDraw = False

            .Row = 1
            .Col = Col

            .Lock = True
            .TypeButtonTextColor = Color.Gray '    RGB(128, 128, 128)
            .Protect = True
            .TypeButtonPicture = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\PICTURE\" & bitmapdirection & "DIS.BMP")

            .ReDraw = True
        End With

    End Sub
    Public Sub UpdatePageCount(ByRef pSpreadView As AxFPSpreadADO.AxfpSpread, ByRef pSpreadPreview As AxFPSpreadADO.AxfpSpreadPreview, ByRef pSpreadCommand As AxFPSpreadADO.AxfpSpread)
        'Page Count
        With pSpreadCommand
            .Row = 1
            .Col = 14
            .Text = "Page " & pSpreadPreview.PageCurrent & " of " & pSpreadView.PrintPageCount
        End With
    End Sub
    Public Sub EnableButton(ByRef Col As Integer, ByRef bitmapdirection As String, ByRef pSpreadCommand As Object)
        'Enable specified button
        With pSpreadCommand
            .Redraw = False

            .Row = 1
            .Col = Col

            .Lock = False
            .TypeButtonTextColor = RGB(0, 0, 0)
            .Protect = False
            .TypeButtonPicture = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\PICTURE\" & bitmapdirection & ".BMP")

            .Redraw = True
        End With
    End Sub
End Module
