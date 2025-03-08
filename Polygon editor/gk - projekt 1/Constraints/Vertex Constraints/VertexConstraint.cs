using System.Text.Json.Serialization;
using gk___projekt_1.Constraints;

namespace gk___projekt_1
{
    [JsonDerivedType(typeof(G1ContinuityConstraint), "G1ContinuityConstraint")]
    [JsonDerivedType(typeof(C1ContinuityConstraint), "C1ContinuityConstraint")]
    internal abstract class VertexConstraint
    {
        public Vertex ConstrainedVertex;
        public VertexConstraint(Vertex vertex) { ConstrainedVertex = vertex; }
        public abstract Vertex? MaintainConstraint();
    }
}
