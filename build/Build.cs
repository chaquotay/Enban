using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DocFX;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DocFX.DocFXTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    [Parameter("nuget.org API key", Name = "apikey")] string ApiKey;
    [Parameter("serve docfx documentation", Name = "docserve")] bool DocServe;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath TestResultsDirectory = RootDirectory / "output" / "test-results";
    AbsolutePath DocFXDirectory => RootDirectory / "doc";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetResultsDirectory(TestResultsDirectory)
                .SetLoggers("xunit")
                .SetProjectFile(Solution.GetProject("Enban.Test")));
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetOutputDirectory(OutputDirectory)
                .SetConfiguration(Configuration)
                .SetProject(Solution.GetProject("Enban")));
        });

    Target Doc => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DocFXMetadata(s => s
                .SetProjects(Solution.GetProject("Enban"))
                .SetOutputFolder(DocFXDirectory / "api"));
            
            DocFXBuild(s => s
                .SetConfigFile(DocFXDirectory / "docfx.json")
                .SetOutputFolder(OutputDirectory / "doc")
                .SetServe(DocServe));
        });

    Target Push => _ => _
        .DependsOn(Pack)
        .OnlyWhenDynamic(() => !string.IsNullOrEmpty(ApiKey))
        .Executes(() =>
        {
            var packages = OutputDirectory.GlobFiles("*.nupkg");
            DotNetNuGetPush(s => s
                .SetSource("https://api.nuget.org/v3/index.json")
                .SetApiKey(ApiKey)
                .CombineWith(packages, (s, package) => s.SetTargetPath(package)));
        });

}
