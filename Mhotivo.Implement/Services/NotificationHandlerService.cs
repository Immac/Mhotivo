﻿using System.Collections.Generic;
using System.Linq;
using Mhotivo.Data.Entities;
using Mhotivo.Interface.Interfaces;

namespace Mhotivo.Implement.Services
{
    public class NotificationHandlerService : INotificationHandlerService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IStudentRepository _suStudentRepository;
        private readonly IAcademicGradeRepository _academicGradeRepository;
        private readonly IAcademicCourseRepository _academicCourseRepository;
        private readonly IUserRepository _userRepository;

        public NotificationHandlerService(INotificationRepository notificationRepository, IStudentRepository suStudentRepository, 
            IAcademicGradeRepository academicGradeRepository, IAcademicCourseRepository academicCourseRepository, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _suStudentRepository = suStudentRepository;
            _academicGradeRepository = academicGradeRepository;
            _academicCourseRepository = academicCourseRepository;
            _userRepository = userRepository;
        }

        public List<Notification> GetAllActiveOfType(NotificationType type)
        {
            return _notificationRepository.Filter(x => x.NotificationType == type && x.AcademicYear.IsActive).ToList();
        }

        public List<Notification> GetAllOfTypeAndYear(NotificationType type, long yearId)
        {
            return _notificationRepository.Filter(x => x.NotificationType == type && x.AcademicYear.Id == yearId).ToList();
        }

        public void SendNotification(Notification notification)
        {
            if (notification.Sent)
                return;
            switch (notification.NotificationType)
            {
                case NotificationType.General:
                    var allGrades = _academicGradeRepository.Filter(x => x.AcademicYear.Id == notification.AcademicYear.Id).ToList();
                    foreach (var grade in allGrades)
                    {
                        SendToStudents(grade.Students, notification);
                    }
                    notification.Sent = true;
                    _notificationRepository.Update(notification);
                    break;
                case NotificationType.EducationLevel:
                    var gradesForLevel =
                        _academicGradeRepository.Filter(
                            x => x.Grade.EducationLevel.Id == notification.DestinationId &&
                                 x.AcademicYear.Id == notification.AcademicYear.Id).ToList();
                    foreach (var grade in gradesForLevel)
                    {
                        SendToStudents(grade.Students, notification);
                    }
                    notification.Sent = true;
                    _notificationRepository.Update(notification);
                    break;
                case NotificationType.Grade:
                    var grades =
                        _academicGradeRepository.Filter(x => x.Grade.Id == notification.DestinationId &&
                                                                 x.AcademicYear.Id == notification.AcademicYear.Id).ToList();
                    foreach (var grade in grades)
                    {
                        SendToStudents(grade.Students, notification);
                    }
                    notification.Sent = true;
                    _notificationRepository.Update(notification);
                    break;
                case NotificationType.Section:
                    var singleGrade =
                        _academicGradeRepository.Filter(x => x.Id == notification.DestinationId &&
                                                                 x.AcademicYear.Id == notification.AcademicYear.Id).FirstOrDefault();
                    if (singleGrade != null)
                    {
                        SendToStudents(singleGrade.Students, notification);
                        notification.Sent = true;
                        _notificationRepository.Update(notification);
                    }
                    break;
                case NotificationType.Course:
                    var course = _academicCourseRepository.Filter(x => x.Id == notification.DestinationId &&
                                                                           x.AcademicGrade.AcademicYear.Id ==
                                                                           notification.AcademicYear.Id).FirstOrDefault();
                    if (course != null)
                    {
                        SendToStudents(course.AcademicGrade.Students, notification);
                        notification.Sent = true;
                        _notificationRepository.Update(notification);
                    }
                    break;
                case NotificationType.Personal:
                    var singleStudent = _suStudentRepository.Filter(x => x.Id == notification.DestinationId).FirstOrDefault();
                    if (singleStudent != null)
                    {
                        SendToStudent(singleStudent, notification);
                        notification.Sent = true;
                        _notificationRepository.Update(notification);
                    }
                    break;
            }
        }

        private void SendToStudent(Student student, Notification notification)
        {
            if (student.Tutor1 != null && !student.Tutor1.User.Notifications.Contains(notification))
            {
                var user = student.Tutor1.User;
                user.Notifications.Add(notification);
                notification.RecipientUsers.Add(user);
                _userRepository.Update(user);
                _notificationRepository.Update(notification);
                if (notification.SendEmail)
                    EmailService.SendEmailToUser(student.Tutor1.User, notification);
            }
            if (student.Tutor2 != null && !student.Tutor2.User.Notifications.Contains(notification))
            {
                var user = student.Tutor2.User;
                user.Notifications.Add(notification);
                notification.RecipientUsers.Add(user);
                _userRepository.Update(user);
                _notificationRepository.Update(notification);
                if (notification.SendEmail)
                    EmailService.SendEmailToUser(student.Tutor2.User, notification);
            }
        }

        private void SendToStudents(IEnumerable<Student> students, Notification notification)
        {
            foreach (var student in students.ToList())
            {
                SendToStudent(student, notification);
            }
        }

        public void SendAllPending()
        {
            var notifications =
                _notificationRepository.Filter(x => x.Approved && !x.Sent && x.AcademicYear.IsActive);
            foreach (var notification in notifications.ToList())
            {
                SendNotification(notification);
            }
        }
    }
}
