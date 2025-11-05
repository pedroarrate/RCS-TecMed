using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_SOCIO
    {
        /*ATRIBUTOS DE LA CLASE*/
        public int Rut { get; set; }
        public string Dv { get; set; }
        public int FolioRegistro { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Domicilio { get; set; }
        public string IdComuna { get; set; }
        public string IdNacionalidad { get; set; }
        public int TelMovil { get; set; }
        public int TelFijo { get; set; }
        public string Email { get; set; }
        public int IdFormaPagoCuota { get; set; }
        public string ObservacionPagocuota { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstadoSocio { get; set; }
        public Nullable<int> FolioSuperintendencia { get; set; }
        public int IdEstimadoPago { get; set; }
        public DateTime FechaRegistro { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        public string FechaNacimientoMostrar { get; set; }
        public string FechaRegistroMostrar { get; set; }         
        string NombreComunaRegion;
        public string _nombreComunaRegion { get { return NombreComunaRegion; } }
        string NombreComunaRegionLaboral;
        public string _nombreComunaRegionLaboral { get { return NombreComunaRegionLaboral; } }
        string DescripcionNacionalidad;
        public string _descripcionNacionalidad { get { return DescripcionNacionalidad; } }
        string DescripcionFormaPago;
        public string _descripcionFormaPago { get { return DescripcionFormaPago; } }
        string NombreUsuario;
        public string _nombreUsuario { get { return NombreUsuario; } }
        string DescripcionEstadoSocio;
        public string _descripcionEstadoSocio { get { return DescripcionEstadoSocio; } }
        string NombreEstimadoPago;
        public string _nombreEstimadoPago { get { return NombreEstimadoPago; } }
        string NombreEstablecimiento;
        public string _nombreEstablecimiento { get { return NombreEstablecimiento; } }

        private void Init() //INICIALIZADOR DE LA CLASE
        {
            /*ATRIBUTOS DE LA CLASE*/
            Rut = 0;
            Dv = string.Empty;
            FolioRegistro = 0;
            ApellidoPaterno = string.Empty;
            ApellidoMaterno = string.Empty;
            Nombres = string.Empty;
            FechaNacimiento = DateTime.Today;
            Domicilio = string.Empty;
            IdComuna = string.Empty;
            IdNacionalidad = string.Empty;
            TelMovil = 0;
            TelFijo = 0;
            Email = string.Empty;
            IdFormaPagoCuota = 0;
            ObservacionPagocuota = string.Empty;
            IdUsuario = 0;
            IdEstadoSocio = 0;
            FolioSuperintendencia = null;
            IdEstimadoPago = 0;
            FechaRegistro = DateTime.Today;

            /*VARIABLES COMPLEMENTARIAS*/
            FechaNacimientoMostrar = string.Empty;
            FechaRegistroMostrar = string.Empty;
            NombreComunaRegion = string.Empty;
            DescripcionNacionalidad = string.Empty;
            DescripcionFormaPago = string.Empty;
            NombreUsuario = string.Empty;
            DescripcionEstadoSocio = string.Empty;
            NombreEstimadoPago = string.Empty;
            NombreEstablecimiento = string.Empty;
        }

        public Controll_SOCIO() { Init(); } //CONSTRUTOR DE LA CLASE

        /*METODOS DE LLAMADA COMPLEMENTARIOS*/
        private void ObtenerComunaRegion()
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();

            var com = new Controll_COMUNA { IdComuna = IdComuna };

            if (com.ReadId())
            {
                var reg = new Controll_REGION { IdRegion = com.IdRegion };

                if (reg.ReadId())
                {
                    NombreComunaRegion = $"{com.NombreComuna} - {reg.NombreRegion}";
                }
                else
                {
                    NombreComunaRegion = com.NombreComuna ?? string.Empty;
                }
            }
            else
            {
                NombreComunaRegion = string.Empty;
            }
        }

        private void ObtenerNacionalidad()
        {
            var nac = new Controll_NACIONALIDAD { IdNacionalidad = IdNacionalidad };
            DescripcionNacionalidad = nac.ReadId() ? nac.NombreNacionalidad ?? string.Empty : string.Empty;
        }

        private void ObtenerFormaPago()
        {
            var pag = new Controll_FORMAPAGO { IdFormaPagoCuota = IdFormaPagoCuota };
            DescripcionFormaPago = pag.ReadId() ? pag.DescripcionFormaPagoCuota ?? string.Empty : string.Empty;
        }

        private void ObtenerUsuario()
        {
            var us = new Controll_USUARIO { IdUsuario = IdUsuario };
            NombreUsuario = us.ReadId() ? us.UserName ?? string.Empty : string.Empty;
        }

        private void ObtenerEstadoSocio()
        {
            var es = new Controll_ESTADOSOCIO { IdEstadoSocio = IdEstadoSocio };
            DescripcionEstadoSocio = es.ReadId() ? es.DescripcionEstadoSocio ?? string.Empty : string.Empty;
        }

        private void ObtenerEstimadoPago()
        {
            var ep = new Controll_ESTIMADOPAGO { IdEstimadoPago = IdEstimadoPago };
            NombreEstimadoPago = ep.ReadId() ? ep.DescripcionEstimadoPago ?? string.Empty : string.Empty;
        }

        private void ObtenerEstablecimientoActual()
        {
            var estac = new Controll_ESTABLECIMIENTOACTUAL();
            var est = new Controll_ESTABLECIMIENTO();
            int rut = Rut;

            if (!estac.ReadRutFechaHastaNull(rut))
                return;
            

            est.IdEstablecimiento = estac.IdEstablecimiento;
            NombreEstablecimiento = est.ReadId() ? est.NombreEstablecimiento : string.Empty;
        }

        private void ObtenerComunaRegionLaboral()
        {
            var estac = new Controll_ESTABLECIMIENTOACTUAL();
            var est = new Controll_ESTABLECIMIENTO();
            var com = new Controll_COMUNA();
            int rut = Rut;

            if (!estac.ReadRutFechaHastaNull(rut))            
                return;
            

            est.IdEstablecimiento = estac.IdEstablecimiento;
            if (!est.ReadId())
                return;

            com.IdComuna = est.IdComuna;
            if (com.ReadId())
            {
                var reg = new Controll_REGION { IdRegion = com.IdRegion };

                if (reg.ReadId())
                {
                    NombreComunaRegionLaboral = $"{com.NombreComuna} - {reg.NombreRegion}";
                }
                else
                {
                    NombreComunaRegionLaboral = com.NombreComuna ?? string.Empty;
                }
            }
            else
            {
                NombreComunaRegionLaboral = string.Empty;
            }
        }

        /*METODOS DE CRUD*/
        public bool Create() //CREA REGISTRO QUE SE GRABA EN LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                SOCIO soc = new SOCIO();
                try
                {
                    CommonDB.Synchronize(this, soc);
                    db.SOCIO.Add(soc);
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
                    SOCIO soc = db.SOCIO.FirstOrDefault(x => x.Rut == Rut);
                    if (soc == null)
                        return false;

                    CommonDB.Synchronize(soc, this);
                    ObtenerComunaRegion();
                    ObtenerNacionalidad();
                    ObtenerFormaPago();
                    ObtenerUsuario();
                    ObtenerEstadoSocio();
                    ObtenerEstimadoPago();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadApellido() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DE LA DESCRIPCION O NOMBRE DE ESTE
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    SOCIO soc = db.SOCIO.First(x => x.ApellidoPaterno == ApellidoPaterno);
                    if (soc == null)
                        return false;

                    CommonDB.Synchronize(soc, this);
                    ObtenerComunaRegion();
                    ObtenerNacionalidad();
                    ObtenerFormaPago();
                    ObtenerUsuario();
                    ObtenerEstadoSocio();
                    ObtenerEstimadoPago();

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
                    SOCIO soc = db.SOCIO.FirstOrDefault(x => x.Rut == Rut);
                    if (soc == null)
                        return false;

                    CommonDB.Synchronize(this, soc);
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
                    SOCIO soc = db.SOCIO.FirstOrDefault(x => x.Rut == Rut);
                    if (soc == null)
                        return false;

                    db.SOCIO.Remove(soc);
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
        public List<Controll_SOCIO> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<SOCIO> listaDatos = db.SOCIO.ToList<SOCIO>();
                List<Controll_SOCIO> listaSocio = GenerarLista(listaDatos);
                return listaSocio;
            }
            catch (Exception)
            {
                return new List<Controll_SOCIO>();
            }
        }

        public List<Controll_SOCIO> ReadAllOrdenadoRut() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<SOCIO> listaDatos = db.SOCIO.ToList<SOCIO>();
                List<Controll_SOCIO> listaSocio = GenerarLista(listaDatos);
                listaSocio = listaSocio.OrderBy(x => x.Rut).ToList();
                return listaSocio;
            }
            catch (Exception)
            {
                return new List<Controll_SOCIO>();
            }
        }

        public List<Controll_SOCIO> ReadAllOrdenadoApellido() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<SOCIO> listaDatos = db.SOCIO.ToList<SOCIO>();
                List<Controll_SOCIO> listaSocio = GenerarLista(listaDatos);
                listaSocio = listaSocio.OrderBy(x => x.ApellidoPaterno).ToList();
                return listaSocio;
            }
            catch (Exception)
            {
                return new List<Controll_SOCIO>();
            }
        }

        public List<Controll_SOCIO> ReadAllBuscarApellido(string _apellido) //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<SOCIO> listaDatos = db.SOCIO.Where(x => x.ApellidoPaterno == _apellido).ToList<SOCIO>();
                List<Controll_SOCIO> listaSocio = GenerarLista(listaDatos);
                listaSocio = listaSocio.OrderBy(x => x.ApellidoPaterno).ToList();
                return listaSocio;
            }
            catch (Exception)
            {
                return new List<Controll_SOCIO>();
            }
        }

        public List<Controll_SOCIO> ListaSocioPorRegion(int region) //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<COMUNA> com = db.COMUNA.Where(x => x.IdRegion == region).ToList();
                List<SOCIO> soc = db.SOCIO.ToList();

                List<SOCIO> listaDatos = (from c in com join s in soc on c.IdComuna equals s.IdComuna select s).Distinct().OrderBy(x => x.ApellidoPaterno).ToList();
                List<Controll_SOCIO> listaSocio = GenerarLista(listaDatos);

                return listaSocio;
            }
            catch (Exception)
            {
                return new List<Controll_SOCIO>();
            }
        }

        private List<Controll_SOCIO> GenerarLista(List<SOCIO> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_SOCIO> listaSocio = new List<Controll_SOCIO>();
            foreach (SOCIO data in dataList)
            {
                Controll_SOCIO soc = new Controll_SOCIO();
                CommonDB.Synchronize(data, soc);
                soc.FechaNacimientoMostrar = data.FechaNacimiento.ToString("dd-MM-yyyy");
                soc.FechaRegistroMostrar = data.FechaRegistro.ToString("dd-MM-yyyy");
                soc.ObtenerComunaRegion();
                soc.ObtenerNacionalidad();
                soc.ObtenerFormaPago();
                soc.ObtenerUsuario();
                soc.ObtenerEstadoSocio();
                soc.ObtenerEstimadoPago();
                soc.ObtenerEstablecimientoActual();
                soc.ObtenerComunaRegionLaboral();

                listaSocio.Add(soc);
            }
            return listaSocio;
        }



        public int AsignarFolioRegistro() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoFolio = db.SOCIO
                    .OrderByDescending(x => x.FolioRegistro)
                    .Select(x => x.FolioRegistro)
                    .FirstOrDefault();

                return ultimoFolio + 1;
            }
        }
    }
}
