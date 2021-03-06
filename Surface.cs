﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Paint
{
    public partial class Surface : PictureBox
    {
        #region Component
        Rectangle OldRect;
        Rectangle AreaRect;

        GraphicsPath path;
        Graphics grp;
        Graphics gra;
        Pen pen;
        Color color;
        public Color Pickercolor;
        int Pensize;

        DrawStatus CurrentStatus;

        Stack<Bitmap> Undo;
        Stack<Bitmap> Redo;
        Stack<Point> UndoLocation;
        Stack<Point> RedoLocation;

        BrushType tempBrush;
        Point MouseDown;
        Shape shape;
        Rectangle resolution = Screen.PrimaryScreen.Bounds;
        List<Point> points = new List<Point>();
        #endregion

        #region Init Surface
        public Surface()
        {
            InitializeComponent();
            Undo = new Stack<Bitmap>();
            Redo = new Stack<Bitmap>();
            UndoLocation = new Stack<Point>();
            RedoLocation = new Stack<Point>();

            Location = new Point(10, 10);
            Size = new Size(resolution.Width*3/4, resolution.Height*3/4 );
            Image = new Bitmap(Width, Height);
            BackColor = Color.Azure;

            Region region = new Region(new Rectangle(0, 0, Width, Height));
            grp = Graphics.FromImage(Image);

        }

        #endregion

        #region Paint and Mouse Control
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            switch (CurrentStatus)
            {
                case DrawStatus.ShapeDraw:
                    DrawShape.Draw(pe.Graphics, pen, AreaRect, Test.CurrentShape);
                    break;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MouseDown = e.Location;
            switch (CurrentStatus)
            {
                case DrawStatus.Idle:

                    if (e.Button == MouseButtons.Left)
                    {
                        color = Test.color;
                        Pensize = Test.PenSize;
                        points.Add(e.Location);

                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        tempBrush = Test.CurrentBrush;
                        Test.CurrentBrush = BrushType.Picker;
                    }
                    SetGraphics();

                    Cursor = Cursors.Cross;
                    break;

            }
        }



        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            switch (CurrentStatus)
            {
                case DrawStatus.ToolDraw:
                    DrawDrag(MouseDown, e.Location, Test.CurrentBrush);
                    MouseDown = e.Location;
                    points.Add(e.Location);
                    break;
                case DrawStatus.ShapeDraw:
                    AreaRect = DrawShape.CreateRectangle(MouseDown, e.Location);
                    break;
            }
            this.Refresh();
                    
        }



        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            points.Clear();
            switch (CurrentStatus)
            {
                case DrawStatus.ToolDraw:
                    CurrentStatus = DrawStatus.Idle;
                    Redo.Push(new Bitmap(Image));
                    RedoLocation.Push(Location);
                    Cursor = Cursors.Default;

                    if (path != null) path.ClearMarkers();
                    if (Test.CurrentBrush == BrushType.Picker)
                    {
                        Test.CurrentBrush = tempBrush;
                    }
                    //grp.DrawPath(pen, path);
                    
                    break;
                case DrawStatus.ShapeDraw:
                    grp = CreateGraphics();

                    Undo.Push(new Bitmap(Image));
                    Redo.Clear();

                    Bitmap temp = (Bitmap)Image;
                    Image = new Bitmap(Width, Height);
                    grp = Graphics.FromImage(Image);
                    grp.DrawImage(temp, 0, 0);

                    DrawShape.Draw(grp, pen, AreaRect, Test.CurrentShape);
                    CurrentStatus = DrawStatus.Idle;

                    Cursor = Cursors.Default;
                    break;
                    //ongoing
            }
        }

        private void SetGraphics()
        {
            switch (Test.CurrentBrush)
            {
                case BrushType.Pencil:
                    Undo.Push(new Bitmap(Image));
                    UndoLocation.Push(Location);
                    Redo.Clear();

                    RedoLocation.Clear();

                    pen = new Pen(color, Test.PenSize);
                    pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    CurrentStatus = DrawStatus.ToolDraw;
                    break;
                case BrushType.Eraser:
                    Undo.Push(new Bitmap(Image));
                    UndoLocation.Push(Location);
                    Redo.Clear();
                    RedoLocation.Clear();


                    pen = new Pen(Color.Transparent, Test.EraserSize);

                    pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    grp.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    CurrentStatus = DrawStatus.ToolDraw;
                    break;
                case BrushType.Bucket:
                    Undo.Push(new Bitmap(Image));
                    UndoLocation.Push(Location);
                    Redo.Clear();
                    RedoLocation.Clear();

                    FloodFill(MouseDown, Color.FromArgb(Test.Opacity, color));

                    Redo.Push(new Bitmap(Image));
                    RedoLocation.Push(Location);
                    break;
                case BrushType.Brush:
                    Undo.Push(new Bitmap(Image));
                    UndoLocation.Push(Location);
                    Redo.Clear();

                    RedoLocation.Clear();

                    pen = new Pen(Color.FromArgb(Test.Opacity, color), Test.PenSize);

                    pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    
                    //grp.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    CurrentStatus = DrawStatus.ToolDraw;
                    break;
                case BrushType.Picker:
                    CurrentStatus = DrawStatus.ToolDraw;
                    break;
                case BrushType.Shape:
                    pen = new Pen(Color.FromArgb(Test.ShapeOpacity, color),Test.ShapeSize);
                    CurrentStatus = DrawStatus.ShapeDraw;
                    break;
            }
        }

        #endregion

        //bucket
        private void FloodFill(Point node, Color replaceColor)
        {
            Bitmap DrawBitmap = new Bitmap(Image);
            Color targetColor = DrawBitmap.GetPixel(node.X, node.Y);

            if (targetColor.ToArgb() == replaceColor.ToArgb())
                return;

            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(node);

            while (pixels.Count != 0)
            {
                Point floodNode = pixels.Pop();
                Color floodColor = DrawBitmap.GetPixel(floodNode.X, floodNode.Y);

                if (floodColor == targetColor)
                {
                    DrawBitmap.SetPixel(floodNode.X, floodNode.Y, replaceColor);

                    if (floodNode.X != 0)
                        pixels.Push(new Point(floodNode.X - 1, floodNode.Y));

                    if (floodNode.X + 1 < Width)
                        pixels.Push(new Point(floodNode.X + 1, floodNode.Y));

                    if (floodNode.Y != 0)
                        pixels.Push(new Point(floodNode.X, floodNode.Y - 1));

                    if (floodNode.Y + 1 < Height)
                        pixels.Push(new Point(floodNode.X, floodNode.Y + 1));
                }
            }
            Image = DrawBitmap;

        }
        //chuột kéo 
        private void DrawDrag(Point mouseDown, Point location, BrushType currentBrush)
        {
            switch (currentBrush)
            {

                case BrushType.Pencil:

                    grp = Graphics.FromImage(Image);
                    grp.DrawLine(pen, mouseDown, location);
                    // grp.SmoothingMode = 
                    break;
                case BrushType.Eraser:
                    grp = Graphics.FromImage(Image);
                    grp.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    grp.DrawLine(pen, mouseDown, location);
                    break;
                case BrushType.Brush:
                    path = new GraphicsPath();
                    Bitmap temp = (Bitmap)Image;
           
                    gra = Graphics.FromImage(temp);
                    gra.CompositingMode = CompositingMode.SourceCopy;

                    grp = (Graphics)gra;
                    grp.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

                    path.AddLines(points.ToArray());
                    gra.DrawPath(pen, path);
                    pen.LineJoin = LineJoin.Round;
                    break; 
                case BrushType.Picker:
                    temp =  (Bitmap) Image;
                    Pickercolor = temp.GetPixel(location.X, location.Y);
                    break;
                    //ongoing
            }
        }


        #region Undo & Redo
        public void UndoPress()
        {
            if (Undo.Count > 0 && UndoLocation.Count > 0) 
            {
                RedoLocation.Push(UndoLocation.Peek());
                Redo.Push(Undo.Peek());
                Location = UndoLocation.Pop();
                Image = Undo.Peek();
                Size = Undo.Pop().Size;
            }
        }

        public void RedoPress()
        {
            if (Redo.Count > 1 && RedoLocation.Count > 1) 
            {
                UndoLocation.Push(RedoLocation.Pop());
                Undo.Push(Redo.Pop());
                Location = RedoLocation.Peek();
                Image = Redo.Peek();
                Size = Image.Size;
            }
        }

        public void PushUndo(Image img)
        {
            Undo.Push(new Bitmap(img));
        }

        public void PushRedo(Image img)
        {
            Redo.Push(new Bitmap(img));
        }
        #endregion
    }
    public enum BrushType
    {
        Pencil, Eraser, Brush, Bucket, Shape, Picker
    }
    enum DrawStatus
    {
        Idle, ToolDraw, ShapeDraw,
    }
}
