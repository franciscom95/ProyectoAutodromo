Imports System
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Net.NetworkInformation
Imports ProyAutodromo

Public Class TraAltaCompetidor
    Dim NombreCategoria As List
    Dim IdCategoria As List
    Dim CostoPorPartida As Int32 = 0





    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If Len(Trim(txtNombreCompetidor.Text)) = 0 Or Len(Trim(txtCantidadPartidas.Text)) = 0 Or Len(Trim(txtPlacas.Text)) = 0 Then
            'Lado verdadero 
            MessageBox.Show("Por favor captura todos los campos")
            Return
        Else
            'Lado Falso


            'Captura de otro registro 
            Dim resultCobro As Integer = MessageBox.Show("Debes cobrar antes de guardar la cantida de :  " + (Convert.ToInt32(txtCantidadPartidas.Text) * CostoPorPartida).ToString, "caption", MessageBoxButton.YesNo)
            If resultCobro = MessageBoxResult.Yes Then
#Region "Insertar"
                Try
                    Conectar()
                    Dim _buscarid = BuscarID(cbxCategorias.SelectedItem.ToString())
                    Dim PrepararInsert As New OleDbCommand("insert into VueltaSalida(NombreCompetidor,IDTipoAuto,Placas,CantidadVueltas,FechaCreacion,UsuarioIns) values (@NombreCompetidor,@IDTipoAuto,@Placas,@CantidadVueltas,@FechaCreacion,@UsuarioIns)", con)
                    PrepararInsert.Parameters.AddWithValue("@NombreCompetidor", txtNombreCompetidor.Text)
                    PrepararInsert.Parameters.AddWithValue("@IDTipoAuto", _buscarid)
                    PrepararInsert.Parameters.AddWithValue("@Placas", txtPlacas.Text)
                    PrepararInsert.Parameters.AddWithValue("@CantidadVueltas", Convert.ToInt32(txtCantidadPartidas.Text))
                    PrepararInsert.Parameters.AddWithValue("@FechaCreacion", Format(Date.Now, "dd/MM/yyyy"))
                    PrepararInsert.Parameters.AddWithValue("@UsuarioIns", 1)
                    PrepararInsert.ExecuteNonQuery()
                    MessageBox.Show("Se registro correctamente el Folio del Vehiculo")
                    con.Close()
                    'Captura de otro registro 
                    Dim result As Integer = MessageBox.Show("Desea capturar otro registro ", "caption", MessageBoxButton.YesNo)
                    If result = MessageBoxResult.Yes Then
                        txtNombreCompetidor.Text = ""
                        cbxCategorias.SelectedItem = ""
                        txtPlacas.Text = ""
                        txtCantidadPartidas.Text = ""
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

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        'Cargando el evento 
        CargarCombo()
        BuscarCobroActual()

    End Sub



    Private Sub BuscarCobroActual()
        Try
            Conectar()

            Dim MyCommand As OleDb.OleDbCommand
            Dim rsCaregorias As OleDbDataReader

            MyCommand = con.CreateCommand
            MyCommand.CommandText = "SELECT top 1 * FROM AdmCobro order by ID desc"


            rsCaregorias = MyCommand.ExecuteReader()


            If rsCaregorias.HasRows Then
                Do While rsCaregorias.Read()

                    CostoPorPartida = rsCaregorias(1).ToString()


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

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        'MessageBox.Show(cbxCategorias.SelectedValue)
        Me.Close()
    End Sub

    'Buscar el id del combo seleccionado
    Public Function BuscarID(ByVal NombreCategoria As String) As Int32




        Dim ID As Integer

        Dim ObtenerID As New OleDbCommand("SELECT ID FROM AdmTipoVehiculo where NomTipoVehiculo = @Nombre", con)
        ObtenerID.Parameters.AddWithValue("@Nombre", NombreCategoria)
        ID = Convert.ToInt32(ObtenerID.ExecuteScalar())


        Return ID
    End Function





End Class
