namespace TankBattle.Interfaces
{
    interface IPrinter
    {
        void EnqueueForPrinting(IPrintable obj);

        void PrintAll();

        void ClearQueue();
    }
}
