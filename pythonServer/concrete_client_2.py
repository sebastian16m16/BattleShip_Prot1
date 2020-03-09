from classes.client import *

if __name__ == "__main__":
    name = input('Please provide a username:\n')
    client = Client(name, "10.129.62.71", 7000)
    client.connect()
    client.play()
