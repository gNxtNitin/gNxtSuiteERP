Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class cJSONScript

    'Dim dictVars As New Scripting.Dictionary
    'Dim dictVars As Object
    'Set dictVars = CreateObject("Scripting.Dictionary")
    Dim plNestCount As Integer


    Public Function Eval(ByRef sJSON As String) As String
        Dim SB As New cStringBuilder
        Dim o As Object
        Dim c As Object
        Dim i As Integer

        o = JSON.parse(sJSON)
        If (JSON.GetParserErrors = "") And Not (o Is Nothing) Then
            For i = 1 To o.Count
                Select Case VarType(o.Item(i))
                    Case VariantType.Null
                        SB.Append("null")
                    Case VariantType.Date
                        SB.Append(CStr(o.Item(i)))
                    Case VariantType.String
                        SB.Append(CStr(o.Item(i)))
                    Case Else
                        c = o.Item(i)
                        SB.Append(ExecCommand(c))
                End Select
            Next
        Else
            MsgBox(JSON.GetParserErrors, MsgBoxStyle.Exclamation, "Parser Error")
        End If
        Eval = SB.toString_Renamed
    End Function

    Public Function ExecCommand(ByRef obj As Object) As String
        Dim SB As New cStringBuilder

        Dim i As Integer
        Dim j As Integer
        Dim this As Object
        Dim key As Object
        Dim paramKeys As Object
        Dim sOut As String
        Dim sRet As String
        Dim keys As Object
        Dim val1 As String
        Dim val2 As String
        Dim bRes As Boolean
        Dim Value As Object
        'sandeep
        'If plNestCount > 40 Then
        '    ExecCommand = "ERROR: Nesting level exceeded."
        'Else
        '    plNestCount = plNestCount + 1

        '    Select Case VarType(obj)
        '        Case VariantType.Null
        '            SB.Append("null")
        '        Case VariantType.Date
        '            SB.Append(CStr(obj))
        '        Case VariantType.String
        '            SB.Append(CStr(obj))
        '        Case VariantType.Object


        '            If TypeName(obj) = "Dictionary" Then

        '                keys = obj.keys
        '                For i = 0 To obj.Count - 1
        '                    sRet = ""

        '                    key = keys(i)
        '                    If VarType(obj.Item(key)) = VariantType.String Then
        '                        sRet = obj.Item(key)
        '                    Else
        '                        this = obj.Item(key)
        '                    End If

        '                    ' command implementation											
        '                    Select Case LCase(key)
        '                        Case "alert"
        '                            MsgBox(ExecCommand(this.Item("message")), MsgBoxStyle.Information, ExecCommand(this.Item("title")))

        '                        Case "input"
        '                            SB.Append(InputBox(ExecCommand(this.Item("prompt")), ExecCommand(this.Item("title")), ExecCommand(this.Item("default"))))

        '                        Case "switch"
        '                            sOut = ExecCommand(this.Item("default"))
        '                            sRet = LCase(ExecCommand(this.Item("case")))
        '                            For j = 0 To this.Item("items").Count - 1
        '                                If LCase(this.Item("items").Item(j + 1).Item("case")) = sRet Then
        '                                    sOut = ExecCommand(this.Item("items").Item(j + 1).Item("return"))
        '                                    Exit For
        '                                End If
        '                            Next
        '                            SB.Append(sOut)

        '                        Case "set"
        '                            If dictVars.Exists(this.Item("name")) Then
        '                                dictVars.let_Item(this.Item("name"), ExecCommand(this.Item("value")))
        '                            Else
        '                                dictVars.Add(this.Item("name"), ExecCommand(this.Item("value")))
        '                            End If

        '                        Case "get"
        '                            sRet = ExecCommand(dictVars(CStr(this.Item("name"))))
        '                            If sRet = "" Then
        '                                sRet = ExecCommand(this.Item("default"))
        '                            End If

        '                            SB.Append(sRet)

        '                        Case "if"
        '                            val1 = ExecCommand(this.Item("value1"))
        '                            val2 = ExecCommand(this.Item("value2"))

        '                            bRes = False
        '                            Select Case LCase(this.Item("type"))
        '                                Case "eq" ' =											
        '                                    If LCase(val1) = LCase(val2) Then
        '                                        bRes = True
        '                                    End If

        '                                Case "gt" ' >											
        '                                    If val1 > val2 Then
        '                                        bRes = True
        '                                    End If

        '                                Case "lt" ' <											
        '                                    If val1 < val2 Then
        '                                        bRes = True
        '                                    End If

        '                                Case "gte" ' >=											
        '                                    If val1 >= val2 Then
        '                                        bRes = True
        '                                    End If

        '                                Case "lte" ' <=											
        '                                    If val1 <= val2 Then
        '                                        bRes = True
        '                                    End If

        '                            End Select

        '                            If bRes Then
        '                                SB.Append(ExecCommand(this.Item("true")))
        '                            Else
        '                                SB.Append(ExecCommand(this.Item("false")))
        '                            End If

        '                        Case "return"
        '                            SB.Append(obj.Item(key))


        '                        Case Else
        '                            If TypeName(this) = "Dictionary" Then
        '                                paramKeys = this.keys
        '                                For j = 0 To this.Count - 1
        '                                    If j > 0 Then
        '                                        sRet = sRet & ","
        '                                    End If
        '                                    sRet = sRet & CStr(this.Item(paramKeys(j)))
        '                                Next
        '                            End If


        '                            SB.Append("<%" & UCase(key) & "(" & sRet & ")%>")

        '                    End Select
        '                Next i

        '            ElseIf TypeName(obj) = "Collection" Then

        '                For Each Value In obj
        '                    SB.Append(ExecCommand(Value))
        '                Next Value

        '            End If
        '            this = Nothing

        '        Case VariantType.Boolean
        '            If obj Then SB.Append("true") Else SB.Append("false")

        '        Case VariantType.Object, VariantType.Array, VariantType.Array + VariantType.Object

        '        Case Else
        '            SB.Append(Replace(obj, ",", "."))
        '    End Select
        '    plNestCount = plNestCount - 1
        'End If

        'ExecCommand = SB.toString_Renamed
        'SB = Nothing

    End Function
End Class
