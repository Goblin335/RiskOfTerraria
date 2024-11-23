using Microsoft.Xna.Framework;
using RiskOfTerraria.Content.Items.Accessoiry;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace RiskOfTerraria.Content.Items.WeaponMelee
{
    public class СlawsОfPredator : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(0, 8, 30, 12);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.crit = 20;
            Item.maxStack = 1;
            Item.useTurn = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            _ = CreateRecipe()
                .AddIngredient(ItemID.RottenChunk, 15)
                .AddIngredient(ItemID.Sunflower, 2)
                .AddIngredient(ModContent.ItemType<monsterTeeth>(), 1)
                .AddIngredient(ItemID.DaybloomSeeds, 5)
                .AddTile(TileID.AlchemyTable)
                .Register();


            _ = CreateRecipe()
                .AddIngredient(ItemID.Vertebrae, 15) // альтернатива
                .AddIngredient(ItemID.Sunflower, 2)
                .AddIngredient(ModContent.ItemType<monsterTeeth>(), 1)
                .AddIngredient(ItemID.DaybloomSeeds, 5)
                .AddTile(TileID.AlchemyTable)
                .Register();


            // исключительно для тестов
            _ = CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            player.Heal(10);
        }
    }
}
