using System;
using System.IO;
using QuickType;

namespace quicktypetest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var p = LoadJson();

            var data = Pokedex.FromJson(p);

            Console.WriteLine(data.Pokemon[0].Name);
        }

        public static string LoadJson()
        {
            using (StreamReader r = new StreamReader("pokedex.json"))
            {
                string json = r.ReadToEnd();

                return json;
            }
        }
    }
}
