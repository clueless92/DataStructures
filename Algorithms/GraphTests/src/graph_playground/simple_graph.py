import sys


class SimpleGraph(object):

    def __init__(self, number_of_nodes=10):
        self._adjacency_list = [[]]
        self._number_of_nodes = number_of_nodes
        self._edge_weights = [[0 for x in range(number_of_nodes)] for y in range(number_of_nodes)]

    def size(self):
        return self._number_of_nodes

    def add_node(self, node):
        self._adjacency_list.insert(node, [])

    def add_edge(self, node1, node2, weight=1):
        self._adjacency_list[node1].append(node2)
        self._adjacency_list[node2].append(node1)
        self._edge_weights[node1][node2] = weight
        self._edge_weights[node2][node1] = weight

    def get_neighbours(self, node):
        return self._adjacency_list[node]

    def get_weight(self, node1, node2):
        return self._edge_weights[node1][node2]

    def bfs_traversal(self, start_node):
        queue = []
        visited = []
        queue.append(start_node)
        visited.append(start_node)
        while len(queue) > 0:
            curr_node = queue.pop(0)
            for neighbourNode in self.get_neighbours(curr_node):
                if not visited.__contains__(neighbourNode):
                    queue.append(neighbourNode)
                    visited.append(neighbourNode)
        return visited

    def dfs_traversal(self, start_node):
        stack = []
        visited = []
        stack.append(start_node)
        visited.append(start_node)
        while len(stack) > 0:
            curr_node = stack.pop()
            for neighbourNode in self.get_neighbours(curr_node):
                if not visited.__contains__(neighbourNode):
                    stack.append(neighbourNode)
                    visited.append(neighbourNode)
        return visited

    def dijkstra(self, start_node, destination_node):
        distance = []
        visited = []
        prev = []
        UNKNOWN = sys.maxsize >> 1
        for i in range(0, self._number_of_nodes, 1):
            distance.append(UNKNOWN)
            visited.append(False)
            prev.append(-1)

        distance[start_node] = 0
        while True:
            # find the nearest unvisited node from the source (in place of priority queue -> deque)
            min_distance = UNKNOWN
            min_node = 0
            for node in range(0, self._number_of_nodes, 1):
                if visited[node] == 0 and distance[node] < min_distance:
                    min_distance = distance[node]
                    min_node = node

            if min_distance == UNKNOWN:
                # no min_distance node found -> algorithm finished
                break

            visited[min_node] = True
            for i in range(0, self._number_of_nodes, 1):
                # improve the distance[0..n-1] through min_node
                curr_weight = self.get_weight(min_node, i)
                if curr_weight > 0:
                    # no 'i' is connected to min_node
                    new_distance = distance[min_node] + self.get_weight(min_node, i)
                    if new_distance < distance[i]:
                        distance[i] = new_distance
                        prev[i] = min_node

        if distance[destination_node] == UNKNOWN:
            # no path found from source to destination
            return None

        # reconstruct path from source to destination
        path = []
        curr_node = destination_node
        while curr_node != -1:
            path.insert(0, curr_node)
            curr_node = prev[curr_node]

        return path
