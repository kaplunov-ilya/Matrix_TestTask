using System;
using System.Collections.Generic;
using Game.Gameplay.Data;

namespace Game.Gameplay
{
    public sealed class CubeVisualizePresenter : IDisposable
    {
        private readonly FindedOffsetsData _data;
        private readonly VisualConfig _config;

        private Dictionary<CubeData, Action<bool>> _handlers;

        public CubeVisualizePresenter(FindedOffsetsData data, VisualConfig config)
        {
            _data = data;
            _config = config;
        }

        public void Start()
        {
            _handlers = new(_data.Space.Count); 
            
            foreach (var model in _data.Space)
            {
                Action<bool> handler = (result) =>
                {
                    ModelValidChanged(model, result);
                };
                
                model.OnValid += handler;
                
                _handlers.Add(model, handler);
            }
        }

        private void ModelValidChanged(CubeData data, bool isValid)
        {
            if(!data.Asset)
                return;
            
            var color = isValid ? _config.ValidColor : _config.UnvalidColor;
            data.Asset.MeshRenderer.material.color = color;
        }


        public void Dispose()
        {
            foreach (var model in _handlers)
            {
                model.Key.OnValid -= model.Value;
            }
        }
    }
}