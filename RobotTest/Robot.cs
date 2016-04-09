using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RobotTest
{
    class Robot
    {
        public int xCrd { get; set; }
        public int yCrd { get; set; }
        public string dirFace { get; set; }
        public bool placed { get; set; }

        public Robot() {
            placed = false;
        }

        public string cmdExec(string inpCommand) {
            string command = System.Text.RegularExpressions.Regex.Replace(inpCommand.ToUpper(), " ", "");

            // check if it's a place command
            Regex regex = new Regex(@"PLACE(\d),(\d),(\S*)");
            Match match = regex.Match(command);
            if (match.Success)
            {
                int xNew = Convert.ToInt16(match.Groups[1].Value);
                int yNew = Convert.ToInt16(match.Groups[2].Value);
                string fNew = match.Groups[3].Value;

                // execute place
                int placeRes = cmdPlace(xNew, yNew, fNew);

                if (placeRes == 0) return "\nInvalid coordinate: please choose X and Y from 0 to 4.\n";
                else if (placeRes == -1) return "\nInvalid direction: please choose from NORTH, EAST, SOUTH or WEST.\n";
                else return "";
            }
            else
            {
                // execute another command
                if (placed) {
                    switch (command)
                    {
                        case "MOVE":
                            cmdMove();
                            break;
                        case "LEFT":
                            cmdLeft();
                            break;
                        case "RIGHT":
                            cmdRight();
                            break;
                        case "REPORT":
                            return cmdOutput();
                        default:
                            return "\nInvalid command: use HELP for the list of available commands.\n";
                    }
                    return "";
                }
                else return "\nRobot should be placed first. Please use the PLACE X,Y,F command, where: \n X and Y are numbers from 0 to 4 \n F is a direction (NORTH, EAST, SOUTH or WEST)\n"; 
            }

        }

        public int cmdPlace(int X, int Y, string F) {
            // returns:
            // 1 if placed successfull
            // 0 if coordinate is invalid
            // -1 if direction is invalid

            if (X <= 4 && X >= 0 && Y <= 4 && Y >= 0)
            {
                xCrd = X;
                yCrd = Y;
            }
            else return 0;

            if (F == "NORTH" || F == "EAST" || F == "SOUTH" || F == "WEST") dirFace = F;
            else return -1;

            placed = true;
            return 1;
        }

        public int cmdMove() {
            // returns:
            // 1 if moved successfull
            // 0 if hit the border

            int xNew = xCrd;
            int yNew = yCrd;

            switch (dirFace)
            {
                case "NORTH":
                    yNew ++;
                    break;
                case "SOUTH":
                    yNew --;
                    break;
                case "WEST":
                    xNew --;
                    break;
                case "EAST":
                    xNew ++;
                    break;
            }
            if (xNew <= 4 && xNew >= 0 && yNew <= 4 && yNew >= 0)
            {
                xCrd = xNew;
                yCrd = yNew;
                return 1;
            }
            else return 0;
            
        }

        public void cmdLeft()
        {
            switch (dirFace)
            {
                case "NORTH":
                    dirFace = "WEST";
                    break;
                case "WEST":
                    dirFace = "SOUTH";
                    break;
                case "SOUTH":
                    dirFace = "EAST";
                    break;
                case "EAST":
                    dirFace = "NORTH";
                    break;
            }
        }

        public void cmdRight()
        {
            switch (dirFace)
            {
                case "NORTH":
                    dirFace = "EAST";
                    break;
                case "EAST":
                    dirFace = "SOUTH";
                    break;
                case "SOUTH":
                    dirFace = "WEST";
                    break;
                case "WEST":
                    dirFace = "NORTH";
                    break;
            }
        }

        public string cmdOutput() {
            return "\nOutput: " + xCrd.ToString() + "," + yCrd.ToString() + "," + dirFace + "\n";
        }

    }
}
