namespace Monopoly_Client
{
    partial class ConnectToServer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            nameBox = new TextBox();
            ipAddressBox = new TextBox();
            connectButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // nameBox
            // 
            nameBox.Location = new Point(98, 107);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(195, 23);
            nameBox.TabIndex = 0;
            // 
            // ipAddressBox
            // 
            ipAddressBox.Location = new Point(98, 151);
            ipAddressBox.Name = "ipAddressBox";
            ipAddressBox.ReadOnly = true;
            ipAddressBox.Size = new Size(195, 23);
            ipAddressBox.TabIndex = 1;
            // 
            // connectButton
            // 
            connectButton.Location = new Point(158, 198);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(75, 23);
            connectButton.TabIndex = 2;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 110);
            label1.Name = "label1";
            label1.Size = new Size(28, 15);
            label1.TabIndex = 3;
            label1.Text = "Tên:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 158);
            label2.Name = "label2";
            label2.Size = new Size(20, 15);
            label2.TabIndex = 4;
            label2.Text = "IP:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(158, 45);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 5;
            label3.Text = "Nhập Tên";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(98, 70);
            label4.Name = "label4";
            label4.Size = new Size(195, 15);
            label4.TabIndex = 6;
            label4.Text = "(Tối đa 20 kí tự, không kí tự đặc biệt";
            // 
            // ConnectToServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(381, 267);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(connectButton);
            Controls.Add(ipAddressBox);
            Controls.Add(nameBox);
            Name = "ConnectToServer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connect To Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox nameBox;
        private TextBox ipAddressBox;
        private Button connectButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
