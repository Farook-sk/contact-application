Imports System.Data.OleDb
Public Class Form1


    ReadOnly Con As New OleDbConnection(My.Settings.ContactsDatabaseConnectionString)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TextBox1.Visible = True
        Me.TextBox2.Visible = True
        Me.Button1.Visible = True


        Me.TextBox3.Visible = False
        Me.Button2.Visible = False
        Me.Label3.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text <> "" And TextBox2.Text <> "" Then

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            Using command As New OleDbCommand("select count(*) from t_loginDetails where  [User_Id] = @uname AND [Password] = @pass", Con)
                command.Parameters.AddWithValue("@uname", OleDbType.VarChar).Value = TextBox1.Text.Trim
                command.Parameters.AddWithValue("@pass", OleDbType.VarChar).Value = TextBox2.Text.Trim

                Dim count = Convert.ToInt32(command.ExecuteScalar())
                If count > 0 Then
                    Me.Hide()
                    Form2.Show()
                Else
                    MessageBox.Show("user details not found")
                End If

            End Using
            Con.Close()
        Else

            MsgBox("please enter the login details")


        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text <> "" And TextBox2.Text <> "" And TextBox3.Text <> "" Then

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            Using create As New OleDbCommand("insert into T_LoginDetails([User_Id],[Password],[Secret]) values(@name, @pass1 ,@secrs)", Con)
                create.Parameters.AddWithValue("@name", OleDbType.VarChar).Value = TextBox1.Text.Trim
                create.Parameters.AddWithValue("@pass1", OleDbType.VarChar).Value = TextBox2.Text.Trim
                create.Parameters.AddWithValue("@secrs", OleDbType.VarChar).Value = TextBox3.Text.Trim

                If create.ExecuteNonQuery Then
                    MessageBox.Show("Account Created ", "information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MsgBox("create account fail")
                End If

            End Using
            Con.Close()

        Else

            MsgBox("please enter the login details")

        End If

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.TextBox3.Visible = True
        Me.Button2.Visible = True
        Me.Label3.Visible = True
    End Sub
End Class
