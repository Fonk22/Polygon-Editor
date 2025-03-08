namespace gk___projekt_1.Utilities
{
    enum Algorithms
    {
        Library,
        Bresenham,
        IterativeBesier,
    }
    enum FreedomDegree
    {
        None,
        Both,
        X,
        Y,
    }
    internal static class GraphicsSettings
    {
        public static int BitmapWidth;
        public static int BitmapHeight;

        public static readonly int EdgeWidth = 4;
        public static readonly int SampleCount = 40;
        public static readonly int WindowWidth = 2160;
        public static readonly int WindowHeight = 1215;
        public static readonly int VertexRadius = 15;

        public static readonly float PointComparisonThreshold = 15.0f;
        public static readonly float LengthConstraintTolerance = 1.8f;

        public static readonly Color ControlPointColor = Color.Orange;
        public static readonly Color BackgroundColor = Color.SkyBlue;
        public static readonly Color VertexColor = Color.Red;
        public static readonly Color EdgeColor = Color.Black;

        public static Algorithms BezierAlgorithm = Algorithms.Library;
        public static Algorithms LineAlgorithm = Algorithms.Library;

        public static readonly Image FixedLengthLineIcon = Image.FromFile("Icons/fixedlength-line.png");
        public static readonly Image HorizontalLineIcon = Image.FromFile("Icons/horizontal-line.png");
        public static readonly Image VerticalLineIcon = Image.FromFile("Icons/vertical-line.png");

        public static readonly Brush ControlPointBrush = new SolidBrush(ControlPointColor);
        public static readonly Brush VertexBrush = new SolidBrush(VertexColor);
        public static readonly Brush ContinuityLabelBrush = new SolidBrush(Color.LightGray);

        public static readonly Pen BezierPen = new Pen(Color.Blue, 2) { DashPattern = [5, 2] };
        public static readonly Pen EdgePen = new Pen(EdgeColor, EdgeWidth);

        public static readonly Font ContinuityLabelFont = new Font("Arial", 10);


    }
}
