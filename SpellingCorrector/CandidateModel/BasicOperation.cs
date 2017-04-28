using System;
using System.Collections.Generic;
using System.Text;
using SpellingCorrector.Interface;

namespace SpellingCorrector.CandidateModel
{
	internal class BasicOperation : IBasicOperation
	{
		private const string letters = "abcdefghijklmnopqrstuvwxyz";
		private static char[] AcceptedChar => letters.ToCharArray();

		public IEnumerable<string> Delete(string orig)
		{
			for(int i = 0; i < orig.Length; i ++)
			{
				yield return orig.Remove(i, 1);
			}
		}
		public IEnumerable<string> Transpose(string orig)
		{
			for (int i = 0; i < orig.Length - 1; i++)
			{
				char current = orig[i];
				char next = orig[i + 1];
				yield return orig.Remove(i, 2).Insert(i, next.ToString()).Insert(i + 1, current.ToString());
			}
		}
		public IEnumerable<string> Replace(string orig)
		{
			for (int i = 0; i < orig.Length; i++)
			{
				foreach (char c in AcceptedChar)
				{
					yield return orig.Remove(i, 1).Insert(i, c.ToString());
				}
			}
		}
		public IEnumerable<string> Insert(string orig)
		{
			for (int i = 0; i <= orig.Length; i++)
			{
				foreach(char c in AcceptedChar)
				{
					yield return orig.Insert(i, c.ToString());
				}
			}
		}

	}
}
