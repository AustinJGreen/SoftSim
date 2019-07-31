namespace Simulator.Plays
{
    public class Walk : Play
    {
        public Walk(InningState next) : base(next)
        {
        }

        public override InningState Simulate(InningState state)
        {
            state.WalkHitter();
            return state;
        }
    }
}
