Option Strict Off
Option Explicit On
Imports System.Collections.Generic
Public Module JSON
    ' VBJSON is a VB6 adaptation of the VBA JSON project at http://code.google.com/p/vba-json/
    ' Some bugs fixed, speed improvements added for VB6 by Michael Glaser (vbjson@ediy.co.nz)
    ' BSD Licensed


    Const INVALID_JSON As Integer = 1
    Const INVALID_OBJECT As Integer = 2
    Const INVALID_ARRAY As Integer = 3
    Const INVALID_BOOLEAN As Integer = 4
    Const INVALID_NULL As Integer = 5
    Const INVALID_KEY As Integer = 6
    Const INVALID_RPC_CALL As Integer = 7

    Private psErrors As String

    Public Function GetParserErrors() As String
        GetParserErrors = psErrors
    End Function

    Public Function ClearParserErrors() As String
        psErrors = ""
        ClearParserErrors = ""
    End Function


    '
    '   parse string and create JSON object
    '
    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function parse(ByRef str_Renamed As String) As Object

        Dim Index As Integer
        Index = 1
        psErrors = ""
        On Error Resume Next
        Call skipChar(str_Renamed, Index)
        Select Case Mid(str_Renamed, Index, 1)
            Case "{"
                parse = ParseObject(str_Renamed, Index)
            Case "["
                parse = ParseArray(str_Renamed, Index)
            Case Else
                psErrors = "Invalid JSON"
        End Select


    End Function

    '
    '   parse collection of key/value
    '
    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Function ParseObject(ByRef str_Renamed As String, ByRef Index As Integer) As Dictionary(Of String, String)

        ParseObject = New Dictionary(Of String, String)        'Scripting.Dictionary
        Dim sKey As String

        ' "{"
        Call skipChar(str_Renamed, Index)
        If Mid(str_Renamed, Index, 1) <> "{" Then
            psErrors = psErrors & "Invalid Object at position " & Index & " : " & Mid(str_Renamed, Index) & vbCrLf
            Exit Function
        End If

        Index = Index + 1

        Do
            Call skipChar(str_Renamed, Index)
            If "}" = Mid(str_Renamed, Index, 1) Then
                Index = Index + 1
                Exit Do
            ElseIf "," = Mid(str_Renamed, Index, 1) Then
                Index = Index + 1
                Call skipChar(str_Renamed, Index)
            ElseIf Index > Len(str_Renamed) Then
                psErrors = psErrors & "Missing '}': " & Right(str_Renamed, 20) & vbCrLf
                Exit Do
            End If


            ' add key/value pair
            sKey = parseKey(str_Renamed, Index)
            On Error Resume Next

            ParseObject.Add(sKey, ParseValue(str_Renamed, Index))
            If Err.Number <> 0 Then
                psErrors = psErrors & Err.Description & ": " & sKey & vbCrLf
                Exit Do
            End If
        Loop
