using MusicEvents.Data;

namespace MusicEvents.Common;

public static class CurrentUser
{
    public static Guid? CurrentUserId { get; set; } = null;
}