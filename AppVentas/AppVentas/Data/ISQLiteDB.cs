using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppVentas.Data
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnectionAsync();

        SQLiteConnection GetConnection();
    }
}
