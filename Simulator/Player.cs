using System;
using System.Diagnostics;

namespace Simulator
{
    [DebuggerDisplay("{Name}")]
    public class Player
    {
        public string Name { get; set; }

        public PlayerStats Stats { get; set; }

        public Player(string name, PlayerStats stats)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Stats = stats;
        }
    }
}
