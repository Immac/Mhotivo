﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Globalization;
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

        public void Import(DataSet oDataSet, AcademicYear academicYear, IParentRepository parentRepository, IStudentRepository studentRepository, IEnrollRepository enrollRepository)
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

            SaveData(listStudents, listParents, academicYear, parentRepository, studentRepository, enrollRepository);
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


        private void SaveData(IEnumerable<Student> listStudents, IEnumerable<Parent> listParents, AcademicYear academicYear, IParentRepository parentRepository, IStudentRepository studentRepository, IEnrollRepository enrollRepository)
        {
            var allParents = parentRepository.GetAllParents();
            var allStudents = studentRepository.GetAllStudents();
            var allEnrolls = enrollRepository.GetAllsEnrolls();

            
            foreach (var pare in listParents)
            {
                var temp = allParents.Where(x => Equals(x.IdNumber, pare.IdNumber));
                if (!temp.Any())
                    parentRepository.Create(pare);
            }
            
            foreach (var stu in listStudents)
            {
                var temp = allStudents.Where(x => Equals(x.IdNumber, stu.IdNumber));
                if (!temp.Any())
                {
                    studentRepository.Create(stu);
                    var enr = allEnrolls.Where(x => Equals(x.AcademicYear, academicYear) && Equals(x.Student, stu));
                    if (!enr.Any())
                    {
                        var te = new Enroll() {AcademicYear = academicYear,Student = stu};
                        enrollRepository.Create(te);
                    }
                    //if (academicYear.Any() && academicYear.First().Approved)
                    //{
                    //    var t = new Enroll { AcademicYear = academicYear.First(), Student = stu };
                    //    _context.Enrolls.Add(t);
                    //}
                }
            }
        }
    }
}
