using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_USUARIO
    {
        /*ATRIBUTOS DE LA TABLA*/
        public int IdUsuario { get; set; }
        public int Rut { get; set; }
        public string Dv { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdEstado { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        public string FechaCracionMostrar {  get; set; }
        string EstadoUsuario;
        public string _estadoUsuario { get { return EstadoUsuario; } }

        private void Init() //CONSTRUCTOR DE LA CLASE
        {
            /*ATRIBUTOS DE LA TABLA*/
            IdUsuario = 0;
            Rut = 0;
            Dv = string.Empty;
            ApellidoPaterno = string.Empty;
            Nombre = string.Empty;
            Email = string.Empty;
            FechaCreacion = DateTime.Today;
            IdEstado = 0;
            UserName = string.Empty;
            Password = string.Empty;

            /*VARIABLES COMPLEMENTARIAS*/
            FechaCracionMostrar = string.Empty;
            EstadoUsuario = string.Empty;
        }

        public Controll_USUARIO() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE LLAMADA COMPLEMENTARIOS*/
        private void ObtenerEstadoUsuario()
        {
            try
            {
                Controll_ESTADOUSUARIO eu = new Controll_ESTADOUSUARIO { IdEstado = IdEstado };
                if (eu.ReadId())
                {
                    EstadoUsuario = eu.EstadoUsuario ?? string.Empty;
                }
                else
                {
                    EstadoUsuario = string.Empty;
                }
            }
            catch (Exception ex)
            {
                EstadoUsuario = string.Empty;
            }
        }

    }
}
