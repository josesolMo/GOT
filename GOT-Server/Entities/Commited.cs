using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using GOT_Server.Contexts;

namespace GOT_Server.Entities
{
    public class Commited
    {
        public int IDCommit { get; set; }
        public string MessageCommit { get; set; }
        public DateTime DateCommit { get; set; }

        internal AppDb Db { get; set; }

        public Commited()
        {
        }

        internal Commited(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `Commited` (`IDCommit`, `MessageCommit`, `DateCommit`) VALUES (@id, @message, @date);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            DateCommit = DateTime.Now.AddDays(0);
            IDCommit = ((int)cmd.LastInsertedId);
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `Commited` SET `MessageCommit` = @message, `DateCommit` = @date WHERE `IDCommit` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Commited` WHERE `IDCommit` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = IDCommit,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@message",
                DbType = DbType.String,
                Value = MessageCommit,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@date",
                DbType = DbType.DateTime,
                Value = DateCommit,
            });
        }
    }
}
