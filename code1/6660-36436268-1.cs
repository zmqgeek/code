    <#@ template language="C#" hostspecific="True" debug="True" #>
    <#@ output extension="cs" #>
    <#@ assembly name="System.Core.dll" #>
    <#@ import namespace="System" #>
    <#@ import namespace="System.Collections.Generic" #>
    <#@ import namespace="System.IO" #>
    
    <#
    string hPath = Host.ResolveAssemblyReference("$(ProjectDir)") + "SomeDirectory";  
    string[] hFiles = System.IO.Directory.GetFiles(hPath, "*.h", System.IO.SearchOption.AllDirectories);
    var namespaceName = System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("NamespaceHint");
    #>
    //------------------------------------------------------------------------------
    //     This code was generated by template for T4
    //     Generated at <#=DateTime.Now#>
    //------------------------------------------------------------------------------
    
    namespace <#=namespaceName#>
    {
    <#foreach (string input_file in hFiles)
    {
    StreamReader defines = new StreamReader(input_file);
    #>
        public class <#=System.IO.Path.GetFileNameWithoutExtension(input_file)#>
        {
    <#    // constants definitions
    
        while (defines.Peek() >= 0)
        {
            string def = defines.ReadLine();
            string[] parts;
            if (def.Length > 3 && def.StartsWith("#define"))
            {
    			def = def.TrimEnd(';');
                parts = def.Split(null as char[], StringSplitOptions.RemoveEmptyEntries);
    			Int32 intVal;
    			double dblVal;
    			if (Int32.TryParse(parts[2], out intVal))
    			{
    			#>
            public static readonly int <#=parts[1]#> = <#=parts[2]#>;			
    <#
    			}
    			else if (Double.TryParse(parts[2], out dblVal))
    			{
    			#>
            public static readonly double <#=parts[1]#> = <#=parts[2]#>;			
    <#
    			}
    			else
    			{
    			#>
            public static readonly string <#=parts[1]#> = "<#=parts[2]#>";
    <#			
    			}
            }
        } #>
    	}
    <#}#>     
    }
