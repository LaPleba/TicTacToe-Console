using System;
using TestProject.Models;

namespace TestProject
{
	public class GameManager
	{
		private readonly GridGen gridGenerator;
		private readonly DisplayManager displayManager;

		public GameManager(GridGen gridGenerator, DisplayManager displayManager)
		{
			this.gridGenerator = gridGenerator;
			this.displayManager = displayManager;
		}

		public void BeginGame()
		{
            GridState gridState = gridGenerator.GenerateGrid(3, 3);

            displayManager.DisplayCurrentGridState(gridState);

            Console.ReadLine();
        }

		private void GameLoop()
		{

		}
	}
}

