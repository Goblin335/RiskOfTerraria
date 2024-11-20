using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;


namespace RiskOfTerraria.Content.Items.Accessoiry
{
    public class monsterTeeth : ModItem
    {
        public override void SetDefaults()
        {
            // прописывание аксессуара.
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.sellPrice(0, 0, 0, 80);
            Item.AllowReforgeForStackableItem = true;
            Item.value = Item.buyPrice(0, 1, 10, 8);
            Item.accessory = true;
            Item.rare = ItemRarityID.White;
            Item.crit = 6;
            Item.material = true;
            Item.handOnSlot = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            _ = CreateRecipe()
                .AddIngredient(ItemID.AntlionMandible, 5)
                .AddTile(TileID.AlchemyTable)
                .Register();

            // исключительно для тестов
            recipe = CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
        // эффект предмета, при убийстве любого врага, выпадает ItemID.Heart.
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            
        }
    }
}
