using System;
using System.IO;
using Salaros.Configuration;

namespace Wallfl0w3r
{
    class Program
    {

        static WallpaperStyle style = WallpaperStyle.Fill;

        static void Main(string[] args)
        {
            Directory.CreateDirectory("images");
            ConfigParser config = new ConfigParser("config.ini");
            string watchFolder = config.GetValue("general", "watchFolder");
            string wallpaperStyle = config.GetValue("general", "wallpaperStyle", "Fill");

            try
            {

                style = Enum.Parse<WallpaperStyle>(wallpaperStyle, true);
            } catch(Exception e)
            {
                Console.WriteLine("error parsing wallpaper style. Leaving at default Fill");
                style = WallpaperStyle.Fill;
            }

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
                System.Threading.Thread.Sleep(5000); // In case the image isn't fully written yet? Give it time.
                string fullPath = Path.GetFullPath(e.FullPath);
                Console.WriteLine("Setting wallpaper silently: "+fullPath);
                Wallpaper.SilentSet(fullPath,style);
            }
        }
    }
}
