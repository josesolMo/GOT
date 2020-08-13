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
    public class RepositorieQuery
    {
        public AppDb Db { get; }

        public RepositorieQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Repositorie> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `IDRepositorie`, `NameRepositorie` FROM `Repositorie` WHERE `IDRepositorie` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<Repositorie> FindID(string name)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `IDRepositorie`, `NameRepositorie` FROM `Repositorie` WHERE `NameRepositorie` = @name";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = name,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Repositorie>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `IDRepositorie`, `NameRepositorie` FROM `Repositorie` ORDER BY `IDRepositorie` DESC;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Repositorie`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Repositorie>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Repositorie>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Repositorie(Db)
                    {
                        IDRepositorie = reader.GetInt32(0),
                        NameRepositorie = reader.GetString(1),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
