from classes.graphics import *

if __name__ == '__main__':
    win = GraphWin("Battleships", 1280, 720)
    res = win.getMouse()
    print(str(res.getX()) + " " + str(res.getY()))
