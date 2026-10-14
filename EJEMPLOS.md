# Ejemplo de Uso: MaterialSymbols.Avalonia

Este documento muestra ejemplos prácticos de cómo usar el paquete MaterialSymbols.Avalonia en diferentes escenarios.

## 1. Configuración Inicial

### App.axaml

```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             x:Class="TuApp.App"
             RequestedThemeVariant="Default">
    <Application.Styles>
        <FluentTheme />
    </Application.Styles>
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceInclude Source="avares://MaterialSymbols.Avalonia/Themes/Generic.axaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

## 2. Uso Básico en XAML

### Agregar el namespace

```xml
<Window xmlns:ms="https://github.com/materialsymbols/avalonia"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class="TuApp.MainWindow">
    
    <StackPanel Margin="20" Spacing="10">
        
        <!-- Icono básico con peso W200 -->
        <ms:MaterialSymbol Icon="home" Weight="W200" Width="48" Height="48" />
        
        <!-- Icono con peso W400 -->
        <ms:MaterialSymbol Icon="home" Weight="W400" Width="48" Height="48" />
        
        <!-- Icono con variante filled -->
        <ms:MaterialSymbol Icon="favorite" Filled="True" Width="48" Height="48" />
        
        <!-- Icono con estilo Rounded -->
        <ms:MaterialSymbol Icon="settings" Style="Rounded" Width="48" Height="48" />
        
        <!-- Icono con estilo Sharp -->
        <ms:MaterialSymbol Icon="settings" Style="Sharp" Width="48" Height="48" />
        
    </StackPanel>
</Window>
```

## 3. Colores Personalizados

```xml
<StackPanel Spacing="10">
    <!-- Color específico -->
    <ms:MaterialSymbol Icon="star" Foreground="Gold" Width="48" Height="48" />
    
    <!-- Usando brushes del sistema -->
    <ms:MaterialSymbol Icon="check_circle" Foreground="{DynamicResource SystemAccentColor}" Width="48" Height="48" />
    
    <!-- Con binding -->
    <ms:MaterialSymbol Icon="error" Foreground="{Binding ErrorColor}" Width="48" Height="48" />
</StackPanel>
```

## 4. Uso con PathIcon (Extensión de Marcado)

```xml
<StackPanel Spacing="10">
    <!-- Uso básico -->
    <PathIcon Data="{ms:MaterialSymbol home}" Width="48" Height="48" />
    
    <!-- Con parámetros -->
    <PathIcon Data="{ms:MaterialSymbol favorite, Weight=W200, Filled=True}" 
              Foreground="Red" 
              Width="48" 
              Height="48" />
    
    <!-- En un botón -->
    <Button>
        <StackPanel Orientation="Horizontal" Spacing="8">
            <PathIcon Data="{ms:MaterialSymbol save}" Width="16" Height="16" />
            <TextBlock Text="Guardar" />
        </StackPanel>
    </Button>
</StackPanel>
```

## 5. Uso en Code Behind (C#)

### Creación directa

```csharp
using MaterialSymbols.Avalonia.Controls;
using Avalonia.Media;

var symbol = new MaterialSymbol
{
    Icon = "grocery_store",
    Weight = SymbolWeight.W200,
    Style = SymbolStyle.Outlined,
    Filled = false,
    Foreground = Brushes.Blue,
    Width = 48,
    Height = 48
};

MiContenedor.Children.Add(symbol);
```

### Cambiar propiedades dinámicamente

```csharp
// Cambiar el icono
symbol.Icon = "favorite";

// Cambiar el peso
symbol.Weight = SymbolWeight.W400;

// Cambiar el color
symbol.Foreground = Brushes.Red;

// Activar variante filled
symbol.Filled = true;
```

## 6. Ejemplo: Botón con Icono

```xml
<Button HorizontalAlignment="Center" Padding="16,8">
    <StackPanel Orientation="Horizontal" Spacing="8">
        <ms:MaterialSymbol Icon="shopping_cart" Width="24" Height="24" />
        <TextBlock Text="Agregar al Carrito" VerticalAlignment="Center" />
    </StackPanel>
</Button>
```

## 7. Ejemplo: Lista de Navegación

```xml
<ListBox>
    <ListBoxItem>
        <StackPanel Orientation="Horizontal" Spacing="12">
            <ms:MaterialSymbol Icon="dashboard" Width="24" Height="24" />
            <TextBlock Text="Dashboard" VerticalAlignment="Center" />
        </StackPanel>
    </ListBoxItem>
    <ListBoxItem>
        <StackPanel Orientation="Horizontal" Spacing="12">
            <ms:MaterialSymbol Icon="inventory" Width="24" Height="24" />
            <TextBlock Text="Inventario" VerticalAlignment="Center" />
        </StackPanel>
    </ListBoxItem>
    <ListBoxItem>
        <StackPanel Orientation="Horizontal" Spacing="12">
            <ms:MaterialSymbol Icon="settings" Width="24" Height="24" />
            <TextBlock Text="Configuración" VerticalAlignment="Center" />
        </StackPanel>
    </ListBoxItem>
