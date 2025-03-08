using System.Windows.Forms.VisualStyles;
using gk___projekt_1.Constraints;
using gk___projekt_1.Utilities;

namespace gk___projekt_1
{
    internal static class PredefinedPolygonCreator
    {
        public static Polygon PreparePolygon0()
        {
            Point p1 = new Point(500, 400);
            Point p2 = new Point(700, 400);
            Point p3 = new Point(800, 560);
            Point p4 = new Point(500, 700);
            
            Polygon polygon = new Polygon();
            polygon.AddVertex(p1);
            polygon.AddVertex(p2);
            polygon.AddVertex(p3);
            polygon.AddVertex(p4);
            polygon.AddVertex(p1);

            polygon.Edges[0].AddConstraint(new HorizontalConstraint(polygon.Edges[0]));
            polygon.Edges[1].AddConstraint(new FixedLengthConstraint(polygon.Edges[1]));
            polygon.Edges[3].AddConstraint(new VerticalConstraint(polygon.Edges[3]));

            polygon.ConvertToBezier(polygon.Edges[2]);

            polygon.Vertices[3].AddConstraint(new C1ContinuityConstraint(polygon.Vertices[3]));

            return polygon;
        }
        public static Polygon PreparePolygon1()
        {
            Point p1 = new Point(909, 408);
            Point p2 = new Point(961, 408);
            Point p3 = new Point(1265, 623);
            Point p4 = new Point(600, 500);

            Polygon polygon = new Polygon();
            polygon.AddVertex(p1);
            polygon.AddVertex(p2);
            polygon.AddVertex(p3);
            polygon.AddVertex(p4);
            polygon.AddVertex(p1);

            List<Vertex> vertices = polygon.Vertices;
            List<Edge> edges = polygon.Edges;
            edges[0].AddConstraint(new HorizontalConstraint(edges[0]));
            edges[1].AddConstraint(new FixedLengthConstraint(edges[1]));
            edges[3].AddConstraint(new FixedLengthConstraint(edges[3]));

            return polygon;
        }
        public static Polygon PreparePolygon2()
        {
            Point p1 = new Point(560, 400);
            Point p2 = new Point(735, 215);
            Point p3 = new Point(1040, 330);
            Point p4 = new Point(1265, 625);
            Point p5 = new Point(680, 700);
            Polygon polygon = new Polygon();
            polygon.AddVertex(p1);
            polygon.AddVertex(p2);
            polygon.AddVertex(p3);
            polygon.AddVertex(p4);
            polygon.AddVertex(p5);
            polygon.AddVertex(p1);

            List<Vertex> vertices = polygon.Vertices;
            List<Edge> edges = polygon.Edges;
            edges[4].AddConstraint(new FixedLengthConstraint(edges[4]));
            edges[0].AddConstraint(new FixedLengthConstraint(edges[0]));
            polygon.ConvertToBezier(edges[3]);
            polygon.ConvertToBezier(edges[1]);

            return polygon;
        }
        public static Polygon PreparePolygon3()
        {
            Polygon polygon = new Polygon();
            List<Point> points = new List<Point>();
            Random random = new Random();
            for (int i = 0; i < 12; i++)
            {
                points.Add(new Point(random.Next(GraphicsSettings.BitmapWidth), random.Next(GraphicsSettings.BitmapHeight)));
            }
            foreach (Point p in points) { polygon.AddVertex(p); }
            polygon.AddVertex(points[0]);
            foreach (Edge e in polygon.Edges)
            {
                e.AddConstraint(new FixedLengthConstraint(e, 180.0f));
            }
            return polygon;
        }
        public static Polygon PreparePolygon4()
        {
            Polygon polygon = new Polygon();
            Point p1 = new Point(560, 400);
            Point p2 = new Point(735, 215);
            Point p3 = new Point(1040, 330);

            polygon.AddVertex(p1);
            polygon.AddVertex(p2);
            polygon.AddVertex(p3);
            polygon.AddVertex(p1);

            polygon.ConvertToBezier(polygon.Edges[1]);
            polygon.Vertices[1].AddConstraint(new C1ContinuityConstraint(polygon.Vertices[1]));
            polygon.Vertices[2].AddConstraint(new C1ContinuityConstraint(polygon.Vertices[2]));

            polygon.Edges[0].AddConstraint(new FixedLengthConstraint(polygon.Edges[0], 420));
            polygon.Edges[1].AddConstraint(new HorizontalConstraint(polygon.Edges[1]));
            
            return polygon;
        }

    }
}
