using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_HISTORICOPAGO
    {
        /*ATRIBUTOS DE LA CLASE*/
        public int IdHistoricoPago { get; set; }
        public int Rut { get; set; }
        public int IdFormaPagoCuota { get; set; }
        public DateTime FechaPagoCuota { get; set; }
        public string MesCancelado { get; set; }
        public int AnnoMesCancekadi { get; set; }
        public int ValorCancelado { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        public string FechaPagoCuotaMostrar {  get; set; }
        string DescripcionFormaPago;
        public string _descripcionFormaPago { get {  return DescripcionFormaPago; } }
        string NombreUsuario;
        public string _nombreUsuario { get {  return NombreUsuario; } }

        private void Init() //INICIALICION DE LA CLASE
        {
            /*ATRIBUTOS DE LA CLASE*/
            IdHistoricoPago = 0;
            Rut = 0;
            IdFormaPagoCuota = 0;
            FechaPagoCuota = DateTime.Today;
            MesCancelado = string.Empty;
            AnnoMesCancekadi = 0;
            ValorCancelado = 0;
            IdUsuario = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            FechaPagoCuotaMostrar = string.Empty;
            DescripcionFormaPago = string.Empty;
            NombreUsuario = string.Empty;
        }

        public Controll_HISTORICOPAGO() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE LLAMADA COMPLEMENTARIOS*/
        private void ObtenerFormaPago()
        {
            var fp = new Controll_FORMAPAGO { IdFormaPagoCuota = IdFormaPagoCuota };
            DescripcionFormaPago = fp.ReadId() ? fp.DescripcionFormaPagoCuota ?? string.Empty : string.Empty;
        }

        private void ObtenerUsuario()
        {
            var us = new Controll_USUARIO { IdUsuario = IdUsuario };
            NombreUsuario = us.ReadId() ? us.UserName ?? string.Empty : string.Empty;
        }

        /*METODOS DE CRUD*/
        public bool Create() //CREA REGISTRO QUE SE GRABA EN LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                HISTORICO_PAGO hp = new HISTORICO_PAGO();
                try
                {
                    CommonDB.Synchronize(this, hp);
                    db.HISTORICO_PAGO.Add(hp);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadId() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DEL ID
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    HISTORICO_PAGO hp = db.HISTORICO_PAGO.FirstOrDefault(x => x.IdHistoricoPago == IdHistoricoPago);
                    if (hp == null)
                        return false;

                    CommonDB.Synchronize(hp, this);
                    ObtenerFormaPago();
                    ObtenerUsuario();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadRut() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DE LA RUT
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    HISTORICO_PAGO hp = db.HISTORICO_PAGO.First(x => x.Rut == Rut);
                    if (hp == null)
                        return false;

                    CommonDB.Synchronize(hp, this);
                    ObtenerFormaPago();
                    ObtenerUsuario();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool Update() //ACTUALIZA UN REGISTRO EN LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    HISTORICO_PAGO hp = db.HISTORICO_PAGO.FirstOrDefault(x => x.IdHistoricoPago == IdHistoricoPago);
                    if (hp == null)
                        return false;

                    CommonDB.Synchronize(this, hp);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool Delete() //ELIMINA UN REGISTRO DE LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    HISTORICO_PAGO hp = db.HISTORICO_PAGO.FirstOrDefault(x => x.IdHistoricoPago == IdHistoricoPago);
                    if (hp == null)
                        return false;

                    db.HISTORICO_PAGO.Remove(hp);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /*METODOS DE LISTADOS*/
        public List<Controll_HISTORICOPAGO> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<HISTORICO_PAGO> listaDatos = db.HISTORICO_PAGO.ToList<HISTORICO_PAGO>();
                List<Controll_HISTORICOPAGO> listaHistoricoPago = GenerarLista(listaDatos);
                return listaHistoricoPago;
            }
            catch (Exception)
            {
                return new List<Controll_HISTORICOPAGO>();
            }
        }

        public List<Controll_HISTORICOPAGO> ReadAllOrdenadoRut() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<HISTORICO_PAGO> listaDatos = db.HISTORICO_PAGO.ToList<HISTORICO_PAGO>();
                List<Controll_HISTORICOPAGO> listaHistoricoPago = GenerarLista(listaDatos);
                listaHistoricoPago = listaHistoricoPago.OrderBy(x => x.Rut).ToList();
                return listaHistoricoPago;
            }
            catch (Exception)
            {
                return new List<Controll_HISTORICOPAGO>();
            }
        }

        public List<Controll_HISTORICOPAGO> ReadAllxRut(int rut) //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<HISTORICO_PAGO> listaDatos = db.HISTORICO_PAGO.Where(x => x.Rut == rut).ToList<HISTORICO_PAGO>();
                List<Controll_HISTORICOPAGO> listaHistoricoPago = GenerarLista(listaDatos);
                listaHistoricoPago = listaHistoricoPago.OrderBy(x => x.FechaPagoCuota).ToList();
                return listaHistoricoPago;
            }
            catch (Exception)
            {
                return new List<Controll_HISTORICOPAGO>();
            }
        }

        private List<Controll_HISTORICOPAGO> GenerarLista(List<HISTORICO_PAGO> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_HISTORICOPAGO> listaHistoricoPago = new List<Controll_HISTORICOPAGO>();
            foreach (HISTORICO_PAGO data in dataList)
            {
                Controll_HISTORICOPAGO hp = new Controll_HISTORICOPAGO();
                CommonDB.Synchronize(data, hp);
                hp.FechaPagoCuotaMostrar = data.FechaPagoCuota.ToString("dd-MM-yyyy");
                hp.ObtenerFormaPago();
                hp.ObtenerUsuario();
                listaHistoricoPago.Add(hp);
            }
            return listaHistoricoPago;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.HISTORICO_PAGO
                    .OrderByDescending(x => x.IdHistoricoPago)
                    .Select(x => x.IdHistoricoPago)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }


    }
}
