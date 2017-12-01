Imports System
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Net.NetworkInformation
Imports ProyAutodromo

Public Class AdmParticipanteTorneo
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        CargarCombo()
        CargarComboTorneos()
    End Sub


    Private Sub CargarCombo()

        Try
            Conectar()

            Dim MyCommand As OleDb.OleDbCommand
            Dim rsCaregorias As OleDbDataReader

            MyCommand = con.CreateCommand
            MyCommand.CommandText = "SELECT * FROM AdmTipoVehiculo order by ID desc"


            rsCaregorias = MyCommand.ExecuteReader()


            If rsCaregorias.HasRows Then
                Do While rsCaregorias.Read()
                    'Si tiene Registros la consulta 

                    Me.cbxCategorias.Items.Add(rsCaregorias(1).ToString())
                    'cbxCategorias.
                Loop
            Else
                MessageBox.Show("No se encontro datos para categorías")
            End If
            rsCaregorias.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

        con.Close()





    End Sub


    Private Sub CargarComboTorneos()
        Try
            Conectar()

            Dim MyCommand As OleDb.OleDbCommand
            Dim rsCaregorias As OleDbDataReader

            MyCommand = con.CreateCommand
            MyCommand.CommandText = "SELECT * FROM AdmTorenoMundial order by IdToreno desc"


            rsCaregorias = MyCommand.ExecuteReader()


            If rsCaregorias.HasRows Then
                Do While rsCaregorias.Read()
                    'Si tiene Registros la consulta 

                    Me.cbxTorneo.Items.Add(rsCaregorias(1).ToString())
                    'cbxCategorias.
                Loop
            Else
                MessageBox.Show("No se encontro datos para categorías")
            End If
            rsCaregorias.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

        con.Close()
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If Len(Trim(txtNombreCompetidor.Text)) = 0 Or Len(Trim(txtPlacas.Text)) = 0 Then
            'Lado verdadero 
            MessageBox.Show("Por favor captura todos los campos")
            Return
        Else
            'Captura de otro registro

            Dim resultCobro As Integer = MessageBox.Show("Debes cobrar antes de guardar la cantida de :  " + BuscarCosto(cbxTorneo.SelectedItem.ToString()).ToString(), "caption", MessageBoxButton.YesNo)
            If resultCobro = MessageBoxResult.Yes Then

#Region "Insertar"
                Try
                    Conectar()
                    Dim _buscarid = BuscarID(cbxTorneo.SelectedItem.ToString())
                    Dim _buscaridVehiculo = BuscarIdVehiculo(cbxCategorias.SelectedItem.ToString())
                    Dim PrepararInsert As New OleDbCommand("insert into AdmTorneoMundialParticipante(NomCompetidor,IdToreno,Placas,IdTipoVehiculo) values (@NombreCompetidor,@IdTorneo,@Placas,@IDTipoAuto)", con)
                    PrepararInsert.Parameters.AddWithValue("@NombreCompetidor", txtNombreCompetidor.Text)
                    PrepararInsert.Parameters.AddWithValue("@IDTipoAuto", _buscaridVehiculo)
                    PrepararInsert.Parameters.AddWithValue("@Placas", txtPlacas.Text)
                    PrepararInsert.Parameters.AddWithValue("@IdTorneo", _buscarid)

                    PrepararInsert.ExecuteNonQuery()
                    MessageBox.Show("Se registro correctamente el Folio del Vehiculo")
                    con.Close()
                    'Captura de otro registro 
                    Dim result As Integer = MessageBox.Show("Desea capturar otro registro ", "caption", MessageBoxButton.YesNo)
                    If result = MessageBoxResult.Yes Then
                        txtNombreCompetidor.Text = ""
                        cbxCategorias.SelectedItem = ""
                        txtPlacas.Text = ""
                        cbxTorneo.SelectedItem = ""
                    Else
                        Me.Close()
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.ToString())
                End Try
#End Region

            Else
                MessageBox.Show("Realiza el cobro")
            End If
        End If
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
    Public Function BuscarID(ByVal NombreCategoria As String) As Int32

        Dim ID As Integer

        Dim ObtenerID As New OleDbCommand("SELECT IdToreno FROM AdmTorenoMundial where NomTorneo = @Nombre", con)
        ObtenerID.Parameters.AddWithValue("@Nombre", NombreCategoria)
        ID = Convert.ToInt32(ObtenerID.ExecuteScalar())


        Return ID
    End Function
    Public Function BuscarIdVehiculo(ByVal NombreCategoria As String) As Int32


        Dim ID As Integer

        Dim ObtenerID As New OleDbCommand("SELECT ID FROM AdmTipoVehiculo where NomTipoVehiculo = @Nombre", con)
        ObtenerID.Parameters.AddWithValue("@Nombre", NombreCategoria)
        ID = Convert.ToInt32(ObtenerID.ExecuteScalar())


        Return ID
    End Function
    Public Function BuscarCosto(ByVal NombreCategoria As String) As Int32


        con.Open()
        Dim ID As Integer

        Dim ObtenerID As New OleDbCommand("SELECT CostoXParticipante FROM AdmTorenoMundial where NomTorneo = @Nombre", con)
        ObtenerID.Parameters.AddWithValue("@Nombre", NombreCategoria)
        ID = Convert.ToInt32(ObtenerID.ExecuteScalar())
        con.Close()

        Return ID
    End Function


End Class

