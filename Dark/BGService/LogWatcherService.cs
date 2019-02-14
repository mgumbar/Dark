﻿using Dark.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dark.BGService
{
    public class LogWatcherService : BackgroundService
    {
        private readonly ILogger<LogWatcherService> _logger;
        private readonly IHubContext<LogHub> _logHub;
        private Dictionary<string, long> _fileIndex = new Dictionary<string, long>();

        public LogWatcherService(ILogger<LogWatcherService> logger, IHubContext<LogHub> logHub)
        {
            this._logger = logger;      
            this._logHub = logHub;      
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => Console.WriteLine("Test start 2"));
            _logger.LogDebug($"GracePeriodManagerService is starting.");

            await Task.Run(() => Console.WriteLine("Test start 3"));
            stoppingToken.Register(() =>
                    _logger.LogDebug($" GracePeriod background task is stopping."));
            await Task.Run(() => Console.WriteLine("Test start 4"));

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(() => Console.WriteLine("Test start 5"));
                _logger.LogDebug($"GracePeriod task doing background work.");
                await Task.Run(() => Console.WriteLine("Test start 6"));
                this.Watch();

                // This eShopOnContainers method is querying a database table 
                // and publishing events into the Event Bus (RabbitMS / ServiceBus)

            }

            _logger.LogDebug($"GracePeriod background task is stopping.");

        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => Console.WriteLine("Test stop"));
            // Run your graceful clean-up actions
            _logger.LogDebug($"Stoping asynchronusly");

        }

        private void Watch()
        {
            Console.WriteLine("Test start 7");
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = "/var/lib/docker/containers/";
            Console.WriteLine("Test start 8");
            //watcher.Path = @"C:\Users\hp_envy\Downloads\";
            if (Directory.Exists(watcher.Path))
            {
                Console.WriteLine("IT EXISTS");
            }
            else
            {
                Console.WriteLine("IT DOES NOT EXISTS");
            }

            watcher.IncludeSubdirectories = true;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter = "*.*";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChangedAsync);
            watcher.Created += new FileSystemEventHandler(OnChangedAsync);
            watcher.Deleted += new FileSystemEventHandler(OnChangedAsync);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }

        // Define the event handlers.
        private void OnChangedAsync(object source, FileSystemEventArgs e)
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
                        if (!this._fileIndex.ContainsKey(e.FullPath))
                        {
                            this._fileIndex.Add(e.FullPath, currentLine);
                        }
                        while ((line = sr.ReadLine()) != null)
                        {
                            currentLine++;
                            var previousReadLine = this._fileIndex[e.FullPath];
                            if (currentLine > previousReadLine)
                            {
                                //SendEvent(line, currentLine, e.FullPath);
                                _logHub.Clients.All.SendAsync("ReceiveMessage", e.FullPath, line);
                                this._fileIndex.Remove(e.FullPath);
                                this._fileIndex.Add(e.FullPath, previousReadLine + 1);
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

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            //Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }
    }
}
