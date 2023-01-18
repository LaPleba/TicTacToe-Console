using System;
using TestProject.Interfaces;
using TestProject.Models;
using TestProject.Models.Enums;

namespace TestProject
{
	public class DisplayManager : IDisplayManager
	{
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

