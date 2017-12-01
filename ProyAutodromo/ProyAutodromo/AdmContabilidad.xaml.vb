Imports System.Data
Imports System.Data.OleDb

Public Class AdmContabilidad
    Dim TreintaPociento As Double
    Dim Porcentaje As Double



    Private Sub button_Click(sender As Object, e As RoutedEventArgs)
        'Obtener los ingresos totales cuando si es torneo 

        If (checkBox.IsChecked) Then


            lblPremioARepartir.Visibility = Visibility.Visible
            lblPremioARepartirCantidad.Visibility = Visibility.Visible
            lblLugar1.Visibility = Visibility.Visible
            lblLugar2.Visibility = Visibility.Visible
            lblLugar3.Visibility = Visibility.Visible

            lblCompetidor1.Visibility = Visibility.Visible
            lblCompetidor2.Visibility = Visibility.Visible
            lblCompetidor3.Visibility = Visibility.Visible


            lblDinero1.Visibility = Visibility.Visible
            lblDinero2.Visibility = Visibility.Visible
            lblDinero3.Visibility = Visibility.Visible




            Try
                Conectar()
                Dim Query As String
                Query = "SELECT   SUM(CantidadVueltas)
FROM            VueltaSalida
where           FechaCreacion =#" + Format(Date.Now, "MM/dd/yyyy").ToString() + "#"
                Dim PrepararInsert As New OleDbCommand(Query, con)
                Dim CVueltas As Integer
                CVueltas = PrepararInsert.ExecuteScalar()
                CVueltas = CVueltas * 50

                TreintaPociento = CVueltas * Porcentaje
                CVueltas = CVueltas - TreintaPociento
                lblDineroTotal.Content = CVueltas.ToString()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try
            con.Close()




            Dim PremioCompetidoruno As String
            Dim PremioCompetidordos As String
            Dim PremioCompetidortres As String

            PremioCompetidoruno = ""
            PremioCompetidordos = ""
            PremioCompetidortres = ""
            Try
                'Buscar un folio del tipo de categoría seleccionada y la fecha actual 
                Conectar()



                Dim MyCommand As OleDb.OleDbCommand
                Dim rsCaregorias As OleDbDataReader

                MyCommand = con.CreateCommand





                MyCommand.CommandText = " 


Select
vta.Placas
,vta.NombreCompetidor
,sub.Expr1
FROM 
(

SELECT        Participante2, COUNT(Participante2) AS Expr1
FROM            VueltaSalidaRegistros
WHERE        (Fecha = # " + Format(Date.Now, "MM/dd/yyyy").ToString() + "#) AND (Consecutivo = EsGano)
GROUP BY Participante2
ORDER BY COUNT(Participante2) DESC
) sub
LEFT JOIN VueltaSalida vta on sub.Participante2 = vta.Placas
"




                rsCaregorias = MyCommand.ExecuteReader()
                Dim x As Integer
                x = 0

                If rsCaregorias.HasRows Then
                    Do While rsCaregorias.Read()
                        'Si tiene Registros la consulta 
                        x = x + 1


                        Select Case x
                            Case 1
                                PremioCompetidoruno = rsCaregorias(1).ToString()

                            Case 2
                                PremioCompetidordos = rsCaregorias(1).ToString()

                            Case 3
                                PremioCompetidortres = rsCaregorias(1).ToString()

                        End Select




                    Loop
                Else
                    MessageBox.Show("No se encontro datos para repatir el premio ")
                    rsCaregorias.Close()


                End If




            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try
            con.Close()




            lblCompetidor1.Content = PremioCompetidoruno
            lblCompetidor2.Content = PremioCompetidordos
            lblCompetidor3.Content = PremioCompetidortres



            lblDinero1.Content = (TreintaPociento * 0.6).ToString()
            lblDinero2.Content = (TreintaPociento * 0.3).ToString()
            lblDinero3.Content = (TreintaPociento * 0.1).ToString()




        Else

            lblPremioARepartir.Visibility = Visibility.Collapsed
            lblPremioARepartirCantidad.Visibility = Visibility.Collapsed
            lblLugar1.Visibility = Visibility.Collapsed
            lblLugar2.Visibility = Visibility.Collapsed
            lblLugar3.Visibility = Visibility.Collapsed

            lblCompetidor1.Visibility = Visibility.Collapsed
            lblCompetidor2.Visibility = Visibility.Collapsed
            lblCompetidor3.Visibility = Visibility.Collapsed


            lblDinero1.Visibility = Visibility.Collapsed
            lblDinero2.Visibility = Visibility.Collapsed
            lblDinero3.Visibility = Visibility.Collapsed

            'Cuando no es torneo 
            Try
                Conectar()
                Dim Query As String
                Query = "SELECT   SUM(CantidadVueltas)
FROM            VueltaSalida
where           FechaCreacion =#" + Format(Date.Now, "MM/dd/yyyy").ToString() + "#"
                Dim PrepararInsert As New OleDbCommand(Query, con)
                Dim CVueltas As Integer
                CVueltas = PrepararInsert.ExecuteScalar()
                CVueltas = CVueltas * 50
                lblDineroTotal.Content = CVueltas.ToString()
            Catch ex As Exception
                MessageBox.Show("No tiene información para este día ")
            End Try
            con.Close()


        End If


        'Buscar pista 




    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)



        lblPremioARepartir.Visibility = Visibility.Collapsed
        lblPremioARepartirCantidad.Visibility = Visibility.Collapsed
        lblLugar1.Visibility = Visibility.Collapsed
        lblLugar2.Visibility = Visibility.Collapsed
        lblLugar3.Visibility = Visibility.Collapsed

        lblCompetidor1.Visibility = Visibility.Collapsed
        lblCompetidor2.Visibility = Visibility.Collapsed
        lblCompetidor3.Visibility = Visibility.Collapsed


        lblDinero1.Visibility = Visibility.Collapsed
        lblDinero2.Visibility = Visibility.Collapsed
        lblDinero3.Visibility = Visibility.Collapsed
        BuscarPorcentajeGanancia()

    End Sub


    Private Sub BuscarPorcentajeGanancia()
        Try
            Conectar()

            Dim MyCommand As OleDb.OleDbCommand
            Dim rsCaregorias As OleDbDataReader

            MyCommand = con.CreateCommand
            MyCommand.CommandText = "SELECT top 1 * FROM AdmPorcentajeGanancia order by ID desc"


            rsCaregorias = MyCommand.ExecuteReader()


            If rsCaregorias.HasRows Then
                Do While rsCaregorias.Read()

                    Porcentaje = rsCaregorias(1).ToString()
                    Porcentaje = Porcentaje / 100

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


End Class
