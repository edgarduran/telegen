@ECHO OFF

ECHO TELEGEN 1.0 DEMO for WINDOWS
ECHO.

dotnet telegen.dll /help | more

ECHO Demo # 1: 
ECHO     Run script "demo.json". 
ECHO     Pipe the output to more. 
dotnet telegen.dll <./demo.json | more

ECHO Demo # 2: 
ECHO     Run script "demo.json". 
ECHO     Write the output to "demo.output.json".
dotnet telegen.dll <./demo.json >./demo.output.json

ECHO Demo # 3: 
ECHO     Run script "demo.json".
ECHO     Write the unformatted output to the screen.
dotnet telegen.dll /raw <./demo.json

ECHO Demos complete!