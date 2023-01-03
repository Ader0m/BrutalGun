using System.Collections.Generic;
using System;
using UnboundLib.Cards;

namespace BrutalGun
{
    public static class CardContainer
    {
        public static CardInfo DevilMantleCurse;
        public static CardInfo AuraGreatCurse;

        private static Dictionary<Type, CardInfo> storedCardInfo = new Dictionary<Type, CardInfo>();

        public static void RegisterCard<T>() where T : CustomCard
        {
            CustomCard.BuildCard<T>(StoreCard<T>);
        }

        private static void StoreCard<T>(CardInfo card) where T : CustomCard
        {
            storedCardInfo.Add(typeof(T), card);
        }

        public static CardInfo GetCard<T>() where T : CustomCard
        {
            if (storedCardInfo.TryGetValue(typeof(T), out CardInfo value))
            {
                return value;
            }

            UnityEngine.Debug.LogError("Card not found");

            return null;
        }
    }
}
