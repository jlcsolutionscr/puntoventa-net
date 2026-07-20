Imports LeandroSoftware.ClienteWCF

Public Class FrmCodigoPIN
    Private Sub txtPIN4_TextChanged(sender As Object, e As EventArgs) Handles txtPIN4.TextChanged
        Dim bolError = False
        If txtPIN1.Text <> "" And txtPIN2.Text <> "" And txtPIN3.Text <> "" And txtPIN4.Text <> "" Then
            FrmPrincipal.strBusqueda = txtPIN1.Text & txtPIN2.Text & txtPIN3.Text & txtPIN4.Text
            Close()
        Else
            MessageBox.Show("El PIN ingresado es incorrecto. Por favor verifique la información suministrada!", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPIN1.Text = ""
            txtPIN2.Text = ""
            txtPIN3.Text = ""
            txtPIN4.Text = ""
            txtPIN1.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtPIN1_TextChanged(sender As Object, e As EventArgs) Handles txtPIN1.TextChanged
        txtPIN2.Focus()
    End Sub

    Private Sub txtPIN2_TextChanged(sender As Object, e As EventArgs) Handles txtPIN2.TextChanged
        txtPIN3.Focus()
    End Sub

    Private Sub txtPIN3_TextChanged(sender As Object, e As EventArgs) Handles txtPIN3.TextChanged
        txtPIN4.Focus()
    End Sub

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs) Handles txtPIN1.KeyPress, txtPIN2.KeyPress, txtPIN3.KeyPress, txtPIN4.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub
End Class