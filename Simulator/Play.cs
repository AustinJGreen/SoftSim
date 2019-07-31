namespace Simulator
{
    public class Play
    {
        public InningState NextState { get; private set; }

        public virtual InningState Simulate(InningState state)
        {
            return state;
        }

        public bool IsValid(InningState state)
        {
            return Simulate(state) == NextState;
        }

        public Play(InningState next)
        {
            NextState = next;
        }
    }
}
