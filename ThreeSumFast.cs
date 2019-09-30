using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
	class ThreeSumFast
	{
		static int MAXVALUE = 50000;
		static int MINVALUE = -50000;
		static int MININPUT = 1;
		static int MAXINPUT = Convert.ToInt32(Math.Pow(2, 8));
		static int numberOfTrials = 10000;

		static string resultsFolderPath = "C:\\Users\\Adria\\School Stuff\\CSC482\\Lab3";

		public static int countFast(long[] a)
		{
			long[] testList = MergeSort(a);
			int N = testList.Length;
			int cnt = 0;
			for (int i = 0; i < N; i++)
			{
				for (int j = i + 1; j < N; j++)
				{
					if((BinarySearch(testList, -(testList[i]+testList[j])) > j))
					{
						cnt++;
					}
					
				}
			}
			return cnt;
		}

		public static void verifyThreesumFast()
		{
			long[] testList1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; //Should return 0
			long[] testList2 = { -2, -1, 3, 5, 10, -15 }; // Should return 2
			long[] testList3 = { 1, 2, -3, 3, 6, -9, 13, 26, -39 }; // Should return 3

			Console.WriteLine("Testing list 1...");
			Console.WriteLine(countFast(testList1));
			Console.WriteLine("Testing list 2...");
			Console.WriteLine(countFast(testList2));
			Console.WriteLine("Testing list 3...");
			Console.WriteLine(countFast(testList3));
		}

		public static long[] CreateRandomListOfInts(int size)
		{
			Random random = new Random();
			long[] newList = new long[size];
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
					long[] testList = CreateRandomListOfInts(inputSize);
					stopwatch.Restart();
					countFast(testList);
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

		public static long[] MergeSort(long[] list)
		{
			long[] left;
			long[] right;
			long[] result = new long[list.Length];

			if (list.Length <= 1)
			{
				return list;
			}

			long midPoint = list.Length / 2;
			left = new long[midPoint];

			if (list.Length % 2 == 0)
			{
				right = new long[midPoint];
			}
			else
			{
				right = new long[midPoint + 1];
			}

			for (int i = 0; i < midPoint; i++)
			{
				left[i] = list[i];
			}

			int x = 0;

			for (long i = midPoint; i < list.Length; i++)
			{
				right[x] = list[i];
				x++;
			}

			left = MergeSort(left);
			right = MergeSort(right);
			result = Merge(left, right);
			return result;

		}

		public static long[] Merge(long[] left, long[] right)
		{
			long resultLength = right.Length + left.Length;
			long[] result = new long[resultLength];

			int indexLeft = 0, indexRight = 0, indexResult = 0;

			while (indexLeft < left.Length || indexRight < right.Length)
			{
				if (indexLeft < left.Length && indexRight < right.Length)
				{
					if (left[indexLeft] <= right[indexRight])
					{
						result[indexResult] = left[indexLeft];
						indexLeft++;
						indexResult++;
					}
					else
					{
						result[indexResult] = right[indexRight];
						indexRight++;
						indexResult++;
					}
				}
				else if (indexLeft < left.Length)
				{
					result[indexResult] = left[indexLeft];
					indexLeft++;
					indexResult++;
				}
				else if (indexRight < right.Length)
				{
					result[indexResult] = right[indexRight];
					indexRight++;
					indexResult++;
				}
			}
			return result;
		}

		public static long BinarySearch(long[] inputArray, long key)
		{
			int min = 0;
			int max = inputArray.Length - 1;
			while (min <= max)
			{
				int mid = (min + max) / 2;
				if (key == inputArray[mid])
				{
					return ++mid;
				}
				else if (key < inputArray[mid])
				{
					max = mid - 1;
				}
				else
				{
					min = mid + 1;
				}
			}
			return -1;
		}
	}
}
