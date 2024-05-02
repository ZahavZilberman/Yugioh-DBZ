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

public class ExtraDeck
{
    #region ctor
    public ExtraDeck(Player player = null, string extraDeckPath = null)
    {
        AllCards = new List<Card>();
        FusionCardsWithEffects = new List<MonsterCard>();
        FusionCardsWithoutEffects = new List<MonsterCard>();
        RitualCardsWithEffects = new List<MonsterCard>();
        RitualCardsWithoutEffects = new List<MonsterCard>();
        IsFull = false;
        Name = DeckName(extraDeckPath);

        if(extraDeckPath != null)
        {
            FusionCardsWithEffects = FusionMonstersWithEffects(Name);
            FusionCardsWithoutEffects = FusionWithoutEffectMonsters(Name);
            RitualCardsWithEffects = RitualWithEffects(Name);
            RitualCardsWithoutEffects = RitualWithoutEffects(Name);
            Card SingleCard = null;
            MonsterCard AMonsterCard = null;
            // the following shows the order which the cards are in the deck - fused without effect monsters 1st, fused with 2nd, ritual with effects 3rd, ritual without effect 4th
            if(FusionCardsWithEffects.Count() > 0)
            {
                for (int start = 0; start < FusionCardsWithEffects.Count; start++)
                {
                    AMonsterCard = FusionCardsWithEffects[start];
                    SingleCard = new Card("Fusion");
                    SingleCard.Name = AMonsterCard.Name;
                    SingleCard.Description = AMonsterCard.Description;
                    SingleCard.MonstersToFuse = AMonsterCard.MonstersToFuse;
                    AllCards.Add(SingleCard);
                }
            }
            if(FusionCardsWithEffects.Count() > 0)
            {
                for (int start = 0; start < FusionCardsWithoutEffects.Count; start++)
                {
                    AMonsterCard = FusionCardsWithoutEffects[start];
                    SingleCard = new Card("Fusion");
                    SingleCard.Name = AMonsterCard.Name;
                    SingleCard.Description = AMonsterCard.Description;
                    SingleCard.MonstersToFuse = AMonsterCard.MonstersToFuse;
                    AllCards.Add(SingleCard);
                }
            }
            if(RitualCardsWithEffects.Count() > 0)
            {
                for (int start = 0; start < RitualCardsWithEffects.Count; start++)
                {
                    AMonsterCard = RitualCardsWithEffects[start];
                    SingleCard = new Card("Ritual");
                    SingleCard.Name = AMonsterCard.Name;
                    SingleCard.Description = AMonsterCard.Description;
                    AllCards.Add(SingleCard);
                }
            }
            if(RitualCardsWithEffects.Count() > 0)
            {
                for (int start = 0; start < RitualCardsWithoutEffects.Count; start++)
                {
                    AMonsterCard = RitualCardsWithoutEffects[start];
                    SingleCard = new Card("Ritual");
                    SingleCard.Name = AMonsterCard.Name;
                    SingleCard.Description = AMonsterCard.Description;
                    AllCards.Add(SingleCard);
                }
            }
        }

    }
    #endregion

    #region methods

    #region other methods

    public void AddCard(Card card)
    {
        if (card.TheCardType.Contains("Fusion"))
        {
            MonsterCard monsterCard = FindCardInExtraDeck(card.Name, Name, false, true, false);
            if(monsterCard.HasEffect)
            {
                FusionCardsWithEffects.Add(monsterCard);
            }
            else
            {
                FusionCardsWithoutEffects.Add(monsterCard);
            }
            AllCards.Add(card);
        }
        else if (card.TheCardType.Contains("Ritual"))
        {
            MonsterCard monsterCard = FindCardInExtraDeck(card.Name, Name, true, false, false);
            if (monsterCard.HasEffect)
            {
                RitualCardsWithEffects.Add(monsterCard);
            }
            else
            {
                RitualCardsWithoutEffects.Add(monsterCard);
            }
            AllCards.Add(card);
        }
    }

    public void RemoveCard(Card card)
    {
        if (card.TheCardType.Contains("Fusion"))
        {
            MonsterCard monsterCard = FindCardInExtraDeck(card.Name, Name, false, true, false);
            if (monsterCard.HasEffect)
            {
                FusionCardsWithEffects.Remove(monsterCard);
            }
            else
            {
                FusionCardsWithoutEffects.Remove(monsterCard);
            }
            AllCards.Remove(card);
        }
        else if (card.TheCardType.Contains("Ritual"))
        {
            MonsterCard monsterCard = FindCardInExtraDeck(card.Name, Name, true, false, false);
            if (monsterCard.HasEffect)
            {
                RitualCardsWithEffects.Remove(monsterCard);
            }
            else
            {
                RitualCardsWithoutEffects.Remove(monsterCard);
            }
            AllCards.Remove(card);
        }
    }

    public void IsThisEmpty()
    {
        if (AllCards.Count == 0)
        {
            IsEmptyBool = true;
        }
        else
        {
            IsEmptyBool = false;
        }
    }

    public void RemoveAllCards()
    {
        FusionCardsWithoutEffects.Clear();
        FusionCardsWithEffects.Clear();
        RitualCardsWithEffects.Clear();
        RitualCardsWithoutEffects.Clear();
        RitualCardsWithEffects.Clear();
        AllCards.Clear();
    }

    #endregion

    #region copy paste from deck object

