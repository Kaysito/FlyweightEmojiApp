using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyweightPattern.Interfaces;

namespace Models
{
    // Representa el uso concreto de un emoji en la interfaz
    public class EmojiUso
    {
        public IEmoji? Emoji { get; set; }
        public int PosicionX { get; set; }
        public int PosicionY { get; set; }
        public string? Contexto { get; set; }
    }
}

