using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Inventory
    {
        List<Item> itemList = new List<Item>();

        //base equiped items
        Equipment[] equippedItems = new Equipment[] { new Equipment_Sword(), new Equipment_Bow() };

        //holds the values the inventory will always have
        int maxHealingPotionAmount = 1;
        int healingPotionAmount = 1;
        int moneyAmount = 0;

        public Inventory() 
        { 
            //player always starts with a compass
            itemList.Add(new Compass());
        }


        public void PrintInventory(Player pPlayer)
        {
            //clears the screen
            Console.Clear();

            //shows the players stats
            pPlayer.PrintShowStats();

            Console.WriteLine();

            //display basic information
            Console.WriteLine($"Coins: {moneyAmount}");
            Console.WriteLine($"Healing Potions: {healingPotionAmount}/{maxHealingPotionAmount}");
            Console.WriteLine();

            //shows equipped equipment
            Console.WriteLine($"Slot 1: {equippedItems[0].GetName()} : Min Damage:{equippedItems[0].GetItemDamage()[0]}, Max Damage:{equippedItems[0].GetItemDamage()[1]}");
            Console.WriteLine($"Slot 2: {equippedItems[1].GetName()} : Min Damage:{equippedItems[1].GetItemDamage()[0]}, Max Damage:{equippedItems[1].GetItemDamage()[1]}");
            Console.WriteLine();


            if (itemList.Count > 0)
            {
                //write all items to screen
                foreach (Item item in itemList)
                {
                    string itemAmount = (item.GetAmount() != 0) ? $"Amount: {item.GetAmount()}" : "";
                    Console.WriteLine($"- {item.GetName()} {itemAmount}");
                }
            } 
            else 
            { 
                Console.WriteLine("- No items in your inventory!"); 
            }

            Console.WriteLine("\nPress any key to close menu");

        }



        public int GetMaxHealingPotionAmount() => maxHealingPotionAmount;
        public void SetMaxHealingPotionAmount(int pAmount)
        {
            int difference = maxHealingPotionAmount - pAmount;

            maxHealingPotionAmount += difference;
            healingPotionAmount += difference;
        }
        public void AddMaxHealingPotionAmount(int pAmount)
        {
            maxHealingPotionAmount += pAmount;
            healingPotionAmount += pAmount;
        }



        public int GetHealingPotionAmount() => healingPotionAmount;
        public void SetHealingPotionAmount(int pAmount) => healingPotionAmount = pAmount;
        public void AddHealingPotionAmount(int pAmount) => healingPotionAmount += pAmount;
        public void RechargeHealingPotion() => healingPotionAmount = maxHealingPotionAmount;



        public int GetCoins() => moneyAmount;
        public void SetCoins(int pMoney) => moneyAmount = pMoney;
        public void AddCoins(int pMoney) => moneyAmount += pMoney;



        public Equipment[] GetEquippedItems() => equippedItems;



        /// <summary>
        /// checks if the inventory contains an item with a given name
        /// </summary>
        /// <param name="pItemName"></param>
        /// <returns></returns>
        public bool ContainsByName(string pItemName)
        {
            //goes through all the items
            foreach (Item item in itemList)
            {
                //if the item is in the list then return true
                if (item.GetName() == pItemName) { return true; };
            }

            //if not in list return false
            return false;
        }
        /// <summary>
        /// check if the inventory contains an item with a given ID
        /// </summary>
        /// <param name="pItemID"></param>
        /// <returns></returns>
        public bool ContainsByID(int pItemID)
        {
            //goes through all the items
            foreach (Item item in itemList)
            {
                //if the item is in the list then return true
                if (item.GetUniqueID() == pItemID) { return true; };
            }

            //if not in list return false
            return false;
        }



        public void AddItem(Item pItem) => itemList.Add(pItem);
    }
}
