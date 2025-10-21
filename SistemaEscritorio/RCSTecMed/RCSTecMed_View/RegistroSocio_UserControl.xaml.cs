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
    /// Lógica de interacción para RegistroSocio_UserControl.xaml
    /// </summary>
    public partial class RegistroSocio_UserControl : UserControl
    {
        public RegistroSocio_UserControl()
        {
            InitializeComponent();
            LB_TotalRegistrados.Content = new MostrarDatos_Controll().TotalSociosRegistrados().ToString();
        }

        private void BTN_Home_Click(object sender, RoutedEventArgs e)
        {
            // Buscar el TabControl en la ventana contenedora
            var parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                var moduloSecretaria = parentWindow as View_ModuloSecretaria;
                if (moduloSecretaria != null)
                {
                    // Activar la pestaña "Home"
                    moduloSecretaria.MainTabControl.SelectedItem = moduloSecretaria.TAB_Home;
                }
            }

        }

        private void BTN_Buscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Grabar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Actualizar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Eliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Limpiar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
