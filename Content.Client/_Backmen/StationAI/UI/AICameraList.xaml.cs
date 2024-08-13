using System.Linq;
using System.Net.Mime;
using System.Numerics;
using Content.Client.Pinpointer.UI;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.XAML;
using Content.Shared._Backmen.StationAI;
using Robust.Client.UserInterface.Controls;
using Content.Client.UserInterface.Controls;
using Robust.Client.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Utility;

namespace Content.Client._Backmen.StationAI.UI;

[GenerateTypedNameReferences]
public sealed partial class AICameraList : FancyWindow
{
    [Dependency] private readonly IEntityManager _entityManager = default!;
    public event Action<NetEntity>? WarpToCamera;

    private readonly EntityUid _owner;
    public AICameraList(EntityUid? mapUid, EntityUid? trackedEntity)
    {
        _owner = trackedEntity ?? EntityUid.Invalid;
        RobustXamlLoader.Load(this);
        IoCManager.InjectDependencies(this);

        NavMapScreen.MapUid = mapUid;

        var msg = new FormattedMessage();
        if (_entityManager.TryGetComponent<MetaDataComponent>(mapUid, out var metadata))
        {
            msg.AddText(metadata.EntityName);
        }
        else
        {
            msg.AddText(Loc.GetString("ai-warp-menu-no-cameras"));
        }
        StationName.SetMessage(msg);

        NavMapScreen.TrackedEntitySelectedAction += ItemSelected;
        UpdateCameras();




    }

    public void UpdateCameras()
    {
        if (!_entityManager.TryGetComponent<AIEyeComponent>(_owner, out var eyeComponent))
            return;

        NavMapScreen.TrackedEntities.Clear();

        foreach (var (camera,pos) in eyeComponent.FollowsCameras)
        {
            var texture = _entityManager.System<SpriteSystem>().Frame0(new SpriteSpecifier.Texture(new ResPath("/Textures/Interface/NavMap/beveled_circle.png")));
            var isSame = _entityManager.TryGetEntity(camera, out var camUid) && camUid == eyeComponent.Camera;
            var blip = new NavMapBlip(_entityManager.GetCoordinates(pos), texture, isSame ? Color.DarkRed : Color.Cyan, isSame, true);
            NavMapScreen.TrackedEntities[camera] = blip;
        }

        NavMapScreen.ForceNavMapUpdate();
    }

    private void ItemSelected(NetEntity? obj)
    {
        if(obj != null)
            WarpToCamera?.Invoke(obj.Value);
    }
}
