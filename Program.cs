using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace CalculateMaxSumOfNumbers
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var result = GetInput()
                .ConvertInputToArray()
                .DeletePrimeNumbers()
                .TravelTreeNodes();

            Console.WriteLine("Maximum Sum Of The Numbers: : " + result);
            Console.ReadLine();
        }
        private static string GetInput()
        {
            const string input = @" 215
                                    193 124
                                    117 237 442
                                    218 935 347 235
                                    320 804 522 417 345
                                    229 601 723 835 133 124
                                    248 202 277 433 207 263 257
                                    359 464 504 528 516 716 871 182
                                    461 441 426 656 863 560 380 171 923
                                    381 348 573 533 447 632 387 176 975 449
                                    223 711 445 645 245 543 931 532 937 541 444
                                    330 131 333 928 377 733 017 778 839 168 197 197
                                    131 171 522 137 217 224 291 413 528 520 227 229 928
                                    223 626 034 683 839 053 627 310 713 999 629 817 410 121
                                    924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";
            /*
              const string input = @"  1
                                      8 4
                                     2 6 9
                                    8 5 9 3";
            */
            return input;
        }

        private static int TravelTreeNodes(this int[,] matrix)
        {
            int length = matrix.GetLength(0);

            int res = -1;
            for (int i = 0; i < length - 2; i++)
                res = Math.Max(res, matrix[0, i]);

            for (int i = 1; i < length; i++)
            {
                res = -1;
                for (int j = 0; j < length; j++)
                {
                    if (j == 0 && matrix[i, j] != -1)
                    {
                        if (matrix[i - 1, j] != -1)
                            matrix[i, j] += matrix[i - 1, j];
                        else
                            matrix[i, j] = -1;
                    }
                    else if (j > 0 && j < length - 1 && matrix[i, j] != -1)
                    {
                        int tmp = calculateNodeValue(matrix[i - 1, j],
                                   matrix[i - 1, j - 1]);
                        if (tmp == -1)
                        {
                            matrix[i, j] = -1;
                        }
                        else
                            matrix[i, j] += tmp;
                    }

                    else if (j > 0 && matrix[i, j] != -1)
                    {
                        int tmp = calculateNodeValue(matrix[i - 1, j],
                                         matrix[i - 1, j - 1]);
                        if (tmp == -1)
                        {
                            matrix[i, j] = -1;
                        }
                        else
                            matrix[i, j] += tmp;
                    }
                    else if (j != 0 && j < length - 1 && matrix[i, j] != -1)
                    {
                        int tmp = calculateNodeValue(matrix[i - 1, j],
                                     matrix[i - 1, j - 1]);
                        if (tmp == -1)
                        {
                            matrix[i, j] = -1;
                        }
                        else
                            matrix[i, j] += tmp;
                    }
                    res = Math.Max(matrix[i, j], res);
                }
            }
            return res;
        }

        private static int[,] ConvertInputToArray(this string input)
        {
            string[] array = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int[,] matrix = new int[array.Length, array.Length + 1];

            for (var row = 0; row < array.Length; row++)
            {
                int[] digitsInRow = Regex.Matches(array[row], "[0-9]+")
                    .Cast<Match>()
                    .Select(m => int.Parse(m.Value)).ToArray();

                for (var column = 0; column < digitsInRow.Length; column++)
                {
                    matrix[row, column] = digitsInRow[column];
                }
            }
            return matrix;
        }

        private static int calculateNodeValue(int input1, int input2)
        {
            if (input1 == -1 && input2 == -1 || input1 == 0 && input2 == 0)
                return -1;
            else
                return Math.Max(input1, input2);
        }

        private static bool IsPrime(this int number)
        {
            if ((number & 1) == 0)
            {
                if (number == 2)
                {
                    return true;
                }
                return false;
            }
            for (var i = 3; (i * i) <= number; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return number != 1;
        }

        private static int[,] DeletePrimeNumbers(this int[,] matrix)
        {
            int length = matrix.GetLength(0);
            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        continue;
                    }
                    else if (IsPrime(matrix[i, j]))
                    {
                        matrix[i, j] = -1;
                    }
                }
            }
            return matrix;
        }
    }
}