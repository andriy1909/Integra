namespace ABShell
{
    partial class ButtonNew
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbSetting = new System.Windows.Forms.CheckBox();
            this.pbSetting = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSetting
            // 
            this.cbSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSetting.AutoSize = true;
            this.cbSetting.Location = new System.Drawing.Point(49, 50);
            this.cbSetting.Name = "cbSetting";
            this.cbSetting.Size = new System.Drawing.Size(15, 14);
            this.cbSetting.TabIndex = 2;
            this.cbSetting.UseVisualStyleBackColor = true;
            this.cbSetting.Visible = false;
            // 
            // pbSetting
            // 
            this.pbSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSetting.Image = global::ABShell.Properties.Resources.settings_5054;
            this.pbSetting.Location = new System.Drawing.Point(48, 0);
            this.pbSetting.Name = "pbSetting";
            this.pbSetting.Size = new System.Drawing.Size(16, 16);
            this.pbSetting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSetting.TabIndex = 3;
            this.pbSetting.TabStop = false;
            this.pbSetting.Visible = false;
            // 
            // ButtonNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pbSetting);
            this.Controls.Add(this.cbSetting);
            this.Name = "ButtonNew";
            this.Size = new System.Drawing.Size(64, 64);
            this.Load += new System.EventHandler(this.ButtonNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSetting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSetting;
        private System.Windows.Forms.CheckBox cbSetting;
    }
}
