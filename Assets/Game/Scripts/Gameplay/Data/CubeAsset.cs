using UnityEngine;

namespace Game.Gameplay.Data
{
    public sealed class CubeAsset : MonoBehaviour
    {
        [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
    }
}