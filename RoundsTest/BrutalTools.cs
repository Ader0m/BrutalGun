using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
