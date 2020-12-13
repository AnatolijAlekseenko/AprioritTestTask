using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioritTestTask
{
    class UsersDatabase
    {
        public static void CreateDatabaseUsers(string PathUsers,string PathTracking) 
        { 
                Console.WriteLine($"Создаем БД users:{PathUsers}");
            
            // создать базу данных, по указанному пути содаётся пустой файл базы данных"); 
            SQLiteConnection.CreateFile($@"{PathUsers}"); 
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={PathUsers}; Version=3;")) // в строке указывается к какой базе подключаемся
                {
                    // строка запроса, который надо будет выполнить
                    string commandText = @"
                            CREATE TABLE users(
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            Name    STRING(255),
                                            Surname STRING(255),
                                            Age     INTEGER(20),
                                            Tracking STRING (1000)  
                                            )
                    "; // создать таблицу, если её нет
        SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
        Connect.Open(); // открыть соединение
                    Command.ExecuteNonQuery(); // выполнить запрос
                    Connect.Close(); // закрыть соединение


                if (!File.Exists($@"{PathTracking}")) // если базы данных нету, то...
                {
                    //Создаем базу tracking
                    AprioritTestTask.TrackingDatabase.CreateDatabaseTrackings(PathTracking);
                }
                else
                {
                    Console.WriteLine("Обе базы данных созданы");
                }
            }


            


        }
    }
}
