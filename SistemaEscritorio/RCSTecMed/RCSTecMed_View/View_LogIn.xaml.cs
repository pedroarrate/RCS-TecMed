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
    /// Lógica de interacción para View_LogIn.xaml
    /// </summary>
    public partial class View_LogIn : Window
    {
        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private int usuarioId;
        private int rol;

        public View_LogIn()
        {
            InitializeComponent();
            Limpiar();
        }

        private void Limpiar()
        {
            TXT_Usuario.Text = string.Empty;
            PASS_Contraseña.Password = string.Empty;
            TXT_Usuario.Focus();
        }

        private void MostrarError(string mensaje) //MUESTRA MENSAJES DE ERROR SEGUN SO CONTEXTO
        {
            MessageBox.Show(mensaje, "RCSTecMed - Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MostrarInformacion(string mensaje)
        {
            MessageBox.Show(mensaje, "RCSTecMed - Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ValidarUsuario() //VALIDA NOMBRE DE USUARIO ES CORRECTO Y/O SI FUE INGRESADO
        {
            Controll_USUARIO cu = new Controll_USUARIO();
            string nombreUsuario = TXT_Usuario.Text;
            cu.UserName = nombreUsuario;

            if (val.CampoVacio(nombreUsuario))
            {
                MostrarError("Debe Ingresar un Nombre de Usuario.");
                TXT_Usuario.Focus();
                return;
            }

            if (!cu.ReadUserName())
            {
                MostrarError("Usuario Invalido o No Existe en Base de Datos.");
                TXT_Usuario.Text = string.Empty;
                TXT_Usuario.Focus();
                return;
            }

            PASS_Contraseña.Focus();
        }

        private void TXT_Usuario_KeyDown(object sender, KeyEventArgs e) // VALIDA EL COMPORTAMIENTO DE ENTER O TAB EN NOMBRE USUARIO
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
                ValidarUsuario();            
        }

        private void ValidarContraseña()//VALIDA CONTRASEÑA ES CORRECTA Y/O SI FUE INGRESADA
        {
            Controll_USUARIO cu = new Controll_USUARIO();
            string nombreUsuario = TXT_Usuario.Text;
            string contraseña = PASS_Contraseña.Password;
            cu.UserName = nombreUsuario;

            if (val.CampoVacio(contraseña))
            {
                MostrarError("Debe Ingresar una Contraseña.");
                PASS_Contraseña.Focus();
                return;
            }

            if (!cu.ReadUserName())
            {
                MostrarError("Contraseña Incorrecta.");
                PASS_Contraseña.Password = string.Empty;
                PASS_Contraseña.Focus();
                return;
            }

            
            if (cu.Password != contraseña)
            {
                MostrarError("Contraseña Incorrecta.");
                PASS_Contraseña.Password = string.Empty;
                PASS_Contraseña.Focus();
                return;
            }                
            
            if (cu.IdEstado != 1)
            {
                MostrarError("Usuario Inhabilitado.\nComunicarse con Administrador de Sistema");
                Limpiar();
                return;
            }

            ValidaUsuarioEscritorio(cu.IdUsuario);
        }

        private void GrillaUsuario(int id) //GENERA LISTA DE DATAGRID
        {
            DG_Perfiles.ItemsSource = new Controll_USUARIODESK().ReadAllIdUsuario(id);
        }

        private void ValidaUsuarioEscritorio(int id) //VALIDA SI USUARIO DE ESCRITORIO ES VALIDO
        {
            Controll_USUARIODESK ud = new Controll_USUARIODESK();
            ud.IdUsuario = id;
            if (ud.ReadIdUsuario())
            {
                GrillaUsuario(id);
                MostrarInformacion("Usuario y Contraseña Correctos.\nSeleccione Perfil a Utilizar");
            }
            else
            {
                MostrarError("Usuario no cuenta con perfiles para Módulos de Escritorio");
            }
        }
        private void PASS_Contraseña_KeyDown(object sender, KeyEventArgs e) // VALIDA EL COMPORTAMIENTO DE ENTER O TAB EN CONTRASEÑA
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
                ValidarContraseña();
        }

        private void DG_Perfiles_SelectionChanged(object sender, SelectionChangedEventArgs e) // CONTROLA LA SELECCION DESDE LA GRILLA
        {
            BTN_Ingresar.IsEnabled = DG_Perfiles.SelectedItem != null;            
        }

        private void BTN_Ingresar_Click(object sender, RoutedEventArgs e)
        {
            if (DG_Perfiles.SelectedItem is Controll_USUARIODESK ud)
            {
                usuarioId = ud.IdUsuario;
                rol = ud.IdRol;

                
                // Aquí podrías usar rol para redirigir al módulo correspondiente
            }
            else
            {
                MostrarError("Debe seleccionar un perfil para continuar.");
            }

        }

        private void RecuperarContrasena_Click(object sender, MouseButtonEventArgs e)
        {

        }

       
    }
}
