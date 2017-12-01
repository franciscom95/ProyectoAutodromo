Imports System
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Net.NetworkInformation
Imports ProyAutodromo
Public Class GanadoresMundial
    Public Function BuscarIDTorneo(ByVal NombreCategoria As String) As Int32
        Dim ID As Integer
        Dim ObtenerID As New OleDbCommand("SELECT IdToreno FROM AdmTorenoMundial where NomTorneo = @Nombre", con)
        ObtenerID.Parameters.AddWithValue("@Nombre", NombreCategoria)
        ID = Convert.ToInt32(ObtenerID.ExecuteScalar())
        Return ID
    End Function
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        'Insertar 
#Region "Insertar"
        Try
            Conectar()
            Dim _buscarid = BuscarIDTorneo(cbxTorneo.SelectedItem.ToString())

            Dim name As String

            name = "nombre"

            Dim PrepararInsert As New OleDbCommand("insert into AdmTorneoGanador(IdTorneo,Lugar,Nombre) values (@IdTorneo,@Lugar,@Nombre)", con)
            'PrepararInsert.Parameters.AddWithValue("@NomCompetidor", cbxLugarUno.Text.ToString())
            PrepararInsert.Parameters.AddWithValue("@IdTorneo", _buscarid)
            PrepararInsert.Parameters.AddWithValue("@Lugar", 1)
            PrepararInsert.Parameters.AddWithValue("@Nombre", cbxLugarUno.Text)
            PrepararInsert.ExecuteNonQuery()




            'SegundoLugar
            Dim PrepararInsert2 As New OleDbCommand("insert into AdmTorneoGanador(IdTorneo,Nombre,Lugar) values (@IdTorneo,@Nombre,@Lugar)", con)

            PrepararInsert2.Parameters.AddWithValue("@IdTorneo", _buscarid)
            PrepararInsert2.Parameters.AddWithValue("@Nombre", cbxSegundoLugar.Text)
            PrepararInsert2.Parameters.AddWithValue("@Lugar", 2)
            PrepararInsert2.ExecuteNonQuery()
            'TercerLugar
            Dim PrepararInsert3 As New OleDbCommand("insert into AdmTorneoGanador(IdTorneo,Nombre,Lugar) values (@IdTorneo,@Nombre,@Lugar)", con)
            PrepararInsert3.Parameters.AddWithValue("@IdTorneo", _buscarid)
            PrepararInsert3.Parameters.AddWithValue("@Nombre", cbxTercerLugar.Text)
            PrepararInsert3.Parameters.AddWithValue("@Lugar", 3)
            PrepararInsert3.ExecuteNonQuery()
            MessageBox.Show("Se registro correctamente el Folio del Vehiculo")
            con.Close()
            'Captura de otro registro 
            Dim result As Integer = MessageBox.Show("Desea capturar otro registro ", "caption", MessageBoxButton.YesNo)
            If result = MessageBoxResult.Yes Then
                cbxTorneo.Text = ""
                cbxLugarUno.SelectedItem = ""
                cbxSegundoLugar.Text = ""
                cbxTercerLugar.Text = ""
            Else
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
#End Region
    End Sub
    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
    Private Sub CargarTorneos(sender As Object, e As RoutedEventArgs)
        CargarComboTorneos()
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
    Private Sub CargarParticipantes()
        Try
            Conectar()
            Dim MyCommand As OleDb.OleDbCommand
            Dim rsCaregorias As OleDbDataReader
            MyCommand = con.CreateCommand
            MyCommand.CommandText = "SELECT * FROM AdmTorneoMundialParticipante order by IdToreno desc"
            rsCaregorias = MyCommand.ExecuteReader()
            If rsCaregorias.HasRows Then
                Do While rsCaregorias.Read()
                    'Si tiene Registros la consulta 
                    Me.cbxLugarUno.Items.Add(rsCaregorias(2).ToString())
                    Me.cbxSegundoLugar.Items.Add(rsCaregorias(2).ToString())
                    Me.cbxTercerLugar.Items.Add(rsCaregorias(2).ToString())
                    'cbxCategorias.
                Loop
            Else
                MessageBox.Show("No se encontro datos para Participantes del torneo seleccionado")
            End If
            rsCaregorias.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

        con.Close()
    End Sub
    Private Sub cbxTorneo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        CargarParticipantes()
    End Sub
End Class

