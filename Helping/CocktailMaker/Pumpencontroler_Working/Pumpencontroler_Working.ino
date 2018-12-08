

// Wifi-Libary
#include <ESP8266WiFi.h>

// Input WiFI
const char* ssid     = "mutterschiff";
const char* password = "railsgirls";

// Set web server port number to 80
WiFiServer server(80);

// Variable to store the HTTP request
String header;

// Variabeln für den Jetztigen Output
String output5State = "off";
String output4State = "off";
String output3State = "off";

// Jedem Output ein GPIO Port Zuweisen
const int output5 = 5;
const int output4 = 4;
const int output3 = 3;

void setup() {
  Serial.begin(115200);
  // GPIO Ports als output definieren
  pinMode(output5, OUTPUT);
  pinMode(output4, OUTPUT);
  pinMode(output3, OUTPUT);
  // Alle outputs Low
  digitalWrite(output5, LOW);
  digitalWrite(output4, LOW);
  digitalWrite(output3, LOW);

  // WiFi Connecten
  Serial.print("Connecting to ");
  Serial.println(ssid);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  // Webserver Starten und Ip Adresse über Seriell ausgeben
  Serial.println("");
  Serial.println("WiFi connected.");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
  server.begin();
}

void loop(){
  WiFiClient client = server.available();   // Auf neue Verbindungen Warten

  if (client) {                             // If a new client connects,
    Serial.println("New Client.");          // print a message out in the serial port
    String currentLine = "";                // make a String to hold incoming data from the client
    while (client.connected()) {            // loop while the client's connected
      if (client.available()) {             // if there's bytes to read from the client,
        char c = client.read();             // read a byte, then
        Serial.write(c);                    // print it out the serial monitor
        header += c;
        if (c == '\n') {                    // if the byte is a newline character
          // if the current line is blank, you got two newline characters in a row.
          // that's the end of the client HTTP request, so send a response:
          if (currentLine.length() == 0) {
            // HTTP headers always start with a response code (e.g. HTTP/1.1 200 OK)
            // and a content-type so the client knows what's coming, then a blank line:
            client.println("HTTP/1.1 200 OK");
            client.println("Content-type:text/html");
            client.println("Connection: close");
            client.println();
            
                     // turns the GPIOs on and off - 10sec gpio 3, 4, 5
              if (header.indexOf("GET /5/on") >= 0) {
                Serial.println("GPIO 5 on");
                output5State = "on";
                output4State = "on";
                output3State = "on";

                digitalWrite(output5, HIGH);
                delay(10000);
                digitalWrite(output5, LOW);
                digitalWrite(output4, HIGH);
                delay(10000);
                digitalWrite(output4, LOW);
                digitalWrite(output3, HIGH);
                delay(10000);
                digitalWrite(output3, LOW);

              } else if (header.indexOf("GET /5/off") >= 0) {
                Serial.println("GPIO 5 off");
                output5State = "off";
                output4State = "off";
                               output3State = "off";
                                              digitalWrite(output5, LOW);
                digitalWrite(output4, LOW);
                digitalWrite(output3, LOW);

                //Waschvorgang - ALLE PUMPEN FÜR 30SEC
              } else if (header.indexOf("GET /4/on") >= 0) {
                Serial.println("GPIO 4 on");
                output4State = "on";
                output3State = "on";
                output5State = "on";
                digitalWrite(output4, HIGH);
                digitalWrite(output3, HIGH);
                digitalWrite(output5, HIGH);
                delay(30000);
                digitalWrite(output4, LOW);
                digitalWrite(output3, LOW);
                digitalWrite(output5, LOW);

              } else if (header.indexOf("GET /4/off") >= 0) {
                Serial.println("GPIO 4 off");
                output4State = "off";
                digitalWrite(output4, LOW);
                digitalWrite(output3, LOW);
                digitalWrite(output5, LOW);

              }
            // HTML Der Website
            client.println("<!DOCTYPE html><html>");
            client.println("<head><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            client.println("<link rel=\"icon\" href=\"data:,\">");
            // CSS Design der Knöpfe
            client.println("<style>html { font-family: Helvetica; display: inline-block; margin: 0px auto; text-align: center;}");
            client.println(".button { background-color: #195B6A; border: none; color: white; padding: 16px 40px;");
            client.println("text-decoration: none; font-size: 30px; margin: 2px; cursor: pointer;}");
            client.println(".button2 {background-color: #77878A;}</style></head>");
            
            // Web Page Heading
            client.println("<body><h1>TSCHUNKMASTER 9000</h1>");
            
            // Display current state, and ON/OFF buttons for GPIO 5  
            client.println("<p>GPIO 5 - State " + output5State + "</p>");
            // If the output5State is off, it displays the ON button       
            if (output5State=="off") {
              client.println("<p><a href=\"/5/on\"><button class=\"button\">FILL ME UP</button></a></p>");
            } else {
              client.println("<p><a href=\"/5/off\"><button class=\"button button2\">RESTE ME</button></a></p>");
            } 
               
            // Display current state, and ON/OFF buttons for GPIO 4  
            client.println("<p>GPIO 4 - State " + output4State + "</p>");
            // If the output4State is off, it displays the ON button       
            if (output4State=="off") {
              client.println("<p><a href=\"/4/on\"><button class=\"button\">I AM DIRTY</button></a></p>");
            } else {
              client.println("<p><a href=\"/4/off\"><button class=\"button button2\">RESET</button></a></p>");
            }
            client.println("</body></html>");
            
            // The HTTP response ends with another blank line
            client.println();
            // Break out of the while loop
            break;
          } else { // if you got a newline, then clear currentLine
            currentLine = "";
          }
        } else if (c != '\r') {  // if you got anything else but a carriage return character,
          currentLine += c;      // add it to the end of the currentLine
        }
      }
    }
    // Clear the header variable
    header = "";
    // Close the connection
    client.stop();
    Serial.println("Client disconnected.");
    Serial.println("");
  }
}