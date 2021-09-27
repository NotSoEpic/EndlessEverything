using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using System.Collections.Generic;
using Terraria.ModLoader.Config;
using System.Linq;
using EndlessEverything.Items;

namespace EndlessEverything
{
    class EndlessDeterminer
    {
        private static readonly EndlessConfigServer cfg = ModContent.GetInstance<EndlessConfigServer>();

        // returns true if the item is endless
        public static bool IsEndless(Item item, Player player) // tests if player is in journey mode, and if they have endless
        {
            if (item.type == ModContent.ItemType<EndlessEnabler>() || IsntOverride(item))
            {
                return false;
            }
            return
                (IsEndless(item, player.difficulty == 3) || (HasBeenResearched(item, player) && CanBeEndless(item, true)))
                && HasEndless(player);
        }

        public static bool IsEndless(Item item, bool journey)
        {
            // if explicitly disabled
            if (IsntOverride(item))
            {
                return false;
            }
            // if has enough to be researched and is ammo, consumable, potion, placeable, or specifically defined
            if (HasResearchAmount(item) && (cfg.UseResearch || journey) && CanBeEndless(item, journey))
            {
                return true;
            }
            // if has general amounts defined for ammo, consumable, potion, or specifically defined (overrides general)
            if ((IsAmmoCount(item) || IsConsumableCount(item) || IsPotionCount(item)) && !IsInItemDefinitionL(cfg.OverrideEnable, item) || IsOverride(item))
            {
                return true;
            }
            return false;
        }

        public static bool IsEndless(Item item) // assumes no journey
        {
            return IsEndless(item, false);
        }

        public static bool CanBeEndless(Item item, bool journey)
        {
            return IsAmmo(item) || IsConsumable(item) || IsPotion(item) || (IsPlaceable(item) && journey) || IsInItemDefinitionL(cfg.OverrideEnable, item);
        }

        public static bool HasEndless(Player player)
        {
            EndlessPlayer p = player.GetModPlayer<EndlessPlayer>();
            return cfg.StartEnabled || p.endless;
        }

        // returns true if the itemstack is greater than the required research
        public static bool HasResearchAmount(Item item)
        {
            Dictionary<int, int> r = CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId;
            if (r.ContainsKey(item.type) && item.stack >= r[item.type]) // if item can be researched and has enough
            {
                return true;
            }
            return false;
        }

        public static bool HasBeenResearched(Item item, Player player)
        {
            Dictionary<int, int> s = player.creativeTracker.ItemSacrifices.SacrificesCountByItemIdCache;
            Dictionary<int, int> r = CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId;
            if (s.ContainsKey(item.type) && s[item.type] >= r[item.type]) // if enough items have been researched
            {
                return true;
            }
            return false;
        }

        public static bool IsAmmo(Item item)
        {
            return item.ammo != 0;
        }

        public static bool IsConsumable(Item item)
        {
            if (item.createTile != -1 || item.createWall != -1 || item.accessory || item.buffType != 0) // blocks and potions count as consumable
            {
                return false;
            }
            return item.consumable;
        }

        public static bool IsPotion(Item item)
        {
            return (item.buffType != 0 || item.healLife > 0 || item.healMana > 0) &&
                item.damage <= 0 &&
                !Main.lightPet[item.buffType] && !Main.vanityPet[item.buffType]; // light / vanity pet items pass every other test
        }

        public static bool IsAmmoCount(Item item)
        {
            return IsAmmo(item) && (item.stack >= cfg.AmmoStack || item.stack >= item.maxStack);
        }

        public static bool IsPlaceable(Item item)
        {
            return item.createTile != -1 || item.createWall != -1;
        }

        public static bool IsConsumableCount(Item item)
        {
            return IsConsumable(item) && (item.stack >= cfg.ConsumableStack || item.stack >= item.maxStack);
        }

        public static bool IsPotionCount(Item item)
        {
            return IsPotion(item) && (item.stack >= cfg.PotionStack || item.stack >= item.maxStack);
        }

        // returns true and associated value if it key exists, or false and -1 if it doesnt
        public static (bool, int) IsInItemDefinitionD(Dictionary<ItemDefinition, int> d, Item item)
        {
            foreach (KeyValuePair<ItemDefinition, int> kv in d)
            {
                if (kv.Key.Type == item.type) { return (true, kv.Value); }
            }
            return (false, -1);
        }

        public static bool IsInItemDefinitionL(List<ItemDefinition> l, Item item)
        {
            return l.Contains(new ItemDefinition(item.type));
        }

        public static bool IsInItemDefinitionL(Dictionary<ItemDefinition, int> d, Item item)
        {
            List<ItemDefinition> l = d.Keys.ToList();
            return l.Contains(new ItemDefinition(item.type));
        }

        public static bool IsOverride(Item item)
        {
            (bool, int) idl = IsInItemDefinitionD(cfg.OverrideEnable, item);
            return idl.Item1 && (item.stack >= idl.Item2 || item.stack >= item.maxStack);
        }

        public static bool IsntOverride(Item item)
        {
            return IsInItemDefinitionL(cfg.OverrideDisable, item);
        }
    }
}
