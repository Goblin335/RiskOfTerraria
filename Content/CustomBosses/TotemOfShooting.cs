using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RiskOfTerraria.Content.Items.BossAccessoiry;
using System;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.CustomBosses
{
    public class TotemOfShooting : ModNPC
    {
        private int shotTimer = 0;
        public override void SetDefaults()
        {
            NPC.boss = true;
            NPC.width = 50;
            NPC.height = 200;
            NPC.aiStyle = -1;
            NPC.damage = 100;
            NPC.defense = 40;
            NPC.lifeMax = 6000;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = true;
            NPC.DeathSound = SoundID.Shimmer2;
            NPC.HitSound = SoundID.NPCHit20;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
        }

        public override void AI()
        {
            // Логика для стрельбы
            shotTimer++;
            if (shotTimer >= 30) // 1 раз в секунду
            {
                Shoot();
                shotTimer = 0; // Сбрасываем таймер
            }
        }

        private void Shoot()
        {
            // Генерация случайного угла
            float randomAngle = Main.rand.NextFloat(0f, MathHelper.TwoPi);

            // Определяем направление снаряда
            Vector2 shootDirection = new Vector2((float)Math.Cos(randomAngle), (float)Math.Sin(randomAngle));

            // Умножаем направление на скорость снаряда
            float projectileSpeed = 15f; // Измените скорость по желанию
            shootDirection *= projectileSpeed;

            // Определяем начальную позицию немного дальше от центра босса
            Vector2 spawnPosition = NPC.Center + shootDirection * 10f; // 1f - можно изменить по желанию для увеличения расстояния

            // Создание снаряда
            Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnPosition, shootDirection, ProjectileID.BouncyBoulder, NPC.damage, 0f);
        }


        public override void ModifyTypeName(ref string typeName)
        {
            typeName = "Totem of shooting";
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FlowerOfConcentration>()));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;

        }
    }

    public class TotemOfShootingSpawnItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 20;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 100)
                .AddIngredient(ItemID.GlowingMushroom, 5)
                .AddIngredient(ItemID.Sunflower, 1)
                .AddTile(TileID.AlchemyTable)
                .Register();

            // only for tests 
            recipe = CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.npc.Any(n => n.type == ModContent.NPCType<TotemOfShooting>() && n.active);
        }

        public override bool? UseItem(Player player)
        {

            NPC.SpawnBoss((int)player.Center.X, (int)player.Center.Y - 300, ModContent.NPCType<TotemOfShooting>(), player.whoAmI);
            return true;
        }
    }
}
