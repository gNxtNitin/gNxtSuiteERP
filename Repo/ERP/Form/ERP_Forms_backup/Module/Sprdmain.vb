Option Strict Off
Option Explicit On
Module FormatSpread
    '----------------------------------------------------------
    '
    ' File: SSOCX.BAS
    '
    ' Copyright (C) 1996 FarPoint Technologies.
    ' All rights reserved.
    '
    '----------------------------------------------------------

    '' Action property settings
    'Public Const SS_ACTION_ACTIVE_CELL As Short = 0
    'Public Const SS_ACTION_GOTO_CELL As Short = 1
    'Public Const SS_ACTION_SELECT_BLOCK As Short = 2
    'Public Const SS_ACTION_CLEAR As Short = 3
    'Public Const SS_ACTION_DELETE_COL As Short = 4
    'Public Const SS_ACTION_DELETE_ROW As Short = 5
    'Public Const SS_ACTION_INSERT_COL As Short = 6
    'Public Const SS_ACTION_INSERT_ROW As Short = 7
    'Public Const SS_ACTION_RECALC As Short = 11
    'Public Const SS_ACTION_CLEAR_TEXT As Short = 12
    'Public Const SS_ACTION_PRINT As Short = 13
    'Public Const SS_ACTION_DESELECT_BLOCK As Short = 14
    'Public Const SS_ACTION_DSAVE As Short = 15
    'Public Const SS_ACTION_SET_CELL_BORDER As Short = 16
    'Public Const SS_ACTION_ADD_MULTISELBLOCK As Short = 17
    'Public Const SS_ACTION_GET_MULTI_SELECTION As Short = 18
    'Public Const SS_ACTION_COPY_RANGE As Short = 19
    'Public Const SS_ACTION_MOVE_RANGE As Short = 20
    'Public Const SS_ACTION_SWAP_RANGE As Short = 21
    'Public Const SS_ACTION_CLIPBOARD_COPY As Short = 22
    'Public Const SS_ACTION_CLIPBOARD_CUT As Short = 23
    'Public Const SS_ACTION_CLIPBOARD_PASTE As Short = 24
    'Public Const SS_ACTION_SORT As Short = 25
    'Public Const SS_ACTION_COMBO_CLEAR As Short = 26
    'Public Const SS_ACTION_COMBO_REMOVE As Short = 27
    'Public Const SS_ACTION_RESET As Short = 28
    'Public Const SS_ACTION_SEL_MODE_CLEAR As Short = 29
    'Public Const SS_ACTION_VMODE_REFRESH As Short = 30
    'Public Const SS_ACTION_SMARTPRINT As Short = 32

    '' SelectBlockOptions property settings
    'Public Const SS_SELBLOCKOPT_COLS As Short = 1
    'Public Const SS_SELBLOCKOPT_ROWS As Short = 2
    'Public Const SS_SELBLOCKOPT_BLOCKS As Short = 4
    'Public Const SS_SELBLOCKOPT_ALL As Short = 8

    '' DAutoSize property settings
    'Public Const SS_AUTOSIZE_NO As Short = 0
    'Public Const SS_AUTOSIZE_MAX_COL_WIDTH As Short = 1
    'Public Const SS_AUTOSIZE_BEST_GUESS As Short = 2

    '' BackColorStyle property settings
    'Public Const SS_BACKCOLORSTYLE_OVERGRID As Short = 0
    'Public Const SS_BACKCOLORSTYLE_UNDERGRID As Short = 1

    '' CellType property settings
    'Public Const SS_CELL_TYPE_DATE As Short = 0
    'Public Const SS_CELL_TYPE_EDIT As Short = 1
    'Public Const SS_CELL_TYPE_FLOAT As Short = 2
    'Public Const SS_CELL_TYPE_INTEGER As Short = 3
    'Public Const SS_CELL_TYPE_PIC As Short = 4
    'Public Const SS_CELL_TYPE_STATIC_TEXT As Short = 5
    'Public Const SS_CELL_TYPE_TIME As Short = 6
    'Public Const SS_CELL_TYPE_BUTTON As Short = 7
    'Public Const SS_CELL_TYPE_COMBOBOX As Short = 8
    'Public Const SS_CELL_TYPE_PICTURE As Short = 9
    'Public Const SS_CELL_TYPE_CHECKBOX As Short = 10
    'Public Const SS_CELL_TYPE_OWNER_DRAWN As Short = 11

    '' CellBorderType property settings
    'Public Const SS_BORDER_TYPE_NONE As Short = 0
    'Public Const SS_BORDER_TYPE_OUTLINE As Short = 16
    'Public Const SS_BORDER_TYPE_LEFT As Short = 1
    'Public Const SS_BORDER_TYPE_RIGHT As Short = 2
    'Public Const SS_BORDER_TYPE_TOP As Short = 4
    'Public Const SS_BORDER_TYPE_BOTTOM As Short = 8

    '' CellBorderStyle property settings
    'Public Const SS_BORDER_STYLE_DEFAULT As Short = 0
    'Public Const SS_BORDER_STYLE_SOLID As Short = 1
    'Public Const SS_BORDER_STYLE_DASH As Short = 2
    'Public Const SS_BORDER_STYLE_DOT As Short = 3
    'Public Const SS_BORDER_STYLE_DASH_DOT As Short = 4
    'Public Const SS_BORDER_STYLE_DASH_DOT_DOT As Short = 5
    'Public Const SS_BORDER_STYLE_BLANK As Short = 6
    'Public Const SS_BORDER_STYLE_FINE_SOLID As Short = 11
    'Public Const SS_BORDER_STYLE_FINE_DASH As Short = 12
    'Public Const SS_BORDER_STYLE_FINE_DOT As Short = 13
    'Public Const SS_BORDER_STYLE_FINE_DASH_DOT As Short = 14
    'Public Const SS_BORDER_STYLE_FINE_DASH_DOT_DOT As Short = 15

    '' ColHeaderDisplay and RowHeaderDisplay property settings
    'Public Const SS_HEADER_BLANK As Short = 0
    'Public Const SS_HEADER_NUMBERS As Short = 1
    'Public Const SS_HEADER_LETTERS As Short = 2

    '' TypeCheckTextAlign property settings
    'Public Const SS_CHECKBOX_TEXT_LEFT As Short = 0
    'Public Const SS_CHECKBOX_TEXT_RIGHT As Short = 1

    '' CursorStyle property settings
    'Public Const SS_CURSOR_STYLE_USER_DEFINED As Short = 0
    'Public Const SS_CURSOR_STYLE_DEFAULT As Short = 1
    'Public Const SS_CURSOR_STYLE_ARROW As Short = 2
    'Public Const SS_CURSOR_STYLE_DEFCOLRESIZE As Short = 3
    'Public Const SS_CURSOR_STYLE_DEFROWRESIZE As Short = 4

    '' CursorType property settings
    'Public Const SS_CURSOR_TYPE_DEFAULT As Short = 0
    'Public Const SS_CURSOR_TYPE_COLRESIZE As Short = 1
    'Public Const SS_CURSOR_TYPE_ROWRESIZE As Short = 2
    'Public Const SS_CURSOR_TYPE_BUTTON As Short = 3
    'Public Const SS_CURSOR_TYPE_GRAYAREA As Short = 4
    'Public Const SS_CURSOR_TYPE_LOCKEDCELL As Short = 5
    'Public Const SS_CURSOR_TYPE_COLHEADER As Short = 6
    'Public Const SS_CURSOR_TYPE_ROWHEADER As Short = 7

    '' OperationMode property settings
    'Public Const SS_OP_MODE_NORMAL As Short = 0
    'Public Const SS_OP_MODE_READONLY As Short = 1
    'Public Const SS_OP_MODE_ROWMODE As Short = 2
    'Public Const SS_OP_MODE_SINGLE_SELECT As Short = 3
    'Public Const SS_OP_MODE_MULTI_SELECT As Short = 4
    'Public Const SS_OP_MODE_EXT_SELECT As Short = 5

    '' SortKeyOrder property settings
    'Public Const SS_SORT_ORDER_NONE As Short = 0
    'Public Const SS_SORT_ORDER_ASCENDING As Short = 1
    'Public Const SS_SORT_ORDER_DESCENDING As Short = 2

    '' SortBy property settings
    'Public Const SS_SORT_BY_ROW As Short = 0
    'Public Const SS_SORT_BY_COL As Short = 1

    '' UserResize property settings
    'Public Const SS_USER_RESIZE_COL As Short = 1
    'Public Const SS_USER_RESIZE_ROW As Short = 2

    '' UserResizeCol and UserResizeRow property settings
    'Public Const SS_USER_RESIZE_DEFAULT As Short = 0
    'Public Const SS_USER_RESIZE_ON As Short = 1
    'Public Const SS_USER_RESIZE_OFF As Short = 2

    '' VScrollSpecialType property settings
    'Public Const SS_VSCROLLSPECIAL_NO_HOME_END As Short = 1
    'Public Const SS_VSCROLLSPECIAL_NO_PAGE_UP_DOWN As Short = 2
    'Public Const SS_VSCROLLSPECIAL_NO_LINE_UP_DOWN As Short = 4

    '' Position property settings
    'Public Const SS_POSITION_UPPER_LEFT As Short = 0
    'Public Const SS_POSITION_UPPER_CENTER As Short = 1
    'Public Const SS_POSITION_UPPER_RIGHT As Short = 2
    'Public Const SS_POSITION_CENTER_LEFT As Short = 3
    'Public Const SS_POSITION_CENTER_CENTER As Short = 4
    'Public Const SS_POSITION_CENTER_RIGHT As Short = 5
    'Public Const SS_POSITION_BOTTOM_LEFT As Short = 6
    'Public Const SS_POSITION_BOTTOM_CENTER As Short = 7
    'Public Const SS_POSITION_BOTTOM_RIGHT As Short = 8

    '' ScrollBars property settings
    'Public Const SS_SCROLLBAR_NONE As Short = 0
    'Public Const SS_SCROLLBAR_H_ONLY As Short = 1
    'Public Const SS_SCROLLBAR_V_ONLY As Short = 2
    'Public Const SS_SCROLLBAR_BOTH As Short = 3

    '' PrintOrientation property settings
    'Public Const SS_PRINTORIENT_DEFAULT As Short = 0
    'Public Const SS_PRINTORIENT_PORTRAIT As Short = 1
    'Public Const SS_PRINTORIENT_LANDSCAPE As Short = 2

    '' PrintType property settings
    'Public Const SS_PRINT_ALL As Short = 0
    'Public Const SS_PRINT_CELL_RANGE As Short = 1
    'Public Const SS_PRINT_CURRENT_PAGE As Short = 2
    'Public Const SS_PRINT_PAGE_RANGE As Short = 3

    '' TypeButtonType property settings
    'Public Const SS_CELL_BUTTON_NORMAL As Short = 0
    'Public Const SS_CELL_BUTTON_TWO_STATE As Short = 1

    '' TypeButtonAlign property settings
    'Public Const SS_CELL_BUTTON_ALIGN_BOTTOM As Short = 0
    'Public Const SS_CELL_BUTTON_ALIGN_TOP As Short = 1
    'Public Const SS_CELL_BUTTON_ALIGN_LEFT As Short = 2
    'Public Const SS_CELL_BUTTON_ALIGN_RIGHT As Short = 3

    '' ButtonDrawMode property settings
    'Public Const SS_BDM_ALWAYS As Short = 0
    'Public Const SS_BDM_CURRENT_CELL As Short = 1
    'Public Const SS_BDM_CURRENT_COLUMN As Short = 2
    'Public Const SS_BDM_CURRENT_ROW As Short = 4

    '' TypeDateFormat property settings
    'Public Const SS_CELL_DATE_FORMAT_DDMONYY As Short = 0
    'Public Const SS_CELL_DATE_FORMAT_DDMMYY As Short = 1
    'Public Const SS_CELL_DATE_FORMAT_MMDDYY As Short = 2
    'Public Const SS_CELL_DATE_FORMAT_YYMMDD As Short = 3

    '' TypeEditCharCase property settings
    'Public Const SS_CELL_EDIT_CASE_LOWER_CASE As Short = 0
    'Public Const SS_CELL_EDIT_CASE_NO_CASE As Short = 1
    'Public Const SS_CELL_EDIT_CASE_UPPER_CASE As Short = 2

    '' TypeEditCharSet property settings
    'Public Const SS_CELL_EDIT_CHAR_SET_ASCII As Short = 0
    'Public Const SS_CELL_EDIT_CHAR_SET_ALPHA As Short = 1
    'Public Const SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC As Short = 2
    'Public Const SS_CELL_EDIT_CHAR_SET_NUMERIC As Short = 3

    '' TypeTextAlignVert property settings
    'Public Const SS_CELL_STATIC_V_ALIGN_BOTTOM As Short = 0
    'Public Const SS_CELL_STATIC_V_ALIGN_CENTER As Short = 1
    'Public Const SS_CELL_STATIC_V_ALIGN_TOP As Short = 2

    '' TypeTime24Hour property settings
    'Public Const SS_CELL_TIME_12_HOUR_CLOCK As Short = 0
    'Public Const SS_CELL_TIME_24_HOUR_CLOCK As Short = 1

    ''Unit type
    'Public Const SS_CELL_UNIT_NORMAL As Short = 0
    'Public Const SS_CELL_UNIT_VGA As Short = 1
    'Public Const SS_CELL_UNIT_TWIPS As Short = 2

    '' TypeHAlign property settings
    'Public Const SS_CELL_H_ALIGN_LEFT As Short = 0
    'Public Const SS_CELL_H_ALIGN_RIGHT As Short = 1
    'Public Const SS_CELL_H_ALIGN_CENTER As Short = 2

    '' EditEnterAction property settings
    'Public Const SS_CELL_EDITMODE_EXIT_NONE As Short = 0
    'Public Const SS_CELL_EDITMODE_EXIT_UP As Short = 1
    'Public Const SS_CELL_EDITMODE_EXIT_DOWN As Short = 2
    'Public Const SS_CELL_EDITMODE_EXIT_LEFT As Short = 3
    'Public Const SS_CELL_EDITMODE_EXIT_RIGHT As Short = 4
    'Public Const SS_CELL_EDITMODE_EXIT_NEXT As Short = 5
    'Public Const SS_CELL_EDITMODE_EXIT_PREVIOUS As Short = 6
    'Public Const SS_CELL_EDITMODE_EXIT_SAME As Short = 7
    'Public Const SS_CELL_EDITMODE_EXIT_NEXTROW As Short = 8

    '' Custom function parameter type used with CFGetParamInfo method
    'Public Const SS_VALUE_TYPE_LONG As Short = 0
    'Public Const SS_VALUE_TYPE_DOUBLE As Short = 1
    'Public Const SS_VALUE_TYPE_STR As Short = 2
    'Public Const SS_VALUE_TYPE_CELL As Short = 3
    'Public Const SS_VALUE_TYPE_RANGE As Short = 4

    '' Custom function parameter status used with CFGetParamInfo method
    'Public Const SS_VALUE_STATUS_OK As Short = 0
    'Public Const SS_VALUE_STATUS_ERROR As Short = 1
    'Public Const SS_VALUE_STATUS_EMPTY As Short = 2

    '' Reference style settings used with GetRefStyle/SetRefStyle methods
    'Public Const SS_REFSTYLE_DEFAULT As Short = 0
    'Public Const SS_REFSTYLE_A1 As Short = 1
    'Public Const SS_REFSTYLE_R1C1 As Short = 2

    '' Options used with Flags parameter of AddCustomFunctionExt method
    'Public Const SS_CUSTFUNC_WANTCELLREF As Short = 1
    'Public Const SS_CUSTFUNC_WANTRANGEREF As Short = 2




    Public Sub FormatSprdSheet(ByRef sprd As Object, ByRef col2 As Integer, ByRef mColWidth As Double, ByRef mCellType As FPSpreadADO.CellTypeConstants, ByRef mTypeEditLen As Integer, ByRef mHAlign As FPSpreadADO.TypeHAlignConstants, ByRef mEditCharSet As FPSpreadADO.TypeEditCharSetConstants, ByRef mEditCharCase As FPSpreadADO.TypeEditCharCaseConstants, ByRef mProtectCell As Boolean, ByRef mFloatDecimalPlaces As Integer, ByRef mHiddenCol As Boolean, ByRef mRowHeight As Double, ByRef mValue As System.Windows.Forms.CheckState, ByRef Arow As Integer, ByRef mMultiLine As Boolean)
        Dim MainClass_Renamed As Object

        On Error GoTo ErrPart

        With sprd
            .RowHeight(Arow) = mRowHeight
            .Row = Arow
            .Col = col2

            .CellType = mCellType
            .TypeHAlign = mHAlign
            .ColWidth(col2) = mColWidth
            .ColHidden = mHiddenCol

            Select Case mCellType

                Case 0
                    .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
                    .TypeDateCentury = True
                    .TypeDateMin = "01011990"
                    .TypeDateMax = "01312030"
                Case 1
                    .TypeEditCharSet = mEditCharSet
                    .TypeEditCharCase = mEditCharCase
                    .TypeMaxEditLen = mTypeEditLen
                    .TypeEditMultiLine = mMultiLine
                Case 2
                    .TypeFloatMax = "99999999999.99"
                    .TypeFloatMin = "-99999999999.99"
                    .TypeFloatSeparator = True
                    .TypeFloatDecimalPlaces = mFloatDecimalPlaces
                    .FloatDefSepChar = Asc(".")
                Case 3
                    .TypeIntegerMin = 0
                    .TypeIntegerMax = 999999999
                Case 8
                    .Value = mValue
                Case 10
                    .TypeCheckCenter = True
                    '.TypeCheckType = SS_CHECK_BOX_THREE_STATE
            End Select

            If mProtectCell = True Then
                MainClass.ProtectCell(sprd, Arow, .MaxRows, col2, col2)
            End If

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Public Sub CalcRowTotal(ByRef sprd As Object, ByRef mCol1 As Integer, ByRef mRow1 As Integer, ByRef mCol2 As Integer, ByRef mRow2 As Integer, ByRef mResultRow As Integer, ByRef mResultCol As Integer, ByRef Optional mIsStockCol As String = "N")
        Dim I As String
        Dim j As String

        Dim z As String

        If mIsStockCol = "Y" Then
            With sprd
                I = .ColNumberToLetter(mCol1) & mRow1
                j = .ColNumberToLetter(mCol2) & mRow2 - 1
                z = .ColNumberToLetter(mCol2) & mRow2
                .Row = mResultRow
                .Col = mResultCol
                .Formula = "SUM(" & I & ":" & j & ") - SUM(" & z & ":" & z & ")"
                .FontBold = True
            End With
        Else
            With sprd
                I = .ColNumberToLetter(mCol1) & mRow1
                j = .ColNumberToLetter(mCol2) & mRow2
                .Row = mResultRow
                .Col = mResultCol
                .Formula = "SUM(" & I & ":" & j & ")"
                .FontBold = True
            End With
        End If

    End Sub

    Public Sub SprdHeading(ByRef sprd As Object, ByRef Row2 As Integer, ByRef col2 As Integer, ByRef mHeading As String)

        With sprd
            .Row = Row2
            .Col = col2
            .Text = mHeading
            .FontBold = True
        End With
    End Sub
End Module
