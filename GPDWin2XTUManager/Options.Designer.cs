namespace GPDWin2XTUManager
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.lstOptionsProfiles = new System.Windows.Forms.ListBox();
            this.btnAddProfile = new System.Windows.Forms.Button();
            this.btnDeleteProfile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numMinW = new System.Windows.Forms.NumericUpDown();
            this.numMaxW = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numCPUuv = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSettingsOK = new System.Windows.Forms.Button();
            this.numGPUuv = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMinW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCPUuv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGPUuv)).BeginInit();
            this.SuspendLayout();
            // 
            // lstOptionsProfiles
            // 
            this.lstOptionsProfiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstOptionsProfiles.FormattingEnabled = true;
            this.lstOptionsProfiles.ItemHeight = 25;
            this.lstOptionsProfiles.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.lstOptionsProfiles.Location = new System.Drawing.Point(8, 10);
            this.lstOptionsProfiles.Name = "lstOptionsProfiles";
            this.lstOptionsProfiles.Size = new System.Drawing.Size(173, 204);
            this.lstOptionsProfiles.TabIndex = 0;
            this.lstOptionsProfiles.SelectedIndexChanged += new System.EventHandler(this.lstOptionsProfiles_SelectedIndexChanged);
            // 
            // btnAddProfile
            // 
            this.btnAddProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProfile.Location = new System.Drawing.Point(8, 230);
            this.btnAddProfile.Name = "btnAddProfile";
            this.btnAddProfile.Size = new System.Drawing.Size(74, 39);
            this.btnAddProfile.TabIndex = 7;
            this.btnAddProfile.Text = "+";
            this.btnAddProfile.UseVisualStyleBackColor = true;
            this.btnAddProfile.Click += new System.EventHandler(this.btnAddProfile_Click);
            // 
            // btnDeleteProfile
            // 
            this.btnDeleteProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteProfile.Location = new System.Drawing.Point(107, 230);
            this.btnDeleteProfile.Name = "btnDeleteProfile";
            this.btnDeleteProfile.Size = new System.Drawing.Size(74, 39);
            this.btnDeleteProfile.TabIndex = 8;
            this.btnDeleteProfile.Text = "-";
            this.btnDeleteProfile.UseVisualStyleBackColor = true;
            this.btnDeleteProfile.Click += new System.EventHandler(this.btnDeleteProfile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(187, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(345, 10);
            this.txtName.MaxLength = 12;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(227, 26);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "NEW PROFILE";
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(187, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Minimum Wattage";
            // 
            // numMinW
            // 
            this.numMinW.DecimalPlaces = 1;
            this.numMinW.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMinW.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numMinW.Location = new System.Drawing.Point(345, 46);
            this.numMinW.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numMinW.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numMinW.Name = "numMinW";
            this.numMinW.Size = new System.Drawing.Size(227, 26);
            this.numMinW.TabIndex = 2;
            this.numMinW.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numMinW.ValueChanged += new System.EventHandler(this.numMinW_ValueChanged);
            // 
            // numMaxW
            // 
            this.numMaxW.DecimalPlaces = 1;
            this.numMaxW.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMaxW.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numMaxW.Location = new System.Drawing.Point(345, 78);
            this.numMaxW.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numMaxW.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numMaxW.Name = "numMaxW";
            this.numMaxW.Size = new System.Drawing.Size(227, 26);
            this.numMaxW.TabIndex = 3;
            this.numMaxW.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numMaxW.ValueChanged += new System.EventHandler(this.numMaxW_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(187, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Maximum Wattage";
            // 
            // numCPUuv
            // 
            this.numCPUuv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCPUuv.Location = new System.Drawing.Point(345, 110);
            this.numCPUuv.Name = "numCPUuv";
            this.numCPUuv.Size = new System.Drawing.Size(227, 26);
            this.numCPUuv.TabIndex = 4;
            this.numCPUuv.ValueChanged += new System.EventHandler(this.numCPUuv_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(187, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "CPU Undervolt (mV)";
            // 
            // btnSettingsOK
            // 
            this.btnSettingsOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettingsOK.Location = new System.Drawing.Point(345, 203);
            this.btnSettingsOK.Name = "btnSettingsOK";
            this.btnSettingsOK.Size = new System.Drawing.Size(227, 66);
            this.btnSettingsOK.TabIndex = 6;
            this.btnSettingsOK.Text = "OK";
            this.btnSettingsOK.UseVisualStyleBackColor = true;
            this.btnSettingsOK.Click += new System.EventHandler(this.btnSettingsOK_Click);
            // 
            // numGPUuv
            // 
            this.numGPUuv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numGPUuv.Location = new System.Drawing.Point(345, 142);
            this.numGPUuv.Name = "numGPUuv";
            this.numGPUuv.Size = new System.Drawing.Size(227, 26);
            this.numGPUuv.TabIndex = 5;
            this.numGPUuv.ValueChanged += new System.EventHandler(this.numGPUuv_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(187, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "GPU Undervolt (mV)";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 281);
            this.Controls.Add(this.numGPUuv);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSettingsOK);
            this.Controls.Add(this.numCPUuv);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numMaxW);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numMinW);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDeleteProfile);
            this.Controls.Add(this.btnAddProfile);
            this.Controls.Add(this.lstOptionsProfiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 320);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 320);
            this.Name = "Options";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_FormClosing);
            this.Load += new System.EventHandler(this.Options_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMinW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCPUuv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGPUuv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstOptionsProfiles;
        private System.Windows.Forms.Button btnAddProfile;
        private System.Windows.Forms.Button btnDeleteProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numMinW;
        private System.Windows.Forms.NumericUpDown numMaxW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numCPUuv;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSettingsOK;
        private System.Windows.Forms.NumericUpDown numGPUuv;
        private System.Windows.Forms.Label label5;
    }
}