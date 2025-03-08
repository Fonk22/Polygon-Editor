using gk___projekt_1.Utilities;

namespace gk___projekt_1
{
    internal static class PolygonIntegrityGuard
    {
        public static Polygon polygon = new Polygon();
        public static bool IsActive = true;
        private static List<(float, float)> backup = new List<(float, float)>();

        public static void ResetFreedomDegrees(Vertex? vertex1 = null)
        {
            foreach (Vertex v in polygon.Vertices)
            {
                v.FreedomDegree = FreedomDegree.Both;
            }
            foreach (BezierEdge bezier in polygon.Edges.OfType<BezierEdge>())
            {
                bezier.ControlPointA.FreedomDegree = FreedomDegree.Both;
                bezier.ControlPointB.FreedomDegree = FreedomDegree.Both;
            }
            if (vertex1 is not null) { vertex1.FreedomDegree = FreedomDegree.None; }
        }
        public static void CreateBackup()
        {
            backup.Clear();
            foreach (Vertex v in polygon.Vertices) { backup.Add((v.X, v.Y)); }
            foreach (BezierEdge bezier in polygon.Edges.OfType<BezierEdge>())
            {
                backup.Add((bezier.ControlPointA.X, bezier.ControlPointA.Y));
                backup.Add((bezier.ControlPointB.X, bezier.ControlPointB.Y));
            }
        }
        public static void Rollback(Vertex vertex, (int x, int y) vector)
        {
            int i = 0;
            while (i < polygon.Vertices.Count)
            {
                polygon.Vertices[i].SetPosition(backup[i]);
                i++;
            }
            foreach (BezierEdge bezier in polygon.Edges.OfType<BezierEdge>())
            {
                bezier.ControlPointA.SetPosition(backup[i]);
                bezier.ControlPointB.SetPosition(backup[i + 1]);
                i += 2;
            }
            vertex.MoveVertex((-vector.x, -vector.y));
        }

        // 0 - nie udalo sie, 1 - udalo sie tylko lewa czesc, 2 - udalo sie calosc
        public static int CorrectLeft(Vertex vertex)
        {
            Edge? pomEdge = vertex.PrevEdge;
            Vertex a = vertex;
            do
            {
                if (pomEdge?.Constraint == null) { return 1; }
                if (!pomEdge.Constraint.MaintainConstraint(a)) { return 0; }
                a = pomEdge.prev;
                pomEdge = a.PrevEdge;
            } while (a != vertex);
            return 2;
        }
        public static int CorrectRight(Vertex vertex)
        {
            Vertex a = vertex;
            Edge? pomEdge = vertex.NextEdge;
            do
            {
                if (pomEdge?.Constraint == null) { return 1; }
                if (!pomEdge.Constraint.MaintainConstraint(a)) { return 0; }
                a = pomEdge.next;
                pomEdge = a.NextEdge;
            } while (a != vertex);
            return 2;
        }

        public static void CorrectContinuity()
        {
            foreach (Vertex v in polygon.Vertices) { v.Constraint?.MaintainConstraint(); }
        }
        public static Vertex? FindMoved(Vertex vertex)
        {
            foreach (BezierEdge bezierEdge in polygon.Edges.OfType<BezierEdge>())
            {
                if (bezierEdge.ControlPointA.Id == vertex.Id) { return bezierEdge.prev.Constraint?.MaintainConstraint(); }
                if (bezierEdge.ControlPointB.Id == vertex.Id) { return bezierEdge.next.Constraint?.MaintainConstraint(); }
            }
            return null;
        }
        public static bool TryFixPolygon(Vertex vertex, (int x, int y) vector)
        {
            CreateBackup();
            ResetFreedomDegrees(vertex);
            Vertex? PomVertex = vertex is not ControlPoint ? vertex : FindMoved(vertex);
            if (PomVertex is not null)
            {
                int res1 = CorrectLeft(PomVertex);
                if (res1 < 2)
                {
                    if (res1 == 0) { ResetFreedomDegrees(PomVertex); }
                    int res2 = CorrectRight(PomVertex);
                    if (res1 + res2 < 2)
                    {
                        Rollback(vertex, vector);
                        polygon.MovePolygon(vector);
                        return false;
                    }
                }
            }
            CorrectContinuity();
            return true;
        }
        public static void VertexMovedHandler(Vertex vertex, (int x, int y) vector)
        {
            if (!IsActive) { return; }
            IsActive = false;
            TryFixPolygon(vertex, vector);
            IsActive = true;
        }
        public static void EdgeConstraintAddedHandler(Edge edge)
        {
            if (!TryFixPolygon(edge.prev, (0, 0)) && !TryFixPolygon(edge.next, (0, 0)))
            {
                MessageBox.Show("Unable to set the constraint");
                edge.RemoveConstraint();
            }
        }
        public static void PolygonStructureChangedHandler()
        {
            ResetFreedomDegrees();
            CorrectContinuity();
        }
    }
}
