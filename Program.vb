Imports System

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
        Dim op As Operation
        Dim args As String()
        Public Sub New(o As Char, arg As String())
            op = CharacterToOp(o)
            args = arg

        End Sub
        Public Function Exec(q As Queue(Of String)) As String
            Select Case op
                Case Operation.Enque
                    Try
                        For Each arg As String In args
                            q.Enqueue(arg)
                        Next
                    Catch e As Exception
                        Return e.Message
                    End Try
                Case Operation.Deque
                    q.Dequeue()

            End Select
        End Function
    End Structure
    Dim InstructionDescriptions As String() = {
        "[D]equeue - Remove an item from the queue",
        "[E]nqueue - Add an item to the queue",
        "[R]eset - Empty the queue and enter the maximum capacaity"
    }
    Sub Main(args As String())
        Console.WriteLine("Enter a comma seperated list of instructions from the list e.g E 4, D, E 3, R")
        For Each instruction As String In InstructionDescriptions
            Console.WriteLine(instruction)
        Next
        Dim instructions = Console.ReadLine()
        Dim instructionList = instructions.Split(" ")
        Dim commands = instructionList.Select(Function(s) New Command(s(0), s.Substring(1).Split(" ")))



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

    End Class
End Module
