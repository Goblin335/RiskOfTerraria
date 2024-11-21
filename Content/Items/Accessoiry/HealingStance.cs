using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items.Accessoiry
{
    public class HealingStance : ModItem
    {
        public override void SetDefaults()
        {
            // прописывание аксессуара.
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.sellPrice(0, 15, 80, 10);
            Item.AllowReforgeForStackableItem = true;
            Item.value = Item.buyPrice(0, 30, 11, 5);
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.crit = 4;
            Item.material = true;
            Item.headSlot = 1;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            _ = CreateRecipe()
                .AddIngredient(ItemID.LifeCrystal, 4)
                .AddIngredient(ItemID.Daybloom, 5)
                .AddIngredient(ItemID.Mushroom, 5)
                .AddTile(TileID.AlchemyTable)
                .Register();

            // исключительно для тестов
            _ = CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
        // эффект регенерация умноженная на 2 
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegenCount *= 2;
        }
    }
}
