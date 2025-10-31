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
    /// Lógica de interacción para EstaimadoPago_UserControl.xaml
    /// </summary>
    public partial class EstaimadoPago_UserControl : UserControl
    {
        private readonly MostrarDatos_Controll md = new MostrarDatos_Controll();
        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private readonly Mensajes_Controll msc = new Mensajes_Controll();
        private readonly Controll_ESTIMADOPAGO ep = new Controll_ESTIMADOPAGO();

        private int IdUsuario { get; set; }

        public EstaimadoPago_UserControl(int IdUser)
        {
            InitializeComponent();

            IdUsuario = IdUser;

            Limpiar();
        }

        //METODOS VARIOS
        private void Limpiar()
        {
            LB_TotalRegistrados.Content = md.TotalFechaEstimaPagoRegistrado().ToString();

            TXT_IdFechaPago.Text = string.Empty;
            TXT_FechaPagoCuota.Text = string.Empty;

            CargarGrillaEstimadoPago();

            TXT_IdFechaPago.Focus();
        }

        private void ResetIdTexto()
        {
            TXT_IdFechaPago.Text = string.Empty;
            TXT_IdFechaPago.Focus();
        }

        private void ResetDescripcionTexto()
        {
            TXT_FechaPagoCuota.Text = string.Empty;
            TXT_FechaPagoCuota.Focus();
        }

        private void ValidarIdEstimadoPago(string campo)
        {
            if (val.CampoVacio(campo))
            {
                MessageBoxResult resp = MessageBox.Show("Desea Asignar un Número de Registro", "RCSTecMed - Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (resp)
                {
                    case MessageBoxResult.Yes:
                        TXT_IdFechaPago.Text = ep.AsignarId().ToString();
                        break;

                    case MessageBoxResult.No:
                        TXT_FechaPagoCuota.Focus();
                        break;
                }
            }
            else
            {
                if (val.CampoSoloNumero(campo))
                {
                    TXT_FechaPagoCuota.Focus();
                }
                else
                {
                    msc.MostrarError("Debe Ingresar Solo Números");
                    ResetIdTexto();
                }
            }
        }

        private void ValidarNombreEstimadoPago(string campo)
        {
            if (val.CampoVacio(campo))
            {
                msc.MostrarError("El campo no puede estar vacío.");
                TXT_FechaPagoCuota.Focus();
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
        private void TXT_IdFechaPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_IdFechaPago.Text;
                ValidarIdEstimadoPago(campo);
            }
        }

        private void TXT_FechaPagoCuota_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_FechaPagoCuota.Text;
                ValidarNombreEstimadoPago(campo);
            }
        }

        //ACCIONES EN GRILLA
        private void CargarGrillaEstimadoPago()
        {
            try
            {
                DG_FechaPagoCuota.ItemsSource = ep.ListaEstimadoPagoOrdenado();
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void DG_FechaPagoCuota_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG_FechaPagoCuota.SelectedItems.Count == 0)
                return;

            if (DG_FechaPagoCuota.SelectedItems[0] is Controll_ESTIMADOPAGO epc)
            {
                TXT_IdFechaPago.Text = epc.IdEstimadoPago.ToString();
                TXT_FechaPagoCuota.Text = epc.DescripcionEstimadoPago;
            }

        }

        private void GrillaEstimadoPagoPorId(int id)
        {
            try
            {
                DG_FechaPagoCuota.ItemsSource = ep.ListaEstimadoPagoId(id);
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void GrillaEstimadoPagoPorDescripcion(string desc)
        {
            try
            {
                DG_FechaPagoCuota.ItemsSource = ep.ListaEstimadoPagoDesc(desc);
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
                "\n2. Ingresar una Descripción de Fecha Estimada de Pago y hacer click en botón BUSCAR" +
                "\n3. Seleccionar un Registro desde el Listado de la Grilla");
        }

        private void CargarDatosEstimadoPago(Controll_ESTIMADOPAGO epc)
        {
            TXT_IdFechaPago.Text = epc.IdEstimadoPago.ToString();
            TXT_FechaPagoCuota.Text = epc.DescripcionEstimadoPago;
        }

        private void BTN_Buscar_Click(object sender, RoutedEventArgs e)
        {
            string campoId = TXT_IdFechaPago.Text;
            string campoDesc = TXT_FechaPagoCuota.Text;

            if (campoId.Length == 0 && campoDesc.Length == 0 || campoId.Length > 0 && campoDesc.Length > 0)
            {
                MostrarMensajeBusquedaInvalida();
                TXT_IdFechaPago.Focus();
                return;
            }

            if (campoId.Length > 0)
            {
                if (int.TryParse(campoId, out int id))
                {
                    ep.IdEstimadoPago = id;
                    if (ep.ReadId())
                    {
                        msc.MostrarInformacion("Registro Encontrado");
                        CargarDatosEstimadoPago(ep);
                        GrillaEstimadoPagoPorId(id);
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
                ep.DescripcionEstimadoPago = campoDesc;
                if (ep.ReadDesc())
                {
                    msc.MostrarInformacion("Registro Encontrado");
                    CargarDatosEstimadoPago(ep);
                    GrillaEstimadoPagoPorDescripcion(campoDesc);
                }
                else
                {
                    msc.MostrarError("Registro No Encontrado\nComunicarse con Administrador");
                    ResetIdTexto();
                }
            }

            TXT_IdFechaPago.Focus();

        }

        //BOTONES DE ACCION: GRABAR Y ACTUALIZAR
        private bool ValidarEntradas()
        {
            bool hayCampoVacio = val.CampoVacio(TXT_IdFechaPago.Text) || val.CampoVacio(TXT_FechaPagoCuota.Text);

            if (hayCampoVacio)
            {
                msc.MostrarError("Debe completar todos los campos o Seleccionar un Registro");
                TXT_IdFechaPago.Focus();
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

            ep.IdEstimadoPago = int.Parse(TXT_IdFechaPago.Text);
            if (ep.ReadId())
            {
                MostrarErrorYFoco("Número de Registro ya existe en la base de datos\nRevisar datos ingresados", TXT_IdFechaPago);
                return;
            }

            ep.DescripcionEstimadoPago = TXT_FechaPagoCuota.Text.ToUpper();
            if (ep.ReadDesc())
            {
                MostrarErrorYFoco("Nombre de Centro Académico ya existe en la base de datos\nRevisar datos ingresados", TXT_FechaPagoCuota);
                return;
            }

            ep.IdEstimadoPago = int.Parse(TXT_IdFechaPago.Text);
            ep.DescripcionEstimadoPago = TXT_FechaPagoCuota.Text.ToUpper();
            ep.IdUsuario = IdUsuario;

            if (!ep.Create())
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

            ep.IdEstimadoPago = int.Parse(TXT_IdFechaPago.Text);
            if (!ep.ReadId())
            {
                MostrarErrorYFoco("No se encontró el registro a actualizar\nVerifique el Número de Registro ingresado", TXT_IdFechaPago);
                return;
            }

            if (!Confirmacion("¿Desea actualizar este registro?"))
                return;

            ep.IdEstimadoPago = int.Parse(TXT_IdFechaPago.Text);
            ep.DescripcionEstimadoPago = TXT_FechaPagoCuota.Text.ToUpper();
            ep.IdUsuario = IdUsuario;
            if (!ep.Update())
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

            ep.IdEstimadoPago = int.Parse(TXT_IdFechaPago.Text);
            if (!ep.ReadId())
            {
                MostrarErrorYFoco("No se encontró el registro a eliminar\nVerifique el Número de Registro ingresado", TXT_IdFechaPago);
                return;
            }

            if (Confirmacion("¿Desea Eliminar este registro?"))
            {
                string idReg = TXT_IdFechaPago.Text;
                View_ConfirmaEliminar vce = new View_ConfirmaEliminar(IdUsuario, idReg, "EstimadoPago");
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
