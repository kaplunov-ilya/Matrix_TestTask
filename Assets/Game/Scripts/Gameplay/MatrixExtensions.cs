using UnityEngine;

namespace Game.Gameplay
{
    public static class MatrixExtensions
    {
        public static Vector3 Position(this Matrix4x4 m)
        {
            return m.GetColumn(3);
        }

        public static Quaternion Rotation(this Matrix4x4 m)
        {
            return m.rotation;
        }
    }
}