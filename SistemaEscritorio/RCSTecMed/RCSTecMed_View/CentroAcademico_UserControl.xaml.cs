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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RCSTecMed_Controll;

namespace RCSTecMed_View
{
    /// <summary>
    /// Lógica de interacción para CentroAcademico_UserControl.xaml
    /// </summary>
    public partial class CentroAcademico_UserControl : UserControl
    {
        private readonly MostrarDatos_Controll md = new MostrarDatos_Controll();
        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private readonly Mensajes_Controll msc = new Mensajes_Controll();
        private readonly Controll_CENTROACADEMICO cca = new Controll_CENTROACADEMICO();

        private int idUsuario {  get; set; }
        public CentroAcademico_UserControl(int idUser)
        {
            InitializeComponent();
            idUsuario = idUser;
            LB_TotalRegistrados.Content = md.TotalCentrosAcademicosRegistrados().ToString();
            Limpiar();
        }

        //METODOS VARIOS
        private void Limpiar()
        {
            TXT_IdCentroAcademico.Text = string.Empty;
            TXT_NombreCentroAcademico.Text = string.Empty;
            TXT_TelefonoCentroAcademico.Text = string.Empty;
            TXT_EmailCentroAcademico.Text = string.Empty;

            CargarGrillaCentroAcademico();
            TXT_IdCentroAcademico.Focus();
        }

        private void ResetIdTexto()
        {
            TXT_IdCentroAcademico.Text = string.Empty;
            TXT_IdCentroAcademico.Focus();
        } 

        private void ResetNombreTexto()
        {
            TXT_NombreCentroAcademico.Text = string.Empty;
            TXT_NombreCentroAcademico.Focus();
        }

        private void ResetTelefono()
        {
            TXT_TelefonoCentroAcademico.Text = string.Empty;
            TXT_TelefonoCentroAcademico.Focus();
        }

        private void ResetEmail()
        {
            TXT_EmailCentroAcademico.Text = string.Empty;
            TXT_EmailCentroAcademico.Focus();
        }

        private void ValidarIdCentroAcademico(string campo)
        {
            if (val.CampoVacio(campo))
            {
                MessageBoxResult resp = MessageBox.Show("Desea Asignar un Numero de Registro", "RCSTecMed - Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (resp)
                {
                    case MessageBoxResult.Yes:
                        TXT_IdCentroAcademico.Text = cca.AsignarId().ToString();
                        break;

                    case MessageBoxResult.No:
                        TXT_NombreCentroAcademico.Focus();
                        break;
                }
            }
            else
            {
                if (val.CampoSoloNumero(campo))
                {
                    TXT_NombreCentroAcademico.Focus();
                }
                else
                {
                    msc.MostrarError("Debe Ingresar Solo Números");
                    ResetIdTexto();
                }
            }
        }

        private void ValidarNombreCentroacademico (string campo)
        {
            if (val.CampoVacio(campo))
            {
                msc.MostrarError("El campo no puede estar vacío.");
                TXT_NombreCentroAcademico.Focus();
                return;
            }

            int largo = val.LargoCampo(campo);
            if ((largo > 0 && largo < 10) || largo > 200 )
            {
                msc.MostrarError($"El nombre ingresado tiene {largo} caracteres.\nDebe tener entre 10 y 200.");
                ResetNombreTexto();
                return;
            }

            TXT_TelefonoCentroAcademico.Focus();
            return;
        }

        private void ValidarNumeroTelefono(string campo)
        {
            if (val.CampoVacio(campo))
            {
                msc.MostrarError("El campo no puede estar vacío.\nSi no cuenta con número de teléfono, debe ingresar 0.");
                TXT_TelefonoCentroAcademico.Focus();
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

            TXT_EmailCentroAcademico.Focus();
            return;
        }

        private void ValidarEmailCentroAcademico(string campo)
        {
            if (!val.FormatoCorreoValido(campo))
            {
                msc.MostrarError("Email no puede estar vacio o Formato Invalido");
                ResetEmail();
                return;
            }

            int largo = val.LargoCampo(campo);
            if (largo > 200)
            {
                msc.MostrarError($"Email tiene {largo} caracteres\nNo puede ser mayor a 200 carateres");
                ResetEmail();
                return;
            }

            BTN_Grabar.Focus();
            return;
        }

        //REGISTROS EN TEXTBOX
        private void TXT_IdCentroAcademico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_IdCentroAcademico.Text;
                ValidarIdCentroAcademico(campo);
            }
        }

        private void TXT_NombreCentroAcademico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_NombreCentroAcademico.Text;
                ValidarNombreCentroacademico(campo);
            }
        }

        private void TXT_TelefonoCentroAcademico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_TelefonoCentroAcademico.Text;
                ValidarNumeroTelefono(campo);
            }
        }

        private void TXT_EmailCentroAcademico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_EmailCentroAcademico.Text;
                ValidarEmailCentroAcademico(campo);
            }
        }

        //ACCIONES EN GRILLA
        private void CargarGrillaCentroAcademico()
        {
            try
            {
                DG_CentroAcademico.ItemsSource = cca.ReadAllOrdenadoCentroAcademico();
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }
        private void DG_CentroAcademico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //BOTONES DE ACCION
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
