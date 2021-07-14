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
