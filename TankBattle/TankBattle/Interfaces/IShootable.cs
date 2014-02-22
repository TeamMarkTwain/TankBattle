namespace TankBattle.Interfaces
{
    interface IShootable : IMoveable, IHitable, IDestroyable
    {
        // We can use "int" for speed and CurrentThread.Sleep(int)
        FieldCoords Speed { get; }

        int ShootPower { get; }
    }
}
