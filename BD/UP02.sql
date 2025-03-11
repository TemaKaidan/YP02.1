-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3312
-- Время создания: Мар 11 2025 г., 13:51
-- Версия сервера: 8.0.30
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

-- --------------------------------------------------------

--
-- Структура таблицы `Absences`
--

CREATE TABLE `Absences` (
  `id` int NOT NULL,
  `studentId` int NOT NULL,
  `disciplineId` int NOT NULL,
  `delayMinutes` int NOT NULL,
  `explanatoryNote` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Absences`
--

INSERT INTO `Absences` (`id`, `studentId`, `disciplineId`, `delayMinutes`, `explanatoryNote`) VALUES
(1, 1, 1, 20, 'Есть'),
(2, 2, 2, 10, 'Нет'),
(8, 2, 1, 123, 'Нет'),
(12, 12, 2, 22, 'Есть');

-- --------------------------------------------------------

--
-- Структура таблицы `ConsultationResults`
--

CREATE TABLE `ConsultationResults` (
  `id` int NOT NULL,
  `consultationId` int NOT NULL,
  `studentId` int NOT NULL,
  `presence` varchar(3) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `submittedPractice` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `date` datetime NOT NULL DEFAULT '2025-01-01 00:00:00',
  `explanatoryNote` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `ConsultationResults`
--

INSERT INTO `ConsultationResults` (`id`, `consultationId`, `studentId`, `presence`, `submittedPractice`, `date`, `explanatoryNote`) VALUES
(1, 1, 1, 'Да', 'ПР13', '2025-01-01 00:00:00', 'Да'),
(2, 2, 2, 'Нет', 'ПР15', '2025-01-01 00:00:00', 'Нет'),
(14, 1, 12, 'Да', 'ПР23', '2025-03-08 00:00:00', 'Нет');

-- --------------------------------------------------------

--
-- Структура таблицы `Consultations`
--

CREATE TABLE `Consultations` (
  `id` int NOT NULL,
  `disciplineId` int NOT NULL,
  `submittedWorks` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Consultations`
--

INSERT INTO `Consultations` (`id`, `disciplineId`, `submittedWorks`, `date`) VALUES
(1, 1, 'ПР1', '2025-02-13 00:00:00'),
(2, 2, 'ПР2, ПР3', '2025-02-13 00:00:00'),
(4, 14, 'ПР22, ПР23, ПР25', '2025-03-05 00:00:00');

-- --------------------------------------------------------

--
-- Структура таблицы `DisciplinePrograms`
--

CREATE TABLE `DisciplinePrograms` (
  `id` int NOT NULL,
  `disciplineId` int NOT NULL,
  `theme` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `lessonTypeId` int NOT NULL,
  `hoursCount` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `DisciplinePrograms`
--

INSERT INTO `DisciplinePrograms` (`id`, `disciplineId`, `theme`, `lessonTypeId`, `hoursCount`) VALUES
(1, 1, 'С#', 1, 2),
(2, 2, 'UML', 2, 6),
(5, 14, 'Сервер-клиент', 1, 6),
(9, 1, 'qwe', 1, 2);

-- --------------------------------------------------------

--
-- Структура таблицы `Disciplines`
--

CREATE TABLE `Disciplines` (
  `id` int NOT NULL,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `teacherId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Disciplines`
--

INSERT INTO `Disciplines` (`id`, `name`, `teacherId`) VALUES
(1, 'МДК 01.01', 1),
(2, 'МДК 01.04', 2),
(14, 'МДК 02.02', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `ErrorLogs`
--

CREATE TABLE `ErrorLogs` (
  `Id` int NOT NULL,
  `ErrorMessage` varchar(1000) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `StackTrace` text COLLATE utf8mb4_unicode_ci,
  `DateTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `AdditionalInfo` varchar(1000) COLLATE utf8mb4_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `LessonTypes`
--

CREATE TABLE `LessonTypes` (
  `id` int NOT NULL,
  `typeName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL
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
  `id` int NOT NULL,
  `mark` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `disciplineProgramId` int NOT NULL,
  `studentId` int NOT NULL,
  `description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Marks`
--

INSERT INTO `Marks` (`id`, `mark`, `disciplineProgramId`, `studentId`, `description`) VALUES
(1, '5', 1, 1, 'Отлично'),
(2, '4', 2, 2, 'Хорошо'),
(8, '2', 5, 12, 'Неудовлетворительно');

-- --------------------------------------------------------

--
-- Структура таблицы `Roles`
--

CREATE TABLE `Roles` (
  `id` int NOT NULL,
  `roleName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL
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
  `id` int NOT NULL,
  `surname` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `lastname` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `studGroupId` int NOT NULL,
  `dateOfRemand` datetime DEFAULT NULL,
  `userId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Students`
--

INSERT INTO `Students` (`id`, `surname`, `name`, `lastname`, `studGroupId`, `dateOfRemand`, `userId`) VALUES
(1, 'Мохов', 'Артём ', 'Александрович', 1, '2025-02-21 00:00:00', 1),
(2, 'Граф', 'Данил', 'Станиславович', 2, '2025-02-20 00:00:00', 1),
(12, 'Еловиков', 'Степан', 'Алексеевич', 30, '2025-03-03 00:00:00', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `StudGroups`
--

CREATE TABLE `StudGroups` (
  `id` int NOT NULL,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `StudGroups`
--

INSERT INTO `StudGroups` (`id`, `name`) VALUES
(1, 'ИСП-21-2'),
(2, 'ИСП-21-4'),
(30, 'ИСВ-21-1');

-- --------------------------------------------------------

--
-- Структура таблицы `Teachers`
--

CREATE TABLE `Teachers` (
  `id` int NOT NULL,
  `surname` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `lastname` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `login` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `userId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Teachers`
--

INSERT INTO `Teachers` (`id`, `surname`, `name`, `lastname`, `login`, `password`, `userId`) VALUES
(1, 'Ощепков', 'Александр', 'Олегович', 'log1', 'pass1', 2),
(2, 'Суслонова', 'Мария', 'Лазаревна', 'log2', 'pass2', 2);

-- --------------------------------------------------------

--
-- Структура таблицы `TeachersLoad`
--

CREATE TABLE `TeachersLoad` (
  `id` int NOT NULL,
  `teacherId` int NOT NULL,
  `disciplineId` int NOT NULL,
  `studGroupId` int NOT NULL,
  `lectureHours` int NOT NULL,
  `practiceHours` int NOT NULL,
  `сonsultationHours` int NOT NULL,
  `courseprojectHours` int NOT NULL,
  `examHours` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `TeachersLoad`
--

INSERT INTO `TeachersLoad` (`id`, `teacherId`, `disciplineId`, `studGroupId`, `lectureHours`, `practiceHours`, `сonsultationHours`, `courseprojectHours`, `examHours`) VALUES
(1, 1, 1, 1, 20, 21, 5, 5, 5),
(10, 2, 14, 30, 22, 44, 11, 0, 22);

-- --------------------------------------------------------

--
-- Структура таблицы `Users`
--

CREATE TABLE `Users` (
  `id` int NOT NULL,
  `login` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `role` int NOT NULL
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
-- Индексы таблицы `ErrorLogs`
--
ALTER TABLE `ErrorLogs`
  ADD PRIMARY KEY (`Id`);

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
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT для таблицы `ConsultationResults`
--
ALTER TABLE `ConsultationResults`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT для таблицы `Consultations`
--
ALTER TABLE `Consultations`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT для таблицы `DisciplinePrograms`
--
ALTER TABLE `DisciplinePrograms`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT для таблицы `Disciplines`
--
ALTER TABLE `Disciplines`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT для таблицы `ErrorLogs`
--
ALTER TABLE `ErrorLogs`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT для таблицы `LessonTypes`
--
ALTER TABLE `LessonTypes`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT для таблицы `Marks`
--
ALTER TABLE `Marks`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT для таблицы `Roles`
--
ALTER TABLE `Roles`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT для таблицы `Students`
--
ALTER TABLE `Students`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT для таблицы `StudGroups`
--
ALTER TABLE `StudGroups`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;

--
-- AUTO_INCREMENT для таблицы `Teachers`
--
ALTER TABLE `Teachers`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT для таблицы `TeachersLoad`
--
ALTER TABLE `TeachersLoad`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT для таблицы `Users`
--
ALTER TABLE `Users`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `Absences`
--
ALTER TABLE `Absences`
  ADD CONSTRAINT `absences_ibfk_1` FOREIGN KEY (`studentId`) REFERENCES `Students` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `ConsultationResults`
--
ALTER TABLE `ConsultationResults`
  ADD CONSTRAINT `consultationresults_ibfk_1` FOREIGN KEY (`consultationId`) REFERENCES `Consultations` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `consultationresults_ibfk_2` FOREIGN KEY (`studentId`) REFERENCES `Students` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `Consultations`
--
ALTER TABLE `Consultations`
  ADD CONSTRAINT `consultations_ibfk_1` FOREIGN KEY (`disciplineId`) REFERENCES `Disciplines` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `DisciplinePrograms`
--
ALTER TABLE `DisciplinePrograms`
  ADD CONSTRAINT `disciplineprograms_ibfk_1` FOREIGN KEY (`disciplineId`) REFERENCES `Disciplines` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `disciplineprograms_ibfk_2` FOREIGN KEY (`lessonTypeId`) REFERENCES `LessonTypes` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `Disciplines`
--
ALTER TABLE `Disciplines`
  ADD CONSTRAINT `disciplines_ibfk_1` FOREIGN KEY (`teacherId`) REFERENCES `Teachers` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `Marks`
--
ALTER TABLE `Marks`
  ADD CONSTRAINT `marks_ibfk_1` FOREIGN KEY (`disciplineProgramId`) REFERENCES `DisciplinePrograms` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `marks_ibfk_2` FOREIGN KEY (`studentId`) REFERENCES `Students` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `Students`
--
ALTER TABLE `Students`
  ADD CONSTRAINT `students_ibfk_1` FOREIGN KEY (`studGroupId`) REFERENCES `StudGroups` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `Teachers`
--
ALTER TABLE `Teachers`
  ADD CONSTRAINT `teachers_ibfk_1` FOREIGN KEY (`userId`) REFERENCES `Users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `TeachersLoad`
--
ALTER TABLE `TeachersLoad`
  ADD CONSTRAINT `teachersload_ibfk_2` FOREIGN KEY (`disciplineId`) REFERENCES `Disciplines` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `teachersload_ibfk_3` FOREIGN KEY (`studGroupId`) REFERENCES `StudGroups` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `Users`
--
ALTER TABLE `Users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`role`) REFERENCES `Roles` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
