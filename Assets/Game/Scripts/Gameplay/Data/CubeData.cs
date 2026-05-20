using System;
using UnityEngine;

namespace Game.Gameplay.Data
{
    public sealed class CubeData
    {
        public CubeAsset Asset { get; set; }
        public Matrix4x4 Matrix4X4 { get; set; }
        
        public Action<bool> OnValid;
    }
}