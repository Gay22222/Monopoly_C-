﻿// The TradeWindow that lets the player trade with another player.
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonopolyClient
{
    public partial class TradeWindow : Form
    {
        public TradeWindow()
        {
            // Make a new client.
            m_client = new Client();
            InitializeComponent();
            m_requestedPlayer = "";
        }
        // Constructor that initializes a client and gets the appropriate information needed for the window.
        public TradeWindow(Client a_client)
        {
            // Initialize the client with the client passed in.
            m_client = a_client;
            // Initialize components.
            InitializeComponent();
            m_requestedPlayer = "";
            // Get the names of the players and add them to the ComboBox.
            GetNames();
            // Add the properties the requester owns to the list box.
            GetRequesterProperties();
        }
        // Gets the list of names of players in the game, besides the one who is requesting a trade.
        private void GetNames()
        {
            // Get the client's updated player list.
            List<string> playerNames = m_client.PlayerNames;
            // Add these names to the ComboBox.
            foreach(string name in playerNames)
            {
                namesBox.Items.Insert(0, name);
            }
        }
        // Adds the requester's properties to the corresponding list box.
        private void GetRequesterProperties()
        {
            // Get the player's properties.
            Hashtable propertyList = m_client.PropertyList;
            // Cycle through.
            foreach (DictionaryEntry de in propertyList)
            {
                // Is this propery owned by the player, and does it have no buildings?
                if ((bool)de.Value == true && (int)m_client.PropertiesWithBuildings[de.Key] == 0)
                {
                    // Add it to the list.
                    yourPropertiesBox.Items.Add(de.Key);
                }
            }
        }
        
        private void namesBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // First, clear the requested player property list in the client.
            m_client.RequestedPlayerProperties.Clear();
            // Also clear the list box of the other player's properties.
            theirPropertiesBox.Items.Clear();
            // Get the ComboBox.
            ComboBox namesBox = (ComboBox)sender;
            // Get the selected name.
            string selectedName = (string)namesBox.SelectedItem;
            // Store it for later.
            m_requestedPlayer = selectedName;
            // Send a GetProperties command to the server to get a list of available properties to trade.
            this.Text = "Getting properties...";
            m_client.SendCommand("getProperties", selectedName);
            // Now add them to the list box after waiting a little bit.
            System.Threading.Thread.Sleep(1500);
            this.Text = "Trade";
            foreach(string property in m_client.RequestedPlayerProperties)
            {
                theirPropertiesBox.Items.Add(property);
            }
        }
        
        private void offerButton_Click(object sender, EventArgs e)
        {
            // If there are no selections in either of the list boxes, return.
            if (yourPropertiesBox.SelectedIndex == -1 && theirPropertiesBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select at least one property from either list before making an offer.", "Trade Error");
                return;
            }
            // Next, check the text boxes for money. If what was entered is not a number, say so.
            int yourMoney = 0, theirMoney = 0;
            if (!Int32.TryParse(yourMoneyBox.Text, out yourMoney) || !Int32.TryParse(theirMoneyBox.Text, out theirMoney))
            {
                MessageBox.Show("Please enter a real, positive integer for money.", "Money Error");
                return;
            }
            // The money amounts cannot be negative, either.
            if (yourMoney < 0 || theirMoney < 0)
            {
                MessageBox.Show("Please enter an integer above 0 for money.", "Money Error");
                return;
            }
            // We also need to make sure that funds are sufficient enough for both players.
            m_client.SendCommand("getFunds", (string)namesBox.SelectedItem);
            // Wait while the server sends funds.
            System.Threading.Thread.Sleep(150);
            if (yourMoney > m_client.Funds)
            {
                MessageBox.Show("Please enter money that does not exceed how much money you have.", "Money Error");
                return;
            }
            if (theirMoney > m_client.RequestedPlayerFunds)
            {
                MessageBox.Show("Please enter money that does not exceed how much money the requested player has.", "Money Error");
                return;
            }

            // All the checks seem to have passed. So let's start the trade offer!
            SendTradeRequest();
        }
        
        private void SendTradeRequest()
        {
            // Get the necessary info we need to send.
            string propertyOffered = "";
            string moneyOffered = "";
            string propertyRequested = "";
            string moneyRequested = "";

            // Was there a property offered by this player?
            if (yourPropertiesBox.SelectedIndex != -1)
            {
                // Record it.
                propertyOffered = yourPropertiesBox.SelectedItem.ToString();
            }
            // Otherwise, record it as null.
            else
            {
                propertyOffered = "null";
            }

            // Was there money offered by this player?
            if (yourMoneyBox.Text != "0")
            {
                // Record it.
                moneyOffered = yourMoneyBox.Text;
            }
            else
            {
                // Otherwise, record it as null.
                moneyOffered = "null";
            }

            // Was there a property requested by this player?
            if (theirPropertiesBox.SelectedIndex != -1)
            {
                // Record it.
                propertyRequested = theirPropertiesBox.SelectedItem.ToString();
            }
            // Otherwise, record it as null.
            else
            {
                propertyRequested = "null";
            }

            // Was there money requested by this player?
            if (theirMoneyBox.Text != "0")
            {
                // Record it.
                moneyRequested = theirMoneyBox.Text;
            }
            else
            {
                // Otherwise, record it as null.
                moneyRequested = "null";
            }

            // Now start sending the appropriate information over.
            string allTogether = propertyOffered + "," + moneyOffered + "," + (string)namesBox.SelectedItem
                + "," + propertyRequested + "," + moneyRequested;
            m_client.SendCommand("tradeRequest", allTogether);
            // Close the window.
            Close();
        }

        // The client who wants to trade.
        private Client m_client;
        // The player the client wants to trade with.
        private string m_requestedPlayer;
    }
}
