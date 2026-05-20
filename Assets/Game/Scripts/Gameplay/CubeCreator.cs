using System.Collections.Generic;
using Game.Gameplay.Data;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class CubeCreator
    {
        private readonly CubeAsset _asset;

        public CubeCreator(CubeAsset  asset)
        {
            _asset = asset;
        }
        
        public List<CubeData> CreateMatrix(string nameParent, List<Matrix4x4> list, Color color)
        {
            var cubeAssets = new List<CubeData>(list.Count);
            
            var parent = new GameObject(nameParent);

            foreach (var matrix in list)
            {
                var cubeData = new CubeData
                {
                    Asset = Object.Instantiate(_asset,
                                               matrix.Position(),
                                               matrix.Rotation(), 
                                               parent: parent.transform),
                    Matrix4X4 = matrix,
                };

                cubeData.Asset.MeshRenderer.material.color = color;
                
                cubeAssets.Add(cubeData);
            }
            
            return cubeAssets;
        }
    }
}