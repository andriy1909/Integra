using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ABShell
{
    [Serializable]
    public class ProgramSetting
    {
        public int id;
        public string path;
        public Image image;
        public bool isVisible;
        public string name;

        public ProgramSetting()
        {

        }

        public void revers()
        {
            isVisible = !isVisible;
        }

        public void setSetting(UserButton button)
        {
            id = button.id;
            path = button.path;
            name = button.SetText;
            image = button.image;
            isVisible = button.isVisible;
        }
        public UserButton getButton()
        {
            UserButton button = new UserButton();
            button.id = id;
            button.path = path;
            button.image = image;
            button.SetText = name;
            button.isVisible = isVisible;
            return button;
        }
    }
}

