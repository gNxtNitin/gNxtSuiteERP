Option Strict Off
Option Explicit On
'Not a real (fractional) number, but Major.Minor integers:

Public Class JsonBag
    Implements System.Collections.IEnumerable



    Public ReadOnly Property Count() As Integer
        Get
            Count = Values.Count()
        End Get
    End Property


    Public Shared Property DecimalMode() As Boolean
        Get
            DecimalMode = mDecimalMode
        End Get
        Set(ByVal Value As Boolean)
            mDecimalMode = Value
            If mDecimalMode Then
                NumberType = VariantType.Decimal
            Else
                NumberType = VariantType.Double
            End If
        End Set
    End Property


    ''UPGRADE_NOTE: IsArray was upgraded to IsArray_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    'Public Shared Property IsArray_Renamed() As Boolean
    '	Get
    '		IsArray_Renamed = mIsArray
    '	End Get
    '	Set(ByVal Value As Boolean)
    '		If Values.Count() > 0 Then
    '			err.Raise(5, TypeNameOfMe, "Cannot change IsArray setting after items have been added")
    '		Else
    '			mIsArray = Value
    '			'UPGRADE_NOTE: Object Names may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
    '			If mIsArray Then Names = Nothing
    '		End If
    '	End Set
    'End Property
    Public Shared Property IsArray() As Boolean
        Get
            IsArray = mIsArray
        End Get
        Set(ByVal RHS As Boolean)
            If Values.Count > 0 Then
                Err.Raise(5, TypeNameOfMe, "Cannot change IsArray setting after items have been added")
            Else
                mIsArray = RHS
                If mIsArray Then Names = Nothing
            End If
        End Set
    End Property

    'Default property.


    Public Shared Property Item(ByVal key As Object) As Object
        Get

            Dim PrefixedKey As String
            If IsDBNull(key) Then Err.Raise(94, TypeNameOfMe, "Key must be String or an index)")
            If VarType(key) = VariantType.String Then
                If mIsArray Then
                    Err.Raise(5, TypeNameOfMe, "Array values can only be acessed by index")
                End If

                PrefixedKey = PrefixHash(key)
                If IsReference(Values.Item(PrefixedKey)) Then
                    Item = Values.Item(PrefixedKey)
                Else
                    Item = Values.Item(PrefixedKey)
                End If
            Else
                If IsReference(Values.Item(key)) Then
                    Item = Values.Item(key)
                Else
                    Item = Values.Item(key)
                End If
            End If
        End Get
        Set(ByVal RHS As Object)
            Item(key) = RHS
        End Set
    End Property


    Public Shared Property JSON() As String
        Get
            CursorOut = 1
            'SerializeItem(vbNullString, JB)
            JSON = Left(TextOut, CursorOut - 1)

            'Clear for next reuse.  Do it here to reclaim space.
            TextOut = ""
        End Get
        Set(ByVal Value As String)
            Clear()

            CursorIn = 1
            LengthIn = Len(Value)

            SkipWhitespace(Value)

            Select Case Mid(Value, CursorIn, 1)
                Case LBRACE
                    CursorIn = CursorIn + 1
                    ParseObject(Value, CursorIn, Len(Value))
                Case LBRACKET
                    CursorIn = CursorIn + 1
                    ParseArray(Value, CursorIn, Len(Value))
                Case Else
                    Error13A("either " & LBRACE & " or " & LBRACKET, CursorIn)
            End Select
        End Set
    End Property

    Public Shared ReadOnly Property Name(ByVal Index As Integer) As String
        Get
            If mIsArray Then Err.Raise(5, TypeNameOfMe, "Array items do not have names")

            'UPGRADE_WARNING: Couldn't resolve default property of object Names.Item(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            Name = Names.Item(Index)
        End Get
    End Property

    Public Shared ReadOnly Property Version() As String()
        Get
            Version = Split(CLASS_VERSION)
        End Get
    End Property

    '=== Public Shared Methods ====================================================================

    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    Public Shared Function AddNewArray(Optional ByVal key As Object = vbNullString) As JsonBag
        Dim NewArray As JsonBag

        'Dim NewArray As New JsonBag
        NewArray.IsArray = True
        Item(key) = NewArray
        AddNewArray = NewArray
    End Function
    Public Shared Function AddNewObject(Optional ByVal key As Object = vbNullString) As JsonBag
        Dim NewObject As JsonBag

        'Dim NewObject As New JsonBag
        Item(key) = NewObject
        AddNewObject = NewObject
    End Function

    Public Shared Sub Clear()
        Dim Names As New Collection
        Dim Values As New Collection
        mIsArray = False
    End Sub

    Public Shared Function Exists(ByVal key As Object) As Boolean
        Dim Name As String

        On Error Resume Next
        'UPGRADE_WARNING: Couldn't resolve default property of object Names.Item(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        Name = Names.Item(key)
        Exists = Err.Number = 0
        Err.Clear()
    End Function

    'Marked as hidden and ProcedureID = -4
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public Shared Function NewEnum() As stdole.IUnknown
    'If mIsArray Then 'err.Raise(5, TypeNameOfMe, "Arrays must be iterated using index values")
    '
    'NewEnum = Names.GetEnumerator
    'End Function

    Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        'GetEnumerator = Names.GetEnumerator
    End Function

    Public Shared Sub Remove(ByVal key As Object)
        'Allow remove by Key or Index (only by Index for arrays).  If the item
        'does not exist return silently.

        Dim PrefixedKey As String

        'UPGRADE_WARNING: VarType has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        If VarType(key) = VariantType.String Then
            If mIsArray Then Err.Raise(5, TypeNameOfMe, "Must remove by index for arrays")

            'UPGRADE_WARNING: Couldn't resolve default property of object key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            PrefixedKey = PrefixHash(key)
            On Error Resume Next
            Names.Remove(PrefixedKey)
            If Err.Number Then
                Exit Sub
            End If
            On Error GoTo 0
            Values.Remove(PrefixedKey)
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            If key < Values.Count() Then
                Values.Remove(key)
                If Not IsArray = True Then Names.Remove(key)
            End If
        End If
    End Sub

    '=== Friend Methods (do not call from client logic) ====================================

    Public Shared Sub ParseArray(ByRef Text As String, ByRef StartCursor As Integer, ByVal TextLength As Integer)
        'This call is made within the context of the instance at hand.

        Dim ArrayValue As Object

        CursorIn = StartCursor
        LengthIn = TextLength

        mIsArray = True
        Do
            SkipWhitespace(Text)
            Select Case Mid(Text, CursorIn, 1)
                Case COMMA
                    CursorIn = CursorIn + 1
                Case RBRACKET
                    CursorIn = CursorIn + 1
                    Exit Do
                Case Else
                    ParseValue(Text, ArrayValue)
                    Values.Add(ArrayValue)
            End Select
        Loop
        StartCursor = CursorIn
    End Sub

    Public Shared Sub ParseObject(ByRef Text As String, ByRef StartCursor As Integer, ByVal TextLength As Integer)
        'This call is made within the context of the instance at hand.

        'UPGRADE_NOTE: Char was upgraded to Char_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
        Dim Char_Renamed As String
        Dim ItemName As String
        Dim Value As Object
        Dim FoundFirstItem As Boolean

        CursorIn = StartCursor
        LengthIn = TextLength

        Do
            SkipWhitespace(Text)
            Char_Renamed = Mid(Text, CursorIn, 1)
            CursorIn = CursorIn + 1
            Select Case Char_Renamed
                Case QUOTE
                    ItemName = ParseName(Text)
                    ParseValue(Text, Value)
                    'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Item(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Item(ItemName) = Value
                    FoundFirstItem = True
                Case COMMA
                    If Not FoundFirstItem Then
                        Err.Raise(13, TypeNameOfMe, "Found "","" before first item at character " & CStr(CursorIn - 1))
                    End If
                Case RBRACE
                    Exit Do
                Case Else
                    Error13A(", or }", CursorIn - 1)
            End Select
        Loop
        StartCursor = CursorIn
    End Sub

    '=== Public Methods ===================================================================

    Public Sub Cat(ByRef NewText As String)
        Const TEXT_CHUNK As Integer = 512 'Allocation size for destination buffer Text.
        Dim LenNew As Integer

        LenNew = Len(NewText)
        If LenNew > 0 Then
            If CursorOut + LenNew - 1 > Len(TextOut) Then
                If LenNew > TEXT_CHUNK Then
                    TextOut = TextOut & Space(LenNew + TEXT_CHUNK)
                Else
                    TextOut = TextOut & Space(TEXT_CHUNK)
                End If
            End If
            Mid(TextOut, CursorOut, LenNew) = NewText
            CursorOut = CursorOut + LenNew
        End If
    End Sub

    Public Shared Sub Error13A(ByVal Symbol As String, ByVal Position As Integer)
        Err.Raise(13, TypeNameOfMe, "Expected " & Symbol & " at character " & CStr(Position))
    End Sub

    Public Shared Sub Error13B(ByVal Position As Integer)
        Err.Raise(13, TypeNameOfMe, "Bad string character escape at character " & CStr(Position))
    End Sub

    Public Shared Function ParseName(ByRef Text As String) As String
        ParseName = ParseString(Text)

        SkipWhitespace(Text)
        If Mid(Text, CursorIn, 1) <> COLON Then
            Error13A(COLON, CursorIn)
        End If
        CursorIn = CursorIn + 1
    End Function

    Public Shared Function ParseNumber(ByRef Text As String) As Object
        Dim SaveCursor As Integer
        Dim BuildString As String
        Dim BuildCursor As Integer
        'UPGRADE_NOTE: Char was upgraded to Char_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
        Dim Char_Renamed As String
        Dim GotDecPoint As Boolean
        Dim GotExpSign As Boolean

        SaveCursor = CursorIn 'Saved for "bad number format" error.
        BuildString = Space(LengthIn - CursorIn + 1)

        'We know 1st char has been validated by the caller.
        BuildCursor = 1
        Mid(BuildString, 1, 1) = Mid(Text, CursorIn, 1)

        For CursorIn = CursorIn + 1 To LengthIn
            Char_Renamed = LCase(Mid(Text, CursorIn, 1))
            Select Case Char_Renamed
                Case RADIXPOINT
                    If GotDecPoint Then
                        Err.Raise(13, TypeNameOfMe, "Second decimal point at character " & CStr(CursorIn))
                    End If
                    If Mid(BuildString, BuildCursor, 1) = MINUS Then
                        Err.Raise(13, TypeNameOfMe, "Digit expected at character " & CStr(CursorIn))
                    End If
                    GotDecPoint = True
                Case ZERO To NINE
                    'Do nothing.
                Case "e"
                    CursorIn = CursorIn + 1
                    Exit For
                Case Else
                    Exit For
            End Select
            BuildCursor = BuildCursor + 1
            Mid(BuildString, BuildCursor, 1) = Char_Renamed
        Next

        If Char_Renamed = "e" Then
            BuildCursor = BuildCursor + 1
            Mid(BuildString, BuildCursor, 1) = Char_Renamed

            For CursorIn = CursorIn To LengthIn
                Char_Renamed = Mid(Text, CursorIn, 1)
                Select Case Char_Renamed
                    Case PLUS, MINUS
                        If GotExpSign Then
                            Err.Raise(13, TypeNameOfMe, "Second exponent sign at character " & CStr(CursorIn))
                        End If
                        GotExpSign = True
                    Case ZERO To NINE
                        'Do nothing.
                    Case Else
                        Exit For
                End Select
                BuildCursor = BuildCursor + 1
                Mid(BuildString, BuildCursor, 1) = Char_Renamed
            Next
        End If

        If CursorIn > LengthIn Then
            Err.Raise(13, TypeNameOfMe, "Ran off end of string while parsing a number")
        End If

        ParseNumber = Left(BuildString, BuildCursor)
        If VariantChangeTypeEx(ParseNumber, ParseNumber, LOCALE_INVARIANT, 0, NumberType) <> S_OK Then
            Err.Raise(6, TypeNameOfMe, "Number overflow or parse error at character " & CStr(SaveCursor))
        End If
    End Function

    Public Shared Function ParseString(ByRef Text As String) As String
        Dim BuildCursor As Integer
        'UPGRADE_NOTE: Char was upgraded to Char_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
        Dim Char_Renamed As String

        ParseString = Space(LengthIn - CursorIn + 1)

        Dim counter As Short
        counter = CursorIn
        For CursorIn = counter To LengthIn
            Char_Renamed = Mid(Text, CursorIn, 1)
            Select Case Char_Renamed
                Case vbNullChar To vbUS
                    Err.Raise(13, TypeNameOfMe, "Invalid string character at " & CStr(CursorIn))
                Case REVSOLIDUS
                    CursorIn = CursorIn + 1
                    If CursorIn > LengthIn Then
                        Error13B(CursorIn)
                    End If
                    Char_Renamed = LCase(Mid(Text, CursorIn, 1)) 'Accept uppercased escape symbols.
                    Select Case Char_Renamed
                        Case QUOTE, REVSOLIDUS, "/"
                            'Do nothing.
                        Case "b"
                            Char_Renamed = vbBack
                        Case "f"
                            Char_Renamed = vbFormFeed
                        Case "n"
                            Char_Renamed = vbLf
                        Case "r"
                            Char_Renamed = vbCr
                        Case "t"
                            Char_Renamed = vbTab
                        Case "u"
                            CursorIn = CursorIn + 1
                            If LengthIn - CursorIn < 3 Then
                                Error13B(CursorIn)
                            End If
                            On Error Resume Next
                            Char_Renamed = ChrW(CInt("&H0" & Mid(Text, CursorIn, 4)))
                            If Err.Number Then
                                On Error GoTo 0
                                Error13B(CursorIn)
                            End If
                            On Error GoTo 0
                            CursorIn = CursorIn + 3 'Not + 4 because For loop will increment again.
                        Case Else
                            Error13B(CursorIn)
                    End Select
                Case QUOTE
                    CursorIn = CursorIn + 1
                    Exit For
                    'Case Else
                    'Do Nothing, i.e. pass Char unchanged.
            End Select
            BuildCursor = BuildCursor + 1
            Mid(ParseString, BuildCursor, 1) = Char_Renamed
        Next

        If CursorIn > LengthIn Then
            Error13A(QUOTE, LengthIn + 1)
        End If
        ParseString = Left(ParseString, BuildCursor)
    End Function

    Public Shared Sub ParseValue(ByRef Text As String, ByRef Value As Object)
        Dim SubBag As JsonBag
        Dim Token As String

        SkipWhitespace(Text)
        Select Case Mid(Text, CursorIn, 1)
            Case QUOTE
                CursorIn = CursorIn + 1
                'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Value = ParseString(Text)
            Case LBRACE
                CursorIn = CursorIn + 1
                SubBag = New JsonBag
                SubBag.DecimalMode = DecimalMode
                SubBag.ParseObject(Text, CursorIn, LengthIn)
                Value = SubBag
            Case LBRACKET
                CursorIn = CursorIn + 1
                SubBag = New JsonBag
                SubBag.DecimalMode = DecimalMode
                SubBag.ParseArray(Text, CursorIn, LengthIn)
                Value = SubBag
            Case MINUS, ZERO To NINE
                'UPGRADE_WARNING: Couldn't resolve default property of object ParseNumber(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Value = ParseNumber(Text)
            Case Else
                'Special value tokens.
                Token = LCase(Mid(Text, CursorIn, 4))
                If Token = "null" Then
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Value = System.DBNull.Value
                    CursorIn = CursorIn + 4
                ElseIf Token = "true" Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Value = True
                    CursorIn = CursorIn + 4
                Else
                    Token = LCase(Mid(Text, CursorIn, 5))
                    If Token = "false" Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        Value = False
                        CursorIn = CursorIn + 5
                    Else
                        Err.Raise(13, TypeNameOfMe, "Bad value at character " & CStr(CursorIn))
                    End If
                End If
        End Select
    End Sub

    Public Shared Function PrefixHash(ByVal KeyString As String) As String
        'This is used to make Collection access by key case-sensitive.

        Dim Hash As Integer

        'UPGRADE_ISSUE: VarPtr function is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
        'UPGRADE_ISSUE: StrPtr function is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
        HashData((KeyString), 2 * Len(KeyString), (Hash), 4)        ''HashData(StrPtr(KeyString), 2 * Len(KeyString), VarPtr(Hash), 4)
        PrefixHash = Right("0000000" & Hex(Hash), 8) & KeyString
    End Function

    Public Sub SerializeItem(ByVal ItemName As String, ByVal Item As Object, Optional ByVal Level As Short = 0)
        'For outer level call set CursorOut = 1 before calling.  For outer level call
        'or array calls pass vbNullString as ItemName for "anonymity."

        Const TEXT_CHUNK As Integer = 64
        Dim Indent As String
        Dim Anonymous As Boolean
        Dim Name As Object
        Dim ItemIndex As Integer
        Dim TempItem As Object
        Dim ItemBag As JsonBag
        Dim SubBag As JsonBag
        Dim ItemText As String
        Dim ArrayItem As Object

        If Whitespace Then
            Indent = Space(4 * Level)
        End If

        'UPGRADE_ISSUE: StrPtr function is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
        Anonymous = ItemName = 0 ' StrPtr(ItemName) = 0 'Check for vbNullString.
        If Not Anonymous Then
            'Not vbNullString so we have a named Item.
            If Whitespace Then Cat(Indent)
            Cat(SerializeString(ItemName) & COLON)
        End If

        'UPGRADE_WARNING: VarType has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        Select Case VarType(Item)
            Case VariantType.Empty, VariantType.Null 'vbEmpty case should actually never occur.
                If Whitespace And Anonymous Then Cat(Indent)
                Cat("null")
            Case VariantType.Short, VariantType.Integer, VariantType.Single, VariantType.Double, VariantType.Decimal, VariantType.Decimal, VariantType.Byte, VariantType.Boolean
                If Whitespace And Anonymous Then Cat(Indent)
                If VariantChangeTypeEx(TempItem, Item, LOCALE_INVARIANT, VARIANT_ALPHABOOL, VariantType.String) <> S_OK Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Item. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Err.Raise(51, TypeNameOfMe, ItemName & ", value " & CStr(Item) & " failed to serialize")
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object TempItem. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Cat(LCase(TempItem)) 'Convert to lowercase "true" and "false" and "1.234e34" and such.
            Case VariantType.String
                If Whitespace And Anonymous Then Cat(Indent)
                'UPGRADE_WARNING: Couldn't resolve default property of object Item. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Cat(SerializeString(Item))
            Case VariantType.Object
                ItemBag = Item
                If ItemBag.IsArray = True Then
                    If Whitespace And Anonymous Then Cat(Indent)
                    Cat(LBRACKET)
                    If ItemBag.Count < 1 Then
                        Cat(RBRACKET)
                    Else
                        If Whitespace Then Cat(vbNewLine)
                        With ItemBag
                            For ItemIndex = 1 To .Count
                                'UPGRADE_WARNING: IsObject has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                                If IsReference(.Item(ItemIndex)) Then
                                    TempItem = .Item(ItemIndex)
                                Else
                                    'UPGRADE_WARNING: Couldn't resolve default property of object ItemBag.Item(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                    'UPGRADE_WARNING: Couldn't resolve default property of object TempItem. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                    TempItem = .Item(ItemIndex)
                                End If
                                SerializeItem(vbNullString, TempItem, Level + 1)
                                Cat(COMMA)
                                If Whitespace Then Cat(vbNewLine)
                            Next
                        End With
                        If Whitespace Then
                            CursorOut = CursorOut - 3
                            Cat(vbNewLine & Indent & RBRACKET)
                        Else
                            Mid(TextOut, CursorOut - 1) = RBRACKET
                        End If
                    End If
                Else
                    If Whitespace And Anonymous Then Cat(Indent)
                    Cat(LBRACE)
                    If ItemBag.Count < 1 Then
                        Cat(RBRACE)
                    Else
                        If Whitespace Then Cat(vbNewLine)
                        For Each Name In ItemBag
                            'UPGRADE_WARNING: IsObject has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                            If IsReference(ItemBag.Item(Name)) Then
                                TempItem = ItemBag.Item(Name)
                            Else
                                'UPGRADE_WARNING: Couldn't resolve default property of object ItemBag.Item(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                'UPGRADE_WARNING: Couldn't resolve default property of object TempItem. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                TempItem = ItemBag.Item(Name)
                            End If
                            'UPGRADE_WARNING: Couldn't resolve default property of object Name. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                            SerializeItem(Name, TempItem, Level + 1)
                            Cat(COMMA)
                            If Whitespace Then Cat(vbNewLine)
                        Next Name
                        If Whitespace Then
                            CursorOut = CursorOut - 3
                            Cat(vbNewLine & Indent & RBRACE)
                        Else
                            Mid(TextOut, CursorOut - 1) = RBRACE
                        End If
                    End If
                End If
            Case Else
                'UPGRADE_WARNING: VarType has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                Err.Raise(51, TypeNameOfMe, ItemName & ", unknown/unsupported type = " & CStr(VarType(Item)))
        End Select
    End Sub

    Public Shared Function SerializeString(ByVal Text As String) As String
        Dim BuildString As String
        Dim BuildCursor As Integer
        Dim TextCursor As Integer
        'UPGRADE_NOTE: Char was upgraded to Char_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
        Dim Char_Renamed As String
        Dim intChar As Short

        BuildString = Space(3 * Len(Text) \ 2)
        BuildCursor = 1
        StringCat(BuildString, BuildCursor, QUOTE)
        For TextCursor = 1 To Len(Text)
            Char_Renamed = Mid(Text, TextCursor, 1)
            Select Case Char_Renamed
                Case QUOTE, REVSOLIDUS
                    StringCat(BuildString, BuildCursor, REVSOLIDUS & Char_Renamed)
                Case vbBack
                    StringCat(BuildString, BuildCursor, REVSOLIDUS & "b")
                Case vbFormFeed
                    StringCat(BuildString, BuildCursor, REVSOLIDUS & "f")
                Case vbLf
                    StringCat(BuildString, BuildCursor, REVSOLIDUS & "n")
                Case vbCr
                    StringCat(BuildString, BuildCursor, REVSOLIDUS & "r")
                Case vbTab
                    StringCat(BuildString, BuildCursor, REVSOLIDUS & "t")
                Case " " To "!", "#" To LBRACKET, RBRACKET To "~"
                    StringCat(BuildString, BuildCursor, Char_Renamed)
                Case Else
                    intChar = AscW(Char_Renamed)
                    Select Case intChar
                        Case 0 To &H1F, &H7F To &H9F, &H34F, &H200B To &H200F, &H2028 To &H202E, &H2060 ', &HFE01 To &HFE0F, &HFEFF, &HFFFD, &HD800 To &HDFFF
                            StringCat(BuildString, BuildCursor, REVSOLIDUS & "u" & Right("000" & Hex(intChar), 4))
                        Case Else
                            StringCat(BuildString, BuildCursor, Char_Renamed)
                    End Select
            End Select
        Next
        StringCat(BuildString, BuildCursor, QUOTE)
        SerializeString = Left(BuildString, BuildCursor - 1)
    End Function

    Public Shared Sub SkipWhitespace(ByRef Text As String)
        'UPGRADE_ISSUE: StrPtr function is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
        'CursorIn = CursorIn + StrSpn(StrPtr(Text) + 2 * (CursorIn - 1), StrPtr(WHITE_SPACE))
        CursorIn = CursorIn + StrSpn((Text) + 2 * (CursorIn - 1), (WHITE_SPACE))
    End Sub

    Public Shared Sub StringCat(ByRef TextOut As String, ByRef CursorOut As Object, ByRef NewText As String)
        Const TEXT_CHUNK As Integer = 64 'Allocation size for destination buffer Text.
        Dim LenNew As Integer

        LenNew = Len(NewText)
        If LenNew > 0 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object CursorOut. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            If CursorOut + LenNew - 1 > Len(TextOut) Then
                If LenNew > TEXT_CHUNK Then
                    TextOut = TextOut & Space(LenNew + TEXT_CHUNK)
                Else
                    TextOut = TextOut & Space(TEXT_CHUNK)
                End If
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object CursorOut. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            Mid(TextOut, CursorOut, LenNew) = NewText
            'UPGRADE_WARNING: Couldn't resolve default property of object CursorOut. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            CursorOut = CursorOut + LenNew
        End If
    End Sub

    '=== Public Events ====================================================================

    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Sub Class_Initialize_Renamed()
        'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        TypeNameOfMe = TypeName(Me)
        vbUS = ChrW(&H1F)
        DecimalMode = False

        Clear()
    End Sub
    'Public Shared Sub New()
    '    MyBase.New()
    '    Class_Initialize_Renamed()
    'End Sub
End Class