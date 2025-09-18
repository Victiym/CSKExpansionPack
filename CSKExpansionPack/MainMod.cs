using ExpansionPack;
using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using System;
using UnityEngine;
using static Il2CppSystem.Array;

[assembly: MelonInfo(typeof(MainMod), "CSK's Expansion Pack", "1.0", "CharaDr33murr")]
[assembly: MelonGame("UmiArt", "Demon Bluff")]

namespace ExpansionPack;
public class MainMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        ClassInjector.RegisterTypeInIl2Cpp<Cleric>();
    }
    public override void OnLateInitializeMelon()
    {
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

        CharacterData assassin = new CharacterData();
        assassin.role = new Assassin();
        assassin.name = "Assassin";
        assassin.description = "Pick 1 Target. It is Executed without dealing any damage.\n\nIf I am Evil or Corrupted, instead take normal damage + 2 HP.";
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
        Characters.Instance.startGameActOrder = insertAfterAct("Baa", atheist);

        AscensionsData advancedAscension = ProjectContext.Instance.gameData.advancedAscension;
        foreach (CustomScriptData scriptData in advancedAscension.possibleScriptsData)
        {
            ScriptInfo script = scriptData.scriptInfo;
            addRole(script.startingTownsfolks, cleric);
            addRole(script.startingTownsfolks, assassin);
            addRole(script.startingOutsiders, atheist);
            //addRole(script.startingMinions, myCustomMinionData);
        }
        // By the vanilla rule of one demon per village max
        //CustomScriptData newScriptData = GameObject.Instantiate(advancedAscension.possibleScriptsData[0]);
        //ScriptInfo newScript = newScriptData.scriptInfo;
        //newScript.startingDemons.Clear();
        //newScript.startingDemons.Add(myCustomDemonData);
        //int len = advancedAscension.possibleScriptsData.Length;
        //CustomScriptData[] newPSD = new CustomScriptData[len + 1];
        //for (int i = 0; i < len; i++)
        //{
        //    newPSD[i] = advancedAscension.possibleScriptsData[i];
        //}
        //newPSD[len] = newScriptData;
        //advancedAscension.possibleScriptsData = newPSD;
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
}