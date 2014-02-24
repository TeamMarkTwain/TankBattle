using System;

namespace TankBattle.MenuItems
{
    public class MenuItem
    {
        private int x, y;
        private string name;
        private bool selected;
        /* properties */
        public int X
        {
            get { return this.x; }
        }
        public int Y
        {
            get { return this.y; }
        }
        public string Name
        {
            get { return this.name; }
        }
        public bool Selected
        {
            get { return this.selected; }
            set { this.selected = value; }
        }
        public MenuItem(int x, int y, string name, bool selected)   //constructor
        {
            this.x = x;
            this.y = y;
            this.name = name;
            this.selected = selected;
        }
    }
}
