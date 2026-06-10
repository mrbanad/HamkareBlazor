namespace HamkareBlazor;

/// <summary>
/// Provides contextual information about a <see cref="HamkareStep"/> within a <see cref="HamkareStepper"/>.
/// </summary>
public sealed class HamkareStepContext
{
    /// <summary>
    /// Gets the owning <see cref="HamkareStepper"/>.
    /// </summary>
    public HamkareStepper Stepper { get; }

    /// <summary>
    /// Gets the <see cref="HamkareStep"/> associated with the context.
    /// </summary>
    public HamkareStep Step { get; }

    /// <summary>
    /// Gets a value indicating whether the associated step is currently active.
    /// </summary>
    public bool IsActive => Stepper.ActiveStep == Step;

    /// <summary>
    /// Initializes a new instance of the <see cref="HamkareStepContext"/> class.
    /// </summary>
    /// <param name="stepper">The owning stepper.</param>
    /// <param name="step">The step associated with the context.</param>
    public HamkareStepContext(HamkareStepper stepper, HamkareStep step)
    {
        Stepper = stepper ?? throw new ArgumentNullException(nameof(stepper));
        Step = step ?? throw new ArgumentNullException(nameof(step));
    }
}
