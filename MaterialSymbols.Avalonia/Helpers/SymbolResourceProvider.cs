using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MaterialSymbols.Avalonia;

public static class SymbolResourceProvider
{
    private static readonly Dictionary<(SymbolWeight, SymbolStyle, bool), Dictionary<string, string>> _cache = new();
    private static readonly object _lock = new();

    public static string? GetPath(string iconName, SymbolWeight weight, SymbolStyle style, bool filled)
    {
        var key = (weight, style, filled);
        
        lock (_lock)
        {
            if (!_cache.TryGetValue(key, out var icons))
            {
                icons = LoadIcons(weight, style, filled);
                _cache[key] = icons;
            }

            return icons.TryGetValue(iconName, out var path) ? path : null;
        }
    }

    private static Dictionary<string, string> LoadIcons(SymbolWeight weight, SymbolStyle style, bool filled)
    {
        var resourceName = GetResourceName(weight, style, filled);
        var assembly = typeof(SymbolResourceProvider).Assembly;
        
        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new FileNotFoundException($"Resource not found: {resourceName}");
        }

        var icons = new Dictionary<string, string>();
        using var reader = new StreamReader(stream);
        string? line;
        
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split('|', 2);
            if (parts.Length == 2)
            {
                icons[parts[0]] = parts[1];
            }
        }

        return icons;
    }

    private static string GetResourceName(SymbolWeight weight, SymbolStyle style, bool filled)
    {
        var weightStr = weight == SymbolWeight.W200 ? "W200" : "W400";
        var styleStr = style.ToString();
        var filledStr = filled ? "Filled" : "";
        
        return $"MaterialSymbols.Avalonia.Resources.Symbols.{weightStr}.{styleStr}{filledStr}.txt";
    }

    public static IEnumerable<string> GetIconNames(SymbolWeight weight, SymbolStyle style, bool filled)
    {
        var key = (weight, style, filled);
        
        lock (_lock)
        {
            if (!_cache.TryGetValue(key, out var icons))
            {
                icons = LoadIcons(weight, style, filled);
                _cache[key] = icons;
            }

            return icons.Keys;
        }
    }
}
