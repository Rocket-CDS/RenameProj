using RenameProj;
using System.IO;
using System.Text;

if (Environment.GetCommandLineArgs().Length > 2)
{
    var oldSystemKey = "RocketSystemProjectTemplate";

    string projectDir = Environment.GetCommandLineArgs()[1];
    var newProjectName = new DirectoryInfo(projectDir).Name;
    var newSystemKey = newProjectName;

    if (Environment.GetCommandLineArgs().Length > 2) newSystemKey = Environment.GetCommandLineArgs()[2];
    if (Environment.GetCommandLineArgs().Length > 3) oldSystemKey = Environment.GetCommandLineArgs()[3];

    Console.WriteLine("IMPORTANT: You must rename the project directory to the NEW Project Name.");
    Console.WriteLine("");
    Console.WriteLine("Project Directory = " + projectDir);
    Console.WriteLine("New Project Name = " + newProjectName);
    Console.WriteLine("New SystemKey = " + newSystemKey);
    Console.WriteLine("Old SystemKey = " + oldSystemKey);
    Console.WriteLine("");
    Console.WriteLine("WARNING: The files in the '" + projectDir + "' project directory will be changed.");
    Console.WriteLine("");
    Console.WriteLine("Continue (Y or N)?");

    if (projectDir.Contains("_"))
    {
        Console.WriteLine("ERROR: Do NOT use an underscore ( _ ) in the new project name.  Change the directory name. ");
    }
    else
    {
        if (Console.ReadKey(false).Key == ConsoleKey.Y)
        {
            // Get Project Name
            var projectName = "";
            foreach (var f in Directory.GetFiles(projectDir, "*.csproj"))
            {
                projectName = Path.GetFileNameWithoutExtension(f);
            }
            Console.WriteLine($"projectName, {projectName}");

            // Rename Project text in files.
            var eo = new EnumerationOptions();
            eo.RecurseSubdirectories = true;
            foreach (var f in Directory.GetFiles(projectDir, "*.*", eo))
            {
                if (!f.StartsWith(projectDir.TrimEnd('\\') + "\\packages\\") && !f.StartsWith(projectDir.TrimEnd('\\') + "\\bin\\") && !f.StartsWith(projectDir.TrimEnd('\\') + "\\obj\\"))
                {
                if (!f.StartsWith(projectDir + "obj") && !f.StartsWith(projectDir + "bin"))
                {
                    if (Path.GetExtension(f) == ".cs"
                        || Path.GetExtension(f) == ".xml"
                        || Path.GetExtension(f) == ".csproj"
                        || Path.GetExtension(f) == ".sln"
                        || Path.GetExtension(f) == ".dnn"
                        || Path.GetExtension(f) == ".md"
                        || Path.GetExtension(f) == ".json"
                        || Path.GetExtension(f) == ".dnnpack"
                        || Path.GetExtension(f) == ".rules"
                        || Path.GetExtension(f) == ".resx"
                        || Path.GetExtension(f) == ".txt"
                        || Path.GetExtension(f) == ".SqlDataProvider"
                        || Path.GetExtension(f) == ".cshtml"
                        )
                    {

                        var content = FileUtils.ReadFile(f);

                        content = content.Replace(oldSystemKey, newSystemKey, false, new System.Globalization.CultureInfo("en-US"));
                        content = content.Replace(oldSystemKey.ToLower(), newSystemKey.ToLower(), false, new System.Globalization.CultureInfo("en-US"));

                        content = content.Replace(projectName, newProjectName, false, new System.Globalization.CultureInfo("en-US"));
                        content = content.Replace(projectName.ToLower(), newProjectName.ToLower(), false, new System.Globalization.CultureInfo("en-US"));


                        FileUtils.SaveFile(f, content);

                        Console.WriteLine(f);

                    }
                }
                }
            }
            // Rename Files
            var projFile = projectDir.TrimEnd('\\') + "\\" + projectName + ".csproj";
            var newProjFile = projectDir.TrimEnd('\\') + "\\" + newProjectName + ".csproj";
            if (File.Exists(projFile)) File.Move(projFile, newProjFile);

            var projFilednn = projectDir.TrimEnd('\\') + "\\" + projectName + ".dnn";
            var newProjFilednn = projectDir.TrimEnd('\\') + "\\" + newProjectName + ".dnn";
            if (File.Exists(projFilednn)) File.Move(projFilednn, newProjFilednn);

            var projFilesln = projectDir.TrimEnd('\\') + "\\" + projectName + ".sln";
            var newProjFilesln = projectDir.TrimEnd('\\') + "\\" + newProjectName + ".sln";
            if (File.Exists(projFilesln)) File.Move(projFilesln, newProjFilesln);

            var projFileToken = projectDir.TrimEnd('\\') + "\\Render\\" + projectName + "Tokens.cs";
            var newProjFileToken = projectDir.TrimEnd('\\') + "\\Render\\" + newProjectName + "Tokens.cs";
            if (File.Exists(projFileToken)) File.Move(projFileToken, newProjFileToken);

            //[TODO:] The resx files may have multiple languages, we should loop through them and rename as require.
            var projFileresx = projectDir.TrimEnd('\\') + "\\App_LocalResources\\" + projectName + ".resx";
            var newProjFileresx = projectDir.TrimEnd('\\') + "\\App_LocalResources\\" + newProjectName + ".resx";
            if (File.Exists(projFileresx)) File.Move(projFileresx, newProjFileresx);
        }
    }
}
else
{
    Console.WriteLine("Invalid Arguments for Application. 2 arguments are required.");
    Console.WriteLine("----------------------------------------------------------");
    Console.WriteLine("");
    Console.WriteLine("RenameProj.exe <ProjectDirectory> <SystemKey> <OldSystemKey>");
    Console.WriteLine("");
    Console.WriteLine("<ProjectDirectory> = The root directory of the Project files.");
    Console.WriteLine("<SystemKey> = The SystemKey, in the case of a RocketSystem project this will be the same as <NewProjectName>.");
    Console.WriteLine("<OldSystemKey> [OPTIONAL] = The SystemKey to be replace in the project files. default = 'RocketSystemProjectTemplate' ");
    Console.WriteLine("");
    Console.WriteLine("IMPORTANT: You must rename the project directory to the New Project Name.");
    Console.WriteLine("----------------------------------------------------------");

}
