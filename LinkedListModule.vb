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
        Dim Data(10) As Node(Of T)

        Sub Add(item As T)
            If head = -1 Then
                head = 0
                Data(head) = New Node(Of T)(item, 0)
            End If
            Dim _head = head

            While _head < 10 AndAlso Data(head)._next > 0
                _head = Data(_head)._next
            End While
            Data(_head) = New Node(Of T)(item, _head)
        End Sub

    End Class
End Module
