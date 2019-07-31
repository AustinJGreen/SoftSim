using System;

namespace Simulator
{
    public struct InningState
    {
        private byte bases;

        /// <summary>
        /// Gets the bases state
        /// </summary>
        public byte Bases => bases;

        /// <summary>
        /// Gets or sets the number of outs
        /// </summary>
        public int Outs { get; set; }

        /// <summary>
        /// Gets or sets the runs scored
        /// </summary>
        public int RunsScored { get; set; }

        /// <summary>
        /// Checks if the inning is over
        /// </summary>
        public bool IsOver => Outs >= 3 || RunsScored >= 7;

        /// <summary>
        /// Gets the amount of base runners
        /// </summary>
        public int BaseRunners => (bases & 1) + ((bases >> 1) & 1) + ((bases >> 2) & 1);

        /// <summary>
        /// Calculates and carry's scored runs from bases to RunsScored
        /// </summary>
        private void CarryPoints()
        {
            RunsScored += ((bases >> 3) & 1) + ((bases >> 4) & 1) + ((bases >> 5) & 1) + ((bases >> 6) & 1) + ((bases >> 7) & 1);
            bases &= 0b111;
        }

        /// <summary>
        /// Gets whether there is a runner on a base
        /// </summary>
        /// <param name="base">Base number, first base is 0.</param>
        /// <returns></returns>
        public bool RunnerOnBase(int @base)
        {
            return (bases & (1 << @base)) != 0;
        }

        /// <summary>
        /// Sets a batter on first and advances all batters if necessary
        /// </summary>
        public void WalkHitter()
        {
            bases |= (byte)(bases + 1);
            CarryPoints();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bases"></param>
        public void Hit(int bases)
        {
            bases <<= bases;
            bases |= (byte)(1 << bases);
            CarryPoints();
        }

        /// <summary>
        /// Moves all runners on base a specified number of bases, does not add any new base runners.
        /// </summary>
        /// <param name="numBases">The number of bases to move each runner</param>
        public void MoveAllRunners(int numBases)
        {
            if (numBases > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(numBases), "Maximum amount of bases to run is 4.");
            }

            bases <<= numBases;
            CarryPoints();
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
            bases = previous.bases;
        }

        public InningState(byte bases) : this()
        {
            this.bases = bases;
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
