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
		public bool valueEquals(Card c)
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

		public int removeIfSame()
		{
			int i = 0;
			int numRemoved = 0;
			while (i != group.Count-1)
			{
				Console.WriteLine("i: " + i + " count-1: " + (group.Count-1) + "\n");
				bool found = false;
				int j = i + 1;
				while (!found && j != group.Count)
				{
					Console.WriteLine("j: " + j + "group[i]: " + group[i] + " group[j]: " + group[j]);
					if (group[i].getValue() == group[j].getValue())
					{
						found = true;
						Console.WriteLine("Removing: " + group[i]);
						group.RemoveAt(i);
						Console.WriteLine("Removing: " + group[j-1]);
						group.RemoveAt(j - 1);
						numRemoved++;
						i = -1;
					}
					j++;
				}
				i++;
			}
			return numRemoved;
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
			//Create a new deck that gets shuffled and create user and computer scores and hands
			Deck deck = new Deck();
			deck.Shuffle();
			int userScore = 0;
			//int computerScore = 0;
			Hand userHand = new Hand();
			//Hand computerHand = new Hand();

			//Number of cards in a player's hand to start is 7
			//for (int i = 0; i < 7; i++)
			//{
			//	userHand.add(deck.Deal());
			//	computerHand.add(deck.Deal());
			//}

			userHand.add(new Card("diamonds", 5));
			userHand.add(new Card("spades", 6));
			userHand.add(new Card("spades", 5));
			userHand.add(new Card("diamonds", 6));
			userHand.add(new Card("hearts", 7));
			userHand.add(new Card("clubs", 3));
			userHand.add(new Card("diamonds", 7));
			//User and computer now have hands and scores. We are ready to play
			//First we need to remove cards from the user's hand if it exists,
			//and increase the player's scores respectively
			userHand.PrintHand();
			userScore = userScore + userHand.removeIfSame();
			//computerScore = computerScore + computerHand.removeIfSame();
			//Show the user their hand
			Console.WriteLine("This is your hand and score\nScore: " + userScore);
			userHand.PrintHand();

		}
	}
}
