<Query Kind="Program">
  <Reference Relative="..\FileSignatures\bin\Debug\FileSignatures.dll">C:\dev\vs.net\FileSignatures\FileSignatures\bin\Debug\FileSignatures.dll</Reference>
  <Namespace>FileSignatures</Namespace>
</Query>

void Main()
{
    var path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), @"..\", "TestFiles"));
    
    foreach (var filename in Directory.GetFiles(path))
        ContentIdentifier.Default.Identify(filename).Dump(Path.GetFileName(filename));
}