using FlyweightPattern.Flyweights;
using FlyweightPattern.Interfaces;
using System.Collections.Concurrent;

namespace FlyweightPattern.Factories
{
    public class EmojiFactory
    {
        private readonly ConcurrentDictionary<string, IEmoji> _emojisEnMemoria = new();

        public IEmoji ObtenerEmoji(string simbolo)
        {
            if (_emojisEnMemoria.TryGetValue(simbolo, out IEmoji emoji))
            {
                System.Diagnostics.Debug.WriteLine($"--- FLYWEIGHT REUTILIZADO: {simbolo} ---");
                return emoji;
            }

            var nuevoFlyweight = new EmojiCompartido(simbolo);
            _emojisEnMemoria.TryAdd(simbolo, nuevoFlyweight);
            System.Diagnostics.Debug.WriteLine($"--- FLYWEIGHT CREADO: {simbolo} ---");
            return nuevoFlyweight;
        }

        public IEnumerable<IEmoji> ObtenerTodos()
        {
            return _emojisEnMemoria.Values;
        }

        public int TotalEmojisEnMemoria => _emojisEnMemoria.Count;
        public IEnumerable<string> ObtenerSimbolosCacheados() => _emojisEnMemoria.Keys;
    }
}
