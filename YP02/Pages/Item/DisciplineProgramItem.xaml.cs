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
    /// Логика взаимодействия для DisciplineProgramItem.xaml
    /// </summary>
    public partial class DisciplineProgramItem : UserControl
    {
        private DisciplinePrograms _disciplinePrograms;
        private readonly DisciplinesContext _DisciplinesContext;
        private readonly LessonTypesContext _lessonTypesContext;

        public DisciplineProgramItem(DisciplinePrograms disciplinePrograms)
        {
            InitializeComponent();
            _disciplinePrograms = disciplinePrograms;

            DisciplinesContext _disciplinesContext = new DisciplinesContext();
            var disciplinesContext = _disciplinesContext.Disciplines.FirstOrDefault(g => g.id == disciplinePrograms.disciplineId);
            lb_disciplineId.Content = "Дисциплина: " + (disciplinesContext != null ? disciplinesContext.name : "Неизвестно");

            lb_theme.Content = "Тема: " + disciplinePrograms.theme;

            LessonTypesContext _lessonTypesContext = new LessonTypesContext();
            var lessonTypesContext = _lessonTypesContext.LessonTypes.FirstOrDefault(g => g.id == disciplinePrograms.lessonTypeId);
            lb_lessonTypeId.Content = "Тип: " + (lessonTypesContext != null ? lessonTypesContext.typeName : "Неизвестно");

            lb_hoursCount.Content = "Количество часов: " + disciplinePrograms.hoursCount;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
