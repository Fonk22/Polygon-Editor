using System.Text.Json.Serialization;
using gk___projekt_1.Utilities;

namespace gk___projekt_1
{
    internal class HorizontalConstraint : EdgeConstraint
    {
        public HorizontalConstraint(Edge edge) : base(edge) { }

        [JsonConstructor]
        public HorizontalConstraint() : base(new Edge(new Vertex(0, 0), new Vertex(0, 0))) { }
        public override bool MaintainConstraint(Vertex moved)
        {
            Vertex b = moved == Edge.next ? Edge.prev : Edge.next;
            if ((int)moved.Y == (int)b.Y)
            {
                b.FreedomDegree = FreedomDegree.X;
                return true;
            }
            if (moved.FreedomDegree == FreedomDegree.Both || moved.FreedomDegree == FreedomDegree.Y)
            {
                moved.Y = b.Y;
                b.FreedomDegree = FreedomDegree.X;
                return true;
            }
            if (b.FreedomDegree == FreedomDegree.Both || b.FreedomDegree == FreedomDegree.Y)
            {
                b.Y = moved.Y;
                b.FreedomDegree = FreedomDegree.X;
                return true;
            }
            return false;
        }
    }
}
