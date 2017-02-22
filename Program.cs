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

		//Mostly for testing, prints the deck 
		public void PrintDeck()
		{
			for (int i = 0; i < group.Count; i++)
			{
				Console.WriteLine(group[i]);
			}
		}



		//Shuffles all of the cards in the deck. Uses the swap method to exchange 2 randomly selected cards.
		public void Shuffle()
		{
			//500 shuffles ensures the deck is incredibly shuffled
			for (int x = 0; x < 500; x++)
			{
				//Runs through each element in the array and swaps it with a randomly selected one
				for (int i = 0; i < group.Count; i++)
				{
					Random rnd = new Random();
					int j = rnd.Next(52);
					//Swap group[i] and group[j]
					Swap(i, j);
				}
			}
		}
		//Swaps 2 cards in the deck group based on their indexes
		private void Swap(int a, int b)
		{
			Card temp = group[a];
			group[a] = group[b];
			group[b] = temp;
		}

		//Removes the card at the last index of the deck and returns it
		public Card Deal()
		{
			Card c = group[group.Count-1];
			group.RemoveAt(group.Count-1);
			return c;
		}
	}

	class Hand
	{
		private List<Card> group;

		public Hand()
		{ 
			group = new List<Card>();
		}

		public void add(Card c)
		{
			group.Add(c);
		}

		public Card RemoveAtIndex(int i)
		{
			Card c = group[i];
			group.RemoveAt(i);
			return c;
		}

		public void removeIfSame()
		{
			int i = 0;
			while (i != group.Count)
			{
				bool found = false;
				int j = i + 1;
				while (!found && j != group.Count)
				{
					if (group[i].getValue() == group[j].getValue())
					{
						found = true;
						group.RemoveAt(i);
						group.RemoveAt(j - 1);
					}
					j++;
				}
				i++;
			}
		}

		public void PrintHand()
		{
			for (int i = 0; i < group.Count; i++)
			{
				Console.WriteLine(group[i]);
			}
		}

		public void TestHand()
		{
			Hand h = new Hand();
			h.add(new Card("diamond", 3));
			h.add(new Card("diamond", 7));
			h.add(new Card("hearts ", 14));
			h.add(new Card("clubs", 3));
			h.add(new Card("spades", 14));
			h.add(new Card("hearts", 8));
			PrintHand();
			h.removeIfSame();
			h.PrintHand();
		}
	}

	class MainClass
	{
		public static void Main(string[] args)
		{
			Hand h = new Hand();
			//Create a new deck that gets shuffled and create user and computer scores
			Deck deck = new Deck();
			deck.Shuffle();
			h.TestHand();

			//int userScore = 0;
			//int computerScore = 0;


		}
	}
}
