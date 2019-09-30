using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
	class ThreeSumBruteForce
	{
		static int MAXVALUE = 50000;
		static int MINVALUE = -50000;
		static int MININPUT = 1;
		static int MAXINPUT = Convert.ToInt32(Math.Pow(2, 8));
		static int numberOfTrials = 10000;

		static string resultsFolderPath = "C:\\Users\\Adria\\School Stuff\\CSC482\\Lab3";

		public static int count(int[] a)
		{
			int N = a.Length;
			int cnt = 0;
			for (int i = 0; i < N; i++)
			{
				for (int j = i + 1; j < N; j++)
				{
					for (int k = j + 1; k < N; k++)
					{
						if (a[i] + a[j] + a[k] == 0)
						{
							cnt++;
						}
					}
				}
			}
			return cnt;
		}

		public static void verifyThreesumBruteForce()
		{
			int[] testList1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; //Should return 0
			int[] testList2 = { -2, -1, 3, 5, 10, -15 }; // Should return 2
			int[] testList3 = { 1, 2, -3, 3, 6, -9, 13, 26, -39 }; // Should return 3

			Console.WriteLine("Testing list 1...");
			Console.WriteLine(count(testList1));
			Console.WriteLine("Testing list 2...");
			Console.WriteLine(count(testList2));
			Console.WriteLine("Testing list 3...");
			Console.WriteLine(count(testList3));
		}

		public static int[] CreateRandomListOfInts(int size)
		{
			Random random = new Random();
			int[] newList = new int[size];
			for (int i = 0; i < size; i++)
			{
				newList[i] = random.Next(MINVALUE, MAXVALUE);
			}
			return newList;
		}

		public static void RunFullThreeSum(string resultFile)
		{
			Stopwatch stopwatch = new Stopwatch();


			Console.WriteLine("Input Size\t\tAvg Time (ns)");

			for (int inputSize = MININPUT; inputSize <= MAXINPUT; inputSize *= 2)
			{
				double nanoSecs = 0;

				System.GC.Collect();

				for (long trial = 0; trial < numberOfTrials; trial++)
				{
					int[] testList = CreateRandomListOfInts(inputSize);
					stopwatch.Restart();
					count(testList);
					nanoSecs += stopwatch.Elapsed.TotalMilliseconds * 1000000;

				}
				double averageTrialTime = nanoSecs / numberOfTrials;
				Console.WriteLine("{0,-20} {1,10:N2}", inputSize, averageTrialTime);

				using (StreamWriter outputFile = new StreamWriter(Path.Combine(resultsFolderPath, resultFile), true))
				{
					outputFile.WriteLine("{0,-20}  {1,10:N2}", inputSize, averageTrialTime);
				}
			}
		}
	}
}
