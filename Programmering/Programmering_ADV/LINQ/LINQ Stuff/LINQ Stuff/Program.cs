using System.Xml;

namespace LINQ_Stuff {

    public class Book { 
    
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int PublishedYear { get; set; }
        public double Price { get; set; }

    }
    internal class Program {
        static void Main() {

            bool programRunning = true;
            int upperRange = 10;
            int count = 10;

            int[] nums = GenerateRandomNumbers(count, 1, upperRange);

            List<string> randomNamn = new List<string> { "Mikael", "Bert", "Zarah", "Tanya", "Ahmed", "Berit" };  

            List<Book> library = new List<Book> {
            new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", PublishedYear = 1925, Price = 15.99 },
            new Book { Title = "1984", Author = "George Orwell", PublishedYear = 1949, Price = 19.99 },
            new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", PublishedYear = 1960, Price = 7.99 },
            new Book { Title = "Moby Dick", Author = "Herman Melville", PublishedYear = 1851, Price = 12.99 }
        };

            List<string> publishers = new List<string> { "Penguin", "HarperCollings", "Random House" };

            var modernBooks = from book in library
                              where book.PublishedYear > 1900
                              select book;

            var bookTitles = from book in library
                             select book.Title;

            var sortedBooks = from book in library
                              orderby book.PublishedYear
                              select book;

            var avaragePrice = (from book in library
                                select book.Price).Average();

            var bookWithPublisher = from book in library
                                    from publisher in publishers
                                    select $"{book.Title} by {publisher}";

            while (programRunning) {
                Console.Write("[1] Exit.\n" +
                    "[2] Print even Numbers from an Array using Linq\n" +
                    "[3] Filter a list of names by if they contain the let 'a'\n" +
                    "[4] Sort numbers in an Array by descending order\n" +
                    "[5] Change the variables in random number list\n" +
                    "[6] Update the random number list\n" +
                    "\nUser Input: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice) {

                    case 1:
                        Console.WriteLine("Exiting. Goodbye\n");
                        programRunning = false;
                        break;

                    case 2: 
                        LINQEvenNumbers(nums);
                        break;

                    case 3:
                        LINQFilterListByLet(randomNamn);
                        break;

                    case 4:
                        LINQSortNumbersByDescendingOrder(nums);
                        break;

                    case 5:

                        Console.Write("Enter a num for the upper range of numbers generated. Default is 10\n"
                            + "Input: ");
                        upperRange = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter a num for the amount of numbers in the array. Default is 10\n"
                            + "Input: ");
                        count = Convert.ToInt32(Console.ReadLine());
                        UpdateRandomNumbers(nums, count, 1, upperRange);
                        Console.WriteLine("List is now updated, the current array is: ");
                        foreach (var arrayContent in nums) {

                            Console.Write(arrayContent + " ");

                        }
                        //Empty for formating
                        Console.WriteLine("\n");
                        break;

                    case 6:
                        UpdateRandomNumbers(nums, count, 1, upperRange);
                        Console.WriteLine("List updated\n");
                        break;
                }
               
            }

        }

        static void LINQEvenNumbers(int[] nums) {

            var evenNumbers = nums.Where(n => n % 2 == 0);

            foreach (var number in evenNumbers) {

                Console.WriteLine(number);

            }

            //Empty write line for formating
            Console.WriteLine();
        }

        static void LINQFilterListByLet(List<string> randomNamn) {

            var namnMedA = randomNamn.Where(namn => namn.Contains("a", StringComparison.OrdinalIgnoreCase));

            foreach (var namn in namnMedA) {

                Console.WriteLine(namn);

            }

            //Empty write line for formating
            Console.WriteLine();
        }

        static void LINQSortNumbersByDescendingOrder(int[] nums) {

            var sortedNumbers = nums.OrderByDescending(num => num);

            foreach (var num in sortedNumbers) {
                Console.WriteLine(num);
            }

        }
        static int[] GenerateRandomNumbers(int count, int minValue, int maxValue) {
            Random random = new Random();
            return Enumerable.Range(1, count).Select(_ => random.Next(minValue, maxValue + 1)).ToArray();
        }
        static void UpdateRandomNumbers(int[] numbers,int count, int minValue, int maxValue) {
            Random random = new Random();
            for (int i = 0; i < numbers.Length; i++) {
                numbers[i] = random.Next(minValue, maxValue + 1);
            }
        }
    }
}
