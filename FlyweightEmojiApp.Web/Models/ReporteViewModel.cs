using FlyweightPattern.Interfaces;

namespace FlyweightEmojiApp.Models
{
    public class ReporteViewModel
    {
        public IEnumerable<EmojiUso> UsosDeEmojis { get; set; } = new List<EmojiUso>();

        public int TotalFlyweightsEnMemoria { get; set; }

        public IEnumerable<string> FlyweightsCacheados { get; set; } = new List<string>();

        public IEnumerable<IEmoji> EmojisDisponibles { get; set; } = new List<IEmoji>();
    }
}
