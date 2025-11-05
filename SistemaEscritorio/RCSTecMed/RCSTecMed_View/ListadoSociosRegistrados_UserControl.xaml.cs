using RCSTecMed_Controll;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RCSTecMed_View
{
    /// <summary>
    /// Lógica de interacción para ListadoSociosRegistrados_UserControl.xaml
    /// </summary>
    public partial class ListadoSociosRegistrados_UserControl : System.Windows.Controls.UserControl
    {
        private readonly Controll_SOCIO soc = new Controll_SOCIO();
        private readonly Mensajes_Controll ms = new Mensajes_Controll();
        private readonly MostrarDatos_Controll md = new MostrarDatos_Controll();
        private int IdUsuario {  get; set; }
        public ListadoSociosRegistrados_UserControl(int IdUser)
        {
            InitializeComponent();
            IdUsuario = IdUser;

            Limpiar();
        }

        private void Limpiar()
        {
            RegionComboBox();
            LimpiarGrilla();
        }

        private void RegionComboBox()
        {
            var region = new Controll_REGION().ReadAll();
            if (region != null && region.Any())
            {
                CB_Region.ItemsSource = region;
                CB_Region.DisplayMemberPath = "NombreRegion";
                CB_Region.SelectedValuePath = "IdRegion";
                CB_Region.SelectedIndex = -1;
            }
            else
            {
                CB_Region.ItemsSource = null;
                CB_Region.IsEnabled = false;
            }
        }

        private void GrillaCompleta()
        {
            try
            {
                DG_Reporte.ItemsSource = soc.ReadAllOrdenadoApellido();
            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }

        }

        private void LimpiarGrilla()
        {
            try
            {
                DG_Reporte.ItemsSource = soc.ListaSocioPorRegion(0);
            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void GrillaRegion(int region)
        {
            try
            {
                DG_Reporte.ItemsSource = soc.ListaSocioPorRegion(region);
            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void BTN_Mostrar_Click(object sender, RoutedEventArgs e)
        {
            if (CB_Region.SelectedIndex == -1)
            {
                GrillaCompleta();
            }
            else
            {
                int region = (int)CB_Region.SelectedValue;
                GrillaRegion(region);
            }
        }

        private string EscapePipe(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            return value.Replace("|", "||");
        }

        public void ExportarDataGridACSV(System.Windows.Controls.DataGrid dataGrid, string rutaArchivo)
        {
            var sb = new StringBuilder();

            // Columnas visibles ordenadas por DisplayIndex
            var columnas = dataGrid.Columns
                .Where(x => x.Visibility == Visibility.Visible)
                .OrderBy(x => x.DisplayIndex)
                .ToList();

            // Encabezados
            foreach (var col in columnas)
            {
                sb.Append(EscapePipe(col.Header?.ToString()) + "|");
            }
            sb.AppendLine();

            // Filas
            foreach (var item in dataGrid.Items)
            {
                if (item == CollectionView.NewItemPlaceholder)
                    continue;

                foreach (var col in columnas)
                {
                    // Cast explícito a DataGridBoundColumn
                    var boundColumn = col as DataGridBoundColumn;
                    var binding = boundColumn?.Binding as System.Windows.Data.Binding;
                    var propName = binding?.Path?.Path;

                    string valor = string.Empty;
                    if (!string.IsNullOrEmpty(propName))
                    {
                        var propiedad = item.GetType().GetProperty(propName);
                        valor = propiedad?.GetValue(item, null)?.ToString() ?? string.Empty;
                    }

                    sb.Append(EscapePipe(valor) + "|");
                }
                sb.AppendLine();
            }

            // Escritura segura del archivo
            try
            {
                File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error al guardar el archivo: {ex.Message}");
            }

        }

        private void OpenFolder(string folderpath)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = folderpath,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        public static bool DataGridEstaVacio(System.Windows.Controls.DataGrid dataGrid)
        {
            return dataGrid.Items.Cast<object>()
                .All(item => item == CollectionView.NewItemPlaceholder);
        }

        

        private void BTN_Reporte_Click(object sender, RoutedEventArgs e)
        {
            string folder = @"c:\ReporteRCSTecMed";
            string file = string.Empty;

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (DataGridEstaVacio(DG_Reporte))
            {
                ms.MostrarError("Lista sin Datos, No se puede Generar Reporte");
                Limpiar();
                return;
            }

            string freport = DateTime.Now.ToString("dd-MM-yyyy-H-mm-ss");
            if (CB_Region.SelectedIndex == -1)
            {                
                file = @"c:\ReporteRCSTecMed\" + $"ReporteListadoSociosCompleto_{freport}.csv";
                ExportarDataGridACSV(DG_Reporte, file);
                MessageBoxResult resp = System.Windows.MessageBox.Show("Archivo Generado\n" +
                    "Desea abrir Carpeta?", "RCSTecMed - Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resp == System.Windows.MessageBoxResult.Yes)
                {
                    OpenFolder(folder);
                    Limpiar();
                }
                else
                {
                    Limpiar();
                }
            }
            else
            {
                string region = md.MostrarRegion((int)CB_Region.SelectedValue);
                file = @"c:\ReporteRCSTecMed\" + $"ReporteListadoSociosRegion_{region}_{freport}.csv";
                ExportarDataGridACSV(DG_Reporte, file);
                MessageBoxResult resp = System.Windows.MessageBox.Show("Archivo Generado\n" +
                    "Desea abrir Carpeta?", "RCSTecMed - Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resp == System.Windows.MessageBoxResult.Yes)
                {
                    OpenFolder(folder);
                    Limpiar();
                }
                else
                {
                    Limpiar();
                }
            }

        }

        private void BTN_Limpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void BTN_Home_Click(object sender, RoutedEventArgs e)
        {
            // Buscar el TabControl en la ventana contenedora
            if (Window.GetWindow(this) is View_ModuloSecretaria mdSec)
            {
                // Activar la pestaña "Home"
                mdSec.MainTabControl.SelectedItem = mdSec.TAB_Home;
            }

        }
    }
}
