using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    internal class Programs
    {
        internal static int UpdatePrograms()
        {
            return Data.Programs.UpdatePrograms();
        }
    }

    //------------------------------------------------------------------

    internal class Courses
    {
        internal static int UpdateCourses()
        {
            return Data.Courses.UpdateCourses();


        }
    }
    //------------------------------------------------------------------

    internal class Students
    {
        

        internal static int UpdateStudents()
        {
            return Data.Students.UpdateStudents();
        }
    }
    //------------------------------------------------------------------

    internal class Enrollments
    {
        internal static int UpdateEnrollments()
        {
            DataSet ds = Data.DataTables.getDataSet();

            DataTable dt = ds.Tables["Projects"]
                              .GetChanges(DataRowState.Added | DataRowState.Modified);
            if ((dt != null) && (dt.Select("Duration < 3").Length > 0))
            {
                FinalProject.Form1.BLLMessage("Project Insertion/Update rejected: Duration less than 3");
                ds.Tables["Projects"].RejectChanges();
                return -1;
            }
            else
            {
                return Data.Enrollments.UpdateEnrollments();
            }

        }
        internal static void UpdateEnrollment()
        {
            throw new NotImplementedException();
        }
    }
}
