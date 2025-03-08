using System.Numerics;
using gk___projekt_1.Utilities;

namespace gk___projekt_1
{
    internal static class GraphicsExtensions
    {
        public static void FillCircle(this Graphics graphics, Brush brush, Vertex center, int radius)
        {
            graphics.FillEllipse(brush, center.X - radius, center.Y - radius, radius * 2, radius * 2);
        }
        public static void DrawBezier(this Graphics graphics, Pen pen, Vertex a,
            Vertex c1, Vertex c2, Vertex b)
        {
            Point p1 = new Point((int)a.X, (int)a.Y);
            Point p2 = new Point((int)c1.X, (int)c1.Y);
            Point p3 = new Point((int)c2.X, (int)c2.Y);
            Point p4 = new Point((int)b.X, (int)b.Y);
            graphics.DrawBezier(pen, p1, p2, p3, p4);
        }
        public static void DrawBezierIterative(Graphics graphics, BezierEdge edge)
        {
            Vector2 V0 = new Vector2(edge.prev.X, edge.prev.Y);
            Vector2 V1 = new Vector2(edge.ControlPointA.X, edge.ControlPointA.Y);
            Vector2 V2 = new Vector2(edge.ControlPointB.X, edge.ControlPointB.Y);
            Vector2 V3 = new Vector2(edge.next.X, edge.next.Y);

            Vector2 A0 = V0;
            Vector2 A1 = 3 * (V1 - V0);
            Vector2 A2 = 3 * (V2 - 2 * V1 + V0);
            Vector2 A3 = V3 + 3 * (V1 - V2) - V0;


            int SampleCount = GraphicsSettings.SampleCount;
            float t = 0;
            float d = 1 / (float)SampleCount;
            float d2 = d * d;
            float d3 = d2 * d;

            Vector2 P = A0;
            Vector2 dP = d3 * A3 + d2 * A2 + d * A1;
            Vector2 ddP = 6 * d3 * A3 + 2 * d2 * A2;
            Vector2 dddP = 6 * d3 * A3;

            Vector2 Ppom;

            while (t + d < 1)
            {
                Ppom = P + dP;
                graphics.DrawLine(GraphicsSettings.EdgePen, P.X, P.Y, Ppom.X, Ppom.Y);
                P = Ppom;
                dP += ddP;
                ddP += dddP;
                t += d;
            }
        }
        public static void DrawLine(this Graphics graphics, Pen pen, Vertex vertexA, Vertex vertexB)
        {
            graphics.DrawLine(pen, vertexA.X, vertexA.Y, vertexB.X, vertexB.Y);
        }
        public static void DrawLine(this Graphics graphics, Pen pen, Vertex vertex, Point point)
        {
            graphics.DrawLine(pen, vertex.X, vertex.Y, point.X, point.Y);
        }

        // source: https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
        public static void DrawLineBresenham(Bitmap bitmap, Vertex vertexA, Vertex vertexB)
        {
            int x0 = (int)vertexA.X;
            int y0 = (int)vertexA.Y;
            int x1 = (int)vertexB.X;
            int y1 = (int)vertexB.Y;
            if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
            {
                if (x0 > x1) { DrawLineLow(bitmap, x1, y1, x0, y0); }
                else { DrawLineLow(bitmap, x0, y0, x1, y1); }
            }
            else
            {
                if (y0 > y1) { DrawLineHigh(bitmap, x1, y1, x0, y0); }
                else { DrawLineHigh(bitmap, x0, y0, x1, y1); }
            }
        }

        private static void DrawLineLow(Bitmap bitmap, int x0, int y0, int x1, int y1)
        {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int yi = 1;
            if (dy < 0)
            {
                yi = -1;
                dy = -dy;
            }
            int D = (2 * dy) - dx;
            int y = y0;
            for (int x = x0; x < x1; x++)
            {
                if (0<= x && x < bitmap.Width && 0<= y && y < bitmap.Height)
                {
                    bitmap.SetPixel(x, y, GraphicsSettings.EdgeColor);
                }
                if (D > 0)
                {
                    y += yi;
                    D += 2 * (dy - dx);
                }
                else { D += 2 * dy; }
            }
        }
        private static void DrawLineHigh(Bitmap bitmap, int x0, int y0, int x1, int y1)
        {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int xi = 1;
            if (dx < 0)
            {
                xi = -1;
                dx = -dx;
            }
            int D = (2 * dx) - dy;
            int x = x0;
            for (int y = y0; y < y1; y++)
            {
                if (0<=x && x < bitmap.Width && 0<= y && y < bitmap.Height)
                {
                    bitmap.SetPixel(x, y, GraphicsSettings.EdgeColor);
                }
                if (D > 0)
                {
                    x += xi;
                    D += (2 * (dx - dy));
                }
                else
                {
                    D += 2 * dx;
                }

            }
        }

    }
}
