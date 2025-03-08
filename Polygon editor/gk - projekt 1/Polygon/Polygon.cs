using System;
using System.Drawing.Drawing2D;
using System.Text.Json;
using System.Text.Json.Serialization;
using gk___projekt_1.Constraints;
using gk___projekt_1.Utilities;

namespace gk___projekt_1
{
    internal class Polygon
    {
        private readonly Action PolygonStructureChanged;

        [JsonInclude]
        internal List<Vertex> Vertices;

        [JsonInclude]
        internal List<Edge> Edges;
        public bool IsClosed { get; private set; }

        [JsonIgnore]
        public int VertexCount { get { return Vertices.Count; } }

        [JsonIgnore]
        public Vertex GetFirst { get { return Vertices[0]; } }

        [JsonIgnore]
        public Vertex GetLast { get { return Vertices[VertexCount - 1]; } }

        public Vertex GetVertex(int index) { return Vertices[index]; }

        public Polygon()
        {
            PolygonStructureChanged = PolygonIntegrityGuard.PolygonStructureChangedHandler;
            PolygonIntegrityGuard.polygon = this;
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
            IsClosed = false;
        }

        private void AddEdge(Vertex a, Vertex b)
        {
            Edge edge = new(a, b);
            a.NextEdge = edge;
            b.PrevEdge = edge;
            Edges.Add(edge);
        }
        public void AddVertex(Point p)
        {
            if (IsClosed) { return; }
            if (VertexCount > 2 && GetFirst.IsPointNear(p))
            {
                AddEdge(GetLast, GetFirst);
                IsClosed = true;
                return;
            }
            Vertex v = new Vertex(p);
            if (Vertices.Count == 0)
            {
                Vertices.Add(v);
                return;
            }
            AddEdge(GetLast, v);
            Vertices.Add(v);
            return;
        }
        public object? FindeClickedObject(Point point)
        {
            foreach (Vertex v in Vertices) { if (v.IsPointNear(point)) return v; }
            foreach (BezierEdge bezier in Edges.OfType<BezierEdge>())
            {
                if (bezier.FindClickedControlPoint(point) is ControlPoint controlPoint) { return controlPoint; }
            }
            foreach (Edge edge in Edges.Where((e)=>e is not BezierEdge))
            {
                if (edge.Distance(point) < GraphicsSettings.PointComparisonThreshold) { return edge; }
            }
            if (IsPointInsidePolygon(point)) return this;
            return null;
        }
        public void Render(Bitmap bitmap)
        {
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(GraphicsSettings.BackgroundColor);

            foreach (Edge e in Edges) { e.DrawEdge(bitmap); }
            foreach (Vertex v in Vertices) { v.DrawVertex(graphics); }
        }
        public void RemoveVertex(Vertex vertex)
        {
            if (!Vertices.Remove(vertex)) { return; }
            if (VertexCount < 3)
            {
                RemovePolygon();
                return;
            }
            if (vertex.PrevEdge is null || vertex.NextEdge is null) { return; }
            Vertex a = vertex.PrevEdge.prev;
            Vertex b = vertex.NextEdge.next;
            Edge newEdge = new Edge(a, b);
            if(a.PrevEdge is not BezierEdge) { a.RemoveConstraint(); }
            if(b.NextEdge is not BezierEdge) { b.RemoveConstraint(); }
            Edges.Remove(vertex.PrevEdge);
            Edges.Remove(vertex.NextEdge);
            Edges.Add(newEdge);
            a.NextEdge = newEdge;
            b.PrevEdge = newEdge;
            OnPolygonStructureChanged();
        }
        public void RemoveEdge(Edge e)
        {
            if (!Edges.Contains(e)) { return; }
            RemoveVertex(e.prev);
            RemoveVertex(e.next);
            OnPolygonStructureChanged();
        }
        public bool IsPointInsidePolygon(Point p)
        {
            if (!IsClosed) { return false; }
            using GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddPolygon(Vertices.Select(v => new Point((int)v.X, (int)v.Y)).ToArray());
            return graphicsPath.IsVisible(p);
        }
        public void MovePolygon((int x, int y) vector)
        {
            PolygonIntegrityGuard.IsActive = false;
            foreach (Vertex vertex in Vertices) { vertex.MoveVertex(vector); }
            foreach (BezierEdge bezier in Edges.OfType<BezierEdge>())
            {
                bezier.ControlPointA.MoveVertex(vector);
                bezier.ControlPointB.MoveVertex(vector);
            }
            PolygonIntegrityGuard.IsActive = true;
        }
        public void SplitEdge(Edge edge)
        {
            if (!Edges.Remove(edge)) { return; }

            Vertex a = edge.prev;
            Vertex b = edge.next;
            Vertex c = new Vertex(edge.GetMiddlePoint());

            int i = Vertices.FindIndex((v) => v.Id == a.Id);
            if (i == -1) { return; }
            Vertices.Insert(i + 1, c);
            Edge e1 = new Edge(a, c);
            Edge e2 = new Edge(c, b);
            a.NextEdge = e1;
            c.PrevEdge = e1;
            c.NextEdge = e2;
            b.PrevEdge = e2;
            Edges.AddRange([e1, e2]);
            OnPolygonStructureChanged();
        }
        public void ConvertToBezier(Edge edge)
        {
            if (!Edges.Remove(edge)) { return; }
            Vertex a = edge.prev;
            Vertex b = edge.next;
            BezierEdge newBezierEdge = new BezierEdge(a, b);
            a.NextEdge = newBezierEdge;
            b.PrevEdge = newBezierEdge;
            Edges.Add(newBezierEdge);
            if (a.Constraint is null) { a.AddConstraint(new G1ContinuityConstraint(a)); }
            if (b.Constraint is null) { b.AddConstraint(new G1ContinuityConstraint(b)); }
            OnPolygonStructureChanged();
        }
        public void ConvertToEdge(Edge edge)
        {
            if (!Edges.Remove(edge)) return;
            Vertex a = edge.prev;
            Vertex b = edge.next;
            Edge newEdge = new Edge(a, b);
            if(a.PrevEdge is not BezierEdge) { a.RemoveConstraint(); }
            if(b.NextEdge is not BezierEdge) { b.RemoveConstraint(); }
            a.NextEdge = newEdge;
            b.PrevEdge = newEdge;
            Edges.Add(newEdge);
            OnPolygonStructureChanged();
        }
        public void RemovePolygon()
        {
            Vertices.Clear();
            Edges.Clear();
            IsClosed = false;
        }
        private void OnPolygonStructureChanged() { PolygonStructureChanged.Invoke(); }
        public void SavePolygon()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // Formatowanie JSON-a
            };
            string jsonString = JsonSerializer.Serialize(this,options);
            File.WriteAllText("polygon.json", jsonString);
        }

        public Polygon? LoadPolygon()
        {
            string jsonString = File.ReadAllText("polygon.json");
            Polygon? deserialized = JsonSerializer.Deserialize<Polygon>(jsonString);
            if(deserialized == null) { return null; }
            deserialized.IsClosed = true;
            foreach (Edge e in deserialized.Edges)
            {
                Vertex? a = deserialized.Vertices.FirstOrDefault((v) => v.Id == e.prev.Id);
                Vertex? b = deserialized.Vertices.FirstOrDefault((v) => v.Id == e.next.Id);
                if(a is not null && b is not null)
                {
                    a.NextEdge = e;
                    b.PrevEdge = e;
                    e.prev = a;
                    e.next = b;
                }
            }
            foreach(Edge e in deserialized.Edges)
            {
                if (e.Constraint is not null)
                {
                    e.Constraint.Edge = e;
                }
            }
            foreach (Vertex v in deserialized.Vertices)
            {
                if (v.Constraint is not null)
                {
                    v.Constraint.ConstrainedVertex = v;
                }
            }
            return deserialized;
        }
    }
}
