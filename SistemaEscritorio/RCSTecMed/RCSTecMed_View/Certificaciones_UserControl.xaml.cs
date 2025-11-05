using RCSTecMed_Controll;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RCSTecMed_View
{
    /// <summary>
    /// Lógica de interacción para Certificaciones_UserControl.xaml
    /// </summary>
    public partial class Certificaciones_UserControl : UserControl
    {
        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private readonly MostrarDatos_Controll md = new MostrarDatos_Controll();
        private readonly Mensajes_Controll ms = new Mensajes_Controll();
        private readonly Controll_ACADEMICO aca = new Controll_ACADEMICO();
        private readonly EmailNotifier mail = new EmailNotifier();

        private int IdUsuario {  get; set; }
        public Certificaciones_UserControl(int IdUser)
        {
            InitializeComponent();
            IdUsuario = IdUser;

            Limpiar();
        }

        private void Limpiar()
        {
            TXT_Rut.Text = string.Empty;
            TXT_Dv.Text = string.Empty;
            LB_NombreSocio.Content = string.Empty;
            DP_FechaCurso.SelectedDate = null;
            TXT_Nota.Text = string.Empty;
            TXT_Horas.Text = string.Empty;
            LB_FolioRegistro.Content = string.Empty;

            CB_Curso.SelectedIndex = -1;
            LimpiarGrilla();
            InhabilitarDatos();

            TXT_Rut.Focus();
        }
        
        private void InhabilitarDatos()
        {
            DP_FechaCurso.IsEnabled =false;
            CB_Curso.IsEnabled =false;
            TXT_Nota.IsEnabled = false;
            TXT_Horas.IsEnabled = false;

            BTN_Buscar.IsEnabled = false;
            DG_Certificaciones.IsEnabled = false;
            BTN_Enviar.IsEnabled = false;
            BTN_Generar.IsEnabled = false;
            BTN_Grabar.IsEnabled = false;
            BTN_Imprimir.IsEnabled = false;
        }

        private void HabilitarDatos()
        {
            DP_FechaCurso.IsEnabled = true;
            CB_Curso.IsEnabled = true;
            TXT_Nota.IsEnabled = true;
            TXT_Horas.IsEnabled = true;
        }

        private void CursosComboBox()
        {
            var certificacion = new Controll_CERTIFICACIONES().ListaNombreCertificacionesOrdenadaDescrpcion();
            if (certificacion != null && certificacion.Any())
            {
                CB_Curso.ItemsSource = certificacion;
                CB_Curso.DisplayMemberPath = "NombreCertificacion";
                CB_Curso.SelectedValuePath = "IdCertificaciones";
                CB_Curso.SelectedIndex = -1;
            }
            else
            {
                CB_Curso.ItemsSource = null;
            }
        }
        

        private void GrillaMostrar(int rut)
        {
            try
            {
                DG_Certificaciones.ItemsSource = aca.ListaCertificacionesTelMedPorRut(rut);
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
                DG_Certificaciones.ItemsSource = aca.ListaCertificacionesTelMedPorRut(0);
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
                if (aca.ReadRut(rut))
                {
                    BTN_Buscar.IsEnabled = true;
                    BTN_Buscar.Focus();
                }
                else
                {
                    ms.MostrarError("RUT no encontrado en Tabla Antecedentes Academicos.\n" +
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

        private bool RutRegistrado(int rut)
        {
            try
            {
                var socio = new Controll_SOCIO { Rut = rut };

                bool existe = socio.ReadId();

                if (!existe)
                    ms.MostrarError("RUT Ingresado no Registrado como Socio");
                
                return existe;                    

            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error al verificar RUT: {ex.Message}");
                return false;
            }

        }

        private void BTN_Buscar_Click(object sender, RoutedEventArgs e)
        {
            int rut = val.ConvertirEnteroSeguro(TXT_Rut.Text);

            if (!RutRegistrado(rut))
            {
                ResetRutDv();
                return;
            }
                

            GrillaMostrar(rut);
            
            ms.MostrarInformacion("Si desea Enviar o Generar un Certificado\n" +
                "Fabor Seleccione desde el listado");

            HabilitarDatos();

            LB_NombreSocio.Content = md.MostrarNombreSocio(rut);
            CursosComboBox();

            DG_Certificaciones.Focus();
        }

        private void DG_Certificaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG_Certificaciones.SelectedItems.Count == 0)
                return;

            var aca = DG_Certificaciones.SelectedItems[0] as Controll_ACADEMICO;
            if (aca == null)
                return;

            DP_FechaCurso.SelectedDate = aca.Fecha;
            CB_Curso.SelectedValue = aca.IdCertificaciones;
            TXT_Nota.Text = aca.Nota.ToString();
            TXT_Horas.Text = aca.Horas.ToString();
            LB_FolioRegistro.Content = aca.FolioRegistroAcademico.ToString();

        }

        private void ResetNotaCertificacion()
        {
            TXT_Nota.Text = string.Empty;
            TXT_Nota.Focus();
        }

        private void ValidarNotaCertificacion(string campo)
        {
            if (string.IsNullOrWhiteSpace(campo))
            {
                ResetNotaCertificacion(); ;
                return;
            }

            // Normalizar separador decimal
            string normalizado = campo.Trim().Replace(',', '.');

            // Intentar convertir a decimal usando cultura invariante
            if (decimal.TryParse(normalizado, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal nota))
            {
                // Validar rango
                if (nota < 1.0m || nota > 7.0m)
                {
                    ms.MostrarError("La nota debe estar entre 1.0 y 7.0.");
                    ResetNotaCertificacion();
                    return;
                }

                // Validar que tenga exactamente un decimal
                string[] partes = normalizado.Split('.');
                if (partes.Length == 2 && partes[1].Length == 1)
                {
                    TXT_Horas.Focus();
                    return;
                }

                ms.MostrarError("La nota debe tener solo un decimal (ej: 5.3).");
                ResetNotaCertificacion();
                return;
            }
        }

        private void TXT_Nota_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_Nota.Text;
                ValidarNotaCertificacion(campo);
            }

        }

        private void ResetHorasCertificacion()
        {
            TXT_Horas.Text = string.Empty;
            TXT_Horas.Focus();
        }

        private void HabilitarBotones()
        {
            BTN_Enviar.IsEnabled = false;
            BTN_Generar.IsEnabled = false;
            BTN_Grabar.IsEnabled = true;
            BTN_Imprimir.IsEnabled = false;
        }

        private string GeneraFolioCertificacion(DateTime fecha)
        {
            string final = $"{fecha.ToString("dd")}{fecha.ToString("MM")}{fecha.ToString("yyyy")}";
            return $"{TXT_Rut.Text}-RCSTecMed{aca.AsignarId().ToString()}-{final}";
        }

        private void ValidarHorasCertificacion(string campo, DateTime fecha)
        {
            if (val.CampoVacio(campo))
            {
                ms.MostrarError("Debe Ingresar Horas de Duración de Curso");
                TXT_Horas.Focus();
                return;
            }

            if (!val.CampoSoloNumero(campo))
            {
                ms.MostrarError("Las horas de certificación deben ser numéricas.");
                ResetHorasCertificacion();
                return;
            }

            if (campo.Length > 5)
            {
                ms.MostrarError("Las horas de certificación no pueden superar los 5 dígitos.");
                ResetHorasCertificacion();
                return;
            }

            LB_FolioRegistro.Content = GeneraFolioCertificacion(fecha);
            HabilitarBotones();
            BTN_Grabar.Focus();
        }

        private void TXT_Horas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_Dv.Text;
                DateTime fecha = DP_FechaCurso.SelectedDate.Value;
                ValidarHorasCertificacion(campo, fecha);
            }

        }

        private bool Grabar()
        {
            try
            {
                aca.IdAcademcico = aca.AsignarId();
                aca.Rut = val.ConvertirEnteroSeguro(TXT_Rut.Text);
                aca.IdCertificacion = 2;
                aca.Fecha = (DateTime)DP_FechaCurso.SelectedDate;
                aca.IdCentroAcademico = 1;
                aca.IdCertificaciones = (int)CB_Curso.SelectedValue;
                aca.FolioRegistroAcademico = LB_FolioRegistro.Content.ToString();
                aca.IdUsuario = IdUsuario;
                aca.Nota = TXT_Nota.Text;
                aca.Horas = val.ConvertirEnteroSeguro(TXT_Horas.Text);

                return aca.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar grabar los datos: " + ex.Message,
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void BTN_Grabar_Click(object sender, RoutedEventArgs e)
        {
            bool exito = Grabar();

            if (!exito)
            {
                ms.MostrarError(
                    "No se logró registrar la certificación en la base de datos.\n" +
                    "Revise los datos ingresados o comuníquese con el administrador."
                );

                TXT_Rut.Focus();
                return;
            }

            ms.MostrarInformacion("Certificación registrada correctamente en la base de datos.");
            Limpiar();
        }

        private void BTN_Generar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Imprimir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Enviar_Click(object sender, RoutedEventArgs e)
        {

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
