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

        Dim _cap As Integer = 10
        Dim Data(_cap) As Node(Of T)

        Sub Add(item As T)
            If head = -1 Then
                head = 0
                Data(head) = New Node(Of T)(item, 0)
                Return
            End If
            Dim _head = head

            While _head < _cap AndAlso Data(head)._next > 0
                _head = Data(_head)._next
            End While
            Data(_head) = New Node(Of T)(item, _head)
        End Sub
        Function Nth(n As Integer) As Node(Of T)
            Return Data(n)
        End Function

    End Class
End Module
