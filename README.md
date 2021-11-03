# ItazuraM5StackUDP

M5Stack で PC にイタズラ画面を表示する App です (WiFi / UDP)


# 動作

https://twitter.com/devemin/status/1455881644007559170



# 構成

ItazuraM5 が、 Visual Studio 2019 (VS2019) C# で作ったプログラムです。

M5Stack_Itazura が、M5Stack / Arduino IDE で作ったプログラムです。


# 設定

VS2019 では、 コード内の IPアドレス, port番号を修正し、

Arduino IDE では、コード内の SSID, パスワード, IPアドレス, port番号を修正してください。


画像と音声は、ライセンスのためブランクにしています。

適宜、VS2019 Resources.resx 内のイメージ / オーディオ　を変更してください。


画像　ペニーワイズがオススメするシリーズ

https://dic.nicovideo.jp/a/%E3%83%9A%E3%83%8B%E3%83%BC%E3%83%AF%E3%82%A4%E3%82%BA%E3%81%8C%E3%82%AA%E3%82%B9%E3%82%B9%E3%83%A1%E3%81%99%E3%82%8B%E3%82%B7%E3%83%AA%E3%83%BC%E3%82%BA

音声　びたちー素材館

http://vita-chi.net/sozai1.htm



# 使い方

M5Stack の左ボタンを押すと、画像が出たり消えたり音を出したりします。

中、右ボタンを押すと、ウィンドウを左右に移動します。

アクティブ前面化処理はしていないです。必要ならこのあたりが参考になりそうです。

https://dobon.net/vb/dotnet/form/activate.html


# 参考 THANKS

https://dobon.net/vb/dotnet/internet/udpclient.html

https://johobase.com/windows-form-transparencykey/

https://algorithm.joho.info/m5stack/m5stack-button/


# ライセンス

MIT Liscense
