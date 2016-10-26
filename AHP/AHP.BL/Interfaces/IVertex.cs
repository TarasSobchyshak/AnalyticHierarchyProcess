using AHP.BL.Models;

namespace AHP.BL.Interfaces
{
    public interface IVertex
    {
        PairwiseComparisonMatrix PCM { get; set; } 
        string Value { get; set; }
        double Weight { get; set; }
        int Level { get; }
        double Index { get; set; }
    }
}
