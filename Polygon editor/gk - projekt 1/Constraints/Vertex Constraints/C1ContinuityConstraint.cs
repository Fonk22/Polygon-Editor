using System.Numerics;
using System.Text.Json.Serialization;
using gk___projekt_1.Utilities;

namespace gk___projekt_1.Constraints
{
    internal class C1ContinuityConstraint : VertexConstraint
    {
        public C1ContinuityConstraint(Vertex vertex) : base(vertex) { }

        [JsonConstructor]
        public C1ContinuityConstraint() : base(new Vertex(0, 0)) { }
        public override Vertex? MaintainConstraint()
        {
            Edge? prevEdge = ConstrainedVertex.PrevEdge;
            Edge? nextEdge = ConstrainedVertex.NextEdge;
            if (nextEdge is null || prevEdge is null) { return null; }
            if (prevEdge is not BezierEdge && nextEdge is not BezierEdge) { return null; }

            Vertex v1 = ConstrainedVertex;
            Vertex v0 = prevEdge is BezierEdge bezEdge ? bezEdge.ControlPointB : prevEdge.prev;
            Vertex v2 = nextEdge is BezierEdge bezEdge2 ? bezEdge2.ControlPointA : nextEdge.next;

            if (v2 is not ControlPoint) { (v0, v2) = (v2, v0); }
            if (v2.FreedomDegree != FreedomDegree.Both) { (v0, v2) = (v2, v0); }

            float dx = v1.X - v0.X;
            float dy = v1.Y - v0.Y;
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);
            float ratio = v2 is ControlPoint ? v0 is ControlPoint ? 1.0f : 1.0f / 3.0f : 3.0f;

            Vector2 VectorToTarget = new Vector2(dx, dy);
            VectorToTarget = Vector2.Normalize(VectorToTarget);
            VectorToTarget *= dist * ratio;
            if (float.IsNaN(VectorToTarget.X)) { return null; }

            v2.X = v1.X + VectorToTarget.X;
            v2.Y = v1.Y + VectorToTarget.Y;

            v1.FreedomDegree = FreedomDegree.None;
            v2.FreedomDegree = FreedomDegree.None;
            return v2;
        }
    }
}
