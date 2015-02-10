using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
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
            if (!_context.AcademicYears.Any())
                return false;

            var academicYears = _context.AcademicYears.Where(x => Equals(year, x.Year.Year) && x.Grade != null).ToList();
            if (academicYears.Any())
                return false;
            
            var academicYears2 = academicYears.Where(x => Equals(grade, x.Grade.Id) && Equals(section, x.Section.ToString())).ToList();

            if (academicYears.Any())
                return true;

            return false;
        }

        public DataSet GetDataSetFromExcelFile(string path)
        {
            //var excelConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=Excel 12.0;Persist Security Info=False";
            var excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + path + "';Extended Properties= \"Excel 8.0;HDR=Yes;IMEX=1\";";
            var excelConnection = new OleDbConnection(excelConnectionString);

            var database = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='"+ path +"';Extended Properties= \"Excel 8.0;HDR=Yes;IMEX=1\";");

            var cmd = new OleDbCommand("Select * from [Matricula$]", excelConnection);

            var oDataAdapter = new OleDbDataAdapter(cmd);
            var oSet = new DataSet();

            oDataAdapter.Fill(oSet);

            return oSet;
        }
    }
}
