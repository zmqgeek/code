    <#@ template language="C#" hostspecific="true" #>
    // 
    // This code was generated by a tool. Any changes made manually will be lost
    // the next time this code is regenerated.
    // 
    
    using System.Reflection;
    
    [assembly: AssemblyVersion("4.<#= this.RevisionYear #>.<#= this.RevisionNumber #>.<#= this.RevisionTime #>")]
    [assembly: AssemblyFileVersion("4.<#= this.RevisionYear #>.<#= this.RevisionNumber #>.<#= this.RevisionTime #>")]
    <#+
        int RevisionYear = DateTime.UtcNow.Year;
        int RevisionNumber = (int)(DateTime.UtcNow - new DateTime(DateTime.UtcNow.Year,1,1)).TotalDays;
    	int RevisionTime = (int)(DateTime.UtcNow - new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day)).TotalMinutes;
    #>