
import zipfile

with zipfile.ZipFile('windows_zipslip_test.zip', 'w') as zf:
    # パストラバーサル: カレントの1階層上に展開されるファイル
    zf.writestr('..\\evil1.txt', 'This is a Windows ZipSlip test!')
    zf.writestr('../evil2.txt', 'This is a Windows ZipSlip test!')

    # 絶対パス
    zf.writestr('C:\\Windows\\Temp\\evil3.txt', 'Absolute path test!')
    zf.writestr('C:C:\\Windows\\Temp\\evil4.txt', 'Absolute path test!')

    # 通常ファイル
    zf.writestr('normal.txt', 'This is a normal file.')

