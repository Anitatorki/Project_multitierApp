using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinalProject
{
    public partial class Form2 : Form
    {
        internal enum Modes
        {
            INSERT,
            UPDATE

        }
        internal static Form2 current;
        private Modes mode = Modes.INSERT;
        private int[] EnrollInitial;
        public Form2()
        {
            current = this;
            InitializeComponent();

        }

        internal void Setup(DataGridViewSelectedRowCollection c)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;

            comboBox1.SelectedValue = c[0].Cells["StId"].Value;
            textBox1.Text = (string)c[0].Cells["StName"].Value;

            comboBox2.SelectedValue = c[0].Cells["CId"].Value;
            textBox2.Text = (string)c[0].Cells["CName"].Value;


            Text = "" + mode;

            comboBox1.DisplayMember = "StId";
            comboBox1.ValueMember = "StId";
            comboBox1.DataSource = Data.Enrollments.GetEnrollments();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;

            comboBox2.DisplayMember = "CId";
            comboBox2.ValueMember = "CId";
            comboBox2.DataSource = Data.Enrollments.GetEnrollments();
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.SelectedIndex = 0;

            //textBox1.ReadOnly = true;
            //textBox2.ReadOnly = true;

            //if ((mode == Modes.UPDATE) && (c != null))
            //{
            //    comboBox1.SelectedValue = c[0].Cells["StId"].Value;
            //    comboBox2.SelectedValue = c[0].Cells["CId"].Value;
            //    EnrollInitial = new int[] { (int)c[0].Cells["StId"].Value, (int)c[0].Cells["CId"].Value };
            //}

            ShowDialog();

        }


        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (comboBox1.SelectedItem != null)
            //{
            //    var a = from r in Data.Enrollments.GetEnrollments().AsEnumerable()
            //            where r.Field<string>("StId") == (string)comboBox1.SelectedValue
            //            select new { Name = r.Field<string>("StName") };
            //    textBox1.Text = a.Single().Name;
            //}
        }
        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {



            //if (comboBox2.SelectedItem != null)
            //{
            //    var a = from r in Data.Enrollments.GetEnrollments().AsEnumerable()
            //            where r.Field<string>("CId") == (string)comboBox2.SelectedValue
            //            select new { Name = r.Field<string>("CName") };
            //    textBox2.Text = a.Single().Name;
            //}
        }

        //-----------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            int r = -1;
            if (mode == Modes.INSERT)
            {
                r = Data.Enrollments.InsertData(new int[] { (int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue });
            }
            if (mode == Modes.UPDATE)
            {
                List<int[]> lId = new List<int[]>();
                lId.Add(EnrollInitial);

                r = Data.Enrollments.InsertData(new int[] { (int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue });

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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            textBox1.Text = comboBox1.SelectedValue.ToString();




        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = comboBox1.SelectedValue.ToString();

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        //---------------------------------------------------------------
        private void Form2_Load(object sender, EventArgs e)
        {
            funcomboBox1student();
            funcomboBox2Cource();
        }
        private void funcomboBox1student()
        {
            comboBox1.DataSource = Data.Students.GetStudents();
            comboBox1.ValueMember = "StName";
            comboBox1.DisplayMember = "StId";
        }


        private void funcomboBox2Cource()
        {
            comboBox2.DataSource = Data.Courses.GetCourses();
            comboBox2.ValueMember = "CName";
            comboBox2.DisplayMember = "CId";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.SelectedValue.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = comboBox1.SelectedValue.ToString();

        }

       
    

}
}