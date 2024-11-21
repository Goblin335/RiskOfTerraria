using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items.Accessoiry
{
    public class ArmorPlate : ModItem
    {
        public override void SetDefaults()
        {
            // прописывание аксессуара.
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 10;
            Item.sellPrice(0, 1, 5, 80);
            Item.AllowReforgeForStackableItem = true;
            Item.value = Item.buyPrice(0, 1, 0, 8);
            Item.accessory = true;
            Item.rare = ItemRarityID.White;
            Item.crit = 4;
            Item.material = false;
            Item.legSlot = 1;

        }
        public override void AddRecipes()
        {
            

            // рецепт из 2 меди, 5 верёвки, 2 золота
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.CopperBar, 5)
                .AddIngredient(ItemID.Rope, 5)
                .AddIngredient(ItemID.GoldBar, 2)
                .AddTile(TileID.Anvils)
                .Register();

            // платина в альтернативу золота
            _ = CreateRecipe()
                .AddIngredient(ItemID.CopperBar, 5)
                .AddIngredient(ItemID.Rope, 5)
                .AddIngredient(ItemID.PlatinumBar, 2)
                .AddTile(TileID.Anvils)
                .Register();

            // олово в альтернативу меди и тоже самое с золотом и платиной 
            _ = CreateRecipe()
                .AddIngredient(ItemID.TinBar, 5)
                .AddIngredient(ItemID.Rope, 5)
                .AddIngredient(ItemID.GoldBar, 2)
                .AddTile(TileID.Anvils)
                .Register();

            _ = CreateRecipe()
                .AddIngredient(ItemID.TinBar, 5)
                .AddIngredient(ItemID.Rope, 5)
                .AddIngredient(ItemID.PlatinumBar, 2)
                .AddTile(TileID.Anvils)
                .Register();

            // для тестов 

            _ = CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        // эффект повышение количества брони на 5 ед. За каждый предмет в стаке. 
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 5 * Item.stack;
        }

    }
}
