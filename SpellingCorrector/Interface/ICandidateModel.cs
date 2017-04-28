using System;
using System.Collections.Generic;
using System.Text;
using SpellingCorrector.CandidateModel;

namespace SpellingCorrector.Interface
{
	public static class CandidateFactory
	{
		public static ICandidateModel Generate(int editDistance) 
			=> new EditDistanceCandidateModel(editDistance);
	}

	public interface ICandidateModel
    {
		int EditDistance { get; }
		IEnumerable<string> Edit(string orig);
		void SetLanguageModel(ILanguageModel languageMode);
	}
}
