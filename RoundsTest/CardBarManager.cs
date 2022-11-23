using BrutalGun.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using UnityEngine;

namespace BrutalGun
{
    public class CardBarManager : MonoBehaviour
    {
        /// <summary>
        /// key - PlayerID, value - count player card
        /// </summary>
        public Dictionary<int, int> CardBarLengthDict;

        public CardBarManager()
        {
            CardBarLengthDict = new Dictionary<int, int>();
        }

        public IEnumerator CheckCardBar()
        {
            foreach (Player player in BrutalGunMain.Instance.PLAYERS)
            {             
                if (CardBarLengthDict.ContainsKey(player.playerID))
                {
                    if (CardBarLengthDict[player.playerID] < player.data.currentCards.Count)
                    {                     
                        FindExtraWeapon(player);
                    }            
                }
                else
                { 
                    FindExtraWeapon(player);
                }
            }

            yield break;
        }

        private void FindExtraWeapon(Player player)
        {          
            int countWeapon = 0;
            List<int> countWeaponCardsIndex = new List<int>();
            CardInfo currentWeapon = null;
            List<CardInfo> cards = new List<CardInfo>();


            for (int i = 0; i < player.data.currentCards.Count; i++)
            {
                if (player.data.currentCards[i].categories.Contains(MyCategories.Weapon))
                {
                    countWeaponCardsIndex.Add(i);
                    currentWeapon = player.data.currentCards[i];
                    countWeapon++;
                }
                else
                {
                    cards.Add(player.data.currentCards[i]);
                }
            }

            if (currentWeapon != null)
            {
                ModdingUtils.Utils.Cards.instance.RemoveAllCardsFromPlayer(player, true);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, currentWeapon, false, "", 0, 0);
                if (cards.Count != 0)
                {
                    ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, cards.ToArray(), false, null, null, null);
                }
            }
            
            CardBarLengthDict[player.playerID] = player.data.currentCards.Count - countWeapon + 1;                    
        }

        public void Restore()
        {
            CardBarLengthDict.Clear();
        }
    }
}
