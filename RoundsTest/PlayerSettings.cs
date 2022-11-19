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
        public void SetStartStats()
        {
            foreach (Player player in BrutalGunMain.Instance.PLAYERS)
            {
                Gun gun = player.data.weaponHandler.gun;

                FieldInfo fieldInfo = typeof(Gun).GetField("gunAmmo", BindingFlags.Instance | BindingFlags.NonPublic);
                GunAmmo gunAmmo = (GunAmmo)fieldInfo.GetValue(gun);

                gunAmmo.maxAmmo = 5;
                gun.projectileSpeed = 1.4f;
                gun.multiplySpread = 0.09f;
                gun.damage = 0.8f;
            }
        }
    }
}
