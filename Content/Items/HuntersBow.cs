using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items
{
    public class HuntersBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 10, 0, 12);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.AllowReforgeForStackableItem = true;
            Item.crit = 20;
            Item.material = false;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.shoot = ProjectileID.JestersArrow;
            Item.shootSpeed = 20;
        }

        public override void AddRecipes()
        {
            _ = CreateRecipe();
            // рецепт лука охотницы на мир с порчей
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.Bone, 25)
                .AddIngredient(ItemID.RottenChunk, 15)
                .AddIngredient(ItemID.Gel, 30)
                .AddIngredient(ItemID.Bunny, 1)
                .AddTile(TileID.Anvils)
                .Register();
            // рецепт лука охотницы для мира с кризоном

            _ = CreateRecipe()
                .AddIngredient(ItemID.Bone, 25)
                .AddIngredient(ItemID.Vertebrae, 15)
                .AddIngredient(ItemID.Gel, 30)
                .AddIngredient(ItemID.Bunny, 1)
                .AddTile(TileID.Anvils)
                .Register();
            // исключительно для тестов
            _ = CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 5)
                .AddTile(TileID.WorkBenches)
                .Register();

        }
        // при выстреле любой стрелой, она должна самонаводится на врага, стрелы использутся из самой игры, новое придумывать не нужно.

        private int shootCooldown = 0; // таймер для контроля времени между выстрелами
        private const int shootRate = 15; // время (в кадрах) между выстрелами (примерно 20 кадров = 1/3 секунды)

        public override void HoldItem(Player player)
        {
            
            if (Main.mouseLeft)
            {
                shootCooldown++; // увеличиваем таймер выстрелов

                if (shootCooldown >= shootRate) // если прошедшее время больше или равно задержке между выстрелами
                {
                    // получить врага, к которому будет направлена стрела
                    NPC target = GetClosestEnemy(player);
                    if (target != null)
                    {
                        // задаем параметры самонаведения, можно настроить далее
                        Vector2 direction = (target.Center - player.Center);
                        direction.Normalize();

                        // создаем новый снаряд
                        Item.useStyle = ItemUseStyleID.Shoot;
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction * (Item.shootSpeed * 20), Item.shoot, Item.damage, Item.knockBack, player.whoAmI);

                        shootCooldown = 0; // сбрасываем таймер после выстрела
                    }
                }
            }
        }


        private NPC GetClosestEnemy(Player player)
        {
            NPC closestNPC = null;
            float closestDistance = float.MaxValue;

            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && !npc.immortal)
                {
                    float distance = Vector2.Distance(player.Center, npc.Center);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestNPC = npc;
                    }
                }
            }

            return closestNPC;
        }
    }
}
