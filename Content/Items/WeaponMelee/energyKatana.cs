using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items.WeaponMelee
{
    public class energyKatana : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(0, 5, 30, 12);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.AllowReforgeForStackableItem = true;
            Item.crit = 20;
            Item.material = true;
            Item.maxStack = 1;
            Item.material = true;
            Item.useTurn = true;
        }
        public override void AddRecipes()
        {
            _ = CreateRecipe();

            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.Diamond, 5)
                .AddIngredient(ItemID.Sapphire, 5)
                .AddIngredient(ItemID.MeteoriteBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
            // исключительно для тестов
            //recipe = CreateRecipe()
            //    .AddIngredient(ItemID.DirtBlock, 5)
            //    .AddTile(TileID.WorkBenches)
            //    .Register();
        }

        public override void HoldItem(Player player)
        {
            // за 1 телепорт нужна 1 упавшая звезда, проблема с телепортом сквозь блоки и в них. 
            int fallenStarItemId = ItemID.FallenStar;
            int fallenStarCount = 0;
            // цикл перебора инвентаря
            for (int i = 0; i < player.inventory.Length; i++)
            {
                if (player.inventory[i].type == fallenStarItemId)
                {
                    fallenStarCount += player.inventory[i].stack;
                }
            }
            // проверка на звёзды и нажатие ПКМ. 
            if (fallenStarCount > 0 && Main.mouseRightRelease && Main.mouseRight)
            {
                fallenStarCount--;
                for (int i = 0; i < player.inventory.Length; i++)
                {
                    if (player.inventory[i].type == fallenStarItemId)
                    {
                        // Удаляем одну звезду
                        player.inventory[i].stack--;

                        // Если количество стало равно 0, то убираем предмет из инвентаря
                        if (player.inventory[i].stack <= 0)
                        {
                            player.inventory[i].TurnToAir();
                        }
                        KatanaDash();
                        Main.NewText($"Количество оставшихся звёзд: {fallenStarCount}");
                    }
                }
            }
        }
        // модификация энергетической катаны, что при нажатии на ПКМ телепортирует игрока на N блоков в сторону в завистимости от расположения мышки на экране

        public void KatanaDash()
        {

            Player player = Main.LocalPlayer;

            // Проверка, что выбранный предмет - это energyKatana
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<energyKatana>())
            {
                // Получаем направление мыши
                Vector2 mousePosition = Main.MouseWorld;
                Vector2 playerPosition = player.position;
                Vector2 direction = mousePosition - playerPosition;
                direction.Normalize(); // Нормализуем вектор направления

                // Устанавливаем смещение на 10 блоков в указанном направлении
                float dashDistance = 200f; // Можно изменить, если нужно
                player.position += direction * dashDistance;

                // Можете добавить эффект, анимацию или звук здесь
                // Например, если вы хотите добавить звук:
                // Main.PlaySound(SoundID.Item1);

                // Предотвращение дальнейшего использования, если необходимо
                // player.releaseRightClick = true;
            }
        }
    }
}
