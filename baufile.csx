// parameters
var ci = Environment.GetEnvironmentVariable("CI");
var versionSuffix = Environment.GetEnvironmentVariable("VERSION_SUFFIX");
var msBuildFileVerbosity = (BauMSBuild.Verbosity)Enum.Parse(typeof(BauMSBuild.Verbosity), Environment.GetEnvironmentVariable("MSBUILD_FILE_VERBOSITY") ?? "minimal", true);
var nugetVerbosity = Environment.GetEnvironmentVariable("NUGET_VERBOSITY") ?? "quiet";

// solution specific variables
var version = File.ReadAllText("src/CommonAssemblyInfo.cs").Split(new[] { "AssemblyInformationalVersion(\"" }, 2, StringSplitOptions.None).ElementAt(1).Split(new[] { '"' }).First();
var nugetCommand = "scriptcs_packages/NuGet.CommandLine.2.8.3/tools/NuGet.exe";
var solution = "NServiceBus.ScriptCs.sln";
var output = "artifacts/output";
var logs = "artifacts/logs";
var packs = new[] { "src/NServiceBus.ScriptCs.ServiceControl/NServiceBus.ScriptCs.ServiceControl.csproj", };

// solution agnostic tasks
var bau = Require<Bau>();

bau
.Task("default").DependsOn("pack")

.Task("logs").Do(() => CreateDirectory(logs))

.MSBuild("clean").DependsOn("logs").Do(msb =>
    {
        msb.MSBuildVersion = "net45";
        msb.Solution = solution;
        msb.Targets = new[] { "Clean", };
        msb.Properties = new { Configuration = "Release" };
        msb.MaxCpuCount = -1;
        msb.NodeReuse = false;
        msb.Verbosity = msBuildFileVerbosity;
        msb.NoLogo = true;
        msb.FileLoggers.Add(
            new FileLogger
            {
                FileLoggerParameters = new FileLoggerParameters
                {
                    PerformanceSummary = true,
                    Summary = true,
                    Verbosity = BauMSBuild.Verbosity.Normal,
                    LogFile = logs + "/clean.log",
                }
            });
    })

.Task("clobber").DependsOn("clean").Do(() => DeleteDirectory(output))

.Exec("restore").Do(exec => exec
    .Run(nugetCommand).With("restore", solution))

.MSBuild("build").DependsOn("clean", "restore", "logs").Do(msb =>
    {
        msb.MSBuildVersion = "net45";
        msb.Solution = solution;
        msb.Targets = new[] { "Build", };
        msb.Properties = new { Configuration = "Release" };
        msb.MaxCpuCount = -1;
        msb.NodeReuse = false;
        msb.Verbosity = msBuildFileVerbosity;
        msb.NoLogo = true;
        msb.FileLoggers.Add(
            new FileLogger
            {
                FileLoggerParameters = new FileLoggerParameters
                {
                    PerformanceSummary = true,
                    Summary = true,
                    Verbosity = BauMSBuild.Verbosity.Normal,
                    LogFile = logs + "/build.log",
                }
            });
    })

.Task("output").Do(() => CreateDirectory(output))

.Task("pack").DependsOn("clobber", "build", "output").Do(() =>
    {
        foreach (var pack in packs)
        {
            bau.CurrentTask.LogInfo("Packing '" + pack + "'...");
            new Exec { Name = "pack " + pack }
                .Run(nugetCommand)
                .With(
                    "pack", pack,
                    "-OutputDirectory", output,
                    "-Properties", "Configuration=Release",
                    "-IncludeReferencedProjects",
                    "-Verbosity", nugetVerbosity,
                    "-Version", version)
                .Execute();
        }
    })

.Exec("sample").DependsOn("pack").Do(exec => exec
    .Run("scriptcs").With("sample.csx").In("samples"))

.Run();

void CreateDirectory(string name)
{
    if (!Directory.Exists(name))
    {
        Directory.CreateDirectory(name);
        System.Threading.Thread.Sleep(100); // HACK (adamralph): wait for the directory to be created
    }
}

void DeleteDirectory(string name)
{
    if (Directory.Exists(name))
    {
        Directory.Delete(name, true);
    }
}
