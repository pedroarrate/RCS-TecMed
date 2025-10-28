using RCSTecMed_Controll;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
    /// Lógica de interacción para View_ModuloSecretaria.xaml
    /// </summary>
    public partial class View_ModuloSecretaria : Window
    {
        private int idUsuario {  get; set; }
        public View_ModuloSecretaria(int id)
        {
            InitializeComponent();
            idUsuario = id;
            LB_Fecha.Content = DateTime.Today.ToString("D");
            LB_HoraConexion.Content = DateTime.Now.ToString("T");
            LB_Usuario.Content = new MostrarDatos_Controll().MostrarUsuario(id);
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Asegura que el evento provenga del TabControl y que haya elementos seleccionados
            if (e.Source is TabControl && e.AddedItems.Count > 0)
            {
                if (e.AddedItems[0] is TabItem selectedTab)
                {
                    // Evita cargar si ya tiene contenido (como el caso de TAB_Home)
                    if (selectedTab.Content == null)
                    {
                        switch (selectedTab.Name)
                        {
                            case "TAB_RegistroSocio":
                                selectedTab.Content = new RegistroSocio_UserControl(idUsuario);
                                break;

                            case "TAB_Certificaciones":
                                selectedTab.Content = new Certificaciones_UserControl();
                                break;

                            case "TAB_ListadoSociosRegistrados":
                                selectedTab.Content = new ListadoSociosRegistrados_UserControl();
                                break;
                        }
                    }
                }
            }
        }


    }
}
