using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrutalGun.Cards
{
    public static class MyCategories
    {
        public static CardCategory Weapon = CustomCardCategories.instance.CardCategory("Weapon");
        public static CardCategory Module = CustomCardCategories.instance.CardCategory("Module");
        public static CardCategory Vampire = CustomCardCategories.instance.CardCategory("Vampire");
        public static CardCategory Human = CustomCardCategories.instance.CardCategory("Human");
        public static CardCategory TimeEffect = CustomCardCategories.instance.CardCategory("TimeEffect");
    }
}
