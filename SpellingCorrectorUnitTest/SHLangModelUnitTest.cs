using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellingCorrector.Interface;

namespace SpellingCorrectorUnitTests
{
	[TestClass]
	public class SHLangModelUnitTest
	{
		private ILanguageModel lan;
		private ICandidateModel can1;
		private ICandidateModel can2;

		public SHLangModelUnitTest()
		{
			lan = LanguageFactory.Generate(LanguageModelEnum.SherlockHolmes);

			can1 = CandidateFactory.Generate(1);
			can1.SetLanguageModel(lan);
			can2 = CandidateFactory.Generate(2);
			can2.SetLanguageModel(lan);
		}

		[TestMethod]
		public void LoadTest()
		{
			Assert.IsTrue(lan.TotalWordCount > 0);
			Assert.IsTrue(lan.Probability("THE") <= 1 && lan.Probability("The") >= 0);
			Assert.IsTrue(lan.Probability("tyuie") == 0);
			Assert.IsFalse(lan.Validate("tyuie"));
			Assert.IsTrue(lan.Validate("network"));
		}

		[TestMethod]
		public void InjectLaugugeModelIntoCandidateModelTest()
		{
			const string test = "somthing";

			IEnumerable<string> list1 = can1.Edit(test);
			IEnumerable<string> list2 = can2.Edit(test);

			Assert.IsTrue(list1.Contains("something"));
			Assert.IsTrue(list1.Contains("soothing"));
			Assert.IsFalse(list1.Contains("smoothing"));
			Assert.IsTrue(list1.Count() == 2);

			Assert.IsTrue(list2.Contains("loathing"));
			Assert.IsTrue(list2.Contains("nothing"));
			Assert.IsTrue(list2.Contains("scathing"));
			Assert.IsTrue(list2.Contains("seething"));
			Assert.IsTrue(list2.Contains("smoothing"));
			Assert.IsTrue(list2.Contains("something"));
			Assert.IsTrue(list2.Contains("soothing"));
			Assert.IsTrue(list2.Contains("sorting"));

			Assert.IsTrue(list2.Count() == 8);
		}

		[TestMethod]
		public void IsDistance2AlwaysContainsDistance1()
		{
			string[] tests = { "somthing", "cheeze", "favorit" };

			foreach(string test in tests)
			{
				IEnumerable<string> list1 = can1.Edit(test);
				IEnumerable<string> list2 = can2.Edit(test);
				IEnumerable<string> exp = list1.Except(list2);
				Assert.IsTrue(!exp.Any());
			}
		}

	}
}
