namespace Jewellery_shop
{
    partial class StaffDashboard
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuyItem = new System.Windows.Forms.Button();
            this.btnMyHistory = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Britannic Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label1.Location = new System.Drawing.Point(254, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "WELCOME AS CUSTOMER";
            // 
            // btnBuyItem
            // 
            this.btnBuyItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuyItem.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.btnBuyItem.Location = new System.Drawing.Point(304, 112);
            this.btnBuyItem.Name = "btnBuyItem";
            this.btnBuyItem.Size = new System.Drawing.Size(133, 55);
            this.btnBuyItem.TabIndex = 1;
            this.btnBuyItem.Text = "Buy Item";
            this.btnBuyItem.UseVisualStyleBackColor = true;
            this.btnBuyItem.Click += new System.EventHandler(this.btnBuyItem_Click);
            // 
            // btnMyHistory
            // 
            this.btnMyHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMyHistory.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.btnMyHistory.Location = new System.Drawing.Point(304, 198);
            this.btnMyHistory.Name = "btnMyHistory";
            this.btnMyHistory.Size = new System.Drawing.Size(133, 55);
            this.btnMyHistory.TabIndex = 2;
            this.btnMyHistory.Text = "My History";
            this.btnMyHistory.UseVisualStyleBackColor = true;
            this.btnMyHistory.Click += new System.EventHandler(this.btnMyHistory_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.btnLogout.Location = new System.Drawing.Point(304, 283);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(133, 55);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // StaffDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnMyHistory);
            this.Controls.Add(this.btnBuyItem);
            this.Controls.Add(this.label1);
            this.Name = "StaffDashboard";
            this.Text = "StaffDashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuyItem;
        private System.Windows.Forms.Button btnMyHistory;
        private System.Windows.Forms.Button btnLogout;
    }
}