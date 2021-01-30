# HL7Sender

## Install and Run

Install VSCode and do dotnet core. You can run dotnet command like this below against the .dll. Probably windows can do .exe.

or just change parameters in the program and hit run button.

```
dotnet restore
dotnet publish
dotnet bin/Debug/netcoreapp3.1/HL7Sender.dll sample.hl7 127.0.0.1 7777
```

```
dotnet HL7Sender.dll [FILE PATH] [IP ADDRESS] [PORT]
```

## About HL7

HL7 is really simple message based on text that works like this:

```
(Vertical Tab) - header, indicates start of contents
(actual contents of HL7)
(File Separator)(Carriage Return) - Terminator, this will tell the system it's end of file. you should get ACK 
```

e.g.

```
0x0b
MSH|^~\&|ADT1|MCM|LABADT|MCM|198808181126|SECURITY|ADT^A04|MSG00001|P|2.4
EVN|A01-|198808181123
PID|||PATID1234^5^M11||JONES^DOE^A^III||19610615|M-||2106-3|1200 N ELM STREET^^GREENSBORO^NC^27401-1020|GL|(919)379-1212|(919)271-3434~(919)277-3114||S||PATID12345001^2^M10|123456789|9-87654^NC
0x1c
0x0d
```



