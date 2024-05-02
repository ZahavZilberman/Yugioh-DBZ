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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

public class Card
{
    #region ctor

    public Card(string CardType)
    {
        CardTypes = new List<string>();
        IsOnField = false;
        CardTypes.Add("Monster");
        CardTypes.Add("Spell");
        CardTypes.Add("Trap");
        CardTypes.Add("Fusion");
        CardTypes.Add("Ritual");
        ToRitualSummon = null;
        TheCardType = "";
        MonstersToFuse = new List<Card>();
        for (int i = 0; i < CardTypes.Count; i++)
        {
            if (CardType.Contains(CardTypes[i]))
            {
                TheCardType = CardTypes[i];
            }
        }

        CardPositionsInDuel = new List<string>();
        CardPositionsInDuel.Add("Graveyard");
        CardPositionsInDuel.Add("Hand");
        CardPositionsInDuel.Add("Deck");
        CardPositionsInDuel.Add("Field");
        CardPositionsInDuel.Add("Banished");
        CardPositionsInDuel.Add("RemovedFromPlay");
        CardPositionsInDuel.Add("ExtraDeck");

        ThisCardPositionInDuel = "Deck";
        CardPositionInDeck = 0;

    }
    #endregion

    #region Methods

    public void ReadDeck(XDocument data, string pathToData)
    {
        data = new XDocument(pathToData);
        XElement root = data.Root;
        List<Card> allMonsterCards = new List<Card>();
        XElement monsters = new XElement(root.Element("Monsters"));
        XElement normalMonsters = new XElement(monsters.Element("NormalMonsters"));
        XElement NoneffectMonsters = new XElement(monsters.Element("WithoutEffects"));
        List<XElement> allNormalMonsterCards = new List<XElement>();
    }

    #region Normal Summon

    public void SummonMonster(Field field, Hand hand, MonsterCard monster, bool toAttackMode, bool ToFaceDown)
    {
        int countOfMonstersAvilableToTribute = 0;
        if (TheCardType == "Monster" && field.IsFieldFull() == false)
        {
            //IsOnField = true; save this to the end

            if (monster.IsRitual == false && monster.IsFusion == false)
            {
                if(monster.StarsNumber <= 4)
                {
                    monster.NumberOfTributesRequired = 0;
                }
                if (monster.StarsNumber > 4 && monster.StarsNumber < 7)
                {
                    monster.NumberOfTributesRequired = 1;
                }
                if (monster.StarsNumber > 6 && monster.StarsNumber < 9)
                {
                    monster.NumberOfTributesRequired = 2;
                }
                if (monster.StarsNumber > 8 && monster.StarsNumber < 11)
                {
                    monster.NumberOfTributesRequired = 3;
                }
                if (monster.StarsNumber > 10 && monster.StarsNumber < 13)
                {
                    monster.NumberOfTributesRequired = 4;
                }
                for (int start = 10; start < 15; start++)
                {
                    if (field.AllCards[start] != null && field.AllCards[start].TheCardType == "Monster")
                    {
                        monster.MonstersToTributeSummon.Add(field.AllCards[start]);
                        countOfMonstersAvilableToTribute++;
                    }
                }

                if (countOfMonstersAvilableToTribute >= monster.NumberOfTributesRequired)
                {
                    /* here you need to ask the player which monsters he wants to tribute summon, if the stars > 4 */
                    /* afterwards ask him if to put in attack mode or face down*/
                    /* than make sure to update the field, hand, player, and duel*/
                }
                else
                {
                    throw new Exception("You do not have enough monsters to tribute summon this monster");
                }
            }
            
            #endregion
        }
        if (field.IsFieldFull())
        {
            throw new Exception("The FieldInstance is full");
        }
        // ritual and fusion require spell card activation, so I won't include them for now
        throw new Exception("This CardInstance is not a monster");
    }

    #region Switch monster position

    public void SwichMonsterToOtherMode(Field field, Hand hand, MonsterCard selectedMonster, bool IsFaceDown, int MonsterPositionInField)
    {
        bool EffectActivated = false;
        if (field == null)
        {
            string SwitchedPosition = "";
            if (selectedMonster.Position == "Defense")
            {
                SwitchedPosition = "Attack";
                field.MonstersInDefenseMood.Remove(selectedMonster);
                field.MonstersInAttackMood.Add(selectedMonster);
            }
            if (selectedMonster.Position == "Attack")
            {
                SwitchedPosition = "Defense";
                field.MonstersInDefenseMood.Add(selectedMonster);
                field.MonstersInAttackMood.Remove(selectedMonster);

            }
            if (selectedMonster.Position == "FaceDown")
            {
                SwitchedPosition = "Attack";
                EffectActivated = true;
                field.MonstersInFaceDown.Remove(selectedMonster);
                field.MonstersInAttackMood.Add(selectedMonster);
                //selectedMonster.EffectActivation();
            }

            field.HasAPlayerGotAMonsterPositionChange = true;


            if (selectedMonster.IsInField)
            {
                selectedMonster.Position = SwitchedPosition;
            }
            else
            {
                throw new Exception("This monster is not on the FieldInstance ");
            }

            return;
        }
        else
        {
            throw new Exception("This monster is not on the FieldInstance");
        }
    }
    #endregion

    #endregion

    #region Properties

    public string Description { get; set; }
    public List<string> CardTypes { get; set; }
    public string TheCardType { get; set; }
    public string Name { get; set; }
    public SpellCard SpellTemplate { get; set; }
    public SpellCard ToRitualSummon { get; set; }
    public List<Card> MonstersToFuse { get; set; }
    public bool IsOnField { get; set; }
    public Player ThePlayerWhoOwnsThisCard { get; set; }
    public List<string> CardPositionsInDuel { get; set; }
    public string ThisCardPositionInDuel { get; set; }
    public int CardPositionInDeck { get; set; }
    public XElement CardXML { get; set; }
    public enum MonsterType
    {
        Cyborg,
        Saiyan,
        Human,
        Namekian,
        Magical,
        Demon,
        Android,
        UniqueAndroid,
        Kai
    }
    #endregion
}
