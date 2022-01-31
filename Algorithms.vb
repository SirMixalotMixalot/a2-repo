Module Algorithms
    Sub InsertionSort(Of T As IComparable)(ByRef Data() As T, start As Integer, _end As Integer)
        For i = start + 1 To _end - 1
            Dim item = Data(i)
            Dim p = i - 1

            While p >= 0 AndAlso item.CompareTo(Data(p)) < 0
                Data(p + 1) = Data(p)
                p -= 1
            End While
            Data(p + 1) = item
        Next
    End Sub
End Module
