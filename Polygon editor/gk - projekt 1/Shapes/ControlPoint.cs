using System.Text.Json.Serialization;
using gk___projekt_1.Utilities;

namespace gk___projekt_1
{
    internal class ControlPoint : Vertex
    {
        [JsonIgnore]
        public BezierEdge BeziereEdge { get; }
        public ControlPoint(BezierEdge edge, float x, float y) : base(x, y) { BeziereEdge = edge; }

        [JsonConstructor]
        public ControlPoint(float x, float y, int id) : base(x, y, id)
        {
            this.BeziereEdge = new BezierEdge(new Edge(new Vertex(0, 0), new Vertex(0, 0)));
        }
        public override void DrawVertex(Graphics graphic)
        {
            graphic.FillCircle(GraphicsSettings.ControlPointBrush, this, GraphicsSettings.VertexRadius);
        }
    }
}
