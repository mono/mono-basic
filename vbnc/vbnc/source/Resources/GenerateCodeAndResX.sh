rm Errors.resx
mono ../../../msg-generator/msg-gen.exe /mergesource:Source.xml  /outvb:../General/Messages.vb /outresx:Errors.resx
set-lf Errors.resx
dos2unix ../General/Messages.vb