using UnityEngine;

namespace Game.Gameplay.Data
{
    [CreateAssetMenu(fileName = "VisualConfig", menuName = "Config/VisualConfig")]
    public sealed class VisualConfig : ScriptableObject
    {
        [field: SerializeField] public Color SpaceColor { get; private set; } = Color.gray;
        [field: SerializeField] public Color ModelColor { get; private set; } = Color.deepSkyBlue;
        [field: SerializeField] public Color ValidColor { get; private set; } = Color.green;
        [field: SerializeField] public Color UnvalidColor { get; private set; } = Color.red;
    }
}