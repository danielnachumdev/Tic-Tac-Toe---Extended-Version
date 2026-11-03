# Tic Tac Toe – Extended Version – Agent Context

Implementation guide for coding agents. Product pitch and user build steps: [`README.md`](README.md).

---

## Orientation

| Item | Detail |
| ---- | ------ |
| What is this? | WinForms **Ultimate Tic-Tac-Toe** (3×3 of 3×3) |
| UI entry | `Tic Tac Toe - Extended Version/Main.cs` (+ `Main.Designer.cs`, `Main.resx`) |
| Host | `Program.cs` → `Application.Run(new Main())` |
| Toolchain | Visual Studio / MSBuild + .NET Framework (non-SDK `.csproj`) |
| Target | `v4.5.2`, `OutputType` = `WinExe`, WinForms refs |
| Tests | None today |
| Platform | **Windows only** |
| Secrets | None; do not commit personal absolute paths or binary dumps |

```text
.
├── Tic Tac Toe - Extended Version.sln
├── Tic Tac Toe - Extended Version/
│   ├── Tic Tac Toe - Extended Version.csproj
│   ├── Program.cs
│   ├── Main.cs                 # board state, clicks, win checks, drawing
│   ├── Main.Designer.cs        # WinForms layout
│   ├── Main.resx
│   ├── App.config
│   ├── tic_tac_toe.ico
│   └── Properties/             # AssemblyInfo, Resources, Settings
├── README.md
└── AGENTS.md
```

Namespace: **`Tic_Tac_Toe___Extended_Version`**. Keep spaced folder / `.csproj` / `.sln` names unless intentionally renaming everything.

---

## Develop

Prefer Visual Studio on Windows (F5). CLI:

```bat
msbuild "Tic Tac Toe - Extended Version.sln" /p:Configuration=Debug
```

Almost all game logic is in `Main.cs`:

| Symbol / area | Role |
| ------------- | ---- |
| `MyButton` | Cell button with outer/inner `(x,y)` indices |
| `board[,,,]` | Mini cells: `-1` empty, `0` P1 (X), `1` P2 (O), `2` closed-empty |
| `boards[,]` | Mini-board winners: `-1` open, `0`/`1` claimed |
| `turn` | Move counter; `turn % 2` is current player |
| `check()` | Resolve mini-board wins + lock/overlay |
| `check2()` | Resolve big-board win + MessageBox |
| `MyButton_Click` | Place mark, redraw, apply send-to-board enable/disable |

Do not “modernize” to SDK-style / .NET (Core) / cross-platform UI in drive-by PRs.

---

## PRs, CI, and releases

1. Branch off `main`; open a PR into `main`.
2. Attach **exactly one** semver label: `noBump` | `bumpPatch` | `bumpMinor` | `bumpMajor`.
3. Prefer required checks **`CI / CI`** and **`Semver / Semver`** when thin CI from [`danielnachumdev/actions`](https://github.com/danielnachumdev/actions) is wired (`allow-empty` is appropriate while there are no tests).
4. Docs-only / chore PRs use **`noBump`**.
5. Do not invent NuGet publish for this personal WinForms game unless explicitly requested.

Shared how-to: [actions HOW-TO-USE](https://github.com/danielnachumdev/actions/blob/v1/HOW-TO-USE.md).

---

## Do not

- Rewrite off WinForms / .NET Framework without an explicit request
- Rename the spaced project paths in isolation (breaks the solution)
- Commit `bin/` / `obj/`, user-specific `.user` files, or large unrelated binaries
- Delete tests later if any are added — treat them as protected
