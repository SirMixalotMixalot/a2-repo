Module LinkedListModule
    Private ReadOnly instructionDescriptions As String() = {
        "[A]dd    - Adds an item to the front of the list",
        "         - SYNTAX: Add <item> [index] (Index is optional and must be last)",
        "[R]emove - Removes an item from the list",
        "         - SYNTAX: Remove <item>",
        "[D]elete - Deletes a node at the specified index",
        "         - SYNTAX: Delete <index>"
    }
    Private Enum Operation
        Add
        Remove
        Delete
        Err
    End Enum
    'Imagine if there was meta programming in visual basic
    Private Function charToOperation(c As Char) As Operation
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
        Return Operation.Err
    End Function
    Private Class Command
        Dim op As Operation
        Dim word As String
        Dim args As String()
        Public Sub New(s As String)
            Dim index = s.IndexOf(" ")
            word = s
            Dim c = s(0)
            If index < 0 Then
                op = Operation.Err
                Return
            End If
            op = charToOperation(c)
            If op <> Operation.Err Then
                args = s.Substring(index).Trim().Split(" ")
            End If
        End Sub
        Public Function Exec(l As LinkedList(Of String)) As String
            Try
                Select Case op
                    Case Operation.Add
                        Dim maybeIndex = args.Last
                        Dim index = -1

                        If Integer.TryParse(maybeIndex, index) Then
                            For Each arg In args.Take(args.Length - 1)
                                l.InsertAfter(index, arg)
                            Next
                        Else
                            For Each arg In args
                                l.Push(arg)
                            Next
                        End If

                End Select
            Catch ex As Exception
                Return ex.Message
            End Try
            Return ""
        End Function
    End Class
    Public Sub LinkExec()
        Preamble(instructionDescriptions)
        Dim list As New LinkedList(Of String)


    End Sub

    Public Sub TestLinkedList()
        Dim link As New LinkedList(Of Integer)

        For i = 0 To 10
            link.Push(i)
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
        link.Push(10)
        link.InsertAfter(4, 15)

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
            Dim sb As New Text.StringBuilder()

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
        Sub CheckIfNeedResize()
            If length = _cap Then
                Resize((_cap * 2) - 1)
            End If
        End Sub
        ''' <summary>
        ''' Add <paramref name="item"/> to the front of the list [O(1) procedure]
        ''' </summary>
        ''' <param name="item"></param>
        Sub Push(item As T)
            CheckIfNeedResize()
            length += 1
            Dim node = New Node(Of T)(item)
            If head = -1 Then
                Data(0) = node
                head = 0
                heap = 1

                Return
            End If
            If free <> -1 Then
                AddAfterToFree(node, -1)
                Return
            End If
            node._next = head
            Data(heap) = node
            head = heap
            heap += 1


        End Sub
        Private Function IsValidLink(link As Integer) As Boolean
            Return link < _cap AndAlso link >= 0
        End Function
        Public Function Contains(item As T) As Boolean
            Dim link = head
            While IsValidLink(link) AndAlso Not Data(link).data.Equals(item)
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
            While IsValidLink(link) AndAlso Not Data(link).data.Equals(item)
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
        Private Sub AddAfterToFree(node As Node(Of T), before As Integer)
            Dim location = free

            If before >= 0 Then
                Dim after = Data(before)._next
                Data(before)._next = location
                node._next = after
            Else
                node._next = head
                head = location
            End If
            free = Data(free)._next
            Data(location) = node




        End Sub
        Public Sub RemoveAt(index As Integer)
            If length = 0 Then
                Throw New Exception("Linked list empty")
            End If
            If index >= length Then
                Throw New Exception("Invalid index")
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
            CheckIfNeedResize()
            If index >= length Then
                Throw New Exception("Invalid index")
            End If
            length += 1
            Dim node As New Node(Of T)(item)
            Dim i As Integer = 0
            Dim link = head
            While i <> index
                link = Data(link)._next
                i += 1
            End While
            If free <> -1 Then
                AddAfterToFree(node, link)
                Return
            End If
            node._next = Data(link)._next
            Data(link)._next = heap
            Data(heap) = node
            heap += 1


        End Sub
        Public Overrides Function ToString() As String
            Dim info = $"Head: {head}, Heap: {heap}, Length: {length}"
            Dim ss As New Text.StringBuilder()
            ss.AppendLine(info)
            Dim i = 0
            Dim link = head
            While IsValidLink(link)
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
