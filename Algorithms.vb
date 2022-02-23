Module Algorithms
    Sub InsertionSort(Of T As IComparable)(ByRef Data() As T, start As Integer, _end As Integer)
        For i = start + 1 To _end - 1
            Dim item = Data(i)
            Dim p = i - 1

            While p >= 0 AndAlso item.CompareTo(Data(p)) < 0 ' item < Data(p)
                Data(p + 1) = Data(p)
                p -= 1
            End While
            Data(p + 1) = item
        Next
    End Sub
    Sub Preamble(InstructionDescriptions As String())
        Console.WriteLine("Enter a comma seperated list of instructions from the list")
        Console.WriteLine("Press Enter or '.exit' to exit")
        For Each instruction As String In InstructionDescriptions
            Console.WriteLine(instruction)
        Next

    End Sub
End Module
