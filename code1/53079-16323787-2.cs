    <#@ template debug="true" hostspecific="true" language="C#" #>
    <#@ output extension=".cs" #>
    <#@ assembly name="System.Windows.Forms" #>
    <#@ import namespace="System.IO" #>
    <#@ import namespace="System.Text.RegularExpressions" #>
    <#
        int incRevision = 1;
        int incBuild = 1;
    
        try { incRevision = Convert.ToInt32(this.Host.ResolveParameterValue("","","Debug"));} catch( Exception ) { incBuild=0; }
        try { incBuild = Convert.ToInt32(this.Host.ResolveParameterValue("","","Release")); } catch( Exception ) { incRevision=0; }
        try {
            string currentDirectory = Path.GetDirectoryName(Host.TemplateFile);
            string assemblyInfo = File.ReadAllText(Path.Combine(currentDirectory,"AssemblyInfo.cs"));
            Regex pattern = new Regex("AssemblyVersion\\(\"\\d+\\.\\d+\\.(?<revision>\\d+)\\.(?<build>\\d+)\"\\)");
            MatchCollection matches = pattern.Matches(assemblyInfo);
            revision = Convert.ToInt32(matches[0].Groups["revision"].Value) + incRevision;
            build = Convert.ToInt32(matches[0].Groups["build"].Value) + incBuild;
        }
        catch( Exception ) { }
    #>
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    
    // General Information about an assembly is controlled through the following
    // set of attributes. Change these attribute values to modify the information
    // associated with an assembly.
    [assembly: AssemblyTitle("Game engine. Keys: F2 (Debug trace), F4 (Fullscreen), Shift+Arrows (Move view). ")]
    [assembly: AssemblyProduct("Game engine")]
    [assembly: AssemblyDescription("My engine for game")]
    [assembly: AssemblyCompany("")]
    [assembly: AssemblyCopyright("Copyright © Name 2013")]
    [assembly: AssemblyTrademark("")]
    [assembly: AssemblyCulture("")]
    
    // Setting ComVisible to false makes the types in this assembly not visible
    // to COM components.  If you need to access a type in this assembly from
    // COM, set the ComVisible attribute to true on that type. Only Windows
    // assemblies support COM.
    [assembly: ComVisible(false)]
    
    // On Windows, the following GUID is for the ID of the typelib if this
    // project is exposed to COM. On other platforms, it unique identifies the
    // title storage container when deploying this assembly to the device.
    [assembly: Guid("00000000-0000-0000-0000-000000000000")]
    
    // Version information for an assembly consists of the following four values:
    //
    //      Major Version
    //      Minor Version
    //      Build Number
    //      Revision
    //
    [assembly: AssemblyVersion("0.1.<#= this.revision #>.<#= this.build #>")]
    [assembly: AssemblyFileVersion("0.1.<#= this.revision #>.<#= this.build #>")]
    
    <#+
        int revision = 0;
        int build = 0;
    #>
