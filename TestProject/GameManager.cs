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
            var gridIconPlaying = state.IsPlayer1Playing ? GridIcon.Cross : GridIcon.Circle;

            for (int i = 1; i <= state.Grid.Length; i++)
            {
                string? playerResponse = Console.ReadLine()?.ToUpper();

                Console.Clear();

                if (playerResponse == "Q")
                {
                    break;
                }

                if (string.IsNullOrEmpty(playerResponse))
                {
                    ShowCurrentGameState("Cannot enter an empty response", state);
                    continue;
                }

                if (playerResponse!.Length != 2)
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
                GridIcon gridAtCoords;

                try
                {
                    gridAtCoords = state.Grid[coords.Item1, coords.Item2];
                }
                catch (IndexOutOfRangeException)
                {
                    ShowCurrentGameState("Coordinates out of bounds", state);
                    continue;
                }

                if (gridAtCoords != GridIcon.None)
                {
                    ShowCurrentGameState("Error: You cannot select a square that already has a nought or cross", state);
                    continue;
                }

                state.Grid[coords.Item1, coords.Item2] = state.IsPlayer1Playing ? GridIcon.Cross : GridIcon.Circle;

                if (CheckForWinner(state, coords))
                {
                    EndGameWin(state);
                    break;
                }

                if(i == state.Grid.Length)
                {
                    EndGameDraw();
                    break;
                }

                state.IsPlayer1Playing = !state.IsPlayer1Playing;

                ShowCurrentGameState(state);
            }
        }

        private void EndGameWin(GridState state)
        {
            Console.Clear();

            string winnerName = state.IsPlayer1Playing ? "Player 1 (Crosses)" : "Player 2 (Noughts)";

            Console.WriteLine($"Congratulations {winnerName}, you have won the game!!!");

            AskUserToContinueOrStopPlaying();
        }

        private void AskUserToContinueOrStopPlaying()
        {
            Console.WriteLine("\nDo you want to stop playing (Y). Press any key to play a new game");

            var response = Console.ReadKey().Key;

            if (response != ConsoleKey.Y)
            {
                Console.Clear();
                BeginGame();
            }
        }

        private void EndGameDraw()
        {
            Console.Clear();

            Console.WriteLine("Unfortunately, the game has ended in a draw");
            
            AskUserToContinueOrStopPlaying();
        }

        private void ShowCurrentGameState(string message, GridState state)
        {
            Console.WriteLine(message);
            ShowCurrentGameState(state);
        }

        private void ShowCurrentGameState(GridState state)
        {
            displayManager.DisplayCurrentGridState(state);

            Console.WriteLine($"\n{(state.IsPlayer1Playing ? "Player 1 (Crosses)" : "Player 2 (Noughts)")}, " +
                $"enter your coordinates (e.g. A2)");
        }

        private bool CheckForWinner(GridState state, (int, int) coordinates)
        {
            if (CheckXAxisForWinner(state, coordinates.Item2) ||
                CheckYAxisForWinner(state, coordinates.Item1) ||
                CheckDiagonalsForWinner(state, coordinates))
            {
                return true;
            }

            return false;
        }

        private bool CheckXAxisForWinner(GridState state, int yCoordinate)
        {
            int numberOfCrosses = 0, numberOfNoughts = 0;

            for (int x = 0; x < state.Grid.GetLength(1); x++)
            {
                GridIcon gridIcon = state.Grid[x, yCoordinate];
                UpdateNoughtsAndCrossesCount(gridIcon, ref numberOfNoughts, ref numberOfCrosses);
            }

            if (numberOfCrosses == state.Grid.GetLength(1) || numberOfNoughts == state.Grid.GetLength(1))
            {
                return true;
            }

            return false;
        }

        private bool CheckYAxisForWinner(GridState state, int xCoordinate)
        {
            int numberOfCrosses = 0, numberOfNoughts = 0;

            for (int y = 0; y < state.Grid.GetLength(1); y++)
            {
                GridIcon gridIcon = state.Grid[xCoordinate, y];
                UpdateNoughtsAndCrossesCount(gridIcon, ref numberOfNoughts, ref numberOfCrosses);
            }

            if (numberOfCrosses == state.Grid.GetLength(1) || numberOfNoughts == state.Grid.GetLength(1))
            {
                return true;
            }


            return false;
        }

        private bool CheckDiagonalsForWinner(GridState state, (int, int) coords)
        {
            if (state.DiagonalLength is null)
            {
                return false;
            }
            int xyMax = state.Grid.GetLength(0) - 1; //For there to be a diagonal, x and y max must be the same

            if(coords.Item1 == coords.Item2 && (coords.Item1 == 0 || coords.Item1 == xyMax))
            {
                return CheckLeftDiagonal(state);
            }

            if(coords.Item1 != coords.Item2 && ((coords.Item1 == xyMax && coords.Item2 == 0) || (coords.Item2 == xyMax && coords.Item1 == 0)))
            {
                return CheckRightDiagonal(state);
            }

            if(coords.Item1 == (xyMax + 1) / 2)
            {
                return CheckLeftDiagonal(state) || CheckRightDiagonal(state);
            }

            return false;
        }

        private bool CheckLeftDiagonal(GridState state)
        {
            int numberOfNoughts = 0, numberOfCrosses = 0;

            for(int i = 0; i < state.DiagonalLength; i++)
            {
                GridIcon gridIcon = state.Grid[i, i];

                UpdateNoughtsAndCrossesCount(gridIcon, ref numberOfNoughts, ref numberOfCrosses);
            }

            if (numberOfNoughts == state.DiagonalLength || numberOfCrosses == state.DiagonalLength)
            {
                return true;
            }

            return false;
        }

        private bool CheckRightDiagonal(GridState state)
        {
            int numberOfNoughts = 0, numberOfCrosses = 0;
            int xMax = state.Grid.GetLength(0) - 1;

            for(int i = 0; i < state.DiagonalLength; i++)
            {
                GridIcon gridIcon = state.Grid[xMax - i, i];

                UpdateNoughtsAndCrossesCount(gridIcon, ref numberOfNoughts, ref numberOfCrosses);
            }

            if(numberOfNoughts == state.DiagonalLength || numberOfCrosses == state.DiagonalLength)
            {
                return true;
            }

            return false;
        }

        private void UpdateNoughtsAndCrossesCount(GridIcon gridIcon, ref int numberOfNoughts, ref int numberOfCrosses)
        {
            if (gridIcon == GridIcon.Circle)
            {
                numberOfNoughts++;
                return;
            }

            if (gridIcon == GridIcon.Cross)
            {
                numberOfCrosses++;
                return;
            }
        }
    }
}

