
namespace TankBattle.Interfaces
{
    interface IDestroyable : IHitable
    {
        void LooseHealth(int amount);
        bool IsDestroyed { get; }

        // void Update();
    }
}
