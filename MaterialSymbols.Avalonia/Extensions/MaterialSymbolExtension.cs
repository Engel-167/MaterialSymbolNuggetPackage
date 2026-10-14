using System;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace MaterialSymbols.Avalonia.Extensions;

public class MaterialSymbolExtension : MarkupExtension
{
    public string Icon { get; set; } = string.Empty;
    public SymbolWeight Weight { get; set; } = SymbolWeight.W400;
    public SymbolStyle Style { get; set; } = SymbolStyle.Outlined;
    public bool Filled { get; set; } = false;

    public MaterialSymbolExtension()
    {
    }

    public MaterialSymbolExtension(string icon)
    {
        Icon = icon;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (string.IsNullOrEmpty(Icon))
        {
            return null!;
        }

        var pathData = SymbolResourceProvider.GetPath(Icon, Weight, Style, Filled);
        
        if (string.IsNullOrEmpty(pathData))
        {
            return null!;
        }

        try
        {
            return StreamGeometry.Parse(pathData);
        }
        catch
        {
            return null!;
        }
    }
}
