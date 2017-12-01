Imports System
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Net.NetworkInformation
Imports System.Timers
Imports System.Windows.Threading
Public Class Menu
    Public parametros As Object
    Public Parametroslista As New List(Of String)
    Public EsAdministrador As Boolean


    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Actualizar()

        Dim dt As DispatcherTimer = New DispatcherTimer()


        ' Aqui se busca por minuto
        dt.Interval = TimeSpan.FromSeconds(15)
        AddHandler dt.Tick, AddressOf dispatcherTimer_Tick

        dt.Start()

    End Sub

    Public Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)


        Actualizar()

    End Sub

    Private Sub Actualizar()
        Try
            con.Close()
            Conectar()
            Dim Query As String
            Query = "SELECT * FROM AdmBuffer  Where   Id = @Id"
            Dim PrepararInsert As New OleDbCommand(Query, con)


            PrepararInsert.Parameters.AddWithValue("@Id", 1)





            Dim reader As OleDbDataReader = PrepararInsert.ExecuteReader()
            If reader.HasRows Then
                Do While reader.Read()
                    competidor1.Text = reader(1).ToString()
                    competidor2.Text = reader(2).ToString()
                    lblPista.Content = "Pista : " + reader(3).ToString()
                Loop
            Else
                MessageBox.Show("No hay Competidores Disponibles ")
            End If
            reader.Close()




        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        con.Close()

    End Sub

End Class