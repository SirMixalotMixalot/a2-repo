Module LinkedListModule
    Class Node(Of T)
        'Public n As Node(Of T) = Nothing
        Public _next As Integer = -1
        Public data As T

        Sub New(item As T, pointer As Integer)
            data = item
            _next = pointer
        End Sub
        Sub New(item As T)
            data = item
            _next = -1
        End Sub
    End Class
    Class LinkedList(Of T As IComparable)
        Dim head As Integer = -1
        Dim heap As Integer = -1
        Dim free As Integer = -1
        Dim _cap As Integer = 10
        Dim length As Integer = 0

        Dim Data(_cap - 1) As Node(Of T)
        Sub Resize(newSize As Integer)
            ReDim Preserve Data(newSize)
            _cap = newSize
        End Sub
        ''' <summary>
        ''' Add item to the front of the list
        ''' </summary>
        ''' <param name="item"></param>
        Sub Add(item As T)
            Dim node = New Node(Of T)(item)
            If head = -1 Then
                Data(0) = node
                head = 0
                heap = 1
                length = 1
                Return
            End If

            node._next = head
            Data(heap) = node
            head = heap
            heap += 1


        End Sub
    End Class
End Module
