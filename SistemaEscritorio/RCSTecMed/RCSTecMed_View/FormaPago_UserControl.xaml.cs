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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RCSTecMed_View
{
    /// <summary>
    /// Lógica de interacción para FormaPago_UserControl.xaml
    /// </summary>
    public partial class FormaPago_UserControl : UserControl
    {
        private readonly MostrarDatos_Controll md = new MostrarDatos_Controll();
        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private readonly Mensajes_Controll msc = new Mensajes_Controll();
        private readonly Controll_FORMAPAGO fp = new Controll_FORMAPAGO();

        private int IdUsuario {  get; set; }
        public FormaPago_UserControl(int IdUser)
        {
            InitializeComponent();
            IdUsuario = IdUser;

            Limpiar();
        }

        //METODOS VARIOS
        private void Limpiar()
        {
            LB_TotalRegistrados.Content = md.TotalFormaPagoRegistrado().ToString();

            TXT_IdFormaPago.Text = string.Empty;
            TXT_FormaPago.Text = string.Empty;

            CargarGrillaFormaPago();

            TXT_IdFormaPago.Focus();
        }

        private void ResetIdTexto()
        {
            TXT_IdFormaPago.Text = string.Empty;
            TXT_IdFormaPago.Focus();
        }

        private void ResetDescripcionTexto()
        {
            TXT_FormaPago.Text = string.Empty;
            TXT_FormaPago.Focus();
        }

        private void ValidarIdFormaPago(string campo)
        {
            if (val.CampoVacio(campo))
            {
                MessageBoxResult resp = MessageBox.Show("Desea Asignar un Número de Registro", "RCSTecMed - Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (resp)
                {
                    case MessageBoxResult.Yes:
                        TXT_IdFormaPago.Text = fp.AsignarId().ToString();
                        break;

                    case MessageBoxResult.No:
                        TXT_FormaPago.Focus();
                        break;
                }
            }
            else
            {
                if (val.CampoSoloNumero(campo))
                {
                    TXT_FormaPago.Focus();
                }
                else
                {
                    msc.MostrarError("Debe Ingresar Solo Números");
                    ResetIdTexto();
                }
            }
        }

        private void ValidarNombreFormaPago(string campo)
        {
            if (val.CampoVacio(campo))
            {
                msc.MostrarError("El campo no puede estar vacío.");
                TXT_FormaPago.Focus();
                return;
            }

            int largo = val.LargoCampo(campo);
            if ((largo > 0 && largo < 10) || largo > 50)
            {
                msc.MostrarError($"La descripción ingresada tiene {largo} caracteres.\nDebe tener entre 10 y 50.");
                ResetDescripcionTexto();
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
        private void TXT_IdFormaPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_IdFormaPago.Text;
                ValidarIdFormaPago(campo);
            }
        }

        private void TXT_FormaPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_FormaPago.Text;
                ValidarNombreFormaPago(campo);
            }
        }

        //ACCIONES EN GRILLA
        private void CargarGrillaFormaPago()
        {
            try
            {
                DG_FormaPago.ItemsSource = fp.ListaFormaPagoOrdenadoDesc();
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void DG_FormaPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG_FormaPago.SelectedItems.Count == 0)
                return;

            if (DG_FormaPago.SelectedItems[0] is Controll_FORMAPAGO fpg)
            {
                TXT_IdFormaPago.Text = fpg.IdFormaPagoCuota.ToString();
                TXT_FormaPago.Text = fpg.DescripcionFormaPagoCuota;
            }

        }

        private void GrillaFormaPagoPorId(int id)
        {
            try
            {
                DG_FormaPago.ItemsSource = fp.ListaFormaPagoPorId(id);
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void GrillaFormaPagoPorDescripcion(string desc)
        {
            try
            {
                DG_FormaPago.ItemsSource = fp.ListaFormaPagoPorDesc(desc);
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        //BOTONES DE ACCION
        //BOTONES DE ACCION: BUSCAR
        private void MostrarMensajeBusquedaInvalida()
        {
            msc.MostrarError("Para Buscar tiene las siguientes opciones:" +
                "\n1. Ingresar un Número de Registro y hacer click en botón BUSCAR" +
                "\n2. Ingresar una Descripción de Forma de Pago y hacer click en botón BUSCAR" +
                "\n3. Seleccionar un Registro desde el Listado de la Grilla");
        }

        private void CargarDatosFormaPago(Controll_FORMAPAGO fpg)
        {
            TXT_IdFormaPago.Text = fpg.IdFormaPagoCuota.ToString();
            TXT_FormaPago.Text = fpg.DescripcionFormaPagoCuota;
        }

        private void BTN_Buscar_Click(object sender, RoutedEventArgs e)
        {
            string campoId = TXT_IdFormaPago.Text;
            string campoDesc = TXT_FormaPago.Text;

            if (campoId.Length == 0 && campoDesc.Length == 0 || campoId.Length > 0 && campoDesc.Length > 0)
            {
                MostrarMensajeBusquedaInvalida();
                TXT_IdFormaPago.Focus();
                return;
            }

            if (campoId.Length > 0)
            {
                if (int.TryParse(campoId, out int id))
                {
                    fp.IdFormaPagoCuota = id;
                    if (fp.ReadId())
                    {
                        msc.MostrarInformacion("Registro Encontrado");
                        CargarDatosFormaPago(fp);
                        GrillaFormaPagoPorId(id);
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
            else if (campoDesc.Length > 0)
            {
                fp.DescripcionFormaPagoCuota = campoDesc;
                if (fp.ReadDesc())
                {
                    msc.MostrarInformacion("Registro Encontrado");
                    CargarDatosFormaPago(fp);
                    GrillaFormaPagoPorDescripcion(campoDesc);
                }
                else
                {
                    msc.MostrarError("Registro No Encontrado\nComunicarse con Administrador");
                    ResetIdTexto();
                }
            }

            TXT_IdFormaPago.Focus();

        }

        //BOTONES DE ACCION: GRABAR Y ACTUALIZAR
        private bool ValidarEntradas()
        {
            bool hayCampoVacio = val.CampoVacio(TXT_IdFormaPago.Text) || val.CampoVacio(TXT_FormaPago.Text);

            if (hayCampoVacio)
            {
                msc.MostrarError("Debe completar todos los campos o Seleccionar un Registro");
                TXT_IdFormaPago.Focus();
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

            fp.IdFormaPagoCuota = int.Parse(TXT_IdFormaPago.Text);
            if (fp.ReadId())
            {
                MostrarErrorYFoco("Número de Registro ya existe en la base de datos\nRevisar datos ingresados", TXT_IdFormaPago);
                return;
            }

            fp.DescripcionFormaPagoCuota = TXT_FormaPago.Text.ToUpper();
            if (fp.ReadDesc())
            {
                MostrarErrorYFoco("Nombre de Centro Académico ya existe en la base de datos\nRevisar datos ingresados", TXT_FormaPago);
                return;
            }

            fp.IdFormaPagoCuota = int.Parse(TXT_IdFormaPago.Text);
            fp.DescripcionFormaPagoCuota = TXT_FormaPago.Text.ToUpper();
            fp.IdUsuario = IdUsuario;

            if (!fp.Create())
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

            fp.IdFormaPagoCuota = int.Parse(TXT_IdFormaPago.Text);
            if (!fp.ReadId())
            {
                MostrarErrorYFoco("No se encontró el registro a actualizar\nVerifique el Número de Registro ingresado", TXT_IdFormaPago);
                return;
            }

            if (!Confirmacion("¿Desea actualizar este registro?"))
                return;

            fp.IdFormaPagoCuota = int.Parse(TXT_IdFormaPago.Text);
            fp.DescripcionFormaPagoCuota = TXT_FormaPago.Text.ToUpper();
            fp.IdUsuario = IdUsuario;
            if (!fp.Update())
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
            if (!ValidarEntradas())
                return;

            fp.IdFormaPagoCuota = int.Parse(TXT_IdFormaPago.Text);
            if (!fp.ReadId())
            {
                MostrarErrorYFoco("No se encontró el registro a eliminar\nVerifique el Número de Registro ingresado", TXT_IdFormaPago);
                return;
            }

            if (Confirmacion("¿Desea Eliminar este registro?"))
            {
                string idReg = TXT_IdFormaPago.Text;
                View_ConfirmaEliminar vce = new View_ConfirmaEliminar(IdUsuario, idReg, "FormaPago");
                vce.ShowDialog();
                Limpiar();
                return;
            }

            Limpiar();

        }

        //BOTON ACCION: LIMPIAR
        private void BTN_Limpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        //BOTON ACCION: HOME
        private void BTN_Home_Click(object sender, RoutedEventArgs e)
        {
            // Buscar el TabControl en la ventana contenedora
            if (Window.GetWindow(this) is View_CompletarDatosRegistroSocio mdSec)
            {
                // Activar la pestaña "Home"
                mdSec.MainTabControl.SelectedItem = mdSec.TAB_Home;
            }


        }
    }
}
