

Module Program
    Dim MainMenu As String() = {
        "[Q]ueue - Operate a queue and get a visual representation of your actions",
        "[S]tack - Operate a stack and get a visual representation of your actions"
    }


    Sub Main()
        For Each menu As String In MainMenu
            Console.WriteLine(menu)
        Next
        Console.WriteLine("Enter .exit to quit")
        Dim response = Console.ReadLine().ToLower()
        While response(0) <> "."
            Select Case response(0)
                Case "q"
                    QueueExec()
            End Select
            For Each menu As String In MainMenu
                Console.WriteLine(menu)
            Next
            Console.WriteLine("Enter exit to quit")
            response = Console.ReadLine().ToLower()

        End While
    End Sub


End Module
