import socket
import random
from classes.board import *
from config.config import *

DIRECTIONS = ['LEFT', 'UP', 'RIGHT', 'DOWN']
INDEX_HANDLING = {'LEFT': {'i': 0, 'j': -1}, 'UP': {'i': -1, 'j': 0},
                  'RIGHT': {'i': 0, 'j': 1}, 'DOWN': {'i': 1, 'j': 0}}
SPACE_BETWEEN_BOARDS = "         "
BOARD_HEADER = "YOU:                  " + SPACE_BETWEEN_BOARDS + "ENEMY:\n"
BOARD_HEADER = BOARD_HEADER + "  0 1 2 3 4 5 6 7 8 9 " + SPACE_BETWEEN_BOARDS + "  0 1 2 3 4 5 6 7 8 9\n"
BOARD_HEADER = BOARD_HEADER + "  ____________________" + SPACE_BETWEEN_BOARDS + "  ___________________"


class Client:

    def __init__(self, name, server_ip, server_port):
        self.name = name
        self.server_ip = server_ip
        self.server_port = server_port
        self.my_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.board = Board(BOARD_SIZE)
        self.enemy_board = Board(BOARD_SIZE)
        self.enemy_name = ""
        self.game_over = False

    def connect(self):
        address = (self.server_ip, self.server_port)
        self.my_socket.connect(address)
        self.my_socket.sendall("OK".encode(TCP_ENCODING))

    def setup_player_names(self):
        data_from_server = self.my_socket.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING)
        if data_from_server == 'SEND_NAME':
            self.my_socket.sendall(self.name.encode(TCP_ENCODING))
            opponent_name = self.my_socket.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING)
            self.enemy_name = opponent_name
            self.my_socket.sendall("OK".encode(TCP_ENCODING))
        else:
            print('>>> Unknown data from server: {} <<<'.format(data_from_server))

    def __pre_check(self, pos_x, pos_y, direction, boat_length):
        if direction == 0:
            # Not enough space to go left
            if (pos_y - boat_length) < 0:
                return False
        else:
            if direction == 1:
                # Not enough space to go up
                if (pos_x - boat_length) < 0:
                    return False
            else:
                if direction == 2:
                    # Not enough space to go right
                    if (pos_y + boat_length) > (BOARD_SIZE - 1):
                        return False
                else:
                    # Not enough space to go down
                    if (pos_x + boat_length) > (BOARD_SIZE - 1):
                        return False

        for i in range(0, boat_length):
            curr_x = pos_x + i * INDEX_HANDLING[DIRECTIONS[direction]]['i']
            curr_y = pos_y + i * INDEX_HANDLING[DIRECTIONS[direction]]['j']
            if self.board.matrix[curr_x][curr_y] != BOARD_STATE['EMPTY']:
                return False

        return True

    def setup_board(self):
        boat_length = 5
        nr_of_boats = 5
        while nr_of_boats > 0:
            found_place = False
            while not found_place:
                pos_x = random.randint(0, BOARD_SIZE-1)
                pos_y = random.randint(0, BOARD_SIZE-1)
                direction = random.randint(0, 3)

                if self.__pre_check(pos_x, pos_y, direction, boat_length):
                    for i in range(0, boat_length):
                        curr_x = pos_x + i * INDEX_HANDLING[DIRECTIONS[direction]]['i']
                        curr_y = pos_y + i * INDEX_HANDLING[DIRECTIONS[direction]]['j']
                        self.board.matrix[curr_x][curr_y] = BOARD_STATE['OCCUPIED']
                    found_place = True

            if nr_of_boats != 3:
                boat_length -= 1
            nr_of_boats -= 1
        return True

    def send_initial_board(self):
        data_from_server = self.my_socket.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING)
        server_ack = ""
        if data_from_server == 'SEND_BOARD_INFO':
            print('>>> Sending board data to server <<<')
            for i in range(len(self.board.matrix)):
                for j in range(len(self.board.matrix[i])):
                    if self.board.matrix[i][j] != BOARD_STATE['EMPTY']:
                        self.my_socket.sendall('({} {} {})'.format(i, j, self.board.matrix[i][j]).encode(TCP_ENCODING))
                        # print('>>> Data: ({} {} {}) sent to server <<<'.format(i, j, self.board.matrix[i][j]))
                        server_ack = self.my_socket.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING)
                        while server_ack != 'ACK':
                            server_ack = self.my_socket.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING)
                        # print('>>> Received ACK from server <<<')
            self.my_socket.sendall('END'.encode(TCP_ENCODING))
            print('>>> Finished sending board data to server! <<<')
        else:
            print(">>> Unknown data from server: {} <<<".format(data_from_server))

    @staticmethod
    def repr_own(val):
        if val == BOARD_STATE['EMPTY']:
            return '.'
        if val == BOARD_STATE['OCCUPIED']:
            return 'T'
        if val == BOARD_STATE['HIT']:
            return '\033[1;30;41mX\033[0m'
        if val == BOARD_STATE['MISSED']:
            return '\033[1;30;42mM\033[0m'

    @staticmethod
    def repr_enemy(val):
        if val == BOARD_STATE['EMPTY'] or val == BOARD_STATE['OCCUPIED']:
            return '.'
        if val == BOARD_STATE['HIT']:
            return '\033[1;30;41mX\033[0m'
        if val == BOARD_STATE['MISSED']:
            return '\033[1;30;42mM\033[0m'

    def draw_boards(self):
        print(BOARD_HEADER)
        for i in range(len(self.board.matrix)):
            own_line = str(i) + "|"
            enemy_line = str(i) + "|"
            for j in range(len(self.board.matrix[i])):
                own_line = own_line + self.repr_own(self.board.matrix[i][j]) + " "
            for j in range(len(self.enemy_board.matrix[i])):
                enemy_line = enemy_line + self.repr_enemy(self.enemy_board.matrix[i][j]) + " "
            print(own_line + SPACE_BETWEEN_BOARDS + enemy_line)
        print()

    @staticmethod
    def validate_input(input_data):
        if len(input_data) != 2:
            return False
        if not input_data[0].isdigit:
            return False
        if not input_data[1].isdigit:
            return False
        if int(input_data[0]) < 0 or int(input_data[0]) > 9:
            return False
        if int(input_data[1]) < 0 or int(input_data[1]) > 9:
            return False
        return True

    def my_round(self):
        coordinates = input('Insert coordinates to shoot at: \n').strip().split(' ')
        while not self.validate_input(coordinates):
            print('>>> ERROR: insert two values between 0 and 9 separated by a single space <<<')
            coordinates = input('Insert coordinates to shoot at: \n').strip().split(' ')
        self.my_socket.sendall('{} {}'.format(coordinates[0], coordinates[1]).encode(TCP_ENCODING))
        data = self.my_socket.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING)
        return data

    def update_own_board(self, coord_x, coord_y, value):
        self.board.matrix[int(coord_x)][int(coord_y)] = int(value)
        pass

    def update_enemy_board(self, coord_x, coord_y, value):
        self.enemy_board.matrix[int(coord_x)][int(coord_y)] = int(value)
        pass

    def play_next_round(self):
        print('>>> Current game status <<<')
        self.draw_boards()
        data = self.my_socket.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING)
        if data == 'YOUR_TURN':
            print('>>> Your turn! <<<')
            data = self.my_round()
            while data != 'OK':
                if data == 'INVALID_FIELD':
                    print('>>> SERVER: invalid field selected, you already shot at that location! <<<')
                else:
                    if data == 'INVALID_COORDINATES':
                        print('>>> SERVER: insert two values between 0 and 9 separated by a single space <<<')
                    else:
                        print('>>> SERVER: unknown error, please try again! <<<')
                data = self.my_round()

            self.my_socket.sendall('READY'.encode(TCP_ENCODING))
            indices_to_update = self.my_socket.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING).strip().split(' ')
            print(">>> You hit field on ({} {}) result: {}".format(indices_to_update[0], indices_to_update[1],
                                                                   indices_to_update[2]))
            self.update_enemy_board(int(indices_to_update[0]), int(indices_to_update[1]), indices_to_update[2])

        else:
            if data == 'PASS':
                print('>>> Enemy turn! <<<')
                self.my_socket.sendall('READY'.encode(TCP_ENCODING))
                indices_to_update = self.my_socket.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING).strip().split(' ')
                print(">>> Enemy hit field on ({} {}) result: {}".format(indices_to_update[0], indices_to_update[1],
                                                                         indices_to_update[2]))
                self.update_own_board(int(indices_to_update[0]), int(indices_to_update[1]), indices_to_update[2])
            else:
                print('>>> Unknown data from server: {} <<<'.format(data))

        self.my_socket.sendall('OK'.encode(TCP_ENCODING))

    def play(self):
        data = self.my_socket.recv(MAX_MESSAGE_SIZE).decode("UTF-8")
        if data == 'SUCCESS':
            print('>>> Successfully connected to server! <<<')
            self.setup_player_names()
            print('>>> Successfully set up names! <<<')
            print('>>> Your name: {} Opponent name: {}'.format(self.name, self.enemy_name))
            self.setup_board()
            # print(self.board)
            print('>>> Board set up, waiting for server <<<')
            self.send_initial_board()
            while not self.game_over:
                self.play_next_round()
                data = self.my_socket.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING)
                if data == 'END_GAME':
                    self.game_over = True
                    print(">>> GAME OVER <<<")
                else:
                    if data == 'NEXT_ROUND':
                        print(">>> NEXT ROUND <<<")
                        self.my_socket.sendall("OK".encode('UTF-8'))
                    else:
                        print(">>> Unknown data from server: {} <<<".format(data))
