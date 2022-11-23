using BrutalGun.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace BrutalGun
{
    public class PlayerSettings
    {
        public void SetStartStats(List<CardInfo> startCardList)
        {
            foreach (Player player in BrutalGunMain.Instance.PlayersMass)
            {
                ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, startCardList.ToArray(), false, null, null, null);
            }
        }
    }
}
