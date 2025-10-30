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
    /// Lógica de interacción para View_ConfirmaEliminar.xaml
    /// </summary>
    public partial class View_ConfirmaEliminar : Window
    {
        private readonly Validaciones_Controll val = new Validaciones_Controll();
        private readonly Mensajes_Controll msc = new Mensajes_Controll();
        private readonly Controll_USUARIO us = new Controll_USUARIO();

        private int idUsuario {  get; set; }
        private string idRegistro { get; set; }
        private string nombreTabla {  get; set; }

        public View_ConfirmaEliminar(int idUser, string idReg, string tabla)
        {
            InitializeComponent();
            idUsuario = idUser;
            idRegistro = idReg;
            nombreTabla = tabla;
            Limpiar();
        }

        private void Limpiar()
        {
            PB_Contraseña1.Password = string.Empty;
            PB_Contraseña2.Password = string.Empty;

            BTN_ConfirmaEliminar.IsEnabled = false;

            PB_Contraseña1.Focus();
        }

        private bool ValidaIngresPass(string pass, Control control)
        {
            if (val.CampoVacio(pass))
            {
                msc.MostrarWarning("Debe ingresar una contraseña valida");
                control.Focus();
                return false;
            }               

            return true;
        }

        private bool ComparaContraseñas(string pass1, string pass2)
        {
            if (pass1 != pass2)
            {
                msc.MostrarError("Contraseñas deben ser iguales");
                Limpiar();
                return false;
            }
            return true;
        }

        private void PB_Contraseña1_KeyDown(object sender, KeyEventArgs e)
        {
            string pass = PB_Contraseña1.Password.ToString();
            
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                bool valida = ValidaIngresPass(pass, PB_Contraseña1);
                if (valida)
                {
                    PB_Contraseña2.Focus();
                }
                return;
            }
        }

        private void PB_Contraseña2_KeyDown(object sender, KeyEventArgs e)
        {
            string pass1 = PB_Contraseña1.Password.ToString();
            string pass2 = PB_Contraseña2.Password.ToString();

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (ValidaIngresPass(pass2, PB_Contraseña2) && ComparaContraseñas(pass1, pass2))
                {
                    BTN_ConfirmaEliminar.IsEnabled = true;
                    BTN_ConfirmaEliminar.Focus();
                }
            }
        }

        private bool ValidarUsuario(int id)
        {
            us.IdUsuario = id;
            if (!us.ReadId())
            {
                msc.MostrarError("No existe usuario en la base de datos");
                Close();
                return false;
            }
            return true;
        }

        private bool ValidarContraseña(string realPass, string inputPass)
        {
            if (realPass != inputPass)
            {
                msc.MostrarError("Contraseña ingresada es incorrecta\nReingresa contraseñas");
                Limpiar();
                return false;
            }
            return true;
        }

        private bool EliminarRegistro(string taBla, string id)
        {   
            switch (taBla)
            {
                case "CentroAcademico":
                    var cca= new Controll_CENTROACADEMICO() { IdCentroAcademico = int.Parse(id) };
                    if (!cca.Delete())
                    {
                        msc.MostrarError("No se logró eliminar registro en la base de datos\nComunicarse con el administrador");
                        Limpiar();
                        return false;
                    }
                    return true;

                default:
                    msc.MostrarError("Tabla no reconocida para eliminación");
                    return false;

            }

        }

        private void BTN_ConfirmaEliminar_Click(object sender, RoutedEventArgs e)
        {
            string pass = PB_Contraseña2.Password.ToString();

            if (!ValidarUsuario(idUsuario))
                return;

            if (!ValidarContraseña(us.Password, pass))
                return;

            if (!EliminarRegistro(nombreTabla, idRegistro))
                return;

            msc.MostrarInformacion("Registro Eliminado de Base de Datos");
            Close();            
        }
    }
}
