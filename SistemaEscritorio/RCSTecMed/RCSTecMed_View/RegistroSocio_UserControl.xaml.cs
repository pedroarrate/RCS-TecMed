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
        private int idUser {  get; set; }
        public RegistroSocio_UserControl(int idUsuario)
        {
            InitializeComponent();
            idUser = idUsuario;
            LB_TotalRegistrados.Content = new MostrarDatos_Controll().TotalSociosRegistrados().ToString();
        }

        private void BTN_Home_Click(object sender, RoutedEventArgs e)
        {
            // Buscar el TabControl en la ventana contenedora
            var parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                var mdSec = parentWindow as View_ModuloSecretaria;
                if (mdSec != null)
                {
                    // Activar la pestaña "Home"
                    mdSec.MainTabControl.SelectedItem = mdSec.TAB_Home;
                }
            }

        }

        //BOTONES DE CRUD
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

        //DATOS PERSONALES
        private void TXT_Rut_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_Dv_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_ApellidoPaterno_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_ApellidoMaterno_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_Nombres_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_Domicilio_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CB_Region_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TXT_EMail_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_FonoFijo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_FonoMovil_KeyDown(object sender, KeyEventArgs e)
        {

        }

        //DATOS ACADEMICOS
        private void TXT_NotaCertificacion_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_HorasCertificacion_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_FolioCertificacion_KeyDown(object sender, KeyEventArgs e)
        {

        }

        //DATOS LABORALES
        private void TXT_DireccionLaboral_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_TelefonoLaboral_KeyDown(object sender, KeyEventArgs e)
        {

        }

        //LLAMAR COMPLETAR DATOS
        private void BTN_CompletarDatos_Click(object sender, RoutedEventArgs e)
        {
            View_CompletarDatosRegistroSocio vcds = new View_CompletarDatosRegistroSocio(idUser) ;
            vcds.ShowDialog();
        }
    }
}
