using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using Il2CppSystem;
using Il2Cpp;
using UnityEngine;
using System.ComponentModel.Design;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class Assassin : Role
{
    Character chRef;
    private Il2CppSystem.Action action1;
    private Il2CppSystem.Action action2;
    private Il2CppSystem.Action action3;
    private bool killSuccess = true;
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
            return "Kills a character no matter what";
        }
    }
    public override void bcs(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.kx(1);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    private void CharacterPicked()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);

        string info = $"";
        bool shouldExecute = true;

        info = ConjourInfo(chars[0].id);

        if (chars[0].state == ECharacterState.Dead)
        {
            shouldExecute = false;
            return;
        }

        onActed?.Invoke(new ActedInfo(info, chars));
        Debug.Log($"{info}");

        if (shouldExecute)
        {
            killSuccess = true;
            if (chars[0].dq().name == "Knight" && chars[0].alignment == EAlignment.Good && !chars[0].statuses.fo(ECharacterStatus.Corrupted)) killSuccess = false;
            else if (chRef.alignment == EAlignment.Evil || chRef.statuses.statuses.Contains(ECharacterStatus.Corrupted))
            {
                Health health = PlayerController.PlayerInfo.health;
                chars[0].es();
                if (chars[0].alignment == EAlignment.Evil) return; /*else if (chars[0].dq().name == "Knight") health.jl(4);*/ else health.jl(2);
            }
            else
            {
                Health health = PlayerController.PlayerInfo.health;
                int oldHealth = health.value.jw();
                health.jl(-10);
                chars[0].es();
                int newHealth = health.value.jw();
                health.jl(newHealth - oldHealth);
            }
        }
    }

    public override void bcx(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.kx(1);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }

    private void CharacterPickedDrunk()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);

        string info = $"";
        bool shouldExecute = true;

        info = ConjourInfo(chars[0].id);

        if (chars[0].state == ECharacterState.Dead)
        {
            shouldExecute = false;
            return;
        }

        onActed?.Invoke(new ActedInfo(info, chars));
        Debug.Log($"{info}");

        if (shouldExecute)
        {
            killSuccess = true;
            if (chars[0].dq().name == "Knight" && chars[0].alignment == EAlignment.Good && !chars[0].statuses.fo(ECharacterStatus.Corrupted)) killSuccess = false;
            else if(chRef.alignment == EAlignment.Evil || chRef.statuses.statuses.Contains(ECharacterStatus.Corrupted))
            {
                Health health = PlayerController.PlayerInfo.health;
                chars[0].es();
                if (chars[0].alignment == EAlignment.Evil) return; /*else if (chars[0].dq().name == "Knight") health.jl(4);*/ else health.jl(2);
            }
            else
            {
                Health health = PlayerController.PlayerInfo.health;
                int oldHealth = health.value.jw();
                health.jl(-10);
                chars[0].es();
                int newHealth = health.value.jw();
                health.jl(newHealth - oldHealth);
            }
        }
    }

    public string ConjourInfo(int id)
    {
        string info;
        if (killSuccess)
        {
            info = $"I killed #{id}";
        }
        else
        {
            info = $"I couldn't kill #{id}";
        }
        return info;
    }
    public Assassin() : base(ClassInjector.DerivedConstructorPointer<Assassin>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
    }
    public Assassin(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedDrunk);
    }
}