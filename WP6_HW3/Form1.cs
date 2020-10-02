using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;    //DashStyle

namespace WP6_HW3
{

    public partial class Form1 : Form
    {
        Color c = Color.Red;
        List<Point> startPt = new List<Point>();
        List<Point> endPt = new List<Point>();
        List<Color> colorPt = new List<Color>();
        List<int> color = new List<int>();
        List<int> linewidth = new List<int>();
        List<int> linesolid = new List<int>();
        int lineWidth = 1;
        bool lineSolid = true;


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "線段檔(*.pnt)|*.pnt";
            saveFileDialog1.Filter = "線段檔(*.pnt)|*.pnt";
            redToolStripMenuItem.Checked = true;
            


        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String s = saveFileDialog1.FileName;
                BinaryWriter outFile = new BinaryWriter(File.Open(s, FileMode.Create));
                outFile.Write(endPt.Count());
                for (int i = 0; i < endPt.Count(); i++)
                {
                    outFile.Write(startPt[i].X);
                    outFile.Write(startPt[i].Y);
                    outFile.Write(endPt[i].X);
                    outFile.Write(endPt[i].Y);
                }
                outFile.Close();
            }

        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startPt.Clear();
            endPt.Clear();
            Invalidate();

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String s = openFileDialog1.FileName;
                if (!File.Exists(s)) return;
                BinaryReader inFile = new BinaryReader(File.Open(s, FileMode.Open));
                startPt.Clear();
                endPt.Clear();
                int n = inFile.ReadInt32();
                for (int i = 0; i < n; i++)
                {
                    startPt.Add(new Point(inFile.ReadInt32(), inFile.ReadInt32()));
                    endPt.Add(new Point(inFile.ReadInt32(), inFile.ReadInt32()));
                }
                Invalidate();
                inFile.Close();
            }

        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c = Color.Red;
            redToolStripMenuItem.Checked = true;
            greenToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;

        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c = Color.Green;
            redToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = true;
            blueToolStripMenuItem.Checked = false;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c = Color.Blue;
            redToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = true;

        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lineSolid = true;
            solidToolStripMenuItem.Checked = true;
            dashToolStripMenuItem.Checked = false;
        }

        private void dashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lineSolid = false;
            solidToolStripMenuItem.Checked = false;
            dashToolStripMenuItem.Checked = true;

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            endPt.Add(e.Location);
            if (redToolStripMenuItem.Checked)
                color.Add(1);
            else if (greenToolStripMenuItem.Checked)
                color.Add(2);
            else if (blueToolStripMenuItem.Checked)
                color.Add(3);
            linewidth.Add(lineWidth);
            if (lineSolid)
                linesolid.Add(1);
            else
                linesolid.Add(0);
            this.Invalidate();

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            startPt.Add(e.Location);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen1 = new Pen(c, lineWidth);
            for (int i = 0; i < endPt.Count(); i++)
            {
                if (color[i] == 1)
                    pen1 = new Pen(Color.Red, linewidth[i]);
                else if (color[i] == 2)
                    pen1 = new Pen(Color.Green, linewidth[i]);
                else if (color[i] == 3)
                    pen1 = new Pen(Color.Blue, linewidth[i]);
                if (linesolid[i] == 1)
                    pen1.DashStyle = DashStyle.Solid;
                else
                    pen1.DashStyle = DashStyle.Dash;
                e.Graphics.DrawLine(pen1, startPt[i].X, startPt[i].Y, endPt[i].X, endPt[i].Y);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            lineWidth = 1;
            toolStripMenuItem2.Checked = true;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
            toolStripMenuItem6.Checked = false;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            lineWidth = 2;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = true;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
            toolStripMenuItem6.Checked = false;

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            lineWidth = 3;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = true;
            toolStripMenuItem5.Checked = false;
            toolStripMenuItem6.Checked = false;

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            lineWidth = 4;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = true;
            toolStripMenuItem6.Checked = false;

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            lineWidth = 5;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
            toolStripMenuItem6.Checked = true;

        }
    }
}
