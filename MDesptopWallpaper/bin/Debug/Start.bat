@echo off 
if "%1" == "h" goto label 
mshta vbscript:createobject("wscript.shell").run("""%~nx0"" h",0)(window.close)&&exit 
:label
MDesptopWallpaper.exe 0 C:\Windows\Web\Wallpaper\Theme1

::	如何执行（双击Start.bat即可）和配置
::	"MDesptopWallpaper.exe 0 C:\Windows\Web\Wallpaper\Theme1" 
::	第一个参数是投放到屏幕  第二个参数是图片路径(可以自己创建一个文件夹放入要显示的图片 然后把文件路径改为自己的文件夹路径)

::	如何设置开机自动执行
::	右键创建Start.bat文件的快捷方式 然后把这个快捷方式 '剪切' 到'C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp'这个路径下 杀毒软件跳出来 选择允许操作