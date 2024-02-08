using System;
using System.Collections.Generic;
using System.IO;

public class EternalQuestProgram
{
    // Define an enumeration for goal types
    public enum GoalType
    {
        Simple,
        Eternal,
        Checklist
    }

    // Define a class to represent a goal
    public class Goal
    {
        public string Name { get; set; }
        public int PointsPerCompletion { get; set; }
        public int TargetCompletionCount { get; set; }
        public int BonusPoints { get; set; }
        public int CompletionCount { get; set; }
        public GoalType Type { get; set; }
        public int Points { get; set; }
        public bool IsOpen { get; set; } = true;

        // Parameterless constructor for simple goals
        public Goal()
        {
            Name = "Default Goal";
            Type = GoalType.Simple;
        }
        // Constructor for eternal goals
        public Goal(string name, int pointsPerCompletion, GoalType type)
        {
            Name = name;
            PointsPerCompletion = pointsPerCompletion;
            TargetCompletionCount = 0;
            BonusPoints = 0;
            CompletionCount = 0;
            Type = type;
        }

        // Constructor for checklist goals
        public Goal(string name, int pointsPerCompletion, int targetCompletionCount, int bonusPoints, GoalType type)
        {
            Name = name;
            PointsPerCompletion = pointsPerCompletion;
            TargetCompletionCount = targetCompletionCount;
            BonusPoints = bonusPoints;
            CompletionCount = 0;
            Type = type;
        }
        public bool IsCompleted => CompletionCount >= TargetCompletionCount;
    }

