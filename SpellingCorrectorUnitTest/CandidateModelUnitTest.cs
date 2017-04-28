using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellingCorrector.Interface;

namespace SpellingCorrectorUnitTests
{
	[TestClass]
	public class CandidateModelUnitTest
	{
		[TestMethod]
		public void TestOneEditDistance()
		{
			const string test = "at";

			ICandidateModel candidateMode = CandidateFactory.Generate(1);
			IEnumerable<string> edited = candidateMode.Edit(test);
			Assert.IsTrue(edited.Count() <= CountCap(test));

			Assert.IsTrue(edited.All(x => x.Length >= test.Length - 1 && x.Length <= test.Length + 1));

			Assert.IsTrue(edited.Contains("t"));
			Assert.IsTrue(edited.Contains("a"));
			Assert.IsTrue(edited.Contains("ta"));
			Assert.IsTrue(edited.Contains("atz"));
			Assert.IsTrue(edited.Contains("tt"));
			Assert.IsTrue(edited.Contains("aa"));

			Assert.IsFalse(edited.Contains("taz"));
		}

		[TestMethod]
		public void AnotherOneTest()
		{
			const string another = "somthing";

			ICandidateModel candidateMode = CandidateFactory.Generate(1);
			IEnumerable<string> anotherEdited = candidateMode.Edit(another);

			Assert.IsTrue(anotherEdited.Count() == 442);
		}

		[TestMethod]
		public void TestTwoEditDistance()
		{
			const string test = "at";

			ICandidateModel candidateMode = CandidateFactory.Generate(2);
			IEnumerable<string> edited = candidateMode.Edit(test);

			Assert.IsTrue(edited.All(x => x.Length >= test.Length - 2 && x.Length <= test.Length + 2));

			Assert.IsTrue(edited.Contains("t"));
			Assert.IsTrue(edited.Contains("a"));
			Assert.IsTrue(edited.Contains("ta"));
			Assert.IsTrue(edited.Contains("atz"));
			Assert.IsTrue(edited.Contains("tt"));
			Assert.IsTrue(edited.Contains("aa"));

			Assert.IsTrue(edited.Contains("taz"));
			Assert.IsTrue(edited.Contains(""));
			Assert.IsTrue(edited.Contains("tqa"));
		}

		[TestMethod]
		public void AnotherTwoTest()
		{
			const string another = "somthing";

			ICandidateModel candidateMode = CandidateFactory.Generate(2);
			IEnumerable<string> anotherEdited = candidateMode.Edit(another);

			Assert.IsTrue(anotherEdited.Count() == 90902);
		}


		private int Deleted(string str) => str.Length;
		private int Inserted(string str) => 26 * (str.Length + 1);
		private int Replaced(string str) => 26 * str.Length;
		private int Transposed(string str) => str.Length - 1;
		private int CountCap(string str) => Deleted(str) + Inserted(str) + Replaced(str) + Transposed(str);

	}
}
