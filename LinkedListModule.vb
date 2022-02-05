Module LinkedListModule
    Class Node(Of T)
        Public _next As Integer = -1
        Public data As T

        Sub New(item As T, pointer As Integer)
            data = item
            _next = pointer
        End Sub
    End Class
    Class LinkedList(Of T As IComparable)
        Dim head As Integer = -1
        Dim _heapStart As Integer = 0
        Dim _cap As Integer = 10
        Dim length As Integer = 0
        Dim lastInsertNext As Integer = 0
        Dim Data(_cap - 1) As Node(Of T)
        Function Contains(item As T) As Boolean
            Dim _head = head
            While _head >= 0 AndAlso Not Data(_head).data.Equals(item)
                _head = Data(_head)._next
            End While
            Return _head < 0
        End Function
        Sub InsertAt(item As T, index As Integer)
            '''If inserting before b
            ''' |a| -> |b| -> |c| , save pointer from a to b, set pointer from a to inserted item, set pointer of inserted item to saved b
            ''' |b| -> |c| set pointer from saved pointer to b, set head as pointer to inserted item
            ''' |a| -> |b| 
            Add(item)
            Dim after = Data(index)._next
            Data(index)._next = lastInsertNext - 1


        End Sub
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
