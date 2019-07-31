namespace Simulator.Plays
{
    public class Hit : Play
    {
        public int Bases { get; set; }

        public Hit(InningState next, int bases) : base(next)
        {
            Bases = bases;
        }

        public override InningState Simulate(InningState state)
        {
            state.Hit(Bases);
            return state;
        }
    }
}
