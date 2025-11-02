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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RCSTecMed_Controll;

namespace RCSTecMed_View
{
    /// <summary>
    /// Lógica de interacción para RegistroSocio_UserControl.xaml
    /// </summary>
    public partial class RegistroSocio_UserControl : UserControl
    {
        private readonly Mensajes_Controll ms = new Mensajes_Controll();
        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private readonly MostrarDatos_Controll md = new MostrarDatos_Controll();
        private readonly Controll_REGISTROTECMED rtm = new Controll_REGISTROTECMED();
        private readonly Controll_SOCIO soc = new Controll_SOCIO();

        private int idUser {  get; set; }
        public RegistroSocio_UserControl(int idUsuario)
        {
            InitializeComponent();
            idUser = idUsuario;
            
            Limpiar();
        }

        private void Limpiar()
        {
            LB_TotalRegistrados.Content = md.TotalSociosRegistrados().ToString();

            //ANTECEDENTES PERSONALES
            TXT_Rut.Text = string.Empty;
            TXT_Dv.Text = string.Empty;
            TXT_ApellidoPaterno.Text = string.Empty;
            TXT_ApellidoMaterno.Text = string.Empty;
            TXT_Nombres.Text = string.Empty;
            DP_FechaNacimiento.SelectedDate = null;
            TXT_RegistroSuperintendencia.Text = string.Empty;
            CB_Nacionalidad.SelectedIndex = -1;
            TXT_Domicilio.Text = string.Empty;
            CB_Region.SelectedIndex = -1;
            CB_Comuna.SelectedIndex = -1;
            TXT_EMail.Text = string.Empty;
            TXT_FonoFijo.Text = string.Empty;
            TXT_FonoMovil.Text = string.Empty;
            CB_EstadoSocio.SelectedIndex = -1;
            
            //ANTECEDENTES ACADEMICOS
            CB_TipoCertificacion.SelectedIndex = -1;
            CB_NombreCertificacion.SelectedIndex = -1;
            DP_FechaCertificcion.SelectedDate = null;
            TXT_NotaCertificacion.Text = string.Empty;
            TXT_HorasCertificacion.Text = string.Empty;
            TXT_FolioCertificacion.Text = string.Empty;
            CB_CentroAcademico.SelectedIndex = -1;

            //ANTECEDENTES LABORALES
            CB_RegionLaboral.SelectedIndex = -1;
            CB_ComunaLaboral.SelectedIndex = -1;
            CB_EstablecimientoLaboral.SelectedIndex = -1;
            TXT_DireccionLaboral.Text = string.Empty;
            TXT_TelefonoLaboral.Text = string.Empty;

            //FORMA DE PAGO
            CB_FormaPago.SelectedIndex = -1;
            CB_FechaAproxPago.SelectedIndex = -1;

            BloquearEntradas();
        }

        private void BloquearEntradas()
        {
            //ANTECEDENTES PERSONALES
            TXT_ApellidoPaterno.IsEnabled = false;
            TXT_ApellidoMaterno.IsEnabled = false;
            TXT_Nombres.IsEnabled = false;
            DP_FechaNacimiento.IsEnabled = false;
            TXT_RegistroSuperintendencia.IsEnabled = false;
            CB_Nacionalidad.IsEnabled = false;
            TXT_Domicilio.IsEnabled = false;
            CB_Region.IsEnabled = false;
            CB_Comuna.IsEnabled = false;
            TXT_EMail.IsEnabled = false;
            TXT_FonoFijo.IsEnabled = false;
            TXT_FonoMovil.IsEnabled = false;
            CB_EstadoSocio.IsEnabled = false;

            //ANTECEDENTES ACADEMICOS
            CB_TipoCertificacion.IsEnabled = false;
            CB_NombreCertificacion.IsEnabled = false;
            DP_FechaCertificcion.IsEnabled = false;
            TXT_NotaCertificacion.IsEnabled = false;
            TXT_HorasCertificacion.IsEnabled = false;
            TXT_FolioCertificacion.IsEnabled = false;
            CB_CentroAcademico.IsEnabled = false;

            //ANTECEDENTES LABORALES
            CB_RegionLaboral.IsEnabled = false;
            CB_ComunaLaboral.IsEnabled = false;
            CB_EstablecimientoLaboral.IsEnabled = false;
            TXT_DireccionLaboral.IsEnabled = false;
            TXT_TelefonoLaboral.IsEnabled = false;

            //FORMA DE PAGO
            CB_FormaPago.IsEnabled = false;
            CB_FechaAproxPago.IsEnabled = false;
        }

