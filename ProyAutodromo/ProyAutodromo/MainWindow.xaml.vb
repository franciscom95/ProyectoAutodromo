Imports System.Windows.Controls
Imports System.Data
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Reflection
Class MainWindow
    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click



        'Dim LLamada As New Menu
        'LLamada.parametros = "PARAMETROS"
        'LLamada.Show()
        ''LLamada.ShowDialog()

        'Aqui empieza lo correcto 
        Try
            Conectar()
            Dim Query As String
            Dim Contra As String
            Contra = textBox_Copy.Password.ToString()

            Query = "SELECT * FROM CfgUsuario Where Log=@Usuario and Contra=@Pass"
            Dim PrepararInsert As New OleDbCommand(Query, con)
            PrepararInsert.Parameters.AddWithValue("@Usuario", textBox.Text)
            PrepararInsert.Parameters.AddWithValue("@Pass", Contra)





            Dim ID As String
            ID = ""

            Dim TipoUsuario As Integer


            Dim reader As OleDbDataReader = PrepararInsert.ExecuteReader()
            If reader.HasRows Then
                Do While reader.Read()
                    ID = reader(0).ToString()
                    TipoUsuario = reader(4).ToString()
                Loop
            Else

                ID = ""
            End If
            reader.Close()


            If (ID = "") Then

                MessageBox.Show("No se encontro información")
            Else
                Me.Hide()



                Dim LLamada As New Menu


                LLamada.Parametroslista.Add(ID)


                If (TipoUsuario = "1") Then

                    LLamada.EsAdministrador = True
                Else

                    LLamada.EsAdministrador = False

                End If


                LLamada.Show()




                End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        con.Close()
    End Sub
End Class
