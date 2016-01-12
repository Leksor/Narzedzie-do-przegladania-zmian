using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patch.Models
{
    class PatchData
    {
        public int DeletedIndex { get; set; }
        public int DeletedAmount { get; set; }
        public int InsertedIndex { get; set; }
        public int InsertedAmount{ get; set; }

        public List<string> insertedRows { get; set; }

        public PatchData()
        {
            DeletedIndex = DeletedAmount = InsertedIndex = InsertedAmount = 0;
            insertedRows = new List<string>();
        }

        /// <summary>
        /// Method adds row to list
        /// </summary>
        /// <param name="row"></param>
        public void AddRow(string row)
        {
            insertedRows.Add(row);
        }
    }
}
