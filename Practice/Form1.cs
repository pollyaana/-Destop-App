using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using Label = System.Windows.Forms.Label;

namespace Practice
{
    public partial class Form1 : Form
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            graphics = e.Graphics;
            SolidBrush brush1 = new SolidBrush(ColorTranslator.FromHtml("#9B85BD"));

            graphics.FillRectangle(brush1, 0, 0, 2000, 60);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            rect = new Rectangle(0, 0, Size.Width, 60);
            graphics.DrawString("Пересечение прямой и окружности", new Font("Segoe Print", 16), Brushes.Black, rect, sf);
            graphics.DrawLine(new Pen(Color.Black, 2), 0, 60, 2000, 60);
            
        }

        void MyResize(object sender, EventArgs e)
        {
            Refresh();
        }
        public Rectangle rect;
        public Size oldFormSize;
        public Graphics graphics;


        private void button1_Click(object sender, EventArgs e)
        {
            while (line.Text == "" || cent.Text == "" || radius.Text == "")
            {
                MessageBox.Show("Заполните все поля ввода ^_^ !", "Пустые поля");
                return;
            }
            double res = 0;
            int cnt = 0;
            string strPr = "";
            string linePr = "";
            string strXcent="";
            string strYcent="";
            for (int i = 0; i < cent.Text.Length; i++)
            {
                if (cent.Text[i] == '_')
                {
                    cnt++;
                }
                    
                else
                {
                    if( cent.Text[i] != '-')
                        strPr += cent.Text[i];
                }
            }
            strYcent = cent.Text.Split('_')[1];
            strXcent = cent.Text.Split('_')[0];
            int cnt2 = 0;
            int cnt3 = 0;
            int cnt4 = 0;
            for (int i = 0; i < line.Text.Length; i++)
            {
                if (line.Text[i] == 'x' )
                {
                    cnt2++;
                    continue;
                }
                if( line.Text[i] == '+')
                {
                    if(i != 0)
                        cnt3++;
                    continue;
                }
                if ( line.Text[i] == '-')
                {
                    if (i != 0)
                        cnt4++;
                    continue;
                }
                else
                {
                    linePr += line.Text[i];
                }
            }
            while (!double.TryParse(linePr, out res) || !double.TryParse(strXcent, out res) || !double.TryParse(strYcent, out res) || !double.TryParse(radius.Text, out res) 
                || cnt != 1 ||cnt2 > 1 || cnt3 > 1 || cnt4 > 1 || (Convert.ToDouble(radius.Text))<=0)
            {
                MessageBox.Show("Заполните все поля ввода в правильном формате ^_^ !", "Некорректные входные данные");
                return;
            }
            Form2 form2 = new Form2(line.Text, cent.Text, radius.Text);
            line.Clear();
            cent.Clear();
            radius.Clear();
            Hide();
            form2.ShowDialog();
            Form1 form1 = new Form1();
            form1.ShowDialog();
           
        }
        public Form1()
        {
            InitializeComponent();
            this.Resize += MyResize;
            Size = new Size(880, 440);
            oldFormSize = Size;
            MinimumSize = Size;
            this.Text = "Пересечение прямой и окружности";
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = ColorTranslator.FromHtml("#B88686");
            rect.Width = Size.Width;
            MinimizeBox = false;
            Label equation = new Label();
            equation.Text = "Уравнение прямой вида kx + b :";
            equation.Font = new Font("Segoe Print", 13);
            equation.Size = new Size(500, 40);
            equation.Location = new Point(30, 165);
            Controls.Add(equation);
            //equation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            line = new TextBox();
            line.Font = new Font("Segoe Print", 13);
            line.Location = new Point(550, 165);
            line.Size = new Size(270, 100);
            Controls.Add(line);
            line.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            
            Label center = new Label();
            center.Text = "Коордианта центра окружности в виде x_y:";
            center.Font = new Font("Segoe Print", 13);
            Controls.Add(center);

            cent = new TextBox();
            cent.Font = new Font("Segoe Print", 13);
            Controls.Add(cent);
            cent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            
            Label rad = new Label();
            rad.Text = "Радиус окружности  в размерности [ед] : ";
            rad.Font = new Font("Segoe Print", 13);
            Controls.Add(rad);

            radius = new TextBox();
            radius.Font = new Font("Segoe Print", 13);
            Controls.Add(radius);
            radius.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;


            //equation.BackColor = Color.Transparent;

            Button button = new Button
            {
                Text = "Вычислить",
                BackColor = ColorTranslator.FromHtml("#9B85BD"),
                Font = new Font("Segoe Print", 13),

            };

            button.FlatAppearance.BorderColor = Color.Black;
            button.FlatAppearance.BorderSize = 1;
            button.FlatStyle = FlatStyle.Flat;
            Controls.Add(button);
            button.Click += new EventHandler(button1_Click);
            
            center.Location = new Point(30, 215);
            center.Size = new Size(500, 40);
            cent.Location = new Point(550, 215);
            cent.Size = new Size(270, 100);
            rad.Location = new Point(30, 270);
            rad.Size = new Size(500, 40);;
            radius.Location = new Point(550, 270);
            radius.Size = new Size(270, 100);
            button.Location = new Point(380, 340);
            button.Size = new Size(150, 40);
            button.Anchor = AnchorStyles.Bottom;
        }

        public TextBox line;
        public TextBox cent;
        public TextBox radius;
        public double width;

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
