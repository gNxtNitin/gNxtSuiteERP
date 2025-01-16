Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class cStringBuilder

    ' ======================================================================================			
    ' Name:     vbAccelerator cStringBuilder			
    ' Author:   Steve McMahon (steve@vbaccelerator.com)			
    ' Date:     1 January 2002			
    '			
    ' Copyright © 2002 Steve McMahon for vbAccelerator			
    ' --------------------------------------------------------------------------------------			
    ' Visit vbAccelerator - advanced free source code for VB programmers			
    ' http://vbaccelerator.com			
    ' --------------------------------------------------------------------------------------			
    '			
    ' VB can be slow to append strings together because of the continual			
    ' reallocation of string size.  This class pre-allocates a string in			
    ' blocks and hence removes the performance restriction.			
    '			
    ' Quicker insert and remove is also possible since string space does			
    ' not have to be reallocated.			
    '			
    ' Example:			
    ' Adding "http://vbaccelerator.com/" 10,000 times to a string:			
    ' Standard VB:   34s			
    ' This Class:    0.35s			
    '			
    ' ======================================================================================			

    Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByRef pDst As Object, ByRef pSrc As Object, ByVal ByteLen As Integer)


    Private m_sString As String
    Private m_iChunkSize As Integer
    Private m_iPos As Integer
    Private m_iLen As Integer

    Public ReadOnly Property Length() As Integer
        Get
            Length = m_iPos \ 2
        End Get
    End Property

    Public ReadOnly Property Capacity() As Integer
        Get
            Capacity = m_iLen \ 2
        End Get
    End Property


    Public Property ChunkSize() As Integer
        Get
            ' Return the unicode character chunk size:			
            ChunkSize = m_iChunkSize \ 2
        End Get
        Set(ByVal Value As Integer)
            ' Set the chunksize.  We multiply by 2 because internally			
            ' we are considering bytes:			
            m_iChunkSize = Value * 2
        End Set
    End Property

    Public ReadOnly Property toString_Renamed() As String
        Get
            ' The internal string:			
            If m_iPos > 0 Then
                toString_Renamed = Left(m_sString, m_iPos \ 2)
            End If
        End Get
    End Property

    Public WriteOnly Property TheString() As String
        Set(ByVal Value As String)
            Dim lLen As Integer
            'sandeep
            '' Setting the string:			
            lLen = Len(Value)
            If lLen = 0 Then
                'Clear			
                m_sString = ""
                m_iPos = 0
                m_iLen = 0
            Else
                If m_iLen < lLen Then
                    ' Need to expand string to accommodate:			
                    Do
                        m_sString = m_sString & Space(m_iChunkSize \ 2)
                        m_iLen = m_iLen + m_iChunkSize
                    Loop While m_iLen < lLen
                End If
                CopyMemory(Convert.ToInt64(m_sString), Convert.ToInt64(Value), lLen)
                m_iPos = lLen
            End If

        End Set
    End Property

    Public Sub Clear()
        m_sString = ""
        m_iPos = 0
        m_iLen = 0
    End Sub

    Public Sub AppendNL(ByRef sThis As String)
        Append(sThis)
        Append(vbCrLf)
    End Sub

    Public Sub Append(ByRef sThis As String)
        Dim lLen As Integer
        Dim lLenPlusPos As Integer
        'sandeep
        ' Append an item to the string:			
        lLen = Len(sThis)
        lLenPlusPos = lLen + m_iPos
        Dim lTemp As Integer
        If lLenPlusPos > m_iLen Then

            lTemp = m_iLen
            Do While lTemp < lLenPlusPos
                lTemp = lTemp + m_iChunkSize
            Loop

            m_sString = m_sString & Space((lTemp - m_iLen) \ 2)
            m_iLen = lTemp
        End If

        CopyMemory(UnsignedAdd(Convert.ToInt64(m_sString), m_iPos), Convert.ToInt64(sThis), lLen)
        m_iPos = m_iPos + lLen
    End Sub

    Public Sub AppendByVal(ByVal sThis As String)
        Append(sThis)
    End Sub

    Public Sub Insert(ByVal iIndex As Integer, ByRef sThis As String)
        Dim lLen As Integer
        Dim lPos As Integer
        Dim lSize As Integer
        'sandeep
        ' is iIndex within bounds?			
        If (iIndex * 2 > m_iPos) Then
            Err.Raise(9)
        Else

            lLen = Len(sThis)
            If (m_iPos + lLen) > m_iLen Then
                m_sString = m_sString & Space(m_iChunkSize \ 2)
                m_iLen = m_iLen + m_iChunkSize
            End If

            ' Move existing characters from current position			
            lPos = UnsignedAdd(Convert.ToInt64(m_sString), iIndex * 2)
            lSize = m_iPos - iIndex * 2

            ' moving from iIndex to iIndex + lLen			
            CopyMemory(UnsignedAdd(lPos, lLen), lPos, lSize)

            ' Insert new characters:			
            CopyMemory(lPos, Convert.ToInt64(sThis), lLen)

            m_iPos = m_iPos + lLen
        End If
    End Sub

    Public Sub InsertByVal(ByVal iIndex As Integer, ByVal sThis As String)
        Insert(iIndex, sThis)
    End Sub

    Public Sub Remove(ByVal iIndex As Integer, ByVal lLen As Integer)
        Dim lSrc As Integer
        Dim lDst As Integer
        Dim lSize As Integer
        'sandeep
        '' is iIndex within bounds?			
        If (iIndex * 2 > m_iPos) Then
            Err.Raise(9)
        Else
            ' is there sufficient length?			
            If ((iIndex + lLen) * 2 > m_iPos) Then
                Err.Raise(9)
            Else
                ' Need to copy characters from iIndex*2 to m_iPos back by lLen chars:			
                lSrc = UnsignedAdd(Convert.ToInt64(m_sString), (iIndex + lLen) * 2)
                lDst = UnsignedAdd(Convert.ToInt64(m_sString), iIndex * 2)
                lSize = (m_iPos - (iIndex + lLen) * 2)
                CopyMemory(lDst, lSrc, lSize)
                m_iPos = m_iPos - lLen * 2
            End If
        End If
    End Sub

    Public Function Find(ByVal sToFind As String, Optional ByVal lStartIndex As Integer = 1, Optional ByVal compare As CompareMethod = CompareMethod.Text) As Integer

        Dim lInstr As Integer
        If (lStartIndex > 0) Then
            lInstr = InStr(lStartIndex, m_sString, sToFind, compare)
        Else
            lInstr = InStr(CShort(m_sString), sToFind, CStr(compare))
        End If
        If (lInstr < m_iPos \ 2) Then
            Find = lInstr
        End If
    End Function

    Public Sub HeapMinimize()
        Dim iLen As Integer

        ' Reduce the string size so only the minimal chunks			
        ' are allocated:			
        If (m_iLen - m_iPos) > m_iChunkSize Then
            iLen = m_iLen
            Do While (iLen - m_iPos) > m_iChunkSize
                iLen = iLen - m_iChunkSize
            Loop
            m_sString = Left(m_sString, iLen \ 2)
            m_iLen = iLen
        End If

    End Sub
    Private Function UnsignedAdd(ByRef start As Integer, ByRef Incr As Integer) As Integer
        ' This function is useful when doing pointer arithmetic,			
        ' but note it only works for positive values of Incr			

        If start And &H80000000 Then 'Start < 0			
            UnsignedAdd = start + Incr
        ElseIf (start Or &H80000000) < -Incr Then
            UnsignedAdd = start + Incr
        Else
            UnsignedAdd = (start + &H80000000) + (Incr + &H80000000)
        End If

    End Function
    Private Sub Class_Initialize_Renamed()
        ' The default allocation: 8192 characters.			
        m_iChunkSize = 16384
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub
End Class
