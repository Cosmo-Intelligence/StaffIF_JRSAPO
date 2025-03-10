＜32bit版ExBSecurity.dllのレジストリ登録方法＞
あらかじめ、dllファイルを下記の方法で、追加処理を行ってください。
なお、OSによって処理内容が異なりますので、ご注意下さい。

「OS：32bit版」
１．DLLを以下にコピーする。
C:\Windows\System32\ExBSecurity.dll

２．DLLをレジストリに登録する。
C:\Windows\System32\cmd.exeを立ち上げる。
※以下のコマンドを入力し実行。
regsvr32 ExBSecurity.dll

「OS：64bit版」
１．DLLを以下にコピーする。
C:\Windows\SysWOW64\ExBSecurity.dll

２．DLLをレジストリに登録する。
C:\Windows\SysWOW64\cmd.exeを立ち上げる。
※以下のコマンドを入力し実行。
regsvr32 ExBSecurity.dll
