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
            
            Limpiar();
        }

        //METODOS VARIOS
        private void Limpiar()
        {
            LB_TotalRegistrados.Content = md.TotalCentrosAcademicosRegistrados().ToString();

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

        private bool Confirmacion(string mensaje)
        {
            MessageBoxResult resultado = MessageBox.Show(mensaje, "Confirmación requerida", MessageBoxButton.YesNo, MessageBoxImage.Question);

            return resultado == MessageBoxResult.Yes;
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
            if (DG_CentroAcademico.SelectedItems.Count == 0)
                return;

            var ca = DG_CentroAcademico.SelectedItems[0] as Controll_CENTROACADEMICO;
            if (ca == null)
                return;

            TXT_IdCentroAcademico.Text = ca.IdCentroAcademico.ToString();
            TXT_NombreCentroAcademico.Text = ca.NombreCentroAcademico;
            TXT_TelefonoCentroAcademico.Text = ca.TelefonoCentroAcademico.ToString();
            TXT_EmailCentroAcademico.Text = ca.EmailCentroAcademico;
        }

        //BOTONES DE ACCION
        //BOTONES DE ACCION: BUSCAR
        private void MostrarMensajeBusquedaInvalida()
        {
            msc.MostrarError("Para Buscar tiene las siguientes opciones:" +
                "\n1. Ingresar un Número de Registrado y hacer click en botón BUSCAR" +
                "\n2. Ingresar un Nombre de Centro Académico Registrado y hacer click en botón BUSCAR" +
                "\n3. Seleccionar un Registro desde el Listado de la Grilla");
        }

        private void CargarDatosCentroAcademico(Controll_CENTROACADEMICO ca)
        {
            TXT_IdCentroAcademico.Text = ca.IdCentroAcademico.ToString();
            TXT_NombreCentroAcademico.Text = ca.NombreCentroAcademico;
            TXT_TelefonoCentroAcademico.Text = ca.TelefonoCentroAcademico.ToString();
            TXT_EmailCentroAcademico.Text = ca.EmailCentroAcademico;
        }

        private void CargarGrillaCentroAcademicoPorId(int id)
        {
            try
            {
                DG_CentroAcademico.ItemsSource = cca.ListaCentroAcademicoPorId(id);
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void CargarGrillaCentroAcademicoPorNombre(string nombre)
        {
            try
            {
                DG_CentroAcademico.ItemsSource = cca.ListaCentroAcademicoPorNombre(nombre);
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void BTN_Buscar_Click(object sender, RoutedEventArgs e)
        {
            string campoId = TXT_IdCentroAcademico.Text;
            string campoNombre = TXT_NombreCentroAcademico.Text;

            if (campoId.Length == 0 && campoNombre.Length == 0 || campoId.Length > 0 && campoNombre.Length > 0)
            {
                MostrarMensajeBusquedaInvalida();
                TXT_IdCentroAcademico.Focus();
                return;
            }

            if (campoId.Length > 0)
            {
                if (int.TryParse(campoId, out int id))
                {
                    cca.IdCentroAcademico = id;
                    if (cca.ReadId())
                    {
                        msc.MostrarInformacion("Registro Encontrado");
                        CargarDatosCentroAcademico(cca);
                        CargarGrillaCentroAcademicoPorId(id);
                    }
                    else
                    {
                        msc.MostrarError("Registro No Encontrado\nComunicarse con Administrador");
                        ResetIdTexto();
                    }
                }
                else
                {
                    msc.MostrarError("Recuerde, el campo Número de Registro debe ser numérico");
                    ResetIdTexto();
                }
            }
            else if (campoNombre.Length > 0)
            {
                cca.NombreCentroAcademico = campoNombre;
                if (cca.ReadCentroAcademico())
                {
                    msc.MostrarInformacion("Registro Encontrado");
                    CargarDatosCentroAcademico(cca);
                    CargarGrillaCentroAcademicoPorNombre(campoNombre);
                }
                else
                {
                    msc.MostrarError("Registro No Encontrado\nComunicarse con Administrador");
                    ResetIdTexto();
                }
            }

            TXT_IdCentroAcademico.Focus();
        }

        //BOTONES DE ACCION: GRABAR Y ACTUALIZAR
        private bool ValidarEntradas()
        {
            bool hayCampoVacio = val.CampoVacio(TXT_IdCentroAcademico.Text) || val.CampoVacio(TXT_NombreCentroAcademico.Text) || val.CampoVacio(TXT_TelefonoCentroAcademico.Text) || val.CampoVacio(TXT_EmailCentroAcademico.Text);

            if (hayCampoVacio)
            {
                msc.MostrarError("Debe completar todos los campos o Seleccionar un Registro");
                TXT_IdCentroAcademico.Focus();
                return false;
            }

            return true;
        }

        private void MostrarErrorYFoco(string mensaje, Control control)
        {
            msc.MostrarError(mensaje);
            control.Focus();
        }


        private void BTN_Grabar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarEntradas())
                return;

            cca.IdCentroAcademico = int.Parse(TXT_IdCentroAcademico.Text);
            cca.NombreCentroAcademico = TXT_NombreCentroAcademico.Text.ToUpper();

            if (cca.ReadId())
            {
                MostrarErrorYFoco("Número de Registro ya existe en la base de datos\nRevisar datos ingresados", TXT_IdCentroAcademico);
                return;
            }

            if (cca.ReadCentroAcademico())
            {
                MostrarErrorYFoco("Nombre de Centro Académico ya existe en la base de datos\nRevisar datos ingresados", TXT_NombreCentroAcademico);
                return;
            }

            cca.IdCentroAcademico = int.Parse(TXT_IdCentroAcademico.Text);
            cca.NombreCentroAcademico = TXT_NombreCentroAcademico.Text.ToUpper();
            cca.EmailCentroAcademico = TXT_EmailCentroAcademico.Text.ToUpper();
            cca.TelefonoCentroAcademico = int.Parse(TXT_TelefonoCentroAcademico.Text);
            cca.IdUsuario = idUsuario;

            if (!cca.Create())
            {
                msc.MostrarError("No se logró realizar el registro en la base de datos\nComunicarse con el administrador");
                Limpiar();
                return;
            }

            msc.MostrarInformacion("Registro exitoso en la base de datos");
            Limpiar();
        }

        private void BTN_Actualizar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarEntradas())
                return;

            cca.IdCentroAcademico = int.Parse(TXT_IdCentroAcademico.Text);
            if (!cca.ReadId())
            {
                MostrarErrorYFoco("No se encontró el registro a actualizar\nVerifique el Número de Registro ingresado", TXT_IdCentroAcademico);
                return;
            }

            if (!Confirmacion("¿Desea actualizar este registro?"))
                return;

            cca.IdCentroAcademico = int.Parse(TXT_IdCentroAcademico.Text);
            cca.NombreCentroAcademico = TXT_NombreCentroAcademico.Text.ToUpper();
            cca.EmailCentroAcademico = TXT_EmailCentroAcademico.Text.ToUpper();
            cca.TelefonoCentroAcademico = int.Parse(TXT_TelefonoCentroAcademico.Text);
            cca.IdUsuario = idUsuario;

            if (!cca.Update())
            {
                msc.MostrarError("No se logró realizar Actualización de registro en la base de datos\nComunicarse con el administrador");
                Limpiar();
                return;
            }

            msc.MostrarInformacion("Actualización de registro exitosa en la base de datos");
            Limpiar();
        }

        //BOTON ACCION: ELIMINAR
        private void BTN_Eliminar_Click(object sender, RoutedEventArgs e)
        {
            string idReg = TXT_IdCentroAcademico.Text;

            if (!ValidarEntradas())
                return;


            cca.IdCentroAcademico = int.Parse(idReg);
            if (!cca.ReadId())
            {
                MostrarErrorYFoco("No se encontró el registro a eliminar\nVerifique el Número de Registro ingresado", TXT_IdCentroAcademico);
                return;
            }

            if (Confirmacion("¿Desea Eliminar este registro?"))
            {
                View_ConfirmaEliminar vce = new View_ConfirmaEliminar(idUsuario, idReg, "CentroAcademico") ;
                vce.ShowDialog();
                Limpiar();
                return;
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
