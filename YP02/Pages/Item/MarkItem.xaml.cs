﻿using System;
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
    /// Логика взаимодействия для MarkItem.xaml
    /// </summary>
    public partial class MarkItem : UserControl
    {
        Pages.listPages.Mark MainMark;
        Models.Marks marks;

        public MarkItem(Models.Marks marks, Mark MainMark)
        {
            InitializeComponent();
            this.marks = marks;
            this.MainMark = MainMark;

            lb_date.Content = "Дата: " + marks.date;
            lb_mark.Content = "Оценка: " + marks.mark;

            DisciplineProgramsContext _disciplinePrograms = new DisciplineProgramsContext();
            var disciplinePrograms = _disciplinePrograms.DisciplinePrograms.FirstOrDefault(g => g.id == marks.disciplineProgramId);
            lb_disciplineProgramId.Content = "Программа дисциплина: " + (disciplinePrograms != null ? disciplinePrograms.theme : "Неизвестно");

            StudentsContext _studentsContext = new StudentsContext();
            var studentsContext = _studentsContext.Students.FirstOrDefault(g => g.id == marks.studentId);
            lb_studentId.Content = "Студент: " + (studentsContext != null ? studentsContext.surname : "Неизвестно") + " " + (studentsContext != null ? studentsContext.name : "Неизвестно") + " " + (studentsContext != null ? studentsContext.lastname : "Неизвестно");

            lb_description.Content = "Описание: " + marks.description;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainMark._marksContext.Marks.Remove(marks);
                MainMark._marksContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
        }
    }
}
