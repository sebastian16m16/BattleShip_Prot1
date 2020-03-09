
class Player:

    def __init__(self, ip, port, connection, board):
        self.name = ""
        self.ip = ip
        self.port = port
        self.connection = connection
        self.board = board
        self.board_set = False
        self.turn = False
        self.has_won = False

    def __str__(self):
        return 'Name: {}\n' \
               'IP: {}\n' \
               'Port: {}\n' \
               'Board: \n{}'.format(self.name, self.ip, self.port, self.board)
