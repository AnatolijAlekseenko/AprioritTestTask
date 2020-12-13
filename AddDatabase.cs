using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioritTestTask
{
    class AddDatabase
    {
        static int IdUser = 0;

        public static void Add(string UserPath,string TrackingPath,string Name, string SurName, int Age, string Tracking)
        {
            //Console.WriteLine("UserPath" + UserPath);
            //Console.WriteLine("TrackingPath" + TrackingPath);
            // "C:\TestApp\users.db"";
            using (SQLiteConnection ConnectUsers = new SQLiteConnection($@"Data Source={UserPath}; Version=3;"))
            {
                ConnectUsers.Open();
                SQLiteCommand Command = new SQLiteCommand
                {
                    Connection = ConnectUsers,
                    CommandText = $@"INSERT INTO [USERS] (Name,Surname,Age,Tracking) 
                                                        values('{Name}',
                                                                '{SurName}',
                                                                '{Age}',
                                                                  '{Tracking}')"
                };
                int sqlReader = Command.ExecuteNonQuery();

                SQLiteCommand GetId = new SQLiteCommand
                {
                    Connection = ConnectUsers,
                    CommandText = $@"select id from [USERS] where Name ='{Name}'
                                                                   and
                                                                   Surname='{SurName}' 
                                                                   and 
                                                                   Age='{Age}'
                                                                   and
                                                                   Tracking='{Tracking}'
                                                                   "
                };

                IdUser = Convert.ToInt32(GetId.ExecuteScalar());
             
                ConnectUsers.Close();
            }


            using (SQLiteConnection ConnectTracking =
            new SQLiteConnection($@"Data Source={TrackingPath}; Version=3;"))
            {
                ConnectTracking.Open();

                SQLiteCommand InsertTracking = new SQLiteCommand
                {


                    Connection = ConnectTracking,
                    CommandText = $@"INSERT INTO [tracking] (UserId,Track) 
                         values({IdUser},
                                '{Tracking}')"
                };

                int sqlReader2 = InsertTracking.ExecuteNonQuery();


                Console.WriteLine($@"В таблицу users Добавлены следующие данные:
                                        Name:{Name}
                                        SurName:{SurName}
                                        Age:{Age}
                                        Tracking:{Tracking}
                                     ");
                ConnectTracking.Close();
            }

        }


        public static void Add(string Name, string SurName, int Age)
        {
            Console.WriteLine("test");
            using (SQLiteConnection ConnectUsers = new SQLiteConnection($@"Data Source=C:\TestApp\users.db; Version=3;"))
            {
                ConnectUsers.Open();
                SQLiteCommand Command = new SQLiteCommand
                {
                    Connection = ConnectUsers,
                    CommandText = $@"INSERT INTO [USERS] (Name,Surname,Age) 
                                                        values('{Name}',
                                                                '{SurName}',
                                                                '{Age}')"
                };
                int sqlReader = Command.ExecuteNonQuery();
                //Console.WriteLine("Запись в таблицу users:{0}", sqlReader);
                ConnectUsers.Close();
            }

        }
    


    //public static void Add(string Name, string SurName, int Age, double Track1, double Track2)
    //{
    //    using (SQLiteConnection ConnectUsers = new SQLiteConnection($@"Data Source=C:\Programming\TestTask\users.db; Version=3;"))
    //    {
    //        ConnectUsers.Open();
    //        SQLiteCommand Command = new SQLiteCommand
    //        {
    //            Connection = ConnectUsers,
    //            CommandText = $@"INSERT INTO [USERS] (Name,Surname,Age) 
    //                                                    values('{Name}',
    //                                                            '{SurName}',
    //                                                            '{Age}')"
    //        };
    //        int sqlReader = Command.ExecuteNonQuery();
    //       // Console.WriteLine("Запись в таблицу users:{0}", sqlReader);

    //        SQLiteCommand GetId = new SQLiteCommand
    //        {
    //            Connection = ConnectUsers,
    //            CommandText = $@"select id from [USERS] where Name ='{Name}'
    //                                                               and
    //                                                               Surname='{SurName}' 
    //                                                               and 
    //                                                               Age='{Age}'
    //                                                               "
    //        };

    //        IdUser = Convert.ToInt32(GetId.ExecuteScalar());
    //       // Console.WriteLine("Получение Id пользователя из табл. userId:{0}", IdUser);
    //        ConnectUsers.Close();
    //    }


    //    using (SQLiteConnection ConnectTracking =
    //    new SQLiteConnection($@"Data Source=C:\Programming\TestTask\tracking.db; Version=3;"))
    //    {
    //        ConnectTracking.Open();

    //        SQLiteCommand InsertTracking = new SQLiteCommand
    //        {


    //            Connection = ConnectTracking,
    //            CommandText = $@"INSERT INTO [tracking] (UserId,Track1,Track2) 
    //                     values({IdUser},
    //                            '{Track1}'
    //                            ,'{Track2}')"
    //        };

    //        int sqlReader2 = InsertTracking.ExecuteNonQuery();


    //        //Console.WriteLine($@"В таблицу users добавлены следующие данные:
    //        //                            Name:{Name}
    //        //                            SurName:{SurName}
    //        //                            Age:{Age}
    //        //                         ");
    //        ConnectTracking.Close();
    //    }

    //}
}
}