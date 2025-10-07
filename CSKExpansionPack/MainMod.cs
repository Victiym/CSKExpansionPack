using ExpansionPack;
using HarmonyLib;
using Il2Cpp;
using Il2CppDissolveExample;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem.IO;
using MelonLoader;
using MyCustomRole;
using System;
using UnityEngine;
using static Il2Cpp.Interop;
using static Il2CppSystem.Array;

[assembly: MelonInfo(typeof(MainMod), "CSK's Expansion Pack", "1.2", "CharaDr33murr")]
[assembly: MelonGame("UmiArt", "Demon Bluff")]

namespace ExpansionPack;
public class MainMod : MelonMod
{
    public static Sprite[] allSprites = Array.Empty<Sprite>();
    public override void OnInitializeMelon()
    {
        ClassInjector.RegisterTypeInIl2Cpp<Cleric>();
        ClassInjector.RegisterTypeInIl2Cpp<Assassin>();
        ClassInjector.RegisterTypeInIl2Cpp<Atheist>();
        ClassInjector.RegisterTypeInIl2Cpp<Sapper>();
    }
    public override void OnLateInitializeMelon()
    {
        var loadedAllSprites = Resources.FindObjectsOfTypeAll(Il2CppSystem.Type.GetTypeFromHandle(RuntimeReflectionHelper.GetRuntimeTypeHandle<Sprite>()));
        allSprites = new Sprite[loadedAllSprites.Length];
        for (int i = 0; i < loadedAllSprites.Length; i++)
        {
            allSprites[i] = loadedAllSprites[i]!.Cast<Sprite>();
        }

        CharacterData cleric = new CharacterData();
        cleric.role = new Cleric();
        cleric.name = "Cleric";
        cleric.description = "Heal 3 HP.\nIf Evil or Corrupted, take 3 damage instead.";
        cleric.flavorText = "\"Once tried to heal the Confessor's headache. She's been having migraines ever since.\"";
        cleric.hints = "";
        cleric.ifLies = "";
        cleric.picking = true;
        cleric.startingAlignment = EAlignment.Good;
        cleric.type = ECharacterType.Villager;
        cleric.abilityUsage = EAbilityUsage.Once;
        cleric.bluffable = true;
        cleric.characterId = "Cleric_EP";
        cleric.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        cleric.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        cleric.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        cleric.color = new Color(1f, 0.935f, 0.7302f);
        cleric.art_cute = getSprite("saint02a");
        cleric.backgroundArt = getSprite("good");

        CharacterData assassin = new CharacterData();
        assassin.role = new Assassin();
        assassin.name = "Assassin";
        assassin.description = "Pick 1 Target. It is Executed without dealing any damage.\n\nIf I am Evil or Corrupted, instead take normal damage + 2 HP.\n\nIf I kill an Evil, I will always deal no damage.";
        assassin.flavorText = "\"Blinded by his hatred for Evils and really good at excusing his mistakes.\"";
        assassin.hints = "Bombardier's loss is not related to your HP.\r\n\nI can never kill a Truthful Knight.";
        assassin.ifLies = "";
        assassin.picking = true;
        assassin.startingAlignment = EAlignment.Good;
        assassin.type = ECharacterType.Villager;
        assassin.abilityUsage = EAbilityUsage.Once;
        assassin.bluffable = true;
        assassin.characterId = "Assassin_EP";
        assassin.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        assassin.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        assassin.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        assassin.color = new Color(1f, 0.935f, 0.7302f);
        assassin.backgroundArt = getSprite("good");

        CharacterData atheist = new CharacterData();
        atheist.role = new Atheist();
        atheist.name = "Atheist";
        atheist.description = "One fake Minion and one fake Demon are added to the Deck View.";
        atheist.flavorText = "\"Never before have Demons ever seen someone so passionately advocate for their non-existence. Makes it way easier to infiltrate the village.\"";
        atheist.hints = "";
        atheist.ifLies = "";
        atheist.picking = false;
        atheist.startingAlignment = EAlignment.Good;
        atheist.type = ECharacterType.Outcast;
        atheist.bluffable = true;
        atheist.characterId = "Atheist_EP";
        atheist.artBgColor = new Color(0.3679f, 0.2014f, 0.1541f);
        atheist.cardBgColor = new Color(0.102f, 0.0667f, 0.0392f);
        atheist.cardBorderColor = new Color(0.7843f, 0.6471f, 0f);
        atheist.color = new Color(0.9659f, 1f, 0.4472f);
        atheist.backgroundArt = getSprite("good");
        Characters.Instance.startGameActOrder = insertAfterAct("Alchemist", atheist);

        CharacterData belias = new CharacterData();
        belias.role = new Belias();
        belias.name = "Belias";
        belias.description = "At Game Start, I Convert one Good Villager. They become Evil and Lie.\n\n(Massive thanks to Victim for helping with the character!)";
        belias.flavorText = "\"My decisions are final. Your beliefs are absolute. Your will is mine.\"";
        belias.hints = "Converted is still considered a Villager.";
        belias.ifLies = "";
        belias.notes = "";
        belias.picking = false;
        belias.startingAlignment = EAlignment.Evil;
        belias.type = ECharacterType.Demon;
        belias.abilityUsage = EAbilityUsage.Once;
        belias.bluffable = false;
        belias.characterId = "Belias_EP";
        belias.artBgColor = new Color(1f, 0f, 0f);
        belias.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        belias.cardBorderColor = new Color(0.8208f, 0f, 0.0241f);
        belias.color = new Color(1f, 0.3811f, 0.3811f);
        belias.tags = new Il2CppSystem.Collections.Generic.List<ECharacterTag>();
        belias.art_cute = getSprite("demon_x");
        belias.backgroundArt = getSprite("evil");
        Characters.Instance.startGameActOrder = insertAfterAct("Atheist", belias);

        CharacterData converted = new CharacterData();
        converted.role = new Converted();
        converted.name = "Converted";
        converted.description = "This character has been Converted by Belias. They are considered Evil and Lie, but stay a Villager.";
        converted.flavorText = "\"Poor, unfortunate soul. They're far too deep into their false beliefs.\"";
        converted.hints = "Can be noticed by Witness.";
        converted.ifLies = "";
        converted.notes = "";
        converted.picking = false;
        converted.startingAlignment = EAlignment.Evil;
        converted.type = ECharacterType.Villager;
        converted.abilityUsage = EAbilityUsage.Once;
        converted.bluffable = false;
        converted.characterId = "Converted_EP";
        converted.artBgColor = new Color(1f, 0f, 0f);
        converted.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        converted.cardBorderColor = new Color(0.8208f, 0f, 0.0241f);
        converted.color = new Color(0.8491f, 0.4555f, 0f);
        converted.tags = new Il2CppSystem.Collections.Generic.List<ECharacterTag>();

        CharacterData cavalier = new CharacterData();
        cavalier.role = new Cavalier();
        cavalier.name = "Cavalier";
        cavalier.description = "<b>At Night:</b>\nLose 1 Health.\r\n\nI Lie and Disguise.";
        cavalier.flavorText = "\"He's overly enthusiastic about having a sledgehammer, but nobody's caught on yet.\"";
        cavalier.hints = "";
        cavalier.ifLies = "";
        cavalier.notes = "";
        cavalier.picking = false;
        cavalier.startingAlignment = EAlignment.Evil;
        cavalier.type = ECharacterType.Minion;
        cavalier.abilityUsage = EAbilityUsage.Once;
        cavalier.bluffable = false;
        cavalier.characterId = "Cavalier_EP";
        cavalier.artBgColor = new Color(1f, 0f, 0f);
        cavalier.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        cavalier.cardBorderColor = new Color(0.8208f, 0f, 0.0241f);
        cavalier.color = new Color(0.8491f, 0.4555f, 0f);
        belias.backgroundArt = getSprite("evil");
        cavalier.tags = new Il2CppSystem.Collections.Generic.List<ECharacterTag>();

        GameObject content = GameObject.Find("Game/Gameplay/Content");
        NightPhase nightPhase = content.GetComponent<NightPhase>();
        nightPhase.nightCharactersOrder.Add(cavalier);

        CustomScriptData beliasScriptData = new CustomScriptData();
        beliasScriptData.name = "Belias_1";
        ScriptInfo beliasScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> beliasList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        beliasList.Add(belias);
        beliasScript.mustInclude = beliasList;
        beliasScript.startingDemons = beliasList;
        beliasScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        beliasScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        beliasScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        CharactersCount beliasCounter1 = new CharactersCount(7, 4, 1, 1, 1);
        beliasCounter1.dOuts = beliasCounter1.outs + 1;
        CharactersCount beliasCounter2 = new CharactersCount(9, 6, 1, 1, 2);
        beliasCounter2.dOuts = beliasCounter2.outs + 1;
        CharactersCount beliasCounter3 = new CharactersCount(9, 5, 1, 2, 1);
        beliasCounter3.dOuts = beliasCounter3.outs + 1;
        CharactersCount beliasCounter4 = new CharactersCount(10, 6, 1, 1, 2);
        beliasCounter4.dOuts = beliasCounter4.outs + 1;
        Il2CppSystem.Collections.Generic.List<CharactersCount> beliasCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        beliasCounterList.Add(beliasCounter1);
        beliasCounterList.Add(beliasCounter2);
        beliasCounterList.Add(beliasCounter3);
        beliasCounterList.Add(beliasCounter4);
        beliasScript.characterCounts = beliasCounterList;
        beliasScriptData.scriptInfo = beliasScript;

        CustomScriptData beliasScriptDataDifficult = new CustomScriptData();
        beliasScriptData.name = "Belias_Difficult";
        ScriptInfo beliasScriptDifficult = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> beliasListDifficult = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        beliasListDifficult.Add(belias);
        beliasScriptDifficult.mustInclude = beliasListDifficult;
        beliasScriptDifficult.startingDemons = beliasListDifficult;
        beliasScriptDifficult.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        beliasScriptDifficult.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        beliasScriptDifficult.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        CharactersCount beliasCounter1Difficult = new CharactersCount(6, 4, 1, 0, 1);
        beliasCounter1Difficult.dOuts = beliasCounter1Difficult.outs + 1;
        CharactersCount beliasCounter2Difficult = new CharactersCount(8, 5, 1, 1, 1);
        beliasCounter1Difficult.dOuts = beliasCounter2Difficult.outs + 1;
        CharactersCount beliasCounter3Difficult = new CharactersCount(10, 5, 1, 2, 2);
        beliasCounter1Difficult.dOuts = beliasCounter3Difficult.outs + 1;
        Il2CppSystem.Collections.Generic.List<CharactersCount> beliasCounterListDifficult = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        beliasCounterListDifficult.Add(beliasCounter1Difficult);
        beliasCounterListDifficult.Add(beliasCounter2Difficult);
        beliasCounterListDifficult.Add(beliasCounter3Difficult);
        beliasScriptDifficult.characterCounts = beliasCounterListDifficult;
        beliasScriptDataDifficult.scriptInfo = beliasScriptDifficult;

        AscensionsData advancedAscension = ProjectContext.Instance.gameData.advancedAscension;
        Il2CppReferenceArray<CharacterData> advancedAscensionDemons = new Il2CppReferenceArray<CharacterData>(advancedAscension.demons.Length + 1);
        advancedAscensionDemons = advancedAscension.demons;
        advancedAscensionDemons[advancedAscensionDemons.Length-1] = belias;
        advancedAscension.demons = advancedAscensionDemons;
        Il2CppReferenceArray<CharacterData> advancedAscensionStartingDemons = new Il2CppReferenceArray<CharacterData>(advancedAscension.startingDemons.Length + 1);
        advancedAscensionStartingDemons = advancedAscension.startingDemons;
        advancedAscensionStartingDemons[advancedAscensionStartingDemons.Length-1] = belias;
        advancedAscension.startingDemons = advancedAscensionStartingDemons;
        Il2CppReferenceArray<CustomScriptData> advancedAscensionScriptsData = new Il2CppReferenceArray<CustomScriptData>(advancedAscension.possibleScriptsData.Length + 2);
        advancedAscensionScriptsData = advancedAscension.possibleScriptsData;
        advancedAscensionScriptsData[advancedAscensionScriptsData.Length - 2] = beliasScriptData;
        advancedAscensionScriptsData[advancedAscensionScriptsData.Length - 1] = beliasScriptDataDifficult;
        advancedAscension.possibleScriptsData = advancedAscensionScriptsData;
        foreach (CustomScriptData scriptData in advancedAscension.possibleScriptsData)
        {
            ScriptInfo script = scriptData.scriptInfo;
            addRole(script.startingTownsfolks, cleric);
            addRole(script.startingTownsfolks, assassin);
            addRole(script.startingOutsiders, atheist);
            addRole(script.startingMinions, cavalier);
        }
    }
    public void addRole(Il2CppSystem.Collections.Generic.List<CharacterData> list, CharacterData data)
    {
        if (list.Contains(data))
        {
            return;
        }
        list.Add(data);
    }
    public CharacterData[] allDatas = Array.Empty<CharacterData>();
    public override void OnUpdate()
    {
        if (allDatas.Length == 0)
        {
            var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
            if (loadedCharList != null)
            {
                allDatas = new CharacterData[loadedCharList.Length];
                for (int i = 0; i < loadedCharList.Length; i++)
                {
                    allDatas[i] = loadedCharList[i]!.Cast<CharacterData>();
                }
            }
        }
    }
    public CharacterData[] insertAfterAct(string previous, CharacterData data)
    {
        CharacterData[] actList = Characters.Instance.startGameActOrder;
        int actSize = actList.Length;
        CharacterData[] newActList = new CharacterData[actSize + 1];
        bool inserted = false;
        for (int i = 0; i < actSize; i++)
        {
            if (inserted)
            {
                newActList[i + 1] = actList[i];
            }
            else
            {
                newActList[i] = actList[i];
                if (actList[i].name == previous)
                {
                    newActList[i + 1] = data;
                    inserted = true;
                }
            }
        }
        if (!inserted)
        {
            LoggerInstance.Msg("");
        }
        return newActList;
    }
    public static Sprite? getSprite(string name)
    {
        foreach (Sprite sprite in allSprites)
        {
            if (sprite.name == name)
            {
                return sprite;
            }
        }
        return null;
    }
}