namespace MyMonkeyApp;

/// <summary>
/// Represents a monkey species with detailed information including location, population, and geographical coordinates.
/// </summary>
public class Monkey
{
    /// <summary>
    /// Gets or sets the name of the monkey species or individual.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geographic location(s) where the monkey is found.
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets detailed information about the monkey species.
    /// </summary>
    public string Details { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL to an image of the monkey.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the population count of the monkey species.
    /// </summary>
    public int Population { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate of the monkey's primary location.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the monkey's primary location.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Returns a string representation of the monkey with basic information.
    /// </summary>
    /// <returns>A formatted string containing the monkey's name and location.</returns>
    public override string ToString()
    {
        return $"{Name} - {Location}";
    }
}
