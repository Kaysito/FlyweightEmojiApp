using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweightPattern.Interfaces
{
    // Contrato para el Flyweight de emojis
    public interface IEmoji
    {
        // Carácter o imagen del emoji
        string Simbolo { get; }

        // Operación para "mostrar" el emoji (simulación)
        void Mostrar(int posicionX, int posicionY);
    }
}
