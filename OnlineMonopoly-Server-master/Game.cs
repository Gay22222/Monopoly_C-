// The Game class, which implements the entire game of Monopoly within this class.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineMonopoly
{
    class Game : Object
    {
    	// Default constructor.
    	public Game()
    	{
    		m_board = new Board();
    		m_playerRoster = new ArrayList();
    		m_bankruptPlayers = new ArrayList();
    		m_playerPosition = 0;
    	}

    	// Property to get the current player position.
    	public int PlayerPosition
    	{
    		get
    		{
    			return m_playerPosition;
    		}
    		set
    		{
    			m_playerPosition = value;
    		}
    	}

    	// Adds a player to the game.
    	public void AddPlayer(Player a_player)
    	{
            m_playerRoster.Add(a_player);
    	}

    	// Gets the player specified by the name.
    	public Player GetPlayer(string a_name)
    	{
    		foreach (Player player in m_playerRoster)
    		{
    			// Is this the player with the name we're looking for? If so, return them.
    			if (player.Name == a_name) return player;
    		}
    		// They weren't found. Just return a regular player.
    		return new Player();
    	}

    	// Gets a list of players in the game.
    	public List<string> GetPlayers()
    	{
    		// Initialize the list.
    		List<string> list = new List<string>();
    		foreach (Player player in m_playerRoster)
    		{
    			// Add the player's name to the list.
    			list.Add(player.Name);
    		}
    		return list;
    	}

    	// Gets the player at a specific position.
    	public Player GetPlayerAt(int position)
    	{
    		return (Player)m_playerRoster[position];
    	}

    	// Gets the funds for the player.
    	public int GetFundsForPlayer(string a_name)
    	{
    		return GetPlayer(a_name).Funds;
    	}

    	// Gets the space the player landed on.
    	public Space GetSpacePlayerIsOn(Player a_player)
    	{
    		return m_board.SpaceAt(a_player.Position);
    	}

    	// Gets how many players are still playing (not bankrupt).
    	public int PlayersStillPlaying
    	{
    		get
    		{
    			return m_playerRoster.Count;
    		}
    	}

    	
    	public void DoTurn(ref Player a_player)
    	{
    		// Check if the player is in jail.
    		if (a_player.IsInJail)
    		{
    			// We go through the jail turn process. First, check if the player has a "Get Out of Jail Free" card.
    			if (a_player.GetOutOfJailAmount > 0)
    			{
    				// Set them free! But also decrease the amount of "Get Out of Jail Free" cards they have.
    				a_player.GetOutOfJailAmount = a_player.GetOutOfJailAmount - 1;
    				a_player.IsInJail = false;
    				a_player.JailTurnCount = 0;
    			}
    			// Otherwise, we'll just have to go through the regular jail turn.
    			else
    			{
    				// Start the jail turn by rolling for doubles, and for freedom.
    				int firstJailDie = 0, secondJailDie = 0;
    				a_player.RollDice(ref firstJailDie, ref secondJailDie);
    				// Check if doubles were rolled.
    				if (firstJailDie == secondJailDie)
    				{
                        // The player has been freed! They can continue on.
                        Console.WriteLine("Doubles rolled! You are free from jail without paying up!");
    					a_player.IsInJail = false;
    					a_player.JailTurnCount = 0;
    					// Clear the double count as well.
    					a_player.DoublesCount = 0;
    				}
    				// Otherwise, they will remain in jail.
    				else
    				{
    					a_player.JailTurnCount = a_player.JailTurnCount + 1;
    					// If this is the player's third turn of failing a doubles roll, then they must.
    					if (a_player.JailTurnCount == 3)
    					{
    						a_player.IsInJail = false;
    						a_player.JailTurnCount = 0;
    					}
    					// Otherwise, their turn is over.
    					else return;
    				}
    			}
    		}
    		// Check the player's double count. If it's 3, well, the only place they're moving to is jail!
    		if (a_player.DoublesCount == 3)
	    	{
                // Write that the player is going to jail to the console. This also is for debugging purposes.
                Console.WriteLine("Too many doubles rolled. Off to jail with " + a_player.Name + "!");
	    		// Go directly to jail and end the turn.
	    		a_player.GoToJail();
	    		return;
	    	}
	    	// Otherwise, move them by the amount they rolled.
	    	a_player.MakeMove(a_player.DieRoll);
	    	Console.WriteLine(a_player.Name + " is now at " + GetSpacePlayerIsOn(a_player).Name);
    	}

    	
    	public string GetPositions()
    	{
            // Start building a string.
            string positionListing = "";
    		// Cycle through the players...
    		foreach (Player player in m_playerRoster)
    		{
    			// We must check for the jail space.
    			string spaceName = GetSpacePlayerIsOn(player).Name;
    			if (GetSpacePlayerIsOn(player).Name == "Just Visiting")
    			{
    				// Is the player in jail? If so, the space name is actually jail.
    				if (player.IsInJail) spaceName = "Jail";
    			}
    			// Add the player and their position.
    			positionListing += (player.Name + ": " + spaceName + "\n");
    		}

            return positionListing;
    	}
    	
    	public bool PlayerExistsInRoster(string a_name)
    	{
    		foreach (Player player in m_playerRoster)
    		{
    			// Is this player's name the same as the one in the argument?
    			if (player.Name == a_name)
    			{
    				// Return true, as the name already exists.
    				return true;
    			}
    		}
    		// The name does not exist in the player roster.
    		return false;
    	}
    	
    	public string EvaluateSpace(ref Player a_player)
    	{
    		// Get the space that the player landed on.
    		Space spaceLandedOn = GetSpacePlayerIsOn(a_player);
    		// Check if this space is a property.
    		if (spaceLandedOn.HasProperty)
    		{
    			// Get the property that this space contains, then get the property's owner.
    			Property spaceProperty = spaceLandedOn.GetProperty();
    			string propertyOwner = spaceProperty.Owner.Name;
    			// Is this property owned by anyone?
    			if (propertyOwner != "N/A")
    			{
    				// Check who the owner is.
    				// If it's the player on the space, don't do anything.
    				if (propertyOwner == a_player.Name)
    				{
    					return "nothing";
    				}
    				// If the property is mortgaged, don't do anything.
    				else if (spaceProperty.IsMortgaged)
    				{
    					return "nothing";
    				}
    				// Otherwise, pay up the rent!
    				else
    				{
    					return "pay up";
    				}
    			}
    			// Otherwise, ask to buy the property.
    			else
    			{
                    // Before even asking if they want to buy, determine if funds are sufficient enough.
                    if (! (a_player.Funds >= spaceProperty.Price))
                    {
                    	// If not, they cannot buy it. Return nothing.
                    	return "nothing";
                    }
                    // Otherwise, they are able to buy the property.
                    return "wanna buy";
    			}
    		}
    		// If this space is not a property, it can only be a few other things. Check and see what they are.
    		else
    		{
    			// Set up a switch statement to do the appropriate evaluation.
    			switch (spaceLandedOn.Name)
    			{
    				case "Chance":
    					// Draw a Chance card.
    					return "chance";
    					//DrawCard(ref a_player, "Chance");
    					//break;
    				case "Community Chest":
    					// Draw a Community Chest card.
    					return "community chest";
    					//DrawCard(ref a_player, "CommunityChest");
    					//break;
    				case "Income Tax":
    					// Pay an income tax of $200. (I'm electing out of the 15% option here because let's be honest, nobody did that)
    					a_player.Funds = a_player.Funds - 200;
    					return "income tax";
    				case "Luxury Tax":
    					// Pay a luxury tax of $75.
    					a_player.Funds = a_player.Funds - 75;
    					return "luxury tax";
    				case "Go To Jail":
    					// Send the player to jail.
    					a_player.GoToJail();
    					return "jail";
    				default:
                        return "whatever";
    			}
    		}
    	}
    	
    	public int CalculateRent(Space a_space, int a_spacesMoved)
    	{
    		// Get the space's property.
    		Property propertyOnSpace = a_space.GetProperty();
    		// Get the property's color.
    		string propertyColor = propertyOnSpace.Color;
            // Initialize a potential property count.
            int propCount = 0;
    		// A switch statement seems more appropriate for rent checking than a bunch of if statements...
    		switch(propertyColor)
    		{
    			case "Railroad":
                    // Check how many railroads the owner has.
    				propCount = PropertiesOwned(propertyOnSpace);
    				// Have another switch statement for the property amount.
    				switch (propCount)
    				{
    					case 1:
    						return propertyOnSpace.Rent;
    					case 2:
    						return propertyOnSpace.Rent1House;
    					case 3:
    						return propertyOnSpace.Rent2House;
    					default:
    						return propertyOnSpace.Rent3House;
    				}
    			case "Utility":
                    // Check how many utilities the owner has. If one, rent is amount rolled * 4. Otherwise,
    				// it's amount rolled * 10.
    				propCount = PropertiesOwned(propertyOnSpace);
    				if (propCount == 1)
    				{
    					return a_spacesMoved * 4;
    				}
    				else
    				{
    					return a_spacesMoved * 10;
    				}
                default:
                	// This is a "normal" property, so we can calculate it normally.
                	// First, check and see if there is at least one building on the space.
                	if (a_space.BuildingAmount > 0)
                	{
                		// Return the appropriate rent for the corresponding building amount.
                		switch (a_space.BuildingAmount)
                		{
                			case 1:
                				return propertyOnSpace.Rent1House;
                			case 2:
                				return propertyOnSpace.Rent2House;
                			case 3:
                				return propertyOnSpace.Rent3House;
                			case 4:
                				return propertyOnSpace.Rent4House;
                			default:
                				return propertyOnSpace.RentHotel;
                		}
                	}
                	// Otherwise, check if the owner of the property owns all of the same color group.
                	// If so, return the rent owed * 2.
                	if (PlayerOwnsAll(propertyOnSpace)) return propertyOnSpace.Rent * 2;
                	// Nothing else needs to be checked. Return the rent.
                    return propertyOnSpace.Rent;
    		}
    	}
        
        public List<string> GetPropertiesToBuildOn(string a_name)
        {
            // Get the player from the name.
            Player player = GetPlayer(a_name);
            // Start making a list:
            List<string> buildingList = new List<string>();
            // Start looking at all the properties.
            Property propertyToCheck = m_board.GetSpaceWithProperty("Mediterranean Avenue").GetProperty();
            if (PlayerOwnsAll(propertyToCheck) && propertyToCheck.Owner.Name == a_name)
            {
                // Add the purple properties to the list.
                buildingList.Add("Mediterranean Avenue");
                buildingList.Add("Baltic Avenue");
            }
            propertyToCheck = m_board.GetSpaceWithProperty("Oriental Avenue").GetProperty();
            if (PlayerOwnsAll(propertyToCheck) && propertyToCheck.Owner.Name == a_name)
            {
                // Add the light blue properties to the list.
                buildingList.Add("Oriental Avenue");
                buildingList.Add("Vermont Avenue");
                buildingList.Add("Connecticut Avenue");
            }
            propertyToCheck = m_board.GetSpaceWithProperty("St. Charles Place").GetProperty();
            if (PlayerOwnsAll(propertyToCheck) && propertyToCheck.Owner.Name == a_name)
            {
                // Add the pink properties to the list.
                buildingList.Add("St. Charles Place");
                buildingList.Add("States Avenue");
                buildingList.Add("Virginia Avenue");
            }
            propertyToCheck = m_board.GetSpaceWithProperty("St. James Place").GetProperty();
            if (PlayerOwnsAll(propertyToCheck) && propertyToCheck.Owner.Name == a_name)
            {
                // Add the orange properties to the list.
                buildingList.Add("St. James Place");
                buildingList.Add("Tennessee Avenue");
                buildingList.Add("New York Avenue");
            }
            propertyToCheck = m_board.GetSpaceWithProperty("Kentucky Avenue").GetProperty();
            if (PlayerOwnsAll(propertyToCheck) && propertyToCheck.Owner.Name == a_name)
            {
                // Add the red properties to the list.
                buildingList.Add("Kentucky Avenue");
                buildingList.Add("Indiana Avenue");
                buildingList.Add("Illinois Avenue");
            }
            propertyToCheck = m_board.GetSpaceWithProperty("Atlantic Avenue").GetProperty();
            if (PlayerOwnsAll(propertyToCheck) && propertyToCheck.Owner.Name == a_name)
            {
                // Add the yellow properties to the list.
                buildingList.Add("Atlantic Avenue");
                buildingList.Add("Ventnor Avenue");
                buildingList.Add("Marvin Gardens");
            }
            propertyToCheck = m_board.GetSpaceWithProperty("Pacific Avenue").GetProperty();
            if (PlayerOwnsAll(propertyToCheck) && propertyToCheck.Owner.Name == a_name)
            {
                // Add the green properties to the list.
                buildingList.Add("Pacific Avenue");
                buildingList.Add("North Carolina Avenue");
                buildingList.Add("Pennsylvania Avenue");
            }
            propertyToCheck = m_board.GetSpaceWithProperty("Park Place").GetProperty();
            if (PlayerOwnsAll(propertyToCheck) && propertyToCheck.Owner.Name == a_name)
            {
                // Add the blue properties to the list.
                buildingList.Add("Park Place");
                buildingList.Add("Boardwalk");
            }
            // Return the final list.
            return buildingList;
        }
        
        public bool AllPropertiesHaveNoHouses(string a_color)
        {
            switch(a_color)
            {
                case "Purple":
                    // If Mediterranean or Baltic have one building, return false.
                    if (m_board.GetSpaceWithProperty("Mediterranean Avenue").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("Baltic Avenue").BuildingAmount > 0)
                    {
                        return false;
                    }
                    return true;
                case "Light Blue":
                    // If any of the Light Blue properties have one building, return false.
                    if (m_board.GetSpaceWithProperty("Oriental Avenue").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("Vermont Avenue").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("Connecticut Avenue").BuildingAmount > 0)
                    {
                        return false;
                    }
                    return true;
                case "Pink":
                    // If any of the Pink properties have one building, return false.
                    if (m_board.GetSpaceWithProperty("St. Charles Place").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("States Avenue").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("Virginia Avenue").BuildingAmount > 0)
                    {
                        return false;
                    }
                    return true;
                case "Orange":
                    // If any of the Orange properties have one building, return false.
                    if (m_board.GetSpaceWithProperty("St. James Place").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("Tennessee Avenue").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("New York Avenue").BuildingAmount > 0)
                    {
                        return false;
                    }
                    return true;
                case "Red":
                    // If any of the Red properties have one building, return false.
                    if (m_board.GetSpaceWithProperty("Kentucky Avenue").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("Indiana Avenue").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("Illinois Avenue").BuildingAmount > 0)
                    {
                        return false;
                    }
                    return true;
                case "Yellow":
                    // If any of the Yellow properties have one building, return false.
                    if (m_board.GetSpaceWithProperty("Atlantic Avenue").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("Ventnor Avenue").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("Marvin Gardens").BuildingAmount > 0)
                    {
                        return false;
                    }
                    return true;
                case "Green":
                    // If any of the Green properties have one building, return false.
                    if (m_board.GetSpaceWithProperty("Pacific Avenue").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("North Carolina Avenue").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("Pennsylvania Avenue").BuildingAmount > 0)
                    {
                        return false;
                    }
                    return true;
                case "Blue":
                    // If any of the Blue properties have one building, return false.
                    if (m_board.GetSpaceWithProperty("Park Place").BuildingAmount > 0 ||
                        m_board.GetSpaceWithProperty("Boardwalk").BuildingAmount > 0)
                    {
                        return false;
                    }
                    return true;
                default:
                    return false;
            }
        }
        // Shifts funds from one player to another.
        public void PayUp(int a_paymentAmount, ref Player a_playerPaying, ref Player a_playerReceiving)
        {
            // Subtract the payment amount from the player paying and add it to the funds of the player receiving.
            a_playerPaying.Funds = a_playerPaying.Funds - a_paymentAmount;
            a_playerReceiving.Funds = a_playerReceiving.Funds + a_paymentAmount;
        }
        
        public Card DrawCard(ref Player a_player, string a_deckName)
        {
            // First, draw the card that's on top of the deck.
            Card topCard = new Card();
            if (a_deckName == "Chance") topCard = m_board.DrawFromDeck("Chance");
            else topCard = m_board.DrawFromDeck("CommunityChest");
            // Now put the card on the bottom.
            if (a_deckName == "Chance") m_board.PutCardOnBottom("Chance");
            else m_board.PutCardOnBottom("CommunityChest");
            return topCard;
        }
        
        public void PerformCardAction(ref Player a_player, string a_action)
        {
            // A switch statement is appropriate for this!
            switch (a_action)
            {
                case "advanceGo":
                    // Advance to Go, collect $200.
                    a_player.Position = 0;
                    a_player.Funds = a_player.Funds + 200;
                    break;
                case "advanceIllinois":
                    // Advance to Illinois Avenue.
                    // First, see if the player's position is greater than Illinois Ave's (they are past the space).
                    if (a_player.Position > 24)
                    {
                        // If so, the player will pass Go when advancing to Illinois. Collect $200.
                        a_player.Funds = a_player.Funds + 200;
                    }
                    a_player.Position = 24;
                    break;
                case "advanceStCharles":
                    // Advance to St. Charles Place.
                    // First, see if the player's position is greater than St. Charles Place's (they are past the space).
                    if (a_player.Position > 11)
                    {
                        // If so, the player will pass Go when advancing to St Charles. Collect $200.
                        a_player.Funds = a_player.Funds + 200;
                    }
                    a_player.Position = 11;
                    break;
                case "advanceUtility":
                    // Advance to the nearest utility.
                    // If the player is closer to Electric Company, move there.
                    if (a_player.Position < 12)
                    {
                        a_player.Position = 12;
                    }
                    // If the player is closer to Water Works, move there.
                    else if (a_player.Position < 28)
                    {
                        a_player.Position = 28;
                    }
                    // If the player is past Water Works, they are closer to Electric Company and will need to pass Go to get there.
                    else
                    {
                        a_player.Funds = a_player.Funds + 200;
                        a_player.Position = 12;
                    }
                    break;
                case "advanceRailroad":
                    // Advance to the nearest Railroad.
                    // If the player is closer to Pennsylvania, move there.
                    if (a_player.Position < 15)
                    {
                        a_player.Position = 15;
                    }
                    // If the player is closer to B&O, move there.
                    else if (a_player.Position < 25)
                    {
                        a_player.Position = 25;
                    }
                    // If the player is closer to Reading, move there. The chance to move there is always before Go.
                    else
                    {
                        a_player.Funds = a_player.Funds + 200;
                        a_player.Position = 5;
                    }
                    break;
                case "bankDividend":
                    // Bank pays you dividend of $50.
                    a_player.Funds = a_player.Funds + 50;
                    break;
                case "jailFree":
                    // Get Out of Jail Free.
                    a_player.GetOutOfJailAmount = a_player.GetOutOfJailAmount + 1;
                    break;
                case "goBack3":
                    // Go back 3 spaces.
                    a_player.Position = a_player.Position - 3;
                    break;
                case "jail":
                    // Go directly to Jail. Do not pass Go and do not collect $200.
                    a_player.GoToJail();
                    break;
                case "generalRepairs":
                    // Make general repairs on all your property.
                    // $25 per house, $100 per hotel.
                    a_player.Funds = a_player.Funds - (25 * a_player.HouseAmount);
                    a_player.Funds = a_player.Funds - (100 * a_player.HotelAmount);
                    break;
                case "poorTax":
                    // Pay Poor Tax of $15.
                    a_player.Funds = a_player.Funds - 15;
                    break;
                case "readingRailroad":
                    // Take a ride on the Reading!
                    // No matter what Chance space you land on, you will always pass Go to get on the Reading.
                    a_player.Funds = a_player.Funds + 200;
                    a_player.Position = 5;
                    break;
                case "boardwalk":
                    // Take a walk on the Boardwalk!
                    // No matter what Chance space you land on, you don't pass Go to get to Boardwalk.
                    a_player.Position = 39;
                    break;
                case "chairman":
                    // You have been elected Chairman of the Board! Pay each player $50.
                    PayEveryoneElse(ref a_player, 50);
                    break;
                case "matures":
                    // Your building and loan matures. Collect $150.
                    a_player.Funds = a_player.Funds + 150;
                    break;
                case "bankError":
                    // Bank error in your favor. Collect $200.
                    a_player.Funds = a_player.Funds + 200;
                    break;
                case "doctorsFee":
                    // Doctor's fee. Pay $50.
                    a_player.Funds = a_player.Funds - 50;
                    break;
                case "stockSale":
                    // From sale of stock, you get $50.
                    a_player.Funds = a_player.Funds + 50;
                    break;
                case "grandOpera":
                    // Grand Opera Opening, collect $50 from each player for opening night seats.
                    EveryonePays(ref a_player, 50);
                    break;
                case "xmasFund":
                    // X-Mas Fund matures. Collect $100.
                    a_player.Funds = a_player.Funds + 100;
                    break;
                case "taxRefund":
                    // Income tax refund, collect $20.
                    a_player.Funds = a_player.Funds + 20;
                    break;
                case "lifeInsurance":
                    // Life insurance matures. Collect $100.
                    a_player.Funds = a_player.Funds + 100;
                    break;
                case "hospitalFees":
                    // Pay hospital fees of $100.
                    a_player.Funds = a_player.Funds - 100;
                    break;
                case "schoolTax":
                    // Pay school tax of $150.
                    a_player.Funds = a_player.Funds - 150;
                    break;
                case "services":
                    // Receive for services $25.
                    a_player.Funds = a_player.Funds + 25;
                    break;
                case "streetRepairs":
                    // You are assessed for street repairs.
                    // $40 per house, $115 per hotel.
                    a_player.Funds = a_player.Funds - (40 * a_player.HouseAmount);
                    a_player.Funds = a_player.Funds - (115 * a_player.HotelAmount);
                    break;
                case "beautyContest":
                    // You have won second prize in a beauty contest. Collect $10.
                    a_player.Funds = a_player.Funds + 10;
                    break;
                case "inherit100":
                    // You inherit $100.
                    a_player.Funds = a_player.Funds + 100;
                    break;
                default:
                    break;
            }
        }
        // Finds the space of the property specified.
        public Space GetSpaceWithProperty(string a_propertyName)
        {
            // Call the function to find the space in the Board.
            return m_board.GetSpaceWithProperty(a_propertyName);
        }

        
        public void TradeProperty(ref Player a_playerLosingProperty, ref Player a_playerReceivingProperty, string a_propertyName)
        {
            // Get the property.
            Property tradedProperty = m_board.GetSpaceWithProperty(a_propertyName).GetProperty();
            // Remove the property from the player losing the property and add it to the player receiving it.
            a_playerLosingProperty.RemoveProperty(ref tradedProperty);
            a_playerReceivingProperty.AddProperty(ref tradedProperty);
        }

        
        public string BankruptcyCheck()
        {
            // cycle through the array list
            foreach (Player player in m_playerRoster)
            {
                // Does this player have funds less than 0?
                if (player.Funds < 0)
                {
                    // If so, they are bankrupt. Return their name.
                    return player.Name;
                }
            }
            // Otherwise, nobody is bankrupt! Return null.
            return "null";
        }

        
        public void LiquidateAndBankrupt(ref Player a_player)
        {
            // All the properties that this player owns are now open season.
            for (int i = 0; i < a_player.Properties.Count; i++)
            {
                // Change the property's owner to N/A and unmortgage them. Be sure to also remove any buildings it may have.
                Property property = (Property)a_player.Properties[i];
                property.Owner = new Player();
                property.IsMortgaged = false;
                GetSpaceWithProperty(property.Name).BuildingAmount = 0;
                // Finally, remove the propery from their list.
                a_player.Properties.Remove(property);
            }
            // Take this player out of the player roster and put them in bankrupt players.
            m_bankruptPlayers.Add(a_player);
            m_playerRoster.Remove(a_player);
        }
    	
    	private int PropertiesOwned(Property a_property)
    	{
    		// Get the owner of the property.
    		Player owner = a_property.Owner;
    		// Get all their properties.
    		ArrayList ownerProps = owner.Properties;
    		// Don't forget to intialize a count of the properties!
    		int propCount = 0;
    		// Start cycling through all of the properties that the owner has.
    		for (int i = 0; i < ownerProps.Count; i++)
    		{
                // Does this property's color match the color of the property passed in the argument?
                Property currentProperty = (Property)ownerProps[i];
    			if (currentProperty.Color == a_property.Color)
    			{
                    // If this property is not mortgaged, increment propCount.
    				if (!currentProperty.IsMortgaged) propCount++;
    			}
    		}
    		// Return the amount of those properties the owner has (and are not mortgaged).
    		return propCount;
    	}
    	
    	private bool PlayerOwnsAll(Property a_property)
    	{
    		// Get the color to check for.
    		string colorToCheck = a_property.Color;
    		// Get the amount of properties of this color the owner has.
    		int propCount = PropertiesOwned(a_property);
    		// Check if the property's owner owns all of the same color group. A monopoly, if you will...
    		switch (colorToCheck)
    		{
    			case "Purple":
    			case "Blue":
    				if (propCount == 2) return true;
    				return false;
    			case "Light Blue":
    			case "Pink":
    			case "Orange":
    			case "Red":
    			case "Yellow":
    			case "Green":
    				if (propCount == 3) return true;
    				return false;
    			default:
    				return false;
    		}
    	}
    	
    	private void PayEveryoneElse(ref Player a_player, int a_amount)
    	{
    		// Get the player's name.
    		string payingName = a_player.Name;
    		// Cycle through the list of players.
    		for (int i = 0; i < m_playerRoster.Count; i++)
    		{
    			// Pay the player if it is not the player who needs to pay up.
    			Player currentPlayer = (Player)m_playerRoster[i];
    			if (currentPlayer.Name != payingName)
    			{
    				a_player.Funds = a_player.Funds - a_amount;
    				currentPlayer.Funds = currentPlayer.Funds + a_amount;
    			}
    		}
    	}
    	
    	private void EveryonePays(ref Player a_player, int a_amount)
    	{
    		// Get the player's name.
    		string payingName = a_player.Name;
    		// Cycle through the list of players.
    		for (int i = 0; i < m_playerRoster.Count; i++)
    		{
    			// The player needs to pay up, unless this player is the player themselves.
    			Player currentPlayer = (Player)m_playerRoster[i];
    			if (currentPlayer.Name != payingName)
    			{
    				currentPlayer.Funds = currentPlayer.Funds - a_amount;
    				a_player.Funds = a_player.Funds + a_amount;
    			}
    		}
    	}

        // The board that the game will be played on.
        private Board m_board;
        // The players that will be playing the game.
        private ArrayList m_playerRoster;
        // The players that have been bankrupted (players that lost).
        private ArrayList m_bankruptPlayers;
        // The current player "position," also known as the player that is currently taking their turn in the game.
        private int m_playerPosition;
    }
}