using Terraria.ModLoader;

namespace RiskOfTerraria.Content
{
    public class CustomModMenu : ModMenu
    {
        public override int Music => MusicLoader.GetMusicSlot(RiskOfTerraria.ModID, "Content/Music/CustomMainMenuRoR2");
    }
}
