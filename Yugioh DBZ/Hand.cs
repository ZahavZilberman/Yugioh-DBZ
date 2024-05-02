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

public class Hand
{
    #region ctor

    public Hand(Player player)
    {
        AllCards = new List<Card>();
        MonsterCards = new List<Card>();
        SpellCards = new List<Card>();
        TrapCards = new List<Card>();
        WhichPlayerHasThisHand = player;
        HowManyCardsAreInHand = 0;
        MaxCardsInHand = 10;
        IsHandFull = false;
        HasPlayerSelectedACard = false;
        WhichCardDidThePlayerSelect = null;
        HasThePlayerSelectedAMonsterCard = false;
        HasThePlayerSelectedASpellCard = false;
        HasThePlayerSelectedATrapCard = false;
        HasThePlayerChoosedToSummonAMonster = false;
        HasThePlayerChoosedToSetAMonster = false;
        HasThePlayerChoosedToSetASpell = false;
        HasThePlayerChoosedToSetATrap = false;
        HasThePlayerChoosedToActivateASpell = false;

        for (int start = 0; start < 5; start++)
        {
            player.DeckInstance.DrawCard();
        }
    }

    #endregion

    #region methods

    public bool CheckingIfHandIsFull(List<Card> cards)
    {

        if (HowManyCardsAreInHand >= MaxCardsInHand)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddCardToHand(Card card)
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
        HowManyCardsAreInHand++;
        IsHandFull = CheckingIfHandIsFull(AllCards);
        // should we add a "set in hand" method here?
    }

    public void RemoveCardFromHand(Card card)
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
        HowManyCardsAreInHand--;
        IsHandFull = CheckingIfHandIsFull(AllCards);
    }

    public void RemoveAllCardsFromHand()
    {
        MonsterCards.Clear();
        SpellCards.Clear();
        TrapCards.Clear();
        AllCards.Clear();
        HowManyCardsAreInHand = 0;
        IsHandFull = CheckingIfHandIsFull(AllCards);
    }

    public List<Card> GetAllCardsInHand()
    {
        return AllCards;
    }

    public bool IsHandEmpty()
    {
        return HowManyCardsAreInHand == 0;
    }

    public int GetHandSize()
    {
        return AllCards.Count;
    }
    #endregion

    #region properties
    public int HowManyCardsAreInHand { get; set; }
    public int MaxCardsInHand { get; set; }
    public List<Card> AllCards { get; set; }
    public List<Card> MonsterCards { get; set; }
    public List<Card> SpellCards { get; set; }
    public List<Card> TrapCards { get; set; }
    public Player WhichPlayerHasThisHand { get; set; }
    public bool IsHandFull { get; set; }
    public bool HasPlayerSelectedACard { get; set; }
    public Card WhichCardDidThePlayerSelect { get; set; }
    public bool HasThePlayerSelectedAMonsterCard { get; set; }
    public bool HasThePlayerSelectedASpellCard { get; set; }
    public bool HasThePlayerSelectedATrapCard { get; set; }
    public bool HasThePlayerChoosedToSummonAMonster { get; set; }
    public bool HasThePlayerChoosedToSetAMonster { get; set; }
    public bool HasThePlayerChoosedToSetASpell { get; set; }
    public bool HasThePlayerChoosedToSetATrap { get; set; }
    public bool HasThePlayerChoosedToActivateASpell { get; set; }
    #endregion


}
