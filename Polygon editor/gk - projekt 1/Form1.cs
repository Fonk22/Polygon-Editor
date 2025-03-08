using gk___projekt_1.Constraints;
using gk___projekt_1.Utilities;
namespace gk___projekt_1
{
    public partial class Form1 : Form
    {
        private readonly Bitmap bitmap;
        private Polygon polygon;
        private object? selectedObject;
        private Point prevLocation;
        public Form1()
        {
            InitializeComponent();
            Width = GraphicsSettings.WindowWidth;
            Height = GraphicsSettings.WindowHeight;
            polygon = new Polygon();
            bitmap = new Bitmap(canvas.Width, canvas.Height);
            GraphicsSettings.BitmapWidth = bitmap.Width;
            GraphicsSettings.BitmapHeight = bitmap.Height;
            canvas.Image = bitmap;
            polygon = PredefinedPolygonCreator.PreparePolygon0();
            updateCanvas();
        }
        private void updateCanvas()
        {
            polygon.Render(bitmap);
            canvas.Invalidate();
        }
        private void canvasMouseDown(object sender, MouseEventArgs e)
        {
            if (polygon.IsClosed) { selectedObject = polygon.FindeClickedObject(e.Location); }
            if (e.Button == MouseButtons.Left && !polygon.IsClosed)
            {
                polygon.AddVertex(e.Location);
                updateCanvas();
            }
            if (e.Button == MouseButtons.Right && polygon.IsClosed)
            {
                if (selectedObject is null) { return; }
                contextMenu.Show(MousePosition);

                RemoveVertex.Visible = selectedObject is Vertex and not ControlPoint;
                RemoveEdge.Visible = selectedObject is Edge or ControlPoint;
                SplitEdge.Visible = selectedObject is Edge;
                ConvertToBezier.Visible = selectedObject is Edge;
                ConvertToEdge.Visible = selectedObject is ControlPoint;
                AddEdgeConstraint.Visible = selectedObject is Edge edge && edge.Constraint is null;
                RemoveConstraint.Visible = selectedObject is Edge edge1 && edge1.Constraint is not null;
                RemovePolygon.Visible = selectedObject is Polygon;
                AddHorizontalConstraint.Visible = selectedObject is Edge edge2 && IsHorizontalConstraintAllowed(edge2);
                AddVerticalConstraint.Visible = selectedObject is Edge edge3 && IsVerticalConstraintAllowed(edge3);
                ChangeContinuityConstraint.Visible = selectedObject is Vertex vertex && IsContinuityConstraintAllowed(vertex);
                AddC1Continuity.Visible = selectedObject is Vertex vertex1 && vertex1.Constraint is not C1ContinuityConstraint;
                AddG1Continuity.Visible = selectedObject is Vertex vertex2 && vertex2.Constraint is not G1ContinuityConstraint;
                G0Continuity.Visible = selectedObject is Vertex vertex3 && vertex3.Constraint is not null;
                LengthTextBox.Text = selectedObject is Edge edge4 ? $"{(int)edge4.GetLength()}" : "0";
            }
            prevLocation = e.Location;
        }

        private void CanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (!polygon.IsClosed && polygon.VertexCount > 0)
            {
                using Graphics graphics = Graphics.FromImage(bitmap);
                updateCanvas();
                graphics.DrawLine(GraphicsSettings.EdgePen, polygon.GetLast, e.Location);
            }
            if (selectedObject is null) { return; }
            (int, int) vector = (e.X - prevLocation.X, e.Y - prevLocation.Y);
            switch (selectedObject)
            {
                case Vertex vertex:
                    vertex.MoveVertex(vector);
                    break;
                case Polygon:
                    polygon.MovePolygon(vector);
                    break;
            }
            prevLocation = e.Location;
            updateCanvas();
        }

