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
    public class IceGuardian : ModNPC
    {
        private const float MoveSpeed = 6f; // Скорость передвижения босса
        private const float RotateSpeed = 0.06f; // Скорость вращения
        private const int ShotsPerVolley = 5; // Количество снарядов в одной очереди
        private const int VolleyDelay = 120; // Задержка между очередями
        private float inertia = 30f; // Инерция движения босса
        private int volleyCounter = 0; // Счетчик очередей
        private bool isAttacking = false; // Флаг, показывающий, атакует ли босс в данный момент
        public override void SetDefaults()
        {
            NPC.boss = true;
            NPC.width = 100;
            NPC.height = 100;
            NPC.aiStyle = -1;
            NPC.damage = 50;
            NPC.defense = 20;
            NPC.lifeMax = 7501;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = true;
            NPC.DeathSound = SoundID.Shimmer1;
            NPC.HitSound = SoundID.NPCHit20;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            

        }


        private bool tackling = false; // Флаг атаки тараном

        public override void AI()
        {
            NPC.rotation += MathHelper.ToRadians(1f);

            if (!NPC.HasValidTarget) // Если игрока нет на виду, просто стоим на месте
            {
                NPC.TargetClosest(); // Находим игрока
                return;
            }

            Player player = Main.player[NPC.target];

            // Направление движения к игроку
            Vector2 targetDirection = player.Center - NPC.Center;
            targetDirection.Normalize(); // Нормализуем вектор направления

            // Плавный поворот к игроку
            NPC.rotation = MathHelper.Lerp(NPC.rotation, (float)Math.Atan2(targetDirection.Y, targetDirection.X), 0.1f);

            if (!isAttacking)
            {
                // Изменение позиции босса с учетом инерции движения
                NPC.velocity = (NPC.velocity * (inertia - 1) + targetDirection * MoveSpeed) / inertia;

                // Логика тарана
                if (NPC.Distance(player.Center) < 300f && !tackling) // Если близко к игроку и не тараним
                {
                    tackling = true; // Устанавливаем флаг тарана
                    NPC.velocity = targetDirection * (MoveSpeed * 1.5f); // Увеличиваем скорость
                                                                         // Добавляем немного инерции для дальнейшего движения
                }

                // Когда босс достаточно близко к игроку, сворачиваем, чтобы избежать резких столкновений
                if (NPC.Distance(player.Center) < 50f)
                {
                    tackling = false; // Сбрасываем флаг тарана
                    NPC.velocity *= 0.5f; // Уменьшаем скорость, чтобы остановиться
                }
            }
            else
            {
                // Устанавливаем новое направление движения босса после завершения очереди стрельбы
                NPC.velocity = targetDirection * 0.1f;
            }

            // Стрельба по игроку
            if (volleyCounter <= 0)
            {
                isAttacking = true;

                for (int i = 0; i < ShotsPerVolley; i++)
                {
                    Vector2 shotDirection = targetDirection.RotatedByRandom(MathHelper.ToRadians(10));
                    shotDirection.Normalize(); // Нормализуем вектор направления
                    shotDirection *= 10f;

                    int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shotDirection, ProjectileID.FrostBoltSword, 20, 0f);
                    Main.projectile[proj].hostile = true;
                    Main.projectile[proj].friendly = false;
                }

                volleyCounter = VolleyDelay;
            }
            else
            {
                isAttacking = false;
                volleyCounter--;
            }

            // Обновляем скорость босса, даже если он атакует
            NPC.velocity = targetDirection * MoveSpeed; // Поддержка скорости в сторону игрока

        }




        public override void ModifyTypeName(ref string typeName)
        {
            typeName = "Ise Guardian";
        }

        public override void SetStaticDefaults()
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Main.npcFrameCount[NPC.type] = 1; // Указываем, что у нас 4 кадра анимации
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            int frameHeight = texture.Height / Main.npcFrameCount[NPC.type];
            Rectangle sourceRectangle = new Rectangle(0, NPC.frame.Y, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(texture, NPC.Center - screenPos, sourceRectangle, drawColor, NPC.rotation, origin, NPC.scale, effects, 0f);

            return false;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceHeart>()));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;

        }

    }


    // предмет спавна босса 
    public class IceGuardianSpawnItem : ModItem
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
                .AddIngredient(ItemID.IceBlock, 100)
                .AddIngredient(ItemID.SnowBlock, 100)
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
            return !Main.npc.Any(n => n.type == ModContent.NPCType<IceGuardian>() && n.active);
        }
        // Призыв возможен только в снежном биоме 
        public override bool? UseItem(Player player)
        {
            // Проверяем, в сложном ли режиме находится игра
            if (Main.hardMode && player.ZoneSnow)
            {
                // Призыв босса
                NPC.SpawnBoss((int)player.Center.X - 250, (int)player.Center.Y, ModContent.NPCType<IceGuardian>(), player.whoAmI);
                return true;
            }
            return false;
        }

        
        
    }
}
