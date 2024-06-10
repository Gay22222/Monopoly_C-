// The designer file of the MainWindow window.
using System.Windows.Forms;

namespace Monopoly_Client
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            chatLog = new TextBox();
            chatInput = new TextBox();
            rollButton = new Button();
            firstDieBox = new TextBox();
            secondDieBox = new TextBox();
            doneButton = new Button();
            startButton = new Button();
            positionLabel = new Label();
            fundsLabel = new Label();
            propertiesButton = new Button();
            mortgageButton = new Button();
            tradeButton = new Button();
            buildingsButton = new Button();
            bankruptcyButton = new Button();
            boardPicture = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)boardPicture).BeginInit();
            SuspendLayout();
            // 
            // chatLog
            // 
            chatLog.BackColor = SystemColors.Window;
            chatLog.Location = new Point(15, 15);
            chatLog.Margin = new Padding(4, 3, 4, 3);
            chatLog.Multiline = true;
            chatLog.Name = "chatLog";
            chatLog.ReadOnly = true;
            chatLog.ScrollBars = ScrollBars.Vertical;
            chatLog.Size = new Size(389, 333);
            chatLog.TabIndex = 0;
            // 
            // chatInput
            // 
            chatInput.Location = new Point(15, 345);
            chatInput.Margin = new Padding(4, 3, 4, 3);
            chatInput.Name = "chatInput";
            chatInput.Size = new Size(389, 23);
            chatInput.TabIndex = 1;
            chatInput.KeyDown += chatInput_KeyDown;
            // 
            // rollButton
            // 
            rollButton.Enabled = false;
            rollButton.Location = new Point(15, 375);
            rollButton.Margin = new Padding(4, 3, 4, 3);
            rollButton.Name = "rollButton";
            rollButton.Size = new Size(93, 27);
            rollButton.TabIndex = 2;
            rollButton.Text = "Xi ngau";
            rollButton.UseVisualStyleBackColor = true;
            rollButton.Click += rollButton_Click;
            // 
            // firstDieBox
            // 
            firstDieBox.Location = new Point(432, 15);
            firstDieBox.Margin = new Padding(4, 3, 4, 3);
            firstDieBox.Name = "firstDieBox";
            firstDieBox.ReadOnly = true;
            firstDieBox.Size = new Size(35, 23);
            firstDieBox.TabIndex = 3;
            // 
            // secondDieBox
            // 
            secondDieBox.Location = new Point(475, 15);
            secondDieBox.Margin = new Padding(4, 3, 4, 3);
            secondDieBox.Name = "secondDieBox";
            secondDieBox.ReadOnly = true;
            secondDieBox.Size = new Size(35, 23);
            secondDieBox.TabIndex = 4;
            // 
            // doneButton
            // 
            doneButton.Enabled = false;
            doneButton.Location = new Point(15, 408);
            doneButton.Margin = new Padding(4, 3, 4, 3);
            doneButton.Name = "doneButton";
            doneButton.Size = new Size(93, 27);
            doneButton.TabIndex = 5;
            doneButton.Text = "Xong";
            doneButton.UseVisualStyleBackColor = true;
            doneButton.Click += doneButton_Click;
            // 
            // startButton
            // 
            startButton.Location = new Point(302, 496);
            startButton.Margin = new Padding(4, 3, 4, 3);
            startButton.Name = "startButton";
            startButton.Size = new Size(93, 27);
            startButton.TabIndex = 6;
            startButton.Text = "Bat dau";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // positionLabel
            // 
            positionLabel.AutoSize = true;
            positionLabel.Location = new Point(514, 397);
            positionLabel.Margin = new Padding(4, 0, 4, 0);
            positionLabel.Name = "positionLabel";
            positionLabel.Size = new Size(0, 15);
            positionLabel.TabIndex = 7;
            // 
            // fundsLabel
            // 
            fundsLabel.AutoSize = true;
            fundsLabel.Location = new Point(14, 502);
            fundsLabel.Margin = new Padding(4, 0, 4, 0);
            fundsLabel.Name = "fundsLabel";
            fundsLabel.Size = new Size(75, 15);
            fundsLabel.TabIndex = 8;
            fundsLabel.Text = "Funds: $1500";
            // 
            // propertiesButton
            // 
            propertiesButton.Enabled = false;
            propertiesButton.Location = new Point(114, 375);
            propertiesButton.Margin = new Padding(4, 3, 4, 3);
            propertiesButton.Name = "propertiesButton";
            propertiesButton.Size = new Size(93, 27);
            propertiesButton.TabIndex = 9;
            propertiesButton.Text = "Tuy chinh";
            propertiesButton.UseVisualStyleBackColor = true;
            propertiesButton.Click += propertiesButton_Click;
            // 
            // mortgageButton
            // 
            mortgageButton.Enabled = false;
            mortgageButton.Location = new Point(214, 375);
            mortgageButton.Margin = new Padding(4, 3, 4, 3);
            mortgageButton.Name = "mortgageButton";
            mortgageButton.Size = new Size(93, 27);
            mortgageButton.TabIndex = 10;
            mortgageButton.Text = "The chap";
            mortgageButton.UseVisualStyleBackColor = true;
            mortgageButton.Click += mortgageButton_Click;
            // 
            // tradeButton
            // 
            tradeButton.Enabled = false;
            tradeButton.Location = new Point(313, 375);
            tradeButton.Margin = new Padding(4, 3, 4, 3);
            tradeButton.Name = "tradeButton";
            tradeButton.Size = new Size(93, 27);
            tradeButton.TabIndex = 11;
            tradeButton.Text = "Trao doi";
            tradeButton.UseVisualStyleBackColor = true;
            tradeButton.Click += tradeButton_Click;
            // 
            // buildingsButton
            // 
            buildingsButton.Enabled = false;
            buildingsButton.Location = new Point(412, 375);
            buildingsButton.Margin = new Padding(4, 3, 4, 3);
            buildingsButton.Name = "buildingsButton";
            buildingsButton.Size = new Size(93, 27);
            buildingsButton.TabIndex = 12;
            buildingsButton.Text = "Xay nha";
            buildingsButton.UseVisualStyleBackColor = true;
            buildingsButton.Click += buildingsButton_Click;
            // 
            // bankruptcyButton
            // 
            bankruptcyButton.Enabled = false;
            bankruptcyButton.Location = new Point(15, 442);
            bankruptcyButton.Margin = new Padding(4, 3, 4, 3);
            bankruptcyButton.Name = "bankruptcyButton";
            bankruptcyButton.Size = new Size(93, 27);
            bankruptcyButton.TabIndex = 13;
            bankruptcyButton.Text = "Pha san";
            bankruptcyButton.UseVisualStyleBackColor = true;
            bankruptcyButton.Click += bankruptcyButton_Click;
            // 
            // boardPicture
            // 
            boardPicture.Image = (Image)resources.GetObject("boardPicture.Image");
            boardPicture.Location = new Point(518, 15);
            boardPicture.Margin = new Padding(4, 3, 4, 3);
            boardPicture.Name = "boardPicture";
            boardPicture.Size = new Size(933, 923);
            boardPicture.TabIndex = 14;
            boardPicture.TabStop = false;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1314, 799);
            Controls.Add(boardPicture);
            Controls.Add(bankruptcyButton);
            Controls.Add(buildingsButton);
            Controls.Add(tradeButton);
            Controls.Add(mortgageButton);
            Controls.Add(propertiesButton);
            Controls.Add(fundsLabel);
            Controls.Add(positionLabel);
            Controls.Add(startButton);
            Controls.Add(doneButton);
            Controls.Add(secondDieBox);
            Controls.Add(firstDieBox);
            Controls.Add(rollButton);
            Controls.Add(chatInput);
            Controls.Add(chatLog);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainWindow";
            Text = "Monopoly";
            ((System.ComponentModel.ISupportInitialize)boardPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox chatLog;
        private System.Windows.Forms.TextBox chatInput;
        private Button rollButton;
        private TextBox firstDieBox;
        private TextBox secondDieBox;
        private Button doneButton;
        private Button startButton;
        private Label positionLabel;
        private Label fundsLabel;
        private Button propertiesButton;
        private Button mortgageButton;
        private Button tradeButton;
        private Button buildingsButton;
        private Button bankruptcyButton;
        private PictureBox boardPicture;
    }
}