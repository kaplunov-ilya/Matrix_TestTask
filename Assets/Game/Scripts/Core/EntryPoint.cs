using Cysharp.Threading.Tasks;
using Game.Core.Parser;
using Game.Gameplay;
using Game.Gameplay.Data;
using Game.Gameplay.Managers;
using UnityEngine;

namespace Game.Core
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private TextAsset _modelAsset;
        [SerializeField] private TextAsset _spaceAsset;

        [SerializeField] private CubeAsset _prefab;
        [SerializeField] private VisualConfig _config;
        
        private void Start()
        {
            var model = MatrixParser.LoadMatrices(_modelAsset);
            var space = MatrixParser.LoadMatrices(_spaceAsset);

            Debug.Log($"Model count: {model.Count}");
            Debug.Log($"Space count: {space.Count}");

            var data = new FindedOffsetsData();
            
            var manager = new MatrixOffsetsManager(space,
                                                   model, 
                                                   new CubeCreator(_prefab),
                                                   new MatrixOffsetFinder(data), 
                                                   data,
                                                   _config,
                                                   new CubeVisualizePresenter(data, _config));
            manager.Start().Forget();
        }
    }
}