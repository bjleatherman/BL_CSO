using IdentityTutorial.Areas.Identity.Data;
using IdentityTutorial.Models;
using IdentityTutorial.Models.ViewModels;
using IdentityTutorial.Utilities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Linq;

namespace IdentityTutorial.Services
{
    public class TurnService : ITurnService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlayerService _playerService;

        public TurnService(ApplicationDbContext context, IPlayerService playerService)
        {
            //Console.WriteLine("=============================================================\r\nI N I T I A L I Z I N G --T U R N    S E R V I C E\r\n=============================================================");
            _context = context;
            _playerService = playerService;
        }

        public bool SubmitPlayerTurn(TurnVM turn)
        {
            //log to the console that a turn was submitted
            Console.WriteLine("Submitted Turn:");
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(turn))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(turn);

                if (value != null)
                {
                    Console.WriteLine("{0}={1}", name, value);
                }
            }

            // TODO: fix this unholy monstrosity
            TurnData turnData = new TurnData(this, _playerService, turn);

            /*
             *                  Things that need to be tested (right now lol)
             *      1. Charge Powers
             *          > If all of the powers are == max, then the result shoult be Helper.ALL_CHARGED
             *              - Accept and do nothing
             *          > Does the selection exceed the max
             *              - Throw error
             *          > Selection is less than max
             *              - Accept and increment Db value by 1
             *
             *      2. Systems
             *          > Get current syststem state
             *          > Match T/F values from Db to Systems buttons
             *          > If a button that is false has been selected
             *              - Button cannot be toggled to true
             *              - Throw error
             *          > Check if a pipe has been cleared
             *              - If all buttons in a pipe have been cleared, change all of their values to true
             *          > Check selected direction
             *              - Must match Systems direction, else, throw error
             *              - If direction == Helper.SURFACE
             *                  * Clear all pipes
             *              - If all buttons on a direction card are false
             *              
             *                  * Damage ship, clear pipes? Check rules *
             *
             *      3. Direction
             *          > Does the direction selected match the selection for the Systems
             *              - If it doesn't, throw error
             *          > Check for collision with
             *              - Map boundries 
             *              - Islands
             *              - User sub path
             *                           
             *              * Collisions cause health damage? Check rules *   
             *          > If direction == Helper.SURFACE
             *              - Figure out how to mark that opponent goes for 3 turns in a row
             *                  
             */

            // Checks the state of the game. If the Game state checker returns true
            //      Then it is pushed to the Db
            //      If the state checker or the Db push are false, the whole method returns false
            if (CheckGameState(turnData))
            {
                //update systemsModelArray here or call method that updates systemsModelArray here
                if (PushTurnToDb(turnData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // Throw error
            }

            return true;
        }

        private bool CheckGameState(TurnData turnData)
        {
            // Tracks validity of Game State, if it is false, then there was a logical error
            bool isValid = true;

            // Check powers
            if (!CheckChargePower(turnData))
            {
                isValid = false;
            }

            // If the user selected a power to activate, check all aspects here
            if (turnData.turn.ActivatedPower != null)
            {
                if (!CheckActivatePower(turnData))
                {
                    isValid = false;
                }
            }

            //check systems
            if (!CheckSystems(turnData))
            {
                isValid = false;
            }

            //check direction

            return isValid;
        }

        //  ********************************************************************************
        //  -------------Get Variables that will be used as the turn is checked-------------
        //  ********************************************************************************
        public PlayerGameSave GetPlayerGameSave(TurnVM turn)
        {
            // Get the table that will be checked against
            PlayerGameSave _playerGameSave = _context.PlayerGameSaves?.FirstOrDefault(
                x => x.GameId == turn.GameId &&
                x.PlayerId == turn.UserId)
                ?? new PlayerGameSave();

            // If the query returned a null obj, return that there was an error
            if (_playerGameSave == null) { throw new Exception("Could not find Player Game Save with GameId and Current User"); }

            return _playerGameSave;
        }
        public Game GetGame(TurnVM turn)
        {
            Game _game = _context.Games?.FirstOrDefault(x => x.GameId.Equals(turn.GameId)) ?? new Game();

            if (_game == null) { throw new Exception("Could not find Player Game Save with GameId and Current User"); }
            
            return _game;
        }
        public Dictionary<string, int>[] GetPowerState(PlayerGameSave _playerGameSave, Game _game)
        {

            // Create dictionaries to store data from _playerGameSaves and _games tables
            Dictionary<string, int> playerGameSaves = new Dictionary<string, int>();
            Dictionary<string, int> games = new Dictionary<string, int>();

            // Put the arrays into an array so that they can be returned
            Dictionary<string, int>[] powerState = new Dictionary<string, int>[]
            {
                playerGameSaves,
                games
            };

            // Populate dictionaries with data from _playerGameSaves and _games tables
            playerGameSaves.Add(Helper.HEALTH, _playerGameSave.Health);
            playerGameSaves.Add(Helper.MINES, _playerGameSave.Mines);
            playerGameSaves.Add(Helper.DRONES, _playerGameSave.Drones);
            playerGameSaves.Add(Helper.SNEAK, _playerGameSave.Sneak);
            playerGameSaves.Add(Helper.TORPEDO, _playerGameSave.Torpedo);
            playerGameSaves.Add(Helper.SONAR, _playerGameSave.Sonar);

            games.Add(Helper.MAX_HEALTH, _game.MaxHealth);
            games.Add(Helper.MAX_MINES, _game.MaxMines);
            games.Add(Helper.MAX_DRONES, _game.MaxDrones);
            games.Add(Helper.MAX_SNEAK, _game.MaxSneak);
            games.Add(Helper.MAX_TORPEDO, _game.MaxTorpedo);
            games.Add(Helper.MAX_SONAR, _game.MaxSonar);

            return powerState;
        }
        public Dictionary<string, bool[]> GetSystemsState(PlayerGameSave _playerGameSave)
        {
            // Get the current state of the systems buttons
            Dictionary<string, bool[]> systemsState = new Dictionary<string, bool[]>
            {
                { Helper.ATTACK, Array.ConvertAll(_playerGameSave.SystemsAttack.Split(','), bool.Parse) },
                { Helper.DETECT, Array.ConvertAll(_playerGameSave.SystemsDetect.Split(','), bool.Parse) },
                { Helper.EVADE, Array.ConvertAll(_playerGameSave.SystemsEvade.Split(','), bool.Parse) },
                { Helper.REACTOR, Array.ConvertAll(_playerGameSave.SystemsReactor.Split(','), bool.Parse) }
            };

            return systemsState;
        }
        public SystemsModel[] PopulateIsFixedInModel(SystemsModel[] systemsModels, Dictionary<string, bool[]> systemsState)
        {
            foreach (var i in systemsModels)
            {
                i.isFixed = systemsState[i.Power][i.DbIndex];
            }
            return systemsModels;
        }

        // Methods that will check the turn functions
        private bool CheckChargePower(TurnData turnData)
        {
            string power = turnData.turn.Power;

            if (power == Helper.ALL_CHARGED)
            {
                return true;
            }

            if (power == null)
            {
                return false;
            }

            bool isValid = true;

            string maxPower = "max" + char.ToUpper(power[0]) + power.Substring(1);

            // Get values from dictionaries for comparison in if statement
            int currentPowerValue = turnData.powerState[0][power];
            int maxPowerValue = turnData.powerState[1][maxPower];

            //If the value is less than the max, then we can count it as a valid input
            if (currentPowerValue < maxPowerValue)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }
        private bool CheckActivatePower(TurnData turnData)
        {
            bool isValid = true;
            string power = turnData.turn.ActivatedPower ?? 
                throw new Exception("Expected Activated Power, found none");
            string maxPower = "max" + char.ToUpper(power[0]) + power.Substring(1);

            // Get values from dictionaries for comparison in if statement
            int currentPowerValue = turnData.powerState[0][power];
            int maxPowerValue = turnData.powerState[1][maxPower];

            //If the value is less than the max, then we can count it as a valid input
            if (currentPowerValue == maxPowerValue)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }
        private bool CheckSystems(TurnData turnData)
        {
            bool isValid = true;
            string systems = turnData.turn.Systems;
            string direction = turnData.turn.Direction;

            // If there is no system btn selected, return false
            if (systems == null) { return false; }

            // If each input is to surface, return true
            if (systems == Helper.SURFACE &&
                direction == Helper.SURFACE) { return true; }

            // If it is the first move, no system should be selected
            // TODO: validate that it is actully the first move
            if(systems == Helper.FIRST_MOVE) { return true; }

            // Gets the (int)HtmlIdValue that is used to find the right object in 'systemsModelArray' 
            // by searching the helper Dictionary for the int that acts as an index in 'systemsModelArray'
            int htmlIdValue = Helper.HtmlIdDict[systems];

            // This is only necessary to test
            //PrintSystemsState(systemsModelArray, systemsState);

            // Get the obj that contains the information about the system btn that was pressed
            SystemsModel newSystems = turnData.systemsModels[htmlIdValue];

            // Lookup the value that corresponds to the pressed button from the DB
            bool checkDbPowerValueFromDb = turnData.systemsState[newSystems.Power][newSystems.DbIndex];

            // If the Db value is false, then it was already pressed so,
            // the turn is invalid
            if (!checkDbPowerValueFromDb)
            {
                isValid= false;
            }

            return isValid;
        }


        // TODO:!! Fix this mess, there needs to be many more method calls in here
        private bool PushTurnToDb(TurnData turnData)
        {
            bool isSuccessful = true;
            List<Helper.SYSTEMS_PIPES> pipesToClear = new List<Helper.SYSTEMS_PIPES>();

            bool isSurface = turnData.turn.Direction == Helper.SURFACE && turnData.turn.Systems == Helper.SURFACE;
            bool isFirstMove = turnData.turn.Direction == Helper.FIRST_MOVE && turnData.turn.Systems == Helper.FIRST_MOVE;
            
            // Charge Power by 1
            if (turnData.turn.Power != null && turnData.turn.Power != Helper.ALL_CHARGED)
            {
                turnData._playerGameSave = ChargePowerByOne(turnData.turn.Power, turnData._playerGameSave);
            }

            // Decriment Activated Power to 0
            if (turnData.turn.ActivatedPower != null)
            {
                turnData._playerGameSave = SetPowerToZero(turnData.turn.ActivatedPower, turnData._playerGameSave);
            }
            
            // Update the string for the power buttons in the database
            // Function for not doing surface
            if (!isSurface && !isFirstMove)
            {
                turnData._playerGameSave = SetSystemsStatusToFalse(turnData);

                //update the systemsModels with the up to date information
                int htmlIdValue = Helper.HtmlIdDict[turnData.turn.Systems];
                turnData.systemsModels[htmlIdValue].isFixed = false;

                pipesToClear = GetPipesToClear(turnData.systemsModels);
            }
            // If the user is surfacing
            if (isSurface && !isFirstMove)
            {
                // TODO: set it so that the other player gets 3 turns in a row
                // TODO: check that the move is valid
                // TODO: when surfacing, split Current Coordinate History and Total Coordinate History

                // Set all pipes to be cleared
                pipesToClear.Add(Helper.SYSTEMS_PIPES.ZERO);
                pipesToClear.Add(Helper.SYSTEMS_PIPES.ONE);
                pipesToClear.Add(Helper.SYSTEMS_PIPES.TWO);
            }

            // Clear all pipes that are full, or clear pipes on surface
            if(pipesToClear.Count > 0)
            {
                turnData._playerGameSave = ClearPipes(turnData, pipesToClear);
            }
            
            if(isFirstMove)
            {
                // TODO: Validate direction (maybe take damage), update all 4 coordinate trackers, follow contitions from surfacing

                    turnData._playerGameSave = EnterMoveToDb(turnData._playerGameSave, turnData.turn.FirstMoveLocation ?? throw new Exception("Tried and failed to insert first move location"));
            }

            // If all changes were made succesfully, then update the database
            if (isSuccessful)
            {
                _context.Update(turnData._playerGameSave);
                _context.SaveChanges();
            }

            PlayerService.UpdatePlayerLastDateTime(turnData.turn.UserId, _context);

            return isSuccessful;
        }
        private PlayerGameSave ChargePowerByOne(string power, PlayerGameSave _playerGameSave)
        {
            // Get the strings for the power values
            string powerColumn = char.ToUpper(power[0]) + power.Substring(1);

            try
            {
                // Charge the power in turn
                PropertyInfo powerProperty = _playerGameSave.GetType().GetProperty(powerColumn) ??
                    throw new Exception($"{powerColumn} is not a power");
                powerProperty.SetValue(_playerGameSave, (int)powerProperty.GetValue(_playerGameSave) + 1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            

            return _playerGameSave;
        }
        private PlayerGameSave SetPowerToZero(string power, PlayerGameSave _playerGameSave)
        {
            // Get the strings for the power values
            string powerColumn = char.ToUpper(power[0]) + power.Substring(1);

            try
            { 
                // Change the power in turn to 0
                PropertyInfo activatedPowerProperty = _playerGameSave.GetType().GetProperty(powerColumn) ??
                    throw new Exception($"{powerColumn} is not a power");
                activatedPowerProperty.SetValue(_playerGameSave, 0);
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return _playerGameSave;
        }
        private PlayerGameSave SetSystemsStatusToFalse(TurnData turnData)
        {
            string systems = turnData.turn.Systems;
            SystemsModel[] systemsModels = turnData.systemsModels;
            Dictionary<string, bool[]> systemsState = turnData.systemsState;
            PlayerGameSave _playerGameSave = turnData._playerGameSave;

            // gets the index to use to grab the right systemsModels object
            int htmlIdValue = Helper.HtmlIdDict[systems]; 

            // gets the right systemsModels object
            SystemsModel newSystems = systemsModels[htmlIdValue]; 

            //set string to find db col with
            string powerNameInDb = "Systems" + char.ToUpper(newSystems.Power[0]) + newSystems.Power.Substring(1); 
            
            // gets the power array associtated with the selected systems button
            bool[] systemsPowerArray = systemsState[newSystems.Power]; 
            
            // sets the exact systems button to false in the array that will be pushed to Db
            systemsPowerArray[newSystems.DbIndex] = false; 
            
            // string that will be sent to the database
            string newSystemsPowerString = string.Join(",", systemsPowerArray).ToLowerInvariant(); 

            //update the systemsModels with the up to date information
            //Done in the PushToDb for now
            //systemsModels[htmlIdValue].isFixed = false;

            // Find the associated property in the Db and set the string
            PropertyInfo genericSystemsProperty = _playerGameSave.GetType().GetProperty(powerNameInDb) ??
                throw new Exception($"{powerNameInDb} not found as Db Column");
            genericSystemsProperty.SetValue(_playerGameSave, newSystemsPowerString);

            return _playerGameSave;
        }
        private List<Helper.SYSTEMS_PIPES> GetPipesToClear(SystemsModel[] systemsModels)
        {
            List<Helper.SYSTEMS_PIPES> pipesToClear = new List<Helper.SYSTEMS_PIPES>();

            if (systemsModels.Where(x => x.isFixed == false && x.Pipe == Helper.SYSTEMS_PIPES.ZERO).Count() == 4)
            {
                pipesToClear.Add(Helper.SYSTEMS_PIPES.ZERO);
            }
            if (systemsModels.Where(x => x.isFixed == false && x.Pipe == Helper.SYSTEMS_PIPES.ONE).Count() == 4)
            {
                pipesToClear.Add(Helper.SYSTEMS_PIPES.ONE); 
            }
            if (systemsModels.Where(x => x.isFixed == false && x.Pipe == Helper.SYSTEMS_PIPES.TWO).Count() == 4)
            {
                pipesToClear.Add(Helper.SYSTEMS_PIPES.TWO);
            }
            return pipesToClear;
        }
        private PlayerGameSave ClearPipes(TurnData turnData, List<Helper.SYSTEMS_PIPES> pipesToClear)
        {

            string attackPropertyName = "Systems" + char.ToUpper(Helper.ATTACK[0]) + Helper.ATTACK.Substring(1);
            string detectPropertyName = "Systems" + char.ToUpper(Helper.DETECT[0]) + Helper.DETECT.Substring(1);
            string evadePropertyName = "Systems" + char.ToUpper(Helper.EVADE[0]) + Helper.EVADE.Substring(1);
            string reactorPropertyName = "Systems" + char.ToUpper(Helper.REACTOR[0]) + Helper.REACTOR.Substring(1);

            Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>
            {
                { Helper.ATTACK, turnData._playerGameSave.GetType().GetProperty(attackPropertyName) ?? throw new Exception($"{attackPropertyName} not found as Db Column") },
                { Helper.DETECT, turnData._playerGameSave.GetType().GetProperty(detectPropertyName) ?? throw new Exception($"{detectPropertyName} not found as Db Column") },
                { Helper.EVADE,  turnData._playerGameSave.GetType().GetProperty(evadePropertyName) ?? throw new Exception($"{evadePropertyName} not found as Db Column") },
                { Helper.REACTOR, turnData._playerGameSave.GetType().GetProperty(reactorPropertyName) ?? throw new Exception($"{reactorPropertyName} not found as Db Column") }
            };

            foreach (SystemsModel item in turnData.systemsModels)
            {
                if (pipesToClear.Contains(item.Pipe))
                {
                    item.isFixed = true;
                    turnData.systemsState[item.Power][item.DbIndex] = true;
                }
            }

            foreach (KeyValuePair<string, bool[]> entry in turnData.systemsState)
            {
                properties[entry.Key].SetValue(turnData._playerGameSave, String.Join(",", entry.Value).ToLowerInvariant());
            }

            return turnData._playerGameSave;
        }
        private PlayerGameSave EnterMoveToDb(PlayerGameSave _playerGameSave, int move)
        {
            // TODO add support for all 4 types of movement tracking

            _playerGameSave.TotalCoordinateHistory += move.ToString() + ',';
            _playerGameSave.CurrentCoordinateHistory += move.ToString() + ',';

            return _playerGameSave;
        }
        private void PrintSystemsState(SystemsModel[] systemsModels, Dictionary<string, bool[]> systemsState)
        {
            foreach (var i in systemsModels)
            {
                i.isFixed = systemsState[i.Power][i.DbIndex];
                // Outputs each systems state object
                Console.WriteLine($"=================================================\r\n" +
                    $"Id: {i.Id}\r\n" +
                    $"HtmlId: {i.HtmlId}\r\n" +
                    $"Pipe: {i.Pipe}\r\n" +
                    $"Direction: {i.Direction}\r\n" +
                    $"Power: {i.Power}\r\n" +
                    $"DbIndex: {i.DbIndex}\r\n" +
                    $"IsFixed: {i.isFixed}");
            }
        }
    }
}
