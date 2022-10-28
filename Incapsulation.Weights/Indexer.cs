using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Weights
{
	class Indexer
	{
		private double[] _weights;
		private  void ValidateIndex(int index)
		{
			if (index < 0 || index >= Length)
				throw new IndexOutOfRangeException("Index must be more than zero and less than array length");
		}
		public  double this[int index]
		{
			get
			{
				ValidateIndex(index);
				return _weights[_start+index];
			}
			set
			{
				ValidateIndex(index);
				_weights[_start+ index] = value;
			}
		}
		public int Length { get; }
		private int _start;
	
		public Indexer(double[] weights, int start, int length)
		{
			if (start < 0 || start > weights.Length
				|| length > weights.Length || start+length>weights.Length || length<0)
				throw new ArgumentException("start and length must be less than array length");
			_weights = weights;
			Length = weights.Length;
			_start = start;
			Length = length;

		}
	}
	
}
