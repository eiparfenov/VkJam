namespace Signals
{
    public interface IInventoryOpenSignal: ISignal
    {
        bool InventoryOpened { get; }
    }
}