﻿//https://docs.microsoft.com/en-us/dotnet/api/system.io.filesystemwatcher.changed?view=netcore-2.2
using DAL;
using DAL.Models;
using System;
using System.IO;
using System.Net;
using System.Security.Permissions;

namespace FileWatcher
{
    class Program
    {
        public static int lastRead = 0;
        static void Main(string[] args)
        {
            Run();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        public static void Run()
        {
            string[] args = System.Environment.GetCommandLineArgs();

            // If a directory is not specified, exit program.
            if (args.Length != 2)
            {
                // Display the proper way to call the program.
                Console.WriteLine("Usage: Watcher.exe (directory)");
                //return;
            }

            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = args[1];
            watcher.IncludeSubdirectories = true;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter = "*.log";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            try
            {
                using (FileStream stream = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        // Read the stream to a string, and write the string to the console.
                        string line;
                        int currentLine = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            currentLine++;
                            if (currentLine > Program.lastRead)
                            {
                                Console.WriteLine(line);
                                SendEvent(line, currentLine, e.FullPath);
                                Program.lastRead += 1;
                            }
                        }
                        //sr.Close();
                        //sr.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            //Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }

        public static void SendEvent(string data, int line, string path)
        {
            Log log = new Log(data, line, path, "fileWatcher", "LOG", "INFO", "0");
            string url = @"http://192.168.1.16:8001/Log";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Accept = "application/json; charset=utf-8";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(log.ToJson());
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        }
    }
}