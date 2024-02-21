using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        internal enum Grids
        {
            Pro,
            Cour,
            Stud,
            Enroll
        }
        internal static Form1 current;

        private Grids grid;

        public Form1()
        {
            current = this;
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            new Form2();
            Form2.current.Visible = false;

            dataGridView1.Dock = DockStyle.Fill;
        }
        //------------------------------------------------------------------

        private void ProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grid = Grids.Pro;
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            bindingSource1.DataSource = Data.Programs.GetPrograms();
            bindingSource1.Sort = "ProgId";
            dataGridView1.DataSource = bindingSource1;

            dataGridView1.Columns["ProgName"].HeaderText = "Programs Name";
            dataGridView1.Columns["ProgId"].DisplayIndex = 0;
            dataGridView1.Columns["ProgName"].DisplayIndex = 1;
            

        }
        //------------------------------------------------------------------

        private void CoursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grid = Grids.Cour;
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            bindingSource2.DataSource = Data.Courses.GetCourses();
            bindingSource2.Sort = "CId";
            dataGridView1.DataSource = bindingSource2;

            dataGridView1.Columns["CName"].HeaderText = "Courses Name";
            dataGridView1.Columns["CId"].DisplayIndex = 0;
            dataGridView1.Columns["CName"].DisplayIndex = 1;
            dataGridView1.Columns["ProgId"].DisplayIndex = 2;
        }
        //------------------------------------------------------------------

        private void StudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
  
            grid = Grids.Stud;
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            bindingSource3.DataSource = Data.Students.GetStudents();
            bindingSource3.Sort = "StId";
            dataGridView1.DataSource = bindingSource3;

            dataGridView1.Columns["StName"].HeaderText = "Students Name";
            dataGridView1.Columns["StId"].DisplayIndex = 0;
            dataGridView1.Columns["StName"].DisplayIndex = 1;
            dataGridView1.Columns["ProgId"].DisplayIndex = 2;
         
        }
        //------------------------------------------------------------------

        private void EnrollmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grid != Grids.Enroll)
            {
                grid = Grids.Enroll;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource4.DataSource = Data.Enrollments.GetDisplayEnrollments();
                bindingSource4.Sort = "StId, CId";
                dataGridView1.DataSource = bindingSource4;
               

                //dataGridView1.Columns["StName"].HeaderText = "Students Name";
                //dataGridView1.Columns["StId"].DisplayIndex = 0;
                //dataGridView1.Columns["StName"].DisplayIndex = 1;
                //dataGridView1.Columns["FinalGrade"].DisplayIndex = 2;
            }
        }
        //------------------------------------------------------------------

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            BusinessLayer.Programs.UpdatePrograms();
        }
        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            BusinessLayer.Courses.UpdateCourses();
        }

        private void bindingSource3_CurrentChanged(object sender, EventArgs e)
        {
            BusinessLayer.Students.UpdateStudents();
        }
        
        private void bindingSource4_CurrentChanged(object sender, EventArgs e)
        {
            BusinessLayer.Enrollments.UpdateEnrollments();
        }
  
        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            //BusinessLayer.Enrollments.UpdateEnrollments();
            BusinessLayer.Programs.UpdatePrograms();
            BusinessLayer.Courses.UpdateCourses();
            BusinessLayer.Students.UpdateStudents();
            
        }
    
        internal static void BLLMessage(string s)
        {
            MessageBox.Show("Business Layer: " + s);
        }

        internal static void DALMessage(string s)
        {
            MessageBox.Show("Data Layer: " + s);
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("A line must be selected for update");
            }
            else if (c.Count > 1)
            {
                MessageBox.Show("Only one line must be selected for update!");
            }
            else
            {
                Form2 newform2 = new Form2();
                newform2.Setup(c);
                newform2.ShowDialog();
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("A line must be selected for update");
            }
            else if (c.Count > 1)
            {
                MessageBox.Show("Only one line must be selected for update!");
            }
            else
            {
                Form2 newform2 = new Form2();
                newform2.Setup(c);
                newform2.ShowDialog();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("At least one line must be selected for deletion!");
            }
            else // (c.Count > 1)
            {
                List<int[]> lId = new List<int[]>();
                for (int i = 0; i < c.Count; i++)
                {
                    lId.Add(new int[] { int.Parse("" + c[i].Cells["StId"].Value),
                                            int.Parse("" + c[i].Cells["CId"].Value),
                                            int.Parse("" + c[i].Cells["StName"].Value),
                                            int.Parse("" + c[i].Cells["CName"].Value),});
                }
                Data.Enrollments.DeleteData(lId);
            }
        }
   
       
        
        private void finalGradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("A line must be selected for update final grade");
            }
            else if (c.Count > 1)
            {
                MessageBox.Show("Only one line must be selected for update final grade");
            }
            else
            {
                Form3 newform3 = new Form3();
                newform3.Start(c);
                newform3.ShowDialog();
            }
            

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            MessageBox.Show("Impossible to insert / update");
        }
    }
}