using System;
using Microsoft.VisualBasic;
using TestProject.Interfaces;
using TestProject.Models;
using TestProject.Models.Enums;

namespace TestProject
{
	public class GameManager
	{
		private readonly IGridGen gridGenerator;
		private readonly IDisplayManager displayManager;

		public GameManager(IGridGen gridGenerator, IDisplayManager displayManager)
		{
			this.gridGenerator = gridGenerator;
			this.displayManager = displayManager;
		}

		public void BeginGame()
		{
            GridState state = gridGenerator.GenerateGrid(3, 3);

			ShowCurrentGameState(state);

			GameLoop(state);
        }

		private void GameLoop(GridState state)
		{
			while(true)
			{
				string? playerResponse = Console.ReadLine()?.ToUpper();

				Console.Clear();

				if(playerResponse == "Q")
				{
					break;
				}

				if(string.IsNullOrEmpty(playerResponse))
				{
					ShowCurrentGameState("Cannot enter an empty response", state);
					continue;
				}

				if(playerResponse!.Length != 2)
				{
					ShowCurrentGameState("Response must be two characters (A letter and a number)", state);
					continue;
				}

				char letterCoord = playerResponse[0];
				int numberCoord;

                if (!int.TryParse(playerResponse[1].ToString(), out numberCoord))
				{
					ShowCurrentGameState("Error converting coordinates, a coordinate must consist of a letter and number",
										 state);

					continue;
				}

				(int, int) coords = ((int)letterCoord - 65, numberCoord - 1);

				try
				{
                    state.Grid[coords.Item1, coords.Item2] = state.IsPlayer1Playing ? GridIcons.Cross : GridIcons.Circle;
                }
				catch (IndexOutOfRangeException)
				{
					ShowCurrentGameState("Coordinates out of bounds", state);
					continue;
				}

				if(CheckForWinner(state))
				{
					EndGame(state);
					break;
				}

				state.IsPlayer1Playing = !state.IsPlayer1Playing;

				ShowCurrentGameState(state);
            }
		}

		private void EndGame(GridState state)
		{
			Console.Clear();

			string winnerName = state.IsPlayer1Playing ? "Player 1 (Crosses)" : "Player 2 (Noughts)";

            Console.WriteLine($"Congratulations {winnerName}, you have won the game!!!");
			Console.WriteLine("\nPress any key to end the game");

			Console.ReadKey();
		}

		private void ShowCurrentGameState(string message, GridState state)
		{
			Console.WriteLine(message);
			displayManager.DisplayCurrentGridState(state);
		}

		private void ShowCurrentGameState(GridState state)
		{
            displayManager.DisplayCurrentGridState(state);

            Console.WriteLine($"\n{ (state.IsPlayer1Playing ? "Player 1 (Crosses)" : "Player 2 (Noughts)")}, " +
				$"enter your coordinates (e.g. A2)");
        }

        private bool CheckForWinner(GridState state)
        {
			if(CheckXAxisForWinner(state) || CheckYAxisForWinner(state) || CheckDiagonalsForWinner(state))
			{
				return true;
			}

            return false;
        }

		private bool CheckYAxisForWinner(GridState state)
		{
			for(int x = 0; x < state.Grid.GetLength(0); x++)
			{
				int numberOfCrosses = 0, numberOfNoughts = 0;

				for(int y = 0; y < state.Grid.GetLength(1); y++)
				{
                    GridIcons gridIcon = state.Grid[x, y];
					UpdateNoughtsAndCrossesCount(gridIcon, ref numberOfNoughts, ref numberOfCrosses);
                }

				if(numberOfCrosses == state.Grid.GetLength(1) || numberOfNoughts == state.Grid.GetLength(1))
				{
					return true;
				}
			}

			return false;
		}

		private bool CheckXAxisForWinner(GridState state)
		{
            for (int y = 0; y < state.Grid.GetLength(0); y++)
            {
                int numberOfCrosses = 0, numberOfNoughts = 0;

                for (int x = 0; x < state.Grid.GetLength(1); x++)
                {
                    GridIcons gridIcon = state.Grid[x, y];
                    UpdateNoughtsAndCrossesCount(gridIcon, ref numberOfNoughts, ref numberOfCrosses);
                }

                if (numberOfCrosses == state.Grid.GetLength(1) || numberOfNoughts == state.Grid.GetLength(1))
                {
                    return true;
                }
            }

            return false;
        }

		private bool CheckDiagonalsForWinner(GridState state)
		{
			if(state.Grid.GetLength(0) != state.Grid.GetLength(1))
			{
				return false;
			}

			int diagonalLength = state.Grid.GetLength(1);
			
			for (int i = 0; i < diagonalLength; i++)
			{
				GridIcons gridIcon = state.Grid[i, i];

				int numberOfNoughts = 0, numberOfCrosses = 0;

				UpdateNoughtsAndCrossesCount(gridIcon, ref numberOfNoughts, ref numberOfCrosses);

				if(numberOfCrosses == diagonalLength || numberOfNoughts == diagonalLength)
				{
					return true;
				}
			}

			for (int i = state.Grid.GetLength(0) - 1; i >= 0; i--)
			{
				GridIcons gridIcon = state.Grid[i, i];

                int numberOfNoughts = 0, numberOfCrosses = 0;

                UpdateNoughtsAndCrossesCount(gridIcon, ref numberOfNoughts, ref numberOfCrosses);

                if (numberOfCrosses == diagonalLength || numberOfNoughts == diagonalLength)
                {
                    return true;
                }
            }

			return false;
		}

		private void UpdateNoughtsAndCrossesCount(GridIcons gridIcon, ref int numberOfNoughts, ref int numberOfCrosses)
		{
            if (gridIcon == GridIcons.Circle)
            {
                numberOfNoughts++;
                return;
            }

            if (gridIcon == GridIcons.Cross)
            {
                numberOfCrosses++;
                return;
            }
        }
    }
}

