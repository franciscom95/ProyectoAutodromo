Imports System.Data
Imports System.Data.OleDb
Imports System.Globalization
Public Class AdmTorneoMundial
    Public Modo As Int16
    Public IdModificar As Int32
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If Len(Trim(txtNombreTorneo.Text)) = 0 Or Len(Trim(txtCosto.Text)) = 0 Or Len(Trim(txtNumeroPistas.Text)) = 0 Then
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
                        QueryInsertar_Actualizar = "insert into AdmTorenoMundial(NomTorneo,NumeroPistas,CostoXParticipante) values (@NomTorneo,@NumeroPistas,@CostoXParticipante)"
                    Case 2 'Editar
                        QueryInsertar_Actualizar = "Update AdmTorenoMundial set NomTorneo = @NomTorneo ,NumeroPistas =@NumeroPistas,CostoXParticipante = @CostoXParticipante where IdToreno =@ID"
                End Select

                Dim PrepararInsert As New OleDbCommand(QueryInsertar_Actualizar, con)
                PrepararInsert.Parameters.AddWithValue("@NomTorneo", txtNombreTorneo.Text)
                PrepararInsert.Parameters.AddWithValue("@NumeroPistas", Convert.ToInt32(txtNumeroPistas.Text))
                PrepararInsert.Parameters.AddWithValue("@CostoXParticipante", Convert.ToInt32(txtCosto.Text))



                If (Modo = 2) Then
                    PrepararInsert.Parameters.AddWithValue("@IdToreno", IdModificar)
                End If


                PrepararInsert.ExecuteNonQuery()
                MessageBox.Show("Se registro correctamente el usuario")
                con.Close()
                'Captura de otro registro 
                Dim result As Integer = MessageBox.Show("Desea capturar otro registro ", "caption", MessageBoxButton.YesNo)

                If result = MessageBoxResult.Yes Then
                    txtNombreTorneo.Text = ""
                    txtCosto.Text = ""
                    txtNumeroPistas.Text = ""

                Else
                    Me.Close()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try
        End If

    End Sub

    Private Sub Button_Click2(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
End Class
