
import zipfile

with zipfile.ZipFile('windows_zipslip_test.zip', 'w') as zf:
    # �p�X�g���o�[�T��: �J�����g��1�K�w��ɓW�J�����t�@�C��
    zf.writestr('..\\evil1.txt', 'This is a Windows ZipSlip test!')
    zf.writestr('../evil2.txt', 'This is a Windows ZipSlip test!')

    # ��΃p�X
    zf.writestr('C:\\Windows\\Temp\\evil3.txt', 'Absolute path test!')
    zf.writestr('C:C:\\Windows\\Temp\\evil4.txt', 'Absolute path test!')

    # �ʏ�t�@�C��
    zf.writestr('normal.txt', 'This is a normal file.')

