using FHAachen.XR.Powerwall;
using UnityEditor.XR.Management;

public class PowerwallBuildProcessor : XRBuildHelper<PowerwallBuildSettings>
{
    public override string BuildSettingsKey => "xr.sdk.powerwall.settings";
}
