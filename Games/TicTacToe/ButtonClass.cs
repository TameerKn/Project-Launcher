using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project_C_.Games.TicTacToe
{
    internal class ButtonClass : Button
    {
        string ButtonContent { get; set; }
        string ButtonName { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsPressed { get; set; }


        public ButtonClass(int col, int row, bool ispressed)
        {
            Row = row;
            Col = col;
            IsPressed = ispressed;
        }
    }
}
