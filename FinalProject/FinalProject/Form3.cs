using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using static FinalProject.Form2;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinalProject
{
    public partial class Form3 : Form
    {
        internal enum Modes
        {
            INSERT,
            UPDATE
        }

        internal static Form3 current;

        private Modes mode = Modes.UPDATE;

        private int[] EnrollInitial;
        public Form3()
        {
            current = this;
            InitializeComponent();
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text;

        }
        internal void Start(DataGridViewSelectedRowCollection c)
        {
           

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;

            textBox1.Text = (string)c[0].Cells["StId"].Value;
            textBox2.Text = (string)c[0].Cells["StName"].Value;
            textBox3.Text = (string)c[0].Cells["CId"].Value;
            textBox4.Text = (string)c[0].Cells["CName"].Value;



            ShowDialog();
        }
        public void InstantiateMyNumericUpDown()
        {
            numericUpDown1 = new NumericUpDown();
            numericUpDown1.Dock = System.Windows.Forms.DockStyle.Top;

            numericUpDown1.Value = 5;
            numericUpDown1.Maximum =100;
            numericUpDown1.Minimum = 60;

            Controls.Add(numericUpDown1);
        }

      
        

        //------------------------------------------------------------------

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //------------------------------------------------------------------
        private void TextBox1(object sender, EventArgs e)
        {
           
        }
        private void TextBox2(object sender, EventArgs e)
        {

        }
        private void TextBox3(object sender, EventArgs e)
        {
            

        }

        private void TextBox4(object sender, EventArgs e)
        {
            

        }
        //------------------------------------------------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            
            int r = -1;

          
            if (mode == Modes.INSERT)
            {
                r = Data.Enrollments.InsertData(new int[] { (int)numericUpDown1.Value });
            }
            if (mode == Modes.UPDATE)
            {
                List<int[]> lId = new List<int[]>();
                lId.Add(EnrollInitial);

                r = Data.Enrollments.InsertData(new int[] { (int)numericUpDown1.Value });

                if (r == 0)
                {
                    r = Data.Enrollments.DeleteData(lId);
                }

            }

            if (r == 0) { Close(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Close();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
        //------------------------------------------------------------------

        //private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        //{
        //    MessageBox.Show(numericUpDown1.Value.ToString());
        //}

    
       
    }
}