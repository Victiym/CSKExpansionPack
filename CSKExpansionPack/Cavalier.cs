using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using UnityEngine;
using UnityEngine.Profiling;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class Cavalier : Minion
{
    public override Il2CppSystem.Collections.Generic.List<SpecialRule> bcm()
    {
        Il2CppSystem.Collections.Generic.List<SpecialRule> sr = new Il2CppSystem.Collections.Generic.List<SpecialRule>();
        bool lilisInPlay = false;
        Il2CppSystem.Collections.Generic.List<CharacterData> charInPlay = Gameplay.Instance.mo();
        foreach (CharacterData character in charInPlay)
        {
            if (character.characterId == "Lillith_90453844")
            {
                lilisInPlay = true;
            }
        }
        if (!lilisInPlay)
        {
            sr.Add(new NightModeRule(4));
        }
        return sr;
    }
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
            return "1 damage per night";
        }
    }
    public override void bcs(ETriggerPhase trigger, Character charRef)
    {
        if (charRef.state == ECharacterState.Dead) return;
        if (trigger == ETriggerPhase.Night)
        {
            Health health = PlayerController.PlayerInfo.health;
            health.jl(1);
        }
    }
    public Cavalier() : base(ClassInjector.DerivedConstructorPointer<Cavalier>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public Cavalier(IntPtr ptr) : base(ptr)
    {
    }
}