Imports System.Data
Imports System.Data.OleDb
Public Class AdmProcentaje
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Conectar()

            Dim MyCommand As OleDb.OleDbCommand
            Dim rsCaregorias As OleDbDataReader

            MyCommand = con.CreateCommand
            MyCommand.CommandText = "SELECT top 1 * FROM AdmPorcentajeGanancia order by ID desc"
            rsCaregorias = MyCommand.ExecuteReader()
            If rsCaregorias.HasRows Then
                Do While rsCaregorias.Read()
                    TxtPorcentajeNormal.Text = rsCaregorias(1).ToString()
                    PorcentajeMunidal.Text = rsCaregorias(2).ToString()
                Loop
            Else
                MessageBox.Show("No se encontro datos para cobro")
            End If
            rsCaregorias.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        con.Close()


    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If TxtPorcentajeNormal.Text.Length <= 0 & PorcentajeMunidal.Text.Length <= 0 Then


            Try
                con.Open()
                Dim cantidadNormal As New Integer
                Dim cantidadMundial As New Integer
                cantidadNormal = Convert.ToInt32(TxtPorcentajeNormal.Text)
                cantidadMundial = Convert.ToInt32(PorcentajeMunidal.Text)

                Dim QueryInsertar_Actualizar As String
                QueryInsertar_Actualizar = "Update AdmPorcentajeGanancia set GananciaNormal = @GananciaNormal ,GananciaTorneo = @GananciaTorneo  where ID=1 "
                Dim PrepararInsert As New OleDbCommand(QueryInsertar_Actualizar, con)
                PrepararInsert.Parameters.AddWithValue("@GananciaNormal", cantidadNormal)
                PrepararInsert.Parameters.AddWithValue("@GananciaTorneo", cantidadMundial)
                PrepararInsert.ExecuteNonQuery()

                MessageBox.Show("Se actualizo correctamente")

                con.Close()
                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        Else
            MessageBox.Show("Llene la cantidades de porcentaje por favor")
        End If
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
End Class
