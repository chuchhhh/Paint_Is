﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Test : MetroFramework.Forms.MetroForm
    {
        public Surface surface;
        public static Shape CurrentShape { get; set; }
        public static BrushType CurrentBrush { get; set; }
        public static BrushType PenBrush { get; set; }
        public static Shape shapeB { get; set; }
        public static Color color{ get; set; }
        public static Color PenColor { get; set; }
        public static Color ShapesColor { get; set; }
        public event EventHandler PanelPaint;

        public static int PenSize;
        public static int EraserSize;

        public Test()
        {
            InitializeComponent();
            penUC1.Visible = false;
            fileUC1.Visible = false;
            shapesUC1.Visible = false;
            ESize.Visible = false;
            surface = drawPanel1.Surface;
            surface.MouseDown += Surface_MouseDown;
            surface.MouseMove += Surface_MouseMove;
            surface.MouseLeave += Surface_MouseLeave;
            surface.SizeChanged += Surface_SizeChange;
            //doi anh shape
            shapesUC1.LineClicked += ShapesUC1_LineClicked;
            shapesUC1.CircleClicked += ShapesUC1_CircleClicked;
            shapesUC1.StarClicked += ShapesUC1_StarClicked;
            shapesUC1.TriangleClicked += ShapesUC1_TriangleClicked;
            shapesUC1.RectangleClicked += ShapesUC1_RectangleClicked;
            shapesUC1.PentagonClicked += ShapesUC1_PentagonClicked;
            shapesUC1.HexagonClicked += ShapesUC1_HexagonClicked;
            shapesUC1.RightArrowClicked += ShapesUC1_RightArrowClicked;
            shapesUC1.LeftArrowClicked += ShapesUC1_LeftArrowClicked;
            //doi mau shape
            shapesUC1.WhiteClicked += ShapesUC1_WhiteClicked;
            shapesUC1.RedClicked += ShapesUC1_RedClicked;
            shapesUC1.YellowClicked += ShapesUC1_YellowClicked;
            shapesUC1.OrangeClicked += ShapesUC1_OrangeClicked;
            shapesUC1.BlackClicked += ShapesUC1_BlackClicked;
            shapesUC1.BlueClicked += ShapesUC1_BlueClicked;
            shapesUC1.GreenClicked += ShapesUC1_GreenClicked;
            shapesUC1.PurpleClicked += ShapesUC1_PurpleClicked;

            //đổi ảnh button
            penUC1.PencilClicked += PenUC1_PencilClicked;
            penUC1.BrushClicked += PenUC1_BrushClicked;
            penUC1.BucketClicked += PenUC1_BucketClicked;

            //đổi màu button
            penUC1.WhiteClicked += PenUC1_WhiteClicked;
            penUC1.RedClicked += PenUC1_RedClicked;
            penUC1.YellowClicked += PenUC1_YellowClicked;
            penUC1.OrangeClicked += PenUC1_OrangeClicked;
            penUC1.BlackClicked += PenUC1_BlackClicked;
            penUC1.BlueClicked += PenUC1_BlueClicked;
            penUC1.GreenClicked += PenUC1_GreenClicked;
            penUC1.PurpleClicked += PenUC1_PurpleClicked;
            //Open file
            fileUC1.OpenClicked += FileUC1_OpenClicked;

            //always start with...
            CurrentBrush = BrushType.Pencil;
            CurrentShape = Shape.Star;
            shapeB = CurrentShape;
            PenBrush = BrushType.Pencil ;
            PenSize = 10;
            color = Color.Black;
            PenB.BackColor = Color.FromArgb(50, Color.Black);
            EraserSize = 10;
            ShapesB.BackColor = Color.FromArgb(50, Color.Black);
            ShapesColor = Color.Black;

        }

        private void ShapesUC1_PurpleClicked(object sender, EventArgs e)
        {
            ShapesB.BackColor = Color.FromArgb(50, ShapesUC.Instance.getPurple());
            color = ShapesUC.Instance.getPurple();
            ShapesColor = color;
        }

        private void ShapesUC1_GreenClicked(object sender, EventArgs e)
        {
            ShapesB.BackColor = Color.FromArgb(50, ShapesUC.Instance.getGreen());
            color = ShapesUC.Instance.getGreen();
            ShapesColor = color;
        }

        private void ShapesUC1_BlueClicked(object sender, EventArgs e)
        {
            ShapesB.BackColor = Color.FromArgb(50, ShapesUC.Instance.getBlue());
            color = ShapesUC.Instance.getBlue();
            ShapesColor = color;
        }

        private void ShapesUC1_BlackClicked(object sender, EventArgs e)
        {
            ShapesB.BackColor = Color.FromArgb(50, ShapesUC.Instance.getBlack());
            color = ShapesUC.Instance.getBlack();
            ShapesColor = color;
        }

        private void ShapesUC1_OrangeClicked(object sender, EventArgs e)
        {
            ShapesB.BackColor = Color.FromArgb(50, ShapesUC.Instance.getOrange());
            color = ShapesUC.Instance.getOrange();
            ShapesColor = color;
        }

        private void ShapesUC1_YellowClicked(object sender, EventArgs e)
        {
            ShapesB.BackColor = Color.FromArgb(50, ShapesUC.Instance.getYellow());
            color = ShapesUC.Instance.getYellow();
            ShapesColor = color;
        }

        private void ShapesUC1_RedClicked(object sender, EventArgs e)
        {
            ShapesB.BackColor = Color.FromArgb(50, ShapesUC.Instance.getRed());
            color = ShapesUC.Instance.getRed();
            ShapesColor = color;
        }

        private void ShapesUC1_WhiteClicked(object sender, EventArgs e)
        {
            ShapesB.BackColor = Color.FromArgb(50, ShapesUC.Instance.getWhite());
            color = ShapesUC.Instance.getWhite();
            ShapesColor = color;
        }

        private void ShapesUC1_LeftArrowClicked(object sender, EventArgs e)
        {
            ShapesB.BackgroundImage = ShapesUC.Instance.getLeftArrow();
            Test.CurrentBrush = BrushType.Shape;
            Test.CurrentShape = Shape.LeftArrow;
            shapeB = CurrentShape;

        }

        private void ShapesUC1_RightArrowClicked(object sender, EventArgs e)
        {
            ShapesB.BackgroundImage = ShapesUC.Instance.getRightArrow();
            Test.CurrentBrush = BrushType.Shape;
            Test.CurrentShape = Shape.RightArrow;
            shapeB = CurrentShape;

        }

        private void ShapesUC1_HexagonClicked(object sender, EventArgs e)
        {
            ShapesB.BackgroundImage = ShapesUC.Instance.getHexagon();
            Test.CurrentBrush = BrushType.Shape;
            Test.CurrentShape = Shape.Hexagon;
            shapeB = CurrentShape;
        }

        private void ShapesUC1_PentagonClicked(object sender, EventArgs e)
        {
            ShapesB.BackgroundImage = ShapesUC.Instance.getPentagon();
            Test.CurrentBrush = BrushType.Shape;
            Test.CurrentShape = Shape.Pentagon;
            shapeB = CurrentShape;
        }

        private void ShapesUC1_RectangleClicked(object sender, EventArgs e)
        {
            ShapesB.BackgroundImage = ShapesUC.Instance.getRectangle();
            Test.CurrentBrush = BrushType.Shape;
            Test.CurrentShape = Shape.RecTangle;
            shapeB = CurrentShape;
        }

        private void ShapesUC1_TriangleClicked(object sender, EventArgs e)
        {
            ShapesB.BackgroundImage = ShapesUC.Instance.getTriangle();
            Test.CurrentBrush = BrushType.Shape;
            Test.CurrentShape = Shape.Triangle;
            shapeB = CurrentShape;
        }

        private void ShapesUC1_StarClicked(object sender, EventArgs e)
        {
            ShapesB.BackgroundImage = ShapesUC.Instance.getStar();
            Test.CurrentBrush = BrushType.Shape;
            Test.CurrentShape = Shape.Star;
            shapeB = CurrentShape;
        }

        private void ShapesUC1_CircleClicked(object sender, EventArgs e)
        {
            ShapesB.BackgroundImage = ShapesUC.Instance.getCircle();
            Test.CurrentBrush = BrushType.Shape;
            Test.CurrentShape = Shape.Circle;
            shapeB = CurrentShape;
        }

        private void ShapesUC1_LineClicked(object sender, EventArgs e)
        {
            ShapesB.BackgroundImage = ShapesUC.Instance.getLine();
            Test.CurrentBrush = BrushType.Shape;
            Test.CurrentShape = Shape.Line;
            shapeB = CurrentShape;
        }

        private void FileUC1_OpenClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PenUC1_PurpleClicked(object sender, EventArgs e)
        {
            PenB.BackColor = Color.FromArgb(50, PenUC.Instance.getPurple());
            color = PenUC.Instance.getPurple();
            PenColor = color;
        }

        private void PenUC1_GreenClicked(object sender, EventArgs e)
        {
            PenB.BackColor = Color.FromArgb(50, PenUC.Instance.getGreen());
            color = PenUC.Instance.getGreen();
            PenColor = color;
        }

        private void PenUC1_BlueClicked(object sender, EventArgs e)
        {
            PenB.BackColor = Color.FromArgb(50, PenUC.Instance.getBlue());
            color = PenUC.Instance.getBlue();
            PenColor = color;
        }

        private void PenUC1_BlackClicked(object sender, EventArgs e)
        {
            PenB.BackColor = Color.FromArgb(50, PenUC.Instance.getBlack());
            color = PenUC.Instance.getBlack();
            PenColor = color;
        }

        private void PenUC1_OrangeClicked(object sender, EventArgs e)
        {
            PenB.BackColor = Color.FromArgb(50, PenUC.Instance.getOrange());
            color = PenUC.Instance.getOrange();
            PenColor = color;
        }

        private void PenUC1_YellowClicked(object sender, EventArgs e)
        {
            PenB.BackColor = Color.FromArgb(50, PenUC.Instance.getYellow());
            color = PenUC.Instance.getYellow();
            PenColor = color;
        }

        private void PenUC1_RedClicked(object sender, EventArgs e)
        {
            PenB.BackColor = Color.FromArgb(50, PenUC.Instance.getRed());
            color = PenUC.Instance.getRed();
            PenColor = color;
        }

        private void PenUC1_WhiteClicked(object sender, EventArgs e)
        {
            PenB.BackColor = Color.FromArgb(50, PenUC.Instance.getWhite());
            color = PenUC.Instance.getWhite();
            PenColor = color;
        }
        private void PenUC1_BucketClicked(object sender, EventArgs e)
        {
            PenB.Text = "";
            PenB.BackgroundImage = PenUC.Instance.getBucket();
            Test.CurrentBrush = BrushType.Bucket;
            PenBrush = CurrentBrush;
        }

        private void PenUC1_BrushClicked(object sender, EventArgs e)
        {
            PenB.Text = "";
            PenB.BackgroundImage = PenUC.Instance.getBrush();
            Test.CurrentBrush = BrushType.Brush;
            PenBrush = CurrentBrush;
        }

        private void PenUC1_PencilClicked(object sender, EventArgs e)
        {
            PenB.Text = "";
            PenB.BackgroundImage = PenUC.Instance.getPencil();
            Test.CurrentBrush = BrushType.Pencil;
            PenBrush = CurrentBrush;
        }


        private void Surface_SizeChange(object sender, EventArgs e)
        {
            drawPanel1.Refresh();
        }

        private void Surface_MouseLeave(object sender, EventArgs e)
        {

        }

        private void Surface_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Surface_MouseMove(object sender, MouseEventArgs e)
        {

        }
    
        private void EraserB_Click(object sender, EventArgs e)
        {
            Test.CurrentBrush = BrushType.Eraser;
            if (!ESize.Visible)
            {
                HideAllPanels();
                Transition1.ShowSync(ESize, false, BunifuAnimatorNS.Animation.HorizSlide);
            }
            else
            {
                Transition1.HideSync(ESize, false, BunifuAnimatorNS.Animation.HorizSlide);
            }
        }
        void HideAllPanels()
        {
            shapesUC1.Visible = false;
            penUC1.Visible = false;
            fileUC1.Visible = false;
            ESize.Visible = false;
        }
        private void FileB_Click(object sender, EventArgs e)
        {
            if (!fileUC1.Visible)
            {
                HideAllPanels();              
                Transition1.ShowSync(fileUC1, false, BunifuAnimatorNS.Animation.HorizSlide);
            }
            else
            {               
                Transition1.HideSync(fileUC1, false, BunifuAnimatorNS.Animation.HorizSlide);
            }
        }

        private void PenB_Click(object sender, EventArgs e)
        {
            if (!penUC1.Visible)
            {
                HideAllPanels();
                Transition1.ShowSync(penUC1, false, BunifuAnimatorNS.Animation.HorizSlide);
                CurrentBrush = PenBrush;
                color = PenColor;
            }
            else
                Transition1.HideSync(penUC1, false, BunifuAnimatorNS.Animation.HorizSlide);
        }

        private void EraserB_Click_1(object sender, EventArgs e)
        {
            Test.CurrentBrush = BrushType.Eraser;
            if (!ESize.Visible)
            {
                HideAllPanels();
                Transition1.ShowSync(ESize, false, BunifuAnimatorNS.Animation.HorizSlide);
            }
            else
            {
                Transition1.HideSync(ESize, false, BunifuAnimatorNS.Animation.HorizSlide);
            }
        }

        private void drawPanel1_Paint(object sender, PaintEventArgs e)
        {
            if (this.PanelPaint != null)
                this.PanelPaint(sender, e);
        }

        private void UndoB_Click(object sender, EventArgs e)
        {
            surface.UndoPress();
        }

        private void RedoB_Click(object sender, EventArgs e)
        {
            surface.RedoPress();
        }

        private void ShapesB_Click(object sender, EventArgs e)
        {
            if (!shapesUC1.Visible)
            {
                HideAllPanels();
                Transition1.ShowSync(shapesUC1, false, BunifuAnimatorNS.Animation.HorizSlide);
                CurrentShape = shapeB;
                color = ShapesColor;
                CurrentBrush = BrushType.Shape;
            }
            else
                Transition1.HideSync(shapesUC1, false, BunifuAnimatorNS.Animation.HorizSlide);
        }

        private void Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MetroFramework.MetroMessageBox.Show(this, "File chưa được lưu, bạn có muốn đóng ứng dụng ?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.No)
            {
                MetroFramework.MetroMessageBox.Show(this, "xxxx", "zzzz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void ESize_Scroll(object sender, ScrollEventArgs e)
        {
            EraserSize = ESize.Value;
        }

        private void shapesUC1_Load(object sender, EventArgs e)
        {

        }
    }
}
