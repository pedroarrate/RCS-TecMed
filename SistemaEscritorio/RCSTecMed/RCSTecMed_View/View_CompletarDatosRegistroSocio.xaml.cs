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
    /// Lógica de interacción para View_CompletarDatosRegistroSocio.xaml
    /// </summary>
    public partial class View_CompletarDatosRegistroSocio : Window
    {        
        private readonly MostrarDatos_Controll md = new MostrarDatos_Controll();

        private int idUsuario {  get; set; }
        public View_CompletarDatosRegistroSocio(int idUser)
        {
            InitializeComponent();
            LB_Fecha.Content = DateTime.Now.ToString("dd-MM-yyyy");
            LB_HoraConexion.Content = DateTime.Now.ToString("T");
            LB_Usuario.Content = md.MostrarUsuario(idUser);
            idUsuario = idUser;
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
                            case "TAB_CentroAcademico":
                                selectedTab.Content = new CentroAcademico_UserControl(idUsuario);
                                break;

                            case "TAB_TipoCertificacion":
                                selectedTab.Content = new TipoCertificacion_UserControl(idUsuario);
                                break;

                            case "TAB_EstablecimientoLaboral":
                                selectedTab.Content = new EstablecimientoLaboral_UserControl(idUsuario);
                                break;

                            case "TAB_FormaPago":
                                selectedTab.Content = new FormaPago_UserControl(idUsuario);
                                break;

                            case "TAB_EstimadoPago":
                                selectedTab.Content = new EstaimadoPago_UserControl(idUsuario);
                                break;

                            case "TAB_Salir":
                                Close();
                                break;
                        }
                    }
                }
            }
        }
    }
}
