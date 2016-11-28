namespace Pr08LinkedQueueUnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using Pr07LinkedQueue;

    [TestClass]
    public class UnitTests // Tests were adapted from CircularQueue tests
    {
        [TestMethod]
        public void Enqueue_EmptyQueue_ShouldAddElement()
        {
            var queue = new LinkedQueue<int>();
            queue.Enqueue(5);
            Assert.AreEqual(1, queue.Count);
        }

        [TestMethod]
        public void EnqueueDeque_ShouldWorkCorrectly()
        {
            var queue = new LinkedQueue<string>();
            var element = "some value";

            queue.Enqueue(element);
            var elementFromQueue = queue.Dequeue();

            Assert.AreEqual(0, queue.Count);
            Assert.AreEqual(element, elementFromQueue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dequeue_EmptyQueue_ThrowsException()
        {
            var queue = new LinkedQueue<int>();
            queue.Dequeue();
        }

        [TestMethod]
        public void EnqueueDequeue100Elements_ShouldWorkCorrectly()
        {
            var queue = new LinkedQueue<int>();
            int numberOfElements = 1000;

            for (int i = 0; i < numberOfElements; i++)
            {
                queue.Enqueue(i);
            }

            for (int i = 0; i < numberOfElements; i++)
            {
                Assert.AreEqual(numberOfElements - i, queue.Count);
                var element = queue.Dequeue();
                Assert.AreEqual(i, element);
                Assert.AreEqual(numberOfElements - i - 1, queue.Count);
            }
        }

        [TestMethod]
        public void LinkedQueue_EnqueueDequeueManyChunks_ShouldWorkCorrectly()
        {
            var queue = new LinkedQueue<int>();
            int chunks = 100;

            int value = 1;
            for (int i = 0; i < chunks; i++)
            {
                Assert.AreEqual(0, queue.Count);
                var chunkSize = i + 1;
                for (int counter = 0; counter < chunkSize; counter++)
                {
                    Assert.AreEqual(value - 1, queue.Count);
                    queue.Enqueue(value);
                    Assert.AreEqual(value, queue.Count);
                    value++;
                }
                for (int counter = 0; counter < chunkSize; counter++)
                {
                    value--;
                    Assert.AreEqual(value, queue.Count);
                    queue.Dequeue();
                    Assert.AreEqual(value - 1, queue.Count);
                }
                Assert.AreEqual(0, queue.Count);
            }
        }

        [TestMethod]
        public void Enqueue500Elements_ToArray_ShouldWorkCorrectly()
        {
            var array = Enumerable.Range(1, 500).ToArray();
            var queue = new LinkedQueue<int>();

            for (int i = 0; i < array.Length; i++)
            {
                queue.Enqueue(array[i]);
            }
            var arrayFromQueue = queue.ToArray();

            CollectionAssert.AreEqual(array, arrayFromQueue);
        }
    }
}
