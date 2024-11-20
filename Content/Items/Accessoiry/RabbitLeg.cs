using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items.Accessoiry
{
    public class RabbitLeg : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 10;
            Item.sellPrice(0, 0, 0, 50);
            Item.AllowReforgeForStackableItem = true;
            Item.value = Item.buyPrice(0, 1, 10, 8);
            Item.accessory = true;
            Item.rare = ItemRarityID.White;
            Item.crit = 4;
            Item.material = true;
            Item.legSlot = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            _ = CreateRecipe()
                .AddIngredient(ItemID.Bunny, 1)
                .AddTile(TileID.Tables)
                .Register();
            // только для тестов 

            _ = CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 1)
                .AddTile(TileID.Tables)
                .Register();
        }

        // эффект предмета увеличение скорости передвижения игрока на 20% за каждый предмет до 10 шт.
        // обнаружен баг, скорость не складывается со скоростью от обуви того же гермеса, но остальные свойства ботинок игра видит на ура.
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            float movementSpeedBoost = 0.20f * Item.stack;

            player.accRunSpeed += movementSpeedBoost;
            player.moveSpeed += movementSpeedBoost;
        }
    }
}
