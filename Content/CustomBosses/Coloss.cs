
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RiskOfTerraria.Content.Items;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.CustomBosses
{
    public class Coloss : ModNPC
    {
        private Vector2 _targetPosition;
        private float _moveSpeed = 2f;
        private int attackCooldown = 0;
        private bool _shouldJump = false;

        public override void SetDefaults()
        {
            NPC.boss = true;
            NPC.width = 50;
            NPC.height = 150;
            NPC.aiStyle = 3;
            NPC.damage = 40;
            NPC.defense = 20;
            NPC.lifeMax = 4000;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = false;
            NPC.DeathSound = SoundID.DD2_SkeletonDeath;
            NPC.HitSound = SoundID.NPCHit1;

        }
        // имя босса
        public override void ModifyTypeName(ref string typeName)
        {
            typeName = "Stone Titan";
        }
        // текстуры
        public override void Load()
        {
            if (!Main.dedServ)
            {
                Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            }
        }
        private int _attackCooldown = 0;
        private int _attackCooldownMax = 200; // 200 тиков = 3.33 секунды

        // ии босса
        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];

            // Перемещение босса
            Vector2 direction = player.Center - NPC.Center;
            direction.Normalize();
            NPC.velocity = direction * _moveSpeed;

            // Проверка препятствий
            if (NPC.collideX)
            {
                _shouldJump = true;
            }

            // Атака
            _attackCooldown--;
            if (_attackCooldown <= 0)
            {
                ThrowingAStone();
                _attackCooldown = _attackCooldownMax;
            }

            // Прыжок
            if (_shouldJump)
            {
                NPC.velocity.Y = -10f; // Сила прыжка
                _shouldJump = false;
            }
        }

        // навык босса
        public void ThrowingAStone()
        {
            Player player = Main.player[NPC.target];
            Vector2 direction = player.position - NPC.position;
            direction.Normalize();
            direction *= 10f;
            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position, direction, ModContent.ProjectileType<LargeBoulder>(), 30, 0f, Main.myPlayer);
        }

        // отрисовка босса 
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 15) // Меняем кадр каждые 10 тиков
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight; // Переходим к следующему кадру
                if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
                {
                    NPC.frame.Y = 0; // Возвращаемся к первому кадру
                }
            }
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4; // Указываем, что у нас 4 кадра анимации
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
        // дропчик с босса 

        public override void ModifyNPCLoot(NPCLoot npcLoot) 
        {
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LivingCore>()));
		}

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;

        }

    }

    

   
    // предмет спавна босса 
    public class ColossSpawnItem : ModItem
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
                .AddIngredient(ItemID.StoneBlock, 100)
                .AddIngredient(ItemID.DirtBlock, 100)
                .AddIngredient(ItemID.JungleSpores, 5)
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
            return !Main.npc.Any(n => n.type == ModContent.NPCType<Coloss>() && n.active);
        }

        public override bool? UseItem(Player player)
        {
            
            NPC.SpawnBoss((int)player.Center.X - 200, (int)player.Center.Y - 200, ModContent.NPCType<Coloss>(), player.whoAmI);
            return true;
        }
    }
}