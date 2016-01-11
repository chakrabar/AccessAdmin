using AccessManagement.Domain.Entities;
using AccessManagement.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AccessManagement.Repository
{
    public class AccessPointRepository : IRepository<AccessPoint>
    {
        public IEnumerable<AccessPoint> Get()
        {
            const string command = @"SELECT a.Id, a.Name, s.Id AS SiteId, s.Name AS SiteName
                            FROM AccessPoints a
                            JOIN Sites s
                            ON a.SiteId = s.Id";

            var accessPoints = new List<AccessPoint>();
            using (var conn = SqlHelper.GetConnection())
            {
                SqlCommand cmd = CommandFactory.CreateSimpleTextCommand(conn, command);

                var reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var ap = new AccessPoint();
                        ap.Name = reader.GetString(reader.GetOrdinal("Name"));
                        ap.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        ap.Facility = new Site { Id = reader.GetInt32(reader.GetOrdinal("SiteId")), Name = reader.GetString(reader.GetOrdinal("SiteName")) };
                        accessPoints.Add(ap);
                    }
                }
            }

            return accessPoints;
        }

        public AccessPoint Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
