# RenameProj

**IMPORTANT: You must rename the project directory to the New Project Name.**

RenameProj.exe *[ProjectDirectory]* *[SystemKey]* *[OldSystemKey]*");  


*[ProjectDirectory]* = The FULL root directory of the Project files.  

*[SystemKey]* [OPTIONAL] = The SystemKey.  In the case of a RocketSystem project this will be the same as *[NewProjectName]*.  In the case of a plugin it will be the systemkey of the plugin parent system.  default = 'Project Directory Name'  

*[OldSystemKey]* [OPTIONAL] = The SystemKey to be replace in the project files. default = 'RocketSystemProjectTemplate'  

Example of system..
```
RenameProj.exe D:\Nevoweb\Projects\DesktopModules\DNNrocketModules\RocketNewSystem 
```

Example of plugin..
```
RenameProj.exe D:\Nevoweb\Projects\DesktopModules\DNNrocketModules\RocketIntraSqlReports rocketintra
```

**NOTE: The new Project Name is taken from the Directory name.  This is used as the namespace root.**

