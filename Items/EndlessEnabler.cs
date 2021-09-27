using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace EndlessEverything.Items
{
	public class EndlessEnabler : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Toggles endless items.");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() 
		{
			Item.width = 22;
			Item.height = 22;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
			Item.expert = true;
			Item.UseSound = SoundID.Item4;
		}

		public override void AddRecipes() 
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.EndlessQuiver, 1);
			recipe.AddIngredient(ItemID.EndlessMusketPouch, 1);
			recipe.AddIngredient(ItemID.JestersArrow, 99);
			recipe.AddIngredient(ItemID.UnholyArrow, 99);
			recipe.AddRecipeGroup("SilverBullet", 99);
			recipe.AddIngredient(ItemID.MeteorShot, 99);
			recipe.AddTile(TileID.CrystalBall);
			recipe.Register();
		}

        public override bool? UseItem(Player player)
        {
			EndlessPlayer p = player.GetModPlayer<EndlessPlayer>();
			p.endless = !p.endless;
			return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			Player p = Main.player[Main.myPlayer];
			string t;
            if (p.GetModPlayer<EndlessPlayer>().endless)
            {
				t = "Currently [c/" + Main.DiscoColor.Hex3() + ":enabled]";
            } else
            {
				t = "Currently disabled";
            }
			tooltips.Add(new TooltipLine(Mod, "EndlessState", t));

		}
    }
}