using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using MySqlConnector;
using GOT_Server.Contexts;
using GOT_Server.Entities;

namespace GOT_Server.Queries
{
    public class ArchivoQuery
    {

        public AppDb Db { get; }

        public ArchivoQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Archivo> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `IDArchivo`, `DireccionArchivo`, `DataArchivo`, `NombreArchivo`, `IDRepositorie`, `IDCommit` FROM `Archivo` WHERE `IDArchivo` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }
        public async Task<Archivo> FindRepoFile(string idrepo, string file)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `IDArchivo`, `DireccionArchivo`, `DataArchivo`, `NombreArchivo`, `IDRepositorie`, `IDCommit` FROM `Archivo` NATURAL JOIN `Repositorie` WHERE `NameRepositorie` = @idrepo AND `NombreArchivo` = @name ORDER BY `IDCommit` DESC;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idrepo",
                DbType = DbType.String,
                Value = idrepo,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = file,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<Archivo> FindFileCommit(string file, int commit)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT  `DireccionArchivo`, `DataArchivo`, `NombreArchivo`, `IDCommit` FROM `Archivo` NATURAL JOIN `Repositorie` WHERE `NombreArchivo` = @file AND `IDCommit` = @commit;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@file",
                DbType = DbType.String,
                Value = file,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@commit",
                DbType = DbType.Int32,
                Value = commit,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Archivo>> FindAllRepoFile(string idrepo, string file)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `IDArchivo`, `DireccionArchivo`, `DataArchivo`, `NombreArchivo`, `IDRepositorie`, `IDCommit` FROM `Archivo` NATURAL JOIN `Repositorie` WHERE `NameRepositorie` = @idrepo AND `NombreArchivo` = @name ORDER BY `IDCommit`;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idrepo",
                DbType = DbType.String,
                Value = idrepo,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = file,
            });

            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<Archivo>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `IDArchivo`, `DireccionArchivo`, `DataArchivo`, `NombreArchivo`, `IDRepositorie`, `IDCommit` FROM `Archivo` ORDER BY `IDArchivo` DESC;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Archivo`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Archivo>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Archivo>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Archivo(Db)
                    {
                        IDArchivo = reader.GetInt32(0),
                        DireccionArchivo = reader.GetString(1),
                        DataArchivo = reader.GetString(2),
                        NombreArchivo = reader.GetString(3),
                        IDRepositorie = reader.GetInt32(4),
                        IDCommit = reader.GetInt32(5),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

    }
}
