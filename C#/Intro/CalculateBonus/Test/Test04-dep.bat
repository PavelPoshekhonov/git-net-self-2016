CalculateBonus.exe 92,Петров

@echo off
if errorlevel 0  goto result0 
if errorlevel -1 goto result1
if errorlevel -2 goto result2
if errorlevel -3 goto result3
goto oneexit

:result0
echo Код завершения программы: 0 Премия успешно начислена
goto oneexit

:result1
echo Код завершения программы: -1 Не был передан обязательный (0й) аргумент
goto oneexit

:result2
echo Код завершения программы: -2 Указаный департамент не существует       
goto oneexit

:result3
echo Код завершения программы: -3 Указанная должность не существует        
goto oneexit

:oneexit
