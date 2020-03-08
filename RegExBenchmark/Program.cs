using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RegExBenchmark
{
	class Program
	{
		private static List<string> objectNames = new List<string>() { "Detector", "SG", "Output", "Plan", "Stage", "Sequence", "SignalFolge", "FireBrigade", "Extension" };
		static void Main(string[] args)
		{
			var sw = new Stopwatch();
			var benchmarkData = GenerateStrings();

			var replacer = new ScalaObjectIdentifierReplacer(new ObjectProvider());
			var finalCollection = new List<string>();
			sw.Start();
			foreach (var item in benchmarkData)
			{
				finalCollection.Add(replacer.Replace(item));
			}

			sw.Stop();
			Console.WriteLine(sw.Elapsed);
			Console.ReadKey();
		}

		private static List<string> GenerateStrings()
		{
			var result = new List<string>();
			var random = new Random(DateTime.Now.Millisecond);
			for (int i = 0; i < 10000; i++)
			{
				var randomRepetitionCount = random.Next(1, 20);
				var sb = StringBuilderCache.Acquire();
				for (int j = 0; j < randomRepetitionCount; j++)
				{
					sb.Append(" ");
					sb.Append(RandomString(random.Next(1, 50)));
					sb.Append(GenerateIdentifier());
				}

				result.Add(StringBuilderCache.GetStringAndRelease(sb));
			}

			return result;
		}

		private static string GenerateIdentifier()
		{
			var rand = new Random(DateTime.Now.Millisecond);
			var objIndex = rand.Next(0, objectNames.Count - 1);
			var objName = objectNames[objIndex];
			return $"{{{objName}:{Guid.NewGuid().ToString()}}}";
		}

		private static Random random = new Random();
		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}

}
