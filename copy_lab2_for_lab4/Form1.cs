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
using System.Text;

namespace Lab2_CS
{
    
    public partial class Form1 : Form
    {
        public double tmp = 0;
        Equilateral triangle = new Equilateral();
        Prisma prisma = new Prisma();
        public Form1()
        {
            
            InitializeComponent();
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            triangle.Line = Convert.ToDouble(textBox5.Text);
            prisma.Height = Convert.ToDouble(textBox6.Text);

            triangle.CalculateArea();
            triangle.CalculatePerimeter();

            if (prisma.Height !=0)
            {
               prisma.CalculateArea(triangle.Line, triangle.Area, prisma.Height);
               prisma.CalcVolume();
               richTextBox1.Text = prisma.ToString(triangle.Line, prisma.Height, triangle.Perimeter, triangle.Area, prisma.Area1, prisma.Volume);
               dataGridView1.Rows.Add("Prisma", triangle.Line,prisma.Height, triangle.Area, triangle.Perimeter, prisma.Area1, prisma.Volume);
            }
            else
            {
              richTextBox1.Text = triangle.ToString();
              dataGridView1.Rows.Add("Triangle", triangle.Line, prisma.Height, triangle.Area, triangle.Perimeter,0,0);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string path = saveFileDialog1.FileName;
            BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate));
            int counter = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    writer.Write(dataGridView1.Rows[i].Cells[j].Value.ToString());
                }
                counter++;
            }
            writer.Close();
            richTextBox1.Text = "Exporting succesfull";
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            dataGridView1.Rows.Add();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string path = openFileDialog1.FileName;
            BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (reader.PeekChar() != -1)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = reader.ReadString();
                    }
                    dataGridView1.Rows.Add();
                }
                else
                {
                    reader.Close();
                    break;
                }
            }
            dataGridView1.Rows.RemoveAt(dataGridView1.RowCount - 1);
            richTextBox1.Text = "File imported succesfully";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int numb = 0;
            tmp = Convert.ToDouble(dataGridView1.Rows[1].Cells[6].Value);
            for (int i = 0; i < (dataGridView1.RowCount-1); i++)
            {
                if (tmp < Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value))
                {
                    tmp = Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                    numb = i;
                }
            }
            numb = numb + 1;
            richTextBox1.Text = "Prisma with biggest are in row" + numb;
            tmp = 0;
            int n = 0;
            for (int i = 0; i < dataGridView1.RowCount-1; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) == "Triangle")
                {
                    tmp = tmp + Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                    n++;
                }
            }
            tmp = tmp / dataGridView1.RowCount;
            richTextBox1.Text += "\nTriangles wich area less then average area of triangles in line: ";
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) == "Triangle")
                {
                    if (Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value) < tmp)
                    {
                        numb = i + 1;
                        richTextBox1.Text += numb + " ";
                    }
                }
            }
        }
    }
}
