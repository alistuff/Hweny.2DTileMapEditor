using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace TileMapEditor.Forms
{
    public partial class frm_About : Form
    {
        private const int BOX_COUNT = 12;
        private const float fr = 0.72f;
        private float spring = 0.1f;
        private float angle = 0.01f;
        private float dx, dy, ax, ay, mouseX, mouseY;
        private bool _mouseMove;
        private Thread th;
        private Box box;
        private List<Box> boxes;
        private Random random;

        public frm_About()
        {
            InitializeComponent();

            random = new Random();
            box = new Box();
            mouseX = Width / 2;

            FormClosed += new FormClosedEventHandler(frm_About_FormClosed);
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
            pictureBox1.MouseLeave += new EventHandler(pictureBox1_MouseLeave);
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);

            boxes = new List<Box>();
            boxes.Clear();
            boxes.Add(box);
            for (int i = 0; i < BOX_COUNT; i++)
                boxes.Add(new Box());

            th = new Thread((state) =>
            {
                Run();
            });
            th.Start();
        }

        void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            _mouseMove = false;
        }

        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            _mouseMove = true;
            mouseX = (float)e.X;
            mouseY = (float)e.Y;
        }

        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Pen pen = new Pen(Color.Black))
            {
                for (int i = 1; i < boxes.Count; i++)
                    g.DrawLine(pen, boxes[i].X, boxes[i].Y, boxes[i - 1].X, boxes[i - 1].Y);
                for (int i = 0; i < boxes.Count; i++)
                    boxes[i].Render(g);
            }
        }

        void frm_About_FormClosed(object sender, FormClosedEventArgs e)
        {
            pictureBox1.Dispose();
            pictureBox1 = null;
            if (th != null)
            {
                th.Abort();
                th = null;
            }
        }

        void MoveBox(Box box, float x, float y)
        {
            dx = x - box.X;
            ax = dx * spring;
            box.Vx += ax;
            box.Vx *= fr;
            box.X += box.Vx;

            dy = y - box.Y;
            ay = dy * spring;
            box.Vy += ay;
            box.Vy += box.Weight;
            box.Vy *= fr;
            box.Y += box.Vy;
        }

        void Run()
        {
            int startTime = 0;
            while (true)
            {
                startTime = Environment.TickCount;
                if (!_mouseMove)
                {
                    mouseX = (float)Math.Cos(angle) * 30 + 50;
                    mouseY = (float)Math.Sin(angle) * 10 + 60;
                    angle += 0.18f;
                    angle %= 90.0f;
                }
                MoveBox(box, mouseX, mouseY);
                for (int i = 1; i < boxes.Count; i++)
                {
                    MoveBox(boxes[i], boxes[i - 1].X, boxes[i - 1].Y);
                }
                while (Environment.TickCount - startTime < 30)
                    Thread.Sleep(1);
                if (pictureBox1 != null)
                    pictureBox1.Invalidate(false);
            }
        }
    }

    public class Box
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Vx { get; set; }
        public float Vy { get; set; }
        public Color BgColor { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }

        public Box()
        {
            BgColor = Color.OrangeRed;
            Width = 10;
            Height =10;
            Weight = 1.1f;
        }

        public void Render(Graphics g)
        {
            using (Brush brush = new SolidBrush(BgColor))
            {
                g.FillRectangle(brush, X - Width / 2, Y - Height / 2, Width, Height);
            }
        }
    }
}
