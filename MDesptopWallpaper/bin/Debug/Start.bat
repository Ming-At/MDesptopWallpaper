@echo off 
if "%1" == "h" goto label 
mshta vbscript:createobject("wscript.shell").run("""%~nx0"" h",0)(window.close)&&exit 
:label
MDesptopWallpaper.exe 0 C:\Windows\Web\Wallpaper\Theme1

::	���ִ�У�˫��Start.bat���ɣ�������
::	"MDesptopWallpaper.exe 0 C:\Windows\Web\Wallpaper\Theme1" 
::	��һ��������Ͷ�ŵ���Ļ  �ڶ���������ͼƬ·��(�����Լ�����һ���ļ��з���Ҫ��ʾ��ͼƬ Ȼ����ļ�·����Ϊ�Լ����ļ���·��)

::	������ÿ����Զ�ִ��
::	�Ҽ�����Start.bat�ļ��Ŀ�ݷ�ʽ Ȼ��������ݷ�ʽ '����' ��'C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp'���·���� ɱ����������� ѡ���������