using MySQLBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLBlog.ViewModel
{
    public class StudentViewModel : NotificationBase<Student>
    {
        public StudentViewModel(Student student = null) : base(student) { }

        public String Student_name
        {
            get { return This.Student_name; }
            set { SetProperty(This.Student_name, value, () => This.Student_name = value); }
        }
        public String Student_mark
        {
            get { return This.Student_mark; }
            set { SetProperty(This.Student_mark, value, () => This.Student_mark = value); }
        }
        public int Student_id
        {
            get { return This.Student_id; }
            set { SetProperty(This.Student_id, value, () => This.Student_id = value); }
        }
    }
}
