using FlyweightPattern.Interfaces;

namespace FlyweightPattern.Flyweights
{
    public class EmojiCompartido : IEmoji
    {
        public string Simbolo { get; private set; }
        public EmojiCompartido(string simbolo)
        {
            Simbolo = simbolo;
        }
        public void Mostrar(int posicionX, int posicionY)
        {
            System.Console.WriteLine($"Emoji {Simbolo} en ({posicionX}, {posicionY})");
        }
    }
}


