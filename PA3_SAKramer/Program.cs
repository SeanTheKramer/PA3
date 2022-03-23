using System;
using System.IO;

namespace PA3
{
    class Program
    {
        static void Main(string[] args)
        {
            int forcewon = 0;
            int forcelost = 0;
            int blasterswon = 0;
            int blasterslost = 0;
            int credits = 50;

            
            string userInput = GetMenuChoice(ref credits);                      // gets menu choice from user
            
            while (userInput != "4")                                            // as long as input is not 4, will select a game or scoreboard
            {
                Route(userInput, ref forcewon, ref forcelost, ref blasterswon, ref blasterslost, ref credits);                    
                userInput = GetMenuChoice(ref credits);                         // after coming back from a game or scoreboard
            }  
             
            
            Goodbye(ref forcewon, ref forcelost, ref blasterswon, ref blasterslost, ref credits);   // end of the game


            
            
        }
        static string GetMenuChoice(ref int credits)
        {
            if (credits < 0)
            {
                System.Console.WriteLine("You have lost. Better luck next time Luke!");
                System.Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                return "4";
            }
            if (credits > 300)
            {
                System.Console.WriteLine("You have won! Good job Luke!");
                System.Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                return "4";
            }
            else
            {
                DisplayMenu();                                          
                string userInput = Console.ReadLine();

                while (!ValidMenuChoice(userInput))
                {
                    Console.WriteLine("Invalid menu choice.  Please Enter a Valid Menu Choice");
                    Console.WriteLine("Press any key to continue....");
                    Console.ReadKey();

                    DisplayMenu();
                    userInput = Console.ReadLine();
                }

            return userInput;
            }
        }
        static void DisplayMenu()                                           // Displays Menu options
        {
            Console.Clear();
            Yoda();                                                                                 // draws Yoda
            Console.WriteLine("\nLuke Skywalker, welcome! An option below, please select.");
            Console.WriteLine("1:   Play The Force");
            Console.WriteLine("2:   Play Blasters");
            Console.WriteLine("3:   See Scoreboard");
            Console.WriteLine("4:   Exit Game");
        }
        static bool ValidMenuChoice(string userInput)           // Checks for a valid input
        {
            if(userInput == "1")
            {
                return true;    //Good choice
            }

            else if(userInput == "2")
            {
                return true;    // good choice
            }

            else if(userInput == "3")
            {
                return true;    // good choice
            }

            else if(userInput == "4")
            {
                return true;    // good choice
            }

            else
            {
                return false;   // bad choice
            }
        }
        static void Route(string userInput, ref int forcewon, ref int forcelost, ref int blasterswon, ref int blasterslost, ref int credits)
        {
            if(userInput == "1")
            {
                ForceConfirm(ref forcewon, ref forcelost, ref credits);                 // Takes to The Force
            }

            else if(userInput == "2")
            {
                BlastersConfirm(ref blasterswon, ref blasterslost, ref credits);        // Takes to Blasters 
            }
            else if(userInput == "3")
            {
                ScoreBoard(ref forcewon, ref forcelost, ref blasterswon, ref blasterslost, ref credits);    // Takes to ScoreBoard
            }

        }
        static void ForceConfirm(ref int forcewon, ref int forcelost, ref int credits)          // Confirmation before playing The Force
        {
            Console.Clear();
            System.Console.WriteLine("Yoda will lay out a row of 10 cards. The game starts by Luke being shown a random card from the deck.");
            System.Console.WriteLine("Luke then must guess if the next card will be under that amount or over that amount. (Example: Luke drew a seven, he then has to guess if the next card is greater or less than seven)");
            System.Console.WriteLine("Each time Luke guesses correctly, he moves on to the next card in his row of ten");
            System.Console.WriteLine("If Luke makes it through all ten, he triples his credits");
            System.Console.WriteLine("If Luke makes it through seven, but not all ten, he doubles his credits");
            System.Console.WriteLine("If Luke makes it through five, but not seven, he breaks even");
            System.Console.WriteLine("If Luke loses before getting to 5, he loses the credits he bet");
            System.Console.WriteLine("Important to remember: in this game, Aces are considered the lowest card.");
            System.Console.WriteLine("Would you like to play The Force?");
            System.Console.WriteLine("1. Yes");
            System.Console.WriteLine("2. No");
            string answer = Console.ReadLine();
            if (answer == "1")
            {
                TheForce(ref forcewon, ref forcelost, ref credits);
            }

        }
        static void BlastersConfirm(ref int blasterswon, ref int blasterslost, ref int credits)           // Confirmation before playing Blasters
        {
            Console.Clear();
            System.Console.WriteLine("In this game, Yoda shoots lasers at Luke with a blaster. It is Luke's job to either dodge or \ndeflect the laser to not get hit");
            System.Console.WriteLine("For this game, you start off with 15 points, and each time you get hit you lose 5 points, each \ntime you deflect you get 10, and each time you dodge you get 5 points");
            System.Console.WriteLine("Dodging has a 50% chance of success and Deflecting has a 30% chance of success");
            System.Console.WriteLine("Luke should be able to decide if he wants to dodge, deflect, or quit out of the game.");
            System.Console.WriteLine("To play this game, he must bet at least 20 credit");
            System.Console.WriteLine("Luke loses when he hits 0 points");
            System.Console.WriteLine("Luke wins when he gets 40 points");
            System.Console.WriteLine("Would you like to play Blasters?");
            System.Console.WriteLine("1. Yes");
            System.Console.WriteLine("2. No");
            string answer = Console.ReadLine();
            if (answer == "1")
            {
                Blasters(ref blasterswon, ref blasterslost, ref credits);
            }
        }
        static void TheForce(ref int forcewon, ref int forcelost, ref int credits)           // The Force Game
        {
            Console.Clear();
            int wager = ForceWager(ref credits);
            const int TOTAL_CARDS= 52;
            string[] deck = new string[TOTAL_CARDS];
            TheDeck(deck);
            Shuffle(deck, TOTAL_CARDS);
            int guesses = 0;
            int missed = 0;
            int i = 0;
            while(KeepGoing(ref missed, ref guesses))                                                   // Keeps playing the game
            {
                DisplayCards(deck, i);
                Console.WriteLine($"The current card is: {deck[i]} \nis the next card higher or lower?");
                System.Console.WriteLine("1. Higher \n2. Lower");
                System.Console.WriteLine("Correct guesses: " + guesses);
                string guess = Console.ReadLine();
                if (CheckChoice(deck, guess, ref guesses, ref missed, i))
                {                    
                    i++;
                }
            }
            if (guesses == 10)                                                                          // For 10 correct guesses
            {
                System.Console.WriteLine("PERFECT! \nYou tripled your wager! \nPress any key to continue . . .");
                credits = credits + wager + wager + wager;
                forcewon++;
                Console.ReadKey();
                Console.Clear();
                if (credits != 0)
                {
                    ReplayForce(ref forcewon, ref forcelost, ref credits);
                }
            }
            if (missed != 0)            // For a missed guess
            {
                if (guesses < 5)                //for less than 5 correct
                {
                    System.Console.WriteLine("You lost! \n You lose your wager. \nPress any key to continue . . .");
                    credits = credits - wager;
                    forcelost++;
                    Console.ReadKey();
                    Console.Clear();
                    if (credits != 0)
                    {
                        ReplayForce(ref forcewon, ref forcelost, ref credits);
                    }
                }
                if (guesses == 5 || guesses == 6)           // for between 5 and 6 correct
                {
                    System.Console.WriteLine("You did okay, I guess. \nYou broke even. \nPress any key to continue . . .");
                    credits = credits;
                    Console.ReadKey();
                    Console.Clear();
                    if (credits != 0)
                    {
                        ReplayForce(ref forcewon, ref forcelost, ref credits);
                    }
                }
                if (guesses == 7 || guesses == 8 || guesses == 9)           // for between 7 and 9 correct
                {
                    System.Console.WriteLine("You did great! \nYou doubled your wager. \nPress any key to continue . . .");
                    credits = credits + wager + wager;
                    forcewon++;
                    Console.ReadKey();
                    Console.Clear();
                    if (credits != 0)
                    {
                        ReplayForce(ref forcewon, ref forcelost, ref credits);
                    }
                }
            }
        }
        static int ForceWager(ref int credits)
        {
            bool confirm = false;
            System.Console.WriteLine("How many credits would you like to wager?");      // Requests wager
            System.Console.WriteLine($"You currently have {credits} credits.");
            int wager = int.Parse(Console.ReadLine());
            while (!confirm)
            {
                if (wager > credits)
                {
                    System.Console.WriteLine("Invalid! Your wager must not be greater than your current credits! \nPlease try again.");               // Bad Wager
                    System.Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    System.Console.WriteLine("How many credits would you like to wager?");              // Requests wager
                    System.Console.WriteLine($"You currently have {credits} credits.");
                    wager = int.Parse(Console.ReadLine());
                }
                else if (wager <= 0)
                {
                    System.Console.WriteLine("Invalid! Your wager must not be less than or equal to 0 credits! \nPlease try again.");               // Bad Wager
                    System.Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    System.Console.WriteLine("How many credits would you like to wager?");              // Requests wager
                    System.Console.WriteLine($"You currently have {credits} credits.");
                    wager = int.Parse(Console.ReadLine());
                }
                else if (wager <= credits)                                              // Good Wager
                {
                    confirm = true;
                    Console.Clear();
                }
                else
                {
                    System.Console.WriteLine("Invalid input! \nPlease try again.");
                    System.Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    System.Console.WriteLine("How many credits would you like to wager?");              // Requests wager
                    System.Console.WriteLine($"You currently have {credits} credits.");
                    wager = int.Parse(Console.ReadLine());
                }
            }
            return wager;
        }
        static bool CheckChoice(string[] deck, string guess, ref int guesses, ref int missed, int i)        // checks if choice is correct, incorrect, or invalid
        {
            int current = CurrentCard(deck, i);
            int next = NextCard(deck, i);
            Console.Clear();
            

            if (guess == "1")               // if guess is higher
            {
                if (next > current)                             // if higher is correct
                {
                    FaceDown();
                    System.Console.WriteLine($"The next card is . . . ");
                    System.Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    DisplayNextCards(deck, i);
                    System.Console.WriteLine($"The {deck[i+1]}!");
                    System.Console.WriteLine("Good job! \nPress any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    guesses++;
                    return true;
                }
                else                                            // if higher is incorrect
                {
                    FaceDown();
                    System.Console.WriteLine($"The next card is . . . ");
                    System.Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    DisplayNextCards(deck, i);
                    System.Console.WriteLine($"The {deck[i+1]}!");
                    System.Console.WriteLine("Incorrect! \nPress any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    missed++;
                    return false;
                }
            }
            if (guess == "2")                   // if guess is lower
            {
                if (next < current)                                // if lower is correct
                {
                    FaceDown();
                    System.Console.WriteLine($"The next card is . . . ");
                    System.Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    DisplayNextCards(deck, i);
                    System.Console.WriteLine($"The {deck[i+1]}!");
                    System.Console.WriteLine("Good job! \nPress any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    guesses++;
                    return true;
                }
                else                                               // if lower is incorrect
                {
                    FaceDown();
                    System.Console.WriteLine($"The next card is . . . ");
                    System.Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    DisplayNextCards(deck, i);
                    System.Console.WriteLine($"The {deck[i+1]}!");
                    System.Console.WriteLine("Incorrect! \nPress any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    missed++;
                    return false;
                }
            }
            else                                                    // if choice is invalid
            {
                System.Console.WriteLine("Invalid input, please try again \nPress any key to continue . . .");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
        }
        static void TheDeck(string[] deck)                          // creates the deck
        {
            string[] numbers = {"Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King"};
            string[] suits = {"Hearts", "Diamonds", "Clubs", "Spades"};

           
            
            int count = 0;
            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < numbers.Length; j++)
                {
                    deck[count] = numbers[j] + " of " + suits[i];
                    count++;
                }
            }
        }
        static void Shuffle(string[] deck, int TOTAL_CARDS)     // shuffles the deck by swapping
        {
            for(int i = 0; i < deck.Length; i++)
            {
                Random rnd = new Random();
                int j = rnd.Next(TOTAL_CARDS);
                string temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
            // for(int i = 0; i < 11; i++)                      // testing The Force, shows next 10 cards
            // {
            //     Console.WriteLine(deck[i]);
            // }
        }
        static bool KeepGoing(ref int missed, ref int guesses)  // game keeps going unless guesses = 10 or missed = 1
        {
            if (guesses == 10 || missed > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        static void FaceDown()                                  // draws unflipped card
        {
            Console.WriteLine("┌───────────┐");
            Console.WriteLine("│░░░░░░░░░░░│");
            Console.WriteLine("│░░░░░░░░░░░│");
            Console.WriteLine("│░░░░░░░░░░░│");
            Console.WriteLine("│░░░░░░░░░░░│");
            Console.WriteLine("│░░░░░░░░░░░│");
            Console.WriteLine("│░░░░░░░░░░░│");
            Console.WriteLine("│░░░░░░░░░░░│");
            Console.WriteLine("└───────────┘");
        }
        static int CurrentCard(string[] deck, int i)    // assigns number to current card
        {
            if (deck[i].Contains("Ace"))
            {
                return 1;
            }
            else if(deck[i].Contains("Two"))
            {
                return 2;
            }
            else if(deck[i].Contains("Three"))
            {
                return 3;
            }
            else if(deck[i].Contains("Four"))
            {
                return 4;
            }
            else if(deck[i].Contains("Five"))
            {
                return 5;
            }
            else if(deck[i].Contains("Six"))
            {
                return 6;
            }
            else if(deck[i].Contains("Seven"))
            {
                return 7;
            }
            else if(deck[i].Contains("Eight"))
            {
                return 8;
            }
            else if(deck[i].Contains("Nine"))
            {
                return 9;
            }
            else if(deck[i].Contains("Ten"))
            {
                return 10;
            }
            else if(deck[i].Contains("Jack"))
            {
                return 11;
            }
            else if(deck[i].Contains("Queen"))
            {
                return 12;
            }
            else if(deck[i].Contains("King"))
            {
                return 13;
            }
            else                                // will not be reached
            {
                return 0;
            }
        }
        static int NextCard(string[] deck, int i)           // assigns number to next card
        {
            if (deck[i+1].Contains("Ace"))
            {
                return 1;
            }
            else if(deck[i+1].Contains("Two"))
            {
                return 2;
            }
            else if(deck[i+1].Contains("Three"))
            {
                return 3;
            }
            else if(deck[i+1].Contains("Four"))
            {
                return 4;
            }
            else if(deck[i+1].Contains("Five"))
            {
                return 5;
            }
            else if(deck[i+1].Contains("Six"))
            {
                return 6;
            }
            else if(deck[i+1].Contains("Seven"))
            {
                return 7;
            }
            else if(deck[i+1].Contains("Eight"))
            {
                return 8;
            }
            else if(deck[i+1].Contains("Nine"))
            {
                return 9;
            }
            else if(deck[i+1].Contains("Ten"))
            {
                return 10;
            }
            else if(deck[i+1].Contains("Jack"))
            {                
                return 11;
            }
            else if(deck[i+1].Contains("Queen"))
            {
                return 12;
            }
            else if(deck[i+1].Contains("King"))
            {
                return 13;
            }
            else                                            // will not be reached
            {
                return 0;
            }
        }
        static void Blasters(ref int blasterswon, ref int blasterslost, ref int credits)     // plays Blasters game
        {
            Console.Clear();
            int wager = BlastersWager(ref credits);
            int points = 15;
            bool quit = false;

            while (points < 40 && points > 0 && !quit)                                  // while points are between 0 and 40, and player has not quit
            {
                Console.WriteLine($"You currently have {points} points.");
                Console.WriteLine("Yoda has shot his blaster at you! \nWill you try to dodge or deflect the laser?");
                Console.WriteLine("1. Dodge");
                Console.WriteLine("2. Deflect");
                Console.WriteLine("3. Quit");
                string guess = Console.ReadLine();
                if (guess == "1")                                                           // chose dodge
                {
                    bool result = CheckDodge();
                    if (result == true)                                                     // dodge successful
                    {
                        Console.WriteLine("Success! You have dodged the laser.");
                        Console.WriteLine("Press any key to continue...");
                        points = points + 5;
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else                                                                    // dodge fail
                    {
                        Console.WriteLine("Fail! You have been hit by the laser.");
                        Console.WriteLine("Press any key to continue...");
                        points = points - 5;
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else if (guess == "2")                                                      // chose deflect
                {
                    bool result = CheckDeflect();
                    if (result == true)                                                     // deflect successful
                    {
                        Console.WriteLine("Success! You have deflected the laser.");
                        Console.WriteLine("Press any key to continue...");
                        points = points + 10;
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else                                                                     // deflect fail
                    {
                        Console.WriteLine("Fail! You have been hit by the laser.");
                        Console.WriteLine("Press any key to continue...");
                        points = points - 5;
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else if (guess == "3")                  // quits game
                {
                    quit = true;
                }
                else                                    // for invalid input
                {
                    System.Console.WriteLine("Invalid input, please try again \nPress any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            if (points <= 0)                            // game was lost
            {
                blasterslost++;
                credits = credits - wager;
                Console.WriteLine("You lost!");
                Console.ReadKey();
                Console.Clear();
                if (credits != 0)
                {
                    ReplayBlasters(ref blasterswon, ref blasterslost, ref credits);
                }
            }
            if (points >= 40)                           // game was won
            {
                credits = credits + wager;
                blasterswon++;
                Console.WriteLine("You won!");
                Console.ReadKey();
                Console.Clear();
                if (credits != 0)
                {
                    ReplayBlasters(ref blasterswon, ref blasterslost, ref credits);
                }
            }
        }
        static int BlastersWager(ref int credits)
        {
            bool confirm = false;
            System.Console.WriteLine("How many credits would you like to wager?");              // Requests wager
            System.Console.WriteLine($"You currently have {credits} credits.");
            int wager = int.Parse(Console.ReadLine());
            while (!confirm)
            {

                if (wager > credits)    
                {
                    System.Console.WriteLine("Invalid! Your wager must not be greater than your current credits! \nPlease try again.");               // Bad Wager
                    System.Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    System.Console.WriteLine("How many credits would you like to wager?");              // Requests wager
                    System.Console.WriteLine($"You currently have {credits} credits.");
                    wager = int.Parse(Console.ReadLine());
                }
                else if (wager < 20)
                {
                    System.Console.WriteLine("Invalid! Your wager must be at least 20 credits! \nPlease try again.");               // Bad wager
                    System.Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    System.Console.WriteLine("How many credits would you like to wager?");              // Requests wager
                    System.Console.WriteLine($"You currently have {credits} credits.");
                    wager = int.Parse(Console.ReadLine());
                }
                else if (wager == 0)
                {
                    System.Console.WriteLine("Invalid! Your wager must not be 0 credits! \nPlease try again.");               // Bad Wager
                    System.Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Console.Clear();
                    System.Console.WriteLine("How many credits would you like to wager?");              // Requests wager
                    System.Console.WriteLine($"You currently have {credits} credits.");
                    wager = int.Parse(Console.ReadLine());
                }
                else if (wager <= credits && wager >= 20)
                {
                    confirm = true;                                                             // Good wager
                    Console.Clear();
                }
            }
            return wager;
        }
        static bool CheckDodge()                // checks if dodge was success or not
        {
            Random rnd = new Random();
            int chance = rnd.Next(99);
            if (chance <= 49)                    // 50% chance
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool CheckDeflect()              // checks if deflect was success or not
        {
            Random rnd = new Random();
            int chance = rnd.Next(99);
            if (chance <= 29)                    // 30% chance
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void ReplayForce(ref int forcewon, ref int forcelost, ref int credits)               // Option to replay The Force
        {
            Console.WriteLine("Would you like to play The Force again? \n1. Yes \n2. No");
            string replayAnswer = Console.ReadLine();
            if (replayAnswer == "1")
            {
                TheForce(ref forcewon, ref forcelost, ref credits);                             // takes back to force confirmation
            }
            if (replayAnswer == "2")                                                                // returns to menu
            {
                
            }
            else                                                                                    // invalid input
            {
                System.Console.WriteLine("Invalid input, please try again. \nPress any key to continue . . .");
                Console.ReadKey();
                Console.Clear();
                ReplayForce(ref forcewon, ref forcelost, ref credits);
            }
        }
        static void ReplayBlasters(ref int blasterswon, ref int blasterslost, ref int credits)      // Option to replay Blasters
        {
            Console.WriteLine("Would you like to play Blasters again? \n1. Yes \n2. No");
            string replayAnswer = Console.ReadLine();
            if (replayAnswer == "1")                                                                // takes back to blasters confirmation
            {
                Blasters(ref blasterswon, ref blasterslost, ref credits);
            }
            if (replayAnswer == "2")                                                                // returns to menu
            {

            }
            else                                                                                    // invalid input
            {
                System.Console.WriteLine("Invalid input, please try again. \nPress any key to continue . . .");
                Console.ReadKey();
                Console.Clear();
                ReplayBlasters(ref blasterswon, ref blasterslost, ref credits);
            }
        }
        static void ScoreBoard(ref int forcewon, ref int forcelost, ref int blasterswon, ref int blasterslost, ref int credits) // shows the scoreboard
        {
            Console.Clear();
            Console.WriteLine($"Current Credits: {credits}");
            Console.WriteLine($"Current wins in The Force: {forcewon} \nCurrent losses in The Force: {forcelost}");
            Console.WriteLine($"Current wins in Blasters: {blasterswon} \nCurrent losses in Blasters: {blasterslost}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        static void Goodbye(ref int forcewon, ref int forcelost, ref int blasterswon, ref int blasterslost, ref int credits)    // end of the game
        {
            Console.Clear();
            Console.WriteLine("Thank you for playing... \n" +
                "Press any key for one final look at the scoreboard" +
                " before you go");
            Console.ReadKey();
            ScoreBoard(ref forcewon, ref forcelost, ref blasterswon, ref blasterslost, ref credits);

        }
        static void Yoda()                                      // extra - displays yoda
        {
            //open
            StreamReader inFile = new StreamReader("yoda.txt");
            //process
            string line = inFile.ReadLine();
            while (line != null)
            {
                System.Console.WriteLine(line);
                line = inFile.ReadLine();
            }
            //close
            inFile.Close();
        }
        static void DisplayCards(string[] deck, int i)          // extra - displays current card 
        {
            
            if (deck[i].Contains("Ace of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│A          │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│          A│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Two of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│2          │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│          2│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Three of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│3          │");
                Console.WriteLine("│  ♥        │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│        ♥  │");
                Console.WriteLine("│          3│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Four of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│4          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          4│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Five of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│5          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          5│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Six of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│6          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          6│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Seven of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│7          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♥  ♥  ♥  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          7│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Eight of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│8          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          8│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Nine of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│9          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          9│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Ten of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│10         │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│         10│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Jack of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│J          │");
                Console.WriteLine("│  ♥♥♥♥♥♥♥  │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│  ♥  ♥     │");
                Console.WriteLine("│   ♥♥♥     │");
                Console.WriteLine("│          J│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Queen of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│Q          │");
                Console.WriteLine("│   ♥♥♥♥♥   │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥  ♥  ♥  │");
                Console.WriteLine("│  ♥   ♥ ♥  │");
                Console.WriteLine("│   ♥♥♥♥ ♥  │");
                Console.WriteLine("│          Q│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("King of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│K          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥    ♥   │");
                Console.WriteLine("│  ♥  ♥     │");
                Console.WriteLine("│  ♥    ♥   │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          K│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Ace of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│A          │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│          A│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Two of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│2          │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│          2│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Three of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│3          │");
                Console.WriteLine("│  ♦        │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│        ♦  │");
                Console.WriteLine("│          3│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Four of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│4          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          4│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Five of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│5          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          5│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Six of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│6          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          6│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Seven of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│7          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♦  ♦  ♦  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          7│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Eight of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│8          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          8│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Nine of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│9          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          9│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Ten of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│10         │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│         10│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Jack of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│J          │");
                Console.WriteLine("│  ♦♦♦♦♦♦♦  │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│  ♦  ♦     │");
                Console.WriteLine("│   ♦♦♦     │");
                Console.WriteLine("│          J│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Queen of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│Q          │");
                Console.WriteLine("│   ♦♦♦♦♦   │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦  ♦  ♦  │");
                Console.WriteLine("│  ♦   ♦ ♦  │");
                Console.WriteLine("│   ♦♦♦♦ ♦  │");
                Console.WriteLine("│          Q│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("King of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│K          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦    ♦   │");
                Console.WriteLine("│  ♦  ♦     │");
                Console.WriteLine("│  ♦    ♦   │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          K│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Ace of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│A          │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│          A│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Two of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│2          │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│          2│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Three of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│3          │");
                Console.WriteLine("│  ♣        │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│        ♣  │");
                Console.WriteLine("│          3│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Four of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│4          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          4│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Five of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│5          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          5│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Six of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│6          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          6│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Seven of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│7          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♣  ♣  ♣  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          7│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Eight of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│8          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          8│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Nine of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│9          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          9│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Ten of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│10         │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│         10│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Jack of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│J          │");
                Console.WriteLine("│  ♣♣♣♣♣♣♣  │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│  ♣  ♣     │");
                Console.WriteLine("│   ♣♣♣     │");
                Console.WriteLine("│          J│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Queen of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│Q          │");
                Console.WriteLine("│   ♣♣♣♣♣   │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣  ♣  ♣  │");
                Console.WriteLine("│  ♣   ♣ ♣  │");
                Console.WriteLine("│   ♣♣♣♣ ♣  │");
                Console.WriteLine("│          Q│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("King of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│K          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣    ♣   │");
                Console.WriteLine("│  ♣  ♣     │");
                Console.WriteLine("│  ♣    ♣   │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          K│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Ace of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│A          │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│          A│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Two of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│2          │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│          2│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Three of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│3          │");
                Console.WriteLine("│  ♠        │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│        ♠  │");
                Console.WriteLine("│          3│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Four of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│4          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          4│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Five of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│5          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          5│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Six of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│6          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          6│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Seven of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│7          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♠  ♠  ♠  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          7│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Eight of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│8          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          8│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Nine of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│9          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          9│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Ten of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│10         │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│         10│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Jack of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│J          │");
                Console.WriteLine("│  ♠♠♠♠♠♠♠  │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│  ♠  ♠     │");
                Console.WriteLine("│   ♠♠♠     │");
                Console.WriteLine("│          J│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Queen of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│Q          │");
                Console.WriteLine("│   ♠♠♠♠♠   │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠  ♠  ♠  │");
                Console.WriteLine("│  ♠   ♠ ♠  │");
                Console.WriteLine("│   ♠♠♠♠ ♠  │");
                Console.WriteLine("│          Q│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("King of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│K          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠    ♠   │");
                Console.WriteLine("│  ♠  ♠     │");
                Console.WriteLine("│  ♠    ♠   │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          K│");
                Console.WriteLine("└───────────┘");
            }
        }
        static void DisplayNextCards(string[] deck, int i)          // extra - displays next card
        {
            if (deck[i+1].Contains("Ace of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│A          │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│          A│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Two of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│2          │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│          2│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Three of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│3          │");
                Console.WriteLine("│  ♥        │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│        ♥  │");
                Console.WriteLine("│          3│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Four of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│4          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          4│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Five of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│5          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          5│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Six of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│6          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          6│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Seven of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│7          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♥  ♥  ♥  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          7│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Eight of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│8          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          8│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Nine of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│9          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          9│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Ten of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│10         │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│         10│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Jack of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│J          │");
                Console.WriteLine("│  ♥♥♥♥♥♥♥  │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│     ♥     │");
                Console.WriteLine("│  ♥  ♥     │");
                Console.WriteLine("│   ♥♥♥     │");
                Console.WriteLine("│          J│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Queen of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│Q          │");
                Console.WriteLine("│   ♥♥♥♥♥   │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥  ♥  ♥  │");
                Console.WriteLine("│  ♥   ♥ ♥  │");
                Console.WriteLine("│   ♥♥♥♥ ♥  │");
                Console.WriteLine("│          Q│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("King of Hearts"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│K          │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│  ♥    ♥   │");
                Console.WriteLine("│  ♥  ♥     │");
                Console.WriteLine("│  ♥    ♥   │");
                Console.WriteLine("│  ♥     ♥  │");
                Console.WriteLine("│          K│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Ace of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│A          │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│          A│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Two of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│2          │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│          2│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Three of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│3          │");
                Console.WriteLine("│  ♦        │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│        ♦  │");
                Console.WriteLine("│          3│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Four of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│4          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          4│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Five of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│5          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          5│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Six of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│6          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          6│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Seven of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│7          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♦  ♦  ♦  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          7│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Eight of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│8          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          8│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Nine of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│9          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          9│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Ten of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│10         │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│         10│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Jack of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│J          │");
                Console.WriteLine("│  ♦♦♦♦♦♦♦  │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│     ♦     │");
                Console.WriteLine("│  ♦  ♦     │");
                Console.WriteLine("│   ♦♦♦     │");
                Console.WriteLine("│          J│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Queen of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│Q          │");
                Console.WriteLine("│   ♦♦♦♦♦   │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦  ♦  ♦  │");
                Console.WriteLine("│  ♦   ♦ ♦  │");
                Console.WriteLine("│   ♦♦♦♦ ♦  │");
                Console.WriteLine("│          Q│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("King of Diamonds"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│K          │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│  ♦    ♦   │");
                Console.WriteLine("│  ♦  ♦     │");
                Console.WriteLine("│  ♦    ♦   │");
                Console.WriteLine("│  ♦     ♦  │");
                Console.WriteLine("│          K│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Ace of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│A          │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│          A│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Two of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│2          │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│          2│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Three of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│3          │");
                Console.WriteLine("│  ♣        │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│        ♣  │");
                Console.WriteLine("│          3│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Four of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│4          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          4│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Five of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│5          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          5│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Six of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│6          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          6│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Seven of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│7          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♣  ♣  ♣  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          7│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Eight of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│8          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          8│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Nine of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│9          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          9│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Ten of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│10         │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│         10│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Jack of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│J          │");
                Console.WriteLine("│  ♣♣♣♣♣♣♣  │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│     ♣     │");
                Console.WriteLine("│  ♣  ♣     │");
                Console.WriteLine("│   ♣♣♣     │");
                Console.WriteLine("│          J│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Queen of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│Q          │");
                Console.WriteLine("│   ♣♣♣♣♣   │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣  ♣  ♣  │");
                Console.WriteLine("│  ♣   ♣ ♣  │");
                Console.WriteLine("│   ♣♣♣♣ ♣  │");
                Console.WriteLine("│          Q│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("King of Clubs"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│K          │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│  ♣    ♣   │");
                Console.WriteLine("│  ♣  ♣     │");
                Console.WriteLine("│  ♣    ♣   │");
                Console.WriteLine("│  ♣     ♣  │");
                Console.WriteLine("│          K│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Ace of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│A          │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│          A│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Two of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│2          │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│          2│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Three of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│3          │");
                Console.WriteLine("│  ♠        │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│        ♠  │");
                Console.WriteLine("│          3│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Four of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│4          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          4│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Five of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│5          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          5│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Six of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│6          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          6│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Seven of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│7          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♠  ♠  ♠  │");
                Console.WriteLine("│           │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          7│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i].Contains("Eight of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│8          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          8│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Nine of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│9          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          9│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Ten of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│10         │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│         10│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Jack of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│J          │");
                Console.WriteLine("│  ♠♠♠♠♠♠♠  │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│     ♠     │");
                Console.WriteLine("│  ♠  ♠     │");
                Console.WriteLine("│   ♠♠♠     │");
                Console.WriteLine("│          J│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("Queen of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│Q          │");
                Console.WriteLine("│   ♠♠♠♠♠   │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠  ♠  ♠  │");
                Console.WriteLine("│  ♠   ♠ ♠  │");
                Console.WriteLine("│   ♠♠♠♠ ♠  │");
                Console.WriteLine("│          Q│");
                Console.WriteLine("└───────────┘");
            }
            if (deck[i+1].Contains("King of Spades"))
            {
                Console.WriteLine("┌───────────┐");
                Console.WriteLine("│K          │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│  ♠    ♠   │");
                Console.WriteLine("│  ♠  ♠     │");
                Console.WriteLine("│  ♠    ♠   │");
                Console.WriteLine("│  ♠     ♠  │");
                Console.WriteLine("│          K│");
                Console.WriteLine("└───────────┘");
            }
        }
    }
}