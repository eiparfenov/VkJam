using System;
using Installers.Inventory;
using UnityEngine.UI;
using Zenject;

namespace Inventory
{
    public class InventoryCellUI
    {
        public class Factory: PlaceholderFactory<InventoryCellUI> { }
        private readonly Image _image;
        private readonly IInventoryCellUiSettings _settings;
        
        public InventoryCellUI(Image image, IInventoryCellUiSettings settings)
        {
            _settings = settings;
            _image = image;
            CellState = State.Off;
        }

        public State CellState
        {
            get => _state;
            set
            {
                if (value == _state) return;
                _state = value;

                _image.color = value switch
                {
                    State.CanPlace => _settings.CanPlace,
                    State.CantPlace => _settings.CantPlace,
                    State.Off => _settings.Clear,
                    _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
                };
            }
        }

        private State _state;

        public enum State
        {
            CanPlace, CantPlace, Off
        }
    }
}