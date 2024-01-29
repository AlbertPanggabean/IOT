// Kelompok 07
// 1. Albert Panggabean
// 2. Naek Butarbutar
// 3. Risda Br. Malau

#include "DHT.h"

DHT dht(2, DHT11);

#define RELAY_ON 0
#define RELAY_OFF 1
#define RELAY_1  7   // pin yang digunakan yaitu pin 8
#define RELAY_2  8
#define RELAY_3  9

#include <LiquidCrystal_I2C.h>
LiquidCrystal_I2C lcd(0x27,16,2);

#define suhuon 11
#define suhuoff 12


void setup() {
  lcd.init();
  lcd.backlight(); 

  Serial.begin(9600); 
  Serial.println("DHTxx test!");
  dht.begin();
  
 // Set pin as output.
  pinMode(RELAY_1, OUTPUT);
  pinMode(RELAY_2, OUTPUT);
  pinMode(RELAY_3, OUTPUT);
  pinMode(suhuon, OUTPUT);
  pinMode(suhuoff, OUTPUT);
   
 // Initialize relay one as off so that on reset it would be off by default
  digitalWrite(RELAY_1, RELAY_OFF);
  digitalWrite(RELAY_2, RELAY_OFF);
  digitalWrite(RELAY_3, RELAY_OFF);

  lcd.begin(16, 2);
  lcd.setCursor(3,0);
  lcd.print("INCUBATOR");
  lcd.setCursor(3,1);
  lcd.print("TELUR AYAM");
  delay(5000);
  lcd.clear();

  lcd.setCursor(2,0);
  lcd.print("Di Buat Oleh");
  lcd.setCursor(2,1);
  lcd.print("Kelompok  07");
  delay(5000);
  lcd.clear();
}

void loop() {
  // Baca humidity dan temperature
  float h = dht.readHumidity();
  float t = dht.readTemperature();

  // Cek hasil pembacaan, dan tampilkan bila ok
  if (isnan(t) || isnan(h)) {
    Serial.println("Failed to read from DHT");
    return;
  }

  if (t<37.00)// ON
{
  digitalWrite(RELAY_1, RELAY_ON);
  digitalWrite(RELAY_2, RELAY_ON);
  digitalWrite(RELAY_3, RELAY_ON);
  digitalWrite(suhuon, HIGH);
  digitalWrite(suhuoff, LOW);
}
else if (t >= 37.00 && t <38.50)//relay 2 off
{
  digitalWrite(RELAY_1, RELAY_ON);
  digitalWrite(RELAY_2, RELAY_OFF);
  digitalWrite(RELAY_3, RELAY_ON);
  digitalWrite(suhuon, HIGH);
  digitalWrite(suhuoff, LOW);
}  
 else if (t>39.00)//OFF
{
  digitalWrite(RELAY_1, RELAY_OFF);
  digitalWrite(RELAY_3, RELAY_OFF);
  digitalWrite(suhuoff, HIGH); 
  digitalWrite(suhuon, LOW);
}

  Serial.print("kelembaban: ");
  Serial.print(h);
  Serial.print(" ");
  Serial.print("suhu: ");
  Serial.println(t);

  lcd.clear();
  lcd.setCursor(0,0);
  lcd.print("Kelembapan: ");
  lcd.setCursor(11,0);
  lcd.print(h);

  lcd.setCursor(0,1);
  lcd.print("Suhu: ");
  lcd.setCursor(5,1);
  lcd.print(t);
  delay(1000);
}