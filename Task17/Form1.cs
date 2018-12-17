using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task17
{
    public partial class Form1 : Form
    {
        private int[,] Ground;
        private int mostLeftX;
        private int mostRightX;
        private int mostLeftY;
        private int mostRightY;
        private List<Vector> AllVectors;
        public Form1()
        {
            InitializeComponent();
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");
            string line;
            AllVectors = new List<Vector>();

            int lineNumber = 0;
            while ((line = file.ReadLine()) != null)
            {
                AllVectors.Add(new Vector(line));
                lineNumber++;
            }

            file.Close();


            Ground = new int[AllVectors.Max(v => v.YEnd) + 2, AllVectors.Max(v => v.XEnd) + 2];

            foreach (Vector vector in AllVectors)
            {
                for (int i = vector.YStart; i <= vector.YEnd; i++)
                {
                    for (int j = vector.XStart; j <= vector.XEnd; j++)
                    {
                        Ground[i, j] = 1;
                    }
                }
            }

            Ground[0, 500] = 2;

            mostLeftX = AllVectors.Min(v => v.XStart)-2;
            mostRightX = AllVectors.Max(v => v.XEnd)+2;
            mostLeftY = AllVectors.Min(v => v.YStart)-1;
            mostLeftY = 0;
            mostRightY = AllVectors.Max(v => v.YEnd)+2;

            bool WasChanged = true;
            while (WasChanged)
            {
                WasChanged = false;

            }


            gr = pictureBox1.CreateGraphics();
        }
        //0 - empty, 1 - wall, 2 - water, 3 - settle
        private Graphics gr;
        private Brush Brush = Brushes.Black;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Draw()
        {
            gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);

            int w = pictureBox1.Width / (mostRightX-mostLeftX);
            float h = (float)pictureBox1.Height/ (mostRightY-mostLeftY );
            if (h <=1) h = 1;

            for (int i = mostLeftY; i < mostRightY; i++)
            {
                for (int j = mostLeftX; j < mostRightX; j++)
                {
                    if(Ground[i,j]==1)
                        gr.FillRectangle(Brush,(j-mostLeftX)*w,(i-mostLeftY)*h, w, h);
                        //gr.FillRectangle(Brush,(j-mostLeftX)*w,(i-mostLeftY)*h-1300, w, h);
                    if(Ground[i,j]==2)
                        gr.FillRectangle(Brushes.Blue,(j-mostLeftX)*w,(i-mostLeftY)*h , w, h);
                        //gr.FillRectangle(Brushes.Blue,(j-mostLeftX)*w,(i-mostLeftY)*h - 1300, w, h);
                    if(Ground[i,j]==3)
                        gr.FillRectangle(Brushes.SaddleBrown,(j-mostLeftX)*w,(i-mostLeftY)*h , w, h);
                        //gr.FillRectangle(Brushes.SaddleBrown,(j-mostLeftX)*w,(i-mostLeftY)*h - 1300, w, h);
                }
            }
            //gr.ScaleTransform(2,2);
        }

        private void Print()
        {
            string s = "";
            for (int i = mostLeftY; i < mostRightY; i++)
            {
                for (int j = mostLeftX; j < mostRightX; j++)
                {
                    s += (Ground[i, j]==1?"#":"=") + " ";
                }

                s += "\r\n";
            }

            MessageBox.Show(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Draw();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Print();
        }

        //0 - empty, 1 - wall, 2 - water, 3 - settle
        private bool Move()
        {
            bool moved = false;
            for (int i = 0; i < mostRightY -2; i++)
            {
                for (int j = mostLeftX; j < mostRightX; j++)
                {
                    if (Ground[i, j] == 2)
                    {
                        if (i < Ground.GetLength(0) - 1)
                        {
                            if (Ground[i + 1, j] == 0) //if empty
                            {
                                Ground[i + 1, j] = 2;
                                moved = true;
                                //return true;
                            }
                            else if (Ground[i + 1, j] == 3 || Ground[i + 1, j] == 1) //if wall or settle
                            {
                                if (Ground[i, j - 1] != 0 && Ground[i, j + 1] != 0) //if left and right are not empty
                                {
                                    if (IsSettle(j, i))
                                    {
                                        Ground[i, j] = 3;
                                        moved = true;
                                        //return true;
                                    }
                                }

                                if (Ground[i, j - 1] == 0) //if left empty
                                {
                                    Ground[i, j - 1] = 2;
                                    moved = true;
                                    //return true;
                                }

                                if (Ground[i, j + 1] == 0) //if right empty
                                {
                                    Ground[i, j + 1] = 2;
                                    moved = true;
                                    //return true;
                                }

                            }
                        }
                    }
                }
            }

            return moved;
        }

        //0 - empty, 1 - wall, 2 - water, 3 - settle
        private bool IsSettle(int X, int Y)
        {
            int currentX = X;
            while ((Ground[Y + 1, currentX] == 1 || Ground[Y + 1, currentX] == 3)&&currentX>=mostLeftX&&
                   Ground[Y, currentX - 1] != 1 && Ground[Y, currentX - 1] != 3) currentX--;
            if (currentX < mostLeftX|| !(Ground[Y + 1, currentX] == 1 || Ground[Y + 1, currentX] == 3)) return false;
            
            currentX = X;
            while ((Ground[Y + 1, currentX] == 1 || Ground[Y + 1, currentX] == 3) && currentX <= mostRightX&&
                   Ground[Y, currentX + 1] != 1 && Ground[Y, currentX + 1] != 3) currentX++;
            if (currentX > mostRightX|| !((Ground[Y + 1, currentX] == 1 || Ground[Y + 1, currentX] == 3))) return false;

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            for (int i = 0; i < n; i++)
            {
                Move();
            }
            Draw();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            while (Move()) ;

            Draw();

            int count = 0;
            for (int i = AllVectors.Min(v => v.YStart); i < Ground.GetLength(0); i++)
            {
                for (int j = 0; j < Ground.GetLength(1); j++)
                {
                    if (
                        //Ground[i, j] == 2 || //part1
                        Ground[i, j] == 3)
                        count++;
                }
            }
            
            MessageBox.Show(count+"");

            //List<int> lst = Ground.Cast<int>().ToList();
            //MessageBox.Show((lst.Count(ls => ls == 2 || ls == 3)-1)+"");
        }
    }

    class Vector
    {
        public int XStart { get; set; }
        public int YStart { get; set; }
        public int XEnd { get; set; }
        public int YEnd { get; set; }

        public Vector(string s)
        {
            string[] split = s.Split(',');
            string xInfo = split.FirstOrDefault(str => str.Contains("x="));
            string yInfo = split.FirstOrDefault(str => str.Contains("y="));

            if (xInfo.Contains(".."))
            {
                string[] split2 = xInfo.Split('=')[1]
                    .Split(new char[] {'.', '.'}, StringSplitOptions.RemoveEmptyEntries);

                XStart = Convert.ToInt32(split2[0]);
                XEnd = Convert.ToInt32(split2[1]);
            }
            else
            {
                string split2 = xInfo.Split('=')[1];
                XStart = Convert.ToInt32(split2);
                XEnd = Convert.ToInt32(split2);
            }
            if (yInfo.Contains(".."))
            {
                string[] split2 = yInfo.Split('=')[1]
                    .Split(new char[] {'.', '.'}, StringSplitOptions.RemoveEmptyEntries);

                YStart = Convert.ToInt32(split2[0]);
                YEnd = Convert.ToInt32(split2[1]);
            }
            else
            {
                string split2 = yInfo.Split('=')[1];
                YStart = Convert.ToInt32(split2);
                YEnd = Convert.ToInt32(split2);
            }


        }
    }
}
