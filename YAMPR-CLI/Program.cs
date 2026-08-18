using System;
using YAMPR_LIB;

string mpoPath = "";
string outputMpoPath = "";
string jsonPath = "";

if (args.Length < 3)
{
    Console.WriteLine("Insufficient arguments!");
    Console.WriteLine("Usage: ./YAMPR [path-to-original-data-file] [path-to-output-data-file] [path-to-json-file]");
    return -1;
}


mpoPath = args[0];
outputMpoPath = args[1];
jsonPath = args[2];

Patcher.Main(mpoPath, outputMpoPath, jsonPath);
return 0;