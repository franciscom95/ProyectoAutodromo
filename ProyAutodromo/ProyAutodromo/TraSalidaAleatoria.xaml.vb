Imports System
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Net.NetworkInformation
Imports ProyAutodromo

Public Class TraSalidaAleatoria

    Public EsGuardar As Integer = 0
    Public FolioUno_ As Integer = 0
    Public FolioDos_ As Integer = 0
    Public PlacasCompetidor1 As String
    Public PlacasCompetidor1_Find2 As String


    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        'Buscar autos con misma categoria para emparejarlos a los perros
        Try
            Dim _buscarid = BuscarID(cbxCategorias.SelectedItem.ToString())
            'Buscar un folio del tipo de categoría seleccionada y la fecha actual 
            Conectar()
#Region "Buscar el primer Competidor"


            Dim MyCommand As OleDb.OleDbCommand
            Dim rsCaregorias As OleDbDataReader

            MyCommand = con.CreateCommand





            MyCommand.CommandText = "SELECT   top 1     ID, FechaCreacion, VueltasRealizadas, CantidadPagadas, Expr2, Diferencia, tpo, Placas
FROM            (SELECT        vta.ID, vta.FechaCreacion, COUNT(vtaS.Folio) AS VueltasRealizadas, MAX(vta.CantidadVueltas) AS CantidadPagadas, MAX(vta.NombreCompetidor) AS Expr2, MAX(vta.CantidadVueltas) 
                                                    - COUNT(vtaS.Folio) AS Diferencia, MAX(vta.IDTipoAuto) AS tpo, MAX(vta.Placas) AS Placas
                          FROM            (VueltaSalida vta LEFT OUTER JOIN
                                                    VueltaSalidaRegistros vtaS ON vta.ID = vtaS.Folio)
                          GROUP BY vta.ID, vta.FechaCreacion) sub
WHERE        (Diferencia > 0) AND (tpo = @tpo)   AND FechaCreacion =#" + Format(Date.Now, "MM/dd/yyyy").ToString() + "#" + " " + OrdenQuery().ToString


            MyCommand.Parameters.AddWithValue("@tpo", _buscarid)


            rsCaregorias = MyCommand.ExecuteReader()


            If rsCaregorias.HasRows Then
                Do While rsCaregorias.Read()
                    'Si tiene Registros la consulta 
                    c1.Text = rsCaregorias(4).ToString() + "  Placas : " + rsCaregorias(7).ToString()
                    PlacasCompetidor1 = rsCaregorias(7).ToString()
                    FolioUno_ = Convert.ToInt32(rsCaregorias(0).ToString())

#Region "Buscar al Segundo competidor"


                    'Buscar al siguiente Competidor ,que no sea igual al primero
                    Dim MyCommand_Find2 As OleDb.OleDbCommand
                    Dim rsCaregorias_Find2 As OleDbDataReader

                    MyCommand_Find2 = con.CreateCommand
                    MyCommand_Find2.CommandText = "SELECT   top 1     ID, FechaCreacion, VueltasRealizadas, CantidadPagadas, Expr2, Diferencia, tpo, Placas
FROM            (SELECT        vta.ID, vta.FechaCreacion, COUNT(vtaS.Folio) AS VueltasRealizadas, MAX(vta.CantidadVueltas) AS CantidadPagadas, MAX(vta.NombreCompetidor) AS Expr2, MAX(vta.CantidadVueltas) 
                                                    - COUNT(vtaS.Folio) AS Diferencia, MAX(vta.IDTipoAuto) AS tpo, MAX(vta.Placas) AS Placas
                          FROM            (VueltaSalida vta LEFT OUTER JOIN
                                                    VueltaSalidaRegistros vtaS ON vta.ID = vtaS.Folio)
                          GROUP BY vta.ID, vta.FechaCreacion) sub
