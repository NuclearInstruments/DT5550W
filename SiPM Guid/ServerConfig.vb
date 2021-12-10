Imports System.IO

Public Class ServerConfig
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim FolderBrowserDialog1 As New FolderBrowserDialog
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub ServerConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckBox1.Checked = My.Settings.EnableServer
        NumericUpDown1.Value = IIf(My.Settings.ServerPort > 0, My.Settings.ServerPort, 1)
        Dim pth = My.Settings.folderpos
        TextBox1.Text = IIf(File.GetAttributes(pth).HasFlag(FileAttributes.Directory), pth, My.Computer.FileSystem.SpecialDirectories.Desktop)
        TextBox2.Text = IIf(My.Settings.DetectorName <> "", My.Settings.DetectorName, "DET")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim pth = TextBox1.Text
        If File.GetAttributes(pth).HasFlag(FileAttributes.Directory) = False Then
            MsgBox("Path does not exist or is invalid", vbCritical + vbOKOnly)
            Exit Sub
        End If
        My.Settings.EnableServer = CheckBox1.Checked
        My.Settings.ServerPort = NumericUpDown1.Value
        My.Settings.folderpos = TextBox1.Text
        My.Settings.DetectorName = TextBox2.Text
        My.Settings.Save()
        MsgBox("Restart the application to apply server settings!", vbInformation + vbOKOnly)
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class