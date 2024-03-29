﻿using System.Collections;
using UnboundLib.Cards;
using UnboundLib.GameModes;

namespace BrutalGun.BetterCardControl
{
    public class DynamicCardStatsManager
    {
        public delegate void StatsHundler(DynamicType type);

        private event StatsHundler OnGameStartEvent;
        private event StatsHundler OnPickPhaseStartEvent;
        private event StatsHundler OnPickPhaseEndEvent;

        public enum DynamicType
        {
            OnGameStart,
            OnPickPhaseStart,
            OnPickPhaseEnd
        }

        public DynamicCardStatsManager()
        {
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, OnPickPhaseEnd);
            GameModeManager.AddHook(GameModeHooks.HookPickStart, OnPickPhaseStart);
            GameModeManager.AddHook(GameModeHooks.HookGameStart, OnGameStart, 99);           
        }

        public void AddDynamicCard<T>(DynamicType type) where T : CustomCard
        {
            switch (type)
            {
                case DynamicType.OnGameStart:
                    {
                        if (CardContainer.GetCard<T>().GetComponent<T>() is IDynamicCardStats temp)
                        {
                            OnGameStartEvent += temp.ChangeCardStats;
                        }

                        break;
                    }
                case DynamicType.OnPickPhaseStart:
                    {
                        if (CardContainer.GetCard<T>().GetComponent<T>() is IDynamicCardStats temp)
                        {
                            
                            OnPickPhaseStartEvent += temp.ChangeCardStats;
                        }

                        break;
                    }
                case DynamicType.OnPickPhaseEnd:
                    {
                        if (CardContainer.GetCard<T>().GetComponent<T>() is IDynamicCardStats temp)
                        {
                            OnPickPhaseEndEvent += temp.ChangeCardStats;
                        }

                        break;
                    }
            }
        }

        private IEnumerator OnGameStart(IGameModeHandler arg)
        {
            if (OnGameStartEvent != null)
                OnGameStartEvent.Invoke(DynamicType.OnGameStart);        

            yield break;
        }

        private IEnumerator OnPickPhaseStart(IGameModeHandler arg)
        {
            if (OnPickPhaseStartEvent != null)
                OnPickPhaseStartEvent.Invoke(DynamicType.OnPickPhaseStart);  

            yield break;
        }

        private IEnumerator OnPickPhaseEnd(IGameModeHandler arg)
        {
            if (OnPickPhaseEndEvent != null)
                OnPickPhaseEndEvent.Invoke(DynamicType.OnPickPhaseEnd);

            yield break;
        }
    }
}
