class Node(object):
    def __init__(self, value):
        self.value = value
        self.next = None
        self.prev = None


class LinkedList(object):

    def __init__(self):
        self.head = None
        self.tail = None
        self._size = 0

    def size(self):
        return self._size

    def add(self, value):
        new_node = Node(value)
        if self._size == 0:
            self.head = new_node
            self.tail = new_node
        elif self._size == 1:
            self.tail = new_node
            self.head.next = self.tail
            self.tail.prev = self.head
        else:
            new_node.prev = self.tail
            self.tail.next = new_node
            self.tail = new_node
        self._size += 1

    def insert(self, node, value):
        new_node = Node(value)
        node_next = node.next
        node.next = new_node
        new_node.prev = node
        if node_next is not None:
            new_node.next = node_next
            node_next.prev = new_node
        self._size += 1

    def display(self):
        values = []
        n = self.head
        while n is not None:
            values.append(str(n.value))
            n = n.next
        print("List: ", ", ".join(values))

    def delete(self, node):
        prev_node = node.prev
        next_node = node.next
        if prev_node is not None:
            prev_node.next = next_node
        else:
            self.head = node
        if next_node is not None:
            next_node.prev = prev_node
        else:
            self.tail = node
        node = None
        self._size -= 1
