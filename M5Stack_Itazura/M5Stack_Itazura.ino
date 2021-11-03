#include <M5Stack.h>

#include <WiFi.h>
#include <WiFiUdp.h>


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//WiFi
const char* ssid = "xxxxxxxx";
const char* password = "xxxxxxxx";
const char* client_address = "192.168.0.33";  //送り先
const int client_port = 2002;  //送り先
const int server_port = 2004;  //このESP32 のポート番号
WiFiUDP udp;



byte recvbuf[1024];
byte sendbuf[1024];

unsigned long times[6];


typedef union {
  unsigned long lval;
  float fval;
  byte binary[4];
} uf;

#define RECVUDPSIZE 1
#define SENDUDPSIZE 1

uf ufdata[RECVUDPSIZE];
uf sendufdata[SENDUDPSIZE];





void setup() {

  
  M5.begin(); // 初期化処理
  M5.Power.begin();
  delay(500);
  // 文字の色とサイズ
  M5.Lcd.fillScreen(BLACK);
  M5.Lcd.setTextColor(WHITE);
  M5.Lcd.setTextSize(4);

  
  //Serial.begin(115200);


  Serial.println("[ESP32] Connecting to WiFi network: " + String(ssid));
  WiFi.disconnect(true, true);
  delay(500);
  WiFi.begin(ssid, password);
  while ( WiFi.status() != WL_CONNECTED) {
    delay(500);
  }
  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
  udp.begin(server_port);
  delay(500);


  delay(100);




  



  
}


void receiveUDP() {
  int packetSize = udp.parsePacket();

  if (packetSize > 0) {
    //Serial.println(String(packetSize));

    //Serial.println("recv");
    for (int a = 0; a < RECVUDPSIZE; a++) {
      udp.read(ufdata[a].binary, sizeof(float));
      //Serial.print("float data: " + String(ufdata[a].val) );
      //Serial.print("\n");
    }
  }
}

void sendUDP() {
  udp.beginPacket(client_address, client_port);


  for (int a = 0; a < SENDUDPSIZE; a++) {
    udp.write(sendufdata[a].binary, sizeof(float) );
    //Serial.print("float data: " + String(ufdata[a].val) );
    //Serial.print("\n");
  }

  udp.endPacket();
  //２バイト以上のデータタイプはビットシフトしてバイト分割して送る
  //float は共用体を利用して送る
  //https://hawksnowlog.blogspot.com/2016/11/sending-multibytes-with-serialwrite.html#float-4byte-%E3%81%AE%E6%83%85%E5%A0%B1%E3%82%92%E9%80%81%E4%BF%A1%E3%81%99%E3%82%8B%E6%96%B9%E6%B3%95

  Serial.println("send.");
}


int cnt = 0;



void loop() {


  // Aボタンが押されたら+1
  if(M5.BtnA.wasPressed())
  {
    cnt++;
    M5.Lcd.clear(); // 画面全体を消去
      
    sendufdata[0].lval = 1;
    
    sendUDP();

  }

  // Bボタンが押されたら-1
  if(M5.BtnB.wasPressed())
  {
    cnt--;
    M5.Lcd.clear(); // 画面全体を消去
    
    sendufdata[0].lval = 2;
    
    sendUDP();
  }

  // Cボタンが押されたら0
  if(M5.BtnC.wasPressed())
  {
    cnt = 0;
    M5.Lcd.clear(); // 画面全体を消去
    
    sendufdata[0].lval = 3;
    
    sendUDP();
  }
  delay(30);

  M5.Lcd.drawString("ITAZURA=" + String(cnt), 0, 40); // カウント値を表示
  M5.update();  // ボタン操作の状況を読み込む関数(ボタン操作を行う際は必須)

  
}
