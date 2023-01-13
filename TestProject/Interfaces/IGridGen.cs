using System;
using TestProject.Models;

namespace TestProject.Interfaces
{
	public interface IGridGen
	{
		GridState GenerateGrid(int xSize, int ySize);
	}
}

