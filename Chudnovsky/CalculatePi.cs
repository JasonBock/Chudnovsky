using MathNet.Numerics;
using System;
using SN = System.Numerics;

namespace Chudnovsky
{
	public static class BigRationalExtensions
	{
		public static SN.BigInteger GetWholePart(this BigRational self) => SN.BigInteger.Divide(self.Numerator, self.Denominator);

		public static BigRational GetFractionPart(this BigRational self) => 
			BigRational.FromBigIntFraction(SN.BigInteger.Remainder(self.Numerator, self.Denominator), self.Denominator);
	}

	public static class CalculatePi
	{
		public static void Calculate()
		{
			var shifterValue = "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
			var shifterFormat = shifterValue.Replace("1", string.Empty);
			var shifter = BigRational.FromBigInt(SN.BigInteger.Parse(shifterValue));

			var pi = CalculateValue(BigRational.FromDecimal(400m));

			Console.Out.Write($"{pi.GetWholePart()}.");

			for (var i = 0; i < 40; i++)
			{
				pi = pi.GetFractionPart() * shifter;
				Console.Out.Write(pi.GetWholePart().ToString(shifterFormat));
			}
		}

		// https://en.wikipedia.org/wiki/Chudnovsky_algorithm
		private static BigRational CalculateValue(BigRational maxK)
		{
			var K = BigRational.FromDecimal(6m);
			var M = BigRational.FromDecimal(1m);
			var L = BigRational.FromDecimal(13591409m);
			var X = BigRational.FromDecimal(1m);
			var S = BigRational.FromDecimal(13591409m);

			var sixteen = BigRational.FromInt(16);
			var L_constant = BigRational.FromInt(545140134);
			var X_constant = BigRational.FromDecimal(-262537412640768000M);
			var twelve = BigRational.FromInt(12);

			for (var k = BigRational.One; k < (maxK + BigRational.One); k += BigRational.One)
			{
				M = M * (BigRational.Pow(K, 3) - (K * sixteen)) / BigRational.Pow(k, 3);
				L += L_constant;
				X *= X_constant;
				S += M * L / X;
				K += twelve;
			}

			return BigRational.FromDecimal(426880m) * GetSquareRoot(BigRational.FromDecimal(10005m)) / S;
		}

		private static BigRational GetSquareRoot(BigRational b)
		{
			var two = BigRational.FromInt(2);

			var s = b;

			for (var i = 0; i < 18; i++)
			{
				b = (b + (s / b)) / two;
			}

			return b;
		}
	}
}