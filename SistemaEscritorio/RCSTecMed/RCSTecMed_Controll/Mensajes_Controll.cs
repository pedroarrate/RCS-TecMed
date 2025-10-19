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
using System.Windows.Shapes;

namespace RCSTecMed_Controll
{
    public class Mensajes_Controll
    {
        public Mensajes_Controll() { }

        public void MostrarError(string mensaje) //MUESTRA MENSAJES DE ERROR SEGUN SO CONTEXTO
        {
            MessageBox.Show(mensaje, "RCSTecMed - Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void MostrarInformacion(string mensaje)
        {
            MessageBox.Show(mensaje, "RCSTecMed - Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void MostrarWarning(string mensaje)
        {
            MessageBox.Show(mensaje, "RCSTecMed - Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
