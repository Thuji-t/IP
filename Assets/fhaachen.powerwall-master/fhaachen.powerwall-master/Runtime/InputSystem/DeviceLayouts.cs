#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;
using UnityEngine.Scripting;


namespace FHAachen.XR.Powerwall
{
    [Preserve]
    [InputControlLayout(displayName = "Powerwall Controller", commonUsages = new[] { "LeftHand", "RightHand" })]
    public class PowerwallController : XRController
    {
        [Preserve]
        [InputControl(aliases = new[] { "Primary2DAxis", "Joystick" })]
        public Vector2Control thumbstick { get; private set; }

        [Preserve]
        [InputControl]
        public AxisControl trigger { get; private set; }
        [Preserve]
        [InputControl]
        public AxisControl grip { get; private set; }

        [Preserve]
        [InputControl(aliases = new[] { "A", "X", "Alternate" })]
        public ButtonControl primaryButton { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "B", "Y", "Primary" })]
        public ButtonControl secondaryButton { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "GripButton" })]
        public ButtonControl gripPressed { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "indexButton", "indexTouched" })]
        public ButtonControl triggerPressed { get; private set; }
        [Preserve]
        [InputControl]
        public ButtonControl menu { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "JoystickOrPadPressed", "thumbstickClick" })]
        public ButtonControl thumbstickClicked { get; private set; }

        [Preserve]
        [InputControl(aliases = new[] { "ControllerIsTracked" })]
        public new ButtonControl isTracked { get; private set; }

        protected override void FinishSetup()
        {
            base.FinishSetup();

            thumbstick = GetChildControl<Vector2Control>("thumbstick");
            trigger = GetChildControl<AxisControl>("trigger");
            grip = GetChildControl<AxisControl>("grip");

            primaryButton = GetChildControl<ButtonControl>("primaryButton");
            secondaryButton = GetChildControl<ButtonControl>("secondaryButton");
            gripPressed = GetChildControl<ButtonControl>("gripPressed");
            thumbstickClicked = GetChildControl<ButtonControl>("thumbstickClicked");
            triggerPressed = GetChildControl<ButtonControl>("triggerPressed");
            menu = GetChildControl<ButtonControl>("menuButton");

            isTracked = GetChildControl<ButtonControl>("isTracked");
        }
    }
}
#endif