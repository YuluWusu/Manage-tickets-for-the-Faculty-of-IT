using HUIT_PriorityQueue.Models;
using System;
using System.Collections.Generic;

namespace HUIT_PriorityQueue.Models
{
    public class QNode
    {
        public PhieuGiaiQuyet Info { get; set; }
        public int Priority { get; set; }
        public QNode Next { get; set; }
    }

    public class PriorityQueue
    {
        public QNode Head { get; set; }

        public PriorityQueue()
        {
            Head = null;
        }

        public bool IsEmpty()
        {
            return Head == null;
        }

        public void Enqueue(PhieuGiaiQuyet item, int priority)
        {
            QNode newNode = new QNode { Info = item, Priority = priority, Next = null };

            if (Head == null || priority < Head.Priority)
            {
                newNode.Next = Head;
                Head = newNode;
                return;
            }

            QNode current = Head;
            while (current.Next != null && priority >= current.Next.Priority)
            {
                current = current.Next;
            }
            newNode.Next = current.Next;
            current.Next = newNode;
        }

        public PhieuGiaiQuyet Dequeue()
        {
            if (IsEmpty())
                return null;

            PhieuGiaiQuyet item = Head.Info;
            Head = Head.Next;
            return item;
        }

        public PhieuGiaiQuyet Peek()
        {
            return IsEmpty() ? null : Head.Info;
        }

        public int Count()
        {
            int count = 0;
            QNode current = Head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }

        public List<PhieuGiaiQuyet> GetAllItems()
        {
            List<PhieuGiaiQuyet> items = new List<PhieuGiaiQuyet>();
            QNode current = Head;
            while (current != null)
            {
                items.Add(current.Info);
                current = current.Next;
            }
            return items;
        }
    }
}