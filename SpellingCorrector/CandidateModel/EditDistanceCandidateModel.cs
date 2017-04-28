using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpellingCorrector.Interface;

namespace SpellingCorrector.CandidateModel
{
    public class EditDistanceCandidateModel : ICandidateModel
    {
		private readonly BasicOperation basic = new BasicOperation();
		private readonly int stepNum;
		private const int DEFAULT = 2;
		private ILanguageModel languageModel;

		internal EditDistanceCandidateModel(int distance)
		{
			stepNum = distance;
		}
		internal EditDistanceCandidateModel()
		{
			stepNum = DEFAULT;
		}

		public int EditDistance => stepNum;

		public IEnumerable<string> Edit(string orig) => InternalEdit(orig.ToLower(), stepNum);

		private IEnumerable<string> InternalEdit(string orig, int count)
		{
			IEnumerable<string> current = basic.Delete(orig)
				.Union(basic.Insert(orig))
				.Union(basic.Replace(orig))
				.Union(basic.Transpose(orig))
				.Distinct();

			if (count == 1)
			{
				return HasLanguageModel() ? current.Where(x => languageModel.Validate(x)) : current;
			}
			return current.SelectMany(modified => InternalEdit(modified, stepNum - 1)).Distinct();
		}

		public void SetLanguageModel(ILanguageModel languageMode)
		{
			languageModel = languageMode;
		}
		private bool HasLanguageModel() => languageModel != null;
	}
}
