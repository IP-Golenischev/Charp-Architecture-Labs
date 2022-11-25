using System;
using System.Collections.Generic;
using System.Linq;

namespace Generics.Table
{
    public class MultiIndexesDictionary<T1, T2, T3>
    {
        private static IDictionary<Tuple<T1, T2>, T3> _dictionary;

        public MultiIndexesDictionary()
        {
            _dictionary = new Dictionary<Tuple<T1, T2>, T3>();
            Rows = new HashSet<T1>();
            Columns = new HashSet<T2>();
        }
        public  T3 this[T1 first, T2 second]
        {
            get
            {
                if (Rows.Contains(first) && Columns.Contains(second) && !_dictionary.ContainsKey(new Tuple<T1, T2>(first,second)))
                    return default;
                if (!_dictionary.ContainsKey(new Tuple<T1, T2>(first, second)))
                    throw new ArgumentException();
                return _dictionary[new Tuple<T1, T2>(first, second)];
            } 
            set
            {
                AddRow(first);
                AddColumn(second);
                _dictionary[new Tuple<T1, T2>(first, second)] = value;
            }
        }
        public static HashSet<T1> Rows { get; set; }
        public static HashSet<T2> Columns { get; set; }

        public void AddRow(T1 row)
        {
            if (!Rows.Contains(row))
                Rows.Add(row);
            
        }

        public void AddColumn(T2 column)
        {
            if (!Columns.Contains(column))
                Columns.Add(column);
        }
        
    }
    public class Table<T1, T2, T3>
    {
        public HashSet<T1> Rows => MultiIndexesDictionary<T1,T2,T3>.Rows;
        public HashSet<T2> Columns => MultiIndexesDictionary<T1,T2,T3>.Columns;
        public MultiIndexesDictionary<T1,T2,T3> Open { get; }
        public MultiIndexesDictionary<T1,T2,T3> Existed { get; }
        
        public Table()
        {
            Open = new MultiIndexesDictionary<T1, T2, T3>();
            Existed = new MultiIndexesDictionary<T1, T2, T3>();

        }
        public void AddRow(T1 row) => Open.AddRow(row);
        public void AddColumn(T2 column) => Open.AddColumn(column);


    }
    
}
