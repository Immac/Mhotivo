using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhotivo.Data.Entities;

namespace Mhotivo.Interface.Interfaces
{
    public interface IImportDataRepository
    {
        void Import(DataSet oDataSet);
        bool ExistAcademicYear(int year, Grade grade, char section);

    }
}
