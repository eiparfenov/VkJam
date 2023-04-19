using Cysharp.Threading.Tasks;
using Signals;
using Zenject;

namespace Inventory
{
    public class TempInventoryOpener: IInitializable
    {
        private readonly SignalBus _signalBus;

        public TempInventoryOpener(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public async void Initialize()
        {
            await UniTask.Delay(1000);
            _signalBus.AbstractFire<TempInventoryOpenSignal>();
        }
    }
}