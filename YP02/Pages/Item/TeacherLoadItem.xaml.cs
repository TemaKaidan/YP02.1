using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YP02.Context;
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для TeacherLoadItem.xaml
    /// </summary>
    public partial class TeacherLoadItem : UserControl
    {
        private Models.TeachersLoad _teachersLoad;

        private readonly TeachersContext _teachersContext;
        private readonly DisciplinesContext _disciplinesContext;
        private readonly StudGroupsContext _studGroupsContext;

        public TeacherLoadItem(Models.TeachersLoad teachersLoad)
        {
            InitializeComponent();
            _teachersLoad = teachersLoad;

            TeachersContext _teachersContext = new TeachersContext();
            var teacher = _teachersContext.Teachers.FirstOrDefault(g => g.id == teachersLoad.teacherId);
            lb_teacherId.Content = "Преподователь: " + (teacher != null ? teacher.surname : "Неизвестно ") + " " + (teacher != null ? teacher.name : "Неизвестно ") + " " + (teacher != null ? teacher.lastname : "Неизвестно ");

            DisciplinesContext _disciplinesContext = new DisciplinesContext();
            var disciplinesContext = _disciplinesContext.Disciplines.FirstOrDefault(g => g.id == teachersLoad.disciplineId);
            lb_disciplineId.Content = "Дисциплина: " + (disciplinesContext != null ? disciplinesContext.name : "Неизвестно");

            StudGroupsContext _groupe = new StudGroupsContext();
            var group = _groupe.StudGroups.FirstOrDefault(g => g.id == teachersLoad.studGroupId);
            lb_studGroupId.Content = "Группа: " + (group != null ? group.name : "Неизвестно");

            lb_lectureHours.Content = "Часы лекций: " + teachersLoad.lectureHours;
            lb_practiceHours.Content = "Часы практик: " + teachersLoad.practiceHours;
            lb_examHours.Content = "Часы экзамена: " + teachersLoad.examHours;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}