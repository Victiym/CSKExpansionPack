using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using UnityEngine;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class Atheist : Role
{
    public override ActedInfo bcq(Character charRef)
    {
        return new ActedInfo("", null);
    }
    public override ActedInfo bcr(Character charRef)
    {
        return new ActedInfo("", null);
    }
    public override string Description
    {
        get
        {
            return "1 fake Minion and 1 fake Demon";
        }
    }
    public override void bcs(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            Gameplay gameplay = Gameplay.Instance;
            Characters instance = Characters.Instance;
            Il2CppSystem.Collections.Generic.List<CharacterData> list1 = gameplay.mr();
            Il2CppSystem.Collections.Generic.List<CharacterData> list2 = instance.hh(list1);
            Il2CppSystem.Collections.Generic.List<CharacterData> list3 = instance.gw(list2, ECharacterType.Minion);
            if (list3.Count == 0)
            {
                return;
            }
            CharacterData randomData = list3[UnityEngine.Random.RandomRangeInt(0, list3.Count)];
            gameplay.ml(ECharacterType.Minion, randomData);

            Gameplay gameplayD = Gameplay.Instance;
            Characters instanceD = Characters.Instance;
            var allDemons = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());

            var list1D = new Il2CppSystem.Collections.Generic.List<CharacterData>(allDemons.Length);
            for (int i = 0; i < allDemons.Length; i++)
            {
                var item = allDemons[i].Cast<CharacterData>();
                if (item != null)
                    list1D.Add(item);
            }

            Il2CppSystem.Collections.Generic.List<CharacterData> list2D = instanceD.hh(list1D);
            Il2CppSystem.Collections.Generic.List<CharacterData> list3D = instanceD.gp(list2D, ECharacterType.Demon);
            if (list3D.Count == 0)
            {
                return;
            }
            CharacterData randomDataD = list3D[UnityEngine.Random.RandomRangeInt(0, list3D.Count)];
            while (randomDataD.name == "Mutant")
            {
                randomDataD = list3D[UnityEngine.Random.RandomRangeInt(0, list3D.Count)];
            }
            gameplayD.ml(ECharacterType.Demon, randomDataD);
        }
    }
    public Atheist() : base(ClassInjector.DerivedConstructorPointer<Atheist>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public Atheist(IntPtr ptr) : base(ptr)
    {
    }
}