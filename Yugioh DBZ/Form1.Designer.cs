namespace Yugioh_DBZ
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.FirstTextBox = new System.Windows.Forms.RichTextBox();
            this.Options = new System.Windows.Forms.Button();
            this.DuelButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FirstTextBox
            // 
            this.FirstTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FirstTextBox.Location = new System.Drawing.Point(82, 42);
            this.FirstTextBox.Name = "FirstTextBox";
            this.FirstTextBox.ReadOnly = true;
            this.FirstTextBox.Size = new System.Drawing.Size(1666, 195);
            this.FirstTextBox.TabIndex = 0;
            this.FirstTextBox.Text = resources.GetString("FirstTextBox.Text");
            this.FirstTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Options
            // 
            this.Options.Enabled = false;
            this.Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Options.Location = new System.Drawing.Point(119, 260);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(360, 350);
            this.Options.TabIndex = 1;
            this.Options.Text = "Options";
            this.Options.UseVisualStyleBackColor = true;
            // 
            // DuelButton
            // 
            this.DuelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.DuelButton.Location = new System.Drawing.Point(650, 260);
            this.DuelButton.Name = "DuelButton";
            this.DuelButton.Size = new System.Drawing.Size(360, 350);
            this.DuelButton.TabIndex = 2;
            this.DuelButton.Text = "Duel";
            this.DuelButton.UseVisualStyleBackColor = true;
            this.DuelButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button1.Location = new System.Drawing.Point(1209, 274);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(377, 336);
            this.button1.TabIndex = 3;
            this.button1.Text = "Card List";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1050);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DuelButton);
            this.Controls.Add(this.Options);
            this.Controls.Add(this.FirstTextBox);
            this.Name = "Form1";
            this.Text = "Yugioh Power of Chaos Dragon Ball";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.RichTextBox FirstTextBox;
        private System.Windows.Forms.Button DuelButton;
        private System.Windows.Forms.Button Options;
        private System.Windows.Forms.Button button1;
    }
}

