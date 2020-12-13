using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioritTestTask
{
    /// <summary>
    /// Класс печатает содержимое всей базы
    /// </summary>
    class ReadDataBase
    {
        public static void Read(string PathUsers,string DataBase, string TableName)
        {
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={PathUsers}; Version=3;"))
            {
                Connect.Open();
                SQLiteCommand Command = new SQLiteCommand
                {
                    Connection = Connect,
                    CommandText = $@"SELECT * FROM [{TableName}]"
                };
                SQLiteDataReader sqlReader = Command.ExecuteReader();
            
                while (sqlReader.Read())
                {
                    string Name = (string)sqlReader["Name"];
                    string Surname = (string)sqlReader["Surname"];
                    var Age = sqlReader["Age"];
                    var Tracking = sqlReader["Tracking"];
                    //Вывод результата в консоль
                    Console.WriteLine($@"Name:{Name} Surname:{Surname} Age:{Age} {Tracking}");

                    

                }
                Connect.Close();
            }

        }
    }
}