using System;
using System.Collections.Generic;
using System.Data; 
using System.Data.SqlClient; 
using Dapper;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;

namespace Ikuzo.Infra.Data.Repository
{
    public class ItineraryRepository : BaseRepository<Itinerary>, IItineraryRepository
    {
        public ItineraryRepository(Context.Context context) : base(context)
        {
        }

        public void RemoveFromLine(string lineId)
        {
            var sql = $@"
                      SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; 
                      DELETE 
                      FROM [Itinerary] 
                      WHERE [LineId] = '" + lineId + "'";

            using (var cn = Connection)
            {
                if (cn.State.Equals(ConnectionState.Open) == false) cn.Open();

                var trans = cn.BeginTransaction("removeItFromTransaction");

                var comm = new SqlCommand(sql, cn, trans);

                comm.ExecuteNonQuery();

                trans.Commit();

                if (cn.State.Equals(ConnectionState.Open)) cn.Close();
            }
        }

        public IEnumerable<Line> GetLocalLines(decimal latitude, decimal longitude, decimal variance)
        {
            var itens = new List<Line>();

            //Negatives Lat/Lon
            var y1 = latitude - variance;
            var y2 = latitude + variance;

            var x1 = longitude - variance;
            var x2 = longitude + variance;

            var sql = $@"
                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                    SELECT L.* 
                    FROM [Itinerary] as I
                    INNER JOIN [Line] as L
                    ON I.LineId = L.LineId
                    WHERE (I.[Latitude] BETWEEN @Y1 AND @Y2 )  
                    AND   (I.[Longitude] BETWEEN @X1 AND @X2 ) 
                    GROUP BY L.[LineId], L.[Description], L.[LastUpdateDate]";

            using (var cn = Connection)
            {
                var args = new DynamicParameters();
                args.Add("Y1", y1, DbType.Decimal, precision: 12, scale: 6);
                args.Add("Y2", y2, DbType.Decimal, precision: 12, scale: 6);
                args.Add("X1", x1, DbType.Decimal, precision: 12, scale: 6);
                args.Add("X2", x2, DbType.Decimal, precision: 12, scale: 6);

                if (cn.State.Equals(ConnectionState.Open) == false) cn.Open();

                itens.AddRange(Connection.Query<Line>(sql, args, commandTimeout: 6000));

                if (cn.State.Equals(ConnectionState.Open)) cn.Close();
            }

            return itens; 
        }

        public void ItineraryBulkInsert(IEnumerable<Itinerary> itineraries)
        {
            var dt = MakeTable(itineraries);

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

        private DataTable MakeTable(IEnumerable<Itinerary> itineraries)
        {
            var dtTable = new DataTable("Itinerary");

            var column1 = new DataColumn
            {
                DataType = Type.GetType("System.Guid"),
                ColumnName = "ItineraryGuid"
            }; 

            var column2 = new DataColumn
            {
                DataType = Type.GetType("System.Decimal"),
                ColumnName = "Latitude"
            };

            var column3 = new DataColumn
            {
                DataType = Type.GetType("System.Decimal"),
                ColumnName = "Longitude"
            };

            var column4 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "LineId"
            };

            var column5 = new DataColumn
            {
                DataType = Type.GetType("System.Boolean"),
                ColumnName = "Returning"
            };
             
            
            var column6 = new DataColumn
            {
                DataType = Type.GetType("System.DateTime"),
                ColumnName = "LastUpdateDate"
            };

            dtTable.Columns.Add(column1);
            dtTable.Columns.Add(column2);
            dtTable.Columns.Add(column3);
            dtTable.Columns.Add(column4);
            dtTable.Columns.Add(column5);
            dtTable.Columns.Add(column6); 

            //Adding rows
            foreach (var itinerary in itineraries)
            {
                var dr = dtTable.NewRow();
                dr["ItineraryGuid"] = itinerary.ItineraryGuid; 
                dr["Latitude"] = itinerary.Latitude;
                dr["Longitude"] = itinerary.Longitude;
                dr["LineId"] = itinerary.LineId;
                dr["Returning"] = itinerary.Returning; 
                dr["LastUpdateDate"] = itinerary.LastUpdateDate;

                dtTable.Rows.Add(dr);
            }

            return dtTable;
        }
    }
}
