using System.Diagnostics;
using System.IO;
using System;

namespace NumericalCalculator
{
    public static class Logger
    {
        private static string fileName = "log.txt";

        public static void Log(string message)
        {
            StreamWriter file;
            Stream s;

            if (File.Exists(fileName))
                s = new FileStream(fileName, FileMode.Append);
            else
                s = new FileStream(fileName, FileMode.CreateNew);

            file = new StreamWriter(s);

            file.WriteLine(DateTime.Now + ": " + message);

            file.Flush();
            file.Close();
        }
    }
}
