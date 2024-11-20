using Terraria;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items
{

    public class PlayerModding : ModPlayer
    {

        public static bool clearPrecentDamageCheck;
        public static double clearPrecentDamage;
        public static int num = 0;


        // Индексы 3–9 содержат аксессуары, а индексы 13–19 — социальные аксессуары.
        public Item[] armor = new Item[20];

        public override void PostUpdate()
        {
            // Получаем список всех активных баффов игрока
            for (int i = 0; i < Player.buffType.Length; i++)
            {
                // Проверяем, если бафф активен
                if (Player.buffType[i] > 0)
                {
                    int buffID = Player.buffType[i]; // ID баффа

                    if (buffID == 355)
                    {
                        clearPrecentDamageCheck = true;
                        break;
                    }
                    else
                    {
                        clearPrecentDamageCheck = false;
                    }
                }
            }
        }
        // мне похуй кто это читает, но я скажу, в рот ебал выходить за рамки и делать стаканье аксессуаров в слотах для них. А это их метод дааа.
        public override void UpdateEquips()
        {
            // Кликаем
            if (Main.mouseLeftRelease && Main.mouseLeft)
            {
                // Получаем предмет хранящийся в мышке
                ref Item cursorItem = ref Main.mouseItem;
                // Получаем объект игрока
                Player player = Main.LocalPlayer;

                for (int i = 3; i < 20; i++)
                {
                    // Ссылка на слоты брони
                    ref Item slotItem = ref player.armor[i];

                    if (slotItem.type == cursorItem.type && slotItem.maxStack >= cursorItem.stack + slotItem.stack)
                    {
                        slotItem.stack += cursorItem.stack;
                        cursorItem.TurnToAir();
                    }
                }
            }
        }

        // sharp dagger эффект
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (clearPrecentDamageCheck)
            {
                double hit = target.life;
                double t1 = hit / 100;
                double t2 = t1 * clearPrecentDamage;
                double dmg = t2;
                //Main.NewText($"NPC Life: {hit}");
                //Main.NewText($"{hit} / 100 = {t1}, {t1} * precentDamage[{clearPrecentDamage}] = {t2}");
                //Main.NewText($"Calc dmg (Double): {dmg}");
                //Main.NewText($"Calc dmg (int): {(int)dmg}");
                DealDamageToNPC(target, (int)dmg, 0, false);
            }
        }

        // sharp dagger эффект
        // Метод для нанесения урона по NPC с использованием HitInfo
        public void DealDamageToNPC(NPC target, int damage, float knockback, bool crit)
        {
            // Создание объекта HitInfo
            NPC.HitInfo hitInfo = new NPC.HitInfo
            {
                Damage = damage,      // Урон
                Knockback = knockback, // Отбрасывание
                Crit = crit            // Критический удар
            };

            // Проверка, существует ли NPC
            if (target != null && target.active)
            {
                // Наносим урон NPC через StrikeNPC с использованием HitInfo
                target.StrikeNPC(hitInfo);
            }
        }
    }

}
