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
        private Validaciones_Controll val = new Validaciones_Controll();
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

        private void TXT_Usuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (val.CampoVacio(TXT_Usuario.Text) == true)
                {
                    MessageBox.Show("Debe Ingresar un Usuario Valido!!!", "Stop", MessageBoxButton.OK, MessageBoxImage.Stop);
                    TXT_Usuario.Focus();
                }
                else
                {
                    Controll_USUARIO us = new Controll_USUARIO();
                    us.UserName = TXT_Usuario.Text;
                    if (us.ReadUserName() == true)
                    {
                        PASS_Contraseña.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Usuario Valido No Existe en Registros!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        TXT_Usuario.Focus();
                    }
                }
            }             
        }

        
        private void PASS_Contraseña_KeyDown(object sender, KeyEventArgs e)
        {

        }

        

        private void DG_Perfiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BTN_Ingresar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RecuperarContrasena_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
