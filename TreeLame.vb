Module Lame
    Public Class TreeLame(Of T As IComparable)
        Dim _cap As Integer = 10
        Dim _len As Integer = 0
        Dim Data(_cap) As TreeNode(Of T)
        Dim rootPointer As Integer = -1
        Dim heap As Integer = 0

        Sub New()

        End Sub
        Sub New(item As T)
            _len += 1
            Dim root As New TreeNode(Of T)(item)
            Data(heap) = root
            rootPointer = heap
            heap += 1
        End Sub
        Sub Add(item As T)
            If rootPointer = -1 Then
                'Assign root
            End If
            Dim root = rootPointer
            Dim previousRoot
            Dim useLeftBranch As Boolean = False
            While root <> -1
                previousRoot = root
                If Data(root).data.CompareTo(item) > 0 Then 'item < Data(root), go left
                    useLeftBranch = True
                    root = Data(root).leftPointer
                Else
                    useLeftBranch = False
                    root = Data(root).rightPointer
                End If
            End While
            If useLeftBranch Then

            End If
        End Sub
    End Class

    Private Class TreeNode(Of T As IComparable)
        Public leftPointer As Integer = -1
        Public rightPointer As Integer = -1
        Public data As T = Nothing
        Sub New(item As T)
            data = item
            leftPointer = -1
            rightPointer = -1
        End Sub
    End Class

End Module

