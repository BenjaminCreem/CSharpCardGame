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

		public int size()
		{
			return group.Count;
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
			Card c = group[group.Count - 1];
			group.RemoveAt(group.Count - 1);
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
			//Posistion in the List
			int i = 0;
			//Number of Cards removed is the total we need to increase the score by
			int numRemoved = 0;
			while (i != group.Count - 1 && group.Count > 0)
			{
				bool found = false;
				int j = i + 1;
				//If a match is found we need to reset. If not, continue until j cant go any higher
				while ((!found && j < group.Count) && group.Count != 0)
				{
					//If a match is found, remove both cards and set found to true and increase num removed
					if (group[i].getValue() == group[j].getValue())
					{
						found = true;
						Console.WriteLine(group[i] + " matched with " + group[j]);
						group.RemoveAt(i);
						group.RemoveAt(j - 1);
						numRemoved++;
						//i will be incremented after this, so set it to -1 so that it will be incremented to 0
						//afterwards and therefore start at the beginning again. 
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
				Console.WriteLine(i + ": " + group[i]);
			}
		}

		public Card cardAt(int i)
		{
			return group[i];
		}

		public int Size()
		{
			return group.Count;
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
		static Deck deck = new Deck();
		static int userScore = 0;
		static int computerScore = 0;
		static Hand userHand = new Hand();
		static Hand computerHand = new Hand();
		public static void Main(string[] args)
		{
			playGame();
		}


		public static void startGame()
		{ 
			deck.Shuffle();

			//Number of cards in a player's hand to start is 7
			for (int i = 0; i < 7; i++)
			{
				userHand.add(deck.Deal());
				computerHand.add(deck.Deal());
			}
			Console.WriteLine("You are dealt 7 cards. If any of those are matches they are removed and\n" +
							  "then your score is automatically increased.");
			//User and computer now have hands and scores. We are ready to play
			//First we need to remove cards from the user's hand if it exists,
			//and increase the player's scores respectively
			userScore = userScore + userHand.removeIfSame();
			computerScore = computerScore + computerHand.removeIfSame();
		}

		//Starts, plays, and determines winner of a game of go fish
		public static void playGame()
		{
			startGame();
			Console.WriteLine("\n\n\n");
			gamePlay();
			Console.WriteLine("\n");
			determineWinner();
		}

		//Continue to play game game until all cards are gone through the deck, and there are no more cards in anyone's hands
		public static void gamePlay()
		{
			while (userHand.Size() != 0 && computerHand.Size() != 0)
			{
				userTurn();
				Console.WriteLine();
				computerTurn();
			}
		}

		//User turn for a game of go fish
		public static void userTurn()
		{
			//Display user hand and scores
			Console.WriteLine("Current Scores and your hand: \n" +
			                  "Your Score: " + userScore + "\tComputer Score: " + computerScore);
			userHand.PrintHand();
			//Find out which card the user wants to ask the computer about 
			Console.WriteLine("Ask the computer if they have any of your cards by pressing the corresponding number.");
			int userChoice = Convert.ToInt32(Console.ReadLine());
			int i = 0;
			bool found = false;
			//search the computer's hand for the card. 
			while (i < computerHand.Size() && !found)
			{
				if (userHand.cardAt(userChoice).getValue() == computerHand.cardAt(i).getValue())
				{
					found = true;
					Console.WriteLine("Your card matched with the computer's " + computerHand.cardAt(i));
					userScore++;
					userHand.RemoveAtIndex(userChoice);
					computerHand.RemoveAtIndex(i);
					//If the user or computer runs out of cards and the deck still has cards, give them a card
					if (userHand.Size() == 0 && deck.size() > 0)
					{
						Console.WriteLine("You have no more cards in your hand! Giving you one card...");
						userHand.add(deck.Deal());
					}
					if (computerHand.Size() == 0 && deck.size() > 0)
					{
						computerHand.add(deck.Deal());
					}
				}
				i++;
			}
			//Go fish 
			if (!found && deck.size() > 0)
			{
				Console.WriteLine("Go Fish!");
				userHand.add(deck.Deal());
				userScore = userScore + userHand.removeIfSame();
				if (userHand.Size() == 0 && deck.size() > 0)
				{
					Console.WriteLine("You have no more cards in your hand! Giving you one card...");
					userHand.add(deck.Deal());
				}
			}
			else if(deck.size() == 0)
			{
				Console.WriteLine("No more cards in the deck! Can't go fish!");
			}
		}

		//Computer turn for a game of go fish
		public static void computerTurn()
		{
			Random rnd = new Random();
			int computerChoice = rnd.Next(computerHand.Size());
			int i = 0;
			bool found = false;
			while (i < userHand.Size() && !found)
			{
				if (computerHand.cardAt(computerChoice).getValue() == userHand.cardAt(i).getValue())
				{
					found = true;
					computerScore++;
					Console.WriteLine("The computer's " + computerHand.cardAt(computerChoice) + " matched with your " +
									  userHand.cardAt(i) + "!");
					computerHand.RemoveAtIndex(computerChoice);
					userHand.RemoveAtIndex(i);
				}
				if (userHand.Size() == 0 && deck.size() > 0)
				{
					userHand.add(deck.Deal());
				}
				if (computerHand.Size() == 0 && deck.size() > 0)
				{
					computerHand.add(deck.Deal());
				}
				i++;
			}
			if (!found && deck.size() > 0)
			{
				Console.WriteLine("Computer went fish!");
				computerHand.add(deck.Deal());
				computerScore = computerScore + computerHand.removeIfSame();
			}
			if (computerHand.Size() == 0 && deck.size() > 0)
			{
				Console.WriteLine("Computer ran out of cards! Giving computer a card...");
				computerHand.add(deck.Deal());
			}
			else if(deck.size() == 0)
			{
				Console.WriteLine("No more cards in the deck! Cant go fish!");
			}
		}


		//THIS IS A COMMENT
		public static void determineWinner()
		{
			if (userScore > computerScore)
			{
				Console.WriteLine("You win!");
			}
			else if (computerScore > userScore)
			{
				Console.WriteLine("Computer wins!");
			}
			else
			{
				Console.WriteLine("Tie Game!");
			}
			Console.WriteLine("Your score was: " + userScore + " and the computer's was " + computerScore);
		}
	}
}
