		<#@ template debug="true" hostspecific="true" language="C#" #>
		<#@ output extension=".cs" #>
		<#@ import namespace="System.IO" #>
		<#@ import namespace="System.Text.RegularExpressions" #>
		<#
			string output = File.ReadAllText(this.Host.ResolvePath("AssemblyInfo.cs"));
			Regex pattern = new Regex("AssemblyVersion\\(\"(?<major>\\d+)\\.(?<minor>\\d+)\\.(?<revision>\\d+)\\.(?<build>\\d+)\"\\)");
			MatchCollection matches = pattern.Matches(output);
			if( matches.Count == 1 )
			{
				major = Convert.ToInt32(matches[0].Groups["major"].Value);
				minor = Convert.ToInt32(matches[0].Groups["minor"].Value);
				build = Convert.ToInt32(matches[0].Groups["build"].Value) + 1;
				revision = Convert.ToInt32(matches[0].Groups["revision"].Value);
				if( this.Host.ResolveParameterValue("-","-","BuildConfiguration") == "Release" )
					revision++;
			}
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
			int major = 1;
			int minor = 0;
			int revision = 0;
			int build = 0;
		#>
