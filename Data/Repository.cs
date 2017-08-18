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

        public  void SaveChanges()
        {
             dbSchoolContext.SaveChanges();
        }
        public IList<Department> getDepartments()
        {
            return dbSchoolContext.Departments.Include(d => d.Administrator).ToList<Department>();
        }


        public Department getDepartment(int id)
        {
            return dbSchoolContext.Departments.Find(id);
        }
        public void addDepartment(Department department) {
            dbSchoolContext.Departments.Add(department);
        }
        public void updDepartment(Department department)
        {
            dbSchoolContext.Entry(department).State = EntityState.Modified;
        }

        public void removeDepartament(int id)
        {
            Department department = dbSchoolContext.Departments.Find(id);
            dbSchoolContext.Departments.Remove(department);
        }
        public IList<Course> getCourses()
        {
            return dbSchoolContext.Courses.ToList<Course>();

        }
        public Course getCourse(int courseID)
        {
            return dbSchoolContext.Courses.Where(x => x.CourseID == courseID).FirstOrDefault<Course>();
        }
        public void addCourse(Course course)
        {
            dbSchoolContext.Courses.Add(course);
        }
        public void updCourse(Course course)
        {
            dbSchoolContext.Entry(course).State = EntityState.Modified;
        }
        public void removeCourse(int id)
        {
            Course course = dbSchoolContext.Courses.Find(id);
            dbSchoolContext.Courses.Remove(course);
        }
        public IList<Person> getPeople()
        {
            return dbSchoolContext.People.ToList<Person>();

        }
        public Person getPerson(int PersonID)
        {
            return dbSchoolContext.People.Where(x => x.PersonID == PersonID).FirstOrDefault<Person>();
        }


    }
}
