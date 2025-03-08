using System.Text.Json.Serialization;
using gk___projekt_1.Constraints;
using gk___projekt_1.Utilities;

namespace gk___projekt_1
{
    [JsonDerivedType(typeof(BezierEdge), "BezierEdge")]
    internal class Edge
    {
        private event Action<Edge> EdgeConstraintAdded;
        [JsonInclude]
        public Vertex prev;
        [JsonInclude]
        public Vertex next;

        [JsonInclude]
        public EdgeConstraint? Constraint { get; private set; }
        public Edge(Vertex a, Vertex b)
        {
            prev = a;
            next = b;
            EdgeConstraintAdded = PolygonIntegrityGuard.EdgeConstraintAddedHandler;
        }

        [JsonConstructor]
        public Edge(Vertex prev, Vertex next, EdgeConstraint? constraint = null)
        {
            this.prev = prev;
            this.next = next;
            this.Constraint = constraint;
            EdgeConstraintAdded = PolygonIntegrityGuard.EdgeConstraintAddedHandler;
        }
        public void AddConstraint(EdgeConstraint constraint)
        {
            Constraint = constraint;
            OnEdgeConstraintAdded();
        }
        public void RemoveConstraint() { Constraint = null; }
        public float Distance(Point p)
        {
            float x0, y0;
            float x1, y1;
            float x2, y2;
            (x0, y0) = (p.X, p.Y);
            (x1, y1) = (prev.X, prev.Y);
            (x2, y2) = (next.X, next.Y);
            if (x0 <= Math.Max(x1, x2) + 10 && x0 >= Math.Min(x1, x2) - 10 && 
                y0 <= Math.Max(y1, y2) + 10 && y0 >= Math.Min(y1, y2) - 10)
            {
                return Math.Abs((y2 - y1) * x0 - (x2 - x1) * y0 + x2 * y1 - y2 * x1) /
                    (float)Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
            }
            return float.MaxValue;
        }
        public virtual void DrawEdge(Bitmap bitmap)
        {
            using Graphics graphics = Graphics.FromImage(bitmap);
            switch (GraphicsSettings.LineAlgorithm)
            {
                case Algorithms.Bresenham: GraphicsExtensions.DrawLineBresenham(bitmap, prev, next);
                    break;
                default: graphics.DrawLine(GraphicsSettings.EdgePen, prev, next);
                    break;
            }

            Point p = GetMiddlePoint();
            p.X -= 18;
            p.Y -= 18;
            switch (Constraint)
            {
                case (HorizontalConstraint): graphics.DrawImage(GraphicsSettings.HorizontalLineIcon, p);
                    break;
                case (VerticalConstraint): graphics.DrawImage(GraphicsSettings.VerticalLineIcon, p);
                    break;
                case (FixedLengthConstraint): graphics.DrawImage(GraphicsSettings.FixedLengthLineIcon, p);
                    break;
            }
        }
        public Point GetMiddlePoint()
        {
            float x = (prev.X + next.X) / 2;
            float y = (prev.Y + next.Y) / 2;
            return new Point((int)x, (int)y);
        }
        public float GetLength()
        {
            float dx = prev.X - next.X;
            float dy = prev.Y - next.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
        private void OnEdgeConstraintAdded() { EdgeConstraintAdded.Invoke(this); }
    }
}
