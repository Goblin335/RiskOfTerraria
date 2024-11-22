using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items.Accessoiry
{
    
    public class ScytheOfLife : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.sellPrice(0, 0, 90, 15);
            Item.AllowReforgeForStackableItem = true;
            Item.value = Item.buyPrice(0, 30, 11, 5);
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.crit = 4;
            Item.headSlot = -2;
            
        }
    
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.LesserHealingPotion, 5)
                  .AddIngredient(ItemID.Wood, 15)
                  .AddIngredient(ItemID.SilverBar, 2)
                  .Register();

            // альтернатива серебра, вольфрам
            recipe = CreateRecipe(); // Создаем новый рецепт для второй версии
            recipe.AddIngredient(ItemID.LesserHealingPotion, 5)
                  .AddIngredient(ItemID.Wood, 15)
                  .AddIngredient(ItemID.TungstenBar, 2)
                  .Register();

            // исключительно для тестов
            _ = CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<YourModPlayer>().HasLifeScythe = true;
        }
    }

    public class YourModPlayer : ModPlayer
    {
        public bool HasLifeScythe;

        public override void ResetEffects()
        {
            HasLifeScythe = false;
        }

        // Переопределяем метод OnHitNPC
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (HasLifeScythe && hit.Crit) // Проверяем критический удар
            {      
                Main.LocalPlayer.Heal(6);
            }
        }
    }
}
