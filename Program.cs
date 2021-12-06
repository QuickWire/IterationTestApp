using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
/// <summary>
/// Simple string iteration tester, new Span class (struct) works well
/// 
/// Bill Miller, July 2021
/// </summary>
namespace IterationTestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			IterTest();
		}

		internal class TestTime
		{
			public TestTime(string name, long t)
			{
				Name = name;
				T = t;
			}
			public string Name { get; set; }
			public long T { get; set; }
		}

		static void IterTest()
		{
			string source = "/once/upon/a/time/once/upon/a/time/once/upon/a/time/";
			int max_loop = 100000000;

			Console.WriteLine($"Starting test, {max_loop} iterations set");
			Stopwatch sw = new Stopwatch();
			List<TestTime> times = new List<TestTime>();
			bool add_theSlowOnes = true;

			sw.Start();
			for (int x = 0; x < max_loop; x++)
			{
				int count = 0;
				foreach (char c in source)
				{
					if (c == '/')
						count++;
				}
			}
			TestTime t_base = new TestTime("(base) foreach", sw.ElapsedMilliseconds);
			times.Add(t_base);

			sw.Restart();
			for (int x = 0; x < max_loop; x++)
			{
				int count = 0;
				int length = source.Length;
				for (int n = 0; n < length; n++)
				{
					if (source[n] == '/')
						count++;
				}
			}
			times.Add(new TestTime("for [i++]", sw.ElapsedMilliseconds));


			sw.Restart();
			for (int x = 0; x < max_loop; x++)
			{
				int count = 0;
				int length = source.Length;
				for (int n = length - 1; n >= 0; n--)
				{
					if (source[n] == '/')
						count++;
				}
			}
			times.Add(new TestTime("for [i--]", sw.ElapsedMilliseconds));

			sw.Restart();
			for (int x = 0; x < max_loop; x++)
			{
				int count = 0;
				var span = source.AsSpan();
				int length = span.Length;
				for (int n = length - 1; n >= 0; n--)
				{
					if (span[n] == '/')
						count++;
				}
			}
			times.Add(new TestTime("Span [i--]", sw.ElapsedMilliseconds));


			sw.Restart();
			for (int x = 0; x < max_loop; x++)
			{
				int count = 0;
				var span = source.AsSpan();
				int length = span.Length;
				for (int i = 0; i < length; i++)
				{
					if (span[i] == '/')
						count++;
				}
			}
			times.Add(new TestTime("Span [i++]", sw.ElapsedMilliseconds));


			sw.Restart();
			for (int x = 0; x < max_loop; x++)
			{
				int count = 0;
				foreach (var c in source.AsSpan())
				{
					if (c == '/')
						count++;
				}
			}
			times.Add(new TestTime("foreach Span", sw.ElapsedMilliseconds));


			//new had to try RegEx..
			if (add_theSlowOnes)
			{
				sw.Restart();
				for (int x = 0; x < max_loop; x++)
				{
					int count = source.Split('/').Length - 1;
				}
				times.Add(new TestTime("Split", sw.ElapsedMilliseconds));


				sw.Restart();
				for (int x = 0; x < max_loop; x++)
				{
					int count = source.Count(c => c == '/');
				}
				times.Add(new TestTime("Linq", sw.ElapsedMilliseconds));


				sw.Restart();
				for (int x = 0; x < max_loop; x++)
				{
					int count = Regex.Matches(source, @"\/").Count;
				}
				times.Add(new TestTime("Regex", sw.ElapsedMilliseconds));


				// and a compiled RegEx..
				var regex = new Regex(@"\/", RegexOptions.Compiled);
				sw.Restart();
				for (int x = 0; x < max_loop; x++)
				{
					int count = regex.Matches(source).Count;
				}
				times.Add(new TestTime("Regex Compiled", sw.ElapsedMilliseconds));


				sw.Restart();
				for (int x = 0; x < max_loop; x++)
				{
					int count = source.Length - source.Replace("/", "").Length;
				}
				times.Add(new TestTime("Replace", sw.ElapsedMilliseconds));

			}


			Console.WriteLine($"{t_base.Name,14} = { t_base.T,5} ms ");

			Console.WriteLine("fastest to slowest");
			times.Sort((a, b) => a.T.CompareTo(b.T));
			foreach (var t in times)
			{
				Console.WriteLine($"{t.Name,14} = {t.T,5} ms {(1 - (double)t.T / (double)t_base.T) * 100,8:N1}%");
			}
			Console.WriteLine("\ncomplete");
		}



	}
}
