Imports System.Data
Imports System.Data.OleDb
Imports System.Globalization
Public Class TraCategorias
    Public Modo As Int16
    Public IdModificar As Int32

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If Len(Trim(txtNombreCategoria.Text)) = 0 Or Len(Trim(txtDesc.Text)) = 0 Then
            'Lado verdadero 
            MessageBox.Show("Por favor captura todos los campos")
            Return
        Else
            'Lado Falso


            Try
                Conectar()


                Dim QueryInsertar_Actualizar As String



                Select Case Modo
                    Case 1 'Nuevo
                        QueryInsertar_Actualizar = "insert into AdmTipoVehiculo(NomTipoVehiculo,Descripción) values (@Categoria,@Desc)"
                    Case 2 'Editar
                        QueryInsertar_Actualizar = "Update AdmTipoVehiculo set NomTipoVehiculo = @Categoria ,Descripción =@Desc where ID =@ID"
                End Select


                Dim PrepararInsert As New OleDbCommand(QueryInsertar_Actualizar, con)
                PrepararInsert.Parameters.AddWithValue("@Categoria", txtNombreCategoria.Text)
                PrepararInsert.Parameters.AddWithValue("@Desc", txtDesc.Text)


                If (Modo = 2) Then
                    PrepararInsert.Parameters.AddWithValue("@ID", IdModificar)
                End If
                PrepararInsert.ExecuteNonQuery()
                MessageBox.Show("Se registro correctamente la Categoría")
                con.Close()





                'Captura de otro registro 
                Dim result As Integer = MessageBox.Show("Desea capturar otro registro ", "caption", MessageBoxButton.YesNo)

                If result = MessageBoxResult.Yes Then



                    txtNombreCategoria.Text = ""
                    txtDesc.Text = ""

                Else
                    Me.Close()
                End If










            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try

        End If
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
End Class
