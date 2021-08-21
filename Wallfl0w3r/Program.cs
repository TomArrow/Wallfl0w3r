using System;
using System.IO;
using Salaros.Configuration;

namespace Wallfl0w3r
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.CreateDirectory("images");
            ConfigParser config = new ConfigParser("config.ini");
            string watchFolder = config.GetValue("general", "watchFolder");

            FileSystemWatcher fsw = new FileSystemWatcher();

            fsw.Path = watchFolder;

            fsw.Created += fsw_created;

            fsw.EnableRaisingEvents = true;

            Console.WriteLine("Monitoring "+watchFolder+" now. Press any key to stop");
            Console.ReadKey();
        }

        private static void fsw_created(object sender, FileSystemEventArgs e)
        {
            
            Console.WriteLine(e.Name + " was created in watch folder.");
            string extension = Path.GetExtension(e.Name).ToLower();
            if(extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp")
            {
                string fullPath = Path.GetFullPath(e.FullPath);
                Console.WriteLine("Setting wallpaper silently: "+fullPath);
                Wallpaper.SilentSet(fullPath);
            }
        }
    }
}
