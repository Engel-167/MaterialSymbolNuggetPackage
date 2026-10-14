#!/bin/bash

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_DIR="$(dirname "$SCRIPT_DIR")"
SVG_DIR="$PROJECT_DIR/svg-downloads"
RESOURCES_DIR="$PROJECT_DIR/MaterialSymbols.Avalonia/Resources/Symbols"

echo "=== Material Symbols SVG Downloader ==="
echo ""

mkdir -p "$SVG_DIR"
cd "$SVG_DIR"

download_npm_package() {
    local package=$1
    local version=$2
    local output_dir=$3
    
    echo "Downloading $package..."
    
    local url="https://registry.npmjs.org/${package}/-/${package##*/}-${version}.tgz"
    local filename="${package##*/}-${version}.tgz"
    
    if [ ! -f "$filename" ]; then
        wget -q --show-progress "$url" -O "$filename" || {
            echo "Trying latest version..."
            wget -q --show-progress "https://registry.npmjs.org/${package}/-/${package##*/}-latest.tgz" -O "$filename"
        }
    fi
    
    echo "Extracting $filename..."
    mkdir -p "$output_dir"
    tar -xzf "$filename" -C "$output_dir" --strip-components=1
    
    echo "✓ $package extracted to $output_dir"
}

download_npm_package "@material-symbols/svg-200" "latest" "svg-200"
download_npm_package "@material-symbols/svg-400" "latest" "svg-400"

echo ""
echo "=== Download Complete ==="
echo "SVGs downloaded to: $SVG_DIR"
echo ""
echo "Next step: Run the C# generator to create resource files"
echo "  dotnet run --project MaterialSymbols.Avalonia.Generator"
