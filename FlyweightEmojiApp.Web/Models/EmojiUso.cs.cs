using FlyweightPattern.Interfaces;

namespace FlyweightEmojiApp.Models
{
    public class EmojiUso
    {
        public IEmoji? Emoji { get; set; }
        public int PosicionX { get; set; }
        public int PosicionY { get; set; }
        public string? Contexto { get; set; }
    }
}

