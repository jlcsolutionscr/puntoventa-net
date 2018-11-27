Public Class FrmInicio
#Region "Eventos"
    Private Sub FrmInicio_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        lblVersion.Text = "Versión " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision
    End Sub

    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Close()
    End Sub

    Private Sub CmdClave_Click(sender As Object, e As EventArgs)
        Dim formActualizarClave As New FrmActualizarClave()
        formActualizarClave.ShowDialog()
    End Sub
#End Region
End Class