    #region Finding a monster card in the deck
    public MonsterCard FindAMonsterCardInDeck(string cardName, string deckName)
    {
        MonsterCard foundMonster = null;
        Card card;
        string pathToDeck = "";
        XElement path;
        XDocument navigator = new XDocument(XDocument.Load(@"YugiohDBZ\DeckNavigatorTemplate.xml"));

        XElement root = navigator.Root;
        List<XElement> allDecks = root.Element("Decks").Elements("Deck").ToList();
        foreach (XElement deck in allDecks)
        {
            string name = deck.Element("Name").Value;
            if (name == deckName)
            {
                path = deck.Element("Path");
                pathToDeck = path.Value;
            }
        }

        XDocument monstersFile = new XDocument(XDocument.Load(pathToDeck));
        XElement MonstersFileRoot = monstersFile.Root;

        XElement monstersRoot = MonstersFileRoot.Element("Monsters");
        XElement normalMonstersRoot = monstersRoot.Element("NormalMonsters");
        XElement normalMonsterWithoutEffectRoot = normalMonstersRoot.Element("WithoutEffects");
        List<XElement> normalMonstersWithoutEffects = normalMonsterWithoutEffectRoot.Elements("Monster").ToList();

        foreach (XElement normalMonster in normalMonstersWithoutEffects)
        {
            XElement nameElement = normalMonster.Element("Name");
            string actualName = nameElement.Value;
            if (actualName == cardName)
            {
                string name = normalMonster.Element("Name").Value;
                string type = normalMonster.Element("Type").Value;
                int stars = int.Parse(normalMonster.Element("star").Value);
                string picturePath = normalMonster.Element("Picture").Value;
                string description = normalMonster.Element("Description").Value;
                int ATK = int.Parse(normalMonster.Element("ATK").Value);
                int DEF = int.Parse(normalMonster.Element("DEF").Value);
                int positionInDeck = int.Parse(normalMonster.Element("PositionInDeck").Value);

                card = new Card("Monster");

                card.Name = name;
                card.Description = description;
                card.CardPositionInDeck = positionInDeck;

                foundMonster = new MonsterCard(card, "Normal");
                foundMonster.Description = description;
                foundMonster.Name = name;
                foundMonster.StarsNumber = stars;
                foundMonster.PictureFile = picturePath;
                foundMonster.AttackPoints = ATK;
                foundMonster.DefensePoints = DEF;
                foundMonster.MonsterType = type;
                foundMonster.HasEffect = false;

                return foundMonster;
            }
        }

        XElement normalMonsterWithEffectRoot = normalMonstersRoot.Element("WithEffects");
        List<XElement> normalMonstersWithEffects = normalMonsterWithEffectRoot.Elements("Monster").ToList();

        foreach (XElement normalMonster in normalMonstersWithEffects)
        {
            XElement nameElement = normalMonster.Element("Name");
            string actualName = nameElement.Value;
            if (actualName == cardName)
            {
                string name = normalMonster.Element("Name").Value;
                string type = normalMonster.Element("Type").Value;
                int stars = int.Parse(normalMonster.Element("star").Value);
                string picturePath = normalMonster.Element("Picture").Value;
                string description = normalMonster.Element("Description").Value;
                int ATK = int.Parse(normalMonster.Element("ATK").Value);
                int DEF = int.Parse(normalMonster.Element("DEF").Value);
                int positionInDeck = int.Parse(normalMonster.Element("PositionInDeck").Value);

                card = new Card("Monster");

                card.Name = name;
                card.Description = description;
                card.CardPositionInDeck = positionInDeck;

                foundMonster = new MonsterCard(card, "Normal");
                foundMonster.Description = description;
                foundMonster.Name = name;
                foundMonster.StarsNumber = stars;
                foundMonster.PictureFile = picturePath;
                foundMonster.AttackPoints = ATK;
                foundMonster.DefensePoints = DEF;
                foundMonster.MonsterType = type;
                foundMonster.HasEffect = true;

                return foundMonster;
            }
        }
        return null;
    }

    #endregion

    #region Finding a spell card in the deck

