﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Data.Entities;

namespace Mhotivo.Interface.Interfaces
{
    public interface IPeopleWithUserRepository
    {
        PeopleWithUser GetById(long id);
        PeopleWithUser Create(PeopleWithUser itemToCreate);
        IQueryable<PeopleWithUser> Query(Expression<Func<PeopleWithUser, PeopleWithUser>> expression);
        IQueryable<PeopleWithUser> Filter(Expression<Func<PeopleWithUser, bool>> expression);
        PeopleWithUser Update(PeopleWithUser itemToUpdate);
        PeopleWithUser Delete(long id);
        PeopleWithUser Delete(PeopleWithUser itemToDelete);
        IEnumerable<PeopleWithUser> GetAllPeopleWithUsers();
    }
}
