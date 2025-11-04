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
    /// Lógica de interacción para ActualizaEstabelcimientoActual_UserControl.xaml
    /// </summary>
    public partial class ActualizaEstabelcimientoActual_UserControl : UserControl
    {

        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private readonly Mensajes_Controll ms = new Mensajes_Controll();
        private readonly MostrarDatos_Controll md = new MostrarDatos_Controll();
        private readonly Controll_ESTABLECIMIENTOACTUAL est = new Controll_ESTABLECIMIENTOACTUAL();

        private int IdUsuario {  get; set; }
        public ActualizaEstabelcimientoActual_UserControl(int IdUser)
        {
            InitializeComponent();
            IdUsuario = IdUser;
        }

        private void TXT_Rut_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_Dv_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void BTN_Buscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DG_Laborales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CB_Region_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CB_Comuna_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CB_Establecimiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BTN_Grabar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Limpiar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Home_Click(object sender, RoutedEventArgs e)
        {
            // Buscar el TabControl en la ventana contenedora
            if (Window.GetWindow(this) is View_ModuloSecretaria mdSec)
            {
                // Activar la pestaña "Home"
                mdSec.MainTabControl.SelectedItem = mdSec.TAB_Home;
            }
        }

        
    }
}
