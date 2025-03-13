using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Models
{
    class ConsoleMenu
    {
        private static BrowserManager browserManager = new();
        private static WebsiteManager websiteManager = new();
        private static PdfManager pdfmgr = new();

        public static void ShowStartMessage()
        {
            string visit = "visit.txt";

            if(File.Exists(visit))
            {
                ShowMainOptions();
            }
            else
            {
                Console.WriteLine("Welcome to FLOW!");
                Console.WriteLine();
                Console.WriteLine("FLOW is a simple yet powerful console application designed to boost your productivity.");
                Console.WriteLine("It allows you to:");
                Console.WriteLine("- Add and manage your preferred browsers");
                Console.WriteLine("- Save frequently visited websites");
                Console.WriteLine("- Store and access important PDFs easily");
                Console.WriteLine();
                Console.WriteLine("With FLOW, you no longer need to open everything manually.");
                Console.WriteLine("Just launch the application, press a key, and all your saved preferences will start automatically—");
                Console.WriteLine("helping you focus on what truly matters.");
                Console.WriteLine();
                Console.WriteLine("Get started and enjoy a seamless workflow!");
                Console.WriteLine();
                Console.WriteLine("Press any key");
                using (FileStream fs = File.Create(visit))
                {
                    Console.ReadKey();
                    ShowMainOptions();
                }
            }
                
        }
        public static void ShowMainOptions() {

            string brwsrTxtFile = "browserFile.txt";
            string browserName = (File.Exists(brwsrTxtFile) ? BrowserManager.FetchBrowserName(brwsrTxtFile) : "");

            Console.Clear();
            Console.WriteLine("=========== MAIN MENU ===========\n");

            Console.WriteLine($"1. Add Browser <Current: \"{(string.IsNullOrEmpty(browserName) ? "No Browser Yet" : browserName)}\">\n");

            Console.WriteLine("2. Edit your website list (Do not forget to add a browser first!)\n");

            Console.WriteLine("3. Edit your pdf list\n");

            Console.WriteLine("4. Start the Processes \n");

            HandleUserNavigation();
        }

        public static void ShowBrowserMenu() {
            Console.WriteLine("=========== ADD BROWSER ===========\n");

            Console.Write("Enter the full path for your preferred browser (Adding a new browser will override the old one) -> ");
            string browserPath = Console.ReadLine();

            int validationResult = Validation.ValidateURL(browserPath);

            if (validationResult == 0)
            {
                Console.WriteLine("Oops! Something is wrong with the way you typed the path, please be careful and enter the path again\n");
                ShowBrowserMenu();
            }


            var browser = new Browser(browserPath);

            string browserName = BrowserManager.FetchBrowserName(browser);
            browser.Name = browserName;

            string fileName = "browserFile.txt";

            browserManager.AddBrowserToFile(fileName, browserPath);
           
        }

        public static void ShowWebsiteMenu() => HandleConfigMenu("website", websiteManager.HandleAdd, websiteManager.HandleRemove, websiteManager.HandleShowList);
        public static void ShowPDFs() => HandleConfigMenu("pdf", pdfmgr.HandleAdd, pdfmgr.HandleRemove, pdfmgr.HandleShowList);

       

        public static void Start()
        {
            string wholePath = Path.Combine(Directory.GetCurrentDirectory(), "browserFile.txt");
            browserManager.LoadBrowserAndSites(wholePath);
            PdfManager.LoadPDFS("pdfs.txt");
        }

        public static void HandleUserNavigation() {

            Console.Write("Select an option from the menu above -> ");

            try
            {
                int userChoice = Convert.ToInt32(Console.ReadLine());

                switch (userChoice)
                {
                    case 1: Console.Clear(); ShowBrowserMenu(); break;
                    case 2: Console.Clear(); ShowWebsiteMenu(); break;
                    case 3: Console.Clear(); ShowPDFs(); break;
                    case 4: Console.Clear(); Start(); break;
                    default:
                        Console.WriteLine("Invalid Choice! Try again");
                        HandleUserNavigation();
                        break;
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Invalid Choice! Try again");
                HandleUserNavigation();
            }
        }


        public static void ConfigMenu(string type)
        {
            Console.WriteLine($"=========== {type.ToUpper()} MENU ===========\n");


            Console.WriteLine($"1. Add {type}\n");
            Console.WriteLine($"2. Remove {type}\n");
            Console.WriteLine($"3. View {type} list\n");
            Console.WriteLine($"4. Back\n");
        }
        public static void HandleConfigMenu(string type, Action<string>add, Action<string>remove, Action<string>showList)
        {
            char config;
            ConfigMenu($"{type}");
            config = Console.ReadKey().KeyChar;

            string pdfFile = $"{type}s.txt";

            switch (config)
            {
                case '1': 
                    Console.Clear(); 
                    add(pdfFile); 
                    break;
                case '2': 
                    Console.Clear(); 
                    remove(pdfFile); 
                    break;
                case '3': 
                    Console.Clear(); 
                    showList(pdfFile); 
                    break;
                case '4': 
                    Console.Clear(); 
                    ShowMainOptions(); 
                    break;
                default:
                    Console.WriteLine("\nInvalid Choice\n");
                    Console.WriteLine("Redirecting...");

                    Thread.Sleep(3000);
                    ShowMainOptions();

                    break;
            }
        }
    }
}



//Registry Class
//isRegistred?
//make add to registry
//make remove from registry
