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
    /// Lógica de interacción para NombreCertificacion_UserControl.xaml
    /// </summary>
    public partial class NombreCertificacion_UserControl : UserControl
    {
        private readonly MostrarDatos_Controll md = new MostrarDatos_Controll();
        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private readonly Mensajes_Controll msc = new Mensajes_Controll();
        private readonly Controll_CERTIFICACIONES cer = new Controll_CERTIFICACIONES();

        private int IdUsuario { get; set; }

        public NombreCertificacion_UserControl(int idUser)
        {
            InitializeComponent();
            IdUsuario = idUser;

            Limpiar();
        }

        //METODOS VARIOS
        private void Limpiar()
        {
            LB_TotalRegistrados.Content = md.TotalNombreCertificaciones().ToString();

            TXT_IdNombreCertificacion.Text = string.Empty;
            TXT_NombreCertificacion.Text = string.Empty;

            CargarGrillaNombreCertificacion();

            TXT_IdNombreCertificacion.Focus();
        }

        private void ResetIdTexto()
        {
            TXT_IdNombreCertificacion.Text = string.Empty;
            TXT_IdNombreCertificacion.Focus();
        }

        private void ResetDescripcionTexto()
        {
            TXT_NombreCertificacion.Text = string.Empty;
            TXT_NombreCertificacion.Focus();
        }

        private void ValidarIdNombreCertificacion(string campo)
        {
            if (val.CampoVacio(campo))
            {
                MessageBoxResult resp = MessageBox.Show("Desea Asignar un Número de Registro", "RCSTecMed - Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (resp)
                {
                    case MessageBoxResult.Yes:
                        TXT_IdNombreCertificacion.Text = cer.AsignarId().ToString();
                        break;

                    case MessageBoxResult.No:
                        TXT_IdNombreCertificacion.Focus();
                        break;
                }
            }
            else
            {
                if (val.CampoSoloNumero(campo))
                {
                    TXT_NombreCertificacion.Focus();
                }
                else
                {
                    msc.MostrarError("Debe Ingresar Solo Números");
                    ResetIdTexto();
                }
            }
        }

        private void ValidarNombreCertificacion(string campo)
        {
            if (val.CampoVacio(campo))
            {
                msc.MostrarError("El campo no puede estar vacío.");
                TXT_NombreCertificacion.Focus();
                return;
            }

            int largo = val.LargoCampo(campo);
            if ((largo > 0 && largo < 5) || largo > 30)
            {
                msc.MostrarError($"La descripción ingresada tiene {largo} caracteres.\nDebe tener entre 5 y 30.");
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
        private void TXT_IdNombreCertificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_IdNombreCertificacion.Text;
                ValidarIdNombreCertificacion(campo);
            }
        }

        private void TXT_NombreCertificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string campo = TXT_NombreCertificacion.Text;
                ValidarNombreCertificacion(campo);
            }
        }

        //ACCIONES EN GRILLA
        private void CargarGrillaNombreCertificacion()
        {
            try
            {
                DG_NombreCertificacion.ItemsSource = cer.ListaNombreCertificacionesOrdenadaDescrpcion();
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void DG_NombreCertificacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG_NombreCertificacion.SelectedItems.Count == 0)
                return;

            if (DG_NombreCertificacion.SelectedItems[0] is Controll_CERTIFICACIONES nc)
            {
                TXT_IdNombreCertificacion.Text = nc.IdCertificaciones.ToString();
                TXT_NombreCertificacion.Text = nc.NombreCertificacion;
            }
        }

        private void GrillaNombreCertificacionPorId(int id)
        {
            try
            {
                DG_NombreCertificacion.ItemsSource = cer.ListaNombreCertificacionporId(id);
            }
            catch (Exception ex)
            {
                msc.MostrarError($"Error al Cargar Datos de Grilla\nDebido a: {ex.Message}");
            }
        }

        private void GrillaNombreCertificacionPorDescripcion(string desc)
        {
            try
            {
                DG_NombreCertificacion.ItemsSource = cer.ListaNombreCertificacionporDescripcion(desc);
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
                "\n2. Ingresar un Nombre de Certificación y hacer click en botón BUSCAR" +
                "\n3. Seleccionar un Registro desde el Listado de la Grilla");
        }

        private void CargarDatosNombreCertificacion(Controll_CERTIFICACIONES nc)
        {
            TXT_IdNombreCertificacion.Text = nc.IdCertificaciones.ToString();
            TXT_NombreCertificacion.Text = nc.NombreCertificacion;
        }

        private void BTN_Buscar_Click(object sender, RoutedEventArgs e)
        {
            string campoId = TXT_IdNombreCertificacion.Text;
            string campoDesc = TXT_NombreCertificacion.Text;

            if (campoId.Length == 0 && campoDesc.Length == 0 || campoId.Length > 0 && campoDesc.Length > 0)
            {
                MostrarMensajeBusquedaInvalida();
                TXT_IdNombreCertificacion.Focus();
                return;
            }

            if (campoId.Length > 0)
            {
                if (int.TryParse(campoId, out int id))
                {
                    cer.IdCertificaciones = id;
                    if (cer.ReadId())
                    {
                        msc.MostrarInformacion("Registro Encontrado");
                        CargarDatosNombreCertificacion(cer);
                        GrillaNombreCertificacionPorId(id);
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
                cer.NombreCertificacion = campoDesc;
                if (cer.ReadDesc())
                {
                    msc.MostrarInformacion("Registro Encontrado");
                    CargarDatosNombreCertificacion(cer);
                    GrillaNombreCertificacionPorDescripcion(campoDesc);
                }
                else
                {
                    msc.MostrarError("Registro No Encontrado\nComunicarse con Administrador");
                    ResetIdTexto();
                }
            }

            TXT_IdNombreCertificacion.Focus();

        }

        //BOTONES DE ACCION: GRABAR Y ACTUALIZAR
        private bool ValidarEntradas()
        {
            bool hayCampoVacio = val.CampoVacio(TXT_IdNombreCertificacion.Text) || val.CampoVacio(TXT_NombreCertificacion.Text);

            if (hayCampoVacio)
            {
                msc.MostrarError("Debe completar todos los campos o Seleccionar un Registro");
                TXT_IdNombreCertificacion.Focus();
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
            cer.IdCertificaciones = int.Parse(TXT_IdNombreCertificacion.Text);
            cer.NombreCertificacion = TXT_NombreCertificacion.Text.ToUpper();
            cer.IdUsuario = IdUsuario;

            if (!ValidarEntradas())
                return;

            if (cer.ReadId())
            {
                MostrarErrorYFoco("Número de Registro ya existe en la base de datos\nRevisar datos ingresados", TXT_IdNombreCertificacion);
                return;
            }

            if (cer.ReadDesc())
            {
                MostrarErrorYFoco("Nombre de Centro Académico ya existe en la base de datos\nRevisar datos ingresados", TXT_NombreCertificacion);
                return;
            }

            if (!cer.Create())
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

            cer.IdCertificaciones = int.Parse(TXT_IdNombreCertificacion.Text);
            if (!cer.ReadId())
            {
                MostrarErrorYFoco("No se encontró el registro a actualizar\nVerifique el Número de Registro ingresado", TXT_IdNombreCertificacion);
                return;
            }

            if (!Confirmacion("¿Desea actualizar este registro?"))
                return;

            cer.IdCertificaciones = int.Parse(TXT_IdNombreCertificacion.Text);
            cer.NombreCertificacion = TXT_NombreCertificacion.Text.ToUpper();
            cer.IdUsuario = IdUsuario;

            if (!cer.Update())
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

            cer.IdCertificaciones = int.Parse(TXT_IdNombreCertificacion.Text);
            if (!cer.ReadId())
            {
                MostrarErrorYFoco("No se encontró el registro a eliminar\nVerifique el Número de Registro ingresado", TXT_IdNombreCertificacion);
                return;
            }

            if (Confirmacion("¿Desea Eliminar este registro?"))
            {
                string idReg = TXT_IdNombreCertificacion.Text;
                View_ConfirmaEliminar vce = new View_ConfirmaEliminar(IdUsuario, idReg, "NombreCertificacion");
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
