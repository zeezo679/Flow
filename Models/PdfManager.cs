using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Models
{
    class PdfManager : Ihandler
    {
        static List<string> pdfs = new List<string>();
        public static void AddPDF(string pdfPath, string pdfFile)
        {
            try
            {
                File.AppendAllText(pdfFile, pdfPath + "\n");
            }
            catch
            {
                Console.WriteLine("Error occured!");
            }
        }
        public static void LoadPDFS(string pdfFile)
        {
            if (File.Exists(pdfFile))
            {
                try
                {
                    if (File.Exists(pdfFile))
                    {
                        pdfs = new List<string>(File.ReadAllLines(pdfFile));
                    }
                    Process.Start(new ProcessStartInfo("msedge.exe", pdfs) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Pdf path is invalid.");
                    Console.WriteLine("Redirecting...");
                    Thread.Sleep(3000);
                    ConsoleMenu.ShowMainOptions();
                }
            }
        }


        public void HandleAdd(string pdfFile)
        {
            Console.Clear();
            Console.WriteLine("=========== ADD PDF ===========\n");
            char choice;
            do
            {
                Console.Write("\nEnter the path for you pdf -> ");
                string pdfPath = Console.ReadLine();

                int validationResult = Validation.ValidateURL(pdfPath);

                if (validationResult == 0)
                {
                    Console.WriteLine("Oops! Something is wrong with the way you typed the Path, please be careful and enter the URL again\n");
                    ConsoleMenu.ShowPDFs();
                }

                //Adding pdf to the text file
                PdfManager.AddPDF(pdfPath, pdfFile);
                Console.WriteLine("PDF Path added successfully!\n");


                Console.Write("Press any key to add more Paths or [Q] to go back to menu -> \n");

                choice = Console.ReadKey(true).KeyChar;

                if (choice == 'q')
                {
                    ConsoleMenu.ShowMainOptions();
                    break;
                }
            } while (char.ToLower(choice) != 'q');
        }
        public void HandleRemove(string pdfFIle)
        {
            Console.Clear();
            Console.WriteLine("=========== REMOVE PDF ===========\n");

            List<string> lines = new List<string>(File.ReadLines(pdfFIle));
            Dictionary<int, string> pdfs = new Dictionary<int, string>();

            if(lines.Count() == 0)
            {
                Console.WriteLine("Woah, the list is empty! you cannot delete an empty list.");
                Console.WriteLine("\nRedirecting...");
                Thread.Sleep(2500);
                ConsoleMenu.ShowMainOptions();
            }

            Console.WriteLine("Here is the list of your pdfs, select one of them to delete\n");

            for (int i = 0; i < lines.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. {lines[i]}\n");
                pdfs.Add(i + 1, lines[i]);
            }

            Console.WriteLine();
            int deleteChoice;
            deleteChoice = Convert.ToInt32(Console.ReadLine());

            if(deleteChoice > lines.Count())
            {
                Console.WriteLine($"you have only {lines.Count()} files listed, and your selection is out of range, going back...");
                Thread.Sleep(3000);
                Console.Clear();
                ConsoleMenu.ShowPDFs();
            }

            lines.Remove(pdfs[deleteChoice]);
            pdfs.Remove(deleteChoice);

            Console.Clear();

            Console.WriteLine("Here is your updated list: \n");
            int c = 1;
            foreach (var pdf in pdfs)
            {
                Console.WriteLine($"{c}. {pdf.Value}\n");
                c++;
            }

            try
            {
                File.WriteAllLines(pdfFIle, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine("File is not found\n");
            }
            finally
            {

                Console.WriteLine("Redirecting...\n");
                Thread.Sleep(2000);
                Console.Clear();
                ConsoleMenu.ShowPDFs();
            }
        }

        public void HandleShowList(string pdfFile)
        {

            Console.Clear();
            Console.WriteLine("=========== YOUR LIST ===========\n");

            try
            {
                List<string> pdfs = new List<string>(File.ReadLines(pdfFile));

                int counter = 0;
                foreach (var pdf in pdfs)
                {
                    Console.WriteLine($"{++counter}. {pdf}\n");
                }

                Console.WriteLine("\npress any key to go back");
                Console.ReadKey();
                Console.Clear();
                ConsoleMenu.ShowPDFs();
            }
            catch
            {
                Console.WriteLine("File does not exist");
                Thread.Sleep(2000);
                ConsoleMenu.ShowPDFs();
            }
        }
    }
}



//improve the structure of the program
//we need a go back option in the select browser
//we need a go back option in the website add
//handle exceptions in the delete pdf and add a back option
//we need to add option to add the program to registry to open on startup