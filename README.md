# TicTacToe Console
A simple tic-tac-toe game built using the console in .NET 7

## Getting Started
To get started, head to the releases page and download the .ZIP file from the latest release. Extract the zip to a location of your choice and run the TestProject executable

## How To Play
Following the title screen, you will be prompted to enter the width and height of your chosen grid.

After this the game will begin, you should see the following screen:
```
  +---+---+---+
3 |   |   |   |
  +---+---+---+
2 |   |   |   |
  +---+---+---+
1 |   |   |   |
  +---+---+---+
    A   B   C   

Player 1 (Crosses), enter your coordinates (e.g. A2)

```

> Note that the grid size will change based on your selected width and height.

Simply enter your coordinates to place your icon in an empty space on the grid. Play will then switch to the second player who must place their icon on an empty space in the grid. The game will then switch back to the first player and play will continue until one player has filled either a row, column or a diagonal with their icon. That player will then have won the game!

> Diagonals can only be used to win if the grids width and height are equal
