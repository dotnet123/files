echo *************************************************************

@cd /d "%~dp0"

cd ../

rd /s /Q CMD\app
dotnet clean
dotnet restore
dotnet build
dotnet publish -c Release -o CMD/app
rd /s /Q CMD\app\config
pause  

echo *************************************************************
