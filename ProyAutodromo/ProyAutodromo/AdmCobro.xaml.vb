Imports System.Data
Imports System.Data.OleDb

Public Class AdmCobro
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        'Buscar la ultima cantidad de cobro

        Try
            Conectar()

            Dim MyCommand As OleDb.OleDbCommand
            Dim rsCaregorias As OleDbDataReader

            MyCommand = con.CreateCommand
            MyCommand.CommandText = "SELECT top 1 * FROM AdmCobro order by ID desc"


            rsCaregorias = MyCommand.ExecuteReader()


            If rsCaregorias.HasRows Then
                Do While rsCaregorias.Read()

                    txtCantidadCobro.Text = rsCaregorias(1).ToString()


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


        If txtCantidadCobro.ToString().Length > 0 Then


            Try
                con.Open()
                Dim cantidad As New Integer
                cantidad = Convert.ToInt32(txtCantidadCobro.Text)



                Dim QueryInsertar_Actualizar As String
                QueryInsertar_Actualizar = "Update AdmCobro set Cantidad = @Cantidad   where ID=2 "
                Dim PrepararInsert As New OleDbCommand(QueryInsertar_Actualizar, con)
                PrepararInsert.Parameters.AddWithValue("@Cantidad", cantidad)
                PrepararInsert.ExecuteNonQuery()

                MessageBox.Show("Se actualizo correctamente")

                con.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        Else
            MessageBox.Show("Llene la cantidad de cobro por favor")
        End If


    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
End Class
