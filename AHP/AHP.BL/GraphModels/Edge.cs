using AHP.BL.Interfaces;
using QuickGraph;

namespace AHP.BL.GraphModels
{
    public class Edge : Edge<IVertex>
    {
        public string Id { get; protected set; }

        public Edge(IVertex source, IVertex target) : base(source, target)
        {
            Id = $"{source.Value} -> {target.Value}";
        }
    }
}
