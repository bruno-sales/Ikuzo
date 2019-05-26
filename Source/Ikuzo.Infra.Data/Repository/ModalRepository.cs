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
    public class ModalRepository : BaseRepository<Modal>, IModalRepository
    {
        public ModalRepository(Context.Context context) : base(context)
        {
        }

        public override IEnumerable<Modal> GetAll()
        {
            return DbSet
                .Include(i => i.Line);
        } 

        public Modal Details(string modalId)
        {
            return DbSet
                .Include(i => i.Line)
                .FirstOrDefault(i => string.Equals(i.ModalId.ToLower(), modalId.ToLower()));
        }

        public void RemoveFromLine(string lineId)
        {
            var sql = @"
                      SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; 
                      DELETE 
                      FROM [Modal] 
                      WHERE [LineId] = '" + lineId + "'";

            using (var cn = Connection)
            {
                if (cn.State.Equals(ConnectionState.Open) == false) cn.Open();

                var trans = cn.BeginTransaction("removeModalTransaction");

                var comm = new SqlCommand(sql, cn, trans);

                comm.ExecuteNonQuery();

                trans.Commit();

                if (cn.State.Equals(ConnectionState.Open)) cn.Close();
            }
        }

        public void ModalBulkInsert(IEnumerable<Modal> modals)
        {
            var dt = MakeTable(modals);

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

        private DataTable MakeTable(IEnumerable<Modal> modals)
        {
            var dtTable = new DataTable("Modal");
 
            var column1 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "ModalId"
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
            foreach (var modal in modals)
            {
                var dr = dtTable.NewRow();
                dr["ModalId"] = modal.ModalId;
                dr["LineId"] = modal.LineId; 
                dr["LastUpdateDate"] = modal.LastUpdateDate;

                dtTable.Rows.Add(dr);
            }

            return dtTable;
        }
    }
}
