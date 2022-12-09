﻿using BrutalGun.Cards;
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
    public class CardBarController : MonoBehaviour
    {
        /// <summary>
        /// key - PlayerID, value - count player card
        /// </summary>
        public Dictionary<int, int> CardBarLengthDict;

        public CardBarController()
        {
            CardBarLengthDict = new Dictionary<int, int>();
        }

        public IEnumerator CheckCardBar(Player player)
        {                        
            if (CardBarLengthDict.ContainsKey(player.playerID))
            {
                if (CardBarLengthDict[player.playerID] < player.data.currentCards.Count)
                {                     
                    yield return FindExtraCard(player);
                }            
            }
            else
            {
                yield return FindExtraCard(player);
            }           
        }

        private IEnumerator FindExtraCard(Player player)
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
                else if (player.data.currentCards[i].categories.Contains(MyCategories.TimeEffect))
                {
                    ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, i);
                }
                else
                {
                    cards.Add(player.data.currentCards[i]);
                }
            }
            
            yield return WithStartWeapon(player, currentWeapon, cards, countWeapon);           
        }

        public void Restore()
        {
            CardBarLengthDict.Clear();
        }

        /// <summary>
        /// How startPlayercards not include weapon
        /// </summary>
        /// <param name="player"></param>
        /// <param name="currentWeapon"></param>
        /// <param name="cards"></param>
        /// <param name="countWeapon"></param>
        /// <returns></returns>
        private IEnumerator WithoutStartWeapon(Player player, CardInfo currentWeapon, List<CardInfo> cards, int countWeapon)
        {
            if (currentWeapon != null)
            {
                ModdingUtils.Utils.Cards.instance.RemoveAllCardsFromPlayer(player, true);
                yield return null;
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, currentWeapon, false, "", 0, 0);
                if (cards.Count != 0)
                {
                    ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, cards.ToArray(), false, null, null, null);
                }
            }

            CardBarLengthDict[player.playerID] = player.data.currentCards.Count - (countWeapon > 0 ? countWeapon + 1 : 0);
        }

        /// <summary>
        /// How startPlayercards include weapon
        /// </summary>
        /// <param name="player"></param>
        /// <param name="currentWeapon"></param>
        /// <param name="cards"></param>
        /// <param name="countWeapon"></param>
        /// <returns></returns>
        private IEnumerator WithStartWeapon(Player player, CardInfo currentWeapon, List<CardInfo> cards, int countWeapon)
        {           
            if (currentWeapon != null && countWeapon > 0)
            {
                ModdingUtils.Utils.Cards.instance.RemoveAllCardsFromPlayer(player, true);
                yield return null;
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, currentWeapon, false, "", 0, 0);
                if (cards.Count != 0)
                {
                    ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, cards.ToArray(), false, null, null, null);
                }
            }

            CardBarLengthDict[player.playerID] = player.data.currentCards.Count - countWeapon + 1;
        }
    }
}