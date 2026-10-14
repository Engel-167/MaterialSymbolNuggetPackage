using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MaterialSymbols.Avalonia.Generator;

class Program
{
    static void Main(string[] args)
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        var svgDir = Path.Combine(projectRoot, "svg-downloads");
        var resourcesDir = Path.Combine(projectRoot, "MaterialSymbols.Avalonia", "Resources", "Symbols");

        if (!Directory.Exists(svgDir))
        {
            Console.WriteLine($"SVG directory not found: {svgDir}");
            Console.WriteLine("Please run scripts/download-svgs.sh first");
            return;
        }

        Console.WriteLine("=== Material Symbols Resource Generator ===");
        Console.WriteLine();

        var weights = new[] { "200", "400" };
        var styles = new[] { "outlined", "rounded", "sharp" };
        var fills = new[] { false, true };

        foreach (var weight in weights)
        {
            var weightDir = Path.Combine(svgDir, $"svg-{weight}");
            if (!Directory.Exists(weightDir))
            {
                Console.WriteLine($"Warning: Weight directory not found: {weightDir}");
                continue;
            }

            foreach (var style in styles)
            {
                var styleDir = Path.Combine(weightDir, style);
                if (!Directory.Exists(styleDir))
                {
                    Console.WriteLine($"Warning: Style directory not found: {styleDir}");
                    continue;
                }

                foreach (var fill in fills)
                {
                    var suffix = fill ? "-fill" : "";
                    var className = $"W{weight}{Capitalize(style)}{(fill ? "Filled" : "")}";
                    var outputDir = Path.Combine(resourcesDir, $"W{weight}");
                    var outputFile = Path.Combine(outputDir, $"{Capitalize(style)}{(fill ? "Filled" : "")}.cs");

                    Console.WriteLine($"Generating {className}...");

                    var icons = new Dictionary<string, string>();
                    var svgFiles = Directory.GetFiles(styleDir, $"*{suffix}.svg");

                    foreach (var svgFile in svgFiles)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(svgFile);
                        var iconName = fileName.EndsWith("-fill") 
                            ? fileName.Substring(0, fileName.Length - 5) 
                            : fileName;

                        var pathData = ExtractPathFromSvg(svgFile);
                        if (!string.IsNullOrEmpty(pathData))
                        {
                            icons[iconName] = pathData;
                        }
                    }

                    if (icons.Count > 0)
                    {
                        Directory.CreateDirectory(outputDir);
                        GenerateResourceFile(outputFile, className, icons);
                        
                        var textFile = Path.Combine(outputDir, $"{Capitalize(style)}{(fill ? "Filled" : "")}.txt");
                        GenerateTextResourceFile(textFile, icons);
                        
                        Console.WriteLine($"  ✓ Generated {icons.Count} icons");
                    }
                    else
                    {
                        Console.WriteLine($"  ⚠ No icons found");
                    }
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== Generation Complete ===");
    }

    static string ExtractPathFromSvg(string svgPath)
    {
        try
        {
            var content = File.ReadAllText(svgPath);
            var doc = XDocument.Parse(content);
            var pathElement = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "path");
            
            if (pathElement != null)
            {
                var d = pathElement.Attribute("d")?.Value;
                if (!string.IsNullOrEmpty(d))
                {
                    return d;
                }
            }

            var match = Regex.Match(content, @"d=""([^""]+)""");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Warning: Could not parse {svgPath}: {ex.Message}");
        }

        return string.Empty;
    }

    static void GenerateResourceFile(string outputPath, string className, Dictionary<string, string> icons)
    {
        var sb = new StringBuilder();
        
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine();
        sb.AppendLine("namespace MaterialSymbols.Avalonia.Resources;");
        sb.AppendLine();
        sb.AppendLine($"public static class {className}");
        sb.AppendLine("{");
        sb.AppendLine("    public static readonly Dictionary<string, string> Icons = new()");
        sb.AppendLine("    {");

        foreach (var kvp in icons.OrderBy(x => x.Key))
        {
            var escapedPath = kvp.Value.Replace("\\", "\\\\").Replace("\"", "\\\"");
            sb.AppendLine($"        [\"{kvp.Key}\"] = \"{escapedPath}\",");
        }

        sb.AppendLine("    };");
        sb.AppendLine();
        sb.AppendLine($"    public static string? GetPath(string iconName)");
        sb.AppendLine("    {");
        sb.AppendLine("        return Icons.TryGetValue(iconName, out var path) ? path : null;");
        sb.AppendLine("    }");
        sb.AppendLine("}");

        File.WriteAllText(outputPath, sb.ToString());
    }

    static void GenerateTextResourceFile(string outputPath, Dictionary<string, string> icons)
    {
        var sb = new StringBuilder();
        
        foreach (var kvp in icons.OrderBy(x => x.Key))
        {
            sb.AppendLine($"{kvp.Key}|{kvp.Value}");
        }

        File.WriteAllText(outputPath, sb.ToString());
    }

    static string Capitalize(string str)
    {
        if (string.IsNullOrEmpty(str)) return str;
        return char.ToUpper(str[0]) + str.Substring(1);
    }
}