    public SpellCard FindASpellCardInDeck(string cardName, string deckName)
    {
        SpellCard foundSpell = null;
        Card card;
        string pathToDeck = "";
        XElement path;
        XDocument navigator = new XDocument(XDocument.Load(@"YugiohDBZ\DeckNavigatorTemplate.xml"));

        XElement root = navigator.Root;
        List<XElement> allDecks = root.Element("Decks").Elements("Deck").ToList();
        foreach (XElement deck in allDecks)
        {
            string name = deck.Element("Name").Value;
            if (name == deckName)
            {
                path = deck.Element("Path");
                pathToDeck = path.Value;
            }
        }

        XDocument DeckFile = new XDocument(XDocument.Load(pathToDeck));
        XElement DeckRoot = DeckFile.Root;
        XElement spellsRoot = DeckRoot.Element("SpellCards");

        #region normal spell cards

        XElement normalSpellsRoot = spellsRoot.Element("NormalSpellCards");
        List<XElement> normalSpells = normalSpellsRoot.Elements("NormalSpellCard").ToList();

        foreach (XElement normalSpell in normalSpells)
        {
            XElement nameElement = normalSpell.Element("Name");
            string actualElement = nameElement.Value;
            if (actualElement == cardName)
            {
                string name = normalSpell.Element("Name").Value;
                string picturePath = normalSpell.Element("Picture").Value;
                string description = normalSpell.Element("Description").Value;
                int positionInDeck = int.Parse(normalSpell.Element("PositionInDeck").Value);
                string effect = normalSpell.Element("Effect").Value;
                card = new Card("Spell");

                card.Name = name;
                card.Description = description;
                card.CardPositionInDeck = positionInDeck;

                foundSpell = new SpellCard(card, "Normal");
                foundSpell.Description = description;
                foundSpell.Name = name;
                foundSpell.PictureFile = picturePath;
                foundSpell.PositionInDeck = positionInDeck;
                foundSpell.Effect = effect;

                return foundSpell;
            }
        }
        #endregion

        #region Equiq spell cards

        XElement EquiqSpellsRoot = spellsRoot.Element("EquipSpellCards");
        List<XElement> equiqSpells = EquiqSpellsRoot.Elements("EquipSpellCard").ToList();

        foreach (XElement equiqSpell in equiqSpells)
        {
            XElement nameElement = equiqSpell.Element("Name");
            string actualName = nameElement.Value;
            if (actualName == cardName)
            {
                string name = equiqSpell.Element("Name").Value;
                string picturePath = equiqSpell.Element("Picture").Value;
                string description = equiqSpell.Element("Description").Value;
                int positionInDeck = int.Parse(equiqSpell.Element("PositionInDeck").Value);
                bool canThisBeUsedOnAllMonsters = bool.Parse(equiqSpell.Element("CanItBeUsedOnEveryone").Value);

                card = new Card("Spell");
                card.Name = name;
                card.Description = description;
                card.CardPositionInDeck = positionInDeck;

                foundSpell = new SpellCard(card, "Equip");
                foundSpell.Description = description;
                foundSpell.Name = name;
                foundSpell.Duration = 1;
                foundSpell.PictureFile = picturePath;
                foundSpell.PositionInDeck = positionInDeck;
                foundSpell.WhichEquiqSpellCardIsThisUsedOn = null;
                foundSpell.IsForRitualSummon = false;
                foundSpell.IsForFusionSummon = false;


                XElement xElement = equiqSpell.Element("WhoCanItBeUsedOn");
                List<XElement> whoCanItBeUsedOn = xElement.Elements("Monster").ToList();
                List<MonsterCard> whichCardCanEquiqBeUsedOn = new List<MonsterCard>();
                if (!canThisBeUsedOnAllMonsters)
                {
                    foreach (XElement monster in whoCanItBeUsedOn)
                    {
                        string monsterName = monster.Value;
                        if (DoesThisMonsterCardBelongToDeck(monsterName, deckName))
                        {
                            MonsterCard monsterCard = FindAMonsterCardInDeck(monsterName, deckName);
                            whichCardCanEquiqBeUsedOn.Add(monsterCard);
                        }
                        if (!DoesThisMonsterCardBelongToDeck(monsterName, deckName))
                        {
                            MonsterCard monsterCard = FindCardInExtraDeck(monsterName, deckName, false, false, true);
                            whichCardCanEquiqBeUsedOn.Add(monsterCard);
                        }
                    }
                }
                // you can put an else here of adding all monsters on both deck and extra deck
                foreach (MonsterCard PossibeToEquiqMonster in whichCardCanEquiqBeUsedOn)
                {
                    foundSpell.WhoCanBeEquiqed.Add(PossibeToEquiqMonster);
                }

                if (!canThisBeUsedOnAllMonsters)
                {
                    foreach (MonsterCard monsterthatCanBeEqiqued in whichCardCanEquiqBeUsedOn)
                    {
                        foundSpell.WhoCanBeEquiqed.Add(monsterthatCanBeEqiqued);
                    }
                }

                return foundSpell;
            }
        }

        #endregion

        #region Continuous spell cards

        XElement ContinuousSpellsRoot = spellsRoot.Element("ContinuousSpellCards");
        List<XElement> ContinuousSpells = EquiqSpellsRoot.Elements("ContinuousSpellCard").ToList();

        foreach (XElement ContinuousSpell in ContinuousSpells)
        {
            XElement nameElement = ContinuousSpell.Element("Name");
            string actualName = nameElement.Value;
            if (actualName == cardName)
            {
                string name = ContinuousSpell.Element("Name").Value;
                string picturePath = ContinuousSpell.Element("Picture").Value;
                string description = ContinuousSpell.Element("Description").Value;
                int positionInDeck = int.Parse(ContinuousSpell.Element("PositionInDeck").Value);
                bool canThisBeUsedOnAllMonsters = bool.Parse(ContinuousSpell.Element("CanItBeUsedOnEveryone").Value);
                int duration = int.Parse(ContinuousSpell.Element("Duration").Value);

                card = new Card("Spell");
                card.Name = name;
                card.Description = description;
                card.CardPositionInDeck = positionInDeck;

                foundSpell = new SpellCard(card, "Continuous");
                foundSpell.Description = description;
                foundSpell.Name = name;
                foundSpell.Duration = duration;
                foundSpell.PictureFile = picturePath;
                foundSpell.PositionInDeck = positionInDeck;
                foundSpell.IsForRitualSummon = false;
                foundSpell.IsForFusionSummon = false;
                foundSpell.Duration = duration;

                XElement xElement = ContinuousSpell.Element("WhoCanItBeUsedOn");
                List<XElement> whoCanItBeUsedOn = xElement.Elements("Monster").ToList();
                List<MonsterCard> whichCardCanContinousBeUsedOn = new List<MonsterCard>();
                if (!canThisBeUsedOnAllMonsters)
                {
                    foreach (XElement monster in whoCanItBeUsedOn)
                    {
                        string monsterName = monster.Value;
                        if (DoesThisMonsterCardBelongToDeck(monsterName, deckName))
                        {
                            MonsterCard monsterCard = FindAMonsterCardInDeck(monsterName, deckName);
                            whichCardCanContinousBeUsedOn.Add(monsterCard);
                        }
                        if (!DoesThisMonsterCardBelongToDeck(monsterName, deckName))
                        {
                            MonsterCard monsterCard = FindCardInExtraDeck(monsterName, deckName, false, false, true); ;
                            whichCardCanContinousBeUsedOn.Add(monsterCard);
                        }
                    }
                }
                // you can put an else here of adding all monsters on both deck and extra deck
                foreach (MonsterCard PossibeToContinueos in whichCardCanContinousBeUsedOn)
                {
                    foundSpell.WhoCanBeContinous.Add(PossibeToContinueos);
                }

                return foundSpell;
            }
        }

        #endregion

        #region Ritual spell cards

        XElement RitualSpellsRoot = spellsRoot.Element("RitualSpellCards");
        List<XElement> ritualSpells = RitualSpellsRoot.Elements("RitualSpellCard").ToList();

        foreach (XElement ritualSpell in ritualSpells)
        {
            XElement nameElement = ritualSpell.Element("Name");
            string actualName = nameElement.Value;
            if (actualName == cardName)
            {
                string name = ritualSpell.Element("Name").Value;
                string picturePath = ritualSpell.Element("Picture").Value;
                string description = ritualSpell.Element("Description").Value;
                int positionInDeck = int.Parse(ritualSpell.Element("PositionInDeck").Value);
                string MonsterToSarafice = ritualSpell.Element("Tribute").Value;
                string MonsterToSummon = ritualSpell.Element("Summons").Value;
                MonsterCard monsterSacrafice = null;
                MonsterCard monsterToSummon = null;

                if (DoesThisMonsterCardBelongToDeck(MonsterToSummon, deckName))
                {
                    monsterSacrafice = FindAMonsterCardInDeck(MonsterToSarafice, deckName);
                }

                if (!DoesThisMonsterCardBelongToDeck(MonsterToSummon, deckName))
                {
                    monsterSacrafice = FindCardInExtraDeck(MonsterToSarafice, deckName, false, false, true);
                }

                //monsterToSummon = FindACardInExtraDeck(MonsterToSummon, deckName, deckName, true, false, false);

                card = new Card("Spell");

                card.Name = name;
                card.Description = description;
                card.CardPositionInDeck = positionInDeck;

                foundSpell = new SpellCard(card, "Ritual");
                foundSpell.Description = description;
                foundSpell.Name = name;
                foundSpell.Duration = 0;
                foundSpell.PictureFile = picturePath;
                foundSpell.PositionInDeck = positionInDeck;
                foundSpell.NameOfMonsterTribute = MonsterToSarafice;
                foundSpell.NameOfMonsterToSummon = MonsterToSummon;
                foundSpell.IsForRitualSummon = true;
                foundSpell.IsForFusionSummon = false;
                foundSpell.CardSacraficedToRitualSummon = monsterSacrafice;
                foundSpell.CardThatItCanRitualSummon = monsterToSummon;

                return foundSpell;
            }
        }

        #endregion

        #region Fusion spell card/s (always the same card)

        if (cardName == "Polymerization")
        {
            XElement FusionSpellsRoot = spellsRoot.Element("FusionSpellCards");
            List<XElement> fusionSpells = FusionSpellsRoot.Elements("FusionSpellCard").ToList();

            foreach (XElement fusionSpell in fusionSpells)
            {
                string name = "Polymerization";
                string Description = "This is the CardInstance that does all the fusions shown in Dragon Ball aside from Vegetto's. It works the same as it always does - choose 2-7 monsters from your HandInstance and/or FieldInstance that can be fused, activate this CardInstance, and tribute those monsters to summon their fused CardInstance.";
                string picturePath = @"YugiohDBZ\Polymerization.jpeg";
                int positionInDeck = int.Parse(fusionSpell.Element("PositionInDeck").Value);

                card = new Card("Spell");
                card.Name = name;
                card.Description = Description;
                card.CardPositionInDeck = positionInDeck;

                SpellCard fusionCard = new SpellCard(card, "Fusion");
                fusionCard.Description = Description;
                fusionCard.Name = name;
                fusionCard.Duration = 0;
                fusionCard.PictureFile = picturePath;
                fusionCard.PositionInDeck = positionInDeck;
                fusionCard.IsForRitualSummon = false;
                fusionCard.IsForFusionSummon = true;

                return fusionCard;
            }
        }
        return null;
        #endregion
    }

