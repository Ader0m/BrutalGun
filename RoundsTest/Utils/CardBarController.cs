using BrutalGun.Cards;
using BrutalGun.Cards.VimpireCard.Rare;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnboundLib;
using UnityEngine;

namespace BrutalGun.Utils
{
    public class CardBarController : MonoBehaviour
    {
        /// <summary>
        /// key - PlayerID, value - count player card
        /// </summary>
        public Dictionary<int, int> CardBarLengthDict;
        public static bool InProcess = false;

        public CardBarController()
        {
            CardBarLengthDict = new Dictionary<int, int>();
        }

        public IEnumerator AdaptateHumanToVampire(Player player)
        {
            while (InProcess)
            {
                UnityEngine.Debug.Log("AdaptateHumanToVampire - wait");
                yield return null;
            }

            InProcess = true;

            CardInfo[] CommonCardNameObj =  {   CardContainer.GetCard<ArmsReinforcement>(),
                                                CardContainer.GetCard<BatWatching>(),
                                                CardContainer.GetCard<BodyReinforcement>(),                                                                                      
                                                CardContainer.GetCard<LegsReinforcement>(),
                                                CardContainer.GetCard<TasteBlood>(),
                                            };

            CardInfo[] UncommonCardNameObj ={   CardContainer.GetCard<AuraGreat>(),
                                                CardContainer.GetCard<HeavyPunch>(),
                                                CardContainer.GetCard<LongPunch>(),
                                                CardContainer.GetCard<Lacerations>()
                                            };

            CardInfo[] RareCardNameObj =    {   CardContainer.GetCard<Dash>(),
                                                CardContainer.GetCard<DevilMantle>(),
                                                CardContainer.GetCard<SteelClaws>()                                               
                                            };

            for (int i = 0; i < player.data.currentCards.Count; i++)
            {
                yield return null;

                switch (player.data.currentCards[i].cardName)
                {
                    case "Rocket Jump":
                        {
                            yield return BrutalTools.ReplaseCard(player, CardContainer.GetCard<BatWatching>(), i);

                            break;
                        }
                    case "IFAK":
                        {
                            yield return BrutalTools.ReplaseCard(player, CardContainer.GetCard<TasteBlood>(), i);

                            break;
                        }
                    case "Armor-piercing bullet":
                        {
                            yield return BrutalTools.ReplaseCard(player, CardContainer.GetCard<SteelClaws>(), i);

                            break;
                        }
                    case "Armored Suit":
                        {
                            yield return BrutalTools.ReplaseCard(player, CardContainer.GetCard<DevilMantle>(), i);

                            break;
                        }
                    case "Hummer Bullet":
                        {
                            yield return BrutalTools.ReplaseCard(player, CardContainer.GetCard<HeavyPunch>(), i);

                            break;
                        }
                    case "Long Barrel":
                        {
                            yield return BrutalTools.ReplaseCard(player, CardContainer.GetCard<LongPunch>(), i);

                            break;
                        }
                    default:
                        {
                            if (player.data.currentCards[i].categories.Contains(MyCategories.Human))
                            {
                                switch (player.data.currentCards[i].rarity)
                                {
                                    case CardInfo.Rarity.Common:
                                        {
                                            yield return BrutalTools.ReplaseCard(player, CommonCardNameObj.GetRandom<CardInfo>(), i);
                                            break;
                                        }
                                    case CardInfo.Rarity.Uncommon:
                                        {
                                            yield return BrutalTools.ReplaseCard(player, UncommonCardNameObj.GetRandom<CardInfo>(), i);
                                            break;
                                        }
                                    case CardInfo.Rarity.Rare:
                                        {
                                            yield return BrutalTools.ReplaseCard(player, RareCardNameObj.GetRandom<CardInfo>(), i);
                                            break;
                                        }
                                    default:
                                        {
                                            yield return BrutalTools.ReplaseCard(player, RareCardNameObj.GetRandom<CardInfo>(), i);
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                }
            }

            InProcess = false;
        }

        public IEnumerator CheckCorrectCardBar(Player player)
        {
            while (InProcess)
            {
                UnityEngine.Debug.Log("CheckCorrectCardBar - wait");
                yield return null;
            }

            InProcess = true;

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

            InProcess = false;
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
        /// When startPlayercards not include weapon
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
        /// When startPlayercards include weapon
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
