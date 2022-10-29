using System;

namespace Incapsulation.Weights
{
	//Класс индексатора по массиву, то есть выдающий элементы массива начинающий с заданного
	class Indexer
	{
		//Сам массив, по которому мы смотрим данные
		private double[] _weights;
		//Метод проверки на корректность индекса в массиве
		private  void ValidateIndex(int index)
		{
			//Если индекс отрицательные или больше либо равен длине массиве, тогда бросаем исключение
			if (index < 0 || index >= Length)
				throw new IndexOutOfRangeException("Index must be more than zero and less than array length");
		}
		//Индексатор по массиву. Метод, позволяющий получать или изменять значения по индексу в массиве
		public  double this[int index]
		{
			//Получение элемента по индексу
			get
			{
				//Проверяем индекс
				ValidateIndex(index);
				//Выдём элемент массива, начиная со стартовой на index позиций
				return _weights[_start+index];
			}
			//Изменение элемента массива
			set
			{
				// Проверяем индекс
				ValidateIndex(index);
				//Присваеыем значение жлементу стоящему на index позиции, начиная со start
				_weights[_start+ index] = value;
			}
		}
		// Свойство длины массива, доступное только для чтения
		public int Length { get; }
		//Стартовая позиция подмассива
		private int _start;
		//Конструктор для создание объекта-позмассива, принимающий исходный массив,
		//индекс начала подмассива, а так же количество элементов в подмассиве
		public Indexer(double[] weights, int start, int length)
		{
			//Если стартовый индекс меньше 0 или больше длины исходного массива, если длина подмассива
			//больше длины исходного массива, если длина массива меньше 0 или если стартовый индекс плюс
			//требуемая длина подмассива больше длины исходного подмассива(то есть пытаемся получить подмассив
			//больше, чем длина исходного массива), то бросаем исключение
			if (start < 0 || start > weights.Length
				|| length > weights.Length || start+length>weights.Length || length<0)
				throw new ArgumentException("start and length must be less than array length");
			//Присваивание массива
			_weights = weights;
			//Присваивание длины масива
			Length = weights.Length;
			//Присваивание стартового индекса
			_start = start;
			//Обновление длины массива
			Length = length;

		}
	}
	
}
