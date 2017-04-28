using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpellingCorrector.Interface;

namespace SpellingCorrector.ErrorModel
{
	internal class ErrorModel : IErrorModel
	{
		private IEnumerable<ICandidateModel> candidateModels;
		private ILanguageModel language;

		public ErrorModel(ILanguageModel lang, IEnumerable<ICandidateModel> candidates)
		{
			language = lang;
			candidateModels = candidates;
		}

		public string Correction(string orig)
		{
			orig = orig.ToLower();
			if (language.Validate(orig)) return orig;

			foreach(ICandidateModel candidate in candidateModels)
			{
				IEnumerable<string> potentials = candidate.Edit(orig);
				if (potentials.Any()) return potentials.OrderByDescending(x => language.Probability(x)).First();
			}

			return orig;
		}

	}
}
