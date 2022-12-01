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
            _player.data.healthHandler.regeneration += _regen;
            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(
                _player, SupportCardContainer.PowerfullBerserk, false, "", 0, 0);
            Destroy(this, _regenDuration);
        }

        private void OnDestroy()
        {
            ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(
                _player, SupportCardContainer.PowerfullBerserk, ModdingUtils.Utils.Cards.SelectionType.All);
            AddCardNextFrame();
            _player.data.healthHandler.regeneration -= _regen;        
            _player.data.healthHandler.TakeDamageOverTime(UnityEngine.Vector2.up * _sumDamage, UnityEngine.Vector2.zero, _damageDuration, 1f, Color.red);
        }

        private IEnumerator AddCardNextFrame()
        {
            yield return null;
            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(
               _player, SupportCardContainer.DyingBerserk, false, "", 0, 0);
        }
    }
}

