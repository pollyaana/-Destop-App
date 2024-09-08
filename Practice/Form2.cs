using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace Practice
{
    public partial class Form2 : Form
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            SolidBrush brush1 = new SolidBrush(ColorTranslator.FromHtml("#9B85BD"));

            graphics.FillRectangle(brush1, 0, 0, 2000, 60);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            var rect = new Rectangle(0, 0, Size.Width, 60);
            graphics.DrawString("Пересечение прямой и окружности", new Font("Segoe Print", 16), Brushes.Black, rect, sf);
            graphics.DrawLine(new Pen(Color.Black, 2), 0, 60, 2000, 60);

            SolidBrush brush3 = new SolidBrush(ColorTranslator.FromHtml("#42582C"));
            graphics.FillRectangle(brush3, 5, 80, Size.Width - 30, 50);
            SolidBrush brush = new SolidBrush(ColorTranslator.FromHtml("#96C08F"));
            rect = new Rectangle(10, 90, Size.Width - 45, 30);
            graphics.FillRectangle(brush, 10, 90, Size.Width - 45, 30);
            graphics.DrawString("Решение в координатах:  " + solution, new Font("Segoe Print", 14), Brushes.Black, rect, sf);
            graphics.DrawLine(new Pen(Color.Black, 2), 0, 60, 914, 60);

        }
        public string solution;
        public int changeW;
        public int changeH;
        public Button button;
        void MyResize(object sender, EventArgs e)
        {
            float v = 13;
            chart1.Location = new Point(Size.Width / 3 - 35, Size.Height / 3 - 85);
            oldSizeForm = Size;
            if (Size.Width > 1500)
            {
                v = 18;
                chart1.Size = new Size(1300, 650);
                chart1.Location = new Point((Size.Width / 3) + 100, 140);
            }
            else
            {
                chart1.Size = oldSize2;
                button.Size = new Size(200, 50);
            }
            if (oldSize.Width < chart1.Size.Width)
            {
                var newS = chart1.Size.Height - (chart1.Size.Width - oldSize.Width);
            }

            equation.Font = new Font("Segoe Print", v);
            equation2.Font = new Font("Segoe Print", v);
            equation3.Font = new Font("Segoe Print", v);
            oldSize = chart1.Size;
            Refresh();

        }
        public Label equation;
        public Label equation2;
        public Label equation3;
        public Size oldSize;
        public Size oldSize2;
        public Size oldSizeForm;
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        public Form2(string uravn, string center, string rad)
        {
            InitializeComponent();
            Size = new Size(880, 670);
            oldSize = Size;
            MinimumSize = Size;
            this.Resize += MyResize;

            //FormBorderStyle = FormBorderStyle.FixedSingle;
            changeW = 880;
            changeH = 570;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = ColorTranslator.FromHtml("#B88686");
            button = new Button
            {
                Text = "Отмена",
                BackColor = ColorTranslator.FromHtml("#9B85BD"),
                Location = new Point(25, 530),
                Size = new Size(200, 50),
                MaximumSize = new Size(400, 100),
                Font = new Font("Segoe Print", 13),

            };

            button.FlatAppearance.BorderColor = Color.Black;
            button.FlatAppearance.BorderSize = 1;
            button.FlatStyle = FlatStyle.Flat;
            Controls.Add(button);
            button.Click += new EventHandler(button1_Click);
            button.Anchor = AnchorStyles.Left | AnchorStyles.Right;


            var k = 0;
            var b = 0;
            var flagA = 0;
            char sign = '+';
            var ur = uravn;
            if (uravn.Contains('x'))
            {
                if (uravn[0] == 'x')
                    k = 1;
                else
                {
                    if (uravn[0] == '-')
                    {
                        k = Convert.ToInt32(uravn.Split('x')[0]);
                        uravn = uravn.Substring(1);
                    }

                    else
                        k = Convert.ToInt32(uravn.Split('x')[0]);
                }

                if (uravn.Split('+').Length > 1)
                {
                    b = Convert.ToInt32(uravn.Split('+')[1]);
                }

                if (uravn.Split('-').Length > 1)
                {
                    b = Convert.ToInt32(uravn.Split('-')[1]) * (-1);
                }

            }
            else
            {
                b = Convert.ToInt32(uravn);
                flagA = 1;
            }


            var x0 = Convert.ToDouble(center.Split('_')[0]);
            var y0 = Convert.ToDouble(center.Split('_')[1]);
            var r = Convert.ToDouble(rad);

            equation = new Label();
            equation.Text = "Уравнение прямой:\n" + "y = " + ur;
            equation.Font = new Font("Segoe Print", 13);
            equation.Size = new Size(500, 80);
            equation.Location = new Point(10, 165);
            Controls.Add(equation);
            equation.Anchor = AnchorStyles.Left;
            equation2 = new Label();
            equation2.Text = "Центр окружности:\n ( " + x0 + " ; " + y0 + " )";
            equation2.Font = new Font("Segoe Print", 13);
            equation2.Size = new Size(500, 80);
            equation2.Location = new Point(10, 250);
            Controls.Add(equation2);
            equation3 = new Label();
            equation3.Text = "Радиус окружности:\n " + r + " [ ед ]";
            equation3.Font = new Font("Segoe Print", 13);
            equation3.Size = new Size(500, 80);
            equation3.Location = new Point(10, 330);
            Controls.Add(equation3);
            equation2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            equation3.Anchor = AnchorStyles.Left;

            double aa = k * k + 1;
            double b1 = -2 * x0 + 2 * k * b - 2 * k * y0;
            double c = -r * r + Math.Pow((b - y0), 2) + x0 * x0;
            double d = b1 * b1 - 4 * aa * c;
            double x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            int flag = 0;

            if (d > 0)
            {
                x1 = (-b1 + Math.Sqrt(d)) / (2 * aa);
                y1 = k * x1 + b;
                x2 = (-b1 - Math.Sqrt(d)) / (2 * aa);
                y2 = k * x2 + b;
                var strX1 = string.Format($"{{0:f2}}", x1);
                var strY1 = string.Format($"{{0:f2}}", y1);
                var strX2 = string.Format($"{{0:f2}}", x2);
                var strY2 = string.Format($"{{0:f2}}", y2);
                solution = "Точки пересечения: ( " + strX1 + " ; " + strY1 + " ) , " + "( " + strX2 + " ; " + strY2 + " )";
                flag = 2;
            }
            if (d == 0)
            {
                x1 = (-b1 + Math.Sqrt(d)) / (2 * aa);
                y1 = k * x1 + b;
                var strX1 = string.Format($"{{0:f2}}", x1);
                var strY1 = string.Format($"{{0:f2}}", y1);
                solution = "Точки пересечения: ( " + strX1 + " ; " + strY1 + " )";
                flag = 1;
            }
            if (d < 0)
            {
                solution = "Нет точек пересечения";
            }

            chart1.ChartAreas[0].AxisX.Minimum = x0 - r - 1;
            chart1.ChartAreas[0].AxisY.Maximum = y0 + r + 1;
            flagA = 1;
            if (flagA == 1)
            {
                chart1.ChartAreas[0].AxisY.Minimum = y0 - 2 * r;
                chart1.ChartAreas[0].AxisX.Minimum = x0 - 2 * r;
                chart1.ChartAreas[0].AxisX.Maximum = x0 + 2 * r;
                chart1.ChartAreas[0].AxisY.Maximum = y0 + 1.5 * r;
            }

            else
            {
                chart1.ChartAreas[0].AxisY.Minimum = y0 - r - 1.5;
                chart1.ChartAreas[0].AxisX.Maximum = x0 + r + 3;
            }
            double x = -r;
            if (y0 + 1.5 * r <= b)
            {
                chart1.ChartAreas[0].AxisY.Maximum += 0.5 * b;
                chart1.ChartAreas[0].AxisX.Minimum -= 0.5 * b;
                x = chart1.ChartAreas[0].AxisX.Minimum;
            }
            if (flag == 0)
            {
                chart1.ChartAreas[0].AxisY.Minimum = y0 - 2 * r - 2 * b;
                chart1.ChartAreas[0].AxisX.Minimum = 0 - 2 * r;
                chart1.ChartAreas[0].AxisX.Maximum = x0 + 2 * r + 2 * b;
                chart1.ChartAreas[0].AxisY.Maximum = y0 + 1.5 * r + 2 * b;
                x = chart1.ChartAreas[0].AxisX.Minimum;
            }

            double s = 0;
            s = Math.Round(Math.Abs(y0 - k * x0 - b) / Math.Sqrt(k * k + 1), 0);
            chart1.ChartAreas[0].AxisX.Minimum = x0 - 2 * s;
            chart1.ChartAreas[0].AxisX.Maximum = x0 + 2 * s;
            chart1.ChartAreas[0].AxisY.Minimum = y0 - 2 * s;
            chart1.ChartAreas[0].AxisY.Maximum = y0 + 2 * s;
            if (x0 - 2 * s <= r || (y0 == b && k == 0))
            {
                chart1.ChartAreas[0].AxisX.Minimum -= r + 1;
                chart1.ChartAreas[0].AxisX.Maximum += 2 * r + 1;
                chart1.ChartAreas[0].AxisY.Minimum -= r + 1;
                chart1.ChartAreas[0].AxisY.Maximum += r + 1;
            }
            x = chart1.ChartAreas[0].AxisX.Minimum;
            MinimizeBox = false;
            var a = x0 - r * 3;
            double bbb = chart1.ChartAreas[0].AxisX.Maximum;

            double y;
            double h = 0.1;

            this.chart1.Series[0].Points.Clear();

            while (x <= bbb)
            {
                y = k * x + b;
                this.chart1.Series[0].Points.AddXY(x, y);
                x += h;
            }

            this.chart1.Series[1].Points.Clear();
            double x3, y3 = 0;

            for (double angle = 0; angle <= 360; angle += 3)
            {
                x3 = x0 + r * Math.Cos(angle * Math.PI / 180);
                y3 = y0 + r * Math.Sin(angle * Math.PI / 180);
                this.chart1.Series[1].Points.AddXY(x3, y3);
            }

            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Crossing = 0;
            chart1.ChartAreas[0].AxisY.Crossing = 0;
            chart1.ChartAreas[0].AxisX.Interval = Math.Round(bbb / 3, 0);
            chart1.ChartAreas[0].AxisY.Interval = Math.Round(chart1.ChartAreas[0].AxisY.Maximum / 3, 0);
            if (flag > 0)
            {
                if (x1==0)
                {
                    chart1.Series[2].Points.AddXY(0, y1);
                    chart1.Series[2].Points.AddXY(-1, y0 - 2 * s-10);
                }
                    
                else
                    chart1.Series[2].Points.AddXY(x1, y1);
                
                if (flag == 2)
                    chart1.Series[3].Points.AddXY(x2, y2);
            }
            chart1.Location = new Point(Size.Width / 3 - 35, Size.Height / 3 - 85);
            oldSize = chart1.Size;
            oldSize2 = chart1.Size;
            oldSizeForm = Size;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}

