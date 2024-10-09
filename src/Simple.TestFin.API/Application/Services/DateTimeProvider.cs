using Simple.TestFin.API.Application.Services.Interfaces;

namespace Simple.TestFin.API.Application.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow()
    {
        return DateTime.UtcNow;
    }
}