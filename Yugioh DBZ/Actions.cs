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

public class Actions
{
    #region ctor
    public Actions()
    {
        MonsterActions = new List<string>();
        SpellActions = new List<string>();
        TrapActions = new List<string>();

        MonsterActions.Add("Summon in attack position");
        MonsterActions.Add("Summon in defense position");
        MonsterActions.Add("Summon face down");
        MonsterActions.Add("Send to GraveyardInstance");
        MonsterActions.Add("Send to HandInstance");
        MonsterActions.Add("Special summon fusion");
        MonsterActions.Add("Special summon ritual");

        SpellActions.Add("Activate");
        SpellActions.Add("Set");
        SpellActions.Add("Send to GraveyardInstance");
        SpellActions.Add("Send to HandInstance");

        TrapActions.Add("Activate");
        TrapActions.Add("Set");
        TrapActions.Add("Send to GraveyardInstance");
        TrapActions.Add("Send to HandInstance");

        ViewCardsActions = new List<string>();
        ViewCardsActions.Add("View cards in HandInstance");
        ViewCardsActions.Add("View cards in DeckInstance");
        ViewCardsActions.Add("View cards in GraveyardInstance");
        ViewCardsActions.Add("View cards in extra DeckInstance");

        PageScroll = new List<int>(); // apply two buttons to go to next page or previous page
        CurrentPage = 0;
        SelectedCard = 0;
        IsCardSelected = false;
    }
    #endregion

    #region Properties

    public List<string> MonsterActions { get; set; }
    public List<string> SpellActions { get; set; }
    public List<string> TrapActions { get; set; }
    public List<string> ViewCardsActions { get; set; }
    public List<string> MonsterActionField { get; set; }
    public List<string> SpellActionField { get; set; }
    public List<string> TrapActionField { get; set; }
    public List<int> PageScroll { get; set; } //split the view cards into pages of 10 cards per page
    public int CurrentPage { get; set; }
    public int SelectedCard { get; set; }
    public bool IsCardSelected { get; set; }

    #endregion
}
