using System;
using System.IO;
using System.Collections.Generic;



const string NAMN_LISTA = @"C:\Users\major\OneDrive\Dokument\CPGIT\CpStuff\Programmering\Programmering_ADV\Programmering_ADV\NamnLista.txt";
string mittNamn = "empty";
bool Menu = true;
int menuVal = 0;
List<string> names = new List<string>();
//var logFile = File.ReadAllLines(NAMN_LISTA);
//var logList = new List<string>(logFile);

while (Menu) {
    Console.Clear();
    Console.WriteLine("[1] Enter name\n[2] Print name\n[3] Add name to a list\n[4] Print list\n[5] Print file contents(test)\n" + "[0] Exit\n\nEnter your choice: ");
    menuVal = Convert.ToInt32(Console.ReadLine());

    switch (menuVal) {
        case 1:
            Console.Clear();
            Console.WriteLine("Enter your name: ");
            mittNamn = Console.ReadLine();

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            break;

        case 2:
            Console.Clear();
            Console.WriteLine("Your name is {0}\n", mittNamn);

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            break;

        case 3:
            Console.Clear();
            Console.WriteLine("Add name to a list: ");
            mittNamn = Console.ReadLine();
            names.Add(mittNamn);

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            break;

        case 4:
            Console.Clear();
            Console.WriteLine("The names in the list are: ");
            for (int i = 0; i < names.Count; i++) {
                Console.WriteLine(names[i]);

            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            break;

        case 5:
            string[] readFile = File.ReadAllLines(NAMN_LISTA);
            List<string> logList = new List<string>(readFile);
            IEnumerable<string> enumerableList = File.ReadAllLines(NAMN_LISTA);
            var tempList = new List<string>(enumerableList);
            Console.WriteLine("THis is line 2 in the list: " + readFile[1]);
            Console.WriteLine("This is line 3 in the list: " + logList[2]);
            Console.WriteLine(tempList[1]);

            foreach (string name in enumerableList) { Console.WriteLine(name); }




            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            break;


        //Exit choice at 0 and spaced out for convience
        case 0:
            Console.WriteLine("Have a good day!\n");
            Menu = false;

            break;

    }
}
