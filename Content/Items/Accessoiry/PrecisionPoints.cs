using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items.Accessoiry
{
    public class PrecisionPoints : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 6;
            Item.sellPrice(0, 0, 50, 80);
            Item.AllowReforgeForStackableItem = true;
            Item.value = Item.buyPrice(0, 0, 30, 8);
            Item.accessory = true;
            Item.rare = ItemRarityID.White;
            Item.crit = 4;
            Item.material = false;
            Item.faceSlot = 0;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            _ = CreateRecipe()
                .AddIngredient(ItemID.Lens, 2)
                .AddIngredient(ItemID.Fireblossom, 2)
                .AddTile(TileID.Tables)
                .Register();

            // для тестов 

            _ = CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += 10 * Item.stack;
        }
    }
}
