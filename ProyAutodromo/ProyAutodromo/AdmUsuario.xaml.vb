
Imports System.Data
Imports System.Data.OleDb
Imports System.Globalization
Public Class AdmUsuario
    Public Modo As Int16
    Public IdModificar As Int32


    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If Len(Trim(txtNombreUsuario.Text)) = 0 Or Len(Trim(txtlogin.Text)) = 0 Or Len(Trim(txtContraseña.Text)) = 0 Then
            'Lado verdadero 
            MessageBox.Show("Por favor captura todos los campos")
            Return
        Else
            'Lado Falso


            Try
                Dim EsAdmin As New Int32
                If (chkesadmin.IsChecked) Then
                    EsAdmin = 1
                Else
                    EsAdmin = 0
                End If

                Conectar()



                Dim QueryInsertar_Actualizar As String


                Select Case Modo
                    Case 1 'Nuevo
                        QueryInsertar_Actualizar = "insert into CfgUsuario(NomUsuario,Contra,Log,TipoUser) values (@NomUsuario,@Contra,@Log,@TipoUser)"
                    Case 2 'Editar
                        QueryInsertar_Actualizar = "Update CfgUsuario set NomUsuario = @NomUsuario ,Contra =@Contra,Log = @Log,TipoUser = @TipoUser where ID =@ID"
                End Select

                Dim PrepararInsert As New OleDbCommand(QueryInsertar_Actualizar, con)
                PrepararInsert.Parameters.AddWithValue("@NomUsuario", txtNombreUsuario.Text)
                PrepararInsert.Parameters.AddWithValue("@Contra", txtContraseña.Text)
                PrepararInsert.Parameters.AddWithValue("@Log", txtlogin.Text)
                PrepararInsert.Parameters.AddWithValue("@TipoUser", EsAdmin)


                If (Modo = 2) Then
                    PrepararInsert.Parameters.AddWithValue("@ID", IdModificar)
                End If


                PrepararInsert.ExecuteNonQuery()
                MessageBox.Show("Se registro correctamente el usuario")
                con.Close()
                'Captura de otro registro 
                Dim result As Integer = MessageBox.Show("Desea capturar otro registro ", "caption", MessageBoxButton.YesNo)

                If result = MessageBoxResult.Yes Then
                    txtNombreUsuario.Text = ""
                    txtContraseña.Text = ""
                    txtlogin.Text = ""
                    chkesadmin.IsChecked = False
                Else
                    Me.Close()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try
        End If

    End Sub
End Class
