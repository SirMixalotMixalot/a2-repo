

Module Program
    Dim MainMenu As String() = {
        "[Q]ueue       - Operate a queue and get a visual representation of your actions",
        "[S]tack       - Operate a stack and get a visual representation of your actions",
        "[L]inked List - Operate a linked list and get a visual representation of your actions",
        "[T]ree        - Operate a binary tree"
    }


    Sub Main()
        For Each menu As String In MainMenu
            Console.WriteLine(menu)
        Next
        Console.WriteLine("Enter .exit to quit")
        Dim response = Console.ReadLine().ToLower()
        While response.Length > 0 AndAlso response(0) <> "."
            Select Case response(0)
                Case "q"
                    QueueExec()
                Case "s"
                    StackExec()
                Case "l"
                    TestLinkedList()
                Case "t"
                    Dim tree As New Tree(Of Integer)()
                    Dim rand As New Random()
                    For i = 0 To 9
                        tree.Add(rand.Next(20))
                    Next
                    Console.WriteLine(String.Join(",", tree.PostOrderTraversal()))
            End Select
            For Each menu As String In MainMenu
                Console.WriteLine(menu)
            Next
            Console.WriteLine("Enter .exit to quit")
            response = Console.ReadLine().ToLower()

        End While
    End Sub


End Module