        private void CanvasMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { selectedObject = null; }
        }

        private void RemoveVertexClick(object sender, EventArgs e)
        {
            if (selectedObject is not null and Vertex vertex)
            {
                polygon.RemoveVertex(vertex);
                selectedObject = null;
                updateCanvas();
            }
        }

        private void RemoveEdgeClick(object sender, EventArgs e)
        {
            switch (selectedObject)
            {
                case ControlPoint ControlPoint:
                    polygon.RemoveEdge(ControlPoint.BeziereEdge);
                    break;
                case Edge edge:
                    polygon.RemoveEdge(edge);
                    break;
            }
            selectedObject = null;
            updateCanvas();
        }

        private void SplitEdgeClick(object sender, EventArgs e)
        {
            if (selectedObject is not null and Edge edge)
            {
                polygon.SplitEdge(edge);
                selectedObject = null;
                updateCanvas();
            }
        }

        private void ConvertToBezierClick(object sender, EventArgs e)
        {
            if (selectedObject is not null and Edge edge)
            {
                polygon.ConvertToBezier(edge);
                selectedObject = null;
                updateCanvas();
            }
        }

        private void ConvertToEdgeClick(object sender, EventArgs e)
        {
            if (selectedObject is not null and ControlPoint controlPoint)
            {
                polygon.ConvertToEdge(controlPoint.BeziereEdge);
                selectedObject = null;
                updateCanvas();
            }
        }
        private bool IsHorizontalConstraintAllowed(Edge edge)
        {
            Edge? PrevEdge = edge.prev.PrevEdge;
            Edge? NextEdge = edge.next.NextEdge;
            if (PrevEdge?.Constraint is HorizontalConstraint || NextEdge?.Constraint is HorizontalConstraint)
            {
                return false;
            }
            return true;
        }
        private void AddHorizontalConstraintClick(object sender, EventArgs e)
        {
            if (selectedObject is not null and Edge edge)
            {
                EdgeConstraint constraint = new HorizontalConstraint(edge);
                selectedObject = null;
                edge.AddConstraint(constraint);
                updateCanvas();
            }
        }
        private bool IsVerticalConstraintAllowed(Edge edge)
        {
            Edge? PrevEdge = edge.prev.PrevEdge;
            Edge? NextEdge = edge.next.NextEdge;
            if (PrevEdge?.Constraint is VerticalConstraint || NextEdge?.Constraint is VerticalConstraint)
            {
                return false;
            }
            return true;
        }
        private bool IsContinuityConstraintAllowed(Vertex vertex)
        {
            if (vertex is ControlPoint) { return false; }
            return vertex.NextEdge is BezierEdge || vertex.PrevEdge is BezierEdge;

        }
        private void AddVerticalConstraintClick(object sender, EventArgs e)
        {
            if (selectedObject is not null and Edge edge)
            {
                EdgeConstraint constraint = new VerticalConstraint(edge);
                selectedObject = null;
                edge.AddConstraint(constraint);
                updateCanvas();
            }

        }
        private void RemovePolygonClick(object sender, EventArgs e)
        {
            if (selectedObject is not null and Polygon)
            {
                polygon.RemovePolygon();
                selectedObject = null;
                updateCanvas();
            }
        }
        private void SettingsMenuClick(object sender, EventArgs e)
        {
            DefaultLineAlgorithm.Enabled = GraphicsSettings.LineAlgorithm != Algorithms.Library;
            BresenhamLineAlgorithm.Enabled = GraphicsSettings.LineAlgorithm != Algorithms.Bresenham;
            DefaultBezierAlgorithm.Enabled = GraphicsSettings.BezierAlgorithm != Algorithms.Library;
            IterativeBezierAlgorithm.Enabled = GraphicsSettings.BezierAlgorithm != Algorithms.IterativeBesier;
        }

        private void RemoveConstraintClick(object sender, EventArgs e)
        {
            if (selectedObject is not null and Edge edge)
            {
                edge.RemoveConstraint();
                selectedObject = null;
                updateCanvas();
            }
        }
        private void DefaultDrawingAlgorithmClick(object sender, EventArgs e)
        {
            GraphicsSettings.LineAlgorithm = Algorithms.Library;
            updateCanvas();
        }

        private void BresenhamDrawingAlgorithmClick(object sender, EventArgs e)
        {
            GraphicsSettings.LineAlgorithm = Algorithms.Bresenham;
            updateCanvas();
        }
        private void IterativeBezierAlgorithmClick(object sender, EventArgs e)
        {
            GraphicsSettings.BezierAlgorithm = Algorithms.IterativeBesier;
            updateCanvas();
        }

        private void DefaultBezierAlgorithmClick(object sender, EventArgs e)
        {
            GraphicsSettings.BezierAlgorithm = Algorithms.Library;
            updateCanvas();
        }
        private void AddG1ContinuityClick(object sender, EventArgs e)
        {
            if (selectedObject is Vertex v)
            {
                G1ContinuityConstraint constraint = new G1ContinuityConstraint(v);
                selectedObject = null;
                v.AddConstraint(constraint);
                updateCanvas();
            }
        }
        private void AddC1ContinuityClick(object sender, EventArgs e)
        {
            if (selectedObject is Vertex v)
            {
                C1ContinuityConstraint constraint = new C1ContinuityConstraint(v);
                selectedObject = null;
                v.AddConstraint(constraint);
                updateCanvas();
            }
        }

        private void RemoveContinuityConstraintClick(object sender, EventArgs e)
        {
            if (selectedObject is Vertex v)
            {
                selectedObject = null;
                v.RemoveConstraint();
                updateCanvas();
            }
        }

        private void AddFixedLengthConstraint(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) { return; }
            if (sender is ToolStripTextBox textBox && selectedObject is not null and Edge edge)
            {
                if (int.TryParse(textBox.Text, out int length) && length > 0)
                {
                    EdgeConstraint constraint = new FixedLengthConstraint(edge, length);
                    selectedObject = null;
                    edge.AddConstraint(constraint);
                    contextMenu.Hide();
                    updateCanvas();
                }
                else
                {
                    MessageBox.Show("Invalid input\nPlease enter a positive " +
                        "intiger");
                }
            }
        }

        private void ClearSceneClick(object sender, EventArgs e)
        {
            polygon.RemovePolygon();
            updateCanvas();
        }

        private void SavePolygonClick(object sender, EventArgs e)
        {
            polygon.SavePolygon();
            MessageBox.Show("Polygon Saved!");
        }

        private void LoadPolygonClick(object sender, EventArgs e)
        {
            Polygon? newPol = polygon.LoadPolygon();
            polygon = newPol is not null ? newPol : polygon;
            updateCanvas();
            MessageBox.Show("Polygon loaded");
        }
    }
}
