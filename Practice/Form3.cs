using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Minimum = -1;
            chart1.Series[0].Points.AddXY(4, 0);
            BackColor = ColorTranslator.FromHtml("#B88686");
            //chart1.Series[0].Points.AddXY(2, 0);
            //chart1.Series[0].Points.AddXY(1, 0);
            // chart1.ChartAreas[0].AxisX.Interval = 1;
            //chart1.Series[0].Points.AddXY(5, 0);
            //chart1.Series[0].Points.AddXY(8, 7);
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
