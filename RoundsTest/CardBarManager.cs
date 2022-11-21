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
        public Dictionary<int, int> CardBarLengthDict;
        public Player[] nonAIPlayers;

        public CardBarManager()
        {
            CardBarLengthDict = new Dictionary<int, int>();
        }

        //найти эвент рематч и обнулить массивы
        //продебажить количество карт в массиве и у игрока, до пика и после пика
        public IEnumerator CheckCardBar()
        {
            UnityEngine.Debug.Log("Start");

            foreach (Player player in BrutalGunMain.Instance.PLAYERS)
            {
                
                if (CardBarLengthDict.ContainsKey(player.playerID))
                {
                    UnityEngine.Debug.Log("Containse" + player.playerID);
                    UnityEngine.Debug.Log("Foreach InDict " + CardBarLengthDict[player.playerID] + " InPlayer " + player.data.currentCards.Count);
                    if (CardBarLengthDict[player.playerID] < player.data.currentCards.Count)
                    {
                        UnityEngine.Debug.Log("Start containce and + card" + player.playerID);                      
                        FindExtraWeapon(player);
                    }
                    else
                    {
                        UnityEngine.Debug.Log("pass" + player.playerID);
                    }                
                }
                else
                {
                    UnityEngine.Debug.Log("Not containse" + player.playerID);
                    FindExtraWeapon(player);
                }
                UnityEngine.Debug.Log("- - - - - - - - - - - - - - - - - - -");
            }

            UnityEngine.Debug.Log("------------------------------------------------");
            yield break;
        }

        private void FindExtraWeapon(Player player)
        {          
            UnityEngine.Debug.Log("FindExtraWeapon start");
            int countWeapon = 0;
            List<int> countWeaponCardsIndex = new List<int>();
            CardInfo currentWeapon = null;
            List<CardInfo> cards = new List<CardInfo>();

            for (int i = 0; i < player.data.currentCards.Count; i++)
            {
                if (player.data.currentCards[i].categories.Contains(MyCategories.Weapon))
                {
                    UnityEngine.Debug.Log("FindWeapon " + player.data.currentCards[i].cardName);
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
                UnityEngine.Debug.Log("Rebuild1");
                ModdingUtils.Utils.Cards.instance.RemoveAllCardsFromPlayer(player, true);
                UnityEngine.Debug.Log("Rebuild2");
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, currentWeapon, false, "", 0, 0);
                if (cards.Count != 0)
                {
                    UnityEngine.Debug.Log("Rebuild3");
                    ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, cards.ToArray(), false, null, null, null);
                }
                UnityEngine.Debug.Log("Rebuild4");
            }
            
            CardBarLengthDict[player.playerID] = player.data.currentCards.Count - countWeapon + 1;
            UnityEngine.Debug.Log("Foreach InDict " + CardBarLengthDict[player.playerID] + " InPlayer " + player.data.currentCards.Count);                    
        }
        public void Restore()
        {
            CardBarLengthDict.Clear();
        }
    }
}
