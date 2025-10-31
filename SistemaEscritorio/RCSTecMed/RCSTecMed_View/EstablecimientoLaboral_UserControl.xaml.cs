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

            LimpiarGrilla();

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
                TXT_Telefono.Focus();
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
        private void LimpiarGrilla()
        {
            try
            {
                DG_Establecimiento.ItemsSource = est.ListaLimpiarGrilla();
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

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

        private void GrillaEstablecimientoLaboralId(int id)
        {
            try
            {
                DG_Establecimiento.ItemsSource = est.ListaEstablecimientoPorId(id);
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void GrillaEstablecimientoLaboralDescripcion(string desc)
        {
            try
            {
                DG_Establecimiento.ItemsSource = est.ListaEstablecimientoPorDescripcion(desc);
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void GrillaEstablecimientoLaboralRegion(int id)
        {
            try
            {
                DG_Establecimiento.ItemsSource = est.ListaEstablecimientoPorRegion(id);
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void GrillaEstablecimientoLaboralComuna(string com)
        {
            try
            {
                DG_Establecimiento.ItemsSource = est.ListaEstablecimientoPorComuna(com);
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void GrillaEstablecimientoLaboralRegionComuna(int id, string com)
        {
            try
            {
                DG_Establecimiento.ItemsSource = est.ListaEstablecimientoPorRegionComuna(id, com);
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void DG_Establecimiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG_Establecimiento.SelectedItems.Count == 0)
                return;

            var el = DG_Establecimiento.SelectedItems[0] as Controll_ESTABLECIMIENTO;
            if (el == null)
                return;

            TXT_IdEstablecimiento.Text = el.IdEstablecimiento.ToString();
            TXT_NombreEstablecimiento.Text = el.NombreEstablecimiento;
            CB_Region.SelectedValue = el.Region;
            CB_Comuna.SelectedValue = el.IdComuna;
            TXT_Direccion.Text = el.Direccion;
            TXT_Telefono.Text = el.Telefono.ToString();
            TXT_Email.Text = el.Email;
            TXT_Contacto.Text = el.NombreContacto;
        }

        //BOTONES DE ACCION
        //BOTON: BUSCAR
        private void MostrarMensajeBusquedaInvalida()
        {
            msc.MostrarWarning("Para Buscar tiene las siguientes opciones:" +
                "\n1. Ingresar un Número de Registro y hacer click en botón BUSCAR" +
                "\n2. Ingresar un Nombre de Centro Laboral y hacer click en botón BUSCAR" +
                "\n3. Seleccionar una Región del listado Desplegable y hacer click en BUSCAR" +
                "\n4. Seleccionar una Región y una Comuna y hacer click en Buscar");
        }

        private bool CamposVaciosBusqueda()
        {
            return val.CampoVacio(TXT_IdEstablecimiento.Text) 
                && val.CampoVacio(TXT_NombreEstablecimiento.Text)
                && CB_Region.SelectedIndex == -1
                && CB_Comuna.SelectedIndex == -1;
        }

        private bool CampoIdBusqueda()
        {
            return !val.CampoVacio(TXT_IdEstablecimiento.Text)
                && val.CampoVacio(TXT_NombreEstablecimiento.Text)
                && CB_Region.SelectedIndex == -1
                && CB_Comuna.SelectedIndex == -1;
        }

        private void MostrarId(int id)
        {
            est.IdEstablecimiento = id;

            if (!est.ReadId())
            {
                DG_Establecimiento.IsEnabled = false;
                msc.MostrarError("Registro no encontrado en Base de Datos");
                return;
            }

            msc.MostrarInformacion("Registro encontrado. Se desplegarán los campos.");

            // Asignación de campos
            TXT_IdEstablecimiento.Text = est.IdEstablecimiento.ToString();
            TXT_NombreEstablecimiento.Text = est.NombreEstablecimiento;
            CB_Region.SelectedValue = est.Region;
            CB_Comuna.SelectedValue = est.IdComuna;
            TXT_Direccion.Text = est.Direccion;
            TXT_Telefono.Text = est.Telefono.ToString();
            TXT_Email.Text = est.Email;
            TXT_Contacto.Text = est.NombreContacto;

            DG_Establecimiento.IsEnabled = true;
            GrillaEstablecimientoLaboralId(id);
        }

        private bool CampoNombreBusqueda()
        {
            return val.CampoVacio(TXT_IdEstablecimiento.Text)
                && !val.CampoVacio(TXT_NombreEstablecimiento.Text)
                && CB_Region.SelectedIndex == -1
                && CB_Comuna.SelectedIndex == -1;
        }

        private void MostrarNombre(string nombre)
        {
            est.NombreEstablecimiento = nombre;

            if (!est.ReadEstablecimiento())
            {
                DG_Establecimiento.IsEnabled = false;
                msc.MostrarError("Registro no encontrado en Base de Datos");
                return;
            }

            msc.MostrarInformacion("Registro encontrado. Se desplegarán los campos.");

            // Asignación de campos
            TXT_IdEstablecimiento.Text = est.IdEstablecimiento.ToString();
            TXT_NombreEstablecimiento.Text = est.NombreEstablecimiento;
            CB_Region.SelectedValue = est.Region;
            CB_Comuna.SelectedValue = est.IdComuna;
            TXT_Direccion.Text = est.Direccion;
            TXT_Telefono.Text = est.Telefono.ToString();
            TXT_Email.Text = est.Email;
            TXT_Contacto.Text = est.NombreContacto;

            DG_Establecimiento.IsEnabled = true;
            GrillaEstablecimientoLaboralDescripcion(nombre);
        }

        private bool CampoRegionBusqueda()
        {
            return val.CampoVacio(TXT_IdEstablecimiento.Text)
                && val.CampoVacio(TXT_NombreEstablecimiento.Text)
                && CB_Region.SelectedIndex != -1
                && CB_Comuna.SelectedIndex == -1;
        }

        private void MostrarRegion(int id)
        {
            int cantidad = md.TotalEstablecimientosRegistradosPorRegion(id);
            msc.MostrarInformacion($"Se desplegaran {cantidad} Registros encontrados en Grilla.\n" +
                "Fabor selccionar del listado.");

            LB_TotalRegistrados.Content = cantidad.ToString();
            DG_Establecimiento.IsEnabled = true;
            GrillaEstablecimientoLaboralRegion(id);
        }

        private bool CampoRegioncomuaBusqueda()
        {
            return val.CampoVacio(TXT_IdEstablecimiento.Text)
                && val.CampoVacio(TXT_NombreEstablecimiento.Text)
                && CB_Region.SelectedIndex != -1
                && CB_Comuna.SelectedIndex != -1;
        }

        private void MostrarRegionComuna(string com)
        {
            int cantidad = md.TotalEstablecimientosRegistradosPorRegionComuna(com);
            msc.MostrarInformacion($"Se desplegarán Registros {cantidad} encontrados en Grilla.\n" +
                "Fabor seleccionar del listado.");

            LB_TotalRegistrados.Content = cantidad.ToString();
            DG_Establecimiento.IsEnabled = true;
            GrillaEstablecimientoLaboralComuna(com);
        }

        private void BTN_Buscar_Click(object sender, RoutedEventArgs e)
        {
            if (CamposVaciosBusqueda())
            {
                if(Confirmacion("Se desplegara el listado completo de registros de Establecimientos Laborales" +
                    "\nDesea continuar"))
                {
                    DG_Establecimiento.IsEnabled = true;
                    GrillaEstablecimientoLaboralCompleta();
                }
                else
                {
                    MostrarMensajeBusquedaInvalida();
                    DG_Establecimiento.IsEnabled = false;
                    TXT_IdEstablecimiento.Focus();
                }
            }
            
            if (CampoIdBusqueda())
            {
                int campoId = int.Parse(TXT_IdEstablecimiento.Text);
                MostrarId(campoId);
                return;
            }

            if (CampoNombreBusqueda())
            {
                string campoNombre = TXT_NombreEstablecimiento.Text;
                MostrarNombre(campoNombre);
                return;
            }

            if (CampoRegionBusqueda())
            {
                int cbRegion = (int)CB_Region.SelectedValue;
                MostrarRegion(cbRegion);
                return;
            }

            if (CampoRegioncomuaBusqueda())
            {
                int cbRegion = (int)CB_Region.SelectedValue;
                string cbComuna = (string)CB_Comuna.SelectedValue;

                MostrarRegionComuna(cbComuna);
                return;
            }
        }

        //BOTON: GRABAR Y ACTUALIZAR
        private bool ValidarEntradas()
        {
            bool hayCampoVavio = val.CampoVacio(TXT_IdEstablecimiento.Text)
                || val.CampoVacio(TXT_NombreEstablecimiento.Text)
                || CB_Region.SelectedIndex == -1
                || CB_Comuna.SelectedIndex == -1
                || val.CampoVacio(TXT_Direccion.Text)
                || val.CampoVacio(TXT_Telefono.Text)
                || val.CampoVacio(TXT_Email.Text)
                || val.CampoVacio(TXT_Contacto.Text);

            if (hayCampoVavio)
            {
                msc.MostrarError("Todos los campos son obligatorios\n" +
                    "Si no cuenta con datos como Dirección completar con NO HAY REGISTRO\n" +
                    "Telefono completar con 0\n" +
                    "Email completar con REGISTRAR@EMAIL.XX\n" +
                    "Contacto completar con NO HAY REGISTRO");
                TXT_IdEstablecimiento.Focus();
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

            est.IdEstablecimiento = int.Parse(TXT_IdEstablecimiento.Text);            
            if (est.ReadId())
            {
                MostrarErrorYFoco("Número de Registro ya existe en la base de datos\nRevisar datos ingresados", TXT_IdEstablecimiento);
                return;
            }

            est.NombreEstablecimiento = TXT_NombreEstablecimiento.Text.ToUpper();
            if (est.ReadEstablecimiento())
            {
                MostrarErrorYFoco("Nombre de Establecimiento Laboral ya existe en la base de datos\nRevisar datos ingresados", TXT_NombreEstablecimiento);
                return;
            }

            est.IdEstablecimiento = int.Parse(TXT_IdEstablecimiento.Text);
            est.NombreEstablecimiento = TXT_NombreEstablecimiento.Text.ToUpper();
            est.IdComuna = (string)CB_Comuna.SelectedValue;
            est.Direccion = TXT_Direccion.Text.ToUpper();
            est.Telefono = int.Parse(TXT_Telefono.Text);
            est.Email = TXT_Email.Text.ToUpper();
            est.NombreContacto = TXT_Contacto.Text.ToUpper();
            est.IdUsuario = idUsuario;
            est.Region = (int)CB_Region.SelectedValue;

            if (!est.Create())
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

            est.IdEstablecimiento = int.Parse(TXT_IdEstablecimiento.Text);
            if (!est.ReadId())
            {
                MostrarErrorYFoco("No se encontró el registro a actualizar\nVerifique el Número de Registro ingresado", TXT_IdEstablecimiento);
                return;
            }


            if (!Confirmacion("Desea Actualizar este Registro?"))
                return;

            est.IdEstablecimiento = int.Parse(TXT_IdEstablecimiento.Text);
            est.NombreEstablecimiento = TXT_NombreEstablecimiento.Text.ToUpper();
            est.IdComuna = (string)CB_Comuna.SelectedValue;
            est.Direccion = TXT_Direccion.Text.ToUpper();
            est.Telefono = int.Parse(TXT_Telefono.Text);
            est.Email = TXT_Email.Text.ToUpper();
            est.NombreContacto = TXT_Contacto.Text.ToUpper();
            est.IdUsuario = idUsuario;
            est.Region = (int)CB_Region.SelectedValue;

            if (!est.Update())
            {
                msc.MostrarError("No se logró realizar la Actualización del registro en la base de datos\nComunicarse con el administrador");
                Limpiar();
                return;
            }

            msc.MostrarInformacion("Actualización de Registro exitoso en la base de datos");
            Limpiar();
        }

        //BOTON: ELIMINAR
        private void BTN_Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarEntradas())
                return;

            est.IdEstablecimiento = int.Parse(TXT_IdEstablecimiento.Text);
            if (!est.ReadId())
            {
                MostrarErrorYFoco("No se encontró el registro a eliminar\nVerifique el Número de Registro ingresado", TXT_IdEstablecimiento);
                return;
            }

            if (Confirmacion("¿Desea Eliminar este registro?"))
            {
                string idReg = TXT_IdEstablecimiento.Text;
                View_ConfirmaEliminar vce = new View_ConfirmaEliminar(idUsuario, idReg, "Establecimiento");
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
