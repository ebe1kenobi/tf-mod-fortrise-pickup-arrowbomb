# ArrowBomb

Adds a pickup that grants a volley of arrows of a chosen type (bomb, laser,
bramble...). A chest in the level may hold it, based on an adjustable random roll.

A mod for **FortRise 5** (>= 5.3.3). The FortRise 4 version (`tf-mod-fortrise-pickup-arrowbomb`) is no longer maintained: fixes and new features only land in this repository.

## Installation

1. Install FortRise 5 and start the game through `FortRise.exe`.
2. Copy `release/arrowbomb` (or the shipped folder) into `<TowerFall>/FortRise/Mods/`.

Settings are under **Options > Mods > ArrowBomb**.
Data and log files live in `<TowerFall>/FortRise/Saves/ArrowBomb/` and `<TowerFall>/FortRise/Logs/`.

## Usage

Tick the **ArrowBomb** variant on the versus variants screen, or turn on the
"Pickup activated even when variant is not selected" setting to make the pickup
appear without the variant.

> All my mods declare the same `Header` (`EBE1 MODS`), so their variants are
> grouped into a **single column** of the variants screen instead of one column
> per mod.

## Settings

| Setting | Purpose |
|---------|---------|
| Pickup activated even when variant is not selected | spawn the pickup even when the variant is unticked |
| Periodicity | `Normal` (random roll) or `Test` (every level, for trying it out) |
| Treasure Rate 1 chance on N | spawn odds: 1 chance in N |
| Number of Arrow | how many arrows are granted |
| Arrow Type | arrow type granted (Bomb, Laser, Bramble, Drill, Bolt, Toy, Feather, Trigger, Prism...) |

## Build / deployment

| Script | Purpose |
|--------|---------|
| `script/release.bat` | build, then assemble into `release/` |
| `script/deploy.bat` | copy `release/` into the TowerFall `Mods` folder |
| `script/release_deploy.bat` | both, one after the other |

Paths (game folder, module name) are set in `script/config.bat`.
