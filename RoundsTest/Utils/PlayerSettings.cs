using System.Collections.Generic;

namespace BrutalGun.Utils
{
    public static class PlayerSettings
    {
        public static void SetStartStats(List<CardInfo> startCardList)
        {
            foreach (Player player in BrutalGunMain.Instance.PlayersMass)
            {
                ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, startCardList.ToArray(), false, null, null, null);
            }
        }
    }
}
