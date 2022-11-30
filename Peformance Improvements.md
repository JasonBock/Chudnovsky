When I've run this, I've noticed that it's really, **really** slow, especially when you compare it to the Python version that's included in this repository (it comes from [here](https://python.algorithms-library.com/maths/chudnovsky_algorithm)). Getting 10,000 digits of PI from the Python code takes around 3 seconds. Doing it from my .NET code takes minutes.

Why?

Maybe it's the way I've coded the algorithm. But it's also that support for arbitray precision arithmetic in .NET is anemic. There's `BigInteger`, but...that's it. I needed a `BigRational` number, which I finally landed on one from `MathNet.Numerics.FSharp`. 

After running the code through the debugger, the hot spots appear to be:

GetSquareRoot() - it starts out OK, but as the iteration gets around 15 or higher, it starts taking longer.
GetFractionPart() - this is the big problem. It take seconds for a call to complete.