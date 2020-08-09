using GOT_Server.Contexts;
using Microsoft.AspNetCore.Authentication;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GOT_Server.Entities
{
    public class Archivo
    {
        public string IDArchivo { get; set; }
        public string DireccionArchivo { get; set; }
        public string DataArchivo { get; set; }
        public string NombreArchivo { get; set; }

        internal AppDb Db { get; set; }

        public Archivo()
        {
        }

        internal Archivo(AppDb db)
        {
            Db = db;
        }


        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `Archivo` (`DireccionArchivo`, `DataArchivo`, `NombreArchivo`) VALUES (@dir, @data, @name);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            IDArchivo = ((int)cmd.LastInsertedId).ToString();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `Archivo` SET `DireccionArchivo` = @dir, `DataArchivo` = @data, `NombreArchivo` = @name WHERE `IDArchivo` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Archivo` WHERE `IDArchivo` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.String,
                Value = IDArchivo,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dir",
                DbType = DbType.String,
                Value = DireccionArchivo,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@data",
                DbType = DbType.String,
                Value = DataArchivo,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = NombreArchivo,
            });
        }

    }
}
