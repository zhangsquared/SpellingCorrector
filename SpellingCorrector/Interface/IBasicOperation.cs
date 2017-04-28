using System;
using System.Collections.Generic;
using System.Text;
using SpellingCorrector.CandidateModel;

namespace SpellingCorrector.Interface
{
	public static class OperationFactory
	{
		public static IBasicOperation Generate() => new BasicOperation();
	}

	public interface IBasicOperation
    {
		IEnumerable<string> Delete(string orig);
		IEnumerable<string> Transpose(string orig);
		IEnumerable<string> Replace(string orig);
		IEnumerable<string> Insert(string orig);
	}
}
