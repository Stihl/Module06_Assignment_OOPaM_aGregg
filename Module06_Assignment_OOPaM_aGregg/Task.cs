using System;
using System.Collections.Generic;
using System.Text;

namespace Module06_Assignment_OOPaM_aGregg
{
    //MY EXAMPLE OF CLASSES
    class Task
    {
        //Properties
        public static int ID_HOLD { get; protected set; } = 0; // A static property to be called by each new Task instance
        public int ID { get; protected set; } = 0;
        public string name { get; set; } = "";
        public string description { get; set; } = "";
        public DateTime created { get; protected set; }
        public DateTime goal { get; set; }  
        public bool finished { get; set; } = false;
        public string taskType { get; protected set; } = "standard";

        //Constructors
        //Default contructor
        public Task()
        {
            //More than one task will have the same ID but no unique task will share the same id
            ID = ID_HOLD;
            
        }
        //Overloaded constructor to prevent excess IDs from being generated when old ones are "erased".
        public Task(int old)
        {
            ID = old;

        }
        //Overloaded constructor
        public Task(string t_name, string t_desc)
        {
            ID = Gen_ID();

            name = t_name;
            description = t_desc;
            created = DateTime.Now;
        }


        //protected static memeber method for creating the next ID
        protected static int Gen_ID()
        {
            //Generates the ID for the next task
            return ID_HOLD += 1;
        }

        //Public member methods
        public void Rename(string t_rename)
        {
            name = t_rename;
        }
        public void Redescribe(string t_redescription)
        {
            description = t_redescription;
        }

    }

    //MY EXAMPLE OF INHERITANCE
    //The inherited TimedTask class inherits all Task properties and methods (no constructors though)
    class TimedTask : Task
    {
        
        //A constructor for an inherited class
        public TimedTask(string t_name, string t_desc, DateTime t_goal)
        {
            ID = Gen_ID();
            name = t_name;
            description = t_desc;
            created = DateTime.Now;
            goal = t_goal;
            taskType = "timed";
        }

        //Heres an example of a public method accessing an otherwise protected property
        //Might normally extend the date by setting a further date rather than adding time.
        public void ExtendGoal(TimeSpan time)
        {
            goal.Add(time);
        }
        public void FinishGoal()
        {
            finished = true;
        }
    }
}
