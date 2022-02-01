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
    End Class
End Module
