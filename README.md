# raf

`raf` is a mini Git written in C#.

## Implemented Commands

The CLI currently exposes these commands:

- `raf init`
- `raf add [file]`
- `raf add .`
- `raf status`
- `raf commit [message]`
- `raf help`
- `raf clean`
- `raf update --tool`
- `raf switch [branch]`

### `raf init`

Initializes a repository in the current folder by creating:

- `.raf/`
- `.raf/objects/`
- `.raf/refs/`
- `.raf/refs/heads/`
- `.raf/logs/`
- `.raf/HEAD`
- `.raf/index`

Example:

```powershell
raf init
```

### `raf add [file]`

Adds a single file to `.raf/index` and saves its object inside `.raf/objects`.

Example:

```powershell
raf add teste.txt
```

### `raf add .`

Adds all files from the current folder except files inside `.raf`.

Example:

```powershell
raf add .
```

### `raf status`

Shows the current repository state:

- untracked files
- staged files (`Pendentes`)
- modified files

Example:

```powershell
raf status
```

### `raf commit [message]`

Creates a commit using the current tree and updates `HEAD`.

Example:

```powershell
raf commit first commit
```

### `raf help`

Prints the command list in the terminal.

Example:

```powershell
raf help
```

### `raf clean`

Removes the `.raf` directory after confirmation. If the index contains staged files, it asks for confirmation again before deleting the repository metadata.

Example:

```powershell
raf clean
```

### `raf update --tool`

Updates the tool version in the `.csproj`, packs the project, and runs the global tool update flow.

Example:

```powershell
raf update --tool
```

### `raf switch [branch]`

Switches `HEAD` to an existing branch under `.raf/refs/heads`.

Example:

```powershell
raf switch main
```

## How To Test

This project is a .NET console app. Build it first:

```powershell
dotnet build .\raf.sln
```

After build, run it with:

```powershell
dotnet .\raf\bin\Debug\net9.0\raf.dll [command]
```

Examples:

```powershell
dotnet .\raf\bin\Debug\net9.0\raf.dll init
dotnet .\raf\bin\Debug\net9.0\raf.dll add teste.txt
dotnet .\raf\bin\Debug\net9.0\raf.dll add .
dotnet .\raf\bin\Debug\net9.0\raf.dll status
dotnet .\raf\bin\Debug\net9.0\raf.dll commit "first commit"
dotnet .\raf\bin\Debug\net9.0\raf.dll help
dotnet .\raf\bin\Debug\net9.0\raf.dll clean
dotnet .\raf\bin\Debug\net9.0\raf.dll update --tool
dotnet .\raf\bin\Debug\net9.0\raf.dll switch main
```

If you want to use the exact format `raf [command]`, add the build output folder to your `PATH` or create an alias pointing to:

```powershell
dotnet .\raf\bin\Debug\net9.0\raf.dll
```

## Quick Manual Test

Create a test folder, build the project, and run:

```powershell
raf init
raf status
raf add teste.txt
raf status
raf commit "first commit"
raf help
```

Suggested flow:

1. Create a file like `teste.txt`
2. Run `raf init`
3. Run `raf add teste.txt`
4. Run `raf status`
5. Edit `teste.txt`
6. Run `raf status` again to see it as modified
7. Run `raf commit "first commit"`
