Imports System.Text
Module StackModule
    Public Class Stack(Of T)
        Private ReadOnly _cap = 10
        Public Data(_cap - 1) As T
        Public Length As Integer = 0
        Public _sp As Integer = 0
        Public Sub New()

        End Sub
        Public Sub New(capacity As Integer)
            ReDim Preserve Data(capacity - 1)
            _cap = capacity
        End Sub
        Public Sub Push(item As T)
            If _sp = _cap Then
                Throw New Exception("Stack is full")
            End If
            Data(_sp) = item
            _sp += 1
            Length += 1
        End Sub
        Public Function Pop() As T
            If Length = 0 Then
                Throw New Exception("Stack is empty")
            End If
            _sp -= 1
            Length -= 1
            Return Data(_sp)

        End Function
        Public Overrides Function ToString() As String
            Dim arrowLength = Data.Take(_sp).Sum(Function(x) x.ToString().Length)
            Dim stack As New StringBuilder()
            Dim arrow = ""
            For i = 0 To arrowLength + _sp
                arrow += "-"
            Next
            arrow += "v"
            stack.AppendLine($"Stack Pointer is [{_sp}]")
            stack.AppendLine(arrow)
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
            stack.AppendLine(s)
            Return stack.ToString()
        End Function
    End Class
    Function wordToOperation(word As String) As Operation
        Dim c As Char = word(0)
        Select Case c
            Case "P"
            Case "p"
                If word(1) = "u" Or word(1) = "U" Then
                    Return Operation.Push
                Else
                    Return Operation.Pop
                End If

        End Select
        Return Operation.Err
    End Function
    Enum Operation
        Push
        Pop
        Err
    End Enum
    Class Command
        Dim word As String
        Dim op As Operation
        Dim args As String()
        Public Sub New(s As String)
            Dim index = s.IndexOf(" ")
            word = s
            If index < 0 Then
                op = wordToOperation(s)
            Else
                word = s.Substring(0, index)
                op = wordToOperation(word)
            End If
            If op <> Operation.Pop AndAlso op <> Operation.Err Then
                args = s.Substring(index).Trim().Split(" ")
            End If

        End Sub
        Public Function Exec(ByRef stack As Stack(Of String)) As String
            Try
                Select Case op
                    Case Operation.Pop
                        stack.Pop()
                    Case Operation.Push
                        For Each arg As String In args
                            stack.Push(arg)
                        Next
                    Case Operation.Err
                        Return $"{word} is not a valid operation"
                End Select
            Catch e As Exception
                Return e.Message
            End Try
            Return ""
        End Function
    End Class
    Dim instructionDescriptions As String() = {
        "[Pu]sh",
        "[Po]p"
    }

    Public Sub StackExec()
        Dim stack As New Stack(Of String)
        Preamble(instructionDescriptions)
        While True
            Dim instructionLine = Console.ReadLine()
            If String.IsNullOrWhiteSpace(instructionLine) Or instructionLine(0) = "." Then
                Exit While
            End If
            Dim commands = instructionLine.Split(",").Select(Function(s) New Command(s.Trim()))
            For Each command As Command In commands
                Dim errorMessage = command.Exec(stack)
                If errorMessage <> "" Then
                    Console.ForegroundColor = ConsoleColor.DarkRed
                    Console.WriteLine(errorMessage)
                    Console.ResetColor()
                End If
                Console.WriteLine(stack)
            Next



        End While



    End Sub
End Module
