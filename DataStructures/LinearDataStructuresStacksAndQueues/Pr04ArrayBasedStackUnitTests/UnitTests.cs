namespace Pr04ArrayBasedStackUnitTests
{
    using System;
    using Pr03ImplementArrayBasedStack;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestPushPop_SingleElement_ExpectSuccess()
        {
            ArrayStack<int> testStack = new ArrayStack<int>();
            Assert.AreEqual(0, testStack.Count);
            int expected = 5;
            testStack.Push(expected);
            Assert.AreEqual(1, testStack.Count);
            int actual = testStack.Pop();
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(0, testStack.Count);
        }

        [TestMethod]
        public void TestPushPop_1000Elements_ExpectSuccess()
        {
            ArrayStack<string> testStack = new ArrayStack<string>();
            Assert.AreEqual(0, testStack.Count);
            for (int i = 1; i <= 1000; i++)
            {
                testStack.Push(i.ToString());
                Assert.AreEqual(i, testStack.Count);
            }

            for (int i = 1000; i >= 1; i--)
            {
                string actualEl = testStack.Pop();
                Assert.AreEqual(i.ToString(), actualEl);
                int expectedIndex = i - 1;
                Assert.AreEqual(expectedIndex, testStack.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void TestPop_EmptyStack_ExpectException()
        {
            ArrayStack<int> testStack = new ArrayStack<int>();
            testStack.Pop();
        }

        [TestMethod]
        public void TestPushPop_InnitialCapacityOne_ExpectSuccess()
        {
            ArrayStack<int> testStack = new ArrayStack<int>(1);
            Assert.AreEqual(0, testStack.Count);
            testStack.Push(1);
            Assert.AreEqual(1, testStack.Count);
            testStack.Push(2);
            Assert.AreEqual(2, testStack.Count);
            int actual = testStack.Pop();
            Assert.AreEqual(2, actual);
            Assert.AreEqual(1, testStack.Count);
            actual = testStack.Pop();
            Assert.AreEqual(1, actual);
            Assert.AreEqual(0, testStack.Count);
        }

        [TestMethod]
        public void TestToArray_FourElements_ExpectSuccess()
        {
            ArrayStack<int> testStack = new ArrayStack<int>();
            testStack.Push(3);
            testStack.Push(5);
            testStack.Push(-2);
            testStack.Push(7);

            int[] actual = testStack.ToArray();
            for (int i = actual.Length - 1; i >= 0; i--)
            {
                Assert.AreEqual(testStack.Pop(), actual[i]);
            }
        }

        [TestMethod]
        public void TestToArray_EmptyStack_ExpectSuccess()
        {
            ArrayStack<DateTime> testStack = new ArrayStack<DateTime>();

            DateTime[] actual = testStack.ToArray();

            Assert.AreEqual(0, actual.Length);
        }
    }
}
