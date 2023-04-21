namespace Signals
{
    public class TempInventoryOpenSignal: IInventoryOpenSignal
    {
        public bool InventoryOpened => true;
    }
}