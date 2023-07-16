using System.Text.Json;
using HyperProf.Core.Extensions;

namespace HyperProf.Core.Utils.NamingPolicies;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

    public override string ConvertName(string name)
    {
        return name.ToSnakeCase();
    }
}