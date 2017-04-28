using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SpellingCorrector.Interface;

namespace SpellingCorrector.LanguageModel
{
	internal class SherlockHolmesBook : ILanguageModel
	{
		public LanguageModelEnum LanguageEnum => LanguageModelEnum.SherlockHolmes;

		private static Regex _wordRegex = new Regex("[a-z]+", RegexOptions.Compiled);
		private IDictionary<string, int> words;
		private int totalWordCount;

		// http://norvig.com/big.txt
		public SherlockHolmesBook()
		{
			Init();
		}

		private void Init()
		{
			words = new Dictionary<string, int>();
			totalWordCount = 0;

			string fileContent = File.ReadAllText(@"C:\Users\zzhang\Downloads\big.txt");
			string[] allWords = fileContent.Split(new [] { "\n", " " }, StringSplitOptions.RemoveEmptyEntries);
			foreach(string word in allWords)
			{
				string trimmedWord = word.Trim().ToLower();
				if (_wordRegex.IsMatch(trimmedWord))
				{
					if (words.ContainsKey(trimmedWord)) words[trimmedWord]++;
					else words[trimmedWord] = 1;

					totalWordCount++;
				}
			}
		}

		public bool Validate(string input) => words.ContainsKey(input.ToLower());

		public double Probability(string input)
		{
			if(words == null || !words.Any() || totalWordCount == 0)
			{
				throw new InvalidOperationException("Data is not trained.");
			}

			return words.ContainsKey(input.ToLower()) ? (double)words[input.ToLower()] / totalWordCount : 0;
		}

		public IDictionary<string, int> WordFreq => words;

		public int TotalWordCount => totalWordCount;
	}
}
 