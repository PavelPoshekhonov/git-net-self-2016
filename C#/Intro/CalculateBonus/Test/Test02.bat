CalculateBonus.exe 01,���஢

@echo off
if errorlevel 0  goto result0 
if errorlevel -1 goto result1
if errorlevel -2 goto result2
if errorlevel -3 goto result3
goto oneexit2

:result0
echo ��� �����襭�� �ணࠬ��: 0 �६�� �ᯥ譮 ���᫥��
goto oneexit2

:result1
echo ��� �����襭�� �ணࠬ��: -1 �� �� ��।�� ��易⥫�� (0�) ��㬥��
goto oneexit2

:result2
echo ��� �����襭�� �ணࠬ��: -2 ������� �����⠬��� �� �������       
goto oneexit2

:result3
echo ��� �����襭�� �ணࠬ��: -3 ��������� ��������� �� �������        
goto oneexit2

:oneexit2