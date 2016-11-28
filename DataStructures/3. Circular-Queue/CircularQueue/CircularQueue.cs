using System;
using System.Collections.Generic;

public class CircularQueue<T>
{
    private const int DeffaultCapacity = 16;

    private T[] arr;
    private int head;
    private int tail;

    public int Count { get; private set; }

    public CircularQueue(int capacity = DeffaultCapacity)
    {
        this.arr = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if (this.Count >= this.arr.Length)
        {
            this.Resize();
        }

        this.arr[this.tail] = element;
        this.tail = ++this.tail % this.arr.Length;
        this.Count++;
    }

    private void Resize()
    {
        T[] newArr = new T[this.arr.Length << 1];
        this.CopyAllElementsTo(newArr);
        this.arr = newArr;
        this.head = 0;
        this.tail = this.Count;
    }

    private void CopyAllElementsTo(T[] resultArr)
    {
        int sourceIndex = this.head;

        for (int destinationIndex = 0; destinationIndex < this.Count; destinationIndex++)
        {
            resultArr[destinationIndex] = this.arr[sourceIndex];
            sourceIndex = ++sourceIndex % this.arr.Length;
        }
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty!");
        }

        T element = this.arr[this.head];
        this.arr[this.head] = default(T); // mem leak if we don't do this!
        this.head = ++this.head % this.arr.Length;
        this.Count--;

        return element;
    }

    public T[] ToArray()
    {
        T[] arrResult = new T[this.Count];
        this.CopyAllElementsTo(arrResult);

        return arrResult;
    }
}


class Example
{
    static void Main()
    {
        var queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        var first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
