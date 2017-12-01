Imports System.Data
Imports System.Data.OleDb

Public Class DataGrid_Busqueda
    Public Tipo As New Integer

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        FillDataGrid()
        dataGrid.IsEnabled = True
        dataGrid.IsReadOnly = True
    End Sub

    Private Sub FillDataGrid()
        Dim Query As String
        Select Case Tipo
            Case 1
                Query = "Select * From AdmTipoVehiculo"
            Case 2
                Query = "SELECT * From CfgUsuario"
            Case 3
                Query = "SELECT Folio,Consecutivo,Participante2,EsGano,Fecha FROM VueltaSalidaRegistros"
                buttonNuevo.Visibility = Visibility.Collapsed
                button_Copy.Visibility = Visibility.Collapsed

        End Select
        Try

            Dim ds As New DataSet
            Dim dbConnString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\ProyAutodromo\\AutodromoProyect1.accdb"
            con.ConnectionString = dbConnString
            con.Open()

            Dim da = New OleDbDataAdapter(Query, con)



            da.Fill(ds, "TabletInventory")



            dataGrid.DataContext = ds



        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        con.Close()
    End Sub

    Private Sub buttonNuevo_Click(sender As Object, e As RoutedEventArgs) Handles buttonNuevo.Click
        'Nuevo
        Select Case Tipo
            Case 1
                Dim LLamada As New TraCategorias
                LLamada.Modo = 1
                LLamada.Show()
                Me.Close()

            Case 2
                Dim LLamada As New AdmUsuario
                LLamada.Modo = 1
                LLamada.Show()
                Me.Close()

        End Select
    End Sub

    Private Sub button_Copy_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy.Click
        'Modificar



        Select Case Tipo
            Case 1
                Dim LLamada As New TraCategorias
                Dim dtr As DataRow = DirectCast(dataGrid.SelectedValue, System.Data.DataRowView).Row
                LLamada.Modo = 2
                LLamada.IdModificar = Convert.ToInt32(dtr(0))
                LLamada.txtNombreCategoria.Text = dtr(1).ToString()
                LLamada.txtDesc.Text = dtr(2).ToString()

                LLamada.Show()
                Me.Close()

            Case 2
                Dim LLamada As New AdmUsuario
                Dim dtr As DataRow = DirectCast(dataGrid.SelectedValue, System.Data.DataRowView).Row

                LLamada.Modo = 2
                LLamada.IdModificar = Convert.ToInt32(dtr(0))
                LLamada.txtNombreUsuario.Text = dtr(1).ToString()
                LLamada.txtContraseña.Text = dtr(2).ToString()
                LLamada.txtlogin.Text = dtr(3).ToString()


                LLamada.Show()
                Me.Close()
        End Select
    End Sub
End Class
