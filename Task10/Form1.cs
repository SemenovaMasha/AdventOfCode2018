using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task10
{
    public partial class Form1 : Form
    {
        private Point[] Points;

        public Form1()
        {
            InitializeComponent();

            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");
            string line;

            Points = new Point[File.ReadAllLines("../../input.txt").Length];
            int lineNumber = 0;
            while ((line = file.ReadLine()) != null)
            {
                Points[lineNumber] = new Point();

                Regex regex = new Regex(@"=<(.*?),");
                var v = regex.Matches(line);
                Points[lineNumber].X = Convert.ToInt32(v[0].Groups[1].ToString());//
                Points[lineNumber].VelocityX = Convert.ToInt32(v[1].Groups[1].ToString());

                regex = new Regex(@",(.*?)>");
                v = regex.Matches(line);
                Points[lineNumber].Y = Convert.ToInt32(v[0].Groups[1].ToString());//
                Points[lineNumber].VelocityY = Convert.ToInt32(v[1].Groups[1].ToString());

                lineNumber++;
            }

            file.Close();

            gr = pictureBox1.CreateGraphics();
            //gr.ScaleTransform(0.01f, 0.01f);

            //Image im = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //pictureBox1.Image = im;
            //gr = Graphics.FromImage(pictureBox1.Image);
        }

        private Graphics gr;
        private void button1_Click(object sender, EventArgs e)
        {
            Move();
            DrawPoints();


        }

        private void Move()
        {
            foreach (Point point in Points)
            {
                point.Move();
            }
        }
        private void MoveBack()
        {
            foreach (Point point in Points)
            {
                point.MoveBack();
            }
        }



        private Brush Brush = Brushes.Blue;
        private void DrawPoints()
        {
            gr.Clear(Color.White);

            //MessageBox.Show((Points[0].X - 49800) + " " + (Points[0].Y - 49800));
            foreach (Point point in Points)
            {
                gr.FillRectangle(Brush, point.X - Points.Min(p => p.X), point.Y - Points.Min(p => p.Y), 1,1);
                //gr.FillRectangle(Brush, point.X+100, point.Y+100, 1,1);
            }

            //gr.FillRectangle(Brushes.Red, 10,10, 1, 1);
            //gr.FillRectangle(Brushes.Red, 0, 0, 10, 10);
            //gr.FillRectangle(Brushes.Red, 0, 0, 10000, 10000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Steps += Convert.ToInt32(textBox1.Text);
            for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
            {
                Move();
            }

            DrawPoints();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            gr = pictureBox1.CreateGraphics();
            
            MessageBox.Show("Steps = "+Steps+"\r\nMax X = " + Points.Max(p => p.X) + ", Min X = " + Points.Min(p => p.X) +
                            " \r\nMax Y = " + Points.Max(p => p.Y) + ", Min Y = " + Points.Min(p => p.Y));
        }

        private int Steps = 0;

        private void button4_Click(object sender, EventArgs e)
        {
            Steps -= Convert.ToInt32(textBox1.Text);
            for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
            {
                MoveBack();
            }

            DrawPoints();
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int VelocityX { get; set; }
        public int VelocityY { get; set; }

        public void Move()
        {
            X += VelocityX;
            Y += VelocityY;
        }
        public void MoveBack()
        {
            X -= VelocityX;
            Y -= VelocityY;
        }
    }
}
