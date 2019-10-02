using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
	class Program
	{
		static void Main(string[] args)
		{
			//ThreeSumBruteForce.verifyThreesumBruteForce();
			//ThreeSumBruteForce.RunFullThreeSum("TrueTest1.txt");
			//ThreeSumBruteForce.RunFullThreeSum("TrueTest2.txt");
			ThreeSumBruteForce.RunFullThreeSum("Test.txt");

			//ThreeSumFast.verifyThreesumFast();
			//ThreeSumFast.RunFullThreeSum("TrueTestFast1.txt");
			//ThreeSumFast.RunFullThreeSum("TrueTestFast2.txt");

			//ThreeSumFastest.verifyThreesumFastest();
			//ThreeSumFastest.RunFullThreeSum("TrueTestFastest1.txt");
			//ThreeSumFastest.RunFullThreeSum("TrueTestFastest2.txt");
			ThreeSumFastest.RunFullThreeSum("Test.txt");
			
		}
	}
}
