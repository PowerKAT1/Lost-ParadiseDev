using Content.Shared.Actions;
using Robust.Shared.Prototypes;

namespace Content.Shared._Backmen.StationAI;

[RegisterComponent]
public sealed partial class AIEyePowerComponent : Component
{
    [DataField("prototype")]
    public EntProtoId Prototype = "AIEye";

    [DataField("prototypeAction")]
    public EntProtoId PrototypeAction = "AIEyeAction";

    public EntityUid? EyePowerAction = null;
}
