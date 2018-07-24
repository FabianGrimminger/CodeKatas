# CodeKatas
A repository for some coding katas. https://en.wikipedia.org/wiki/Kata_(programming)

## Structure of the repository
- First ordered by languages
- Then ordered by katas
- Every kata is a own project

## Game of Life kata
I start with some Game Of Life implementations every day (if possible) in Java, C# and C++.
In every implementation I try to focus on one special aspect, like using a special data structure, not using a language feature or overusing it.

### Requirements every implementation must fulfill:
- 10x10 playing field, 20 cells are living for at the start (except for the g option)
- Torus-like field, so if something would go past one edge it occurs on the other side
- Console output, 'x' for a living cell and 'o' for a dead one. If possible, dark blue background, white 'o' and red 'x'.
- Different Parameters when calling for initialization. Only the first one gets considered, if any.
    - none: just a random field
	- a number: seed for the random generator
	- g: g for glider, the only figure on the field, starting in the top left corner

### The Jurney
1. Day: One Class, just typing it down without much thinking about, it should only work.