eh:

    End Function

    '
    '   parse list
    '
    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Function ParseArray(ByRef str_Renamed As String, ByRef Index As Integer) As Collection

        ParseArray = New Collection

        ' "["
        Call skipChar(str_Renamed, Index)
        If Mid(str_Renamed, Index, 1) <> "[" Then
            psErrors = psErrors & "Invalid Array at position " & Index & " : " & Mid(str_Renamed, Index, 20) & vbCrLf
            Exit Function
        End If

        Index = Index + 1

        Do

            Call skipChar(str_Renamed, Index)
            If "]" = Mid(str_Renamed, Index, 1) Then
                Index = Index + 1
                Exit Do
            ElseIf "," = Mid(str_Renamed, Index, 1) Then
                Index = Index + 1
                Call skipChar(str_Renamed, Index)
            ElseIf Index > Len(str_Renamed) Then
                psErrors = psErrors & "Missing ']': " & Right(str_Renamed, 20) & vbCrLf
                Exit Do
            End If

            ' add value
            On Error Resume Next
            ParseArray.Add(ParseValue(str_Renamed, Index))
            If Err.Number <> 0 Then
                psErrors = psErrors & Err.Description & ": " & Mid(str_Renamed, Index, 20) & vbCrLf
                Exit Do
            End If
        Loop

    End Function

    '
    '   parse string / number / object / array / true / false / null
    '
    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Function ParseValue(ByRef str_Renamed As String, ByRef Index As Integer) As Object

        Call skipChar(str_Renamed, Index)

        Select Case Mid(str_Renamed, Index, 1)
            Case "{"
                ParseValue = ParseObject(str_Renamed, Index)
            Case "["
                ParseValue = ParseArray(str_Renamed, Index)
            Case """", "'"
                ParseValue = ParseString(str_Renamed, Index)
            Case "t", "f"
                ParseValue = parseBoolean(str_Renamed, Index)
            Case "n"
                'UPGRADE_WARNING: Couldn't resolve default property of object parseNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                ParseValue = parseNull(str_Renamed, Index)
            Case Else
                'UPGRADE_WARNING: Couldn't resolve default property of object ParseNumber(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                ParseValue = ParseNumber(str_Renamed, Index)
        End Select

    End Function

    '
    '   parse string
    '
    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Function ParseString(ByRef str_Renamed As String, ByRef Index As Integer) As String

        Dim QUOTE As String
        'UPGRADE_NOTE: Char was upgraded to Char_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
        Dim Char_Renamed As String
        Dim Code As String

        Dim SB As New cStringBuilder

        Call skipChar(str_Renamed, Index)
        QUOTE = Mid(str_Renamed, Index, 1)
        Index = Index + 1

        Do While Index > 0 And Index <= Len(str_Renamed)
            Char_Renamed = Mid(str_Renamed, Index, 1)
            Select Case (Char_Renamed)
                Case "\"
                    Index = Index + 1
                    Char_Renamed = Mid(str_Renamed, Index, 1)
                    Select Case (Char_Renamed)
                        Case """", "\", "/", "'"
                            SB.Append(Char_Renamed)
                            Index = Index + 1
                        Case "b"
                            SB.Append(vbBack)
                            Index = Index + 1
                        Case "f"
                            SB.Append(vbFormFeed)
                            Index = Index + 1
                        Case "n"
                            SB.Append(vbLf)
                            Index = Index + 1
                        Case "r"
                            SB.Append(vbCr)
                            Index = Index + 1
                        Case "t"
                            SB.Append(vbTab)
                            Index = Index + 1
                        Case "u"
                            Index = Index + 1
                            Code = Mid(str_Renamed, Index, 4)
                            SB.Append(ChrW(Val("&h" & Code)))
                            Index = Index + 4
                    End Select
                Case QUOTE
                    Index = Index + 1

                    ParseString = SB.toString_Renamed
                    'UPGRADE_NOTE: Object SB may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    SB = Nothing

                    Exit Function

                Case Else
                    SB.Append(Char_Renamed)
                    Index = Index + 1
            End Select
        Loop

        ParseString = SB.toString_Renamed
        'UPGRADE_NOTE: Object SB may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        SB = Nothing

    End Function

    '
    '   parse number
    '
    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Function ParseNumber(ByRef str_Renamed As String, ByRef Index As Integer) As Object

        Dim Value As String
        'UPGRADE_NOTE: Char was upgraded to Char_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
        Dim Char_Renamed As String

        Call skipChar(str_Renamed, Index)
        Do While Index > 0 And Index <= Len(str_Renamed)
            Char_Renamed = Mid(str_Renamed, Index, 1)
            If InStr("+-0123456789.eE", Char_Renamed) Then
                Value = Value & Char_Renamed
                Index = Index + 1
            Else
                ParseNumber = CDec(Value)
                Exit Function
            End If
        Loop
    End Function

    '
    '   parse true / false
    '
    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Function parseBoolean(ByRef str_Renamed As String, ByRef Index As Integer) As Boolean

        Call skipChar(str_Renamed, Index)
        If Mid(str_Renamed, Index, 4) = "true" Then
            parseBoolean = True
            Index = Index + 4
        ElseIf Mid(str_Renamed, Index, 5) = "false" Then
            parseBoolean = False
            Index = Index + 5
        Else
            psErrors = psErrors & "Invalid Boolean at position " & Index & " : " & Mid(str_Renamed, Index) & vbCrLf
        End If

    End Function

    '
    '   parse null
    '
    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Function parseNull(ByRef str_Renamed As String, ByRef Index As Integer) As Object

        Call skipChar(str_Renamed, Index)
        If Mid(str_Renamed, Index, 4) = "null" Then
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            'UPGRADE_WARNING: Couldn't resolve default property of object parseNull. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            parseNull = System.DBNull.Value
            Index = Index + 4
        Else
            psErrors = psErrors & "Invalid null value at position " & Index & " : " & Mid(str_Renamed, Index) & vbCrLf
        End If

    End Function

    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Function parseKey(ByRef str_Renamed As String, ByRef Index As Integer) As String

        Dim dquote As Boolean
        Dim squote As Boolean
        'UPGRADE_NOTE: Char was upgraded to Char_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
        Dim Char_Renamed As String

        Call skipChar(str_Renamed, Index)
        Do While Index > 0 And Index <= Len(str_Renamed)
            Char_Renamed = Mid(str_Renamed, Index, 1)
            Select Case (Char_Renamed)
                Case """"
                    dquote = Not dquote
                    Index = Index + 1
                    If Not dquote Then
                        Call skipChar(str_Renamed, Index)
                        If Mid(str_Renamed, Index, 1) <> ":" Then
                            psErrors = psErrors & "Invalid Key at position " & Index & " : " & parseKey & vbCrLf
                            Exit Do
                        End If
                    End If
                Case "'"
                    squote = Not squote
                    Index = Index + 1
                    If Not squote Then
                        Call skipChar(str_Renamed, Index)
                        If Mid(str_Renamed, Index, 1) <> ":" Then
                            psErrors = psErrors & "Invalid Key at position " & Index & " : " & parseKey & vbCrLf
                            Exit Do
                        End If
                    End If
                Case ":"
                    Index = Index + 1
                    If Not dquote And Not squote Then
                        Exit Do
                    Else
                        parseKey = parseKey & Char_Renamed
                    End If
                Case Else
                    If InStr(vbCrLf & vbCr & vbLf & vbTab & " ", Char_Renamed) Then
                    Else
                        parseKey = parseKey & Char_Renamed
                    End If
                    Index = Index + 1
            End Select
        Loop

    End Function

    '
    '   skip special character
    '
    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub skipChar(ByRef str_Renamed As String, ByRef Index As Integer)
        Dim bComment As Boolean
        Dim bStartComment As Boolean
        Dim bLongComment As Boolean
        Do While Index > 0 And Index <= Len(str_Renamed)
            Select Case Mid(str_Renamed, Index, 1)
                Case vbCr, vbLf
                    If Not bLongComment Then
                        bStartComment = False
                        bComment = False
                    End If

                Case vbTab, " ", "(", ")"

                Case "/"
                    If Not bLongComment Then
                        If bStartComment Then
                            bStartComment = False
                            bComment = True
                        Else
                            bStartComment = True
                            bComment = False
                            bLongComment = False
                        End If
                    Else
                        If bStartComment Then
                            bLongComment = False
                            bStartComment = False
                            bComment = False
                        End If
                    End If

                Case "*"
                    If bStartComment Then
                        bStartComment = False
                        bComment = True
                        bLongComment = True
                    Else
                        bStartComment = True
                    End If

                Case Else
                    If Not bComment Then
                        Exit Do
                    End If
            End Select

            Index = Index + 1
        Loop

    End Sub

    'UPGRADE_NOTE: toString was upgraded to toString_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function toString_Renamed(ByRef obj As Object) As String
        Dim SB As New cStringBuilder
        Dim bFI As Boolean
        Dim i As Integer
        Dim keys As Object
        Dim key As Object
        Dim Value As Object
        Dim sEB As Object
        'UPGRADE_WARNING: VarType has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        Select Case VarType(obj)
            Case VariantType.Null
                SB.Append("null")
            Case VariantType.Date
                'UPGRADE_WARNING: Couldn't resolve default property of object obj. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                SB.Append("""" & CStr(obj) & """")
            Case VariantType.String
                SB.Append("""" & Encode(obj) & """")
            Case VariantType.Object


                bFI = True
                'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                If TypeName(obj) = "Dictionary" Then

                    SB.Append("{")
                    'UPGRADE_WARNING: Couldn't resolve default property of object obj.keys. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object keys. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    keys = obj.keys
                    'UPGRADE_WARNING: Couldn't resolve default property of object obj.Count. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    For i = 0 To obj.Count - 1
                        If bFI Then bFI = False Else SB.Append(",")
                        'UPGRADE_WARNING: Couldn't resolve default property of object keys(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        key = keys(i)
                        'UPGRADE_WARNING: Couldn't resolve default property of object obj.Item. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        SB.Append("""" & key & """:" & toString_Renamed(obj.Item(key)))
                    Next i
                    SB.Append("}")

                    'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                ElseIf TypeName(obj) = "Collection" Then

                    SB.Append("[")
                    For Each Value In obj
                        If bFI Then bFI = False Else SB.Append(",")
                        SB.Append(toString_Renamed(Value))
                    Next Value
                    SB.Append("]")

                End If
            Case VariantType.Boolean
                'UPGRADE_WARNING: Couldn't resolve default property of object obj. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If obj Then SB.Append("true") Else SB.Append("false")
            Case VariantType.Object, VariantType.Array, VariantType.Array + VariantType.Object
                'UPGRADE_WARNING: Couldn't resolve default property of object multiArray(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                SB.Append(multiArray(obj, 1, "", sEB))
            Case Else
                'UPGRADE_WARNING: Couldn't resolve default property of object obj. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                SB.Append(Replace(obj, ",", "."))
        End Select

        toString_Renamed = SB.toString_Renamed
        'UPGRADE_NOTE: Object SB may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        SB = Nothing

    End Function

    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Function Encode(ByRef str_Renamed As Object) As String

        Dim SB As New cStringBuilder
        Dim i As Integer
        Dim j As Integer
        Dim aL1 As Object
        Dim aL2 As Object
        Dim c As String
        Dim p As Boolean

        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        'UPGRADE_WARNING: Couldn't resolve default property of object aL1. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        aL1 = New Object() {&H22, &H5C, &H2F, &H8, &HC, &HA, &HD, &H9}
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        'UPGRADE_WARNING: Couldn't resolve default property of object aL2. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        aL2 = New Object() {&H22, &H5C, &H2F, &H62, &H66, &H6E, &H72, &H74}
        Dim a As Object
        For i = 1 To Len(str_Renamed)
            p = True
            'UPGRADE_WARNING: Couldn't resolve default property of object str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            c = Mid(str_Renamed, i, 1)
            For j = 0 To 7
                'UPGRADE_WARNING: Couldn't resolve default property of object aL1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If c = Chr(aL1(j)) Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object aL2(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    SB.Append("\" & Chr(aL2(j)))
                    p = False
                    Exit For
                End If
            Next

            If p Then
                'UPGRADE_WARNING: Couldn't resolve default property of object a. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                a = AscW(c)
                'UPGRADE_WARNING: Couldn't resolve default property of object a. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If a > 31 And a < 127 Then
                    SB.Append(c)
                    'UPGRADE_WARNING: Couldn't resolve default property of object a. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                ElseIf a > -1 Or a < 65535 Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object a. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    SB.Append("\u" & New String("0", 4 - Len(Hex(a))) & Hex(a))
                End If
            End If
        Next

        Encode = SB.toString_Renamed
        'UPGRADE_NOTE: Object SB may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        SB = Nothing

    End Function

    Private Function multiArray(ByRef aBD As Object, ByRef iBC As Object, ByRef sPS As Object, ByRef sPT As Object) As Object ' Array BoDy, Integer BaseCount, String PoSition

        Dim iDU As Integer
        Dim iDL As Integer
        Dim i As Integer

        On Error Resume Next
        'UPGRADE_WARNING: Couldn't resolve default property of object iBC. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        iDL = LBound(aBD, iBC)
        'UPGRADE_WARNING: Couldn't resolve default property of object iBC. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        iDU = UBound(aBD, iBC)

        Dim SB As New cStringBuilder

        Dim sPB1, sPB2 As Object ' String PointBuffer1, String PointBuffer2
        If Err.Number = 9 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object sPS. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object sPT. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object sPB1. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sPB1 = sPT & sPS
            For i = 1 To Len(sPB1)
                'UPGRADE_WARNING: Couldn't resolve default property of object sPB2. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If i <> 1 Then sPB2 = sPB2 & ","
                'UPGRADE_WARNING: Couldn't resolve default property of object sPB1. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object sPB2. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                sPB2 = sPB2 & Mid(sPB1, i, 1)
            Next
            '        multiArray = multiArray & toString(Eval("aBD(" & sPB2 & ")"))
            SB.Append(toString_Renamed(aBD(sPB2)))
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object sPS. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object sPT. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sPT = sPT & sPS
            SB.Append("[")
            For i = iDL To iDU
                'UPGRADE_WARNING: Couldn't resolve default property of object iBC. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object multiArray(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                SB.Append(multiArray(aBD, iBC + 1, i, sPT))
                If i < iDU Then SB.Append(",")
            Next
            SB.Append("]")
            'UPGRADE_WARNING: Couldn't resolve default property of object iBC. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object sPT. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sPT = Left(sPT, iBC - 2)
        End If
        Err.Clear()
        'UPGRADE_WARNING: Couldn't resolve default property of object multiArray. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        multiArray = SB.toString_Renamed

        'UPGRADE_NOTE: Object SB may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        SB = Nothing
    End Function

    ' Miscellaneous JSON functions

    Public Function StringToJSON(ByRef st As String) As String

        Const FIELD_SEP As String = "~"
        Const RECORD_SEP As String = "|"

        Dim sFlds As String
        Dim sRecs As New cStringBuilder
        Dim lRecCnt As Integer
        Dim lFld As Integer
        Dim fld As Object
        Dim rows As Object

        lRecCnt = 0
        If st = "" Then
            StringToJSON = "null"
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object rows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            rows = Split(st, RECORD_SEP)
            For lRecCnt = LBound(rows) To UBound(rows)
                sFlds = ""
                'UPGRADE_WARNING: Couldn't resolve default property of object rows(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object fld. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                fld = Split(rows(lRecCnt), FIELD_SEP)
                For lFld = LBound(fld) To UBound(fld) Step 2
                    'UPGRADE_WARNING: Couldn't resolve default property of object fld(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    sFlds = (sFlds & IIf(sFlds <> "", ",", "") & """" & fld(lFld) & """:""" & toUnicode(fld(lFld + 1) & "") & """")
                Next  'fld
                sRecs.Append(IIf((Trim(sRecs.toString_Renamed) <> ""), "," & vbCrLf, "") & "{" & sFlds & "}")
            Next  'rec
            StringToJSON = ("( {""Records"": [" & vbCrLf & sRecs.toString_Renamed & vbCrLf & "], " & """RecordCount"":""" & lRecCnt & """ } )")
        End If
    End Function


    Public Function RStoJSON(ByRef RS As ADODB.Recordset) As String
        On Error GoTo ErrHandler
        Dim sFlds As String
        Dim sRecs As New cStringBuilder
        Dim lRecCnt As Integer
        Dim fld As ADODB.Field

        lRecCnt = 0
        If RS.State = ADODB.ObjectStateEnum.adStateClosed Then
            RStoJSON = "null"
        Else
            If RS.EOF Or RS.BOF Then
                RStoJSON = "null"
            Else
                Do While Not RS.EOF And Not RS.BOF
                    lRecCnt = lRecCnt + 1
                    sFlds = ""
                    For Each fld In RS.Fields
                        sFlds = (sFlds & IIf(sFlds <> "", ",", "") & """" & fld.Name & """:""" & toUnicode(fld.Value & "") & """")
                    Next fld 'fld
                    sRecs.Append(IIf((Trim(sRecs.toString_Renamed) <> ""), "," & vbCrLf, "") & "{" & sFlds & "}")
                    RS.MoveNext()
                Loop
                RStoJSON = ("( {""Records"": [" & vbCrLf & sRecs.toString_Renamed & vbCrLf & "], " & """RecordCount"":""" & lRecCnt & """ } )")
            End If
        End If

        Exit Function
ErrHandler:

    End Function

    'Public Function JsonRpcCall(url As String, methName As String, args(), Optional user As String, Optional pwd As String) As Object
    '    Dim r As Object
    '    Dim cli As Object
    '    Dim pText As String
    '    Static reqId As Integer
    '
    '    reqId = reqId + 1
    '
    '    Set r = CreateObject("Scripting.Dictionary")
    '    r("jsonrpc") = "2.0"
    '    r("method") = methName
    '    r("params") = args
    '    r("id") = reqId
    '
    '    pText = toString(r)
    '
    '    Set cli = CreateObject("MSXML2.XMLHTTP.6.0")
    '   ' Set cli = New MSXML2.XMLHTTP60
    '    If Len(user) > 0 Then   ' If Not IsMissing(user) Then
    '        cli.Open "POST", url, False, user, pwd
    '    Else
    '        cli.Open "POST", url, False
    '    End If
    '    cli.setRequestHeader "Content-Type", "application/json"
    '    cli.Send pText
    '
    '    If cli.Status <> 200 Then
    '        Err.Raise vbObjectError + INVALID_RPC_CALL + cli.Status, , cli.statusText
    '    End If
    '
    '    Set r = parse(cli.responseText)
    '    Set cli = Nothing
    '
    '    If r("id") <> reqId Then Err.Raise vbObjectError + INVALID_RPC_CALL, , "Bad Response id"
    '
    '    If r.Exists("error") Or Not r.Exists("result") Then
    '        Err.Raise vbObjectError + INVALID_RPC_CALL, , "Json-Rpc Response error: " & r("error")("message")
    '    End If
    '
    '    If Not r.Exists("result") Then Err.Raise vbObjectError + INVALID_RPC_CALL, , "Bad Response, missing result"
    '
    '    Set JsonRpcCall = r("result")
    'End Function




    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function toUnicode(ByRef str_Renamed As String) As String

        Dim x As Integer
        Dim uStr As New cStringBuilder
        Dim uChrCode As Short

        For x = 1 To Len(str_Renamed)
            uChrCode = Asc(Mid(str_Renamed, x, 1))
            Select Case uChrCode
                Case 8 ' backspace
                    uStr.Append("\b")
                Case 9 ' tab
                    uStr.Append("\t")
                Case 10 ' line feed
                    uStr.Append("\n")
                Case 12 ' formfeed
                    uStr.Append("\f")
                Case 13 ' carriage return
                    uStr.Append("\r")
                Case 34 ' quote
                    uStr.Append("\""")
                Case 39 ' apostrophe
                    uStr.Append("\'")
                Case 92 ' backslash
                    uStr.Append("\\")
                Case 123, 125 ' "{" and "}"
                    uStr.Append(("\u" & Right("0000" & Hex(uChrCode), 4)))
                Case Is < 32, Is > 127 ' non-ascii characters
                    uStr.Append(("\u" & Right("0000" & Hex(uChrCode), 4)))
                Case Else
                    uStr.Append(Chr(uChrCode))
            End Select
        Next
        toUnicode = uStr.toString_Renamed
        Exit Function

    End Function

    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        psErrors = ""
    End Sub
End Module