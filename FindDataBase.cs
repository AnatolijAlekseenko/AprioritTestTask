using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioritTestTask
{
    class FindDataBase
    {
        public static void Find(string PathUsers, string DataBase, string TableName, string SearchWord,string SerchTwoWord)
        {
            Console.WriteLine("Ищем по запросу двух слов:{0} {1}", SearchWord, SerchTwoWord);
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={PathUsers}; Version=3;"))
            {
                Connect.Open();
                SQLiteCommand Command = new SQLiteCommand
                {
                    Connection = Connect,
                    CommandText = $@"SELECT * FROM [{TableName}] WHERE Name like ('%{SearchWord}%')
                                                                             or 
                                                                    Surname like('%{SerchTwoWord}%')"
                };
                SQLiteDataReader sqlReader = Command.ExecuteReader();
                Console.WriteLine("НАЙДЕНО:");
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

        public static void Find(string PathUsers, string DataBase, string TableName,string SearchWord)
        {
            int SearchWord_Param = 0;
            string CommandText = string.Empty;

            if (!SearchWord.Contains("*"))
            {
                SearchWord_Param = 1;
                //Console.WriteLine(SearchWord_Param);
            }
            else {

                SearchWord_Param = 2;
               // Console.WriteLine(SearchWord_Param);
            }
            if (SearchWord_Param == 1)
            {
               // Console.WriteLine("не содержит *");
                CommandText = $@"SELECT * FROM [{TableName}] WHERE Name ='{SearchWord}'";

            }
            else
            {
                //Удаляем один симол SearchWord а именно *
                SearchWord = SearchWord.Remove(SearchWord.Length - 1);

                // Console.WriteLine("Содержит *");
                CommandText = $@"SELECT * FROM [{TableName}] WHERE Name like ('%{SearchWord}%')";
            }


            //Console.WriteLine("Ищем по запросу одного слова:{0}", SearchWord);
            
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={PathUsers}; Version=3;"))
            {
                Connect.Open();
                SQLiteCommand Command = new SQLiteCommand
                {
                    Connection = Connect,
                    CommandText = $@"SELECT * FROM [{TableName}] WHERE Name like ('%{SearchWord}%')"
                };
                SQLiteDataReader sqlReader = Command.ExecuteReader();
                Console.WriteLine("НАЙДЕНО:");
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

