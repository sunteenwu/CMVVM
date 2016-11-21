using MySQLBlog.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLBlog.Model
{
    public class Organization
    {
        public ObservableCollection<Student> Students { get; set; }
        public String Name { get; set; }

        public Organization(String databaseName)
        {
            Name = databaseName;
            Students = DataService.GetStudents();
        }

        public void Add(Student student)
        {
            if (!Students.Contains(student))
            {
                Students.Add(student);
                DataService.InsertNewStudent(student);
            }
        }

        public void Delete(Student student)
        {
            if (Students.Contains(student))
            {
                Students.Remove(student);
                DataService.Delete(student);
            }
        }

        public void Update(Student student)
        {
            DataService.UpdateStudent(student);
        }
    }

}
