using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using System.Collections.Generic;

namespace EndlessEverything
{
    class EndlessItem : GlobalItem
    {
        private static readonly EndlessConfigClient cfg = ModContent.GetInstance<EndlessConfigClient>();

        private int oldStack = 0;
        public override bool CanBeConsumedAsAmmo(Item ammo, Player player)
        {
            return !EndlessDeterminer.IsEndless(ammo, player);
        }

        public override void OnConsumeItem(Item item, Player player)
        {
            if (EndlessDeterminer.IsEndless(item, player))
            {
                // stores items stack before being decreased
                oldStack = item.stack;
                // increases item stack to prevent it from disappearing
                item.stack++;
                // in updateInventory: sets stack size to oldStack in case an item wasn't actually consumed
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Player p = Main.player[Main.myPlayer];
            if (EndlessDeterminer.IsEndless(item, p))
            {
                if (cfg.RainbowTooltip)
                {
                    tooltips.Add(new TooltipLine(Mod, "Endless", "[c/" + Main.DiscoColor.Hex3() + ":Endless]"));
                }
                else
                {
                    tooltips.Add(new TooltipLine(Mod, "Endless", "Endless"));
                }
                if (item.IsCurrency)
                {
                    tooltips.Add(new TooltipLine(Mod, "CoinEasterEgg", "[c/4f4f4f:No, not like that]"));
                }
                /*if (EndlessDeterminer.IsAmmo(item))
                {
                    tooltips.Add(new TooltipLine(Mod, "DebugAmmo", "Endless Ammo"));
                }
                if (EndlessDeterminer.IsConsumable(item))
                {
                    tooltips.Add(new TooltipLine(Mod, "DebugConsumable", "Endless Consumable"));
                }
                if (EndlessDeterminer.IsPotion(item))
                {
                    tooltips.Add(new TooltipLine(Mod, "DebugPotion", "Endless Potion"));
                }
                if (EndlessDeterminer.IsPlaceable(item))
                {
                    tooltips.Add(new TooltipLine(Mod, "DebugPlaceable", "Endless Placeable"));
                }*/
            }
        }

        public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            // called just before item count is rendered, this stops it showing 1 higher than it is
            if (oldStack != 0)
            {
                item.stack = oldStack;
                oldStack = 0;
            }
            
            if (cfg.RainbowSprite && ((Main.gameMenu && EndlessDeterminer.IsEndless(item)) || EndlessDeterminer.IsEndless(item, Main.player[Main.myPlayer]))) {
                Texture2D texture = TextureAssets.Item[item.type].Value;
                Rectangle sourceRect = Main.itemAnimations[item.type]?.GetFrame(texture) ?? texture.Frame();
                spriteBatch.Draw(texture, position, sourceRect, Main.DiscoColor, 0, origin, scale, SpriteEffects.None, 0);
                return false;
            }
            return true;
        }

        public override void UpdateInventory(Item item, Player player)
        {
            // if the item wasn't drawn in inventory
            if (oldStack != 0)
            {
                item.stack = oldStack;
                oldStack = 0;
            }

            if (item.buffType != 0 && EndlessDeterminer.IsEndless(item, player))
            {
                player.AddBuff(item.buffType, 2);
            }
        }

        public override bool InstancePerEntity => true;
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            EndlessItem g = (EndlessItem)base.Clone(item, itemClone);
            g.oldStack = 0;
            return g;
        }
    }
}