WHERE        (Diferencia > 0) AND (tpo = @tpo ) AND (Placas <> @Plaquitas) AND FechaCreacion =#" + Format(Date.Now, "MM/dd/yyyy").ToString() + "#" + " " + " " + OrdenQuery().ToString



                    MyCommand_Find2.Parameters.AddWithValue("@tpo", _buscarid)
                    MyCommand_Find2.Parameters.AddWithValue("@Plaquitas", PlacasCompetidor1)



                    rsCaregorias_Find2 = MyCommand_Find2.ExecuteReader()


                    If rsCaregorias_Find2.HasRows Then
                        Do While rsCaregorias_Find2.Read()
                            'Si tiene Registros la consulta 
                            c2.Text = rsCaregorias_Find2(4).ToString() + "  Placas : " + rsCaregorias_Find2(7).ToString()
                            PlacasCompetidor1_Find2 = rsCaregorias_Find2(7).ToString()


                            FolioDos_ = Convert.ToInt32(rsCaregorias_Find2(0).ToString())


                            '+ "Placas" + rsCaregorias(4).ToString(6)

                            'cbxCategorias.
                            LblEstadoCarrera.Text = "Por Comenzar Carrera"

                        Loop
                    Else
                        MessageBox.Show("No se encontro datos para categorías")
                        LblEstadoCarrera.Text = "Por Buscar Corredor 2"
                        Me.EsGuardar = 0
                    End If
                    rsCaregorias_Find2.Close()
#End Region

                Loop
            Else
                MessageBox.Show("No se encontro datos para categorías")
                LblEstadoCarrera.Text = "Por Buscar Corredor 1"
                Me.EsGuardar = 0
            End If
            rsCaregorias.Close()
