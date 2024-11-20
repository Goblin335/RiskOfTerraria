using RiskOfTerraria.Content;
using System;
using Terraria;

using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items
{
    public class LivingCore : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.sellPrice(0, 8, 30, 20);
            Item.AllowReforgeForStackableItem = true;
            Item.value = Item.buyPrice(0, 1, 10, 8);
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.crit = 4;
            Item.material = false;
            Item.bodySlot = 0;
        }
        // повышает максимальное хп на 150, регенерацию на 20, защиту на 10 ед.
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 150;
            player.lifeRegen += 20;
            player.statDefense += 10;
        }
    }
}
