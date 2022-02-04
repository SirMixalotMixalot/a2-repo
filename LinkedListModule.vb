Module LinkedListModule
    Class Node(Of T)
        Public _next As Integer = -1
        Dim data As T

        Sub New(item As T, pointer As Integer)
            data = item
            _next = pointer
        End Sub
    End Class
    Class LinkedList(Of T)
        Dim head As Integer = -1
        Dim _heapStart As Integer = 0
        Dim _cap As Integer = 10
        Dim length As Integer = 0
        Dim lastInsertNext As Integer = 0
        Dim Data(_cap - 1) As Node(Of T)

        Sub Add(item As T)


            If head = -1 Then
                head = 0
                Data(head) = New Node(Of T)(item, 0)
                lastInsertNext = 1
                length = 1
                Return
            End If

            If lastInsertNext = _cap Then
                ReDim Preserve Data(2 * _cap)
                _cap *= 2
            End If
            Data(lastInsertNext) = New Node(Of T)(item, lastInsertNext + 1)
            lastInsertNext += 1
            length += 1
        End Sub
        Function Nth(n As Integer) As Node(Of T)
            Return Data(n)
        End Function

    End Class
End Module
