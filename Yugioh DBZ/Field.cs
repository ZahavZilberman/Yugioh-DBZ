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

public class Field
{
    #region ctor
    public Field()
    {
        AllCards = new List<Card>();
        MonsterCards = new List<Card>();
        SpellCards = new List<Card>();
        TrapCards = new List<Card>();

        IsThereACardHere = new List<bool>();
        for (int start = 0; start < 20; start++)
        {
            IsThereACardHere.Add(false);
        }
        for (int start = 0; start < 20; start++)
        {
            MonsterCards.Add(null);
            SpellCards.Add(null);
            TrapCards.Add(null);
            AllCards.Add(null);
        }


        MonstersInDefenseMood = new List<MonsterCard>();
        MonstersInAttackMood = new List<MonsterCard>();
        MonstersInFaceDown = new List<MonsterCard>();
        ActivatedSpells = new List<SpellCard>();
        FaceDownSpells = new List<SpellCard>();
        ActivatedTraps = new List<TrapCard>();
        FaceDownTraps = new List<TrapCard>();

        HasAPlayerChhosenToAttack = false;
        HasAPlayerChhosenToSetASpell = false;
        HasAPlayerChhosenToSetATrap = false;
        HasAPlayerChhosenToSummonAMonster = false;
        HasAPlayerChhosenToActivateASpell = false;
        HasAPlayerChhosenToActivateATrap = false;
        HasAPlayerChhosenToSetAMonsterInDefenseMode = false;
        HasAPlayerChhosenToSetAMonsterInAttackMode = false;
        HasAPlayerGotAttacked = false;
        HasAPlayerGotACardDestroyed = false;
        HasAPlayerGotAMonsterDestroyed = false;
        HasAPlayerGotASpellDestroyed = false;
        HasAPlayerGotATrapDestroyed = false;
        HasAPlayerGotALifePointChange = false;
        HasAPlayerGotAMonsterPositionChange = false;
        HasAPlayerGotASpellPositionChange = false;
        HasAPlayerGotATrapPositionChange = false;

    }

    #endregion

    #region methods

    public void AddCardToField(Card card)
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
        IsACardBeingAddedToTheField = true;
    }

    public void RemoveCardFromField(Card card)
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
        IsACardBeingAddedToTheField = false;
    }

    public List<Card> GetFieldContents()
    {
        return AllCards;
    }

    public bool IsFieldEmpty()
    {
        return IsEmpty;
    }

    public int GetNumberOfCardsInField()
    {
        return AllCards.Count();
    }

    public int GetFieldNumOfCards()
    {
        return AllCards.Count();
    }

    public bool IsFieldFull()
    {
        return AllCards.Count() == 20;
    }

    public bool IsMonsterCardInField()
    {
        return MonsterCards.Count() > 0;
    }
    /*
    public List<MonsterCard> GetMonsterCardPosition()
    {
        foreach (MonsterCard monster in MonsterCards)
        {
            if (monster.Position == "Attack")
            {
                monstersInAttackMood.Add(monster);
            }
            else if (monster.Position == "Defense")
            {
                monstersInDefenseMood.Add(monster);
            }
            else if (monster.Position == "FaceDown")
            {
                monstersInFaceDown.Add(monster);
            }
        }

        foreach (SpellCard spellCard in SpellCards)
        {
            if (spellCard.Position == "FaceDown")
            {
                FaceDownSpells.Add(spellCard);
            }
            else if (spellCard.Position == "Activated")
            {
                ActivatedSpells.Add(spellCard);
            }
        }

        foreach (TrapCard trapCard in TrapCards)
        {
            if (trapCard.Position == "FaceDown")
            {
                FaceDownTraps.Add(trapCard);
            }
            else if (trapCard.Position == "Activated")
            {
                ActivatedTraps.Add(trapCard);
            }
        }
        throw new Exception();
    }
    */

    public void RemoveAllCardsFromField()
    {
        AllCards.Clear();
        MonsterCards.Clear();
        SpellCards.Clear();
        TrapCards.Clear();
        MonstersInAttackMood.Clear();
        MonstersInDefenseMood.Clear();
        MonstersInFaceDown.Clear();
        ActivatedSpells.Clear();
        FaceDownSpells.Clear();
        ActivatedTraps.Clear();
        FaceDownTraps.Clear();
        IsEmpty = true;
    }

    #endregion

    #region Properties
    public List<Card> AllCards { get; set; }
    public List<Card> MonsterCards { get; set; }
    public List<Card> SpellCards { get; set; }
    public List<Card> TrapCards { get; set; }
    public List<string> CardPosition { get; set; }
    public List<bool> IsThereACardHere { get; set; }
    public List<MonsterCard> MonstersInDefenseMood { get; set; }
    public List<MonsterCard> MonstersInAttackMood { get; set; }
    public List<MonsterCard> MonstersInFaceDown { get; set; }
    public List<SpellCard> ActivatedSpells { get; set; }
    public List<SpellCard> FaceDownSpells { get; set; }
    public List<TrapCard> ActivatedTraps { get; set; }
    public List<TrapCard> FaceDownTraps { get; set; }
    public bool IsEmpty { get; set; }
    public bool HasAPlayerChhosenToAttack { get; set; }
    public bool HasAPlayerChhosenToSetASpell { get; set; }
    public bool HasAPlayerChhosenToSetATrap { get; set; }
    public bool HasAPlayerChhosenToSummonAMonster { get; set; }
    public bool HasAPlayerChhosenToActivateASpell { get; set; }
    public bool HasAPlayerChhosenToActivateATrap { get; set; }
    public bool HasAPlayerChhosenToSetAMonsterInDefenseMode { get; set; }
    public bool HasAPlayerChhosenToSetAMonsterInAttackMode { get; set; }
    public bool HasAPlayerGotAttacked { get; set; }
    public bool HasAPlayerGotACardDestroyed { get; set; }
    public bool HasAPlayerGotAMonsterDestroyed { get; set; }
    public bool HasAPlayerGotASpellDestroyed { get; set; }
    public bool HasAPlayerGotATrapDestroyed { get; set; }
    public bool HasAPlayerGotALifePointChange { get; set; }
    public bool HasAPlayerGotAMonsterPositionChange { get; set; }
    public bool HasAPlayerGotASpellPositionChange { get; set; }
    public bool HasAPlayerGotATrapPositionChange { get; set; }
    public bool IsACardBeingAddedToTheField { get; set; }
    #endregion
}
