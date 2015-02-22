using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;
using Mhotivo.Interface.Interfaces;

namespace Mhotivo.Implement.Repositories
{
    public class AcademicYearDetailsRepository : IAcademicYearDetailsRepository
    {
        private readonly MhotivoContext _context;

        public AcademicYearDetailsRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }
        public AcademicYearDetail First(Expression<Func<AcademicYearDetail, AcademicYearDetail>> query)
        {
            var academicYearDetails = _context.AcademicYearsDetails.Select(query);
            return academicYearDetails.Count() != 0 ? academicYearDetails.Include(x => x.AcademicYear)
                                                                         .Include(x =>x.Course)
                                                                         .Include(x => x.Teacher).First() : null;
        }

        public AcademicYearDetail GetById(int id)
        {
            var academicYearDetails = _context.AcademicYearsDetails.Where(x => x.Id == id);
            return academicYearDetails.Count() != 0 ? academicYearDetails.Include(x => x.AcademicYear)
                                                                         .Include(x => x.Course)
                                                                         .Include(x => x.Teacher).First() : null;
        }

        public AcademicYearDetail Create(AcademicYearDetail academicYearToCreate)
        {
            var academicYearDetails = _context.AcademicYearsDetails.Add(academicYearToCreate);
            _context.Entry(academicYearToCreate.Course).State = EntityState.Modified;
            _context.Entry(academicYearToCreate.Teacher).State = EntityState.Modified;
            _context.SaveChanges();
            return academicYearDetails;
        }

        public IQueryable<AcademicYearDetail> Query(Expression<Func<AcademicYearDetail, AcademicYearDetail>> expression)
        {
            int id = 0;
            var academicYearDetails = _context.AcademicYearsDetails.Select(expression);
            return academicYearDetails.Count() != 0 ? academicYearDetails.Include(x => x.AcademicYear)
                                                                         .Include(x => x.Course)
                                                                         .Include(x => x.Teacher): academicYearDetails;
        }

        public IQueryable<AcademicYearDetail> Filter(Expression<Func<AcademicYearDetail, bool>> expression)
        {
            var academicYearDetails = _context.AcademicYearsDetails.Where(expression);
            return academicYearDetails.Count() != 0 ? academicYearDetails.Include(x => x.AcademicYear)
                                                                         .Include(x => x.Course)
                                                                         .Include(x => x.Teacher) : academicYearDetails;
        }

        public AcademicYearDetail Update(AcademicYearDetail itemToUpdate, bool updateAcademicYear = true, bool updateCourse = true,
            bool updateTeacher = true)
        {
            if (updateAcademicYear)
                _context.Entry(itemToUpdate.AcademicYear).State = EntityState.Modified;

            if (updateCourse)
                _context.Entry(itemToUpdate.Course).State = EntityState.Modified;

            if (updateTeacher)
                _context.Entry(itemToUpdate.Teacher).State = EntityState.Modified;

            _context.SaveChanges();
            return itemToUpdate;
        }

        public AcademicYearDetail Update(AcademicYearDetail itemToUpdate)
        {
            var updateCourse = false;
            var updateAcademicYear = false;
            var updateTeacher = false;

            var academicYearDetail = GetById(itemToUpdate.Id);
            academicYearDetail.TeacherStartDate = itemToUpdate.TeacherStartDate;
            academicYearDetail.TeacherEndDate = itemToUpdate.TeacherEndDate;
            academicYearDetail.Schedule = itemToUpdate.Schedule;
            academicYearDetail.Room = itemToUpdate.Room;

            if (academicYearDetail.AcademicYear.Id != itemToUpdate.AcademicYear.Id)
            {
                academicYearDetail.AcademicYear = itemToUpdate.AcademicYear;
                updateAcademicYear = true;
            }

            if (academicYearDetail.Course.Id != itemToUpdate.Course.Id)
            {
                academicYearDetail.Course = itemToUpdate.Course;
                updateCourse = true;
            }

            if (academicYearDetail.Teacher.Id != itemToUpdate.Teacher.Id)
            {
                academicYearDetail.Teacher = itemToUpdate.Teacher;
                updateTeacher = true;
            }

            return Update(academicYearDetail, updateAcademicYear, updateCourse, updateTeacher);
        }
        public IEnumerable<AcademicYearDetail> GetAllAcademicYearsDetails(int academicYearId)
        {
            return Query(x => x).ToList().Select(x => new AcademicYearDetail
            {
                Id = x.Id,
                TeacherStartDate = x.TeacherStartDate,
                TeacherEndDate = x.TeacherEndDate,
                Schedule = x.Schedule,
                Room = x.Room,
                AcademicYear = x.AcademicYear,
                Course = x.Course,
                Teacher = x.Teacher
            }).Where(x => x.AcademicYear.Id == academicYearId);
        }

        public AcademicYearDetail Delete(int id)
        {
            var itemToDelete = GetById(id);
            _context.AcademicYearsDetails.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
