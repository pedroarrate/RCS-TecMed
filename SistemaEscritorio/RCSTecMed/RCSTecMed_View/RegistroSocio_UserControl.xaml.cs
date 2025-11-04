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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
        private readonly Controll_ACADEMICO aca = new Controll_ACADEMICO();
        private readonly Controll_ESTABLECIMIENTOACTUAL esta = new Controll_ESTABLECIMIENTOACTUAL();

        private int IdUser {  get; set; }
        public RegistroSocio_UserControl(int idUsuario)
        {
            InitializeComponent();
            IdUser = idUsuario;
            
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
            NacionalidadComboBox();
            TXT_Domicilio.Text = string.Empty;
            RegionComboBox();
            CB_Comuna.SelectedIndex = -1;
            TXT_EMail.Text = string.Empty;
            TXT_FonoFijo.Text = string.Empty;
            TXT_FonoMovil.Text = string.Empty;
            EstadoSocioComboBox();
            DP_FechaRegistro.SelectedDate = DateTime.Today;

            //ANTECEDENTES ACADEMICOS
            TipoCertificacionComboBox();
            NombreCertificacionComboBox();
            DP_FechaCertificcion.SelectedDate = null;
            TXT_NotaCertificacion.Text = string.Empty;
            TXT_HorasCertificacion.Text = string.Empty;
            TXT_FolioCertificacion.Text = string.Empty;
            CentroAcademicoComboBox();

            //ANTECEDENTES LABORALES
            RegionLaboralComboBox();
            CB_ComunaLaboral.SelectedIndex = -1;
            CB_EstablecimientoLaboral.SelectedIndex = -1;
            TXT_DireccionLaboral.Text = string.Empty;
            TXT_TelefonoLaboral.Text = string.Empty;

            //FORMA DE PAGO
            FormaPagoComboBox();
            FechaPagoComboBox();

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
            CB_Comuna.IsEnabled = false;
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
            CB_ComunaLaboral.IsEnabled = false;
            CB_EstablecimientoLaboral.IsEnabled = true;
            TXT_DireccionLaboral.IsEnabled = false;
            TXT_TelefonoLaboral.IsEnabled = false;

            //FORMA DE PAGO
            CB_FormaPago.IsEnabled = true;
            CB_FechaAproxPago.IsEnabled = true;
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

        //METODOS DE COMBOBOX 
        private void NacionalidadComboBox()
        {
            var nac = new Controll_NACIONALIDAD().ReadAllOrdenadoNacionalidad();
            if (nac != null && nac.Any())
            {
                CB_Nacionalidad.ItemsSource = nac;
                CB_Nacionalidad.DisplayMemberPath = "NombreNacionalidad";
                CB_Nacionalidad.SelectedValuePath = "IdNacionalidad";
                CB_Nacionalidad.SelectedIndex = -1;
            }
            else
            {
                CB_Nacionalidad.ItemsSource = null;
                CB_Nacionalidad.IsEnabled = false;
            }
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

        private void EstadoSocioComboBox()
        {
            var estdado = new Controll_ESTADOSOCIO().ReadAllOrdenado();
            if (estdado != null && estdado.Any())
            {
                CB_EstadoSocio.ItemsSource = estdado;
                CB_EstadoSocio.DisplayMemberPath = "DescripcionEstadoSocio";
                CB_EstadoSocio.SelectedValuePath = "IdEstadoSocio";
                CB_EstadoSocio.SelectedIndex = -1;
            }
            else
            {
                CB_EstadoSocio.ItemsSource = null;
                CB_EstadoSocio.IsEnabled = false;
            }
        }

        private void TipoCertificacionComboBox()
        {
            var tipo = new Controll_CERTIFICACION().ListaTipoCertificacionOrdenadaDescrpcion();
            if (tipo != null && tipo.Any())
            {
                CB_TipoCertificacion.ItemsSource = tipo;
                CB_TipoCertificacion.DisplayMemberPath = "DescripcionCertificacion";
                CB_TipoCertificacion.SelectedValuePath = "IdCertificacion";
                CB_TipoCertificacion.SelectedIndex = -1;
            }
            else
            {
                CB_TipoCertificacion.ItemsSource = null;
                CB_TipoCertificacion.IsEnabled = false;
            }
        }

        private void NombreCertificacionComboBox()
        {            
            var certificacion = new Controll_CERTIFICACIONES().ListaNombreCertificacionesOrdenadaDescrpcion();
            if (certificacion != null && certificacion.Any())
            {
                CB_NombreCertificacion.ItemsSource = certificacion;                
                CB_NombreCertificacion.DisplayMemberPath = "NombreCertificacion";
                CB_NombreCertificacion.SelectedValuePath = "IdCertificaciones";
                CB_NombreCertificacion.SelectedIndex = -1;
            }
            else
            {
                CB_NombreCertificacion.ItemsSource = null;
                CB_NombreCertificacion.IsEnabled = false;
            }
        }

        private void CentroAcademicoComboBox()
        {
            var centro = new Controll_CENTROACADEMICO().ReadAllOrdenadoCentroAcademico();
            if (centro != null && centro.Any())
            {
                CB_CentroAcademico.ItemsSource = centro;
                CB_CentroAcademico.DisplayMemberPath = "NombreCentroAcademico";
                CB_CentroAcademico.SelectedValuePath = "IdCentroAcademico";
                CB_CentroAcademico.SelectedIndex = -1;
            }
            else
            {
                CB_CentroAcademico.ItemsSource = null;
                CB_CentroAcademico.IsEnabled = false;
            }
        }

        private void RegionLaboralComboBox()
        {
            var region = new Controll_REGION().ReadAll();
            if (region != null && region.Any())
            {
                CB_RegionLaboral.ItemsSource = region;
                CB_RegionLaboral.DisplayMemberPath = "NombreRegion";
                CB_RegionLaboral.SelectedValuePath = "IdRegion";
                CB_RegionLaboral.SelectedIndex = -1;
            }
            else
            {
                CB_RegionLaboral.ItemsSource = null;
                CB_RegionLaboral.IsEnabled = false;
            }
        }

        private void ComunaLaboralComboBox(int region)
        {
            var comuna = new Controll_COMUNA().ListaComunaPorRegion(region);
            if (comuna != null && comuna.Any())
            {
                CB_ComunaLaboral.ItemsSource = comuna;
                CB_ComunaLaboral.DisplayMemberPath = "NombreComuna";
                CB_ComunaLaboral.SelectedValuePath = "IdComuna";
                CB_ComunaLaboral.SelectedIndex = -1;
            }
            else
            {
                CB_ComunaLaboral.ItemsSource = null;
                CB_ComunaLaboral.IsEnabled = false;
            }
        }

        private void EstablecimientoLaboralComboBox(int region, string comuna)
        {
            var establecimiento = new Controll_ESTABLECIMIENTO().ListaEstablecimientoPorRegionComuna(region, comuna);
            if (establecimiento != null && establecimiento.Any())
            {
                CB_EstablecimientoLaboral.ItemsSource = establecimiento;
                CB_EstablecimientoLaboral.DisplayMemberPath = "NombreEstablecimiento";
                CB_EstablecimientoLaboral.SelectedValuePath = "IdEstablecimiento";
                CB_EstablecimientoLaboral.SelectedIndex = -1;
            }
            else
            {
                CB_EstablecimientoLaboral.ItemsSource = null;
                CB_EstablecimientoLaboral.IsEnabled = false;
            }
        }

        private void FormaPagoComboBox()
        {
            var formapago = new Controll_FORMAPAGO().ListaFormaPagoOrdenadoDesc();
            if (formapago != null && formapago.Any())
            {
                CB_FormaPago.ItemsSource = formapago;
                CB_FormaPago.DisplayMemberPath = "DescripcionFormaPagoCuota";
                CB_FormaPago.SelectedValuePath = "IdFormaPagoCuota";
                CB_FormaPago.SelectedIndex = -1;
            }
            else
            {
                CB_FormaPago.ItemsSource = null;
                CB_FormaPago.IsEnabled = false;
            }
        }

        private void FechaPagoComboBox()
        {
            var fechapago = new Controll_ESTIMADOPAGO().ListaEstimadoPagoOrdenado();
            if (fechapago != null && fechapago.Any())
            {
                CB_FechaAproxPago.ItemsSource = fechapago;
                CB_FechaAproxPago.DisplayMemberPath = "DescripcionEstimadoPago";
                CB_FechaAproxPago.SelectedValuePath = "IdEstimadoPago";
                CB_FechaAproxPago.SelectedIndex = -1;
            }
            else
            {
                CB_FechaAproxPago.ItemsSource = null;
                CB_FechaAproxPago.IsEnabled = false;
            }
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

            int rut = int.Parse(TXT_Rut.Text);
            rtm.Rut = rut;
            soc.Rut = rut;
            try
            {
                if (soc.ReadId())
                {
                    BTN_Buscar.Focus();
                }
                else
                {
                    if (rtm.ReadId())
                    {
                        bool cargar = Confirmacion("RUT ingresado existe en registro de Superintendencia.\n¿Desea cargar los datos encontrados?");                        

                        if (cargar)
                        {
                            CargarDatos(rtm);
                            ActivarEntradas();
                            TXT_Domicilio.Focus();
                        }
                        else
                        {
                            ActivarEntradas();
                            TXT_ApellidoPaterno.Focus();
                        }
                    }
                    else
                    {
                            ActivarEntradas();
                            TXT_ApellidoPaterno.Focus();
                       
                    }
                }
                
            }
            catch (Exception ex)
            {
                ms.MostrarError("Ocurrió un error al consultar el registro de Superintendencia.\n" +
                                "Detalle técnico: " + ex.Message);
                TXT_Rut.Focus();
            }

        }

        private void ValidarApPaterno(string campo)
        {
            if (val.CampoVacio(campo))
            {
                ms.MostrarError("El campo no puede estar vacío.");
                TXT_ApellidoPaterno.Focus();
                return;
            }

            int largo = val.LargoCampo(campo);
            if ((largo > 0 && largo < 2) || largo > 50)
            {
                ms.MostrarError($"Apellido Paterno tiene {largo} caracteres.\nDebe tener entre 2 y 50.");
                TXT_ApellidoPaterno.Text = string.Empty;
                TXT_ApellidoPaterno.Focus();
                return;
            }

            TXT_ApellidoMaterno.Focus();
            return;
        }

        private void TXT_ApellidoPaterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_ApellidoPaterno.Text;
                ValidarApPaterno(campo);
            }
        }

        private void ValidarApMaterno(string campo)
        {
            if (val.CampoVacio(campo))
            {
                TXT_ApellidoMaterno.Text = "*";
                TXT_Nombres.Focus();
                return;
            }

            int largo = val.LargoCampo(campo);
            if ((largo > 0 && largo < 1) || largo > 50)
            {
                ms.MostrarError($"Apellido Materno tiene {largo} caracteres.\nDebe tener entre 1 y 50.");
                TXT_ApellidoMaterno.Text = string.Empty;
                TXT_ApellidoMaterno.Focus();
                return;
            }

            TXT_Nombres.Focus();
            return;
        }

        private void TXT_ApellidoMaterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_ApellidoMaterno.Text;
                ValidarApMaterno(campo);
            }
        }

        private void ValidarNombres(string campo)
        {
            if (val.CampoVacio(campo))
            {
                ms.MostrarError("El campo no puede estar vacío.");
                TXT_Nombres.Focus();
                return;
            }

            int largo = val.LargoCampo(campo);
            if ((largo > 0 && largo < 2) || largo > 50)
            {
                ms.MostrarError($"Nombres tiene {largo} caracteres.\nDebe tener entre 2 y 50.");
                TXT_Nombres.Text = string.Empty;
                TXT_Nombres.Focus();
                return;
            }

            DP_FechaNacimiento.Focus();
            return;
        }

        private void TXT_Nombres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_Nombres.Text;
                ValidarNombres(campo);
            }
        }

        private bool FechaMayorDeEdad(DateTime? fechaSeleccionada)
        {
            if (!fechaSeleccionada.HasValue)
            {
                ms.MostrarError("Debe seleccionar una fecha.");
                return false;
            }

            DateTime fechaMinima = DateTime.Today.AddYears(-18);
            if (fechaSeleccionada.Value > fechaMinima)
            {
                ms.MostrarError("Debe tener al menos 18 años.");
                return false;
            }

            return true;
        }

        private void DP_FechaNacimiento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!FechaMayorDeEdad(DP_FechaNacimiento.SelectedDate))
            {
                DP_FechaNacimiento.Focus();
                return;
            }

            TXT_RegistroSuperintendencia.Focus();
        }

        private void ValidaRegistroSuperintenedencia(string campo)
        {
            if (val.CampoVacio(campo))
            {
                CB_Nacionalidad.Focus();
                return;
            }
        
            if (!val.CampoSoloNumero(campo))
            {
                ms.MostrarError("Registro de Superintendencia debe ser Numérico");
                TXT_RegistroSuperintendencia.Text = string.Empty;
                TXT_RegistroSuperintendencia.Focus();
                return;
            }
            CB_Nacionalidad.Focus(); 
        }

        private void TXT_RegistroSuperintendencia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_RegistroSuperintendencia.Text;
                ValidaRegistroSuperintenedencia(campo);
            }
        }

        private void CB_Nacionalidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBAvanzarSiSeSelecciona(CB_Nacionalidad, TXT_Domicilio);
        }

        private void ValidarDomicilioPersonal(string campo)
        {
            if (val.CampoVacio(campo))
            {
                ms.MostrarError("El campo no puede estar vacío.");
                TXT_Domicilio.Focus();
                return;
            }

            int largo = val.LargoCampo(campo);
            if ((largo > 0 && largo < 10) || largo > 150)
            {
                ms.MostrarError($"Domicilio tiene {largo} caracteres.\nDebe tener entre 10 y 150.");
                TXT_Domicilio.Text = string.Empty;
                TXT_Domicilio.Focus();
                return;
            }

            CB_Region.Focus();
            return;
        }

        private void TXT_Domicilio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_Domicilio.Text;
                ValidarDomicilioPersonal(campo);
            }
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
            CBAvanzarSiSeSelecciona(CB_Comuna, TXT_EMail);
        }

        private void ResetEmail()
        {
            TXT_EMail.Text = string.Empty;
            TXT_EMail.Focus();
        }

        private void ValidarEmail(string campo)
        {
            if (!val.FormatoCorreoValido(campo))
            {
                ms.MostrarError("Email no puede estar vacio o Formato Invalido");
                ResetEmail();
                return;
            }

            int largo = val.LargoCampo(campo);
            if (largo > 150)
            {
                ms.MostrarError($"Email tiene {largo} caracteres\nNo puede ser mayor a 150 carateres");
                ResetEmail();
                return;
            }

            TXT_FonoFijo.Focus();
            return;
        }


        private void TXT_EMail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_EMail.Text;
                ValidarEmail(campo);
            }
        }

        private void ResetFonoFijo()
        {
            TXT_FonoFijo.Text = string.Empty;
            TXT_FonoFijo.Focus();
        }

        private void ValidarTelefonoFijo(string campo)
        {
            if (val.CampoVacio(campo))
            {
                ms.MostrarError("El campo no puede estar vacío.\nSi no cuenta con número de teléfono, debe ingresar 0.");
                TXT_FonoFijo.Focus();
                return;
            }

            if (!val.CampoSoloNumero(campo))
            {
                ms.MostrarError("El teléfono debe contener solo números.");
                ResetFonoFijo();
                return;
            }

            int largo = val.LargoCampo(campo);
            if (largo > 9)
            {
                ms.MostrarError($"El teléfono ingresado tiene {largo} dígitos.\nNo debe superar los 9 dígitos.");
                ResetFonoFijo();
                return;
            }

            int telefono = int.Parse(campo);
            if (largo < 9 && telefono != 0)
            {
                ms.MostrarError($"El teléfono ingresado tiene {largo} dígitos.\nDebe tener 9 o ser 0.");
                ResetFonoFijo();
                return;
            }

            TXT_FonoMovil.Focus();
            return;
        }

        private void TXT_FonoFijo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_FonoFijo.Text;
                ValidarTelefonoFijo(campo);
            }
        }

        private void ResetFonoMovil()
        {
            TXT_FonoMovil.Text = string.Empty;
            TXT_FonoMovil.Focus();
        }

        private void ValidarTelefonoMovil(string campo)
        {
            if (val.CampoVacio(campo))
            {
                ms.MostrarError("El campo no puede estar vacío.\nSi no cuenta con número de teléfono, debe ingresar 0.");
                TXT_FonoMovil.Focus();
                return;
            }

            if (!val.CampoSoloNumero(campo))
            {
                ms.MostrarError("El teléfono debe contener solo números.");
                ResetFonoMovil();
                return;
            }

            int largo = val.LargoCampo(campo);
            if (largo > 9)
            {
                ms.MostrarError($"El teléfono ingresado tiene {largo} dígitos.\nNo debe superar los 9 dígitos.");
                ResetFonoMovil();
                return;
            }

            int telefono = int.Parse(campo);
            if (largo < 9 && telefono != 0)
            {
                ms.MostrarError($"El teléfono ingresado tiene {largo} dígitos.\nDebe tener 9 o ser 0.");
                ResetFonoMovil();
                return;
            }

            CB_EstadoSocio.Focus();
            return;
        }
        private void TXT_FonoMovil_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_FonoMovil.Text;
                ValidarTelefonoMovil(campo);
            }
        }

        //DATOS ACADEMICOS
        private void CB_TipoCertificacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBAvanzarSiSeSelecciona(CB_TipoCertificacion, CB_NombreCertificacion);
        }

        private void CB_NombreCertificacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBAvanzarSiSeSelecciona(CB_NombreCertificacion, TXT_NotaCertificacion);
        }

        private void ResetNotaCertificacion()
        {
            TXT_NotaCertificacion.Text = string.Empty;
            TXT_NotaCertificacion.Focus();
        }

        private void ValidarNotaCertificacion(string campo)
        {
            // Permitir campo vacío
            if (string.IsNullOrWhiteSpace(campo))
            {
                TXT_HorasCertificacion.Focus();
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
                    TXT_HorasCertificacion.Focus();
                    return;
                }

                ms.MostrarError("La nota debe tener solo un decimal (ej: 5.3).");
                ResetNotaCertificacion();
                return;
            }

            ms.MostrarError("La nota debe ser un número válido.");
            ResetNotaCertificacion();
        }


        private void TXT_NotaCertificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_NotaCertificacion.Text;
                ValidarNotaCertificacion(campo);
            }
        }

        private void ResetHorasCertificacion()
        {
            TXT_HorasCertificacion.Text = string.Empty;
            TXT_HorasCertificacion.Focus();
        }

        private void ValidarHorasCertificacion(string campo)
        {
            if (val.CampoVacio(campo))
            {
                TXT_FolioCertificacion.Focus();
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

            TXT_FolioCertificacion.Focus();
        }

        private void TXT_HorasCertificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_HorasCertificacion.Text;
                ValidarHorasCertificacion(campo);
            }
        }

        private void ValidarFolioCertificacion(string campo)
        {
            if (val.CampoVacio(campo))
            {
                CB_CentroAcademico.Focus();
                return;
            }

            if (campo.Length > 100)
            {
                ms.MostrarError("El Folio de certificación no puede superar los 100 caracteres.");
                TXT_FolioCertificacion.Text = string.Empty;
                TXT_FolioCertificacion.Focus();
                return;
            }

            CB_CentroAcademico.Focus();
        }

        private void TXT_FolioCertificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_FolioCertificacion.Text;
                ValidarFolioCertificacion(campo);
            }
        }

        private void CB_CentroAcademico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBAvanzarSiSeSelecciona(CB_CentroAcademico, CB_RegionLaboral);
        }

        //DATOS LABORALES
        private void CB_RegionLaboral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_RegionLaboral.SelectedValue is int id)
            {
                ComunaLaboralComboBox(id);
                CB_ComunaLaboral.IsEnabled = true;
                CB_ComunaLaboral.Focus();
            }
            else
            {
                CB_ComunaLaboral.IsEnabled = false;
            }
        }

        private void CB_ComunaLaboral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_ComunaLaboral.SelectedValue is string comuna &&
                CB_RegionLaboral.SelectedValue is int region)
            {
                EstablecimientoLaboralComboBox(region, comuna);
                CB_EstablecimientoLaboral.IsEnabled = true;
                CB_EstablecimientoLaboral.Focus();
            }
            else
            {
                CB_EstablecimientoLaboral.IsEnabled = false;
            }
        }

        private void LimpiarLaborales()
        {
            TXT_DireccionLaboral.Text = string.Empty;
            TXT_TelefonoLaboral.Text = string.Empty;
        }

        private void CB_EstablecimientoLaboral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_EstablecimientoLaboral.SelectedValue is int id)
            {
                var est = new Controll_ESTABLECIMIENTO { IdEstablecimiento = id };

                if (est.ReadId())
                {
                    TXT_DireccionLaboral.Text = est.Direccion ?? string.Empty;
                    TXT_TelefonoLaboral.Text = est.Telefono.ToString() ?? string.Empty;
                }
                else
                {
                    LimpiarLaborales();
                }
            }
            else
            {
                LimpiarLaborales();
            }

        }

        //FORMAS DE PAGO
        private void CB_FormaPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBAvanzarSiSeSelecciona(CB_FormaPago, CB_FechaAproxPago);
        }

        private void CB_FechaAproxPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBAvanzarSiSeSelecciona(CB_FechaAproxPago, BTN_Grabar);
        }

        //LLAMAR COMPLETAR DATOS
        private void BTN_CompletarDatos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vcds = new View_CompletarDatosRegistroSocio(IdUser);
                vcds.ShowDialog();
                Limpiar();
            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error al Abrir Ventana para Completar Datos\nEl Error es: {ex.Message}");
            }
        }

        //BOTONES DE CRUD
        //BOTON: BUSCAR
        private void ExtraerDatosPersonales(int rut)
        {
            soc.Rut = rut;

            if (!soc.ReadId())
                return;

            TXT_ApellidoPaterno.Text = soc.ApellidoPaterno;
            TXT_ApellidoMaterno.Text = soc.ApellidoMaterno;
            TXT_Nombres.Text = soc.Nombres;
            DP_FechaNacimiento.SelectedDate = soc.FechaNacimiento;
            DP_FechaRegistro.SelectedDate = soc.FechaRegistro;

            TXT_RegistroSuperintendencia.Text = soc.FolioSuperintendencia?.ToString() ?? string.Empty;
            CB_Nacionalidad.SelectedValue = soc.IdNacionalidad;
            TXT_Domicilio.Text = soc.Domicilio;
            CB_Region.SelectedValue = md.IdRegionxComuna(soc.IdComuna);
            CB_Comuna.SelectedValue = soc.IdComuna;
            TXT_EMail.Text = soc.Email;
            TXT_FonoFijo.Text = $"{soc.TelFijo}";
            TXT_FonoMovil.Text = $"{soc.TelMovil}";
            CB_EstadoSocio.SelectedValue = soc.IdEstadoSocio;
        }

        private void ExtraerAntecedentesAcedemicos(int rut)
        {
            var ac = new Controll_ACADEMICO();

            if (!ac.ReadRut(rut))
                return;

            CB_TipoCertificacion.SelectedValue = ac.IdCertificacion;
            CB_NombreCertificacion.SelectedValue = ac.IdCertificaciones;
            DP_FechaCertificcion.SelectedDate = ac.Fecha;

            TXT_NotaCertificacion.Text = ac.Nota?.ToString() ?? string.Empty;
            TXT_HorasCertificacion.Text = ac.Horas?.ToString() ?? string.Empty;
            TXT_FolioCertificacion.Text = ac.FolioRegistroAcademico ?? string.Empty;

            CB_CentroAcademico.SelectedValue = ac.IdCentroAcademico;

        }

        private void ExtraerAntecedentesLaborales(int rut)
        {
            var lab = new Controll_ESTABLECIMIENTOACTUAL();
            var est = new Controll_ESTABLECIMIENTO();

            if (!lab.ReadRutFechaHastaNull(rut))
                return;

            est.IdEstablecimiento = lab.IdEstablecimiento;

            if (!est.ReadId())
                return;

            CB_RegionLaboral.SelectedValue = md.IdRegionxComuna(est.IdComuna);
            CB_ComunaLaboral.SelectedValue = est.IdComuna;
            CB_EstablecimientoLaboral.SelectedValue = lab.IdEstablecimiento;
            TXT_DireccionLaboral.Text = est.Direccion ?? string.Empty;
            TXT_TelefonoLaboral.Text = $"{est.Telefono}";
        }

        private void ExtraerFormaPago(int rut)
        {
            soc.Rut = rut;

            if (!soc.ReadId())
                return;

            CB_FormaPago.SelectedValue = soc.IdFormaPagoCuota;
            CB_FechaAproxPago.SelectedValue = soc.IdEstimadoPago;
        }
        
        private void ActivarEntradasBusqueda()
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
            CB_Comuna.IsEnabled = false;
            TXT_EMail.IsEnabled = true;
            TXT_FonoFijo.IsEnabled = true;
            TXT_FonoMovil.IsEnabled = true;
            CB_EstadoSocio.IsEnabled = true;

            //FORMA DE PAGO
            CB_FormaPago.IsEnabled = true;
            CB_FechaAproxPago.IsEnabled = true;
        }

        private void MostrarDatosBusqueda(int rut)
        {
            soc.Rut = rut;

            if (!soc.ReadId())
            {
                ms.MostrarError("RUT a Buscar no Encontrado en Base de Datos de Socios");
                Limpiar();
                return;
            }

            ms.MostrarInformacion("RUT encontrado, se desplegaran los campos");
            ActivarEntradasBusqueda();

            //ANTECEDENTES PERSONALES
            ExtraerDatosPersonales(rut);

            //ANTECEDENTES ACADEMICOS
            ExtraerAntecedentesAcedemicos(rut);

            //ANTECEDENTES LABORALES
            ExtraerAntecedentesLaborales(rut);

            //FORMA DE PAGO
            ExtraerFormaPago(rut);
        }
   
        private void ResetRutyDv()
        {
            TXT_Rut.Text = string.Empty;
            TXT_Dv.Text = string.Empty;
            TXT_Rut.Focus();
        }

        private bool VerificaRutyDv(string rut, string dv)
        {
            if (val.CampoVacio(rut) || val.CampoVacio(dv))
            {
                ms.MostrarError("Debe ingresar un RUT y DV válidos para realizar la búsqueda.");
                ResetRutyDv();
                return false;
            }

            if (!int.TryParse(rut, out int rt))
            {
                ms.MostrarError("El RUT ingresado no es numérico.");
                ResetRutyDv();
                return false;
            }

            string dvCalculado = val.CalcularDV(rt);
            if (!string.Equals(dvCalculado, dv, StringComparison.OrdinalIgnoreCase))
            {
                ms.MostrarError("El RUT y DV ingresados no son válidos.");
                ResetRutyDv();
                return false;
            }

            return true;
        }

        private void BTN_Buscar_Click(object sender, RoutedEventArgs e)
        {
            string rut = TXT_Rut.Text;
            string dv = TXT_Dv.Text;

            if (!VerificaRutyDv(rut, dv))
                return;

            int rt = val.ConvertirEnteroSeguro(rut);
            MostrarDatosBusqueda(rt);
        }

        //BOTONES: GRABAR Y ACTUALIZAR
        private bool ValidarEntradas()
        {
            bool esValido = !val.CampoVacio(TXT_Rut.Text) &&
                !val.CampoVacio(TXT_Dv.Text) && 
                !val.CampoVacio(TXT_ApellidoPaterno.Text) &&
                !val.CampoVacio(TXT_ApellidoMaterno.Text) && 
                !val.CampoVacio(TXT_Nombres.Text) &&
                DP_FechaNacimiento.SelectedDate != null && 
                CB_Nacionalidad.SelectedIndex != -1 &&
                !val.CampoVacio(TXT_Domicilio.Text) && 
                CB_Region.SelectedIndex != -1 &&
                CB_Comuna.SelectedIndex != -1 && 
                !val.CampoVacio(TXT_EMail.Text) &&
                !val.CampoVacio(TXT_FonoFijo.Text) && 
                !val.CampoVacio(TXT_FonoMovil.Text) &&
                CB_EstadoSocio.SelectedIndex != -1 && 
                CB_TipoCertificacion.SelectedIndex != -1 &&
                CB_NombreCertificacion.SelectedIndex != -1 && 
                DP_FechaCertificcion.SelectedDate != null &&
                CB_CentroAcademico.SelectedIndex != -1 && 
                CB_RegionLaboral.SelectedIndex != -1 &&
                CB_ComunaLaboral.SelectedIndex != -1 && 
                CB_EstablecimientoLaboral.SelectedIndex != -1 &&
                !val.CampoVacio(TXT_DireccionLaboral.Text) && 
                !val.CampoVacio(TXT_TelefonoLaboral.Text) &&
                CB_FormaPago.SelectedIndex != -1 && 
                CB_FechaAproxPago.SelectedIndex != -1;

            if (!esValido)
            {
                ms.MostrarError("Todos los Datos son obligatorios con excepción de:\n" +
                    "- N° Reg. Superintendencia\n" +
                    "- Nota Certificación\n" +
                    "- Hora Certificación\n" +
                    "- Folio Certificación");

                TXT_Rut.Focus();
                return false;
            }

            return true;
        }

        private bool GrabarSocio(int rut) 
        {
            soc.Rut = rut;

            if (soc.ReadId())
            {
                ms.MostrarError("RUT ya Existe en Base de Datos\nNo es posible Registrar");
                TXT_Rut.Focus();
                return false;
            }

            soc.FolioRegistro = soc.AsignarFolioRegistro();
            soc.ApellidoPaterno = TXT_ApellidoPaterno.Text.ToUpper();
            soc.ApellidoMaterno = TXT_ApellidoMaterno.Text.ToUpper();
            soc.Nombres = TXT_Nombres.Text.ToUpper();
            soc.FechaNacimiento = (DateTime)DP_FechaNacimiento.SelectedDate;
            soc.Domicilio = TXT_Domicilio.Text.ToUpper();
            soc.IdComuna = (string)CB_Comuna.SelectedValue;
            soc.IdNacionalidad = (string)CB_Nacionalidad.SelectedValue;
            soc.TelMovil = val.ConvertirEnteroSeguro(TXT_FonoMovil.Text);
            soc.TelFijo = val.ConvertirEnteroSeguro(TXT_FonoFijo.Text);
            soc.Email = TXT_EMail.Text.ToUpper();
            soc.IdFormaPagoCuota = (int)CB_FormaPago.SelectedValue;
            soc.ObservacionPagocuota = CB_FechaAproxPago.Text.ToUpper();
            soc.IdUsuario = IdUser;
            soc.IdEstadoSocio = (int)CB_EstadoSocio.SelectedValue;
            if (val.CampoVacio(TXT_RegistroSuperintendencia.Text))
            {
                soc.FolioSuperintendencia = null;
            }
            else
            {
                soc.FolioSuperintendencia = val.ConvertirEnteroSeguro(TXT_RegistroSuperintendencia.Text);
            }
            soc.IdEstimadoPago = (int)CB_FechaAproxPago.SelectedValue;
            soc.FechaRegistro = (DateTime)DP_FechaRegistro.SelectedDate;

            try
            {
                if (!soc.Create())
                {
                    ms.MostrarError("No es posible Grabar Registro en BD\nComunicarse con Administrador");
                    Limpiar();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error en Base de datos: {ex.Message}");
                return false;
            }

            return true;
        }

        private bool GrabarAcademico()
        {
            aca.IdAcademcico = aca.AsignarId();
            aca.Rut = val.ConvertirEnteroSeguro(TXT_Rut.Text);
            aca.IdCertificacion = (int)CB_TipoCertificacion.SelectedValue;
            aca.Fecha = (DateTime)DP_FechaCertificcion.SelectedDate;
            aca.IdCentroAcademico = (int)CB_CentroAcademico.SelectedValue;
            aca.IdCertificaciones = (int)CB_NombreCertificacion.SelectedValue;
            aca.FolioRegistroAcademico = string.Empty;
            aca.IdUsuario = IdUser;
            if (val.CampoVacio(TXT_NotaCertificacion.Text))
            {
                aca.Nota = null;
            }
            else
            {
                aca.Nota = val.ConvertirDecimalSeguro(TXT_NotaCertificacion.Text);
            }

            if (val.CampoVacio(TXT_HorasCertificacion.Text))
            {
                aca.Horas = null;
            }
            else
            {
                aca.Horas = val.ConvertirEnteroSeguro(TXT_HorasCertificacion.Text);
            }

            try
            {
                if (!aca.Create())
                {
                    ms.MostrarError("No es posible Grabar Registro en BD\nComunicarse con Administrador");
                    Limpiar();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error en Base de datos: {ex.Message}");
                return false;
            }

            return true;
        }

        private bool GrabarEstablecimiento()
        {
            esta.IdEstablecimientoActual = esta.AsignarId();
            esta.IdEstablecimiento = (int)CB_EstablecimientoLaboral.SelectedValue;
            esta.Rut = val.ConvertirEnteroSeguro(TXT_Rut.Text);
            esta.FechaDesde = (DateTime)DP_FechaRegistro.SelectedDate;
            esta.FechaHasta = null;
            esta.IdUsuario = IdUser;

            try
            {
                if (!esta.Create())
                {
                    ms.MostrarError("No es posible Grabar Registro en BD\nComunicarse con Administrador");
                    Limpiar();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error en Base de datos: {ex.Message}");
                return false;
            }

            return true;
        }

        private void BTN_Grabar_Click(object sender, RoutedEventArgs e) //BOTON GRABAR
        {
            if (!ValidarEntradas())
                return;

            int rut = int.Parse(TXT_Rut.Text);
            soc.Rut = rut;
            if (soc.ReadId())
            {
                ms.MostrarError("RUT ya Existe en Base de Datos\nNo es posible Registrar");
                TXT_Rut.Focus();
                return;
            }

            if (!GrabarSocio(rut))
                return;

            if (!GrabarAcademico())
                return;

            if (!GrabarEstablecimiento())
                return;

            ms.MostrarInformacion("Se Registran datos en las Siguientes Tablas:\n" +
                "-Socios\n" +
                "-Academico\n" +
                "-Establecimiento Actual");

            Limpiar();
        }

        private bool ActualizarSocio()
        {

            soc.Rut = val.ConvertirEnteroSeguro(TXT_Rut.Text);
            soc.FolioRegistro = soc.AsignarFolioRegistro();
            soc.ApellidoPaterno = TXT_ApellidoPaterno.Text.ToUpper();
            soc.ApellidoMaterno = TXT_ApellidoMaterno.Text.ToUpper();
            soc.Nombres = TXT_Nombres.Text.ToUpper();
            soc.FechaNacimiento = (DateTime)DP_FechaNacimiento.SelectedDate;
            soc.Domicilio = TXT_Domicilio.Text.ToUpper();
            soc.IdComuna = (string)CB_Comuna.SelectedValue;
            soc.IdNacionalidad = (string)CB_Nacionalidad.SelectedValue;
            soc.TelMovil = val.ConvertirEnteroSeguro(TXT_FonoMovil.Text);
            soc.TelFijo = val.ConvertirEnteroSeguro(TXT_FonoFijo.Text);
            soc.Email = TXT_EMail.Text.ToUpper();
            soc.IdFormaPagoCuota = (int)CB_FormaPago.SelectedValue;
            soc.ObservacionPagocuota = CB_FechaAproxPago.Text.ToUpper();
            soc.IdUsuario = IdUser;
            soc.IdEstadoSocio = (int)CB_EstadoSocio.SelectedValue;
            if (val.CampoVacio(TXT_RegistroSuperintendencia.Text))
            {
                soc.FolioSuperintendencia = null;
            }
            else
            {
                soc.FolioSuperintendencia = val.ConvertirEnteroSeguro(TXT_RegistroSuperintendencia.Text);
            }
            soc.IdEstimadoPago = (int)CB_FechaAproxPago.SelectedValue;
            soc.FechaRegistro = (DateTime)DP_FechaRegistro.SelectedDate;

            try
            {
                if (!soc.Update())
                {
                    ms.MostrarError("No es posible Actualizar Registro en BD\nComunicarse con Administrador");
                    Limpiar();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ms.MostrarError($"Error en Base de datos: {ex.Message}");
                return false;
            }

            return true;
        }

        private void BTN_Actualizar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarEntradas())
                return;

            bool actualiza = Confirmacion("¿Confirma actualización de Datos de Socio?");

            if (!actualiza)
                return;

            if (!ActualizarSocio())
                return;

            ms.MostrarInformacion("Registro de Socio Actualizado en BD");
            Limpiar();
        }

        /*
        private void BTN_Eliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        */

        private void BTN_Limpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        //BOTON HOME
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
