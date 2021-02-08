import requests as re
import os
import json
import subprocess
import serial
import base64
import time
from credentials import URL, PORT, TOKEN

while True:
    x = json.loads(re.get(f'{URL}?token={TOKEN}').text)

    # checks if some code is available
    if(x["IsWaiting"]==True):
        code=(base64.b64decode(x["File"])).decode('utf8')

        # saves the code to a .ino file
        with open('code.ino', 'w') as the_file:
            the_file.write(code)

        # uses the installed Arduino to run the code
        result=subprocess.run(f'arduino_debug code.ino --port {PORT} --upload' , capture_output=True)
        output=result.stderr
        output+=result.stdout

        # reads the Serial output
        ser = serial.Serial(port=PORT,baudrate=9600)


        time.sleep(5)
        
        output+=ser.read_all()

        
        ser.close()

        # Arduino doesn't offer a way to stop a program, we have to load an empty file
        subprocess.run(f'arduino_debug reset.ino --port {PORT} --upload')
        
        # submits results to the server
        re.post(f'{URL}?token={TOKEN}', data = {'id':x["Id"],'message':output})