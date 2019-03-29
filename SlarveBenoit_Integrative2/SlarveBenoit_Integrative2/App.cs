using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Timers;

namespace SlarveBenoit_Integrative2
{
    class App
    {

        StringBuilder sb = new StringBuilder();
        StringBuilder st = new StringBuilder();
        StringBuilder sp = new StringBuilder();

        private static System.Timers.Timer myAnimationTimer;
        static int myTimerCounter = 0;

        string cs = @"server=172.16.6.1; userid=bestAdmin; password=password; database=RestaurantDataBase; port=8889";
        MySqlConnection conn = null;


        public string _directory = @"..\..\Json\";

        Menu myMenu = new Menu();
        public App()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            myMenu = new Menu("Convert Restaurant Reviews from SQL to JSON", "Showcase 5 Star Rating System", "Showcase Animated Bar Graph Review System", "Play Card Game", "EXIT");
            myMenu.Title = "Hello Admin, What Would You Like To Do Today?";
            myMenu.Display();
            Select();

        }

        private static void SetTimer()
        {
            //Set time to happen really fast 1,000 = 1 second
            //Start the function every 50/1000 seconds
            myAnimationTimer = new System.Timers.Timer(50);

            //At 50/1000, run this method "OnTimedEvent"
            //Every time it elapses, do it
            myAnimationTimer.Elapsed += OnTimedEvent;

            //Reset timer again after 50/1000, over and over again
            //False means to only run the timer one time
            myAnimationTimer.AutoReset = true;

            //The timer is enabled so it will work
            //False means the timer will not work
            myAnimationTimer.Enabled = true;
        }


        //Timer Method that runs every time the timer elapses
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //Setting the bar graph colors
            var myBackgroundColor = ConsoleColor.Gray;
            var myBarGraphColor = ConsoleColor.Blue;

            //Add one to the timer counter every time it elapses
            myTimerCounter++;

            //Random number for the bar graph animation
            Random myRandomNumber = new Random();

            //This randomly selects a number from 0 to 10 and assigns it to a variable.
            //0 and 10 could be any number, but we want the bar graph to be 10 spaces long
            //...think a rating of 8/10.
            //We will randomize it over and over to get the animation
            var theRating = myRandomNumber.Next(0, 11);

            //Set color for the bar graph
            //We set the color each time because we keep re-drawing the graph
            //We can set the color once here, or set it over and over again in the loop below.
            Console.BackgroundColor = myBarGraphColor;

            //Create bar graph, not the bar graph background
            //We create the bar we want first, and the background second.
            for (int ii = 0; ii <= theRating; ii++)
            {
                //We could run the below code here if we wanted, but we just set it once above instead of each time here.
                Console.BackgroundColor = myBarGraphColor;
                //This creates a colored bar graph of spaces, so if you have a 5/10, you will get 5 colored spaces.
                Console.Write(" ");
            }

            //Set total number for the length of the bar graph, which is also the background
            int myTotalNumber = 10;

            //Set bar graph background color
            //We can set the color once here, or set it over and over again in the loop below.
            Console.BackgroundColor = myBackgroundColor;

            //Draw bar graph background
            //The background is not seen around the edges of the bar, or also with the foreground color of the bar. We only see one color at a time.
            //So, we just start drawing the background after the final drawing of the bar graph based on the data.
            //For Example...if we have 5/10, then the bar graph is 1-5 and the background is 6-10.
            for (int iii = theRating; iii <= myTotalNumber; iii++)
            {
                //We could run the below code here if we wanted, but we just set it once above instead of each time here.
                //Console.BackgroundColor = myBackgroundColor;
                //This creates a colored background of spaces, so if you have a 5/10, you will get 5 colored spaces to make the background after the 5 colored spaces that made up the bar.
                Console.Write(" ");
            }

            //Move the cursor back to the left, where it started to draw the animation to begin with.
            //Once we redraw, and redraw, and redraw, it will look animated.
            //We are doing this because we are uisng a Console.Write to stay on the same line, so we move back, and redraw over top of the old one.
            Console.CursorLeft = 0;