    #endregion

    #region Finding a trap card in deck

    public TrapCard FindATrapCardInDeck(string cardName, string deckName)
    {
        TrapCard foundTrap = null;
        Card card;
        string pathToDeck = "";
        XElement path;
        XDocument navigator = new XDocument(XDocument.Load(@"YugiohDBZ\DeckNavigatorTemplate.xml"));

        XElement root = navigator.Root;
        List<XElement> allDecks = root.Element("Decks").Elements("Deck").ToList();
        foreach (XElement deck in allDecks)
        {
            string name = deck.Element("Name").Value;
            if (name == deckName)
            {
                path = deck.Element("Path");
                pathToDeck = path.Value;
            }
        }

        XElement trapsRoot = root.Element("TrapCards");
        List<XElement> traps = trapsRoot.Elements("TrapCard").ToList();

        foreach (XElement trap in traps)
        {
            XElement nameElement = trap.Element("Name");
            string actualName = nameElement.Value;
            if (actualName == cardName)
            {
                string name = trap.Element("Name").Value;
                string picturePath = trap.Element("Picture").Value;
                string description = trap.Element("Description").Value;
                int positionInDeck = int.Parse(trap.Element("PositionInDeck").Value);
                string effect = trap.Element("Effect").Value;
                card = new Card("Trap");

                card.Name = name;
                card.Description = description;
                card.CardPositionInDeck = positionInDeck;

                foundTrap = new TrapCard(card);
                foundTrap.Description = description;
                foundTrap.Name = name;
                foundTrap.PictureFile = picturePath;
                foundTrap.PositionInDeck = positionInDeck;
                foundTrap.Effect = effect;

                return foundTrap;
            }
        }
        return null;
    }

    #endregion

    #region Finding extra deck file from deck file

    public string FindExtraDeckFilePathFromDeckFile(string deckName)
    {
        XDocument extraDeckNaigator = new XDocument(XDocument.Load(@"YugiohDBZ\ExtraDeckNavigatorTemplate.xml"));
        XElement navigator = extraDeckNaigator.Root;
        List<XElement> allDecks = navigator.Element("Decks").Elements("Deck").ToList();
        foreach (XElement deck in allDecks)
        {
            string name = deck.Element("Name").Value;
            if (name == deckName)
            {
                XElement path = deck.Element("Path");
                string pathToExtraDeck = path.Value;
                return pathToExtraDeck;
            }
        }

        return "";
    }

    #endregion

    #region finding deck file from extra deck file

    public string FindDeckFilePathFromExtraDeckFile(string deckName)
    {
        XDocument DeckNaigator = new XDocument(XDocument.Load(@"YugiohDBZ\DeckNavigatorTemplate.xml"));
        XElement navigator = DeckNaigator.Root;
        List<XElement> allDecks = navigator.Element("Decks").Elements("Deck").ToList();
        foreach (XElement deck in allDecks)
        {
            string name = deck.Element("Name").Value;
            if (name == deckName)
            {
                XElement path = deck.Element("Path");
                string pathToExtraDeck = path.Value;
                return pathToExtraDeck;
            }
        }

        return "";
    }

    #endregion

    #region determing if a monster is in extra or normal deck based on name

    public bool DoesThisMonsterCardBelongToDeck(string monsterName, string deckName)
    {

        string pathToExtraDeck = FindExtraDeckFilePathFromDeckFile(deckName);
        string pathToDeck = FindDeckFilePathFromExtraDeckFile(deckName);

        XDocument extraDeck = new XDocument(XDocument.Load(pathToExtraDeck));
        XElement extraDeckRoot = extraDeck.Root;
        XElement fusionMonsters = extraDeckRoot.Element("FusionMonsters");
        XElement fusionsWithoutEffects = fusionMonsters.Element("WithoutEffects");
        List<XElement> fusionMonstersWithoutEffects = fusionsWithoutEffects.Elements("FusionMonster").ToList();

        foreach (XElement fusionMonsterNoEffect in fusionMonstersWithoutEffects)
        {
            XElement name = fusionMonsterNoEffect.Element("Name");
            string actualName = name.Value;
            if (actualName == monsterName)
            {
                return false;
            }
        }

        XElement fusionsWithEffects = fusionMonsters.Element("WithEffects");
        List<XElement> fusionMonstersWithEffects = fusionsWithEffects.Elements("FusionMonster").ToList();

        foreach (XElement fusionMonsterNoEffect in fusionMonstersWithEffects)
        {
            XElement name = fusionMonsterNoEffect.Element("Name");
            string actualName = name.Value;
            if (actualName == monsterName)
            {
                return false;
            }
        }

        XElement ritualMonsters = extraDeckRoot.Element("RitualMonsters");
        XElement ritualMonstersWithoutEffects = ritualMonsters.Element("WithoutEffects");
        List<XElement> ritualMonstersWithoutEffectsList = ritualMonstersWithoutEffects.Elements("RitualMonster").ToList();
        foreach (XElement ritualMonsterWithoutEffect in ritualMonstersWithoutEffectsList)
        {
            XElement name = ritualMonsterWithoutEffect.Element("Name");
            string actualName = name.Value;
            if (actualName == monsterName)
            {
                return false;
            }
        }

        XElement ritualMonstersWithEffects = ritualMonsters.Element("WithEffects");
        List<XElement> ritualMonstersWithEffectsList = ritualMonstersWithEffects.Elements("RitualMonster").ToList();
        foreach (XElement ritualMonsterWithEffect in ritualMonstersWithEffectsList)
        {
            XElement name = ritualMonsterWithEffect.Element("Name");
            string actualName = name.Value;
            if (actualName == monsterName)
            {
                return false;
            }
        }

        XDocument deck = new XDocument(XDocument.Load(pathToDeck));
        XElement deckRoot = deck.Root;
        XElement monsters = deckRoot.Element("Monsters");
        XElement normalMonsters = monsters.Element("NormalMonsters");

        XElement normalMonstersWithoutEffects = normalMonsters.Element("WithoutEffects");
        List<XElement> normalMonstersWithoutEffectsList = normalMonstersWithoutEffects.Elements("Monster").ToList();
        foreach (XElement normalMonsterWithoutEffect in normalMonstersWithoutEffectsList)
        {
            XElement name = normalMonsterWithoutEffect.Element("Name");
            string actualName = name.Value;
            if (actualName == monsterName)
            {
                return true;
            }
        }

        XElement normalMonstersWithEffects = normalMonsters.Element("WithEffects");
        List<XElement> normalMonstersWithEffectsList = normalMonstersWithEffects.Elements("Monster").ToList();
        foreach (XElement normalMonstersWithEffect in normalMonstersWithEffectsList)
        {
            XElement name = normalMonstersWithEffect.Element("Name");
            string actualName = name.Value;
            if (actualName == monsterName)
            {
                return true;
            }
        }

        throw new Exception("This CardInstance's nowhere!");
    }

