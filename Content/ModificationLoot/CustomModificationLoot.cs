using RiskOfTerraria.Content.Items.Accessoiry;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.ModificationLoot
{
    public class CustomModificationLoot : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            foreach (Player player in Main.player)
            {
                if (player.active && !player.dead && npc.Distance(player.Center) <= 1000f)
                {
                    if (IsAccessoryEquipped(player))
                    {
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Heart);
                    }
                }
            }
        }

        private bool IsAccessoryEquipped(Player player)
        {
            int accessoryType = ModContent.ItemType<monsterTeeth>();

            for (int i = 3; i < 10; i++)
                if (player.armor[i].type == accessoryType)
                    return true;

            return false;
        }
    }
}
