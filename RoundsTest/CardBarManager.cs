using BrutalGun.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace BrutalGun
{
    public class CardBarManager : MonoBehaviour
    {
        public Dictionary<Player, int> CardBarLengthDict;
        public Player[] nonAIPlayers;

        public CardBarManager()
        {
            CardBarLengthDict = new Dictionary<Player, int>();
        }

        public IEnumerator CheckCardBar()
        {
            UnityEngine.Debug.Log("fff2");
            

            foreach (Player player in BrutalGunMain.Instance.PLAYERS)
            {
                // try het value
                if (CardBarLengthDict.ContainsKey(player))
                {
                    FindExtraWeapon(player);
                }
                else
                {
                    CardBarLengthDict[player] = player.data.currentCards.Count;
                    FindExtraWeapon(player);
                }
            }

            yield break;
        }

        private void FindExtraWeapon(Player player)
        {
            bool rebuildCardBar = false;
            List<int> countWeaponCardsIndex = new List<int>();
            CardInfo currentWearon = null;
            List<CardInfo> cards = new List<CardInfo>();

            for (int i = 0; i < player.data.currentCards.Count; i++)
            {
                if (player.data.currentCards[i].categories.Contains(MyCategories.Weapon))
                {
                    countWeaponCardsIndex.Add(i);
                    currentWearon = player.data.currentCards[i];
                }
                else
                {
                    cards.Add(player.data.currentCards[i]);
                }
            }

            if (currentWearon != null)
            {
                ModdingUtils.Utils.Cards.instance.RemoveAllCardsFromPlayer(player, true);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, currentWearon, false, "", 0, 0);
                ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, cards.ToArray(), false, null, null, null);
            }
        }
    }
}
