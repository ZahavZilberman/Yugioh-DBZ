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

public class TrapCard
{
    #region Ctor

    public TrapCard(Card card)
    {
        PositionInDeck = 0;
        Name = card.Name;
        Description = card.Description;
        IsActivated = false;
        IsFaceDownBool = false;
        IsInHand = false;
        IsInDeck = true;
        IsInGraveyard = false;
        CanItBeActivatedNow = false;
    }

    #endregion

    #region functions



    #endregion

    #region Properties

    public string Name { get; set; }
    public string PictureFile { get; set; }
    public string Position { get; set; }
    public string Description { get; set; }
    public string Effect { get; set; }
    public int PositionInDeck { get; set; }
    public bool IsActivated { get; set; }
    public bool IsFaceDownBool { get; set; }
    public bool IsInHand { get; set; }
    public bool IsInDeck { get; set; }
    public bool IsInField { get; set; }
    public bool IsInGraveyard { get; set; }
    public bool CanItBeActivatedNow { get; set; }
    public XElement TrapCardDatabase { get; set; }
    public Player Owner { get; set; }
    #endregion
}
