using System.Text.Json.Serialization;
using gk___projekt_1.Utilities;

namespace gk___projekt_1
{
    internal class BezierEdge : Edge
    {
        [JsonInclude]
        public ControlPoint ControlPointA;
        [JsonInclude]
        public ControlPoint ControlPointB;
        public BezierEdge(Vertex a, Vertex b) : base(a, b)
        {
            ControlPointA = new ControlPoint(this, a.X + 40.0f, a.Y + 40.0f);
            ControlPointB = new ControlPoint(this, b.X - 40.0f, b.Y - 40.0f);
        }
        public BezierEdge(Edge e) : this(e.prev, e.next) { }

        [JsonConstructor]
        public BezierEdge(ControlPoint controlPointA, ControlPoint controlPointB, Vertex prev, Vertex next)
        : base(prev, next)
        {
            ControlPointA = controlPointA;
            ControlPointB = controlPointB;
        }
        public override void DrawEdge(Bitmap bitmap)
        {
            using Graphics graphics = Graphics.FromImage(bitmap);
            switch (GraphicsSettings.BezierAlgorithm)
            {
                case (Algorithms.IterativeBesier): GraphicsExtensions.DrawBezierIterative(graphics, this);
                    break;
                default: graphics.DrawBezier(GraphicsSettings.EdgePen, prev, ControlPointA, ControlPointB, next);
                    break;
            }
            graphics.DrawLine(GraphicsSettings.BezierPen, prev, ControlPointA);
            graphics.DrawLine(GraphicsSettings.BezierPen, ControlPointA, ControlPointB);
            graphics.DrawLine(GraphicsSettings.BezierPen, ControlPointB, next);
            graphics.FillCircle(GraphicsSettings.ControlPointBrush, ControlPointA, GraphicsSettings.VertexRadius);
            graphics.FillCircle(GraphicsSettings.ControlPointBrush, ControlPointB, GraphicsSettings.VertexRadius);

        }
        public ControlPoint? FindClickedControlPoint(Point point)
        {
            if (ControlPointA.IsPointNear(point)) { return ControlPointA; }
            if (ControlPointB.IsPointNear(point)) { return ControlPointB; }
            return null;
        }
    }
}
