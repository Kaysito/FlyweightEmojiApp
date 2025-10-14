using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyweightPattern.Interfaces;

namespace FlyweightPattern.Flyweights
{
    // Implementa el Flyweight para emojis compartidos
    public class EmojiCompartido : IEmoji
    {
        public string Simbolo { get; private set; }

        public EmojiCompartido(string simbolo)
        {
            Simbolo = simbolo;
        }

        public void Mostrar(int posicionX, int posicionY)
        {
            // Ejemplo simple de “mostrar” el emoji y su posición
            System.Console.WriteLine($"Emoji {Simbolo} en ({posicionX}, {posicionY})");
        }
    }
}


