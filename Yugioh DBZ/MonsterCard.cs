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

public class MonsterCard
{
    #region ctor
    public MonsterCard(Card card, string MonsterType2)
    {
        CardToFuse = null;
        MonstersToFuse = new List<Card>();
        AllCardVersion = card;
        MonsterTypes = new List<string>();
        MonsterTypes.Add("Normal");
        MonsterTypes.Add("Ritual");
        MonsterTypes.Add("Fusion");
        NumberOfTributesRequired = 0;
        MonstersToTributeSummon = new List<Card>();
        Position = "Deck";
        MonsterType = "";
        MonsterTypes = new List<string>();
        MonsterTypes.Add("Cyborg");
        MonsterTypes.Add("Saiyan");
        MonsterTypes.Add("Human");
        MonsterTypes.Add("Namekian");
        MonsterTypes.Add("Kai");
        MonsterTypes.Add("Magical");
        MonsterTypes.Add("Demon");
        MonsterTypes.Add("Android");
        MonsterTypes.Add("Unique Android");
        MonsterTypes.Add("Helior");
        MonsterTypes.Add("Other");

        foreach (string type in MonsterTypes)
        {
            if (type == MonsterType2)
            {
                MonsterType = type;
            }
        }

        if (card.TheCardType.Contains("Monster") || card.TheCardType.Contains("Ritual") || card.TheCardType.Contains("Fusion"))
        {
            this.Name = card.Name;
            //this.stars = card.stars;
            //this.PictureFile = card.PictureFile;
            //this.attackPoints = card.attackPoints;
            //this.defensePoints = card.defensePoints;
            //this.MonsterType = card.MonsterType;
            this.IsFaceUpBool = false;
            this.IsFaceDownBool = false;
            this.IsAttackPosition = false;
            this.IsDefensePosition = false;
            this.IsSummoned = false;
            this.IsInHand = false;
            this.IsInGraveyard = false;

            this.IsInField = false;
            if (card.TheCardType.Contains("Ritual"))
            {
                this.IsInDeck = false;
                this.IsInExtraDeck = true;
                this.CardToRitualSummon = card.ToRitualSummon;
                //this.MonstersTributesToRitualSummon = card.MonstersTributesToRitualSummon;
            }
            else if (card.TheCardType.Contains("Fusion"))
            {
                this.IsInDeck = false;
                this.IsInExtraDeck = true;
                //this.MonstersToFuse = card.MonstersToFuse;
            }
            else if (card.TheCardType.Contains("Monster"))
            {
                this.IsInDeck = true;
                this.IsInExtraDeck = false;
                this.CardToRitualSummon = null;
                //this.MonstersTributesToRitualSummon = null;
                //this.MonstersToFuse = null;
            }
        }
        else
        {
            throw new Exception("This is not a Monster Card");
        }
    }

    #endregion

    #region Methods

    public void EquiqSpell(SpellCard equiqCard)
    {
        SpellCardsEffectingThisMonster.Add(equiqCard);
        SpellCardsEffectingThisMonsterAndOther.Add(equiqCard);
    }

    public void EffectActivation()
    {

    }

    #endregion

    #region properties


    public string Description { get; set; }
    public string Name { get; set; }
    public int StarsNumber { get; set; }
    public string PictureFile { get; set; }
    public bool HasEffect { get; set; }
    public int AttackPoints { get; set; }
    public int DefensePoints { get; set; }
    public string MonsterType { get; set; }
    public string Position { get; set; }
    public bool IsFaceUpBool { get; set; }
    public bool IsFaceDownBool { get; set; }
    public bool IsAttackPosition { get; set; }
    public bool IsDefensePosition { get; set; }
    public bool IsSummoned { get; set; }
    public bool IsInHand { get; set; }
    public bool IsInDeck { get; set; }
    public bool IsInGraveyard { get; set; }
    public bool IsInExtraDeck { get; set; }
    public bool IsInField { get; set; }
    public bool IsRitual { get; set; }
    public bool IsFusion { get; set; }
    public bool IsBeingAttacked { get; set; }
    public bool IsAttacking { get; set; }
    public bool IsAttackingDirectly { get; set; }
    public Card WhoAttacksMe { get; set; }
    public Card WhoIAttack { get; set; }
    public bool IsAttackingWithMe { get; set; }
    public bool IsAttackingWithMeDirectly { get; set; }
    public bool IsAttackingWithMeAndOther { get; set; }
    public bool IsAttackingWithOther { get; set; }
    public bool IsAttackingWithOtherDirectly { get; set; }
    public bool IsAttackingWithOtherAndMe { get; set; }
    public bool IsAttackingWithOtherAndMeDirectly { get; set; }
    public bool IsAttackingWithOtherAndOther { get; set; }
    public bool IsAttackingWithOtherAndOtherDirectly { get; set; }
    public bool IsAttackingWithOtherAndOtherAndMe { get; set; }
    public List<SpellCard> SpellCardsEffectingThisMonster { get; set; }
    public List<SpellCard> SpellCardsEffectingThisMonsterAndOther { get; set; }
    public List<Card> MonstersToTributeSummon { get; set; }
    public int NumberOfTributesRequired { get; set; }
    public string RitualTributeRequired { get; set; }
    public string NameOfSpellCardToSummonRitual { get; set; }
    public SpellCard CardToRitualSummon { get; set; }
    public List<string> MonsterTypes { get; set; }
    public Card AllCardVersion { get; set; }
    public Card CardTributeForRitualSummon { get; set; }
    public List<Card> MonstersToFuse { get; set; }
    public SpellCard CardToFuse { get; set; }
    public Player Owner { get; set; }

    /*
    public enum MonsterType
    {
        Cyborg,
        Saiyan,
        Human,
        Namekian,
		Kai,
        Magical,
        Demon,
        Android,
        Unique Android,
		Helior,
		Other
    }
	*/
    #endregion
}
