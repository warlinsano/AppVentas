using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppVentas.Data;
using AppVentas.Droid;
using AppVentas.Models;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDB))]
namespace AppVentas.Droid
{
    //Accedemos a la interfaz que se creo en el proyecto compartido
    public class SQLiteDB : ISQLiteDB
    {
        private string GetPath()
        {   //Se crea la Base de datos
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");
            return path;
        }

        public SQLiteAsyncConnection GetConnectionAsync()
        {
            var path = GetPath();
             var db = new SQLiteAsyncConnection(path);
            //await db.CreateTableAsync<Facturas>(CreateFlags.None).ConfigureAwait(false);
            db.CreateTableAsync<Facturas>();
            db.CreateTableAsync<Clientes>();
            return db;
        }

        public SQLiteConnection GetConnection()
        {
            var path = GetPath();
            var db = new SQLiteConnection(path);
            db.CreateTable<Facturas>();
            db.CreateTable<Clientes>();
            return db;
        }
    }
}