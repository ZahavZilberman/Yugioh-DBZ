using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Media;

using static System.Net.WebRequestMethods;

public class Duel
{
    #region ctor

    public Duel(List<Player> Players)
    {
        PlayersList = Players;
        CurrentPlayer = PlayersList[0];
        OpponentPlayer = PlayersList[1];
        Winner = null;
        Loser = null;
        ThePlayerWhoStarts = null;
        ThePlayerWhoDoesNotStart = null;
        FieldInstance = new Field();
        GameStates = new List<string>();
        GameStates.Add("Start");
        GameStates.Add("Draw");
        GameStates.Add("Standby");
        GameStates.Add("Main1");
        GameStates.Add("Battle");
        GameStates.Add("Main2");
        GameStates.Add("End");
        GameStates.Add("EndTurn");
        GameStates.Add("EndPhase");
        GameStates.Add("EndGame");

        CurrentGameState = GameStates[0];
        DuelTypes = new List<string>();
        DuelTypes.Add("Single");
        DuelTypes.Add("Match");

        TheDuelType = "";
        WinnerPlayers = new List<Player>();

        Random rnd = new Random();
        int choosenNumber = rnd.Next(rnd.Next(1, 2));
        if (choosenNumber == 1)
        {
            ThePlayerWhoStarts = PlayersList[0];
            ThePlayerWhoDoesNotStart = PlayersList[1];
        }
        else
        {
            ThePlayerWhoStarts = PlayersList[1];
            ThePlayerWhoDoesNotStart = PlayersList[0];
        }

        MonsterFields = new List<Card>();
        SpellFields = new List<Card>();
        TrapFields = new List<Card>();
    }

        #endregion

    #region Methods

        public void StartDuel()
        {
            if (TheDuelType == "Single")
            {
                while (Winner == null)
                {
                    if (CurrentPlayer == ThePlayerWhoStarts)
                    {
                        CurrentPlayer.DeckInstance.DrawCard();
                    }
                }
            }
            else if (TheDuelType == "Match")
            {
                while (WinnerPlayers.Count < 2)
                {
                    if (CurrentPlayer == ThePlayerWhoStarts)
                    {
                    CurrentPlayer.DeckInstance.DrawCard();
                }
                }
            }
            return;
        }

        public void EndDuel()
        {
            if (TheDuelType == "Single")
            {
                if (Winner == PlayersList[0])
                {
                    Loser = PlayersList[1];
                }
                else
                {
                    Loser = PlayersList[0];
                }

            }
            else if (TheDuelType == "Match")
            {
                if (WinnerPlayers[0] == PlayersList[0])
                {
                    WinnerPlayers.Add(PlayersList[1]);
                }
                else
                {
                    WinnerPlayers.Add(PlayersList[0]);
                }
            }

            // in this part you can add all the stupid winning and losing sound effects, animations, etc.
            return;
        }

        // condier adding a function here that resets all properties to their default values.

        #endregion

    #region Properties

    public Actions Actions { get; set; }
    public List<Player> PlayersList { get; set; }
    public Player CurrentPlayer { get; set; }
    public Player OpponentPlayer { get; set; }
    public Player Winner { get; set; }
    public Player Loser { get; set; }
    public Player ThePlayerWhoStarts { get; set; }
    public Player ThePlayerWhoDoesNotStart { get; set; }
    public Field FieldInstance { get; set; }
    public List<string> GameStates { get; set; }
    public string CurrentGameState { get; set; }
    public List<string> DuelTypes { get; set; }
    public string TheDuelType { get; set; }
    public List<Player> WinnerPlayers { get; set; }
    public List<Card> MonsterFields { get; set; }
    public List<Card> SpellFields { get; set; }
    public List<Card> TrapFields { get; set; }
    #endregion
}