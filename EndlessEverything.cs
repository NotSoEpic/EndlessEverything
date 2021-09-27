using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EndlessEverything
{
	public class EndlessEverything : Mod
	{
        public override void AddRecipeGroups()
        {
            RecipeGroup silverBullet = new(
                () => "Any Silver Bullet",
                new int[] {
                    ItemID.SilverBullet,
                    ItemID.TungstenBullet
                }
            );
            RecipeGroup.RegisterGroup("SilverBullet", silverBullet);
        }
    }
}