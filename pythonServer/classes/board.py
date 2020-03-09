from config.config import *


class Board:
    def __init__(self, size):
        self.size = size
        self.matrix = []
        self.init_game_board()

    def generate_board_matrix(self):
        for i in range(self.size):
            self.matrix.append([])
            for j in range(self.size):
                self.matrix[i].append(BOARD_STATE["EMPTY"])

    def init_game_board(self):
        self.generate_board_matrix()

    def print_board_matrix(self):
        for i in self.matrix:
            line = ""
            for j in i:
                line = line + str(j) + " "
            print(line)

    def hit_field(self, coord_x, coord_y):
        if self.matrix[coord_x][coord_y] == BOARD_STATE['EMPTY']:
            self.matrix[coord_x][coord_y] = BOARD_STATE['MISSED']
            return True
        else:
            if self.matrix[coord_x][coord_y] == BOARD_STATE['OCCUPIED']:
                self.matrix[coord_x][coord_y] = BOARD_STATE['HIT']
                return True
        return False

    def __str__(self):
        output = ''
        for i in self.matrix:
            line = ""
            for j in i:
                line = line + str(j) + " "
            output = output + line + '\n'
        return output
