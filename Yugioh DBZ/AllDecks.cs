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

namespace Yugioh_DBZ
{
    public class AllDecks
    {
        #region ctor

        public AllDecks()
        {
            AllNormalDecks = new List<Deck>();
            AllExtraDecks = new List<ExtraDeck>();

            #region normal decks

            XDocument DecksNavigator = new XDocument(XDocument.Load($@"YugiohDBZ\DeckNavigatorTemplate.xml"));
            XElement DecksElementroot = DecksNavigator.Root;
            int NumberOfDecks = int.Parse(DecksElementroot.Element("NumberOfDecks").Value);
            List<XElement> DeckNavigators = new List<XElement>();
            DeckNavigators = DecksElementroot.Element("Decks").Elements("Deck").ToList();
            List<string> PathToDecks = new List<string>();
            List<int> DeckNumberS = new List<int>();
            List<string> DeckNames = new List<string>();
            foreach( XElement Deck in DeckNavigators )
            {
                string PathToDeck = Deck.Element("Path").Value;
                PathToDecks.Add(PathToDeck);
                DeckNumberS.Add(int.Parse(Deck.Element("DeckNumber").Value));
                DeckNames.Add(Deck.Element("Name").Value);
            }

            Deck TheDeck = null;

            foreach(string path in PathToDecks)
            {
                TheDeck = new Deck(null, path);
                AllNormalDecks.Add(TheDeck);
            }

            #endregion

            #region Extra decks

            XDocument ExtraDecksNavigator = new XDocument(XDocument.Load(@"YugiohDBZ\ExtraDeckNavigatorTemplate.xml"));
            XElement RootExtra = new XElement(ExtraDecksNavigator.Root);

            int NumberOfExtraDecks = int.Parse(RootExtra.Element("NumberOfDecks").Value);
            List<XElement> ExtraDeckNavigators = new List<XElement>();
            List<XElement> ExtraDecksElements = ExtraDecksNavigator.Root.Element("Decks").Elements("Deck").ToList();
            List<string> PathToExtraDecks = new List<string>();
            List<int> ExtraDeckNumberS = new List<int>();
            List<string> ExtraDeckNames = new List<string>();
            List<string> ParrelDeckPathes = new List<string>();
            foreach (XElement ExtraDeck in ExtraDecksElements)
            {
                string PathToDeck = ExtraDeck.Element("Path").Value;
                PathToExtraDecks.Add(PathToDeck);
                ExtraDeckNumberS.Add(int.Parse(ExtraDeck.Element("DeckNumber").Value));
                ExtraDeckNames.Add(ExtraDeck.Element("Name").Value);
                XElement ParrelDeckPathElement = new XElement(ExtraDeck.Element("ParrelDeckPath"));
                string ParrelDeckPath = $@"{ExtraDeck.Element("ParrelDeckPath").Value}";
                ParrelDeckPathes.Add(ParrelDeckPath);
            }

            ExtraDeck TheExtraDeck = null;

            foreach (string Extrapath in PathToExtraDecks)
            {
                TheExtraDeck = new ExtraDeck(null, Extrapath);
                AllExtraDecks.Add(TheExtraDeck);
            }

            #endregion
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        public List<Deck> AllNormalDecks { get; set; }
        public List<ExtraDeck> AllExtraDecks { get; set; }

        #endregion
    }
}
