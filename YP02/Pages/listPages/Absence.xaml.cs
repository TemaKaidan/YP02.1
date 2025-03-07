﻿using Microsoft.EntityFrameworkCore;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YP02.Context;
using YP02.Pages.Item;

namespace YP02.Pages.listPages
{
    /// <summary>
    /// Логика взаимодействия для Absence.xaml
    /// </summary>
    public partial class Absence : Page
    {
        private bool isMenuCollapsed = false;
        public AbsencesContext _absencesContext = new AbsencesContext();

        public StudentsContext _studentsContext = new StudentsContext();

        public Absence()
        {
            InitializeComponent();
            CreateUI();

        }
        private void CreateUI()
        {
            parrent.Children.Clear();
            foreach (var x in _absencesContext.Absences.ToList())
            {
                parrent.Children.Add(new AbsenceItem(x, this));
            }
        }

        private async void KeyDown_Search(object sender, KeyEventArgs e) /// Поиск по ФИО 
        {
            string searchText = search.Text.ToLower();

            var foundStudents = await _studentsContext.Students
                .Where(t => t.surname.ToLower().Contains(searchText) ||
                             t.name.ToLower().Contains(searchText) ||
                             t.lastname.ToLower().Contains(searchText))
                .ToListAsync();

            parrent.Children.Clear();
            foreach (var student in foundStudents)
            {
                var studentAbsences = _absencesContext.Absences
                    .Where(a => a.studentId == student.id)
                    .ToList();

                foreach (var absence in studentAbsences)
                {
                    parrent.Children.Add(new Pages.Item.AbsenceItem(absence, this));
                }
            }
        }

        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                widthAnimation.From = 200;
                widthAnimation.To = 50;
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);
            isMenuCollapsed = !isMenuCollapsed;
        }

        private void Click_Student(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.student);
        }

        private void Click_Groups(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.group);
        }

        private void Click_Disciplines(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.discipline);
        }

        private void Click_DisciplinePrograms(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram);
        }

        private void Click_TeachersLoad(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teachersLoad);
        }

        private void Click_Consultations(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultation);
        }

        private void Click_Teachers(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teacher);
        }

        private void Click_Marks(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.marks);
        }

        private void Click_ConsultationResults(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultationResult);
        }

        private void Click_LessonTypes(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.lessonType);
        }

        private void Click_Roles(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.role);
        }

        private void Click_Users(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.user);
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.absenceAdd);
        }
    }
}
