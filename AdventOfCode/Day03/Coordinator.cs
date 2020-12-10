namespace AdventOfCode2020.Day3
{
    public class Coordinator : ICoordinator
    {
        public Coordinator(int stepsUp = 0, int stepsDown = 0, int stepsRight = 0, int stepsLeft = 0)
        {
            StepsUp = stepsUp;
            StepsDown = stepsDown;
            StepsRight = stepsRight;
            StepsLeft = stepsLeft;
        }

        public int StepsUp { get; set; }
        public int StepsDown { get; set; }
        public int StepsRight { get; set; }
        public int StepsLeft { get; set; }

        public void MakeMove(TreesCounter treesCounter)
        {
            treesCounter.hPosition += StepsRight - StepsLeft;
            treesCounter.vPosition += StepsDown - StepsUp;
        }
    }
}