using System;
using System.Collections.Generic;
using System.Text;
using SpellingCorrector.ErrorModel;

namespace SpellingCorrector.Interface
{
	public static class ErrorModelFactory
	{
		public static IErrorModel Generate(ILanguageModel lang, IEnumerable<ICandidateModel> cans)
			=> new ErrorModel.ErrorModel(lang, cans);
	}

	public interface IErrorModel
	{
		string Correction(string orig);
	}
}
