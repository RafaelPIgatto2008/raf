# raf

`raf` is a mini Git written in C#.

Today it already supports three basic commands:

- `init`
- `add`
- `status`

## Implemented Commands

### `raf init`

Initializes a repository in the current folder by creating:

- `.raf/`
- `.raf/objects/`
- `.raf/refs/`
- `.raf/HEAD`
- `.raf/index`

Example:

```powershell
raf init
```

### `raf add [file]`

Adds a single file to the `.raf/index` and saves its object inside `.raf/objects`.

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
```

Suggested flow:

1. Create a file like `teste.txt`
2. Run `raf init`
3. Run `raf add teste.txt`
4. Run `raf status`
5. Edit `teste.txt`
6. Run `raf status` again to see it as modified