#End Region





        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        con.Close()






        'Buscar pista 
        Try
            Conectar()
            Dim Query As String
            Query = "SELECT * FROM TraPista  Where EsOcupada=0 and Id = @Id"
            Dim PrepararInsert As New OleDbCommand(Query, con)
            Randomize()
            Dim value As Integer = CInt(Int((4 * Rnd()) + 1))
            PrepararInsert.Parameters.AddWithValue("@Id", value)



            Dim NombrePista As String
            NombrePista = ""
            Dim reader As OleDbDataReader = PrepararInsert.ExecuteReader()
            If reader.HasRows Then
                Do While reader.Read()
                    NombrePista = reader(1).ToString()
                Loop
            Else
                NombrePista = ""
            End If
            reader.Close()
            If (NombrePista = "") Then

                MessageBox.Show("No hay pistas disponibles ")
            Else

                Pista.Text = NombrePista
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        con.Close()

        ActualizarBuffer()
    End Sub

    Private Sub ActualizarBuffer()
        'Actualizando el buffer
        Conectar()

        Dim QueryInsertar_Actualizar As String
        QueryInsertar_Actualizar = "Update AdmBuffer set NomCompetidor1 = @NomCompetidor1 ,NomCompetidor2 =@NomCompetidor2 , Pista = @Pista Where ID =@ID"


        Dim PrepararInsert As New OleDbCommand(QueryInsertar_Actualizar, con)
        PrepararInsert.Parameters.AddWithValue("@NomCompetidor1", c1.Text)
        PrepararInsert.Parameters.AddWithValue("@NomCompetidor2", c2.Text)
        PrepararInsert.Parameters.AddWithValue("@Pista", Pista.Text)
        PrepararInsert.Parameters.AddWithValue("@ID", 1)

        PrepararInsert.ExecuteNonQuery()
        con.Close()

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        CargarCombo()
        Me.EsGuardar = 0
        LblEstadoCarrera.Text = "Por Escoger Participantes"

    End Sub

    'Cargar Combo Categoria 
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

                    cbxCategorias.Items.Add(rsCaregorias(1).ToString())
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


    'Buscar el id del combo seleccionado
    Public Function BuscarID(ByVal NombreCategoria As String) As Int32


        Conectar()
        Dim ID As Integer

        Dim ObtenerID As New OleDbCommand("SELECT ID FROM AdmTipoVehiculo where NomTipoVehiculo = @Nombre", con)
        ObtenerID.Parameters.AddWithValue("@Nombre", NombreCategoria)
        ID = Convert.ToInt32(ObtenerID.ExecuteScalar())
        con.Close()

        Return ID
    End Function




    Public Function NumeroGanador() As Int32


        Dim QuienGana As New Integer

        If (chk1.IsChecked) Then
            QuienGana = 1
        Else
            QuienGana = 2
        End If

        Return QuienGana
    End Function

    'Darle orden aleatorio al select
    Public Function OrdenQuery() As String

        Randomize()
        Dim value As Integer = CInt(Int((6 * Rnd()) + 1))
        Dim OrdenarQuery1 As String

        'Ordenar aleatoriamente 
        Select Case value
            Case 1
                OrdenarQuery1 = " Order by Placas desc"
            Case 2
                OrdenarQuery1 = " Order by Placas asc"

            Case 3
                OrdenarQuery1 = " Order by ID desc"

            Case 4
                OrdenarQuery1 = " Order by ID asc"

            Case 5
                OrdenarQuery1 = " Order by Diferencia desc"

            Case 6
                OrdenarQuery1 = " Order by Diferencia asc"

        End Select


        Return OrdenarQuery1
    End Function

    Private Sub buttonComenzar_Click(sender As Object, e As RoutedEventArgs) Handles buttonComenzar.Click



        'Seleccionar una pista


        Me.EsGuardar = 1
        LblEstadoCarrera.Text = "Por Guardar Resultado"
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        If (Me.EsGuardar = 1) Then
            If (chk1.IsChecked Or chk2.IsChecked) Then
                Dim QuienGana As New Integer
                If (chk1.IsChecked) Then
                    QuienGana = 1
                Else
                    QuienGana = 2
                End If
                'Insertar el registro 
                Try
                    Conectar()
                    'Insertar el registro del primer competidor 


                    Dim PrepararInsert As New OleDbCommand("insert into VueltaSalidaRegistros(Folio,Consecutivo,Participante2,EsGano,Fecha) values (@Folio,@Consecutivo,@Participante2,@EsGano,@Fecha)", con)
                    PrepararInsert.Parameters.AddWithValue("@Folio", FolioUno_)
                    PrepararInsert.Parameters.AddWithValue("@Consecutivo", 1)
                    PrepararInsert.Parameters.AddWithValue("@Participante2", PlacasCompetidor1)
                    PrepararInsert.Parameters.AddWithValue("@EsGano", NumeroGanador())
                    PrepararInsert.Parameters.AddWithValue("@Fecha", Format(Date.Now, "dd/MM/yyyy"))
                    PrepararInsert.ExecuteNonQuery()








                    Dim PrepararInsert2 As New OleDbCommand("insert into VueltaSalidaRegistros(Folio,Consecutivo,Participante2,EsGano,Fecha) values (@Folio,@Consecutivo,@Participante2,@EsGano,@Fecha)", con)
                    PrepararInsert2.Parameters.AddWithValue("@Folio", FolioDos_)
                    PrepararInsert2.Parameters.AddWithValue("@Consecutivo", 2)
                    PrepararInsert2.Parameters.AddWithValue("@Participante2", PlacasCompetidor1_Find2
                                                            )
                    PrepararInsert2.Parameters.AddWithValue("@EsGano", NumeroGanador())
                    PrepararInsert2.Parameters.AddWithValue("@Fecha", Format(Date.Now, "dd/MM/yyyy"))
                    PrepararInsert2.ExecuteNonQuery()





                    MessageBox.Show("Se registro correctamente la Carrera")
                    Me.EsGuardar = 0
                    con.Close()

                    Me.Close()










                Catch ex As Exception
                    MessageBox.Show(ex.ToString())
                End Try






            End If
        Else
            MessageBox.Show("Revisa el estatus de la carrera")
        End If
    End Sub

    Private Sub Window_Unloadded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub clos_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles clos.Closing
        If Me.EsGuardar = 0 Then

        Else
            MessageBox.Show("Tienes datos pendientes de guardar")
            e.Cancel = True
        End If
    End Sub

    Private Sub chk1_Click(sender As Object, e As RoutedEventArgs) Handles chk1.Click


        chk2.IsChecked = False

    End Sub

    Private Sub chk2_Checked(sender As Object, e As RoutedEventArgs) Handles chk2.Checked
        chk1.IsChecked = False

    End Sub
End Class
