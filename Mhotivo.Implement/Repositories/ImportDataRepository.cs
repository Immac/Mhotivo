using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Interface;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Data;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;


namespace Mhotivo.Implement.Repositories
{
    public class ImportDataRepository : IImportDataRepository
    {
        private readonly MhotivoContext _context;

        public ImportDataRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public void Import(DataSet oDataSet)
        {
            throw new NotImplementedException();
        }

        public bool ExistAcademicYear(int year, long grade, string section)
        {
            return false;
            throw new NotImplementedException();
        }

        public bool ExistAcademicYear(int year, Grade grade, string section)
        {
            return false;            
        }

        public bool ExistAcademicYear(int year, Grade grade, char section)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataSetFromExcelFile(byte[] fileBytes)
        {
            throw new NotImplementedException();
        }
    }
}
