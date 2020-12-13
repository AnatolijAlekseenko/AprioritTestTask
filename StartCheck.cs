using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioritTestTask
{
    class StartCheck
    {
        public static bool Start(string PathUsers, string PathTracking, string password)
        {
            bool ret = false;
            var x = new AprioritTestTask.Xor();
            string DB = string.Empty;
            string DB2 = string.Empty;

            var encryptedMessageByPassusers = x.Encrypt("users.db", password);
            var encryptedMessageByPasstracking = x.Encrypt("tracking.db", password);

            if (password == "12345")
            {
               
                DB = x.Decrypt(encryptedMessageByPassusers, password);
                DB2 = x.Decrypt(encryptedMessageByPasstracking, password);


                ret = true;
                if (!File.Exists($@"{PathUsers}") || !File.Exists($@"{PathTracking}")) // если базы данных нету, то...
                {
                    //Создаем новые БД
                    AprioritTestTask.UsersDatabase.CreateDatabaseUsers(PathUsers, PathTracking);
                    ret = true;
                }
                else
                {
                   // Console.WriteLine("Базы данных users and tracking уже существуют");
                    ret = true;
                }
            }
            else {

                Console.WriteLine("Password is incorrect.");
                ret = false;

            }
            //Console.WriteLine("Зашифрованное сообщение {0}", encryptedMessageByPass);
            //Console.WriteLine("Расшифрованное сообщение {0}", x.Decrypt(encryptedMessageByPass, password));
            //Console.ReadLine();



          
           
            return ret;
        }
        
    }
}
