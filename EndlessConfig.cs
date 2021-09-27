using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader.Config;

namespace EndlessEverything
{
    class EndlessConfigClient: ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(true)]
        //[Label("$Mods.EndlessEverything.Config.RainbowSprite.Label")]
        //[Tooltip("$Mods.EndlessEverything.Config.RainbowSprite.Tooltip")]
        [Label("Rainbow Sprites")]
        [Tooltip("Makes endless items get all disco-y")]
        public bool RainbowSprite { get; set; }

        [DefaultValue(true)]
        //[Label("$Mods.EndlessEverything.Config.RainbowTooltip.Label")]
        //[Tooltip("$Mods.EndlessEverything.Config.RainbowTooltip.Tooltip")]
        [Label("Rainbow Tooltips")]
        [Tooltip("Makess the \"Endless\" tooltip get all disco-y")]
        public bool RainbowTooltip { get; set; }
    }
    class EndlessConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(false)]
        //[Label("$Mods.EndlessEverything.Config.StartEnabled.Label")]
        //[Tooltip("$Mods.EndlessEverything.Config.StartEnabled.Tooltip")]
        [Label("Always Enabled")]
        [Tooltip("Items will become endless even if you don't use the Endless Enabler.\nWhether or not it's enabled through Endless Enabler will still be remembered in the background")]
        public bool StartEnabled { get; set; }

        [DefaultValue(false)]
        //[Label("$Mods.EndlessEverything.Config.UseResearch.Label")]
        //[Tooltip("$Mods.EndlessEverything.Config.UseResearch.Tooltip")]
        [Label("Use Research Counts")]
        [Tooltip("Will use each items research amount for the limit to become endless.\nAlways on if player is using journey difficulty\nDoesn't disable other ways of becoming endless.")]
        public bool UseResearch { get; set; }

        //[Label("$Mods.EndlessEverything.Config.Ammo.Label")]
        //[Tooltip("$Mods.EndlessEverything.Config.Ammo.Tooltip")]
        [Label("Ammo Limit")]
        [Tooltip("Stack size needed for ammo to become endless")]
        [DefaultValue(3996)]
        [Range(1, 9999)]
        public int AmmoStack;

        //[Label("$Mods.EndlessEverything.Config.Consumable.Label")]
        //[Tooltip("$Mods.EndlessEverything.Config.Consumable.Tooltip")]
        [Label("Consumable Limit")]
        [Tooltip("Stack size needed for consumable items to become endless")]
        [DefaultValue(3996)]
        [Range(1, 9999)]
        public int ConsumableStack;

        //[Label("$Mods.EndlessEverything.Config.Potion.Label")]
        //[Tooltip("$Mods.EndlessEverything.Config.Potion.Tooltip")]
        [Label("Potion Limit")]
        [Tooltip("Stack size needed for potions to become endless")]
        [DefaultValue(30)]
        [Range(1, 9999)]
        public int PotionStack;

        //[Label("$Mods.EndlessEverything.Config.OverrideEnable.Label")]
        //[Tooltip("$Mods.EndlessEverything.Config.OverrideEnable.Tooltip")]
        [Label("Override Enables")]
        [Tooltip("Define specific items and their thresholds to become endless")]
        public Dictionary<ItemDefinition, int> OverrideEnable { get; set; } = new Dictionary<ItemDefinition, int>()
        {
            [new ItemDefinition(ItemID.Torch)] = 396,
            [new ItemDefinition(ItemID.BeachBall)] = 1,
            [new ItemDefinition(ItemID.Wire)] = 100,
            [new ItemDefinition(ItemID.Actuator)] = 50
        };

        //[Label("$Mods.EndlessEverything.Config.OverrideDisable.Label")]
        //[Tooltip("$Mods.EndlessEverything.Config.OverrideDisable.Tooltip")]
        [Label("Override Disables")]
        [Tooltip("Define specific items to not become endless (unless researched)")]
        public List<ItemDefinition> OverrideDisable { get; set; } = new List<ItemDefinition>()
        {

        };
    }
}
