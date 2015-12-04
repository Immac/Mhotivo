using System;
using System.Collections.Generic;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;
using Mhotivo.Implement.Repositories;
using Mhotivo.Interface.Interfaces;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Mhotivo.Implement.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<MhotivoContext>
    {
        private IEducationLevelRepository _areaRepository;
        private IGradeRepository _gradeRepository;
        private ICourseRepository _courseRepository;
        private IPensumRepository _pensumRepository;
        private IAcademicYearRepository _academicYearRepository;
        private IRoleRepository _roleRepository;
        private IUserRepository _userRepository;
        private ITeacherRepository _teacherRepository;
        private ITutorRepository _tutorRepository;
        private IAcademicGradeRepository _academicGradeRepository;
        private IAcademicCourseRepository _academicCourseRepository;
        private IPeopleWithUserRepository _peopleWithUserRepository;
        private IPrivilegeRepository _privilegeRepository;
        private INotificationRepository _notificationRepository;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MhotivoContext context)
        {
            if (context.Users.Any())
                return;
            _areaRepository = new EducationLevelRepository(context);
            _gradeRepository = new GradeRepository(context);
            _courseRepository = new CourseRepository(context);
            _pensumRepository = new PensumRepository(context);
            _academicYearRepository = new AcademicYearRepository(context);
            _roleRepository = new RoleRepository(context);
            _userRepository = new UserRepository(context);
            _teacherRepository = new TeacherRepository(context);
            _tutorRepository = new TutorRepository(context);
            _academicGradeRepository = new AcademicGradeRepository(context);
            _academicCourseRepository = new AcademicCourseRepository(context);
            _peopleWithUserRepository = new PeopleWithUserRepository(context);
            _privilegeRepository = new PrivilegeRepository(context);
            _notificationRepository = new NotificationRepository(context);

            var allRoles = new List<Role>();
            
            var tRole = _roleRepository.Create(new Role { Name = "Administrador", Id = 0 });
            _privilegeRepository.Create(new Privilege { Id = 0, Description = "Privilegio de nivel Administrador", Name = "Administrador", Roles = new List<Role> { tRole } });
            allRoles.Add(tRole);

            tRole = _roleRepository.Create(new Role { Name = "Tutor", Id = 1 });
            _privilegeRepository.Create(new Privilege { Id = 1, Description = "Privilegio de nivel Padre", Name = "Padre", Roles = new List<Role> { tRole } });
            allRoles.Add(tRole);

            tRole = _roleRepository.Create(new Role { Name = "Maestro", Id = 2 });
            _privilegeRepository.Create(new Privilege { Id = 1, Description = "Privilegio de nivel Maestro", Name = "Maestro", Roles = new List<Role> { tRole } });
            allRoles.Add(tRole);

            tRole = _roleRepository.Create(new Role { Name = "Director", Id = 3 });
            _privilegeRepository.Create(new Privilege { Id = 1, Description = "Privilegio de nivel Director", Name = "Director", Roles = new List<Role> { tRole } });
            allRoles.Add(tRole);

            tRole = _roleRepository.Create(new Role { Name = "Maestro de Seccion", Id = 4 });
            _privilegeRepository.Create(new Privilege { Id = 1, Description = "Privilegio de nivel Maestro de Seccion", Name = "Maestro de Seccion", Roles = new List<Role> { tRole } });
            allRoles.Add(tRole);

            _privilegeRepository.Create(new Privilege { Id = 1, Description = "Privilegio de Login", Name = "Login", Roles = allRoles});

            var adminPeople = new PeopleWithUser
            {
                Address = "",
                BirthDate = DateTime.UtcNow,
                City = "",
                FirstName = "Administrador",
                IsActive = true,
                IdNumber = "0000-0000-00000",
                LastName = "",
                MyGender = Gender.Masculino,
                Photo = null,
                State = ""
            };
            adminPeople.FullName = adminPeople.FirstName + "" + adminPeople.LastName;
            adminPeople = _peopleWithUserRepository.Create(adminPeople);
            var admin = new User
            {
                Email = "admin@mhotivo.org",
                Password = "password",
                IsActive = true,
                UserOwner = adminPeople,
                Role = _roleRepository.Filter(x => x.Name == "Administrador").FirstOrDefault()
            };
            admin = _userRepository.Create(admin);
            adminPeople.User = admin;
            _peopleWithUserRepository.Update(adminPeople);
            DebuggingSeeder();
        }

        private void DebuggingSeeder()
        {
            _areaRepository.Create(new EducationLevel { Name = "Pre-Escolar" });
            _areaRepository.Create(new EducationLevel { Name = "Primaria" });
            _areaRepository.Create(new EducationLevel { Name = "Secundaria" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(1), Name = "Kinder" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(1), Name = "Preparatoria" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(2), Name = "Primero" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(2), Name = "Segundo" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(2), Name = "Tercero" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(2), Name = "Cuarto" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(2), Name = "Quinto" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(2), Name = "Sexto" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(3), Name = "Septimo" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(3), Name = "Octavo" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(3), Name = "Noveno" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(3), Name = "Decimo" });
            _gradeRepository.Create(new Grade { EducationLevel = _areaRepository.GetById(3), Name = "Onceavo" });

            foreach (var grade in _gradeRepository.GetAllGrade())
            {
                var p = new Pensum{ Grade = grade, Name = "Pensum para " + grade.Name};
                p = _pensumRepository.Create(p);
                grade.Pensums.Add(p);
                _gradeRepository.Update(grade);
                var c1 = new Course {Name = "Ingles para " + grade.Name, Pensum = p};
                var c2 = new Course { Name = "Matematicas para " + grade.Name, Pensum = p };
                var c3 = new Course { Name = "Ciencias para " + grade.Name, Pensum = p };
                _courseRepository.Create(c1);
                _courseRepository.Create(c2);
                _courseRepository.Create(c3);
                p.Courses.Add(c1);
                p.Courses.Add(c2);
                p.Courses.Add(c3);
                _pensumRepository.Update(p);
            }

            var generTeacher = new Teacher
            {
                Address = "Jardines del Valle, 4 calle, 1 etapa",
                BirthDate = new DateTime(1993, 3, 8),
                City = "San Pedro Sula",
                IsActive = true,
                FirstName = "Jose",
                LastName = "Avila",
                FullName = "Jose Avila",
                IdNumber = "0501-1993-08405",
                MyGender = Gender.Masculino,
                State = "Cortes",
            };
            generTeacher = _teacherRepository.Create(generTeacher);
            var genericTeacher = new User
            {
                UserOwner = generTeacher,
                Email = "teacher@mhotivo.org",
                Password = "password",
                IsActive = true,
                Role = _roleRepository.Filter(x => x.Name == "Maestro").FirstOrDefault(),
                IsUsingDefaultPassword = false
            };
            genericTeacher = _userRepository.Create(genericTeacher);
            generTeacher.User = genericTeacher;
            generTeacher = _teacherRepository.Update(generTeacher);
            var academicYear = new AcademicYear { IsActive = true, Year = 2015, EnrollsOpen = true};
            academicYear = _academicYearRepository.Create(academicYear);
            for (int i = 1; i <= 13; i++)
            {
                var rnd = new Random();
                char section = 'A';
                do
                {
                    if (section == 'K')
                        break;
                    var grade = _gradeRepository.GetById(i);
                    var newGrade = new AcademicGrade
                    {
                        Grade = grade,
                        AcademicYear = academicYear,
                        Section = section++ + "",
                        ActivePensum = grade.Pensums.ElementAt(0)
                    };
                    newGrade = _academicGradeRepository.Create(newGrade);
                    foreach (var course in newGrade.ActivePensum.Courses)
                    {
                        var academicCourse = new AcademicCourse
                        {
                            Course = course,
                            AcademicGrade = newGrade,
                            Schedule = newGrade.CoursesDetails.Any() ? 
                            newGrade.CoursesDetails.Last().Schedule.Duration().Add(new TimeSpan(0, 40, 0)) : 
                            new TimeSpan(7, 0, 0),
                            Teacher = generTeacher //TODO: Create more teachers to randomize this a little bit more?
                        };
                        academicCourse = _academicCourseRepository.Create(academicCourse);
                        newGrade.CoursesDetails.Add(academicCourse);
                        newGrade = _academicGradeRepository.Update(newGrade);
                    }
                    academicYear.Grades.Add(newGrade);
                    _academicYearRepository.Update(academicYear);
                } while (rnd.Next(0, 2) == 0);
            }

            var allCourses = _academicCourseRepository.GetAllAcademicYearDetails();
            foreach (var academicCourse in allCourses)
            {
                academicCourse.Homeworks.Add(new Homework
                {
                    AcademicCourse = academicCourse,
                    DeliverDate = DateTime.UtcNow,
                    Description = "Tarea para " + academicCourse.Course.Name + " para hoy.",
                    Points = 5,
                    Title = "Tarea para hoy."
                });
                academicCourse.Homeworks.Add(new Homework
                {
                    AcademicCourse = academicCourse,
                    DeliverDate = DateTime.UtcNow.Subtract(new TimeSpan(2, 0, 0, 0)),
                    Description = "Tarea para " + academicCourse.Course.Name + " para ayer.",
                    Points = 5,
                    Title = "Tarea para ayer."
                });
                academicCourse.Homeworks.Add(new Homework
                {
                    AcademicCourse = academicCourse,
                    DeliverDate = DateTime.UtcNow.AddDays(2),
                    Description = "Tarea para " + academicCourse.Course.Name + " para maniana.",
                    Points = 5,
                    Title = "Tarea para maniana."
                });
                _academicCourseRepository.Update(academicCourse);
            }
            _notificationRepository.Create(new Notification
            {
                Title = "Notificacion General",
                AcademicYear = _academicYearRepository.GetCurrentAcademicYear(),
                Approved = false,
                Message = "Esta es una notificacion general.",
                NotificationCreator = _peopleWithUserRepository.GetById(1),
                NotificationType = NotificationType.General,
                SendEmail = false,
                Sent = false,
                DestinationId = -1
            });
            var educationLevels = _areaRepository.GetAllAreas();
            foreach (var educationLevel in educationLevels)
            {
                _notificationRepository.Create(new Notification
                {
                    Title = "Notificacion Para " + educationLevel.Name,
                    AcademicYear = _academicYearRepository.GetCurrentAcademicYear(),
                    Approved = false,
                    Message = "Esta es una notificacion de nivel de educacion.",
                    NotificationCreator = _peopleWithUserRepository.GetById(1),
                    NotificationType = NotificationType.EducationLevel,
                    SendEmail = false,
                    Sent = false,
                    DestinationId = educationLevel.Id
                });
            }
            var grades = _gradeRepository.GetAllGrade();
            foreach (var grade in grades)
            {
                _notificationRepository.Create(new Notification
                {
                    Title = "Notificacion Para " + grade.Name,
                    AcademicYear = _academicYearRepository.GetCurrentAcademicYear(),
                    Approved = false,
                    Message = "Esta es una notificacion de grado.",
                    NotificationCreator = _peopleWithUserRepository.GetById(1),
                    NotificationType = NotificationType.Grade,
                    SendEmail = false,
                    Sent = false,
                    DestinationId = grade.Id
                });
            }
            var academicGrades = _academicGradeRepository.GetAllGrades();
            foreach (var grade in academicGrades)
            {
                _notificationRepository.Create(new Notification
                {
                    Title = "Notificacion Para " + grade.Grade.Name + " " + grade.Section,
                    AcademicYear = _academicYearRepository.GetCurrentAcademicYear(),
                    Approved = false,
                    Message = "Esta es una notificacion de seccion.",
                    NotificationCreator = _peopleWithUserRepository.GetById(1),
                    NotificationType = NotificationType.Section,
                    SendEmail = false,
                    Sent = false,
                    DestinationId = grade.Id
                });
            }
            var academicCourses = _academicCourseRepository.GetAllAcademicYearDetails();
            foreach (var academicCourse in academicCourses)
            {

                _notificationRepository.Create(new Notification
                {
                    Title = "Notificacion Para " + academicCourse.Course.Name,
                    AcademicYear = _academicYearRepository.GetCurrentAcademicYear(),
                    Approved = false,
                    Message = "Esta es una notificacion de curso.",
                    NotificationCreator = _peopleWithUserRepository.GetById(1),
                    NotificationType = NotificationType.Course,
                    SendEmail = false,
                    Sent = false,
                    DestinationId = academicCourse.Id
                });
            }
            var generTutor = new Tutor
            {
                IdNumber = "0501-1956-03145",
                FirstName = "Tutor",
                LastName = "Generico",
                FullName = "Tutor Generico",
                IsActive = true,
                MyGender = Gender.Femenino,
                BirthDate = new DateTime(1956, 11, 23),
                Parentage = Parentage.Mother,
                City = "San Pedro Sula",
                State = "Cortes",
                Address = "Jardines del Valle, 4 Calle, 1 Etapa, #9D",
            };
            generTutor = _tutorRepository.Create(generTutor);
            var genericTutor = new User
            {
                UserOwner = generTutor,
                Email = "tutor@mhotivo.org",
                Password = "password",
                IsActive = true,
                Role = _roleRepository.Filter(x => x.Name == "Tutor").FirstOrDefault()
            };
            genericTutor = _userRepository.Create(genericTutor);
            generTutor.User = genericTutor;
            _tutorRepository.Update(generTutor);

            var director = new PeopleWithUser
            {
                Address = "",
                BirthDate = DateTime.UtcNow,
                City = "",
                FirstName = "Director Generico",
                IsActive = true,
                IdNumber = "0000-0000-00000",
                LastName = "",
                MyGender = Gender.Masculino,
                Photo = null,
                State = "",
            };
            director.FullName = director.FirstName + "" + director.LastName;
            director = _peopleWithUserRepository.Create(director);
            var dir = new User
            {
                Email = "director@mhotivo.org",
                Password = "password",
                IsActive = true,
                UserOwner = director,
                Role = _roleRepository.Filter(x => x.Name == "Director").FirstOrDefault()
            };
            dir = _userRepository.Create(dir);
            director.User = dir;
            _peopleWithUserRepository.Update(director);
        }
    }
}
