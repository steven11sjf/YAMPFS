# mpo_yampfs

Python bindings for `YAMPFS_LIB.Patcher.Main`, via [pythonnet](https://pythonnet.github.io/).

```python
from mpo_yampfs import patch

patch("input.mpo", "output.mpo", "config.json")
```

or, to reuse the loaded CLR/assembly across multiple calls:

```python
from mpo_yampfs import load_wrapper

with load_wrapper() as wrapper:
    wrapper.patch("input.mpo", "output.mpo", "config.json")
```

## Native assemblies

`mpo_yampfs/native/` holds a vendored, framework-dependent build of
`YAMPFS-LIB.dll` plus its managed dependencies (`UndertaleModLib.dll`,
`Underanalyzer.dll`, Roslyn scripting, Magick.NET, ...) and the win-x64
native runtime. It is not checked into git (see `.gitignore`) — regenerate
it after changing YAMPFS-LIB with:

```bash
dotnet publish YAMPFS-LIB/YAMPFS-LIB.csproj -c Release --self-contained false -r win-x64
cp -r YAMPFS-LIB/bin/Release/net10.0/publish/. mpo_yampfs/native/
```

(swap `win-x64` for your platform's RID if you're not on Windows.)

## Requirements

```bash
pip install pythonnet
```

A .NET runtime matching the assembly's target framework (net10.0) must be
installed; `pythonnet.load("coreclr")` auto-detects the newest installed
`Microsoft.NETCore.App` runtime.
