namespace SharpViews;

/// <summary>
/// An interface that exposes getters for UI width, and UI height.
/// </summary>
public interface IDimensionsProvider {
    /// <summary>
    /// Gets the window width.
    /// </summary>
    public int UiWidth {get;}

    /// <summary>
    /// Gets the window height.
    /// </summary>
    public int UiHeight {get;}
}