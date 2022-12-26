using BrutalGun.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnboundLib;
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

        public IEnumerator AdaptateHumanToVampire(Player player)
        {
            string[] CommonCardNameObj = {  "__BGun__Body Reinforcement",
                                            "__BGun__Arms Reinforcement",
                                            "__BGun__Legs Reinforcement"
                                            };

            string[] UncommonCardNameObj = {"__BGun__Aura Great",
                                            "__BGun__Heavy Punch",
                                            "__BGun__Long Punch",
                                            "__BGun__Lacerations"                                          
                                           };

            string[] RareCardNameObj = {    "__BGun__Dash"
                                            
                                            
                                       };

            for (int i = 0; i < player.data.currentCards.Count; i++)
            {
                yield return null;

                switch (player.data.currentCards[i].cardName)
                {
                    case "RocketJump":
                        {
                            yield return ReplaseCard("__BGun__Bat Watching", i);

                            break;
                        }
                    case "IFAK":
                        {
                            yield return ReplaseCard("__BGun__Taste Blood", i);                 

                            break;
                        }
                    case "Extended Magazin":
                        {
                            yield return ReplaseCard(CommonCardNameObj.GetRandom<string>(), i);

                            break;
                        }
                    case "Laser":
                        {
                            yield return ReplaseCard(CommonCardNameObj.GetRandom<string>(), i);

                            break;
                        }
                    case "Quick Drop":
                        {
                            yield return ReplaseCard(CommonCardNameObj.GetRandom<string>(), i);

                            break;
                        }
                    case "Titanium Parts":
                        {
                            yield return ReplaseCard(CommonCardNameObj.GetRandom<string>(), i);

                            break;
                        }
                    case "Armor-piercing bullet":
                        {
                            yield return ReplaseCard("__BGun__Steel Claws", i);

                            break;
                        }
                    case "Armored Suit":
                        {
                            yield return ReplaseCard("__BGun__The Devil Mantle", i);

                            break;
                        }
                    case "Scope X8":
                        {
                            yield return ReplaseCard(RareCardNameObj.GetRandom<string>(), i);

                            break;
                        }
                    case "AmmoXL":
                        {
                            yield return ReplaseCard("__BGun__Taste Blood", i);

                            break;
                        }
                    case "Hummer Bullet":
                        {
                            yield return ReplaseCard("__BGun__Heavy Punch", i);

                            break;
                        }
                    case "Big Bullet":
                        {                           
                            yield return ReplaseCard(UncommonCardNameObj.GetRandom<string>(), i);

                            break;
                        }              
                    case "Light Bolt":
                        {
                            yield return ReplaseCard(UncommonCardNameObj.GetRandom<string>(), i);

                            break;
                        }
                    case "Long Barrel":
                        {                            
                            yield return ReplaseCard("__BGun__Long Punch", i);

                            break;
                        }
                    case "Scope X2":
                        {
                            yield return ReplaseCard(UncommonCardNameObj.GetRandom<string>(), i);

                            break;
                        }
                    case "Light Armor":
                        {
                            yield return ReplaseCard(UncommonCardNameObj.GetRandom<string>(), i);

                            break;
                        }
                        default: { break; }
                }       
            }    

            IEnumerator ReplaseCard(string cardPath, int index)
            {
                yield return ModdingUtils.Utils.Cards.instance.ReplaceCard(player, index, ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(cardPath), "", 0, 0);
            }
        }

        public IEnumerator CheckCorrectCardBar(Player player)
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
            VampireManager.PlayerStatsDict.Clear();
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
                ModdingUtils.Utils.Cards.instance.RemoveAllCardsFromPlayer(player);
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
            if (currentWeapon != null && countWeapon > 1)
            {
                ModdingUtils.Utils.Cards.instance.RemoveAllCardsFromPlayer(player);
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
