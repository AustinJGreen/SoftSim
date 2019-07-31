using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Plays
{
    public class Strikeout : Play
    {
        public override InningState Simulate(InningState state)
        {
            state.Outs += 1;
            return state;
        }

        public Strikeout(InningState next) : base(next)
        {

        }
    }
}
