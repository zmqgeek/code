    <#@ template debug="true" hostspecific="true" language="C#" #>
    <#@ output extension=".cs" #>
    <#@ assembly name="System.Windows.Forms" #>
    <#@ import namespace="System.IO" #>
    <#@ import namespace="System.Text.RegularExpressions" #>
    
    <#
    	bool incRevision = false;
    	bool incBuild = false;
    
    	try { incRevision = Convert.ToBoolean(this.Host.ResolveParameterValue("","","revision")); } catch( Exception ) { }
    	try	{ incBuild = Convert.ToBoolean(this.Host.ResolveParameterValue("","","build")); } catch( Exception ) { }
    	try {
    		string currentDirectory = Path.GetDirectoryName(Host.TemplateFile);
    		string assemblyInfo = File.ReadAllText(Path.Combine(currentDirectory,"AssemblyInfo.cs"));
    		Regex pattern = new Regex("AssemblyVersion\\(\"\\d+\\.\\d+\\.(?<revision>\\d+)\\.(?<build>\\d+)\"\\)");
    		MatchCollection matches = pattern.Matches(assemblyInfo);
    		revision = Convert.ToInt32(matches[0].Groups["revision"].Value) + (incRevision?1:0);
    		build = Convert.ToInt32(matches[0].Groups["build"].Value) + (incBuild?1:0);
    	}
    	catch( Exception ) { }
    #>
    
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Resources;
    
    // General Information
    [assembly: AssemblyTitle("Insert title here")]
    [assembly: AssemblyDescription("Insert description here")]
    [assembly: AssemblyConfiguration("")]
    [assembly: AssemblyCompany("Insert company here")]
    [assembly: AssemblyProduct("Insert product here")]
    [assembly: AssemblyCopyright("Insert copyright here")]
    [assembly: AssemblyTrademark("Insert trademark here")]
    [assembly: AssemblyCulture("")]
    
    // Version informationr(
    [assembly: AssemblyVersion("1.0.<#= this.revision #>.<#= this.build #>")]
    [assembly: AssemblyFileVersion("1.0.<#= this.revision #>.<#= this.build #>")]
    [assembly: NeutralResourcesLanguageAttribute( "en-US" )]
    
    <#+
    	int revision = 0;
    	int build = 0;
    #>
