namespace Simulator.Plays
{
    public class Strikeout : Play
    {
        public Strikeout(InningState next) : base(next)
        {

        }

        public override InningState Simulate(InningState state)
        {
            state.Outs += 1;
            return state;
        }
    }
}
