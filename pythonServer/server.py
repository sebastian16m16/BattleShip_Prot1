import socket
from classes.player import *
from classes.board import *
from config.config import *


class Game:
    def __init__(self, ip, port):
        self.ip = ip
        self.port = port
        self.players = []
        self.server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.game_over = False

    def add_player(self, player):
        if len(self.players) < 2:
            self.players.append(player)
            return True
        else:
            return False

    def player_ack(self, player_index):
        resp = self.players[player_index].connection.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING).strip()
        if resp == 'OK':
            return True
        else:
            return False

    def connect_clients(self):
        address = (self.ip, self.port)
        self.server_socket.bind(address)
        print(">>> Server started on {}:{} <<<".format(self.ip, self.port))

        self.server_socket.listen(1)
        print(">>> Server listening! <<<")

        while len(self.players) < 2:
            print(">>> Server waiting for a client to connect! <<<")
            connection, client_address = self.server_socket.accept()
            print(">>> Client with the address: {} has connected <<<".format(client_address))
            if self.add_player(Player(client_address[0], client_address[1], connection, Board(BOARD_SIZE))):
                connection.sendall('SUCCESS'.encode(TCP_ENCODING))
            else:
                connection.sendall('FAILED'.encode(TCP_ENCODING))

        if not self.player_ack(0):
            return False

        if not self.player_ack(1):
            return False

        return True

    def get_player_names(self):
        self.players[0].connection.sendall('SEND_NAME'.encode(TCP_ENCODING))
        self.players[1].connection.sendall('SEND_NAME'.encode(TCP_ENCODING))

        name_0 = self.players[0].connection.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING).strip()
        name_1 = self.players[1].connection.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING).strip()

        self.players[0].name = name_0
        self.players[1].name = name_1

        self.players[0].connection.sendall(name_1.encode(TCP_ENCODING))
        self.players[1].connection.sendall(name_0.encode(TCP_ENCODING))

        if not self.player_ack(0):
            return False

        if not self.player_ack(1):
            return False

        return True

    def players_board_set(self):
        return self.players[0].board_set and self.players[1].board_set

    def get_player_board(self, player_index):
        player = self.players[player_index]
        player.connection.sendall('SEND_BOARD_INFO'.encode(TCP_ENCODING))
        all_information_received = False
        while not all_information_received:
            data_from_client = player.connection.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING)
            if data_from_client == 'END':
                all_information_received = True
            else:
                # print('>>> Data from client: {} <<<'.format(data_from_client))
                board_data = data_from_client.strip().replace('(', '').replace(')', '')
                board_data_list = board_data.split(' ')
                player.board.matrix[int(board_data_list[0])][int(board_data_list[1])] = int(board_data_list[2])
                player.connection.sendall('ACK'.encode(TCP_ENCODING))

        return True

    def setup_game_boards(self):
        player_index = 0
        if self.get_player_board(player_index):
            self.players[player_index].board_set = True
            self.players[player_index].turn = True
            # print(self.players[player_index])

        player_index = 1
        if self.get_player_board(player_index):
            self.players[player_index].board_set = True
            # print(self.players[player_index])

        return self.players_board_set()

    def get_current_player_index(self):
        if self.players[0].turn:
            return 0
        else:
            return 1

    def get_opponent_player_index(self):
        if self.players[0].turn:
            return 1
        else:
            return 0

    def next_player(self):
        tmp = self.players[0].turn
        self.players[0].turn = self.players[1].turn
        self.players[1].turn = tmp

    def round_start(self, current_player_index, opponent_index):
        self.players[current_player_index].connection.sendall('YOUR_TURN'.encode(TCP_ENCODING))
        self.players[opponent_index].connection.sendall('PASS'.encode(TCP_ENCODING))

    @staticmethod
    def validate_indexes(indexes_to_hit):
        if len(indexes_to_hit) != 2:
            return False
        if not indexes_to_hit[0].isdigit:
            return False
        if not indexes_to_hit[1].isdigit:
            return False
        if int(indexes_to_hit[0]) < 0 or int(indexes_to_hit[0]) > 9:
            return False
        if int(indexes_to_hit[1]) < 0 or int(indexes_to_hit[1]) > 9:
            return False
        return True

    def check_selected_field(self, indexes_to_hit, opponent_index):

        enemy_board = self.players[opponent_index].board.matrix
        coord_x = int(indexes_to_hit[0])
        coord_y = int(indexes_to_hit[1])

        if enemy_board[coord_x][coord_y] == BOARD_STATE['HIT']:
            return False
        if enemy_board[coord_x][coord_y] == BOARD_STATE['MISSED']:
            return False

        return True

    def hit_opponent_field(self, indexes_to_hit, opponent_index):
        coord_x = int(indexes_to_hit[0])
        coord_y = int(indexes_to_hit[1])
        self.players[opponent_index].board.hit_field(coord_x, coord_y)

    def round_logic(self, current_player_index, opponent_index):
        correct_coordinates_provided = False

        data = self.players[current_player_index].connection.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING).strip()
        indexes_to_hit = data.split(' ')
        while not correct_coordinates_provided:
            if not self.validate_indexes(indexes_to_hit):
                self.players[current_player_index].connection.sendall('INVALID_COORDINATES'.encode(TCP_ENCODING))
            else:
                if not self.check_selected_field(indexes_to_hit, opponent_index):
                    self.players[current_player_index].connection.sendall('INVALID_FIELD'.encode(TCP_ENCODING))
                else:
                    correct_coordinates_provided = True

            if correct_coordinates_provided:
                self.hit_opponent_field(indexes_to_hit, opponent_index)
                self.players[current_player_index].connection.sendall('OK'.encode(TCP_ENCODING))
            else:
                data = self.players[current_player_index].connection.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING).strip()
                indexes_to_hit = data.split(' ')

        return indexes_to_hit

    def round_end(self, current_player_index, opponent_index, field_hit_coordinates):
        x = int(field_hit_coordinates[0])
        y = int(field_hit_coordinates[1])
        val = self.players[opponent_index].board.matrix[x][y]

        data = self.players[current_player_index].connection.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING).strip()
        if data == 'READY':
            self.players[current_player_index].connection.sendall('{} {} {}'.format(x, y, val).encode(TCP_ENCODING))
        else:
            print('>>> Unknown command from current_player client <<<')

        data = self.players[opponent_index].connection.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING).strip()
        if data == 'READY':
            self.players[opponent_index].connection.sendall('{} {} {}'.format(x, y, val).encode(TCP_ENCODING))
        else:
            print('>>> Unknown command from opponent_player client <<<')

        current_player_data = self.players[current_player_index].connection.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING)
        opponent_data = self.players[opponent_index].connection.recv(MAX_MESSAGE_SIZE).decode(TCP_ENCODING)

        if current_player_data == "OK" and opponent_data == "OK":
            print('>>> Both clients ready for next turn <<<')
        else:
            print('>>> Invalid data from one of the clients! <<<')

    def play_round(self):
        current_player_index = self.get_current_player_index()
        opponent_index = self.get_opponent_player_index()

        self.round_start(current_player_index, opponent_index)
        field_hit = self.round_logic(current_player_index, opponent_index)
        self.round_end(current_player_index, opponent_index, field_hit)

    def check_game_over(self):
        found_occupied = False
        for i in self.players[0].board.matrix:
            for j in i:
                if j == BOARD_STATE['OCCUPIED']:
                    found_occupied = True

        if found_occupied:
            found_occupied = False
            for i in self.players[1].board.matrix:
                for j in i:
                    if j == BOARD_STATE['OCCUPIED']:
                        found_occupied = True

            if found_occupied:
                return False
            else:
                self.players[0].has_won = True
                return True
        else:
            self.players[1].has_won = True
            return True

    def play_game(self):
        while not self.game_over:
            self.play_round()
            if self.check_game_over():
                self.game_over = True
                self.players[0].connection.sendall('END_GAME'.encode(TCP_ENCODING))
                self.players[1].connection.sendall('END_GAME'.encode(TCP_ENCODING))
                print(">>> GAME OVER <<<")
            else:
                self.next_player()
                self.players[0].connection.sendall('NEXT_ROUND'.encode(TCP_ENCODING))
                self.players[1].connection.sendall('NEXT_ROUND'.encode(TCP_ENCODING))
                print(">>> Next round commences! <<<")

                if self.player_ack(0):
                    print(">>> Client 0 ready for next round <<<")
                if self.player_ack(1):
                    print(">>> Client 1 ready for next round <<<")
                    

if __name__ == '__main__':

    game = Game("10.129.62.71", 7000)
    if game.connect_clients():
        print('>>> Both clients connected, entering setup phase! <<<')
        if game.get_player_names():
            print('>>> Client names have been set, setting boards now! <<<')
            if game.setup_game_boards():
                print('>>> Boards have been set up, game starts! <<<')
                game.play_game()
            else:
                print('>>> Board setup failed, aborting game! <<<')
        else:
            print('>>> Failed getting client names! aborting game! <<<')
    else:
        print('>>> Connection problem, aborting game <<<')
