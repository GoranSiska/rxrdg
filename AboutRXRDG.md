# Introduction #

RXRDG is a learning project that showcases the use of design patterns while still creating a useful utility.

# Details #

Rxrdg started as a solution to a problem of creating test data for a RL project. The basic idea is to leverage the existing (regular expression) validation patterns to create random data that conforms to such patterns. This way _valid_ random data is created.
Soon after the initial solution was developed I realized this would be an ideal project to showcase code readability and use of design patterns. I've started working on a rewrite while keeping log of the development process at [ReadableCode](http://www.readablecode.blogspot.com) blog.
As this is a learning project there are a few goals I wish to achieve:
  * The code should be as expressive and readable as possible
  * The code should be maintainable and extendable
  * The code should showcase the use of design patters

There are also a few goals I'm trying to stay clear of such as:
  * Speed optimization at the cost of primary goals
  * Memory footprint optimization at the cost of primary goals

Any suggestions for improvements within these guidelines is quite welcome and encouraged.

# Todo #

This is still very much a work in progress. The project goal was to showcase design patterns and is in no way optimized for speed or memory usage.

Not all regular expression flavors and extensions are supported.