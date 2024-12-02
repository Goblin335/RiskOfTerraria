using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace RiskOfTerraria.Content.Items.SpecialCraftItem
{
    public class RainSegments : ModItem
    {
        public override void SetDefaults()
        {
            // прописывание аксессуара.
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.sellPrice(0, 0, 0, 1);
            Item.value = Item.buyPrice(0, 0, 0, 10);
            Item.rare = ItemRarityID.White;
            Item.material = true;
        }
    }
}
