@echo off
SET MODE=Release

"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" ModEnabler/ModEnabler.sln /p:Configuration=%MODE% /p:PostBuildEvent=

for /R ModEnabler\bin\%MODE%\ %%f in (*.dll) do (
  copy %%f "Unity Sample\Assets\ModEnabler\"
  copy %%f "AssetStore Package\Assets\ModEnabler\"
)
for /R ModEnabler\bin\%MODE%\ %%f in (*.txt) do (
  copy %%f "Unity Sample\Assets\ModEnabler\"
  copy %%f "AssetStore Package\Assets\ModEnabler\"
)

cd "Unity Sample\"
"C:\Program Files\7-Zip\7z.exe" a -r -tzip "../AssetStore Package\Assets\Sample.zip" "Assets\*" "ProjectSettings\*" "Mods\*"