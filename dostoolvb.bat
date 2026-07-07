::[Bat To Exe Converter]
::
::YAwzoRdxOk+EWAjk
::fBw5plQjdCyDJGyX8VAjFDNbWReHAE+/Fb4I5/jHy+WUlkISWNQdRIbY1brAKeMcig==
::YAwzuBVtJxjWCl3EqQJgSA==
::ZR4luwNxJguZRRnk
::Yhs/ulQjdF+5
::cxAkpRVqdFKZSjk=
::cBs/ulQjdF+5
::ZR41oxFsdFKZSDk=
::eBoioBt6dFKZSDk=
::cRo6pxp7LAbNWATEpCI=
::egkzugNsPRvcWATEpCI=
::dAsiuh18IRvcCxnZtBJQ
::cRYluBh/LU+EWAnk
::YxY4rhs+aU+IeA==
::cxY6rQJ7JhzQF1fEqQJhZksaHErSXA==
::ZQ05rAF9IBncCkqN+0xwdVsFAlTMbCXqZg==
::ZQ05rAF9IAHYFVzEqQIUJwhgQwuOCkna
::eg0/rx1wNQPfEVWB+kM9LVsJDCCNL1+1Cbkqyqb+9+/n
::fBEirQZwNQPfEVWB+kM9LVsJDCCNL1+1Cbkqyog=
::cRolqwZ3JBvQF1fEqQIRaAtGQwOQPWb6ALoOqOm74uOJqw05e9F/eZvP27eFYOIKqlPmepc5mBo=
::dhA7uBVwLU+EWE+G+0MkSA==
::YQ03rBFzNR3SWATE4kA/KQ80
::dhAmsQZ3MwfNWATE4kA/KQ8YDFbSbj3a
::ZQ0/vhVqMQ3MEVWAtB9wSA==
::Zg8zqx1/OA3MEVWAtB9wSA==
::dhA7pRFwIByZRRmo/UE1JghRSESXMm+/FPUK6uf6+6qLq04YWvE6Nu8=
::Zh4grVQjdCyDJGyX8VAjFDNbWReHAE+/Fb4I5/jHy+WUlkISWNQdRKvUyYCBL+wlyHnAeoUZ2XVWrulCCQNdHg==
::YB416Ek+ZG8=
::
::
::978f952a14a936cc963da21a135fa983
@echo off
chcp 65001 >nul
title DosToolVB
cls
title DosToolVB - check license
if not exist "license.rtf" (
    cls
    title DosToolVB - check license
    echo ================================
    echo        ОШИБКА ЛИЦЕНЗИИ
    echo ================================
    echo Файл license.rtf не найден!
    echo Пожалуйста, убедитесь, что файл лицензии
    echo находится в той же папке, что и программа.
    echo ================================
    pause
    exit /b
)
:menu
title DosToolVB - main
echo ================================
echo            МЕНЮ
echo       Dos Tool v 1.0
echo          Optimized
echo ================================
echo 1. Отправка 1400 пакетов на google.com
echo 2. Отправка 1400 пакетов на pulsevisuals.pro
echo 3. Отправка 1400 пакетов на youtube.com
echo 4. Отправка 1400 пакетов на github.com
echo 5. Отправка 1400 пакетов на свой сайт
echo 0. Выход
echo ================================
set /p choice="Выберите пункт: "
if "%choice%"=="0" goto exit
if "%choice%"=="1" goto prog1
if "%choice%"=="2" goto prog2
if "%choice%"=="3" goto prog3
if "%choice%"=="4" goto prog4
if "%choice%"=="5" goto prog5
echo Неверный выбор.
timeout /t 2 > nul
cls
goto menu
:prog1
cls
title DosToolVB - google.com
echo Пингую google.com...
ping google.com -t -l 1400
pause
goto menu
:prog2
cls
title DosToolVB - pulsevisuals.pro
echo Пингую pulsevisuals.pro...
ping pulsevisuals.pro -t -l 1400
pause
goto menu
:prog3
cls
title DosToolVB - youtube.com
echo Пингую youtube.com...
ping youtube.com -t -l 1400
pause
goto menu
:prog4
cls
title DosToolVB - github.com
echo Пингую github.com...
ping github.com -t -l 1400
pause
goto menu
:prog5
cls
title DosToolVB - select
echo ================================
echo   ВВЕДИТЕ САЙТ ДЛЯ ПИНГА
echo ================================
set /p site="Введите URL (например, google.com): "
if "%site%"=="" (
    echo Вы ничего не ввели!
    timeout /t 2 >nul
    goto menu
)
cls
title DosToolVB - %site%
echo Пингую %site%...
ping %site% -t -l 1400
pause
cls
goto menu
:exit
cls
echo Выход...
timeout /t 1 >nul
exit /b