        private void ActivarEntradas()
        {
            //ANTECEDENTES PERSONALES
            TXT_ApellidoPaterno.IsEnabled = true;
            TXT_ApellidoMaterno.IsEnabled = true;
            TXT_Nombres.IsEnabled = true;
            DP_FechaNacimiento.IsEnabled = true;
            TXT_RegistroSuperintendencia.IsEnabled = true;
            CB_Nacionalidad.IsEnabled = true;
            TXT_Domicilio.IsEnabled = true;
            CB_Region.IsEnabled = true;
            CB_Comuna.IsEnabled = true;
            TXT_EMail.IsEnabled = true;
            TXT_FonoFijo.IsEnabled = true;
            TXT_FonoMovil.IsEnabled = true;
            CB_EstadoSocio.IsEnabled = true;

            //ANTECEDENTES ACADEMICOS
            CB_TipoCertificacion.IsEnabled = true;
            CB_NombreCertificacion.IsEnabled = true;
            DP_FechaCertificcion.IsEnabled = true;
            TXT_NotaCertificacion.IsEnabled = true;
            TXT_HorasCertificacion.IsEnabled = true;
            TXT_FolioCertificacion.IsEnabled = true;
            CB_CentroAcademico.IsEnabled = true;

            //ANTECEDENTES LABORALES
            CB_RegionLaboral.IsEnabled = true;
            CB_ComunaLaboral.IsEnabled = true;
            CB_EstablecimientoLaboral.IsEnabled = true;
            TXT_DireccionLaboral.IsEnabled = true;
            TXT_TelefonoLaboral.IsEnabled = true;

            //FORMA DE PAGO
            CB_FormaPago.IsEnabled = true;
            CB_FechaAproxPago.IsEnabled = true;
        }

        private bool Confirmacion(string mensaje)
        {
            MessageBoxResult resultado = MessageBox.Show(mensaje, "Confirmación requerida", MessageBoxButton.YesNo, MessageBoxImage.Question);

            return resultado == MessageBoxResult.Yes;
        }

        private void CargarDatos(Controll_REGISTROTECMED rt)
        {
            //ANTECEDENTES PERSONALES
            TXT_ApellidoPaterno.Text = rt.ApellidoPaterno;
            TXT_ApellidoMaterno.Text = rt.ApellidoMaterno;
            TXT_Nombres.Text = rt.Nombres;
            DP_FechaNacimiento.SelectedDate = rt.FechaNacimiento;
            TXT_RegistroSuperintendencia.Text = rt.Registro.ToString();
            CB_Nacionalidad.SelectedValue = rt.IdNacionalidad;
            TXT_Domicilio.Text = string.Empty;
            CB_Region.SelectedIndex = -1;
            CB_Comuna.SelectedIndex = -1;
            TXT_EMail.Text = string.Empty;
            TXT_FonoFijo.Text = string.Empty;
            TXT_FonoMovil.Text = string.Empty;
            CB_EstadoSocio.SelectedIndex = -1;

            //ANTECEDENTES ACADEMICOS
            CB_TipoCertificacion.SelectedValue = rt.IdCertificacion;
            CB_NombreCertificacion.SelectedValue = rt.IdCertificaciones;
            DP_FechaCertificcion.SelectedDate = rt.FechaAntecedente;
            TXT_NotaCertificacion.Text = string.Empty;
            TXT_HorasCertificacion.Text = string.Empty;
            TXT_FolioCertificacion.Text = string.Empty;
            CB_CentroAcademico.SelectedValue = rt.IdCentroAcademico;

            //ANTECEDENTES LABORALES
            CB_RegionLaboral.SelectedValue = rt.IdRegion;
            CB_ComunaLaboral.SelectedIndex = -1;
            CB_EstablecimientoLaboral.SelectedIndex = -1;
            TXT_DireccionLaboral.Text = string.Empty;
            TXT_TelefonoLaboral.Text = string.Empty;

            //FORMA DE PAGO
            CB_FormaPago.SelectedIndex = -1;
            CB_FechaAproxPago.SelectedIndex = -1;
        }

