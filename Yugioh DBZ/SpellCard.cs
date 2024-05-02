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

public class SpellCard:Deck
{
    #region ctor
    public SpellCard(Card card, string spellType)
    {
        Duration = 0;

        SpellType = new List<string>();
        SpellType.Add("Normal");
        SpellType.Add("Continuous");
        SpellType.Add("Equiq");
        SpellType.Add("Ritual");
        SpellType.Add("Fusion");

        WhoCanBeContinous = new List<MonsterCard>();
        WhoCanBeEquiqed = new List<MonsterCard>();

        if (spellType == "Normal")
        {
            IsForFusionSummon = false;
            IsForRitualSummon = false;
        }
        if(spellType == "Continuous")
        {
            IsForFusionSummon = false;
            IsForRitualSummon = false;
        }
        if(spellType == "Equiq")
        {
            IsForFusionSummon = false;
            IsForRitualSummon = false;
            WhichEquiqSpellCardIsThisUsedOn = null;
        }
        if(spellType == "Ritual")
        {
            IsForFusionSummon = false;
            IsForRitualSummon = true;
            CardSacraficedToRitualSummon = null;
            CardThatItCanRitualSummon = null;
            // you can get the names and monstercards easily from the card object
        }
        if(spellType == "Fusion")
        {
            IsForFusionSummon = true;
            IsForRitualSummon = false;

        }

        PositionInDeck = 0;

        Name = card.Name;
        // need to get the object itself for this PictureFile = card.PictureFile;
        Description = card.Description;
        IsActivated = false;
        IsFaceDownBool = false;
        IsInHand = false;
        IsInDeck = true;
        IsInGraveyard = false;
        IsForFusionSummon = false;
        IsForRitualSummon = false;
        Position = "Deck";

    }
    #endregion

    #region Methods



    #endregion

    #region Properties
    public Card CardInstance { get; set; }
    public new string Name { get; set; }
    public string PictureFile { get; set; }
    public string Position { get; set; }
    public new string Description { get; set; }
    public bool IsActivated { get; set; }
    public bool IsFaceDownBool { get; set; }
    public bool IsInHand { get; set; }
    public bool IsInDeck { get; set; }
    public bool IsInField { get; set; }
    public bool IsChaining { get; set; }
    public bool IsInGraveyard { get; set; }
    public bool IsForRitualSummon { get; set; }
    public bool IsForFusionSummon { get; set; }
    public string NameOfMonsterTribute { get; set; }
    public string NameOfMonsterToSummon { get; set; }
    public MonsterCard CardSacraficedToRitualSummon { get; set; }
    public MonsterCard CardThatItCanRitualSummon { get; set; }
    public XElement SpellCardDatabase { get; set; }
    public List<string> SpellType { get; set; }
    public int PositionInDeck { get; set; }
    public string Effect { get; set; }
    public List<MonsterCard> WhoCanBeContinous { get; set; }
    public List<MonsterCard> WhoCanBeEquiqed { get; set; }
    public MonsterCard WhichEquiqSpellCardIsThisUsedOn { get; set; }
    public int Duration { get; set; }
    public Player Owner { get; set; }
    /*
    public enum SpellType
	{
        Normal,
        Continuous,
        Field,
        Equip,
        QuickPlay,
        Ritual,
		Fusion
    }
	*/
    #endregion
}
