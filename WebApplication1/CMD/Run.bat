
set appName= "orderqueue"

@echo off
for /f "tokens=2 delims==" %%a in ('wmic path win32_operatingsystem get LocalDateTime /value') do (
  set t=%%a)
set Today=%t:~0,4%%t:~4,2%%t:~6,2%
@cd /d "%~dp0"
set dateStr=%Today%
echo  -------------------- AppName=%appName% ---------------------------- 
echo  -------------------- Tag=%dateStr% --------------------
echo  -------------------- Remove Container --------------------
   docker  rm -f %appName% 
echo  -------------------- Remove Image --------------------
   docker  rmi -f %appName%:%dateStr% 
echo  -------------------- Build Image --------------------
   docker build -t %appName%:%dateStr% .
echo  -------------------- Run Image --------------------
   docker run -it --restart=always  -v c:/config/:c:/appruntime/config/ --name %appName% %appName%:%dateStr%
echo  -------------------- Success --------------------
pause  
echo *************************************************************