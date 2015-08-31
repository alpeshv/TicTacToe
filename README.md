# TicTacToe
TicTacToe game

Notes:

1)	By default Player O gets the first turn. Subsequent games maintain player turns in continuous order.

Thoughts:

1)	RandomNumberGenerator can be replaced with RandomMoveGenerator which can generate and return random coordinates as a Tuple<int, int>. Did not use it since it does not add up any value in the current context.
2)	Not checking for Game Board boundaries before making a move since numbers are always going to be between 1 to 3.

