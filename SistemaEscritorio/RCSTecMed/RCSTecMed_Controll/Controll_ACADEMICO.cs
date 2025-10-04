using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_ACADEMICO
    {
        /*ATRIBUTOS DE LA TABLA*/
        public int IdAcademcico { get; set; }
        public int Rut { get; set; }
        public int IdCertificacion { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCentroAcademico { get; set; }
        public string DescripcionAcademico { get; set; }
        public string FolioRegistroAcademico { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIA*/
        public string FechaRegistro { get; set; }
        string _descCertificacion;
        public string DescripcionCertificacion { get { return _descCertificacion; } }
        string _descCentroAcademico;
        public string DescripcionCentroAcademico { get { return _descCentroAcademico; } }
        string _usuarioQueRegistra;
        public string UsuarioQueRegistra { get { return _usuarioQueRegistra; } }

        private void Init() //INICIALIZACION DE LOS ATRIBUTOS
        {
            /*ATRIBUTOS DE LA TABLA*/
            IdAcademcico = 0;
            Rut = 0;
            IdCertificacion = 0;
            Fecha = DateTime.Today;
            IdCentroAcademico = 0;
            DescripcionAcademico = string.Empty;
            FolioRegistroAcademico = string.Empty;
            IdUsuario = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            FechaRegistro = string.Empty;
            _descCertificacion = string.Empty;
            _descCentroAcademico = string.Empty;
            _usuarioQueRegistra = string.Empty;
        }

        public Controll_ACADEMICO() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS COMPLEMENTARIOS DE LLAMADA DE DATOS*/

        /*METODOS DE CRUD*/
        public bool Create()
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            ACADEMICO ac = new ACADEMICO();
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
