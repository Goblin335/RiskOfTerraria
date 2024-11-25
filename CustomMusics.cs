﻿using RiskOfTerraria.Content.CustomBosses;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Debug = RiskOfTerraria.Content.Debug;

namespace RiskOfTerraria
{
    public class CustomMusics
    {
        public static void CustomMusicLoader(RiskOfTerraria ModID)
        {
            //
            // музыка для биомов и ивентов и прочего
            //
            Debug.WriteLine("Загрузка музыки");
            // Используйте AddMusic, передавая мод и путь к музыкальному файлу
            // Грибной биом
            MusicLoader.AddMusic(ModID, "Content/Music/CustomMushroomsRoR2"); // Путь без расширения
            // Главное меню
            MusicLoader.AddMusic(ModID, "Content/Music/CustomMainMenuRoR2");
            // Парящие острова
            MusicLoader.AddMusic(ModID, "Content/Music/CustomSkyFieldAndSpace");
            // Шторм
            MusicLoader.AddMusic(ModID, "Content/Music/CustomStormRoR2");
            //
            // музыка для боссов
            //
            // колосс
            MusicLoader.AddMusic(ModID, "Content/Music/CustomColossMusic");

            // ледяной страж
            MusicLoader.AddMusic(ModID, "Content/Music/IceGuardianFite");

            // тотем стрельбы
            MusicLoader.AddMusic(ModID, "Content/Music/TotemOfShooting");
        }

        public class PriorityBiomeHigh : ModSceneEffect
        {
            public override bool IsSceneEffectActive(Player player)
            {
                // Проверяем, активен ли грибной биом
                return player.ZoneGlowshroom; // Стандартное условие для грибного биома
            }

            public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

            public override int Music
            {
                get
                {
                    if (Main.LocalPlayer.ZoneGlowshroom)
                    {
                        return MusicLoader.GetMusicSlot(RiskOfTerraria.ModID, "Content/Music/CustomMushroomsRoR2");
                    }
                    // Указываем путь к нашему музыкальному файлу

                    return base.Music;
                }
            }
        }
        // музыка для биомов
        public class PriorityEnvironment : ModSceneEffect
        {
            public override bool IsSceneEffectActive(Player player)
            {
                // Проверяем, активен ли грибной биом
                return player.ZoneSkyHeight || player.ZoneRain; // Стандартное условие для грибного биома
            }

            public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

            public override int Music
            {
                get
                {
                    if (Main.LocalPlayer.ZoneSkyHeight)
                    {
                        return MusicLoader.GetMusicSlot(RiskOfTerraria.ModID, "Content/Music/CustomSkyFieldAndSpace");
                    }
                    else if (Main.LocalPlayer.ZoneRain)
                    {
                        return MusicLoader.GetMusicSlot(RiskOfTerraria.ModID, "Content/Music/CustomStormRoR2");
                    }
                    // Указываем путь к нашему музыкальному файлу


                    return base.Music;
                }
            }
        }
        // музыка для босса 
        public class PriorityCustomBossMusic : ModSceneEffect
        {
            public override bool IsSceneEffectActive(Player player)
            {
                return Main.npc.Any(n => n.type == ModContent.NPCType<Coloss>() && n.active || Main.npc.Any(n => n.type == ModContent.NPCType<IceGuardian>() && n.active) ||
                Main.npc.Any(n => n.type == ModContent.NPCType<TotemOfShooting>() && n.active));
            }

            public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;

            public override int Music
            {
                get
                {
                    if (Main.npc.Any(n => n.type == ModContent.NPCType<Coloss>() && n.active))
                    {
                        return MusicLoader.GetMusicSlot(RiskOfTerraria.ModID, "Content/Music/CustomColossMusic");
                    }
                    else if(Main.npc.Any(n => n.type == ModContent.NPCType<IceGuardian>() && n.active)) 
                    {
                        return MusicLoader.GetMusicSlot(RiskOfTerraria.ModID, "Content/Music/IceGuardianFite");
                    }
                    else if(Main.npc.Any(n => n.type == ModContent.NPCType<TotemOfShooting>() && n.active))
                    {
                        return MusicLoader.GetMusicSlot(RiskOfTerraria.ModID, "Content/Music/TotemOfShooting");
                    }
                    return base.Music;
                }
            }
        }


    }
}

