using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace RiskOfTerraria.Content.CustomBosses
{
    public class LargeBoulder : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.friendly = false;
            Projectile.hostile = true; // Делаем снаряд враждебным для игрока
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true; // Камень должен сталкиваться с блоками
            Projectile.penetrate = 1; // Камень исчезает после первого столкновения
            Projectile.alpha = 0; // Делаем камень видимым
            Projectile.timeLeft = 600; // Камень исчезнет через 10 секунд, если не столкнется
            Projectile.aiStyle = 1; // Используем стандартный стиль движения для метательных предметов
            AIType = ProjectileID.Bullet;


        }

        public override void Load()
        {
            // Эта строка автоматически загрузит текстуру из файла
            // YourMod/Content/CustomBosses/LargeBoulder.png
            if (!Main.dedServ) // Проверка, что это не выделенный сервер
            {
                Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            }
        }
        public override void AI()
        {
            // Добавляем вращение камня
            Projectile.rotation += 0.1f * Projectile.direction;

            // Создаем эффект пыли при движении
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
            }
        }

        [System.Obsolete]
        public override void Kill(int timeLeft)
        {
            // Эффект при уничтожении камня
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
            }

        }


    }

}
