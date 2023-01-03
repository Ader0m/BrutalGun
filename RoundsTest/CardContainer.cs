using System.Collections.Generic;
using System;
using UnboundLib.Cards;

namespace BrutalGun
{
    public static class CardContainer
    {
        private static Dictionary<Type, CardInfo> _storedCardInfo = new Dictionary<Type, CardInfo>();

        public static void RegisterCard<T>() where T : CustomCard
        {
            CustomCard.BuildCard<T>(StoreCard<T>);
        }

        private static void StoreCard<T>(CardInfo card) where T : CustomCard
        {
            _storedCardInfo.Add(typeof(T), card);
        }

        public static CardInfo GetCard<T>() where T : CustomCard
        {
            if (_storedCardInfo.TryGetValue(typeof(T), out CardInfo value))
            {
                return value;
            }

            UnityEngine.Debug.LogError("Card not found");

            return null;
        }
    }
}
