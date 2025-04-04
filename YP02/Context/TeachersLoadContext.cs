﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class TeachersLoadContext : DbContext
    {
        // Представление таблицы TeachersLoad в базе данных
        public DbSet<TeachersLoad> TeachersLoad { get; set; }

        // Конструктор контекста, который создает базу данных, если она не существует,
        // и загружает данные из таблицы TeachersLoad
        public TeachersLoadContext()
        {
            // Обеспечивает создание базы данных, если она еще не была создана
            Database.EnsureCreated();

            // Загружает все записи из таблицы TeachersLoad
            TeachersLoad.Load();
        }

        // Метод настройки контекста с использованием строки подключения и версии MySQL
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Настройка подключения к базе данных MySQL с использованием строки из Config
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }

}