</ListBox>
```

## 8. Ejemplo: Galería de Iconos

```xml
<ItemsControl>
    <ItemsControl.ItemsPanel>
        <ItemsPanelTemplate>
            <WrapPanel />
        </ItemsPanelTemplate>
    </ItemsControl.ItemsPanel>
    <ItemsControl.ItemTemplate>
        <DataTemplate>
            <StackPanel Margin="10" Spacing="5">
                <ms:MaterialSymbol Icon="{Binding IconName}" 
                                  Width="48" 
                                  Height="48" 
                                  HorizontalAlignment="Center" />
                <TextBlock Text="{Binding IconName}" 
                          HorizontalAlignment="Center" 
                          FontSize="10" />
            </StackPanel>
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ItemsControl>
```

## 9. Ejemplo: Tema Dinámico

```xml
<UserControl>
    <StackPanel Spacing="20">
        
        <!-- Estos iconos cambian de color automáticamente con el tema -->
        <ms:MaterialSymbol Icon="light_mode" Width="48" Height="48" />
        <ms:MaterialSymbol Icon="dark_mode" Width="48" Height="48" />
        
        <!-- Botón para cambiar el tema -->
        <Button Click="ToggleTheme_Click" Content="Cambiar Tema" />
        
    </StackPanel>
</UserControl>
```

```csharp
private void ToggleTheme_Click(object? sender, RoutedEventArgs e)
{
    var app = Application.Current!;
    app.RequestedThemeVariant = app.RequestedThemeVariant == ThemeVariant.Dark 
        ? ThemeVariant.Light 
        : ThemeVariant.Dark;
}
```

## 10. Iconos Populares

Aquí hay una lista de algunos iconos populares que puedes usar:

### Navegación
- `home`, `menu`, `close`, `arrow_back`, `arrow_forward`
- `chevron_left`, `chevron_right`, `expand_more`, `expand_less`

### Acciones
- `search`, `add`, `remove`, `edit`, `delete`
- `save`, `download`, `upload`, `share`, `print`

### Estado
- `check_circle`, `error`, `warning`, `info`, `help`

### Social
- `favorite`, `star`, `thumb_up`, `thumb_down`, `share`

### Comunicación
- `email`, `phone`, `chat`, `message`, `notifications`

### Persona
- `person`, `group`, `account_circle`, `face`

### Comercio
- `shopping_cart`, `payment`, `local_shipping`, `inventory`, `store`

## 11. Acceso Programático a Iconos

```csharp
using MaterialSymbols.Avalonia;
using Avalonia.Media;

// Obtener el path de un icono
var pathData = SymbolResourceProvider.GetPath("home", SymbolWeight.W400, SymbolStyle.Outlined, false);

// Crear un StreamGeometry
if (pathData != null)
{
    var geometry = StreamGeometry.Parse(pathData);
    // Usar el geometry...
}

// Obtener todos los nombres de iconos
var iconNames = SymbolResourceProvider.GetIconNames(SymbolWeight.W400, SymbolStyle.Outlined, false);
foreach (var name in iconNames)
{
    Console.WriteLine(name);
}
```

## 12. Mejores Prácticas

### 1. Usar tamaños consistentes
```xml
<!-- Definir un estilo reutilizable -->
<Style Selector="ms|MaterialSymbol">
    <Setter Property="Width" Value="24" />
    <Setter Property="Height" Value="24" />
</Style>
```

### 2. Usar recursos para colores
```xml
<Application.Resources>
    <SolidColorBrush x:Key="PrimaryIconBrush" Color="#2196F3" />
    <SolidColorBrush x:Key="DangerIconBrush" Color="#F44336" />
</Application.Resources>

<!-- Uso -->
<ms:MaterialSymbol Icon="info" Foreground="{StaticResource PrimaryIconBrush}" />
<ms:MaterialSymbol Icon="error" Foreground="{StaticResource DangerIconBrush}" />
```

### 3. Combinar con texto
```xml
<StackPanel Orientation="Horizontal" Spacing="8">
    <ms:MaterialSymbol Icon="info" VerticalAlignment="Center" />
    <TextBlock Text="Información importante" VerticalAlignment="Center" />
</StackPanel>
```

## 13. Solución de Problemas

### El icono no se muestra
- Verifica que el nombre del icono sea correcto (usa guiones bajos, no espacios)
- Asegúrate de haber agregado el namespace `xmlns:ms="https://github.com/materialsymbols/avalonia"`
- Verifica que el paquete esté instalado correctamente

### El color no cambia
- La propiedad `Foreground` sobrescribe el tema
- Asegúrate de usar un `IBrush` válido (ej. `Brushes.Red`, `SolidColorBrush`)

### El icono se ve pixelado
- Asegúrate de usar tamaños múltiplos de 24 (24, 48, 72, etc.)
- Los iconos son vectoriales, así que puedes usar cualquier tamaño
