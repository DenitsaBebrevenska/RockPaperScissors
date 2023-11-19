namespace RockPaperScissors
{
	internal class RockPaperScissors
	{
		static List<string> playerChoices = new List<string>();
		const string Rock = "Rock";
		const string Paper = "Paper";
		const string Scissors = "Scissors";
		static bool restart = true;
		private static int playerScore = 0;
		private static int computerScore = 0;
		static void Main(string[] args)
		{
			while (restart)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("It`s time to play Rock Paper Scissors!");
				Console.Write("Make a choice: [r]ock, [p]aper or [s]cissors:");

				string userInput = Console.ReadLine();
				bool userInputCorrect = CheckUserInput(userInput).success;
				string userChoice = CheckUserInput(userInput).userChoice;
				if (userInputCorrect == false)
				{
					while (userInputCorrect == false)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Invalid input! Try again!");
						Console.WriteLine("Make your choice again: [r]ock, [p]aper or [s]cissors:");
						userInput = Console.ReadLine();
						userInputCorrect = CheckUserInput(userInput).success;
					}
					userChoice = CheckUserInput(userInput).userChoice;
				}
				playerChoices.Add(userChoice);
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine($"You chose: {userChoice}");

				string computerMove = GenerateComputerMove();
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.WriteLine($"The computer chose: {computerMove}");
				ScoringSystem(CheckMoves(userChoice, computerMove));
				RestartOrEndGame();
			}

        }
		static (bool success, string userChoice) CheckUserInput(string userInput)
		{
			if (userInput == "r" || userInput == "R")
			{
				return (true, Rock);
			}
			else if (userInput == "p" || userInput == "P")
			{
				return (true, Paper);
			}
			else if (userInput == "s" || userInput == "S")
			{
				return (true, Scissors);
			}
			else
			{
				return (false, "");
			}
		}
		static string GenerateComputerMove()
		{
			string likelyCounterVsPlayerChoice = AnalizeLastChoices();
			if (playerChoices.Count >= 5 && likelyCounterVsPlayerChoice != null)
			{
				return likelyCounterVsPlayerChoice;
			}
			else
			{
				Random random = new Random();
				int computerRandomNumber = random.Next(1, 4);
				if (computerRandomNumber == 1)
				{
					return Rock;
				}
				else if (computerRandomNumber == 2)
				{
					return Paper;
				}
				else
				{ 
					return Scissors;
				}
			}
		}

		static string AnalizeLastChoices()
		{
			int rockCount = 0, paperCount = 0, scissorsCount = 0;
			foreach (string choice in playerChoices)
			{
				if (choice == Rock)
				{
					rockCount++;
				}
				else if (choice == Paper)
				{
					paperCount++;
				}
				else
				{
					scissorsCount++;
				}
			}
			
			if (rockCount > paperCount && rockCount > scissorsCount)
			{
				return Paper;
			}
			else if (paperCount > rockCount && paperCount > scissorsCount)
			{
				return Scissors;
			}
			else if (scissorsCount > rockCount && scissorsCount > paperCount)
			{
				return Rock;
			}

			return null;

		}

		static string CheckMoves(string userChoice, string computerMove)
		{
			
			if ((userChoice == Rock && computerMove == Scissors) ||
				(userChoice == Paper && computerMove == Rock) ||
				(userChoice == Scissors) && computerMove == Paper)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("You win!");
				return "win";
			}
			else if ((userChoice == Scissors && computerMove == Rock) ||
				(userChoice == Rock && computerMove == Paper) ||
				(userChoice == Paper && computerMove == Scissors))
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("You lose!");
				return "lose";
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("It`s a draw!");
				return "";
			}
			
		}

		static void ScoringSystem(string result)
		{
			if (result == "win")
			{
				playerScore++;
			}
			else if (result == "lose")
			{
				computerScore++;
			}
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"You won: {playerScore} games in total!");
			Console.WriteLine($"The computer won: {computerScore} games in total!");
		}

		static void RestartOrEndGame()
		{
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("Do you wish to continue?");
			Console.WriteLine("Press any key to restart the game or press [q] to quit: ");
			var keyPressed = Console.ReadKey();
			if (keyPressed.KeyChar == 'q' || keyPressed.KeyChar == 'Q')
			{
				restart = false;
			}
			else 
			{
				Console.Clear();
			}
		}
	}
}