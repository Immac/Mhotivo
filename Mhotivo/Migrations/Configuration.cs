using AutoMapper.Mappers;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Mhotivo.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MhotivoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Mhotivo.Implement.Context.MhotivoContext";
        }

        protected override void Seed(MhotivoContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Roles.AddOrUpdate(new Role { Id = 1, Description = "Admin", Name = "Admin" });
            context.Roles.AddOrUpdate(new Role { Id = 2, Description = "Principal", Name = "Principal" });
            context.SaveChanges();
            context.Users.AddOrUpdate(new User
            {
                Id = 1,
                DisplayName = "Alex Fernandez",
                Email = "olorenzo@outlook.com",
                Password = "123",
                Role = context.Roles.First(),
                Status = true
            });
            context.Users.AddOrUpdate(new User
            {
                Id = 2,
                DisplayName = "Franklin Castellanos",
                Email = "castellarfrank@hotmail.com",
                Password = "siniestro",
                Role = context.Roles.First(),
                Status = true
            });
            context.Users.AddOrUpdate(new User
            {
                Id = 3,
                DisplayName = "La directora",
                Email = "holis@holis.com",
                Password = "holis",
                Role = context.Roles.Find(2),
                Status = true
            });
            context.Areas.AddOrUpdate(new Area { Id = 1, Name = "Ciencias Sociales" });

            context.Courses.AddOrUpdate(new Course
            {
                Id = 2,
                Area = new Area { Id = 1, Name = "Ciencias Sociales" },
                Name = "Estudios Sociales"
            });

            context.AcademicYears.AddOrUpdate(new AcademicYear
            {
                Approved = true,
                Grade = new Grade { EducationLevel = "Primaria", Id = 1, Name = "Segundo Grado" },
                Id = 2,
                IsActive = true,
                Section = "A",
                Year = 2015
            });
            context.AcademicYearDetails.AddOrUpdate(new AcademicYearDetail
            {
                Id = 1,
                AcademicYear = new AcademicYear
                    {
                        Approved = true,
                        Grade = new Grade { EducationLevel = "Primaria", Id = 1, Name = "Primer Grado" },
                        Id = 1,
                        IsActive = true,
                        Section = "A",
                        Year = 2015
                    },
                Course = new Course
                {
                    Id = 2,
                    Area = new Area { Id = 1, Name = "Ciencias Sociales" },
                    Name = "Sociologia"
                },
                Room = "203",
                Schedule = null,
                Teacher = new Meister
                {
                    Address = "aaaa",
                    Biography = "bbbb",
                    BirthDate = "19/Noviembre",
                    City = "San Pedro",
                    Contacts = null,
                    Country = "Honduras",
                    Disable = false,
                    EndDate = null,
                    FirstName = "Juan",
                    FullName = "Juan Perez"
                }
            });
        }
    }
}