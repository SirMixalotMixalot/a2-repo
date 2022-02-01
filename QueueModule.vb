﻿Imports System.Text
Friend Module QueueModule
    Private InstructionDescriptions As String() = {
        "[D]equeue       - Remove an item from the queue",
        "                    USAGE: d",
        "[E]nqueue       - Add an item to the queue",
        "                    USAGE: e <args> where <args> are a space delimeted list of items",
        "[R]eset         - Empty the queue and enter the maximum capacaity",
        "                    USAGE: r <n> where n is the new capacity of the new queue ",
        "[L]inear Search - Linearly search for an element and print the index",
        "                    USAGE: l <item> where item is the item to search for in the queue",
        "[B]inary Search - Use binary search to search for an element and print the index",
        "                    USAGE: b <item> where item is the item to search for in the queue",
        "                    WARNING: This operation messes with the queue!"
    }
    Sub QueueExec()
        Dim Q As New Queue(Of String)


        Preamble(InstructionDescriptions)
        While True
            Dim instructions = Console.ReadLine()
            If String.IsNullOrEmpty(instructions) Or instructions(0) = "." Then
                Exit While
            End If
            Dim instructionList = instructions.Split(",")
            Dim commands = instructionList.Select(Function(s) New Command(s))
            For Each command As Command In commands

                Dim errorMessage = command.Exec(Q)
                If errorMessage <> "" Then
                    Console.ForegroundColor = ConsoleColor.DarkRed
                    Console.WriteLine("Unable to execute command!! ERROR MESSAGE: " + errorMessage)
                    Console.ResetColor()
                End If

            Next

            Console.WriteLine(Q)

        End While
    End Sub
    Private Enum Operation
        Enque
        Deque
        Reset
        LinearSearch
        BinarySearch
        Err

    End Enum
    Private Function CharacterToOp(c As Char) As Operation
        Select Case c
            Case "E"
            Case "e"
                Return Operation.Enque
            Case "D"
            Case "d"
                Return Operation.Deque
            Case "R"
            Case "r"
                Return Operation.Reset
            Case "L"
            Case "l"
                Return Operation.LinearSearch
            Case "B"
            Case "b"
                Return Operation.BinarySearch


        End Select
        Return Operation.Err
    End Function

    Private Structure Command
        Dim c As Char
        Dim op As Operation
        Dim args As String()
        Public Sub New(s As String)
            s = s.Trim()
            op = CharacterToOp(s(0))
            If op <> Operation.Deque And op <> Operation.Err Then
                Dim index = s.IndexOf(" ")
                If index > 0 Then
                    args = s.Substring(index).Trim().Split(" ")
                Else
                    op = Operation.Err
                End If

            End If
            c = s(0)

        End Sub
        Public Function Exec(ByRef q As Queue(Of String)) As String
            Try
                Select Case op
                    Case Operation.Enque

                        For Each arg As String In args
                            q.Enqueue(arg)
                        Next
                    Case Operation.Deque

                        q.Dequeue()

                    Case Operation.Reset
                        Dim cap = Integer.Parse(args(0))
                        q = New Queue(Of String)(cap)
                    Case Operation.Err

                        Return $"Unknown command in input '{c}' or improper arguments supplied"
                    Case Operation.LinearSearch
                        Dim searchItem = args(0)
                        Dim min = Math.Min(q.FrontPointer, q.RearPointer)
                        min = Math.Max(0, min)

                        Dim max = Math.Max(q.FrontPointer, q.RearPointer)
                        For i = min To max
                            If q.Data(i) = searchItem Then
                                Console.WriteLine($"Index {i} Contains {searchItem}")
                                Return ""
                            End If
                        Next
                        Console.WriteLine("Could not find item")
                    Case Operation.BinarySearch
                        Dim min = Math.Min(q.FrontPointer, q.RearPointer)
                        Dim max = Math.Max(q.FrontPointer, q.RearPointer)
                        Dim searchItem = args(0)
                        InsertionSort(q.Data, min, max + 1)
                        Dim start = min + 1
                        Dim _end = max + 1
                        Dim mid = (start + _end) >> 1
                        While start <> _end AndAlso q.Data(mid) <> searchItem
                            If searchItem > q.Data(mid) Then
                                start = mid + 1
                            Else
                                _end = mid - 1
                            End If
                            mid = (start + _end) >> 1
                        End While

                        If q.Data(mid) = searchItem Then
                            Console.WriteLine($"Found {searchItem} at index {mid}")
                        Else
                            Console.WriteLine($"{searchItem} does not exist in queue")
                        End If




                End Select
            Catch e As Exception
                Return e.Message
            End Try
            Return ""

        End Function
    End Structure


    Public Class Queue(Of T)
        Private ReadOnly _cap = 10

        Private Function GetCapacity() As Integer
            Return _cap
        End Function

        Public Property FrontPointer As Integer = -1
        Public Property RearPointer As Integer = -1
        Private _len = 0

        Public Function GetLength() As Integer
            Return _len
        End Function

        Public Data(_cap - 1) As T
        Public Sub New()

        End Sub
        Public Sub New(capacity As Integer)
            ReDim Preserve Data(capacity - 1)
            _cap = capacity
        End Sub

        Public Sub Enqueue(item As T)
            If _len = _cap Then
                Throw New Exception("Queue is Full")
            End If
            RearPointer = (RearPointer + 1) Mod _cap
            _len += 1
            Data(RearPointer) = item
        End Sub
        Public Function Dequeue() As T
            If _len = 0 Then
                Throw New Exception("Queue is empty")
            End If
            FrontPointer = (FrontPointer + 1) Mod _cap
            _len -= 1
            Return Data(FrontPointer)
        End Function
        Public Overrides Function ToString() As String
            Dim arrowlenfp = Data.Take(FrontPointer).Sum(Function(x) x.ToString().Length)
            Dim arrowlenrp = Data.Take(RearPointer).Sum(Function(x) x.ToString().Length)
            Dim q As New StringBuilder()
            Dim arrow = ""
            For i = 0 To arrowlenfp + FrontPointer
                arrow += "-"
                '        
            Next
            arrow += "v"
            q.AppendLine($"front pointer [{FrontPointer}]")
            q.AppendLine(arrow)
            Dim s = "["
            Dim j = 0
            For Each item As T In Data


                If item Is Nothing Then
                    s += "_"
                Else
                    s += item.ToString()
                End If

                If j <> Data.Length - 1 Then
                    s += ","
                End If

                j += 1
            Next
            s += "]"
            q.AppendLine(s)
            arrow = ""
            For k = 0 To arrowlenrp + RearPointer
                arrow += "-"
            Next
            arrow += "^"
            q.AppendLine(arrow)
            q.AppendLine($"rear pointer [{RearPointer}]")
            Return q.ToString()
        End Function
    End Class

End Module
