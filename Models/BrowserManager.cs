using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Models
{
    class BrowserManager
    {
        public Browser browser;
        List<string> websites; //to load the websites

        public static bool CheckBrowserAvailability(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if (File.Exists(filePath))
            {
                IEnumerable<string> line = File.ReadLines(filePath);
                if (line.Count() == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        public void AddBrowserToFile(string fileName, string browserPath) {

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.WriteAllText(filePath, browserPath);
            Console.WriteLine("\nBrowser Updated Successfully!");
            Console.WriteLine("Redirecting...");
            Thread.Sleep(3000);
            ConsoleMenu.ShowMainOptions();
        }
        public void LoadBrowserAndSites(string filePath) {
            if (File.Exists(filePath))
            {
                IEnumerable<string> line = File.ReadLines(filePath);
                if (line.Count() == 0)
                {
                    Console.WriteLine("There is no browser");
                }
                else
                {
                    try
                    {
                        string path = line.First();

                        string websitesFile = "websites.txt";

                        if(File.Exists(websitesFile))
                            websites = new List<string>(File.ReadAllLines(websitesFile));


                        if (path.StartsWith("https") || path.StartsWith("www"))
                            throw new Exception();

                        Process.Start(new ProcessStartInfo(path, websites) { UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Browser path is invalid.");
                        Console.WriteLine("Redirecting...");
                        Thread.Sleep(3000);
                        ConsoleMenu.ShowMainOptions();
                    }
                }
            }
        }

        public static string FetchBrowserName(Browser browser)
        {
            string browserName = "";
            try
            {
                string brwsrPath = browser.Path;
                browserName = Path.GetFileNameWithoutExtension(brwsrPath);
            }
            catch (Exception ex)
            {}

            return browserName;
        }
        public static string FetchBrowserName(string brwsrTxtFile)
        {
            string browserName = "";
            try
            {
                string brwsrPath = File.ReadLines(brwsrTxtFile).First();
                browserName = Path.GetFileNameWithoutExtension(brwsrPath);
            }
            catch (Exception ex)
            {}

            return browserName;
        }
    }
}
