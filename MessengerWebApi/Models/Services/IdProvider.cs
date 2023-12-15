using System.Text;
using MessengerWebApi.Models.Interfaces;

namespace MessengerWebApi.Models.Services;

public class IdProvider : IIdProvider
{
    public string GenerateId()
    {
        var builder = new StringBuilder();
        Enumerable
            .Range(65, 26)
            .Select(e => ((char)e).ToString())
            .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
            .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
            .OrderBy(e => Guid.NewGuid())
            .Take(11)
            .ToList().ForEach(e => builder.Append(e));
        var id = builder.ToString();
        return id;
    }
}