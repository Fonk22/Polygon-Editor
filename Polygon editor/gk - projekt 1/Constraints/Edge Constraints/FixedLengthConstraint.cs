using System.Numerics;
using System.Text.Json.Serialization;
using gk___projekt_1.Utilities;

namespace gk___projekt_1.Constraints
{
    internal class FixedLengthConstraint : EdgeConstraint
    {
        [JsonInclude]
        public readonly float Length;
        public FixedLengthConstraint(Edge edge, float lenght = -1) : base(edge)
        {
            Length = lenght > 0 ? lenght : edge.GetLength();
        }

        [JsonConstructor]
        public FixedLengthConstraint(float length) : base(new Edge(new Vertex(0,0), new Vertex(0,0)))
        {
            Length = length;
        }

        public override bool MaintainConstraint(Vertex v1)
        {
            Vertex v2 = v1 == Edge.prev ? Edge.next : Edge.prev;
            Edge? nextEdge = v2.NextEdge == v1.PrevEdge ? v2.PrevEdge : v2.NextEdge;
            float delx = v1.X - v2.X;
            float dely = v1.Y - v2.Y;
            float dist = (float)Math.Sqrt(delx * delx + dely * dely);
            if (nextEdge?.Constraint is FixedLengthConstraint fl && v2.FreedomDegree == FreedomDegree.Both)
            {
                Vertex v3 = nextEdge.prev == v2 ? nextEdge.next : nextEdge.prev;
                float a = v1.X;
                float b = v1.Y;
                float r = Length;

                float p = v3.X;
                float q = v3.Y;
                float d = fl.Length;

                float m = (p - a) / (b - q);
                float c = (a * a + b * b + d * d - (p * p + q * q + r * r)) / (2 * (b - q));

                float A = 1 + m * m;
                float B = 2 * (m * (c - b) - a);
                float C = a * a + (c - b) * (c - b) - r * r;

                float pom = B * B - 4 * A * C;
                if (pom > 0)
                {
                    float X1 = (-B + (float)Math.Sqrt(pom)) / (2 * A);
                    float X2 = (-B - (float)Math.Sqrt(pom)) / (2 * A);
                    float Y1 = m * X1 + c;
                    float Y2 = m * X2 + c;

                    float diff1 = (X1 - v2.X) * (X1 - v2.X) + (Y1 - v2.Y) * (Y1 - v2.Y);
                    float diff2 = (X2 - v2.X) * (X2 - v2.X) + (Y2 - v2.Y) * (Y2 - v2.Y);
                    if (diff1 < diff2)
                    {
                        v2.X = X1;
                        v2.Y = Y1;
                    }
                    else
                    {
                        v2.X = X2;
                        v2.Y = Y2;
                    }
                    v2.FreedomDegree = FreedomDegree.None;
                    v1.FreedomDegree = FreedomDegree.None;
                    return true;
                }
            }
            if (Math.Abs(dist - Length) < GraphicsSettings.LengthConstraintTolerance)
            {
                v1.FreedomDegree = FreedomDegree.None;
                v2.FreedomDegree = FreedomDegree.None;
                return true;
            }
            if (v1.FreedomDegree == FreedomDegree.X)
            {
                float pom = Length * Length - (v1.Y - v2.Y) * (v1.Y - v2.Y);
                if (pom > 0.0f)
                {
                    pom = (float)Math.Sqrt(pom);
                    float x1 = pom + v2.X;
                    float x2 = -pom + v2.X;
                    if (Math.Abs(v1.X - x1) > Math.Abs(v1.X - x2)) { x1 = x2; }
                    v1.X = x1;
                    v1.FreedomDegree = FreedomDegree.None;
                    v2.FreedomDegree = FreedomDegree.None;
                    return true;
                }
            }
            if (v1.FreedomDegree == FreedomDegree.Y)
            {
                float pom = Length * Length - (v1.X - v2.X) * (v1.X - v2.X);
                if (pom > 0.0f)
                {
                    pom = (float)Math.Sqrt(pom);
                    float y1 = pom + v2.Y;
                    float y2 = -pom + v2.Y;
                    if (Math.Abs(v1.Y - y1) > Math.Abs(v1.Y - y2)) { y1 = y2; }
                    v1.Y = y1;
                    v1.FreedomDegree = FreedomDegree.None;
                    v2.FreedomDegree = FreedomDegree.None;
                    return true;
                }
            }
            if (v2.FreedomDegree == FreedomDegree.Both)
            {
                if (dist == 0) { return true; }
                float diff = dist - Length;
                Vector2 ToTargetVector = new Vector2(delx, dely);
                ToTargetVector = Vector2.Normalize(ToTargetVector);
                ToTargetVector *= diff;
                if (float.IsNaN(ToTargetVector.X)) { return false; }
                v2.X += ToTargetVector.X;
                v2.Y += ToTargetVector.Y;
                v2.FreedomDegree = FreedomDegree.None;
                return true;
            }
            return false;
        }
    }
}
