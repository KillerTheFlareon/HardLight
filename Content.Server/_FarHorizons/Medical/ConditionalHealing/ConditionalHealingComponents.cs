using Content.Server.Medical.Components;
using Content.Shared.Chemistry.Reagent;
using Content.Shared.Damage;
using Content.Shared.Damage.Prototypes;
using Content.Shared.Tag;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Server._FarHorizons.Medical.ConditionalHealing;

[Serializable, NetSerializable, DataDefinition]
public sealed partial class ConditionalHealingData
{
    [DataField]
    public DamageSpecifier Damage = default!;
    [DataField]
    public float BloodlossModifier = 0.0f;
    [DataField]
    public float ModifyBloodLevel = 0.0f;
    [DataField]
    public List<string>? DamageContainers;
    [DataField]
    public float Delay = 2f;
    [DataField]
    public float SelfHealPenaltyMultiplier = 2f;
    [DataField]
    public SoundSpecifier? HealingBeginSound = null;
    [DataField]
    public SoundSpecifier? HealingEndSound = null;

    public HealingComponent MakeComponent() =>
        new()
        {
            Damage = Damage,
            BloodlossModifier = BloodlossModifier,
            ModifyBloodLevel = ModifyBloodLevel,
            DamageContainers = DamageContainers,
            Delay = Delay,
            SelfHealPenaltyMultiplier = SelfHealPenaltyMultiplier,
            HealingBeginSound = HealingBeginSound,
            HealingEndSound = HealingEndSound,
        };
}

[Serializable, NetSerializable, DataDefinition]
public sealed partial class ConditionalHealingDefition
{
    [DataField]
    public HashSet<ProtoId<TagPrototype>> AllowedTags = [];
    [DataField]
    public ConditionalHealingData Healing = default!;
}

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class ConditionalHealingComponent : Component
{
    [DataField(required: true), AutoNetworkedField]
    public List<ConditionalHealingDefition> HealingDefinitions = [];
}