namespace Max_Sub_Array {

    internal class Program {
        static int simpleMaxSubArraySum(int[] array) {
            int size = array.Length;
            int currMaxNum = int.MinValue;
            int maxEndingNum = 0;
            List<int> savedNums = new List<int>();

            for (int i = 0; i < size; i++) {
                maxEndingNum = maxEndingNum + array[i];
                savedNums.Add(array[i]);

                if (currMaxNum < maxEndingNum) {
                    currMaxNum = maxEndingNum;
                }

                if (maxEndingNum < 0) {
                    maxEndingNum = 0;
                    savedNums.Clear();
                }
            }

            Console.Write("The SubArray is: ");
            foreach (int i in savedNums) {
                Console.Write(i);
                Console.Write(" ");
            }
            Console.WriteLine();

        return currMaxNum;
        }

        static void Main() {
            int[] topArray_1 = { -2, 3, 6, -5, 1, 4 };
            int[] topArray_2 = { 3, 9, -2, -5, -3, 4, 4, 5, -3, -7, 9, 12 };

            Console.WriteLine("Simple SubArray: Maximum contiguous sum is " + simpleMaxSubArraySum(topArray_2));
        }
    }
}
