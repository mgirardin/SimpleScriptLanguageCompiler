using System;
using System.IO;

var fileContent = File.ReadAllText(args[0]);
Console.WriteLine(fileContent);