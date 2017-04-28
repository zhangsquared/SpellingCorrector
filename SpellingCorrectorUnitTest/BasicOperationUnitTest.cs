using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellingCorrector.CandidateModel;
using SpellingCorrector.Interface;

namespace SpellingCorrectorUnitTests
{
	[TestClass]
	public class BasicOperationUnitTest
	{
		private readonly IBasicOperation candidateMode = OperationFactory.Generate();
		private const string test = "test";

		[TestMethod]
		public void TestDelete()
		{
			IEnumerable<string> deleted = candidateMode.Delete(test);
			Assert.IsTrue(deleted.Count() == test.Length);
			Assert.IsTrue(deleted.All(x => x.Length == test.Length - 1));
			Assert.IsTrue(deleted.Contains("est"));
			Assert.IsTrue(deleted.Contains("tst"));
			Assert.IsTrue(deleted.Contains("tet"));
			Assert.IsTrue(deleted.Contains("tes"));
		}

		[TestMethod]
		public void TestReplace()
		{
			IEnumerable<string> replaced = candidateMode.Replace(test);
			Assert.IsTrue(replaced.Count() == test.Length * 26);
			Assert.IsTrue(replaced.All(x => x.Length == test.Length));
			Assert.IsTrue(replaced.Contains("ttst"));
			Assert.IsTrue(replaced.Contains("tesz"));
			Assert.IsTrue(replaced.Contains("eest"));
			Assert.IsFalse(replaced.Contains("tes"));
		}

		[TestMethod]
		public void TestTranspose()
		{
			IEnumerable<string> transposed = candidateMode.Transpose(test);
			Assert.IsTrue(transposed.Count() == 3 );
			Assert.IsTrue(transposed.All(x => x.Length == test.Length));
			Assert.IsTrue(transposed.Contains("etst"));
			Assert.IsTrue(transposed.Contains("tset"));
			Assert.IsTrue(transposed.Contains("tets"));
		}

		[TestMethod]
		public void TestInsert()
		{
			IEnumerable<string> inserted = candidateMode.Insert(test);
			Assert.IsTrue(inserted.Count() == 26 * (test.Length + 1));
			Assert.IsTrue(inserted.All(x => x.Length == test.Length + 1));
			Assert.IsTrue(inserted.Contains("atest"));
			Assert.IsTrue(inserted.Contains("ttest"));
			Assert.IsTrue(inserted.Contains("tenst"));
			Assert.IsTrue(inserted.Contains("testq"));
			Assert.IsFalse(inserted.Contains("tsnet"));
		}



	}
}
