namespace NineTapTourWeb.Models;

/// <summary>
/// Represents a region/location of the NineTapTour franchise.
/// </summary>
public class NineTapRegion
{
    /// <summary>
    /// Unique identifier for the region.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Display name for the region
    /// </summary>
    public required string Name { get; set; }
}

