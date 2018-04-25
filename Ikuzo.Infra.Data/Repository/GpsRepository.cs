﻿using System;
using System.Collections.Generic; 
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;

namespace Ikuzo.Infra.Data.Repository
{
    public class GpsRepository : BaseRepository<Gps>, IGpsRepository
    {
        public GpsRepository(Context.Context context) : base(context)
        {
        }

        public Gps GetBusGps(string busId)
        {
            var gps = DbSet.FirstOrDefault(i => string.Equals(i.BusId.ToLower(), busId.ToLower()));

            return gps;
        }

        public void RemoveAll()
        {
            using (var cn = Connection)
            {
                if (cn.State.Equals(ConnectionState.Open) == false) cn.Open();

                var sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; " +
                          "DELETE FROM GPS";

                var comm = new SqlCommand(sql, cn);

                comm.ExecuteNonQuery();

                if (cn.State.Equals(ConnectionState.Open)) cn.Close();
            }

            if (Connection.State.Equals(ConnectionState.Open)) Connection.Close();
        }

        public void RemoveFromLine(string lineId)
        {
            using (var cn = Connection)
            {
                if (cn.State.Equals(ConnectionState.Open) == false) cn.Open();

                var sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; " +
                          "DELETE FROM GPS WHERE LINEID = '" + lineId + "'";

                var comm = new SqlCommand(sql, cn);

                comm.ExecuteNonQuery();

                if (cn.State.Equals(ConnectionState.Open)) cn.Close();
            }

            if (Connection.State.Equals(ConnectionState.Open)) Connection.Close();
        }

        public IEnumerable<Gps> GetNerbyBusesGps(decimal latitude, decimal longitude, decimal variance)
        {
            //Negatives Lat/Lon
            /*var startLatitude = latitude - variance;
            var endLatitude = latitude + variance;

            var startLongitude = longitude - variance;
            var endLongitude = longitude + variance;

            var itens = DbSet.Where(i => (i.Latitude >= startLatitude && i.Latitude <= endLatitude)
                                         && (i.Longitude >= startLongitude && i.Longitude <= endLongitude))
                .Include(i => i.Bus).ToList();*/

            return null;
        }

        public IEnumerable<Gps> GetNerbyBusesGpsFromLine(string lineId)
        {
            var itens = new List<Gps>();

            var sql = $@"
                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                    SELECT * 
                    FROM [Gps] 
                    WHERE [LineId] = " + lineId;

            using (Connection)
            {
                if (Connection.State.Equals(ConnectionState.Open) == false) Connection.Open();

                itens.AddRange(Connection.Query<Gps>(sql, commandTimeout: 6000));
            }

            Connection.Close();

            return itens;
        }


        public void GpsBulkInsert(IEnumerable<Gps> gpses)
        {
            var dt = MakeTable(gpses);

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

        private DataTable MakeTable(IEnumerable<Gps> gpses)
        {
            var dtTable = new DataTable("Gps");

            var column1 = new DataColumn
            {
                DataType = Type.GetType("System.Guid"),
                ColumnName = "GpsGuid"
            };

            var column2 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "BusId"
            };

            var column3 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "LineId"
            };

            var column4 = new DataColumn
            {
                DataType = Type.GetType("System.Decimal"),
                ColumnName = "Latitude"
            };

            var column5 = new DataColumn
            {
                DataType = Type.GetType("System.Decimal"),
                ColumnName = "Longitude"
            };

            var column6 = new DataColumn
            {
                DataType = Type.GetType("System.Int32"),
                ColumnName = "Direction"
            };

            var column7 = new DataColumn
            {
                DataType = Type.GetType("System.DateTime"),
                ColumnName = "TimeStamp"
            };

            var column8 = new DataColumn
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
            dtTable.Columns.Add(column8);

            //Adicionando linhas
            foreach (var gps in gpses)
            {
                var dr = dtTable.NewRow();
                dr["GpsGuid"] = gps.GpsGuid;
                dr["BusId"] = gps.BusId;
                dr["LineId"] = gps.LineId;
                dr["Latitude"] = gps.Latitude;
                dr["Longitude"] = gps.Longitude;
                dr["Direction"] = gps.Direction;
                dr["TimeStamp"] = gps.Timestamp.ToLocalTime();
                dr["LastUpdateDate"] = gps.LastUpdateDate;

                dtTable.Rows.Add(dr);
            }

            return dtTable;
        }

    }
}
