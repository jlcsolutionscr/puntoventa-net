Public Class FrmProcesoCierre
#Region "Variables"
    Private strEstado, SQLString, strFecha As String
    Private FechaDia, FechaCxC As Date
    Private Monto As Decimal
    Private I, IdMovCxC, IdCxC, Cuotas As Integer
    Private objDatosLocal As DataTable
    Private objDataRow As DataRow
#End Region

#Region "Métodos"
    Private Sub EvaluaEstado()
        'txtFecha.Text = FrmMenuPrincipal.objGenericoCN.NVL(FrmMenuPrincipal.objGenericoCN.ObtenerValor("SELECT FechaDia FROM Calendario WHERE IdModulo = 1"))
        'strEstado = FrmMenuPrincipal.objGenericoCN.NVL(FrmMenuPrincipal.objGenericoCN.ObtenerValor("SELECT Estado FROM Calendario WHERE IdModulo = 1"))
        If strEstado = "True" Then
            txtEstado.Text = "Proceso concluido"
        Else
            txtEstado.Text = "Proceso pendiente"
        End If
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub CmdCambiarFecha_Click(sender As Object, e As EventArgs) Handles CmdCambiarFecha.Click
        If MessageBox.Show("Desea ejecutar el cambio de fecha calendario?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            FechaDia = CDate(txtFecha.Text)
            FechaDia = FechaDia.AddDays(1)
            strFecha = "'" & DatePart(DateInterval.Year, FechaDia) & "/" & DatePart(DateInterval.Month, FechaDia) & "/" & DatePart(DateInterval.Day, FechaDia) & "'"
            SQLString = "UPDATE Calendario SET FechaDia = " & strFecha & ", Estado = 0 WHERE IdModulo = 1"
            'FrmMenuPrincipal.objGenericoCN.EjecutarSQL(SQLString)
            SQLString = "UPDATE Usuario Set FondoInicio = FondoInicio + Aporte + VentasContado + IngresosCxC + OtrosIngresos - ComprasContado - SalidasCxP - OtrosGastos"
            'FrmMenuPrincipal.objGenericoCN.EjecutarSQL(SQLString)
            MessageBox.Show("Cambio de Fecha concluido satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            EvaluaEstado()
            REM FrmMenuPrincipal.FechaSistema = CDate(FrmMenuPrincipal.objGenericoCN.ObtenerValor("SELECT FechaDia FROM Calendario WHERE IdModulo = 1"))
            REM remFrmMenuPrincipal.StrFecha = DatePart(DateInterval.Year, FrmMenuPrincipal.FechaSistema) & "/" & DatePart(DateInterval.Month, FrmMenuPrincipal.FechaSistema) & "/" & DatePart(DateInterval.Day, FrmMenuPrincipal.FechaSistema)
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            CmdEjecutar.Enabled = True
            CmdEjecutar.Focus()
            CmdCambiarFecha.Enabled = False
        End If
    End Sub

    Private Sub CmdEjecutar_Click(sender As Object, e As EventArgs) Handles CmdEjecutar.Click
        If MessageBox.Show("Desea ejecutar el proceso de cálculo y acreditación de Interés?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            Try
                FechaDia = CDate(txtFecha.Text)
                strFecha = "'" & DatePart(DateInterval.Year, FechaDia) & "/" & DatePart(DateInterval.Month, FechaDia) & "/" & DatePart(DateInterval.Day, FechaDia) & "'"
                'objDatosLocal = FrmMenuPrincipal.objGenericoCN.ObtenerTabla("SELECT IdCxC, Plazo, Tasa, FechaProceso, Saldo, Tipo FROM CuentaPorCobrar WHERE Nulo = 0 AND Saldo > 0 And TipoPago = 2 And Tasa > 0 And FechaProceso = " & strFecha & " AND IdEmpresa = " & FrmMenuPrincipal.intEmpresa, "ProcesoCierre")
                For Each objDataRow In objDatosLocal.Rows
                    Monto = CDbl(objDataRow.Item(4)) * CDbl(objDataRow.Item(2)) / 100
                    If Monto > 0 Then
                        FechaCxC = FrmPrincipal.ObtenerFechaFormateada(Now())
                        FechaCxC = DateAdd(DateInterval.Month, 1, FechaCxC)
                        strFecha = "'" & DatePart(DateInterval.Year, FechaCxC) & "/" & DatePart(DateInterval.Month, FechaCxC) & "/" & DatePart(DateInterval.Day, FechaCxC) & "'"
                        IdCxC = CShort(objDataRow.Item(0))
                        SQLString = "UPDATE CuentaPorCobrar SET FechaProceso = " & strFecha & ", Saldo = Saldo + " & Monto & " WHERE IdCxC = " & IdCxC
                        'FrmMenuPrincipal.objGenericoCN.EjecutarSQL(SQLString)
                        SQLString = "INSERT INTO MovCuentaPorCobrar(IdMovCxC, IdUsuario, IdCxC, Tipo, Recibo, Fecha, Descripcion, Monto) VALUES(Null, " & FrmPrincipal.usuarioGlobal.IdUsuario & "," & IdCxC & ", 3,'0','" & FrmPrincipal.ObtenerFechaFormateada(Now()) & "','Aumento de saldo por concepto de interés periódico'," & Monto & ")"
                        'FrmMenuPrincipal.objGenericoCN.EjecutarSQL(SQLString)
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        'SQLString = "UPDATE Calendario SET Estado = 1 WHERE IdModulo = 1 AND IdEmpresa = " & FrmMenuPrincipal.intEmpresa
        'FrmMenuPrincipal.objGenericoCN.EjecutarSQL(SQLString)
        MessageBox.Show("Proceso concluido satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        EvaluaEstado()
        CmdCambiarFecha.Enabled = True
        CmdCambiarFecha.Focus()
        CmdEjecutar.Enabled = False
    End Sub

    Private Sub FrmProcesoCierre_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EvaluaEstado()
            If strEstado = "True" Then
                CmdEjecutar.Enabled = False
                CmdCambiarFecha.Enabled = True
            Else
                CmdEjecutar.Enabled = True
                CmdCambiarFecha.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub
#End Region
End Class