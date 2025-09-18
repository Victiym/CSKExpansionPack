using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class Cleric : Role
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
            return "+- 3 HP";
        }
    }
    public override void bcs(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            Health health = PlayerController.PlayerInfo.health;
            if (charRef.alignment == EAlignment.Evil || charRef.statuses.statuses.Contains(ECharacterStatus.Corrupted))
            {
                health.jl(3);
            }
            else
            {
                health.jl(-3);
                int healthCount = health.value.jw();
                if (healthCount > 10)
                {
                    health.jl(healthCount - 10);
                }
            }
            charRef.uses--;
            if (charRef.uses <= 0)
            {
                charRef.pickable.SetActive(false);
            }
        }
    }
    public Cleric() : base(ClassInjector.DerivedConstructorPointer<Cleric>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public Cleric(IntPtr ptr) : base(ptr)
    {
    }
}