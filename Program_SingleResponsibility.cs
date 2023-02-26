using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int AddEntry(string text)
        {
            entries.Add($"{++count}:{text}");
            return count; //memento
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        #region "Second Responsibility for Journal Class"
        /*
        public void Save(string filename)
        {
            File.WriteAllText(filename, ToString());
        }

        public void Load(Uri uri) {
        }
        */
        #endregion



    }

    #region "Persistent Class to save any object of a class"
    public class Persistence
    {
        public void SaveToFile(Journal j,string filename,bool overwrite = false)
        {
            if(overwrite || !File.Exists(filename))
            {
                File.WriteAllText(filename, j.ToString());
            }
        }
    }
    #endregion
    public class Demo
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("Subhanallah!");
            j.AddEntry("Al-Hamdulillah!");
            j.AddEntry("Allahuakbar!");
            Console.WriteLine(j);

            var p = new Persistence();
            var filename = @"C:\Users\SYED AMANULLAH\Source\Repos\DesignPatterns\journal.txt";
            p.SaveToFile(j, filename, true);
            Process.Start(filename);
            
            Console.ReadLine();
        }
    }
}
