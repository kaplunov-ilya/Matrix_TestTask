using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Data;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class MatrixOffsetFinder
    {
        private readonly FindedOffsetsData _data;
 
        public MatrixOffsetFinder(FindedOffsetsData data)
        {
            _data = data;
        }

        public UniTask<List<Matrix4x4>> StartFind()
        {
            BuildSpaceSet();
            
            return FindOffsets();
        }

        private void BuildSpaceSet()
        {
            _data.SpaceSet = new HashSet<string>(_data.Space.Count);

            foreach (var m in _data.Space)
                _data.SpaceSet.Add(Key(m));
        }
        
        private async UniTask<List<Matrix4x4>> FindOffsets()
        {
            List<Matrix4x4> result = new List<Matrix4x4>();

            int counter = 0;

            for (int i = 0; i < _data.Model.Count; i++)
            {
                var m = _data.Model[i];
                Matrix4x4 invM = m.Matrix4X4.inverse;

                for (int j = 0; j < _data.Space.Count; j++)
                {
                    var s = _data.Space[j];

                    Matrix4x4 offset = s.Matrix4X4 * invM;

                    bool isValid = IsValidOffset(offset);
                    
                    if (isValid)
                    {
                        result.Add(offset);
                        s.OnValid?.Invoke(true);
                    }

                    if (++counter < 50) 
                        continue;
                    
                    counter = 0;
                    await UniTask.Yield(PlayerLoopTiming.Update);
                }
            }

            return result;
        }

        private bool IsValidOffset(Matrix4x4 offset)
        {
            for (int i = 0; i < _data.Model.Count; i++)
            {
                var transformed = offset * _data.Model[i].Matrix4X4;

                if (!_data.SpaceSet.Contains(Key(transformed)))
                    return false;
            }

            return true;
        }

        private string Key(Matrix4x4 m)
        {
            return $"{R(m.m00)}_{R(m.m01)}_{R(m.m02)}_{R(m.m03)}_" +
                   $"{R(m.m10)}_{R(m.m11)}_{R(m.m12)}_{R(m.m13)}_" +
                   $"{R(m.m20)}_{R(m.m21)}_{R(m.m22)}_{R(m.m23)}_" +
                   $"{R(m.m30)}_{R(m.m31)}_{R(m.m32)}_{R(m.m33)}";
        }

        private string Key(CubeData data)
        {
            var m = data.Matrix4X4;

            return Key(m);
        }

        private float R(float v) => Mathf.Round(v * 1000f) / 1000f;
    }
}