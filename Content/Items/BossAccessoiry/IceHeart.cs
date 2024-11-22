using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items.BossAccessoiry
{
    public class IceHeart : ModItem
    {
        public override void SetDefaults()
        {
            // прописывание аксессуара.
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
            player.GetDamage(DamageClass.Generic) += 5;
            player.statLifeMax2 = 100;
            player.statDefense -= 30;
        }
    }
}
