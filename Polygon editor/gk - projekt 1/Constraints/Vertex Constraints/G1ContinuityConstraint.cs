using System.Numerics;
using System.Text.Json.Serialization;
using gk___projekt_1.Utilities;

namespace gk___projekt_1.Constraints
{
    internal class G1ContinuityConstraint : VertexConstraint
    {
        public G1ContinuityConstraint(Vertex vertex) : base(vertex) { }

        [JsonConstructor]
        public G1ContinuityConstraint() : base(new Vertex(0, 0)) { }
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

            float dist;
            Edge? edge = v1.NextEdge == v2.PrevEdge ? v1.NextEdge : v2.NextEdge;
            if (edge is not null && edge.Constraint is FixedLengthConstraint flconst)
            {
                dist = flconst.Length;
            }
            else
            {
                float dx = v2.X - v1.X;
                float dy = v2.Y - v1.Y;
                dist = (float)Math.Sqrt(dx * dx + dy * dy);
            }

            Vector2 VectorToTarget = new Vector2(v1.X - v0.X, v1.Y - v0.Y);
            VectorToTarget = Vector2.Normalize(VectorToTarget);
            VectorToTarget *= dist;
            if (float.IsNaN(VectorToTarget.X)) { return null; }
            v2.X = (v1.X + VectorToTarget.X);
            v2.Y = (v1.Y + VectorToTarget.Y);

            if (edge?.Constraint is HorizontalConstraint)
            {
                v1.FreedomDegree = FreedomDegree.X;
                v2.FreedomDegree = FreedomDegree.X;
            }
            else if (edge?.Constraint is VerticalConstraint)
            {
                v1.FreedomDegree = FreedomDegree.Y;
                v2.FreedomDegree = FreedomDegree.Y;
            }
            else if (edge is not null)
            {
                v1.FreedomDegree = FreedomDegree.None;
                v2.FreedomDegree = FreedomDegree.None;
            }

            return v2;
        }
    }
}
