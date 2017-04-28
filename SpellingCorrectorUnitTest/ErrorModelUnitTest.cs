using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellingCorrector.Interface;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace SpellingCorrectorUnitTest
{
	[TestClass]
	public class ErrorModelUnitTest
	{
		private IErrorModel errorModel;
		public ErrorModelUnitTest()
		{
			ILanguageModel lan = LanguageFactory.Generate(LanguageModelEnum.SherlockHolmes);
			ICandidateModel can1 = CandidateFactory.Generate(1);
			can1.SetLanguageModel(lan);
			ICandidateModel can2 = CandidateFactory.Generate(2);
			can2.SetLanguageModel(lan);

			errorModel = ErrorModelFactory.Generate(lan, new[] { can1, can2 });
		}

		[TestMethod]
		public void WordsCorrectionTest()
		{
			Assert.IsTrue(errorModel.Correction("speling").Equals("spelling")); // insert
			Assert.IsTrue(errorModel.Correction("korrectud").Equals("corrected")); // replace 2
			Assert.IsTrue(errorModel.Correction("bycycle").Equals("bicycle")); // replace
			Assert.IsTrue(errorModel.Correction("inconvient").Equals("inconvenient")); // insert 2
			Assert.IsTrue(errorModel.Correction("arrainged").Equals("arranged")); // delete
			Assert.IsTrue(errorModel.Correction("peotry").Equals("poetry")); // transpose
			Assert.IsTrue(errorModel.Correction("peotryy").Equals("poetry")); // transpose + delete
			Assert.IsTrue(errorModel.Correction("word").Equals("word")); // known
			Assert.IsTrue(errorModel.Correction("quintessential").Equals("quintessential")); // unknown
		}

		[TestMethod]
		public void FileCorrectionTest()
		{
			int right = 0;
			int wrong = 0;
			Stopwatch watch = new Stopwatch();
			watch.Start();
			string fileContent = File.ReadAllText(@"C:\Users\zzhang\Downloads\spell-testset2.txt");
			string[] lines = fileContent.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
			foreach(string line in lines)
			{
				string[] comps = line.Split(':');
				string correct = comps[0];
				foreach(string incorrect in comps[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
				{
					string corrected = errorModel.Correction(incorrect);
					if (correct == corrected) right++;
					else wrong++;
				}
			}
			watch.Stop();

			int totalCount = right + wrong;
			TimeSpan span = watch.Elapsed;

			double precisionRate = (double)right / totalCount * 100;
			double Speed = totalCount / span.TotalMilliseconds * 1000;

		}



	}
}