    // Define a class to represent a user
    public class User
    {
        public string UserName { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
        public List<Goal> Goals { get; set; }

        public User(string userName)
        {
            UserName = userName;
            Points = 0;
            Level = 1;
            Goals = new List<Goal>();
        }
        public void CompleteGoal(Goal goal)
        {
            if (!goal.IsCompleted)
            {
                goal.CompletionCount++;
                Points += goal.PointsPerCompletion;

                Console.WriteLine($"{UserName} recorded progress on the goal: {goal.Name} and earned {goal.PointsPerCompletion} points!");

                if (goal.IsCompleted)
                {
                    goal.Points += goal.BonusPoints;
                    Points += goal.BonusPoints;

                    Console.WriteLine($"{UserName} achieved the goal: {goal.Name} and earned a bonus of {goal.BonusPoints} points!");
                    Console.WriteLine($"You earned {goal.PointsPerCompletion + goal.BonusPoints} points in total!");

                    // Added line to display earned points for simple goals
                    if (goal.Type == GoalType.Simple)
                    {
                        Console.WriteLine($"You earned {goal.PointsPerCompletion} points for completing the simple goal!");
                    }

                    CheckLevelUp();
                }
            }
            else
            {
                Console.WriteLine($"{UserName} has already completed the goal: {goal.Name}");
            }
        }

        private void CheckLevelUp()
        {
            // Check if the user should level up based on points
            // You can customize the leveling up criteria as per your gamification design
            if (Points >= 1000)
            {
                Level++;
                Console.WriteLine($"{UserName} leveled up to Level {Level}!");
            }
        }

        public void DisplayScore()
        {
            Console.WriteLine($"{UserName}'s Total Score: {Points}");
        }
        public void CreateGoal()
        {
            Console.WriteLine("Create a new goal:");

            Console.WriteLine("The types of Goals are:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");

            Console.Write("Which type of goal would you like to create? ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        CreateSimpleGoal();
                        break;
                    case 2:
                        CreateEternalGoal();
                        break;
                    case 3:
                        CreateChecklistGoal();
                        break;
                    default:
                        Console.WriteLine("Invalid goal type.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid goal type.");
            }
        }
        private void CreateSimpleGoal()
        {
            Console.Write("What is the name of your goal? ");
            string name = Console.ReadLine();

            Console.Write("What is a short description of it? ");
            string description = Console.ReadLine();

            Console.Write("What is the amount of points associated with this goal? ");
            if (int.TryParse(Console.ReadLine(), out int pointsPerCompletion))
            {
                Goal simpleGoal = new Goal
                {
                    Name = name,
                    PointsPerCompletion = pointsPerCompletion,
                    Type = GoalType.Simple,
                    IsOpen = true  // Set the goal as open initially
                };
                Goals.Add(simpleGoal);
                Console.WriteLine($"Simple goal '{name}' created successfully! Description: {description}");
            }
            else
            {
                Console.WriteLine("Invalid points value. Goal creation failed.");
            }
        }

        private void CreateEternalGoal()
        {
            Console.Write("What is the name of your goal? ");
            string name = Console.ReadLine();

            Console.Write("What is a short description of it? ");
            string description = Console.ReadLine();

            Console.Write("What is the amount of points associated with each completion? ");
            if (int.TryParse(Console.ReadLine(), out int pointsPerCompletion))
            {
                Goal eternalGoal = new Goal(name, pointsPerCompletion, GoalType.Eternal);
                Goals.Add(eternalGoal);
                Console.WriteLine($"Eternal goal '{name}' created successfully! Description: {description}");
            }
            else
            {
                Console.WriteLine("Invalid points value. Goal creation failed.");
            }
        }
        private void CreateChecklistGoal()
        {
            Console.Write("What is the name of your goal? ");
            string name = Console.ReadLine();

            Console.Write("What is a short description of it? ");
            string description = Console.ReadLine();

            Console.Write("What is the amount of points associated with this goal? ");
            if (int.TryParse(Console.ReadLine(), out int pointsPerCompletion))
            {
                Console.Write("Enter the target completion count: ");
                if (int.TryParse(Console.ReadLine(), out int targetCompletionCount))
                {
                    Console.Write("Enter the bonus points upon completion: ");
                    if (int.TryParse(Console.ReadLine(), out int bonusPoints))
                    {
                        Goal checklistGoal = new Goal(name, pointsPerCompletion, targetCompletionCount, bonusPoints, GoalType.Checklist);
                        Goals.Add(checklistGoal);
                        Console.WriteLine($"Checklist goal '{name}' created successfully! Description: {description}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid bonus points value. Goal creation failed.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid target completion count value. Goal creation failed.");
                }
            }
            else
            {
                Console.WriteLine("Invalid points value. Goal creation failed.");
            }
        }


        public void RecordEvent()
        {
            Console.WriteLine($"Your Goals:");
            DisplayGoals();

            Console.Write("Select a goal to record an event:\nEnter the number corresponding to the goal: ");

            if (int.TryParse(Console.ReadLine(), out int goalNumber) && goalNumber >= 1 && goalNumber <= Goals.Count)
            {
                Goal selectedGoal = Goals[goalNumber - 1];

                if (selectedGoal.Type == GoalType.Simple)
                {
                    if (selectedGoal.IsOpen)
                    {
                        RecordEventForSimpleGoal(selectedGoal);
                    }
                    else
                    {
                        Console.WriteLine($"{UserName}'s goal '{selectedGoal.Name}' is closed and cannot be recorded.");
                    }
                }
                else
                {
                    // For other goal types, allow recording events without checking completion status
                    RecordEventForGoal(selectedGoal);
                }
            }
            else
            {
                Console.WriteLine("Invalid goal number. Event recording failed.");
            }
        }
        private void RecordEventForGoal(Goal goal)
        {
            // Allow recording events without checking if the goal is completed
            goal.CompletionCount++;  // Increment completion count
            Points += goal.PointsPerCompletion;  // Award points

            Console.WriteLine($"{UserName} recorded progress on the goal: {goal.Name} and earned {goal.PointsPerCompletion} points!");

            if (goal.Type == GoalType.Checklist)
            {
                // Check completion condition for checklist goals
                if (goal.CompletionCount >= goal.TargetCompletionCount)
                {
                    goal.Points += goal.BonusPoints;
                    Points += goal.BonusPoints;

                    Console.WriteLine($"{UserName} achieved the goal: {goal.Name} and earned a bonus of {goal.BonusPoints} points!");
                    Console.WriteLine($"You earned {goal.PointsPerCompletion + goal.BonusPoints} points in total!");

                    Console.WriteLine($"You earned {goal.PointsPerCompletion} points for completing the goal!");

                    CheckLevelUp();

                    // Close the checklist goal after completion
                    goal.IsOpen = false;
                }
                else
                {
                    Console.WriteLine($"{UserName} recorded progress on the goal: {goal.Name} and earned {goal.PointsPerCompletion} points!");
                }
            }
            else if (goal.IsCompleted)
            {
                goal.Points += goal.BonusPoints;
                Points += goal.BonusPoints;

                Console.WriteLine($"{UserName} achieved the goal: {goal.Name} and earned a bonus of {goal.BonusPoints} points!");
                Console.WriteLine($"You earned {goal.PointsPerCompletion + goal.BonusPoints} points in total!");

                Console.WriteLine($"You earned {goal.PointsPerCompletion} points for completing the goal!");

                CheckLevelUp();
            }
        }


        private void RecordEventForSimpleGoal(Goal simpleGoal)
        {
            // Allow recording events only for open simple goals
            RecordEventForGoal(simpleGoal);
        }
        public void DisplayGoals()
        {
            Console.WriteLine($"Goals for {UserName}:");
            for (int i = 0; i < Goals.Count; i++)
            {
                var goal = Goals[i];
                string completionStatus = goal.IsCompleted ? "[X]" : "[ ]";

                if (goal.Type == GoalType.Simple)
                {
                    string goalStatus = goal.IsOpen ? completionStatus : "[Completed]";
                    Console.WriteLine($"{i + 1}. {goal.Name} - Type: {goal.Type} - {goalStatus}");
                }
                else if (goal.Type == GoalType.Checklist)
                {
                    Console.WriteLine($"{i + 1}. {goal.Name} - Type: {goal.Type} - {completionStatus} Completed {goal.CompletionCount}/{goal.TargetCompletionCount} times");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {goal.Name} - Type: {goal.Type} - {completionStatus}");
                }
            }
        }
        public void SaveToFile(string fileName)
        {
            try
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                using (StreamWriter writer = new StreamWriter(fullPath))
                {
                    writer.WriteLine($"UserName: {UserName}");
                    writer.WriteLine($"Points: {Points}");
                    writer.WriteLine($"Level: {Level}");
                    writer.WriteLine("Goals:");

                    foreach (var goal in Goals)
                    {
                        string goalType = goal.Type.ToString();
                        string isActive = goal.Type == GoalType.Simple ? goal.IsOpen.ToString() : "False";


                        // Include the isActive value for simple goals
                        writer.WriteLine($"{goal.Name},{goal.PointsPerCompletion},{goal.TargetCompletionCount},{goal.BonusPoints},{goal.CompletionCount},{goalType},{isActive}");
                    }
                }

                Console.WriteLine($"Data saved successfully. File saved at: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }


        public static User LoadFromFile(string fileName)
        {
            try
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                if (File.Exists(fullPath))
                {
                    User loadedUser = new User("DefaultUser"); // You may want to set a default username
                    using (StreamReader reader = new StreamReader(fullPath))
                    {
                        loadedUser.UserName = GetValueFromLine(reader.ReadLine(), "UserName");
                        loadedUser.Points = int.Parse(GetValueFromLine(reader.ReadLine(), "Points"));
                        loadedUser.Level = int.Parse(GetValueFromLine(reader.ReadLine(), "Level"));

                        // Read and parse goals
                        string line;
                        while ((line = reader.ReadLine()) != null && line != "Goals:")
                        {
                            // Ignore other lines until "Goals:" is reached
                        }

                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] goalData = line.Split(',');

                            if (goalData.Length == 7)
                            {
                                GoalType goalType;
                                if (Enum.TryParse(goalData[5], out goalType))
                                {
                                    string isActive = goalData[6];
                                    Goal goal;
                                    switch (goalType)
                                    {
                                        case GoalType.Simple:
                                            goal = new Goal(goalData[0], int.Parse(goalData[1]), GoalType.Simple)
                                            {
                                                IsOpen = bool.Parse(isActive)
                                            };
                                            break;
                                        case GoalType.Eternal:
                                            goal = new Goal(goalData[0], int.Parse(goalData[1]), GoalType.Eternal);
                                            break;
                                        case GoalType.Checklist:
                                            goal = new Goal(goalData[0], int.Parse(goalData[1]), int.Parse(goalData[2]), int.Parse(goalData[3]), GoalType.Checklist);
                                            break;
                                        default:
                                            goal = null;
                                            break;
                                    }

                                    if (goal != null)
                                    {
                                        goal.CompletionCount = int.Parse(goalData[4]);
                                        loadedUser.Goals.Add(goal);
                                    }
                                }
                            }

                        }
                    }

                    Console.WriteLine($"Data loaded successfully from: {fullPath}");
                    return loadedUser;
                }
                else
                {
                    Console.WriteLine($"No saved data found at: {fullPath}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                return null;
            }
        }

        private static string GetValueFromLine(string line, string key)
        {
            return line?.StartsWith(key) == true ? line.Split(':')[1].Trim() : null;
        }
        public void ActivateSimpleGoals()
        {
            Console.WriteLine("Inactive Simple Goals:");

            // Filter inactive simple goals
            var inactiveSimpleGoals = Goals.Where(g => g.Type == GoalType.Simple && !g.IsOpen).ToList();

            if (inactiveSimpleGoals.Any())
            {
                // Display a list of inactive simple goals
                for (int i = 0; i < inactiveSimpleGoals.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {inactiveSimpleGoals[i].Name}");
                }

                // Ask the user which goal to activate
                Console.Write("Enter the number corresponding to the goal to activate: ");

                if (int.TryParse(Console.ReadLine(), out int goalNumber) && goalNumber >= 1 && goalNumber <= inactiveSimpleGoals.Count)
                {
                    Goal selectedGoal = inactiveSimpleGoals[goalNumber - 1];

                    // Activate the selected goal
                    selectedGoal.IsOpen = true;
                    Console.WriteLine($"Activated simple goal: {selectedGoal.Name}");
                }
                else
                {
                    Console.WriteLine("Invalid goal number. Activation canceled.");
                }
            }
            else
            {
                Console.WriteLine("No inactive simple goals found to activate.");
            }
        }

    }
    public static void Main(string[] args)
    {
        User user = new User("Player1");

        while (true)
        {
            // Display user's current points before the menu
            Console.WriteLine($"You have {user.Points} points.\n");
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("1. Create a New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Activate simple goals again");
            Console.WriteLine("7. Quit");

            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    user.CreateGoal();
                    break;
                case "2":
                    user.DisplayGoals();
                    break;
                case "3":
                    Console.Write("What is the filename for the goal file? ");
                    string fileName = Console.ReadLine();

                    if (!fileName.EndsWith(".txt"))
                    {
                        fileName += ".txt";
                    }

                    user.SaveToFile(fileName);
                    break;

                case "4":
                    Console.Write("Enter the filename for the goal file: ");
                    string loadFileName = Console.ReadLine();

                    // Append ".txt" extension if not provided
                    if (!loadFileName.EndsWith(".txt"))
                    {
                        loadFileName += ".txt";
                    }

                    User loadedUser = User.LoadFromFile(loadFileName);
                    if (loadedUser != null)
                    {
                        user = loadedUser;
                        Console.WriteLine("Goals loaded successfully!");
                    }
                    break;

                case "5":
                    user.RecordEvent();
                    break;
                case "7":
                    Console.WriteLine("Quitting the program. Goodbye!");
                    return;
                case "6":
                    user.ActivateSimpleGoals();
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                    break;
            }

            // Display user's updated status after each operation
            Console.WriteLine($"{user.UserName}'s Points: {user.Points}, Level: {user.Level}");
        }
    }

}
