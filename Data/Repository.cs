using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
//using System.Data.Objects;
//using System.Data.Objects.DataClasses;
//https://msdn.microsoft.com/es-es/library/bb399731(v=vs.100).aspx
namespace SchoolDataBase
{
    public class Repository
    {
        private DbSchoolContext dbSchoolContext;
        public Repository()
        {
            dbSchoolContext = new DbSchoolContext(conectionString);
        }

        private string conectionString
        {
            get
            {
                return @"data source=(localdb)\MSSQLLocalDB;initial catalog=School;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";// System.Configuration.ConfigurationManager.ConnectionStrings["School"].ConnectionString;
            }
        }

        public IList<Course> getCourses()
        {
            return dbSchoolContext.Courses.ToList<Course>();

        }

        public Course getCourse(int courseID)
        {
            return dbSchoolContext.Courses.Where(x => x.CourseID == courseID).FirstOrDefault<Course>();
        }
    }
}
