Imports System.Reflection

Public Class FO_Venta2Descu
    Public descuento As List(Of DescuentoXCategoriaDescuento) = New List(Of DescuentoXCategoriaDescuento)
    Private Sub FO_Venta2Descu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim datatable = ConvertToDataTable(descuento)
        _prCargarDescuento(datatable)
    End Sub
    Private Sub _prCargarDescuento(tabla As DataTable)
        grDescuento.DataSource = tabla
        grDescuento.RetrieveStructure()
        grDescuento.AlternatingColors = True

        With grDescuento.RootTable.Columns("DescuentoId")
            .Width = 100
            .Caption = "CODIGO"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
        End With
        With grDescuento.RootTable.Columns("Descripcion")
            .Width = 300
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
        End With
        With grDescuento.RootTable.Columns("Cantidad")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cantidad".ToUpper
        End With


        With grDescuento
            .GroupByBoxVisible = False
            'diseño de la grilla
            '.VisualStyle = VisualStyle.Office2007
        End With
    End Sub
    Public Shared Function ConvertToDataTable(Of T)(ByVal list As IList(Of T)) As DataTable
        Dim table As New DataTable()
        Dim fields() As FieldInfo = GetType(T).GetFields()
        For Each field As FieldInfo In fields
            table.Columns.Add(field.Name, field.FieldType)
        Next
        For Each item As T In list
            Dim row As DataRow = table.NewRow()
            For Each field As FieldInfo In fields
                row(field.Name) = field.GetValue(item)
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function
End Class