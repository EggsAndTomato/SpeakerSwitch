@echo off
echo ========================================
echo       SpeakerSwitch 发布工具
echo ========================================
echo.
echo 请选择发布类型：
echo 1. 依赖框架版本 (小文件，需要.NET运行时)
echo 2. 独立版本 (大文件，包含运行时)
echo 3. 单文件版本 (推荐，便于分发)
echo.
set /p choice=请输入选择 (1-3): 

if "%choice%"=="1" goto framework
if "%choice%"=="2" goto standalone
if "%choice%"=="3" goto singlefile
echo 无效选择，默认使用单文件版本
goto singlefile

:framework
echo.
echo 正在发布依赖框架版本...
dotnet publish -c Release -r win-x64 --self-contained false -o publish
echo 发布完成！文件位于: publish\SpeakerSwitch.exe
echo 注意：目标机器需要安装.NET 9.0运行时
goto end

:standalone
echo.
echo 正在发布独立版本...
dotnet publish -c Release -r win-x64 --self-contained true -o publish-standalone
echo 发布完成！文件位于: publish-standalone\SpeakerSwitch.exe
echo 注意：文件较大但可在任何Windows机器上运行
goto end

:singlefile
echo.
echo 正在发布单文件版本...
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish-single
echo 发布完成！文件位于: publish-single\SpeakerSwitch.exe
echo 推荐：单个exe文件，便于分发和使用
goto end

:end
echo.
echo ========================================
echo 发布完成！您可以将exe文件复制到任何位置使用。
echo 建议创建桌面快捷方式或设置快捷键。
echo ========================================
pause