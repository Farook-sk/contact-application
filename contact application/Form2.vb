
Imports System.Data.OleDb
Public Class Form2
    ReadOnly Con As New OleDbConnection(My.Settings.ContactsDatabaseConnectionString)

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DataGridView1.DataSource = getcontactdetails()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        If TextBox1.Text <> "" And TextBox2.Text <> "" And TextBox3.Text <> "" Then



            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If


            Using create As New OleDbCommand("insert into Contact_details([Contact_Name],[Contact_PhoneNO],[Contact_Email]) values(@name, @phone ,@email)", Con)
                create.Parameters.AddWithValue("@name", OleDbType.VarChar).Value = TextBox1.Text.Trim
                create.Parameters.AddWithValue("@phone", OleDbType.VarChar).Value = TextBox2.Text.Trim
                create.Parameters.AddWithValue("@email", OleDbType.VarChar).Value = TextBox3.Text.Trim


                If create.ExecuteNonQuery Then
                    MessageBox.Show(" contact details saved ", "information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MsgBox("contact details saving fail")
                End If

            End Using

            Me.DataGridView1.DataSource = getcontactdetails()
        Else

            MsgBox("Please enter all the details before saving")

        End If


        Me.DataGridView1.DataSource = getcontactdetails()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.Hide()
        Form1.TextBox1.Text = ""
        Form1.TextBox2.Text = ""
        Form1.TextBox3.Text = ""
        Form1.Show()
    End Sub

    Private Function getcontactdetails() As DataTable

        Dim grtcontcttable As New DataTable

        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Using Command As New OleDbCommand("select * from Contact_details", Con)


            Dim reader As OleDbDataReader = Command.ExecuteReader()

            grtcontcttable.Load(reader)


        End Using

        Return grtcontcttable


    End Function


End Class