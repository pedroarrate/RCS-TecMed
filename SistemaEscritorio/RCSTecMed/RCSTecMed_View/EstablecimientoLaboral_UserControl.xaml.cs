using RCSTecMed_Controll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
using System.Windows.Threading;

namespace RCSTecMed_View
{
    /// <summary>
    /// Lógica de interacción para EstablecimientoLaboral_UserControl.xaml
    /// </summary>
    public partial class EstablecimientoLaboral_UserControl : UserControl
    {
        private readonly MostrarDatos_Controll md = new MostrarDatos_Controll();
        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private readonly Mensajes_Controll msc = new Mensajes_Controll();
        private readonly Controll_ESTABLECIMIENTO est = new Controll_ESTABLECIMIENTO();

        private int idUsuario { get; set; }

        public EstablecimientoLaboral_UserControl(int idUser)
        {
            InitializeComponent();
            idUsuario = idUser;

            Limpiar();            
        }

        //METODOS VARIOS
        private void Limpiar()
        {
            DG_Establecimiento.IsEnabled = false;

            LB_TotalRegistrados.Content = md.TotalEstablecimientosRegistrados().ToString();

            TXT_IdEstablecimiento.Text = string.Empty;
            TXT_NombreEstablecimiento.Text = string.Empty;
            CB_Region.SelectedIndex = -1;
            CB_Comuna.SelectedIndex = -1;
            TXT_Direccion.Text = string.Empty;
            TXT_Telefono.Text = string.Empty;
            TXT_Email.Text = string.Empty;
            TXT_Contacto.Text = string.Empty;

            RegionComboBox();
            CB_Comuna.IsEnabled = false;

            TXT_IdEstablecimiento.Focus();
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

        private void ComunaComboBox(int idRegion)
        {
            var comuna = new Controll_COMUNA().ListaComunaPorRegion(idRegion);
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

        private void ResetIdTexto()
        {
            TXT_IdEstablecimiento.Text = string.Empty;
            TXT_IdEstablecimiento.Focus();
        }

        private void ResetNombreEstablecimiento()
        {
            TXT_NombreEstablecimiento.Text = string.Empty;
            TXT_NombreEstablecimiento.Focus();
        }

        private void ResetDireccion()
        {
            TXT_Direccion.Text = string.Empty;
            TXT_Direccion.Focus();
        }

        private void ResetTelefono()
        {
            TXT_Telefono.Text = string.Empty;
            TXT_Telefono.Focus();
        }

        private void ResetEmail()
        {
            TXT_Email.Text = string.Empty;
            TXT_Email.Focus();
        }

        private void ResetContacto()
        {
            TXT_Contacto.Text = string.Empty;
            TXT_Contacto.Focus();
        }

        private void ValidarIdEstablecimientoLaboral(string campo)
        {
            if (val.CampoVacio(campo))
            {
                MessageBoxResult resp = MessageBox.Show("Desea Asignar un Número de Registro", "RCSTecMed - Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (resp)
                {
                    case MessageBoxResult.Yes:
                        TXT_IdEstablecimiento.Text = est.AsignarId().ToString();
                        break;

                    case MessageBoxResult.No:
                        TXT_NombreEstablecimiento.Focus();
                        break;
                }
            }
            else
            {
                if (val.CampoSoloNumero(campo))
                {
                    TXT_NombreEstablecimiento.Focus();
                }
                else
                {
                    msc.MostrarError("Debe Ingresar Solo Números");
                    ResetIdTexto();
                }
            }
        }

        private void ValidarNombreEstablecimientoLaboral(string campo)
        {
            if (val.CampoVacio(campo))
            {
                msc.MostrarError("El campo no puede estar vacío.");
                TXT_NombreEstablecimiento.Focus();
                return;
            }

            int largo = val.LargoCampo(campo);
            if ((largo > 0 && largo < 10) || largo > 200)
            {
                msc.MostrarError($"La descripción ingresada tiene {largo} caracteres.\nDebe tener entre 10 y 200.");
                ResetNombreEstablecimiento();
                return;
            }

            CB_Region.Focus();
            return;
        }

        private void ValidarDireccionEstablecimientoLaboral(string campo)
        {
            if (val.CampoVacio(campo))
            {
                msc.MostrarError("El campo no puede estar vacío.");
                TXT_NombreEstablecimiento.Focus();
                return;
            }

            int largo = val.LargoCampo(campo);
            if ((largo > 0 && largo < 10) || largo > 200)
            {
                msc.MostrarError($"La descripción ingresada tiene {largo} caracteres.\nDebe tener entre 10 y 200.");
                ResetNombreEstablecimiento();
                return;
            }

            CB_Region.Focus();
            return;
        }

        private void ValidarNumeroTelefono(string campo)
        {
            if (val.CampoVacio(campo))
            {
                msc.MostrarError("El campo no puede estar vacío.\nSi no cuenta con número de teléfono, debe ingresar 0.");
                TXT_Telefono.Focus();
                return;
            }

            if (!val.CampoSoloNumero(campo))
            {
                msc.MostrarError("El teléfono debe contener solo números.");
                ResetTelefono();
                return;
            }

            int largo = val.LargoCampo(campo);
            if (largo > 9)
            {
                msc.MostrarError($"El teléfono ingresado tiene {largo} dígitos.\nNo debe superar los 9 dígitos.");
                ResetTelefono();
                return;
            }

            int telefono = int.Parse(campo);
            if (largo < 9 && telefono != 0)
            {
                msc.MostrarError($"El teléfono ingresado tiene {largo} dígitos.\nDebe tener 9 o ser 0.");
                ResetTelefono();
                return;
            }

            TXT_Email.Focus();
            return;
        }

        private void ValidarEmailEstablecimientoLaboral(string campo)
        {
            if (!val.FormatoCorreoValido(campo))
            {
                msc.MostrarError("Email no puede estar vacio o Formato Invalido");
                ResetEmail();
                return;
            }

            int largo = val.LargoCampo(campo);
            if (largo > 100)
            {
                msc.MostrarError($"Email tiene {largo} caracteres\nNo puede ser mayor a 100 carateres");
                ResetEmail();
                return;
            }

            TXT_Contacto.Focus();
            return;
        }

        private void ValidarContactoEstablecimientoLaboral(string campo)
        {
            if (val.CampoVacio(campo))
            {
                msc.MostrarError("El campo no puede estar vacío.");
                TXT_Contacto.Focus();
                return;
            }

            int largo = val.LargoCampo(campo);
            if ((largo > 0 && largo < 10) || largo > 200)
            {
                msc.MostrarError($"La descripción ingresada tiene {largo} caracteres.\nDebe tener entre 10 y 150.");
                ResetContacto();
                return;
            }

            BTN_Grabar.Focus();
            return;
        }

        private bool Confirmacion(string mensaje)
        {
            MessageBoxResult resultado = MessageBox.Show(mensaje, "Confirmación requerida", MessageBoxButton.YesNo, MessageBoxImage.Question);

            return resultado == MessageBoxResult.Yes;
        }

        //REGISTROS EN TEXTBOX Y COMBOBOX
        private void TXT_IdEstablecimiento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_IdEstablecimiento.Text;
                ValidarIdEstablecimientoLaboral(campo);
            }

        }

        private void TXT_NombreEstablecimiento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_NombreEstablecimiento.Text;
                ValidarNombreEstablecimientoLaboral(campo);
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

        private void TXT_Direccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_Direccion.Text;
                ValidarDireccionEstablecimientoLaboral(campo);
            }
        }

        private void TXT_Telefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_Telefono.Text;
                ValidarNumeroTelefono(campo);
            }
        }

        private void TXT_Email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_Email.Text;
                ValidarEmailEstablecimientoLaboral(campo);
            }
        }

        private void TXT_Contacto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_Contacto.Text;
                ValidarContactoEstablecimientoLaboral(campo);
            }
        }

        //ACCIONES DE GRILLA
        private void GrillaEstablecimientoLaboralCompleta()
        {
            try
            {
                DG_Establecimiento.ItemsSource = est.ListaEstablecimientoOrdenada();
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void DG_Establecimiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

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
            Limpiar();
        }

        private void BTN_Home_Click(object sender, RoutedEventArgs e)
        {
            // Buscar el TabControl en la ventana contenedora
            var parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                var mdSec = parentWindow as View_CompletarDatosRegistroSocio;
                if (mdSec != null)
                {
                    // Activar la pestaña "Home"
                    mdSec.MainTabControl.SelectedItem = mdSec.TAB_Home;
                }
            }

        }
    }
}
