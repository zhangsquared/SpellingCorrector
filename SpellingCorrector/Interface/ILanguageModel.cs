using System;
using System.Collections.Generic;
using System.Text;
using SpellingCorrector.LanguageModel;

namespace SpellingCorrector.Interface
{
	public enum LanguageModelEnum
	{
		SherlockHolmes
	}

	public static class LanguageFactory
	{
		public static ILanguageModel Generate(LanguageModelEnum languageEnum)
		{
			switch (languageEnum)
			{
				case LanguageModelEnum.SherlockHolmes:
					return new SherlockHolmesBook();
				default:
					return null;
			}
		}
	}

	public interface ILanguageModel
    {
		LanguageModelEnum LanguageEnum { get; }

		int TotalWordCount { get; }
		bool Validate(string input);

		double Probability(string input);
	}
}
