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

            DoublyLinkedList<Minion> minionsIndex = new DoublyLinkedList<Minion>();
            minionsIndex.Add(new Minion(6, "Felix", 21, 2));
            minionsIndex.Add(new Minion(7, "Han", 22, 2));
            minionsIndex.Add(minion);
            Console.WriteLine(minionsIndex[0].Name + " " + minionsIndex[0].Age + " " + minionsIndex[0].TownId);
            Console.WriteLine(minionsIndex[1].Name + " " + minionsIndex[1].Age + " " + minionsIndex[1].TownId);
            Console.WriteLine(minionsIndex[2].Name + " " + minionsIndex[2].Age + " " + minionsIndex[2].TownId);

            Minion minion1 = new Minion(10, "Jimin", 21, 2);
            Minion minion2 = new Minion(11, "Lucas", 21, 1);
            Console.WriteLine(minion1.CompareTo(minion2));
        }

        public class Minion : IComparable<Minion>
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
            public int CompareTo(Minion other)
            {
                var result = this.Name.CompareTo(other.Name);
                if (result == 0)
                {
                    result = this.Age.CompareTo(other.Age);
                }

                return result;
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

            public T this[int index]
            {
                get
                {
                   
                    Node<T> current = head;
                    if (index < 0)
                    {
                        throw new Exception("Index less 0");
                    }
                    for(int i = 0; i < index; i++ )
                    {
                           if(current.Next == null)
                           {
                               throw new Exception("Next element is empty");
                           }
                        current = current.Next;
                    }
                    return current.Data;
                }
            }

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

        public class Comparator: IComparer<Minion>
	    {
		    public int Compare(Minion x, Minion y)
		    {
		    	var result = x.Name.CompareTo(y.Name);
		    	if (result == 0)
		    	{
		    		result = x.Age.CompareTo(y.Age);
		    	}

		    	return result;
		    }
	    }
    }
}
