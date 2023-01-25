using System;
using TestProject.Models;

namespace TestProject.Interfaces
{
	public interface IDisplayManager
	{
		void ShowStartScreen();
		(int, int) RequestGridSize();
		void DisplayCurrentGridState(GridState state);
	}
}

