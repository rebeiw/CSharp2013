dim $windowClass
dim $windowHandle
dim $progName
dim $WindowTitle
$fensterClass = "[CLASS:SimViewApp]"
$progName="C:\Program Files\SIEMENS\Plcsim\s7wsi\s7wsvapx.exe"
$fileName="e:\DropBox\Dropbox\Repos\CSharp2013\PlayGround\ClassDevelopment\plcSimi"
if IsRunning($fensterClass)=0  Then
   StartProgram($progName,$fensterClass)
   Sleep(3000)
EndIf

$WindowTitle = WinGetTitle($fensterClass, "")

if StringInStr($WindowTitle , "PlcSimi") = 0 Then
   WinActivate($fensterClass)
   $fensterClass = "[CLASS:#32770]"
   Send("!sf")
   WinWait($fensterClass)
   ControlSetText($fensterClass,"","Edit1",$fileName)
   ControlClick($fensterClass, "", "Button2")
EndIf

$fensterClass="Nettoplcsim::s7o"
$progName="E:\DevEnv\Nettoplcsim-S7o-v-0-9-4\bin\NetToPLCSim.exe"
$fileName="E:\DropBox\Dropbox\Repos\CSharp2013\PlayGround\ClassDevelopment\NetToPlcSimCfg"

if IsRunning($fensterClass)=0  Then
   StartProgram($progName,$fensterClass)
   $fensterClass = "[CLASS:#32770]"
   WinWait($fensterClass,"",15)
   ControlClick($fensterClass, "", "Button1")
   WinWait($fensterClass,"",15)
   ControlClick($fensterClass, "", "Button1")
EndIf


$WindowTitle = WinGetTitle($fensterClass, "")
$fensterClass="Nettoplcsim::s7o"

if StringInStr($WindowTitle , "NetToPlcSimCfg") = 0 Then
   WinActivate($fensterClass)
   Send("!fo")
   $fensterClass = "[CLASS:#32770]"
   WinWait($fensterClass)
   ControlSetText($fensterClass,"","Edit1",$fileName)
   ControlClick($fensterClass, "", "Button1")
EndIf


$fensterClass = "[CLASS:WindowsForms10.Window.8.app.0.2bf8098_r11_ad1]"
$progName="E:\DropBox\Dropbox\Repos\CSharp2013\PlayGround\ClassDevelopment\bin\Release\ClassDevelopment.exe"
if IsRunning($fensterClass)=0  Then
   StartProgram($progName,$fensterClass)
   Sleep(5000)
   $fensterClass = "Nettoplcsim::s7o"
   ControlClick("Nettoplcsim::s7o", "", "WindowsForms10.BUTTON.app.0.378734a5")
   
   WinSetState($fensterClass,"",@SW_MINIMIZE)
   $fensterClass = "[CLASS:SimViewApp]"
   WinSetState($fensterClass,"",@SW_MINIMIZE)
   $fensterClass = "[CLASS:WindowsForms10.Window.8.app.0.2bf8098_r11_ad1]"
   ControlClick($fensterClass, "", "WindowsForms10.Window.8.app.0.2bf8098_r11_ad12")

EndIf


Func IsRunning($windowClassName)
   $windowHandle=WinExists($windowClassName,"")
   Return $windowHandle
EndFunc

Func StartProgram($progName,$windowClassName)
   Run($progName)
   WinWait($windowClassName,"",40)
EndFunc