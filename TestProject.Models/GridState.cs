using TestProject.Models.Enums;

namespace TestProject.Models;

public class GridState
{ 
    public GridIcons[,] Grid { get; set; }

    public bool IsGameEnded { get; set; } = false;
    public bool IsPlayer1Playing { get; set; } = true;

    public GridState(int xSize, int ySize)
    {
        Grid = new GridIcons[3, 3];
    }
}