Imports System
Imports System.Text

Module Program
    Enum Operation
        Enque
        Deque
        Reset
        Err

    End Enum
    Function CharacterToOp(c As Char) As Operation
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

        End Select
        Return Operation.Err
    End Function
    Structure Command
        Dim c As Char
        Dim op As Operation
        Dim args As String()
        Public Sub New(s As String)
            s = s.Trim()
            op = CharacterToOp(s(0))
            If op = Operation.Enque Then
                args = s.Substring(s.IndexOf(" ")).Trim().Split(" ")
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
                        Console.WriteLine("Enter the new capacity of the queue")
                        Dim cap = Integer.Parse(Console.ReadLine())
                        q = New Queue(Of String)(cap)
                    Case Operation.Err

                        Return $"Unknown command in input '{c}'"

                End Select
            Catch e As Exception
                Return e.Message
            End Try
            Return ""

        End Function
    End Structure
    Dim InstructionDescriptions As String() = {
        "[D]equeue  - Remove an item from the queue",
        "[E]nqueue  - Add an item to the queue",
        "[R]eset    - Empty the queue and enter the maximum capacaity"
    }
    Sub Main()
        Dim Q As New Queue(Of String)

        Dim position_set As Boolean = False
        Console.WriteLine("Enter a comma seperated list of instructions from the list e.g E 4, D, E 3 2 3 11 1 Hello, R")
        Console.WriteLine("Press Enter or q to quit")
        For Each instruction As String In InstructionDescriptions
            Console.WriteLine(instruction)
        Next
        While True
            Dim instructions = Console.ReadLine()
            If String.IsNullOrEmpty(instructions) Or instructions(0) = "q" Then
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

            If position_set Then
                'Console.SetCursorPosition(left, top)
                Console.Clear()
            End If

            position_set = True
            Console.WriteLine(Q)



        End While




    End Sub

    Public Class Queue(Of T)
        Private ReadOnly _cap = 10

        Private Function GetCapacity() As Integer
            Return _cap
        End Function

        Private Property FrontPointer = -1
        Private Property RearPointer = -1
        Private _len = 0

        Public Function GetLength() As Integer
            Return _len
        End Function

        Private Data(_cap - 1) As T
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

                Dim str = "_"
                If item IsNot Nothing Then
                    str = item.ToString()
                End If
                If str.Length = 0 Then
                    s += "_"
                Else
                    s += str
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
