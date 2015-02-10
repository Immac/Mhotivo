using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;
using Mhotivo.Interface.Interfaces;

namespace Mhotivo.Implement.Repositories
{
    public class HomeworkRepository:IHomeworkRepository
    {
        private readonly MhotivoContext _context;

        public HomeworkRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public Homework First(Expression<Func<Homework, Homework>> query)
        {
            IQueryable<Homework> homeworks = _context.Homeworks.Select(query);
            return homeworks.Count() != 0 ? homeworks.First() : null;
        }

        public Homework GetById(long id)
        {
            IQueryable<Homework> homeworks = _context.Homeworks.Where(x => x.Id == id && !false);
            return homeworks.Count() != 0 ? homeworks.First() : null;
        }

        public Homework Create(Homework itemToCreate)
        {
            Homework homework = _context.Homeworks.Add(itemToCreate);
            _context.SaveChanges();
            return homework;
        }

        public IQueryable<Homework> Query(Expression<Func<Homework, Homework>> expression)
        {
            IQueryable<Homework> myHomeworks = _context.Homeworks.Select(expression);
            return myHomeworks;
        }

        public IQueryable<Homework> Filter(Expression<Func<Homework, bool>> expression)
        {
            IQueryable<Homework> myHomeworks = _context.Homeworks.Where(expression);
            return myHomeworks;
        }

        public Homework Update(Homework itemToUpdate)
        {
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Homework Delete(long id)
        {
            Homework itemToDelete = GetById(id);
            _context.SaveChanges();
            return itemToDelete;
        }

        public IEnumerable<Homework> GetAllHomeworks()
        {
            return Query(x => x).Where(x => !false).ToList().Select(x => new Homework
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DeliverDate = x.DeliverDate,
                Points = x.Points,
                Course = x.Course
            });
        }

        public Homework GenerateHomeworkFromRegisterModel(Homework homeworkRegisterModel)
        {
            return new Homework
            {
                Id = homeworkRegisterModel.Id,
                Title = homeworkRegisterModel.Title,
                Description = homeworkRegisterModel.Description,
                DeliverDate = homeworkRegisterModel.DeliverDate,
                Points = homeworkRegisterModel.Points,
                Course = homeworkRegisterModel.Course
            };
        }

        public Homework GetHomeworkEditModelById(long id)
        {
            Homework homework = GetById(id);
            return new Homework
            {
                Id = homework.Id,
                Title = homework.Title,
                Description = homework.Description,
                DeliverDate = homework.DeliverDate,
                Points = homework.Points,
                Course = homework.Course
            };
        }

        public Homework GetHomeworkDisplayModelById(long id)
        {
            Homework homework = GetById(id);
            return new Homework
            {
                Id = homework.Id,
                Title = homework.Title,
                Description = homework.Description,
                DeliverDate = homework.DeliverDate,
                Points = homework.Points,
                Course = homework.Course
            };
        }

        public Homework UpdateHomeworkFromHomeworkEditModel(Homework displayHomeworkModel, Homework homework)
        {
            homework.Id = homework.Id;
            homework.Title = homework.Title;
            homework.Description = homework.Description;
            homework.DeliverDate = homework.DeliverDate;
            homework.Points = homework.Points;
            homework.Course = homework.Course;

            return Update(homework);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
