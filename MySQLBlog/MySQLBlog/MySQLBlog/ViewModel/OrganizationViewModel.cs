using MySQLBlog.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLBlog.ViewModel
{
    public class OrganizationViewModel : NotificationBase
    {
        Organization organization;

        public OrganizationViewModel(String name)
        {
            organization = new Organization(name);
            _SelectedIndex = -1;
            // Load the database
            foreach (var student in organization.Students)
            {
                var np = new StudentViewModel(student);
                np.PropertyChanged += Person_OnNotifyPropertyChanged;
                _students.Add(np);
            }
        }

        ObservableCollection<StudentViewModel> _students
           = new ObservableCollection<StudentViewModel>();
        public ObservableCollection<StudentViewModel> Student
        {
            get { return _students; }
            set { SetProperty(ref _students, value); }
        }

        String _Name;
        public String Name
        {
            get { return organization.Name; }
        }

        int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (SetProperty(ref _SelectedIndex, value))
                { RaisePropertyChanged(nameof(SelectedPerson)); }
            }
        }

        public StudentViewModel SelectedPerson
        {
            get { return (_SelectedIndex >= 0) ? _students[_SelectedIndex] : null; }
        }

        public void Add()
        {
            var person = new StudentViewModel();
            person.PropertyChanged += Person_OnNotifyPropertyChanged;
            Student.Add(person);
            organization.Add(person);
            SelectedIndex = Student.IndexOf(person);
        }

        public void Delete()
        {
            if (SelectedIndex != -1)
            {
                var person = Student[SelectedIndex];
                Student.RemoveAt(SelectedIndex);
                //Student.Remove(person);
                organization.Delete(person);
            }
        }

        void Person_OnNotifyPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            organization.Update((StudentViewModel)sender);
        }
    }
}
