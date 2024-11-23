using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items.BossAccessoiry
{
    public class FlowerOfConcentration : ModItem
    {
        public override void SetDefaults()
        {
            // прописывание аксессуара.
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.sellPrice(0, 6, 90, 72);
            Item.value = Item.buyPrice(0, 10, 15, 4);
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.crit = 4;
            Item.handOnSlot = 1;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed = 0;
            player.accRunSpeed = 0;
            float attackSpeedBonus = 1f; // 15% увеличения за каждый предмет до 5 шт.

            // добавление скорости атаки
            player.GetAttackSpeed(DamageClass.Generic) += attackSpeedBonus; // добавление к любому оружию
        }
    }
}
