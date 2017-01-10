@echo off

set Disk=C

if "%computername%"=="DESKTOP-FPO064E" (
	set Disk=C
)
if "%computername%"=="DESKTOP-P03RPRT" (
	set Disk=D
)
if "%computername%"=="VINCENT-PC" (
	set Disk=D
)
if "%computername%"=="VINCENT-PC" (
	set Disk=D
)
if "%computername%"=="DESKTOP-O2RI4RV" (
	set Disk=D
)


for /d %%i in (*) do (
 echo %%i
 del "%%i.tar" >nul 2>&1
 del "%%i.zip" >nul 2>&1
 "%Disk%:\Program Files\7-Zip\7z.exe" a -tzip "%%i.zip" ".\%%i\*"
)



goto comment

Example git ".git/hooks/post-merge" file


#!/bin/sh
cd "$GIT_DIR/../Unity/Mods/"
start "pack-all.bat" "" > /dev/null


:comment

exit