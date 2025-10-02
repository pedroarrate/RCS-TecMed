using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace RCSTecMed.Exportaciones
{
    public static class LogicaExportacion
    {
        /// <summary>
        /// Exporta los datos visibles del DataGrid a un archivo .txt con separador pipe (|).
        /// Solo incluye columnas visibles y aplica escapado a caracteres especiales.
        /// </summary>
        /// <param name="dataGrid">El DataGrid que contiene los datos.</param>
        /// <param name="rutaArchivo">Ruta completa del archivo de destino.</param>
        public static void ExportarDataGrid(DataGrid dataGrid, string rutaArchivo)
        {
            if (dataGrid.Items.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Exportación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var sb = new StringBuilder();

            // Encabezados visibles
            var columnasVisibles = new List<DataGridColumn>();
            foreach (var col in dataGrid.Columns)
            {
                if (col.Visibility == Visibility.Visible)
                    columnasVisibles.Add(col);
            }

            for (int i = 0; i < columnasVisibles.Count; i++)
            {
                sb.Append(Escape(columnaNombre(columnasVisibles[i])));
                if (i < columnasVisibles.Count - 1)
                    sb.Append("|");
            }
            sb.AppendLine();

            // Filas
            foreach (var item in dataGrid.Items)
            {
                if (item == null) continue;
                for (int i = 0; i < columnasVisibles.Count; i++)
                {
                    var binding = (columnasVisibles[i] as DataGridBoundColumn)?.Binding as System.Windows.Data.Binding;
                    var propName = binding?.Path.Path;
                    var valor = item.GetType().GetProperty(propName)?.GetValue(item, null)?.ToString() ?? "";
                    sb.Append(Escape(valor));
                    if (i < columnasVisibles.Count - 1)
                        sb.Append("|");
                }
                sb.AppendLine();
            }

            // Guardar archivo
            try
            {
                File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Exportación completada correctamente.", "Exportación", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Exportación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Escapa caracteres especiales para mantener integridad del archivo.
        /// </summary>
        private static string Escape(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return input.Replace("|", "\\|").Replace("\n", " ").Replace("\r", "");
        }

        /// <summary>
        /// Obtiene el nombre legible de la columna.
        /// </summary>
        private static string columnaNombre(DataGridColumn col)
        {
            return col.Header?.ToString() ?? "SinNombre";
        }
    }
}
