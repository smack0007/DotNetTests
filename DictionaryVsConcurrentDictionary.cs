using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace DictionaryVsConcurrentDictionary
{
	class Program
	{
		public static void Main(string[] args)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			ConcurrentDictionary<string, int> concurrentDictionary = new ConcurrentDictionary<string, int>();

			for (int i = 0; i < 1000; i++)
			{
				dictionary.Add(i.ToString(), i);
				concurrentDictionary.TryAdd(i.ToString(), i);
			}

			long memoryBefore = GC.GetTotalMemory(false);
			Stopwatch stopwatch = Stopwatch.StartNew();

			for (int i = 0; i < 10000; i++)
			{
				int sum = 0;

				foreach (KeyValuePair<string, int> pair in dictionary)
				{
					sum += pair.Value;
				}
            }

			stopwatch.Stop();
			long memoryAfter = GC.GetTotalMemory(false);

			memoryBefore = GC.GetTotalMemory(false);
			Console.WriteLine("Dictionary: {0} {1}", stopwatch.ElapsedMilliseconds, memoryAfter - memoryBefore);

			stopwatch = Stopwatch.StartNew();

			for (int i = 0; i < 10000; i++)
			{
				int sum = 0;

				foreach (KeyValuePair<string, int> pair in concurrentDictionary)
				{
					sum += pair.Value;
				}
			}

			stopwatch.Stop();
			memoryAfter = GC.GetTotalMemory(false);

			Console.WriteLine("ConcurrentDictionary: {0} {1}", stopwatch.ElapsedMilliseconds, memoryAfter - memoryBefore);

			Console.ReadKey();
		}
	}
}
