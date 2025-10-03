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
        //ATRIBUTOS DE LA TABLA
        public string IdentificadorAcademico { get; set; } //Rut
        public string ResumenPerfilAcademico { get; set; } //DescripcionAcademico
        public DateTime FechaRegistroAcademico { get; set; }
        public int CodigoCentroFormacion { get; set; }
        public int CodigoCertificacionObtenida { get; set; }
        public int CodigoUsuarioAsociado { get; set; }

        //INICIALIZACION DE LOS ATRIBUTOS
        private void Init()
        {
            IdentificadorAcademico = string.Empty;
            ResumenPerfilAcademico = string.Empty;
            FechaRegistroAcademico = DateTime.Today;
            CodigoCentroFormacion = 0;
            CodigoCertificacionObtenida = 0;
            CodigoUsuarioAsociado = 0;

        }
    }
}
