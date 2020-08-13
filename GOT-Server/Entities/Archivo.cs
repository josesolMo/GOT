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
    // Clase Archivo, estructura base de la tabla SQL
    public class Archivo
    {
        //Contiene el Identificador único del archivo
        public int IDArchivo { get; set; }
        //Contiene la dirección del archivo
        public string DireccionArchivo { get; set; }
        //Contiene los datos internos del archivo
        public string DataArchivo { get; set; }
        //Contiene Nombre del archivo
        public string NombreArchivo { get; set; }
        public int IDRepositorie { get; set; }
        public int IDCommit { get; set; }

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
            cmd.CommandText = @"INSERT INTO `Archivo` (`DireccionArchivo`, `NombreArchivo`, `DataArchivo`, `IDRepositorie`, `IDCommit`) VALUES (@dir, @name, @data, @idrepo, @idcommit);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            IDArchivo = ((int)cmd.LastInsertedId);
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `Archivo` SET `DireccionArchivo` = @dir,  `NombreArchivo` = @name, `DataArchivo` = @data, `IDRepositorie` = @idrepo, `IDCommit` = @idcommit WHERE `IDArchivo` = @id;";
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
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idrepo",
                DbType = DbType.Int32,
                Value = IDRepositorie,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idcommit",
                DbType = DbType.Int32,
                Value = IDCommit,
            });
        }

    }
}
