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
using YP02.Pages.listPages;

namespace YP02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;

        public Pages.listPages.Student MainStudent = new Student();
        public Models.Students students = new Models.Students();

        public Pages.listPages.Group MainGroup = new Pages.listPages.Group();
        public Models.StudGroups studGroups = new Models.StudGroups();

        public Pages.listPages.ConsultationResult MainConsultationResult = new Pages.listPages.ConsultationResult();

        public Pages.listPages.DisciplineProgram MainDisciplineProgram = new Pages.listPages.DisciplineProgram();

        public Pages.listPages.LessonType MainLessonType = new Pages.listPages.LessonType();

        public Pages.listPages.Role MainRole = new Pages.listPages.Role();

        public Pages.listPages.User MainUser = new Pages.listPages.User();

        public Pages.listPages.Mark MainMark = new Pages.listPages.Mark();

        public Pages.listPages.Teacher MainTeacher = new Pages.listPages.Teacher();

        public Pages.listPages.Consultation MainConsultation = new Pages.listPages.Consultation();


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
            groupeAdd, groupeEdit,
            studentAdd,
            disciplineProgramAdd, disciplineProgramEdit,
            teacherAdd,
            markAdd,
            consultationResultAdd,
            lessonTypeAdd,
            roleAdd,
            userAdd,
            consultationAdd

        }

        public void OpenPages(pages _pages, Models.StudGroups sgm = null, Models.Students ms = null, Models.DisciplinePrograms mdp = null)
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

                case pages.studentAdd:
                    frame.Navigate(new Pages.Add.StudentAdd(MainStudent));
                    break;

                case pages.disciplineProgramAdd:
                    frame.Navigate(new Pages.Add.DisciplineProgramAdd(MainDisciplineProgram));
                    break;

                case pages.disciplineProgramEdit:
                    frame.Navigate(new Pages.Edit.DisciplineProgramEdit(MainDisciplineProgram, mdp));
                    break;


                case pages.consultationResultAdd:
                    frame.Navigate(new Pages.Add.ConsultationResultAdd(MainConsultationResult));
                    break;

                case pages.lessonTypeAdd:
                    frame.Navigate(new Pages.Add.LessonTypeAdd(MainLessonType));
                    break;

                case pages.roleAdd:
                    frame.Navigate(new Pages.Add.RoleAdd(MainRole));
                    break;

                case pages.userAdd:
                    frame.Navigate(new Pages.Add.UserAdd(MainUser));
                    break;

                case pages.markAdd:
                    frame.Navigate(new Pages.Add.MarkAdd(MainMark));
                    break;

                case pages.teacherAdd:
                    frame.Navigate(new Pages.Add.TeacherAdd(MainTeacher));
                    break;

                case pages.consultationAdd:
                    frame.Navigate(new Pages.Add.ConsultationAdd(MainConsultation));
                    break;
            }
        }
    }
}