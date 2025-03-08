using System.Text.Json.Serialization;
using gk___projekt_1.Utilities;

namespace gk___projekt_1.Constraints
{
    internal class VerticalConstraint : EdgeConstraint
    {
        public VerticalConstraint(Edge edge) : base(edge) { }

        [JsonConstructor]
        public VerticalConstraint() : base(new Edge(new Vertex(0, 0), new Vertex(0, 0))) { }
        public override bool MaintainConstraint(Vertex moved)
        {
            Vertex b = moved == Edge.prev ? Edge.next : Edge.prev;
            if ((int)b.X == (int)moved.X)
            {
                b.FreedomDegree = FreedomDegree.Y;
                return true;
            }
            if (moved.FreedomDegree == FreedomDegree.Both || moved.FreedomDegree == FreedomDegree.X)
            {
                moved.X = b.X;
                b.FreedomDegree = FreedomDegree.Y;
                return true;
            }
            if (b.FreedomDegree == FreedomDegree.Both || b.FreedomDegree == FreedomDegree.X)
            {
                b.X = moved.X;
                b.FreedomDegree = FreedomDegree.Y;
                return true;
            }
            return false;
        }
    }
}
