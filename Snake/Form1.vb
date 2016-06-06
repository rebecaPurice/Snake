Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        create_head()
        create_eat()
        ' tm_snakeMover.Start()
    End Sub
#Region "Snake Stuff" 'sub pentru snake
    Dim snake(1000) As PictureBox
    Dim length_of_snake As Integer = -1
    Dim r As New Random 'folosit pentru mancare, prima aparitie snake ( cap)
    Dim left_right_mover As Integer = 0
    Dim up_down_mover As Integer = 0

    Private Sub create_head()
        length_of_snake += 1
        snake(length_of_snake) = New PictureBox
        With snake(length_of_snake)
            .Height = 10
            .Width = 10
            .BackColor = Color.White
            .Top = r.Next(pb_Field.Top + pb_Field.Bottom) / 2 'apare random, dar in perimetrul pictureBox
            .Left = r.Next(pb_Field.Right + pb_Field.Left) / 2
        End With
        Me.Controls.Add(snake(length_of_snake))
        snake(length_of_snake).BringToFront()

        lengthenSnake() 'la inceput are lungimea 3: cap+2
        lengthenSnake()

    End Sub
    'sub pentru deplasare snake
    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        tm_snakeMover.Start()
        Select Case e.KeyChar
            Case "a"
                left_right_mover = -10
                up_down_mover = 0
            Case "d"
                left_right_mover = 10
                up_down_mover = 0
            Case "w"
                left_right_mover = 0
                up_down_mover = -10
            Case "s"
                left_right_mover = 0
                up_down_mover = 10
        End Select
    End Sub

    Private Sub tm_snakeMover_Tick(sender As Object, e As EventArgs) Handles tm_snakeMover.Tick

        For i = length_of_snake To 1 Step -1
            snake(i).Top = snake(i - 1).Top
            snake(i).Left = snake(i - 1).Left
        Next

        snake(0).Top += up_down_mover
        snake(0).Left += left_right_mover

        collide_With_walls()
        collide_With_eat()
        collide_With_self()

    End Sub
    Private Sub lengthenSnake()
        length_of_snake += 1
        snake(length_of_snake) = New PictureBox
        With snake(length_of_snake)
            .Height = 10
            .Width = 10
            .BackColor = Color.Pink
            .Top = snake(length_of_snake - 1).Top
            .Left = snake(length_of_snake - 1).Left + 10

        End With
        Me.Controls.Add(snake(length_of_snake))
        snake(length_of_snake).BringToFront()
    End Sub
#End Region

#Region "Coliziuni"
    Private Sub collide_With_walls()
        If snake(0).Left < pb_Field.Left Then
            tm_snakeMover.Stop()
            MsgBox("Ai pierdut! Incearca din nou cand vei fi mai odihnit...")
        End If
        If snake(0).Right > pb_Field.Right Then
            tm_snakeMover.Stop()
            MsgBox("Ai pierdut! Incearca din nou cand vei fi mai odihnit...")
        End If
        If snake(0).Top < pb_Field.Top Then
            tm_snakeMover.Stop()
            MsgBox("Ai pierdut! Incearca din nou cand vei fi mai odihnit...")
        End If
        If snake(0).Bottom > pb_Field.Bottom Then
            tm_snakeMover.Stop()
            MsgBox("Ai pierdut! Incearca din nou cand vei fi mai odihnit...")
        End If
    End Sub
    Private Sub collide_With_eat()
        If snake(0).Bounds.IntersectsWith(eat.Bounds) Then
            lengthenSnake()
            eat.Top = r.Next(pb_Field.Top, pb_Field.Bottom - 10)
            eat.Left = r.Next(pb_Field.Left, pb_Field.Right - 10)
        End If
    End Sub
    Private Sub collide_With_self()
        For i = 1 To length_of_snake
            If snake(0).Bounds.IntersectsWith(snake(i).Bounds) Then
                tm_snakeMover.Stop()
                MsgBox("Ti-ai facut rau. Ai pierdut!")
            End If
        Next
    End Sub
#End Region
#Region "Eat"
    Dim eat As PictureBox
    Private Sub create_eat()
        eat = New PictureBox
        With eat
            .Height = 10
            .Width = 10
            .BackColor = Color.Black
            .Top = r.Next(pb_Field.Top, pb_Field.Bottom - 10)
            .Left = r.Next(pb_Field.Left, pb_Field.Right - 10)
        End With
        Me.Controls.Add(eat)
        eat.BringToFront()

    End Sub
#End Region

End Class
