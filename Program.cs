using System;

namespace CardGame
{
	class Card
	{
		private string suit;
		private int value; //Jack = 11, Queen = 12, King = 13, Ace = 14
		private Card(string s, int v)
		{
			suit = s;
			value = v;
		}
		public Boolean valueEquals(Card c)
		{
			if (value == c.getValue())
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public string getSuit()
		{
			return suit;
		}
		public int getValue()
		{
			return value;
		}
		public void setSuit(string s)
		{
			suit = s;
		}
		public void setValue(int v)
		{
			value = v;
		}
	}


	class MainClass
	{
		public static void Main(string[] args)
		{
			
		}
	}
}
