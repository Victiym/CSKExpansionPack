using Il2Cpp;
using Il2CppFIMSpace.Basics;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using UnityEngine;
using static Il2CppSystem.Globalization.CultureInfo;
using static MelonLoader.MelonLaunchOptions;


namespace MyCustomRole;
[RegisterTypeInIl2Cpp]
public class Converted : Role
{
    Character charReference;
    //Il2CppSystem.Action action;
    public override string Description
    {
        get
        {
            return "Evil Villager";
        }
    }
    public override ActedInfo bcq(Character charRef)
    {
        // charReference = charRef;
        return new ActedInfo("", null);
    }

    public override CharacterData bcz(Character charRef)
    {
        charReference = charRef;
        Characters instance = Characters.Instance;
        Gameplay gameplay = Gameplay.Instance;
        Il2CppSystem.Collections.Generic.List<CharacterData> uniquePool = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (CharacterData data1 in instance.UniquePool)
        {
            uniquePool.Add(data1);
        }
        Il2CppSystem.Collections.Generic.List<CharacterData> currentTown = gameplay.currentTownsfolks;
        foreach (CharacterData data in currentTown)
        {
            uniquePool.Add(data);
        }
        Il2CppSystem.Collections.Generic.List<CharacterData> townPool = instance.gw(uniquePool, ECharacterType.Villager);

        CharacterData sameVillager = charReference.dq();

        /*if (charReference.dq().name == "Follower")
        {
            for(int i = 0; i < townPool.Count; i++)
            {
                if (townPool[i].name == charReference.dq().name)
                {
                    MelonLogger.Msg("Entered bcz if to replace Follower");
                    townPool.Remove(townPool[i]);
                }
            }
            charReference.dq().name = townPool[UnityEngine.Random.RandomRangeInt(0, townPool.Count)].name;
        }*/
        if (charReference.dq().name != "Converted")
        {
            foreach (CharacterData data in townPool)
            {

                if (data.name == charReference.dq().name)
                {
                    sameVillager = data;
                }
            }
        }
        else
        {
            sameVillager = townPool[UnityEngine.Random.RandomRangeInt(0, townPool.Count)];
        }


        //System.Action action = new Action(KillDupes);
        //charReference.onStateChange += action;

        return sameVillager;
    }

    public override void bcs(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Start)
        {
            charRef.statuses.fm(ECharacterStatus.MessedUpByEvil, charRef);
        }
    }
    public override ActedInfo bcr(Character charRef)
    {
        // charReference = charRef;
        return new ActedInfo("", null);
    }

    public Converted() : base(ClassInjector.DerivedConstructorPointer<Converted>())
    {
        ClassInjector.DerivedConstructorBody(this);
        // action = new System.Action(ExecuteDupe);
        charReference = this.charRef;
    }

    public Converted(System.IntPtr ptr) : base(ptr)
    {
        // action = new System.Action(ExecuteDupe);
        charReference = this.charRef;
    }
}