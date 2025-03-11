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
        public static MainWindow init;  // Статическая ссылка на текущий экземпляр MainWindow

        public static string UserRole { get; set; }  // Статическая переменная для хранения роли пользователя

        // Инициализация страниц с учетом роли пользователя
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

        // Конструктор, инициализирующий окно и открывающий страницу авторизации
        public MainWindow()
        {
            InitializeComponent();
            init = this;  // Присваиваем текущий экземпляр MainWindow в статическую переменную
            OpenPages(pages.authorization);  // Открываем страницу авторизации
        }

        // Перечисление страниц для навигации
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

        // Метод для открытия разных страниц в зависимости от переданного параметра
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
            // Устанавливаем размеры окна
            this.MinHeight = 800;
            this.MinWidth = 950;
            this.Height = 850;
            this.Width = 1000;

            string role = MainWindow.UserRole;  // Получаем роль пользователя

            // В зависимости от выбранной страницы, навигация по фрейму
            switch (_pages)
            {
                case pages.authorization:
                    frame.Navigate(new Pages.Authorization());  // Открытие страницы авторизации
                    break;

                case pages.main:
                    frame.Navigate(new Pages.Main(role));  // Открытие главной страницы с учетом роли пользователя
                    break;

                case pages.student:
                    frame.Navigate(new Pages.listPages.Student(role));  // Открытие страницы "Студент"
                    break;

                case pages.group:
                    frame.Navigate(new Pages.listPages.Group(role));  // Открытие страницы "Группы"
                    break;

                case pages.discipline:
                    frame.Navigate(new Pages.listPages.Discipline(role));  // Открытие страницы "Дисциплины"
                    break;

                case pages.disciplineProgram:
                    frame.Navigate(new Pages.listPages.DisciplineProgram(role));  // Открытие страницы "Программы дисциплин"
                    break;

                case pages.teachersLoad:
                    frame.Navigate(new Pages.listPages.TeachersLoad(role));  // Открытие страницы "Нагрузка преподавателей"
                    break;

                case pages.consultation:
                    frame.Navigate(new Pages.listPages.Consultation(role));  // Открытие страницы "Консультации"
                    break;

                case pages.absence:
                    frame.Navigate(new Pages.listPages.Absence(role));  // Открытие страницы "Отсутствия"
                    break;

                case pages.teacher:
                    frame.Navigate(new Pages.listPages.Teacher(role));  // Открытие страницы "Преподаватели"
                    break;

                case pages.marks:
                    frame.Navigate(new Pages.listPages.Mark(role));  // Открытие страницы "Оценки"
                    break;

                case pages.consultationResult:
                    frame.Navigate(new Pages.listPages.ConsultationResult(role));  // Открытие страницы "Результаты консультаций"
                    break;

                case pages.lessonType:
                    frame.Navigate(new Pages.listPages.LessonType(role));  // Открытие страницы "Типы занятий"
                    break;

                case pages.role:
                    frame.Navigate(new Pages.listPages.Role(role));  // Открытие страницы "Роли"
                    break;

                case pages.user:
                    frame.Navigate(new Pages.listPages.User(role));  // Открытие страницы "Пользователи"
                    break;

                // Открытие страниц для добавления и редактирования сущностей
                case pages.groupeAdd:
                    frame.Navigate(new Pages.Add.GroupeAdd(MainGroup));  // Добавление группы
                    break;

                case pages.groupeEdit:
                    frame.Navigate(new Pages.Edit.GroupeEdit(MainGroup, sgm));  // Редактирование группы
                    break;

                case pages.studentAdd:
                    frame.Navigate(new Pages.Add.StudentAdd(MainStudent));  // Добавление студента
                    break;

                case pages.disciplineProgramAdd:
                    frame.Navigate(new Pages.Add.DisciplineProgramAdd(MainDisciplineProgram));  // Добавление программы дисциплины
                    break;

                case pages.disciplineProgramEdit:
                    frame.Navigate(new Pages.Edit.DisciplineProgramEdit(MainDisciplineProgram, mdp));  // Редактирование программы дисциплины
                    break;

                case pages.consultationResultAdd:
                    frame.Navigate(new Pages.Add.ConsultationResultAdd(MainConsultationResult));  // Добавление результата консультации
                    break;

                case pages.lessonTypeAdd:
                    frame.Navigate(new Pages.Add.LessonTypeAdd(MainLessonType));  // Добавление типа занятия
                    break;

                case pages.roleAdd:
                    frame.Navigate(new Pages.Add.RoleAdd(MainRole));  // Добавление роли
                    break;

                case pages.userAdd:
                    frame.Navigate(new Pages.Add.UserAdd(MainUser));  // Добавление пользователя
                    break;

                case pages.markAdd:
                    frame.Navigate(new Pages.Add.MarkAdd(MainMark));  // Добавление оценки
                    break;

                case pages.teacherAdd:
                    frame.Navigate(new Pages.Add.TeacherAdd(MainTeacher));  // Добавление преподавателя
                    break;

                case pages.consultationAdd:
                    frame.Navigate(new Pages.Add.ConsultationAdd(MainConsultation));  // Добавление консультации
                    break;

                case pages.disciplineAdd:
                    frame.Navigate(new Pages.Add.DisciplineAdd(MainDiscipline));  // Добавление дисциплины
                    break;

                case pages.teachersLoadAdd:
                    frame.Navigate(new Pages.Add.TeacherLoadAdd(MainTeachersLoad));  // Добавление нагрузки преподавателя
                    break;

                case pages.absenceAdd:
                    frame.Navigate(new Pages.Add.AbsenceAdd(MainAbsence));  // Добавление отсутствия
                    break;

                case pages.disciplineEdit:
                    frame.Navigate(new Pages.Edit.DisciplineEdit(MainDiscipline, md));  // Редактирование дисциплины
                    break;

                case pages.studentEdit:
                    frame.Navigate(new Pages.Edit.StudentEdit(MainStudent, ms));  // Редактирование студента
                    break;

                case pages.teachersLoadEdit:
                    frame.Navigate(new Pages.Edit.TeacherLoadEdit(MainTeachersLoad, mtl));  // Редактирование нагрузки преподавателя
                    break;

                case pages.consultationEdit:
                    frame.Navigate(new Pages.Edit.ConsultationEdit(MainConsultation, mc));  // Редактирование консультации
                    break;

                case pages.userEdit:
                    frame.Navigate(new Pages.Edit.UserEdit(MainUser, mu));  // Редактирование пользователя
                    break;

                case pages.roleEdit:
                    frame.Navigate(new Pages.Edit.RoleEdit(MainRole, mr));  // Редактирование роли
                    break;

                case pages.lessonTypeEdit:
                    frame.Navigate(new Pages.Edit.LessonTypeEdit(MainLessonType, mlt));  // Редактирование типа занятия
                    break;

                case pages.absenceEdit:
                    frame.Navigate(new Pages.Edit.AbsenceEdit(MainAbsence, ma));  // Редактирование отсутствия
                    break;

                case pages.teacherEdit:
                    frame.Navigate(new Pages.Edit.TeacherEdit(MainTeacher, mt));  // Редактирование преподавателя
                    break;

                case pages.markEdit:
                    frame.Navigate(new Pages.Edit.MarkEdit(MainMark, mm));  // Редактирование оценки
                    break;

                case pages.consultationResultEdit:
                    frame.Navigate(new Pages.Edit.ConsultationResultEdit(MainConsultationResult, mcr));  // Редактирование результата консультации
                    break;
            }
        }
    }
}