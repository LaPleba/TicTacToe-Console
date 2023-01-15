using TestProject.Models.Enums;

namespace TestProject.Models;

public class GridState
{ 
    public GridIcon[,] Grid { get; set; }

    public bool IsPlayer1Playing { get; set; } = true;

    public GridState(int xSize, int ySize)
    {
        Grid = new GridIcon[3, 3];
    }
}