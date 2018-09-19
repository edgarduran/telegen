@ECHO OFF

ECHO TELEGEN 1.0 DEMO
ECHO.

ECHO Demo # 1: 
ECHO     Run script "demo.tg". 
ECHO     Write the output to "demo.output.csv". 
ECHO     Delete any existing records before the run.
dotnet telegen.dll demo.tg demo.output.csv --format=..\\Layouts\\CSV.layout --clear

ECHO Demo # 2: 
ECHO     Run script "demo.tg". 
ECHO     Write the output to "demo.output.tsv". (Tab-separated value) 
ECHO     Delete any existing records before the run.
dotnet telegen.dll demo.tg demo.output.tsv --format=..\\Layouts\\TSV.layout --clear

ECHO Demo # 3: 
ECHO     Run script "demo.tg". 
ECHO     Write the output to "demo.output.bsv". (Vertical bar-separated value) 
ECHO     Retains existing records; New headers are added with each run.
dotnet telegen.dll demo.tg demo.output.bsv --format=..\\Layouts\\BSV.layout

ECHO Demo # 4: 
ECHO     Run script "demo.tg". 
ECHO     Write the output to "demo.output.json". (No format file specified.)
ECHO     Delete any existing records before the run.
dotnet telegen.dll demo.tg demo.output.json --clear

ECHO Demos complete!