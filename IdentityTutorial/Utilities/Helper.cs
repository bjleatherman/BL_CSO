namespace IdentityTutorial.Utilities
{
    public class Helper
    {
        /* 
         Helps the Play page and the TurnService by providing values 
         that will be passed from the page to the controller with the POST 
        */

        //powers names
        public const string HEALTH = "health";
        public const string MINES = "mines";
        public const string DRONES = "drones";
        public const string SNEAK = "sneak";
        public const string TORPEDO = "torpedo";
        public const string SONAR = "sonar";
        public const string MAX_HEALTH = "maxHealth";
        public const string MAX_MINES = "maxMines";
        public const string MAX_DRONES = "maxDrones";
        public const string MAX_SNEAK = "maxSneak";
        public const string MAX_TORPEDO = "maxTorpedo";
        public const string MAX_SONAR = "maxSonar";
        public const string ALL_CHARGED = "allCharged";

        //power types
        public const string ATTACK = "attack";
        public const string DETECT = "detect";
        public const string EVADE = "evade";
        public const string REACTOR = "reactor";

        //directions
        public const string WEST = "west";
        public const string NORTH = "north";
        public const string SOUTH = "south";
        public const string EAST = "east";
        public const string SURFACE = "surface";
        public const string FIRST_MOVE = "firstMove";

        

        public enum SYSTEMS_PIPES
        {
            ZERO, ONE, TWO ,NONE
        }

        public enum FRIEND_STATUS
        {
            Recieved, Pending, Confirmed
        }

        public static Dictionary<string, int> HtmlIdDict = new Dictionary<string, int>
        {
            { "w0", 0 }, { "w1", 1 }, { "w2", 2 }, { "w3", 3 }, { "w4", 4 }, { "w5", 5 },
            { "n0", 6 }, { "n1", 7 }, { "n2", 8 }, { "n3", 9 }, { "n4", 10 }, { "n5", 11 },
            { "s0", 12 }, { "s1", 13 }, { "s2", 14 }, { "s3", 15 }, { "s4", 16 }, { "s5", 17 },
            { "e0", 18 }, { "e1", 19 }, { "e2", 20 }, { "e3", 21 }, { "e4", 22 }, { "e5", 23 }
        };

        // This will likely need to be an object that contains all of the locations within the arrays
        // for the values of the powers and how they relate back to the address of the html button ids
        public static SystemsModel[] GetSystemsModel()
        {
            SystemsModel[] systemsModels= new SystemsModel[]
            {
                new SystemsModel(0, "w0",  Helper.SYSTEMS_PIPES.ZERO, Helper.WEST, Helper.ATTACK, 0),
                new SystemsModel(1, "w1",  Helper.SYSTEMS_PIPES.ZERO, Helper.WEST, Helper.EVADE, 0),
                new SystemsModel(2, "w2",  Helper.SYSTEMS_PIPES.ZERO, Helper.WEST, Helper.DETECT, 0),
                new SystemsModel(3, "w3",  Helper.SYSTEMS_PIPES.NONE, Helper.WEST, Helper.DETECT, 1),
                new SystemsModel(4, "w4",  Helper.SYSTEMS_PIPES.NONE, Helper.WEST, Helper.REACTOR, 0),
                new SystemsModel(5, "w5",  Helper.SYSTEMS_PIPES.NONE, Helper.WEST, Helper.REACTOR, 1),

                new SystemsModel(6, "n0",  Helper.SYSTEMS_PIPES.ONE, Helper.NORTH, Helper.EVADE, 1),
                new SystemsModel(7, "n1",  Helper.SYSTEMS_PIPES.ONE, Helper.NORTH, Helper.ATTACK, 1),
                new SystemsModel(8, "n2",  Helper.SYSTEMS_PIPES.ONE, Helper.NORTH, Helper.EVADE, 2),
                new SystemsModel(9, "n3",  Helper.SYSTEMS_PIPES.NONE, Helper.NORTH, Helper.DETECT, 2),
                new SystemsModel(10, "n4",  Helper.SYSTEMS_PIPES.NONE, Helper.NORTH, Helper.ATTACK, 2),
                new SystemsModel(11, "n5",  Helper.SYSTEMS_PIPES.NONE, Helper.NORTH, Helper.REACTOR, 2),

                new SystemsModel(12, "s0",  Helper.SYSTEMS_PIPES.TWO, Helper.SOUTH, Helper.DETECT, 3),
                new SystemsModel(13, "s1",  Helper.SYSTEMS_PIPES.TWO, Helper.SOUTH, Helper.EVADE, 3),
                new SystemsModel(14, "s2",  Helper.SYSTEMS_PIPES.TWO, Helper.SOUTH, Helper.ATTACK, 3),
                new SystemsModel(15, "s3",  Helper.SYSTEMS_PIPES.NONE, Helper.SOUTH, Helper.ATTACK, 4),
                new SystemsModel(16, "s4",  Helper.SYSTEMS_PIPES.NONE, Helper.SOUTH, Helper.REACTOR, 3),
                new SystemsModel(17, "s5",  Helper.SYSTEMS_PIPES.NONE, Helper.SOUTH, Helper.EVADE, 4),

                new SystemsModel(18, "e0",  Helper.SYSTEMS_PIPES.ONE, Helper.EAST, Helper.DETECT, 4),
                new SystemsModel(19, "e1",  Helper.SYSTEMS_PIPES.TWO, Helper.EAST, Helper.EVADE, 5),
                new SystemsModel(20, "e2",  Helper.SYSTEMS_PIPES.ZERO, Helper.EAST, Helper.ATTACK, 5),
                new SystemsModel(21, "e3",  Helper.SYSTEMS_PIPES.NONE, Helper.EAST, Helper.REACTOR, 4),
                new SystemsModel(22, "e4",  Helper.SYSTEMS_PIPES.NONE, Helper.EAST, Helper.DETECT, 5),
                new SystemsModel(23, "e5",  Helper.SYSTEMS_PIPES.NONE, Helper.EAST, Helper.REACTOR, 5)
            };

            return systemsModels;
        }
    }
}
