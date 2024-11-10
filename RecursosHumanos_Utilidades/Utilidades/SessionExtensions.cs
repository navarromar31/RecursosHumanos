using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Ferretero_Utilidades
{
    public static class SessionExtensions
    {
        // Método de extensión para establecer un valor en la sesión
        public static void Set<T>(this ISession session, string key, T value)
        {
            // Serializa el valor a JSON y almacénalo en la sesión
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Método de extensión para obtener un valor de la sesión
        public static T Get<T>(this ISession session, string key)
        {
            // Recupera el valor serializado de la sesión
            var value = session.GetString(key);

            // Deserializa el valor desde JSON y devuélvelo
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
