using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace createrectanglesbyexcel.forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

                   this.inits();
        }


        private void inits()
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.DialogResult = DialogResult.Cancel;

            this.textBox1.Text = StaticValues.openfiles;
            this.textBox2.Text = StaticValues.savefiles;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = "xlsx";
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Filter = "EXCEL文件|*.xlsx";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text= ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StaticValues.openfiles = this.textBox1.Text;
            StaticValues.savefiles = this.textBox2.Text;
            this.Close();

            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = "dwg";
            ofd.CheckFileExists = false;
            ofd.CheckPathExists = true;
            ofd.Filter = "DWG文件|*.dwg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = ofd.FileName;
            }
        }
    }
}
