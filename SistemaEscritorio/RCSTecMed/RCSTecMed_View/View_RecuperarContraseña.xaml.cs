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
using System.Windows.Shapes;
using RCSTecMed_Controll;

namespace RCSTecMed_View
{
    /// <summary>
    /// Lógica de interacción para View_RecuperarContraseña.xaml
    /// </summary>
    public partial class View_RecuperarContraseña : Window
    {
        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private readonly Controll_USUARIO cu = new Controll_USUARIO();
        public View_RecuperarContraseña()
        {
            InitializeComponent();
            Limpiar();
        }

        private void Limpiar()
        {
            TXT_Rut.Text = string.Empty;
            TXT_Dv.Text = string.Empty;
            TXT_Rut.Focus();
            BTN_Recuperar.IsEnabled = false;
        }

        private void MostrarError(string mensaje) //MUESTRA MENSAJES DE ERROR SEGUN SO CONTEXTO
        {
            MessageBox.Show(mensaje, "RCSTecMed - Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MostrarInformacion(string mensaje)
        {
            MessageBox.Show(mensaje, "RCSTecMed - Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TXT_Rut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Tab)
                return;

            string rut = TXT_Rut.Text.Trim();

            if (val.CampoVacio(rut))
            {
                MostrarError("Debe ingresar RUT");
                TXT_Rut.Focus();
                return;
            }

            if (val.EsRutNumericoValido(rut))
            {
                TXT_Dv.Focus();
            }
            else
            {
                MostrarError("RUT debe ser numérico y no superar los 8 dígitos");
                Limpiar();
            }

        }

        private void TXT_Dv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Tab)
                return;

            string rut = TXT_Rut.Text.Trim();
            string dv = TXT_Dv.Text.Trim().ToUpper();

            if (val.CampoVacio(rut))
            {
                MostrarError("Debe ingresar un RUT");
                TXT_Rut.Focus();
                return;
            }

            if (val.CampoVacio(dv))
            {
                MostrarError("Debe ingresar un DV");
                TXT_Dv.Focus();
                return;
            }

            if (!val.EsFormatoDvValido(dv))
            {
                MostrarError("Formato de DV incorrecto");
                TXT_Dv.Text = string.Empty;
                TXT_Dv.Focus();
                return;
            }

            if (!int.TryParse(rut, out int rutval))
            {
                MostrarError("RUT inválido. Debe contener solo números.");
                TXT_Rut.Focus();
                return;
            }

            if (val.CalcularDV(rutval) != dv)
            {
                MostrarError("RUT y/o DV incorrecto\nVolver a ingresar");
                Limpiar();
                return;
            }

            Controll_ESTADOUSUARIO eu = new Controll_ESTADOUSUARIO();
            cu.Rut = rutval;
            if (cu.ReadRutUsuario())
            {
                if (cu.IdEstado == 1)
                {
                    
                    eu.IdEstado = cu.IdEstado;
                    if (eu.ReadId())
                    {                        
                        MostrarInformacion($"Búsqueda exitosa\nRUT encontrado en BD, Estado {eu.EstadoUsuario}");
                        BTN_Recuperar.IsEnabled = true;
                        BTN_Recuperar.Focus();
                    }
                }
                else
                {
                    MostrarError($"No es posible recuperar contraseña, Usuario {eu.EstadoUsuario}\nComunicarse con directorio o Administrador");
                    Limpiar();
                }
            }
            else
            {
                MostrarError("RUT no registrado en Base de Datos\nComunicarse con Administrador o Directorio");
                Limpiar();
            }

        }

        private void BTN_Recuperar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rut = int.Parse(TXT_Rut.Text.Trim());
                cu.Rut = rut;

                if (cu.ReadRutUsuario())
                {
                    string destinatario = cu.Email;
                    string asunto = "RCS-TecMed - Recuperación de Contraseña";

                    string mensaje = $@"
                        <p>Estimado/a <strong>{cu.Nombre} {cu.ApellidoPaterno}</strong>,</p>
                        <p>Sus credenciales de acceso son las siguientes:</p>
                        <p><strong>Usuario:</strong> {cu.UserName}<br>
                        <strong>Contraseña:</strong> {cu.Password}</p>
                        <p>Por favor, cambie su contraseña después de iniciar sesión.</p>
                        <p>Atentamente,<br>Administrador del Sistema RCS-TecMed</p>";

                    EmailNotifier em = new EmailNotifier();
                    bool enviado = em.EnviarCorreo(destinatario, asunto, mensaje);

                    if (enviado)
                    {
                        MostrarInformacion(
                            $"Estimado/a {cu.Nombre} {cu.ApellidoPaterno},\n" +
                            $"Su Usuario y Contraseña han sido enviados al correo: {cu.Email}");
                        Limpiar();
                    }
                    else
                    {
                        MostrarError("No se pudo enviar el correo. Verifique la configuración o conexión.");
                        Limpiar();
                    }
                }
                else
                {
                    MostrarError("No se encontró un usuario registrado con ese RUT.");
                    Limpiar();
                }
            }
            catch (FormatException)
            {
                MostrarError("El RUT ingresado no tiene un formato válido.");
                Limpiar();
            }
            catch (Exception ex)
            {
                MostrarError($"Ocurrió un error inesperado: {ex.Message}");
                Limpiar();
            }
        }


    }
}
