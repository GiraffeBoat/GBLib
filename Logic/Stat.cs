using System;

namespace GBLib
{
	public class Stat
	{
		public int Value;
		private int BaseValue { get; } 

		public int Delta { get { return Value - BaseValue; } }

		public Stat (int baseValue)
		{
			BaseValue = baseValue;
			Value = BaseValue;
		}


		public static Stat operator +(Stat s, int i) {
			s.Value += i;
			return s;
		}

		public static Stat operator -(Stat s, int i) {
			s.Value -= i;
			return s;
		}
	}
}

