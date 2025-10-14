using FlyweightPattern.Interfaces;
using Models;
using System.Collections.Generic;

namespace FlyweightEmojiApp.Models
{
    public class ReporteViewModel
    {
        // Lista de los usos concretos de emojis (puede representar los mensajes del usuario)
        public IEnumerable<EmojiUso> UsosDeEmojis { get; set; } = new List<EmojiUso>();

        // Total de Flyweights únicos en memoria
        public int TotalFlyweightsEnMemoria { get; set; }

        // Lista de símbolos (flyweights) actualmente en cache
        public IEnumerable<string> FlyweightsCacheados { get; set; } = new List<string>();

        // Lista general de todos los emojis disponibles, si es que tienes un "catálogo"
        public IEnumerable<IEmoji> EmojisDisponibles { get; set; } = new List<IEmoji>();
    }
}
