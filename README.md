# ZipSlipTestApp
ZipSlipの動作確認

## 結論
DotNetZipでCVE-2024-48510の問題動作を確認できた  
SharpZipLibでは相対アドレスに注意する必要あり
DotNetZipでも、AES256で暗号化したzipファイルであれば、問題なかった
