using System;
using System.Collections.Generic;
using System.Text;

namespace Delegates.Observers
{
	public delegate void StackChangedEventHandler<T>(object sender, StackEventData<T> data);
	public class StackOperationsLogger
	{
		

		private readonly StringBuilder _log = new StringBuilder();
		private void OnStackChanged<T>(object sender, StackEventData<T> eventData) => _log.Append(eventData);

		public void SubscribeOn<T>(ObservableStack<T> stack)
		{
			stack.StackChanged += OnStackChanged;
		}

		public string GetLog()
		{
			return _log.ToString();
		}



		public class ObservableStack<T>
		{

			public event StackChangedEventHandler<T> StackChanged;
			private readonly List<T> _data = new List<T>();

			public void Push(T obj)
			{
				_data.Add(obj);
				StackChanged?.Invoke(this, new StackEventData<T> { IsPushed = true, Value = obj });
			}

			public T Pop()
			{
				if (_data.Count == 0)
					throw new InvalidOperationException();
				var result = _data[_data.Count - 1];
				StackChanged?.Invoke(this, new StackEventData<T> { IsPushed = false, Value = result });
				return result;

			}
		}
	}


}
