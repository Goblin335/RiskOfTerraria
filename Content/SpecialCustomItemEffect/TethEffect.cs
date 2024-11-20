using RiskOfTerraria.Content.Items.BossAccessoiry;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.SpecialCustomItemEffect
{
    public class TethEffect : ModNPC
    {
        public override void OnKill()
        {
            //Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ItemID.Heart);
            ItemDropRule.Common(ItemID.Heart);
        } 
    }
}
