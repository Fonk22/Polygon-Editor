using System.Text.Json;
using System;
using gk___projekt_1.Constraints;
using gk___projekt_1.Utilities;
using System.Text.Json.Serialization;

namespace gk___projekt_1
{
    internal class Vertex
    {
        private static int _maxId = 0;
        private event Action VertexConstraintAdded;
        private event Action<Vertex, (int, int)> PositionChanged;

        [JsonInclude]
        public float X;
        [JsonInclude]
        public float Y;
        [JsonInclude]
        public readonly int Id;
        public FreedomDegree FreedomDegree;
    
        public Edge? PrevEdge;
        public Edge? NextEdge;

        [JsonInclude]
        public VertexConstraint? Constraint { get; private set; }

        public Vertex(float x, float y)
        {
            X = x;
            Y = y;
            PrevEdge = null;
            NextEdge = null;
            PositionChanged = PolygonIntegrityGuard.VertexMovedHandler;
            VertexConstraintAdded += PolygonIntegrityGuard.PolygonStructureChangedHandler;
            Id = _maxId++;
        }

        [JsonConstructor]
        public Vertex(float x, float y, int id)
        {
            this.X = x;
            this.Y = y;
            this.Id = id;
            PositionChanged = PolygonIntegrityGuard.VertexMovedHandler;
            VertexConstraintAdded += PolygonIntegrityGuard.PolygonStructureChangedHandler;
        }
        public Vertex(Point point) : this(point.X, point.Y) { }

        public float Distance(Point p)
        {
            float dx = p.X - X;
            float dy = p.Y - Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
        public void AddConstraint(VertexConstraint constraint)
        {
            Constraint = constraint;
            OnVertexConstraintAdded();
        }
        public void RemoveConstraint() { Constraint = null; }
        public bool IsPointNear(Point p) { return Distance(p) < GraphicsSettings.PointComparisonThreshold; }
        public virtual void DrawVertex(Graphics graphics)
        { 
            graphics.FillCircle(GraphicsSettings.VertexBrush, this, GraphicsSettings.VertexRadius);
            switch (Constraint)
            {
                case G1ContinuityConstraint:
                    graphics.DrawString("G1", GraphicsSettings.ContinuityLabelFont,
                        GraphicsSettings.ContinuityLabelBrush, X - 15, Y-11);
                    break;
                case C1ContinuityConstraint:
                    graphics.DrawString("C1", GraphicsSettings.ContinuityLabelFont,
                        GraphicsSettings.ContinuityLabelBrush, X - 15, Y - 11);
                    break;
            }
        }
        public virtual void MoveVertex((int x, int y) vector)
        {
            X += vector.x;
            Y += vector.y;
            OnVertexMoved(vector);
        }
        public void SetPosition((float x, float y) position)
        {
            X = position.x;
            Y = position.y;
        }
        private void OnVertexMoved((int, int) vector) { PositionChanged.Invoke(this, vector); }
        private void OnVertexConstraintAdded() { VertexConstraintAdded.Invoke(); }
    }
}
