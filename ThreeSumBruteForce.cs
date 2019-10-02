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
		static int MAXINPUT = Convert.ToInt32(Math.Pow(2, 12));
		static double numberOfTrials = 10;
		//Here is string declared to be the folder path for my project files and where output files will go
		static string resultsFolderPath = "C:\\Users\\Adria\\School Stuff\\CSC482\\Lab3";

		//The 'count' (ThreeSum) algorithm from the book - the 'naive' or 'brute force'
		//approach. Iterates through every possible combination of elements to determine if
		//they sum to 0.
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

		//my method to verify the algorithm, with a few small lists
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

		//this method generates a list of random integers between the MINVALUE and MAXVALUE set above
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

		//my experiment method
		public static void RunFullThreeSum(string resultFile)
		{
			//I'm using the built-in C# Stopwatch class for my tests
			Stopwatch stopwatch = new Stopwatch();

			//some variables to initialize and store the double ratio values
			double previousTime = 0;
			double doubleRatio = 0;
			Console.WriteLine("Input Size\tAvg Time (ns)\tDoubling Ratio");

			//the experiment/test block
			for (int inputSize = MININPUT; inputSize <= MAXINPUT; inputSize += inputSize)
			{
				double nanoSecs = 0;
				
				//here i was forcing garbage collection but it didn't seem to make huge difference
				//System.GC.Collect();

				//My trial loop. A new list is generated, the stopwatch is restarted, and the algorithm runs
				//on the list for each iteration. I set my nanosecond variable to the total elapsed time of the 
				//stopwatch * 1000000 to get nanoseconds.
				for (long trial = 0; trial < numberOfTrials; trial++)
				{
					int[] testList = CreateRandomListOfInts(inputSize);
					stopwatch.Restart();
					count(testList);
					stopwatch.Stop();
					nanoSecs += stopwatch.Elapsed.TotalMilliseconds * 1000000;

				}
				double averageTrialTime = nanoSecs / (double)numberOfTrials;
				
				//block checking for initial previous time, calculates and sets the new double ratio
				//and sets new previous time to the current time
				if (previousTime > 0)
				{
					doubleRatio = averageTrialTime / previousTime;
				}
				previousTime = averageTrialTime;

				//Here we write the output to the terminal.
				Console.WriteLine("{0,-10} {1,16} {2,10:N2}", inputSize, averageTrialTime, doubleRatio);

				//Same as above here but we write to a new file
				using (StreamWriter outputFile = new StreamWriter(Path.Combine(resultsFolderPath, resultFile), true))
				{
					outputFile.WriteLine("{0,-10} {1,16} {2,10:N2}", inputSize, averageTrialTime, doubleRatio);
				}
			}
		}
	}
}
