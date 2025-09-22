using Il2Cpp;
using Il2CppDissolveExample;
using Il2CppFIMSpace.Basics;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.Runtime.Serialization.Formatters.Binary;
using MelonLoader;
using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using UnityEngine;
using static Il2CppSystem.Globalization.CultureInfo;
using static MelonLoader.MelonLaunchOptions;


namespace MyCustomRole;
[RegisterTypeInIl2Cpp]
public class Belias : Demon
{
    Character charReference;
    public override string Description
    {
        get
        {
            return "Adds an Evil Villager.";
        }
    }
    public override ActedInfo bcq(Character charRef)
    {
        return new ActedInfo("", null);
    }
    public override void bcs(ETriggerPhase trigger, Character charRef)
    {
        charReference = charRef;

        if (trigger != ETriggerPhase.Start)
        {
            return;
        }
        Gameplay gameplay = Gameplay.Instance;
        Characters instance = Characters.Instance;

        Il2CppSystem.Collections.Generic.List<Character> list4 = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            list4.Add(character);
        }
        Il2CppSystem.Collections.Generic.List<Character> list5 = instance.hi(list4);
        Il2CppSystem.Collections.Generic.List<Character> list6 = instance.gs(list5, ECharacterType.Villager);
        if (list6.Count > 0)
        {

            var allCharacters = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());

            var list1D = new Il2CppSystem.Collections.Generic.List<CharacterData>(allCharacters.Length);
            for (int i = 0; i < allCharacters.Length; i++)
            {
                var item = allCharacters[i].Cast<CharacterData>();
                if (item != null)
                    list1D.Add(item);
            }

            Character randomCharacter = list6[UnityEngine.Random.RandomRangeInt(0, list6.Count)];
            CharacterData? random = new();

            foreach (var converted in list1D)
            {
                if (converted.characterId == "Converted_EP")
                {
                    converted.name = "Converted";
                    converted.description = "This character has been Converted by Belias. They are considered Evil and Lie, but stay a Villager.";
                    converted.hints = "Can be noticed by Witness.";
                    converted.ifLies = "";
                    converted.notes = "";
                    converted.picking = false;
                    converted.startingAlignment = EAlignment.Evil;
                    converted.type = ECharacterType.Villager;
                    converted.abilityUsage = EAbilityUsage.Once;
                    converted.bluffable = false;
                    converted.art_cute = null;
                    converted.artBgColor = new Color(1f, 0f, 0f);
                    converted.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
                    converted.cardBorderColor = new Color(0.8208f, 0f, 0.0241f);
                    converted.color = new Color(0.8491f, 0.4555f, 0f);
                    converted.tags = new Il2CppSystem.Collections.Generic.List<ECharacterTag>();   

                    random = converted;
                }
            }
            if (random != null)
            {
                Il2CppSystem.Collections.Generic.List<ECharacterStatus> corruptionStatus = randomCharacter.statuses.statuses;
                random.name = randomCharacter.dq().name;
                random.picking = randomCharacter.dq().picking;
                random.bluffable = false;
                random.art_cute = randomCharacter.dq().art_cute;
                random.artBgColor = randomCharacter.dq().artBgColor;
                random.backgroundArt = randomCharacter.dq().backgroundArt;
                random.type = ECharacterType.Villager;
                random.tags = new Il2CppSystem.Collections.Generic.List<ECharacterTag>();
                randomCharacter.dv(random);
                randomCharacter.statuses.statuses = corruptionStatus;
                gameplay.characters.gm(randomCharacter);
            }
        }
    }
    public override void bcx(ETriggerPhase trigger, Character charRef)
    {
    }
    public override ActedInfo bcr(Character charRef)
    {
        return new ActedInfo("", null);
    }

    public Belias() : base(ClassInjector.DerivedConstructorPointer<Belias>())
    {
        ClassInjector.DerivedConstructorBody(this);
        charReference = charRef;
    }

    public Belias(System.IntPtr ptr) : base(ptr)
    {
        charReference = charRef;
    }
}