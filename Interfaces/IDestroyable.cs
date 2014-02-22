
namespace TankBattle.Interfaces
{
    interface IDestroyable : IHitable
    {
        void LooseHealth(int amount);

        bool IsDestroyed { get; }

        int Health { get; }
        // void Update();
    }
}
