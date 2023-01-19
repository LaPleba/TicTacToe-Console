using System;
using TestProject.Models;

namespace TestProject.Interfaces
{
	public interface IDisplayManager
	{
		void ShowStartScreen();
		void DisplayCurrentGridState(GridState state);
	}
}

