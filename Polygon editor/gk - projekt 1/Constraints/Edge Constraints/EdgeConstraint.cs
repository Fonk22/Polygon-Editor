using System.Text.Json.Serialization;
using gk___projekt_1.Constraints;

namespace gk___projekt_1
{
    [JsonDerivedType(typeof(FixedLengthConstraint), "FixedLengthConstraint")]
    [JsonDerivedType(typeof(HorizontalConstraint), "HorizontalConstraint")]
    [JsonDerivedType(typeof(VerticalConstraint), "VerticalConstraint")]
    internal abstract class EdgeConstraint
    {
        public Edge Edge;
        public EdgeConstraint(Edge edge) { Edge = edge; }
        public abstract bool MaintainConstraint(Vertex moved);

    }
}
