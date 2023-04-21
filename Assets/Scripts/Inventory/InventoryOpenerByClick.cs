using System;
using Signals;
using UnityEngine;
using Zenject;

namespace Inventory
{
    public class InventoryOpenerByClick: MonoBehaviour
    {
        private SignalBus _signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnMouseDown()
        {
            _signalBus.AbstractFire<TempInventoryOpenSignal>();
        }
    }
}