using Ionic.Zip;
using ZipEntry = ICSharpCode.SharpZipLib.Zip.ZipEntry;
using ZipInputStream = ICSharpCode.SharpZipLib.Zip.ZipInputStream;
using DotNetZip_ZipFile = Ionic.Zip.ZipFile;


var zipFileName = "windows_zipslip_test.zip";

//解凍サンプル１(無対策)
Console.WriteLine("SharpZipLib1");
string extractDir = "unsafe_extracted1";
Directory.CreateDirectory(extractDir);
using (FileStream fs = File.OpenRead(zipFileName))
using (ZipInputStream zipStream = new ZipInputStream(fs))
{
    ZipEntry entry;
    while ((entry = zipStream.GetNextEntry()) != null)
    {
        // 展開先パス（ZipEntry名そのまま使う）
        string entryPath = Path.Combine(extractDir, entry.Name);

        try
        {
            using FileStream outFile = File.Create(entryPath);
            zipStream.CopyTo(outFile);
        }
        catch
        {
        }
    }
    check();
}

//解凍サンプル２(無対策)
Console.WriteLine("DotNetZip1");
extractDir = Path.GetFullPath("unsafe_extracted_dotnetzip1");
Directory.CreateDirectory(extractDir);
using (var zip = DotNetZip_ZipFile.Read(zipFileName))
{
    foreach (var entry in zip)
    {
        // ファイル書き出し
        entry.Extract(extractDir, ExtractExistingFileAction.OverwriteSilently);
    }
    check();
}

//解凍サンプル３(無対策)
Console.WriteLine("DotNetZip2");
extractDir = Path.GetFullPath("unsafe_extracted_dotnetzip2");
Directory.CreateDirectory(extractDir);
using (var zip = DotNetZip_ZipFile.Read(zipFileName))
{
    zip.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
    zip.ExtractAll(extractDir);
    check();
}

//エラーチェック
Console.WriteLine("DotNetZip_errorCatch");
extractDir = Path.GetFullPath("unsafe_extracted_dotnetzip3");
Directory.CreateDirectory(extractDir);
using (var zip = DotNetZip_ZipFile.Read(zipFileName))
{
    foreach (var entry in zip)
    {
        try
        {
            var destFileName = Path.GetFullPath(Path.Combine(extractDir, entry.FileName));
            string fullDestDirPath = Path.GetFullPath(extractDir + Path.DirectorySeparatorChar);
            if (!destFileName.StartsWith(fullDestDirPath))
            {
                throw new Exception("Entry is outside of the target dir: " + destFileName);
            }

            // 問題ない場合のみファイル書き出し
            entry.Extract(extractDir, ExtractExistingFileAction.OverwriteSilently);
        }
        catch(Exception ex) 
        {
            Console.WriteLine($"Error extracting {entry.FileName}: {ex.Message}");
        }
    }
    check();
}

static void check()
{
    if (File.Exists("evil1.txt"))
    {
        Console.WriteLine("Error1");
        File.Delete(@"evil1.txt");
    }
    if (File.Exists("evil2.txt"))
    {
        Console.WriteLine("Error2");
        File.Delete(@"evil2.txt");
    }
    if (File.Exists(@"C:\Windows\Temp\evil3.txt"))
    {
        Console.WriteLine("Error3");
        File.Delete(@"C:\Windows\Temp\evil3.txt");
    }
    if (File.Exists(@"C:\Windows\Temp\evil4.txt"))
    {
        Console.WriteLine("Error4");
        File.Delete(@"C:\Windows\Temp\evil4.txt");
    }
}