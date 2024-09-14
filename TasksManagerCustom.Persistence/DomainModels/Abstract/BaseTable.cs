using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManagerCustom.Persistence.DomainModels.Abstract
{
    public abstract class BaseTable
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int Id { get; set; }
    }
}
