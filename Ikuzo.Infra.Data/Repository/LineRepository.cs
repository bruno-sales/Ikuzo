using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;

namespace Ikuzo.Infra.Data.Repository
{
    public class LineRepository : BaseRepository<Line>, ILineRepository
    {
        public LineRepository(Context.Context context) : base(context)
        {
        }

        public Line Get(string lineId)
        {
            return DbSet 
                .FirstOrDefault(i => string.Equals(i.LineId.ToLower(), lineId.ToLower()));
        }

        public Line Details(string lineId)
        {
            return DbSet
                .Include(i => i.Buses)
                .Include(i=>i.Itineraries)
                .FirstOrDefault(i => string.Equals(i.LineId.ToLower(), lineId.ToLower()));
        }

        public void LineBulkInsert(IEnumerable<Line> lines)
        {
            var dt = MakeTable(lines);

            using (var cn = Connection)
            {
                if (cn.State.Equals(ConnectionState.Open) == false) cn.Open();

                using (var s = new SqlBulkCopy(cn))
                {
                    s.DestinationTableName = dt.TableName;

                    foreach (var column in dt.Columns)
                    {
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    }

                    s.WriteToServer(dt);
                }

                if (cn.State.Equals(ConnectionState.Open)) cn.Close();
            }
        }

        private DataTable MakeTable(IEnumerable<Line> lines)
        {
            var dtTable = new DataTable("Line");
 
            var column1 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "LineId"
            };

            var column2 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "Description"
            };
             
            var column3 = new DataColumn
            {
                DataType = Type.GetType("System.DateTime"),
                ColumnName = "LastUpdateDate"
            };

            dtTable.Columns.Add(column1);
            dtTable.Columns.Add(column2);
            dtTable.Columns.Add(column3); 

            //Adding rows
            foreach (var line in lines)
            {
                var dr = dtTable.NewRow();
                dr["LineId"] = line.LineId;
                dr["Description"] = line.Description; 
                dr["LastUpdateDate"] = line.LastUpdateDate;

                dtTable.Rows.Add(dr);
            }

            return dtTable;
        }
    }
}
