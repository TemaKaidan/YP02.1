-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3312
-- Время создания: Фев 26 2025 г., 11:02
-- Версия сервера: 5.7.39
-- Версия PHP: 8.1.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `UP02`
--
CREATE DATABASE IF NOT EXISTS `UP02` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `UP02`;

-- --------------------------------------------------------

--
-- Структура таблицы `Absences`
--

CREATE TABLE `Absences` (
  `id` int(11) NOT NULL,
  `studentId` int(11) NOT NULL,
  `disciplineId` int(11) NOT NULL,
  `date` datetime NOT NULL,
  `delayMinutes` int(11) NOT NULL,
  `explanatoryNote` text COLLATE utf8mb4_unicode_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Absences`
--

INSERT INTO `Absences` (`id`, `studentId`, `disciplineId`, `date`, `delayMinutes`, `explanatoryNote`) VALUES
(1, 1, 1, '2025-02-13 00:00:00', 20, 'Есть'),
(2, 2, 2, '2025-02-13 00:00:00', 10, 'Есть');

-- --------------------------------------------------------

--
-- Структура таблицы `ConsultationResults`
--

CREATE TABLE `ConsultationResults` (
  `id` int(11) NOT NULL,
  `consultationId` int(11) NOT NULL,
  `studentId` int(11) NOT NULL,
  `presence` varchar(3) COLLATE utf8mb4_unicode_ci NOT NULL,
  `submittedPractice` text COLLATE utf8mb4_unicode_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `ConsultationResults`
--

INSERT INTO `ConsultationResults` (`id`, `consultationId`, `studentId`, `presence`, `submittedPractice`) VALUES
(1, 1, 1, 'Да', 'ПР13'),
(2, 2, 2, 'Нет', 'ПР15');

-- --------------------------------------------------------

--
-- Структура таблицы `Consultations`
--

CREATE TABLE `Consultations` (
  `id` int(11) NOT NULL,
  `disciplineId` int(11) NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Consultations`
--

INSERT INTO `Consultations` (`id`, `disciplineId`, `date`) VALUES
(1, 1, '2025-02-13 00:00:00'),
(2, 2, '2025-02-13 00:00:00');

-- --------------------------------------------------------

--
-- Структура таблицы `DisciplinePrograms`
--

CREATE TABLE `DisciplinePrograms` (
  `id` int(11) NOT NULL,
  `disciplineId` int(11) NOT NULL,
  `theme` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `lessonTypeId` int(11) NOT NULL,
  `hoursCount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `DisciplinePrograms`
--

INSERT INTO `DisciplinePrograms` (`id`, `disciplineId`, `theme`, `lessonTypeId`, `hoursCount`) VALUES
(1, 1, 'С#', 1, 20),
(2, 2, 'UML', 2, 20);

-- --------------------------------------------------------

--
-- Структура таблицы `Disciplines`
--

CREATE TABLE `Disciplines` (
  `id` int(11) NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `teacherId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Disciplines`
--

INSERT INTO `Disciplines` (`id`, `name`, `teacherId`) VALUES
(1, 'МДК0101', 1),
(2, 'мдк0104', 2);

-- --------------------------------------------------------

--
-- Структура таблицы `LessonTypes`
--

CREATE TABLE `LessonTypes` (
  `id` int(11) NOT NULL,
  `typeName` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `LessonTypes`
--

INSERT INTO `LessonTypes` (`id`, `typeName`) VALUES
(1, 'Лекция'),
(2, 'Практика');

-- --------------------------------------------------------

--
-- Структура таблицы `Marks`
--

CREATE TABLE `Marks` (
  `id` int(11) NOT NULL,
  `date` datetime NOT NULL,
  `mark` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `disciplineProgramId` int(11) NOT NULL,
  `studentId` int(11) NOT NULL,
  `description` text COLLATE utf8mb4_unicode_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Marks`
--

INSERT INTO `Marks` (`id`, `date`, `mark`, `disciplineProgramId`, `studentId`, `description`) VALUES
(1, '2025-02-18 00:00:00', '5', 1, 1, 'Отлично'),
(2, '2025-02-18 00:00:00', '4', 2, 2, 'Удовлетворительно');

-- --------------------------------------------------------

--
-- Структура таблицы `Roles`
--

CREATE TABLE `Roles` (
  `id` int(11) NOT NULL,
  `roleName` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Roles`
--

INSERT INTO `Roles` (`id`, `roleName`) VALUES
(1, 'Студент'),
(2, 'Преподаватель'),
(3, 'Администратор');

-- --------------------------------------------------------

--
-- Структура таблицы `Students`
--

CREATE TABLE `Students` (
  `id` int(11) NOT NULL,
  `surname` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `lastname` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `studGroupId` int(11) NOT NULL,
  `dateOfRemand` datetime DEFAULT NULL,
  `userId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Students`
--

INSERT INTO `Students` (`id`, `surname`, `name`, `lastname`, `studGroupId`, `dateOfRemand`, `userId`) VALUES
(1, 'Мохов', 'Артём ', 'Александрович', 1, '2025-02-21 00:00:00', 1),
(2, 'Граф', 'Данил', 'Станиславович', 2, '2025-02-21 00:00:00', 1),
(3, 'йцу', 'йцуйцу', 'йцуйцуйцу', 2, '2025-02-18 13:27:46', 1),
(4, 'йцу', 'йцуйцу', 'йцуйцуйцу', 2, '2025-02-18 13:27:46', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `StudGroups`
--

CREATE TABLE `StudGroups` (
  `id` int(11) NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `StudGroups`
--

INSERT INTO `StudGroups` (`id`, `name`) VALUES
(1, 'ИСП-21-2'),
(2, 'ИСП-21-4');

-- --------------------------------------------------------

--
-- Структура таблицы `Teachers`
--

CREATE TABLE `Teachers` (
  `id` int(11) NOT NULL,
  `surname` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `lastname` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `userId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Teachers`
--

INSERT INTO `Teachers` (`id`, `surname`, `name`, `lastname`, `userId`) VALUES
(1, 'Ощепков', 'Александр', 'Олегович', 1),
(2, 'Суслонова', 'Мария', 'Лазаревна', 2);

-- --------------------------------------------------------

--
-- Структура таблицы `TeachersLoad`
--

CREATE TABLE `TeachersLoad` (
  `id` int(11) NOT NULL,
  `teacherId` int(11) NOT NULL,
  `disciplineId` int(11) NOT NULL,
  `studGroupId` int(11) NOT NULL,
  `lectureHours` int(11) NOT NULL,
  `practiceHours` int(11) NOT NULL,
  `examHours` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `TeachersLoad`
--

INSERT INTO `TeachersLoad` (`id`, `teacherId`, `disciplineId`, `studGroupId`, `lectureHours`, `practiceHours`, `examHours`) VALUES
(1, 1, 1, 1, 20, 21, 5),
(2, 2, 2, 2, 30, 31, 2);

-- --------------------------------------------------------

--
-- Структура таблицы `Users`
--

CREATE TABLE `Users` (
  `id` int(11) NOT NULL,
  `login` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `role` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Users`
--

INSERT INTO `Users` (`id`, `login`, `password`, `role`) VALUES
(1, 'Student', 'Asdfg123', 1),
(2, 'Teacher', 'Qwerty123', 2),
(3, 'Admin', 'idol', 3);

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `Absences`
--
ALTER TABLE `Absences`
  ADD PRIMARY KEY (`id`),
  ADD KEY `studentId` (`studentId`),
  ADD KEY `disciplineId` (`disciplineId`);

--
-- Индексы таблицы `ConsultationResults`
--
ALTER TABLE `ConsultationResults`
  ADD PRIMARY KEY (`id`),
  ADD KEY `consultationId` (`consultationId`),
  ADD KEY `studentId` (`studentId`);

--
-- Индексы таблицы `Consultations`
--
ALTER TABLE `Consultations`
  ADD PRIMARY KEY (`id`),
  ADD KEY `disciplineId` (`disciplineId`);

--
-- Индексы таблицы `DisciplinePrograms`
--
ALTER TABLE `DisciplinePrograms`
  ADD PRIMARY KEY (`id`),
  ADD KEY `disciplineId` (`disciplineId`),
  ADD KEY `lessonTypeId` (`lessonTypeId`);

--
-- Индексы таблицы `Disciplines`
--
ALTER TABLE `Disciplines`
  ADD PRIMARY KEY (`id`),
  ADD KEY `teacherId` (`teacherId`);

--
-- Индексы таблицы `LessonTypes`
--
ALTER TABLE `LessonTypes`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `Marks`
--
ALTER TABLE `Marks`
  ADD PRIMARY KEY (`id`),
  ADD KEY `disciplineProgramId` (`id`),
  ADD KEY `studentId` (`studentId`),
  ADD KEY `marks_ibfk_1` (`disciplineProgramId`);

--
-- Индексы таблицы `Roles`
--
ALTER TABLE `Roles`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `Students`
--
ALTER TABLE `Students`
  ADD PRIMARY KEY (`id`),
  ADD KEY `studGroupId` (`studGroupId`),
  ADD KEY `userId` (`userId`);

--
-- Индексы таблицы `StudGroups`
--
ALTER TABLE `StudGroups`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `Teachers`
--
ALTER TABLE `Teachers`
  ADD PRIMARY KEY (`id`),
  ADD KEY `userId` (`userId`);

--
-- Индексы таблицы `TeachersLoad`
--
ALTER TABLE `TeachersLoad`
  ADD PRIMARY KEY (`id`),
  ADD KEY `teacherId` (`teacherId`),
  ADD KEY `disciplineId` (`disciplineId`),
  ADD KEY `studGroupId` (`studGroupId`);

--
-- Индексы таблицы `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`id`),
  ADD KEY `role` (`role`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `Absences`
--
ALTER TABLE `Absences`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `ConsultationResults`
--
ALTER TABLE `ConsultationResults`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `Consultations`
--
ALTER TABLE `Consultations`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `DisciplinePrograms`
--
ALTER TABLE `DisciplinePrograms`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `Disciplines`
--
ALTER TABLE `Disciplines`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `LessonTypes`
--
ALTER TABLE `LessonTypes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `Marks`
--
ALTER TABLE `Marks`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `Roles`
--
ALTER TABLE `Roles`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT для таблицы `Students`
--
ALTER TABLE `Students`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT для таблицы `StudGroups`
--
ALTER TABLE `StudGroups`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `Teachers`
--
ALTER TABLE `Teachers`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `TeachersLoad`
--
ALTER TABLE `TeachersLoad`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `Users`
--
ALTER TABLE `Users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `Absences`
--
ALTER TABLE `Absences`
  ADD CONSTRAINT `absences_ibfk_1` FOREIGN KEY (`studentId`) REFERENCES `Students` (`id`);

--
-- Ограничения внешнего ключа таблицы `ConsultationResults`
--
ALTER TABLE `ConsultationResults`
  ADD CONSTRAINT `consultationresults_ibfk_1` FOREIGN KEY (`consultationId`) REFERENCES `Consultations` (`id`),
  ADD CONSTRAINT `consultationresults_ibfk_2` FOREIGN KEY (`studentId`) REFERENCES `Students` (`id`);

--
-- Ограничения внешнего ключа таблицы `Consultations`
--
ALTER TABLE `Consultations`
  ADD CONSTRAINT `consultations_ibfk_1` FOREIGN KEY (`disciplineId`) REFERENCES `Disciplines` (`id`);

--
-- Ограничения внешнего ключа таблицы `DisciplinePrograms`
--
ALTER TABLE `DisciplinePrograms`
  ADD CONSTRAINT `disciplineprograms_ibfk_1` FOREIGN KEY (`disciplineId`) REFERENCES `Disciplines` (`id`),
  ADD CONSTRAINT `disciplineprograms_ibfk_2` FOREIGN KEY (`lessonTypeId`) REFERENCES `LessonTypes` (`id`);

--
-- Ограничения внешнего ключа таблицы `Disciplines`
--
ALTER TABLE `Disciplines`
  ADD CONSTRAINT `disciplines_ibfk_1` FOREIGN KEY (`teacherId`) REFERENCES `Teachers` (`id`);

--
-- Ограничения внешнего ключа таблицы `Marks`
--
ALTER TABLE `Marks`
  ADD CONSTRAINT `marks_ibfk_1` FOREIGN KEY (`disciplineProgramId`) REFERENCES `DisciplinePrograms` (`id`),
  ADD CONSTRAINT `marks_ibfk_2` FOREIGN KEY (`studentId`) REFERENCES `Students` (`id`);

--
-- Ограничения внешнего ключа таблицы `Students`
--
ALTER TABLE `Students`
  ADD CONSTRAINT `students_ibfk_1` FOREIGN KEY (`studGroupId`) REFERENCES `StudGroups` (`id`);

--
-- Ограничения внешнего ключа таблицы `Teachers`
--
ALTER TABLE `Teachers`
  ADD CONSTRAINT `teachers_ibfk_1` FOREIGN KEY (`userId`) REFERENCES `Users` (`id`);

--
-- Ограничения внешнего ключа таблицы `TeachersLoad`
--
ALTER TABLE `TeachersLoad`
  ADD CONSTRAINT `teachersload_ibfk_2` FOREIGN KEY (`disciplineId`) REFERENCES `Disciplines` (`id`),
  ADD CONSTRAINT `teachersload_ibfk_3` FOREIGN KEY (`studGroupId`) REFERENCES `StudGroups` (`id`);

--
-- Ограничения внешнего ключа таблицы `Users`
--
ALTER TABLE `Users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`role`) REFERENCES `Roles` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
