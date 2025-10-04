using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_ESTADOSOCIO
    {
        /*ATRIBUSTOS DE LA TABLA*/
        public int IdEstadoSocio { get; set; }
        public string DescripcionEstadoSocio { get; set; }

        private void Init() //INICIALIZACION DE LOS ATRIBUTOS
        {
            IdEstadoSocio = 0;
            DescripcionEstadoSocio = string.Empty;
        }

        public Controll_ESTADOSOCIO() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE CRUD*/
        public bool Create()
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                ESTADO_SOCIO es = new ESTADO_SOCIO();
                try
                {
                    CommonDB.Synchronize(this, es);
                    db.ESTADO_SOCIO.Add(es);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {                    
                    return false;
                }
            }
        }

        public bool ReadId()
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    ESTADO_SOCIO es = db.ESTADO_SOCIO.FirstOrDefault(x => x.IdEstadoSocio == IdEstadoSocio);
                    if (es == null)
                        return false;

                    CommonDB.Synchronize(es, this);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }   
        }

        public bool ReadDesc()
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    ESTADO_SOCIO es = db.ESTADO_SOCIO.First(x => x.DescripcionEstadoSocio == DescripcionEstadoSocio);
                    if (es == null)
                        return false;

                    CommonDB.Synchronize(es, this);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
