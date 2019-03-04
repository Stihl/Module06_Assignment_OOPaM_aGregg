using System;

//TaskBook
//Task tracker for nothing specific
/*  Homework Index:
 * ARRAY - Line: 27 Array of Task Objects
 * METHODS - This class Lines: 60, 88, 108, 147. In Task.cs Lines: 46, 53, 57, 81, 85
 * CLASSES - Task.cs file Lines: 8 and 65
 * INHERITANCE - Task.cs file Line: 65
 * TRY-CATCH - Lines: 142-165
 * RECURSION - Line: starts at 70 -- ShowTasksMethod
 */

namespace Module06_Assignment_OOPaM_aGregg
{
    class Program
    {

        static void Main(string[] args)
        {
            bool openBook = true;

            //Heres an example of Arrays and polymorphism
            //IMO: Very messy, declaring tons of empty task objects that might not be used.
            //Also limits how many tasks can be used.
            Task[] tasks = new Task[100];
            tasks[0] = new Task();
            tasks[1] = new Task("Zimbabwe", "Fly to zimbabwe");
            tasks[2] = new TimedTask("Finish Game UI", "Properly implement buttons and menus systems", DateTime.Now.Add(new TimeSpan(32, 0, 0, 0)));
            tasks[3] = new Task("Read Book", "Read that one book you havent picked up in over 5 years.");
            

            Task teesk = new TimedTask("blah", "polymorphism can get nasty in languages other than python, and even then....",DateTime.Now.Add(new TimeSpan(4,12,32,1)));

            tasks[0] = teesk;   //Replacing the first empty task with a TimedTask.

            int inpt_num = 0;
            while (openBook)
            {
                Console.WriteLine("\n\n==========Task Book==========");
                Console.WriteLine("\tView Tasks: 0\n\tAdd Task: 1\n\tAdd Timed Task: 2\n\tRemove task: 3\n\tQuit: 4");
                //Fails neatly
                int.TryParse(Console.ReadLine(),out inpt_num);
                switch (inpt_num)
                {
                    case 0:
                        int i = 0;
                        ShowTasks(tasks, i);
                        break;
                    case 1:
                        AddTask(ref tasks);
                        break;
                    case 2:
                        AddTimedTask(ref tasks);
                        break;
                    case 3:
                        RemoveTask(ref tasks);
                        break;
                    case 4:
                        openBook = false;
                        break;
                    default:
                        Console.WriteLine("Choice invalid, please try again.");
                        break;
                }
            }
            
        }

        //RECURSION EXAMPLE
        public static void ShowTasks(Task[] tasks, int i)
        {
            
            if(i < tasks.Length)
            {
                
            
                if (tasks[i] != null)
                {
                    if (tasks[i].name != "")
                    {
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine($"Task ID: {tasks[i].ID.ToString()}\nTaskName: {tasks[i].name}\nTaskDescription:\n\t{tasks[i].description}\nDate Created: {tasks[i].created.ToString()}");

                        //A very backwards way of working around the polymorphism problem while avoiding multiple arrays.
                        //Rather than using if t.getType() == TimedTask, im stuck adding a property to all task objects
                        if (tasks[i].taskType == "timed")

                        {
                            Console.WriteLine($"Finish by: {tasks[i].goal.ToString()}");
                            if (tasks[i].finished != true)
                            {
                                Console.WriteLine("Incomplete");
                            }
                            else
                            {
                                Console.WriteLine("Complete");
                            }
                        }
                    }
                }
                i++;
                ShowTasks(tasks, i);
            }
            
        }
        public static void AddTask(ref Task[] tasks)
        {
            string t_name = "";
            string t_desc = "";

            Console.Write("Your Task: ");
            t_name = Console.ReadLine();
            Console.Write("Task description: ");
            t_desc = Console.ReadLine();

            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i] == null)
                {
                    tasks[i] = new Task(t_name, t_desc);
                    break;
                }else if(tasks[i].name == "")
                {
                    tasks[i] = new Task(t_name, t_desc);
                    break;
                }
            }

        }
        public static void AddTimedTask(ref Task[] tasks)
        {

            DateTime t_goal = DateTime.Now;
            TimeSpan time;
            int days = 0;
            string t_name = "";
            string t_desc = "";

            // THE TRY CATCH EXAMPLE
            
            try
            {
                Console.Write("Your Task: ");
                t_name = Console.ReadLine();
                Console.Write("Task description: ");
                t_desc = Console.ReadLine();
                Console.Write("How many days until the task should be finished?: ");
            
                int.TryParse(Console.ReadLine(), out days);
                time = new TimeSpan(days, 0, 0, 0);
                t_goal = DateTime.Now.Add(time);
            }
            catch (TimeZoneNotFoundException tznfe)
            {
                throw tznfe;
            }
            catch (System.IO.IOException ioe)
            {
                Console.WriteLine($"{ioe.Message}\n Input was incorrect, please try again.");
            }
            catch (Exception e)
            {
                throw e;
            }

            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i] == null)
                {
                    tasks[i] = new TimedTask(t_name, t_desc, t_goal);
                    break;
                }else if (tasks[i].name == "")
                {
                    tasks[i] = new TimedTask(t_name, t_desc, t_goal);
                    break;
                }
            }
        }
        //The tasks array will never get smaller but we can atleast substitute the tasks
        public static void RemoveTask(ref Task[] tasks)
        {
            int id;
            Console.Write("ID of task to delete: ");

            int.TryParse(Console.ReadLine(),out id);

            for (int i = 0; i < tasks.Length; i++)
            {
                if(tasks[i].ID == id)
                {
                    tasks[i] = new Task(id);
                    break;
                }
                else if(tasks[i].ID != id && i == tasks.Length - 1)
                {
                    Console.WriteLine("There are no tasks with that ID\nPlease check the task ID again.");
                }
            }
        }
    }
}
