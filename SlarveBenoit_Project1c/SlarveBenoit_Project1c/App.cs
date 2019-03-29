using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SlarveBenoit_Project1c
{
    class App
    {
        string cs = @"server=172.16.6.1; userid=bestAdmin; password=password; database=TimeTrackerDatabase; port=8889";
        MySqlConnection conn = null;        
        Menu menu = new Menu();
        public App()
        {

            menu = new Menu("Loggin","Sign Up","Exit Application");
            menu.Title = "My Notes";
            menu.Display();
            Select();
        }
        bool running = true;
        private void Select()
        {
            while (running)
            {
                Console.WriteLine("=============================");
                int selection = Util.ValidateInt("Make selection:");
                switch (selection)
                {
                    case 1:
                        Loggin();
                        break;
                    case 2:
                        SignUp();
                        break;

                    case 3:
                        Console.WriteLine("Exiting program...");
                        running = false;
                        Console.ReadKey();
                        Environment.Exit(1);
                        break;
                }
                running = false;
            }
        }

        string cs = @"server=172.16.6.1; userid=bestAdmin; password=password; database=TimeTrackerDatabase; port=8889";
        MySqlConnection conn = null;
        Menu menu = new Menu();
        public App()
        {

            menu = new Menu("Loggin", "Sign Up", "Exit Application");
            menu.Title = "My Notes";
            menu.Display();
            Select();
        }
        bool running = true;
        private void Select()
        {
            while (running)
            {
                Console.WriteLine("=============================");
                int selection = Util.ValidateInt("Make selection:");
                switch (selection)
                {
                    case 1:
                        Loggin();
                        break;
                    case 2:
                        SignUp();
                        break;

                    case 3:
                        Console.WriteLine("Exiting program...");
                        running = false;
                        Console.ReadKey();
                        Environment.Exit(1);
                        break;
                }
                running = false;
            }
        }
        void Loggin()
        {
            Console.Clear();
            conn = new MySqlConnection(cs);
            MySqlDataReader rdr = null;
            conn.Open();

            Console.WriteLine("Loggin");
            Console.WriteLine("=============================");
            string fullName = Util.ValidateString("Enter your Full Name");
            string email = Util.ValidateString("Email:");
            string password = Util.ValidateString("Password:");
            //where emailAddress = @email and password = @password
            string stm = "SELECT * FROM users where emailAddress = @email and password = @password;";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
            cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;
            string version = Convert.ToString(cmd.ExecuteScalar());

            rdr = cmd.ExecuteReader();
            if (!rdr.HasRows)
            {               
                Console.WriteLine("=============================");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The Email or Password you entered is wrong!");
                Console.WriteLine("Press Enter To Try Again");              
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.ReadKey();
                Loggin();              

            }
            //else if (rdr.HasRows)
            //{
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    Console.WriteLine("LOGGING IN .....");
            //    Console.ReadKey();
            //}
            



            Menu mainPageMenu = new Menu("List of Notes","Search By Keyword","Add New Note","Delete Note","SIGN OUT");
            mainPageMenu.Title = "Main Page";
            mainPageMenu.Display();           

            bool running = true;
            void mainSelect()
            {
                while (running)
                {
                    Console.WriteLine("=============================");
                    int selection = Util.ValidateInt("Make selection:");
                    switch (selection)
                    {
                        case 1:
                            NoteList();
                            break;
                        case 2:
                            Search();
                            break;
                        case 3:
                            AddNote();
                            break;
                        case 4:
                            DeleteNote();
                            break;
                        case 5:
                            Console.WriteLine("Exiting....");
                            menu.Display();
                            Select();

                            break;
                    }
                    running = false;
                }
            }
            mainSelect();
        }
        void SignUp()
        {
            Console.Clear();
            Console.WriteLine("Create Account");
            Console.WriteLine("=============================");
            conn = new MySqlConnection(cs);
            MySqlDataReader rdr = null;
            conn.Open();
            
            string fullName = Util.ValidateString(("Please enter full name"));
            string email = Util.ValidateString(("Please create an email"));           
            string password = Util.ValidateString(("Please create a password"));
           
            string stm = "INSERT INTO users (emailAddress,password,fullName) Values(?,?,?)";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            //cmd.Parameters.Add("id", MySqlDbType.Int32).Value = id;
            cmd.Parameters.Add("emailAddress", MySqlDbType.VarChar).Value = email;
            cmd.Parameters.Add("password", MySqlDbType.VarChar).Value = password;
            cmd.Parameters.Add("fullName", MySqlDbType.VarChar).Value = fullName;
            string version = Convert.ToString(cmd.ExecuteScalar());
            Console.WriteLine("Account Created...");
            Console.ReadKey();
            Loggin();
        }
        void NoteList()
        {
            Console.Clear();
            Console.WriteLine("All Notes");
            Console.WriteLine("=============================");
            conn = new MySqlConnection(cs);
            MySqlDataReader rdr = null;
            conn.Open();

            string stm = "SELECT * from NoteList";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            string version = Convert.ToString(cmd.ExecuteScalar());

            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("" + rdr.GetString(1));              
            }
            //Select a note
            Console.WriteLine("=============================");
            string Stringselection1 = Util.ValidateString("Type a Title to View:");
            if (Stringselection1 == " ")
            {
                Search();//After the user has typed a valid note title search for that tile and display content
            }
            conn = new MySqlConnection(cs);
            MySqlDataReader rdo = null;
            conn.Open();

            string sto = "SELECT * from NoteList where title like '%" + Stringselection1 + "%' limit 100";
            MySqlCommand cmo = new MySqlCommand(sto, conn);
            string versions = Convert.ToString(cmo.ExecuteScalar());

            rdo = cmo.ExecuteReader();
            while (rdo.Read())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(rdo.GetInt32(0) + ": " + rdo.GetString(1) + "| " + rdo.GetDateTime(2) + "| " + rdo.GetString(3));
                //Console.WriteLine("\t |                      |");
            }
            Console.ReadKey();
            // RETURN TO MENU
            Menu mainPageMenu = new Menu("List of Notes", "Search By Keyword", "Add New Note", "Delete Note","SIGN OUT");
            mainPageMenu.Title = "Main Page";
            mainPageMenu.Display();

            bool running = true;
            void mainSelect()
            {
                while (running)
                {
                    Console.WriteLine("=============================");
                    int selection = Util.ValidateInt("Make selection:");
                    switch (selection)
                    {
                        case 1:
                            NoteList();
                            break;
                        case 2:
                            Search();
                            break;
                        case 3:
                            AddNote();
                            break;
                        case 4:
                            DeleteNote();
                            break;
                        case 5:
                            Console.WriteLine("Exiting....");
                            menu.Display();
                            Select();
                            break;
                    }
                    running = false;
                }
            }
            mainSelect();
            
        }
        void Search()
        {
            Console.Clear();
            Console.WriteLine("All Notes");
            Console.WriteLine("=============================");
            conn = new MySqlConnection(cs);
            MySqlDataReader rdr = null;
            conn.Open();

            Console.Write("Search By Title:");
            string searchTerm = Console.ReadLine();

            string stm = "SELECT * from NoteList where title like '%" +searchTerm+ "%' limit 100";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            string version = Convert.ToString(cmd.ExecuteScalar());

            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(rdr.GetInt32(0)+": "+ rdr.GetString(1) +"| "+ rdr.GetDateTime(2) +"| "+ rdr.GetString(3));               
                Console.WriteLine("\t |                      |");
            }
            Console.ReadKey();
            // RETURN TO MENU
            Menu mainPageMenu = new Menu("List of Notes", "Search By Keyword", "Add New Note", "Delete Note","SIGN OUT");
            mainPageMenu.Title = "Main Page";
            mainPageMenu.Display();

            bool running = true;
            void mainSelect()
            {
                while (running)
                {
                    Console.WriteLine("=============================");
                    int selection = Util.ValidateInt("Make selection:");
                    switch (selection)
                    {
                        case 1:
                            NoteList();
                            break;
                        case 2:
                            Search();
                            break;
                        case 3:
                            AddNote();
                            break;
                        case 4:
                            DeleteNote();
                            break;
                        case 5:
                            Console.WriteLine("Exiting....");
                            menu.Display();
                            Select();
                            break;
                    }
                    running = false;
                }
            }
            mainSelect();
        }
        void AddNote()
        {
            Console.Clear();
            Console.WriteLine("New Note");
            Console.WriteLine("=============================");
            conn = new MySqlConnection(cs);
            MySqlDataReader rdr = null;
            conn.Open();

            //string id = Util.ValidateString(("Please enter Id"));
            string title = Util.ValidateString(("Please enter a Title"));
            string date = Util.ValidateString(("Please enter Date Created EX: 2019-03-19"));
            string note = Util.ValidateString(("Please enter Note"));
            
            string stm = "INSERT INTO NoteList (title,dateCreated,noteText) Values(?,?,?)";           
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            //cmd.Parameters.Add("id",MySqlDbType.Int32).Value = id;
            cmd.Parameters.Add("title", MySqlDbType.VarChar).Value = title;
            cmd.Parameters.Add("dateCreated", MySqlDbType.DateTime).Value = date;
            cmd.Parameters.Add("noteText", MySqlDbType.VarChar).Value = note;
            string version = Convert.ToString(cmd.ExecuteScalar());
            Console.WriteLine("Note Was Successfully Added!");
            Console.ReadKey();
            // RETURN TO MENU
            Menu mainPageMenu = new Menu("List of Notes", "Search By Keyword", "Add New Note", "Delete Note","SIGN OUT");
            mainPageMenu.Title = "Main Page";
            mainPageMenu.Display();

            bool running = true;
            void mainSelect()
            {
                while (running)
                {
                    Console.WriteLine("=============================");
                    int selection = Util.ValidateInt("Make selection:");
                    switch (selection)
                    {
                        case 1:
                            NoteList();
                            break;
                        case 2:
                            Search();
                            break;
                        case 3:
                            AddNote();
                            break;
                        case 4:
                            DeleteNote();
                            break;
                        case 5:
                            Console.WriteLine("Exiting....");
                            menu.Display();
                            Select();
                            break;
                    }
                    running = false;
                }
            }
            mainSelect();

        }
        void DeleteNote()
        {
            Console.Clear();
            Console.WriteLine("All Notes");
            Console.WriteLine("=============================");
            conn = new MySqlConnection(cs);
            MySqlDataReader rdr = null;
            conn.Open();

            string stm = "SELECT * from NoteList";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            string version = Convert.ToString(cmd.ExecuteScalar());

            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("" + rdr.GetString(1));
            }
            //Select a note
            Console.WriteLine("=============================");
            string title= Util.ValidateString("Type a Title to Delete:");
            if (title == " ")
            {
                Search();//After the user has typed a valid note title search for that tile and display content
            }
            conn = new MySqlConnection(cs);
            MySqlDataReader rdo = null;
            conn.Open();
            string sto = "DELETE FROM NoteList WHERE title = @title;";
            MySqlCommand cmo = new MySqlCommand(sto, conn);
            cmo.Parameters.Add("@title", MySqlDbType.VarChar).Value = title;
            string versions = Convert.ToString(cmo.ExecuteScalar());
            Console.WriteLine("Note Was Successfully Deleted!");



            Console.ReadKey();

            // RETURN TO MENU
            Menu mainPageMenu = new Menu("List of Notes", "Search By Keyword", "Add New Note", "Delete Note", "SIGN OUT");
            mainPageMenu.Title = "Main Page";
            mainPageMenu.Display();

            bool running = true;
            void mainSelect()
            {
                while (running)
                {
                    Console.WriteLine("=============================");
                    int selection = Util.ValidateInt("Make selection:");
                    switch (selection)
                    {
                        case 1:
                            NoteList();
                            break;
                        case 2:
                            Search();
                            break;
                        case 3:
                            AddNote();
                            break;
                        case 4:
                            DeleteNote();
                            break;
                        case 5:
                            Console.WriteLine("Exiting....");
                            menu.Display();
                            Select();
                            break;
                    }
                    running = false;
                }

            }
            void Loggin()
            {
                Console.Clear();
                conn = new MySqlConnection(cs);
                MySqlDataReader rdr = null;
                conn.Open();

                Console.WriteLine("Loggin");
                Console.WriteLine("=============================");
                string fullName = Util.ValidateString("Enter your Full Name");
                string email = Util.ValidateString("Email:");
                string password = Util.ValidateString("Password:");
                //where emailAddress = @email and password = @password
                string stm = "SELECT * FROM users where emailAddress = @email and password = @password;";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;
                string version = Convert.ToString(cmd.ExecuteScalar());

                rdr = cmd.ExecuteReader();
                if (!rdr.HasRows)
                {
                    Console.WriteLine("=============================");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The Email or Password you entered is wrong!");
                    Console.WriteLine("Press Enter To Try Again");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.ReadKey();
                    Loggin();

                }
                //else if (rdr.HasRows)
                //{
                //    Console.ForegroundColor = ConsoleColor.Green;
                //    Console.WriteLine("LOGGING IN .....");
                //    Console.ReadKey();
                //}

                Menu mainPageMenu = new Menu("List of Notes", "Search By Keyword", "Add New Note", "Delete Note", "SIGN OUT");
                mainPageMenu.Title = "Main Page";
                mainPageMenu.Display();

                bool running = true;
                void mainSelect()
                {
                    while (running)
                    {
                        Console.WriteLine("=============================");
                        int selection = Util.ValidateInt("Make selection:");
                        switch (selection)
                        {
                            case 1:
                                NoteList();
                                break;
                            case 2:
                                Search();
                                break;
                            case 3:
                                AddNote();
                                break;
                            case 4:
                                DeleteNote();
                                break;
                            case 5:
                                Console.WriteLine("Exiting....");
                                menu.Display();
                                Select();

                                break;
                        }
                        running = false;
                    }
                }
                mainSelect();
            }
            mainSelect();
        }
    }
}
