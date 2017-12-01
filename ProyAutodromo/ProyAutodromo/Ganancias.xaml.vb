Imports System
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Net.NetworkInformation
Imports ProyAutodromo

Public Class Ganancias
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Dim Estado As ConnectionState
        Estado = con.State
        If (Estado = ConnectionState.Open) Then
            con.Close()
        End If

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

                    Me.CbTorneo.Items.Add(rsCaregorias(1).ToString())
                    'cbxCategorias.
                Loop
            Else
                MessageBox.Show("No se encontro datos para categorías")
                con.Close()

            End If
            rsCaregorias.Close()
            con.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        con.Close()
    End Sub

    Private Sub CbTorneo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

        Dim Estado As ConnectionState
        Estado = con.State
        If (Estado = ConnectionState.Open) Then
            con.Close()
        End If

        con.Open()

        Dim IdTorneo As Integer
        IdTorneo = BuscarIDTorneo(CbTorneo.SelectedItem.ToString())


        Dim Participantes As New Integer
        Dim ObtenerID As New OleDbCommand("SELECT count(*) FROM AdmTorneoMundialParticipante where IdToreno = @ID", con)
        ObtenerID.Parameters.AddWithValue("@ID", IdTorneo)
        Participantes = Convert.ToInt32(ObtenerID.ExecuteScalar())


        Dim Costo As Integer
        Dim ObtenerCosto As New OleDbCommand("SELECT CostoXParticipante FROM AdmTorenoMundial where IdToreno = @ID", con)
        ObtenerCosto.Parameters.AddWithValue("@ID", IdTorneo)
        Costo = Convert.ToInt32(ObtenerCosto.ExecuteScalar())

        Dim Recaudado As Integer
        Recaudado = (Costo * Participantes)




        CantidadParticipantes.Content += " " + Participantes.ToString()
        LblRecaudado.Content += " " + Recaudado.ToString()
        lblCosto.Content += " " + Costo.ToString()
        con.Close()


        ObtenerPrimerLugar(IdTorneo)
        ObtenerSegundoLugar(IdTorneo)
        ObtenerTercerLugar(IdTorneo)
    End Sub

    Private Sub ObtenerPrimerLugar(ByVal id As Integer)
        Try
            Conectar()

            Dim MyCommand As OleDb.OleDbCommand
            Dim rsCaregorias As OleDbDataReader

            MyCommand = con.CreateCommand
            MyCommand.CommandText = "SELECT * FROM AdmTorneoGanador where IdTorneo = @Id and Lugar = 1"
            MyCommand.Parameters.AddWithValue("@Id", id)


            rsCaregorias = MyCommand.ExecuteReader()


            If rsCaregorias.HasRows Then
                Do While rsCaregorias.Read()
                    'Si tiene Registros la consulta 


                    lblUno.Content += rsCaregorias(5).ToString()
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

    Private Sub ObtenerSegundoLugar(ByVal id As Integer)
        Try
            Conectar()

            Dim MyCommand As OleDb.OleDbCommand
            Dim rsCaregorias As OleDbDataReader

            MyCommand = con.CreateCommand
            MyCommand.CommandText = "SELECT * FROM AdmTorneoGanador where IdTorneo = @Id and Lugar = 2"
            MyCommand.Parameters.AddWithValue("@Id", id)

            rsCaregorias = MyCommand.ExecuteReader()


            If rsCaregorias.HasRows Then
                Do While rsCaregorias.Read()
                    'Si tiene Registros la consulta 


                    lbldos.Content += rsCaregorias(5).ToString()
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


    Private Sub ObtenerTercerLugar(ByVal id As Integer)
        Try
            Conectar()

            Dim MyCommand As OleDb.OleDbCommand
            Dim rsCaregorias As OleDbDataReader

            MyCommand = con.CreateCommand
            MyCommand.CommandText = "SELECT * FROM AdmTorneoGanador where IdTorneo = @Id and Lugar = 3"
            MyCommand.Parameters.AddWithValue("@Id", id)


            rsCaregorias = MyCommand.ExecuteReader()


            If rsCaregorias.HasRows Then
                Do While rsCaregorias.Read()
                    'Si tiene Registros la consulta 


                    lbltres.Content += rsCaregorias(5).ToString()
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



    Public Function BuscarIDTorneo(ByVal NombreCategoria As String) As Int32

        Dim ID As Integer
        Dim ObtenerID As New OleDbCommand("SELECT IdToreno FROM AdmTorenoMundial where NomTorneo = @Nombre", con)
        ObtenerID.Parameters.AddWithValue("@Nombre", NombreCategoria)
        ID = Convert.ToInt32(ObtenerID.ExecuteScalar())
        Return ID
    End Function

End Class
