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
    public class BusRepository : BaseRepository<Bus>, IBusRepository
    {
        public BusRepository(Context.Context context) : base(context)
        {
        }

        public override IEnumerable<Bus> GetAll()
        {
            return DbSet
                .Include(i => i.Line);
        } 

        public Bus Details(string busId)
        {
            return DbSet
                .Include(i => i.Line)
                .FirstOrDefault(i => string.Equals(i.BusId.ToLower(), busId.ToLower()));
        }

        public void RemoveFromLine(string lineId)
        {
            var sql = $@"
                      SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; 
                      DELETE 
                      FROM [Bus] 
                      WHERE [LineId] = '" + lineId + "'";

            using (var cn = Connection)
            {
                if (cn.State.Equals(ConnectionState.Open) == false) cn.Open();

                var trans = cn.BeginTransaction("removeBusTransaction");

                var comm = new SqlCommand(sql, cn, trans);

                comm.ExecuteNonQuery();

                trans.Commit();

                if (cn.State.Equals(ConnectionState.Open)) cn.Close();
            }
        }

        public void BusBulkInsert(IEnumerable<Bus> buses)
        {
            var dt = MakeTable(buses);

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

        private DataTable MakeTable(IEnumerable<Bus> buses)
        {
            var dtTable = new DataTable("Bus");
 
            var column1 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "BusId"
            };

            var column2 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "LineId"
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
            foreach (var bus in buses)
            {
                var dr = dtTable.NewRow();
                dr["BusId"] = bus.BusId;
                dr["LineId"] = bus.LineId; 
                dr["LastUpdateDate"] = bus.LastUpdateDate;

                dtTable.Rows.Add(dr);
            }

            return dtTable;
        }
    }
}
