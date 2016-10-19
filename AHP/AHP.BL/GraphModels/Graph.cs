using AHP.BL.Interfaces;
using QuickGraph;

namespace AHP.BL.GraphModels
{
    public class Graph : BidirectionalGraph<IVertex, Edge>
    {
        public Graph() { }

        public Graph(bool allowParallelEdges)
            : base(allowParallelEdges) { }

        public Graph(bool allowParallelEdges, int vertexCapacity)
            : base(allowParallelEdges, vertexCapacity) { }
    }
}