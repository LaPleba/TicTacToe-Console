using System;
using TestProject.Interfaces;
using TestProject.Models;
using TestProject.Models.Enums;

namespace TestProject
{
	public class GridGen : IGridGen
	{
		public GridState GenerateGrid(int xSize, int ySize)
		{
			var gridState = new GridState(xSize, ySize);

			for(int y = 0; y < ySize; y++)
			{
				for(int x = 0; x < xSize; x++)
                {
					gridState.Grid[x, y] = GridIcons.None;
				}
			}

			return gridState;
		}
	}
}

