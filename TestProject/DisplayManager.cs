using System;
using TestProject.Interfaces;
using TestProject.Models;
using TestProject.Models.Enums;

namespace TestProject
{
	public class DisplayManager : IDisplayManager
	{
		public void ShowStartScreen()
		{
			Console.WriteLine("  _______         ______              ______");
			Console.WriteLine(" /_  __(_)____   /_  __/___ ______   /_  __/___  ___ ");
			Console.WriteLine(@"  / / / / ___/    / / / __ `/ ___/    / / / __ \/ _ \");
			Console.WriteLine(" / / / / /__     / / / /_/ / /__     / / / /_/ /  __/");
			Console.WriteLine(@"/_/ /_/\___/    /_/  \__,_/\___/    /_/  \____/\___/");
			Console.WriteLine("\n            ---- By LaPleba ----");
			Console.WriteLine("\nPress any key to continue");
			Console.ReadKey();
			Console.Clear();
		}

		public (int, int) RequestGridSize()
		{
			Console.WriteLine("Please enter a grid width");

			int width = GetAxisLengthFromUser("Error: please enter a valid width number");

			Console.WriteLine("Please enter a grid height");

			int height = GetAxisLengthFromUser("Error: please enter a valid height number");

			return (width, height);
		}

		/// <param name="errorString">The error to return to the user if they enter an incorrect value</param>
		private int GetAxisLengthFromUser (string errorString = "Error: enter a valid number")
		{
			string? input = Console.ReadLine();
			int output;

			while (!int.TryParse(input, out output) || string.IsNullOrEmpty(input))
			{
				Console.WriteLine(errorString);
				input = Console.ReadLine();
			}

			Console.Clear();

			return output;
		}

		public void DisplayCurrentGridState(GridState state)
		{
			int xSize = state.Grid.GetLength(0);
			int ySize = state.Grid.GetLength(1);

			GenerateInitialRow(xSize);
			GenerateAdditionalRows(state, xSize, ySize);
			GenerateLetterCoords(xSize);
		}

		private void GenerateInitialRow(int xSize)
		{
			string row1String = "";

			row1String += "  +---";

			for(int x = 1; x < xSize; x++)
			{

				row1String += "+---";
            }

			row1String += "+";

			Console.WriteLine(row1String);
		}

		private void GenerateAdditionalRows(GridState state, int xSize, int ySize)
		{
			for(int y = 0; y < ySize; y++)
			{
				string rowString = "";
				string rowFinaliser = "";

				for(int x = 0; x < xSize; x++)
				{
					if (x == 0)
					{
						rowString += $"{ySize - y} |";
                        rowFinaliser += "  +---";
                    }
					else
					{
						rowFinaliser += "+---";
					}

					switch(state.Grid[x, ySize - y - 1])
					{
						case GridIcon.None:
							rowString += "   |";
							break;
						case GridIcon.Cross:
							rowString += " X |";
							break;
						case GridIcon.Circle:
							rowString += " O |";
							break;
						default:
							rowString += "ERR|";
							break;
					}
                }

				rowFinaliser += "+";

				Console.WriteLine(rowString);
				Console.WriteLine(rowFinaliser);
			}
		}

		private void GenerateLetterCoords(int xSize)
		{
			string letterCoords = "    ";

			for(int x = 0; x < xSize; x++)
			{
				char letter = (char)('A' + x);
				letterCoords += $"{letter}   ";
			}

			Console.WriteLine(letterCoords);
		}

		public void ShowTitle()
		{
			Console.ReadKey();
		}
	}
}

