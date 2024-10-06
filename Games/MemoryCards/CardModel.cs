using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.Games.MemoryCards
{
    public class CardModel
    {
        public int Id { get; set; }
        public string Color { get; set; }   
        public bool IsFlipped { get; set; }  
        public bool IsMatched { get; set; }  

        public CardModel(int id, string color)
        {
            Id = id;
            Color = color;
            IsFlipped = false;
            IsMatched = false;
        }
    }
}
