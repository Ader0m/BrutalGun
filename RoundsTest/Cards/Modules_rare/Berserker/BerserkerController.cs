using UnboundLib.Cards;
using UnityEngine;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using BrutalGun;
using ModsPlus;
using System.Runtime.CompilerServices;
using Photon.Pun.UtilityScripts;
using System.Xml.Linq;
using UnboundLib.GameModes;
using System.Collections;
using System.Reflection;
using System.Numerics;

namespace BrutalGun.Cards
{
    public class BerserkerController : MonoBehaviour
    {
        private float _regenDuration, _regen, _damageDuration, _sumDamage;
        private Player _player;
        private float _dSpread, _dSpeed;


        /// <summary>
        /// finally damage = damage / damage duration
        /// </summary>
        /// <param name="regenDuration"></param>
        /// <param name="regen"></param>
        /// <param name="damageDuration"></param>
        /// <param name="damage"></param>
        public void Initialize(float regenDuration, float regen, float damageDuration, float damage)
        {
            _regenDuration = regenDuration;
            _regen = regen;
            _damageDuration = damageDuration;
            _sumDamage = damage;
        }

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {          
            _dSpread = _player.data.weaponHandler.gun.spread - _player.data.weaponHandler.gun.spread * 0.8f;
            _dSpeed = _player.data.stats.movementSpeed - _player.data.stats.movementSpeed * 1.2f;
            
            //buf
            _player.data.healthHandler.regeneration += _regen;
            _player.data.weaponHandler.gun.spread -= _dSpread;
            _player.data.stats.movementSpeed -= _dSpeed;
            Destroy(this, _regenDuration);
        }

        private void OnDestroy()
        {
            //reset
            _player.data.healthHandler.regeneration -= _regen;
            _player.data.weaponHandler.gun.spread += _dSpread;
            _player.data.stats.movementSpeed += _dSpeed;

            //debuf
            _player.data.weaponHandler.gun.spread += _dSpread;
            _player.data.stats.movementSpeed += _dSpeed;

            _player.data.healthHandler.TakeDamageOverTime(UnityEngine.Vector2.up * _sumDamage, UnityEngine.Vector2.zero, _damageDuration, 1f, Color.red);
        }
    }
}

