using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("UnitTests")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("UnitTests")]
[assembly: AssemblyCopyright("Copyright ©  2023")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("febcdcc1-eff8-4b97-8c68-600aa9ab9bfa")]

// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Test parallelization
[assembly: Parallelize(Workers = 5, Scope = Microsoft.VisualStudio.TestTools.UnitTesting.ExecutionScope.MethodLevel)]
