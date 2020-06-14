using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AppVentas.Data;
using AppVentas.iOS;
using AppVentas.Models;
using Foundation;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDB))]
namespace AppVentas.iOS
{
    public class SQLiteDB : ISQLiteDB
    {
        private string GetPath()
        {   
            //Se crea la Base de datos
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");
            return path;
        }

        public SQLiteAsyncConnection GetConnectionAsync()
        {
            var path = GetPath();
            var db = new SQLiteAsyncConnection(path);
            db.CreateTableAsync<T_Registro>();
            return db;
        }

        public SQLiteConnection GetConnection()
        {
            var path = GetPath();
            var db = new SQLiteConnection(path);
            db.CreateTable<T_Registro>();
            return db;
        }
    }
}