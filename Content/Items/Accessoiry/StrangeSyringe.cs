using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items.Accessoiry
{
    public class StrangeSyringe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 5;
            Item.sellPrice(0, 0, 0, 30);
            Item.AllowReforgeForStackableItem = true;
            Item.value = Item.buyPrice(0, 3, 20, 8);
            Item.accessory = true;
            Item.rare = ItemRarityID.White;
            Item.crit = 4;
            Item.material = true;
            Item.handOffSlot = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            _ = CreateRecipe()
                .AddIngredient(ItemID.BottledWater, 1)
                .AddIngredient(ItemID.Cactus, 5)
                .AddIngredient(ItemID.GlowingMushroom, 5)
                .AddIngredient(ItemID.Daybloom, 3)
                .AddTile(TileID.CookingPots)
                .Register();

            // only for Tests

            _ = CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
        // эффект предмета - увеличение скорости атаки любого оружия 
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // расчёт бонуса скорости атаки от стака предмета
            float attackSpeedBonus = 0.15f * Item.stack; // 15% увеличения за каждый предмет до 5 шт.

            // добавление скорости атаки
            player.GetAttackSpeed(DamageClass.Generic) += attackSpeedBonus; // добавление к любому оружию
        }

    }
}
