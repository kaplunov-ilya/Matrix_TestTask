using System.Collections.Generic;

namespace Game.Gameplay.Data
{
    public sealed class FindedOffsetsData
    {
        public List<CubeData> Space { get; set; }
        public List<CubeData> Model { get; set; }
        
        public HashSet<string> SpaceSet { get; set; }
    }
}