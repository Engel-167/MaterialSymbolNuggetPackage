# MaterialSymbols.Avalonia

Paquete NuGet que integra los Material Symbols de Google en aplicaciones Avalonia UI con soporte para pesos w200 y w400, múltiples estilos (outlined, rounded, sharp), variantes filled, y temas dinámicos.

## Características

- ✅ **3,584 iconos** por combinación de peso, estilo y variante
- ✅ **Soporte para w200 y w400** (grosores de línea)
- ✅ **3 estilos**: Outlined, Rounded, Sharp
- ✅ **Variantes filled** (con relleno) para cada icono
- ✅ **Temas dinámicos** (Light/Dark mode) con sobrescritura manual
- ✅ **Compatible con desktop y mobile**
- ✅ **Uso en XAML y C#**

## Instalación

```bash
dotnet add package MaterialSymbols.Avalonia
```

## Uso

### En XAML

#### 1. Agregar el namespace

```xml
<Window xmlns:ms="https://github.com/materialsymbols/avalonia"
        ...>
```

#### 2. Uso básico

```xml
<ms:MaterialSymbol Icon="home" Weight="W200" />
<ms:MaterialSymbol Icon="favorite" Filled="True" Foreground="Red" />
<ms:MaterialSymbol Icon="settings" Style="Rounded" />
```

#### 3. Con colores personalizados

```xml
<ms:MaterialSymbol Icon="star" Foreground="Gold" />
<ms:MaterialSymbol Icon="check_circle" Foreground="Green" />
<ms:MaterialSymbol Icon="error" Foreground="Red" />
```

#### 4. Usando extensión de marcado

```xml
<PathIcon Data="{ms:MaterialSymbol home}" />
<PathIcon Data="{ms:MaterialSymbol favorite, Filled=True}" Foreground="Red" />
<PathIcon Data="{ms:MaterialSymbol settings, Style=Rounded}" />
```

### En C# (Code Behind)

```csharp
using MaterialSymbols.Avalonia.Controls;

var symbol = new MaterialSymbol
{
    Icon = "grocery_store",
    Weight = SymbolWeight.W200,
    Style = SymbolStyle.Outlined,
    Filled = false,
    Foreground = Brushes.Blue
};
```

### Configuración de Temas

El paquete soporta temas claro y oscuro automáticamente. Para configurarlo:

```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             x:Class="YourApp.App"
             RequestedThemeVariant="Default">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceInclude Source="avares://MaterialSymbols.Avalonia/Themes/Generic.axaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `Icon` | `string` | Nombre del icono (ej. "home", "favorite") |
| `Weight` | `SymbolWeight` | `W200` o `W400` |
| `Style` | `SymbolStyle` | `Outlined`, `Rounded`, o `Sharp` |
| `Filled` | `bool` | `true` para variante con relleno |
| `Foreground` | `IBrush` | Color del icono (respeta el tema por defecto) |

## Iconos Disponibles

El paquete incluye **3,584 iconos** de Material Symbols por cada combinación:

- **Pesos**: W200, W400
- **Estilos**: Outlined, Rounded, Sharp
- **Variantes**: Normal, Filled

Total: **21,504 iconos** embebidos

### Ejemplos de nombres de iconos

- `home`, `search`, `menu`, `close`
- `favorite`, `star`, `check_circle`
- `settings`, `edit`, `delete`, `save`
- `shopping_cart`, `payment`, `local_shipping`
- `person`, `group`, `email`, `phone`

Para ver todos los iconos disponibles, visita [Google Fonts Icons](https://fonts.google.com/icons).

## Rendimiento

- Los iconos se cargan bajo demanda
- Se cachean en memoria para acceso rápido
- Tamaño del assembly: ~15 MB (todos los iconos embebidos)

## Licencia

- Material Symbols son creados por Google y están bajo licencia [Apache 2.0](https://www.apache.org/licenses/LICENSE-2.0)
- Este paquete está bajo licencia MIT

## Requisitos

- .NET 10.0 o superior
- Avalonia 12.1.4 o superior

## Compilar desde el código fuente

```bash
# Clonar el repositorio
git clone https://github.com/yourusername/MaterialSymbols.Avalonia.git

# Descargar los SVGs
cd MaterialSymbols.Avalonia
bash scripts/download-svgs.sh

# Generar los recursos
cd MaterialSymbols.Avalonia.Generator
dotnet run

# Compilar
cd ..
dotnet build
```

## Contribuir

Las contribuciones son bienvenidas. Por favor, abre un issue o pull request en GitHub.

## Créditos

- [Material Symbols de Google](https://fonts.google.com/icons)
- [Avalonia UI](https://avaloniaui.net/)
