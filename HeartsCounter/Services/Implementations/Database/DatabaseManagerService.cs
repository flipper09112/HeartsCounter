﻿using HeartsCounter.Helpers;
using HeartsCounter.Models.Games;
using HeartsCounter.Services.Interfaces.Database;
using SQLite;

namespace HeartsCounter.Services.Implementations.Database
{
    public class DatabaseManagerService : IDatabaseManagerService
    {
        private SQLiteConnection _conn;

        private string _dbPath = FileAccessHelper.GetLocalFilePath("db.db3");

        public SQLiteConnection SQLConnetion => _conn;

        public DatabaseManagerService()
        {
            Init();
        }

        private void Init()
        {
            if (_conn != null)
                return;

            var name = System.IO.Path.Combine(FileSystem.AppDataDirectory, "db.db3");
            if(!File.Exists(name))
                File.Create(name).Close();


            _conn = new SQLiteConnection(_dbPath);

            _conn.CreateTable<Game>();
        }
    }
}