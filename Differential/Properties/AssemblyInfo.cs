﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("NumericalLibraries Differential")]
[assembly: AssemblyDescription(@"Easy to use library for computing differential equations.

var math = new Rychusoft.NumericalLibraries.Differential.Differential(""3*x^2-5"");
var result = math.ComputeDifferential(4, 2, 0); // result = 46")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Rychusoft")]
[assembly: AssemblyProduct("NumericalLibraries")]
[assembly: AssemblyCopyright("© Pawel Rychlicki 2012-2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("57674ea6-dab7-4458-b0fa-dfb0808e9459")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.*")]
