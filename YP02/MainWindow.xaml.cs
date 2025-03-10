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

        public static string UserRole { get; set; }

        public Pages.listPages.Student MainStudent = new Student(UserRole);
        public Pages.listPages.Group MainGroup = new Pages.listPages.Group(UserRole);
        public Pages.listPages.ConsultationResult MainConsultationResult = new Pages.listPages.ConsultationResult(UserRole);
        public Pages.listPages.DisciplineProgram MainDisciplineProgram = new Pages.listPages.DisciplineProgram(UserRole);
        public Pages.listPages.LessonType MainLessonType = new Pages.listPages.LessonType(UserRole);
        public Pages.listPages.Role MainRole = new Pages.listPages.Role(UserRole);
        public Pages.listPages.User MainUser = new Pages.listPages.User(UserRole);
        public Pages.listPages.Mark MainMark = new Pages.listPages.Mark(UserRole);
        public Pages.listPages.Teacher MainTeacher = new Pages.listPages.Teacher(UserRole);
        public Pages.listPages.Consultation MainConsultation = new Pages.listPages.Consultation(UserRole);
        public Pages.listPages.Discipline MainDiscipline = new Pages.listPages.Discipline(UserRole);
        public Pages.listPages.TeachersLoad MainTeachersLoad = new Pages.listPages.TeachersLoad(UserRole);
        public Pages.listPages.Absence MainAbsence = new Pages.listPages.Absence(UserRole);


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

            absenceAdd, absenceEdit,
            studentAdd, studentEdit,
            disciplineAdd, disciplineEdit,
            disciplineProgramAdd, disciplineProgramEdit,
            teacherAdd, teacherEdit,
            teachersLoadAdd, teachersLoadEdit,
            markAdd, markEdit,
            consultationResultAdd, consultationResultEdit,
            lessonTypeAdd, lessonTypeEdit,
            roleAdd, roleEdit,
            userAdd, userEdit,
            consultationAdd, consultationEdit
        }

        public void OpenPages(pages _pages, 
            Models.StudGroups sgm = null, 
            Models.Students ms = null, 
            Models.DisciplinePrograms mdp = null,
            Models.Disciplines md = null,
            Models.TeachersLoad mtl = null,
            Models.Consultations mc = null,
            Models.Users mu = null,
            Models.Roles mr = null,
            Models.LessonTypes mlt = null,
            Models.Absences ma = null,
            Models.Teachers mt = null,
            Models.Marks mm = null,
            Models.ConsultationResults mcr = null)
        {
            this.MinHeight = 800;
            this.MinWidth = 950;
            this.Height = 850;
            this.Width = 1000;
            string role = MainWindow.UserRole;
            switch (_pages)
            {
                case pages.authorization:
                    frame.Navigate(new Pages.Authorization());
                    break;

                case pages.main:
                    frame.Navigate(new Pages.Main(role));
                    break;

                case pages.student:
                    frame.Navigate(new Pages.listPages.Student(role));
                    break;

                case pages.group:
                    frame.Navigate(new Pages.listPages.Group(role));
                    break;

                case pages.discipline:
                    frame.Navigate(new Pages.listPages.Discipline(role));
                    break;

                case pages.disciplineProgram:
                    frame.Navigate(new Pages.listPages.DisciplineProgram(role));
                    break;

                case pages.teachersLoad:
                    frame.Navigate(new Pages.listPages.TeachersLoad(role));
                    break;

                case pages.consultation:
                    frame.Navigate(new Pages.listPages.Consultation(role));
                    break;

                case pages.absence:
                    frame.Navigate(new Pages.listPages.Absence(role));
                    break;

                case pages.teacher:
                    frame.Navigate(new Pages.listPages.Teacher(role));
                    break;

                case pages.marks:
                    frame.Navigate(new Pages.listPages.Mark(role));
                    break;

                case pages.consultationResult:
                    frame.Navigate(new Pages.listPages.ConsultationResult(role));
                    break;

                case pages.lessonType:
                    frame.Navigate(new Pages.listPages.LessonType(role));
                    break;

                case pages.role:
                    frame.Navigate(new Pages.listPages.Role(role));
                    break;

                case pages.user:
                    frame.Navigate(new Pages.listPages.User(role));
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

                case pages.disciplineAdd:
                    frame.Navigate(new Pages.Add.DisciplineAdd(MainDiscipline));
                    break;

                case pages.teachersLoadAdd:
                    frame.Navigate(new Pages.Add.TeacherLoadAdd(MainTeachersLoad));
                    break;

                case pages.absenceAdd:
                    frame.Navigate(new Pages.Add.AbsenceAdd(MainAbsence));
                    break;

                case pages.disciplineEdit:
                    frame.Navigate(new Pages.Edit.DisciplineEdit(MainDiscipline, md));
                    break;

                case pages.studentEdit:
                    frame.Navigate(new Pages.Edit.StudentEdit(MainStudent, ms));
                    break;

                case pages.teachersLoadEdit:
                    frame.Navigate(new Pages.Edit.TeacherLoadEdit(MainTeachersLoad, mtl));
                    break;

                case pages.consultationEdit:
                    frame.Navigate(new Pages.Edit.ConsultationEdit(MainConsultation, mc));
                    break;

                case pages.userEdit:
                    frame.Navigate(new Pages.Edit.UserEdit(MainUser, mu));
                    break;

                case pages.roleEdit:
                    frame.Navigate(new Pages.Edit.RoleEdit(MainRole, mr));
                    break;

                case pages.lessonTypeEdit:
                    frame.Navigate(new Pages.Edit.LessonTypeEdit(MainLessonType, mlt));
                    break;

                case pages.absenceEdit:
                    frame.Navigate(new Pages.Edit.AbsenceEdit(MainAbsence, ma));
                    break;

                case pages.teacherEdit:
                    frame.Navigate(new Pages.Edit.TeacherEdit(MainTeacher, mt));
                    break;

                case pages.markEdit:
                    frame.Navigate(new Pages.Edit.MarkEdit(MainMark, mm));
                    break;

                case pages.consultationResultEdit:
                    frame.Navigate(new Pages.Edit.ConsultationResultEdit(MainConsultationResult, mcr));
                    break;
            }
        }
    }
}