﻿using System;
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
            if(oDataSet.Tables.Count == 0)
                return;

            if(oDataSet.Tables[0].Rows.Count <= 15)
                return;

            var dtDatos = oDataSet.Tables[0];

            var listStudents = new List<Student>();
            var listParents = new List<Parent>();
            for (var indice = 15; indice < dtDatos.Rows.Count; indice++)
            {
                if(dtDatos.Rows[indice][2].ToString().Trim().Length == 0)
                    continue;

                var newStudents = new Student
                {
                    IdNumber = dtDatos.Rows[indice][2].ToString()
                    ,LastName = (dtDatos.Rows[indice][3].ToString() + " " + dtDatos.Rows[indice][4].ToString()).Trim()
                    ,FirstName = dtDatos.Rows[indice][6].ToString()
                    ,Gender = Utilities.IsMasculino(dtDatos.Rows[indice][8].ToString())
                    ,BirthDate = Utilities.ConvertStringToDateTime(dtDatos.Rows[indice][9].ToString()).ToShortDateString()
                    ,Nationality = dtDatos.Rows[indice][13].ToString()
                    ,State = dtDatos.Rows[indice][15].ToString()
                };

                var newParent = new Parent
                {
                    Nationality = dtDatos.Rows[indice][16].ToString()
                    ,IdNumber = dtDatos.Rows[indice][18].ToString()
                    ,LastName = (dtDatos.Rows[indice][19].ToString() + " " + dtDatos.Rows[indice][20].ToString()).Trim()
                    ,FirstName = dtDatos.Rows[indice][21].ToString()
                    ,Gender = Utilities.IsMasculino(dtDatos.Rows[indice][22].ToString())
                    ,BirthDate = Utilities.ConvertStringToDateTime(dtDatos.Rows[indice][24].ToString()).ToShortDateString()
                    ,State = dtDatos.Rows[indice][25].ToString()
                    ,City = dtDatos.Rows[indice][26].ToString()
                };

                var newContactInformation = new ContactInformation
                {
                    Type = "Telefono"
                    ,Value = dtDatos.Rows[indice][27].ToString()
                    ,People = newParent
                };

                var listContacts = new List<ContactInformation> {newContactInformation};

                newParent.Contacts = listContacts;
                newStudents.Tutor1 = newParent;
                
                listStudents.Add(newStudents);
                listParents.Add(newParent);
            }

            SaveData(listStudents, listParents);
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


        private void SaveData(List<Student> listStudents, List<Parent> listParents)
        {
            //CODIGO KEHIMER
        }

    }
}
