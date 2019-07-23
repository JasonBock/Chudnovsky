using System;
using System.Numerics;

namespace Chudnovsky
{
	public static class CalculatePiBCLWay
	{
		public static void Calculate()
		{
			var shifterValue = "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
			var shifterFormat = shifterValue.Replace("1", string.Empty);
			var shifter = new BigRational(BigInteger.Parse(shifterValue));

			var pi = CalculatePi(new BigRational(400m));

			Console.Out.Write($"{pi.GetWholePart()}.");

			for (var i = 0; i < 40; i++)
			{
				pi = pi.GetFractionPart() * shifter;
				Console.Out.Write(pi.GetWholePart().ToString(shifterFormat));
			}
		}

		// https://en.wikipedia.org/wiki/Chudnovsky_algorithm
		private static BigRational CalculatePi(BigRational maxK)
		{
			var K = new BigRational(6m);
			var M = new BigRational(1m);
			var L = new BigRational(13591409m);
			var X = new BigRational(1m);
			var S = new BigRational(13591409m);

			for (BigRational k = 1; k < (maxK + 1); k++)
			{
				M = M * (BigRational.Pow(K, 3) - (16 * K)) / BigRational.Pow(k, 3);
				L += 545140134;
				X *= -262537412640768000;
				S += M * L / X;
				K += 12;
			}

			return new BigRational(426880m) * GetSquareRoot(new BigRational(10005m)) / S;
		}

		private static BigRational GetSquareRoot(BigRational b)
		{
			var s = b;

			for (var i = 0; i < 18; i++)
			{
				b = (b + (s / b)) / 2;
			}

			return b;
		}
	}
}