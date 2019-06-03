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

        public void RemoveAll()
        {
            var sql = $@"
                      SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; 
                      DELETE 
                      FROM [Itinerary] "  ;

            using (var cn = Connection)
            {
                if (cn.State.Equals(ConnectionState.Open) == false) cn.Open();

                var trans = cn.BeginTransaction("removeAllItFromTransaction");

                var comm = new SqlCommand(sql, cn, trans);

                comm.ExecuteNonQuery();

                trans.Commit();

                if (cn.State.Equals(ConnectionState.Open)) cn.Close();
            }
        }

        public IEnumerable<Line> GetLocalLines(decimal latitude, decimal longitude, decimal distance)
        {
            var itens = new List<Line>();
             
            var sql = $@"
                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                    SELECT L.* 
                    FROM [Itinerary] as I
                    INNER JOIN [Line] as L
                    ON I.LineId = L.LineId
                    WHERE  dbo.FnGetDistance(@lat, @lon, I.[Latitude], I.[Longitude],'Meters') <= @distance   
                    GROUP BY L.[LineId], L.[Description], L.[LastUpdateDate]";

            using (var cn = Connection)
            {
                var args = new DynamicParameters();
                args.Add("lat", latitude, DbType.Decimal, precision: 12, scale: 6);
                args.Add("lon", longitude, DbType.Decimal, precision: 12, scale: 6);
                args.Add("distance", distance, DbType.Decimal, precision: 12, scale: 2); 

                if (cn.State.Equals(ConnectionState.Open) == false) cn.Open();

                itens.AddRange(Connection.Query<Line>(sql, args, commandTimeout: 6000));

                if (cn.State.Equals(ConnectionState.Open)) cn.Close();
            }

            return itens; 
        }

        public IEnumerable<Itinerary> GetLineItinerary(string lineId)
        {
            var itens = new List<Itinerary>();

            var sql = $@"
                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                    SELECT * 
                    FROM [Itinerary] as I  
                    WHERE LineId = @line  
                    ORDER BY returning, sequence";

            using (var cn = Connection)
            {
                var args = new DynamicParameters();
                args.Add("line", lineId, DbType.String); 

                if (cn.State.Equals(ConnectionState.Open) == false) cn.Open();

                itens.AddRange(Connection.Query<Itinerary>(sql, args, commandTimeout: 6000));

                if (cn.State.Equals(ConnectionState.Open)) cn.Close();
            }

            return itens;
        }

        public IEnumerable<Line> GetLocalToDestinyLines(decimal latitude1, decimal longitude1, decimal latitude2, decimal longitude2, decimal distance)
        {
            var itens = new List<Line>();

            var sql = $@"
                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                    SELECT * 
                    FROM [LINE] l 
                    WHERE
                    (
	                    SELECT count(i.LineId) FROM [Itinerary] i
	                    WHERE i.LineId = l.LineId and 
	                    dbo.FnGetDistance(@lat1, @long1, i.Latitude, i.Longitude, 'Meters') <= @distance
                    ) > 0 
                    AND
                    (
	                    SELECT count(i.LineId) FROM [Itinerary] i
	                    WHERE i.LineId = l.LineId and 
	                    dbo.FnGetDistance(@lat2, @long2, i.Latitude, i.Longitude, 'Meters') <= @distance
                    ) > 0";

            using (var cn = Connection)
            {
                var args = new DynamicParameters();
                args.Add("lat1", latitude1, DbType.Decimal, precision: 12, scale: 6);
                args.Add("long1", longitude1, DbType.Decimal, precision: 12, scale: 6);
                args.Add("lat2", latitude2, DbType.Decimal, precision: 12, scale: 6);
                args.Add("long2", longitude2, DbType.Decimal, precision: 12, scale: 6);
                args.Add("distance", distance, DbType.Decimal, precision: 12, scale: 2);

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
                DataType = Type.GetType("System.Int32"),
                ColumnName = "Sequence"
            };

            var column6 = new DataColumn
            {
                DataType = Type.GetType("System.Boolean"),
                ColumnName = "Returning"
            };

            var column7 = new DataColumn
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
            dtTable.Columns.Add(column7);

            //Adding rows
            foreach (var itinerary in itineraries)
            {
                var dr = dtTable.NewRow();
                dr["ItineraryGuid"] = itinerary.ItineraryGuid; 
                dr["Latitude"] = itinerary.Latitude;
                dr["Longitude"] = itinerary.Longitude;
                dr["LineId"] = itinerary.LineId;
                dr["Sequence"] = itinerary.Sequence;
                dr["Returning"] = itinerary.Returning;
                dr["LastUpdateDate"] = itinerary.LastUpdateDate;

                dtTable.Rows.Add(dr);
            }

            return dtTable;
        }
    }
}
