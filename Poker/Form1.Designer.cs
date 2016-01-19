                                        namespace Poker
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
            this.buttonFold = new System.Windows.Forms.Button();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.buttonCall = new System.Windows.Forms.Button();
            this.buttonRaise = new System.Windows.Forms.Button();
            this.pokerBetTimer = new System.Windows.Forms.ProgressBar();
            this.textBoxChips = new System.Windows.Forms.TextBox();
            this.bAdd = new System.Windows.Forms.Button();
            this.tbAdd = new System.Windows.Forms.TextBox();
            this.textBoxBotChips5 = new System.Windows.Forms.TextBox();
            this.textBoxBotChips4 = new System.Windows.Forms.TextBox();
            this.textBoxBotChips3 = new System.Windows.Forms.TextBox();
            this.textBoxBotChips2 = new System.Windows.Forms.TextBox();
            this.textBoxBotChips1 = new System.Windows.Forms.TextBox();
            this.textBoxPot = new System.Windows.Forms.TextBox();
            this.bOptions = new System.Windows.Forms.Button();
            this.buttonBigBlind = new System.Windows.Forms.Button();
            this.textBoxSmallBlind = new System.Windows.Forms.TextBox();
            this.buttonSmallBlind = new System.Windows.Forms.Button();
            this.textBoxBigBlind = new System.Windows.Forms.TextBox();
            this.fifthBotStatus = new System.Windows.Forms.Label();
            this.fourthBotStatus = new System.Windows.Forms.Label();
            this.thirdBotStatus = new System.Windows.Forms.Label();
            this.firstBotStatus = new System.Windows.Forms.Label();
            this.playerStatus = new System.Windows.Forms.Label();
            this.secondBotStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRaise = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonFold
            // 
            this.buttonFold.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFold.Location = new System.Drawing.Point(335, 660);
            this.buttonFold.Name = "buttonFold";
            this.buttonFold.Size = new System.Drawing.Size(130, 62);
            this.buttonFold.TabIndex = 0;
            this.buttonFold.Text = "Fold";
            this.buttonFold.UseVisualStyleBackColor = true;
            this.buttonFold.Click += new System.EventHandler(this.buttonFold_IsClicked);
            // 
            // buttonCheck
            // 
            this.buttonCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheck.Location = new System.Drawing.Point(494, 660);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(134, 62);
            this.buttonCheck.TabIndex = 2;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_IsClicked);
            // 
            // buttonCall
            // 
            this.buttonCall.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCall.Location = new System.Drawing.Point(667, 661);
            this.buttonCall.Name = "buttonCall";
            this.buttonCall.Size = new System.Drawing.Size(126, 62);
            this.buttonCall.TabIndex = 3;
            this.buttonCall.Text = "Call";
            this.buttonCall.UseVisualStyleBackColor = true;
            this.buttonCall.Click += new System.EventHandler(this.buttonCall_IsClicked);
            // 
            // buttonRaise
            // 
            this.buttonRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonRaise.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRaise.Location = new System.Drawing.Point(835, 661);
            this.buttonRaise.Name = "buttonRaise";
            this.buttonRaise.Size = new System.Drawing.Size(124, 62);
            this.buttonRaise.TabIndex = 4;
            this.buttonRaise.Text = "Raise";
            this.buttonRaise.UseVisualStyleBackColor = true;
            this.buttonRaise.Click += new System.EventHandler(this.buttonRaise_IsClicked);
            // 
            // pokerBetTimer
            // 
            this.pokerBetTimer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pokerBetTimer.BackColor = System.Drawing.SystemColors.Control;
            this.pokerBetTimer.Location = new System.Drawing.Point(335, 631);
            this.pokerBetTimer.Maximum = 1000;
            this.pokerBetTimer.Name = "pokerBetTimer";
            this.pokerBetTimer.Size = new System.Drawing.Size(667, 23);
            this.pokerBetTimer.TabIndex = 5;
            this.pokerBetTimer.Value = 1000;
            // 
            // textBoxChips
            // 
            this.textBoxChips.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBoxChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxChips.Location = new System.Drawing.Point(755, 553);
            this.textBoxChips.Name = "textBoxChips";
            this.textBoxChips.Size = new System.Drawing.Size(163, 23);
            this.textBoxChips.TabIndex = 6;
            this.textBoxChips.Text = "playerChips : 0";
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAdd.Location = new System.Drawing.Point(12, 697);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 25);
            this.bAdd.TabIndex = 7;
            this.bAdd.Text = "AddChips";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.buttonAddChips_IsClicked);
            // 
            // tbAdd
            // 
            this.tbAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbAdd.Location = new System.Drawing.Point(93, 700);
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(125, 20);
            this.tbAdd.TabIndex = 8;
            // 
            // textBoxBotChips5
            // 
            this.textBoxBotChips5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBotChips5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBotChips5.Location = new System.Drawing.Point(1012, 553);
            this.textBoxBotChips5.Name = "textBoxBotChips5";
            this.textBoxBotChips5.Size = new System.Drawing.Size(152, 23);
            this.textBoxBotChips5.TabIndex = 9;
            this.textBoxBotChips5.Text = "playerChips : 0";
            // 
            // textBoxBotChips4
            // 
            this.textBoxBotChips4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBotChips4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBotChips4.Location = new System.Drawing.Point(970, 81);
            this.textBoxBotChips4.Name = "textBoxBotChips4";
            this.textBoxBotChips4.Size = new System.Drawing.Size(123, 23);
            this.textBoxBotChips4.TabIndex = 10;
            this.textBoxBotChips4.Text = "playerChips : 0";
            // 
            // textBoxBotChips3
            // 
            this.textBoxBotChips3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBotChips3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBotChips3.Location = new System.Drawing.Point(755, 81);
            this.textBoxBotChips3.Name = "textBoxBotChips3";
            this.textBoxBotChips3.Size = new System.Drawing.Size(125, 23);
            this.textBoxBotChips3.TabIndex = 11;
            this.textBoxBotChips3.Text = "playerChips : 0";
            // 
            // textBoxBotChips2
            // 
            this.textBoxBotChips2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBotChips2.Location = new System.Drawing.Point(276, 81);
            this.textBoxBotChips2.Name = "textBoxBotChips2";
            this.textBoxBotChips2.Size = new System.Drawing.Size(133, 23);
            this.textBoxBotChips2.TabIndex = 12;
            this.textBoxBotChips2.Text = "playerChips : 0";
            // 
            // textBoxBotChips1
            // 
            this.textBoxBotChips1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxBotChips1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBotChips1.Location = new System.Drawing.Point(181, 553);
            this.textBoxBotChips1.Name = "textBoxBotChips1";
            this.textBoxBotChips1.Size = new System.Drawing.Size(142, 23);
            this.textBoxBotChips1.TabIndex = 13;
            this.textBoxBotChips1.Text = "playerChips : 0";
            // 
            // textBoxPot
            // 
            this.textBoxPot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPot.Location = new System.Drawing.Point(606, 212);
            this.textBoxPot.Name = "textBoxPot";
            this.textBoxPot.Size = new System.Drawing.Size(125, 23);
            this.textBoxPot.TabIndex = 14;
            this.textBoxPot.Text = "0";
            // 
            // bOptions
            // 
            this.bOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOptions.Location = new System.Drawing.Point(12, 12);
            this.bOptions.Name = "bOptions";
            this.bOptions.Size = new System.Drawing.Size(75, 36);
            this.bOptions.TabIndex = 15;
            this.bOptions.Text = "BB/SB";
            this.bOptions.UseVisualStyleBackColor = true;
            this.bOptions.Click += new System.EventHandler(this.buttonOptions_IsClicked);
            // 
            // buttonBigBlind
            // 
            this.buttonBigBlind.Location = new System.Drawing.Point(12, 254);
            this.buttonBigBlind.Name = "buttonBigBlind";
            this.buttonBigBlind.Size = new System.Drawing.Size(75, 23);
            this.buttonBigBlind.TabIndex = 16;
            this.buttonBigBlind.Text = "Big Blind";
            this.buttonBigBlind.UseVisualStyleBackColor = true;
            this.buttonBigBlind.Click += new System.EventHandler(this.buttonBigBlind_IsClicked);
            // 
            // textBoxSmallBlind
            // 
            this.textBoxSmallBlind.Location = new System.Drawing.Point(12, 228);
            this.textBoxSmallBlind.Name = "textBoxSmallBlind";
            this.textBoxSmallBlind.Size = new System.Drawing.Size(75, 20);
            this.textBoxSmallBlind.TabIndex = 17;
            this.textBoxSmallBlind.Text = "250";
            // 
            // buttonSmallBlind
            // 
            this.buttonSmallBlind.Location = new System.Drawing.Point(12, 199);
            this.buttonSmallBlind.Name = "buttonSmallBlind";
            this.buttonSmallBlind.Size = new System.Drawing.Size(75, 23);
            this.buttonSmallBlind.TabIndex = 18;
            this.buttonSmallBlind.Text = "Small Blind";
            this.buttonSmallBlind.UseVisualStyleBackColor = true;
            this.buttonSmallBlind.Click += new System.EventHandler(this.buttonSmallBlind_IsClicked);
            // 
            // textBoxBigBlind
            // 
            this.textBoxBigBlind.Location = new System.Drawing.Point(12, 283);
            this.textBoxBigBlind.Name = "textBoxBigBlind";
            this.textBoxBigBlind.Size = new System.Drawing.Size(75, 20);
            this.textBoxBigBlind.TabIndex = 19;
            this.textBoxBigBlind.Text = "500";
            // 
            // fifthBotStatus
            // 
            this.fifthBotStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fifthBotStatus.Location = new System.Drawing.Point(1012, 579);
            this.fifthBotStatus.Name = "fifthBotStatus";
            this.fifthBotStatus.Size = new System.Drawing.Size(152, 32);
            this.fifthBotStatus.TabIndex = 26;
            // 
            // fourthBotStatus
            // 
            this.fourthBotStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fourthBotStatus.Location = new System.Drawing.Point(970, 107);
            this.fourthBotStatus.Name = "fourthBotStatus";
            this.fourthBotStatus.Size = new System.Drawing.Size(123, 32);
            this.fourthBotStatus.TabIndex = 27;
            // 
            // thirdBotStatus
            // 
            this.thirdBotStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.thirdBotStatus.Location = new System.Drawing.Point(755, 107);
            this.thirdBotStatus.Name = "thirdBotStatus";
            this.thirdBotStatus.Size = new System.Drawing.Size(125, 32);
            this.thirdBotStatus.TabIndex = 28;
            // 
            // firstBotStatus
            // 
            this.firstBotStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.firstBotStatus.Location = new System.Drawing.Point(181, 579);
            this.firstBotStatus.Name = "firstBotStatus";
            this.firstBotStatus.Size = new System.Drawing.Size(142, 32);
            this.firstBotStatus.TabIndex = 29;
            // 
            // playerStatus
            // 
            this.playerStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerStatus.Location = new System.Drawing.Point(755, 579);
            this.playerStatus.Name = "playerStatus";
            this.playerStatus.Size = new System.Drawing.Size(163, 32);
            this.playerStatus.TabIndex = 30;
            // 
            // secondBotStatus
            // 
            this.secondBotStatus.Location = new System.Drawing.Point(276, 107);
            this.secondBotStatus.Name = "secondBotStatus";
            this.secondBotStatus.Size = new System.Drawing.Size(133, 32);
            this.secondBotStatus.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(654, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pot";
            // 
            // textBoxRaise
            // 
            this.textBoxRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBoxRaise.Location = new System.Drawing.Point(965, 703);
            this.textBoxRaise.Name = "textBoxRaise";
            this.textBoxRaise.Size = new System.Drawing.Size(108, 20);
            this.textBoxRaise.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.textBoxRaise);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.secondBotStatus);
            this.Controls.Add(this.playerStatus);
            this.Controls.Add(this.firstBotStatus);
            this.Controls.Add(this.thirdBotStatus);
            this.Controls.Add(this.fourthBotStatus);
            this.Controls.Add(this.fifthBotStatus);
            this.Controls.Add(this.textBoxBigBlind);
            this.Controls.Add(this.buttonSmallBlind);
            this.Controls.Add(this.textBoxSmallBlind);
            this.Controls.Add(this.buttonBigBlind);
            this.Controls.Add(this.bOptions);
            this.Controls.Add(this.textBoxPot);
            this.Controls.Add(this.textBoxBotChips1);
            this.Controls.Add(this.textBoxBotChips2);
            this.Controls.Add(this.textBoxBotChips3);
            this.Controls.Add(this.textBoxBotChips4);
            this.Controls.Add(this.textBoxBotChips5);
            this.Controls.Add(this.tbAdd);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.textBoxChips);
            this.Controls.Add(this.pokerBetTimer);
            this.Controls.Add(this.buttonRaise);
            this.Controls.Add(this.buttonCall);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.buttonFold);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "GLS Texas Poker";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Layout_Change);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFold;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonCall;
        private System.Windows.Forms.Button buttonRaise;
        private System.Windows.Forms.ProgressBar pokerBetTimer;
        private System.Windows.Forms.TextBox textBoxChips;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.TextBox tbAdd;
        private System.Windows.Forms.TextBox textBoxBotChips5;
        private System.Windows.Forms.TextBox textBoxBotChips4;
        private System.Windows.Forms.TextBox textBoxBotChips3;
        private System.Windows.Forms.TextBox textBoxBotChips2;
        private System.Windows.Forms.TextBox textBoxBotChips1;
        private System.Windows.Forms.TextBox textBoxPot;
        private System.Windows.Forms.Button bOptions;
        private System.Windows.Forms.Button buttonBigBlind;
        private System.Windows.Forms.TextBox textBoxSmallBlind;
        private System.Windows.Forms.Button buttonSmallBlind;
        private System.Windows.Forms.TextBox textBoxBigBlind;
        private System.Windows.Forms.Label fifthBotStatus;
        private System.Windows.Forms.Label fourthBotStatus;
        private System.Windows.Forms.Label thirdBotStatus;
        private System.Windows.Forms.Label firstBotStatus;
        private System.Windows.Forms.Label playerStatus;
        private System.Windows.Forms.Label secondBotStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRaise;
    }
}