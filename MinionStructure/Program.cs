using System;
using System.Collections;
using System.Collections.Generic;

namespace MinionStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList<Minion> minions = new DoublyLinkedList<Minion>();

            Minion minion = new Minion(4, "Mark", 24, 3);

            minions.Add(new Minion(1, "Rik", 26, 1));
            minions.Add(minion);
            minions.AddFirst(new Minion(3, "Jess", 26, 1));
            //minions.DeleteFirst();
            //minions.DeleteLast();
            minions.Add(new Minion(5, "Jin", 27, 2));
            //minions.EditLast(new Minion(7, "Ten", 16, 4));
            minions.Add(new Minion(2, "Alex", 20, 4));
            minions.Edit(minion, new Minion(6, "Ash", 24, 4));
            //minions.Clear();
            foreach (var data in minions)
            {
                Console.WriteLine("Minion: {0} {1} {2}", data.Name, data.Age, data.TownId);
            }
            Console.WriteLine();
        }

        public class Minion
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public int Age { get; set; }
            public int TownId { get; set; }

            public Minion(int id, String name, int age, int townId)
            {
                Id = id;
                Name = name;
                Age = age;
                TownId = townId;
            }

        }

        public class Node<T>
        {
            public T Data { get; set; }
            public Node<T> Next { get; set; }
            public Node<T> Previous { get; set; }

            public Node(T data)
            {
                Data = data;
            }

            public override string ToString()
            {
                return Data.ToString();
            }
        }

        public class DoublyLinkedList<T>: IEnumerable<T>
        {
            Node<T> head;
            Node<T> tail;
            int count;

            public void Add(T data)
            {
                Node<T> node = new Node<T>(data);

                if (head == null)
                {
                    head = node;
                }
                else
                {
                    tail.Next = node;
                    node.Previous = tail;
                }
                tail = node;
                count++;
            }

            public void AddFirst(T data)
            {
                Node<T> node = new Node<T>(data);
                Node<T> temp = head;
                node.Next = temp;
                head = node;
                if (count == 0)
                    tail = head;
                else
                    temp.Previous = node;
                count++;
            }

            public void DeleteFirst()
            {
                if (count != 0)
                {
                    head = head.Next;
                    count--;
                    if (count == 0)
                    {
                        tail = null;
                    }
                    else
                    {
                        head.Previous = null;
                    }
                }
                count--;
            }

            public void DeleteLast()
            {
               if (count == 1)
               {
                    head = null;
                    tail = null;
               }
               if(count > 1)
               { 
                    tail.Previous.Next = null;
                    tail = tail.Previous;   
               }
                count--;
            }

            public void Edit(T oldData, T newData)
            {
                Node<T> current = head;
                while(current != null)
                {
                    if(current.Data.Equals(oldData))
                    {
                        current.Data = newData;
                    }
                    current = current.Next;
                }                                 
            }

            public void EditLast(T data)
            {
                Node<T> node = new Node<T>(data);
                if (head == null)
                {
                    head = node;
                    tail = node;
                }
                else
                {
                    tail.Data = data;
                }
            }

            public void Clear()
            {
                head = null;
                tail = null;
                count = 0;
            }

            public int Count { get { return count; } }

            public IEnumerator<T> GetEnumerator()
            {
                Node<T> current = head;
                while (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)this).GetEnumerator();
            }

            public DoublyLinkedList()
            {

            }
        }
    }
}
