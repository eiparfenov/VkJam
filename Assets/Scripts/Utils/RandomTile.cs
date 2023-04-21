using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;

namespace Utils
{
    [CreateAssetMenu(menuName = "VkJam/Utils/RandomTile")]
    public class RandomTile : ScriptableObject
    {
        [SerializeField] private TileBase[] tiles;
        public TileBase Tile => tiles.RandomOrDefault();
    }
}