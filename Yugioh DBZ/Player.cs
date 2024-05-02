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

public class Player
{

    #region ctor
    public Player(string name, Deck NormalDeck, ExtraDeck ExtraDeckObject)
    {
        DeckInstance = NormalDeck;
        ExtraDeckInstance = ExtraDeckObject;

        GraveyardInstance = new Graveyard(this);
        HandInstance = new Hand(this);
        //extraDeck = extraDeckCards

        //AllDeckCards = deckCards;
        Name = name;
        LifePoints = 10000;
        IsItMyTurn = false;
        IsItMyTurnToDraw = false;
        IsItMyTurnToMain1 = false;
        IsItMyTurnToBattle = false;
        IsItMyTurnToMain2 = false;
        IsItMyTurnToEnd = false;
        IsItMyTurnToStandby = false;

        IsLosingLifePointsDueToCardEffect = false;
        IsGainingLifePointsDueToCardEffect = false;
        IsLosingLifePointsDueToBattle = false;
        IsNoChangeInLifePointsAfterBattleIfThereIs = false;

        AmIDrawingNow = false;
        AmISummoingFaceUpNow = false;
        AmISummoingFaceDownNow = false;
        AmISettingFaceDownNow = false;
        AmIAttackingNow = false;
        AmIAttackingDirectlyNow = false;
        AmIAttackingAMonsterNow = false;
        AmIAttackingAMonsterWithALowerAttackNow = false;
        AmIAttackingAMonsterWithALowerDefenseNow = false;
        AmIAttackingAMonsterWithAHigherAttackNow = false;
        AmIAttackingAMonsterWithAHigherDefenseNow = false;
        AmIAttackingAMonsterWithTheSameAttackNow = false;
        AmIAttackingAMonsterWithTheSameDefenseNow = false;
        HasPlayerLostAllLifePoints = false;
        HasPlayerWon = false;
        AmIUsingAMonsterEffectNow = false;
        AmIUsingASpellEffectNow = false;
        AmIUsingATrapEffectNow = false;
        AmIUsingAnEffectNow = false;
        AmIUsingAnEffectThatDestroysAMonsterNow = false;
        AmIUsingAnEffectThatDestroysASpellNow = false;
        AmIUsingAnEffectThatDestroysATrapNow = false;
        AmIUsingAnEffectThatDestroysAMonsterAndASpellNow = false;
        AmIUsingAnEffectThatDestroysAMonsterAndATrapNow = false;
        AmIUsingAnEffectThatDestroysASpellAndATrapNow = false;
        AmIUsingAnEffectThatDestroysAMonsterAndASpellAndATrapNow = false;
        AmIUsingAnEffectThatDestroysAllMonstersNow = false;
        AmIUsingAnEffectThatDestroysAllSpellsNow = false;
        AmIUsingAnEffectThatDestroysAllTrapsNow = false;
        AmIUsingAnEffectThatDestroysAllMonstersAndSpellsNow = false;
        AmIUsingAnEffectThatDestroysAllMonstersAndTrapsNow = false;
        AmIUsingAnEffectThatDestroysAllSpellsAndTrapsNow = false;
        AmIUsingAnEffectThatDestroysEverythingNow = false;
        AmIUsingAnEffectThatDestroysEverythingExceptMonstersNow = false;
        AmIUsingAnEffectThatDestroysEverythingExceptSpellsNow = false;
        AmIUsingAnEffectThatDestroysEverythingExceptTrapsNow = false;
        AmIUsingAnEffectThatDestroysEverythingExceptMonstersAndSpellsNow = false;
        AmIUsingAnEffectThatDestroysEverythingExceptMonstersAndTrapsNow = false;
        AmIUsingAnEffectThatDestroysEverythingExceptSpellsAndTrapsNow = false;
        AmIUsingAnEffectThatDestroysEverythingExceptMonstersAndSpellsAndTrapsNow = false;
        AmIUsingAnEffectThatDestroysAllCardsNow = false;
    }

    #endregion

    #region methods

    #endregion

    #region properties
    public string Name { get; set; }
    public int LifePoints { get; set; }
    public List<Card> AllExtraDeckCards { get; set; }
    public List<Card> AllDeckCards { get; set; }
    public Deck DeckInstance { get; set; }
    public Hand HandInstance { get; set; }
    public Graveyard GraveyardInstance { get; set; }
    public ExtraDeck ExtraDeckInstance { get; set; }
    public bool IsItMyTurn { get; set; }
    public bool IsItMyTurnToDraw { get; set; }
    public bool IsItMyTurnToMain1 { get; set; }
    public bool IsItMyTurnToBattle { get; set; }
    public bool IsItMyTurnToMain2 { get; set; }
    public bool IsItMyTurnToEnd { get; set; }
    public bool IsItMyTurnToStandby { get; set; }
    public bool IsLosingLifePointsDueToCardEffect { get; set; }
    public bool IsGainingLifePointsDueToCardEffect { get; set; }
    public bool IsLosingLifePointsDueToBattle { get; set; }
    public bool IsNoChangeInLifePointsAfterBattleIfThereIs { get; set; }
    public bool AmIDrawingNow { get; set; }
    public bool AmISummoingFaceUpNow { get; set; }
    public bool AmISummoingFaceDownNow { get; set; }
    public bool AmISettingFaceDownNow { get; set; }
    public bool AmIAttackingNow { get; set; }
    public bool AmIAttackingDirectlyNow { get; set; }
    public bool AmIAttackingAMonsterNow { get; set; }
    public bool AmIAttackingAMonsterWithALowerAttackNow { get; set; }
    public bool AmIAttackingAMonsterWithALowerDefenseNow { get; set; }
    public bool AmIAttackingAMonsterWithAHigherAttackNow { get; set; }
    public bool AmIAttackingAMonsterWithAHigherDefenseNow { get; set; }
    public bool AmIAttackingAMonsterWithTheSameAttackNow { get; set; }
    public bool AmIAttackingAMonsterWithTheSameDefenseNow { get; set; }
    public bool HasPlayerLostAllLifePoints { get; set; }
    public bool HasPlayerWon { get; set; }
    public bool AmIUsingAMonsterEffectNow { get; set; }
    public bool AmIUsingASpellEffectNow { get; set; }
    public bool AmIUsingATrapEffectNow { get; set; }
    public bool AmIUsingAnEffectNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysAMonsterNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysASpellNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysATrapNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysAMonsterAndASpellNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysAMonsterAndATrapNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysASpellAndATrapNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysAMonsterAndASpellAndATrapNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysAllMonstersNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysAllSpellsNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysAllTrapsNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysAllMonstersAndSpellsNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysAllMonstersAndTrapsNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysAllSpellsAndTrapsNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysEverythingNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysEverythingExceptMonstersNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysEverythingExceptSpellsNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysEverythingExceptTrapsNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysEverythingExceptMonstersAndSpellsNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysEverythingExceptMonstersAndTrapsNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysEverythingExceptSpellsAndTrapsNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysEverythingExceptMonstersAndSpellsAndTrapsNow { get; set; }
    public bool AmIUsingAnEffectThatDestroysAllCardsNow { get; set; }

    #endregion
}
