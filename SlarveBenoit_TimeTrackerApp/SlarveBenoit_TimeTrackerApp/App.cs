using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace SlarveBenoit_TimeTrackerApp
{
    class App
    {
        string cs = @"server=172.16.6.1; userid=bestAdmin; password=password; database=SlarveBenoit_MDV229_Database_201803; port=8889";
        MySqlConnection conn = null;
        Menu myMenu = new Menu();

        public App()
        {
            
            myMenu = new Menu("Enter Activity", "View Tracked Data", "Run Calculations", "EXIT");
            myMenu.Title = "Hello Name, What Would You Like To Do Today?";
            myMenu.Display();
            Select();

        }

        bool running = true;
        private void Select()
        {
            while (running)
            {
                Console.WriteLine("=============================");
                int selection = Utilities.ValidateInt("Make selection:");
                switch (selection)
                {
                    case 1:
                        EnterActivity();
                        EnterActivitySecond();
                        EnterActivityThird();
                        EnterActivityFourth();
                        Console.WriteLine("Your Activity has been added");
                        Console.ReadKey();
                        myMenu.Display();
                        Select();
                        break;
                    case 2:
                        TrackedData();
                        break;
                    case 3:
                        Calculations();
                        break;
                    case 4:
                        Console.WriteLine("Exiting program...");
                        running = false;
                        Console.ReadKey();
                        Environment.Exit(1);
                        break;
                }
                running = false;
            }
                       
        }

        void TrackedData()


        void TrackedData()
        {
            Menu trackedDataMenu = new Menu("Select By Date", "Select By Category", "Select By Description", "Back");
            trackedDataMenu.Title = "View Tracked Data:";
            trackedDataMenu.Display();
            secondSelect();

            void secondSelect()
            {
                Console.WriteLine("=============================");
                int selection = Utilities.ValidateSpecialInt4("Make selection:");
                switch (selection)
                {
                    case 1:
                        SelectByDate();
                        break;
                    case 2:
                        SelectByCategory();
                        break;
                    case 3:
                        SelectByDescription();
                        break;
                    case 4:
                        myMenu.Display();
                        Select();
                        break;
                }
                Console.ReadKey();
                trackedDataMenu.Display();
                secondSelect();
            }
        }
        void SelectByDate()
        {
            Console.Clear();
            Console.WriteLine("View By Dates");
            Console.WriteLine("=============================");
            conn = new MySqlConnection(cs);
            MySqlDataReader rdo = null;
            conn.Open();

            string sto = "SELECT * from tracked_calendar_dates";
            MySqlCommand cmo = new MySqlCommand(sto, conn);
            string versiono = Convert.ToString(cmo.ExecuteScalar());

            rdo = cmo.ExecuteReader();
            while (rdo.Read())
            {
                Console.WriteLine("" + rdo.GetString(1));
                //Console.ReadKey();
            }
            Console.WriteLine("=============================");
            string Stringselection1 = Utilities.ValidateString("Type a Date:");
            if (Stringselection1 == "")
            {

            }
        }
        void SelectByCategory()
        {
            Console.Clear();
            Console.WriteLine("View By Categories");
            Console.WriteLine("=============================");
            conn = new MySqlConnection(cs);
            MySqlDataReader rdr = null;
            conn.Open();

            string stm = "SELECT * from activity_categories";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            string version = Convert.ToString(cmd.ExecuteScalar());

            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("" + rdr.GetString(1));
                //Console.ReadKey();
            }
            Console.WriteLine("=============================");
            string Stringselection = Utilities.ValidateString("Type a Category of Activity:");
            if (Stringselection == "Class")
            {

            }
        }
        void SelectByDescription()
        {

            Console.Clear();
            Console.WriteLine("View By Descriptions");
            Console.WriteLine("=============================");
            conn = new MySqlConnection(cs);
            MySqlDataReader rdo = null;
            conn.Open();

            string sto = "SELECT * from activity_descriptions";
            MySqlCommand cmo = new MySqlCommand(sto, conn);
            string versiono = Convert.ToString(cmo.ExecuteScalar());

            rdo = cmo.ExecuteReader();
            while (rdo.Read())
            {
                Console.WriteLine("" + rdo.GetString(1));
                //Console.ReadKey();
            }
            Console.WriteLine("=============================");
            string Stringselection1 = Utilities.ValidateString("Type a Description:");
            if (Stringselection1 == "Church")
            {

            }
        }
        
        void Calculations()
        {
            Menu calculationMenu = new Menu("Time spent trading", "Time spent eating breakfast", "Time spent driving to school", "Total time spent playing Fifa 19", "Percent of time spent doing homework", "Total time spent going to church", "Time spent eating dinner", "Percent of time spent practicing the guitar", "Total time spent with family", "Back");
            calculationMenu.Title = "Calculations:";
            calculationMenu.Display();
            secondSelect();

            void secondSelect()
            {
                Console.WriteLine("=============================");
                int selection = Utilities.ValidateSpecialInt10("Make selection:");
                switch (selection)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:

                        break;
                    case 8:

                        break;
                    case 9:

                        break;
                    case 10:
                        myMenu.Display();
                        Select();
                        break;
                }
                Console.ReadKey();
                calculationMenu.Display();
                secondSelect();
            }
        }

        void EnterActivity()
        {
            Console.Clear();
            Console.WriteLine("Categories");
            Console.WriteLine("=============================");
            conn = new MySqlConnection(cs);
            MySqlDataReader rdr = null;
            conn.Open();

            string stm = "SELECT * from activity_categories";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            string version = Convert.ToString(cmd.ExecuteScalar());

            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("" + rdr.GetString(1));
                //Console.ReadKey();
            }           
            Console.WriteLine("=============================");
            string Stringselection = Utilities.ValidateString("Type a Category of Activity:");
            
                     
        }
        void EnterActivitySecond()
        {
            Console.Clear();
            Console.WriteLine("Descriptions");
            Console.WriteLine("=============================");
            conn = new MySqlConnection(cs);
            MySqlDataReader rdo = null;
            conn.Open();

            string sto = "SELECT * from activity_descriptions";
            MySqlCommand cmo = new MySqlCommand(sto, conn);
            string versiono = Convert.ToString(cmo.ExecuteScalar());

            rdo = cmo.ExecuteReader();
            while (rdo.Read())
            {
                Console.WriteLine("" + rdo.GetString(1));
                //Console.ReadKey();
            }
            Console.WriteLine("=============================");
            string Stringselection1 = Utilities.ValidateString("Type a Description:");
            if (Stringselection1 == "Church")
            {

            }
        }
        void EnterActivityThird()
        {
            Console.Clear();
            Console.WriteLine("Dates");
            Console.WriteLine("=============================");
            conn = new MySqlConnection(cs);
            MySqlDataReader rdo = null;
            conn.Open();

            string sto = "SELECT * from tracked_calendar_dates";
            MySqlCommand cmo = new MySqlCommand(sto, conn);
            string versiono = Convert.ToString(cmo.ExecuteScalar());

            rdo = cmo.ExecuteReader();
            while (rdo.Read())
            {
                Console.WriteLine("" + rdo.GetString(1));
                //Console.ReadKey();
            }
            Console.WriteLine("=============================");
            string Stringselection2 = Utilities.ValidateString("Type a Date:");
            if (Stringselection2 == "")
            {

            }
        }
        void EnterActivityFourth()
        {
            Console.Clear();
            Console.WriteLine("Times");
            Console.WriteLine("=============================");
            conn = new MySqlConnection(cs);
            MySqlDataReader rdo = null;
            conn.Open();

            string sto = "SELECT * from activity_times";
            MySqlCommand cmo = new MySqlCommand(sto, conn);
            string versiono = Convert.ToString(cmo.ExecuteScalar());

            rdo = cmo.ExecuteReader();
            while (rdo.Read())
            {
                Console.WriteLine("" + rdo.GetString(1));
                //Console.ReadKey();
            }
            Console.WriteLine("=============================");
            string Stringselection3 = Utilities.ValidateString("Type How long you spent on activity:");
            if (Stringselection3 == "")
            {

            }
        }
        
    }

    void EnterActivity()
    {
        Console.Clear();
        Console.WriteLine("Categories");
        Console.WriteLine("=============================");
        conn = new MySqlConnection(cs);
        MySqlDataReader rdr = null;
        conn.Open();

        string stm = "SELECT * from activity_categories";
        MySqlCommand cmd = new MySqlCommand(stm, conn);
        string version = Convert.ToString(cmd.ExecuteScalar());

        rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            Console.WriteLine("" + rdr.GetString(1));
            //Console.ReadKey();
        }
        Console.WriteLine("=============================");
        string Stringselection = Utilities.ValidateString("Type a Category of Activity:");


    }
    void EnterActivitySecond()
    {
        Console.Clear();
        Console.WriteLine("Descriptions");
        Console.WriteLine("=============================");
        conn = new MySqlConnection(cs);
        MySqlDataReader rdo = null;
        conn.Open();

        string sto = "SELECT * from activity_descriptions";
        MySqlCommand cmo = new MySqlCommand(sto, conn);
        string versiono = Convert.ToString(cmo.ExecuteScalar());

        rdo = cmo.ExecuteReader();
        while (rdo.Read())
        {
            Console.WriteLine("" + rdo.GetString(1));
            //Console.ReadKey();
        }
        Console.WriteLine("=============================");
        string Stringselection1 = Utilities.ValidateString("Type a Description:");
        if (Stringselection1 == "Church")
        {

        }
    }
    void EnterActivityThird()
    {
        Console.Clear();
        Console.WriteLine("Dates");
        Console.WriteLine("=============================");
        conn = new MySqlConnection(cs);
        MySqlDataReader rdo = null;
        conn.Open();

        string sto = "SELECT * from tracked_calendar_dates";
        MySqlCommand cmo = new MySqlCommand(sto, conn);
        string versiono = Convert.ToString(cmo.ExecuteScalar());

        rdo = cmo.ExecuteReader();
        while (rdo.Read())
        {
            Console.WriteLine("" + rdo.GetString(1));
            //Console.ReadKey();
        }
        Console.WriteLine("=============================");
        string Stringselection2 = Utilities.ValidateString("Type a Date:");
        if (Stringselection2 == "")
        {

        }
    }
    void EnterActivityFourth()
    {
        Console.Clear();
        Console.WriteLine("Times");
        Console.WriteLine("=============================");
        conn = new MySqlConnection(cs);
        MySqlDataReader rdo = null;
        conn.Open();

        string sto = "SELECT * from activity_times";
        MySqlCommand cmo = new MySqlCommand(sto, conn);
        string versiono = Convert.ToString(cmo.ExecuteScalar());

        rdo = cmo.ExecuteReader();
        while (rdo.Read())
        {
            Console.WriteLine("" + rdo.GetString(1));
            //Console.ReadKey();
        }
        Console.WriteLine("=============================");
        string Stringselection3 = Utilities.ValidateString("Type How long you spent on activity:");
        if (Stringselection3 == "")
        {

        }
    }

}
}
