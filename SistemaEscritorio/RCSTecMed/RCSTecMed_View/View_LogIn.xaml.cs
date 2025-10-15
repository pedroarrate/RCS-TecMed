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
            LimpiarGrilla();
            TXT_Usuario.Text = string.Empty;
            PASS_Contraseña.Password = string.Empty;
            TXT_Usuario.Focus();
        }

        private void LimpiarGrilla() //GENERA LISTA DE DATAGRID
        {
            DG_Perfiles.ItemsSource = new Controll_USUARIODESK().ReadAllIdUsuario(0);
        }

        private void MostrarError(string mensaje) //MUESTRA MENSAJES DE ERROR SEGUN SO CONTEXTO
        {
            MessageBox.Show(mensaje, "RCSTecMed - Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MostrarInformacion(string mensaje)
        {
            MessageBox.Show(mensaje, "RCSTecMed - Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private Controll_USUARIO ObtenerUsuarioValidado() //CONTROLA DATOS INGRESADOS EN NOMBRE USUARIO
        {
            string nombreUsuario = TXT_Usuario.Text;

            if (val.CampoVacio(nombreUsuario))
            {
                MostrarError("Debe ingresar un nombre de usuario.");
                TXT_Usuario.Focus();
                return null;
            }

            var cu = new Controll_USUARIO { UserName = nombreUsuario };

            if (!cu.ReadUserName())
            {
                MostrarError("Usuario inválido o no existe en base de datos.");
                TXT_Usuario.Text = string.Empty;
                TXT_Usuario.Focus();
                return null;
            }

            if (cu.IdEstado != 1)
            {
                MostrarError($"Estado usuario: {cu._estadoUsuario}\nUsuario inhabilitado\nComunicarse con directivos o administrador.");
                TXT_Usuario.Text = string.Empty;
                TXT_Usuario.Focus();
                return null;
            }

            return cu;
        }

        private void ValidarUsuario() //VALIDA NOMBRE DE USUARIO ES CORRECTO Y/O SI FUE INGRESADO
        {
            var cu = ObtenerUsuarioValidado();
            if (cu != null)
                PASS_Contraseña.Focus();
        }

        private void TXT_Usuario_KeyDown(object sender, KeyEventArgs e) // VALIDA EL COMPORTAMIENTO DE ENTER O TAB EN NOMBRE USUARIO
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
                ValidarUsuario();            
        }

        private void ValidarContraseña()//VALIDA CONTRASEÑA ES CORRECTA Y/O SI FUE INGRESADA
        {
            var cu = ObtenerUsuarioValidado();
            if (cu == null)
                return;

            string contraseña = PASS_Contraseña.Password;

            if (val.CampoVacio(contraseña))
            {
                MostrarError("Debe ingresar una contraseña.");
                PASS_Contraseña.Focus();
                return;
            }

            if (cu.Password != contraseña)
            {
                MostrarError("Contraseña incorrecta.");
                PASS_Contraseña.Password = string.Empty;
                PASS_Contraseña.Focus();
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

        private void AbrirModuloAdministrador(int id) 
        {
            //View_ModuloAdministrador vma = new View_ModuloAdministrador(id) { Owner = this };
            //vma.ShowDialog();
            MessageBox.Show("Esta Opción abrira el Módulo de ADMINISTRADOR", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
            Limpiar();
        }

        private void AbrirModuloSecretaria(int id)
        {
            //View_ModuloSecretaria vms = new View_ModuloSecretaria(id) { Owner = this };
            //vms.ShowDialog();
            MessageBox.Show("Esta Opción abrira el Módulo de SECRETARIA", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
            Limpiar();
        }

        private void AbrirModuloTesoreria(int id)
        {
            //View_ModuloTesoreria vmt = new View_ModuloTesoreria(id) { Owner = this };
            //vmt.ShowDialog();
            MessageBox.Show("Esta Opción abrira el Módulo de TESORERIA", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
            Limpiar();
        }

        private void AbrirModuloDirectorio(int id)
        {
            //View_ModuloDirectorio vmd = new View_ModuloDirectorio(id) { Owner = this };
            //vmd.ShowDialog();
            MessageBox.Show("Esta Opción abrira el Módulo de DIRECTORIO", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
            Limpiar();
        }

        private void AbrirModuloSocioTecnologo(int id)
        {
            MessageBox.Show("Opción No Valida para Versión de Escritorio", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
            Limpiar();
        }

        private void AbrirModuloAuditor(int id)
        {
            //View_ModuloAuditor vmau = new View_ModuloAuditor(id) { Owner = this };
            //vmau.ShowDialog();
            MessageBox.Show("Esta Opción abrira el Módulo de AUDITOR", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
            Limpiar();
        }

        private void BTN_Ingresar_Click(object sender, RoutedEventArgs e)
        {
            if (DG_Perfiles.SelectedItem is Controll_USUARIODESK ud)
            {
                usuarioId = ud.IdUsuario;
                rol = ud.IdRol;

                switch (rol)
                {
                    case 1: // Administrador
                        AbrirModuloAdministrador(usuarioId);
                        break;

                    case 2: // Secretaria
                        AbrirModuloSecretaria(usuarioId);
                        break;

                    case 3: // Tesorería
                        AbrirModuloTesoreria(usuarioId);
                        break;

                    case 4: // Directorio
                        AbrirModuloDirectorio(usuarioId);
                        break;

                    case 5: // Socio Tecnólogo
                        AbrirModuloSocioTecnologo(usuarioId);
                        break;

                    case 6: // Auditor
                        AbrirModuloAuditor(usuarioId);
                        break;

                    default:
                        MostrarError("Rol no reconocido. Comuníquese con el Administrador.");
                        break;
                }

            }
            else
            {
                MostrarError("Debe seleccionar un perfil para continuar.");
            }
        }

        private void RecuperarContrasena_Click(object sender, MouseButtonEventArgs e)
        {
            View_RecuperarContraseña rcon = new View_RecuperarContraseña() { Owner = this };
            rcon.ShowDialog();
        }


    }
}
