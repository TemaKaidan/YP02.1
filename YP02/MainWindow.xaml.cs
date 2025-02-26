using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YP02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;

        public Pages.listPages.Group MainGroup = new Pages.listPages.Group();
        public Models.StudGroups studGroups = new Models.StudGroups();

        public MainWindow()
        {
            InitializeComponent();
            init = this;
            OpenPages(pages.authorization);
        }

        public enum pages
        {
            authorization,
            main,
            student, group, discipline, disciplineProgram, teachersLoad, consultation, absence, teacher, marks, 
            consultationResult, lessonType, role, user,
            groupeAdd, groupeEdit
        }

        public void OpenPages(pages _pages, Models.StudGroups sgm = null)
        {
            this.MinHeight = 800;
            this.MinWidth = 950;
            this.Height = 850;
            this.Width = 1000;

            switch (_pages)
            {
                case pages.authorization:
                    frame.Navigate(new Pages.Authorization());
                    break;

                case pages.main:
                   frame.Navigate(new Pages.Main());
                    break;

                case pages.student:
                    frame.Navigate(new Pages.listPages.Student());
                    break;

                case pages.group:
                    frame.Navigate(new Pages.listPages.Group());
                    break;

                case pages.discipline:
                    frame.Navigate(new Pages.listPages.Discipline());
                    break;

                case pages.disciplineProgram:
                    frame.Navigate(new Pages.listPages.DisciplineProgram());
                    break;

                case pages.teachersLoad:
                    frame.Navigate(new Pages.listPages.TeachersLoad());
                    break;

                case pages.consultation:
                    frame.Navigate(new Pages.listPages.Consultation());
                    break;

                case pages.absence:
                    frame.Navigate(new Pages.listPages.Absence());
                    break;

                case pages.teacher:
                    frame.Navigate(new Pages.listPages.Teacher());
                    break;

                case pages.marks:
                    frame.Navigate(new Pages.listPages.Mark());
                    break;

                case pages.consultationResult:
                    frame.Navigate(new Pages.listPages.ConsultationResult());
                    break;

                case pages.lessonType:
                    frame.Navigate(new Pages.listPages.LessonType());
                    break;

                case pages.role:
                    frame.Navigate(new Pages.listPages.Role());
                    break;

                case pages.user:
                    frame.Navigate(new Pages.listPages.User());
                    break;

                case pages.groupeAdd:
                    frame.Navigate(new Pages.Add.GroupeAdd(MainGroup));
                    break;

                case pages.groupeEdit:
                    frame.Navigate(new Pages.Edit.GroupeEdit(MainGroup, sgm));
                    break;
            }
        }
    }
}