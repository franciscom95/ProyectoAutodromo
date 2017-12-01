Public Class Menu
    Public parametros As Object
    Public Parametroslista As New List(Of String)
    Public EsAdministrador As Boolean




    Private Function EsAbrirOtraPantalla() As Boolean
        Dim CantidadPantallasAbiertas As New Integer
        CantidadPantallasAbiertas = 0
        For Each Window In Application.Current.Windows


            If Window.ToString.Contains("ProyAutodromo") Then
                CantidadPantallasAbiertas += 1
            End If



        Next


        If CantidadPantallasAbiertas > 2 Then

            Return False
        Else

            Return True
        End If



    End Function



    Private Sub button_Copy4_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy4.Click




        If EsAbrirOtraPantalla() Then

            Dim LLamada As New DataGrid_Busqueda

            LLamada.Tipo = 3
            'LLamada.Parametroslista.Add(ID)

            If (EsAdministrador) Then




                LLamada.Show()
            Else
                MessageBox.Show("Necesias ser admin")
            End If

        Else
            MessageBox.Show("Tiene abierto otra opciói")
        End If



    End Sub

    Private Sub button_Copy_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy.Click


        If EsAbrirOtraPantalla() Then


            Dim LLamada As New DataGrid_Busqueda
            LLamada.Tipo = 1
            'LLamada.Parametroslista.Add(ID)
            LLamada.Show()
        Else
            MessageBox.Show("Tiene abierto otra opciói")
        End If





    End Sub

    Private Sub button_Copy1_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy1.Click



        If EsAbrirOtraPantalla() Then
            Dim LLamada As New TraAltaCompetidor

            'LLamada.Parametroslista.Add(ID)


            LLamada.Show()

        Else
            MessageBox.Show("Tiene abierto otra opciói")
        End If








    End Sub

    Private Sub button_Copy2_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy2.Click

        If EsAbrirOtraPantalla() Then

            Dim LLamada As New TraSalidaAleatoria
            'LLamada.Parametroslista.Add(ID)
            LLamada.Show()

        Else
            MessageBox.Show("Tiene abierto otra opciói")
        End If



    End Sub

    Private Sub button_Copy5_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy5.Click


        If EsAbrirOtraPantalla() Then

            Dim LLamada As New AdmContabilidad

            If (EsAdministrador) Then
                LLamada.Show()

            Else
                MessageBox.Show("Necesitas ser admin")
            End If

        Else
            MessageBox.Show("Tiene abierto otra opciói")
        End If






        'LLamada.Parametroslista.Add(ID)
    End Sub

    Private Sub button_Copy3_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy3.Click


        If EsAbrirOtraPantalla() Then


            Dim LLamada As New DataGrid_Busqueda
            LLamada.Tipo = 2
            'LLamada.Parametroslista.Add(ID)


            If (EsAdministrador) Then
                LLamada.Show()

            Else
                MessageBox.Show("Necesitas ser admin")
            End If

        Else
            MessageBox.Show("Tiene abierto otra opciói")
        End If



    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        If EsAbrirOtraPantalla() Then
            Dim LLamada As New AdmCobro





            LLamada.Show()
        Else
            MessageBox.Show("Tiene abierto otra opciói")
        End If


    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)


        If EsAbrirOtraPantalla() Then
            Dim LLamada As New AdmTorneoMundial
            LLamada.Modo = 1
            LLamada.Show()
        Else
            MessageBox.Show("Tiene abierto otra opciói")
        End If


    End Sub

    Private Sub AltaParticiapantesTorneo(sender As Object, e As RoutedEventArgs)



        If EsAbrirOtraPantalla() Then

            Dim LLamada As New AdmParticipanteTorneo

            LLamada.Show()
        Else
            MessageBox.Show("Tiene abierto otra opciói")
        End If

    End Sub

    Private Sub Ganadores(sender As Object, e As RoutedEventArgs)



        If EsAbrirOtraPantalla() Then

            Dim LLamada As New GanadoresMundial

            LLamada.Show()
        Else
            MessageBox.Show("Tiene abierto otra opciói")
        End If
    End Sub

    Private Sub Ganancias(sender As Object, e As RoutedEventArgs)



        If EsAbrirOtraPantalla() Then

            Dim LLamada As New Ganancias

            LLamada.Show()
        Else
            MessageBox.Show("Tiene abierto otra opciói")
        End If

    End Sub

    Private Sub buttonPorcentaje_Click(sender As Object, e As RoutedEventArgs)


        If EsAbrirOtraPantalla() Then

            Dim LLamada As New AdmProcentaje

            LLamada.Show()
        Else
            MessageBox.Show("Tiene abierto otra opciói")
        End If
    End Sub
End Class
