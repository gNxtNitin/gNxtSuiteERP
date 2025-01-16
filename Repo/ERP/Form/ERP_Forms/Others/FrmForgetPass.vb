Imports System.Data.OleDb

Public Class FrmForgetPass

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub FrmForgetPass_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
        FrmLogin.Show()
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Dispose()
        FrmLogin.Show()
    End Sub
    Sub clear()
        txtUserName.Clear()
        dtpDOB.Value = Today
        txtPOB.Clear()
        txtUserID.Clear()
    End Sub
    Private Function RequiredEntry() As Boolean
        If txtUserName.Text = "" Or txtPOB.Text = "" Or txtUserID.Text = "" Then
            MsgBox("Please enter all information....", MsgBoxStyle.Critical, "Attention...")
            Return True
            Exit Function
        End If
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If RequiredEntry() = True Then
            Return
        End If

        Try

            Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & My.Application.Info.DirectoryPath.ToString() & "\BackUp\testing.Accdb;Persist Security Info=False;")
          
            Dim dr1 As OleDbDataReader
            Dim com As New OleDbCommand

            If FrmLogin.CBformState.Text = "User" Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn.Open()
                com.CommandText = "select [UserID],[UserName],[DateOfBirth],[PlaceOfBirth],[Pass] from userinfo where userid = '" & txtUserID.Text & "'"
                com.Connection = cn
                If cn.State = ConnectionState.Closed Then cn.Open()
                dr1 = com.ExecuteReader
                If dr1.Read Then
                    If UCase(dr1(1)) = UCase(txtUserName.Text) And UCase(dr1(2)) = UCase(dtpDOB.Text) And UCase(dr1(3)) = UCase(txtPOB.Text) Then
                        MessageBox.Show("Your password is  .:[ '" & UCase(dr1(4)).ToString() & "' ]:. ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LinkLabel2.Visible = True
                        cn.Close()
                    Else
                        MessageBox.Show("Incorect input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        cn.Close()
                    End If
                Else
                    MessageBox.Show("UserID is wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cn.Close()
                End If

            End If

            If FrmLogin.CBformState.Text = "Admin" Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn.Open()
                com.CommandText = "select [UserID],[UserName],[DateOfBirth],[PlaceOfBirth],[Pass] from AdminInfo where userid = '" & txtUserID.Text & "'"
                com.Connection = cn
                dr1 = com.ExecuteReader
                If dr1.Read Then
                    If UCase(dr1("UserName")) = UCase(txtUserName.Text) And UCase(dr1("DateOfBirth")) = UCase(dtpDOB.Text) And UCase(dr1("PlaceOfBirth")) = UCase(txtPOB.Text) Then
                        MessageBox.Show("Your password is  .:[ '" & UCase(dr1(4)).ToString() & "' ]:. ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LinkLabel2.Visible = True
                        cn.Close()
                    Else
                        MessageBox.Show("Incorect input!!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        cn.Close()
                    End If
                Else
                    MessageBox.Show("UserID is wrong!!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cn.Close()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString(), "Data Error")
            Exit Sub
        End Try

    End Sub

    Private Sub FrmForgetPass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDOB.Value = Today
    End Sub
End Class