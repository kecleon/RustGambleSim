using System;
using System.Collections.Generic;

namespace RustWheel
{
	class RustWheel
	{
		enum Colors
		{
			Yellow = 1,
			Green = 3,
			Blue = 5,
			Purple = 10,
			Red = 20
		}

		enum Strategy
		{
			squadBPR,
			AllOnYellow,
			AllOnGreen,
			AllOnBlue,
			AllOnPurple,
			AllOnRed,
			BlueAndPurple,
			PurpleAndRed,
			BlueAndRed,
			ProportionalBPRHigh,
			ProportionalBPRLow,
			AllFive,
			AllFiveHigh,
			AllFiveLow,
		}

		static readonly Colors[] Values = 
		{
			Colors.Red,
			Colors.Yellow,
			Colors.Green,
			Colors.Yellow,
			Colors.Blue,
			Colors.Yellow,
			Colors.Green,
			Colors.Yellow,
			Colors.Purple,
			Colors.Yellow,
			Colors.Green,
			Colors.Yellow,
			Colors.Blue,
			Colors.Yellow,
			Colors.Blue,
			Colors.Green,
			Colors.Yellow,
			Colors.Purple,
			Colors.Yellow,
			Colors.Green,
			Colors.Yellow,
			Colors.Blue,
			Colors.Yellow,
			Colors.Green,
			Colors.Yellow,
		};

		static readonly Random _random = new();

		static void Main(string[] args)
		{
			/*
			 * 1 red
			 * 2 purple
			 * 4 blue
			 * 6 green
			 * 12 yellow
			 */

			const long startingScrap = 1000000;
			var strategies = new Dictionary<Strategy, long>
			{
				{Strategy.squadBPR, startingScrap},
				{Strategy.AllOnYellow, startingScrap},
				{Strategy.AllOnGreen, startingScrap},
				{Strategy.AllOnBlue, startingScrap},
				{Strategy.AllOnPurple, startingScrap},
				{Strategy.AllOnRed, startingScrap},
				{Strategy.BlueAndPurple, startingScrap},
				{Strategy.PurpleAndRed, startingScrap},
				{Strategy.BlueAndRed, startingScrap},
				{Strategy.ProportionalBPRHigh, startingScrap},
				{Strategy.ProportionalBPRLow, startingScrap},
				{Strategy.AllFive, startingScrap},
				{Strategy.AllFiveHigh, startingScrap},
				{Strategy.AllFiveLow, startingScrap},
			};

			for (int i = 0; i < 1000000000; i++)
			{
				var winner = Spin();
				strategies[Strategy.squadBPR] = Gamble(winner, strategies[Strategy.squadBPR], 0, 0, 10, 10, 10);
				strategies[Strategy.AllOnYellow] = Gamble(winner, strategies[Strategy.AllOnYellow], 30, 0, 0, 0, 0);
				strategies[Strategy.AllOnGreen] = Gamble(winner, strategies[Strategy.AllOnGreen], 0, 30, 0, 0, 0);
				strategies[Strategy.AllOnBlue] = Gamble(winner, strategies[Strategy.AllOnBlue], 0, 0, 30, 0, 0);
				strategies[Strategy.AllOnPurple] = Gamble(winner, strategies[Strategy.AllOnPurple], 0, 0, 0, 30, 0);
				strategies[Strategy.AllOnRed] = Gamble(winner, strategies[Strategy.AllOnRed], 0, 0, 0, 0, 30);
				strategies[Strategy.BlueAndPurple] = Gamble(winner, strategies[Strategy.BlueAndPurple], 0, 0, 15, 15, 0);
				strategies[Strategy.PurpleAndRed] = Gamble(winner, strategies[Strategy.PurpleAndRed], 0, 0, 0, 15, 15);
				strategies[Strategy.BlueAndRed] = Gamble(winner, strategies[Strategy.BlueAndRed], 0, 0, 15, 0, 15);
				strategies[Strategy.ProportionalBPRHigh] = Gamble(winner, strategies[Strategy.ProportionalBPRHigh], 0, 0, 5, 10, 15);
				strategies[Strategy.ProportionalBPRLow] = Gamble(winner, strategies[Strategy.ProportionalBPRLow], 0, 0, 15, 10, 5);
				strategies[Strategy.AllFive] = Gamble(winner, strategies[Strategy.AllFive], 6, 6, 6, 6, 6);
				strategies[Strategy.AllFiveHigh] = Gamble(winner, strategies[Strategy.AllFive], 2, 4, 6, 8, 10);
				strategies[Strategy.AllFiveLow] = Gamble(winner, strategies[Strategy.AllFive], 10, 8, 6, 4, 2);
			}

			foreach (var strategy in strategies)
			{
				Console.WriteLine($"{strategy.Key}\t{strategy.Value}");
			}
		}

		static long Gamble(Colors winner, long scrap, int yellow, int green, int blue, int purple, int red)
		{
			scrap -= yellow + green + blue + purple + red;
			
			switch (winner)
			{
				case Colors.Yellow:
					scrap += yellow + yellow;
					break;
				case Colors.Green:
					scrap += green * 3 + green;
					break;
				case Colors.Blue:
					scrap += blue * 5 + blue;
					break;
				case Colors.Purple:
					scrap += purple * 10 + purple;
					break;
				case Colors.Red:
					scrap += red * 20 + red;
					break;
			}

			return scrap;
		}

		static Colors Spin()
		{
			return Values[_random.Next(Values.Length)];
		}
	}
}