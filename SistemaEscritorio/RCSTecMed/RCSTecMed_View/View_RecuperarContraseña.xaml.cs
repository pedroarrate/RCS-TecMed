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
        public View_RecuperarContraseña()
        {
            InitializeComponent();
            Limpiar();
        }

        private void Limpiar()
        {
            TXT_Rut.Text = string.Empty;
            TXT_Dv.Text = string.Empty;
            TXT_Mensaje.Text = string.Empty;
            TXT_Rut.Focus();
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
                MostrarError("RUT debe ser numérico y no superar los 10 dígitos");
                Limpiar();
            }

        }

        private void TXT_Dv_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void BTN_Recuperar_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
