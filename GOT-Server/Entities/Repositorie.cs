using GOT_Server.Contexts;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GOT_Server.Entities
{
    public class Repositorie
    {
        public string IDRepositorie { get; set; }
        public string NameRepositorie { get; set; }

        internal AppDb Db { get; set; }

        public Repositorie()
        {
        }

        internal Repositorie(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `Commited` (`IDRepositorie`, `NameRepositorie`) VALUES (@id, @name);";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
            //IDCommit = ((int)cmd.LastInsertedId) + 1;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `Repositorie` SET `NameRepositorie` = @name WHERE `IDRepositorie` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Repositorie` WHERE `IDRepositorie` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.String,
                Value = IDRepositorie,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = NameRepositorie,
            });
        }
    }
}
