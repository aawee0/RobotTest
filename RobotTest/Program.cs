using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotTest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            string line = null;

            Robot newRobot = new Robot();

            // Intro and question about input from file
            Console.WriteLine("Welcome to the RobotTest. This simple demo is a simulation of a toy robot moving on a square tabletop, of dimensions 5 units x 5 units. The robot is free to roam around the surface of the table, but is prevented from falling to destruction. Any movement that would result in the robot falling from the table must be prevented, however further valid movement commands are still allowed.\n");
            string yesOrNo = null;
            while (yesOrNo != "Y" && yesOrNo != "N") {
                Console.Write("Do you want to input commands from a file? (Y/N) ");
                yesOrNo = Console.ReadLine();
                yesOrNo = yesOrNo.ToUpper();
            }

            // File name dialogue
            System.IO.StreamReader file = null;
            if (yesOrNo == "Y") {
                string filename;
                bool fileopened = false;

                while (!fileopened)
                {
                    Console.Write("Please type in the name of the file (e.g. tests.txt): ");
                    filename = Console.ReadLine();

                    try
                    {
                        file = new System.IO.StreamReader(filename);
                        if (file!=null) fileopened = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The file could not be read. Please check the name or try to use an absolute path.");
                        Console.WriteLine(e.Message);
                    }
                }
            }
            else if (yesOrNo == "N") Console.WriteLine("\nNow entering the console mode. Please start from placing the robot on the table by using PLACE X,Y,F command, or type HELP for more details.\n");

            while (!exit)
            {
                if (yesOrNo == "Y")
                {
                    line = file.ReadLine();
                    Console.WriteLine(line);
                    if (line == null)
                    {
                        Console.WriteLine("Finished processing. Please type any required command or finish by using EXIT.");
                        yesOrNo = "N";
                    }
                }
                else if (yesOrNo == "N") line = Console.ReadLine();

                if (line != null)
                {
                    line = line.ToUpper();
                    switch (line)
                    {
                        case "EXIT":
                            Console.WriteLine("Exiting the program..");
                            exit = true;
                            break;
                        case "HELP":
                            Console.WriteLine("\nAvaliable commands:");
                            Console.WriteLine("PLACE X,Y,F - to place the robot, where: \n - X and Y are numbers from 0 to 4 \n - F is a direction (NORTH, EAST, SOUTH or WEST);");
                            Console.WriteLine("MOVE to move the robot one unit forward in the direction currently facing;");
                            Console.WriteLine("LEFT and RIGHT to rotate the robot in the specified direction;");
                            Console.WriteLine("REPORT to display the coordinates and the direction of the robot.\n");
                            // WRITE HELP MENU
                            break;
                        case "":
                            break;
                        default:
                            string output = newRobot.cmdExec(line);
                            if (output != "") Console.WriteLine(output);
                            break;
                    }
                }


            }


        }
    }
}
