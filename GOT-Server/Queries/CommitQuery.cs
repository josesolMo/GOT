using GOT_Server.Contexts;
using GOT_Server.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace GOT_Server.Queries
{
    public class CommitQuery
    {
        public AppDb Db { get; }

        public CommitQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Commited> FindOneAsync(string id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `IDCommit`, `MessageCommit`, `DateCommit` FROM `Commited` WHERE `IDCommit` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.String,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Commited>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `IDCommit`, `MessageCommit`, `DateCommit` FROM `Commited` ORDER BY `IDCommit` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Commited`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Commited>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Commited>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Commited(Db)
                    {
                        IDCommit = reader.GetString(0),
                        MessageCommit = reader.GetString(1),
                        DateCommit = reader.GetDateTime(2),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
