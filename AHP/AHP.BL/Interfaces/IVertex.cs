namespace AHP.BL.Interfaces
{
    public interface IVertex
    {
        string Value { get; set; }
        double Weight { get; set; }
        int Level { get; }
    }
}
