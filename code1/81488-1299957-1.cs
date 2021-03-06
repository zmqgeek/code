C#
using System;
using System.Collections.Generic;
namespace test
{
    class Program
    {
        class MyList<T> : List<T>
        {
            public event EventHandler OnAdd;
            public void Add(T item)
            {
                if (null != OnAdd)
                {
                    OnAdd(this, null);
                }
                base.Add(item);
            }
        }
        static void Main(string[] args)
        {
            MyList<int> l = new MyList<int>();
            l.OnAdd += new EventHandler(l_OnAdd);
            l.Add(1);
        }
        static void l_OnAdd(object sender, EventArgs e)
        {
            Console.WriteLine("Element added...");
        }
    }
}
Be aware that you should also re-implement `AddRange`, declare your own event type if you want to know what has been added and so on, this is just a basic idea...
