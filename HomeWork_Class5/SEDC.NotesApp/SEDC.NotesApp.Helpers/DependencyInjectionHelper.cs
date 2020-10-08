using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.DataAccess.AdoNet;
using SEDC.NotesApp.DataAccess.Dapper;
using SEDC.NotesApp.Domain;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Services.Implementations;
using SEDC.NotesApp.Services.Interfaces;

namespace SEDC.NotesApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<NotesAppDbContext>(x =>
                x.UseSqlServer("Server=.;Database=NotesAppDb;Trusted_Connection=True"));
        }

        public static void InjectAdoRepositories(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<Note>>(x => new NoteAdoRepository(connectionString));
            services.AddTransient<IRepository<User>>(x => new UserAdoRepository(connectionString));
        }
        public static void InjectDapperRepositories(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<Note>>(x => new NoteDapperRepository(connectionString));
            services.AddTransient<IRepository<User>>(x => new UserDapperRepository(connectionString));
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<UserIService, UserService>();
        }
    }
}
