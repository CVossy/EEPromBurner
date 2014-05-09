/*
  Author:        Christopher Voß
  Date:          03.05.2014
  Description:   Arduino part of "Vossys_EEPROM Burner" for the 8Kbyte Atmel AT28C64B 15FU 1304
*/

//address and databus pin-mapping
int addybus[13]={22,23,24,25,26,27,28,29,30,31,32,33,34};
int databus[8]={40,41,42,43,44,45,46,47};
//controlpin mapping
int ce=52;
int we=51;
int oe=50;
//adress
int addr=0;
byte data[1024];

void setup()
{  
  Serial.begin(9600);
  
  //addressbus
  pinMode(addybus[0],OUTPUT);
  pinMode(addybus[1],OUTPUT);
  pinMode(addybus[2],OUTPUT);
  pinMode(addybus[3],OUTPUT);
  pinMode(addybus[4],OUTPUT);
  pinMode(addybus[5],OUTPUT);
  pinMode(addybus[6],OUTPUT);
  pinMode(addybus[7],OUTPUT);
  pinMode(addybus[8],OUTPUT);
  pinMode(addybus[9],OUTPUT);
  pinMode(addybus[10],OUTPUT);
  pinMode(addybus[11],OUTPUT);
  pinMode(addybus[12],OUTPUT);
  //Databus 
  pinMode(databus[0],OUTPUT);
  pinMode(databus[1],OUTPUT);
  pinMode(databus[2],OUTPUT);
  pinMode(databus[3],OUTPUT);
  pinMode(databus[4],OUTPUT);
  pinMode(databus[5],OUTPUT);
  pinMode(databus[6],OUTPUT);
  pinMode(databus[7],OUTPUT);
  //control bits
  pinMode(ce,OUTPUT);
  pinMode(we,OUTPUT);
  pinMode(oe,OUTPUT);
}
void set_Data_out()
{
  //setting Databus to output 
  pinMode(databus[0],OUTPUT);
  pinMode(databus[1],OUTPUT);
  pinMode(databus[2],OUTPUT);
  pinMode(databus[3],OUTPUT);
  pinMode(databus[4],OUTPUT);
  pinMode(databus[5],OUTPUT);
  pinMode(databus[6],OUTPUT);
  pinMode(databus[7],OUTPUT);
}
void set_Data_in()
{
  //setting databus to input 
  pinMode(databus[0],INPUT);
  pinMode(databus[1],INPUT);
  pinMode(databus[2],INPUT);
  pinMode(databus[3],INPUT);
  pinMode(databus[4],INPUT);
  pinMode(databus[5],INPUT);
  pinMode(databus[6],INPUT);
  pinMode(databus[7],INPUT);
}

void loop()
{
  //waiting for the pc
  if (Serial.available())
  {    
    digitalWrite(ce, HIGH);
    digitalWrite(we, HIGH);
    digitalWrite(oe, HIGH);
    //reading control data
    int usbdata = Serial.read();
    int timeout=0;
    addr=0;
    //what should we do?
    switch(usbdata)
    { 
       //Write to Eeprom     
       case 51:
           //how many bytes should be written?
           timeout=8192;
           //setting control bytes        
           digitalWrite(ce,HIGH);
           digitalWrite(we,HIGH);
           digitalWrite(oe,LOW);
           //setting databus to output
           set_Data_out();
           //writing all bytes
           while(timeout!=addr)
           {  
              //reading a 1024 Byte Block
              block_read();              
              for (int i = 0; i < 1024; i++)
              {           
                data_write(addr,data[i]);   
                addr++;
              }
              Serial.write(0);
           }
          break; 
       //Read from EEPROM
       case 50:             
            timeout=8192;
            set_Data_in();
            digitalWrite(we,HIGH);     
            digitalWrite(ce,HIGH);
            digitalWrite(oe,HIGH);     
            while(timeout!=addr)
            {         
              setting_address(addr);
              delay(1);              
              digitalWrite(ce,LOW);
              digitalWrite(oe,LOW);       
              delay(10);     
              Serial.write(data_read());      
              digitalWrite(ce,HIGH);
              digitalWrite(oe,HIGH);             
              addr++;
            }
            break;
        default:
          break;
    }    
  }  
}

void block_read()
{
  for (int i = 0; i< 1024;i++)
  {
    if (Serial.available())
      data[i]=Serial.read();
    else
      i--;
  }
}

void data_write(int addr,byte dat)
{
  //Adresse "einstellbar machen
  digitalWrite(oe,HIGH);
  setting_address(addr);
  digitalWrite(we,LOW);
  digitalWrite(ce,LOW);
  data_set(dat);
  digitalWrite(ce,HIGH);
  digitalWrite(oe,LOW); 
  digitalWrite(we,HIGH);    
  delay(10);    
}

void setting_address(int Adresse)
{
  //Alle Pins durchgehen
  for (int i = 0; i < 13;i++)
  {
    //entsprechendes Addressbit setzen
    digitalWrite(addybus[i],bitRead(Adresse,i));
  }
}

void data_set(byte data)
{
  for (int i = 0; i < 8; i++)
  {
    digitalWrite(databus[i],bitRead(data,i));
  }
}

byte data_read()
{
  byte x = (digitalRead(databus[7])*128) + (digitalRead(databus[6])*64) + (digitalRead(databus[5])*32) + (digitalRead(databus[4])*16) + (digitalRead(databus[3])*8) + (digitalRead(databus[2])*4) + (digitalRead(databus[1])*2) + (digitalRead(databus[0]));
  return x;
}
