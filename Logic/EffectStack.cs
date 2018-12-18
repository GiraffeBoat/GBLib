using System;

namespace GBLib
{
	public class EffectStack
	{

		private object effect;
		private int total;

		public int Count { get { return total; } }

		public EffectStack (object thing)
		{
			effect = thing;
		}

		public object GetEffect() {
			return effect;
		}

		public bool AddStack(int count=1) {
			total += count;
			return true;
		}

		public bool RemoveStack(int count=1) {
			if (total == 0) {
				return false;
			} else if (total < count) {
				total = 0;
				return true;
			} else {
				total -= count;
				return true;
			}
		}
			

	}
}

