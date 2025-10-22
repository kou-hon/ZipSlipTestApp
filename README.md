# ZipSlipTestApp
ZipSlipの動作確認

## 結論
DotNetZipでCVE-2024-48510の問題動作を確認できた  
SharpZipLibでは相対アドレスに注意する必要あり  
AES256で暗号化したzipファイルでも挙動は変わらず

DotNetZipの場合、絶対アドレスのパスが存在しているか事前チェックが必要

