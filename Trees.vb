﻿Module Trees
    Class Tree(Of T As IComparable)
        Dim left As Tree(Of T) = Nothing
        Dim right As Tree(Of T) = Nothing
        Dim data As T
        Sub New(item As T)
            data = item
        End Sub
        Public Sub Add(item As T)
            If data.CompareTo(item) < 0 Then 'data < item or item > data
                If right IsNot Nothing Then
                    right.Add(item)
                Else
                    right = New Tree(Of T)(item)
                End If
            Else
                If left IsNot Nothing Then
                    left.Add(item)
                Else
                    left = New Tree(Of T)(item)
                End If
            End If
        End Sub
        Function InOrderTraversal() As List(Of T)
            Dim list As New List(Of T)

            If left IsNot Nothing Then
                list.AddRange(left.InOrderTraversal())
            End If
            list.Add(data)
            If right IsNot Nothing Then
                list.AddRange(right.InOrderTraversal())
            End If
            Return list
        End Function
    End Class
End Module
