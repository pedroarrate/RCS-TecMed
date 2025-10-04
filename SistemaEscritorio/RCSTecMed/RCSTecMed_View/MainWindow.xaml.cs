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
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Limpiar();
        }

        private void Limpiar()
        {
            TXT_Codigo.Text = string.Empty;
            TXT_Descripcion.Text = string.Empty;
            TXT_Codigo.Focus();
        }

        private void BTN_Grabar_Click(object sender, RoutedEventArgs e)
        {
            Controll_ESTADOSOCIO es = new Controll_ESTADOSOCIO();
            es.IdEstadoSocio = int.Parse(TXT_Codigo.Text);
            es.DescripcionEstadoSocio = TXT_Descripcion.Text;

            if (es.Create())
            {
                MessageBox.Show("Registro Correcto!!!", "xxx", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show("Registro Incorrecto!!!", "xxx", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
            }
        }

        private void BTN_Buscar_Click(object sender, RoutedEventArgs e)
        {
            Controll_ESTADOSOCIO es = new Controll_ESTADOSOCIO();
            //es.IdEstadoSocio = int.Parse(TXT_Codigo.Text);
            es.DescripcionEstadoSocio = TXT_Descripcion.Text;

            if (es.ReadDesc())
            {
                MessageBox.Show("Registro Encontrado!!!", "xxx", MessageBoxButton.OK, MessageBoxImage.Information);
                TXT_Codigo.Text = es.IdEstadoSocio.ToString();
            }
            else
            {
                MessageBox.Show("Registro No encontrado!!!", "xxx", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
            }
        }
    }
}
