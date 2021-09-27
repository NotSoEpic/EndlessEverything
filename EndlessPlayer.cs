using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace EndlessEverything
{
    class EndlessPlayer : ModPlayer
    {
        public bool endless;

        public override void SaveData(TagCompound tag)
        {
            tag.Set("endless", endless);
        }

        public override void LoadData(TagCompound tag)
        {
            endless = tag.GetBool("endless");
        }

        public override void PreUpdateBuffs()
        {
            foreach (Item item in Player.bank.item)
            {
                EndlessPotion(item, Player);
            }
            foreach (Item item in Player.bank2.item)
            {
                EndlessPotion(item, Player);
            }
            foreach (Item item in Player.bank3.item)
            {
                EndlessPotion(item, Player);
            }
            foreach (Item item in Player.bank4.item)
            {
                EndlessPotion(item, Player);
            }
        }

        private void EndlessPotion(Item item, Player player)
        {
            if (item.buffType != 0 && EndlessDeterminer.IsEndless(item, player))
            {
                player.AddBuff(item.buffType, 2);
            }
        }
    }
}
