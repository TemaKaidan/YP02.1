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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YP02.Context;
using YP02.Models;

namespace YP02.Pages.listPages
{
    /// <summary>
    /// Логика взаимодействия для Discipline.xaml
    /// </summary>
    public partial class Discipline : Page
    {
        private bool isMenuCollapsed = false;
        public DisciplinesContext _disciplinesContext = new DisciplinesContext();

        public TeachersContext _teachersContext = new TeachersContext();

        public Discipline()
        {
            InitializeComponent();
            CreateUI();
        }
        private void CreateUI()
        {
            parrent.Children.Clear();
            var teachers = _teachersContext.Teachers.ToList();
            foreach (var x in _disciplinesContext.Disciplines.ToList())
            {
                parrent.Children.Add(new Pages.Item.DisciplineItem(x, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();
            var result = _disciplinesContext.Disciplines.Where(x =>
                x.name.ToLower().Contains(searchText)
            );
            parrent.Children.Clear();
            foreach (var item in result)
            {
                parrent.Children.Add(new Pages.Item.DisciplineItem(item, this));
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

        private void Click_Absences(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.absence);
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
            MainWindow.init.OpenPages(MainWindow.pages.disciplineAdd);
        }
    }
}
