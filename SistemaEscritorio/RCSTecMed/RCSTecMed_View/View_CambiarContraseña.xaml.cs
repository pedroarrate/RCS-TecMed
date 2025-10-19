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
    /// Lógica de interacción para View_CambiarContraseña.xaml
    /// </summary>
    public partial class View_CambiarContraseña : Window
    {
        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private readonly Controll_USUARIO cu = new Controll_USUARIO();
        private readonly Mensajes_Controll ms = new Mensajes_Controll();

        private int userRut;
        private bool resp;

        
        public View_CambiarContraseña(int rut)
        {
            InitializeComponent();
            userRut = rut;
            Limpiar();
        }

        private void Limpiar()
        {
            //LLENAR CAMPOS DE DATOS ACTUALES
            DatosActuales(userRut);
            //LIMPIAR TEXTOS
            TXT_NewUserName.Text = string.Empty;
            TXT_NewPassword.Text = string.Empty;
            TXT_ConfirmNewPassword.Text = string.Empty;
        }

        private void DatosActuales(int rutUser)
        {
            cu.Rut = rutUser;
            if (cu.ReadRutUsuario())
            {
                LB_NombreCompleto.Content = $"{cu.ApellidoPaterno} {cu.Nombre}";
                LB_UserName.Content = cu.UserName;
                LB_Password.Content = cu.Password;
            }
        }

        private bool ValidarUserName(string userName)
        {
            if (val.CampoVacio(userName))
            {
                ms.MostrarError("Debe ingresar un nombre de usuario");
                resp = false;
            }
            else
            {
                int largo = val.LargoCampo(userName);
                if (largo >= 10 && largo <= 20)
                {
                    resp = true;
                }
                else
                {
                    ms.MostrarError("El nombre de usuario debe tener entre 10 y 20 caracteres");
                    resp = false;
                }
            }
            return resp;
        }

        private void TXT_NewUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (ValidarUserName(TXT_NewUserName.Text))
                {
                    TXT_NewPassword.IsEnabled = true;
                    TXT_NewPassword.Focus();
                }
                else
                {
                    TXT_NewPassword.Text = string.Empty;
                    TXT_NewPassword.Focus();
                }
            }            
        }

        private void TXT_NewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (val.CampoVacio(TXT_NewPassword.Text))
                {
                    ms.MostrarError("Debe ingresar una contraseña");
                    return;
                }

                if (!val.FormatoPassword(TXT_NewPassword.Text))
                {
                    ms.MostrarError("Formato de contraseña inválido\n" +
                                    "Debe tener entre 10 y 20 caracteres\n" +
                                    "Debe incluir al menos una letra minúscula\n" +
                                    "Debe incluir al menos una letra mayúscula\n" +
                                    "Debe incluir al menos un número\n" +
                                    "Debe incluir al menos un carácter especial");

                    TXT_NewPassword.Text = string.Empty;
                    TXT_NewPassword.Focus();
                    return;
                }

                TXT_ConfirmNewPassword.IsEnabled = true;
                TXT_ConfirmNewPassword.Focus();
            }
                
        }

        private void TXT_ConfirmNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            string pass1 = TXT_NewPassword.Text;
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (TXT_ConfirmNewPassword.Text == pass1)
                {
                    BTN_CambiarContraseña.IsEnabled = true;
                    BTN_CambiarContraseña.Focus();
                }
                else
                {
                    ms.MostrarError("Nueva Contraseña y Confirmación debe ser iguales");
                    TXT_ConfirmNewPassword.Text = string.Empty;
                    TXT_ConfirmNewPassword.Focus();
                }
            }

        }

        private void BTN_CambiarContraseña_Click(object sender, RoutedEventArgs e)
        {
            string nuevoUserName = TXT_NewUserName.Text;
            string nuevaPassword = TXT_NewPassword.Text;
            string confirmarPassword = TXT_ConfirmNewPassword.Text;

            // Validaciones
            if (!ValidarUserName(nuevoUserName))
            {
                ms.MostrarError("Nombre de usuario inválido");
                return;
            }

            if (!val.FormatoPassword(nuevaPassword))
            {
                ms.MostrarError("Formato de contraseña inválido\n" +
                                "Debe tener entre 10 y 20 caracteres\n" +
                                "Debe incluir al menos una letra minúscula\n" +
                                "Debe incluir al menos una letra mayúscula\n" +
                                "Debe incluir al menos un número\n" +
                                "Debe incluir al menos un carácter especial");
                return;
            }

            if (nuevaPassword != confirmarPassword)
            {
                ms.MostrarError("La nueva contraseña y su confirmación no coinciden");
                return;
            }

            // Asignación
            cu.Rut = userRut;
            cu.UserName = nuevoUserName;
            cu.Password = nuevaPassword;

            // Actualización
            if (cu.Update())
            {
                ms.MostrarInformacion("Actualización de usuario y contraseña realizada correctamente");
                Limpiar();
            }
            else
            {
                ms.MostrarError($"No se realizó la actualización del usuario {userRut}. Comuníquese con el administrador.");
                Limpiar();
            }

        }



    }
}
