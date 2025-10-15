namespace FlyweightPattern.Interfaces
{
    public interface IEmoji
    {
        string Simbolo { get; }
        void Mostrar(int posicionX, int posicionY);
    }
}
