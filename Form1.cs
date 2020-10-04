using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsPackage
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        void Putpixel(Graphics g, int x, int y, Color c)
        {
            Brush solidBrush = new SolidBrush(c);
            g.FillRectangle(solidBrush, x, y, 1, 1);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void beresenham(int x1, int y1, int x2, int y2, Graphics graph, Color lineColor)
        {
            int w = x2 - x1;
            int h = y2 - y1;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                textBox5.Text = $"{textBox5.Text}({Math.Abs(value: x1).ToString()} , {Math.Abs(y1).ToString()})  ";
                Putpixel(graph, (int)Math.Abs(x1), (int)Math.Abs(y1), lineColor);
                //Putpixel(x1, y1, color);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x1 += dx1;
                    y1 += dy1;
                }
                else
                {
                    x1 += dx2;
                    y1 += dy2;
                }

            }
        }
        void Drawingby_dda(int x1, int y1, int x2, int y2, Graphics graph, Color lineColor)
        {


            int steps;

            int dx, dy;

            float x, xIncrement;
            float y, yIncrement;

            dx = x2 - x1;
            dy = y2 - y1;

            steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);

            xIncrement = (float)dx / (float)steps;
            yIncrement = (float)dy / (float)steps;

            x = (float)x1;
            y = (float)y1;

            textBox5.Text = "";

            do
            {
                textBox5.Text = textBox5.Text + "(" + ((int)Math.Round(x)).ToString() + " , " + ((int)Math.Round(y)).ToString() + ")  ";
                Putpixel(graph, (int)Math.Round(x), (int)Math.Round(y), lineColor);

                x += xIncrement;
                y += yIncrement;

                steps--;

            } while (steps >= 0);
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x1 = Int32.Parse(textBox1.Text);
            int y1 = Int32.Parse(textBox2.Text);
            int x2 = Int32.Parse(textBox3.Text);
            int y2 = Int32.Parse(textBox4.Text);

            Graphics graph = panel1.CreateGraphics();

            beresenham(x1, y1, x2, y2, graph, Color.Black);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x1 = Int32.Parse(textBox1.Text);
            int y1 = Int32.Parse(textBox2.Text);
            int x2 = Int32.Parse(textBox3.Text);
            int y2 = Int32.Parse(textBox4.Text);
           
            Graphics graph = panel1.CreateGraphics();

            Drawingby_dda(x1, y1, x2, y2, graph, Color.Black);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            panel1.Invalidate();
        }
        void drawCircleFromMidpoint(int pointX, int pointY, int radius)
        {
            int x = 0, y = radius, pp = 1 - radius;
            textBox5.Text = "";
            calculatePP(pointX, pointY, x, y);

            while (x < y)
            {
                x++;
                if (pp < 0)
                    pp += 2 * x + 1;
                else
                {
                    y--;
                    pp += 2 * (x - y) + 1;
                }

                calculatePP(pointX, pointY, x, y);
            }

        }

        void calculatePP(int MBx, int MBy, int x, int y)
        {
            drawFrag(MBx + x, MBy + y);
            drawFrag(MBx - x, MBy + y);
            drawFrag(MBx + x, MBy - y);
            drawFrag(MBx - x, MBy - y);
            drawFrag(MBx + y, MBy + x);
            drawFrag(MBx - y, MBy + x);
            drawFrag(MBx + y, MBy - x);
            drawFrag(MBx - y, MBy - x);
            textBox5.Text = textBox5.Text + "( pointX:" + MBx + "±" + "arc(" + x + "," + y + "))" + "( pointY:" + MBy + "±" + "arc(" + x + "," + y + "))";

            

        }
        void drawFrag(int x, int y) {
            var aBrush = Brushes.Black;
            var target = panel1.CreateGraphics();
            target.FillRectangle(aBrush, x,y, 2, 2);
        }
        void drawFrag2(float x, float y)
        {
            var aBrush = Brushes.Black;
            var target = panel1.CreateGraphics();
            target.FillRectangle(aBrush, x, y, 2, 2);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int x1 = Int32.Parse(textBox1.Text);
            int y1 = Int32.Parse(textBox2.Text);
            int raduis = Int32.Parse(textBox6.Text);
            drawCircleFromMidpoint(x1, y1, raduis);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int x1 = Int32.Parse(textBox1.Text);
            int y1 = Int32.Parse(textBox2.Text);
            int raduis = Int32.Parse(textBox6.Text);
            int raduisy = Int32.Parse(textBox7.Text);

            drawEllipseFromMidPoint(x1, y1, raduis, raduisy);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        void drawEllipseFromMidPoint(int xc, int yc, int rx, int ry)
        {

            float dx, dy, d1, d2, x, y;
            x = 0;
            y = ry;
            textBox5.Text = "";
            // reg 1 arc
            d1 = (ry * ry) - (rx * rx * ry) + (0.25f * rx * rx);
            dx = 2 * ry * ry * x;
            dy = 2 * rx * rx * y;

            // For region 1 
            while (dx < dy)
            {

                ellipsePoints(xc, yc, x, y);
                if (d1 < 0)
                {
                    x++;
                    dx = dx + (2 * ry * ry);
                    d1 = d1 + dx + (ry * ry);
                }
                else
                {
                    x++;
                    y--;
                    dx = dx + (2 * ry * ry);
                    dy = dy - (2 * rx * rx);
                    d1 = d1 + dx - dy + (ry * ry);
                }

            }

            // Decision parameter of region 2 
            d2 = ((ry * ry) * ((x + 0.5f) * (x + 0.5f))) + ((rx * rx) * ((y - 1) * (y - 1))) - (rx * rx * ry * ry);

            while (y >= 0)
            {

                ellipsePoints(xc, yc, x, y);
                if (d2 > 0)
                {
                    y--;
                    dy = dy - (2 * rx * rx);
                    d2 = d2 + (rx * rx) - dy;
                }
                else
                {
                    y--;
                    x++;
                    dx = dx + (2 * ry * ry);
                    dy = dy - (2 * rx * rx);
                    d2 = d2 + dx - dy + (rx * rx);
                }

            }
        }

        void ellipsePoints(int MPx, int MPy, float x, float y)
        {
            
            drawFrag2(MPx + x, MPy + y);
            drawFrag2(MPx - x, MPy + y);
            drawFrag2(MPx + x, MPy - y);
            drawFrag2(MPx - x, MPy - y);
            textBox5.Text = textBox5.Text + "( pointX:" + MPx + "±" + "arc(" + x + "," + y + "))°" + "( pointY:" + MPy + "±" + "arc(" + x + "," + y + "))°";


        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