            //After a bit of time, stop the animation
            //We change this as well for longer/shorter, faster/slower
            //Right now, the animation, or resetting/redrawing of the graph will happen 50 times, once every 50/1000 seconds.
            if (myTimerCounter == 50)
            {
                //Stop Timer
                myAnimationTimer.Stop();

                //Add in final database variable code here
                //If the database data says 8/10, you would set the variable to 8 here.
                //Once the animation is over, redraw the bar graph one more time with the actual bar graph data


                //Move the cursor down and away from the artwork/bar graph to have menu options, text, etc.
                for (int x = 0; x < 5; x++)
                {
                    Console.WriteLine("");
                }

                //Show the cursor again so the user can do what you need them to do.
                Console.CursorVisible = true;

            }
        }


        bool running = true;
        private void Select()
        {
            while (running)
            {
                Console.WriteLine("=============================");
                int selection = Utility.ValidateInt("Make selection:");
                switch (selection)
                {
                    case 1:
                        ConvertReviews();
                        SaveToJson();

                        break;

                    case 2:
                        ShowcaseRating();
                        break;

                    case 3:
                        ShowcaseBarGraph();
                        break;

                    case 4:
                        PlayCardGame();
                        Console.ReadKey();
                        myMenu.Display();
                        Select();
                        break;

                    case 5:
                        Console.WriteLine("Exiting program...");
                        running = false;
                        Console.ReadKey();
                        Environment.Exit(1);
                        break;

                }
            }



            void ConvertReviews()
            {

                //string cs = @"server=172.16.6.1; userid=bestAdmin; password=password; database=RestaurantDataBase; port=8889";               
                //MySqlConnection conn = null;

                try
                {
                    conn = new MySqlConnection(cs);
                    MySqlDataReader rdr = null;
                    conn.Open();

                    //string stm = "SELECT * FROM RestaurantReviews left join RestaurantProfiles using (id) left join ClassMonthRatingSymbols using (id) where RestaurantId is not null and ReviewScore is not null and PossibleReviewScore is not null and ReviewText is not null and ReviewColor is not null and RestaurantName is not null and Address is not null and Phone is not null and HoursOfOperation is not null and Price is not null and USACityLocation is not null and Cuisine is not null and FoodRating is not null and ServiceRating is not null and AmbienceRating is not null and ValueRating is not null and OverallRating is not null and OverallPossibleRating is not null and Month is not null and FullRatingIcon is not null and QuarterRatingIcon is not null and HalfRatingIcon is not null and ThreeQuarterRatingIcon is not null";

                    string stm = "SELECT * FROM RestaurantReviews, ClassMonthRatingSymbols, RestaurantProfiles" +
                    " where (RestaurantId is not null and ReviewScore is not null and PossibleReviewScore is not null and ReviewText is not null and ReviewColor is not null " +

                    "and RestaurantName is not null and Address is not null and Phone is not null and HoursOfOperation is not null and Price is not null " +
                    "and USACityLocation is not null and Cuisine is not null and FoodRating is not null and ServiceRating is not null and AmbienceRating is not null " +
                    "and ValueRating is not null and OverallRating is not null and OverallPossibleRating" +

                   //"and Month is not null and FullRatingIcon is not null and QuarterRatingIcon is not null and HalfRatingIcon is not null " +
                   /* "and ThreeQuarterRatingIcon is not null*/") limit 100";//Trying to fix null exexption for all columns
                    ////PossibleReviewScore,ReviewText,Reviewcolor," +
                    //        "Month,FullRatingIcon,EmptyRatingIcon,QuarterRatingIcon,HalfRatingIcon,ThreeQuarterRatingIcon," +
                    //"RestaurantName,Address,Phone,HoursOfOperation,Price,USACityLocation,Cuisine,FoodRating,ServiceRating,AmbienceRating,ValueRating,OverallRating,OverallPossibleRating) is not null limit 500 ";
                    //"SELECT (id) as id, (RestaurantId) as RestaurantId, (ReviewScore) as ReviewScore," +
                    //"(PossibleReviewScore) as PossibleReviewScore, (ReviewText) as ReviewText," +
                    //"(ReviewColor) as ReviewColor FROM RestaurantReviews limit 10 ";               
                    MySqlCommand cmd = new MySqlCommand(stm, conn);
                    string version = Convert.ToString(cmd.ExecuteScalar());

                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        //Console.WriteLine(rdr.GetInt32(0) + rdr.GetInt32(1) + rdr.GetInt32(2) + rdr.GetInt32(3) + rdr.GetString(4) + rdr.GetString(5)
                        //   + rdr.GetInt32(6)+rdr.GetString(7)+rdr.GetString(8)+rdr.GetString(9)+rdr.GetString(10)+rdr.GetString(11)+rdr.GetString(12)+rdr.GetString(13)
                        //   + rdr.GetDecimal(14)+rdr.GetDecimal(15)+rdr.GetDecimal(16)+rdr.GetDecimal(17)+rdr.GetDecimal(18)+rdr.GetDecimal(19)
                        //   + rdr.GetInt32(20)+rdr.GetString(21)+rdr.GetString(22)+rdr.GetString(23)+rdr.GetString(24)+rdr.GetInt32(25)+ rdr.GetString(26)+rdr.GetDecimal(27)
                        //   + rdr.GetInt32(28)+rdr.GetString(29)+rdr.GetString(30)+rdr.GetString(31)+rdr.GetString(32)+rdr.GetString(33)+rdr.GetString(34));
                        //StringBuilder sb = new StringBuilder();
                        //StringBuilder st = new StringBuilder();
                        //StringBuilder sp = new StringBuilder();

                        sb.AppendLine(" id:" + rdr.GetInt32(0) + "  restaurantid:" + rdr.GetInt32(1)
                        + "   reviewscore: " + rdr.GetInt32(2) + "  possiblereviewscore: " + rdr.GetInt32(3)
                        + "   reviewtext: " + rdr.GetString(4) + "  reviewcolor: " + rdr.GetString(5));
                        Console.WriteLine(sb);

                        st.AppendLine(" id:" + rdr.GetInt32(6) + "  Month:" + rdr.GetString(7) + "  FullRatingIcon:" + rdr.GetString(8)
                        + "   EmptyRatingIcon:" + rdr.GetString(9) + "  QuarterRatingIcon:" + rdr.GetString(10) + "  HalfRatingIcon" + rdr.GetString(11)
                        + "   ThreeQuarterRatingIcon:" + rdr.GetString(12));
                        Console.WriteLine(st);

                        sp.AppendLine(" id:" + rdr.GetInt32(13) + "  RestaurantName:" + rdr.GetString(14) + "  Address:" + rdr.GetString(15) + "  Phone:" + rdr.GetString(16)
                        + "   HoursOfOperation:" + rdr.GetString(17) + "  Price" + rdr.GetString(18) + "  USACityLocation" + rdr.GetString(19) + "  Cuisine" + rdr.GetString(20)
                        + "   FoodRating" + rdr.GetDecimal(21) + "  ServiceRating" + rdr.GetDecimal(22) + "  AmbienceRating" + rdr.GetDecimal(23) + "  ValueRating" + rdr.GetDecimal(24)
                        + "   OverallRating:" + rdr.GetDecimal(25) + "  OverallPossibleRating" + rdr.GetDecimal(26));
                        Console.WriteLine(sp);




                        //+"   \r\nid:" + rdr.GetInt32(6) + "  Month:" + rdr.GetString(7) + "  FullRatingIcon:" + rdr.GetString(8)
                        //+ "   EmptyRatingIcon:" + rdr.GetString(9) + "  QuarterRatingIcon:" + rdr.GetString(10) + "  HalfRatingIcon" + rdr.GetString(11)
                        //+ "   ThreeQuarterRatingIcon:" + rdr.GetString(12)

                        //+"   \r\nid:" + rdr.GetInt32(13) + "  RestaurantName:" + rdr.GetString(14) + "  Address:" + rdr.GetString(15) + "  Phone:" + rdr.GetString(16)
                        //+ "   HoursOfOperation:" + rdr.GetString(17) + "  Price" + rdr.GetString(18) + "  USACityLocation" + rdr.GetString(19) + "  Cuisine" + rdr.GetString(20)
                        //+ "   FoodRating" + rdr.GetDecimal(21) + "  ServiceRating" + rdr.GetDecimal(22) + "  AmbienceRating" + rdr.GetDecimal(23) + "  ValueRating" + rdr.GetDecimal(24)
                        //+ "   OverrallRating:" + rdr.GetDecimal(25) + "  OverrallPossibleRating" + rdr.GetDecimal(26));
                    }




                    Directory.CreateDirectory(_directory);
                    Console.WriteLine("....Saving to JSON");
                    using (StreamWriter sw = new StreamWriter(_directory + "database.json"))
                    {
                        sw.WriteLine("[");

                        {

                            sw.WriteLine("{");
                            sw.WriteLine($"\"id\": \"{rdr.GetInt32(0)}\",\r\n");
                            sw.WriteLine($"\"RestaurantId\": \"{rdr.GetInt32(1)}\",\r\n");
                            sw.WriteLine($"\"ReviewScore\": \"{rdr.GetInt32(2)}\",\r\n");
                            sw.WriteLine($"\"PossibleReviewScore\": \"{rdr.GetInt32(3)}\",\r\n");
                            sw.WriteLine($"\"ReviewText\": \"{rdr.GetString(4)}\",\r\n");
                            sw.WriteLine($"\"Reviewcolor\": \"{rdr.GetString(5)}\"\r\n");
                            sw.WriteLine("}");

                            sw.WriteLine("{");
                            sw.WriteLine($"\"id\": \"{rdr.GetInt32(6)}\",\r\n");
                            sw.WriteLine($"\"Month\": \"{rdr.GetString(7)}\",\r\n");
                            sw.WriteLine($"\"FullRatingIcon\": \"{rdr.GetString(8)}\",\r\n");
                            sw.WriteLine($"\"EmptyRatingIcon\": \"{rdr.GetString(9)}\",\r\n");
                            sw.WriteLine($"\"QuarterRatingIcon\": \"{rdr.GetString(10)}\",\r\n");
                            sw.WriteLine($"\"HalfRatingIcon\": \"{rdr.GetString(11)}\"\r\n");
                            sw.WriteLine($"\"ThreeQuarterRatingIcon\": \"{rdr.GetString(12)}\"\r\n");
                            sw.WriteLine("}");

                            sw.WriteLine("{");
                            sw.WriteLine($"\"id\": \"{rdr.GetInt32(13)}\",\r\n");
                            sw.WriteLine($"\"RestaurantName\": \"{rdr.GetString(14)}\",\r\n");
                            sw.WriteLine($"\"Address\": \"{rdr.GetString(15)}\",\r\n");
                            sw.WriteLine($"\"Phone\": \"{rdr.GetString(16)}\",\r\n");
                            sw.WriteLine($"\"HoursOfOperation\": \"{rdr.GetString(17)}\",\r\n");
                            sw.WriteLine($"\"Price\": \"{rdr.GetString(18)}\",\r\n");
                            sw.WriteLine($"\"USACityLocation\": \"{rdr.GetString(19)}\"\r\n");
                            sw.WriteLine($"\"Cuisine\": \"{rdr.GetString(20)}\"\r\n");
                            sw.WriteLine($"\"FoodRating\": \"{rdr.GetDecimal(21)}\"\r\n");
                            sw.WriteLine($"\"ServiceRating\": \"{rdr.GetDecimal(22)}\"\r\n");
                            sw.WriteLine($"\"AmbienceRating\": \"{rdr.GetDecimal(23)}\"\r\n");
                            sw.WriteLine($"\"ValueRating\": \"{rdr.GetDecimal(24)}\"\r\n");
                            sw.WriteLine($"\"OverallRating\": \"{rdr.GetDecimal(25)}\"\r\n");
                            sw.WriteLine($"\"OverallPossibleRating\": \"{rdr.GetDecimal(26)}\"\r\n");
                            sw.WriteLine("}");



                        }
                        sw.WriteLine("]");

                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }

                Console.ReadKey();
                myMenu.Display();
                Select();
            }
            


            void SaveToJson()
            {             
               
            }


            void ShowcaseRating()
            {
                Menu secondMenu = new Menu("List Restaurants Alphabetically", "List Restaurants in Reverse Alphabetical", "Sort Restaurants From Best/Most Stars to Worst", "Sort Restaurants From Worst/Least Stars to Best", "Show Only X and Up", "Exit");
                secondMenu.Title = "Hello Admin, How would you like to sort the data:";
                secondMenu.Display();
                secondSelect();

                void secondSelect()
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("=============================");
                        int selection = Utility.ValidateSpecialInt("Make selection:");
                        switch (selection)
                        {
                            case 1: // Alphabetically
                            conn = new MySqlConnection(cs);
                            MySqlDataReader rdr = null;
                            conn.Open();

                            string stm = "SELECT RestaurantName, OverallRating FROM RestaurantProfiles WHERE OverallRating IS NOT NULL ORDER BY RestaurantName ASC  ";
                            MySqlCommand cmd = new MySqlCommand(stm, conn);
                            string version = Convert.ToString(cmd.ExecuteScalar());

                            rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                Console.WriteLine("RestaurantName:" + rdr.GetString(0) + "  \n\t\t\t\t\t\tOverallRating:" + rdr.GetDecimal(1));
                            }
                            
                            break;


                            case 2: //Reverse Alphabetical
                            conn = new MySqlConnection(cs);
                            MySqlDataReader adr = null;
                            conn.Open();

                            string atm = "SELECT RestaurantName, OverallRating FROM RestaurantProfiles WHERE OverallRating IS NOT NULL ORDER BY RestaurantName DESC ";
                            MySqlCommand amd = new MySqlCommand(atm, conn);
                            string Aersion = Convert.ToString(amd.ExecuteScalar());

                            adr = amd.ExecuteReader();
                            while (adr.Read())
                            {
                                Console.WriteLine("RestaurantName:" + adr.GetString(0) + "  \n\t\t\t\t\t\tOverallRating:" + adr.GetDecimal(1));
                            }
                            break;


                            case 3: //Top Star ratings to worst
                            conn = new MySqlConnection(cs);
                            MySqlDataReader edr = null;
                            conn.Open();

                            string etm = "SELECT RestaurantName, OverallRating FROM RestaurantProfiles WHERE OverallRating IS NOT NULL ORDER BY OverallRating DESC  ";
                            MySqlCommand emd = new MySqlCommand(etm, conn);
                            string eersion = Convert.ToString(emd.ExecuteScalar());

                            //int co = 1;
                            edr = emd.ExecuteReader();
                            while (edr.Read())
                            {
                                string rating = edr.GetDecimal(1).ToString();
                                string r = Symbols(rating);
                                Console.WriteLine("RestaurantName:" + edr.GetString(0) + "  \n\t\t\t\t\t\tOverallRating:" + edr.GetDecimal(1) + " RatingSymbol "+r);
                                
                            }
                            break;


                            case 4: //Worst Star rating to Best
                            conn = new MySqlConnection(cs);
                            MySqlDataReader udr = null;
                            conn.Open();

                            
                            string utm = "SELECT RestaurantName, OverallRating FROM RestaurantProfiles WHERE OverallRating IS NOT NULL ORDER BY OverallRating ASC ";
                            MySqlCommand umd = new MySqlCommand(utm, conn);
                            string uersion = Convert.ToString(umd.ExecuteScalar());

                            udr = umd.ExecuteReader();
                            while (udr.Read())
                            {
                                string rating = udr.GetDecimal(1).ToString();
                                string r = Symbols(rating);
                                Console.WriteLine("RestaurantName:" + udr.GetString(0) + "  \n\t\t\t\t\t\tOverallRating:" + udr.GetDecimal(1) + " RatingSymbol " + r);
                            }
                            break;


                            case 5:                           
                            ShowOnlyX();
                                break;   
                                
                            case 6:
                                myMenu.Display();
                                Select();
                                break;
                        }
                    Console.ReadKey();
                    secondMenu.Display();
                    secondSelect();
                }

                //Menu secondMenu = new Menu("List Restaurants Alphabetically", "List Restaurants in Reverse Alphabetical", "Sort Restaurants From Best/Most Stars to Worst", "Sort Restaurants From Worst/Least Stars to Best", "Show Only X and Up", "Exit");
                //secondMenu.Title = "Hello Admin, How would you like to sort the data:";
                //secondMenu.Display();
                //secondSelect();

            }
            void ShowOnlyX()
            {                
                void subSelect()
                {                   
                        Console.WriteLine("=============================");
                        int selection = Utility.ValidateSpecialInt("Make selection:");
                        switch (selection)
                        {
                            case 1:
                            conn = new MySqlConnection(cs);
                            MySqlDataReader udr = null;
                            conn.Open();


                            string utm = "SELECT RestaurantName, OverallRating FROM RestaurantProfiles WHERE OverallRating = 5.00 ";
                            MySqlCommand umd = new MySqlCommand(utm, conn);
                            string uersion = Convert.ToString(umd.ExecuteScalar());

                            udr = umd.ExecuteReader();
                            while (udr.Read())
                            {
                                string rating = udr.GetDecimal(1).ToString();
                                string r = Symbols(rating);
                                Console.WriteLine("RestaurantName:" + udr.GetString(0) + "  \n\t\t\t\t\t\tOverallRating:" + udr.GetDecimal(1) + " RatingSymbol " + r);
                            }
                            break;

                            case 2:
                            conn = new MySqlConnection(cs);
                            MySqlDataReader sdr = null;
                            conn.Open();


                            string stm = "SELECT RestaurantName, OverallRating FROM RestaurantProfiles WHERE OverallRating >= 4.00 order by OverallRating asc ";
                            MySqlCommand smd = new MySqlCommand(stm, conn);
                            string sersion = Convert.ToString(smd.ExecuteScalar());

                            sdr = smd.ExecuteReader();
                            while (sdr.Read())
                            {
                                string rating = sdr.GetDecimal(1).ToString();
                                string r = Symbols(rating);
                                Console.WriteLine("RestaurantName:" + sdr.GetString(0) + "  \n\t\t\t\t\t\tOverallRating:" + sdr.GetDecimal(1) + " RatingSymbol " + r);
                            }
                            break;

                            case 3:
                            conn = new MySqlConnection(cs);
                            MySqlDataReader pdr = null;
                            conn.Open();


                            string ptm = "SELECT RestaurantName, OverallRating FROM RestaurantProfiles WHERE OverallRating >= 3.00  order by OverallRating asc";
                            MySqlCommand pmd = new MySqlCommand(ptm, conn);
                            string persion = Convert.ToString(pmd.ExecuteScalar());

                            pdr = pmd.ExecuteReader();
                            while (pdr.Read())
                            {
                                string rating = pdr.GetDecimal(1).ToString();
                                string r = Symbols(rating);
                                Console.WriteLine("RestaurantName:" + pdr.GetString(0) + "  \n\t\t\t\t\t\tOverallRating:" + pdr.GetDecimal(1) + " RatingSymbol " + r);
                            }
                            break;

                            case 4:
                            conn = new MySqlConnection(cs);
                            MySqlDataReader wdr = null;
                            conn.Open();


                            string wtm = "SELECT RestaurantName, OverallRating FROM RestaurantProfiles WHERE OverallRating = 1.00 ";
                            MySqlCommand wmd = new MySqlCommand(wtm, conn);
                            string wersion = Convert.ToString(wmd.ExecuteScalar());

                            wdr = wmd.ExecuteReader();
                            while (wdr.Read())
                            {
                                string rating = wdr.GetDecimal(1).ToString();
                                string r = Symbols(rating);
                                Console.WriteLine("RestaurantName:" + wdr.GetString(0) + "  \n\t\t\t\t\t\tOverallRating:" + wdr.GetDecimal(1) + " RatingSymbol " + r);
                            }
                            break;

                            case 5:
                            conn = new MySqlConnection(cs);
                            MySqlDataReader fdr = null;
                            conn.Open();


                            string ftm = "SELECT RestaurantName, OverallRating FROM RestaurantProfiles WHERE OverallRating = 0.00 ";
                            MySqlCommand fmd = new MySqlCommand(ftm, conn);
                            string fersion = Convert.ToString(fmd.ExecuteScalar());

                            fdr = fmd.ExecuteReader();
                            while (fdr.Read())
                            {
                                string rating = fdr.GetDecimal(1).ToString();
                                string r = Symbols(rating);
                                Console.WriteLine("RestaurantName:" + fdr.GetString(0) + "  \n\t\t\t\t\t\tOverallRating:" + fdr.GetDecimal(1) + " RatingSymbol " + r);
                            }
                            break;

                            case 6:
                            ShowcaseRating();
                                break;

                        }
                    Console.ReadKey();
                    ShowOnlyX();
                    subSelect();

                }
                
                Menu subMenu = new Menu("Show the Best (5 Stars)", "Show 4 Stars and Up", "Show 3 Stars and Up", "Show the Worst (1 Stars)", "Show Unrated", "Back");
                subMenu.Title = "Sub-Menu:";
                subMenu.Display();
                subSelect();              
            }

             string Symbols( string s)
            {
                if (s == "5.00" || s == "4.50")
                {                   
                    return " * * * * *";
                }
                else if (s == "4.00" || s == "3.50" || s=="4.25")
                {
                    return " * * * *";
                }
                else if (s == "3.00" || s == "2.75" || s== "3.25")
                {
                    return " * * *";
                }
                else if (s =="3.75")
                {
                    return " 3/4 *";
                }
                else if (s == "2.50")
                {
                    return " 1/2 *";
                }
                else if (s == "2.00" || s == "2.25" || s == "1.75")
                {
                    return " * *";
                }
                else if (s == "1.25")
                {
                    return " 1/4 *";
                }
                else if (s == "1.00" || s == "1.50" )
                {
                    return " *";
                }
                else if (s == "0.50")
                {
                    return " %";
                }
                else if (s == "0.00")
                {
                    return" N/A ";
                }
                return s;
            }

            void ShowcaseBarGraph()
            {

                Menu thirdMenu = new Menu("Show Average of Reviews for Restaurants", "Dinner Spinner(Select random restaurants)", "Top 10 Restaurants", "Back to Main Menu");
                thirdMenu.Title = "Hello Admin, How would you like to sort the data:";
                thirdMenu.Display();
                thirdSelect();

                void thirdSelect()
                {
                    Console.WriteLine("=============================");
                    int selection = Utility.ValidateSpecialInt("Make selection:");
                    switch (selection)
                    {
                        case 1:
                           
                            conn = new MySqlConnection(cs);
                            MySqlDataReader rdr = null;
                            conn.Open();

                            //string stm = "SELECT RestaurantName, ReviewScore FROM RestaurantProfiles left join RestaurantReviews using(id)";

                            string stm = "SELECT f.RestaurantName, (SELECT AVG(s.ReviewScore)FROM RestaurantReviews s " +
                               "WHERE s.RestaurantId= f.id)average FROM RestaurantProfiles f WHERE OverallRating is not null ";
                            MySqlCommand cmd = new MySqlCommand(stm, conn);
                            string version = Convert.ToString(cmd.ExecuteScalar());

                            rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                //string bar = rdr.GetDecimal(1).ToString();
                                //var b = Bar(bar);

                                //This code is pulling both the restaurant names and reviewScores, and changing background color
                                Console.Write(rdr.GetString(0) /*+ "  \n\t\t\t\t\t\tReviewScore:" + rdr.GetDecimal(3) + b)*/);
                                Console.Write("   ");
                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.Write(rdr.GetString(1));
                                Console.Write("   ");
                                Console.BackgroundColor = ConsoleColor.Blue;

                                //Parsing rdr string index to an integer in order to loop through it
                                int convert;
                                int.TryParse(rdr.GetString(1), out convert);
                                //int convert2 = convert / 10;

                                //Converting review scores from 50/100 To 5/10 to make bar graph appear smaller
                                int convert2 = decimal.ToInt32(convert / 10);

                                for (int i = 0; i < convert2; i++)
                                {
                                    Console.Write(" ");

                                }
                                //Changing background color to gray to show end of bar graph and substracting the score with the possible review score which was 100 but now 10 meaning (5-10 OR 5 out 10)
                                Console.BackgroundColor = ConsoleColor.Gray;
                                int endOfBarGraph = 10 - convert2;
                                for (int i = 0; i < endOfBarGraph; i++)
                                {
                                    Console.Write(" ");

                                }
                                //Changing background to black in order to see only the biggining of bar and the end (one full bar)
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.WriteLine("");
                            }
                            break;


                        case 2:
                            conn = new MySqlConnection(cs);
                            MySqlDataReader udr = null;
                            conn.Open();

                            string utm = "SELECT RestaurantName, ReviewScore FROM RestaurantProfiles left join RestaurantReviews using(id) ";                         
                            MySqlCommand umd = new MySqlCommand(utm, conn);
                            string uersion = Convert.ToString(umd.ExecuteScalar());

                            udr = umd.ExecuteReader();
                            while (udr.Read())
                            {
                                ////string bar = udr.GetDecimal(1).ToString();
                                ////var b = Bar(bar);
                                //Console.WriteLine(udr.GetString(0) + "  \n\t\t\t\t\t\tReviewScore:" + udr.GetDecimal(1));

                                Console.Write(udr.GetString(0) /*+ "  \n\t\t\t\t\t\tReviewScore:" + rdr.GetDecimal(3) + b)*/);
                                Console.Write("   ");
                                Console.Write(udr.GetString(1));
                                Console.Write("   ");
                                Console.BackgroundColor = ConsoleColor.Blue;

                                //Parsing rdr string index to an integer in order to loop through it
                                int convert;
                                int.TryParse(udr.GetString(1), out convert);
                                //int convert2 = convert / 10;

                                //Converting review scores from 50/100 To 5/10 to make bar graph appear smaller
                                int convert2 = decimal.ToInt32(convert / 10);

                                for (int i = 0; i < convert2; i++)
                                {
                                    Console.Write(" ");

                                }
                                //Changing background color to gray to show end of bar graph and substracting the score with the possible review score which was 100 but now 10 meaning (5-10 OR 5 out 10)
                                Console.BackgroundColor = ConsoleColor.Gray;
                                int endOfBarGraph = 10 - convert2;
                                for (int i = 0; i < endOfBarGraph; i++)
                                {
                                    Console.Write(" ");

                                }
                                //Changing background to black in order to see only the biggining of bar and the end (one full bar)
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.WriteLine("");
                            }
                            break;


                        case 3:
                            conn = new MySqlConnection(cs);
                            MySqlDataReader jdr = null;
                            conn.Open();

                            string jtm = "SELECT RestaurantName, ReviewScore FROM RestaurantProfiles left join RestaurantReviews using(id) ORDER BY ReviewScore DESC LIMIT 10";
                            MySqlCommand jmd = new MySqlCommand(jtm, conn);
                            string jersion = Convert.ToString(jmd.ExecuteScalar());

                            jdr = jmd.ExecuteReader();
                            while (jdr.Read())
                            {

                                Console.Write(jdr.GetString(0) /*+ "  \n\t\t\t\t\t\tReviewScore:" + rdr.GetDecimal(3) + b)*/);
                                Console.Write("   ");
                                Console.Write(jdr.GetString(1));
                                Console.Write("   ");
                                Console.BackgroundColor = ConsoleColor.Blue;

                                //Parsing rdr string index to an integer in order to loop through it
                                int convert;
                                int.TryParse(jdr.GetString(1), out convert);
                                //int convert2 = convert / 10;

                                //Converting review scores from 50/100 To 5/10 to make bar graph appear smaller
                                int convert2 = decimal.ToInt32(convert / 10);

                                for (int i = 0; i < convert2; i++)
                                {
                                    Console.Write(" ");

                                }
                                //Changing background color to gray to show end of bar graph and substracting the score with the possible review score which was 100 but now 10 meaning (5-10 OR 5 out 10)
                                Console.BackgroundColor = ConsoleColor.Gray;
                                int endOfBarGraph = 10 - convert2;
                                for (int i = 0; i < endOfBarGraph; i++)
                                {
                                    Console.Write(" ");

                                }
                                //Changing background to black in order to see only the biggining of bar and the end (one full bar)
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.WriteLine("");


                            }
                            break;
                    
                        case 4:
                            myMenu.Display();
                            Select();
                            break;
                    }
                    Console.ReadKey();
                    thirdMenu.Display();
                    thirdSelect();
                }

            }

            string Bar(string b)
            {
                ////Create Timer
                //if (b =="30" )
                //{
                //    Console.BackgroundColor = ConsoleColor.Red;
                //}
                //else if (b == "70")
                //{
                //    Console.BackgroundColor =ConsoleColor.Green;
                //}


                SetTimer();

                //If you like, hide the cursor so it does not get in the way of the graph, but be careful...it's invisible so do not make the user do something where they need the curso after you make it invisible.
                Console.CursorVisible = false;

                //This is needed for the timer to work
                //The code needs to stop running, or wait for a response in order to play the animation
                Console.ReadLine();
                return Bar(b);
            }


            void PlayCardGame()
            {
                Console.WriteLine("Card Game");
                decimal erir = 1000000;
                erir.GetType();
                Console.WriteLine(erir);
                string s = "&#9824";
                byte[] unicode = System.Text.Encoding.Unicode.GetBytes(s);
                Console.WriteLine(unicode.Length);
                Console.WriteLine("♠");

            }

            
        }

    }
}
