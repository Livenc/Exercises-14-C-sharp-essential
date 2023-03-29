using System.Collections;

namespace Exercise_2
{

    public static class StaticClass
    {


        internal static class Program
        {

            static void Main(string[] args)
            {

                MyList<int> list = new MyList<int>();
                list.Add(1);
                list.Add(2);
                list.Add(3);
                list.Add(4);
                list.Add(5);


                //Console.WriteLine(list[3]);
                foreach (var i in list)
                {
                    Console.WriteLine(i);
                }
                Console.ReadLine();

            }

            public class MyList<T> : IEnumerable<T>
            {
                private T[] _arrayValue = new T[2];

                private int count = 0;
                public T this[int _index]
                {
                    get
                    {
                        return _arrayValue[_index];
                    }
                    set
                    {
                        _arrayValue[_index] = value;
                    }
                }
                //public void Add(T item)
                //{
                //    var arrayTemp = new T[_arrayValue.Length + 1];
                //    for (int i = 0; i < _arrayValue.Length; i++)
                //    {
                //        arrayTemp[i] = _arrayValue[i];
                //    }
                //    arrayTemp[_arrayValue.Length] = item;
                //    _arrayValue = arrayTemp;
                //}
                public void Add(T item)
                {
                    // if array fills up, double its size
                    if (count == _arrayValue.Length)
                        Array.Resize(ref _arrayValue, _arrayValue.Length * 2);

                    // use POST increment, because we need to increment the count variable AFTER 
                    // specifying the index where we store the new element
                    _arrayValue[count++] = item;
                }
                public IEnumerator<T> GetEnumerator()
                {

                    return new MyListIterator(this);
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return new MyListIterator(this);
                }

                public int Count { get { return _arrayValue.Length; } }
                private class MyListIterator : IEnumerator<T>
                {

                    private int index = -1;
                    private MyList<T> _list;
                    public MyListIterator(MyList<T> _mylist)
                    {
                        _list = _mylist;
                    }
                    public T Current
                    {
                        get
                        {
                            if (index < 0 || _list.count <= index)
                                throw new IndexOutOfRangeException("The index was outside of the valid range");
                            else
                                return _list._arrayValue[index];
                        }
                    }

                    object IEnumerator.Current
                    {
                        get { return Current; }
                    }

                    public void Dispose()
                    {
                        index = -1;
                        //Console.WriteLine("This is where I should dispose myself.");
                    }

                    public bool MoveNext()
                    {
                        return ++index < _list.count;
                    }

                    public void Reset()
                    {
                        index = -1;
                        // throw new NotSupportedException();
                    }
                }

            }
        }
    }
}