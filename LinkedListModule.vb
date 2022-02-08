Module LinkedListModule
    Private instructionDescriptions As String() = {
        "[A]dd    - Adds an item to the front of the list",
        "         - SYNTAX: Add <item> [index] (Index is optional)",
        "[R]emove - Removes an item from the list",
        "         - SYNTAX: Remove <item>",
        "[D]elete - Deletes a node at the specified index",
        "         - SYNTAX: Delete <index>"
    }
    Private Enum Operation
        Add
        Remove
        Delete
    End Enum
    'Imagine if there was meta programming in visual basic
    Private Function charToOperation(c As Char)
        Select Case c
            Case "a"
            Case "A"
                Return Operation.Add
            Case "d"
            Case "D"
                Return Operation.Delete
            Case "r"
            Case "R"
                Return Operation.Remove
        End Select
    End Function
    Public Sub LinkExec()
        Preamble(instructionDescriptions)
        Dim list As New LinkedList(Of String)


    End Sub

    Public Sub TestLinkedList()
        Dim link As New LinkedList(Of Integer)

        For i = 0 To 10
            link.Add(i)
        Next
        Console.WriteLine(link)

        link.InsertAfter(2, 20)
        Console.WriteLine("Inserted 20 after second item in list")
        Console.WriteLine(link)

        link.RemoveAt(5)
        Console.WriteLine("Removed the 5th item in the list")
        Console.WriteLine(link)

        link.Delete(3)
        Console.WriteLine("Deleted the node with a value of 3")
        Console.WriteLine(link)
        Console.WriteLine("Checking if 3 is in linked list")

        If link.Contains(3) Then
            Console.WriteLine("You mucked up contains or delete")
        Else
            Console.WriteLine("3 is not in the linked list")
        End If

        Console.WriteLine(link)
    End Sub
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
        Public Overrides Function ToString() As String
            Return $"|{data}|{_next}-|"
        End Function
    End Class
    Class LinkedList(Of T As IComparable)
        Dim head As Integer = -1
        Dim heap As Integer = 0
        Dim free As Integer = -1
        Dim _cap As Integer = 10
        Dim length As Integer = 0

        Dim Data(_cap - 1) As Node(Of T)
        Sub Resize(newSize As Integer)
            ReDim Preserve Data(newSize)
            _cap = newSize
        End Sub
        ''' <summary>
        ''' Add <paramref name="item"/> to the front of the list [O(1) procedure]
        ''' </summary>
        ''' <param name="item"></param>
        Sub Add(item As T)
            If length = _cap Then
                Resize((_cap * 2) - 1)
            End If
            length += 1
            Dim node = New Node(Of T)(item)
            If head = -1 Then
                Data(0) = node
                head = 0
                heap = 1

                Return
            End If

            node._next = head
            Data(heap) = node
            head = heap
            heap += 1


        End Sub
        Public Function Contains(item As T) As Boolean
            Dim link = head
            While link < _cap AndAlso link > -1 AndAlso Not Data(link).data.Equals(item)
                link = Data(link)._next
            End While
            Return link < _cap AndAlso link > -1
        End Function
        Public Sub Delete(item As T)
            If length = 0 Then
                Throw New Exception("Linked list empty")
            End If
            length -= 1
            Dim link = head
            Dim previousLink = link
            Dim i = 0
            While link < _cap AndAlso link > -1 AndAlso Not Data(link).data.Equals(item)
                previousLink = link
                link = Data(link)._next
                i += 1
            End While

            If link >= _cap Then
                Return 'Item not even in linked list
            End If
            Dim previousFree = free
            Data(previousLink)._next = Data(link)._next
            free = link
            Data(free)._next = previousFree


        End Sub
        Public Sub RemoveAt(index As Integer)
            If length = 0 Then
                Throw New Exception("Linked list empty")
            End If
            length -= 1
            Dim previousFree = free
            If index = 0 Then

                free = head
                head = Data(head)._next
                Data(free)._next = previousFree

            End If
            Dim i As Integer = 0
            Dim link = head
            While i <> index - 1
                link = Data(link)._next
                i += 1
            End While

            free = Data(link)._next
            Data(link)._next = Data(free)._next
            Data(free)._next = previousFree


        End Sub
        Public Sub InsertAfter(index As Integer, item As T)
            If length >= _cap Then
                Resize((_cap * 2) - 1)
            End If
            length += 1
            Dim node As New Node(Of T)(item)
            If index = 0 Then
                Add(item)
                Return
            End If
            Dim i As Integer = 0
            Dim link = head
            While i <> index
                link = Data(link)._next
                i += 1
            End While
            node._next = Data(link)._next
            Data(link)._next = heap
            Data(heap) = node
            heap += 1


        End Sub
        Public Overrides Function ToString() As String
            Dim info = $"Head: {head}, Heap: {heap}, Length: {length}"
            Dim ss As New System.Text.StringBuilder()
            ss.AppendLine(info)
            Dim i = 0
            Dim link = head
            While link >= 0
                Dim str = Data(link).ToString
                If i <> length - 1 Then
                    str &= "--"
                End If
                ss.AppendLine(str)
                If i <> length - 1 Then
                    Dim padding = New String(" ", str.Length - 1)
                    ss.AppendLine(padding & " |")
                    ss.AppendLine(" v" & New String("-", str.Length - 2))
                End If

                i += 1
                link = Data(link)._next
            End While
            Return ss.ToString()
        End Function
    End Class
End Module
