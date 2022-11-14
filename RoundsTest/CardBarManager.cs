using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace RoundsTest
{
    public class CardBarManager : MonoBehaviour
    {
        public Dictionary<int, int> CardBarLengthDict;
        private CardBar[] cardBars;

        public CardBarManager()
        {
            CardBarLengthDict = new Dictionary<int, int>();

        }

        public void CheckCardBar(Player player)
        {
            var triggeringPlayer = player;
            var nonAIPlayers = PlayerManager.instance.players.Where((person) => !ModdingUtils.AIMinion.Extensions.CharacterDataExtension.GetAdditionalData(person.data).isAIMinion).ToArray();
            var originalBoard = nonAIPlayers.ToDictionary((person) => person, (person) => person.data.currentCards.ToArray());

            var newBoard = originalBoard.ToDictionary((kvp) => kvp.Key, (kvp) => kvp.Value.ToList());
        }
    }
}
