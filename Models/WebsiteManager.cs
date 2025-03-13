using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Models
{
    class WebsiteManager : Ihandler
    {

        public static void AddWebsite(string websiteURL, string websiteFileName)
        {
            string websiteFilePath = Path.Combine(Directory.GetCurrentDirectory(), websiteFileName);
            File.AppendAllText(websiteFilePath, websiteURL + "\n");   
        }

        public void HandleRemove(string websiteFileName)
        {
            Console.Clear();
            Console.WriteLine("=========== REMOVE WEBSITE ===========\n");

            List<string> lines = new List<string>(File.ReadLines(websiteFileName));
            Dictionary<int, string> websites = new Dictionary<int, string>();


            Console.WriteLine("Here is the list of your websites, select one of them to delete. [Q] to go back.\n");

            

            for (int i = 0; i < lines.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. {lines[i]}\n");
                websites.Add(i + 1, lines[i]);
            }

            
            try
            {
                Console.WriteLine();
                char deleteChoice;
                deleteChoice = Console.ReadKey().KeyChar;

                if(deleteChoice == 'q')
                {
                    Console.Clear();
                    ConsoleMenu.HandleConfigMenu("website", HandleAdd, HandleRemove, HandleShowList);
                    return;
                }

                lines.Remove(websites[deleteChoice - '0']);
                websites.Remove(deleteChoice - '0');

                Console.Clear();

                Console.WriteLine("Here is your updated list ^_^ \n");
                int c = 1;
                foreach (var website in websites)
                {
                    Console.WriteLine($"{c}. {website.Value}\n");
                    c++;
                }
                File.WriteAllLines(websiteFileName, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nFile is not found x_x\n");
            }
            finally
            {

                Console.WriteLine("Redirecting...\n");
                Thread.Sleep(2000);
                Console.Clear();
                ConsoleMenu.ShowWebsiteMenu();
            }
        }
        public void HandleAdd(string websiteFileName)
        {
            Console.Clear();
            Console.WriteLine("=========== ADD WEBSITE ===========\n");

            char choice;
            do
            {
                
                Console.Write("\nEnter the URL for a website -> ");
                
                string websiteURL = Console.ReadLine();

                int validationResult = Validation.ValidateURL(websiteURL);

                if (validationResult == 0)
                {
                    Console.WriteLine("Oops! Something is wrong with the way you typed the URL, please be careful and enter the URL again\n");
                    ConsoleMenu.ShowWebsiteMenu();
                }

                Website website = new Website(websiteURL);

                WebsiteManager.AddWebsite(websiteURL, websiteFileName);
                Console.WriteLine("Website URL Has Been Added Successfully!\nPress any key to add more. Press [Q] to head back to menu!\n");
                choice = Console.ReadKey().KeyChar;

            } while (char.ToLower(choice) != 'q');

            ConsoleMenu.ShowMainOptions();
        }
        public void HandleShowList(string websiteFileName)
        {
            Console.WriteLine("=========== YOUR LIST ===========\n");
            try
            {
                List<string> websites = new List<string>(File.ReadLines(websiteFileName));

                int counter = 0;
                foreach (var website in websites)
                {
                    Console.WriteLine($"{++counter}. {website}\n");
                }

                Console.WriteLine("\npress any key to go back");
                Console.ReadKey();
                Console.Clear();
                ConsoleMenu.ShowWebsiteMenu();
            }
            catch
            {
                Console.WriteLine("File does not exist");
                Thread.Sleep(2000);
                ConsoleMenu.ShowWebsiteMenu();
            }
        }
    }
}
