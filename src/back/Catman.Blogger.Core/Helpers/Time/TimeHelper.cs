namespace Catman.Blogger.Core.Helpers.Time
{
    using System;

    public class TimeHelper : ITimeHelper
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
