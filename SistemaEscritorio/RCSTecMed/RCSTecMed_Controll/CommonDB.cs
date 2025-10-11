using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RCSTecMed_Controll
{
    public class CommonDB
    {
        internal static void Synchronize(object origen, object destino)
        {
            if (origen == null || destino == null) return;

            Type tipoOrigen = origen.GetType();
            Type tipoDestino = destino.GetType();

            foreach (PropertyInfo propiedadOrigen in tipoOrigen.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                try
                {
                    PropertyInfo propiedadDestino = tipoDestino.GetProperty(propiedadOrigen.Name);
                    if (propiedadDestino != null && propiedadDestino.CanWrite)
                    {
                        object valor = propiedadOrigen.GetValue(origen);
                        if (valor == null || propiedadDestino.PropertyType.IsAssignableFrom(propiedadOrigen.PropertyType))
                        {
                            propiedadDestino.SetValue(destino, valor);
                        }
                    }
                }
                catch (Exception)
                {
                    // Puedes registrar el error si lo deseas
                    //Console.WriteLine($"Error al copiar propiedad '{propiedadOrigen.Name}': {ex.Message}");
                }
            }
        }
    }
}
