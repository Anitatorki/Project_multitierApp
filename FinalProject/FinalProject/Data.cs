using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data
{
    internal class Connect
    {

        private static String cliComConnectionString = GetConnectString();

        internal static String ConnectionString { get => cliComConnectionString; }

        private static String GetConnectString()
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = "(local)";
            cs.InitialCatalog = "College1en";
            cs.UserID = "sa";
            cs.Password = "sysadm";
            return cs.ConnectionString;
        }
    }

    internal class DataTables
    {
        private static SqlDataAdapter adapterStudents = InitAdapterStudents();
        private static SqlDataAdapter adapterEnrollments = InitAdapterEnrollments();
        private static SqlDataAdapter adapterCourses = InitAdapterCourses();
        private static SqlDataAdapter adapterPrograms = InitAdapterPrograms();


        private static DataSet ds = InitDataSet();

        private static SqlDataAdapter InitAdapterStudents()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Students ORDER BY StId",
                Connect.ConnectionString
                );

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }

        private static SqlDataAdapter InitAdapterCourses()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Courses ORDER BY CId",
                Connect.ConnectionString
            );

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }

        private static SqlDataAdapter InitAdapterPrograms()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Programs ORDER BY ProgId",
                Connect.ConnectionString
            );

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }

        private static SqlDataAdapter InitAdapterEnrollments()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Enrollments ORDER BY StId, CId",
                Connect.ConnectionString
            );

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }


        private static DataSet InitDataSet()
        {
            DataSet ds = new DataSet();


            loadPrograms(ds);
            loadCourses(ds);
            loadStudents(ds);
            loadEnrollments(ds);



            return ds;
        }

        //------------------------------------------------------------------

        private static void loadStudents(DataSet ds)
        {
            adapterStudents.Fill(ds, "Students");

            ds.Tables["Students"].Columns["StId"].AllowDBNull = false;
            ds.Tables["Students"].Columns["StName"].AllowDBNull = false;
            ds.Tables["Students"].Columns["ProgId"].AllowDBNull = false;

            ds.Tables["Students"].PrimaryKey = new DataColumn[1]
            {
                ds.Tables["Students"].Columns["StId"]
            };

        }
        //------------------------------------------------------------------

        private static void loadCourses(DataSet ds)
        {
            adapterCourses.Fill(ds, "Courses");

            ds.Tables["Courses"].Columns["CId"].AllowDBNull = false;
            ds.Tables["Courses"].Columns["CName"].AllowDBNull = false;
            //ds.Tables["Courses"].Columns["ProgId"].AllowDBNull = false;

            ds.Tables["Courses"].PrimaryKey = new DataColumn[1]
            {
        ds.Tables["Courses"].Columns["CId"]
            };

            //foreign key Courses and Programs tables
            ForeignKeyConstraint fkConstraint = new ForeignKeyConstraint("FK_Courses_Programs",
                ds.Tables["Programs"].Columns["ProgId"],
                ds.Tables["Courses"].Columns["ProgId"]);

            fkConstraint.UpdateRule = Rule.Cascade;
            fkConstraint.DeleteRule = Rule.Cascade;

            ds.Tables["Courses"].Constraints.Add(fkConstraint);
        }
        //------------------------------------------------------------------

        private static void loadEnrollments(DataSet ds)
        {
            adapterEnrollments.Fill(ds, "Enrollments");

            ds.Tables["Enrollments"].Columns["StId"].AllowDBNull = false;
            ds.Tables["Enrollments"].Columns["CId"].AllowDBNull = false;

            ds.Tables["Enrollments"].PrimaryKey = new DataColumn[2]
            {
                ds.Tables["Enrollments"].Columns["StId"],
                ds.Tables["Enrollments"].Columns["CId"]
            };

            //foreign key Enrollments and Students tables
            ForeignKeyConstraint fkConstraint1 = new ForeignKeyConstraint("FK_Enrollments_Students",
                ds.Tables["Students"].Columns["StId"],
                ds.Tables["Enrollments"].Columns["StId"]);

            fkConstraint1.UpdateRule = Rule.Cascade;
            fkConstraint1.DeleteRule = Rule.Cascade;

            ds.Tables["Enrollments"].Constraints.Add(fkConstraint1);

            //foreign key Enrollments and Courses tables
            ForeignKeyConstraint fkConstraint2 = new ForeignKeyConstraint("FK_Enrollments_Courses",
                ds.Tables["Courses"].Columns["CId"],
                ds.Tables["Enrollments"].Columns["CId"]);

            fkConstraint2.UpdateRule = Rule.None;
            fkConstraint2.DeleteRule = Rule.None;

            ds.Tables["Enrollments"].Constraints.Add(fkConstraint2);



        }
        //------------------------------------------------------------------

        private static void loadPrograms(DataSet ds)
        {
            adapterPrograms.Fill(ds, "Programs");

            ds.Tables["Programs"].Columns["ProgId"].AllowDBNull = false;
            ds.Tables["Programs"].Columns["ProgName"].AllowDBNull = false;

            ds.Tables["Programs"].PrimaryKey = new DataColumn[1]
            {
        ds.Tables["Programs"].Columns["ProgId"]
            };
        }

        internal static SqlDataAdapter getAdapterPrograms()
        {
            return adapterPrograms;
        }

        internal static SqlDataAdapter getAdapterCourses()
        {
            return adapterCourses;
        }

        internal static SqlDataAdapter getAdapterStudents()
        {
            return adapterStudents;
        }

        internal static SqlDataAdapter getAdapterEnrollments()
        {
            return adapterEnrollments;
        }

        internal static DataSet getDataSet()
        {
            return ds;
        }

    }

    //------------------------------------------------------------------
    internal class Programs
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterPrograms();
        private static DataSet ds = DataTables.getDataSet();
        static bool init = false;

        internal static DataTable GetPrograms()
        {
            if (!init)
            {
                adapter.Fill(ds, "Programs");

                init = true;
            }
            return ds.Tables["Programs"];
        }

        internal static int UpdatePrograms()
        {
            if (!ds.Tables["Programs"].HasErrors)
            {
                return adapter.Update(ds.Tables["Programs"]);
            }
            else
            {
                return -1;
            }
        }

        internal static void ReInitData()
        {
            init = false;
        }

    }
    //------------------------------------------------------------------

    internal class Courses
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterCourses();
        private static DataSet ds = DataTables.getDataSet();
        static bool init = false;

        internal static DataTable GetCourses()
        {
            if (!init)
            {
                adapter.Fill(ds, "Courses");

                init = true;
            }
            return ds.Tables["Courses"];
        }

        internal static int UpdateCourses()
        {
            if (!ds.Tables["Courses"].HasErrors)
            {
                return adapter.Update(ds.Tables["Courses"]);
            }
            else
            {
                return -1;
            }
        }

        internal static void ReInitData()
        {
            init = false;
        }
    }

    //------------------------------------------------------------------
    internal class Students
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterStudents();
        private static DataSet ds = DataTables.getDataSet();
        static bool init = false;

        internal static DataTable GetStudents()
        {
            if (!init)
            {
                adapter.Fill(ds, "Students");

                init = true;
            }
            return ds.Tables["Students"];
        }

        internal static int UpdateStudents()
        {
            if (!ds.Tables["Students"].HasErrors)
            {
                return adapter.Update(ds.Tables["Students"]);
            }
            else
            {
                return -1;
            }
        }

      
    }

    //------------------------------------------------------------------
    internal class Enrollments
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterEnrollments();
        private static DataSet ds = DataTables.getDataSet();
        private static DataTable mergedDataTable;

        static bool init = false;

        internal static DataTable GetEnrollments()
        {
            if (!init)
            {
                adapter.Fill(ds, "Enrollments");

                init = true;
            }
            return ds.Tables["Enrollments"];
        }



        internal static DataTable GetDisplayEnrollments()
        {
            if (!init)
            {
                adapter.Fill(ds, "Enrollments");

                var query = (
             from enrollment in ds.Tables["Enrollments"].AsEnumerable()
             from student in ds.Tables["Students"].AsEnumerable()
             from course in ds.Tables["Courses"].AsEnumerable()
             where enrollment.Field<string>("StId") ==
             student.Field<string>("StId")
             where enrollment.Field<string>("CId") ==
             course.Field<string>("CId")
             select new
             {
                 StName = student.Field<string>("StName"),
                 CName = course.Field<string>("CName"),
                 StId = enrollment.Field<string>("StId"),
                 CId = enrollment.Field<string>("CId"),
                 FinalGrade = enrollment.Field<Nullable<int>>("FinalGrade")
             });
                mergedDataTable = new DataTable();
                mergedDataTable.Columns.Add("StId");
                mergedDataTable.Columns.Add("CId");
                mergedDataTable.Columns.Add("FinalGrade");
                mergedDataTable.Columns.Add("StName");
                mergedDataTable.Columns.Add("CName");
                foreach (var x in query)
                {
                    object[] allFields = { x.StId, x.CId, x.FinalGrade, x.StName, x.CName };
                    mergedDataTable.Rows.Add(allFields);
                }

                init = true;
            }

            return mergedDataTable;
        }

        internal static int UpdateEnrollments()
        {
            if (!ds.Tables["Enrollments"].HasErrors)
            {
                return adapter.Update(ds.Tables["Enrollments"]);
            }
            else
            {
                return -1;
            }
        }

        internal static void ReInitData()
        {
            init = false;

        }

        internal static int InsertData(int[] a)
        {
            var test = (
                              from Enroll in ds.Tables["Enrollments"].AsEnumerable()
                              where Enroll.Field<int>("StId") == a[0]
                              where Enroll.Field<int>("CId") == a[1]
                              select Enroll);
            if (test.Count() > 0)
            {
                FinalProject.Form1.DALMessage("This Enrollments already exists");
                return -1;
            }
            try
            {
                DataRow line = ds.Tables["Enrollments"].NewRow();
                line.SetField("EmpId", a[0]);
                line.SetField("ProjId", a[1]);
                ds.Tables["Enrollments"].Rows.Add(line);

                adapter.Update(ds.Tables["Enrollments"]);

                if (mergedDataTable != null)
                {
                    var query = (
                           from Stud in ds.Tables["Enrollments"].AsEnumerable()
                           from Cour in ds.Tables["Enrollments"].AsEnumerable()
                           where Stud.Field<int>("SId") == a[0]
                           where Cour.Field<int>("CId") == a[1]
                           select new
                           {
                               StId = Stud.Field<int>("SId"),
                               StName = Stud.Field<string>("StName"),
                               CId = Cour.Field<int>("CId"),
                               CName = Cour.Field<string>("CName")
                           });
                    var r = query.Single();
                    mergedDataTable.Rows.Add(new object[] { r.StId, r.StName, r.CId, r.CName });
                }
                return 0;
            }
            catch (Exception)
            {
                FinalProject.Form1.DALMessage("Insertion / Update rejected");
                return -1;
            }
        }
        internal static int UpdateData(int[] a)
        {
            return 0;  //not used
        }

        internal static int DeleteData(List<int[]> lId)
        {
            try
            {
                var lines = ds.Tables["Enrollments"].AsEnumerable()
                                .Where(s =>
                                   lId.Any(x => (x[0] == s.Field<int>("StId") && x[1] == s.Field<int>("CId"))));

                foreach (var line in lines)
                {
                    line.Delete();
                }

                adapter.Update(ds.Tables["Enrollments"]);

                if (mergedDataTable != null)
                {
                    foreach (var p in lId)
                    {
                        var r = mergedDataTable.AsEnumerable()
                                .Where(s => (s.Field<int>("StId") == p[0] && s.Field<int>("CId") == p[1]))
                                .Single();
                        mergedDataTable.Rows.Remove(r);
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                FinalProject.Form1.DALMessage("Update / Deletion rejected");
                return -1;
            }
        }
    }


}
