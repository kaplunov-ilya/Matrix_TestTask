using System.Collections.Generic;
using Game.Core.Parser.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Core.Parser
{
    public static class MatrixParser
    {
        public static List<Matrix4x4> LoadMatrices(TextAsset asset)
        {
            string json = asset.text;

            List<MatrixData> data =
                JsonConvert.DeserializeObject<List<MatrixData>>(json);

            List<Matrix4x4> matrices = new();

            foreach (var item in data)
            {
                matrices.Add(item.ToMatrix());
            }

            return matrices;
        }
    }
}