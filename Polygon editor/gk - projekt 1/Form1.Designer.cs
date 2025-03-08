namespace gk___projekt_1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            canvas = new PictureBox();
            contextMenu = new ContextMenuStrip(components);
            RemoveVertex = new ToolStripMenuItem();
            RemoveEdge = new ToolStripMenuItem();
            SplitEdge = new ToolStripMenuItem();
            ConvertToBezier = new ToolStripMenuItem();
            ConvertToEdge = new ToolStripMenuItem();
            AddEdgeConstraint = new ToolStripMenuItem();
            AddHorizontalConstraint = new ToolStripMenuItem();
            AddVerticalConstraint = new ToolStripMenuItem();
            FixedLength = new ToolStripMenuItem();
            LengthTextBox = new ToolStripTextBox();
            RemoveConstraint = new ToolStripMenuItem();
            RemovePolygon = new ToolStripMenuItem();
            ChangeContinuityConstraint = new ToolStripMenuItem();
            G0Continuity = new ToolStripMenuItem();
            AddG1Continuity = new ToolStripMenuItem();
            AddC1Continuity = new ToolStripMenuItem();
            menuStrip = new MenuStrip();
            Settings = new ToolStripMenuItem();
            LineDrawing = new ToolStripMenuItem();
            DefaultLineAlgorithm = new ToolStripMenuItem();
            BresenhamLineAlgorithm = new ToolStripMenuItem();
            BezierDrawing = new ToolStripMenuItem();
            DefaultBezierAlgorithm = new ToolStripMenuItem();
            IterativeBezierAlgorithm = new ToolStripMenuItem();
            ClearScene = new ToolStripMenuItem();
            SavePolygon = new ToolStripMenuItem();
            LoadPolygon = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)canvas).BeginInit();
            contextMenu.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // canvas
            // 
            canvas.BackColor = Color.SkyBlue;
            canvas.Dock = DockStyle.Fill;
            canvas.Location = new Point(0, 0);
            canvas.Name = "canvas";
            canvas.Size = new Size(1478, 944);
            canvas.TabIndex = 0;
            canvas.TabStop = false;
            canvas.MouseDown += canvasMouseDown;
            canvas.MouseMove += CanvasMouseMove;
            canvas.MouseUp += CanvasMouseUp;
            // 
            // contextMenu
            // 
            contextMenu.ImageScalingSize = new Size(24, 24);
            contextMenu.Items.AddRange(new ToolStripItem[] { RemoveVertex, RemoveEdge, SplitEdge, ConvertToBezier, ConvertToEdge, AddEdgeConstraint, RemoveConstraint, RemovePolygon, ChangeContinuityConstraint });
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new Size(317, 292);
            // 
            // RemoveVertex
            // 
            RemoveVertex.Name = "RemoveVertex";
            RemoveVertex.Size = new Size(316, 32);
            RemoveVertex.Text = "Remove Vertex";
            RemoveVertex.Click += RemoveVertexClick;
            // 
            // RemoveEdge
            // 
            RemoveEdge.Name = "RemoveEdge";
            RemoveEdge.Size = new Size(316, 32);
            RemoveEdge.Text = "Remove Edge";
            RemoveEdge.Click += RemoveEdgeClick;
            // 
            // SplitEdge
            // 
            SplitEdge.Name = "SplitEdge";
            SplitEdge.Size = new Size(316, 32);
            SplitEdge.Text = "Split Edge";
            SplitEdge.Click += SplitEdgeClick;
            // 
            // ConvertToBezier
            // 
            ConvertToBezier.Name = "ConvertToBezier";
            ConvertToBezier.Size = new Size(316, 32);
            ConvertToBezier.Text = "Convert to Bezier";
            ConvertToBezier.Click += ConvertToBezierClick;
            // 
            // ConvertToEdge
            // 
            ConvertToEdge.Name = "ConvertToEdge";
            ConvertToEdge.Size = new Size(316, 32);
            ConvertToEdge.Text = "Convert to Edge";
            ConvertToEdge.Click += ConvertToEdgeClick;
            // 
            // AddEdgeConstraint
            // 
            AddEdgeConstraint.DropDownItems.AddRange(new ToolStripItem[] { AddHorizontalConstraint, AddVerticalConstraint, FixedLength });
            AddEdgeConstraint.Name = "AddEdgeConstraint";
            AddEdgeConstraint.Size = new Size(316, 32);
            AddEdgeConstraint.Text = "Add Edge Constraint";
            // 
            // AddHorizontalConstraint
            // 
            AddHorizontalConstraint.Name = "AddHorizontalConstraint";
            AddHorizontalConstraint.Size = new Size(214, 34);
            AddHorizontalConstraint.Text = "Horizontal";
            AddHorizontalConstraint.Click += AddHorizontalConstraintClick;
            // 
            // AddVerticalConstraint
            // 
            AddVerticalConstraint.Name = "AddVerticalConstraint";
            AddVerticalConstraint.Size = new Size(214, 34);
            AddVerticalConstraint.Text = "Vertical";
            AddVerticalConstraint.Click += AddVerticalConstraintClick;
            // 
            // FixedLength
            // 
            FixedLength.DropDownItems.AddRange(new ToolStripItem[] { LengthTextBox });
            FixedLength.Name = "FixedLength";
            FixedLength.Size = new Size(214, 34);
            FixedLength.Text = "Fixed Length";
            // 
            // LengthTextBox
            // 
            LengthTextBox.Name = "LengthTextBox";
            LengthTextBox.Size = new Size(100, 31);
            LengthTextBox.KeyDown += AddFixedLengthConstraint;
            // 
            // RemoveConstraint
            // 
            RemoveConstraint.Name = "RemoveConstraint";
            RemoveConstraint.Size = new Size(316, 32);
            RemoveConstraint.Text = "Remove Constraint";
            RemoveConstraint.Click += RemoveConstraintClick;
            // 
            // RemovePolygon
            // 
            RemovePolygon.Name = "RemovePolygon";
            RemovePolygon.Size = new Size(316, 32);
            RemovePolygon.Text = "Remove Polygon";
            RemovePolygon.Click += RemovePolygonClick;
            // 
            // ChangeContinuityConstraint
            // 
            ChangeContinuityConstraint.DropDownItems.AddRange(new ToolStripItem[] { G0Continuity, AddG1Continuity, AddC1Continuity });
            ChangeContinuityConstraint.Name = "ChangeContinuityConstraint";
            ChangeContinuityConstraint.Size = new Size(316, 32);
            ChangeContinuityConstraint.Text = "Change Continuity Constraint";
            // 
            // G0Continuity
            // 
            G0Continuity.Name = "G0Continuity";
            G0Continuity.Size = new Size(136, 34);
            G0Continuity.Text = "G0";
            G0Continuity.Click += RemoveContinuityConstraintClick;
            // 
            // AddG1Continuity
            // 
            AddG1Continuity.Name = "AddG1Continuity";
            AddG1Continuity.Size = new Size(136, 34);
            AddG1Continuity.Text = "G1";
            AddG1Continuity.Click += AddG1ContinuityClick;
            // 
            // AddC1Continuity
            // 
            AddC1Continuity.Name = "AddC1Continuity";
            AddC1Continuity.Size = new Size(136, 34);
            AddC1Continuity.Text = "C1";
            AddC1Continuity.Click += AddC1ContinuityClick;
            // 
            // menuStrip
            // 
            menuStrip.BackColor = Color.AliceBlue;
            menuStrip.ImageScalingSize = new Size(24, 24);
            menuStrip.Items.AddRange(new ToolStripItem[] { Settings, ClearScene, SavePolygon, LoadPolygon });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1478, 33);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // Settings
            // 
            Settings.DropDownItems.AddRange(new ToolStripItem[] { LineDrawing, BezierDrawing });
            Settings.Name = "Settings";
            Settings.Size = new Size(92, 29);
            Settings.Text = "Settings";
            Settings.Click += SettingsMenuClick;
            // 
            // LineDrawing
            // 
            LineDrawing.DropDownItems.AddRange(new ToolStripItem[] { DefaultLineAlgorithm, BresenhamLineAlgorithm });
            LineDrawing.Name = "LineDrawing";
            LineDrawing.Size = new Size(231, 34);
            LineDrawing.Text = "Line Drawing";
            // 
            // DefaultLineAlgorithm
            // 
            DefaultLineAlgorithm.Name = "DefaultLineAlgorithm";
            DefaultLineAlgorithm.Size = new Size(201, 34);
            DefaultLineAlgorithm.Text = "Default";
            DefaultLineAlgorithm.Click += DefaultDrawingAlgorithmClick;
            // 
            // BresenhamLineAlgorithm
            // 
            BresenhamLineAlgorithm.Name = "BresenhamLineAlgorithm";
            BresenhamLineAlgorithm.Size = new Size(201, 34);
            BresenhamLineAlgorithm.Text = "Bresenham";
            BresenhamLineAlgorithm.Click += BresenhamDrawingAlgorithmClick;
            // 
            // BezierDrawing
            // 
            BezierDrawing.DropDownItems.AddRange(new ToolStripItem[] { DefaultBezierAlgorithm, IterativeBezierAlgorithm });
            BezierDrawing.Name = "BezierDrawing";
            BezierDrawing.Size = new Size(231, 34);
            BezierDrawing.Text = "Bezier Drawing";
            // 
            // DefaultBezierAlgorithm
            // 
            DefaultBezierAlgorithm.Name = "DefaultBezierAlgorithm";
            DefaultBezierAlgorithm.Size = new Size(177, 34);
            DefaultBezierAlgorithm.Text = "Default";
            DefaultBezierAlgorithm.Click += DefaultBezierAlgorithmClick;
            // 
            // IterativeBezierAlgorithm
            // 
            IterativeBezierAlgorithm.Name = "IterativeBezierAlgorithm";
            IterativeBezierAlgorithm.Size = new Size(177, 34);
            IterativeBezierAlgorithm.Text = "Iterative";
            IterativeBezierAlgorithm.Click += IterativeBezierAlgorithmClick;
            // 
            // ClearScene
            // 
            ClearScene.Name = "ClearScene";
            ClearScene.Size = new Size(67, 29);
            ClearScene.Text = "Clear";
            ClearScene.Click += ClearSceneClick;
            // 
            // SavePolygon
            // 
            SavePolygon.Name = "SavePolygon";
            SavePolygon.Size = new Size(65, 29);
            SavePolygon.Text = "Save";
            SavePolygon.Click += SavePolygonClick;
            // 
            // LoadPolygon
            // 
            LoadPolygon.Name = "LoadPolygon";
            LoadPolygon.Size = new Size(67, 29);
            LoadPolygon.Text = "Load";
            LoadPolygon.Click += LoadPolygonClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1478, 944);
            Controls.Add(menuStrip);
            Controls.Add(canvas);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "Polygon Editor - Mikołaj Karbowski";
            ((System.ComponentModel.ISupportInitialize)canvas).EndInit();
            contextMenu.ResumeLayout(false);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox canvas;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem RemoveVertex;
        private ToolStripMenuItem RemoveEdge;
        private ToolStripMenuItem SplitEdge;
        private ToolStripMenuItem ConvertToBezier;
        private ToolStripMenuItem ConvertToEdge;
        private ToolStripMenuItem AddEdgeConstraint;
        private ToolStripMenuItem AddHorizontalConstraint;
        private ToolStripMenuItem AddVerticalConstraint;
        private ToolStripMenuItem FixedLength;
        private ToolStripMenuItem RemovePolygon;
        private MenuStrip menuStrip;
        private ToolStripMenuItem Settings;
        private ToolStripMenuItem LineDrawing;
        private ToolStripMenuItem DefaultLineAlgorithm;
        private ToolStripMenuItem BresenhamLineAlgorithm;
        private ToolStripMenuItem RemoveConstraint;
        private ToolStripMenuItem BezierDrawing;
        private ToolStripMenuItem DefaultBezierAlgorithm;
        private ToolStripMenuItem IterativeBezierAlgorithm;
        private ToolStripMenuItem ChangeContinuityConstraint;
        private ToolStripMenuItem AddG1Continuity;
        private ToolStripMenuItem AddC1Continuity;
        private ToolStripMenuItem G0Continuity;
        private ToolStripTextBox LengthTextBox;
        private ToolStripMenuItem ClearScene;
        private ToolStripMenuItem SavePolygon;
        private ToolStripMenuItem LoadPolygon;
    }
}
