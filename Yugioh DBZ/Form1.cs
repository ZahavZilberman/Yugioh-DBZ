using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using Yugioh_DBZ.Properties;
using System.Xml.Linq;

namespace Yugioh_DBZ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            ButtonsOnScreen = new List<Button>();
            PictureBoxesOnScreen = new List<PictureBox>();
            RichTextBoxesOnScreen = new List<RichTextBox>();
            AllDecksObject = new AllDecks();
            MatchOrDuel = " ";
            WhoPlayAgainst = " ";
            Player1Deck = new Deck();
            Player2Deck = new Deck();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        #region 1st screen response

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                byte[] fileBytes = File.ReadAllBytes(FileName);
            }
            */
            //System.Windows.Forms.
            DuelButton.Visible = false;
            Options.Visible = false;
            FirstTextBox.Visible = false;
            this.Controls.Clear();
            ChoosingDuelOrMatch();
        }

        #endregion

        #region choose duel or match

        public void ChoosingDuelOrMatch()
        {
            #region Single duel choose

            Button SingleDuelChoice = new Button();
            SingleDuelChoice.Text = "Single Duel";
            SingleDuelChoice.Size = new Size(360, 350);
            SingleDuelChoice.Location = new Point(650, 260);
            SingleDuelChoice.Font = new Font("Microsoft Sans Serif", 28F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(177)));
            SingleDuelChoice.AllowDrop = false;
            SingleDuelChoice.Enabled = true;
            SingleDuelChoice.Visible = true;
            ButtonsOnScreen.Add(SingleDuelChoice);
            EventHandler AfterChoosingMatchOrDuel = new EventHandler(this.AfterChoosingMatchOrDuel);
            SingleDuelChoice.Click += AfterChoosingMatchOrDuel;
            this.Controls.Add(SingleDuelChoice);

            #endregion

            #region Match choose

            Button MatchChoice = new Button();
            MatchChoice.Text = "Match";
            MatchChoice.Size = new Size(360, 350);
            MatchChoice.Location = new Point(119, 260);
            MatchChoice.Font = new Font("Microsoft Sans Serif", 28F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(177)));
            MatchChoice.AllowDrop = false;
            MatchChoice.Enabled = true;
            MatchChoice.Visible = true;
            ButtonsOnScreen.Add(MatchChoice);
            EventHandler AfterChoosingMatchOrDuel2 = new EventHandler(this.AfterChoosingMatchOrDuel);
            MatchChoice.Click += AfterChoosingMatchOrDuel2;
            this.Controls.Add(MatchChoice);

            #endregion

            #region Text

            RichTextBox ChoosingDuelOrMatchText = new RichTextBox();
            ChoosingDuelOrMatchText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            ChoosingDuelOrMatchText.Location = new System.Drawing.Point(82, 42);
            ChoosingDuelOrMatchText.Name = "FirstTextBox";
            ChoosingDuelOrMatchText.ReadOnly = true;
            ChoosingDuelOrMatchText.Size = new System.Drawing.Size(1666, 195);
            ChoosingDuelOrMatchText.TabIndex = 0;
            ChoosingDuelOrMatchText.Rtf = ReadTextFromRtfFile(@"YugiohDBZ\duelormatchtext.rtf");
            RichTextBoxesOnScreen.Add(ChoosingDuelOrMatchText);

            this.Controls.Add(ChoosingDuelOrMatchText);

            #endregion
        }

        #region Event handler

        #region Event handler

        public void AfterChoosingMatchOrDuel(object sender, EventArgs respond)
        {
            Button whichButtonIsIt = new Button();
            whichButtonIsIt = (Button)sender;
            if (whichButtonIsIt.Text.Equals("Single Duel") || whichButtonIsIt.Text.Equals("Match"))
            {
                this.Controls.Clear();
                this.ButtonsOnScreen.Clear();
                this.RichTextBoxesOnScreen.Clear();

                if (whichButtonIsIt.Text.Equals("Single Duel"))
                {
                    MatchOrDuel = "Duel";
                    DecidingWhoToPlayAgainst("Duel");
                }
                if (whichButtonIsIt.Text.Equals("Match"))
                {
                    MatchOrDuel = "Match";
                    DecidingWhoToPlayAgainst("Match");
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region Choosing who to play against

        #region Opening the screen

        public void DecidingWhoToPlayAgainst(string MatchOrDuel)
        {

            #region online 

            Button Online = new Button();
            Online.Text = "Multiplayer (Online)";
            Online.Size = new Size(250, 350);
            Online.Location = new Point(120, 260);
            Online.Font = new Font("Microsoft Sans Serif", 28F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(177)));
            Online.AllowDrop = false;
            Online.Enabled = false;
            Online.Visible = true;
            ButtonsOnScreen.Add(Online);
            EventHandler AfterChoosingOnlineOfflineAI = new EventHandler(this.AfterChoosingOnlineOfflineAI);
            Online.Click += AfterChoosingOnlineOfflineAI;
            this.Controls.Add(Online);

            #endregion

            #region offline

            Button Offline = new Button();
            Offline.Text = "Multiplayer (Offline)";
            Offline.Size = new Size(250, 350);
            Offline.Location = new Point(500, 260);
            Offline.Font = new Font("Microsoft Sans Serif", 28F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(177)));
            Offline.AllowDrop = false;
            Offline.Enabled = true;
            Offline.Visible = true;
            ButtonsOnScreen.Add(Offline);
            EventHandler AfterChoosingOnlineOfflineAI2 = new EventHandler(this.AfterChoosingOnlineOfflineAI);
            Offline.Click += AfterChoosingOnlineOfflineAI2;
            this.Controls.Add(Offline);

            #endregion

            #region VS AI

            Button VsAI = new Button();
            VsAI.Text = "Multiplayer (Offline)";
            VsAI.Size = new Size(250, 350);
            VsAI.Location = new Point(900, 260);
            VsAI.Font = new Font("Microsoft Sans Serif", 28F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(177)));
            VsAI.AllowDrop = false;
            VsAI.Enabled = false;
            VsAI.Visible = true;
            ButtonsOnScreen.Add(Online);
            EventHandler AfterChoosingOnlineOfflineAI3 = new EventHandler(this.AfterChoosingOnlineOfflineAI);
            VsAI.Click += AfterChoosingOnlineOfflineAI3;
            this.Controls.Add(VsAI);

            #endregion

            #region Text

            RichTextBox ChooseWhoToPlayAgainst = new RichTextBox();
            ChooseWhoToPlayAgainst.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            ChooseWhoToPlayAgainst.Location = new System.Drawing.Point(82, 42);
            ChooseWhoToPlayAgainst.Name = "FirstTextBox";
            ChooseWhoToPlayAgainst.ReadOnly = true;
            ChooseWhoToPlayAgainst.Size = new System.Drawing.Size(1666, 195);
            ChooseWhoToPlayAgainst.TabIndex = 0;
            ChooseWhoToPlayAgainst.Rtf = ReadTextFromRtfFile(@"YugiohDBZ\WhoToPlayAgainst.rtf");
            RichTextBoxesOnScreen.Add(ChooseWhoToPlayAgainst);

            this.Controls.Add(ChooseWhoToPlayAgainst);

            #endregion
        }

        #region Event handler for online/offline/AI

        public void AfterChoosingOnlineOfflineAI(object sender, EventArgs respond)
        {
            Button whichButtonIsIt = new Button();
            whichButtonIsIt = (Button)sender;
            if(whichButtonIsIt.Text.Equals("Multiplayer (Offline)") || whichButtonIsIt.Text.Equals("Multiplayer (Online)") || whichButtonIsIt.Text.Equals("Vs Computer"))
            {
                this.Controls.Clear();
                this.ButtonsOnScreen.Clear();
                this.RichTextBoxesOnScreen.Clear();

                if (whichButtonIsIt.Text.Equals("Multiplayer (Offline)"))
                {
                    WhoPlayAgainst = "Multiplayer (Offline)";
                    //ChoosingDeck();
                }
                if (whichButtonIsIt.Text.Equals("Multiplayer (Online)"))
                {
                    WhoPlayAgainst = "Multiplayer (Online)";
                    //ChoosingDeck();
                }
                if (whichButtonIsIt.Text.Equals("Vs Computer"))
                {
                    WhoPlayAgainst = "Vs Computer";

                    //ChoosingDeck();
                }
            }

        }

        #endregion

        #endregion

        #endregion

        #region Enter Player names

        public void ChoosingNames()
        {

            #region The textbox to tell the player to submit name

            #endregion

            #region the textbox where the player will submit his name

            #endregion

            #region the buttons with the stupid offered names to choose instead of typing them

            // when the player clicks on one of them, the other controls a

            #endregion

            #region the submit button 

            Button Online = new Button();
            Online.Text = "Submit";
            Online.Size = new Size(250, 350);
            Online.Location = new Point(120, 260);
            Online.Font = new Font("Microsoft Sans Serif", 28F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(177)));
            Online.AllowDrop = false;
            Online.Enabled = false;
            Online.Visible = true;
            ButtonsOnScreen.Add(Online);
            //EventHandler AfterChoosingOnlineOfflineAI = new EventHandler(this.AfterChoosingOnlineOfflineAI);
            //Online.Click += AfterChoosingOnlineOfflineAI;
            this.Controls.Add(Online);

            #endregion
        }

        #endregion

        #region Choosing deck

        #region choosing deck function

        public void ChoosingDeck()
        {
            Random random = new Random();
            int randomNum = random.Next(1, 2);

            string FirstToChoose = $@"Player {randomNum}";
            string SecondToChoose = $@"Player {3 - randomNum}";

            RichTextBox ChooseDeckText = new RichTextBox();
            ChooseDeckText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            ChooseDeckText.Location = new System.Drawing.Point(82, 42);
            ChooseDeckText.Name = "ChooseDeckTextBox";
            ChooseDeckText.ReadOnly = true;
            ChooseDeckText.Size = new System.Drawing.Size(1666, 195);
            ChooseDeckText.TabIndex = 0;
            ChooseDeckText.Rtf = ReadTextFromRtfFile(@"YugiohDBZ\Choose a deck.rtf");
            RichTextBoxesOnScreen.Add(ChooseDeckText);

            this.Controls.Add(ChooseDeckText);

            int DisplayedDecks = 1;
            foreach(Deck PossibleDeck in AllDecksObject.AllNormalDecks)
            {
                Button APossibleDeckChoice = new Button();
                APossibleDeckChoice.Text = PossibleDeck.DeckNameString;
                double NumberOfDecks = AllDecksObject.AllNormalDecks.Count;
                int CurrentWidthPoint = 900 / DisplayedDecks;
                int AvergeWidth = (int)(Math.Round(360 / NumberOfDecks));
                APossibleDeckChoice.Size = new Size(AvergeWidth, 260);
                APossibleDeckChoice.Location = new Point(CurrentWidthPoint, 260);
                APossibleDeckChoice.Font = new Font("Microsoft Sans Serif", 28F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(177)));
                APossibleDeckChoice.AllowDrop = false;
                APossibleDeckChoice.Enabled = true;
                APossibleDeckChoice.Visible = true;
                ButtonsOnScreen.Add(APossibleDeckChoice);
                this.Controls.Add(APossibleDeckChoice);
                DisplayedDecks++;
            }
        }

        #region Event handler

        public void AfterChoosingDecks(object sender, EventArgs respond)
        {
            Button whichButtonIsIt = new Button();
            whichButtonIsIt = (Button)sender;
            foreach(Deck PossibleDeck in AllDecksObject.AllNormalDecks)
            {
                if(whichButtonIsIt.Text == PossibleDeck.DeckNameString)
                {

                }
            }
            /*
            if (whichButtonIsIt.Text.Equals("Multiplayer (Offline)") || whichButtonIsIt.Text.Equals("Multiplayer (Online)") || whichButtonIsIt.Text.Equals("Vs Computer"))
            {
                this.Controls.Clear();
                this.ButtonsOnScreen.Clear();
                this.RichTextBoxesOnScreen.Clear();

                if (whichButtonIsIt.Text.Equals("Multiplayer (Offline)"))
                {
                    WhoPlayAgainst = "Multiplayer (Offline)";
                    ChoosingDeck();
                }
                if (whichButtonIsIt.Text.Equals("Multiplayer (Online)"))
                {
                    WhoPlayAgainst = "Multiplayer (Online)";
                    ChoosingDeck();
                }
                if (whichButtonIsIt.Text.Equals("Vs Computer"))
                {
                    WhoPlayAgainst = "Vs Computer";
                    ChoosingDeck();
                }
            }
            */
        }

        #endregion


        #endregion


        #endregion

        #region reading rtf file

        public string ReadTextFromRtfFile(string PathToRTfFile)
        {
            return File.ReadAllText(PathToRTfFile);
        }

        #endregion

        #region properties

        public List<Button> ButtonsOnScreen { get; set; }
        public List<RichTextBox> RichTextBoxesOnScreen { get; set; }
        public List<PictureBox> PictureBoxesOnScreen { get; set; }
        public AllDecks AllDecksObject { get; set; }
        public string MatchOrDuel { get; set; }
        public string WhoPlayAgainst { get; set; }
        public Deck Player1Deck { get; set; }
        public Deck Player2Deck { get; set; }
        public List<string> PlayerNames { get; set; }

        #endregion
    }
}
