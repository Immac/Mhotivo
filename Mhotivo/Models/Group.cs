using Mhotivo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls.Expressions;

namespace Mhotivo.Models
{
    public class Group
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public List<User> Users { get; set; }
    }
}