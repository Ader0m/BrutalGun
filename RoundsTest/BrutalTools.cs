using System.Collections.Generic;
using UnityEngine;

namespace BrutalGun
{
    public static class BrutalTools
    {
        public static (GameObject AddToProjectile, GameObject effect, Explosion explosion) LoadExplosionElements()
        {
            GameObject explosiveBullet = (GameObject)Resources.Load("0 cards/Explosive bullet");
            Gun explosiveGun = explosiveBullet.GetComponent<Gun>();

            GameObject A_ExplosionSpark = explosiveGun.objectsToSpawn[0].AddToProjectile;
            GameObject explosionCustom = MonoBehaviour.Instantiate(explosiveGun.objectsToSpawn[0].effect);
            explosionCustom.transform.position = new UnityEngine.Vector3(1000, 0, 0);
            explosionCustom.hideFlags = HideFlags.HideAndDontSave;
            explosionCustom.name = "customExpl";

            UnityEngine.Object.DestroyImmediate(explosionCustom.GetComponent<RemoveAfterSeconds>());


            return (A_ExplosionSpark, explosionCustom, explosionCustom.GetComponent<Explosion>());
        }

        public static void AddCurse(Player sender, CardInfo cardInfo, List<Player>? receivers = null)
        {
            foreach (Player enemy in receivers == null ? PlayerManager.instance.players : receivers)
            {
                if (enemy.playerID != sender.playerID)
                {
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(enemy, cardInfo, false, "", 0, 0);
                }
            }
        }

        public static void RemoveCurse(Player sender, CardInfo cardInfo, List<Player>? receivers = null)
        {
            foreach (Player enemy in receivers == null ? PlayerManager.instance.players : receivers)
            {
                if (enemy.playerID != sender.playerID)
                {
                    ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(enemy, cardInfo, ModdingUtils.Utils.Cards.SelectionType.Oldest);
                }
            }
        }
    }
}
