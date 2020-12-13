using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AprioritTestTask
{
   class Program
    {
     
        #region Models
        //Модель Users
        public class Users
        {
            // Auto-Initialized properties  
            public string Name { get; set; }
            public string Surname { get; set; }
            public int Age { get; set; }

        }
        //Модель Tracking
        public class Tracking
        {
            public string TrackCoordinate { get; set; }
        }

        /// <summary>
        /// Модель Zaliv
        /// </summary>
        public class Zaliv
        {
            // Auto-Initialized properties  
            public string Name { get; set; }
            public string Surname { get; set; }
            public int Age { get; set; }
            public string TrackCoordinate { get; set; }
        }

        #endregion

        #region GetUserInfo Разбиираем строку которая относится непосредственно к пользователю
        private static void GetUserInfo(string Stroka, out string Name, out string Surname, out int Age)
        {
            Name = string.Empty;
            Surname = string.Empty;
            Age = 0;

            List<Users> User = new List<Users>();

            String wordQ = Stroka.Substring(0, Stroka.IndexOf(":" + 'q') - 1);
            String wordT = wordQ.Substring(0, Stroka.IndexOf(":" + 't') - 1);

            string[] words = wordT.Split(' ');

            int counter = words.Count();

            //Console.WriteLine("данные для заливки всего данных count: {0}", counter);

            #region Добавление пользователей
            User.Add(new Users
            {
                Name = words[0],
                Surname = words[1],
                Age = Convert.ToInt32(words[2])
            }
               );

            foreach (var item in User)
            {
                Name = item.Name;
                Surname = item.Surname;
                Age = item.Age;

                //Console.WriteLine(@"Содержимое User
                //                        Name:{0}
                //                        Surname:{1}
                //                        Age:{2}
                //                        ", item.Name, item.Surname, item.Age);
            }
            #endregion

        }
        #endregion

        #region GetTrackingInfo Разбиираем строку которая относится непосредственно к Tracking
        private static void GetTrackingInfo(string Stroka, out string ResultString)
        {
            String wordQ = Stroka.Substring(0, Stroka.IndexOf(':' + "" + 'q') - 1);
            String wordT = wordQ.Substring(0, Stroka.IndexOf(':' + "" + 't') - 1);
            string[] words = wordT.Split(' ');
            string ResultCoordinate = string.Empty;
            List<Tracking> Tracking = new List<Tracking>();
            for (int i = 0; i < words.Length; i++)
            {
                if (i > 2)
                {
                    //  Console.WriteLine("i:{0} words:{1}", i, words[i]);

                    Tracking.Add(new Tracking
                    {
                        TrackCoordinate = words[i]
                    });
                }
            }
            foreach (var itemTracking in Tracking)
            {
                ResultCoordinate += String.Join(" ", itemTracking.TrackCoordinate.ToString() + " ");
                // Console.WriteLine(@"----Что запишется в базу данных:{0}", ResultCoordinate);
            }
            // Console.WriteLine(@"Итого:{0} ", ResultCoordinate);
            ResultString = ResultCoordinate;
        }
        #endregion


        static void Main(string[] args)
        {
            string dirName = @"C:\TestApp1\";
         
            #region Начальные параметры
            if (Directory.Exists(dirName))
            {
                //Продолжаем работу
            }
            else
            {
                Console.WriteLine($@"Создаем каталог {dirName}");
                Directory.CreateDirectory($@"{dirName}");
            }

            String PatchUsers = $@"{dirName}users.db";
            String PatchTracking = $@"{dirName}tracking.db";
            string Action = string.Empty;
            string Password = string.Empty;
            string Find = string.Empty;
            
            if (args[0] == "find" && args.Length == 4)
            {
                // Console.WriteLine("первый вариант поиска с одним словом");
                Action = args[0];
                Password = args[3];
                Find = "1";

            }
            else if (args[0] == "find" && args.Length == 5)
            {
                //Console.WriteLine("Второй вариант поиска с двумя словами");
                Action = args[0];
                Password = args[4];
                Find = "2";
            }
            else //Для двух других комманд так как ключ и пароль не соответствуют аргументам поп порядку
            {
               // Console.WriteLine("Все остальные комманды READ и ADD");
                Action = args[0];
                Password = args[2];
            }
            #endregion


            //Проверяем есть ли необходимые Базы данных в наличии или создаем новвые
            bool DB = AprioritTestTask.StartCheck.Start(PatchUsers, PatchTracking, Password); // args[2]);

            if (DB == true)
            {
                var CntStroka = args.Length;
                try
                {
                    //Console.WriteLine("Action:{0} Password:{1}",Action,Password);

                        switch (Action.ToString())
                        {
                            #region Add - добавить записи в БД
                            case "add":
                                //Строка без первых трех Обязательных параметров
                                int str1 = args.Length;
                                List<string> Zaliv = new List<string>();
                                string Stroka = String.Empty;

                                for (int i = 0; i < str1; i++)
                                {
                                    if (i > 2)
                                    {
                                        Zaliv.Add(args[i]);
                                    }
                                }
                                Stroka += string.Join(" ", Zaliv);

                                //Выбираем данные для записи в БД параматров пользователя
                                GetUserInfo(Stroka, out string NameToDb, out string SurnameToD, out int AgeToDb);

                                //Из параметров берем данные для записи Tracking
                                GetTrackingInfo(Stroka, out string Coordinate);

                                ///Выполняем Запись в БД
                                AprioritTestTask.AddDatabase.Add(PatchUsers, PatchTracking, NameToDb, SurnameToD, AgeToDb, Coordinate);

                                break;
                            #endregion
                            #region read - вывести все что ест в БД
                            case "read":
                                //Выполняем считывание с БД users
                                AprioritTestTask.ReadDataBase.Read(PatchUsers, "users", "users");
                                break;
                        #endregion
                            #region find - выполнить поиск по слову и вывести в консоль
                        case "find":
                            //Выполняем поиск с БД users
                            if (Find == "1")
                            { 
                            AprioritTestTask.FindDataBase.Find(PatchUsers, "users", "users", args[1]);
                            }
                            if(Find == "2")
                            {
                                AprioritTestTask.FindDataBase.Find(PatchUsers, "users", "users", args[1],args[2]);
                            }
                            break;
                            #endregion
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Что то пошло не так, или не указаны ключи :t:q 
                                        или не хватает параметров
                                        Текст ошибки:{0}", ex.ToString());

                }
            }
            Console.ReadKey();
        }
      }
    }


