namespace ABShell
{
    partial class CheckBox2
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
            this.lbText = new System.Windows.Forms.Label();
            this.pbOff = new System.Windows.Forms.PictureBox();
            this.pbOn = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbOff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOn)).BeginInit();
            this.SuspendLayout();
            // 
            // lbText
            // 
            this.lbText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbText.Location = new System.Drawing.Point(88, 14);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(0, 23);
            this.lbText.TabIndex = 2;
            this.lbText.Click += new System.EventHandler(this.pbOff_Click);
            // 
            // pbOff
            // 
            this.pbOff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbOff.Image = global::ABShell.Properties.Resources.cb1;
            this.pbOff.Location = new System.Drawing.Point(0, 1);
            this.pbOff.Name = "pbOff";
            this.pbOff.Size = new System.Drawing.Size(82, 47);
            this.pbOff.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOff.TabIndex = 1;
            this.pbOff.TabStop = false;
            this.pbOff.Click += new System.EventHandler(this.pbOff_Click);
            // 
            // pbOn
            // 
            this.pbOn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbOn.Image = global::ABShell.Properties.Resources.cb21;
            this.pbOn.Location = new System.Drawing.Point(0, 1);
            this.pbOn.Name = "pbOn";
            this.pbOn.Size = new System.Drawing.Size(82, 47);
            this.pbOn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOn.TabIndex = 0;
            this.pbOn.TabStop = false;
            this.pbOn.Click += new System.EventHandler(this.pbOff_Click);
            // 
            // CheckBox2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbText);
            this.Controls.Add(this.pbOff);
            this.Controls.Add(this.pbOn);
            this.DoubleBuffered = true;
            this.Name = "CheckBox2";
            this.Size = new System.Drawing.Size(82, 50);
            this.Click += new System.EventHandler(this.CheckBox2_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pbOff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbOn;
        private System.Windows.Forms.PictureBox pbOff;
        private System.Windows.Forms.Label lbText;
    }
}
