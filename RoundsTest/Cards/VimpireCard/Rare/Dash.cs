﻿using ModsPlus;
using System.Collections;

namespace BrutalGun.Cards.VimpireCard.Rare
{
    public class Dash : CustomEffectCard<DashEffect>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Dash",
            Description = "Crush the space with your strength",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Work with block",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Move forward",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Speed",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Vampire };
            cardInfo.allowMultiple = true;

            statModifiers.movementSpeed = 1.1f;
        }
    }

    public class DashEffect: CardEffect
    {
        public float DashSpeed = 35;
        public float JumpM = 2;

        public override void OnBlock(BlockTrigger.BlockTriggerType blockTriggerType)
        {       
            StartCoroutine(Dash(data.stats.movementSpeed));
        }

        public override void OnUpgradeCard()
        {
            DashSpeed *= 1.5f;
            JumpM *= 1.5f;
        }

        private IEnumerator Dash(float resetSpeed)
        {           
            data.stats.movementSpeed = DashSpeed;
            data.input.direction = (gun.shootPosition.position - transform.position).normalized;
            
            if (data.input.direction.y > 0)
            {
                data.isGrounded = true;
                data.currentJumps++;
                data.jump.Jump(false, data.input.direction.y * JumpM);             
            }

            yield return null;

            data.stats.movementSpeed = resetSpeed;

            yield break;
        }
    }
}
