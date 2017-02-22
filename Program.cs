using System;
using System.Collections.Generic;

namespace CardGame
{
	//suit can either be club, spade, heart, or diamond
	//value is an integer from 2 to 14 inclusive
	//this is a basic card class. suit does not matter for go fish, but is included anyway
	//for the sake of simulating a real card and for potential future use
	class Card
	{
		private string suit;
		private int value; //Jack = 11, Queen = 12, King = 13, Ace = 14
		public Card(string s, int v)
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
		public override string ToString()
		{
			return value + " of " + suit;
		}
	}

	class Deck
	{
		List<Card> group = new List<Card>();
		//A basic deck will have 52 cards, 4 of each suit. 
		public Deck()
		{
			for (int i = 2; i <= 14; i++)
			{
				group.Add(new Card("clubs", i));
				group.Add(new Card("diamonds", i));
				group.Add(new Card("hearts", i));
				group.Add(new Card("spades", i));
			}
		}



		//Method is dedicated to testing if creating a deck works 
		public void TestDeckConstruct()
		{
			for (int i = 0; i < group.Count; i++)
			{
				Console.WriteLine(group[i]);
			}
		}
	}

	class MainClass
	{
		public static void Main(string[] args)
		{
			Deck deck = new Deck();
			deck.TestDeckConstruct();
		}
	}
}
