namespace Clockwork.Samples.CSharp
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
            this.keyLabel = new System.Windows.Forms.Label();
            this.key = new System.Windows.Forms.TextBox();
            this.to = new System.Windows.Forms.TextBox();
            this.toLabel = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.TextBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.sendMessage = new System.Windows.Forms.Button();
            this.balance = new System.Windows.Forms.TextBox();
            this.balanceLabel = new System.Windows.Forms.Label();
            this.balanceButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // keyLabel
            // 
            this.keyLabel.AutoSize = true;
            this.keyLabel.Location = new System.Drawing.Point(14, 16);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(25, 13);
            this.keyLabel.TabIndex = 0;
            this.keyLabel.Text = "Key";
            // 
            // key
            // 
            this.key.Location = new System.Drawing.Point(70, 13);
            this.key.Name = "key";
            this.key.Size = new System.Drawing.Size(202, 20);
            this.key.TabIndex = 1;
            // 
            // to
            // 
            this.to.Location = new System.Drawing.Point(70, 39);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(93, 20);
            this.to.TabIndex = 3;
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(14, 42);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(20, 13);
            this.toLabel.TabIndex = 2;
            this.toLabel.Text = "To";
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(70, 65);
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(202, 80);
            this.message.TabIndex = 5;
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Location = new System.Drawing.Point(14, 68);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(50, 13);
            this.messageLabel.TabIndex = 4;
            this.messageLabel.Text = "Message";
            // 
            // sendMessage
            // 
            this.sendMessage.Location = new System.Drawing.Point(70, 152);
            this.sendMessage.Name = "sendMessage";
            this.sendMessage.Size = new System.Drawing.Size(202, 23);
            this.sendMessage.TabIndex = 6;
            this.sendMessage.Text = "Send Message";
            this.sendMessage.UseVisualStyleBackColor = true;
            this.sendMessage.Click += new System.EventHandler(this.sendMessage_Click);
            // 
            // balance
            // 
            this.balance.Enabled = false;
            this.balance.Location = new System.Drawing.Point(70, 229);
            this.balance.Name = "balance";
            this.balance.Size = new System.Drawing.Size(93, 20);
            this.balance.TabIndex = 8;
            // 
            // balanceLabel
            // 
            this.balanceLabel.AutoSize = true;
            this.balanceLabel.Location = new System.Drawing.Point(14, 232);
            this.balanceLabel.Name = "balanceLabel";
            this.balanceLabel.Size = new System.Drawing.Size(46, 13);
            this.balanceLabel.TabIndex = 7;
            this.balanceLabel.Text = "Balance";
            // 
            // balanceButton
            // 
            this.balanceButton.Location = new System.Drawing.Point(169, 227);
            this.balanceButton.Name = "balanceButton";
            this.balanceButton.Size = new System.Drawing.Size(103, 23);
            this.balanceButton.TabIndex = 9;
            this.balanceButton.Text = "Get Balance";
            this.balanceButton.UseVisualStyleBackColor = true;
            this.balanceButton.Click += new System.EventHandler(this.balanceButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.balanceButton);
            this.Controls.Add(this.balance);
            this.Controls.Add(this.balanceLabel);
            this.Controls.Add(this.sendMessage);
            this.Controls.Add(this.message);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.to);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.key);
            this.Controls.Add(this.keyLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label keyLabel;
        private System.Windows.Forms.TextBox key;
        private System.Windows.Forms.TextBox to;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.TextBox message;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button sendMessage;
        private System.Windows.Forms.TextBox balance;
        private System.Windows.Forms.Label balanceLabel;
        private System.Windows.Forms.Button balanceButton;
    }
}

