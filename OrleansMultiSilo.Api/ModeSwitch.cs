namespace OrleansMultiSilo.Api;

public enum ModeSwitch
{
    /// <summary>
    /// Uses default grains (random placement)
    /// </summary>
    Default = 0,
    /// <summary>
    /// Children have "prefer local" attribute
    /// </summary>
    PreferLocal = 1,
    /// <summary>
    /// Parent uses hash based placement, child is a stateless worker (max Count: 1)
    /// </summary>
    HashBasedParentStatelessChild = 2,
    /// <summary>
    /// Parent is using hash based placement, and the children are placed using a dumb local only grain directory implementation
    /// </summary>
    HashBasedParentNaiveChild = 3,
    /// <summary>
    /// Use the experimental distributed grain directory
    /// </summary>
    Experimental = 4,
}