    #endregion

    #region finding cards for extra deck

    #region finding cards in extra deck

    public MonsterCard FindCardInExtraDeck(string cardName, string extraDeckName, bool DoesItHaveToBeRitual, bool DoesItHaveToBeFusion, bool CanBeBoth)
    {
        bool hasTheCardBeenFound = false;
        string pathToExtraDeck = "";
        XElement path;
        XDocument navigator = new XDocument(XDocument.Load(@"YugiohDBZ\ExtraDeckNavigatorTemplate.xml"));
        XElement root = navigator.Root;
        List<XElement> allExtraDecks = root.Element("Decks").Elements("Deck").ToList();
        foreach (XElement extraDeckElement in allExtraDecks)
        {
            string name = extraDeckElement.Element("Name").Value;
            if (name == extraDeckName)
            {
                path = extraDeckElement.Element("Path");
                pathToExtraDeck = path.Value;
            }
        }
        XDocument extraDeckElement2 = new XDocument(XDocument.Load(pathToExtraDeck));
        XElement extraDeckRoot = extraDeckElement2.Root;

        if (DoesItHaveToBeRitual || CanBeBoth)
        {
            XElement ritualMonstersRoot = extraDeckRoot.Element("RitualMonsters");
            XElement ritualMonstersWithoutEffectsRoot = ritualMonstersRoot.Element("WithoutEffects");
            List<XElement> ritualMonstersWithoutEffects = ritualMonstersWithoutEffectsRoot.Elements("RitualMonster").ToList();
            foreach (XElement ritualMonsterWithoutEffect in ritualMonstersWithoutEffects)
            {
                XElement nameElement = ritualMonsterWithoutEffect.Element("Name");
                string actualName = nameElement.Value;
                if (actualName == cardName)
                {
                    hasTheCardBeenFound = true;

                }
            }
            XElement ritualMonstersWithEffectsRoot = ritualMonstersRoot.Element("WithEffects");
            List<XElement> ritualMonstersWithEffects = ritualMonstersWithEffectsRoot.Elements("RitualMonster").ToList();

            foreach (XElement ritualMonsterWithEffect in ritualMonstersWithEffects)
            {
                XElement nameElement = ritualMonsterWithEffect.Element("Name");
                string actualName = nameElement.Value;
                if (actualName == cardName)
                {
                    hasTheCardBeenFound = true;
                }
            }
        }

        if (hasTheCardBeenFound)
        {
            return CreateCardObjectInExtraDeck(cardName, extraDeckName, "Ritual");
        }

        if (DoesItHaveToBeFusion || CanBeBoth)
        {
            XElement FusionMonstersRoot = extraDeckRoot.Element("FusionMonsters");
            XElement fusionMonstersWithoutEffectsRoot = FusionMonstersRoot.Element("WithoutEffects");
            List<XElement> fusionMonstersWithoutEffects = fusionMonstersWithoutEffectsRoot.Elements("FusionMonster").ToList();
            foreach (XElement fusionMonsterWithoutEffect in fusionMonstersWithoutEffects)
            {
                XElement nameElement = fusionMonsterWithoutEffect.Element("Name");
                string actualName = nameElement.Value;
                if (actualName == cardName)
                {
                    hasTheCardBeenFound = true;
                }
            }

            XElement fusionMonstersWithEffectRoot = FusionMonstersRoot.Element("WithEffects");
            List<XElement> fusionMonstersWithEffects = fusionMonstersWithEffectRoot.Elements("FusionMonster").ToList();
            foreach (XElement fusionMonsterWithEffect in fusionMonstersWithEffects)
            {
                XElement nameElement = fusionMonsterWithEffect.Element("Name");
                string actualName = nameElement.Value;
                if (actualName == cardName)
                {
                    hasTheCardBeenFound = true;
                }
            }
        }
        if (hasTheCardBeenFound)
        {
            return CreateCardObjectInExtraDeck(cardName, extraDeckName, "Fusion");
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region creating the found card object in extra deck

    public MonsterCard CreateCardObjectInExtraDeck(string cardName, string extraDeckName, string cardType)
    {
        Card card = new Card(cardType);
        MonsterCard monsterCard = null;
        string pathToExtraDeck = "";
        XElement path;
        XDocument navigator = new XDocument(XDocument.Load(@"YugiohDBZ\ExtraDeckNavigatorTemplate.xml"));
        XElement root = navigator.Root;
        List<XElement> allExtraDecks = root.Element("Decks").Elements("Deck").ToList();
        foreach (XElement extraDeckElement in allExtraDecks)
        {
            string name = extraDeckElement.Element("Name").Value;
            if (name == extraDeckName)
            {
                path = extraDeckElement.Element("Path");
                pathToExtraDeck = path.Value;
            }
        }
        XDocument extraDeck = new XDocument(XDocument.Load(pathToExtraDeck));
        XElement rootElement = extraDeck.Root;

        string pathToDeck = FindDeckFilePathFromExtraDeckFile(extraDeckName);
        SpellCard theSpellToSummonTheCard = null;

        #region Ritual

        if (cardType == "Ritual")
        {
            XElement ritualMonstersRoot = rootElement.Element("RitualMonsters");
            XElement ritualMonstersWithoutEffectsRoot = ritualMonstersRoot.Element("WithoutEffects");
            List<XElement> ritualMonstersWithoutEffects = ritualMonstersWithoutEffectsRoot.Elements("RitualMonster").ToList();
            foreach (XElement ritualMonsterWithoutEffect in ritualMonstersWithoutEffects)
            {
                XElement nameElement = ritualMonsterWithoutEffect.Element("Name");
                string actualName = nameElement.Value;
                if (actualName == cardName)
                {
                    string name = ritualMonsterWithoutEffect.Element("Name").Value;
                    string type = ritualMonsterWithoutEffect.Element("Type").Value;
                    int stars = int.Parse(ritualMonsterWithoutEffect.Element("star").Value);
                    string picturePath = ritualMonsterWithoutEffect.Element("Picture").Value;
                    string description = ritualMonsterWithoutEffect.Element("Description").Value;
                    int ATK = int.Parse(ritualMonsterWithoutEffect.Element("ATK").Value);
                    int DEF = int.Parse(ritualMonsterWithoutEffect.Element("DEF").Value);
                    int positionInDeck = int.Parse(ritualMonsterWithoutEffect.Element("PositionInDeck").Value);
                    string spellCardToSummon = ritualMonsterWithoutEffect.Element("SpellCardToSummon").Value;
                    string monsterCardToTribute = ritualMonsterWithoutEffect.Element("RequiredTribute").Value;

                    card.Name = name;
                    card.Description = description;
                    card.CardPositionInDeck = positionInDeck;

                    monsterCard = new MonsterCard(card, "Ritual");

                    monsterCard.Description = description;
                    monsterCard.Name = name;
                    monsterCard.StarsNumber = stars;
                    monsterCard.PictureFile = picturePath;
                    monsterCard.AttackPoints = ATK;
                    monsterCard.DefensePoints = DEF;
                    monsterCard.MonsterType = type;
                    monsterCard.HasEffect = false;

                    theSpellToSummonTheCard = FindASpellCardInDeck(spellCardToSummon, extraDeckName);
                    monsterCard.CardToRitualSummon = theSpellToSummonTheCard;
                    monsterCard.RitualTributeRequired = monsterCardToTribute;

                    if (DoesThisMonsterCardBelongToDeck(monsterCardToTribute, extraDeckName))
                    {
                        MonsterCard templateCard = FindAMonsterCardInDeck(monsterCardToTribute, extraDeckName);
                        Card cardToAdd = templateCard.AllCardVersion;
                        templateCard.CardTributeForRitualSummon = cardToAdd;
                    }
                    if (!DoesThisMonsterCardBelongToDeck(monsterCardToTribute, extraDeckName))
                    {
                        MonsterCard templateCard = FindCardInExtraDeck(monsterCardToTribute, extraDeckName, false, false, true);
                        Card cardToAdd = templateCard.AllCardVersion;
                        templateCard.CardTributeForRitualSummon = cardToAdd;

                    }

                    return monsterCard;
                }
            }

            XElement ritualMonstersWithEffectsRoot = ritualMonstersRoot.Element("WithEffects");
            List<XElement> ritualMonstersWithEffects = ritualMonstersWithEffectsRoot.Elements("RitualMonster").ToList();

            foreach (XElement ritualMonsterWithEffect in ritualMonstersWithEffects)
            {
                XElement nameElement = ritualMonsterWithEffect.Element("Name");
                string actualNameElement = nameElement.Value;
                if (actualNameElement == cardName)
                {
                    string name = ritualMonsterWithEffect.Element("Name").Value;
                    string type = ritualMonsterWithEffect.Element("Type").Value;
                    int stars = int.Parse(ritualMonsterWithEffect.Element("star").Value);
                    string picturePath = ritualMonsterWithEffect.Element("Picture").Value;
                    string description = ritualMonsterWithEffect.Element("Description").Value;
                    int ATK = int.Parse(ritualMonsterWithEffect.Element("ATK").Value);
                    int DEF = int.Parse(ritualMonsterWithEffect.Element("DEF").Value);
                    int positionInDeck = int.Parse(ritualMonsterWithEffect.Element("PositionInDeck").Value);
                    string spellCardToSummon = ritualMonsterWithEffect.Element("SpellCardToSummon").Value;
                    string monsterCardToTribute = ritualMonsterWithEffect.Element("RequiredTribute").Value;

                    card.Name = name;
                    card.Description = description;
                    card.CardPositionInDeck = positionInDeck;

                    monsterCard = new MonsterCard(card, "Ritual");

                    monsterCard.Description = description;
                    monsterCard.Name = name;
                    monsterCard.StarsNumber = stars;
                    monsterCard.PictureFile = picturePath;
                    monsterCard.AttackPoints = ATK;
                    monsterCard.DefensePoints = DEF;
                    monsterCard.MonsterType = type;
                    monsterCard.HasEffect = true;

                    theSpellToSummonTheCard = FindASpellCardInDeck(spellCardToSummon, extraDeckName);
                    monsterCard.CardToRitualSummon = theSpellToSummonTheCard;
                    monsterCard.RitualTributeRequired = monsterCardToTribute;

                    if (DoesThisMonsterCardBelongToDeck(monsterCardToTribute, extraDeckName))
                    {
                        MonsterCard templateCard = FindAMonsterCardInDeck(monsterCardToTribute, extraDeckName);
                        Card cardToAdd = templateCard.AllCardVersion;
                        templateCard.CardTributeForRitualSummon = cardToAdd;
                    }
                    if (!DoesThisMonsterCardBelongToDeck(monsterCardToTribute, extraDeckName))
                    {
                        MonsterCard templateCard = FindCardInExtraDeck(monsterCardToTribute, extraDeckName, false, false, true);
                        Card cardToAdd = templateCard.AllCardVersion;
                        templateCard.CardTributeForRitualSummon = cardToAdd;

                    }

                    return monsterCard;
                }
            }
        }

        #endregion

        #region Fusion

        if (cardType == "Fusion")
        {
            XElement FusionMonstersRoot = rootElement.Element("FusionMonsters");

            SpellCard theFusionSpell = FindASpellCardInDeck("Polymerization", extraDeckName);


            XElement fusionMonstersWithoutEffectsRoot = FusionMonstersRoot.Element("WithoutEffects");
            List<XElement> fusionMonstersWithoutEffects = fusionMonstersWithoutEffectsRoot.Elements("FusionMonster").ToList();
            foreach (XElement fusionMonsterWithoutEffect in fusionMonstersWithoutEffects)
            {
                XElement nameElement = fusionMonsterWithoutEffect.Element("Name");
                string actualName = nameElement.Value;
                if (actualName == cardName)
                {
                    string name = fusionMonsterWithoutEffect.Element("Name").Value;
                    string type = fusionMonsterWithoutEffect.Element("Type").Value;
                    int stars = int.Parse(fusionMonsterWithoutEffect.Element("star").Value);
                    string picturePath = fusionMonsterWithoutEffect.Element("Picture").Value;
                    string description = fusionMonsterWithoutEffect.Element("Description").Value;
                    int ATK = int.Parse(fusionMonsterWithoutEffect.Element("ATK").Value);
                    int DEF = int.Parse(fusionMonsterWithoutEffect.Element("DEF").Value);
                    int positionInDeck = int.Parse(fusionMonsterWithoutEffect.Element("PositionInDeck").Value);
                    int numOfRequiredMonsters = int.Parse(fusionMonsterWithoutEffect.Element("NumberOfRequiredMonsters").Value);

                    XElement requiredMonstersRoot = fusionMonsterWithoutEffect.Element("RequiredMonsters");
                    List<XElement> requiredMonsters = requiredMonstersRoot.Elements("RequiredMonster").ToList();
                    List<string> requiredMonstersNames = new List<string>();
                    foreach (XElement requiredMonster in requiredMonsters)
                    {
                        string requiredMonsterName = requiredMonster.Value;
                        requiredMonstersNames.Add(requiredMonsterName);
                    }

                    card.Name = name;
                    card.Description = description;
                    card.CardPositionInDeck = positionInDeck;

                    monsterCard = new MonsterCard(card, "Fusion");
                    monsterCard.Name = name;
                    monsterCard.Description = description;
                    monsterCard.StarsNumber = stars;
                    monsterCard.PictureFile = picturePath;
                    monsterCard.AttackPoints = ATK;
                    monsterCard.DefensePoints = DEF;
                    monsterCard.MonsterType = type;
                    monsterCard.HasEffect = false;
                    monsterCard.IsFusion = true;
                    monsterCard.IsRitual = false;
                    monsterCard.CardToFuse = theFusionSpell;

                    foreach (string requiredMonsterName in requiredMonstersNames)
                    {
                        if (DoesThisMonsterCardBelongToDeck(requiredMonsterName, extraDeckName))
                        {
                            MonsterCard templateCard = FindAMonsterCardInDeck(requiredMonsterName, extraDeckName);
                            Card cardToAdd = templateCard.AllCardVersion;
                            monsterCard.MonstersToFuse.Add(cardToAdd);
                        }
                        if (!DoesThisMonsterCardBelongToDeck(requiredMonsterName, extraDeckName))
                        {
                            MonsterCard templateCard = FindCardInExtraDeck(requiredMonsterName, extraDeckName, false, false, true);
                            Card cardToAdd = templateCard.AllCardVersion;
                            monsterCard.MonstersToFuse.Add(cardToAdd);
                        }
                    }
                    return monsterCard;
                }
            }

            XElement fusionMonstersWithEffectRoot = FusionMonstersRoot.Element("WithEffects");
            List<XElement> fusionMonstersWithEffects = fusionMonstersWithEffectRoot.Elements("FusionMonster").ToList();
            foreach (XElement fusionMonsterWithEffect in fusionMonstersWithEffects)
            {
                XElement nameElement = fusionMonsterWithEffect.Element("Name");
                string actualName = nameElement.Value;
                if (actualName == cardName)
                {
                    string name = fusionMonsterWithEffect.Element("Name").Value;
                    string type = fusionMonsterWithEffect.Element("Type").Value;
                    int stars = int.Parse(fusionMonsterWithEffect.Element("star").Value);
                    string picturePath = fusionMonsterWithEffect.Element("Picture").Value;
                    string description = fusionMonsterWithEffect.Element("Description").Value;
                    int ATK = int.Parse(fusionMonsterWithEffect.Element("ATK").Value);
                    int DEF = int.Parse(fusionMonsterWithEffect.Element("DEF").Value);
                    int positionInDeck = int.Parse(fusionMonsterWithEffect.Element("PositionInDeck").Value);

                    int numOfRequiredMonsters = int.Parse(fusionMonsterWithEffect.Element("NumberOfRequiredMonsters").Value);

                    XElement requiredMonstersRoot = fusionMonsterWithEffect.Element("RequiredMonsters");
                    List<XElement> requiredMonsters = requiredMonstersRoot.Elements("RequiredMonster").ToList();
                    List<string> requiredMonstersNames = new List<string>();
                    foreach (XElement requiredMonster in requiredMonsters)
                    {
                        string requiredMonsterName = requiredMonster.Value;
                        requiredMonstersNames.Add(requiredMonsterName);
                    }

                    card.Name = name;
                    card.Description = description;
                    card.CardPositionInDeck = positionInDeck;

                    monsterCard = new MonsterCard(card, "Fusion");
                    monsterCard.Name = name;
                    monsterCard.Description = description;
                    monsterCard.StarsNumber = stars;
                    monsterCard.PictureFile = picturePath;
                    monsterCard.AttackPoints = ATK;
                    monsterCard.DefensePoints = DEF;
                    monsterCard.MonsterType = type;
                    monsterCard.HasEffect = true;
                    monsterCard.IsFusion = true;
                    monsterCard.IsRitual = false;

                    foreach (string requiredMonsterName in requiredMonstersNames)
                    {
                        if (DoesThisMonsterCardBelongToDeck(requiredMonsterName, extraDeckName))
                        {
                            MonsterCard templateCard = FindAMonsterCardInDeck(requiredMonsterName, extraDeckName);
                            Card cardToAdd = templateCard.AllCardVersion;
                            monsterCard.MonstersToFuse.Add(cardToAdd);
                        }
                        if (!DoesThisMonsterCardBelongToDeck(requiredMonsterName, extraDeckName))
                        {
                            MonsterCard templateCard = FindCardInExtraDeck(requiredMonsterName, extraDeckName, false, false, true);
                            Card cardToAdd = templateCard.AllCardVersion;
                            monsterCard.MonstersToFuse.Add(cardToAdd);
                        }
                    }
                    return monsterCard;
                }
            }
        }

        #endregion

        return null;
    }

    #endregion

    #endregion

    #region finding out deck name

    public string DeckName(string pathToDeck)
    {
        XDocument deckFile = new XDocument(XDocument.Load(pathToDeck));
        XElement deckRoot = deckFile.Root;
        XElement deckName = deckRoot.Element("Name");
        return deckName.Value;
    }

    #endregion

    #endregion

    #region for reading an entire extra deck

    #region read all fusion monsters

    public List<MonsterCard> FusionWithoutEffectMonsters(string ExtraDeckName)
    {
        List<MonsterCard> FusionMonstersNoEffect = new List<MonsterCard>();
        MonsterCard AFusionMonsterWithoutEffect = null;

        string pathToExtraDeck = "";
        XElement path;
        XDocument navigator = new XDocument(XDocument.Load(@"YugiohDBZ\ExtraDeckNavigatorTemplate.xml"));
        XElement root = navigator.Root;
        List<XElement> allExtraDecks = root.Element("Decks").Elements("Deck").ToList();
        foreach (XElement extraDeckElement in allExtraDecks)
        {
            string name = extraDeckElement.Element("Name").Value;
            if (name == ExtraDeckName)
            {
                path = extraDeckElement.Element("Path");
                pathToExtraDeck = path.Value;
            }
        }

        XDocument extraDeckElement2 = new XDocument(XDocument.Load(pathToExtraDeck));
        XElement extraDeckRoot = extraDeckElement2.Root;

        XElement FusionMonstersRoot = extraDeckRoot.Element("FusionMonsters");
        XElement fusionMonstersWithoutEffectsRoot = FusionMonstersRoot.Element("WithoutEffects");
        List<XElement> fusionMonstersWithoutEffects = fusionMonstersWithoutEffectsRoot.Elements("FusionMonster").ToList();

        foreach (XElement fusionMonsterWithoutEffect in fusionMonstersWithoutEffects)
        {
            string Name = fusionMonsterWithoutEffect.Element("Name").Value;

            AFusionMonsterWithoutEffect = CreateCardObjectInExtraDeck(Name, ExtraDeckName, "Fusion");
            FusionMonstersNoEffect.Add(AFusionMonsterWithoutEffect);
        }

        return FusionMonstersNoEffect;
    }

    public List<MonsterCard> FusionMonstersWithEffects(string ExtraDeckName)
    {
        List<MonsterCard> AllFusionMonstersWithEffect = new List<MonsterCard>();
        MonsterCard AFusionMonsterWithEffect = null;

        string pathToExtraDeck = "";
        XElement path;
        XDocument navigator = new XDocument(XDocument.Load(@"YugiohDBZ\ExtraDeckNavigatorTemplate.xml"));
        XElement root = navigator.Root;
        List<XElement> allExtraDecks = root.Element("Decks").Elements("Deck").ToList();
        foreach (XElement extraDeckElement in allExtraDecks)
        {
            string name = extraDeckElement.Element("Name").Value;
            if (name == ExtraDeckName)
            {
                path = extraDeckElement.Element("Path");
                pathToExtraDeck = path.Value;
            }
        }

        XDocument extraDeckElement2 = new XDocument(XDocument.Load(pathToExtraDeck));
        XElement extraDeckRoot = extraDeckElement2.Root;

        XElement FusionMonstersRoot = extraDeckRoot.Element("FusionMonsters");
        XElement fusionMonstersWithEffectsRoot = FusionMonstersRoot.Element("WithEffects");
        List<XElement> fusionMonstersWithEffects = fusionMonstersWithEffectsRoot.Elements("FusionMonster").ToList();

        foreach (XElement fusionMonsterWithEffect in fusionMonstersWithEffects)
        {
            string Name = fusionMonsterWithEffect.Element("Name").Value;

            AFusionMonsterWithEffect = CreateCardObjectInExtraDeck(Name, ExtraDeckName, "Fusion");
            AllFusionMonstersWithEffect.Add(AFusionMonsterWithEffect);
        }

        return AllFusionMonstersWithEffect;
    }


    #endregion

    #region read all ritual monsters

    public List<MonsterCard> RitualWithoutEffects(string ExtraDeckName)
    {
        List<MonsterCard> RitualMonstersNoEffect = new List<MonsterCard>();
        MonsterCard ARitualMonsterWithoutEffect = null;

        string pathToExtraDeck = "";
        XElement path;
        XDocument navigator = new XDocument(XDocument.Load(@"YugiohDBZ\ExtraDeckNavigatorTemplate.xml"));
        XElement root = navigator.Root;
        List<XElement> allExtraDecks = root.Element("Decks").Elements("Deck").ToList();
        foreach (XElement extraDeckElement in allExtraDecks)
        {
            string name = extraDeckElement.Element("Name").Value;
            if (name == ExtraDeckName)
            {
                path = extraDeckElement.Element("Path");
                pathToExtraDeck = path.Value;
            }
        }

        XDocument extraDeckElement2 = new XDocument(XDocument.Load(pathToExtraDeck));
        XElement extraDeckRoot = extraDeckElement2.Root;

        XElement ritualMonstersRoot = extraDeckRoot.Element("RitualMonsters");
        XElement ritualMonstersWithoutEffectsRoot = ritualMonstersRoot.Element("WithoutEffects");
        List<XElement> ritualMonstersWithoutEffects = ritualMonstersWithoutEffectsRoot.Elements("RitualMonster").ToList();
        foreach (XElement ritualMonsterWithoutEffect in ritualMonstersWithoutEffects)
        {
            XElement nameElement = ritualMonsterWithoutEffect.Element("Name");
            string actualName = nameElement.Value;
            ARitualMonsterWithoutEffect = CreateCardObjectInExtraDeck(actualName, ExtraDeckName, "Ritual");
            RitualMonstersNoEffect.Add(ARitualMonsterWithoutEffect);
        }

        return RitualMonstersNoEffect;
    }

    public List<MonsterCard> RitualWithEffects(string ExtraDeckName)
    {
        List<MonsterCard> RitualMonstersEffect = new List<MonsterCard>();
        MonsterCard ARitualMonsterWithEffect = null;

        string pathToExtraDeck = "";
        XElement path;
        XDocument navigator = new XDocument(XDocument.Load(@"YugiohDBZ\ExtraDeckNavigatorTemplate.xml"));
        XElement root = navigator.Root;
        List<XElement> allExtraDecks = root.Element("Decks").Elements("Deck").ToList();
        foreach (XElement extraDeckElement in allExtraDecks)
        {
            string name = extraDeckElement.Element("Name").Value;
            if (name == ExtraDeckName)
            {
                path = extraDeckElement.Element("Path");
                pathToExtraDeck = path.Value;
            }
        }

        XDocument extraDeckElement2 = new XDocument(XDocument.Load(pathToExtraDeck));
        XElement extraDeckRoot = extraDeckElement2.Root;

        XElement ritualMonstersRoot = extraDeckRoot.Element("RitualMonsters");
        XElement ritualMonstersWithEffectsRoot = ritualMonstersRoot.Element("WithEffects");
        List<XElement> ritualMonstersWithEffects = ritualMonstersWithEffectsRoot.Elements("RitualMonster").ToList();
        foreach (XElement ritualMonsterWithEffect in ritualMonstersWithEffects)
        {
            XElement nameElement = ritualMonsterWithEffect.Element("Name");
            string actualName = nameElement.Value;
            ARitualMonsterWithEffect = CreateCardObjectInExtraDeck(actualName, ExtraDeckName, "Ritual");
            RitualMonstersEffect.Add(ARitualMonsterWithEffect);
        }

        return RitualMonstersEffect;
    }

    #endregion

    #endregion

    #endregion

    #region properties

    public List<Card> AllCards { get; set; }
    public List<MonsterCard> FusionCardsWithoutEffects { get; set; }
    public List<MonsterCard> FusionCardsWithEffects { get; set; }
    public List<MonsterCard> RitualCardsWithoutEffects { get; set; }
    public List<MonsterCard> RitualCardsWithEffects { get; set; }
    public bool IsEmptyBool { get; set; }
    public bool IsFull { get; set; }
    public string Name { get; set; }
    #endregion
}