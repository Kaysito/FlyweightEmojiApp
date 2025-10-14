using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyweightPattern.Flyweights;
using FlyweightPattern.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace FlyweightPattern.Factories
{
    public class EmojiFactory
    {
        // Diccionario concurrente para cachear los emojis flyweight
        private readonly ConcurrentDictionary<string, IEmoji> _emojisEnMemoria = new();

        // Obtiene o crea un emoji flyweight compartido
        public IEmoji ObtenerEmoji(string simbolo)
        {
            // Si ya existe el emoji, lo devuelve (flyweight reutilizado)
            if (_emojisEnMemoria.TryGetValue(simbolo, out IEmoji emoji))
            {
                System.Diagnostics.Debug.WriteLine($"--- FLYWEIGHT REUTILIZADO: {simbolo} ---");
                return emoji;
            }

            // Si no existe, lo crea y lo agrega al diccionario
            var nuevoFlyweight = new EmojiCompartido(simbolo);
            _emojisEnMemoria.TryAdd(simbolo, nuevoFlyweight);
            System.Diagnostics.Debug.WriteLine($"--- FLYWEIGHT CREADO: {simbolo} ---");
            return nuevoFlyweight;
        }

        // Devuelve todos los emojis actualmente cacheados/compartidos
        public IEnumerable<IEmoji> ObtenerTodos()
        {
            return _emojisEnMemoria.Values;
        }

        // Cantidad de emojis únicos en memoria
        public int TotalEmojisEnMemoria => _emojisEnMemoria.Count;
        public IEnumerable<string> ObtenerSimbolosCacheados() => _emojisEnMemoria.Keys;
    }
}
