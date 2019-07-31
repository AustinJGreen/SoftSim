using System;

namespace Simulator
{
    public struct InningState
    {
        public int Outs { get; set; }

        public int RunsScored { get; set; }

        public byte Bases { get; set; }


        /// <summary>
        /// Gets the amount of base runners
        /// </summary>
        /// <returns>The amount of base runners</returns>
        public int BaseRunnerCount()
        {
            return (Bases & 1) + ((Bases >> 1) & 1) + ((Bases >> 2) & 1);
        }

        /// <summary>
        /// Gets whether the runner on a base has to run
        /// </summary>
        /// <param name="base">The base to check</param>
        /// <returns></returns>
        public bool IsOffensiveForceOn(int @base)
        {
            return (Bases & ((1 << @base) - 1)) != 0;
        }

        /// <summary>
        /// Gets whether there is a runner on a base
        /// </summary>
        /// <param name="base">Base number, first base is 0.</param>
        /// <returns></returns>
        public bool RunnerOnBase(int @base)
        {
            return (Bases & (1 << @base)) != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base"></param>
        public void AddRunnerOnBase(int @base)
        {
            
        }

        public void WalkHitter()
        {
            //Each bit moves up only if bit before is set
            //If person at 3, 2, 1, go to 4th (home)
            //If person at 2, 1, go to 3rd
            //If person at 1, go to 2nd
            //Set 1st

            Bases |= (byte)(Bases + 1);
        }

        /// <summary>
        /// Moves all runners on base a specified number of bases, does not add any new base runners.
        /// </summary>
        /// <param name="bases">The number of bases to move each runner</param>
        public void MoveAllRunners(int bases)
        {
            if (bases > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(bases), "Maximum amount of bases to run is 4.");
            }

            Bases <<= bases;
            RunsScored += ((Bases >> 3) & 1) + ((Bases >> 4) & 1) + ((Bases >> 5) & 1) + ((Bases >> 6) & 1) + ((Bases >> 7) & 1);
        }

        public bool Over()
        {
            return Outs >= 3 || RunsScored >= 7;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Outs.GetHashCode();
                hash += (hash * 57) + RunsScored.GetHashCode();
                hash += (hash * 57) + Bases.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is InningState)
            {
                InningState ins = (InningState)obj;
                return ins.Bases == Bases && ins.Outs == Outs && ins.RunsScored == RunsScored;
            }

            return false;
        }

        public InningState(InningState previous) : this()
        {
            Outs = previous.Outs;
            RunsScored = previous.RunsScored;
            Bases = previous.Bases;
        }

        public static bool operator ==(InningState a, InningState b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(InningState a, InningState b)
        {
            return !a.Equals(b);
        }
    }
}
