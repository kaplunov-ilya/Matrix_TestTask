using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Game.Core.Parser.Data;
using Game.Gameplay.Data;
using UnityEngine;

namespace Game.Gameplay.Managers
{
    public sealed class MatrixOffsetsManager
    {
        private readonly CubeCreator _creator;
        private readonly MatrixOffsetFinder _finder;
        private readonly List<Matrix4x4> _space;
        private readonly List<Matrix4x4> _model;
        private readonly FindedOffsetsData _data;
        private readonly VisualConfig _config;
        private readonly CubeVisualizePresenter _presenter;

        private HashSet<string> _spaceSet;

        public MatrixOffsetsManager(List<Matrix4x4> space,
                                    List<Matrix4x4> model, 
                                    CubeCreator creator,
                                    MatrixOffsetFinder finder,
                                    FindedOffsetsData data,
                                    VisualConfig config,
                                    CubeVisualizePresenter presenter)
        {
            _space = space;
            _model = model;
            _data = data;

            _creator = creator;
            _finder = finder;
            _config = config;
            _presenter = presenter;
        }

        public async UniTask Start()
        {
            _data.Space = _creator.CreateMatrix("Space_Parent", _space, _config.SpaceColor);
            _data.Model = _creator.CreateMatrix("Model_Parent", _model, _config.ModelColor);
            
            _presenter.Start();
            
            Debug.Log($"StartFind");
            
            var offsets = await _finder.StartFind();

            Export(offsets);
            
            Debug.Log($"Found offsets: {offsets.Count}");
        }
        
        
        void Export(List<Matrix4x4> offsets)
        {
            List<MatrixData> data = new List<MatrixData>();

            foreach (var o in offsets)
            {
                data.Add(new MatrixData(o));
            }

            string json = JsonUtility.ToJson(new Wrapper { items = data }, true);
            File.WriteAllText(Application.dataPath + "/offsets.json", json);
        }
        
        [System.Serializable]
        public class Wrapper
        {
            public List<MatrixData> items;
        }
    }
}