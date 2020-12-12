namespace AdventOfCode2020.Day12
{
    public interface ICoordinator
    {
        (int x, int y) GetShipLocation();

        void ProcessCommand(string command);
    }
}