using RCSTecMed_Controll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RCSTecMed_View
{
    /// <summary>
    /// Lógica de interacción para ActualizaEstabelcimientoActual_UserControl.xaml
    /// </summary>
    public partial class ActualizaEstabelcimientoActual_UserControl : UserControl
    {

        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private readonly Mensajes_Controll ms = new Mensajes_Controll();
        private readonly MostrarDatos_Controll md = new MostrarDatos_Controll();
        private readonly Controll_ESTABLECIMIENTOACTUAL est = new Controll_ESTABLECIMIENTOACTUAL();
        //private readonly Controll_SOCIO soc = new Controll_SOCIO();

        private int IdUsuario {  get; set; }
        public ActualizaEstabelcimientoActual_UserControl(int IdUser)
        {
            InitializeComponent();
            IdUsuario = IdUser;

            Limpiar();
        }

        private void Limpiar()
        {
            TXT_Rut.Text = string.Empty;
            TXT_Dv.Text = string.Empty;
            LB_Registro.Content = string.Empty;
            LB_Socio.Content = string.Empty;
            LB_Establecimiento.Content = string.Empty;
            LB_Comuna.Content = string.Empty;
            LB_Region.Content = string.Empty;
            LB_FechaDesde.Content = string.Empty;
            LB_FechaHasta.Content = string.Empty;
            DP_FechaInicial.SelectedDate = null;
            CB_Region.SelectedIndex = -1;
            CB_Comuna.SelectedIndex = -1;
            CB_Establecimiento.SelectedIndex = -1;

            LB_TotalRegistrados.Content = md.TotalEstablecimientoActualRegistrados().ToString();
            
            LimpiarGrilla();
            InhabilitarDatos();
        }

        private void InhabilitarDatos()
        {
            CB_Region.IsEnabled = false;
            CB_Comuna.IsEnabled = false;
            CB_Establecimiento.IsEnabled = false;
            DP_FechaInicial.IsEnabled = false;
            DG_Laborales.IsEnabled = false;

            BTN_Buscar.IsEnabled = false;
            BTN_Grabar.IsEnabled = false;
        }

        private void HabilitarDatos()
        {
            CB_Region.IsEnabled = true;
            CB_Comuna.IsEnabled = true;
            CB_Establecimiento.IsEnabled = true;
            DP_FechaInicial.IsEnabled = true;
            DG_Laborales.IsEnabled = true;

            BTN_Grabar.IsEnabled = true;
            BTN_Limpiar.IsEnabled = true;

            RegionComboBox();
        }

        private void CBAvanzarSiSeSelecciona(ComboBox combo, Control siguiente)
        {
            if (combo.SelectedItem != null)
                siguiente.Focus();
        }

        private bool Confirmacion(string mensaje)
        {
            MessageBoxResult resultado = MessageBox.Show(mensaje, "Confirmación requerida", MessageBoxButton.YesNo, MessageBoxImage.Question);

            return resultado == MessageBoxResult.Yes;
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

        private void ComunaComboBox(int region)
        {
            var comuna = new Controll_COMUNA().ListaComunaPorRegion(region);
            if (comuna != null && comuna.Any())
            {
                CB_Comuna.ItemsSource = comuna;
                CB_Comuna.DisplayMemberPath = "NombreComuna";
                CB_Comuna.SelectedValuePath = "IdComuna";
                CB_Comuna.SelectedIndex = -1;
            }
            else
            {
                CB_Comuna.ItemsSource = null;
                CB_Comuna.IsEnabled = false;
            }
        }

        private void EstablecimientoLaboralComboBox(string comuna)
        {
            var establecimiento = new Controll_ESTABLECIMIENTO().ListaEstablecimientoPorComuna(comuna);
            if (establecimiento != null && establecimiento.Any())
            {
                CB_Establecimiento.ItemsSource = establecimiento;
                CB_Establecimiento.DisplayMemberPath = "NombreEstablecimiento";
                CB_Establecimiento.SelectedValuePath = "IdEstablecimiento";
                CB_Establecimiento.SelectedIndex = -1;
            }
            else
            {
                CB_Establecimiento.ItemsSource = null;
                CB_Establecimiento.IsEnabled = false;
            }
        }

        private void LimpiarGrilla()
        {
            try
            {
                DG_Laborales.ItemsSource = est.ListaLimpiarGrilla();
            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void GrillaMostrar(int rut)
        {
            try
            {
                DG_Laborales.ItemsSource = est.ListaEstablecimientoActualPorRut(rut);
            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void ResetRut()
        {
            TXT_Rut.Text = string.Empty;
            TXT_Rut.Focus();
        }

        private void ResetDv()
        {
            TXT_Dv.Text = string.Empty;
            TXT_Dv.Focus();
        }

        private void ResetRutDv()
        {
            TXT_Rut.Text = string.Empty;
            TXT_Dv.Text = string.Empty;
            TXT_Rut.Focus();
        }

        private bool ValidarEntradaRut()
        {
            string rut = TXT_Rut.Text;

            bool esValido = !val.CampoVacio(rut) &&
                            val.CampoSoloNumero(rut) &&
                            rut.Length >= 6 &&
                            rut.Length <= 8;

            if (!esValido)
            {
                ms.MostrarError("Debe ingresar un RUT válido.\nRUT debe tener de 6 a 8 dígitos.");
                ResetRut();
            }

            return esValido;
        }

        private bool ValidarEntradaDV()
        {
            string dv = TXT_Dv.Text.ToUpper();

            bool esValido = !val.CampoVacio(dv) &&
                dv.Length == 1 &&
                ("0123456789K".Contains(dv));

            if (!esValido)
            {
                ms.MostrarError("El DV deber ser un número entre 0-9 o letra K");
                TXT_Dv.Text = string.Empty;
                TXT_Dv.Focus();
            }

            return esValido;
        }

        private bool RutValido()
        {
            int rut = val.ConvertirEnteroSeguro(TXT_Rut.Text);
            string dv = TXT_Dv.Text.ToUpper();

            bool esValido = val.CalcularDV(rut) == dv;

            if (!esValido)
            {
                ms.MostrarError("RUT ingresado no es Valido\n" +
                    "Fabor Reingresar RUT y DV");
                ResetRutDv();
            }

            return esValido;
        }

        private void TXT_Rut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (ValidarEntradaRut())
                    TXT_Dv.Focus();
            }
        }

        private void TXT_Dv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Tab)
                return;

            if (!ValidarEntradaDV() || !RutValido())
                return;

            int rut = val.ConvertirEnteroSeguro(TXT_Rut.Text);
            
            try
            {
                if (est.ReadRut(rut))
                {
                    BTN_Buscar.IsEnabled = true;
                    BTN_Buscar.Focus();
                }
                else
                {
                    ms.MostrarError("RUT no encontrado en Table Estableimiento Actual.\n" +
                                "Ingrese un RUT valido ");
                    ResetRutDv();
                }
            }
            catch (Exception ex)
            {
                ms.MostrarError("Ocurrió un error al consultar el registro de Establecimiento Actual.\n" +
                                "Detalle técnico: " + ex.Message);

            }
        }

        private void BTN_Buscar_Click(object sender, RoutedEventArgs e)
        {
            int rut = val.ConvertirEnteroSeguro(TXT_Rut.Text);

            GrillaMostrar(rut);
            HabilitarDatos();

            ms.MostrarInformacion("Se Desplegaran Datos de Socio en Grilla\n" +
                "Fabor Seleccione dato a actualizar desde el listado");
            LB_TotalRegistrados.Content = md.TotalEstablecimientoActualPorRutRegistrados(rut).ToString();
            DG_Laborales.Focus();
        }


        private void DG_Laborales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG_Laborales.SelectedItems.Count == 0)
                return;

            var es = DG_Laborales.SelectedItems[0] as Controll_ESTABLECIMIENTOACTUAL;
            if (es == null)
                return;

            if (es.FechaHasta.HasValue)
            {
                ms.MostrarError("Registro no actualizable.\nSeleccione un registro sin 'Fecha Hasta' en el listado.");
                DG_Laborales.SelectedItem = null;
                DG_Laborales.Focus();
                return;
            }

            LB_Registro.Content = es.IdEstablecimientoActual.ToString();
            LB_Socio.Content = md.MostrarNombreSocio(es.Rut);
            LB_Establecimiento.Content = md.MostrarEstablecimiento(es.IdEstablecimiento);
            LB_Comuna.Content = md.MostrarComunaPorIdEstablecimiento(es.IdEstablecimiento);
            LB_Region.Content = md.MostrarRegionPorIdEstablecimiento(es.IdEstablecimiento);
            LB_FechaDesde.Content = es.FechaDesde.ToString("dd-MM-yyyy");           
            LB_FechaHasta.Content = es.FechaHasta.HasValue ? es.FechaHasta.Value.ToString("dd-MM-yyyy") : "Hasta la Fecha";            


            CB_Region.Focus();
        }

        private void CB_Region_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_Region.SelectedValue is int id)
            {
                ComunaComboBox(id);
                CB_Comuna.IsEnabled = true;
                CB_Comuna.Focus();
            }
            else
            {
                CB_Comuna.IsEnabled = false;
            }
        }

        private void CB_Comuna_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_Comuna.SelectedValue is string comuna)
            {
                EstablecimientoLaboralComboBox(comuna);
                CB_Establecimiento.IsEnabled = true;
                CB_Establecimiento.Focus();
            }
            else
            {
                CB_Establecimiento.IsEnabled = false;
            }
        }

        private void CB_Establecimiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_Establecimiento.SelectedItem != null)
            {
                DP_FechaInicial.IsEnabled = true;
                DP_FechaInicial.IsDropDownOpen = true; // 👈 Esto abre el calendario automáticamente
                DP_FechaInicial.Focus();
            }
            else
            {
                DP_FechaInicial.IsEnabled = false;
                CB_Establecimiento.Focus();
            }

        }

        private bool ValidarFechaIngresada(DateTime fecha)
        {
            if (!DateTime.TryParse(LB_FechaDesde.Content?.ToString(), out DateTime fechaComparar))
            {
                ms.MostrarError("La fecha de inicio del establecimiento no es válida.");
                return false;
            }

            if (fecha <= fechaComparar)
            {
                ms.MostrarError("La fecha ingresada no puede ser igual o menor a la fecha de inicio del establecimiento anterior.");
                return false;
            }

            return true;
        }

        private void DP_FechaInicial_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!DP_FechaInicial.SelectedDate.HasValue)
                return;

            DateTime fecha = DP_FechaInicial.SelectedDate.Value;

            if (!ValidarFechaIngresada(fecha))
            {
                DP_FechaInicial.SelectedDate = null;
                DP_FechaInicial.IsDropDownOpen = true; // 👈 Esto abre el calendario automáticamente
                DP_FechaInicial.Focus();
                return;
            }

            BTN_Grabar.IsEnabled = true;
            BTN_Grabar.Focus();
        }

        private bool Actualizar()
        {
            est.IdEstablecimientoActual = val.ConvertirEnteroSeguro(LB_Registro.Content.ToString());
            est.IdEstablecimiento = val.ExtraerIdEstablecimientoNombre(LB_Establecimiento.Content.ToString());
            est.Rut = val.ConvertirEnteroSeguro(TXT_Rut.Text);
            est.FechaDesde = val.ConvertirFechaSeguro(LB_FechaDesde.Content.ToString());
            est.FechaHasta = DP_FechaInicial.SelectedDate.Value;
            est.IdUsuario = IdUsuario;

            return est.Update();
        }

        private bool Grabar()
        {
            est.IdEstablecimientoActual = est.AsignarId();
            est.IdEstablecimiento = (int)CB_Establecimiento.SelectedValue;
            est.Rut = val.ConvertirEnteroSeguro(TXT_Rut.Text);
            est.FechaDesde = DP_FechaInicial.SelectedDate.Value;
            est.FechaHasta = null;
            est.IdUsuario = IdUsuario;

            
            return est.Create();
        }

        private void BTN_Grabar_Click(object sender, RoutedEventArgs e)
        {
            bool registroOk = Actualizar() && Grabar();

            if (registroOk)
            {
                ms.MostrarInformacion("Registro y actualización exitosa.");
            }
            else
            {
                ms.MostrarError("No se logró realizar el registro en la base de datos.\nComunicarse con el administrador.");
            }

            Limpiar();
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
