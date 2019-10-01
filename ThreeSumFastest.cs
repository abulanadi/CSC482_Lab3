using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
	class ThreeSumFastest
	{
		static int MAXVALUE = 50000;
		static int MINVALUE = -50000;
		static int MININPUT = 1;
		static int MAXINPUT = Convert.ToInt32(Math.Pow(2, 15));
		static int numberOfTrials = 5;

		static string resultsFolderPath = "C:\\Users\\Adria\\School Stuff\\CSC482\\Lab3";

		//For the fastest method, we utilize a "two-pointer" algorithm
		public static int countFastest(int[] a)
		{
			//have to sort the list first...
			int[] newList = MergeSort(a);
			int N = newList.Length;
			int cnt = 0;
			
			//we have i = 0...
			for (int i = 0; i < N; ++i)
			{
				//this if statement checks to make sure we don't look at a duplicate index  for i
				if (i != 0 && newList[i] == newList[i - 1]) continue;
				//j = i + 1, so points to element after i
				int j = i + 1;
				//k = length of list - 1, so point to last element.
				int k = newList.Length - 1;
				while(j < k)
				{
					if(newList[i] + newList[j] + newList[k] == 0)
					{
						cnt++;
						++j;
						//avoiding duplicate j indexes
						while (j < k && newList[j] == newList[j - 1]) ++j;
					}
					else if(newList[i]+newList[j]+newList[k] < 0)//if sum less than 0, move j to the right
					{
						++j;
					}
					else//otherwise, move k to the left
					{
						--k;
					}
				}

				//I also implemented a hashset version of this that was comparable in speed, but I honestly
				//don't understand how it works as well as
				//well as I do the two-pointer solution (I need more practice with hash maps, etc...)

				/*HashSet<int> newSet = new HashSet<int>();

				for (int j = i + 1; j < N; j++)
				{
					int k = -(a[i] + a[j]);
					if (newSet.Contains(k))
					{
						cnt++;
					}
					else
					{
						newSet.Add(a[j]);
					}
				}*/
			}
			return cnt;
		}

		public static void verifyThreesumFastest()
		{
			int[] testList1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; //Should return 0
			int[] testList2 = { -2, -1, 3, 5, 10, -15 }; // Should return 2
			int[] testList3 = { 1, 2, -3, 3, 6, -9, 13, 26, -39 }; // Should return 3

			Console.WriteLine("Testing list 1...");
			Console.WriteLine(countFastest(testList1));
			Console.WriteLine("Testing list 2...");
			Console.WriteLine(countFastest(testList2));
			Console.WriteLine("Testing list 3...");
			Console.WriteLine(countFastest(testList3));
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

			double previousTime = 0;
			double doubleRatio = 0;
			Console.WriteLine("Input Size\tAvg Time (ns)\tDoubling Ratio");

			for (int inputSize = MININPUT; inputSize <= MAXINPUT; inputSize += inputSize)
			{
				double nanoSecs = 0;

				//System.GC.Collect();

				for (long trial = 0; trial < numberOfTrials; trial++)
				{
					int[] testList = CreateRandomListOfInts(inputSize);
					stopwatch.Restart();
					countFastest(testList);
					stopwatch.Stop();
					nanoSecs += stopwatch.Elapsed.TotalMilliseconds * 1000000;

				}
				double averageTrialTime = nanoSecs / numberOfTrials;

				if (previousTime > 0)
				{
					doubleRatio = averageTrialTime / previousTime;
				}
				previousTime = averageTrialTime;

				Console.WriteLine("{0,-10} {1,16} {2,10:N2}", inputSize, averageTrialTime, doubleRatio);

				using (StreamWriter outputFile = new StreamWriter(Path.Combine(resultsFolderPath, resultFile), true))
				{
					outputFile.WriteLine("{0,-10} {1,16} {2,10:N2}", inputSize, averageTrialTime, doubleRatio);
				}
			}
		}

		public static int[] MergeSort(int[] list)
		{
			int[] left;
			int[] right;
			int[] result = new int[list.Length];

			if (list.Length <= 1)
			{
				return list;
			}

			int midPoint = list.Length / 2;
			left = new int[midPoint];

			if (list.Length % 2 == 0)
			{
				right = new int[midPoint];
			}
			else
			{
				right = new int[midPoint + 1];
			}

			for (int i = 0; i < midPoint; i++)
			{
				left[i] = list[i];
			}

			int x = 0;

			for (int i = midPoint; i < list.Length; i++)
			{
				right[x] = list[i];
				x++;
			}

			left = MergeSort(left);
			right = MergeSort(right);
			result = Merge(left, right);
			return result;

		}

		public static int[] Merge(int[] left, int[] right)
		{
			int resultLength = right.Length + left.Length;
			int[] result = new int[resultLength];

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
	}
}
