using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Item
    {
        protected string name = "N/a";
        protected string flavorText;
        protected int UniqueID = 0;
        protected int amount = 0;

        public string GetName() => name;
        public string GetFlavorText() => flavorText;
        public int GetAmount() => amount;
        public int GetUniqueID() => UniqueID;


        public void SetAmount(int pAmount) => amount = pAmount;
        
        
        public void AddAmount(int pAmount) => amount += pAmount;
    }
}
