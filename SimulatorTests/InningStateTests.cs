using Simulator;
using NUnit.Framework;

namespace Tests
{
    public class InningStateTests
    {
        public bool IsStart(InningState ins)
        {
            return ins.Bases == 0 && ins.Outs == 0 && ins.RunsScored == 0;
        }

        [Test]
        public void TestMoveAllRunners()
        {
            InningState ins = new InningState();
            for (int i = 0; i <= 4; i++)
            {
                ins.MoveAllRunners(i);
                Assert.IsTrue(IsStart(ins));
            }
        }

        [Test]
        public void TestInningStateEquality()
        {
            InningState a = new InningState();
            InningState b = new InningState();

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.GetHashCode() == b.GetHashCode());

            b.Outs = 1;
            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(a.GetHashCode() == b.GetHashCode());

            a.Outs = 1;
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.GetHashCode() == b.GetHashCode());

            a.RunsScored = 2;
            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(a.GetHashCode() == b.GetHashCode());

            b.RunsScored = 2;
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.GetHashCode() == b.GetHashCode());

            b.Bases = 5;
            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(a.GetHashCode() == b.GetHashCode());

            a.Bases = 5;
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.GetHashCode() == b.GetHashCode());

            InningState c = new InningState(a);
            Assert.IsTrue(a.Equals(c));
            Assert.IsTrue(a.GetHashCode() == c.GetHashCode());
        }

        [Test]
        public void TestWalk()
        {
            byte[] correct = new byte[] { 1, 3, 3, 7, 5, 7, 7, 15, 9 };

            
            for (byte i = 0; i < correct.Length; i++)
            {
                InningState a = new InningState(i);
                a.WalkHitter();
                Assert.AreEqual(a.Bases, correct[i]);
            }
        }
    }
}