using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioritTestTask
{
    class TrackingDatabase
    {
        public static void CreateDatabaseTrackings(string PathTracking)
        {
            Console.WriteLine($"Создаем БД tracking по пути: {PathTracking}");

            SQLiteConnection.CreateFile($"{PathTracking}"); // создать базу данных, по указанному пути содаётся пустой файл базы данных"); 

            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={PathTracking}; Version=3;")) // в строке указывается к какой базе подключаемся
            {
                // строка запроса, который надо будет выполнить
                string commandText = @"
                            CREATE TABLE tracking(
                                           Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            UserID INTEGER(20),
                                            Track1 DOUBLE (10, 3),
                                            Track2 DOUBLE (10, 3),
                                            Track  STRING (1000) 
                                            )
                    "; // создать таблицу, если её нет
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open(); // открыть соединение
                Command.ExecuteNonQuery(); // выполнить запрос
                Connect.Close(); // закрыть соединение
            }

           // Console.WriteLine("Перезапустите еще раз программу для работы с создаными базами данных");
            

        }
    }
}


