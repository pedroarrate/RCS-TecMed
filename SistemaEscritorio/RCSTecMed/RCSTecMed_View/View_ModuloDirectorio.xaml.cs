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

namespace RCSTecMed_View
{
    /// <summary>
    /// Lógica de interacción para View_ModuloDirectorio.xaml
    /// </summary>
    public partial class View_ModuloDirectorio : Window
    {
        private int IdUsuario {  get; set; }
        public View_ModuloDirectorio(int Id)
        {
            InitializeComponent();
            IdUsuario = Id;
        }
    }
}
