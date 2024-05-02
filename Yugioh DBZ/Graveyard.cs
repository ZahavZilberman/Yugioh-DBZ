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

public class Graveyard
{
    #region ctor

    public Graveyard(Player player)
    {
        AllCards = new List<Card>();
        MonsterCards = new List<Card>();
        SpellCards = new List<Card>();
        TrapCards = new List<Card>();
        WhichPlayerHasThisGraveyard = player;
        IsEmpty = true;
        NumberOfCardsInGraveyard = 0;
    }

    #endregion

    #region methods

    public void AddCardToGraveyard(Card card)
    {
        if (card.TheCardType.Contains("Monster"))
        {
            MonsterCards.Add(card);
        }
        else if (card.TheCardType.Contains("Spell"))
        {
            SpellCards.Add(card);
        }
        else if (card.TheCardType.Contains("Trap"))
        {
            TrapCards.Add(card);
        }
        AllCards.Add(card);
        IsEmpty = false;
        IsACardBeingAddedToTheGraveyard = true;
        NumberOfCardsInGraveyard++;
    }

    public void RemoveCardFromGraveyard(Card card)
    {
        if (card.TheCardType.Contains("Monster"))
        {
            MonsterCards.Remove(card);
        }
        else if (card.TheCardType.Contains("Spell"))
        {
            SpellCards.Remove(card);
        }
        else if (card.TheCardType.Contains("Trap"))
        {
            TrapCards.Remove(card);
        }
        AllCards.Remove(card);
        IsEmpty = false;
        IsACardBeingRemovedFromTheGraveyard = true;
        NumberOfCardsInGraveyard--;
    }

    public void RemoveAllCardsFromGraveyard()
    {
        MonsterCards.Clear();
        SpellCards.Clear();
        TrapCards.Clear();
        AllCards.Clear();
        IsEmpty = true;
        IsACardBeingRemovedFromTheGraveyard = true;
        NumberOfCardsInGraveyard = 0;
    }

    public List<Card> GetGraveyardCards()
    {
        return AllCards;
    }
    #endregion

    #region Properties

    public List<Card> AllCards { get; set; }
    public List<Card> MonsterCards { get; set; }
    public List<Card> SpellCards { get; set; }
    public List<Card> TrapCards { get; set; }
    public Player WhichPlayerHasThisGraveyard { get; set; }
    public bool IsEmpty { get; set; }
    public bool IsACardBeingAddedToTheGraveyard { get; set; }
    public bool IsACardBeingRemovedFromTheGraveyard { get; set; }
    public int NumberOfCardsInGraveyard { get; set; }

    #endregion
}
