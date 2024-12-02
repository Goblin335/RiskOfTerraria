using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items.BossAccessoiry
{
    public class StrangeCrysstal : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.sellPrice(0, 20, 15, 5);
            Item.value = Item.buyPrice(0, 40, 30, 25);
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.crit = 4;
            Item.bodySlot = 1;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 = 1000;
            player.GetAttackSpeed(DamageClass.Generic) = 0.3f;
            player.moveSpeed = 0.4f;
            player.GetDamage(DamageClass.Generic) += 3;
        }
    }
}
