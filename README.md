# IterationTestApp
Tests performance of various String iteration methods

Typical run looks like this. Make sure to run tests as Release - totally different outcomes otherwise.
```
Starting test, 10000000 iterations set
(base) foreach =   692 ms
fastest to slowest
  foreach Span =   257 ms   62.9%
    Span [i--] =   291 ms   57.9%
    Span [i++] =   434 ms   37.3%
     for [i++] =   553 ms   20.1%
     for [i--] =   892 ms  -28.9%
       Replace =  1932 ms -179.2%
         Split =  2220 ms -220.8%
          Linq =  3793 ms -448.1%

complete
```

UPDATE: Dec 2021, Visual Studio 2022, .NET 5 & 6

```
.NET 5
Starting test, 100000000 iterations set
(base) foreach =  7658 ms
fastest to slowest
  foreach Span =   3710 ms     51.6%
    Span [i--] =   3745 ms     51.1%
    Span [i++] =   3932 ms     48.7%
     for [i++] =   4593 ms     40.0%
     for [i--] =   7042 ms      8.0%
(base) foreach =   7658 ms      0.0%
       Replace =  18641 ms   -143.4%
         Split =  21469 ms   -180.3%
          Linq =  39726 ms   -418.8%
Regex Compiled = 128422 ms -1,577.0%
         Regex = 179603 ms -2,245.3%
         
         
.NET 6
Starting test, 100000000 iterations set
(base) foreach =  7343 ms
fastest to slowest
  foreach Span =   2918 ms     60.3%
     for [i++] =   2945 ms     59.9%
    Span [i++] =   3105 ms     57.7%
    Span [i--] =   5076 ms     30.9%
(base) foreach =   7343 ms      0.0%
     for [i--] =   8645 ms    -17.7%
       Replace =  18307 ms   -149.3%
         Split =  21440 ms   -192.0%
          Linq =  39354 ms   -435.9%
Regex Compiled = 114178 ms -1,454.9%
         Regex = 186493 ms -2,439.7%
```