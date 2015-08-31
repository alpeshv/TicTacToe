# TicTacToe
MS Visual Studio 2013 is used to develop this application using C# 4.5.1. Tests were written using NUnit and Moq. There are two projects in the solutions.

1) TicTacToe game console project

2) Class library for tests



Notes
-----
1)  Two computer players play game of tic-tac-toe

2)  User is prompted to begin the game

3)  Empty game board is displayed at the start of the game and it is updated after every move

4)	By default Player O gets the first turn. Subsequent games maintain player turns in continuous order

5)  Each player makes random moves one by one

6)  Game pauses for one second between moves

7)  Result of the game is displayed at the end of the game

8)  User is prompted to restart the game at the end of the game


Thoughts
--------

1)	RandomNumberGenerator can be replaced with RandomMoveGenerator which can generate and return random coordinates as a Tuple<int, int>. Did not use it since it does not add up any value in the current context.

2)	Not checking for Game Board boundaries before making a move since numbers are always going to be between 1 to 3.

