using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ABShell
{
    public partial class ButtonApp : Button
    {
        public int id { get; set; }
        public string path { get; set; }
        private Image images;
        public string login { get; set; }
        public string password { get; set; }
        public string server { get; set; }
        public bool isVisible { get; set; }

        public Image image
        {
            get
            {
                return this.images;
            }

            set
            {
                this.images = value;
                if (BackgroundImage != value)
                    BackgroundImage = value;
            }
        }

        public ButtonApp()
        {
            InitializeComponent();
            Width = 75;
            Height = 75;
            FlatStyle = FlatStyle.Flat;
            BackgroundImageLayout = ImageLayout.Stretch;
            UseVisualStyleBackColor = true;
            FlatAppearance.BorderSize = 0;
            Text = "";
            id = -1;
            path = "";
            image = null;
            login = "";
            password = "";
            server = "";
            isVisible = false;
            BackgroundImageChanged += ButtonApp_BackgroundImageChanged;
        }

        private void ButtonApp_BackgroundImageChanged(object sender, EventArgs e)
        {
            image = BackgroundImage;
        }

        public void revers()
        {
            isVisible = !isVisible;
        }
    }
}