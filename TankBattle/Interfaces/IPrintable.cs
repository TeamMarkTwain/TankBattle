namespace TankBattle.Interfaces
{
    public interface IPrintable
    {
        char[,] GetImage();

        FieldCoords GetTopLeft();
    }
}