        private void BTN_Home_Click(object sender, RoutedEventArgs e)
        {
            // Buscar el TabControl en la ventana contenedora
            var parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                var mdSec = parentWindow as View_ModuloSecretaria;
                if (mdSec != null)
                {
                    // Activar la pestaña "Home"
                    mdSec.MainTabControl.SelectedItem = mdSec.TAB_Home;
                }
            }

        }

        //BOTONES DE CRUD
        private void BTN_Buscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Grabar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Actualizar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Eliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Limpiar_Click(object sender, RoutedEventArgs e)
        {

        }

        //DATOS PERSONALES
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
                TXT_Rut.Text = string.Empty;
                TXT_Rut.Focus();
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
            int rut = int.Parse(TXT_Rut.Text);
            string dv = TXT_Dv.Text.ToUpper();

            bool esValido = val.CalcularDV(rut) == dv;

            if (!esValido)
            {
                ms.MostrarError("RUT ingresado no es Valido\n" +
                    "Fabor Reingresar RUT y DV");
                TXT_Rut.Text = string.Empty;
                TXT_Dv.Text = string.Empty;
                TXT_Rut.Focus();
            }

            return esValido;
        }

        private void TXT_Dv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Tab)
                return;

            if (!ValidarEntradaDV())
                return;

            if (!RutValido())
                return;

            rtm.Rut = int.Parse(TXT_Rut.Text);
            try
            {
                if (rtm.ReadId())
                {
                    bool cargar = Confirmacion("RUT ingresado existe en registro de Superintendencia.\n¿Desea cargar los datos encontrados?");
                    ActivarEntradas();

                    if (cargar)
                    {
                        CargarDatos(rtm);
                        TXT_Domicilio.Focus();
                    }
                    else
                    {
                        TXT_ApellidoPaterno.Focus();
                    }
                }
                else
                {
                    ActivarEntradas();
                    TXT_ApellidoPaterno.Focus();
                }
            }
            catch (Exception ex)
            {
                ms.MostrarError("Ocurrió un error al consultar el registro de Superintendencia.\n" +
                                "Detalle técnico: " + ex.Message);
                TXT_Rut.Focus();
            }

        }

        private void TXT_ApellidoPaterno_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_ApellidoMaterno_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_Nombres_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_Domicilio_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CB_Region_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TXT_EMail_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_FonoFijo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_FonoMovil_KeyDown(object sender, KeyEventArgs e)
        {

        }

        //DATOS ACADEMICOS
        private void TXT_NotaCertificacion_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_HorasCertificacion_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_FolioCertificacion_KeyDown(object sender, KeyEventArgs e)
        {

        }

        //DATOS LABORALES
        private void TXT_DireccionLaboral_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_TelefonoLaboral_KeyDown(object sender, KeyEventArgs e)
        {

        }

        //LLAMAR COMPLETAR DATOS
        private void BTN_CompletarDatos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vcds = new View_CompletarDatosRegistroSocio(idUser);
                vcds.ShowDialog();
            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error al Abrir Ventana para Completar Datos\nEl Error es: {ex.Message}");
            }
        }
    }
}
