# Tic Tac Toe – Extended Version

Windows desktop **Ultimate Tic-Tac-Toe**: a 3×3 grid of mini tic-tac-toe boards. Win a mini-board to claim that cell on the big board; win three big cells in a line to win the game.

> Paths and assembly names keep spaces: **`Tic Tac Toe - Extended Version`**. The GitHub repo slug uses hyphens (`Tic-Tac-Toe---Extended-Version`).

## What it does

- 9×9 playable cells (nine 3×3 mini-boards)
- Classic send-to-board rule: your move’s *inner* cell index forces the opponent into the matching *outer* board (if that board is still open)
- If the forced board is already claimed, the opponent may play on any open board
- Mini-board win draws a large X/O overlay and locks remaining empty cells there
- Two local players (X then O); win dialog when the big board is claimed

## Requirements

- **Windows** (WinForms)
- [.NET Framework 4.5.2](https://dotnet.microsoft.com/download/dotnet-framework/net452) (or a newer Framework install that can run/target `v4.5.2`)
- [Visual Studio](https://visualstudio.microsoft.com/) with the **.NET desktop development** workload (MSBuild + Framework targeting pack), or equivalent Build Tools

This is a classic **non-SDK** `.csproj` (`ToolsVersion="14.0"`). Prefer Visual Studio / MSBuild on Windows over `dotnet build` alone.

## Build & run

Open the solution in Visual Studio and press **F5** (Debug) or **Ctrl+F5** (Start without debugging):

```text
Tic Tac Toe - Extended Version.sln
```

From a Developer Command Prompt / PowerShell with MSBuild on `PATH`:

```bat
msbuild "Tic Tac Toe - Extended Version.sln" /p:Configuration=Debug
```

The executable lands under `Tic Tac Toe - Extended Version\bin\Debug\` (or `bin\Release\` for Release).

A root `.lnk` shortcut to an `.exe` may exist from local development; it is not required to build.

## How to play

1. Start the app — X moves first.
2. Click an enabled cell to place your mark.
3. Watch which mini-board lights up for the next player (others are dimmed/disabled).
4. Claim mini-boards; first to three-in-a-row on the **big** board wins.

## Contributing

Agent / maintainer notes (layout, board encoding, PR labels): [`AGENTS.md`](./AGENTS.md).
